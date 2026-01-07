using System.Collections.Generic;
using System.Linq;
using Project5LMS.Models;
using Project5LMS.Repositories;
using Project5LMS.Data;
using Project5LMS.Interfaces;

namespace Project5LMS.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository ?? throw new System.ArgumentNullException(nameof(bookRepository));
        }

        public BookService(DatabaseContext dbContext) : this(new BookRepository(dbContext))
        {
        }

        public Book GetBook(int bookId)
        {
            return _bookRepository.GetById(bookId);
        }

        public Book GetBookByAccessionNumber(string accessionNumber)
        {
            if (string.IsNullOrWhiteSpace(accessionNumber))
                return null;

            return _bookRepository.GetByAccessionNumber(accessionNumber);
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _bookRepository.GetAll();
        }

        public IEnumerable<Book> SearchBooks(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return new List<Book>();

            return _bookRepository.Search(searchTerm);
        }

        public IEnumerable<Book> GetBooksByCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
                return new List<Book>();

            return _bookRepository.GetByCategory(category);
        }

        public bool AddBook(Book book)
        {
            if (book == null || !book.IsValid())
                return false;

            return _bookRepository.Add(book);
        }

        public bool UpdateBook(Book book)
        {
            if (book == null || !book.IsValid())
                return false;

            return _bookRepository.Update(book);
        }

        public bool DeleteBook(int bookId)
        {
            if (bookId <= 0)
                return false;

            return _bookRepository.Delete(bookId);
        }

        public bool IsBookAvailable(int bookId)
        {
            var book = _bookRepository.GetById(bookId);
            return book != null && book.IsAvailable;
        }

        public bool UpdateBookAvailability(int bookId, int change)
        {
            if (bookId <= 0)
                return false;

            return _bookRepository.UpdateAvailability(bookId, change);
        }

        public IEnumerable<string> GetAllCategories()
        {
            var books = _bookRepository.GetAll();
            return books
                .Where(b => !string.IsNullOrWhiteSpace(b.Category))
                .Select(b => b.Category)
                .Distinct()
                .OrderBy(c => c);
        }
    }
}
