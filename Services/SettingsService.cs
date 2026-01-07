using System;
using System.Collections.Generic;
using System.Linq;
using Project5LMS.Data;
using Project5LMS.Interfaces;

namespace Project5LMS.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly DatabaseContext _dbContext;

        public SettingsService(DatabaseContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public SettingsService()
        {
            _dbContext = new DatabaseContext();
        }

        public string GetSetting(string key, string defaultValue = "")
        {
            try
            {
                string query = "SELECT SettingValue FROM Settings WHERE SettingKey = @Key";
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Key", key);
                        object result = cmd.ExecuteScalar();
                        return result?.ToString() ?? defaultValue;
                    }
                }
            }
            catch
            {
                return defaultValue;
            }
        }

        public bool SaveSetting(string key, string value, string category = "General")
        {
            try
            {
                string query = @"INSERT INTO Settings (SettingKey, SettingValue, Category) 
                               VALUES (@Key, @Value, @Category)
                               ON DUPLICATE KEY UPDATE SettingValue = @Value, Category = @Category";
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Key", key);
                        cmd.Parameters.AddWithValue("@Value", value);
                        cmd.Parameters.AddWithValue("@Category", category);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving setting: {ex.Message}");
                return false;
            }
        }

        public Dictionary<string, string> GetSettingsByCategory(string category)
        {
            var settings = new Dictionary<string, string>();
            try
            {
                string query = "SELECT SettingKey, SettingValue FROM Settings WHERE Category = @Category";
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Category", category);
                        using (var adapter = new MySql.Data.MySqlClient.MySqlDataAdapter(cmd))
                        {
                            var result = new System.Data.DataTable();
                            adapter.Fill(result);
                            foreach (System.Data.DataRow row in result.Rows)
                            {
                                settings[row["SettingKey"].ToString()] = row["SettingValue"]?.ToString() ?? "";
                            }
                        }
                    }
                }
            }
            catch { }
            return settings;
        }

        public bool EnsureSettingsTableExists()
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string checkTableQuery = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES 
                                              WHERE TABLE_SCHEMA = DATABASE() 
                                              AND TABLE_NAME = 'Settings'";
                    using (var checkCmd = new MySql.Data.MySqlClient.MySqlCommand(checkTableQuery, conn))
                    {
                        int tableExists = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (tableExists == 0)
                        {
                            string createTableQuery = @"CREATE TABLE IF NOT EXISTS Settings (
                                                        SettingKey VARCHAR(100) PRIMARY KEY,
                                                        SettingValue TEXT,
                                                        Category VARCHAR(50),
                                                        UpdatedDate DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
                                                        )";
                            _dbContext.ExecuteNonQuery(createTableQuery);
                        }
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

