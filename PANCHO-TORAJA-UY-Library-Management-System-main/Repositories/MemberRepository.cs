using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;
using Project5LMS.Data;
using Project5LMS.Models;

namespace Project5LMS.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly DatabaseContext _dbContext;

        public MemberRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Member GetById(int memberId)
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM Members WHERE MemberID = @MemberID LIMIT 1";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", memberId);
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                return MapDataRowToMember(dt.Rows[0]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting member by ID: {ex.Message}");
            }
            return null;
        }

        public Member GetByEmail(string email)
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM Members WHERE Email = @Email LIMIT 1";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                return MapDataRowToMember(dt.Rows[0]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting member by email: {ex.Message}");
            }
            return null;
        }

        public IEnumerable<Member> GetAll()
        {
            List<Member> members = new List<Member>();
            try
            {
                System.Diagnostics.Debug.WriteLine("MemberRepository.GetAll: Starting to fetch members...");
                
                // Start with basic query that should always work - FirstName and LastName are required
                string query = @"SELECT MemberID, FirstName, LastName, Email FROM Members ORDER BY LastName, FirstName";
                DataTable dt = null;
                
                // Try enhanced query with all columns
                try
                {
                    query = @"SELECT MemberID, FirstName, LastName, Email, 
                             COALESCE(Type, MemberType) as MemberType,
                             RegistrationDate, ExpirationDate, 
                             Status, Contact, Address
                             FROM Members 
                             ORDER BY LastName, FirstName";
                    dt = _dbContext.ExecuteQuery(query);
                    System.Diagnostics.Debug.WriteLine($"MemberRepository.GetAll: Enhanced query succeeded, got {dt?.Rows.Count ?? 0} rows");
                }
                catch (MySqlException sqlEx) when (sqlEx.Message.Contains("Unknown column 'MemberType'"))
                {
                    // Fall back to query with 'Type' column if 'MemberType' doesn't exist
                    try
                    {
                        query = @"SELECT MemberID, FirstName, LastName, Email, 
                                 Type as MemberType, RegistrationDate, ExpirationDate, 
                                 Status, Contact, Address
                                 FROM Members 
                                 ORDER BY LastName, FirstName";
                        dt = _dbContext.ExecuteQuery(query);
                        System.Diagnostics.Debug.WriteLine($"MemberRepository.GetAll: Query with Type column succeeded, got {dt?.Rows.Count ?? 0} rows");
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error executing query with Type column: {ex.Message}");
                        throw;
                    }
                }
                catch (MySqlException sqlEx) when (sqlEx.Message.Contains("Unknown column 'Type'"))
                {
                    // Fall back to basic query if neither 'Type' nor 'MemberType' exists
                    try
                    {
                        query = @"SELECT MemberID, FirstName, LastName, Email FROM Members ORDER BY LastName, FirstName";
                        dt = _dbContext.ExecuteQuery(query);
                        System.Diagnostics.Debug.WriteLine($"MemberRepository.GetAll: Basic query succeeded, got {dt?.Rows.Count ?? 0} rows");
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error executing basic query: {ex.Message}");
                        throw new Exception($"Unable to load members. Please verify:\n1. Database connection is active\n2. Members table exists\n3. Table has required columns: MemberID, FirstName, LastName, Email\n\nError: {ex.Message}", ex);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error executing enhanced query: {ex.Message}");
                    // Fall back to basic query
                    try
                    {
                        query = @"SELECT MemberID, FirstName, LastName, Email FROM Members ORDER BY LastName, FirstName";
                        dt = _dbContext.ExecuteQuery(query);
                        System.Diagnostics.Debug.WriteLine($"MemberRepository.GetAll: Fallback basic query succeeded, got {dt?.Rows.Count ?? 0} rows");
                    }
                    catch (Exception ex2)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error executing fallback basic query: {ex2.Message}");
                        throw new Exception($"Unable to load members. Please verify:\n1. Database connection is active\n2. Members table exists\n3. Table has required columns: MemberID, FirstName, LastName, Email\n\nError: {ex2.Message}", ex2);
                    }
                }
                
                if (dt != null && dt.Rows.Count > 0)
                {
                    System.Diagnostics.Debug.WriteLine($"MemberRepository.GetAll: Processing {dt.Rows.Count} rows...");
                    foreach (DataRow row in dt.Rows)
                    {
                        try
                        {
                            var member = MapDataRowToMember(row);
                            members.Add(member);
                        }
                        catch (Exception rowEx)
                        {
                            System.Diagnostics.Debug.WriteLine($"Error mapping member row: {rowEx.Message}");
                            System.Diagnostics.Debug.WriteLine($"Stack trace: {rowEx.StackTrace}");
                            // Continue with next row
                        }
                    }
                    System.Diagnostics.Debug.WriteLine($"MemberRepository.GetAll: Successfully mapped {members.Count} members");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("MemberRepository.GetAll: No rows returned from query");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting all members: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                // Re-throw to allow caller to handle
                throw;
            }
            return members;
        }

        public IEnumerable<Member> Search(string searchTerm)
        {
            List<Member> members = new List<Member>();
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT * FROM Members 
                                    WHERE FirstName LIKE @SearchTerm 
                                    OR LastName LIKE @SearchTerm 
                                    OR Email LIKE @SearchTerm
                                    OR MemberID LIKE @SearchTerm
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
                                members.Add(MapDataRowToMember(row));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error searching members: {ex.Message}");
            }
            return members;
        }

        public bool Add(Member member)
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = @"INSERT INTO Members (FirstName, LastName, Email, Type, RegistrationDate, 
                                    ExpirationDate, Status, Contact, Address)
                                    VALUES (@FirstName, @LastName, @Email, @Type, @RegistrationDate, 
                                    @ExpirationDate, @Status, @Contact, @Address)";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        MapMemberToParameters(cmd, member);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error adding member: {ex.Message}");
                return false;
            }
        }

        public bool Update(Member member)
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = @"UPDATE Members SET FirstName=@FirstName, LastName=@LastName, 
                                    Email=@Email, Type=@Type, ExpirationDate=@ExpirationDate, 
                                    Status=@Status, Contact=@Contact, Address=@Address
                                    WHERE MemberID=@MemberID";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", member.MemberID);
                        MapMemberToParameters(cmd, member);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating member: {ex.Message}");
                return false;
            }
        }

        public bool Delete(int memberId)
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "DELETE FROM Members WHERE MemberID = @MemberID";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", memberId);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error deleting member: {ex.Message}");
                return false;
            }
        }

        public bool Exists(int memberId)
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Members WHERE MemberID = @MemberID";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", memberId);
                        object result = cmd.ExecuteScalar();
                        return result != null && Convert.ToInt32(result) > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error checking member existence: {ex.Message}");
                return false;
            }
        }

        public int GetActiveBorrowingCount(int memberId)
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Transactions WHERE MemberID = @MemberID AND Status = 'Borrowed'";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", memberId);
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
                System.Diagnostics.Debug.WriteLine($"Error getting active borrowing count: {ex.Message}");
            }
            return 0;
        }

        private Member MapDataRowToMember(DataRow row)
        {
            // Handle both 'Type' and 'MemberType' column names
            string memberType = string.Empty;
            if (row.Table.Columns.Contains("MemberType") && row["MemberType"] != DBNull.Value)
            {
                memberType = row["MemberType"]?.ToString() ?? string.Empty;
            }
            else if (row.Table.Columns.Contains("Type") && row["Type"] != DBNull.Value)
            {
                memberType = row["Type"]?.ToString() ?? string.Empty;
            }
            
            try
            {
                return new Member
                {
                    MemberID = row.Table.Columns.Contains("MemberID") && row["MemberID"] != DBNull.Value
                        ? Convert.ToInt32(row["MemberID"])
                        : 0,
                    FirstName = row.Table.Columns.Contains("FirstName") && row["FirstName"] != DBNull.Value
                        ? row["FirstName"]?.ToString() ?? string.Empty
                        : string.Empty,
                    LastName = row.Table.Columns.Contains("LastName") && row["LastName"] != DBNull.Value
                        ? row["LastName"]?.ToString() ?? string.Empty
                        : string.Empty,
                    Email = row.Table.Columns.Contains("Email") && row["Email"] != DBNull.Value
                        ? row["Email"]?.ToString() ?? string.Empty
                        : string.Empty,
                    Type = memberType,
                    RegistrationDate = row.Table.Columns.Contains("RegistrationDate") && row["RegistrationDate"] != DBNull.Value 
                        ? Convert.ToDateTime(row["RegistrationDate"]) 
                        : DateTime.Now,
                    ExpirationDate = row.Table.Columns.Contains("ExpirationDate") && row["ExpirationDate"] != DBNull.Value 
                        ? Convert.ToDateTime(row["ExpirationDate"]) 
                        : DateTime.Now.AddYears(1),
                    Status = row.Table.Columns.Contains("Status") && row["Status"] != DBNull.Value 
                        ? row["Status"]?.ToString() ?? "Active" 
                        : "Active",
                    Contact = row.Table.Columns.Contains("Contact") && row["Contact"] != DBNull.Value 
                        ? row["Contact"]?.ToString() ?? string.Empty 
                        : string.Empty,
                    Address = row.Table.Columns.Contains("Address") && row["Address"] != DBNull.Value 
                        ? row["Address"]?.ToString() ?? string.Empty 
                        : string.Empty
                };
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error mapping DataRow to Member: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Available columns: {string.Join(", ", row.Table.Columns.Cast<DataColumn>().Select(c => c.ColumnName))}");
                throw;
            }
        }

        private void MapMemberToParameters(MySqlCommand cmd, Member member)
        {
            cmd.Parameters.AddWithValue("@FirstName", member.FirstName);
            cmd.Parameters.AddWithValue("@LastName", member.LastName);
            cmd.Parameters.AddWithValue("@Email", member.Email);
            cmd.Parameters.AddWithValue("@Type", member.Type ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@RegistrationDate", member.RegistrationDate);
            cmd.Parameters.AddWithValue("@ExpirationDate", member.ExpirationDate);
            cmd.Parameters.AddWithValue("@Status", member.Status ?? "Active");
            cmd.Parameters.AddWithValue("@Contact", member.Contact ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Address", member.Address ?? (object)DBNull.Value);
        }
    }
}

