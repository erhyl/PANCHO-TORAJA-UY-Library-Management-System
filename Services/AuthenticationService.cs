using MySql.Data.MySqlClient;
using Project5LMS.Data;
using Project5LMS.Interfaces;
using Project5LMS.Models;
using Project5LMS.Helpers;
using System;
namespace Project5LMS.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly DatabaseContext _db;
        public AuthenticationService(DatabaseContext dbContext)
        {
            _db = dbContext ?? throw new System.ArgumentNullException(nameof(dbContext));
        }
        public User Login(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return null;
            
            // Normalize email - trim and convert to lowercase for comparison
            email = email.Trim().ToLowerInvariant();
            
            try
            {
                using (var conn = _db.GetConnection())
                {
                    conn.Open();
                    // Use case-insensitive comparison with LOWER() function
                    string query = "SELECT UserID, Email, PasswordHash, FirstName, LastName, Role, COALESCE(Status, 'Active') as Status FROM Users WHERE LOWER(TRIM(Email)) = @email LIMIT 1";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@email", email);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                System.Diagnostics.Debug.WriteLine($"Login error: No user found with email: {email}");
                                return null;
                            }
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
                                System.Diagnostics.Debug.WriteLine($"Login error: Could not read PasswordHash for email: {email}");
                                return null;
                            }
                            
                            if (string.IsNullOrEmpty(storedHash))
                            {
                                System.Diagnostics.Debug.WriteLine($"Login error: PasswordHash is empty for email: {email}");
                                return null;
                            }
                            
                            bool passwordValid = PasswordHasher.Verify(password, storedHash);
                            if (!passwordValid)
                            {
                                System.Diagnostics.Debug.WriteLine($"Login error: Password verification failed for email: {email}");
                                return null;
                            }
                            // Check if user is suspended
                            try
                            {
                                string status = "Active";
                                int statusOrdinal = reader.GetOrdinal("Status");
                                if (!reader.IsDBNull(statusOrdinal))
                                {
                                    status = reader.GetString(statusOrdinal);
                                }
                                if (status == "Suspended")
                                {
                                    System.Diagnostics.Debug.WriteLine($"Login attempt by suspended user: {email}");
                                    return null;
                                }
                            }
                            catch
                            {
                                // Status column doesn't exist, allow login (backward compatibility)
                            }
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
                            }
                            string emailValue = string.Empty;
                            try
                            {
                                emailValue = reader.GetString("Email");
                            }
                            catch
                            {
                                try
                                {
                                    emailValue = reader.GetString("Username");
                                }
                                catch
                                {
                                    return null;
                                }
                            }
                            string role = string.Empty;
                            try
                            {
                                role = reader.GetString("Role");
                            }
                            catch
                            {
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
                System.Diagnostics.Debug.WriteLine($"Authentication error: {ex.Message}");
                return null;
            }
        }
    }
}