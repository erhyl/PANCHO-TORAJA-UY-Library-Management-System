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

        /// <summary>
        /// Execute multiple commands within a transaction
        /// </summary>
        public bool ExecuteInTransaction(Action<MySqlConnection, MySqlTransaction> action)
        {
            MySqlConnection conn = null;
            MySqlTransaction transaction = null;
            try
            {
                conn = GetConnection();
                conn.Open();
                transaction = conn.BeginTransaction();

                action(conn, transaction);

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                try
                {
                    transaction?.Rollback();
                }
                catch (Exception rollbackEx)
                {
                    System.Diagnostics.Debug.WriteLine($"Error rolling back transaction: {rollbackEx.Message}");
                }
                System.Diagnostics.Debug.WriteLine($"Error in transaction: {ex.Message}");
                throw new Exception("Transaction failed and was rolled back.", ex);
            }
            finally
            {
                transaction?.Dispose();
                conn?.Close();
                conn?.Dispose();
            }
        }

        /// <summary>
        /// Execute a stored procedure
        /// </summary>
        public DataTable ExecuteStoredProcedure(string procedureName, params MySqlParameter[] parameters)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand(procedureName, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
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
                System.Diagnostics.Debug.WriteLine($"Database error executing stored procedure: {ex.Message}");
                throw new Exception($"Error executing stored procedure '{procedureName}'.", ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error executing stored procedure: {ex.Message}");
                throw new Exception($"An error occurred while executing stored procedure '{procedureName}'.", ex);
            }
        }

        /// <summary>
        /// Execute a stored procedure that returns no data
        /// </summary>
        public int ExecuteStoredProcedureNonQuery(string procedureName, params MySqlParameter[] parameters)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand(procedureName, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Database error executing stored procedure: {ex.Message}");
                throw new Exception($"Error executing stored procedure '{procedureName}'.", ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error executing stored procedure: {ex.Message}");
                throw new Exception($"An error occurred while executing stored procedure '{procedureName}'.", ex);
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
