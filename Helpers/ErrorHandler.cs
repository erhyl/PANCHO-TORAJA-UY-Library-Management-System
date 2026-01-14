using System;
using System.Windows.Forms;
namespace Project5LMS.Helpers
{
    /// <summary>
    /// Centralized error handling helper for consistent error messages across the application
    /// </summary>
    public static class ErrorHandler
    {
        /// <summary>
        /// Shows an error message with consistent formatting
        /// </summary>
        public static void ShowError(string message, string title = "Error", Exception ex = null)
        {
            string fullMessage = message;
            if (ex != null)
            {
                fullMessage += $"\n\nDetails: {ex.Message}";
                System.Diagnostics.Debug.WriteLine($"Error: {title} - {message}\nException: {ex}");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Error: {title} - {message}");
            }
            
            MessageBox.Show(fullMessage, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            AuditLogger.Log("Error", $"{title}: {message}", "Failed");
        }
        
        /// <summary>
        /// Shows a warning message with consistent formatting
        /// </summary>
        public static void ShowWarning(string message, string title = "Warning")
        {
            System.Diagnostics.Debug.WriteLine($"Warning: {title} - {message}");
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        
        /// <summary>
        /// Shows a success message with consistent formatting
        /// </summary>
        public static void ShowSuccess(string message, string title = "Success")
        {
            System.Diagnostics.Debug.WriteLine($"Success: {title} - {message}");
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        /// <summary>
        /// Shows a validation error message
        /// </summary>
        public static void ShowValidationError(string message, string title = "Validation Error")
        {
            System.Diagnostics.Debug.WriteLine($"Validation Error: {title} - {message}");
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        
        /// <summary>
        /// Shows a database error with helpful context
        /// </summary>
        public static void ShowDatabaseError(string operation, Exception ex)
        {
            string message = $"Database error during {operation}: {ex.Message}\n\nPlease check:\n1. Database connection\n2. Required tables exist\n3. Database permissions";
            ShowError(message, "Database Error", ex);
        }
    }
}
