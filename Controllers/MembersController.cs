using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
namespace Project5LMS.Controllers
{
    public class MembersController
    {
        private string connectionString;
        public MembersController()
        {
            connectionString = ConfigurationManager.ConnectionStrings["MySqlConnectionString"]?.ConnectionString
                ?? throw new InvalidOperationException("Connection string 'MySqlConnectionString' not found.");
        }
        public DataTable GetMembers()
        {
            string query = @"SELECT
                                MemberID,
                                FirstName,
                                LastName,
                                COALESCE(Type, MemberType) as MemberType,
                                Email,
                                RegistrationDate,
                                ExpirationDate,
                                Status
                             FROM Members
                             ORDER BY LastName, FirstName";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        public bool AddMember(string firstName, string lastName, string email, string type,
                              DateTime regDate, DateTime expDate, string status)
        {
            string query = @"INSERT INTO Members
                            (FirstName, LastName, Email, Type, RegistrationDate, ExpirationDate, Status)
                             VALUES
                            (@FirstName, @LastName, @Email, @Type, @RegDate, @ExpDate, @Status)";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Type", type);
                cmd.Parameters.AddWithValue("@RegDate", regDate);
                cmd.Parameters.AddWithValue("@ExpDate", expDate);
                cmd.Parameters.AddWithValue("@Status", status);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public DataTable SearchMembers(string keyword, string type, string status)
        {
            string query = @"SELECT
                        MemberID,
                        FirstName,
                        LastName,
                        COALESCE(Type, MemberType) as MemberType,
                        Email,
                        RegistrationDate,
                        ExpirationDate,
                        Status
                     FROM Members
                     WHERE
                        (FirstName LIKE @Keyword OR LastName LIKE @Keyword OR Email LIKE @Keyword)
                        AND (@Type = 'All' OR COALESCE(Type, MemberType) = @Type)
                        AND (@Status = 'All' OR Status = @Status)
                     ORDER BY LastName, FirstName";
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
        public bool UpdateMember(int memberId, string firstName, string lastName, string email, string type,
                                 DateTime regDate, DateTime expDate, string status)
        {
            string query = @"UPDATE Members
                             SET FirstName=@FirstName, LastName=@LastName, Email=@Email, Type=@Type,
                                 RegistrationDate=@RegDate, ExpirationDate=@ExpDate, Status=@Status
                             WHERE MemberID=@ID";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ID", memberId);
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
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