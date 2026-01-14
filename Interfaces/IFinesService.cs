using System;
using System.Collections.Generic;
using Project5LMS.Models;
namespace Project5LMS.Interfaces
{
    public interface IFinesService
    {
        decimal CalculateFine(int transactionId);
        decimal CalculateFine(int transactionId, int memberId);
        IEnumerable<CirculationRecord> GetOverdueTransactions();
        decimal GetTotalFinesForMember(int memberId);
        bool UpdateTransactionFine(int transactionId, decimal fineAmount);
    }
}