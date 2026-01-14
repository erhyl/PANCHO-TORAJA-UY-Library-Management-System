using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
namespace Project5LMS.Helpers
{
    /// <summary>
    /// Caches and provides book table column existence information to reduce repeated database queries
    /// </summary>
    public static class BookColumnSchema
    {
        private static Dictionary<string, bool> _columnCache = new Dictionary<string, bool>();
        private static readonly object _lock = new object();
        
        /// <summary>
        /// Gets all book column existence information in a single call
        /// </summary>
        public static Dictionary<string, bool> GetBookColumns(MySqlConnection conn)
        {
            lock (_lock)
            {
                // Check if cache is empty or connection changed (simple cache invalidation)
                if (_columnCache.Count == 0)
                {
                    _columnCache = CheckAllBookColumns(conn);
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
        /// Checks all book columns at once and caches results
        /// </summary>
        private static Dictionary<string, bool> CheckAllBookColumns(MySqlConnection conn)
        {
            var columns = new Dictionary<string, bool>();
            
            // Core columns (always check)
            string[] coreColumns = {
                "BookID", "Title", "Author", "ISBN", "Category", "Publisher",
                "Language", "Available", "Location", "Status", "CallNumber", "BookType"
            };
            
            // Optional columns (may not exist in all database versions)
            string[] optionalColumns = {
                "TotalCopies", "Copies", "AccessionNo", "Barcode",
                "PublicationYear", "YearPublished",
                "Subtitle", "Editor", "Edition", "NumberOfPages",
                "PhysicalDescription", "CoverImagePath"
            };
            
            // Check all columns
            foreach (var column in coreColumns)
            {
                columns[column] = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", column);
            }
            
            foreach (var column in optionalColumns)
            {
                columns[column] = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", column);
            }
            
            return columns;
        }
        
        /// <summary>
        /// Gets the appropriate column name for copies (TotalCopies, Copies, or default)
        /// </summary>
        public static string GetCopiesColumn(Dictionary<string, bool> columns)
        {
            if (columns.ContainsKey("TotalCopies") && columns["TotalCopies"])
                return "TotalCopies";
            if (columns.ContainsKey("Copies") && columns["Copies"])
                return "Copies";
            return "1 as TotalCopies";
        }
        
        /// <summary>
        /// Gets the appropriate column name for accession number
        /// </summary>
        public static string GetAccessionColumn(Dictionary<string, bool> columns)
        {
            if (columns.ContainsKey("AccessionNo") && columns["AccessionNo"])
                return "AccessionNo";
            if (columns.ContainsKey("Barcode") && columns["Barcode"])
                return "Barcode as AccessionNo";
            return "CAST(BookID AS CHAR) as AccessionNo";
        }
        
        /// <summary>
        /// Gets the appropriate column name for publication year
        /// </summary>
        public static string GetYearColumn(Dictionary<string, bool> columns)
        {
            if (columns.ContainsKey("PublicationYear") && columns["PublicationYear"])
                return "PublicationYear";
            if (columns.ContainsKey("YearPublished") && columns["YearPublished"])
                return "YearPublished as PublicationYear";
            return "0 as PublicationYear";
        }
        
        /// <summary>
        /// Builds SELECT column list based on available columns
        /// </summary>
        public static List<string> BuildSelectColumns(Dictionary<string, bool> columns)
        {
            var selectColumns = new List<string>
            {
                "BookID", "Title", "Author", "ISBN", "Category", "Publisher",
                GetYearColumn(columns) + " as PublicationYear",
                "Language",
                GetCopiesColumn(columns) + " as TotalCopies",
                "Available", "Location", "Status",
                GetAccessionColumn(columns) + " as AccessionNo",
                "CallNumber", "BookType"
            };
            
            // Add optional columns if they exist
            if (columns.ContainsKey("Barcode") && columns["Barcode"])
                selectColumns.Add("Barcode");
            if (columns.ContainsKey("Subtitle") && columns["Subtitle"])
                selectColumns.Add("Subtitle");
            if (columns.ContainsKey("Editor") && columns["Editor"])
                selectColumns.Add("Editor");
            if (columns.ContainsKey("Edition") && columns["Edition"])
                selectColumns.Add("Edition");
            if (columns.ContainsKey("NumberOfPages") && columns["NumberOfPages"])
                selectColumns.Add("NumberOfPages");
            if (columns.ContainsKey("PhysicalDescription") && columns["PhysicalDescription"])
                selectColumns.Add("PhysicalDescription");
            if (columns.ContainsKey("CoverImagePath") && columns["CoverImagePath"])
                selectColumns.Add("CoverImagePath");
            
            return selectColumns;
        }
    }
}
