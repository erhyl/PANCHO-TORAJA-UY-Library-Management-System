using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Project5LMS.Helpers
{
    /// <summary>
    /// Caches and provides Members table column existence information to ensure all columns are fetched
    /// </summary>
    public static class MemberColumnSchema
    {
        private static Dictionary<string, bool> _columnCache = new Dictionary<string, bool>();
        private static readonly object _lock = new object();

        /// <summary>
        /// Gets all member column existence information in a single call
        /// </summary>
        public static Dictionary<string, bool> GetMemberColumns(MySqlConnection conn)
        {
            lock (_lock)
            {
                if (_columnCache.Count == 0)
                {
                    _columnCache = CheckAllMemberColumns(conn);
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
        /// Checks all member columns at once and caches results
        /// </summary>
        private static Dictionary<string, bool> CheckAllMemberColumns(MySqlConnection conn)
        {
            var columns = new Dictionary<string, bool>();

            // Core columns (always check)
            string[] coreColumns = {
                "MemberID", "FirstName", "LastName", "Email", "RegistrationDate", 
                "ExpirationDate", "Status", "Contact", "Address"
            };

            // Optional columns (may not exist in all database versions)
            string[] optionalColumns = {
                "Type", "MemberType", "PhotoPath", "ValidIDPath", "MemberCardNumber",
                "CreatedDate", "ModifiedDate"
            };

            // Check all columns
            foreach (var column in coreColumns)
            {
                columns[column] = DatabaseSchemaHelper.CheckColumnExists(conn, "Members", column);
            }

            foreach (var column in optionalColumns)
            {
                columns[column] = DatabaseSchemaHelper.CheckColumnExists(conn, "Members", column);
            }

            return columns;
        }

        /// <summary>
        /// Gets the appropriate column name for member type
        /// </summary>
        public static string GetTypeColumn(Dictionary<string, bool> columns)
        {
            if (columns.ContainsKey("Type") && columns["Type"])
                return "Type";
            if (columns.ContainsKey("MemberType") && columns["MemberType"])
                return "MemberType";
            return "NULL as Type";
        }

        /// <summary>
        /// Builds SELECT column list based on available columns
        /// </summary>
        public static List<string> BuildSelectColumns(Dictionary<string, bool> columns)
        {
            var selectColumns = new List<string>
            {
                "MemberID",
                "FirstName",
                "LastName",
                "Email",
                $"COALESCE({GetTypeColumn(columns)}, MemberType) as Type",
                "RegistrationDate",
                "ExpirationDate",
                "Status"
            };

            // Add optional columns if they exist
            if (columns.ContainsKey("Contact") && columns["Contact"])
                selectColumns.Add("Contact");
            if (columns.ContainsKey("Address") && columns["Address"])
                selectColumns.Add("Address");
            if (columns.ContainsKey("PhotoPath") && columns["PhotoPath"])
                selectColumns.Add("PhotoPath");
            if (columns.ContainsKey("ValidIDPath") && columns["ValidIDPath"])
                selectColumns.Add("ValidIDPath");
            if (columns.ContainsKey("MemberCardNumber") && columns["MemberCardNumber"])
                selectColumns.Add("MemberCardNumber");
            if (columns.ContainsKey("CreatedDate") && columns["CreatedDate"])
                selectColumns.Add("CreatedDate");
            if (columns.ContainsKey("ModifiedDate") && columns["ModifiedDate"])
                selectColumns.Add("ModifiedDate");

            return selectColumns;
        }
    }
}
