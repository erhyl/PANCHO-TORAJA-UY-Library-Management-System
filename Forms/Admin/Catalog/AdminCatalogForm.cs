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
            try
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
                            // Try to get TotalCopies first, then Copies
                            if (rowView.Row.Table.Columns.Contains("TotalCopies") && rowView["TotalCopies"] != DBNull.Value)
                            {
                                object totalValue = rowView["TotalCopies"];
                                if (totalValue is int)
                                    totalCopies = (int)totalValue;
                                else if (totalValue is decimal)
                                    totalCopies = (int)(decimal)totalValue;
                                else if (int.TryParse(totalValue.ToString(), out int parsedTotal))
                                    totalCopies = parsedTotal;
                            }
                            else if (e.Value is int)
                            {
                                totalCopies = (int)e.Value;
                            }
                            else if (e.Value is decimal)
                            {
                                totalCopies = (int)(decimal)e.Value;
                            }
                            else if (int.TryParse(e.Value.ToString(), out int parsedCopies))
                            {
                                totalCopies = parsedCopies;
                            }
                            
                            int available = 0;
                            if (rowView.Row.Table.Columns.Contains("Available") && rowView["Available"] != DBNull.Value)
                            {
                                object availableValue = rowView["Available"];
                                if (availableValue is int)
                                {
                                    available = (int)availableValue;
                                }
                                else if (availableValue is decimal)
                                {
                                    available = (int)(decimal)availableValue;
                                }
                                else if (int.TryParse(availableValue.ToString(), out int parsedAvailable))
                                {
                                    available = parsedAvailable;
                                }
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
                    // Check which columns exist
                    bool hasTotalCopies = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "TotalCopies");
                    bool hasCopies = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "Copies");
                    string copiesColumn = hasTotalCopies ? "TotalCopies" : (hasCopies ? "Copies" : "1");
                    
                    string queryTotalTitles = "SELECT COUNT(DISTINCT BookID) FROM Books";
                    using (MySqlCommand cmd = new MySqlCommand(queryTotalTitles, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        int totalTitles = result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
                        lblMetricTotalTitlesValue.Text = totalTitles.ToString();
                    }
                    
                    string queryTotalCopies = $"SELECT COALESCE(SUM({copiesColumn}), 0) FROM Books";
                    using (MySqlCommand cmd = new MySqlCommand(queryTotalCopies, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        int totalCopies = result != DBNull.Value && result != null ? Convert.ToInt32(result) : 0;
                        lblMetricTotalCopiesValue.Text = totalCopies.ToString();
                    }
                    
                    string queryAvailable = "SELECT COALESCE(SUM(Available), 0) FROM Books WHERE Available IS NOT NULL";
                    using (MySqlCommand cmd = new MySqlCommand(queryAvailable, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        int available = result != DBNull.Value && result != null ? Convert.ToInt32(result) : 0;
                        lblMetricAvailableValue.Text = available.ToString();
                    }
                    
                    string queryOnLoan = $"SELECT COALESCE(SUM({copiesColumn} - COALESCE(Available, 0)), 0) FROM Books WHERE Available IS NOT NULL";
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
                
                foreach (DataRow row in allBooksData.Rows)
                {
                    int bookId = Convert.ToInt32(row["BookID"]);
                    
                    // Handle Barcode/AccessionNo
                    string barcode = "";
                    if (row.Table.Columns.Contains("Barcode") && row["Barcode"] != DBNull.Value)
                        barcode = row["Barcode"].ToString();
                    else if (row.Table.Columns.Contains("AccessionNo") && row["AccessionNo"] != DBNull.Value)
                        barcode = row["AccessionNo"].ToString();
                    
                    // Format AccessionNo - use existing or generate from BookID
                    if (string.IsNullOrEmpty(barcode))
                    {
                        // Generate in format ACC-XXXXXX (6 digits)
                        row["AccessionNo"] = $"ACC-{bookId.ToString().PadLeft(6, '0')}";
                    }
                    else
                    {
                        // Ensure it starts with ACC- if it doesn't
                        if (!barcode.StartsWith("ACC-"))
                        {
                            row["AccessionNo"] = $"ACC-{barcode}";
                        }
                        else
                        {
                            row["AccessionNo"] = barcode;
                        }
                    }
                    
                    string title = row.Table.Columns.Contains("Title") && row["Title"] != DBNull.Value ? row["Title"].ToString() : "";
                    string author = row.Table.Columns.Contains("Author") && row["Author"] != DBNull.Value ? row["Author"].ToString() : "";
                    string isbn = row.Table.Columns.Contains("ISBN") && row["ISBN"] != DBNull.Value ? row["ISBN"].ToString() : "";
                    // Format BookDetails as multiline: Title, Author, ISBN
                    row["BookDetails"] = $"{title}\n{author}\nISBN: {isbn}";
                    
                    string publisher = row.Table.Columns.Contains("Publisher") && row["Publisher"] != DBNull.Value ? row["Publisher"].ToString() : "";
                    string year = "";
                    if (row.Table.Columns.Contains("YearPublished") && row["YearPublished"] != DBNull.Value)
                        year = row["YearPublished"].ToString();
                    else if (row.Table.Columns.Contains("PublicationYear") && row["PublicationYear"] != DBNull.Value)
                        year = row["PublicationYear"].ToString();
                    
                    row["Publisher"] = !string.IsNullOrEmpty(year) ? $"{publisher}, {year}" : publisher;
                    
                    int available = row.Table.Columns.Contains("Available") && row["Available"] != DBNull.Value ? Convert.ToInt32(row["Available"]) : 0;
                    int totalCopies = 0;
                    if (row.Table.Columns.Contains("TotalCopies") && row["TotalCopies"] != DBNull.Value)
                        totalCopies = Convert.ToInt32(row["TotalCopies"]);
                    else if (row.Table.Columns.Contains("Copies") && row["Copies"] != DBNull.Value)
                        totalCopies = Convert.ToInt32(row["Copies"]);
                    
                    row["TotalCopies"] = totalCopies;
                    row["Copies"] = totalCopies;
                    
                    if (row.Table.Columns.Contains("Location") && row["Location"] != DBNull.Value && !string.IsNullOrEmpty(row["Location"].ToString()))
                    {
                        row["Location"] = row["Location"].ToString();
                    }
                    else
                    {
                        string category = row.Table.Columns.Contains("Category") && row["Category"] != DBNull.Value ? row["Category"].ToString() : "";
                        row["Location"] = GenerateLocation(category, bookId);
                    }
                    
                    if (totalCopies == 0)
                    {
                        row["Status"] = "No Copies";
                    }
                    else if (available == 0)
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
            if (allBooksData == null) return;
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
                bool matchesSearch = string.IsNullOrEmpty(searchText) ||
                    row["Title"].ToString().ToLower().Contains(searchText) ||
                    row["Author"].ToString().ToLower().Contains(searchText) ||
                    row["ISBN"].ToString().ToLower().Contains(searchText) ||
                    row["AccessionNo"].ToString().ToLower().Contains(searchText);
                bool matchesCategory = selectedCategory == null || row["Category"].ToString() == selectedCategory;
                bool matchesResourceType = selectedResourceType == null || selectedResourceType == "Books";
                if (matchesSearch && matchesCategory && matchesResourceType)
                {
                    filteredData.ImportRow(row);
                }
            }
                dataGridViewBooks.DataSource = filteredData;
                
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
                // Use AdminCatalogNewBookForm for editing (if it supports edit mode)
                // For now, show a message with book details
                string message = $"Edit Book: {book.Title}\n\n" +
                               $"Book ID: {bookId}\n" +
                               $"Accession No: {book.AccessionNo ?? "N/A"}\n" +
                               $"Author: {book.Author ?? "N/A"}\n" +
                               $"ISBN: {book.ISBN ?? "N/A"}\n\n" +
                               $"Edit functionality will open the book edit form.";
                MessageBox.Show(message, "Edit Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // TODO: Open AdminCatalogNewBookForm or AdminCatalogEditBookForm in edit mode
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
                var book = _bookService.GetBook(bookId);
                if (book == null)
                {
                    MessageBox.Show("Book not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string message = $"Book Details\n\n" +
                               $"Title: {book.Title ?? "N/A"}\n" +
                               $"Author: {book.Author ?? "N/A"}\n" +
                               $"ISBN: {book.ISBN ?? "N/A"}\n" +
                               $"Category: {book.Category ?? "N/A"}\n" +
                               $"Publisher: {book.Publisher ?? "N/A"}\n" +
                               $"Year: {book.PublicationYear?.ToString() ?? "N/A"}\n" +
                               $"Accession No: {book.AccessionNo ?? "N/A"}\n" +
                               $"Location: {book.Location ?? "N/A"}\n" +
                               $"Total Copies: {book.TotalCopies}\n" +
                               $"Available: {book.Available}\n" +
                               $"Status: {book.Status ?? "N/A"}";
                MessageBox.Show(message, "View Book Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                $"Are you sure you want to delete book \"{bookTitle}\" (Accession: {accessionNo})?\n\nThis action cannot be undone.",
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
    }
}