using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Project5LMS.Data;
using Project5LMS.Models;
namespace Project5LMS.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DatabaseContext _dbContext;
        public TransactionRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public CirculationRecord GetById(int transactionId)
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM Transactions WHERE TransactionID = @TransactionID LIMIT 1";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TransactionID", transactionId);
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                return MapDataRowToRecord(dt.Rows[0]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting transaction by ID: {ex.Message}");
            }
            return null;
        }
        public CirculationRecord GetActiveByBookId(int bookId)
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM Transactions WHERE BookID = @BookID AND Status = 'Borrowed' LIMIT 1";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookID", bookId);
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                return MapDataRowToRecord(dt.Rows[0]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting active transaction by book ID: {ex.Message}");
            }
            return null;
        }
        public IEnumerable<CirculationRecord> GetByMemberId(int memberId)
        {
            List<CirculationRecord> records = new List<CirculationRecord>();
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM Transactions WHERE MemberID = @MemberID ORDER BY BorrowDate DESC";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", memberId);
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            foreach (DataRow row in dt.Rows)
                            {
                                records.Add(MapDataRowToRecord(row));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting transactions by member ID: {ex.Message}");
            }
            return records;
        }
        public IEnumerable<CirculationRecord> GetByStatus(string status)
        {
            List<CirculationRecord> records = new List<CirculationRecord>();
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM Transactions WHERE Status = @Status ORDER BY BorrowDate DESC";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Status", status);
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            foreach (DataRow row in dt.Rows)
                            {
                                records.Add(MapDataRowToRecord(row));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting transactions by status: {ex.Message}");
            }
            return records;
        }
        public IEnumerable<CirculationRecord> GetOverdue()
        {
            List<CirculationRecord> records = new List<CirculationRecord>();
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM Transactions WHERE Status = 'Borrowed' AND DueDate < NOW() ORDER BY DueDate ASC";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            foreach (DataRow row in dt.Rows)
                            {
                                records.Add(MapDataRowToRecord(row));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting overdue transactions: {ex.Message}");
            }
            return records;
        }
        public bool Add(CirculationRecord transaction)
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = @"INSERT INTO Transactions (MemberID, BookID, BorrowDate, DueDate, Status, TransactionType)
                                    VALUES (@MemberID, @BookID, @BorrowDate, @DueDate, @Status, @TransactionType)";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        MapRecordToParameters(cmd, transaction);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error adding transaction: {ex.Message}");
                return false;
            }
        }
        public bool Update(CirculationRecord transaction)
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = @"UPDATE Transactions SET MemberID=@MemberID, BookID=@BookID,
                                    BorrowDate=@BorrowDate, DueDate=@DueDate, ReturnDate=@ReturnDate,
                                    Status=@Status, Fine=@Fine, TransactionType=@TransactionType,
                                    RenewalCount=@RenewalCount
                                    WHERE TransactionID=@TransactionID";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TransactionID", transaction.TransactionID);
                        MapRecordToParameters(cmd, transaction);
                        if (transaction.ReturnDate.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@ReturnDate", transaction.ReturnDate.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@ReturnDate", DBNull.Value);
                        }
                        if (transaction.Fine.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@Fine", transaction.Fine.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Fine", DBNull.Value);
                        }
                        cmd.Parameters.AddWithValue("@RenewalCount", transaction.RenewalCount);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating transaction: {ex.Message}");
                return false;
            }
        }
        public bool Delete(int transactionId)
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "DELETE FROM Transactions WHERE TransactionID = @TransactionID";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TransactionID", transactionId);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error deleting transaction: {ex.Message}");
                return false;
            }
        }
        public decimal CalculateFine(int transactionId)
        {
            var transaction = GetById(transactionId);
            if (transaction == null || transaction.ReturnDate.HasValue)
                return 0m;
            if (DateTime.Now <= transaction.DueDate)
                return 0m;
            TimeSpan overdueTime = DateTime.Now - transaction.DueDate;
            int daysOverdue = overdueTime.Days;
            if (daysOverdue == 0 && overdueTime.TotalHours > 0)
            {
                daysOverdue = 1;
            }
            return daysOverdue * Project5LMS.Helpers.Constants.DefaultFinePerDay;
        }
        private CirculationRecord MapDataRowToRecord(DataRow row)
        {
            return new CirculationRecord
            {
                TransactionID = Convert.ToInt32(row["TransactionID"]),
                MemberID = Convert.ToInt32(row["MemberID"]),
                BookID = Convert.ToInt32(row["BookID"]),
                BorrowDate = Convert.ToDateTime(row["BorrowDate"]),
                DueDate = Convert.ToDateTime(row["DueDate"]),
                ReturnDate = row["ReturnDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["ReturnDate"]) : null,
                Status = row["Status"]?.ToString() ?? string.Empty,
                Fine = row["Fine"] != DBNull.Value ? (decimal?)Convert.ToDecimal(row["Fine"]) : null,
                TransactionType = row["TransactionType"]?.ToString() ?? string.Empty,
                RenewalCount = row.Table.Columns.Contains("RenewalCount") && row["RenewalCount"] != DBNull.Value
                    ? Convert.ToInt32(row["RenewalCount"]) : 0
            };
        }
        private void MapRecordToParameters(MySqlCommand cmd, CirculationRecord record)
        {
            cmd.Parameters.AddWithValue("@MemberID", record.MemberID);
            cmd.Parameters.AddWithValue("@BookID", record.BookID);
            cmd.Parameters.AddWithValue("@BorrowDate", record.BorrowDate);
            cmd.Parameters.AddWithValue("@DueDate", record.DueDate);
            cmd.Parameters.AddWithValue("@Status", record.Status ?? "Borrowed");
            cmd.Parameters.AddWithValue("@TransactionType", record.TransactionType ?? "Borrow");
        }
    }
}