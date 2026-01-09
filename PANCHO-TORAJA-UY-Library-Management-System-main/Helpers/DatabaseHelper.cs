using System;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace Project5LMS.Helpers
{
    public static class DatabaseHelper
    {
        public static string GetConnectionString()
        {

            string[] connectionStringNames = { "LmsDb", "MySqlConnectionString" };

            foreach (string name in connectionStringNames)
            {
                var cs = ConfigurationManager.ConnectionStrings[name]?.ConnectionString;
                if (!string.IsNullOrWhiteSpace(cs))
                    return cs;
            }

            return "Server=localhost;Database=librarydb;Uid=root;Pwd=;";
        }

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