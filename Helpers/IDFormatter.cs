using System;
using System.Text.RegularExpressions;

namespace Project5LMS.Helpers
{
    /// <summary>
    /// Utility class for consistent ID formatting and parsing across Admin, Staff, and Member roles
    /// </summary>
    public static class IDFormatter
    {
        // Standard formats
        private const string MEMBER_ID_FORMAT = "MEM-{0:D6}";  // MEM-000001 (6 digits)
        private const string BOOK_ID_FORMAT = "BOOK-{0:D6}";    // BOOK-000001 (6 digits)
        private const string ACCESSION_FORMAT = "ACC-{0:D6}";    // ACC-000001 (6 digits)
        private const string FINE_ID_FORMAT = "FINE-{0:D3}";     // FINE-001 (3 digits)
        private const string RECEIPT_FORMAT = "RCP-{0:yyyyMMdd}-{1:D6}"; // RCP-20240102-000001

        #region Member ID Formatting

        /// <summary>
        /// Format member ID consistently: MEM-000001 (6 digits)
        /// </summary>
        public static string FormatMemberID(int memberId)
        {
            return string.Format(MEMBER_ID_FORMAT, memberId);
        }

        /// <summary>
        /// Format member ID from string (handles various input formats)
        /// </summary>
        public static string FormatMemberID(string memberIdText)
        {
            int memberId = ParseMemberID(memberIdText);
            return memberId > 0 ? FormatMemberID(memberId) : memberIdText;
        }

        /// <summary>
        /// Parse member ID from various formats (MEM-001, M1001, 1001, etc.)
        /// </summary>
        public static int ParseMemberID(string memberIdText)
        {
            if (string.IsNullOrWhiteSpace(memberIdText))
                return 0;

            // Remove common prefixes (case-insensitive)
            string clean = memberIdText.Trim();
            if (clean.StartsWith("MEM-", StringComparison.OrdinalIgnoreCase))
                clean = clean.Substring(4);
            else if (clean.StartsWith("M", StringComparison.OrdinalIgnoreCase) && clean.Length > 1 && char.IsDigit(clean[1]))
                clean = clean.Substring(1);

            // Extract numeric part
            string numeric = Regex.Replace(clean, @"[^\d]", "");
            
            if (int.TryParse(numeric, out int memberId))
                return memberId;

            return 0;
        }

        /// <summary>
        /// Check if member ID format is valid
        /// </summary>
        public static bool IsValidMemberID(string memberIdText)
        {
            return ParseMemberID(memberIdText) > 0;
        }

        #endregion

        #region Book ID Formatting

        /// <summary>
        /// Format book ID consistently: BOOK-000001 (6 digits)
        /// </summary>
        public static string FormatBookID(int bookId)
        {
            return string.Format(BOOK_ID_FORMAT, bookId);
        }

        /// <summary>
        /// Format book ID from string
        /// </summary>
        public static string FormatBookID(string bookIdText)
        {
            int bookId = ParseBookID(bookIdText);
            return bookId > 0 ? FormatBookID(bookId) : bookIdText;
        }

        /// <summary>
        /// Parse book ID from various formats (BOOK-001, B1001, 1001, etc.)
        /// </summary>
        public static int ParseBookID(string bookIdText)
        {
            if (string.IsNullOrWhiteSpace(bookIdText))
                return 0;

            // Remove common prefixes (case-insensitive)
            string clean = bookIdText.Trim();
            if (clean.StartsWith("BOOK-", StringComparison.OrdinalIgnoreCase))
                clean = clean.Substring(5);
            else if (clean.StartsWith("B", StringComparison.OrdinalIgnoreCase) && clean.Length > 1 && char.IsDigit(clean[1]))
                clean = clean.Substring(1);

            // Extract numeric part
            string numeric = Regex.Replace(clean, @"[^\d]", "");
            
            if (int.TryParse(numeric, out int bookId))
                return bookId;

            return 0;
        }

        /// <summary>
        /// Check if book ID format is valid
        /// </summary>
        public static bool IsValidBookID(string bookIdText)
        {
            return ParseBookID(bookIdText) > 0;
        }

        #endregion

        #region Accession Number Formatting

        /// <summary>
        /// Format accession number consistently: ACC-000001 (6 digits)
        /// </summary>
        public static string FormatAccessionNumber(string accessionNo)
        {
            if (string.IsNullOrWhiteSpace(accessionNo))
                return string.Empty;

            // If already formatted, return as is
            if (accessionNo.StartsWith("ACC-", StringComparison.OrdinalIgnoreCase))
                return accessionNo;

            // Extract numeric part
            string numeric = Regex.Replace(accessionNo, @"[^\d]", "");
            
            if (int.TryParse(numeric, out int accNum))
                return string.Format(ACCESSION_FORMAT, accNum);

            return accessionNo; // Return original if can't parse
        }

