using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Project5LMS.Data;

namespace Project5LMS.Helpers
{
    /// <summary>
    /// Database diagnostics and testing utilities
    /// </summary>
    public static class DatabaseDiagnostics
    {
        /// <summary>
        /// Test database connection and display results
        /// </summary>
        public static void TestConnection()
        {
            try
            {
                using (var dbContext = new DatabaseContext())
                {
                    var connectionString = DatabaseHelper.GetConnectionString();
                    var result = $"Connection String: {connectionString}\n\n";

                    // Test basic connection
                    using (var conn = dbContext.GetConnection())
                    {
                        conn.Open();
                        result += "✓ Connection opened successfully\n";
                        result += $"✓ Server Version: {conn.ServerVersion}\n";
                        result += $"✓ Database: {conn.Database}\n";
                        conn.Close();
                    }

                    // Test query execution
                    var testQuery = "SELECT COUNT(*) as TableCount FROM information_schema.tables WHERE table_schema = DATABASE()";
                    var dt = dbContext.ExecuteQuery(testQuery);
                    if (dt.Rows.Count > 0)
                    {
                        result += $"✓ Tables found: {dt.Rows[0]["TableCount"]}\n";
                    }

                    // Test Books table
                    try
                    {
                        var booksQuery = "SELECT COUNT(*) as BookCount FROM Books";
                        var booksDt = dbContext.ExecuteQuery(booksQuery);
                        if (booksDt.Rows.Count > 0)
                        {
                            result += $"✓ Books table accessible - {booksDt.Rows[0]["BookCount"]} books found\n";
                        }
                    }
                    catch (Exception ex)
                    {
                        result += $"✗ Books table error: {ex.Message}\n";
                    }

                    // Test Members table
                    try
                    {
                        var membersQuery = "SELECT COUNT(*) as MemberCount FROM Members";
                        var membersDt = dbContext.ExecuteQuery(membersQuery);
                        if (membersDt.Rows.Count > 0)
                        {
                            result += $"✓ Members table accessible - {membersDt.Rows[0]["MemberCount"]} members found\n";
                        }
                    }
                    catch (Exception ex)
                    {
                        result += $"✗ Members table error: {ex.Message}\n";
                    }

                    MessageBox.Show(result, "Database Connection Test", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Database Connection Test Failed!\n\nError: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}",
                    "Connection Test Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Test a specific query and show results
        /// </summary>
        public static void TestQuery(string query, string description = "Query")
        {
            try
            {
                using (var dbContext = new DatabaseContext())
                {
                    var dt = dbContext.ExecuteQuery(query);
                    var result = $"{description}\n\n";
                    result += $"Rows returned: {dt.Rows.Count}\n\n";

                    if (dt.Rows.Count > 0)
                    {
                        result += "Columns: " + string.Join(", ", dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName)) + "\n\n";
                        result += "First row sample:\n";
                        foreach (DataColumn col in dt.Columns)
                        {
                            result += $"{col.ColumnName}: {dt.Rows[0][col]}\n";
                        }
                    }

                    MessageBox.Show(result, "Query Test Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Query Test Failed!\n\nQuery: {query}\n\nError: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}",
                    "Query Test Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}

