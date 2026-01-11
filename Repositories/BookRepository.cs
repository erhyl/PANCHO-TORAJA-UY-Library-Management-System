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
                    // Check which columns exist and build query accordingly
                    bool hasTotalCopies = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "TotalCopies");
                    bool hasCopies = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "Copies");
                    bool hasAccessionNo = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "AccessionNo");
                    bool hasBarcode = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "Barcode");
                    bool hasPublicationYear = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "PublicationYear");
                    bool hasYearPublished = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "YearPublished");
                    
                    string copiesColumn = hasTotalCopies ? "TotalCopies" : (hasCopies ? "Copies" : "1 as TotalCopies");
                    string accessionColumn = hasAccessionNo ? "AccessionNo" : (hasBarcode ? "Barcode as AccessionNo" : "CAST(BookID AS CHAR) as AccessionNo");
                    string yearColumn = hasPublicationYear ? "PublicationYear" : (hasYearPublished ? "YearPublished as PublicationYear" : "0 as PublicationYear");
                    
                    string query = $@"SELECT 
                                        BookID,
                                        Title,
                                        Author,
                                        ISBN,
                                        Category,
                                        Publisher,
                                        {yearColumn},
                                        Language,
                                        {copiesColumn} as TotalCopies,
                                        Available,
                                        Location,
                                        Status,
                                        {accessionColumn},
                                        CallNumber,
                                        BookType
                                      FROM Books 
                                      WHERE BookID = @BookID 
                                      LIMIT 1";
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
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    // Check which columns exist and build query accordingly
                    bool hasTotalCopies = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "TotalCopies");
                    bool hasCopies = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "Copies");
                    bool hasAccessionNo = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "AccessionNo");
                    bool hasBarcode = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "Barcode");
                    bool hasPublicationYear = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "PublicationYear");
                    bool hasYearPublished = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "YearPublished");
                    
                    string copiesColumn = hasTotalCopies ? "TotalCopies" : (hasCopies ? "Copies" : "1 as TotalCopies");
                    string accessionColumn = hasAccessionNo ? "AccessionNo" : (hasBarcode ? "Barcode as AccessionNo" : "CAST(BookID AS CHAR) as AccessionNo");
                    string yearColumn = hasPublicationYear ? "PublicationYear" : (hasYearPublished ? "YearPublished as PublicationYear" : "0 as PublicationYear");
                    
                    string query = $@"SELECT 
                                        BookID,
                                        Title,
                                        Author,
                                        ISBN,
                                        Category,
                                        Publisher,
                                        {yearColumn},
                                        Language,
                                        {copiesColumn} as TotalCopies,
                                        Available,
                                        Location,
                                        Status,
                                        {accessionColumn},
                                        CallNumber,
                                        BookType
                                      FROM Books 
                                      ORDER BY Title";
                    
                    DataTable dt = _dbContext.ExecuteQuery(query);
                    foreach (DataRow row in dt.Rows)
                    {
                        books.Add(MapDataRowToBook(row));
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting all books: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
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
            // Handle both TotalCopies and Copies columns
            int totalCopies = 0;
            if (row.Table.Columns.Contains("TotalCopies") && row["TotalCopies"] != DBNull.Value)
                totalCopies = Convert.ToInt32(row["TotalCopies"]);
            else if (row.Table.Columns.Contains("Copies") && row["Copies"] != DBNull.Value)
                totalCopies = Convert.ToInt32(row["Copies"]);
            
            // Handle both AccessionNo and Barcode columns
            string accessionNo = string.Empty;
            if (row.Table.Columns.Contains("AccessionNo") && row["AccessionNo"] != DBNull.Value)
                accessionNo = row["AccessionNo"].ToString();
            else if (row.Table.Columns.Contains("Barcode") && row["Barcode"] != DBNull.Value)
                accessionNo = row["Barcode"].ToString();
            
            // Handle PublicationYear and YearPublished
            int publicationYear = 0;
            if (row.Table.Columns.Contains("PublicationYear") && row["PublicationYear"] != DBNull.Value)
                publicationYear = Convert.ToInt32(row["PublicationYear"]);
            else if (row.Table.Columns.Contains("YearPublished") && row["YearPublished"] != DBNull.Value)
                publicationYear = Convert.ToInt32(row["YearPublished"]);
            
            return new Book
            {
                BookID = Convert.ToInt32(row["BookID"]),
                Title = row.Table.Columns.Contains("Title") && row["Title"] != DBNull.Value ? row["Title"].ToString() : string.Empty,
                Author = row.Table.Columns.Contains("Author") && row["Author"] != DBNull.Value ? row["Author"].ToString() : string.Empty,
                ISBN = row.Table.Columns.Contains("ISBN") && row["ISBN"] != DBNull.Value ? row["ISBN"].ToString() : string.Empty,
                Category = row.Table.Columns.Contains("Category") && row["Category"] != DBNull.Value ? row["Category"].ToString() : string.Empty,
                Publisher = row.Table.Columns.Contains("Publisher") && row["Publisher"] != DBNull.Value ? row["Publisher"].ToString() : string.Empty,
                PublicationYear = publicationYear,
                Language = row.Table.Columns.Contains("Language") && row["Language"] != DBNull.Value ? row["Language"].ToString() : string.Empty,
                TotalCopies = totalCopies,
                Available = row.Table.Columns.Contains("Available") && row["Available"] != DBNull.Value ? Convert.ToInt32(row["Available"]) : 0,
                Location = row.Table.Columns.Contains("Location") && row["Location"] != DBNull.Value ? row["Location"].ToString() : string.Empty,
                Status = row.Table.Columns.Contains("Status") && row["Status"] != DBNull.Value ? row["Status"].ToString() : string.Empty,
                AccessionNo = accessionNo,
                CallNumber = row.Table.Columns.Contains("CallNumber") && row["CallNumber"] != DBNull.Value ? row["CallNumber"].ToString() : string.Empty,
                BookType = row.Table.Columns.Contains("BookType") && row["BookType"] != DBNull.Value ? row["BookType"].ToString() : string.Empty
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