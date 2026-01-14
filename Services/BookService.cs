using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using Project5LMS.Models;
using Project5LMS.Repositories;
using Project5LMS.Data;
using Project5LMS.Interfaces;
using Project5LMS.Helpers;
namespace Project5LMS.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly DatabaseContext _dbContext;
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository ?? throw new System.ArgumentNullException(nameof(bookRepository));
            _dbContext = new DatabaseContext();
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
        public IEnumerable<Book> GetBooksByAuthor(string author)
        {
            if (string.IsNullOrWhiteSpace(author))
                return new List<Book>();
            return _bookRepository.GetByAuthor(author);
        }
        public IEnumerable<string> GetAllAuthors()
        {
            return _bookRepository.GetAllAuthors();
        }
        public IEnumerable<string> GetAllPublishers()
        {
            return _bookRepository.GetAllPublishers();
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
        public bool DeleteBook(string accessionNumber)
        {
            if (string.IsNullOrWhiteSpace(accessionNumber))
                return false;
            return _bookRepository.Delete(accessionNumber);
        }
        public bool IsBookAvailable(int bookId)
        {
            try
            {
                // Check BookCopies table directly for accurate availability
                // This is the source of truth, not Books.Available which can be out of sync
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    
                    // First check if BookCopies table exists
                    if (!DatabaseSchemaHelper.CheckTableExists(conn, "BookCopies"))
                    {
                        // Fallback to Books.Available if BookCopies table doesn't exist
                        var book = _bookRepository.GetById(bookId);
                        return book != null && book.IsAvailable;
                    }
                    
                    // Check if there are any available copies in BookCopies table
                    string query = @"SELECT COUNT(*) FROM BookCopies 
                                     WHERE BookID = @BookID 
                                     AND CopyStatus = 'Available'";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookID", bookId);
                        object result = cmd.ExecuteScalar();
                        int availableCount = result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
                        return availableCount > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error checking book availability: {ex.Message}");
                // Fallback to Books.Available if BookCopies check fails
                var book = _bookRepository.GetById(bookId);
                return book != null && book.IsAvailable;
            }
        }
        public bool IsBookAvailableByAccession(string accessionNumber)
        {
            if (string.IsNullOrWhiteSpace(accessionNumber))
                return false;
            
            try
            {
                // First get the book to find BookID
                var book = _bookRepository.GetByAccessionNumber(accessionNumber);
                if (book == null)
                    return false;
                
                // Check BookCopies table directly for accurate availability
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    
                    // First check if BookCopies table exists
                    if (!DatabaseSchemaHelper.CheckTableExists(conn, "BookCopies"))
                    {
                        // Fallback to Books.Available if BookCopies table doesn't exist
                        return book.IsAvailable;
                    }
                    
                    // Check if there are any available copies in BookCopies table
                    string query = @"SELECT COUNT(*) FROM BookCopies 
                                     WHERE BookID = @BookID 
                                     AND CopyStatus = 'Available'";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookID", book.BookID);
                        object result = cmd.ExecuteScalar();
                        int availableCount = result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
                        return availableCount > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error checking book availability by accession: {ex.Message}");
                // Fallback to Books.Available if BookCopies check fails
                var book = _bookRepository.GetByAccessionNumber(accessionNumber);
                return book != null && book.IsAvailable;
            }
        }
        public bool UpdateBookAvailability(string accessionNumber, int change)
        {
            if (string.IsNullOrWhiteSpace(accessionNumber))
                return false;
            return _bookRepository.UpdateAvailability(accessionNumber, change);
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
        public IEnumerable<Book> GetNewArrivals(int limit = Constants.DefaultQueryLimit, DateTime? startDate = null, DateTime? endDate = null)
        {
            return _bookRepository.GetNewArrivals(limit, startDate, endDate);
        }
        public IEnumerable<Book> GetPopularBooks(int limit = Constants.DefaultQueryLimit, bool weightedByRecency = false)
        {
            return _bookRepository.GetPopularBooks(limit, weightedByRecency);
        }
    }
}