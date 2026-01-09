using System.Collections.Generic;
using Project5LMS.Models;

namespace Project5LMS.Interfaces
{
    public interface IBookService
    {
        Book GetBook(int bookId);
        Book GetBookByAccessionNumber(string accessionNumber);
        IEnumerable<Book> GetAllBooks();
        IEnumerable<Book> SearchBooks(string searchTerm);
        IEnumerable<Book> GetBooksByCategory(string category);
        bool AddBook(Book book);
        bool UpdateBook(Book book);
        bool DeleteBook(int bookId);
        bool IsBookAvailable(int bookId);
        bool UpdateBookAvailability(int bookId, int change);
        IEnumerable<string> GetAllCategories();
    }
}