        /// <summary>
        /// Parse accession number to get numeric ID
        /// </summary>
        public static int ParseAccessionNumber(string accessionNo)
        {
            if (string.IsNullOrWhiteSpace(accessionNo))
                return 0;

            // Remove ACC- prefix (case-insensitive)
            string clean = accessionNo.Trim();
            if (clean.StartsWith("ACC-", StringComparison.OrdinalIgnoreCase))
                clean = clean.Substring(4);

            // Extract numeric part
            string numeric = Regex.Replace(clean, @"[^\d]", "");
            
            if (int.TryParse(numeric, out int accNum))
                return accNum;

            return 0;
        }

        #endregion

        #region Fine ID Formatting

        /// <summary>
        /// Format fine ID consistently: FINE-001 (3 digits)
        /// </summary>
        public static string FormatFineID(int fineId)
        {
            return string.Format(FINE_ID_FORMAT, fineId);
        }

        /// <summary>
        /// Parse fine ID from string
        /// </summary>
        public static int ParseFineID(string fineIdText)
        {
            if (string.IsNullOrWhiteSpace(fineIdText))
                return 0;

            string clean = fineIdText.Trim();
            if (clean.StartsWith("FINE-", StringComparison.OrdinalIgnoreCase))
                clean = clean.Substring(5);

            string numeric = Regex.Replace(clean, @"[^\d]", "");
            
            if (int.TryParse(numeric, out int fineId))
                return fineId;

            return 0;
        }

        #endregion

        #region Receipt Number Formatting

        /// <summary>
        /// Format receipt number: RCP-20240102-000001
        /// </summary>
        public static string FormatReceiptNumber(DateTime date, int sequence)
        {
            return string.Format(RECEIPT_FORMAT, date, sequence);
        }

        /// <summary>
        /// Generate receipt number with current date
        /// </summary>
        public static string GenerateReceiptNumber(int sequence)
        {
            return FormatReceiptNumber(DateTime.Now, sequence);
        }

        #endregion

        #region Display Helpers

        /// <summary>
        /// Format member display string: "John Doe (MEM-000001)"
        /// </summary>
        public static string FormatMemberDisplay(string firstName, string lastName, int memberId)
        {
            string fullName = $"{firstName} {lastName}".Trim();
            if (string.IsNullOrWhiteSpace(fullName))
                fullName = "Unknown";
            
            return $"{fullName} ({FormatMemberID(memberId)})";
        }

        /// <summary>
        /// Format book display string: "Book Title (ACC-000001)"
        /// </summary>
        public static string FormatBookDisplay(string title, string accessionNo)
        {
            if (string.IsNullOrWhiteSpace(title))
                title = "Unknown";
            
            string formattedAccession = FormatAccessionNumber(accessionNo);
            return $"{title} ({formattedAccession})";
        }

        #endregion

        #region Transaction ID Formatting

        /// <summary>
        /// Format transaction ID consistently: TXN-001 (3 digits)
        /// </summary>
        public static string FormatTransactionID(int transactionId)
        {
            return $"TXN-{transactionId:D3}";
        }

        /// <summary>
        /// Parse transaction ID from string
        /// </summary>
        public static int ParseTransactionID(string transactionIdText)
        {
            if (string.IsNullOrWhiteSpace(transactionIdText))
                return 0;

            string clean = transactionIdText.Trim();
            if (clean.StartsWith("TXN-", StringComparison.OrdinalIgnoreCase))
                clean = clean.Substring(4);

            string numeric = Regex.Replace(clean, @"[^\d]", "");
            
            if (int.TryParse(numeric, out int transactionId))
                return transactionId;

            return 0;
        }

        #endregion

        #region Reservation ID Formatting

        /// <summary>
        /// Format reservation ID consistently: RES-001 (3 digits)
        /// </summary>
        public static string FormatReservationID(int reservationId)
        {
            return $"RES-{reservationId:D3}";
        }

        /// <summary>
        /// Parse reservation ID from string
        /// </summary>
        public static int ParseReservationID(string reservationIdText)
        {
            if (string.IsNullOrWhiteSpace(reservationIdText))
                return 0;

            string clean = reservationIdText.Trim();
            if (clean.StartsWith("RES-", StringComparison.OrdinalIgnoreCase))
                clean = clean.Substring(4);

            string numeric = Regex.Replace(clean, @"[^\d]", "");
            
            if (int.TryParse(numeric, out int reservationId))
                return reservationId;

            return 0;
        }

        #endregion
    }
}

