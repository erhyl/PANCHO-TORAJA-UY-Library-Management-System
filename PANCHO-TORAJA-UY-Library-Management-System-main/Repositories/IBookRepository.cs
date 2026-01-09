using System.Collections.Generic;
using Project5LMS.Models;

namespace Project5LMS.Repositories
{

    public interface IBookRepository
    {
        Book GetById(int bookId);
        Book GetByAccessionNumber(string accessionNumber);
        IEnumerable<Book> GetAll();
        IEnumerable<Book> Search(string searchTerm);
        IEnumerable<Book> GetByCategory(string category);
        bool Add(Book book);
        bool Update(Book book);
        bool Delete(int bookId);
        bool UpdateAvailability(int bookId, int change);
        int GetAvailableCount(int bookId);
    }
}
