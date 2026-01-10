using System;

namespace Project5LMS.Models
{
    public class FineAdjustment
    {
        public int AdjustmentID { get; set; }
        public int TransactionID { get; set; }
        public int MemberID { get; set; }
        public decimal OriginalAmount { get; set; }
        public decimal AdjustedAmount { get; set; }
        public decimal AdjustmentAmount { get; set; }
        public string AdjustmentType { get; set; }
        public string Reason { get; set; }
        public string AdjustedBy { get; set; }
        public DateTime AdjustmentDate { get; set; }
        public string ApprovalRequired { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? ApprovalDate { get; set; }
    }
}
