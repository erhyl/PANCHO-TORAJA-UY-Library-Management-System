using System;
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
using Project5LMS.Repositories;
namespace Project5LMS.Forms.Admin.Catalog
{
    public partial class AdminCatalogForm : Form
    {
        private DataTable allBooksData;
        private readonly IBookService _bookService;
        private readonly DatabaseContext _dbContext;
        private readonly IBookCopyRepository _copyRepository;
        public AdminCatalogForm()
        {
            InitializeComponent();
            _bookService = ServiceFactory.CreateBookService();
            _dbContext = ServiceFactory.GetDbContext();
            _copyRepository = DependencyInjection.GetRequiredService<IBookCopyRepository>();
            
            // Refresh catalog when form becomes active (e.g., after borrowing/reserving from other forms)
            this.Activated += AdminCatalogForm_Activated;
        }
        
        private void AdminCatalogForm_Activated(object sender, EventArgs e)
        {
            // Refresh catalog data when form becomes active to reflect latest borrowing/reservation status
            try
            {
                LoadBooks();
                LoadMetrics();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error refreshing catalog on activation: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Public method to refresh catalog data and metrics
        /// Can be called from other forms after borrowing/reserving books
        /// </summary>
        public void RefreshCatalog()
        {
            try
            {
                LoadBooks();
                LoadMetrics();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error refreshing catalog: {ex.Message}");
            }
        }
        private void AdminCatalogForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Ensure BookID is AUTO_INCREMENT
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    DatabaseSchemaHelper.EnsureBookIDIsAutoIncrement(conn);
                }
                
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
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading catalog form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Catalog form load error: {ex}");
            }
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
            dataGridViewBooks.DataError -= DataGridViewBooks_DataError;
            dataGridViewBooks.DataError += DataGridViewBooks_DataError;
        }
        private void DataGridViewBooks_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            System.Diagnostics.Debug.WriteLine($"DataGridView error in row {e.RowIndex}, column {e.ColumnIndex}: {e.Exception.Message}");
        }
        private void DataGridViewBooks_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                DataGridViewRow row = dataGridViewBooks.Rows[e.RowIndex];
                string columnName = dataGridViewBooks.Columns[e.ColumnIndex].Name;
            if (columnName == "BookID" && e.Value != null)
            {
                // Ensure BookID is displayed as integer
                if (int.TryParse(e.Value.ToString(), out int bookId))
                {
                    e.Value = bookId.ToString();
                }
                e.FormattingApplied = true;
            }
            if (columnName == "AccessionNo" && e.Value != null)
            {
                string accessionNo = e.Value.ToString();
                if (!accessionNo.StartsWith("ACC-"))
                {
                    if (int.TryParse(accessionNo, out int accNum))
                    {
                        e.Value = $"ACC-{accNum}";
                    }
                    else
                    {
                        e.Value = $"ACC-{accessionNo}";
                    }
                }
                e.FormattingApplied = true;
            }
            if (columnName == "BookDetails" && e.Value != null)
            {
                // BookDetails is already formatted in LoadBooks() as multiline
                // Ensure it displays properly with line breaks
                string details = e.Value.ToString();
                e.CellStyle.WrapMode = DataGridViewTriState.True;
                e.FormattingApplied = true;
            }
            if (columnName == "Publisher" && e.Value != null)
            {
                // Publisher is already formatted in LoadBooks() as multiline
                // Ensure it displays properly with line breaks
                string publisher = e.Value.ToString();
                e.CellStyle.WrapMode = DataGridViewTriState.True;
                e.FormattingApplied = true;
            }
            if (columnName == "Copies" && e.Value != null)
            {
                try
                {
                    if (row.DataBoundItem != null)
                    {
                        DataRowView rowView = row.DataBoundItem as DataRowView;
                        if (rowView != null)
                        {
                            int totalCopies = 0;
                            // Try to get TotalCopies first, then Copies (all are now strings)
                            if (rowView.Row.Table.Columns.Contains("TotalCopies") && rowView["TotalCopies"] != DBNull.Value && rowView["TotalCopies"] != null)
                            {
                                object totalValue = rowView["TotalCopies"];
                                if (totalValue is int)
                                    totalCopies = (int)totalValue;
                                else if (totalValue is decimal)
                                    totalCopies = (int)(decimal)totalValue;
                                else if (int.TryParse(totalValue?.ToString(), out int parsedTotal))
                                    totalCopies = parsedTotal;
                            }
                            else if (e.Value != null)
                            {
                                // e.Value is now a string, parse it
                                if (e.Value is int)
                                    totalCopies = (int)e.Value;
                                else if (e.Value is decimal)
                                    totalCopies = (int)(decimal)e.Value;
                                else if (int.TryParse(e.Value?.ToString(), out int parsedCopies))
                                    totalCopies = parsedCopies;
                            }
                            
                            int available = 0;
                            if (rowView.Row.Table.Columns.Contains("Available") && rowView["Available"] != DBNull.Value && rowView["Available"] != null)
                            {
                                object availableValue = rowView["Available"];
                                if (availableValue is int)
                                    available = (int)availableValue;
                                else if (availableValue is decimal)
                                    available = (int)(decimal)availableValue;
                                else if (int.TryParse(availableValue?.ToString(), out int parsedAvailable))
                                    available = parsedAvailable;
                            }
                            // Format as "X/Y\navailable" for multiline display
                            e.Value = $"{available}/{totalCopies}\navailable";
                            e.CellStyle.WrapMode = DataGridViewTriState.True;
                            e.FormattingApplied = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error formatting Copies column: {ex.Message}");
                    e.FormattingApplied = false;
                }
            }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"CellFormatting error: {ex}");
            }
        }
        private void DataGridViewBooks_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                string columnName = dataGridViewBooks.Columns[e.ColumnIndex].Name;
                
                // Paint Status column with color-coded badges
                if (columnName == "Status")
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                    string value = e.Value?.ToString() ?? "";
                    Color bgColor = Color.LightGray;
                    Color textColor = Color.Black;
                    
                    switch (value.ToLower())
                    {
                        case "available":
                            bgColor = Color.FromArgb(40, 167, 69); // Green
                            textColor = Color.White;
                            break;
                        case "limited":
                            bgColor = Color.FromArgb(255, 193, 7); // Orange/Amber
                            textColor = Color.Black;
                            break;
                        case "out of stock":
                        case "outofstock":
                        case "no copies":
                            bgColor = Color.FromArgb(220, 53, 69); // Red
                            textColor = Color.White;
                            break;
                        default:
                            bgColor = Color.LightGray;
                            textColor = Color.Black;
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
                // For multiline cells (BookDetails, Publisher, Copies), let default rendering handle it
                // WrapMode is already enabled in Designer, so default rendering will show multiline text
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
                    
                    // Calculate from actual BookCopies table for accuracy
                    string queryTotalTitles = "SELECT COUNT(DISTINCT BookID) FROM Books";
                    using (MySqlCommand cmd = new MySqlCommand(queryTotalTitles, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        int totalTitles = result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
                        lblMetricTotalTitlesValue.Text = totalTitles.ToString();
                    }
                    
                    // Get total copies from BookCopies table (actual count)
                    string queryTotalCopies = "SELECT COUNT(*) FROM BookCopies";
                    using (MySqlCommand cmd = new MySqlCommand(queryTotalCopies, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        int totalCopies = result != DBNull.Value && result != null ? Convert.ToInt32(result) : 0;
                        lblMetricTotalCopiesValue.Text = totalCopies.ToString();
                    }
                    
                    // Get available copies from BookCopies table where status is 'Available'
                    string queryAvailable = "SELECT COUNT(*) FROM BookCopies WHERE CopyStatus = 'Available'";
                    using (MySqlCommand cmd = new MySqlCommand(queryAvailable, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        int available = result != DBNull.Value && result != null ? Convert.ToInt32(result) : 0;
                        lblMetricAvailableValue.Text = available.ToString();
                    }
                    
                    // Get on loan copies (total - available)
                    string queryOnLoan = @"SELECT COUNT(*) FROM BookCopies 
                                          WHERE CopyStatus IN ('Borrowed', 'Reserved')";
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
                allBooksData = GetBooksData();
                
                if (allBooksData == null)
                {
                    System.Diagnostics.Debug.WriteLine("GetBooksData returned null");
                    allBooksData = new DataTable();
                }
                
                if (allBooksData.Rows.Count == 0)
                {
                    System.Diagnostics.Debug.WriteLine("No books found in database");
                    // Still set the data source so the grid shows (empty)
                    ApplyFilters();
                    return;
                }
                
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
                if (!allBooksData.Columns.Contains("Location"))
                {
                    allBooksData.Columns.Add("Location", typeof(string));
                }
                if (!allBooksData.Columns.Contains("Status"))
                {
                    allBooksData.Columns.Add("Status", typeof(string));
                }
                if (!allBooksData.Columns.Contains("TotalCopies"))
                {
                    allBooksData.Columns.Add("TotalCopies", typeof(int));
                }
                
                // Convert DataTable to all strings to avoid DataGridView type mismatches
                DataTable stringDataTable = new DataTable();
                foreach (DataColumn col in allBooksData.Columns)
                {
                    stringDataTable.Columns.Add(col.ColumnName, typeof(string));
                }
                
                // Add computed columns if they don't exist
                if (!stringDataTable.Columns.Contains("BookID"))
                    stringDataTable.Columns.Add("BookID", typeof(string));
                if (!stringDataTable.Columns.Contains("AccessionNo"))
                    stringDataTable.Columns.Add("AccessionNo", typeof(string));
                if (!stringDataTable.Columns.Contains("BookDetails"))
                    stringDataTable.Columns.Add("BookDetails", typeof(string));
                if (!stringDataTable.Columns.Contains("TotalCopies"))
                    stringDataTable.Columns.Add("TotalCopies", typeof(string));
                
                System.Diagnostics.Debug.WriteLine($"Books table count: {allBooksData.Rows.Count}");
                System.Diagnostics.Debug.WriteLine($"GetBooksData returned {allBooksData.Rows.Count} books");
                
                foreach (DataRow row in allBooksData.Rows)
                {
                    DataRow newRow = stringDataTable.NewRow();
                    
                    // Copy all original columns as strings
                    foreach (DataColumn col in allBooksData.Columns)
                    {
                        if (stringDataTable.Columns.Contains(col.ColumnName))
                        {
                            object value = row[col.ColumnName];
                            if (value == null || value == DBNull.Value)
                                newRow[col.ColumnName] = "";
                            else
                                newRow[col.ColumnName] = value.ToString();
                        }
                    }
                    
                    // Process computed values
                    int bookId = int.TryParse(newRow["BookID"]?.ToString(), out int bid) ? bid : 0;
                    
                    // Ensure BookID is set as string
                    if (bookId > 0)
                    {
                        newRow["BookID"] = bookId.ToString();
                    }
                    else if (row.Table.Columns.Contains("BookID") && row["BookID"] != DBNull.Value)
                    {
                        newRow["BookID"] = row["BookID"].ToString();
                    }
                    
                    // Handle Barcode/AccessionNo
                    string barcode = "";
                    if (row.Table.Columns.Contains("Barcode") && row["Barcode"] != DBNull.Value)
                        barcode = row["Barcode"].ToString();
                    else if (row.Table.Columns.Contains("AccessionNo") && row["AccessionNo"] != DBNull.Value)
                        barcode = row["AccessionNo"].ToString();
                    
                    // Format AccessionNo - use existing or generate from BookID
                    if (string.IsNullOrEmpty(barcode))
                    {
                        newRow["AccessionNo"] = $"ACC-{bookId.ToString().PadLeft(6, '0')}";
                    }
                    else
                    {
                        if (!barcode.StartsWith("ACC-"))
                            newRow["AccessionNo"] = $"ACC-{barcode}";
                        else
                            newRow["AccessionNo"] = barcode;
                    }
                    
                    string title = newRow["Title"]?.ToString() ?? "";
                    string author = newRow["Author"]?.ToString() ?? "";
                    string isbn = newRow["ISBN"]?.ToString() ?? "";
                    newRow["BookDetails"] = $"{title}\n{author}\nISBN: {isbn}";
                    
                    string publisher = newRow["Publisher"]?.ToString() ?? "";
                    string year = "";
                    if (row.Table.Columns.Contains("YearPublished") && row["YearPublished"] != DBNull.Value)
                        year = row["YearPublished"].ToString();
                    else if (row.Table.Columns.Contains("PublicationYear") && row["PublicationYear"] != DBNull.Value)
                        year = row["PublicationYear"].ToString();
                    
                    newRow["Publisher"] = !string.IsNullOrEmpty(year) ? $"{publisher}, {year}" : publisher;
                    
                    // Calculate actual copies from BookCopies table for accuracy
                    int actualTotalCopies = 0;
                    int actualAvailable = 0;
                    try
                    {
                        var copies = _copyRepository.GetByBookId(bookId).ToList();
                        actualTotalCopies = copies.Count;
                        actualAvailable = copies.Count(c => c.IsAvailable);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error getting copy counts for book {bookId}: {ex.Message}");
                        // Fallback to Books table values if BookCopies query fails
                        if (row.Table.Columns.Contains("TotalCopies") && row["TotalCopies"] != DBNull.Value)
                            actualTotalCopies = Convert.ToInt32(row["TotalCopies"]);
                        else if (row.Table.Columns.Contains("Copies") && row["Copies"] != DBNull.Value)
                            actualTotalCopies = Convert.ToInt32(row["Copies"]);
                        if (row.Table.Columns.Contains("Available") && row["Available"] != DBNull.Value)
                            actualAvailable = Convert.ToInt32(row["Available"]);
                    }
                    
                    newRow["TotalCopies"] = actualTotalCopies.ToString();
                    newRow["Copies"] = actualTotalCopies.ToString();
                    newRow["Available"] = actualAvailable.ToString();
                    
                    if (row.Table.Columns.Contains("Location") && row["Location"] != DBNull.Value && !string.IsNullOrEmpty(row["Location"].ToString()))
                    {
                        newRow["Location"] = row["Location"].ToString();
                    }
                    else
                    {
                        string category = newRow["Category"]?.ToString() ?? "";
                        newRow["Location"] = GenerateLocation(category, bookId);
                    }
                    
                    if (actualTotalCopies == 0)
                        newRow["Status"] = "No Copies";
                    else if (actualAvailable == 0)
                        newRow["Status"] = "Out of Stock";
                    else if (actualAvailable < actualTotalCopies * 0.3)
                        newRow["Status"] = "Limited";
                    else
                        newRow["Status"] = "Available";
                    
                    stringDataTable.Rows.Add(newRow);
                }
                
                System.Diagnostics.Debug.WriteLine($"After conversion: DataTable has {stringDataTable.Rows.Count} rows, {stringDataTable.Columns.Count} columns");
                allBooksData = stringDataTable;
                ApplyFilters();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading books: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
                MessageBox.Show(
                    $"Error loading books data:\n\n{ex.Message}\n\nPlease check:\n1. Database connection\n2. Books table exists\n3. Database permissions\n\nFor detailed diagnostics, use the Database Test feature in Settings.",
                    "Data Load Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
        private DataTable GetBooksData()
        {
            var books = _bookService.GetAllBooks();
            return DataTableHelper.BooksToDataTable(books);
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
            if (allBooksData == null || allBooksData.Rows.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("ApplyFilters: allBooksData is null or empty");
                dataGridViewBooks.DataSource = null;
                return;
            }
            
            // Handle search text - trim and check for placeholder (case-insensitive, with or without emoji)
            string searchText = txtSearch.Text?.Trim() ?? "";
            string placeholderText = "search by title, author, isbn, or accession number...";
            if (string.IsNullOrEmpty(searchText) || 
                searchText.Equals(placeholderText, StringComparison.OrdinalIgnoreCase) ||
                searchText.Equals($"🔍 {placeholderText}", StringComparison.OrdinalIgnoreCase) ||
                searchText.EndsWith(placeholderText, StringComparison.OrdinalIgnoreCase))
            {
                searchText = "";
            }
            else
            {
                // Remove emoji if present
                if (searchText.StartsWith("🔍 "))
                    searchText = searchText.Substring(2);
                searchText = searchText.ToLower();
            }
            
            // Handle filter dropdowns - treat empty strings and "All" options as null
            string selectedCategory = cmbCategoryFilter.SelectedItem?.ToString()?.Trim();
            if (string.IsNullOrEmpty(selectedCategory) || selectedCategory == "All Categories")
                selectedCategory = null;
            
            string selectedResourceType = cmbResourceTypeFilter?.SelectedItem?.ToString()?.Trim();
            if (string.IsNullOrEmpty(selectedResourceType) || selectedResourceType == "All Resource Types")
                selectedResourceType = null;
            
            System.Diagnostics.Debug.WriteLine($"ApplyFilters: Original searchText='{txtSearch.Text}', Processed searchText='{searchText}', selectedCategory='{selectedCategory}', selectedResourceType='{selectedResourceType}'");
            
            // Create filtered table with all string columns
            DataTable filteredData = new DataTable();
            foreach (DataColumn col in allBooksData.Columns)
            {
                filteredData.Columns.Add(col.ColumnName, typeof(string));
            }
            
            int rowsProcessed = 0;
            int rowsMatched = 0;
            
            foreach (DataRow row in allBooksData.Rows)
            {
                rowsProcessed++;
                bool matchesSearch = string.IsNullOrEmpty(searchText) ||
                    (row["Title"]?.ToString().ToLower().Contains(searchText) ?? false) ||
                    (row["Author"]?.ToString().ToLower().Contains(searchText) ?? false) ||
                    (row["ISBN"]?.ToString().ToLower().Contains(searchText) ?? false) ||
                    (row["AccessionNo"]?.ToString().ToLower().Contains(searchText) ?? false);
                
                bool matchesCategory = selectedCategory == null || 
                    (row["Category"]?.ToString().Equals(selectedCategory, StringComparison.OrdinalIgnoreCase) ?? false);
                
                // Match resource type based on BookType field
                bool matchesResourceType = true;
                if (selectedResourceType != null)
                {
                    string bookType = row["BookType"]?.ToString() ?? "Books";
                    
                    // Map dropdown values to BookType values
                    if (selectedResourceType == "Books")
                    {
                        matchesResourceType = bookType.Equals("Books", StringComparison.OrdinalIgnoreCase) ||
                                            bookType.Equals("Circulation", StringComparison.OrdinalIgnoreCase) ||
                                            string.IsNullOrWhiteSpace(bookType);
                    }
                    else if (selectedResourceType == "E-Books")
                    {
                        matchesResourceType = bookType.Equals("E-Books", StringComparison.OrdinalIgnoreCase) ||
                                            bookType.Equals("EBook", StringComparison.OrdinalIgnoreCase) ||
                                            bookType.Equals("E-Book", StringComparison.OrdinalIgnoreCase);
                    }
                    else if (selectedResourceType == "Journals")
                    {
                        matchesResourceType = bookType.Equals("Journals", StringComparison.OrdinalIgnoreCase) ||
                                            bookType.Equals("Journal", StringComparison.OrdinalIgnoreCase);
                    }
                    else if (selectedResourceType == "Magazines")
                    {
                        matchesResourceType = bookType.Equals("Magazines", StringComparison.OrdinalIgnoreCase) ||
                                            bookType.Equals("Magazine", StringComparison.OrdinalIgnoreCase);
                    }
                    else if (selectedResourceType == "Reference Materials")
                    {
                        matchesResourceType = bookType.Equals("Reference Materials", StringComparison.OrdinalIgnoreCase) ||
                                            bookType.Equals("Reference", StringComparison.OrdinalIgnoreCase);
                    }
                    else
                    {
                        // For "All Resource Types", show everything
                        matchesResourceType = true;
                    }
                }
                
                if (matchesSearch && matchesCategory && matchesResourceType)
                {
                    try
                    {
                        DataRow newRow = filteredData.NewRow();
                        foreach (DataColumn col in allBooksData.Columns)
                        {
                            if (filteredData.Columns.Contains(col.ColumnName))
                            {
                                object value = row[col.ColumnName];
                                if (value == null || value == DBNull.Value)
                                    newRow[col.ColumnName] = "";
                                else
                                    newRow[col.ColumnName] = value.ToString();
                            }
                        }
                        filteredData.Rows.Add(newRow);
                        rowsMatched++;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error importing row {rowsProcessed}: {ex.Message}");
                    }
                }
            }
            
            System.Diagnostics.Debug.WriteLine($"ApplyFilters: Processed {rowsProcessed} rows, matched {rowsMatched} rows");
            System.Diagnostics.Debug.WriteLine($"Filtered data has {filteredData.Rows.Count} rows");
            
            // Clear and rebind
            dataGridViewBooks.DataSource = null;
            dataGridViewBooks.Refresh();
            dataGridViewBooks.DataSource = filteredData;
            
            System.Diagnostics.Debug.WriteLine($"DataGridView DataSource set. Rows in grid: {dataGridViewBooks.Rows.Count}");
                
                // Adjust row heights for multiline content after data binding
                if (dataGridViewBooks.Rows.Count > 0)
                {
                    dataGridViewBooks.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
                    // Set minimum row height for multiline content
                    foreach (DataGridViewRow row in dataGridViewBooks.Rows)
                    {
                        if (row.Height < 80)
                        {
                            row.Height = 80; // Ensure enough height for multiline text
                        }
                    }
                }
        }
        private void txtSearch_Enter(object sender, EventArgs e)
        {
            string placeholder = "🔍 Search by title, author, ISBN, or accession number...";
            if (txtSearch.Text == placeholder || txtSearch.Text == "Search by title, author, ISBN, or accession number...")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }
        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "🔍 Search by title, author, ISBN, or accession number...";
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
                if (row.DataBoundItem != null)
                {
                    DataRowView rowView = row.DataBoundItem as DataRowView;
                    if (rowView != null && rowView.Row.Table.Columns.Contains("BookID"))
                    {
                        object bookIdObj = rowView["BookID"];
                        if (bookIdObj != null && bookIdObj != DBNull.Value)
                        {
                            bookId = Convert.ToInt32(bookIdObj);
                        }
                    }
                }
                if (bookId == 0)
                {
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
                else if (columnName == "ManageCopies" || columnName == "Manage Copies")
                {
                    OpenManageCopiesForm(bookId);
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
                var book = _bookService.GetBook(bookId);
                if (book == null)
                {
                    MessageBox.Show("Book not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                using (var editForm = new AdminCatalogNewBookForm(bookId))
                {
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadBooks();
                        LoadMetrics();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening edit form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"EditBook error: {ex}");
            }
        }
        private void ViewBook(int bookId)
        {
            try
            {
                using (var viewForm = new ViewBookForm(bookId))
                {
                    viewForm.ShowDialog();
                    // Refresh the list after viewing (in case book was updated elsewhere)
                    LoadBooks();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening view book form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"ViewBook error: {ex}");
            }
        }
        
        private void OpenManageCopiesForm(int bookId)
        {
            try
            {
                using (var manageCopiesForm = new ManageBookCopiesForm(bookId))
                {
                    if (manageCopiesForm.ShowDialog() == DialogResult.OK)
                    {
                        // Refresh books list and KPIs when copies are added/deleted
                        LoadBooks();
                        LoadMetrics();
                    }
                    else
                    {
                        // Still refresh even if dialog was cancelled, in case changes were made
                        LoadBooks();
                        LoadMetrics();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening manage copies form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"OpenManageCopiesForm error: {ex}");
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
                string accessionNo = null;
                if (row.DataBoundItem != null)
                {
                    DataRowView rowView = row.DataBoundItem as DataRowView;
                    if (rowView != null && rowView.Row.Table.Columns.Contains("AccessionNo"))
                    {
                        object accessionNoObj = rowView["AccessionNo"];
                        if (accessionNoObj != null && accessionNoObj != DBNull.Value)
                        {
                            accessionNo = accessionNoObj.ToString();
                        }
                    }
                }
                if (string.IsNullOrWhiteSpace(accessionNo) && row.Cells["AccessionNo"]?.Value != null)
                {
                    accessionNo = row.Cells["AccessionNo"].Value.ToString();
                }
                if (string.IsNullOrWhiteSpace(accessionNo))
                {
                    var book = _bookService.GetBook(bookId);
                    if (book != null)
                    {
                        accessionNo = book.AccessionNo;
                    }
                }
                if (string.IsNullOrWhiteSpace(accessionNo))
                {
                    MessageBox.Show("Unable to identify book accession number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            var result = MessageBox.Show(
                $"Are you sure you want to delete book \"{bookTitle}\" (Accession: {accessionNo})❓\n\nThis action cannot be undone.",
                "Delete Book",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                try
                {
                    bool deleted = _bookService.DeleteBook(accessionNo);
                    if (deleted)
                    {
                        MessageBox.Show("Book deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadBooks();
                        LoadMetrics();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete book. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "CSV Files|*.csv|Excel Files|*.xlsx;*.xls|All Files|*.*";
                    openFileDialog.FilterIndex = 1;
                    openFileDialog.RestoreDirectory = true;
                    openFileDialog.Title = "Select File to Import (CSV or Excel)";
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = openFileDialog.FileName;
                        ImportBooksFromCSV(filePath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening import form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ImportBooksFromCSV(string filePath)
        {
            try
            {
                var bulkImportService = new Services.BulkImportService(_dbContext, _bookService);
                using (Form progressForm = new Form())
                {
                    progressForm.Text = "Importing Books";
                    progressForm.Size = new Size(400, 150);
                    progressForm.StartPosition = FormStartPosition.CenterParent;
                    progressForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                    progressForm.MaximizeBox = false;
                    progressForm.MinimizeBox = false;
                    string fileType = System.IO.Path.GetExtension(filePath).ToLower() == ".xlsx" ||
                                      System.IO.Path.GetExtension(filePath).ToLower() == ".xls"
                                      ? "Excel" : "CSV";
                    Label lblStatus = new Label
                    {
                        Text = $"Importing books from {fileType} file...",
                        Location = new Point(20, 20),
                        AutoSize = true
                    };
                    progressForm.Controls.Add(lblStatus);
                    ProgressBar progressBar = new ProgressBar
                    {
                        Location = new Point(20, 50),
                        Size = new Size(350, 23),
                        Style = ProgressBarStyle.Marquee,
                        MarqueeAnimationSpeed = 30
                    };
                    progressForm.Controls.Add(progressBar);
                    progressForm.Show();
                    Application.DoEvents();
                    var result = bulkImportService.ImportFromFile(filePath, skipHeader: true);
                    progressForm.Close();
                    string message = $"Import Complete!\n\n" +
                                   $"Total Records: {result.TotalRecords}\n" +
                                   $"Successfully Imported: {result.SuccessCount}\n" +
                                   $"Failed: {result.FailedCount}\n" +
                                   $"Success Rate: {result.SuccessRate:F1}%";
                    if (result.HasErrors && result.Errors.Count > 0)
                    {
                        message += $"\n\nErrors ({Math.Min(result.Errors.Count, 10)} of {result.Errors.Count}):\n";
                        foreach (var error in result.Errors.Take(10))
                        {
                            message += $"• {error}\n";
                        }
                        if (result.Errors.Count > 10)
                        {
                            message += $"... and {result.Errors.Count - 10} more errors.";
                        }
                    }
                    MessageBox.Show(message, "Import Results",
                        MessageBoxButtons.OK,
                        result.SuccessCount > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                    if (result.SuccessCount > 0)
                    {
                        LoadBooks();
                        LoadMetrics();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error importing books: {ex.Message}", "Import Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lblResourceTypeFilter_Click(object sender, EventArgs e)
        {
        }
        private void panelSearchFilter_Paint(object sender, PaintEventArgs e)
        {
        }

        private void panelTableContainer_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}