using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Project5LMS.Models;
using Project5LMS.Repositories;
using Project5LMS.Data;
using Project5LMS.Helpers;
using Project5LMS.Interfaces;
namespace Project5LMS.Services
{
    public class CirculationService : ICirculationService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMemberRepository _memberRepository;
        public CirculationService(
            ITransactionRepository transactionRepository,
            IBookRepository bookRepository,
            IMemberRepository memberRepository)
        {
            _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
        }
        public bool BorrowBook(int memberId, int bookId, int borrowDays = Constants.DefaultBorrowDays)
        {
            if (!_memberRepository.Exists(memberId))
                return false;
            var book = _bookRepository.GetById(bookId);
            if (book == null || !book.IsAvailable || string.IsNullOrWhiteSpace(book.AccessionNo))
                return false;
            var activeTransaction = _transactionRepository.GetActiveByBookId(bookId);
            if (activeTransaction != null)
                return false;
            var dbContext = ServiceFactory.GetDbContext();
            try
            {
                return dbContext.ExecuteInTransaction((conn, trans) =>
                {
                    var transaction = new CirculationRecord
                    {
                        MemberID = memberId,
                        BookID = bookId,
                        BorrowDate = DateTime.Now,
                        DueDate = DateTime.Now.AddDays(borrowDays),
                        Status = "Borrowed",
                        TransactionType = "Borrow"
                    };
                    string insertQuery = @"INSERT INTO Transactions (MemberID, BookID, BorrowDate, DueDate, Status, TransactionType)
                                        VALUES (@MemberID, @BookID, @BorrowDate, @DueDate, @Status, @TransactionType)";
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(insertQuery, conn, trans))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", transaction.MemberID);
                        cmd.Parameters.AddWithValue("@BookID", transaction.BookID);
                        cmd.Parameters.AddWithValue("@BorrowDate", transaction.BorrowDate);
                        cmd.Parameters.AddWithValue("@DueDate", transaction.DueDate);
                        cmd.Parameters.AddWithValue("@Status", transaction.Status);
                        cmd.Parameters.AddWithValue("@TransactionType", transaction.TransactionType);
                        cmd.ExecuteNonQuery();
                    }
                    string updateQuery = "UPDATE Books SET Available = Available - 1 WHERE AccessionNo = @AccessionNo";
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, conn, trans))
                    {
                        cmd.Parameters.AddWithValue("@AccessionNo", book.AccessionNo);
                        cmd.ExecuteNonQuery();
                    }
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error borrowing book with transaction: {ex.Message}");
                return false;
            }
        }
        public bool ReturnBook(int transactionId)
        {
            var transaction = _transactionRepository.GetById(transactionId);
            if (transaction == null || transaction.Status != "Borrowed")
                return false;
            var book = _bookRepository.GetById(transaction.BookID);
            if (book == null || string.IsNullOrWhiteSpace(book.AccessionNo))
                return false;
            var dbContext = ServiceFactory.GetDbContext();
            try
            {
                return dbContext.ExecuteInTransaction((conn, trans) =>
                {
                    string updateQuery = @"UPDATE Transactions
                                        SET ReturnDate = @ReturnDate, Status = @Status
                                        WHERE TransactionID = @TransactionID";
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, conn, trans))
                    {
                        cmd.Parameters.AddWithValue("@TransactionID", transactionId);
                        cmd.Parameters.AddWithValue("@ReturnDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Status", "Returned");
                        cmd.ExecuteNonQuery();
                    }
                    string updateBookQuery = "UPDATE Books SET Available = Available + 1 WHERE AccessionNo = @AccessionNo";
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(updateBookQuery, conn, trans))
                    {
                        cmd.Parameters.AddWithValue("@AccessionNo", book.AccessionNo);
                        cmd.ExecuteNonQuery();
                    }
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error returning book with transaction: {ex.Message}");
                return false;
            }
        }
        public IEnumerable<CirculationRecord> GetMemberTransactions(int memberId)
        {
            return _transactionRepository.GetByMemberId(memberId);
        }
        public IEnumerable<CirculationRecord> GetOverdueTransactions()
        {
            return _transactionRepository.GetOverdue();
        }
        public CirculationRecord GetActiveTransactionByBook(int bookId)
        {
            return _transactionRepository.GetActiveByBookId(bookId);
        }
        public bool CanBorrow(int memberId)
        {
            var member = _memberRepository.GetById(memberId);
            if (member == null || !member.IsActive || member.IsExpired)
                return false;
            int activeBorrowings = _memberRepository.GetActiveBorrowingCount(memberId);
            return activeBorrowings < Constants.MaxBorrowingLimit;
        }
        public bool RenewBook(int transactionId, int additionalDays = 0)
        {
            var transaction = _transactionRepository.GetById(transactionId);
            if (transaction == null || transaction.Status != "Borrowed")
                return false;
            var member = _memberRepository.GetById(transaction.MemberID);
            if (member == null || !member.IsActive || member.IsExpired)
                return false;
            var privileges = MemberTypePrivileges.GetDefaultPrivileges(member.Type);
            int maxRenewals = privileges.RenewalLimit;
            if (transaction.RenewalCount >= maxRenewals)
            {
                System.Diagnostics.Debug.WriteLine($"Renewal limit reached: {transaction.RenewalCount}/{maxRenewals}");
                return false;
            }
            if (additionalDays <= 0)
            {
                additionalDays = privileges.BorrowingPeriodDays;
            }
            DateTime newDueDate = transaction.DueDate.AddDays(additionalDays);
            transaction.DueDate = newDueDate;
            transaction.RenewalCount = (transaction.RenewalCount) + 1;
            return _transactionRepository.Update(transaction);
        }
        public bool CanRenew(int transactionId)
        {
            var transaction = _transactionRepository.GetById(transactionId);
            if (transaction == null || transaction.Status != "Borrowed")
                return false;
            var member = _memberRepository.GetById(transaction.MemberID);
            if (member == null || !member.IsActive || member.IsExpired)
                return false;
            var privileges = MemberTypePrivileges.GetDefaultPrivileges(member.Type);
            if (!privileges.CanRenew)
                return false;
            if (DateTime.Now > transaction.DueDate)
            {
            }
            int maxRenewals = privileges.RenewalLimit;
            return transaction.RenewalCount < maxRenewals;
        }
    }
}