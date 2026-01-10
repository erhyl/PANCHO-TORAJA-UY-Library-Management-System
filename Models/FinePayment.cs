using System;

namespace Project5LMS.Models
{
    /// <summary>
    /// Represents a payment made towards fines/penalties
    /// </summary>
    public class FinePayment
    {
        public int PaymentID { get; set; }
        public int TransactionID { get; set; }
        public int MemberID { get; set; }
        public decimal AmountPaid { get; set; }
        public string PaymentMode { get; set; } // Cash, Online, Check, etc.
        public DateTime PaymentDate { get; set; }
        public string ReceiptNumber { get; set; }
        public string ProcessedBy { get; set; } // User who processed the payment
        public string Notes { get; set; }
        public bool IsWaived { get; set; }
        public string WaiverReason { get; set; }
        public string WaivedBy { get; set; }
        public DateTime? WaiverDate { get; set; }
    }
}

