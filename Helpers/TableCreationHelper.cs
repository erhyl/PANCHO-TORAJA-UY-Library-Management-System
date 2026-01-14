using System;
using MySql.Data.MySqlClient;
namespace Project5LMS.Helpers
{
    /// <summary>
    /// Helper for creating database tables with common patterns
    /// Reduces duplication across forms
    /// </summary>
    public static class TableCreationHelper
    {
        /// <summary>
        /// Ensures a table exists, creating it if necessary
        /// </summary>
        public static bool EnsureTableExists(MySqlConnection conn, string tableName, string createTableQuery, Action<MySqlConnection> postCreateAction = null)
        {
            try
            {
                string checkTableQuery = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES
                                          WHERE TABLE_SCHEMA = DATABASE()
                                          AND TABLE_NAME = @TableName";
                
                using (var checkCmd = new MySqlCommand(checkTableQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@TableName", tableName);
                    int tableExists = Convert.ToInt32(checkCmd.ExecuteScalar());
                    
                    if (tableExists == 0)
                    {
                        using (var createCmd = new MySqlCommand(createTableQuery, conn))
                        {
                            createCmd.ExecuteNonQuery();
                        }
                        
                        postCreateAction?.Invoke(conn);
                        return true; // Table was created
                    }
                }
                return false; // Table already existed
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error ensuring table {tableName} exists: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Ensures a table exists using DatabaseContext
        /// </summary>
        public static bool EnsureTableExists(Data.DatabaseContext dbContext, string tableName, string createTableQuery, Action<MySqlConnection> postCreateAction = null)
        {
            try
            {
                using (var conn = dbContext.GetConnection())
                {
                    conn.Open();
                    return EnsureTableExists(conn, tableName, createTableQuery, postCreateAction);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error ensuring table {tableName} exists: {ex.Message}");
                return false;
            }
        }
    }
}
