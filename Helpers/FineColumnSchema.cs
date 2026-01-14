using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Project5LMS.Helpers
{
    /// <summary>
    /// Caches and provides Fines table column existence information to ensure all columns are fetched
    /// </summary>
    public static class FineColumnSchema
    {
        private static Dictionary<string, bool> _columnCache = new Dictionary<string, bool>();
        private static readonly object _lock = new object();

        /// <summary>
        /// Gets all fine column existence information in a single call
        /// </summary>
        public static Dictionary<string, bool> GetFineColumns(MySqlConnection conn)
        {
            lock (_lock)
            {
                if (_columnCache.Count == 0)
                {
                    _columnCache = CheckAllFineColumns(conn);
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
        /// Checks all fine columns at once and caches results
        /// </summary>
        private static Dictionary<string, bool> CheckAllFineColumns(MySqlConnection conn)
        {
            var columns = new Dictionary<string, bool>();

            // Core columns (always check)
            string[] coreColumns = {
                "FineID", "TransactionID", "MemberID", "BookID", 
                "FineType", "Amount", "Paid", "Status", "DaysOverdue"
            };

            // Optional columns (may not exist in all database versions)
            string[] optionalColumns = {
                "CreatedDate", "PaidDate", "WaivedDate", "Description"
            };

            // Check all columns
            foreach (var column in coreColumns)
            {
                columns[column] = DatabaseSchemaHelper.CheckColumnExists(conn, "Fines", column);
            }

            foreach (var column in optionalColumns)
            {
                columns[column] = DatabaseSchemaHelper.CheckColumnExists(conn, "Fines", column);
            }

            return columns;
        }

        /// <summary>
        /// Builds SELECT column list based on available columns
        /// </summary>
        public static List<string> BuildSelectColumns(Dictionary<string, bool> columns, string tableAlias = "f")
        {
            var selectColumns = new List<string>
            {
                $"{tableAlias}.FineID",
                $"{tableAlias}.TransactionID",
                $"{tableAlias}.MemberID",
                $"{tableAlias}.BookID",
                $"{tableAlias}.FineType",
                $"{tableAlias}.Amount",
                $"{tableAlias}.Paid",
                $"{tableAlias}.Status",
                $"{tableAlias}.DaysOverdue"
            };

            // Add optional columns if they exist
            if (columns.ContainsKey("CreatedDate") && columns["CreatedDate"])
                selectColumns.Add($"{tableAlias}.CreatedDate");
            if (columns.ContainsKey("PaidDate") && columns["PaidDate"])
                selectColumns.Add($"{tableAlias}.PaidDate");
            if (columns.ContainsKey("WaivedDate") && columns["WaivedDate"])
                selectColumns.Add($"{tableAlias}.WaivedDate");
            if (columns.ContainsKey("Description") && columns["Description"])
                selectColumns.Add($"{tableAlias}.Description");

            return selectColumns;
        }
    }
}
