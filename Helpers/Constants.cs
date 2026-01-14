namespace Project5LMS.Helpers
{
    /// <summary>
    /// Centralized constants to avoid magic numbers throughout the codebase
    /// </summary>
    public static class Constants
    {
        // Borrowing Constants
        public const int DefaultBorrowDays = 14;
        public const int MaxBorrowingLimit = 5;
        public const int StudentMaxBorrowings = 5;
        public const int FacultyMaxBorrowings = 10;
        public const int StaffMaxBorrowings = 7;
        public const int GuestMaxBorrowings = 3;
        
        // Fine Constants
        public const decimal DefaultFinePerDay = 0.50m;
        public const decimal MaxAllowedFineThreshold = 10.00m; // Allow small fines up to this amount
        public const decimal LostCardReplacementFee = 10.00m;
        
        // Query Limits
        public const int DefaultQueryLimit = 20; // Default limit for GetPopularBooks, GetNewArrivals, etc.
        public const int MaxQueryLimit = 100; // Maximum limit for queries
        public const int TransactionQueryLimit = 50; // Limit for transaction queries
        
        // Date Range Constants (in days)
        public const int DefaultNewArrivalsDays = 30; // Default days for new arrivals
        public const int RecentActivityDays = 30; // Days for recent activity
        public const int PopularBooksRecentDays = 30; // Days for recent popularity weighting
        public const int PopularBooksMediumDays = 90; // Days for medium popularity weighting
        
        // UI Constants
        public const int DefaultRowHeight = 50; // Default DataGridView row height
        public const int CardHeight = 100; // Default card height
        public const int MaxTitleLength = 50; // Maximum title length before truncation
        public const int ThreadSleepShort = 500; // Short sleep duration in milliseconds
        
        // Color Constants (RGB values)
        public const int ColorSuccessGreen = 34; // Success green color component
        public const int ColorSuccessGreen2 = 139;
        public const int ColorSuccessGreen3 = 34;
        public const int ColorErrorRed = 220; // Error red color component
        public const int ColorErrorRed2 = 20;
        public const int ColorErrorRed3 = 60;
        public const int ColorNeutralGray = 64; // Neutral gray color component
        
        /// <summary>
        /// Gets success green color
        /// </summary>
        public static System.Drawing.Color GetSuccessColor()
        {
            return System.Drawing.Color.FromArgb(ColorSuccessGreen, ColorSuccessGreen2, ColorSuccessGreen3);
        }
        
        /// <summary>
        /// Gets error red color
        /// </summary>
        public static System.Drawing.Color GetErrorColor()
        {
            return System.Drawing.Color.FromArgb(ColorErrorRed, ColorErrorRed2, ColorErrorRed3);
        }
        
        /// <summary>
        /// Gets neutral gray color
        /// </summary>
        public static System.Drawing.Color GetNeutralColor()
        {
            return System.Drawing.Color.FromArgb(ColorNeutralGray, ColorNeutralGray, ColorNeutralGray);
        }
    }
}