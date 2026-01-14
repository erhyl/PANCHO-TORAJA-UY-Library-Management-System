using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Project5LMS.Helpers
{
    /// <summary>
    /// Caches and provides Reservations table column existence information to ensure all columns are fetched
    /// </summary>
    public static class ReservationColumnSchema
    {
        private static Dictionary<string, bool> _columnCache = new Dictionary<string, bool>();
        private static readonly object _lock = new object();

        /// <summary>
        /// Gets all reservation column existence information in a single call
        /// </summary>
        public static Dictionary<string, bool> GetReservationColumns(MySqlConnection conn)
        {
            lock (_lock)
            {
                if (_columnCache.Count == 0)
                {
                    _columnCache = CheckAllReservationColumns(conn);
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
        /// Checks all reservation columns at once and caches results
        /// </summary>
        private static Dictionary<string, bool> CheckAllReservationColumns(MySqlConnection conn)
        {
            var columns = new Dictionary<string, bool>();

            // Core columns (always check)
            string[] coreColumns = {
                "ReservationID", "MemberID", "BookID", "ReservationDate", 
                "PickupDate", "ExpiryDate", "Status", "Priority"
            };

            // Optional columns (may not exist in all database versions)
            string[] optionalColumns = {
                "FulfilledDate", "Notes", "CreatedDate"
            };

            // Check all columns
            foreach (var column in coreColumns)
            {
                columns[column] = DatabaseSchemaHelper.CheckColumnExists(conn, "Reservations", column);
            }

            foreach (var column in optionalColumns)
            {
                columns[column] = DatabaseSchemaHelper.CheckColumnExists(conn, "Reservations", column);
            }

            return columns;
        }

        /// <summary>
        /// Builds SELECT column list based on available columns
        /// </summary>
        public static List<string> BuildSelectColumns(Dictionary<string, bool> columns, string tableAlias = "r")
        {
            var selectColumns = new List<string>
            {
                $"{tableAlias}.ReservationID",
                $"{tableAlias}.MemberID",
                $"{tableAlias}.BookID",
                $"{tableAlias}.ReservationDate",
                $"{tableAlias}.PickupDate",
                $"{tableAlias}.ExpiryDate",
                $"{tableAlias}.Status",
                $"{tableAlias}.Priority"
            };

            // Add optional columns if they exist
            if (columns.ContainsKey("FulfilledDate") && columns["FulfilledDate"])
                selectColumns.Add($"{tableAlias}.FulfilledDate");
            if (columns.ContainsKey("Notes") && columns["Notes"])
                selectColumns.Add($"{tableAlias}.Notes");
            if (columns.ContainsKey("CreatedDate") && columns["CreatedDate"])
                selectColumns.Add($"{tableAlias}.CreatedDate");

            return selectColumns;
        }
    }
}
