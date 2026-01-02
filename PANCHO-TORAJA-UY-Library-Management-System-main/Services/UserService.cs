using MySql.Data.MySqlClient;
using Project5LMS.Models;
using Project5LMS.Data;
using System;

namespace Project5LMS.Services
{
    public class UserService
    {
        private readonly DatabaseContext _db;

        public UserService()
        {
            _db = new DatabaseContext();
        }

        public User Login(string username, string password)
        {
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM Users WHERE Username = @username AND PasswordHash = @password LIMIT 1";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User()
                            {
                                UserID = reader.GetInt32("UserID"),
                                FullName = reader.GetString("FullName"),
                                Username = reader.GetString("Username"),
                                Role = reader.GetString("Role")
                            };
                        }
                    }
                }
            }

            return null; // Invalid username or password
        }
    }
}
