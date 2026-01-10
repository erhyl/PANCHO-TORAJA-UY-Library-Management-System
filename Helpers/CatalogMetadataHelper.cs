using System;
using System.Collections.Generic;
using System.Linq;
using Project5LMS.Services;
namespace Project5LMS.Helpers
{
    public static class CatalogMetadataHelper
    {
        public static string NormalizeAuthor(string author)
        {
            if (string.IsNullOrWhiteSpace(author))
                return string.Empty;
            var authors = author.Split(new[] { ",", ";", "&", " and " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(a => a.Trim())
                .Where(a => !string.IsNullOrWhiteSpace(a))
                .ToList();
            if (authors.Count == 0)
                return string.Empty;
            if (authors.Count == 1)
            {
                return ToTitleCase(authors[0]);
            }
            return string.Join(", ", authors.Select(ToTitleCase));
        }
        public static string NormalizePublisher(string publisher)
        {
            if (string.IsNullOrWhiteSpace(publisher))
                return string.Empty;
            return ToTitleCase(publisher.Trim());
        }
        public static string NormalizeCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
                return "Uncategorized";
            return ToTitleCase(category.Trim());
        }
        public static bool IsValidAuthor(string author)
        {
            if (string.IsNullOrWhiteSpace(author))
                return false;
            return author.Any(char.IsLetter);
        }
        public static bool IsValidPublisher(string publisher)
        {
            if (string.IsNullOrWhiteSpace(publisher))
                return false;
            return publisher.Any(char.IsLetter);
        }
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
                var lowerWord = word.ToLower();
                if (new[] { "a", "an", "the", "and", "or", "but", "in", "on", "at", "to", "for", "of", "with", "by" }.Contains(lowerWord) && titleCaseWords.Count > 0)
                {
                    titleCaseWords.Add(lowerWord);
                }
                else
                {
                    titleCaseWords.Add(char.ToUpper(word[0]) + (word.Length > 1 ? word.Substring(1).ToLower() : ""));
                }
            }
            return string.Join(" ", titleCaseWords);
        }
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