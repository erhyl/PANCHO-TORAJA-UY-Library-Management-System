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
            
            // Removed check for active transaction by BookID - this was blocking multiple copies
            // Availability is already checked via book.IsAvailable (Available > 0)
            // Multiple copies of the same book (same BookID) can be borrowed independently
            // as long as Available count > 0
            
            var dbContext = ServiceFactory.GetDbContext();
            try
            {
                dbContext.ExecuteInTransaction((conn, trans) =>
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
                    // Update book availability - ensure it doesn't go below 0
                    string updateQuery = "UPDATE Books SET Available = GREATEST(0, Available - 1) WHERE AccessionNo = @AccessionNo AND Available > 0";
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, conn, trans))
                    {
                        cmd.Parameters.AddWithValue("@AccessionNo", book.AccessionNo);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            // Book is not available, throw exception to rollback transaction
                            throw new InvalidOperationException("Book is not available for borrowing.");
                        }
                    }
                    
                    // Update BookCopies status to 'Borrowed' for an available copy
                    // Find an available copy for this book and mark it as borrowed
                    string updateCopyQuery = @"UPDATE BookCopies 
                                              SET CopyStatus = 'Borrowed', 
                                                  LastCheckedOut = @LastCheckedOut,
                                                  ModifiedDate = @ModifiedDate
                                              WHERE BookID = @BookID 
                                              AND CopyStatus = 'Available' 
                                              LIMIT 1";
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(updateCopyQuery, conn, trans))
                    {
                        cmd.Parameters.AddWithValue("@BookID", bookId);
                        cmd.Parameters.AddWithValue("@LastCheckedOut", DateTime.Now);
                        cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                });
                return true;
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
                dbContext.ExecuteInTransaction((conn, trans) =>
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
                    // Update book availability - ensure it doesn't exceed TotalCopies
                    string updateBookQuery = "UPDATE Books SET Available = LEAST(TotalCopies, Available + 1) WHERE AccessionNo = @AccessionNo AND Available < TotalCopies";
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(updateBookQuery, conn, trans))
                    {
                        cmd.Parameters.AddWithValue("@AccessionNo", book.AccessionNo);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            // Book availability is already at max, but still allow return
                            // This might happen if there's a data inconsistency
                            System.Diagnostics.Debug.WriteLine($"Warning: Book {book.AccessionNo} availability already at max during return");
                        }
                    }
                    
                    // Update BookCopies status back to 'Available' when book is returned
                    string updateCopyQuery = @"UPDATE BookCopies 
                                              SET CopyStatus = 'Available', 
                                                  LastReturned = @LastReturned,
                                                  ModifiedDate = @ModifiedDate
                                              WHERE BookID = @BookID 
                                              AND CopyStatus = 'Borrowed' 
                                              LIMIT 1";
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(updateCopyQuery, conn, trans))
                    {
                        cmd.Parameters.AddWithValue("@BookID", transaction.BookID);
                        cmd.Parameters.AddWithValue("@LastReturned", DateTime.Now);
                        cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                });
                return true;
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
            
            // Get member type-specific borrowing limit
            var privileges = MemberTypePrivileges.GetDefaultPrivileges(member.Type);
            int maxBorrowingLimit = privileges.MaxBooksAllowed;
            
            int activeBorrowings = _memberRepository.GetActiveBorrowingCount(memberId);
            return activeBorrowings < maxBorrowingLimit;
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