using Project5LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Project5LMS.Helpers
{
    public static class CurrentUser
    {
        public static int UserID { get; set; }
        public static string FirstName { get; set; }
        public static string LastName { get; set; }
        public static string Email { get; set; }
        public static string Role { get; set; }

        // Read-only full name for compatibility. Prefer using FirstName/LastName.
        public static string FullName => string.Join(" ", new[] { FirstName, LastName }.Where(s => !string.IsNullOrWhiteSpace(s)));

        public static void Set(User user)
        {
            UserID = user.UserID;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Role = user.Role;
        }

        public static void Clear()
        {
            UserID = 0;
            FirstName = null;
            LastName = null;
            Email = null;
            Role = null;
        }

        /// <summary>
        /// Gets the MemberID for the current logged-in user by matching email with Members table
        /// </summary>
        public static int GetMemberID()
        {
            if (string.IsNullOrWhiteSpace(Email))
                return 0;

            try
            {
                string connectionString = DatabaseHelper.GetConnectionString();
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT MemberID FROM Members WHERE Email = @Email LIMIT 1";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", Email);
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
                System.Diagnostics.Debug.WriteLine($"Error getting MemberID: {ex.Message}");
            }
            return 0;
        }
    }
}
