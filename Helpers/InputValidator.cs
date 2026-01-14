using System;
using System.Text.RegularExpressions;
namespace Project5LMS.Helpers
{
    public static class InputValidator
    {
        private static readonly Regex EmailRegex = new Regex(
            @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            if (!EmailRegex.IsMatch(email))
                return false;
            if (email.Length > 254)
                return false;
            if (email.Contains("..") || email.StartsWith(".") || email.EndsWith("."))
                return false;
            return true;
        }
        public static PasswordStrength GetPasswordStrength(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return PasswordStrength.None;
            if (password.Length < 6)
                return PasswordStrength.Weak;
            bool hasUpper = false;
            bool hasLower = false;
            bool hasDigit = false;
            bool hasSpecial = false;
            foreach (char c in password)
            {
                if (char.IsUpper(c)) hasUpper = true;
                if (char.IsLower(c)) hasLower = true;
                if (char.IsDigit(c)) hasDigit = true;
                if (!char.IsLetterOrDigit(c)) hasSpecial = true;
            }
            int strength = 0;
            if (hasUpper) strength++;
            if (hasLower) strength++;
            if (hasDigit) strength++;
            if (hasSpecial) strength++;
            if (password.Length >= 8) strength++;
            if (strength <= 2)
                return PasswordStrength.Weak;
            if (strength <= 3)
                return PasswordStrength.Medium;
            return PasswordStrength.Strong;
        }
        public static string SanitizeInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;
            return input.Trim()
                .Replace("'", "")
                .Replace("\"", "")
                .Replace(";", "")
                .Replace("--", "");
        }
        public static bool ContainsSqlInjection(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;
            string lowerInput = input.ToLower();
            string[] dangerousPatterns = {
                "union select",
                "drop table",
                "delete from",
                "insert into",
                "update set",
                "exec(",
                "execute(",
                "script>",
                "<script"
            };
            foreach (var pattern in dangerousPatterns)
            {
                if (lowerInput.Contains(pattern))
                    return true;
            }
            return false;
        }
    }
    public enum PasswordStrength
    {
        None,
        Weak,
        Medium,
        Strong
    }
}