using System;
using System.Collections.Generic;
using System.Linq;
using Project5LMS.Services;

namespace Project5LMS.Helpers
{
    /// <summary>
    /// Helper class for managing Author, Publisher, and Category metadata
    /// While these are stored as strings in the database, this helper provides
    /// validation, normalization, and management functions
    /// </summary>
    public static class CatalogMetadataHelper
    {
        /// <summary>
        /// Normalize author name (trim, title case, handle multiple authors)
        /// </summary>
        public static string NormalizeAuthor(string author)
        {
            if (string.IsNullOrWhiteSpace(author))
                return string.Empty;

            // Split by common separators (comma, semicolon, and)
            var authors = author.Split(new[] { ",", ";", "&", " and " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(a => a.Trim())
                .Where(a => !string.IsNullOrWhiteSpace(a))
                .ToList();

            if (authors.Count == 0)
                return string.Empty;

            // For single author, apply title case
            if (authors.Count == 1)
            {
                return ToTitleCase(authors[0]);
            }

            // For multiple authors, join with comma
            return string.Join(", ", authors.Select(ToTitleCase));
        }

        /// <summary>
        /// Normalize publisher name
        /// </summary>
        public static string NormalizePublisher(string publisher)
        {
            if (string.IsNullOrWhiteSpace(publisher))
                return string.Empty;

            return ToTitleCase(publisher.Trim());
        }

        /// <summary>
        /// Normalize category name
        /// </summary>
        public static string NormalizeCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
                return "Uncategorized";

            return ToTitleCase(category.Trim());
        }

        /// <summary>
        /// Validate author name format
        /// </summary>
        public static bool IsValidAuthor(string author)
        {
            if (string.IsNullOrWhiteSpace(author))
                return false;

            // Author should contain at least one letter
            return author.Any(char.IsLetter);
        }

        /// <summary>
        /// Validate publisher name
        /// </summary>
        public static bool IsValidPublisher(string publisher)
        {
            if (string.IsNullOrWhiteSpace(publisher))
                return false;

            return publisher.Any(char.IsLetter);
        }

        /// <summary>
        /// Get all unique authors from catalog
        /// </summary>
        public static IEnumerable<string> GetAllAuthors()
        {
            try
            {
                var bookService = ServiceFactory.CreateBookService();
                return bookService.GetAllAuthors();
            }
            catch
            {
                return new List<string>();
            }
        }

        /// <summary>
        /// Get all unique publishers from catalog
        /// </summary>
        public static IEnumerable<string> GetAllPublishers()
        {
            try
            {
                var bookService = ServiceFactory.CreateBookService();
                return bookService.GetAllPublishers();
            }
            catch
            {
                return new List<string>();
            }
        }

        /// <summary>
        /// Get all unique categories from catalog
        /// </summary>
        public static IEnumerable<string> GetAllCategories()
        {
            try
            {
                var bookService = ServiceFactory.CreateBookService();
                return bookService.GetAllCategories();
            }
            catch
            {
                return new List<string>();
            }
        }

        /// <summary>
        /// Suggest similar authors (fuzzy matching)
        /// </summary>
        public static IEnumerable<string> SuggestAuthors(string partialName, int limit = 10)
        {
            var allAuthors = GetAllAuthors();
            if (string.IsNullOrWhiteSpace(partialName))
                return allAuthors.Take(limit);

            partialName = partialName.ToLower();
            return allAuthors
                .Where(a => a.ToLower().Contains(partialName))
                .Take(limit);
        }

        /// <summary>
        /// Suggest similar publishers
        /// </summary>
        public static IEnumerable<string> SuggestPublishers(string partialName, int limit = 10)
        {
            var allPublishers = GetAllPublishers();
            if (string.IsNullOrWhiteSpace(partialName))
                return allPublishers.Take(limit);

            partialName = partialName.ToLower();
            return allPublishers
                .Where(p => p.ToLower().Contains(partialName))
                .Take(limit);
        }

        /// <summary>
        /// Suggest similar categories
        /// </summary>
        public static IEnumerable<string> SuggestCategories(string partialName, int limit = 10)
        {
            var allCategories = GetAllCategories();
            if (string.IsNullOrWhiteSpace(partialName))
                return allCategories.Take(limit);

            partialName = partialName.ToLower();
            return allCategories
                .Where(c => c.ToLower().Contains(partialName))
                .Take(limit);
        }

        /// <summary>
        /// Convert string to title case (handles common exceptions)
        /// </summary>
        private static string ToTitleCase(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            var words = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var titleCaseWords = new List<string>();

            foreach (var word in words)
            {
                if (word.Length == 0)
                    continue;

                // Handle common exceptions (articles, prepositions)
                var lowerWord = word.ToLower();
                if (new[] { "a", "an", "the", "and", "or", "but", "in", "on", "at", "to", "for", "of", "with", "by" }.Contains(lowerWord) && titleCaseWords.Count > 0)
                {
                    titleCaseWords.Add(lowerWord);
                }
                else
                {
                    // Capitalize first letter
                    titleCaseWords.Add(char.ToUpper(word[0]) + (word.Length > 1 ? word.Substring(1).ToLower() : ""));
                }
            }

            return string.Join(" ", titleCaseWords);
        }

        /// <summary>
        /// Extract first author from multi-author string
        /// </summary>
        public static string GetPrimaryAuthor(string authors)
        {
            if (string.IsNullOrWhiteSpace(authors))
                return string.Empty;

            var authorList = authors.Split(new[] { ",", ";", "&", " and " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(a => a.Trim())
                .Where(a => !string.IsNullOrWhiteSpace(a))
                .ToList();

            return authorList.Count > 0 ? authorList[0] : string.Empty;
        }

        /// <summary>
        /// Extract all authors from multi-author string
        /// </summary>
        public static List<string> GetAllAuthorsFromString(string authors)
        {
            if (string.IsNullOrWhiteSpace(authors))
                return new List<string>();

            return authors.Split(new[] { ",", ";", "&", " and " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(a => a.Trim())
                .Where(a => !string.IsNullOrWhiteSpace(a))
                .ToList();
        }
    }
}

