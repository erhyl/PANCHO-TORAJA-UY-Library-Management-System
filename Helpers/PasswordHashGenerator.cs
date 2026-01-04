using System;
using System.Windows.Forms;

namespace Project5LMS.Helpers
{
    /// <summary>
    /// Utility class to generate password hashes for database setup
    /// This can be used in a test form or console app to generate hashes
    /// </summary>
    public static class PasswordHashGenerator
    {
        /// <summary>
        /// Generates a password hash and displays it in a message box
        /// Useful for creating test users or updating passwords
        /// </summary>
        /// <param name="password">The plain text password to hash</param>
        /// <param name="showMessageBox">Whether to show the hash in a message box</param>
        /// <returns>The generated password hash</returns>
        public static string GenerateHash(string password, bool showMessageBox = true)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                if (showMessageBox)
                    MessageBox.Show("Password cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            try
            {
                string hash = PasswordHasher.HashPassword(password);
                
                if (showMessageBox)
                {
                    string message = $"Password Hash Generated:\n\n{hash}\n\n" +
                                   "Copy this hash to update your database:\n" +
                                   "UPDATE Users SET PasswordHash = '{hash}' WHERE Email = 'user@example.com';";
                    
                    MessageBox.Show(message, "Password Hash", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
                return hash;
            }
            catch (Exception ex)
            {
                if (showMessageBox)
                    MessageBox.Show($"Error generating hash: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// Generates multiple password hashes for common test passwords
        /// </summary>
        public static void GenerateTestHashes()
        {
            string[] passwords = { "admin123", "librarian123", "staff123", "member123" };
            string result = "Generated Password Hashes:\n\n";

            foreach (string password in passwords)
            {
                try
                {
                    string hash = PasswordHasher.HashPassword(password);
                    result += $"Password: {password}\nHash: {hash}\n\n";
                }
                catch (Exception ex)
                {
                    result += $"Password: {password}\nError: {ex.Message}\n\n";
                }
            }

            MessageBox.Show(result, "Test Password Hashes", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

