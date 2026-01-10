using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Project5LMS.Data;
using Project5LMS.Models;
using Project5LMS.Helpers;
namespace Project5LMS.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly DatabaseContext _dbContext;
        public BookRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public Book GetById(int bookId)
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM Books WHERE BookID = @BookID LIMIT 1";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookID", bookId);
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                return MapDataRowToBook(dt.Rows[0]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting book by ID: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
            return null;
        }
        public Book GetByAccessionNumber(string accessionNumber)
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM Books WHERE AccessionNo = @AccessionNo LIMIT 1";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@AccessionNo", accessionNumber);
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                return MapDataRowToBook(dt.Rows[0]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting book by accession number: {ex.Message}");
            }
            return null;
        }
        public IEnumerable<Book> GetAll()
        {
            List<Book> books = new List<Book>();
            try
            {
                string query = "SELECT * FROM Books ORDER BY Title";
                DataTable dt = _dbContext.ExecuteQuery(query);
                foreach (DataRow row in dt.Rows)
                {
                    books.Add(MapDataRowToBook(row));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting all books: {ex.Message}");
            }
            return books;
        }
        public IEnumerable<Book> Search(string searchTerm)
        {
            List<Book> books = new List<Book>();
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT * FROM Books
                                    WHERE Title LIKE @SearchTerm
                                    OR Author LIKE @SearchTerm
                                    OR ISBN LIKE @SearchTerm
                                    OR AccessionNo LIKE @SearchTerm
                                    LIMIT 100";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            foreach (DataRow row in dt.Rows)
                            {
                                books.Add(MapDataRowToBook(row));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error searching books: {ex.Message}");
            }
            return books;
        }
        public IEnumerable<Book> GetByCategory(string category)
        {
            List<Book> books = new List<Book>();
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM Books WHERE Category = @Category";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Category", category);
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            foreach (DataRow row in dt.Rows)
                            {
                                books.Add(MapDataRowToBook(row));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting books by category: {ex.Message}");
            }
            return books;
        }
        public IEnumerable<Book> GetByAuthor(string author)
        {
            List<Book> books = new List<Book>();
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM Books WHERE Author LIKE @Author ORDER BY Title";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Author", $"%{author}%");
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            foreach (DataRow row in dt.Rows)
                            {
                                books.Add(MapDataRowToBook(row));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting books by author: {ex.Message}");
            }
            return books;
        }
        public IEnumerable<string> GetAllAuthors()
        {
            var authors = new List<string>();
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT DISTINCT Author FROM Books WHERE Author IS NOT NULL AND Author != '' ORDER BY Author";
                    using (var cmd = new MySqlCommand(query, conn))
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        foreach (DataRow row in dt.Rows)
                        {
                            authors.Add(row["Author"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting all authors: {ex.Message}");
            }
            return authors;
        }
        public IEnumerable<string> GetAllPublishers()
        {
            var publishers = new List<string>();
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT DISTINCT Publisher FROM Books WHERE Publisher IS NOT NULL AND Publisher != '' ORDER BY Publisher";
                    using (var cmd = new MySqlCommand(query, conn))
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        foreach (DataRow row in dt.Rows)
                        {
                            publishers.Add(row["Publisher"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting all publishers: {ex.Message}");
            }
            return publishers;
        }
        public bool Add(Book book)
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = @"INSERT INTO Books (Title, Author, ISBN, Category, Publisher, PublicationYear,
                                    Language, TotalCopies, Available, Location, Status, AccessionNo, CallNumber, BookType)
                                    VALUES (@Title, @Author, @ISBN, @Category, @Publisher, @PublicationYear,
                                    @Language, @TotalCopies, @Available, @Location, @Status, @AccessionNo, @CallNumber, @BookType)";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        MapBookToParameters(cmd, book);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error adding book: {ex.Message}");
                return false;
            }
        }
        public bool Update(Book book)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(book.AccessionNo))
                {
                    System.Diagnostics.Debug.WriteLine("Error updating book: AccessionNo is required");
                    return false;
                }
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = @"UPDATE Books SET Title=@Title, Author=@Author, ISBN=@ISBN, Category=@Category,
                                    Publisher=@Publisher, PublicationYear=@PublicationYear, Language=@Language,
                                    TotalCopies=@TotalCopies, Available=@Available, Location=@Location,
                                    Status=@Status, CallNumber=@CallNumber, BookType=@BookType
                                    WHERE AccessionNo=@AccessionNo";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        MapBookToParameters(cmd, book);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating book: {ex.Message}");
                return false;
            }
        }
        public bool Delete(string accessionNumber)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(accessionNumber))
                {
                    System.Diagnostics.Debug.WriteLine("Error deleting book: AccessionNo is required");
                    return false;
                }
                string query = "DELETE FROM Books WHERE AccessionNo = @AccessionNo";
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@AccessionNo", accessionNumber);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error deleting book: {ex.Message}");
                return false;
            }
        }
        public bool UpdateAvailability(string accessionNumber, int change)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(accessionNumber))
                {
                    System.Diagnostics.Debug.WriteLine("Error updating book availability: AccessionNo is required");
                    return false;
                }
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = @"UPDATE Books
                                    SET Available = GREATEST(0, LEAST(Available + @Change, TotalCopies))
                                    WHERE AccessionNo = @AccessionNo AND Available + @Change >= 0 AND Available + @Change <= TotalCopies";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@AccessionNo", accessionNumber);
                        cmd.Parameters.AddWithValue("@Change", change);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating book availability: {ex.Message}");
                return false;
            }
        }
        public int GetAvailableCount(string accessionNumber)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(accessionNumber))
                {
                    return 0;
                }
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT Available FROM Books WHERE AccessionNo = @AccessionNo";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@AccessionNo", accessionNumber);
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            return Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting available count: {ex.Message}");
            }
            return 0;
        }
        public IEnumerable<Book> GetNewArrivals(int limit = 20, DateTime? startDate = null, DateTime? endDate = null)
        {
            List<Book> books = new List<Book>();
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    if (!startDate.HasValue)
                        startDate = DateTime.Now.AddDays(-30);
                    if (!endDate.HasValue)
                        endDate = DateTime.Now;
                    string query = @"SELECT * FROM Books
                                   WHERE (CreatedDate >= @StartDate AND CreatedDate <= @EndDate)
                                   OR (CreatedDate IS NULL AND BookID IN (
                                       SELECT BookID FROM Books ORDER BY BookID DESC LIMIT @Limit
                                   ))
                                   ORDER BY COALESCE(CreatedDate, '1900-01-01') DESC, BookID DESC
                                   LIMIT @Limit";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@StartDate", startDate.Value);
                        cmd.Parameters.AddWithValue("@EndDate", endDate.Value);
                        cmd.Parameters.AddWithValue("@Limit", limit);
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            foreach (DataRow row in dt.Rows)
                            {
                                books.Add(MapDataRowToBook(row));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting new arrivals: {ex.Message}");
            }
            return books;
        }
        public IEnumerable<Book> GetPopularBooks(int limit = 20, bool weightedByRecency = false)
        {
            List<Book> books = new List<Book>();
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query;
                    if (weightedByRecency)
                    {
                        query = @"SELECT b.*,
                                   SUM(CASE
                                       WHEN t.BorrowDate >= DATE_SUB(NOW(), INTERVAL 30 DAY) THEN 3
                                       WHEN t.BorrowDate >= DATE_SUB(NOW(), INTERVAL 90 DAY) THEN 2
                                       ELSE 1
                                   END) as PopularityScore
                                   FROM Books b
                                   LEFT JOIN Transactions t ON b.BookID = t.BookID
                                       AND (t.Status = 'Borrowed' OR t.Status = 'Returned')
                                   GROUP BY b.BookID
                                   ORDER BY PopularityScore DESC, b.Title ASC
                                   LIMIT @Limit";
                    }
                    else
                    {
                        query = @"SELECT b.*, COUNT(t.TransactionID) as BorrowCount
                                   FROM Books b
                                   LEFT JOIN Transactions t ON b.BookID = t.BookID
                                       AND (t.Status = 'Borrowed' OR t.Status = 'Returned')
                                   GROUP BY b.BookID
                                   ORDER BY BorrowCount DESC, b.Title ASC
                                   LIMIT @Limit";
                    }
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Limit", limit);
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            foreach (DataRow row in dt.Rows)
                            {
                                books.Add(MapDataRowToBook(row));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting popular books: {ex.Message}");
            }
            return books;
        }
        private Book MapDataRowToBook(DataRow row)
        {
            return new Book
            {
                BookID = Convert.ToInt32(row["BookID"]),
                Title = row["Title"]?.ToString() ?? string.Empty,
                Author = row["Author"]?.ToString() ?? string.Empty,
                ISBN = row["ISBN"]?.ToString() ?? string.Empty,
                Category = row["Category"]?.ToString() ?? string.Empty,
                Publisher = row["Publisher"]?.ToString() ?? string.Empty,
                PublicationYear = row["PublicationYear"] != DBNull.Value ? Convert.ToInt32(row["PublicationYear"]) : 0,
                Language = row["Language"]?.ToString() ?? string.Empty,
                TotalCopies = row["TotalCopies"] != DBNull.Value ? Convert.ToInt32(row["TotalCopies"]) : 0,
                Available = row["Available"] != DBNull.Value ? Convert.ToInt32(row["Available"]) : 0,
                Location = row["Location"]?.ToString() ?? string.Empty,
                Status = row["Status"]?.ToString() ?? string.Empty,
                AccessionNo = row["AccessionNo"]?.ToString() ?? string.Empty
            };
        }
        private void MapBookToParameters(MySqlCommand cmd, Book book)
        {
            cmd.Parameters.AddWithValue("@Title", book.Title);
            cmd.Parameters.AddWithValue("@Author", book.Author);
            cmd.Parameters.AddWithValue("@ISBN", book.ISBN ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Category", book.Category ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Publisher", book.Publisher ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@PublicationYear", book.PublicationYear);
            cmd.Parameters.AddWithValue("@Language", book.Language ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@TotalCopies", book.TotalCopies);
            cmd.Parameters.AddWithValue("@Available", book.Available);
            cmd.Parameters.AddWithValue("@Location", book.Location ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", book.Status ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@AccessionNo", book.AccessionNo);
            cmd.Parameters.AddWithValue("@CallNumber", book.CallNumber ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@BookType", book.BookType ?? (object)DBNull.Value);
        }
    }
}