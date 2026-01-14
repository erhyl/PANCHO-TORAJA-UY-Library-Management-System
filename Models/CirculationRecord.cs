using System;
namespace Project5LMS.Models
{
    public class CirculationRecord
    {
        public int TransactionID { get; set; }
        public int MemberID { get; set; }
        public int BookID { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Status { get; set; }
        public decimal? Fine { get; set; }
        public string TransactionType { get; set; }
        public int RenewalCount { get; set; }
    }
}