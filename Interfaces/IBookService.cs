using System;
using System.Collections.Generic;
using Project5LMS.Models;
using Project5LMS.Helpers;
namespace Project5LMS.Interfaces
{
    public interface IBookService
    {
        Book GetBook(int bookId);
        Book GetBookByAccessionNumber(string accessionNumber);
        IEnumerable<Book> GetAllBooks();
        IEnumerable<Book> SearchBooks(string searchTerm);
        IEnumerable<Book> GetBooksByCategory(string category);
        IEnumerable<Book> GetBooksByAuthor(string author);
        IEnumerable<string> GetAllAuthors();
        IEnumerable<string> GetAllPublishers();
        bool AddBook(Book book);
        bool UpdateBook(Book book);
        bool DeleteBook(string accessionNumber);
        bool IsBookAvailable(int bookId);
        bool IsBookAvailableByAccession(string accessionNumber);
        bool UpdateBookAvailability(string accessionNumber, int change);
        IEnumerable<string> GetAllCategories();
        IEnumerable<Book> GetNewArrivals(int limit = Constants.DefaultQueryLimit, DateTime? startDate = null, DateTime? endDate = null);
        IEnumerable<Book> GetPopularBooks(int limit = Constants.DefaultQueryLimit, bool weightedByRecency = false);
    }
}