using System;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace Project5LMS.Helpers
{
    public static class DatabaseHelper
    {
        public static string GetConnectionString()
        {
            // Try multiple connection string names
            string[] connectionStringNames = { "LmsDb", "MySqlConnectionString" };
            
            foreach (string name in connectionStringNames)
            {
                var cs = ConfigurationManager.ConnectionStrings[name]?.ConnectionString;
                if (!string.IsNullOrWhiteSpace(cs))
                    return cs;
            }
            
            // Fallback to default connection string
            return "Server=localhost;Database=librarydb;Uid=root;Pwd=;";
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