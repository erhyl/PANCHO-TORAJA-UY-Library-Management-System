using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
                string query = "SELECT * FROM Books WHERE BookID = @BookID LIMIT 1";
                DataTable dt = _dbContext.ExecuteQuery(query.Replace("@BookID", bookId.ToString()));

                if (dt.Rows.Count > 0)
                {
                    return MapDataRowToBook(dt.Rows[0]);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting book by ID: {ex.Message}");
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
                System.Diagnostics.Debug.WriteLine("BookRepository.GetAll: Starting to fetch books...");
                
                // First, try SELECT * to see what columns actually exist
                string query = @"SELECT * FROM Books ORDER BY Title";
                DataTable dt = null;
                
                try
                {
                    dt = _dbContext.ExecuteQuery(query);
                    System.Diagnostics.Debug.WriteLine($"BookRepository.GetAll: SELECT * query succeeded, got {dt?.Rows.Count ?? 0} rows");
                    
                    // Log available columns for debugging
                    if (dt != null && dt.Columns.Count > 0)
                    {
                        var columns = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToList();
                        System.Diagnostics.Debug.WriteLine($"Available columns from SELECT *: {string.Join(", ", columns)}");
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error executing SELECT * query: {ex.Message}");
                    System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                    
                    // Try with explicit basic columns
                    try
                    {
                        query = @"SELECT BookID, Title, Author, ISBN, Publisher, YearPublished, Language, CallNumber
                                 FROM Books 
                                 ORDER BY Title";
                        dt = _dbContext.ExecuteQuery(query);
                        System.Diagnostics.Debug.WriteLine($"BookRepository.GetAll: Explicit columns query succeeded, got {dt?.Rows.Count ?? 0} rows");
                    }
                    catch (Exception ex2)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error executing explicit columns query: {ex2.Message}");
                        System.Diagnostics.Debug.WriteLine($"Stack trace: {ex2.StackTrace}");
                        throw new Exception($"Unable to load books. Please verify:\n1. Database connection is active\n2. Books table exists\n3. Table has required columns: BookID, Title, Author\n\nOriginal Error: {ex.Message}\nFallback Error: {ex2.Message}", ex2);
                    }
                }

                if (dt != null)
                {
                    System.Diagnostics.Debug.WriteLine($"BookRepository.GetAll: DataTable has {dt.Rows.Count} rows and {dt.Columns.Count} columns");
                    
                    if (dt.Rows.Count > 0)
                    {
                        System.Diagnostics.Debug.WriteLine($"BookRepository.GetAll: Processing {dt.Rows.Count} rows...");
                        
                        // Log first row data for debugging
                        if (dt.Rows.Count > 0)
                        {
                            var firstRow = dt.Rows[0];
                            System.Diagnostics.Debug.WriteLine("First row sample data:");
                            foreach (DataColumn col in dt.Columns)
                            {
                                var value = firstRow[col] != DBNull.Value ? firstRow[col].ToString() : "(NULL)";
                                System.Diagnostics.Debug.WriteLine($"  {col.ColumnName}: {value}");
                            }
                        }
                        
                        foreach (DataRow row in dt.Rows)
                        {
                            try
                            {
                                var book = MapDataRowToBook(row);
                                if (book != null)
                                {
                                    books.Add(book);
                                    System.Diagnostics.Debug.WriteLine($"Mapped book: {book.Title} by {book.Author}");
                                }
                            }
                            catch (Exception rowEx)
                            {
                                System.Diagnostics.Debug.WriteLine($"Error mapping book row: {rowEx.Message}");
                                System.Diagnostics.Debug.WriteLine($"Stack trace: {rowEx.StackTrace}");
                                // Continue with next row
                            }
                        }
                        System.Diagnostics.Debug.WriteLine($"BookRepository.GetAll: Successfully mapped {books.Count} books out of {dt.Rows.Count} rows");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("BookRepository.GetAll: DataTable is empty (0 rows)");
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("BookRepository.GetAll: DataTable is null");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting all books: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                // Re-throw to allow caller to handle
                throw;
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
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = @"UPDATE Books SET Title=@Title, Author=@Author, ISBN=@ISBN, Category=@Category,
                                    Publisher=@Publisher, PublicationYear=@PublicationYear, Language=@Language,
                                    TotalCopies=@TotalCopies, Available=@Available, Location=@Location, 
                                    Status=@Status, CallNumber=@CallNumber, BookType=@BookType
                                    WHERE BookID=@BookID";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookID", book.BookID);
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

        public bool Delete(int bookId)
        {
            try
            {
                string query = "DELETE FROM Books WHERE BookID = @BookID";
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookID", bookId);
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

        public bool UpdateAvailability(int bookId, int change)
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = @"UPDATE Books 
                                    SET Available = GREATEST(0, LEAST(Available + @Change, TotalCopies))
                                    WHERE BookID = @BookID AND Available + @Change >= 0 AND Available + @Change <= TotalCopies";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookID", bookId);
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

        public int GetAvailableCount(int bookId)
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT Available FROM Books WHERE BookID = @BookID";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookID", bookId);
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

        private Book MapDataRowToBook(DataRow row)
        {
            try
            {
                // Helper function to safely get column value
                string GetStringValue(string columnName, string defaultValue = "")
                {
                    if (row.Table.Columns.Contains(columnName) && row[columnName] != DBNull.Value)
                        return row[columnName]?.ToString() ?? defaultValue;
                    return defaultValue;
                }
                
                int GetIntValue(string columnName, int defaultValue = 0)
                {
                    if (row.Table.Columns.Contains(columnName) && row[columnName] != DBNull.Value)
                    {
                        if (int.TryParse(row[columnName].ToString(), out int result))
                            return result;
                    }
                    return defaultValue;
                }
                
                // Handle Category - could be CategoryID or Category
                string category = GetStringValue("CategoryID");
                if (string.IsNullOrEmpty(category))
                {
                    category = GetStringValue("Category");
                }
                
                // Handle PublicationYear - could be PublicationYear or YearPublished
                int publicationYear = GetIntValue("PublicationYear");
                if (publicationYear == 0)
                {
                    publicationYear = GetIntValue("YearPublished");
                }
                
                // Handle TotalCopies - could be TotalCopies or Copies
                int totalCopies = GetIntValue("TotalCopies");
                if (totalCopies == 0)
                {
                    totalCopies = GetIntValue("Copies");
                }
                // Default to 1 if no copies column exists
                if (totalCopies == 0)
                {
                    totalCopies = 1;
                }
                
                // Handle Available - default to totalCopies if not found
                int available = GetIntValue("Available");
                if (available == 0 && totalCopies > 0)
                {
                    available = totalCopies;
                }
                
                // Handle AccessionNo - could be AccessionNo or Barcode
                string accessionNo = GetStringValue("AccessionNo");
                if (string.IsNullOrEmpty(accessionNo))
                {
                    accessionNo = GetStringValue("Barcode");
                }
                
                return new Book
                {
                    BookID = GetIntValue("BookID"),
                    Title = GetStringValue("Title"),
                    Author = GetStringValue("Author"),
                    ISBN = GetStringValue("ISBN"),
                    Category = category,
                    Publisher = GetStringValue("Publisher"),
                    PublicationYear = publicationYear,
                    Language = GetStringValue("Language"),
                    TotalCopies = totalCopies,
                    Available = available,
                    Location = GetStringValue("Location"),
                    Status = GetStringValue("Status", "Available"),
                    AccessionNo = accessionNo,
                    CallNumber = GetStringValue("CallNumber"),
                    BookType = GetStringValue("BookType")
                };
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error mapping DataRow to Book: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Available columns: {string.Join(", ", row.Table.Columns.Cast<DataColumn>().Select(c => c.ColumnName))}");
                throw;
            }
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
