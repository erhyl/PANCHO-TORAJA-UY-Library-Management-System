using System;
namespace Project5LMS.Helpers
{
    /// <summary>
    /// Helper methods for text manipulation and formatting
    /// Reduces duplication of common text operations
    /// </summary>
    public static class TextHelper
    {
        /// <summary>
        /// Truncates text to a maximum length, adding ellipsis if truncated
        /// </summary>
        public static string TruncateText(string text, int maxLength = Constants.MaxTitleLength)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            
            if (text.Length <= maxLength)
                return text;
            
            return text.Substring(0, maxLength - 3) + "...";
        }
        
        /// <summary>
        /// Formats text with proper capitalization
        /// </summary>
        public static string CapitalizeFirst(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            
            if (text.Length == 1)
                return text.ToUpper();
            
            return char.ToUpper(text[0]) + text.Substring(1).ToLower();
        }
        
        /// <summary>
        /// Removes extra whitespace and normalizes line breaks
        /// </summary>
        public static string NormalizeWhitespace(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            
            return System.Text.RegularExpressions.Regex.Replace(text, @"\s+", " ").Trim();
        }
    }
}
