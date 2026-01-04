using MySql.Data.MySqlClient;
using Project5LMS.Data;
using Project5LMS.Models;
using Project5LMS.Helpers;
using System;
using System.Linq;

namespace Project5LMS.Services
{
    public class AuthenticationService
    {
        private readonly DatabaseContext _db;

        public AuthenticationService()
        {
            _db = new DatabaseContext();
        }

        /// <summary>
        /// Authenticates a user by email and password
        /// </summary>
        /// <param name="email">User's email address</param>
        /// <param name="password">Plain text password</param>
        /// <returns>User object if authentication succeeds, null otherwise</returns>
        public User Login(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return null;

            try
            {
                using (var conn = _db.GetConnection())
                {
                    conn.Open();
                    // First, get the user by email (don't compare password in SQL)
                    const string query = "SELECT UserID, Email, PasswordHash, FirstName, LastName, Role FROM Users WHERE Email = @email LIMIT 1";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@email", email);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read()) return null;

                            // Get the stored password hash
                            string storedHash = string.Empty;
                            try
                            {
                                int passwordHashOrdinal = reader.GetOrdinal("PasswordHash");
                                if (!reader.IsDBNull(passwordHashOrdinal))
                                {
                                    storedHash = reader.GetString(passwordHashOrdinal);
                                }
                            }
                            catch
                            {
                                // PasswordHash column doesn't exist
                                return null;
                            }

                            // Verify the password using PasswordHasher
                            if (!PasswordHasher.Verify(password, storedHash))
                            {
                                // Password doesn't match
                                return null;
                            }

                            // Password is correct, build and return User object
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

                            // Get Email
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
                                catch 
                                { 
                                    // Neither Email nor Username found
                                    return null;
                                }
                            }

                            // Get Role
                            string role = string.Empty;
                            try
                            {
                                role = reader.GetString("Role");
                            }
                            catch
                            {
                                // Role column doesn't exist
                                return null;
                            }

                            return new User
                            {
                                UserID = reader.GetInt32("UserID"),
                                FirstName = firstName,
                                LastName = lastName,
                                Email = emailValue,
                                Role = role
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception (in production, use proper logging)
                System.Diagnostics.Debug.WriteLine($"Authentication error: {ex.Message}");
                return null;
            }
        }
    }
}
