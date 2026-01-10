using System;
using System.Text;
namespace Project5LMS.Helpers
{
    public static class BarcodeGenerator
    {
        public static string GenerateFromAccession(string accessionNumber)
        {
            if (string.IsNullOrWhiteSpace(accessionNumber))
                return GenerateRandom();
            string clean = accessionNumber.Replace("ACC-", "").Replace("-", "").Trim();
            while (clean.Length < 8)
            {
                clean = "0" + clean;
            }
            return clean;
        }
        public static string GenerateFromISBN(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
                return GenerateRandom();
            string clean = isbn.Replace("-", "").Replace(" ", "").Trim();
            if (clean.Length > 12)
            {
                clean = clean.Substring(clean.Length - 12);
            }
            return clean.PadLeft(12, '0');
        }
        public static string GenerateRandom()
        {
            Random random = new Random();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 12; i++)
            {
                sb.Append(random.Next(0, 10));
            }
            return sb.ToString();
        }
        public static string GenerateWithPrefix(string prefix, string identifier)
        {
            if (string.IsNullOrWhiteSpace(prefix))
                prefix = "LIB";
            string clean = identifier?.Replace("-", "").Replace(" ", "").Trim() ?? "";
            if (string.IsNullOrWhiteSpace(clean))
            {
                clean = GenerateRandom();
            }
            else
            {
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
        public static bool IsValid(string barcode)
        {
            if (string.IsNullOrWhiteSpace(barcode))
                return false;
            if (barcode.Length < 6 || barcode.Length > 20)
                return false;
            foreach (char c in barcode)
            {
                if (!char.IsLetterOrDigit(c) && c != '-')
                    return false;
            }
            return true;
        }
    }
}