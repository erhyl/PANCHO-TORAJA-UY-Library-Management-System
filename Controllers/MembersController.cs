using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;

namespace Project5LMS.Controllers
{
    internal class MembersController
    {
        private string connectionString;

        // ✅ Parameterless constructor: automatically reads connection string
        public MembersController()
        {
            connectionString = ConfigurationManager.ConnectionStrings["MySqlConnectionString"]?.ConnectionString
                ?? throw new InvalidOperationException("Connection string 'MySqlConnectionString' not found.");
        }

        // 🔹 GET ALL MEMBERS
        public DataTable GetMembers()
        {
            string query = @"SELECT 
                                MemberID,
                                FullName,
                                MemberType,
                                Email,
                                RegistrationDate,
                                ExpirationDate,
                                Status
                             FROM Members
                             ORDER BY FullName";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // 🔹 ADD MEMBER
        public bool AddMember(string fullName, string email, string type,
                              DateTime regDate, DateTime expDate, string status)
        {
            string query = @"INSERT INTO Members
                            (FullName, Email, MemberType, RegistrationDate, ExpirationDate, Status)
                             VALUES
                            (@Name, @Email, @Type, @RegDate, @ExpDate, @Status)";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Name", fullName);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Type", type);
                cmd.Parameters.AddWithValue("@RegDate", regDate);
                cmd.Parameters.AddWithValue("@ExpDate", expDate);
                cmd.Parameters.AddWithValue("@Status", status);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // 🔹 SEARCH MEMBERS
        public DataTable SearchMembers(string keyword, string type, string status)
        {
            string query = @"SELECT 
                        MemberID,
                        FullName,
                        MemberType,
                        Email,
                        RegistrationDate,
                        ExpirationDate,
                        Status
                     FROM Members
                     WHERE
                        (FullName LIKE @Keyword OR Email LIKE @Keyword)
                        AND (@Type = 'All' OR MemberType = @Type)
                        AND (@Status = 'All' OR Status = @Status)
                     ORDER BY FullName";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                cmd.Parameters.AddWithValue("@Type", type);
                cmd.Parameters.AddWithValue("@Status", status);

                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // 🔹 UPDATE MEMBER
        public bool UpdateMember(int memberId, string fullName, string email, string type,
                                 DateTime regDate, DateTime expDate, string status)
        {
            string query = @"UPDATE Members
                             SET FullName=@Name, Email=@Email, MemberType=@Type,
                                 RegistrationDate=@RegDate, ExpirationDate=@ExpDate, Status=@Status
                             WHERE MemberID=@ID";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ID", memberId);
                cmd.Parameters.AddWithValue("@Name", fullName);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Type", type);
                cmd.Parameters.AddWithValue("@RegDate", regDate);
                cmd.Parameters.AddWithValue("@ExpDate", expDate);
                cmd.Parameters.AddWithValue("@Status", status);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
