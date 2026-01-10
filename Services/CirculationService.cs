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

            // Use transaction management for atomic operation
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

                    // Insert transaction
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

                    // Update book availability
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

            // Get the book to retrieve its AccessionNo
            var book = _bookRepository.GetById(transaction.BookID);
            if (book == null || string.IsNullOrWhiteSpace(book.AccessionNo))
                return false;

            // Use transaction management for atomic operation
            var dbContext = ServiceFactory.GetDbContext();
            try
            {
                return dbContext.ExecuteInTransaction((conn, trans) =>
                {
                    // Update transaction
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

                    // Update book availability
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

        /// <summary>
        /// Renew a borrowed book
        /// </summary>
        /// <param name="transactionId">The transaction ID to renew</param>
        /// <param name="additionalDays">Number of days to extend (defaults to original borrowing period)</param>
        /// <returns>True if renewal successful, false otherwise</returns>
        public bool RenewBook(int transactionId, int additionalDays = 0)
        {
            var transaction = _transactionRepository.GetById(transactionId);
            if (transaction == null || transaction.Status != "Borrowed")
                return false;

            // Check if member is still active
            var member = _memberRepository.GetById(transaction.MemberID);
            if (member == null || !member.IsActive || member.IsExpired)
                return false;

            // Check if book has reservations (if reservation system is implemented)
            // For now, we'll allow renewal if no active reservations

            // Get member type privileges
            var privileges = MemberTypePrivileges.GetDefaultPrivileges(member.Type);
            
            // Check renewal limit
            int maxRenewals = privileges.RenewalLimit;
            if (transaction.RenewalCount >= maxRenewals)
            {
                System.Diagnostics.Debug.WriteLine($"Renewal limit reached: {transaction.RenewalCount}/{maxRenewals}");
                return false;
            }

            // Calculate new due date
            if (additionalDays <= 0)
            {
                additionalDays = privileges.BorrowingPeriodDays;
            }

            DateTime newDueDate = transaction.DueDate.AddDays(additionalDays);
            
            // Update transaction
            transaction.DueDate = newDueDate;
            transaction.RenewalCount = (transaction.RenewalCount) + 1;

            return _transactionRepository.Update(transaction);
        }

        /// <summary>
        /// Check if a book can be renewed
        /// </summary>
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

            // Check if overdue (some systems don't allow renewal of overdue books)
            if (DateTime.Now > transaction.DueDate)
            {
                // Check if there are unpaid fines that exceed threshold
                // This would need to be implemented with fine checking
            }

            // Check actual renewal count vs limit
            int maxRenewals = privileges.RenewalLimit;
            return transaction.RenewalCount < maxRenewals;
        }
    }
}
