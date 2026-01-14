using System.Collections.Generic;
using Project5LMS.Models;
namespace Project5LMS.Interfaces
{
    public interface IPaymentService
    {
        bool ProcessPayment(FinePayment payment);
        bool WaiveFine(FineAdjustment adjustment);
        IEnumerable<FinePayment> GetPaymentsByMember(int memberId);
        IEnumerable<FineAdjustment> GetAdjustmentsByMember(int memberId);
    }
}