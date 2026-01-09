using System;
using System.Collections.Generic;
using Project5LMS.Models;
using Project5LMS.Repositories;
using Project5LMS.Strategies;
using Project5LMS.Data;
using Project5LMS.Interfaces;

namespace Project5LMS.Services
{
    public class FinesService : IFinesService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IFineCalculationStrategy _fineStrategy;

        public FinesService(ITransactionRepository transactionRepository, IFineCalculationStrategy fineStrategy)
        {
            _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
            _fineStrategy = fineStrategy ?? throw new ArgumentNullException(nameof(fineStrategy));
        }

        public FinesService(DatabaseContext dbContext) 
            : this(new TransactionRepository(dbContext), new StandardFineStrategy())
        {
        }

        public decimal CalculateFine(int transactionId)
        {
            var transaction = _transactionRepository.GetById(transactionId);
            if (transaction == null || transaction.ReturnDate.HasValue)
                return 0m;

            if (DateTime.Now <= transaction.DueDate)
                return 0m;

            TimeSpan overdueTime = DateTime.Now - transaction.DueDate;
            int daysOverdue = overdueTime.Days;
            if (daysOverdue == 0 && overdueTime.TotalHours > 0)
            {
                daysOverdue = 1;
            }

            return _fineStrategy.CalculateFine(daysOverdue);
        }

        public IEnumerable<CirculationRecord> GetOverdueTransactions()
        {
            return _transactionRepository.GetOverdue();
        }

        public decimal GetTotalFinesForMember(int memberId)
        {
            decimal totalFines = 0m;
            var transactions = _transactionRepository.GetByMemberId(memberId);
            
            foreach (var transaction in transactions)
            {
                if (transaction.Fine.HasValue)
                {
                    totalFines += transaction.Fine.Value;
                }
                else if (transaction.Status == "Borrowed" && DateTime.Now > transaction.DueDate)
                {
                    totalFines += CalculateFine(transaction.TransactionID);
                }
            }
            
            return totalFines;
        }

        public bool UpdateTransactionFine(int transactionId, decimal fineAmount)
        {
            var transaction = _transactionRepository.GetById(transactionId);
            if (transaction == null)
                return false;

            transaction.Fine = fineAmount;
            return _transactionRepository.Update(transaction);
        }
    }
}
