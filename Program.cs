using System;
using System.Threading;
using System.Windows.Forms;
using Project5LMS.Services;
using Project5LMS.Helpers;
using MySql.Data.MySqlClient;
namespace Project5LMS
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            DependencyInjection.ConfigureServices();
            EnsureDatabaseSchema();
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new LoginForm());
            }
            catch (Exception ex)
            {
                HandleException(ex, "Application startup error");
            }
        }
        private static void EnsureDatabaseSchema()
        {
            try
            {
                var dbContext = ServiceFactory.GetDbContext();
                using (var conn = dbContext.GetConnection())
                {
                    conn.Open();
                    if (!DatabaseSchemaHelper.CheckColumnExists(conn, "Users", "Status"))
                    {
                        DatabaseSchemaHelper.AddColumnIfNotExists(conn, "Users", "Status", "VARCHAR(50) DEFAULT 'Active'");
                        // Update existing users to have Active status
                        string updateQuery = "UPDATE Users SET Status = 'Active' WHERE Status IS NULL";
                        using (var updateCmd = new MySqlCommand(updateQuery, conn))
                        {
                            updateCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error ensuring database schema: {ex.Message}");
            }
        }
        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            HandleException(e.Exception, "Unhandled thread exception");
        }
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                HandleException(ex, "Unhandled application exception");
            }
        }
        private static void HandleException(Exception ex, string context)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"{context}: {ex}");
                string message = "An unexpected error occurred. The application will attempt to continue.\n\n" +
                               "If this problem persists, please contact your system administrator.\n\n" +
                               $"Error details: {ex.Message}";
                MessageBox.Show(message, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine($"Critical error in exception handler: {ex}");
            }
        }
    }
}