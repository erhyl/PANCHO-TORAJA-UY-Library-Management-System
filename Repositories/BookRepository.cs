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
                    // Get all column information in a single call (cached)
                    var columns = BookColumnSchema.GetBookColumns(conn);
                    var selectColumns = BookColumnSchema.BuildSelectColumns(columns);
                    
                    string query = $@"SELECT {string.Join(", ", selectColumns)}
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
                                var book = MapDataRowToBook(dt.Rows[0]);
                                
                                // Sync Available count from BookCopies table for accuracy
                                // This ensures Books.Available matches actual BookCopies status
                                if (DatabaseSchemaHelper.CheckTableExists(conn, "BookCopies"))
                                {
                                    string syncQuery = @"SELECT COUNT(*) FROM BookCopies 
                                                         WHERE BookID = @BookID 
                                                         AND CopyStatus = 'Available'";
                                    using (var syncCmd = new MySqlCommand(syncQuery, conn))
                                    {
                                        syncCmd.Parameters.AddWithValue("@BookID", bookId);
                                        object result = syncCmd.ExecuteScalar();
                                        int actualAvailable = result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
                                        book.Available = actualAvailable;
                                    }
                                }
                                
                                return book;
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
                    // Get column schema to build proper query
                    var columns = BookColumnSchema.GetBookColumns(conn);
                    string accessionColumn = BookColumnSchema.GetAccessionColumn(columns);
                    
                    // Build query that works with different column names
                    string whereClause = columns.ContainsKey("AccessionNo") && columns["AccessionNo"]
                        ? "AccessionNo = @AccessionNo"
                        : (columns.ContainsKey("Barcode") && columns["Barcode"]
                            ? "Barcode = @AccessionNo"
                            : "CAST(BookID AS CHAR) = @AccessionNo");
                    
                    var selectColumns = BookColumnSchema.BuildSelectColumns(columns);
                    string query = $@"SELECT {string.Join(", ", selectColumns)}
                                      FROM Books 
                                      WHERE {whereClause}
                                      LIMIT 1";
                    
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@AccessionNo", accessionNumber);
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                var book = MapDataRowToBook(dt.Rows[0]);
                                
                                // Sync Available count from BookCopies table for accuracy
                                if (DatabaseSchemaHelper.CheckTableExists(conn, "BookCopies"))
                                {
                                    string syncQuery = @"SELECT COUNT(*) FROM BookCopies 
                                                         WHERE BookID = @BookID 
                                                         AND CopyStatus = 'Available'";
                                    using (var syncCmd = new MySqlCommand(syncQuery, conn))
                                    {
                                        syncCmd.Parameters.AddWithValue("@BookID", book.BookID);
                                        object result = syncCmd.ExecuteScalar();
                                        int actualAvailable = result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
                                        book.Available = actualAvailable;
                                    }
                                }
                                
                                return book;
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
                    // Get all column information in a single call (cached)
                    var columns = BookColumnSchema.GetBookColumns(conn);
                    var selectColumns = BookColumnSchema.BuildSelectColumns(columns);
                    
                    string query = $@"SELECT {string.Join(", ", selectColumns)}
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
                    // Get column schema to build proper query
                    var columns = BookColumnSchema.GetBookColumns(conn);
                    var selectColumns = BookColumnSchema.BuildSelectColumns(columns);
                    string accessionColumn = BookColumnSchema.GetAccessionColumn(columns);
                    
                    // Build WHERE clause that works with different column names
                    string whereClause = $@"Title LIKE @SearchTerm
                                    OR Author LIKE @SearchTerm
                                    OR ISBN LIKE @SearchTerm";
                    
                    if (columns.ContainsKey("AccessionNo") && columns["AccessionNo"])
                        whereClause += " OR AccessionNo LIKE @SearchTerm";
                    else if (columns.ContainsKey("Barcode") && columns["Barcode"])
                        whereClause += " OR Barcode LIKE @SearchTerm";
                    
                    string query = $@"SELECT {string.Join(", ", selectColumns)}
                                    FROM Books
                                    WHERE {whereClause}
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
                    // Get column schema to build proper query
                    var columns = BookColumnSchema.GetBookColumns(conn);
                    var selectColumns = BookColumnSchema.BuildSelectColumns(columns);
                    
                    string query = $@"SELECT {string.Join(", ", selectColumns)}
                                    FROM Books 
                                    WHERE Category = @Category";
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
                    // Get column schema to build proper query
                    var columns = BookColumnSchema.GetBookColumns(conn);
                    var selectColumns = BookColumnSchema.BuildSelectColumns(columns);
                    
                    string query = $@"SELECT {string.Join(", ", selectColumns)}
                                    FROM Books 
                                    WHERE Author LIKE @Author 
                                    ORDER BY Title";
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
        public IEnumerable<Book> GetNewArrivals(int limit = Constants.DefaultQueryLimit, DateTime? startDate = null, DateTime? endDate = null)
        {
            List<Book> books = new List<Book>();
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    if (!startDate.HasValue)
                        startDate = DateTime.Now.AddDays(-Constants.DefaultNewArrivalsDays);
                    if (!endDate.HasValue)
                        endDate = DateTime.Now;
                    // Get column schema to build proper query
                    var columns = BookColumnSchema.GetBookColumns(conn);
                    var selectColumns = BookColumnSchema.BuildSelectColumns(columns);
                    
                    // Rewrite query to avoid LIMIT in subquery (not supported in some MySQL versions)
                    // Use a simpler approach: get all matching books and limit at the end
                    string query = $@"SELECT {string.Join(", ", selectColumns)}
                                   FROM Books
                                   WHERE (CreatedDate >= @StartDate AND CreatedDate <= @EndDate)
                                   OR CreatedDate IS NULL
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
        /// <summary>
        /// Syncs Books.Available count with actual BookCopies table for a specific book
        /// This ensures data consistency between Books and BookCopies tables
        /// </summary>
        public bool SyncBookAvailability(int bookId)
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    
                    // Check if BookCopies table exists
                    if (!DatabaseSchemaHelper.CheckTableExists(conn, "BookCopies"))
                    {
                        return false;
                    }
                    
                    // Get actual available count from BookCopies
                    string countQuery = @"SELECT COUNT(*) FROM BookCopies 
                                         WHERE BookID = @BookID 
                                         AND CopyStatus = 'Available'";
                    int actualAvailable = 0;
                    using (var countCmd = new MySqlCommand(countQuery, conn))
                    {
                        countCmd.Parameters.AddWithValue("@BookID", bookId);
                        object result = countCmd.ExecuteScalar();
                        actualAvailable = result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
                    }
                    
                    // Get total copies count
                    string totalQuery = @"SELECT COUNT(*) FROM BookCopies 
                                          WHERE BookID = @BookID";
                    int totalCopies = 0;
                    using (var totalCmd = new MySqlCommand(totalQuery, conn))
                    {
                        totalCmd.Parameters.AddWithValue("@BookID", bookId);
                        object result = totalCmd.ExecuteScalar();
                        totalCopies = result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
                    }
                    
                    // Update Books table
                    bool hasTotalCopies = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "TotalCopies");
                    string copiesColumn = hasTotalCopies ? "TotalCopies" : "Copies";
                    
                    string updateQuery = $@"UPDATE Books 
                                           SET Available = @Available, {copiesColumn} = @TotalCopies 
                                           WHERE BookID = @BookID";
                    using (var updateCmd = new MySqlCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@Available", actualAvailable);
                        updateCmd.Parameters.AddWithValue("@TotalCopies", totalCopies);
                        updateCmd.Parameters.AddWithValue("@BookID", bookId);
                        int rowsAffected = updateCmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error syncing book availability: {ex.Message}");
                return false;
            }
        }
        
        public IEnumerable<Book> GetPopularBooks(int limit = 20, bool weightedByRecency = false)
        {
            List<Book> books = new List<Book>();
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    // Get column schema to build proper query
                    var columns = BookColumnSchema.GetBookColumns(conn);
                    var selectColumns = BookColumnSchema.BuildSelectColumns(columns);
                    // Build column list with table alias, handling AS clauses
                    var bookColumnsList = new List<string>();
                    foreach (var col in selectColumns)
                    {
                        if (col.Contains(" AS ") || col.Contains(" as "))
                        {
                            // For columns with aliases, extract the actual column name
                            var parts = col.Split(new[] { " AS ", " as " }, StringSplitOptions.None);
                            bookColumnsList.Add($"b.{parts[0]} AS {parts[1]}");
                        }
                        else
                        {
                            bookColumnsList.Add($"b.{col}");
                        }
                    }
                    string bookColumns = string.Join(", ", bookColumnsList);
                    
                    string query;
                    if (weightedByRecency)
                    {
                        query = $@"SELECT {bookColumns},
                                   SUM(CASE
                                       WHEN t.BorrowDate >= DATE_SUB(NOW(), INTERVAL {Constants.PopularBooksRecentDays} DAY) THEN 3
                                       WHEN t.BorrowDate >= DATE_SUB(NOW(), INTERVAL {Constants.PopularBooksMediumDays} DAY) THEN 2
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
                        query = $@"SELECT {bookColumns}, COUNT(t.TransactionID) as BorrowCount
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
            
            // Handle optional fields
            string subtitle = row.Table.Columns.Contains("Subtitle") && row["Subtitle"] != DBNull.Value ? row["Subtitle"].ToString() : null;
            string editor = row.Table.Columns.Contains("Editor") && row["Editor"] != DBNull.Value ? row["Editor"].ToString() : null;
            string edition = row.Table.Columns.Contains("Edition") && row["Edition"] != DBNull.Value ? row["Edition"].ToString() : null;
            int numberOfPages = 0;
            if (row.Table.Columns.Contains("NumberOfPages") && row["NumberOfPages"] != DBNull.Value)
                numberOfPages = Convert.ToInt32(row["NumberOfPages"]);
            string physicalDescription = row.Table.Columns.Contains("PhysicalDescription") && row["PhysicalDescription"] != DBNull.Value ? row["PhysicalDescription"].ToString() : null;
            string coverImagePath = row.Table.Columns.Contains("CoverImagePath") && row["CoverImagePath"] != DBNull.Value ? row["CoverImagePath"].ToString() : null;
            string barcode = row.Table.Columns.Contains("Barcode") && row["Barcode"] != DBNull.Value ? row["Barcode"].ToString() : null;
            
            return new Book
            {
                BookID = Convert.ToInt32(row["BookID"]),
                Title = row.Table.Columns.Contains("Title") && row["Title"] != DBNull.Value ? row["Title"].ToString() : string.Empty,
                Subtitle = subtitle,
                Author = row.Table.Columns.Contains("Author") && row["Author"] != DBNull.Value ? row["Author"].ToString() : string.Empty,
                Editor = editor,
                ISBN = row.Table.Columns.Contains("ISBN") && row["ISBN"] != DBNull.Value ? row["ISBN"].ToString() : string.Empty,
                Category = row.Table.Columns.Contains("Category") && row["Category"] != DBNull.Value ? row["Category"].ToString() : string.Empty,
                Publisher = row.Table.Columns.Contains("Publisher") && row["Publisher"] != DBNull.Value ? row["Publisher"].ToString() : string.Empty,
                PublicationYear = publicationYear,
                Edition = edition,
                Language = row.Table.Columns.Contains("Language") && row["Language"] != DBNull.Value ? row["Language"].ToString() : string.Empty,
                NumberOfPages = numberOfPages,
                PhysicalDescription = physicalDescription,
                TotalCopies = totalCopies,
                Available = row.Table.Columns.Contains("Available") && row["Available"] != DBNull.Value ? Convert.ToInt32(row["Available"]) : 0,
                Location = row.Table.Columns.Contains("Location") && row["Location"] != DBNull.Value ? row["Location"].ToString() : string.Empty,
                Status = row.Table.Columns.Contains("Status") && row["Status"] != DBNull.Value ? row["Status"].ToString() : string.Empty,
                AccessionNo = accessionNo,
                CallNumber = row.Table.Columns.Contains("CallNumber") && row["CallNumber"] != DBNull.Value ? row["CallNumber"].ToString() : string.Empty,
                BookType = row.Table.Columns.Contains("BookType") && row["BookType"] != DBNull.Value ? row["BookType"].ToString() : string.Empty,
                CoverImagePath = coverImagePath,
                Barcode = barcode
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