using System;
using MySql.Data.MySqlClient;
namespace Project5LMS.Helpers
{
    public static class DatabaseSchemaHelper
    {
        public static bool CheckColumnExists(MySqlConnection conn, string tableName, string columnName)
        {
            try
            {
                string query = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS
                                WHERE TABLE_SCHEMA = DATABASE()
                                AND TABLE_NAME = @TableName
                                AND COLUMN_NAME = @ColumnName";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TableName", tableName);
                    cmd.Parameters.AddWithValue("@ColumnName", columnName);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
            catch
            {
                return false;
            }
        }
        public static void AddColumnIfNotExists(MySqlConnection conn, string tableName, string columnName, string columnDefinition)
        {
            try
            {
                if (!CheckColumnExists(conn, tableName, columnName))
                {
                    string alterQuery = $"ALTER TABLE {tableName} ADD COLUMN {columnName} {columnDefinition}";
                    using (MySqlCommand alterCmd = new MySqlCommand(alterQuery, conn))
                    {
                        alterCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error adding column {columnName}: {ex.Message}");
            }
        }
        public static bool CheckTableExists(MySqlConnection conn, string tableName)
        {
            try
            {
                string query = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES
                                WHERE TABLE_SCHEMA = DATABASE()
                                AND TABLE_NAME = @TableName";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TableName", tableName);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
            catch
            {
                return false;
            }
        }
        public static void CreateBookCopiesTableIfNotExists(MySqlConnection conn)
        {
            try
            {
                if (!CheckTableExists(conn, "BookCopies"))
                {
                    string createTableQuery = @"CREATE TABLE IF NOT EXISTS BookCopies (
                        CopyID INT AUTO_INCREMENT PRIMARY KEY,
                        BookID INT NOT NULL,
                        AccessionNumber VARCHAR(100),
                        Barcode VARCHAR(100),
                        CopyStatus VARCHAR(50) DEFAULT 'Available',
                        Location VARCHAR(200),
                        Notes TEXT,
                        LastCheckedOut DATETIME NULL,
                        LastReturned DATETIME NULL,
                        CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
                        ModifiedDate DATETIME NULL,
                        FOREIGN KEY (BookID) REFERENCES Books(BookID) ON DELETE CASCADE,
                        INDEX idx_BookID (BookID),
                        INDEX idx_AccessionNumber (AccessionNumber),
                        INDEX idx_Barcode (Barcode),
                        INDEX idx_CopyStatus (CopyStatus)
                    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci";
                    
                    using (MySqlCommand cmd = new MySqlCommand(createTableQuery, conn))
                    {
                        cmd.ExecuteNonQuery();
                        System.Diagnostics.Debug.WriteLine("BookCopies table created successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating BookCopies table: {ex.Message}");
                throw;
            }
        }
        
        public static void EnsureBookIDIsAutoIncrement(MySqlConnection conn)
        {
            try
            {
                if (CheckTableExists(conn, "Books"))
                {
                    // Check if BookID column exists and if it's AUTO_INCREMENT
                    string checkQuery = @"SELECT EXTRA 
                                        FROM INFORMATION_SCHEMA.COLUMNS 
                                        WHERE TABLE_SCHEMA = DATABASE() 
                                        AND TABLE_NAME = 'Books' 
                                        AND COLUMN_NAME = 'BookID'";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn))
                    {
                        object extra = checkCmd.ExecuteScalar();
                        string extraValue = extra != null && extra != DBNull.Value ? extra.ToString() : "";
                        
                        // Check if it's not already AUTO_INCREMENT
                        if (extraValue.IndexOf("auto_increment", StringComparison.OrdinalIgnoreCase) < 0)
                        {
                            // First, check if there's a primary key constraint
                            string pkCheckQuery = @"SELECT CONSTRAINT_NAME 
                                                  FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
                                                  WHERE TABLE_SCHEMA = DATABASE() 
                                                  AND TABLE_NAME = 'Books' 
                                                  AND CONSTRAINT_TYPE = 'PRIMARY KEY'";
                            string pkName = null;
                            using (MySqlCommand pkCmd = new MySqlCommand(pkCheckQuery, conn))
                            {
                                object pkResult = pkCmd.ExecuteScalar();
                                if (pkResult != null && pkResult != DBNull.Value)
                                {
                                    pkName = pkResult.ToString();
                                }
                            }
                            
                            // Drop primary key if it exists (to modify the column)
                            if (!string.IsNullOrEmpty(pkName))
                            {
                                string dropPkQuery = $"ALTER TABLE Books DROP PRIMARY KEY";
                                using (MySqlCommand dropPkCmd = new MySqlCommand(dropPkQuery, conn))
                                {
                                    dropPkCmd.ExecuteNonQuery();
                                }
                            }
                            
                            // Modify BookID to be AUTO_INCREMENT
                            string alterQuery = @"ALTER TABLE Books 
                                                MODIFY COLUMN BookID INT AUTO_INCREMENT PRIMARY KEY";
                            using (MySqlCommand alterCmd = new MySqlCommand(alterQuery, conn))
                            {
                                alterCmd.ExecuteNonQuery();
                                System.Diagnostics.Debug.WriteLine("BookID column set to AUTO_INCREMENT successfully.");
                            }
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("BookID is already AUTO_INCREMENT.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error ensuring BookID is AUTO_INCREMENT: {ex.Message}");
                // Don't throw - this is a non-critical operation
            }
        }
    }
}