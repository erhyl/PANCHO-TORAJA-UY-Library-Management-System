using System.Collections.Generic;
using Project5LMS.Models;

namespace Project5LMS.Interfaces
{
    public interface ICirculationService
    {
        bool BorrowBook(int memberId, int bookId, int borrowDays = 7);
        bool ReturnBook(int transactionId);
        bool RenewBook(int transactionId, int additionalDays = 0);
        bool CanRenew(int transactionId);
        IEnumerable<CirculationRecord> GetMemberTransactions(int memberId);
        IEnumerable<CirculationRecord> GetOverdueTransactions();
        CirculationRecord GetActiveTransactionByBook(int bookId);
        bool CanBorrow(int memberId);
    }
}
