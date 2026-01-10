using System;
using System.Text.RegularExpressions;
namespace Project5LMS.Helpers
{
    public static class IDFormatter
    {
        private const string MEMBER_ID_FORMAT = "MEM-{0:D6}";
        private const string BOOK_ID_FORMAT = "BOOK-{0:D6}";
        private const string ACCESSION_FORMAT = "ACC-{0:D6}";
        private const string FINE_ID_FORMAT = "FINE-{0:D3}";
        private const string RECEIPT_FORMAT = "RCP-{0:yyyyMMdd}-{1:D6}";
        #region Member ID Formatting
        public static string FormatMemberID(int memberId)
        {
            return string.Format(MEMBER_ID_FORMAT, memberId);
        }
        public static string FormatMemberID(string memberIdText)
        {
            int memberId = ParseMemberID(memberIdText);
            return memberId > 0 ? FormatMemberID(memberId) : memberIdText;
        }
        public static int ParseMemberID(string memberIdText)
        {
            if (string.IsNullOrWhiteSpace(memberIdText))
                return 0;
            string clean = memberIdText.Trim();
            if (clean.StartsWith("MEM-", StringComparison.OrdinalIgnoreCase))
                clean = clean.Substring(4);
            else if (clean.StartsWith("M", StringComparison.OrdinalIgnoreCase) && clean.Length > 1 && char.IsDigit(clean[1]))
                clean = clean.Substring(1);
            string numeric = Regex.Replace(clean, @"[^\d]", "");
            if (int.TryParse(numeric, out int memberId))
                return memberId;
            return 0;
        }
        public static bool IsValidMemberID(string memberIdText)
        {
            return ParseMemberID(memberIdText) > 0;
        }
        #endregion
        #region Book ID Formatting
        public static string FormatBookID(int bookId)
        {
            return string.Format(BOOK_ID_FORMAT, bookId);
        }
        public static string FormatBookID(string bookIdText)
        {
            int bookId = ParseBookID(bookIdText);
            return bookId > 0 ? FormatBookID(bookId) : bookIdText;
        }
        public static int ParseBookID(string bookIdText)
        {
            if (string.IsNullOrWhiteSpace(bookIdText))
                return 0;
            string clean = bookIdText.Trim();
            if (clean.StartsWith("BOOK-", StringComparison.OrdinalIgnoreCase))
                clean = clean.Substring(5);
            else if (clean.StartsWith("B", StringComparison.OrdinalIgnoreCase) && clean.Length > 1 && char.IsDigit(clean[1]))
                clean = clean.Substring(1);
            string numeric = Regex.Replace(clean, @"[^\d]", "");
            if (int.TryParse(numeric, out int bookId))
                return bookId;
            return 0;
        }
        public static bool IsValidBookID(string bookIdText)
        {
            return ParseBookID(bookIdText) > 0;
        }
        #endregion
        #region Accession Number Formatting
        public static string FormatAccessionNumber(string accessionNo)
        {
            if (string.IsNullOrWhiteSpace(accessionNo))
                return string.Empty;
            if (accessionNo.StartsWith("ACC-", StringComparison.OrdinalIgnoreCase))
                return accessionNo;
            string numeric = Regex.Replace(accessionNo, @"[^\d]", "");
            if (int.TryParse(numeric, out int accNum))
                return string.Format(ACCESSION_FORMAT, accNum);
            return accessionNo;
        }
        public static int ParseAccessionNumber(string accessionNo)
        {
            if (string.IsNullOrWhiteSpace(accessionNo))
                return 0;
            string clean = accessionNo.Trim();
            if (clean.StartsWith("ACC-", StringComparison.OrdinalIgnoreCase))
                clean = clean.Substring(4);
            string numeric = Regex.Replace(clean, @"[^\d]", "");
            if (int.TryParse(numeric, out int accNum))
                return accNum;
            return 0;
        }
        #endregion
        #region Fine ID Formatting
        public static string FormatFineID(int fineId)
        {
            return string.Format(FINE_ID_FORMAT, fineId);
        }
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
        public static string FormatReceiptNumber(DateTime date, int sequence)
        {
            return string.Format(RECEIPT_FORMAT, date, sequence);
        }
        public static string GenerateReceiptNumber(int sequence)
        {
            return FormatReceiptNumber(DateTime.Now, sequence);
        }
        #endregion
        #region Display Helpers
        public static string FormatMemberDisplay(string firstName, string lastName, int memberId)
        {
            string fullName = $"{firstName} {lastName}".Trim();
            if (string.IsNullOrWhiteSpace(fullName))
                fullName = "Unknown";
            return $"{fullName} ({FormatMemberID(memberId)})";
        }
        public static string FormatBookDisplay(string title, string accessionNo)
        {
            if (string.IsNullOrWhiteSpace(title))
                title = "Unknown";
            string formattedAccession = FormatAccessionNumber(accessionNo);
            return $"{title} ({formattedAccession})";
        }
        #endregion
        #region Transaction ID Formatting
        public static string FormatTransactionID(int transactionId)
        {
            return $"TXN-{transactionId:D3}";
        }
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
        public static string FormatReservationID(int reservationId)
        {
            return $"RES-{reservationId:D3}";
        }
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