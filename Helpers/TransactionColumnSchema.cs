using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Project5LMS.Helpers
{
    /// <summary>
    /// Caches and provides Transactions table column existence information to ensure all columns are fetched
    /// </summary>
    public static class TransactionColumnSchema
    {
        private static Dictionary<string, bool> _columnCache = new Dictionary<string, bool>();
        private static readonly object _lock = new object();

        /// <summary>
        /// Gets all transaction column existence information in a single call
        /// </summary>
        public static Dictionary<string, bool> GetTransactionColumns(MySqlConnection conn)
        {
            lock (_lock)
            {
                if (_columnCache.Count == 0)
                {
                    _columnCache = CheckAllTransactionColumns(conn);
                }
                return new Dictionary<string, bool>(_columnCache);
            }
        }

        /// <summary>
        /// Clears the column cache (useful when schema changes)
        /// </summary>
        public static void ClearCache()
        {
            lock (_lock)
            {
                _columnCache.Clear();
            }
        }

        /// <summary>
        /// Checks all transaction columns at once and caches results
        /// </summary>
        private static Dictionary<string, bool> CheckAllTransactionColumns(MySqlConnection conn)
        {
            var columns = new Dictionary<string, bool>();

            // Core columns (always check)
            string[] coreColumns = {
                "TransactionID", "MemberID", "BookID", "BorrowDate", 
                "DueDate", "ReturnDate", "Status"
            };

            // Optional columns (may not exist in all database versions)
            string[] optionalColumns = {
                "TransactionType", "Fine", "RenewalCount", 
                "CreatedDate", "ModifiedDate"
            };

            // Check all columns
            foreach (var column in coreColumns)
            {
                columns[column] = DatabaseSchemaHelper.CheckColumnExists(conn, "Transactions", column);
            }

            foreach (var column in optionalColumns)
            {
                columns[column] = DatabaseSchemaHelper.CheckColumnExists(conn, "Transactions", column);
            }

            return columns;
        }

        /// <summary>
        /// Builds SELECT column list based on available columns
        /// </summary>
        public static List<string> BuildSelectColumns(Dictionary<string, bool> columns, string tableAlias = "t")
        {
            var selectColumns = new List<string>
            {
                $"{tableAlias}.TransactionID",
                $"{tableAlias}.MemberID",
                $"{tableAlias}.BookID",
                $"{tableAlias}.BorrowDate",
                $"{tableAlias}.DueDate",
                $"{tableAlias}.ReturnDate",
                $"{tableAlias}.Status"
            };

            // Add optional columns if they exist
            if (columns.ContainsKey("TransactionType") && columns["TransactionType"])
                selectColumns.Add($"{tableAlias}.TransactionType");
            if (columns.ContainsKey("Fine") && columns["Fine"])
                selectColumns.Add($"{tableAlias}.Fine");
            if (columns.ContainsKey("RenewalCount") && columns["RenewalCount"])
                selectColumns.Add($"{tableAlias}.RenewalCount");
            if (columns.ContainsKey("CreatedDate") && columns["CreatedDate"])
                selectColumns.Add($"{tableAlias}.CreatedDate");
            if (columns.ContainsKey("ModifiedDate") && columns["ModifiedDate"])
                selectColumns.Add($"{tableAlias}.ModifiedDate");

            return selectColumns;
        }
    }
}
