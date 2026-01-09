using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Project5LMS.Helpers;
using Project5LMS.Services;
using Project5LMS.Data;
using Project5LMS.Interfaces;
using Project5LMS.Models;

namespace Project5LMS.Forms.Admin.Catalog
{
    public partial class AdminCatalogForm : Form
    {
        private DataTable allBooksData;
        private readonly IBookService _bookService;
        private readonly DatabaseContext _dbContext;

        public AdminCatalogForm()
        {
            InitializeComponent();
            _bookService = ServiceFactory.CreateBookService();
            _dbContext = ServiceFactory.GetDbContext();
        }

        private void AdminCatalogForm_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadResourceTypes();
            LoadCategories();

            if (cmbResourceTypeFilter.Items.Count > 0)
            {
                cmbResourceTypeFilter.SelectedIndex = 0;
            }
            if (cmbCategoryFilter.Items.Count > 0)
            {
                cmbCategoryFilter.SelectedIndex = 0;
            }
            LoadMetrics();
            LoadBooks();
        }

        private void SetupDataGridView()
        {

            dataGridViewBooks.AutoGenerateColumns = false;

            if (dataGridViewBooks.Columns.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("Warning: DataGridView columns not found. Columns should be defined in Designer.");
                return;
            }

            dataGridViewBooks.CellFormatting -= DataGridViewBooks_CellFormatting;
            dataGridViewBooks.CellFormatting += DataGridViewBooks_CellFormatting;

            dataGridViewBooks.CellPainting -= DataGridViewBooks_CellPainting;
            dataGridViewBooks.CellPainting += DataGridViewBooks_CellPainting;
            
            // Handle DataError to prevent error dialogs
            dataGridViewBooks.DataError -= DataGridViewBooks_DataError;
            dataGridViewBooks.DataError += DataGridViewBooks_DataError;
        }
        
