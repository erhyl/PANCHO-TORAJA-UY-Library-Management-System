using System;
using System.Collections.Generic;
using System.Data;
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
                    string query = @"SELECT
                                    MemberID, FirstName, LastName, Email,
                                    COALESCE(Type, MemberType) as Type,
                                    RegistrationDate, ExpirationDate, Status,
                                    Contact, Address, PhotoPath, ValidIDPath, MemberCardNumber
                                    FROM Members WHERE MemberID = @MemberID LIMIT 1";
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
                    string query = @"SELECT
                                    MemberID, FirstName, LastName, Email,
                                    COALESCE(Type, MemberType) as Type,
                                    RegistrationDate, ExpirationDate, Status,
                                    Contact, Address, PhotoPath, ValidIDPath, MemberCardNumber
                                    FROM Members WHERE Email = @Email LIMIT 1";
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
                string query = @"SELECT
                                MemberID, FirstName, LastName, Email,
                                COALESCE(Type, MemberType) as Type,
                                RegistrationDate, ExpirationDate, Status,
                                Contact, Address, PhotoPath, ValidIDPath, MemberCardNumber
                                FROM Members ORDER BY LastName, FirstName";
                DataTable dt = _dbContext.ExecuteQuery(query);
                foreach (DataRow row in dt.Rows)
                {
                    members.Add(MapDataRowToMember(row));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting all members: {ex.Message}");
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
                    string typeColumn = "Type";
                    using (var checkCmd = new MySqlCommand("SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'Members' AND COLUMN_NAME = 'Type'", conn))
                    {
                        int hasType = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (hasType == 0)
                        {
                            using (var checkCmd2 = new MySqlCommand("SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'Members' AND COLUMN_NAME = 'MemberType'", conn))
                            {
                                int hasMemberType = Convert.ToInt32(checkCmd2.ExecuteScalar());
                                if (hasMemberType > 0)
                                {
                                    typeColumn = "MemberType";
                                }
                            }
                        }
                    }
                    string query = $@"UPDATE Members SET FirstName=@FirstName, LastName=@LastName,
                                    Email=@Email, {typeColumn}=@Type, ExpirationDate=@ExpirationDate,
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
            string memberType = string.Empty;
            if (row.Table.Columns.Contains("Type") && row["Type"] != DBNull.Value)
            {
                memberType = row["Type"].ToString();
            }
            else if (row.Table.Columns.Contains("MemberType") && row["MemberType"] != DBNull.Value)
            {
                memberType = row["MemberType"].ToString();
            }
            return new Member
            {
                MemberID = Convert.ToInt32(row["MemberID"]),
                FirstName = row["FirstName"]?.ToString() ?? string.Empty,
                LastName = row["LastName"]?.ToString() ?? string.Empty,
                Email = row["Email"]?.ToString() ?? string.Empty,
                Type = memberType,
                RegistrationDate = row["RegistrationDate"] != DBNull.Value ? Convert.ToDateTime(row["RegistrationDate"]) : DateTime.Now,
                ExpirationDate = row["ExpirationDate"] != DBNull.Value ? Convert.ToDateTime(row["ExpirationDate"]) : DateTime.Now,
                Status = row["Status"]?.ToString() ?? string.Empty,
                Contact = row.Table.Columns.Contains("Contact") && row["Contact"] != DBNull.Value ? row["Contact"].ToString() : string.Empty,
                Address = row.Table.Columns.Contains("Address") && row["Address"] != DBNull.Value ? row["Address"].ToString() : string.Empty,
                PhotoPath = row.Table.Columns.Contains("PhotoPath") && row["PhotoPath"] != DBNull.Value ? row["PhotoPath"].ToString() : null,
                ValidIDPath = row.Table.Columns.Contains("ValidIDPath") && row["ValidIDPath"] != DBNull.Value ? row["ValidIDPath"].ToString() : null,
                MemberCardNumber = row.Table.Columns.Contains("MemberCardNumber") && row["MemberCardNumber"] != DBNull.Value ? row["MemberCardNumber"].ToString() : null
            };
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