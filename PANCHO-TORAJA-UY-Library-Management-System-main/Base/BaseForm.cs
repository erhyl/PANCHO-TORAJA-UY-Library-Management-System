using System;
using System.Windows.Forms;
using Project5LMS.Helpers;
using Project5LMS.Data;

namespace Project5LMS.Base
{
    public abstract class BaseForm : Form
    {
        protected DatabaseContext DatabaseContext { get; private set; }
        protected string ConnectionString { get; private set; }

        protected BaseForm()
        {
            InitializeDatabaseConnection();
        }

        protected virtual void InitializeDatabaseConnection()
        {
            try
            {
                DatabaseContext = new DatabaseContext();
                ConnectionString = DatabaseHelper.GetConnectionString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error initializing database connection: {ex.Message}");
                ConnectionString = "Server=localhost;Database=librarydb;Uid=root;Pwd=;";
                DatabaseContext = new DatabaseContext();
            }
        }

        protected virtual void OnFormLoad()
        {
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                DatabaseContext?.Dispose();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error disposing database context: {ex.Message}");
            }
            base.OnFormClosing(e);
        }

        protected virtual void ShowError(string message, string title = "Error")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            AuditLogger.Log("Error", $"{title}: {message}", "Failed");
        }

        protected virtual void ShowSuccess(string message, string title = "Success")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        protected abstract void ValidateAccess();

        protected abstract string[] GetRequiredRoles();
    }
}