        private void DataGridViewBooks_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"DataGridView DataError: Column={e.ColumnIndex}, Row={e.RowIndex}, Error={e.Exception.Message}");
            e.ThrowException = false; // Suppress the error dialog
        }

        private void DataGridViewBooks_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

                string columnName = dataGridViewBooks.Columns[e.ColumnIndex].Name;

                // Handle null or DBNull values
                if (e.Value == null || e.Value == DBNull.Value)
                {
                    e.Value = string.Empty;
                    e.FormattingApplied = true;
                    return;
                }

                // Convert to string for all text columns to avoid type mismatch
                string stringValue = e.Value?.ToString() ?? string.Empty;

                if (columnName == "AccessionNo")
                {
                    if (!string.IsNullOrEmpty(stringValue) && !stringValue.StartsWith("ACC-"))
                    {
                        if (int.TryParse(stringValue, out int accNum))
                        {
                            e.Value = $"ACC-{accNum}";
                        }
                        else
                        {
                            e.Value = $"ACC-{stringValue}";
                        }
                    }
                    else
                    {
                        e.Value = stringValue;
                    }
                    e.FormattingApplied = true;
                }
                else if (columnName == "BookDetails" || columnName == "Publisher" || 
                         columnName == "Copies" || columnName == "Category" || 
                         columnName == "Location" || columnName == "Status")
                {
                    // Ensure all text columns are strings
                    e.Value = stringValue;
                    e.FormattingApplied = true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"CellFormatting error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                // Don't set FormattingApplied = true on error, let default formatting handle it
                e.FormattingApplied = false;
            }
        }

        private void DataGridViewBooks_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

                string columnName = dataGridViewBooks.Columns[e.ColumnIndex].Name;

            if (columnName == "Status")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                string value = e.Value?.ToString() ?? "";
                Color bgColor = Color.LightGray;
                Color textColor = Color.Black;

                switch (value.ToLower())
                {
                    case "available":
                        bgColor = Color.FromArgb(40, 167, 69);
                        textColor = Color.White;
                        break;
                    case "limited":
                        bgColor = Color.FromArgb(255, 193, 7);
                        textColor = Color.White;
                        break;
                    case "out of stock":
                    case "outofstock":
                        bgColor = Color.FromArgb(220, 53, 69);
                        textColor = Color.White;
                        break;
                }

                Rectangle badgeRect = new Rectangle(
                    e.CellBounds.X + 5,
                    e.CellBounds.Y + (e.CellBounds.Height - 25) / 2,
                    Math.Min(100, e.CellBounds.Width - 10),
                    25
                );

                using (GraphicsPath path = new GraphicsPath())
                {
                    int radius = 12;
                    path.AddArc(badgeRect.X, badgeRect.Y, radius, radius, 180, 90);
                    path.AddArc(badgeRect.Right - radius, badgeRect.Y, radius, radius, 270, 90);
                    path.AddArc(badgeRect.Right - radius, badgeRect.Bottom - radius, radius, radius, 0, 90);
                    path.AddArc(badgeRect.X, badgeRect.Bottom - radius, radius, radius, 90, 90);
                    path.CloseAllFigures();

                    using (SolidBrush brush = new SolidBrush(bgColor))
                    {
                        e.Graphics.FillPath(brush, path);
                    }
                }

                TextRenderer.DrawText(
                    e.Graphics,
                    value,
                    dataGridViewBooks.DefaultCellStyle.Font,
                    badgeRect,
                    textColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                );

                e.Handled = true;
            }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"CellPainting error: {ex}");

                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                e.Handled = false;
            }
        }

        private void LoadResourceTypes()
        {
            try
            {
                cmbResourceTypeFilter.Items.Clear();
                cmbResourceTypeFilter.Items.Add("All Resource Types");
                cmbResourceTypeFilter.Items.Add("Books");
                cmbResourceTypeFilter.Items.Add("E-Books");
                cmbResourceTypeFilter.Items.Add("Journals");
                cmbResourceTypeFilter.Items.Add("Magazines");
                cmbResourceTypeFilter.Items.Add("Reference Materials");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading resource types: {ex.Message}");
            }
        }

        private void LoadCategories()
        {
            try
            {
                cmbCategoryFilter.Items.Clear();
                cmbCategoryFilter.Items.Add("All Categories");
                
                var categories = _bookService.GetAllBooks()
                    .Where(b => !string.IsNullOrWhiteSpace(b.Category))
                    .Select(b => b.Category)
                    .Distinct()
                    .OrderBy(c => c);
                
                foreach (var category in categories)
                {
                    cmbCategoryFilter.Items.Add(category);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading categories: {ex.Message}");
            }
        }

        private void LoadMetrics()
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();

                    string queryTotalTitles = "SELECT COUNT(DISTINCT BookID) FROM Books";
                    using (MySqlCommand cmd = new MySqlCommand(queryTotalTitles, conn))
                    {
                        int totalTitles = Convert.ToInt32(cmd.ExecuteScalar());
                        lblMetricTotalTitlesValue.Text = totalTitles.ToString();
                    }

                    string queryTotalCopies = "SELECT SUM(Copies) FROM Books";
                    using (MySqlCommand cmd = new MySqlCommand(queryTotalCopies, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        int totalCopies = result != DBNull.Value && result != null ? Convert.ToInt32(result) : 0;
                        lblMetricTotalCopiesValue.Text = totalCopies.ToString();
                    }

                    string queryAvailable = "SELECT SUM(Available) FROM Books";
                    using (MySqlCommand cmd = new MySqlCommand(queryAvailable, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        int available = result != DBNull.Value && result != null ? Convert.ToInt32(result) : 0;
                        lblMetricAvailableValue.Text = available.ToString();
                    }

                    string queryOnLoan = "SELECT SUM(Copies - Available) FROM Books";
                    using (MySqlCommand cmd = new MySqlCommand(queryOnLoan, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        int onLoan = result != DBNull.Value && result != null ? Convert.ToInt32(result) : 0;
                        lblMetricOnLoanValue.Text = onLoan.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading metrics: {ex.Message}");
            }
        }

        private void LoadBooks()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("LoadBooks: Starting to load books...");
                
                allBooksData = GetBooksData();
                System.Diagnostics.Debug.WriteLine($"LoadBooks: Retrieved DataTable with {allBooksData?.Rows.Count ?? 0} rows");

                if (allBooksData == null || allBooksData.Rows.Count == 0)
                {
                    System.Diagnostics.Debug.WriteLine("LoadBooks: No books found, creating empty DataTable");
                    // Create empty DataTable with correct structure
                    allBooksData = new DataTable();
                    allBooksData.Columns.Add("BookID", typeof(int));
                    allBooksData.Columns.Add("Title", typeof(string));
                    allBooksData.Columns.Add("Author", typeof(string));
                    allBooksData.Columns.Add("ISBN", typeof(string));
                    allBooksData.Columns.Add("Publisher", typeof(string));
                    allBooksData.Columns.Add("YearPublished", typeof(int));
                    allBooksData.Columns.Add("Category", typeof(string));
                    allBooksData.Columns.Add("Copies", typeof(int));
                    allBooksData.Columns.Add("Available", typeof(int));
                    allBooksData.Columns.Add("Barcode", typeof(string));
                    allBooksData.Columns.Add("Location", typeof(string));
                    allBooksData.Columns.Add("Status", typeof(string));
                    allBooksData.Columns.Add("AccessionNo", typeof(string));
                    allBooksData.Columns.Add("BookDetails", typeof(string));
                    
                    dataGridViewBooks.DataSource = allBooksData;
                    System.Diagnostics.Debug.WriteLine("LoadBooks: Empty DataTable set as DataSource");
                    return;
                }

                // Ensure BookID column exists (critical for row identification)
                if (!allBooksData.Columns.Contains("BookID"))
                {
                    System.Diagnostics.Debug.WriteLine("WARNING: BookID column missing from DataTable!");
                    allBooksData.Columns.Add("BookID", typeof(int));
                }
                
                // Ensure all required columns exist and have correct types
                if (!allBooksData.Columns.Contains("AccessionNo"))
                {
                    allBooksData.Columns.Add("AccessionNo", typeof(string));
                }
                if (!allBooksData.Columns.Contains("BookDetails"))
                {
                    allBooksData.Columns.Add("BookDetails", typeof(string));
                }
                if (!allBooksData.Columns.Contains("Publisher"))
                {
                    allBooksData.Columns.Add("Publisher", typeof(string));
                }
                
                // Handle Copies column - convert from int to string if needed
                if (allBooksData.Columns.Contains("Copies") && allBooksData.Columns["Copies"].DataType == typeof(int))
                {
                    // Create a new string column and copy data
                    DataColumn newCopiesCol = new DataColumn("CopiesString", typeof(string));
                    allBooksData.Columns.Add(newCopiesCol);
                    foreach (DataRow r in allBooksData.Rows)
                    {
                        if (r["Copies"] != DBNull.Value)
                        {
                            r["CopiesString"] = r["Copies"].ToString();
                        }
                    }
                    allBooksData.Columns.Remove("Copies");
                    newCopiesCol.ColumnName = "Copies";
                }
                else if (!allBooksData.Columns.Contains("Copies"))
                {
                    allBooksData.Columns.Add("Copies", typeof(string));
                }
                
                if (!allBooksData.Columns.Contains("Location"))
                {
                    allBooksData.Columns.Add("Location", typeof(string));
                }
                if (!allBooksData.Columns.Contains("Status"))
                {
                    allBooksData.Columns.Add("Status", typeof(string));
                }
                
                // Ensure Category column exists
                if (!allBooksData.Columns.Contains("Category"))
                {
                    allBooksData.Columns.Add("Category", typeof(string));
                }

                System.Diagnostics.Debug.WriteLine($"LoadBooks: Processing {allBooksData.Rows.Count} rows...");
                System.Diagnostics.Debug.WriteLine($"LoadBooks: Available columns: {string.Join(", ", allBooksData.Columns.Cast<DataColumn>().Select(c => c.ColumnName))}");
                
                foreach (DataRow row in allBooksData.Rows)
                {
                    try
                    {
                        int bookId = row.Table.Columns.Contains("BookID") && row["BookID"] != DBNull.Value
                            ? Convert.ToInt32(row["BookID"])
                            : 0;
                        
                        // Set AccessionNo
                        string barcode = row.Table.Columns.Contains("Barcode") && row["Barcode"] != DBNull.Value 
                            ? row["Barcode"].ToString() 
                            : "";
                        string accessionNo = row.Table.Columns.Contains("AccessionNo") && row["AccessionNo"] != DBNull.Value
                            ? row["AccessionNo"].ToString()
                            : "";
                        if (string.IsNullOrEmpty(accessionNo))
                        {
                            row["AccessionNo"] = !string.IsNullOrEmpty(barcode) ? barcode : (bookId > 0 ? bookId.ToString() : "");
                        }

                        // Set BookDetails
                        string title = row.Table.Columns.Contains("Title") && row["Title"] != DBNull.Value 
                            ? row["Title"].ToString() 
                            : "";
                        string author = row.Table.Columns.Contains("Author") && row["Author"] != DBNull.Value 
                            ? row["Author"].ToString() 
                            : "";
                        string isbn = row.Table.Columns.Contains("ISBN") && row["ISBN"] != DBNull.Value 
                            ? row["ISBN"].ToString() 
                            : "";
                        row["BookDetails"] = $"{title}\n{author}\nISBN: {isbn}";

                        // Set Publisher with year
                        string publisher = row.Table.Columns.Contains("Publisher") && row["Publisher"] != DBNull.Value 
                            ? row["Publisher"].ToString() 
                            : "";
                        string year = row.Table.Columns.Contains("YearPublished") && row["YearPublished"] != DBNull.Value 
                            ? row["YearPublished"].ToString() 
                            : "";
                        if (string.IsNullOrEmpty(year) && row.Table.Columns.Contains("PublicationYear") && row["PublicationYear"] != DBNull.Value)
                        {
                            year = row["PublicationYear"].ToString();
                        }
                        row["Publisher"] = !string.IsNullOrEmpty(year) ? $"{publisher}, {year}" : publisher;

                        // Set Copies (available/total)
                        int available = row.Table.Columns.Contains("Available") && row["Available"] != DBNull.Value 
                            ? Convert.ToInt32(row["Available"]) 
                            : 0;
                        int totalCopies = 0;
                        if (row.Table.Columns.Contains("TotalCopies") && row["TotalCopies"] != DBNull.Value)
                        {
                            totalCopies = Convert.ToInt32(row["TotalCopies"]);
                        }
                        else if (row.Table.Columns.Contains("Copies") && row["Copies"] != DBNull.Value)
                        {
                            // If Copies is still int, read it
                            if (row["Copies"] is int)
                            {
                                totalCopies = (int)row["Copies"];
                            }
                            else if (int.TryParse(row["Copies"].ToString(), out int parsed))
                            {
                                totalCopies = parsed;
                            }
                        }
                        // Default to 1 if no copies found
                        if (totalCopies == 0)
                        {
                            totalCopies = 1;
                            available = 1;
                        }
                        row["Copies"] = $"{available}/{totalCopies} available";

                        if (row.Table.Columns.Contains("Location") && row["Location"] != DBNull.Value && !string.IsNullOrEmpty(row["Location"].ToString()))
                        {
                            row["Location"] = row["Location"].ToString();
                        }
                        else
                        {
                            string category = row["Category"] != DBNull.Value ? row["Category"].ToString() : "";
                            row["Location"] = GenerateLocation(category, bookId);
                        }

                        if (available == 0)
                        {
                            row["Status"] = "Out of Stock";
                        }
                        else if (available < totalCopies * 0.3)
                        {
                            row["Status"] = "Limited";
                        }
                        else
                        {
                            row["Status"] = "Available";
                        }
                    }
                    catch (Exception rowEx)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error processing book row: {rowEx.Message}");
                        System.Diagnostics.Debug.WriteLine($"Stack trace: {rowEx.StackTrace}");
                        // Continue with next row
                    }
                }

                System.Diagnostics.Debug.WriteLine($"LoadBooks: Processed {allBooksData.Rows.Count} rows");
                System.Diagnostics.Debug.WriteLine($"LoadBooks: Final DataTable columns: {string.Join(", ", allBooksData.Columns.Cast<DataColumn>().Select(c => $"{c.ColumnName}({c.DataType.Name})"))}");
                
                // Directly set DataSource first to test
                dataGridViewBooks.DataSource = null;
                dataGridViewBooks.DataSource = allBooksData;
                System.Diagnostics.Debug.WriteLine($"LoadBooks: DataSource set directly with {allBooksData.Rows.Count} rows");
                System.Diagnostics.Debug.WriteLine($"LoadBooks: DataGridView row count: {dataGridViewBooks.Rows.Count}");
                
                // Now apply filters
                ApplyFilters();
                System.Diagnostics.Debug.WriteLine("LoadBooks: Filters applied, data should be visible now");
            }
            catch (MySqlException sqlEx)
            {
                System.Diagnostics.Debug.WriteLine($"MySQL Error loading books: {sqlEx.Message}");
                System.Diagnostics.Debug.WriteLine($"Error Number: {sqlEx.Number}");
                MessageBox.Show($"Database error: {sqlEx.Message}\n\nPlease check your database connection and ensure the Books table exists.", 
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading books: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                MessageBox.Show($"Error loading books: {ex.Message}\n\nPlease verify:\n1. Database connection is active\n2. Books table exists\n3. Table has required columns: BookID, Title, Author\n\nError: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable GetBooksData()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("GetBooksData: Fetching books from service...");
                
                IEnumerable<Book> books = null;
                try
                {
                    books = _bookService.GetAllBooks();
                    System.Diagnostics.Debug.WriteLine($"GetBooksData: Service returned IEnumerable, converting to list...");
                }
                catch (Exception serviceEx)
                {
                    System.Diagnostics.Debug.WriteLine($"GetBooksData: Error calling GetAllBooks service: {serviceEx.Message}");
                    System.Diagnostics.Debug.WriteLine($"Inner exception: {serviceEx.InnerException?.Message ?? "None"}");
                    System.Diagnostics.Debug.WriteLine($"Stack trace: {serviceEx.StackTrace}");
                    throw;
                }
                
                if (books == null)
                {
                    System.Diagnostics.Debug.WriteLine("GetBooksData: Service returned null");
                    return new DataTable();
                }
                
                var booksList = books.ToList();
                System.Diagnostics.Debug.WriteLine($"GetBooksData: Retrieved {booksList.Count} books from service");
                
                if (booksList.Count == 0)
                {
                    System.Diagnostics.Debug.WriteLine("GetBooksData: No books returned from service (empty list)");
                    return new DataTable();
                }
                
                System.Diagnostics.Debug.WriteLine($"GetBooksData: Sample book - Title: {booksList[0].Title}, Author: {booksList[0].Author}");
                
                var dataTable = DataTableHelper.BooksToDataTable(booksList);
                System.Diagnostics.Debug.WriteLine($"GetBooksData: Created DataTable with {dataTable.Rows.Count} rows and {dataTable.Columns.Count} columns");
                
                if (dataTable.Rows.Count > 0)
                {
                    System.Diagnostics.Debug.WriteLine($"GetBooksData: Sample DataTable row - Title: {dataTable.Rows[0]["Title"]}, Author: {dataTable.Rows[0]["Author"]}");
                }
                
                return dataTable;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetBooksData error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Inner exception: {ex.InnerException?.Message ?? "None"}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        private string GenerateLocation(string category, int bookId)
        {

            if (string.IsNullOrEmpty(category))
                return $"A-{bookId % 100}-{bookId % 10}";

            char section = 'A';
            if (category.ToLower().Contains("technology") || category.ToLower().Contains("tech"))
                section = 'C';
            else if (category.ToLower().Contains("fiction"))
                section = 'A';
            else
                section = 'B';

            return $"{section}-{(bookId % 100).ToString().PadLeft(2, '0')}-{bookId % 10}";
        }

        private void ApplyFilters()
        {
            if (allBooksData == null)
            {
                System.Diagnostics.Debug.WriteLine("ApplyFilters: allBooksData is null");
                return;
            }

            System.Diagnostics.Debug.WriteLine($"ApplyFilters: Starting with {allBooksData.Rows.Count} total rows");

            string searchText = txtSearch.Text.ToLower();
            if (searchText == "search by title, author, isbn, or accession number...")
                searchText = "";

            string selectedCategory = cmbCategoryFilter.SelectedItem?.ToString();
            if (selectedCategory == "All Categories")
                selectedCategory = null;

            string selectedResourceType = cmbResourceTypeFilter?.SelectedItem?.ToString();
            if (selectedResourceType == "All Resource Types")
                selectedResourceType = null;

            DataTable filteredData = allBooksData.Clone();

            foreach (DataRow row in allBooksData.Rows)
            {
                try
                {
                    bool matchesSearch = string.IsNullOrEmpty(searchText) ||
                        (row.Table.Columns.Contains("Title") && row["Title"]?.ToString().ToLower().Contains(searchText) == true) ||
                        (row.Table.Columns.Contains("Author") && row["Author"]?.ToString().ToLower().Contains(searchText) == true) ||
                        (row.Table.Columns.Contains("ISBN") && row["ISBN"]?.ToString().ToLower().Contains(searchText) == true) ||
                        (row.Table.Columns.Contains("AccessionNo") && row["AccessionNo"]?.ToString().ToLower().Contains(searchText) == true);

                    bool matchesCategory = selectedCategory == null || 
                        (row.Table.Columns.Contains("Category") && row["Category"]?.ToString() == selectedCategory);

                    bool matchesResourceType = selectedResourceType == null || selectedResourceType == "Books";

                    if (matchesSearch && matchesCategory && matchesResourceType)
                    {
                        filteredData.ImportRow(row);
                    }
                }
                catch (Exception rowEx)
                {
                    System.Diagnostics.Debug.WriteLine($"Error filtering row: {rowEx.Message}");
                    // Continue with next row
                }
            }

            System.Diagnostics.Debug.WriteLine($"ApplyFilters: Filtered to {filteredData.Rows.Count} rows");
            
            // Clear and set DataSource
            dataGridViewBooks.DataSource = null;
            dataGridViewBooks.Refresh();
            
            dataGridViewBooks.DataSource = filteredData;
            dataGridViewBooks.Refresh();
            
            System.Diagnostics.Debug.WriteLine($"ApplyFilters: DataSource set, DataGridView should show {filteredData.Rows.Count} rows");
            System.Diagnostics.Debug.WriteLine($"ApplyFilters: Actual DataGridView row count: {dataGridViewBooks.Rows.Count}");
            
            // Force a repaint
            dataGridViewBooks.Invalidate();
            dataGridViewBooks.Update();
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search by title, author, ISBN, or accession number...")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Search by title, author, ISBN, or accession number...";
                txtSearch.ForeColor = Color.Gray;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ApplyFilters();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Search text changed error: {ex}");

            }
        }

        private void cmbResourceTypeFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ApplyFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while filtering: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Filter error: {ex}");
            }
        }

        private void cmbCategoryFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ApplyFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while filtering: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Filter error: {ex}");
            }
        }

        private void dataGridViewBooks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

                DataGridViewRow row = dataGridViewBooks.Rows[e.RowIndex];
                string columnName = dataGridViewBooks.Columns[e.ColumnIndex].Name;

                int bookId = 0;
                
                // Try multiple ways to get BookID
                if (row.DataBoundItem != null)
                {
                    DataRowView rowView = row.DataBoundItem as DataRowView;
                    if (rowView != null)
                    {
                        // Try from DataRowView
                        if (rowView.Row.Table.Columns.Contains("BookID"))
                        {
                            object bookIdObj = rowView["BookID"];
                            if (bookIdObj != null && bookIdObj != DBNull.Value)
                            {
                                if (int.TryParse(bookIdObj.ToString(), out int parsedId))
                                {
                                    bookId = parsedId;
                                }
                            }
                        }
                        
                        // If still 0, try from the underlying DataRow
                        if (bookId == 0 && rowView.Row != null)
                        {
                            if (rowView.Row.Table.Columns.Contains("BookID"))
                            {
                                object bookIdObj = rowView.Row["BookID"];
                                if (bookIdObj != null && bookIdObj != DBNull.Value)
                                {
                                    if (int.TryParse(bookIdObj.ToString(), out int parsedId))
                                    {
                                        bookId = parsedId;
                                    }
                                }
                            }
                        }
                    }
                }
                
                // If still 0, try getting from the DataTable directly
                if (bookId == 0 && allBooksData != null && e.RowIndex < allBooksData.Rows.Count)
                {
                    DataRow dataRow = allBooksData.Rows[e.RowIndex];
                    if (dataRow.Table.Columns.Contains("BookID"))
                    {
                        object bookIdObj = dataRow["BookID"];
                        if (bookIdObj != null && bookIdObj != DBNull.Value)
                        {
                            if (int.TryParse(bookIdObj.ToString(), out int parsedId))
                            {
                                bookId = parsedId;
                            }
                        }
                    }
                }

                if (bookId == 0)
                {
                    System.Diagnostics.Debug.WriteLine($"Unable to identify book. RowIndex={e.RowIndex}, ColumnIndex={e.ColumnIndex}");
                    System.Diagnostics.Debug.WriteLine($"DataBoundItem is null: {row.DataBoundItem == null}");
                    if (allBooksData != null)
                    {
                        System.Diagnostics.Debug.WriteLine($"allBooksData has {allBooksData.Rows.Count} rows");
                        if (e.RowIndex < allBooksData.Rows.Count)
                        {
                            var dr = allBooksData.Rows[e.RowIndex];
                            System.Diagnostics.Debug.WriteLine($"Row columns: {string.Join(", ", dr.Table.Columns.Cast<DataColumn>().Select(c => c.ColumnName))}");
                        }
                    }
                    MessageBox.Show("Unable to identify book.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (columnName == "Edit")
                {
                    EditBook(bookId);
                }
                else if (columnName == "View")
                {
                    ViewBook(bookId);
                }
                else if (columnName == "Delete")
                {
                    DeleteBook(bookId, row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while processing your request: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"CellContentClick error: {ex}");
            }
        }

        private void EditBook(int bookId)
        {
            try
            {

                MessageBox.Show($"Edit book with ID: {bookId}\n\nEditBookForm will be opened here.", "Edit Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening edit form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ViewBook(int bookId)
        {
            try
            {

                MessageBox.Show($"View book details for ID: {bookId}", "View Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error viewing book: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteBook(int bookId, DataGridViewRow row)
        {
            try
            {
                string bookTitle = "Unknown";
                if (row.Cells["BookDetails"]?.Value != null)
                {
                    string bookDetails = row.Cells["BookDetails"].Value.ToString();
                    if (!string.IsNullOrEmpty(bookDetails))
                    {
                        bookTitle = bookDetails.Split('\n')[0];
                    }
                }
            var result = MessageBox.Show(
                $"Are you sure you want to delete book \"{bookTitle}\"?\n\nThis action cannot be undone.",
                "Delete Book",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (var conn = _dbContext.GetConnection())
                    {
                        conn.Open();
                        string query = "DELETE FROM Books WHERE BookID = @bookId";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@bookId", bookId);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Book deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadBooks();
                    LoadMetrics();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting book: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"DeleteBook error: {ex}");
            }
        }

        private void btnAddNewBook_Click(object sender, EventArgs e)
        {
            try
            {
                using (AdminCatalogNewBookForm addBookForm = new AdminCatalogNewBookForm())
                {
                    if (addBookForm.ShowDialog() == DialogResult.OK)
                    {

                        LoadBooks();
                        LoadMetrics();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening add book form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImportCSV_Click(object sender, EventArgs e)
        {
            try
            {

                MessageBox.Show("Import CSV functionality will be implemented here.\n\nImportBooksForm will be opened.", "Import CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening import form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblResourceTypeFilter_Click(object sender, EventArgs e)
        {

        }

        private void panelSearchFilter_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
