using System;
using System.Collections.Generic;
using Project5LMS.Models;
namespace Project5LMS.Repositories
{
    public interface ITransactionRepository
    {
        CirculationRecord GetById(int transactionId);
        CirculationRecord GetActiveByBookId(int bookId);
        IEnumerable<CirculationRecord> GetByMemberId(int memberId);
        IEnumerable<CirculationRecord> GetByStatus(string status);
        IEnumerable<CirculationRecord> GetOverdue();
        bool Add(CirculationRecord transaction);
        bool Update(CirculationRecord transaction);
        bool Delete(int transactionId);
        decimal CalculateFine(int transactionId);
    }
}