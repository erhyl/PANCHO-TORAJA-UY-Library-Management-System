using System;

namespace Project5LMS.Models
{
    /// <summary>
    /// Represents an adjustment/waiver made to a fine with audit trail
    /// </summary>
    public class FineAdjustment
    {
        public int AdjustmentID { get; set; }
        public int TransactionID { get; set; }
        public int MemberID { get; set; }
        public decimal OriginalAmount { get; set; }
        public decimal AdjustedAmount { get; set; }
        public decimal AdjustmentAmount { get; set; }
        public string AdjustmentType { get; set; } // Waiver, Discount, Correction
        public string Reason { get; set; }
        public string AdjustedBy { get; set; } // User who made the adjustment
        public DateTime AdjustmentDate { get; set; }
        public string ApprovalRequired { get; set; } // Yes/No
        public string ApprovedBy { get; set; }
        public DateTime? ApprovalDate { get; set; }
    }
}

