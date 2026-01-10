using System;
using System.Text;

namespace Project5LMS.Helpers
{
    /// <summary>
    /// Utility class for generating barcodes for books
    /// </summary>
    public static class BarcodeGenerator
    {
        /// <summary>
        /// Generate a barcode from accession number
        /// </summary>
        public static string GenerateFromAccession(string accessionNumber)
        {
            if (string.IsNullOrWhiteSpace(accessionNumber))
                return GenerateRandom();

            // Remove common prefixes and format
            string clean = accessionNumber.Replace("ACC-", "").Replace("-", "").Trim();
            
            // Pad to ensure minimum length
            while (clean.Length < 8)
            {
                clean = "0" + clean;
            }

            return clean;
        }

        /// <summary>
        /// Generate a barcode from ISBN
        /// </summary>
        public static string GenerateFromISBN(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
                return GenerateRandom();

            // Remove hyphens and spaces
            string clean = isbn.Replace("-", "").Replace(" ", "").Trim();
            
            // Use last 12 digits for barcode
            if (clean.Length > 12)
            {
                clean = clean.Substring(clean.Length - 12);
            }

            return clean.PadLeft(12, '0');
        }

        /// <summary>
        /// Generate a random barcode
        /// </summary>
        public static string GenerateRandom()
        {
            Random random = new Random();
            StringBuilder sb = new StringBuilder();
            
            // Generate 12-digit barcode
            for (int i = 0; i < 12; i++)
            {
                sb.Append(random.Next(0, 10));
            }
            
            return sb.ToString();
        }

        /// <summary>
        /// Generate barcode with prefix (e.g., "LIB" for library)
        /// </summary>
        public static string GenerateWithPrefix(string prefix, string identifier)
        {
            if (string.IsNullOrWhiteSpace(prefix))
                prefix = "LIB";

            string clean = identifier?.Replace("-", "").Replace(" ", "").Trim() ?? "";
            
            // Generate numeric part
            if (string.IsNullOrWhiteSpace(clean))
            {
                clean = GenerateRandom();
            }
            else
            {
                // Extract numeric part
                StringBuilder numeric = new StringBuilder();
                foreach (char c in clean)
                {
                    if (char.IsDigit(c))
                        numeric.Append(c);
                }
                
                if (numeric.Length == 0)
                    clean = GenerateRandom();
                else
                    clean = numeric.ToString().PadLeft(8, '0');
            }

            return $"{prefix}{clean}";
        }

        /// <summary>
        /// Validate barcode format
        /// </summary>
        public static bool IsValid(string barcode)
        {
            if (string.IsNullOrWhiteSpace(barcode))
                return false;

            // Basic validation: should be alphanumeric and reasonable length
            if (barcode.Length < 6 || barcode.Length > 20)
                return false;

            // Check if contains valid characters
            foreach (char c in barcode)
            {
                if (!char.IsLetterOrDigit(c) && c != '-')
                    return false;
            }

            return true;
        }
    }
}

