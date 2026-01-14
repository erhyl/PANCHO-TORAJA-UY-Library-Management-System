using System;
using System.Collections.Generic;
using Project5LMS.Models;
using Project5LMS.Helpers;
namespace Project5LMS.Repositories
{
    public interface IBookRepository
    {
        Book GetById(int bookId);
        Book GetByAccessionNumber(string accessionNumber);
        IEnumerable<Book> GetAll();
        IEnumerable<Book> Search(string searchTerm);
        IEnumerable<Book> GetByCategory(string category);
        IEnumerable<Book> GetByAuthor(string author);
        IEnumerable<string> GetAllAuthors();
        IEnumerable<string> GetAllPublishers();
        bool Add(Book book);
        bool Update(Book book);
        bool Delete(string accessionNumber);
        bool UpdateAvailability(string accessionNumber, int change);
        int GetAvailableCount(string accessionNumber);
        IEnumerable<Book> GetNewArrivals(int limit = Constants.DefaultQueryLimit, DateTime? startDate = null, DateTime? endDate = null);
        IEnumerable<Book> GetPopularBooks(int limit = Constants.DefaultQueryLimit, bool weightedByRecency = false);
    }
}