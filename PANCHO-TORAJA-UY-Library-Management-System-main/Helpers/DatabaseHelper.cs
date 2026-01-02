using System;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace Project5LMS.Helpers
{
    public static class DatabaseHelper
    {
        public static string GetConnectionString()
        {
            var cs = ConfigurationManager.ConnectionStrings["LmsDb"]?.ConnectionString;
            if (string.IsNullOrWhiteSpace(cs))
                throw new InvalidOperationException("Connection string 'LmsDb' not found in App.config.");
            return cs;
        }

        /// <summary>
        /// Attempts to open and close a connection. Returns true if successful; false and an error message otherwise.
        /// </summary>
        public static bool TestConnection(out string errorMessage)
        {
            errorMessage = null;
            try
            {
                using (var conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    conn.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
    }
}