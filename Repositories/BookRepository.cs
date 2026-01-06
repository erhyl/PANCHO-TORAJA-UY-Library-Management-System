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
