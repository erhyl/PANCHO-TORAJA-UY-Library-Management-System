using System;
namespace Project5LMS.Models
{
    public class BookCopy
    {
        public int CopyID { get; set; }
        public int BookID { get; set; }
        public string AccessionNumber { get; set; }
        public string Barcode { get; set; }
        public string CopyStatus { get; set; }
        public string Location { get; set; }
        public string Notes { get; set; }
        public DateTime? LastCheckedOut { get; set; }
        public DateTime? LastReturned { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsAvailable => CopyStatus?.Equals("Available", System.StringComparison.OrdinalIgnoreCase) == true;
        public bool IsBorrowed => CopyStatus?.Equals("Borrowed", System.StringComparison.OrdinalIgnoreCase) == true;
        public bool IsReserved => CopyStatus?.Equals("Reserved", System.StringComparison.OrdinalIgnoreCase) == true;
        public bool IsLost => CopyStatus?.Equals("Lost", System.StringComparison.OrdinalIgnoreCase) == true;
        public bool IsDamaged => CopyStatus?.Equals("Damaged", System.StringComparison.OrdinalIgnoreCase) == true;
        public bool IsForRepair => CopyStatus?.Equals("For Repair", System.StringComparison.OrdinalIgnoreCase) == true;
        public static BookCopy Create(int bookId, string accessionNumber)
        {
            return new BookCopy
            {
                BookID = bookId,
                AccessionNumber = accessionNumber,
                CopyStatus = "Available",
                CreatedDate = DateTime.Now
            };
        }
    }
}