using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
namespace Project5LMS.Helpers
{
    /// <summary>
    /// Helper methods for DataGridView operations to reduce code duplication
    /// </summary>
    public static class DataGridViewHelper
    {
        /// <summary>
        /// Applies filters to a DataTable and binds to DataGridView
        /// </summary>
        public static void ApplyFilters(DataGridView dgv, DataTable sourceData, string searchText, 
            Func<DataRow, bool> searchPredicate, Func<DataRow, bool> filterPredicate = null)
        {
            if (sourceData == null || sourceData.Rows.Count == 0)
            {
                dgv.DataSource = null;
                return;
            }

            // Normalize search text
            searchText = NormalizeSearchText(searchText);

            // Create filtered table with same structure
            DataTable filteredData = new DataTable();
            foreach (DataColumn col in sourceData.Columns)
            {
                filteredData.Columns.Add(col.ColumnName, typeof(string));
            }

            // Filter rows
            foreach (DataRow row in sourceData.Rows)
            {
                bool matchesSearch = string.IsNullOrEmpty(searchText) || searchPredicate(row);
                bool matchesFilter = filterPredicate == null || filterPredicate(row);

                if (matchesSearch && matchesFilter)
                {
                    try
                    {
                        DataRow newRow = filteredData.NewRow();
                        foreach (DataColumn col in sourceData.Columns)
                        {
                            if (filteredData.Columns.Contains(col.ColumnName))
                            {
                                object value = row[col.ColumnName];
                                newRow[col.ColumnName] = value == null || value == DBNull.Value ? "" : value.ToString();
                            }
                        }
                        filteredData.Rows.Add(newRow);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error filtering row: {ex.Message}");
                    }
                }
            }

            // Bind to DataGridView
            dgv.DataSource = null;
            dgv.Refresh();
            dgv.DataSource = filteredData;
            dgv.Refresh();
            dgv.Update();
        }

        /// <summary>
        /// Normalizes search text (removes placeholder, trims, lowercases)
        /// </summary>
        public static string NormalizeSearchText(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return "";

            searchText = searchText.Trim();
            
            // Check for common placeholder patterns first (before individual word removal)
            string[] fullPlaceholders = { 
                "search members...", 
                "🔍 search members...", 
                "search by", 
                "🔍 search by",
                "search inventory...",
                "🔍 search inventory..."
            };
            
            foreach (var placeholder in fullPlaceholders)
            {
                if (searchText.Equals(placeholder, StringComparison.OrdinalIgnoreCase) || 
                    searchText.StartsWith(placeholder, StringComparison.OrdinalIgnoreCase))
                {
                    return "";
                }
            }
            
            // Remove common placeholder words and symbols
            string[] placeholders = { "search", "🔍", "..." };
            foreach (var placeholder in placeholders)
            {
                if (searchText.IndexOf(placeholder, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    // Case-insensitive replace using Regex
                    searchText = Regex.Replace(searchText, Regex.Escape(placeholder), "", RegexOptions.IgnoreCase).Trim();
                }
            }

            // If after removing placeholders we're left with common words, treat as empty
            string[] commonWords = { "members", "inventory", "books", "by", "member" };
            string lowerText = searchText.ToLower().Trim();
            if (string.IsNullOrWhiteSpace(lowerText) || commonWords.Contains(lowerText))
            {
                return "";
            }

            return lowerText;
        }

        /// <summary>
        /// Creates a search predicate for text matching across multiple columns
        /// </summary>
        public static Func<DataRow, bool> CreateTextSearchPredicate(string searchText, params string[] columnNames)
        {
            if (string.IsNullOrEmpty(searchText))
                return row => true;

            string normalizedSearch = searchText.ToLower();
            return row =>
            {
                foreach (var columnName in columnNames)
                {
                    if (row.Table.Columns.Contains(columnName))
                    {
                        string value = row[columnName]?.ToString()?.ToLower() ?? "";
                        if (value.Contains(normalizedSearch))
                            return true;
                    }
                }
                return false;
            };
        }

        /// <summary>
        /// Creates a filter predicate for dropdown/combobox filters
        /// </summary>
        public static Func<DataRow, bool> CreateDropdownFilterPredicate(string selectedValue, string columnName, string allOptionText = "All")
        {
            if (string.IsNullOrEmpty(selectedValue) || selectedValue == allOptionText)
                return row => true;

            return row =>
            {
                if (!row.Table.Columns.Contains(columnName))
                    return true;

                string value = row[columnName]?.ToString()?.Trim() ?? "";
                return value.Equals(selectedValue, StringComparison.OrdinalIgnoreCase);
            };
        }

        /// <summary>
        /// Sets up common DataGridView properties
        /// </summary>
        public static void SetupCommonProperties(DataGridView dgv, bool allowEdit = false, bool autoSizeColumns = true)
        {
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = !allowEdit;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AutoSizeColumnsMode = autoSizeColumns ? DataGridViewAutoSizeColumnsMode.AllCells : DataGridViewAutoSizeColumnsMode.None;
            dgv.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }
    }
}
