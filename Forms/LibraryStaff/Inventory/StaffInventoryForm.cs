using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Project5LMS.Helpers;
using Project5LMS.Data;
using Project5LMS.Services;
using Project5LMS.Models;
namespace Project5LMS.Forms.LibraryStaff.Inventory
{
    public partial class StaffInventoryForm : Form
    {
        private DataTable inventoryData;
        private string currentCategoryFilter = "All";
        private readonly DatabaseContext _dbContext;
        public StaffInventoryForm()
        {
            InitializeComponent();
            _dbContext = ServiceFactory.GetDbContext();
        }
        private void StaffInventoryForm_Load(object sender, EventArgs e)
        {
            EnsureInventoryTableExists();
            SetupDataGridView();
            LoadCategories();
            LoadMetrics();
            LoadInventory();
            // Staff role restriction: Hide Report Lost card - Staff cannot mark books as lost
            if (cardReportLost != null)
            {
                cardReportLost.Visible = false;
                cardReportLost.Enabled = false;
            }
        }
        private void EnsureInventoryTableExists()
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string checkTableQuery = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES
                                              WHERE TABLE_SCHEMA = DATABASE()
                                              AND TABLE_NAME = 'Inventory'";
                    using (var checkCmd = new MySqlCommand(checkTableQuery, conn))
                    {
                        int tableExists = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (tableExists == 0)
                        {
                            string createTableQuery = @"CREATE TABLE IF NOT EXISTS Inventory (
                                                        InventoryID INT AUTO_INCREMENT PRIMARY KEY,
                                                        BookID INT NOT NULL,
                                                        CopyNumber INT NOT NULL,
                                                        Location VARCHAR(50) NULL,
                                                        Condition VARCHAR(50) DEFAULT 'Good',
                                                        Status VARCHAR(50) DEFAULT 'Available',
                                                        LastVerified DATETIME NULL,
                                                        Notes VARCHAR(255) NULL,
                                                        FOREIGN KEY (BookID) REFERENCES Books(BookID)
                                                        )";
                            _dbContext.ExecuteNonQuery(createTableQuery);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error ensuring Inventory table exists: {ex.Message}");
            }
        }
        private void SetupDataGridView()
        {
            dataGridViewInventory.AutoGenerateColumns = false;
            dataGridViewInventory.AllowUserToAddRows = false;
            dataGridViewInventory.AllowUserToDeleteRows = false;
            dataGridViewInventory.ReadOnly = true;
            dataGridViewInventory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewInventory.MultiSelect = false;
            dataGridViewInventory.BackgroundColor = Color.White;
            dataGridViewInventory.BorderStyle = BorderStyle.None;
            dataGridViewInventory.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridViewInventory.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridViewInventory.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dataGridViewInventory.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            dataGridViewInventory.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            dataGridViewInventory.RowTemplate.Height = 40;
            dataGridViewInventory.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dataGridViewInventory.CellFormatting += DataGridViewInventory_CellFormatting;
            dataGridViewInventory.DataError += DataGridViewInventory_DataError;
            dataGridViewInventory.Columns.Clear();
            dataGridViewInventory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "BookID",
                HeaderText = "Book ID",
                DataPropertyName = "BookID",
                Width = 100
            });
            dataGridViewInventory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Title",
                HeaderText = "Title",
                DataPropertyName = "Title",
                Width = 250,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dataGridViewInventory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Category",
                HeaderText = "Category",
                DataPropertyName = "Category",
                Width = 120
            });
            dataGridViewInventory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Location",
                HeaderText = "Location",
                DataPropertyName = "Location",
                Width = 100
            });
            dataGridViewInventory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Total",
                HeaderText = "Total",
                DataPropertyName = "Total",
                Width = 80
            });
            dataGridViewInventory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Available",
                HeaderText = "Available",
                DataPropertyName = "Available",
                Width = 100
            });
            dataGridViewInventory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CheckedOut",
                HeaderText = "Borrowed",
                DataPropertyName = "CheckedOut",
                Width = 120
            });
            dataGridViewInventory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Damaged",
                HeaderText = "Damaged",
                DataPropertyName = "Damaged",
                Width = 100
            });
            dataGridViewInventory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Lost",
                HeaderText = "Lost",
                DataPropertyName = "Lost",
                Width = 80
            });
            dataGridViewInventory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "LastUpdated",
                HeaderText = "Last Updated",
                DataPropertyName = "LastUpdated",
                Width = 130
            });
            dataGridViewInventory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Status",
                DataPropertyName = "Status",
                Width = 120
            });
        }
        private void DataGridViewInventory_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Suppress DataGridView errors to prevent error dialogs
            e.ThrowException = false;
            System.Diagnostics.Debug.WriteLine($"DataGridView error at row {e.RowIndex}, column {e.ColumnIndex}: {e.Exception.Message}");
        }
        private void DataGridViewInventory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                
                string columnName = dataGridViewInventory.Columns[e.ColumnIndex].Name;
                
                // Handle null or DBNull values
                if (e.Value == null || e.Value == DBNull.Value)
                {
                    e.Value = "";
                    e.FormattingApplied = true;
                    return;
                }
                
                if (columnName == "BookID")
                {
                    string bookIdStr = e.Value.ToString();
                    if (int.TryParse(bookIdStr, out int bookId))
                    {
                        e.Value = $"B{bookId}";
                    }
                    e.FormattingApplied = true;
                }
                else if (columnName == "Available")
                {
                    // Value is already int from ConvertColumnToInt
                    // DataGridView TextBoxColumn automatically converts int to string for display
                    // We only set the color here
                    if (e.Value == null || e.Value == DBNull.Value)
                    {
                        e.Value = 0;
                    }
                    e.CellStyle.ForeColor = Color.FromArgb(40, 167, 69);
                    // Don't set FormattingApplied - let DataGridView handle the int-to-string conversion
                }
                else if (columnName == "CheckedOut")
                {
                    // Value is already int from ConvertColumnToInt
                    // DataGridView TextBoxColumn automatically converts int to string for display
                    // We only set the color here
                    if (e.Value == null || e.Value == DBNull.Value)
                    {
                        e.Value = 0;
                    }
                    e.CellStyle.ForeColor = Color.FromArgb(13, 110, 253);
                    // Don't set FormattingApplied - let DataGridView handle the int-to-string conversion
                }
                else if (columnName == "Status")
                {
                    string status = e.Value?.ToString() ?? "";
                    if (status == "Good" || status == "Available")
                    {
                        e.Value = "✓ Good";
                        e.CellStyle.ForeColor = Color.FromArgb(40, 167, 69);
                    }
                    else if (status == "Needs Attention")
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(220, 53, 69);
                    }
                    e.FormattingApplied = true;
                }
                else if (columnName == "LastUpdated")
                {
                    // Format date if it's a DateTime
                    if (e.Value is DateTime dateTime)
                    {
                        e.Value = dateTime.ToString("MM/dd/yyyy");
                        e.FormattingApplied = true;
                    }
                    else if (e.Value != null)
                    {
                        // Try to parse as DateTime
                        if (DateTime.TryParse(e.Value.ToString(), out DateTime parsedDate))
                        {
                            e.Value = parsedDate.ToString("MM/dd/yyyy");
                            e.FormattingApplied = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Suppress formatting errors to prevent dialog
                System.Diagnostics.Debug.WriteLine($"Cell formatting error: {ex.Message}");
                e.FormattingApplied = false;
            }
        }
        private void ConvertColumnToInt(DataTable table, string columnName)
        {
            if (!table.Columns.Contains(columnName))
            {
                System.Diagnostics.Debug.WriteLine($"Column {columnName} not found in DataTable");
                return;
            }
            
            try
            {
                DataColumn oldColumn = table.Columns[columnName];
                int oldColumnIndex = oldColumn.Ordinal;
                
                // Create a new column with int type
                DataColumn newColumn = new DataColumn(columnName + "_temp", typeof(int));
                newColumn.AllowDBNull = false;
                newColumn.DefaultValue = 0;
                table.Columns.Add(newColumn);
                
                // Convert all values to int
                foreach (DataRow row in table.Rows)
                {
                    try
                    {
                        object value = row[columnName];
                        if (value == null || value == DBNull.Value)
                        {
                            row[newColumn.ColumnName] = 0;
                        }
                        else
                        {
                            // Handle various numeric types
                            int intValue = 0;
                            if (value is int)
                            {
                                intValue = (int)value;
                            }
                            else if (value is long)
                            {
                                intValue = (int)(long)value;
                            }
                            else if (value is decimal)
                            {
                                intValue = (int)(decimal)value;
                            }
                            else if (value is double)
                            {
                                intValue = (int)(double)value;
                            }
                            else if (value is float)
                            {
                                intValue = (int)(float)value;
                            }
                            else if (value is short)
                            {
                                intValue = (int)(short)value;
                            }
                            else
                            {
                                // Try parsing as string
                                if (int.TryParse(value.ToString(), out int parsedValue))
                                {
                                    intValue = parsedValue;
                                }
                                else
                                {
                                    intValue = 0;
                                }
                            }
                            row[newColumn.ColumnName] = intValue;
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error converting row value for {columnName}: {ex.Message}");
                        row[newColumn.ColumnName] = 0;
                    }
                }
                
                // Remove old column and rename new one
                table.Columns.Remove(columnName);
                newColumn.ColumnName = columnName;
                newColumn.SetOrdinal(oldColumnIndex);
                
                System.Diagnostics.Debug.WriteLine($"Successfully converted column {columnName} to int");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error converting column {columnName} to int: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
            }
        }
        private void LoadCategories()
        {
            try
            {
                cmbCategoryFilter.Items.Clear();
                cmbCategoryFilter.Items.Add("All");
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT DISTINCT Category FROM Books WHERE Category IS NOT NULL AND Category != '' ORDER BY Category";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cmbCategoryFilter.Items.Add(reader.GetString("Category"));
                            }
                        }
                    }
                }
                cmbCategoryFilter.SelectedIndex = 0;
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
                    string queryTotal = "SELECT COALESCE(SUM(TotalCopies), 0) FROM Books";
                    using (MySqlCommand cmd = new MySqlCommand(queryTotal, conn))
                    {
                        int total = Convert.ToInt32(cmd.ExecuteScalar());
                        lblTotalCopiesValue.Text = total.ToString();
                    }
                    string queryAvailable = "SELECT COALESCE(SUM(Available), 0) FROM Books";
                    using (MySqlCommand cmd = new MySqlCommand(queryAvailable, conn))
                    {
                        int available = Convert.ToInt32(cmd.ExecuteScalar());
                        lblAvailableValue.Text = available.ToString();
                    }
                    int checkedOut = Convert.ToInt32(lblTotalCopiesValue.Text) - Convert.ToInt32(lblAvailableValue.Text);
                    lblCheckedOutValue.Text = checkedOut.ToString();
                    string queryDamaged = "SELECT COUNT(*) FROM Inventory WHERE `Condition` = 'Damaged'";
                    using (MySqlCommand cmd = new MySqlCommand(queryDamaged, conn))
                    {
                        int damaged = Convert.ToInt32(cmd.ExecuteScalar());
                        lblDamagedValue.Text = damaged.ToString();
                    }
                    string queryLost = "SELECT COUNT(*) FROM Inventory WHERE Status = 'Lost'";
                    using (MySqlCommand cmd = new MySqlCommand(queryLost, conn))
                    {
                        int lost = Convert.ToInt32(cmd.ExecuteScalar());
                        lblLostValue.Text = lost.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading metrics: {ex.Message}");
            }
        }
        private void LoadInventory()
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string checkTableQuery = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES
                                              WHERE TABLE_SCHEMA = DATABASE()
                                              AND TABLE_NAME = 'Inventory'";
                    bool hasInventoryTable = false;
                    using (MySqlCommand checkCmd = new MySqlCommand(checkTableQuery, conn))
                    {
                        hasInventoryTable = Convert.ToInt32(checkCmd.ExecuteScalar()) > 0;
                    }
                    bool hasCondition = false;
                    bool hasStatus = false;
                    bool hasLastVerified = false;
                    if (hasInventoryTable)
                    {
                        hasCondition = DatabaseSchemaHelper.CheckColumnExists(conn, "Inventory", "Condition");
                        hasStatus = DatabaseSchemaHelper.CheckColumnExists(conn, "Inventory", "Status");
                        hasLastVerified = DatabaseSchemaHelper.CheckColumnExists(conn, "Inventory", "LastVerified");
                    }
                    string damagedSubquery = hasInventoryTable && hasCondition
                        ? @"(SELECT BookID, COUNT(*) as DamagedCount
                            FROM Inventory
                            WHERE `Condition` = 'Damaged'
                            GROUP BY BookID)"
                        : @"(SELECT BookID, 0 as DamagedCount
                            FROM (SELECT 1) as dummy
                            WHERE 1=0)";
                    string lostSubquery = hasInventoryTable && hasStatus
                        ? @"(SELECT BookID, COUNT(*) as LostCount
                            FROM Inventory
                            WHERE Status = 'Lost'
                            GROUP BY BookID)"
                        : @"(SELECT BookID, 0 as LostCount
                            FROM (SELECT 1) as dummy
                            WHERE 1=0)";
                    string lastVerifiedSelect = hasLastVerified ? "COALESCE(MAX(i.LastVerified), COALESCE(b.CreatedDate, NOW()))" : "COALESCE(b.CreatedDate, NOW())";
                    string inventoryJoin = hasInventoryTable ? "LEFT JOIN Inventory i ON b.BookID = i.BookID" : "";
                    // Check if Transactions table exists to calculate checked out books
                    bool hasTransactionsTable = false;
                    using (MySqlCommand checkTransCmd = new MySqlCommand(@"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES
                                                                          WHERE TABLE_SCHEMA = DATABASE()
                                                                          AND TABLE_NAME = 'Transactions'", conn))
                    {
                        hasTransactionsTable = Convert.ToInt32(checkTransCmd.ExecuteScalar()) > 0;
                    }
                    
                    // Calculate checked out count from Transactions table (if exists)
                    string checkedOutSubquery = hasTransactionsTable
                        ? @"(SELECT BookID, COUNT(*) as CheckedOutCount
                            FROM Transactions
                            WHERE ReturnDate IS NULL 
                            AND (Status = 'Borrowed' OR Status IS NULL)
                            GROUP BY BookID)"
                        : @"(SELECT BookID, 0 as CheckedOutCount
                            FROM (SELECT 1) as dummy
                            WHERE 1=0)";
                    
                    // Check if Books table has Available column (prefer using it directly)
                    bool hasAvailableColumn = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "Available");
                    
                    // Use b.Available directly if it exists, otherwise calculate it
                    string availableSelect = hasAvailableColumn
                        ? "CAST(COALESCE(b.Available, 0) AS UNSIGNED) as Available"
                        : "CAST(GREATEST(0, COALESCE(b.TotalCopies, 0) - COALESCE(checkedOut.CheckedOutCount, 0) - COALESCE(damaged.DamagedCount, 0) - COALESCE(lost.LostCount, 0)) AS UNSIGNED) as Available";
                    
                    string query = $@"SELECT
                                    b.BookID,
                                    b.Title,
                                    b.Category,
                                    COALESCE(b.Location, 'N/A') as Location,
                                    CAST(COALESCE(b.TotalCopies, 0) AS UNSIGNED) as Total,
                                    CAST(COALESCE(checkedOut.CheckedOutCount, 0) AS UNSIGNED) as CheckedOut,
                                    {availableSelect},
                                    CAST(COALESCE(damaged.DamagedCount, 0) AS UNSIGNED) as Damaged,
                                    CAST(COALESCE(lost.LostCount, 0) AS UNSIGNED) as Lost,
                                    {lastVerifiedSelect} as LastUpdated,
                                    CASE
                                        WHEN COALESCE(damaged.DamagedCount, 0) = 0 AND COALESCE(lost.LostCount, 0) = 0 THEN 'Good'
                                        ELSE 'Needs Attention'
                                    END as Status
                                    FROM Books b
                                    LEFT JOIN {checkedOutSubquery} checkedOut ON b.BookID = checkedOut.BookID
                                    LEFT JOIN {damagedSubquery} damaged ON b.BookID = damaged.BookID
                                    LEFT JOIN {lostSubquery} lost ON b.BookID = lost.BookID
                                    {inventoryJoin}";
                    if (currentCategoryFilter != "All")
                    {
                        query += " WHERE b.Category = @Category";
                    }
                    query += " GROUP BY b.BookID, b.Title, b.Category, b.Location, b.TotalCopies, checkedOut.CheckedOutCount, damaged.DamagedCount, lost.LostCount, b.CreatedDate";
                    query += " ORDER BY b.BookID";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (currentCategoryFilter != "All")
                        {
                            cmd.Parameters.AddWithValue("@Category", currentCategoryFilter);
                        }
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            inventoryData = new DataTable();
                            adapter.Fill(inventoryData);
                            
                            // Ensure numeric columns are properly typed to prevent DataGridView formatting errors
                            // Convert columns BEFORE binding to DataGridView
                            if (inventoryData.Columns.Contains("Available"))
                            {
                                ConvertColumnToInt(inventoryData, "Available");
                            }
                            if (inventoryData.Columns.Contains("CheckedOut"))
                            {
                                ConvertColumnToInt(inventoryData, "CheckedOut");
                            }
                            if (inventoryData.Columns.Contains("Total"))
                            {
                                ConvertColumnToInt(inventoryData, "Total");
                            }
                            if (inventoryData.Columns.Contains("Damaged"))
                            {
                                ConvertColumnToInt(inventoryData, "Damaged");
                            }
                            if (inventoryData.Columns.Contains("Lost"))
                            {
                                ConvertColumnToInt(inventoryData, "Lost");
                            }
                            
                            // Debug: Log column types and sample values
                            System.Diagnostics.Debug.WriteLine($"Available column exists: {inventoryData.Columns.Contains("Available")}");
                            System.Diagnostics.Debug.WriteLine($"CheckedOut column exists: {inventoryData.Columns.Contains("CheckedOut")}");
                            if (inventoryData.Columns.Contains("Available"))
                            {
                                System.Diagnostics.Debug.WriteLine($"Available column type: {inventoryData.Columns["Available"].DataType.Name}");
                                if (inventoryData.Rows.Count > 0)
                                {
                                    System.Diagnostics.Debug.WriteLine($"First Available value: {inventoryData.Rows[0]["Available"]} (type: {inventoryData.Rows[0]["Available"]?.GetType().Name ?? "null"})");
                                }
                            }
                            if (inventoryData.Columns.Contains("CheckedOut"))
                            {
                                System.Diagnostics.Debug.WriteLine($"CheckedOut column type: {inventoryData.Columns["CheckedOut"].DataType.Name}");
                                if (inventoryData.Rows.Count > 0)
                                {
                                    System.Diagnostics.Debug.WriteLine($"First CheckedOut value: {inventoryData.Rows[0]["CheckedOut"]} (type: {inventoryData.Rows[0]["CheckedOut"]?.GetType().Name ?? "null"})");
                                }
                            }
                        }
                    }
                }
                
                // Temporarily disable CellFormatting to prevent errors during binding
                dataGridViewInventory.CellFormatting -= DataGridViewInventory_CellFormatting;
                dataGridViewInventory.DataSource = inventoryData;
                // Re-enable CellFormatting after binding
                dataGridViewInventory.CellFormatting += DataGridViewInventory_CellFormatting;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading inventory: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Error loading inventory: {ex.Message}");
            }
        }
        private void cmbCategoryFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCategoryFilter.SelectedItem != null)
            {
                currentCategoryFilter = cmbCategoryFilter.SelectedItem.ToString();
                LoadInventory();
            }
        }
        private void txtUpdateStockBookID_Enter(object sender, EventArgs e)
        {
            if (txtUpdateStockBookID.Text == "Book ID or Title")
            {
                txtUpdateStockBookID.Text = "";
                txtUpdateStockBookID.ForeColor = Color.Black;
            }
        }
        private void txtUpdateStockBookID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUpdateStockBookID.Text))
            {
                txtUpdateStockBookID.Text = "Book ID or Title";
                txtUpdateStockBookID.ForeColor = Color.Gray;
            }
        }
        private void txtUpdateStockQuantity_Enter(object sender, EventArgs e)
        {
            if (txtUpdateStockQuantity.Text == "Quantity")
            {
                txtUpdateStockQuantity.Text = "";
                txtUpdateStockQuantity.ForeColor = Color.Black;
            }
        }
        private void txtUpdateStockQuantity_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUpdateStockQuantity.Text))
            {
                txtUpdateStockQuantity.Text = "Quantity";
                txtUpdateStockQuantity.ForeColor = Color.Gray;
            }
        }
        private void txtUpdateStockQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void btnAddCopies_Click(object sender, EventArgs e)
        {
            string bookIdText = txtUpdateStockBookID.Text.Trim();
            string quantityText = txtUpdateStockQuantity.Text.Trim();
            if (bookIdText == "Book ID or Title" || string.IsNullOrWhiteSpace(bookIdText))
            {
                MessageBox.Show("Please enter a Book ID or Book Title.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (quantityText == "Quantity" || string.IsNullOrWhiteSpace(quantityText))
            {
                MessageBox.Show("Please enter a Quantity.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(quantityText, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity (greater than 0).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                int bookId = 0;
                var bookService = ServiceFactory.CreateBookService();
                Book book = null;
                
                // Check if input looks like an ID
                bool looksLikeId = (bookIdText.Length <= 10 && int.TryParse(bookIdText, out int parsedId)) || 
                                   (bookIdText.StartsWith("ACC-", StringComparison.OrdinalIgnoreCase) && bookIdText.Length <= 20) ||
                                   (bookIdText.StartsWith("B", StringComparison.OrdinalIgnoreCase) && bookIdText.Length <= 10 && int.TryParse(bookIdText.Substring(1), out _));
                
                // If it looks like an ID, try ID-based searches first
                if (looksLikeId)
                {
                    book = bookService.GetBookByAccessionNumber(bookIdText);
                    if (book != null)
                    {
                        bookId = book.BookID;
                    }
                    else
                    {
                        bookId = Project5LMS.Helpers.IDFormatter.ParseBookID(bookIdText);
                        if (bookId > 0)
                        {
                            var bookById = bookService.GetBook(bookId);
                            if (bookById != null)
                            {
                                book = bookById;
                            }
                            else
                            {
                                bookId = 0;
                            }
                        }
                    }
                }
                
                // If not found by ID (or doesn't look like an ID), search by Title
                if (bookId == 0 || book == null)
                {
                    var searchResults = bookService.SearchBooks(bookIdText);
                    var booksList = searchResults.ToList();
                    
                    if (booksList.Count == 0)
                    {
                        MessageBox.Show($"No book found matching '{bookIdText}'.\n\nPlease check:\n- Book ID format\n- Book Title spelling\n- Accession Number", "Book Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (booksList.Count == 1)
                    {
                        book = booksList.First();
                        bookId = book.BookID;
                    }
                    else
                    {
                        string bookList = string.Join("\n", booksList.Take(10).Select((b, idx) => 
                            $"{idx + 1}. \"{b.Title}\" by {b.Author} (ID: {b.BookID}, Accession: {b.AccessionNo ?? "N/A"})"));
                        string message = $"Multiple books found matching '{bookIdText}':\n\n{bookList}";
                        if (booksList.Count > 10)
                        {
                            message += $"\n\n... and {booksList.Count - 10} more. Please enter a more specific Book ID or Title.";
                        }
                        else
                        {
                            message += "\n\nPlease enter a more specific Book ID or Title, or use the Book ID from the list above.";
                        }
                        MessageBox.Show(message, "Multiple Books Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                
                if (bookId == 0 || book == null)
                {
                    MessageBox.Show("Invalid Book ID or Title.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string checkQuery = "SELECT COUNT(*) FROM Books WHERE BookID = @BookID";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@BookID", bookId);
                        int exists = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (exists == 0)
                        {
                            MessageBox.Show("Book not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    string getBookQuery = "SELECT TotalCopies, Location FROM Books WHERE BookID = @BookID";
                    int currentCopies = 0;
                    string location = "";
                    using (MySqlCommand getCmd = new MySqlCommand(getBookQuery, conn))
                    {
                        getCmd.Parameters.AddWithValue("@BookID", bookId);
                        using (MySqlDataReader reader = getCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                currentCopies = reader["TotalCopies"] != DBNull.Value ? Convert.ToInt32(reader["TotalCopies"]) : 0;
                                location = reader["Location"] != DBNull.Value ? reader["Location"].ToString() : "";
                            }
                        }
                    }
                    for (int i = 1; i <= quantity; i++)
                    {
                        string insertQuery = @"INSERT INTO Inventory (BookID, CopyNumber, Location, `Condition`, Status, LastVerified)
                                              VALUES (@BookID, @CopyNumber, @Location, 'Good', 'Available', @LastVerified)";
                        using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn))
                        {
                            insertCmd.Parameters.AddWithValue("@BookID", bookId);
                            insertCmd.Parameters.AddWithValue("@CopyNumber", currentCopies + i);
                            insertCmd.Parameters.AddWithValue("@Location", location);
                            insertCmd.Parameters.AddWithValue("@LastVerified", DateTime.Now);
                            insertCmd.ExecuteNonQuery();
                        }
                    }
                    string updateQuery = "UPDATE Books SET TotalCopies = TotalCopies + @Quantity, Available = Available + @Quantity WHERE BookID = @BookID";
                    using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@Quantity", quantity);
                        updateCmd.Parameters.AddWithValue("@BookID", bookId);
                        updateCmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show($"{quantity} copy/copies added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUpdateStockBookID.Text = "Book ID or Title";
                txtUpdateStockBookID.ForeColor = Color.Gray;
                txtUpdateStockQuantity.Text = "Quantity";
                txtUpdateStockQuantity.ForeColor = Color.Gray;
                LoadMetrics();
                LoadInventory();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding copies: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtReportDamageBookID_Enter(object sender, EventArgs e)
        {
            if (txtReportDamageBookID.Text == "Book ID or Title")
            {
                txtReportDamageBookID.Text = "";
                txtReportDamageBookID.ForeColor = Color.Black;
            }
        }
        private void txtReportDamageBookID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtReportDamageBookID.Text))
            {
                txtReportDamageBookID.Text = "Book ID or Title";
                txtReportDamageBookID.ForeColor = Color.Gray;
            }
        }
        private void txtReportDamageDescription_Enter(object sender, EventArgs e)
        {
            if (txtReportDamageDescription.Text == "Damage description")
            {
                txtReportDamageDescription.Text = "";
                txtReportDamageDescription.ForeColor = Color.Black;
            }
        }
        private void txtReportDamageDescription_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtReportDamageDescription.Text))
            {
                txtReportDamageDescription.Text = "Damage description";
                txtReportDamageDescription.ForeColor = Color.Gray;
            }
        }
        private void btnReportDamage_Click(object sender, EventArgs e)
        {
            string bookIdText = txtReportDamageBookID.Text.Trim();
            string description = txtReportDamageDescription.Text.Trim();
            if (bookIdText == "Book ID or Title" || string.IsNullOrWhiteSpace(bookIdText))
            {
                MessageBox.Show("Please enter a Book ID or Book Title.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (description == "Damage description" || string.IsNullOrWhiteSpace(description))
            {
                MessageBox.Show("Please enter a damage description.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                int bookId = 0;
                var bookService = ServiceFactory.CreateBookService();
                Book book = null;
                
                // Check if input looks like an ID
                bool looksLikeId = (bookIdText.Length <= 10 && int.TryParse(bookIdText, out int parsedId)) || 
                                   (bookIdText.StartsWith("ACC-", StringComparison.OrdinalIgnoreCase) && bookIdText.Length <= 20) ||
                                   (bookIdText.StartsWith("B", StringComparison.OrdinalIgnoreCase) && bookIdText.Length <= 10 && int.TryParse(bookIdText.Substring(1), out _));
                
                // If it looks like an ID, try ID-based searches first
                if (looksLikeId)
                {
                    book = bookService.GetBookByAccessionNumber(bookIdText);
                    if (book != null)
                    {
                        bookId = book.BookID;
                    }
                    else
                    {
                        bookId = Project5LMS.Helpers.IDFormatter.ParseBookID(bookIdText);
                        if (bookId > 0)
                        {
                            var bookById = bookService.GetBook(bookId);
                            if (bookById != null)
                            {
                                book = bookById;
                            }
                            else
                            {
                                bookId = 0;
                            }
                        }
                    }
                }
                
                // If not found by ID (or doesn't look like an ID), search by Title
                if (bookId == 0 || book == null)
                {
                    var searchResults = bookService.SearchBooks(bookIdText);
                    var booksList = searchResults.ToList();
                    
                    if (booksList.Count == 0)
                    {
                        MessageBox.Show($"No book found matching '{bookIdText}'.\n\nPlease check:\n- Book ID format\n- Book Title spelling\n- Accession Number", "Book Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (booksList.Count == 1)
                    {
                        book = booksList.First();
                        bookId = book.BookID;
                    }
                    else
                    {
                        string bookList = string.Join("\n", booksList.Take(10).Select((b, idx) => 
                            $"{idx + 1}. \"{b.Title}\" by {b.Author} (ID: {b.BookID}, Accession: {b.AccessionNo ?? "N/A"})"));
                        string message = $"Multiple books found matching '{bookIdText}':\n\n{bookList}";
                        if (booksList.Count > 10)
                        {
                            message += $"\n\n... and {booksList.Count - 10} more. Please enter a more specific Book ID or Title.";
                        }
                        else
                        {
                            message += "\n\nPlease enter a more specific Book ID or Title, or use the Book ID from the list above.";
                        }
                        MessageBox.Show(message, "Multiple Books Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                
                if (bookId == 0 || book == null)
                {
                    MessageBox.Show("Invalid Book ID or Title.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string findQuery = @"SELECT InventoryID FROM Inventory
                                        WHERE BookID = @BookID
                                        AND Status = 'Available'
                                        AND `Condition` = 'Good'
                                        LIMIT 1";
                    int inventoryId = 0;
                    using (MySqlCommand findCmd = new MySqlCommand(findQuery, conn))
                    {
                        findCmd.Parameters.AddWithValue("@BookID", bookId);
                        object result = findCmd.ExecuteScalar();
                        if (result != null)
                        {
                            inventoryId = Convert.ToInt32(result);
                        }
                    }
                    if (inventoryId == 0)
                    {
                        MessageBox.Show("No available copy found for this book.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    string updateQuery = @"UPDATE Inventory
                                          SET `Condition` = 'Damaged',
                                              Status = 'For Repair',
                                              Notes = @Notes,
                                              LastVerified = @LastVerified
                                          WHERE InventoryID = @InventoryID";
                    using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@Notes", description);
                        updateCmd.Parameters.AddWithValue("@LastVerified", DateTime.Now);
                        updateCmd.Parameters.AddWithValue("@InventoryID", inventoryId);
                        updateCmd.ExecuteNonQuery();
                    }
                    string updateBookQuery = "UPDATE Books SET Available = Available - 1 WHERE BookID = @BookID AND Available > 0";
                    using (MySqlCommand updateBookCmd = new MySqlCommand(updateBookQuery, conn))
                    {
                        updateBookCmd.Parameters.AddWithValue("@BookID", bookId);
                        updateBookCmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Damage reported successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtReportDamageBookID.Text = "Book ID or Title";
                txtReportDamageBookID.ForeColor = Color.Gray;
                txtReportDamageDescription.Text = "Damage description";
                txtReportDamageDescription.ForeColor = Color.Gray;
                LoadMetrics();
                LoadInventory();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reporting damage: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtReportLostBookID_Enter(object sender, EventArgs e)
        {
            if (txtReportLostBookID.Text == "Book ID or Title")
            {
                txtReportLostBookID.Text = "";
                txtReportLostBookID.ForeColor = Color.Black;
            }
        }
        private void txtReportLostBookID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtReportLostBookID.Text))
            {
                txtReportLostBookID.Text = "Book ID or Title";
                txtReportLostBookID.ForeColor = Color.Gray;
            }
        }
        private void txtReportLostNotes_Enter(object sender, EventArgs e)
        {
            if (txtReportLostNotes.Text == "Additional notes")
            {
                txtReportLostNotes.Text = "";
                txtReportLostNotes.ForeColor = Color.Black;
            }
        }
        private void txtReportLostNotes_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtReportLostNotes.Text))
            {
                txtReportLostNotes.Text = "Additional notes";
                txtReportLostNotes.ForeColor = Color.Gray;
            }
        }
        private void btnReportLost_Click(object sender, EventArgs e)
        {
            // Staff role restriction: Only Admin can mark books as lost
            if (CurrentUser.Role == null || !CurrentUser.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Only administrators can mark books as lost.", "Access Denied", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                AuditLogger.LogAccessControl("Report Lost Book attempt denied", 
                    $"User: {CurrentUser.Email}, Role: {CurrentUser.Role}", "Failed");
                return;
            }
            
            string bookIdText = txtReportLostBookID.Text.Trim();
            string notes = txtReportLostNotes.Text.Trim();
            if (bookIdText == "Book ID or Title" || string.IsNullOrWhiteSpace(bookIdText))
            {
                MessageBox.Show("Please enter a Book ID or Book Title.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (notes == "Additional notes" || string.IsNullOrWhiteSpace(notes))
            {
                notes = "Reported as lost";
            }
            try
            {
                int bookId = 0;
                var bookService = ServiceFactory.CreateBookService();
                Book book = null;
                
                // Check if input looks like an ID
                bool looksLikeId = (bookIdText.Length <= 10 && int.TryParse(bookIdText, out int parsedId)) || 
                                   (bookIdText.StartsWith("ACC-", StringComparison.OrdinalIgnoreCase) && bookIdText.Length <= 20) ||
                                   (bookIdText.StartsWith("B", StringComparison.OrdinalIgnoreCase) && bookIdText.Length <= 10 && int.TryParse(bookIdText.Substring(1), out _));
                
                // If it looks like an ID, try ID-based searches first
                if (looksLikeId)
                {
                    book = bookService.GetBookByAccessionNumber(bookIdText);
                    if (book != null)
                    {
                        bookId = book.BookID;
                    }
                    else
                    {
                        bookId = Project5LMS.Helpers.IDFormatter.ParseBookID(bookIdText);
                        if (bookId > 0)
                        {
                            var bookById = bookService.GetBook(bookId);
                            if (bookById != null)
                            {
                                book = bookById;
                            }
                            else
                            {
                                bookId = 0;
                            }
                        }
                    }
                }
                
                // If not found by ID (or doesn't look like an ID), search by Title
                if (bookId == 0 || book == null)
                {
                    var searchResults = bookService.SearchBooks(bookIdText);
                    var booksList = searchResults.ToList();
                    
                    if (booksList.Count == 0)
                    {
                        MessageBox.Show($"No book found matching '{bookIdText}'.\n\nPlease check:\n- Book ID format\n- Book Title spelling\n- Accession Number", "Book Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (booksList.Count == 1)
                    {
                        book = booksList.First();
                        bookId = book.BookID;
                    }
                    else
                    {
                        string bookList = string.Join("\n", booksList.Take(10).Select((b, idx) => 
                            $"{idx + 1}. \"{b.Title}\" by {b.Author} (ID: {b.BookID}, Accession: {b.AccessionNo ?? "N/A"})"));
                        string message = $"Multiple books found matching '{bookIdText}':\n\n{bookList}";
                        if (booksList.Count > 10)
                        {
                            message += $"\n\n... and {booksList.Count - 10} more. Please enter a more specific Book ID or Title.";
                        }
                        else
                        {
                            message += "\n\nPlease enter a more specific Book ID or Title, or use the Book ID from the list above.";
                        }
                        MessageBox.Show(message, "Multiple Books Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                
                if (bookId == 0 || book == null)
                {
                    MessageBox.Show("Invalid Book ID or Title.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string findQuery = @"SELECT InventoryID FROM Inventory
                                        WHERE BookID = @BookID
                                        AND Status = 'Available'
                                        AND `Condition` = 'Good'
                                        LIMIT 1";
                    int inventoryId = 0;
                    using (MySqlCommand findCmd = new MySqlCommand(findQuery, conn))
                    {
                        findCmd.Parameters.AddWithValue("@BookID", bookId);
                        object result = findCmd.ExecuteScalar();
                        if (result != null)
                        {
                            inventoryId = Convert.ToInt32(result);
                        }
                    }
                    if (inventoryId == 0)
                    {
                        MessageBox.Show("No available copy found for this book.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    string updateQuery = @"UPDATE Inventory
                                          SET Status = 'Lost',
                                              Notes = @Notes,
                                              LastVerified = @LastVerified
                                          WHERE InventoryID = @InventoryID";
                    using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@Notes", notes);
                        updateCmd.Parameters.AddWithValue("@LastVerified", DateTime.Now);
                        updateCmd.Parameters.AddWithValue("@InventoryID", inventoryId);
                        updateCmd.ExecuteNonQuery();
                    }
                    string updateBookQuery = @"UPDATE Books
                                               SET Available = Available - 1,
                                                   TotalCopies = TotalCopies - 1
                                               WHERE BookID = @BookID AND Available > 0 AND TotalCopies > 0";
                    using (MySqlCommand updateBookCmd = new MySqlCommand(updateBookQuery, conn))
                    {
                        updateBookCmd.Parameters.AddWithValue("@BookID", bookId);
                        updateBookCmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Lost book reported successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtReportLostBookID.Text = "Book ID or Title";
                txtReportLostBookID.ForeColor = Color.Gray;
                txtReportLostNotes.Text = "Additional notes";
                txtReportLostNotes.ForeColor = Color.Gray;
                LoadMetrics();
                LoadInventory();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reporting lost book: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAddInventory_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Add Inventory feature - This would open a dialog to add new inventory items.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void DrawBoxIcon(Graphics g, Panel panel)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int size = Math.Min(panel.Width, panel.Height) - 10;
            int x = (panel.Width - size) / 2;
            int y = (panel.Height - size) / 2;
            using (Pen pen = new Pen(Color.White, 3))
            {
                g.DrawRectangle(pen, x + size * 0.2f, y + size * 0.2f, size * 0.6f, size * 0.6f);
            }
        }
        private void DrawUpArrowIcon(Graphics g, Panel panel)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int size = Math.Min(panel.Width, panel.Height) - 10;
            int x = (panel.Width - size) / 2;
            int y = (panel.Height - size) / 2;
            using (Pen pen = new Pen(Color.White, 3))
            {
                g.DrawLine(pen, x + size * 0.5f, y + size * 0.7f, x + size * 0.5f, y + size * 0.3f);
                g.DrawLine(pen, x + size * 0.3f, y + size * 0.5f, x + size * 0.5f, y + size * 0.3f);
                g.DrawLine(pen, x + size * 0.7f, y + size * 0.5f, x + size * 0.5f, y + size * 0.3f);
            }
        }
        private void DrawWavyArrowIcon(Graphics g, Panel panel)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int size = Math.Min(panel.Width, panel.Height) - 10;
            int x = (panel.Width - size) / 2;
            int y = (panel.Height - size) / 2;
            using (Pen pen = new Pen(Color.White, 3))
            {
                PointF[] points = new PointF[]
                {
                    new PointF(x + size * 0.2f, y + size * 0.5f),
                    new PointF(x + size * 0.4f, y + size * 0.3f),
                    new PointF(x + size * 0.6f, y + size * 0.7f),
                    new PointF(x + size * 0.8f, y + size * 0.4f)
                };
                g.DrawLines(pen, points);
            }
        }
        private void DrawWarningIcon(Graphics g, Panel panel)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int size = Math.Min(panel.Width, panel.Height) - 10;
            int x = (panel.Width - size) / 2;
            int y = (panel.Height - size) / 2;
            using (Pen pen = new Pen(Color.White, 3))
            {
                PointF[] points = new PointF[]
                {
                    new PointF(x + size * 0.5f, y + size * 0.2f),
                    new PointF(x + size * 0.2f, y + size * 0.8f),
                    new PointF(x + size * 0.8f, y + size * 0.8f)
                };
                g.DrawPolygon(pen, points);
                g.DrawLine(pen, x + size * 0.5f, y + size * 0.4f, x + size * 0.5f, y + size * 0.6f);
                g.DrawLine(pen, x + size * 0.5f, y + size * 0.7f, x + size * 0.5f, y + size * 0.75f);
            }
        }
        private void lblSubtitle_Click(object sender, EventArgs e)
        {
        }
        private void lblTotalCopiesValue_Click(object sender, EventArgs e)
        {
        }
        private void panelMainContainer_Paint(object sender, PaintEventArgs e)
        {
        }

        private void cardReportLost_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtReportDamageBookID_TextChanged(object sender, EventArgs e)
        {

        }
    }
}