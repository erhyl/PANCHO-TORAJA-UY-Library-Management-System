using System;

namespace Project5LMS.Models
{
    /// <summary>
    /// Defines borrowing privileges and limits for different member types
    /// </summary>
    public class MemberTypePrivileges
    {
        public string MemberType { get; set; }
        
        // Borrowing Limits
        public int MaxBooksAllowed { get; set; }
        public int BorrowingPeriodDays { get; set; }
        public int RenewalLimit { get; set; }
        public int ReservationLimit { get; set; }
        
        // Fine Rates
        public decimal FineRatePerDay { get; set; }
        public decimal MaxFineCap { get; set; }
        
        // Privileges
        public bool CanReserve { get; set; }
        public bool CanRenew { get; set; }
        public bool CanBorrowReference { get; set; }
        
        /// <summary>
        /// Get default privileges for a member type
        /// </summary>
        public static MemberTypePrivileges GetDefaultPrivileges(string memberType)
        {
            if (string.IsNullOrWhiteSpace(memberType))
                memberType = "Student";

            string type = memberType.ToLower();

            if (type.Contains("faculty"))
            {
                return new MemberTypePrivileges
                {
                    MemberType = "Faculty",
                    MaxBooksAllowed = 10,
                    BorrowingPeriodDays = 30,
                    RenewalLimit = 3,
                    ReservationLimit = 5,
                    FineRatePerDay = 0.50m,
                    MaxFineCap = 50.00m,
                    CanReserve = true,
                    CanRenew = true,
                    CanBorrowReference = true
                };
            }
            else if (type.Contains("staff"))
            {
                return new MemberTypePrivileges
                {
                    MemberType = "Staff",
                    MaxBooksAllowed = 7,
                    BorrowingPeriodDays = 21,
                    RenewalLimit = 2,
                    ReservationLimit = 3,
                    FineRatePerDay = 0.50m,
                    MaxFineCap = 50.00m,
                    CanReserve = true,
                    CanRenew = true,
                    CanBorrowReference = false
                };
            }
            else if (type.Contains("student"))
            {
                return new MemberTypePrivileges
                {
                    MemberType = "Student",
                    MaxBooksAllowed = 5,
                    BorrowingPeriodDays = 14,
                    RenewalLimit = 2,
                    ReservationLimit = 3,
                    FineRatePerDay = 0.50m,
                    MaxFineCap = 25.00m,
                    CanReserve = true,
                    CanRenew = true,
                    CanBorrowReference = false
                };
            }
            else // Guest
            {
                return new MemberTypePrivileges
                {
                    MemberType = "Guest",
                    MaxBooksAllowed = 3,
                    BorrowingPeriodDays = 7,
                    RenewalLimit = 1,
                    ReservationLimit = 1,
                    FineRatePerDay = 1.00m,
                    MaxFineCap = 25.00m,
                    CanReserve = false,
                    CanRenew = true,
                    CanBorrowReference = false
                };
            }
        }
    }
}

