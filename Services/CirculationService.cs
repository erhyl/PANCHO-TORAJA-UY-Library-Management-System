using System;
using System.Collections.Generic;
using Project5LMS.Models;
using Project5LMS.Repositories;
using Project5LMS.Data;
using Project5LMS.Helpers;

namespace Project5LMS.Services
{
    public class CirculationService
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

        public CirculationService(DatabaseContext dbContext)
            : this(
                new TransactionRepository(dbContext),
                new BookRepository(dbContext),
                new MemberRepository(dbContext))
        {
        }

        public bool BorrowBook(int memberId, int bookId, int borrowDays = Constants.DefaultBorrowDays)
        {
            if (!_memberRepository.Exists(memberId))
                return false;

            var book = _bookRepository.GetById(bookId);
            if (book == null || !book.IsAvailable)
                return false;

            var activeTransaction = _transactionRepository.GetActiveByBookId(bookId);
            if (activeTransaction != null)
                return false;

            var transaction = new CirculationRecord
            {
                MemberID = memberId,
                BookID = bookId,
                BorrowDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(borrowDays),
                Status = "Borrowed",
                TransactionType = "Borrow"
            };

            if (!_transactionRepository.Add(transaction))
                return false;

            return _bookRepository.UpdateAvailability(bookId, -1);
        }

        public bool ReturnBook(int transactionId)
        {
            var transaction = _transactionRepository.GetById(transactionId);
            if (transaction == null || transaction.Status != "Borrowed")
                return false;

            transaction.ReturnDate = DateTime.Now;
            transaction.Status = "Returned";

            if (!_transactionRepository.Update(transaction))
                return false;

            return _bookRepository.UpdateAvailability(transaction.BookID, 1);
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
    }
}
