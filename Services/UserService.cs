using MySql.Data.MySqlClient;
using Project5LMS.Models;
using Project5LMS.Data;
using System;
using System.Linq;

namespace Project5LMS.Services
{
    public class UserService
    {
        private readonly DatabaseContext _db;

        public UserService()
        {
            _db = new DatabaseContext();
        }

        public User Login(string email, string password)
        {
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM Users WHERE Email = @email AND PasswordHash = @password LIMIT 1";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Read FirstName and LastName directly from database
                            string firstName = string.Empty;
                            string lastName = string.Empty;
                            
                            try
                            {
                                int firstNameOrdinal = reader.GetOrdinal("FirstName");
                                if (!reader.IsDBNull(firstNameOrdinal))
                                {
                                    firstName = reader.GetString(firstNameOrdinal)?.Trim() ?? string.Empty;
                                }
                            }
                            catch
                            {
                                // FirstName column doesn't exist, leave empty
                            }

                            try
                            {
                                int lastNameOrdinal = reader.GetOrdinal("LastName");
                                if (!reader.IsDBNull(lastNameOrdinal))
                                {
                                    lastName = reader.GetString(lastNameOrdinal)?.Trim() ?? string.Empty;
                                }
                            }
                            catch
                            {
                                // LastName column doesn't exist, leave empty
                            }

                            // Try to get Email, fallback to Username if Email column doesn't exist
                            string emailValue = string.Empty;
                            try
                            {
                                emailValue = reader.GetString("Email");
                            }
                            catch
                            {
                                // If Email column doesn't exist, try Username
                                try
                                {
                                    emailValue = reader.GetString("Username");
                                }
                                catch { }
                            }

                            return new User()
                            {
                                UserID = reader.GetInt32("UserID"),
                                FirstName = firstName,
                                LastName = lastName,
                                Email = emailValue,
                                Role = reader.GetString("Role")
                            };
                        }
                    }
                }
            }

            return null; // Invalid email or password
        }
    }
}
