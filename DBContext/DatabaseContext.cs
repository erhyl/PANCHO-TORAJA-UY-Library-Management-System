using MySql.Data.MySqlClient;
using Project5LMS.Helpers;
using System;
using System.Data;

namespace Project5LMS.Data
{
    public class DatabaseContext : IDisposable
    {
        private readonly string connectionString;
        private bool disposed = false;

        public DatabaseContext()
        {
            try
            {
                connectionString = DatabaseHelper.GetConnectionString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting connection string: {ex.Message}");
                connectionString = "Server=localhost;Database=librarydb;Uid=root;Pwd=;";
            }
        }

        public MySqlConnection GetConnection()
        {
            try
            {
                return new MySqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating connection: {ex.Message}");
                throw new Exception("Unable to establish database connection. Please check your database settings.", ex);
            }
        }

        public DataTable ExecuteQuery(string query)
        {
            try
            {
                using (var con = GetConnection())
                {
                    con.Open();
                    using (var cmd = new MySqlCommand(query, con))
                    {
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            var dt = new DataTable();
                            adapter.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Database error executing query: {ex.Message}");
                throw new Exception("Database error occurred. Please try again or contact support.", ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error executing query: {ex.Message}");
                throw new Exception("An error occurred while executing the database query.", ex);
            }
        }

        public int ExecuteNonQuery(string query)
        {
            try
            {
                using (var con = GetConnection())
                {
                    con.Open();
                    using (var cmd = new MySqlCommand(query, con))
                    {
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Database error executing non-query: {ex.Message}");
                throw new Exception("Database error occurred. Please try again or contact support.", ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error executing non-query: {ex.Message}");
                throw new Exception("An error occurred while executing the database operation.", ex);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                }
                disposed = true;
            }
        }
    }
}
