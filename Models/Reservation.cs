using System;

namespace Project5LMS.Models
{
    /// <summary>
    /// Represents a book reservation by a member
    /// </summary>
    public class Reservation
    {
        public int ReservationID { get; set; }
        public int MemberID { get; set; }
        public int BookID { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime? PickupDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Status { get; set; } // Pending, Active, Ready, Fulfilled, Cancelled, Expired
        public int Priority { get; set; }
        public DateTime? FulfilledDate { get; set; }
        public string Notes { get; set; }

        // Navigation properties (for display purposes)
        public string MemberName { get; set; }
        public string BookTitle { get; set; }
        public string AccessionNumber { get; set; }

        public bool IsPending => Status?.Equals("Pending", StringComparison.OrdinalIgnoreCase) == true;
        public bool IsActive => Status?.Equals("Active", StringComparison.OrdinalIgnoreCase) == true;
        public bool IsReady => Status?.Equals("Ready", StringComparison.OrdinalIgnoreCase) == true;
        public bool IsFulfilled => Status?.Equals("Fulfilled", StringComparison.OrdinalIgnoreCase) == true;
        public bool IsCancelled => Status?.Equals("Cancelled", StringComparison.OrdinalIgnoreCase) == true;
        public bool IsExpired => Status?.Equals("Expired", StringComparison.OrdinalIgnoreCase) == true || 
                                 (ExpiryDate.HasValue && ExpiryDate.Value < DateTime.Now && !IsFulfilled);

        public bool IsValid()
        {
            return MemberID > 0 && BookID > 0 && ReservationDate <= DateTime.Now;
        }

        public static Reservation Create(int memberId, int bookId, int priority = 0)
        {
            var now = DateTime.Now;
            return new Reservation
            {
                MemberID = memberId,
                BookID = bookId,
                ReservationDate = now,
                PickupDate = now.AddDays(7),
                ExpiryDate = now.AddDays(7),
                Status = "Pending",
                Priority = priority
            };
        }
    }
}

