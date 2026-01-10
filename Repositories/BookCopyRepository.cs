using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Project5LMS.Data;
using Project5LMS.Models;

namespace Project5LMS.Repositories
{
    public class BookCopyRepository : IBookCopyRepository
    {
        private readonly DatabaseContext _dbContext;

        public BookCopyRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public BookCopy GetById(int copyId)
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM BookCopies WHERE CopyID = @CopyID LIMIT 1";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CopyID", copyId);
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                return MapDataRowToCopy(dt.Rows[0]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting copy by ID: {ex.Message}");
            }
            return null;
        }

        public BookCopy GetByAccessionNumber(string accessionNumber)
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM BookCopies WHERE AccessionNumber = @AccessionNumber LIMIT 1";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@AccessionNumber", accessionNumber);
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                return MapDataRowToCopy(dt.Rows[0]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting copy by accession number: {ex.Message}");
            }
            return null;
        }

        public IEnumerable<BookCopy> GetByBookId(int bookId)
        {
            List<BookCopy> copies = new List<BookCopy>();
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM BookCopies WHERE BookID = @BookID ORDER BY CopyID";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookID", bookId);
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            foreach (DataRow row in dt.Rows)
                            {
                                copies.Add(MapDataRowToCopy(row));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting copies by book ID: {ex.Message}");
            }
            return copies;
        }

        public IEnumerable<BookCopy> GetByStatus(string status)
        {
            List<BookCopy> copies = new List<BookCopy>();
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM BookCopies WHERE CopyStatus = @Status ORDER BY CopyID";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Status", status);
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            foreach (DataRow row in dt.Rows)
                            {
                                copies.Add(MapDataRowToCopy(row));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting copies by status: {ex.Message}");
            }
            return copies;
        }

        public bool Add(BookCopy copy)
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = @"INSERT INTO BookCopies (BookID, AccessionNumber, Barcode, CopyStatus, Location, Notes, CreatedDate)
                                    VALUES (@BookID, @AccessionNumber, @Barcode, @CopyStatus, @Location, @Notes, @CreatedDate)";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        MapCopyToParameters(cmd, copy);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error adding copy: {ex.Message}");
                return false;
            }
        }

        public bool Update(BookCopy copy)
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = @"UPDATE BookCopies SET BookID=@BookID, AccessionNumber=@AccessionNumber, 
                                    Barcode=@Barcode, CopyStatus=@CopyStatus, Location=@Location, Notes=@Notes,
                                    LastCheckedOut=@LastCheckedOut, LastReturned=@LastReturned, ModifiedDate=@ModifiedDate
                                    WHERE CopyID=@CopyID";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CopyID", copy.CopyID);
                        MapCopyToParameters(cmd, copy);
                        cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating copy: {ex.Message}");
                return false;
            }
        }

        public bool Delete(int copyId)
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "DELETE FROM BookCopies WHERE CopyID = @CopyID";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CopyID", copyId);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error deleting copy: {ex.Message}");
                return false;
            }
        }

        public bool UpdateStatus(int copyId, string status)
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE BookCopies SET CopyStatus = @Status, ModifiedDate = @ModifiedDate WHERE CopyID = @CopyID";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CopyID", copyId);
                        cmd.Parameters.AddWithValue("@Status", status);
                        cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating copy status: {ex.Message}");
                return false;
            }
        }

        private BookCopy MapDataRowToCopy(DataRow row)
        {
            return new BookCopy
            {
                CopyID = Convert.ToInt32(row["CopyID"]),
                BookID = Convert.ToInt32(row["BookID"]),
                AccessionNumber = row["AccessionNumber"]?.ToString() ?? string.Empty,
                Barcode = row["Barcode"]?.ToString() ?? string.Empty,
                CopyStatus = row["CopyStatus"]?.ToString() ?? "Available",
                Location = row["Location"]?.ToString() ?? string.Empty,
                Notes = row["Notes"]?.ToString() ?? string.Empty,
                LastCheckedOut = row["LastCheckedOut"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["LastCheckedOut"]) : null,
                LastReturned = row["LastReturned"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["LastReturned"]) : null,
                CreatedDate = row["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(row["CreatedDate"]) : DateTime.Now,
                ModifiedDate = row["ModifiedDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["ModifiedDate"]) : null
            };
        }

        private void MapCopyToParameters(MySqlCommand cmd, BookCopy copy)
        {
            cmd.Parameters.AddWithValue("@BookID", copy.BookID);
            cmd.Parameters.AddWithValue("@AccessionNumber", copy.AccessionNumber ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Barcode", copy.Barcode ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@CopyStatus", copy.CopyStatus ?? "Available");
            cmd.Parameters.AddWithValue("@Location", copy.Location ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Notes", copy.Notes ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@LastCheckedOut", copy.LastCheckedOut.HasValue ? copy.LastCheckedOut.Value : (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@LastReturned", copy.LastReturned.HasValue ? copy.LastReturned.Value : (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedDate", copy.CreatedDate);
        }
    }
}

