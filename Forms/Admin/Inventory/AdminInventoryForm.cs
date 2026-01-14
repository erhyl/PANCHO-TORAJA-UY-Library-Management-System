using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Project5LMS.Helpers;
using Project5LMS.Data;
using Project5LMS.Services;
namespace Project5LMS.Forms.Admin.Inventory
{
    public partial class AdminInventoryForm : Form
    {
        private DataTable allInventoryData;
        private string currentConditionFilter = "All Conditions";
        private string currentStatusFilter = "All Status";
        private readonly DatabaseContext _dbContext;
        public AdminInventoryForm()
        {
            InitializeComponent();
            _dbContext = ServiceFactory.GetDbContext();
        }
        private void AdminInventoryForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            try
            {
                EnsureInventoryTableExists();
                SetupDataGridView();
                LoadMetrics();
                LoadInventory();
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError($"Error loading inventory form: {ex.Message}", "Error", ex);
                System.Diagnostics.Debug.WriteLine($"Inventory form load error: {ex}");
            }
        }
        private void EnsureInventoryTableExists()
        {
            try
            {
                var dbContext = ServiceFactory.GetDbContext();
                string createTableQuery = @"CREATE TABLE IF NOT EXISTS Inventory (
                                            InventoryID INT AUTO_INCREMENT PRIMARY KEY,
                                            BookID INT NOT NULL,
                                            CopyNumber INT NOT NULL,
                                            Location VARCHAR(50) NULL,
                                            `Condition` VARCHAR(50) DEFAULT 'Good',
                                            Status VARCHAR(50) DEFAULT 'Available',
                                            LastVerified DATETIME NULL,
                                            Notes VARCHAR(255) NULL,
                                            FOREIGN KEY (BookID) REFERENCES Books(BookID)
                                            )";
                
                bool wasCreated = TableCreationHelper.EnsureTableExists(dbContext, "Inventory", createTableQuery, conn =>
                {
                    try
                    {
                        PopulateInventoryFromBooks(conn);
                    }
                    catch (Exception populateEx)
                    {
                        System.Diagnostics.Debug.WriteLine($"Warning: Could not populate inventory from books: {populateEx.Message}");
                    }
                });

                if (wasCreated)
                {
                    // Table was just created, inventory should be populated
                }
                else
                {
                    // Table already exists, ensure columns exist
                    using (var conn = dbContext.GetConnection())
                    {
                        conn.Open();
                        DatabaseSchemaHelper.AddColumnIfNotExists(conn, "Inventory", "CopyNumber", "INT NOT NULL DEFAULT 1");
                        DatabaseSchemaHelper.AddColumnIfNotExists(conn, "Inventory", "LastVerified", "DATETIME NULL");
                        DatabaseSchemaHelper.AddColumnIfNotExists(conn, "Inventory", "Notes", "VARCHAR(255) NULL");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error ensuring Inventory table exists: {ex.Message}");
            }
        }
        private void PopulateInventoryFromBooks(MySqlConnection conn)
        {
            try
            {
                bool hasCopies = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "Copies");
                string copiesColumn = hasCopies ? "Copies" : "TotalCopies";
                string query = $"SELECT BookID, {copiesColumn} as Copies, Location FROM Books";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    List<Tuple<int, int, string>> books = new List<Tuple<int, int, string>>();
                    while (reader.Read())
                    {
                        int bookId = Convert.ToInt32(reader["BookID"]);
                        int copies = reader["Copies"] != DBNull.Value ? Convert.ToInt32(reader["Copies"]) : 1;
                        string location = reader["Location"] != DBNull.Value ? reader["Location"].ToString() : "";
                        books.Add(new Tuple<int, int, string>(bookId, copies, location));
                    }
                    reader.Close();
                    foreach (var book in books)
                    {
                        for (int i = 1; i <= book.Item2; i++)
                        {
                            string insertQuery = @"INSERT INTO Inventory (BookID, CopyNumber, Location, `Condition`, Status, LastVerified)
                                                  VALUES (@BookID, @CopyNumber, @Location, 'Good', 'Available', @LastVerified)";
                            using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn))
                            {
                                insertCmd.Parameters.AddWithValue("@BookID", book.Item1);
                                insertCmd.Parameters.AddWithValue("@CopyNumber", i);
                                insertCmd.Parameters.AddWithValue("@Location", book.Item3);
                                insertCmd.Parameters.AddWithValue("@LastVerified", DateTime.Now);
                                insertCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error populating inventory: {ex.Message}");
            }
        }
        private void AddColumnIfNotExists(MySqlConnection conn, string tableName, string columnName, string columnDefinition)
        {
            try
            {
                string checkColumnQuery = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS
                                          WHERE TABLE_SCHEMA = DATABASE()
                                          AND TABLE_NAME = @tableName
                                          AND COLUMN_NAME = @columnName";
                using (MySqlCommand checkCmd = new MySqlCommand(checkColumnQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@tableName", tableName);
                    checkCmd.Parameters.AddWithValue("@columnName", columnName);
                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                    if (count == 0)
                    {
                        string alterQuery = $"ALTER TABLE {tableName} ADD COLUMN {columnName} {columnDefinition}";
                        using (MySqlCommand alterCmd = new MySqlCommand(alterQuery, conn))
                        {
                            alterCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error adding column {columnName}: {ex.Message}");
            }
        }
        private void DrawMetricIcon(Graphics g, Panel panel, string icon)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (Font font = new Font("Segoe UI", 18, FontStyle.Bold))
            using (Brush brush = new SolidBrush(Color.White))
            {
                SizeF textSize = g.MeasureString(icon, font);
                float x = (panel.Width - textSize.Width) / 2;
                float y = (panel.Height - textSize.Height) / 2;
                g.DrawString(icon, font, brush, x, y);
            }
        }
        private void SetupDataGridView()
        {
            dataGridViewInventory.Columns.Clear();
            dataGridViewInventory.AutoGenerateColumns = false;
            
            // Inventory ID - Fixed width, small column
            DataGridViewTextBoxColumn colInventoryID = new DataGridViewTextBoxColumn
            {
                Name = "InventoryID",
                HeaderText = "INVENTORY ID",
                DataPropertyName = "InventoryID",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                MinimumWidth = 120,
                ReadOnly = true
            };
            dataGridViewInventory.Columns.Add(colInventoryID);
            
            // Book Details - Fill mode with wrapping for long content
            DataGridViewTextBoxColumn colBookDetails = new DataGridViewTextBoxColumn
            {
                Name = "BookDetails",
                HeaderText = "BOOK DETAILS",
                DataPropertyName = "BookDetails",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                MinimumWidth = 250,
                ReadOnly = true
            };
            colBookDetails.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridViewInventory.Columns.Add(colBookDetails);
            
            // Category - Auto-size based on content
            DataGridViewTextBoxColumn colCategory = new DataGridViewTextBoxColumn
            {
                Name = "Category",
                HeaderText = "CATEGORY",
                DataPropertyName = "Category",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                MinimumWidth = 120,
                ReadOnly = true
            };
            dataGridViewInventory.Columns.Add(colCategory);
            
            // Location - Auto-size based on content
            DataGridViewTextBoxColumn colLocation = new DataGridViewTextBoxColumn
            {
                Name = "Location",
                HeaderText = "LOCATION",
                DataPropertyName = "Location",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                MinimumWidth = 100,
                ReadOnly = true
            };
            dataGridViewInventory.Columns.Add(colLocation);
            
            // Copy - Fixed small width
            DataGridViewTextBoxColumn colCopy = new DataGridViewTextBoxColumn
            {
                Name = "Copy",
                HeaderText = "COPY",
                DataPropertyName = "Copy",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                MinimumWidth = 80,
                ReadOnly = true
            };
            dataGridViewInventory.Columns.Add(colCopy);
            
            // Condition - Auto-size for badge display
            DataGridViewColumn colCondition = new DataGridViewTextBoxColumn
            {
                Name = "Condition",
                HeaderText = "CONDITION",
                DataPropertyName = "Condition",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                MinimumWidth = 120,
                ReadOnly = true
            };
            dataGridViewInventory.Columns.Add(colCondition);
            
            // Status - Auto-size for badge display
            DataGridViewColumn colStatus = new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "STATUS",
                DataPropertyName = "Status",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                MinimumWidth = 120,
                ReadOnly = true
            };
            dataGridViewInventory.Columns.Add(colStatus);
            
            // Last Verified - Auto-size based on date format
            DataGridViewTextBoxColumn colLastVerified = new DataGridViewTextBoxColumn
            {
                Name = "LastVerified",
                HeaderText = "LAST VERIFIED",
                DataPropertyName = "LastVerified",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                MinimumWidth = 130,
                ReadOnly = true
            };
            dataGridViewInventory.Columns.Add(colLastVerified);
            
            // Actions - Fixed width for buttons
            DataGridViewColumn colActions = new DataGridViewTextBoxColumn
            {
                Name = "Actions",
                HeaderText = "ACTIONS",
                DataPropertyName = "Actions",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = 180,
                MinimumWidth = 180,
                ReadOnly = true
            };
            dataGridViewInventory.Columns.Add(colActions);
            
            // Configure DataGridView appearance
            dataGridViewInventory.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridViewInventory.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dataGridViewInventory.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridViewInventory.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dataGridViewInventory.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            dataGridViewInventory.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            dataGridViewInventory.RowTemplate.Height = 60;
            dataGridViewInventory.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewInventory.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            
            // Enable text wrapping for BookDetails column specifically
            colBookDetails.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            
            dataGridViewInventory.CellFormatting += DataGridViewInventory_CellFormatting;
            dataGridViewInventory.CellPainting += DataGridViewInventory_CellPainting;
            dataGridViewInventory.CellContentClick += DataGridViewInventory_CellContentClick;
            dataGridViewInventory.CellClick += DataGridViewInventory_CellClick;
            dataGridViewInventory.DataError += DataGridViewInventory_DataError;
        }
        
        private void DataGridViewInventory_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Suppress DataGridView errors to prevent error dialogs
            e.ThrowException = false;
            System.Diagnostics.Debug.WriteLine($"DataGridView error in row {e.RowIndex}, column {e.ColumnIndex}: {e.Exception.Message}");
        }
        private void DataGridViewInventory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                
                string columnName = dataGridViewInventory.Columns[e.ColumnIndex].Name;
                
                if (columnName == "InventoryID")
                {
                    if (e.Value != null && e.Value != DBNull.Value)
                    {
                        // Handle different types that might come from the database
                        string inventoryIdStr = "";
                        if (e.Value is int)
                        {
                            inventoryIdStr = ((int)e.Value).ToString();
                        }
                        else if (e.Value is long)
                        {
                            inventoryIdStr = ((long)e.Value).ToString();
                        }
                        else
                        {
                            inventoryIdStr = e.Value.ToString();
                        }
                        
                        if (int.TryParse(inventoryIdStr, out int inventoryId))
                        {
                            e.Value = $"INV-{inventoryIdStr.PadLeft(3, '0')}";
                        }
                        else
                        {
                            e.Value = inventoryIdStr; // Keep original if can't parse
                        }
                    }
                    else
                    {
                        e.Value = "N/A";
                    }
                    e.FormattingApplied = true;
                }
                else if (columnName == "LastVerified")
                {
                    if (e.Value != null && e.Value != DBNull.Value && !string.IsNullOrEmpty(e.Value.ToString()))
                    {
                        DateTime date;
                        if (e.Value is DateTime)
                        {
                            date = (DateTime)e.Value;
                            e.Value = date.ToString("yyyy-MM-dd");
                        }
                        else if (DateTime.TryParse(e.Value.ToString(), out date))
                        {
                            e.Value = date.ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            e.Value = "N/A";
                        }
                    }
                    else
                    {
                        e.Value = "N/A";
                    }
                    e.FormattingApplied = true;
                }
                else if (columnName == "Copy")
                {
                    // Ensure Copy column is always a string
                    if (e.Value != null && e.Value != DBNull.Value)
                    {
                        e.Value = e.Value.ToString();
                    }
                    else
                    {
                        e.Value = "N/A";
                    }
                    e.FormattingApplied = true;
                }
                else if (columnName == "BookDetails" || columnName == "Category" || columnName == "Location" || columnName == "Condition" || columnName == "Status" || columnName == "Actions")
                {
                    // Ensure these text columns are always strings
                    if (e.Value != null && e.Value != DBNull.Value)
                    {
                        e.Value = e.Value.ToString();
                    }
                    else
                    {
                        e.Value = "";
                    }
                    e.FormattingApplied = true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error formatting cell [{e.RowIndex}, {e.ColumnIndex}]: {ex.Message}");
                e.FormattingApplied = false;
            }
        }
        private void DataGridViewInventory_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            string columnName = dataGridViewInventory.Columns[e.ColumnIndex].Name;
            DataGridViewRow row = dataGridViewInventory.Rows[e.RowIndex];
            if (columnName == "Condition")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                string value = e.Value?.ToString() ?? "";
                Color bgColor = Color.LightGray;
                Color textColor = Color.Black;
                switch (value.ToLower())
                {
                    case "excellent":
                        bgColor = Color.FromArgb(40, 167, 69);
                        textColor = Color.White;
                        break;
                    case "good":
                        bgColor = Color.FromArgb(13, 110, 253);
                        textColor = Color.White;
                        break;
                    case "fair":
                        bgColor = Color.FromArgb(255, 193, 7);
                        textColor = Color.Black;
                        break;
                    case "damaged":
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
                    dataGridViewInventory.DefaultCellStyle.Font,
                    badgeRect,
                    textColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                );
                e.Handled = true;
            }
            if (columnName == "Status")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                string value = e.Value?.ToString() ?? "";
                Color bgColor = Color.LightGray;
                Color textColor = Color.Black;
                switch (value.ToLower())
                {
                    case "available":
                        bgColor = Color.FromArgb(212, 237, 218);
                        textColor = Color.FromArgb(25, 135, 84);
                        break;
                    case "borrowed":
                        bgColor = Color.FromArgb(207, 226, 255);
                        textColor = Color.FromArgb(13, 110, 253);
                        break;
                    case "for repair":
                        bgColor = Color.FromArgb(220, 53, 69);
                        textColor = Color.White;
                        break;
                    case "lost":
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
                    dataGridViewInventory.DefaultCellStyle.Font,
                    badgeRect,
                    textColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                );
                e.Handled = true;
            }
            if (columnName == "Actions")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                int buttonY = e.CellBounds.Y + (e.CellBounds.Height - 30) / 2;
                int buttonHeight = 30;
                int buttonWidth = 80;
                int spacing = 5;
                int xOffset = e.CellBounds.X + 5;
                Rectangle btnVerifyRect = new Rectangle(xOffset, buttonY, buttonWidth, buttonHeight);
                DrawButton(e.Graphics, btnVerifyRect, "Verify", Color.FromArgb(13, 110, 253), Color.White);
                Rectangle btnUpdateRect = new Rectangle(xOffset + buttonWidth + spacing, buttonY, buttonWidth, buttonHeight);
                DrawButton(e.Graphics, btnUpdateRect, "Update", Color.FromArgb(40, 167, 69), Color.White);
                e.Handled = true;
            }
        }
        private void DrawButton(Graphics g, Rectangle rect, string text, Color bgColor, Color textColor)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                int radius = 5;
                path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
                path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
                path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
                path.CloseAllFigures();
                using (SolidBrush brush = new SolidBrush(bgColor))
                {
                    g.FillPath(brush, path);
                }
            }
            TextRenderer.DrawText(
                g,
                text,
                new Font("Segoe UI", 9, FontStyle.Bold),
                rect,
                textColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
            );
        }
        private void DataGridViewInventory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                string columnName = dataGridViewInventory.Columns[e.ColumnIndex].Name;
                if (columnName != "Actions") return;
                
                DataGridViewRow row = dataGridViewInventory.Rows[e.RowIndex];
                int inventoryId = 0;
                
                // Try to get inventory ID from the row
                object inventoryIdObj = row.Cells["InventoryID"].Value;
                if (inventoryIdObj != null)
                {
                    string inventoryIdStr = inventoryIdObj.ToString();
                    // Remove "INV-" prefix if present
                    if (inventoryIdStr.StartsWith("INV-"))
                    {
                        inventoryIdStr = inventoryIdStr.Replace("INV-", "");
                    }
                    if (!int.TryParse(inventoryIdStr, out inventoryId))
                    {
                        System.Diagnostics.Debug.WriteLine($"Failed to parse InventoryID: {inventoryIdObj}");
                        ErrorHandler.ShowValidationError("Unable to identify inventory item.");
                        return;
                    }
                }
                else
                {
                    // Try to get from DataBoundItem
                    if (dataGridViewInventory.DataSource is DataTable dt)
                    {
                        if (e.RowIndex < dt.Rows.Count)
                        {
                            DataRow dataRow = dt.Rows[e.RowIndex];
                            if (dataRow["InventoryID"] != null && dataRow["InventoryID"] != DBNull.Value)
                            {
                                string inventoryIdStr = dataRow["InventoryID"].ToString();
                                if (inventoryIdStr.StartsWith("INV-"))
                                {
                                    inventoryIdStr = inventoryIdStr.Replace("INV-", "");
                                }
                                if (!int.TryParse(inventoryIdStr, out inventoryId))
                                {
                                    System.Diagnostics.Debug.WriteLine($"Failed to parse InventoryID from DataRow: {dataRow["InventoryID"]}");
                                    ErrorHandler.ShowValidationError("Unable to identify inventory item.");
                                    return;
                                }
                            }
                        }
                    }
                }
                
                if (inventoryId == 0)
                {
                    MessageBox.Show("Unable to identify inventory item.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                // Get mouse position relative to the DataGridView
                Point clickPoint = dataGridViewInventory.PointToClient(Cursor.Position);
                Rectangle cellRect = dataGridViewInventory.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                
                // Calculate button positions (matching CellPainting logic)
                int buttonHeight = 30;
                int buttonWidth = 80;
                int spacing = 5;
                int xOffset = 5; // Offset from left edge of cell
                int buttonY = (cellRect.Height - buttonHeight) / 2;
                
                // Calculate click position relative to the cell
                int relativeX = clickPoint.X - cellRect.X;
                int relativeY = clickPoint.Y - cellRect.Y;
                
                // Define button rectangles relative to cell
                Rectangle btnVerifyRect = new Rectangle(xOffset, buttonY, buttonWidth, buttonHeight);
                Rectangle btnUpdateRect = new Rectangle(xOffset + buttonWidth + spacing, buttonY, buttonWidth, buttonHeight);
                
                // Check which button was clicked
                if (btnVerifyRect.Contains(new Point(relativeX, relativeY)))
                {
                    VerifyInventory(inventoryId);
                }
                else if (btnUpdateRect.Contains(new Point(relativeX, relativeY)))
                {
                    UpdateInventory(inventoryId);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError($"An error occurred while processing your request: {ex.Message}", "Error", ex);
                System.Diagnostics.Debug.WriteLine($"CellContentClick error: {ex}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
            }
        }
        private void LoadMetrics()
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string queryTotal = "SELECT COUNT(*) FROM Inventory";
                    using (MySqlCommand cmd = new MySqlCommand(queryTotal, conn))
                    {
                        int total = Convert.ToInt32(cmd.ExecuteScalar());
                        lblMetricTotalValue.Text = total.ToString();
                    }
                    string queryNeedsRepair = "SELECT COUNT(*) FROM Inventory WHERE Status = 'For Repair'";
                    using (MySqlCommand cmd = new MySqlCommand(queryNeedsRepair, conn))
                    {
                        int needsRepair = Convert.ToInt32(cmd.ExecuteScalar());
                        lblMetricNeedsRepairValue.Text = needsRepair.ToString();
                    }
                    bool hasCondition = DatabaseSchemaHelper.CheckColumnExists(conn, "Inventory", "Condition");
                    string queryDamaged = hasCondition
                        ? "SELECT COUNT(*) FROM Inventory WHERE `Condition` = 'Damaged'"
                        : "SELECT 0";
                    using (MySqlCommand cmd = new MySqlCommand(queryDamaged, conn))
                    {
                        int damaged = Convert.ToInt32(cmd.ExecuteScalar());
                        lblMetricDamagedValue.Text = damaged.ToString();
                    }
                    string queryLost = "SELECT COUNT(*) FROM Inventory WHERE Status = 'Lost'";
                    using (MySqlCommand cmd = new MySqlCommand(queryLost, conn))
                    {
                        int lost = Convert.ToInt32(cmd.ExecuteScalar());
                        lblMetricLostValue.Text = lost.ToString();
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
                allInventoryData = GetInventoryData();
                
                if (allInventoryData == null)
                {
                    System.Diagnostics.Debug.WriteLine("GetInventoryData returned null");
                    allInventoryData = new DataTable();
                }
                
                if (allInventoryData.Rows.Count == 0)
                {
                    System.Diagnostics.Debug.WriteLine("No inventory items found in database");
                    // Still set the data source so the grid shows (empty)
                    dataGridViewInventory.DataSource = allInventoryData;
                    return;
                }
                
                if (!allInventoryData.Columns.Contains("BookDetails"))
                {
                    allInventoryData.Columns.Add("BookDetails", typeof(string));
                }
                if (!allInventoryData.Columns.Contains("Copy"))
                {
                    allInventoryData.Columns.Add("Copy", typeof(string));
                }
                if (!allInventoryData.Columns.Contains("Actions"))
                {
                    allInventoryData.Columns.Add("Actions", typeof(string));
                }
                
                // Convert DataTable to all strings to avoid DataGridView type mismatches
                DataTable stringDataTable = new DataTable();
                foreach (DataColumn col in allInventoryData.Columns)
                {
                    stringDataTable.Columns.Add(col.ColumnName, typeof(string));
                }
                
                // Add computed columns if they don't exist
                if (!stringDataTable.Columns.Contains("BookDetails"))
                    stringDataTable.Columns.Add("BookDetails", typeof(string));
                if (!stringDataTable.Columns.Contains("Copy"))
                    stringDataTable.Columns.Add("Copy", typeof(string));
                if (!stringDataTable.Columns.Contains("Actions"))
                    stringDataTable.Columns.Add("Actions", typeof(string));
                
                System.Diagnostics.Debug.WriteLine($"Inventory table count: {allInventoryData.Rows.Count}");
                System.Diagnostics.Debug.WriteLine($"GetInventoryData returned {allInventoryData.Rows.Count} items");
                
                foreach (DataRow row in allInventoryData.Rows)
                {
                    DataRow newRow = stringDataTable.NewRow();
                    
                    // Copy all original columns as strings
                    foreach (DataColumn col in allInventoryData.Columns)
                    {
                        if (stringDataTable.Columns.Contains(col.ColumnName))
                        {
                            object value = row[col.ColumnName];
                            if (value == null || value == DBNull.Value)
                                newRow[col.ColumnName] = "";
                            else if (value is DateTime)
                                newRow[col.ColumnName] = ((DateTime)value).ToString("yyyy-MM-dd");
                            else
                                newRow[col.ColumnName] = value.ToString();
                        }
                    }
                    
                    // Process computed values
                    string title = newRow["Title"]?.ToString() ?? "";
                    string author = newRow["Author"]?.ToString() ?? "";
                    int bookId = int.TryParse(newRow["BookID"]?.ToString(), out int bid) ? bid : 0;
                    
                    string barcode = "";
                    if (row.Table.Columns.Contains("Barcode") && row["Barcode"] != DBNull.Value)
                        barcode = row["Barcode"].ToString();
                    else if (row.Table.Columns.Contains("AccessionNo") && row["AccessionNo"] != DBNull.Value)
                        barcode = row["AccessionNo"].ToString();
                    else if (bookId > 0)
                        barcode = $"BOOK-{bookId}";
                    
                    string accessionNo = !string.IsNullOrEmpty(barcode) ? barcode : $"ACC-{bookId.ToString().PadLeft(4, '0')}";
                    string bookDetails;
                    if (!string.IsNullOrEmpty(title) && title != "N/A")
                    {
                        if (!string.IsNullOrEmpty(author) && author != "N/A")
                            bookDetails = $"{title} by {author} ({accessionNo})";
                        else
                            bookDetails = $"{title} ({accessionNo})";
                    }
                    else
                    {
                        bookDetails = $"Book ID: {bookId} ({accessionNo})";
                    }
                    newRow["BookDetails"] = bookDetails;
                    
                    int copyNumber = int.TryParse(newRow["CopyNumber"]?.ToString(), out int cn) ? cn : 1;
                    int totalCopies = int.TryParse(newRow["Copies"]?.ToString(), out int tc) ? tc : 1;
                    newRow["Copy"] = $"{copyNumber}/{totalCopies}";
                    
                    if (string.IsNullOrEmpty(newRow["Location"]?.ToString()))
                    {
                        newRow["Location"] = GenerateLocation(bookId, copyNumber);
                    }
                    
                    // Set Actions column to empty string (buttons are drawn via CellPainting)
                    newRow["Actions"] = "";
                    
                    stringDataTable.Rows.Add(newRow);
                }
                
                System.Diagnostics.Debug.WriteLine($"After conversion: DataTable has {stringDataTable.Rows.Count} rows, {stringDataTable.Columns.Count} columns");
                allInventoryData = stringDataTable;
                ApplyFilters();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading inventory: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
                MessageBox.Show($"Error loading inventory: {ex.Message}\n\nPlease check:\n1. Database connection\n2. Inventory table exists\n3. Database permissions", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private DataTable GetInventoryData()
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
                        try
                        {
                            string createTableQuery = @"CREATE TABLE IF NOT EXISTS Inventory (
                                                        InventoryID INT AUTO_INCREMENT PRIMARY KEY,
                                                        BookID INT NOT NULL,
                                                        CopyNumber INT NOT NULL,
                                                        Location VARCHAR(50) NULL,
                                                        `Condition` VARCHAR(50) DEFAULT 'Good',
                                                        Status VARCHAR(50) DEFAULT 'Available',
                                                        LastVerified DATETIME NULL,
                                                        Notes VARCHAR(255) NULL,
                                                        FOREIGN KEY (BookID) REFERENCES Books(BookID)
                                                        )";
                            using (var createCmd = new MySqlCommand(createTableQuery, conn))
                            {
                                createCmd.ExecuteNonQuery();
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"Error creating Inventory table in GetInventoryData: {ex.Message}");
                            DataTable emptyTable = new DataTable();
                            emptyTable.Columns.Add("InventoryID", typeof(int));
                            emptyTable.Columns.Add("BookID", typeof(int));
                            emptyTable.Columns.Add("CopyNumber", typeof(int));
                            emptyTable.Columns.Add("Location", typeof(string));
                            emptyTable.Columns.Add("Condition", typeof(string));
                            emptyTable.Columns.Add("Status", typeof(string));
                            return emptyTable;
                        }
                    }
                }
                bool hasCopyNumber = DatabaseSchemaHelper.CheckColumnExists(conn, "Inventory", "CopyNumber");
                bool hasLastVerified = DatabaseSchemaHelper.CheckColumnExists(conn, "Inventory", "LastVerified");
                bool hasBarcode = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "Barcode");
                bool hasAccessionNo = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "AccessionNo");
                bool hasCopies = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "Copies");
                bool hasTotalCopies = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "TotalCopies");
                bool hasTitle = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "Title");
                bool hasAuthor = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "Author");
                bool hasCategory = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "Category");
                string bookIdentifier = hasBarcode ? "b.Barcode" : (hasAccessionNo ? "b.AccessionNo" : "CAST(b.BookID AS CHAR)");
                string bookIdentifierAlias = hasBarcode ? "Barcode" : (hasAccessionNo ? "AccessionNo" : "BookID");
                string titleSelect = hasTitle ? "b.Title," : "'N/A' as Title,";
                string authorSelect = hasAuthor ? "b.Author," : "'N/A' as Author,";
                string categorySelect = hasCategory ? "b.Category," : "'N/A' as Category,";
                string copiesColumn = hasCopies ? "b.Copies" : (hasTotalCopies ? "b.TotalCopies" : "(SELECT COUNT(*) FROM BookCopies WHERE BookID = b.BookID)");
                string copiesAlias = "Copies";
                string query;
                if (hasCopyNumber && hasLastVerified)
                {
                    query = $@"SELECT
                                i.InventoryID,
                                i.BookID,
                                i.CopyNumber,
                                i.Location,
                                i.`Condition`,
                                i.Status,
                                i.LastVerified,
                                {titleSelect}
                                {authorSelect}
                                {categorySelect}
                                {copiesColumn} as {copiesAlias},
                                {bookIdentifier} as {bookIdentifierAlias}
                             FROM Inventory i
                             INNER JOIN Books b ON i.BookID = b.BookID
                             ORDER BY i.InventoryID DESC";
                }
                else if (hasCopyNumber)
                {
                    query = $@"SELECT
                                i.InventoryID,
                                i.BookID,
                                i.CopyNumber,
                                i.Location,
                                i.`Condition`,
                                i.Status,
                                NULL as LastVerified,
                                {titleSelect}
                                {authorSelect}
                                {categorySelect}
                                {copiesColumn} as {copiesAlias},
                                {bookIdentifier} as {bookIdentifierAlias}
                             FROM Inventory i
                             INNER JOIN Books b ON i.BookID = b.BookID
                             ORDER BY i.InventoryID DESC";
                }
                else
                {
                    query = $@"SELECT
                                i.InventoryID,
                                i.BookID,
                                1 as CopyNumber,
                                i.Location,
                                i.`Condition`,
                                i.Status,
                                NULL as LastVerified,
                                {titleSelect}
                                {authorSelect}
                                {categorySelect}
                                {copiesColumn} as {copiesAlias},
                                {bookIdentifier} as {bookIdentifierAlias}
                             FROM Inventory i
                             INNER JOIN Books b ON i.BookID = b.BookID
                             ORDER BY i.InventoryID DESC";
                }
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }
        private string GenerateLocation(int bookId, int copyNumber)
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT Category FROM Books WHERE BookID = @BookID";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookID", bookId);
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            string category = result.ToString();
                            string categoryPrefix = category.Length > 0 ? category.Substring(0, 1).ToUpper() : "A";
                            return $"{categoryPrefix}-{bookId.ToString().PadLeft(2, '0')}-{copyNumber}";
                        }
                    }
                }
            }
            catch
            {
            }
            return $"A-{bookId.ToString().PadLeft(2, '0')}-{copyNumber}";
        }
        private void ApplyFilters()
        {
            if (allInventoryData == null || allInventoryData.Rows.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("ApplyFilters: allInventoryData is null or empty");
                dataGridViewInventory.DataSource = null;
                return;
            }
            
            try
            {
                // Handle search text - trim and check for placeholder (case-insensitive, with or without emoji)
                string searchText = txtSearch.Text?.Trim() ?? "";
                string placeholderText = "search inventory...";
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
                
                System.Diagnostics.Debug.WriteLine($"ApplyFilters: Original searchText='{txtSearch.Text}', Processed searchText='{searchText}', currentConditionFilter='{currentConditionFilter}', currentStatusFilter='{currentStatusFilter}'");
                
                // Create filtered table with all string columns
                DataTable filteredData = new DataTable();
                foreach (DataColumn col in allInventoryData.Columns)
                {
                    filteredData.Columns.Add(col.ColumnName, typeof(string));
                }
                
                int rowsProcessed = 0;
                int rowsMatched = 0;
                
                foreach (DataRow row in allInventoryData.Rows)
                {
                    rowsProcessed++;
                    
                    // Check condition filter
                    bool matchesCondition = currentConditionFilter == "All Conditions" ||
                        (row["Condition"]?.ToString().Equals(currentConditionFilter, StringComparison.OrdinalIgnoreCase) ?? false);
                    
                    // Check status filter
                    bool matchesStatus = currentStatusFilter == "All Status" ||
                        (row["Status"]?.ToString().Equals(currentStatusFilter, StringComparison.OrdinalIgnoreCase) ?? false);
                    
                    // Check search text
                    bool matchesSearch = string.IsNullOrEmpty(searchText) ||
                        (row["BookDetails"]?.ToString().ToLower().Contains(searchText) ?? false) ||
                        (row["Location"]?.ToString().ToLower().Contains(searchText) ?? false);
                    
                    if (matchesCondition && matchesStatus && matchesSearch)
                    {
                        try
                        {
                            DataRow newRow = filteredData.NewRow();
                            foreach (DataColumn col in allInventoryData.Columns)
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
                dataGridViewInventory.DataSource = null;
                dataGridViewInventory.Refresh();
                dataGridViewInventory.DataSource = filteredData;
                
                System.Diagnostics.Debug.WriteLine($"DataGridView DataSource set. Rows in grid: {dataGridViewInventory.Rows.Count}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error applying filters: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
                // If filtering fails, just show all data
                dataGridViewInventory.DataSource = allInventoryData;
            }
        }
        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "🔍 Search inventory...")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }
        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "🔍 Search inventory...";
                txtSearch.ForeColor = Color.Gray;
            }
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadInventory();
        }
        private void btnFilterCondition_Click(object sender, EventArgs e)
        {
            ContextMenuStrip filterMenu = new ContextMenuStrip();
            filterMenu.Items.Add("All Conditions", null, (s, args) => { currentConditionFilter = "All Conditions"; btnFilterCondition.Text = "🔍 All Conditions"; LoadInventory(); });
            filterMenu.Items.Add("Excellent", null, (s, args) => { currentConditionFilter = "Excellent"; btnFilterCondition.Text = "⭐ Excellent"; LoadInventory(); });
            filterMenu.Items.Add("Good", null, (s, args) => { currentConditionFilter = "Good"; btnFilterCondition.Text = "✅ Good"; LoadInventory(); });
            filterMenu.Items.Add("Fair", null, (s, args) => { currentConditionFilter = "Fair"; btnFilterCondition.Text = "⚠️ Fair"; LoadInventory(); });
            filterMenu.Items.Add("Damaged", null, (s, args) => { currentConditionFilter = "Damaged"; btnFilterCondition.Text = "🔧 Damaged"; LoadInventory(); });
            filterMenu.Show(btnFilterCondition, new Point(0, btnFilterCondition.Height));
        }
        private void btnFilterStatus_Click(object sender, EventArgs e)
        {
            ContextMenuStrip filterMenu = new ContextMenuStrip();
            filterMenu.Items.Add("All Status", null, (s, args) => { currentStatusFilter = "All Status"; btnFilterStatus.Text = "🔍 All Status"; LoadInventory(); });
            filterMenu.Items.Add("Available", null, (s, args) => { currentStatusFilter = "Available"; btnFilterStatus.Text = "✅ Available"; LoadInventory(); });
            filterMenu.Items.Add("Borrowed", null, (s, args) => { currentStatusFilter = "Borrowed"; btnFilterStatus.Text = "📖 Borrowed"; LoadInventory(); });
            filterMenu.Items.Add("For Repair", null, (s, args) => { currentStatusFilter = "For Repair"; btnFilterStatus.Text = "🔧 For Repair"; LoadInventory(); });
            filterMenu.Items.Add("Lost", null, (s, args) => { currentStatusFilter = "Lost"; btnFilterStatus.Text = "📕 Lost"; LoadInventory(); });
            filterMenu.Show(btnFilterStatus, new Point(0, btnFilterStatus.Height));
        }
        private void VerifyInventory(int inventoryId)
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    bool hasLastVerified = DatabaseSchemaHelper.CheckColumnExists(conn, "Inventory", "LastVerified");
                    string updateQuery;
                    if (hasLastVerified)
                    {
                        updateQuery = "UPDATE Inventory SET LastVerified = @LastVerified WHERE InventoryID = @InventoryID";
                    }
                    else
                    {
                        AddColumnIfNotExists(conn, "Inventory", "LastVerified", "DATETIME NULL");
                        updateQuery = "UPDATE Inventory SET LastVerified = @LastVerified WHERE InventoryID = @InventoryID";
                    }
                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@InventoryID", inventoryId);
                        cmd.Parameters.AddWithValue("@LastVerified", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Inventory item verified successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadInventory();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error verifying inventory: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UpdateInventory(int inventoryId)
        {
            try
            {
                DataRow inventoryRow = null;
                foreach (DataRow row in allInventoryData.Rows)
                {
                    object invIdObj = row["InventoryID"];
                    int rowInventoryId = 0;
                    
                    if (invIdObj != null && invIdObj != DBNull.Value)
                    {
                        string invIdStr = invIdObj.ToString();
                        // Remove "INV-" prefix if present
                        if (invIdStr.StartsWith("INV-"))
                        {
                            invIdStr = invIdStr.Replace("INV-", "");
                        }
                        if (int.TryParse(invIdStr, out rowInventoryId) && rowInventoryId == inventoryId)
                        {
                            inventoryRow = row;
                            break;
                        }
                    }
                }
                if (inventoryRow == null)
                {
                    MessageBox.Show("Inventory item not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                using (Form updateForm = new Form())
                {
                    updateForm.Text = "Update Inventory";
                    updateForm.Size = new Size(400, 300);
                    updateForm.StartPosition = FormStartPosition.CenterParent;
                    updateForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                    updateForm.MaximizeBox = false;
                    updateForm.MinimizeBox = false;
                    Label lblCondition = new Label { Text = "Condition:", Location = new Point(20, 20), AutoSize = true };
                    ComboBox cmbCondition = new ComboBox
                    {
                        Location = new Point(120, 17),
                        Size = new Size(200, 25),
                        DropDownStyle = ComboBoxStyle.DropDownList
                    };
                    cmbCondition.Items.AddRange(new[] { "Excellent", "Good", "Fair", "Damaged" });
                    cmbCondition.SelectedItem = inventoryRow["Condition"]?.ToString() ?? "Good";
                    Label lblStatus = new Label { Text = "Status:", Location = new Point(20, 60), AutoSize = true };
                    ComboBox cmbStatus = new ComboBox
                    {
                        Location = new Point(120, 57),
                        Size = new Size(200, 25),
                        DropDownStyle = ComboBoxStyle.DropDownList
                    };
                    cmbStatus.Items.AddRange(new[] { "Available", "Borrowed", "For Repair", "Lost" });
                    cmbStatus.SelectedItem = inventoryRow["Status"]?.ToString() ?? "Available";
                    Label lblLocation = new Label { Text = "Location:", Location = new Point(20, 100), AutoSize = true };
                    TextBox txtLocation = new TextBox
                    {
                        Location = new Point(120, 97),
                        Size = new Size(200, 25)
                    };
                    txtLocation.Text = inventoryRow["Location"]?.ToString() ?? "";
                    Button btnSave = new Button
                    {
                        Text = "Save",
                        Location = new Point(120, 140),
                        Size = new Size(80, 30),
                        DialogResult = DialogResult.OK
                    };
                    Button btnCancel = new Button
                    {
                        Text = "Cancel",
                        Location = new Point(210, 140),
                        Size = new Size(80, 30),
                        DialogResult = DialogResult.Cancel
                    };
                    updateForm.Controls.AddRange(new Control[] { lblCondition, cmbCondition, lblStatus, cmbStatus, lblLocation, txtLocation, btnSave, btnCancel });
                    updateForm.AcceptButton = btnSave;
                    updateForm.CancelButton = btnCancel;
                    if (updateForm.ShowDialog() == DialogResult.OK)
                    {
                        using (var conn = _dbContext.GetConnection())
                        {
                            conn.Open();
                            string updateQuery = "UPDATE Inventory SET `Condition` = @Condition, Status = @Status, Location = @Location WHERE InventoryID = @InventoryID";
                            using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@InventoryID", inventoryId);
                                cmd.Parameters.AddWithValue("@Condition", cmbCondition.SelectedItem.ToString());
                                cmd.Parameters.AddWithValue("@Status", cmbStatus.SelectedItem.ToString());
                                cmd.Parameters.AddWithValue("@Location", txtLocation.Text);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        MessageBox.Show("Inventory item updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadMetrics();
                        LoadInventory();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating inventory: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DataGridViewInventory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle cell clicks for Actions column
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            string columnName = dataGridViewInventory.Columns[e.ColumnIndex].Name;
            if (columnName != "Actions") return;
            
            try
            {
                DataGridViewRow row = dataGridViewInventory.Rows[e.RowIndex];
                int inventoryId = 0;
                
                // Try to get inventory ID from the row
                object inventoryIdObj = row.Cells["InventoryID"].Value;
                if (inventoryIdObj != null)
                {
                    string inventoryIdStr = inventoryIdObj.ToString();
                    // Remove "INV-" prefix if present
                    if (inventoryIdStr.StartsWith("INV-"))
                    {
                        inventoryIdStr = inventoryIdStr.Replace("INV-", "");
                    }
                    if (!int.TryParse(inventoryIdStr, out inventoryId))
                    {
                        // Try to get from DataTable
                        if (dataGridViewInventory.DataSource is DataTable dt && e.RowIndex < dt.Rows.Count)
                        {
                            DataRow dataRow = dt.Rows[e.RowIndex];
                            if (dataRow["InventoryID"] != null && dataRow["InventoryID"] != DBNull.Value)
                            {
                                inventoryIdStr = dataRow["InventoryID"].ToString();
                                if (inventoryIdStr.StartsWith("INV-"))
                                {
                                    inventoryIdStr = inventoryIdStr.Replace("INV-", "");
                                }
                                int.TryParse(inventoryIdStr, out inventoryId);
                            }
                        }
                    }
                }
                
                if (inventoryId == 0)
                {
                    System.Diagnostics.Debug.WriteLine("Could not determine inventory ID");
                    return;
                }
                
                // Get mouse position relative to the DataGridView
                Point clickPoint = dataGridViewInventory.PointToClient(Cursor.Position);
                Rectangle cellRect = dataGridViewInventory.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                
                // Calculate button positions (matching CellPainting logic)
                int buttonHeight = 30;
                int buttonWidth = 80;
                int spacing = 5;
                int xOffset = 5; // Offset from left edge of cell
                int buttonY = (cellRect.Height - buttonHeight) / 2;
                
                // Calculate click position relative to the cell
                int relativeX = clickPoint.X - cellRect.X;
                int relativeY = clickPoint.Y - cellRect.Y;
                
                // Define button rectangles relative to cell
                Rectangle btnVerifyRect = new Rectangle(xOffset, buttonY, buttonWidth, buttonHeight);
                Rectangle btnUpdateRect = new Rectangle(xOffset + buttonWidth + spacing, buttonY, buttonWidth, buttonHeight);
                
                // Check which button was clicked
                if (btnVerifyRect.Contains(new Point(relativeX, relativeY)))
                {
                    VerifyInventory(inventoryId);
                }
                else if (btnUpdateRect.Contains(new Point(relativeX, relativeY)))
                {
                    UpdateInventory(inventoryId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"CellClick error: {ex}");
            }
        }

        private void panelTableContainer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelMetrics_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}