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
using Project5LMS.Forms.Admin.Search;
using Project5LMS.Models;
using Project5LMS.Repositories;
namespace Project5LMS.Forms.Admin.Circulation
{
    public partial class AdminCirculationForm : Form
    {
        private DataTable allTransactionsData;
        private readonly ICirculationService _circulationService;
        private readonly IFinesService _finesService;
        private readonly IBookService _bookService;
        private readonly IMembersService _membersService;
        private readonly DatabaseContext _dbContext;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IReservationService _reservationService;
        private readonly IBookCopyRepository _bookCopyRepository;
        private readonly BorrowingValidator _borrowingValidator;
        private readonly ReceiptService _receiptService;
        public AdminCirculationForm()
        {
            InitializeComponent();
            try
            {
                AccessControlHelper.RequireAnyRole("Admin", "LibraryStaff");
                AuditLogger.LogAccessControl("AdminCirculationForm accessed", $"User: {CurrentUser.Email}", "Success");
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                AuditLogger.LogAccessControl("AdminCirculationForm access denied", $"User: {CurrentUser.Email}", "Failed");
                this.Close();
                return;
            }
            _dbContext = ServiceFactory.GetDbContext();
            _circulationService = ServiceFactory.CreateCirculationService();
            _finesService = ServiceFactory.CreateFinesService();
            _bookService = ServiceFactory.CreateBookService();
            _membersService = ServiceFactory.CreateMembersService();
            _transactionRepository = DependencyInjection.GetRequiredService<ITransactionRepository>();
            _memberRepository = DependencyInjection.GetRequiredService<IMemberRepository>();
            _reservationService = ServiceFactory.CreateReservationService();
            _bookCopyRepository = DependencyInjection.GetRequiredService<IBookCopyRepository>();
            _borrowingValidator = DependencyInjection.GetRequiredService<BorrowingValidator>();
            _receiptService = DependencyInjection.GetRequiredService<ReceiptService>();
            this.ResizeRedraw = true;
        }
        private void AdminCirculationForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.Shown += AdminCirculationForm_Shown;
        }
        
        private void AdminCirculationForm_Shown(object sender, EventArgs e)
        {
            // Ensure form is properly sized before loading data
            if (this.Parent != null)
            {
                this.Dock = DockStyle.Fill;
                if (this.Parent is Panel parentPanel)
                {
                    this.panelMainContainer.Dock = DockStyle.Fill;
                    this.panelMainContainer.PerformLayout();
                    this.PerformLayout();
                }
            }
            // Auto-size panels based on available space
            AdjustPanelSizes();
            this.Resize += AdminCirculationForm_Resize;
            EnsureTransactionsTableExists();
            SetupDataGridView();
            LoadMetrics();
            LoadTransactions();
            tabControl.SelectedIndex = 0;
            ShowTabContent(0);
        }
        
        private void AdminCirculationForm_Resize(object sender, EventArgs e)
        {
            // Re-adjust panel sizes when form resizes
            if (this.Width > 0 && this.Height > 0)
            {
                AdjustPanelSizes();
            }
        }
        
        private void AdjustPanelSizes()
        {
            try
            {
                // Use helper to get available dimensions
                int availableWidth = PanelSizeHelper.GetAvailableWidth(this);
                int availableHeight = PanelSizeHelper.GetAvailableHeight(this);
                
                // Adjust metrics panel - distribute 4 metric panels evenly
                if (panelMetrics != null && panelMetrics.Width > 0)
                {
                    PanelSizeHelper.DistributePanelsEvenly(panelMetrics, 4, spacing: 16, minPanelWidth: 180);
                }
                
                // Adjust action tabs panel width (use 95% of available width)
                if (panelActionTabs != null)
                {
                    int optimalWidth = PanelSizeHelper.CalculateWidth(this, 0.95, padding: 48, minWidth: 600);
                    if (panelActionTabs.Width != optimalWidth && optimalWidth > 0)
                    {
                        panelActionTabs.Width = optimalWidth;
                    }
                }
                
                // Adjust table container height (use 40% of available height, minimum 200px)
                if (panelTableContainer != null)
                {
                    int optimalHeight = PanelSizeHelper.CalculateHeight(this, 0.40, padding: 48, minHeight: 200);
                    if (panelTableContainer.Height < 200 && optimalHeight >= 200)
                    {
                        panelTableContainer.Height = optimalHeight;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error adjusting panel sizes: {ex.Message}");
            }
        }
        private void EnsureTransactionsTableExists()
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string checkTableQuery = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES
                                              WHERE TABLE_SCHEMA = DATABASE()
                                              AND TABLE_NAME = 'Transactions'";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkTableQuery, conn))
                    {
                        int tableExists = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (tableExists == 0)
                        {
                            string createTableQuery = @"CREATE TABLE IF NOT EXISTS Transactions (
                                                        TransactionID INT AUTO_INCREMENT PRIMARY KEY,
                                                        MemberID INT NOT NULL,
                                                        BookID INT NOT NULL,
                                                        BorrowDate DATETIME NOT NULL,
                                                        DueDate DATETIME NOT NULL,
                                                        ReturnDate DATETIME NULL,
                                                        Status VARCHAR(50) DEFAULT 'Borrowed',
                                                        Fine DECIMAL(10,2) DEFAULT 0.00,
                                                        TransactionType VARCHAR(50) DEFAULT 'Borrow',
                                                        FOREIGN KEY (MemberID) REFERENCES Members(MemberID),
                                                        FOREIGN KEY (BookID) REFERENCES Books(BookID)
                                                        )";
                            using (MySqlCommand createCmd = new MySqlCommand(createTableQuery, conn))
                            {
                                createCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error ensuring Transactions table exists: {ex.Message}");
            }
        }
        private void SetupDataGridView()
        {
            dataGridViewTransactions.Columns.Clear();
            dataGridViewTransactions.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn colTransactionID = new DataGridViewTextBoxColumn
            {
                Name = "TransactionID",
                HeaderText = "TRANSACTION ID",
                DataPropertyName = "TransactionID",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            };
            dataGridViewTransactions.Columns.Add(colTransactionID);
            DataGridViewColumn colType = new DataGridViewTextBoxColumn
            {
                Name = "Type",
                HeaderText = "TYPE",
                DataPropertyName = "Type",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            };
            dataGridViewTransactions.Columns.Add(colType);
            DataGridViewTextBoxColumn colMember = new DataGridViewTextBoxColumn
            {
                Name = "Member",
                HeaderText = "MEMBER",
                DataPropertyName = "Member",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            };
            dataGridViewTransactions.Columns.Add(colMember);
            DataGridViewTextBoxColumn colBook = new DataGridViewTextBoxColumn
            {
                Name = "Book",
                HeaderText = "BOOK",
                DataPropertyName = "Book",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            };
            dataGridViewTransactions.Columns.Add(colBook);
            DataGridViewTextBoxColumn colBorrowDate = new DataGridViewTextBoxColumn
            {
                Name = "BorrowDate",
                HeaderText = "BORROW DATE",
                DataPropertyName = "BorrowDate",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            };
            dataGridViewTransactions.Columns.Add(colBorrowDate);
            DataGridViewTextBoxColumn colDueDate = new DataGridViewTextBoxColumn
            {
                Name = "DueDate",
                HeaderText = "DUE DATE",
                DataPropertyName = "DueDate",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            };
            dataGridViewTransactions.Columns.Add(colDueDate);
            DataGridViewColumn colStatus = new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "STATUS",
                DataPropertyName = "Status",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            };
            dataGridViewTransactions.Columns.Add(colStatus);
            DataGridViewTextBoxColumn colFine = new DataGridViewTextBoxColumn
            {
                Name = "Fine",
                HeaderText = "FINE",
                DataPropertyName = "Fine",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            };
            dataGridViewTransactions.Columns.Add(colFine);
            dataGridViewTransactions.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridViewTransactions.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridViewTransactions.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dataGridViewTransactions.ColumnHeadersDefaultCellStyle.ForeColor = Constants.GetNeutralColor();
            dataGridViewTransactions.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            dataGridViewTransactions.RowTemplate.Height = Constants.DefaultRowHeight;
            dataGridViewTransactions.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dataGridViewTransactions.CellFormatting += DataGridViewTransactions_CellFormatting;
            dataGridViewTransactions.CellPainting += DataGridViewTransactions_CellPainting;
        }
        private void DataGridViewTransactions_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dataGridViewTransactions.Rows[e.RowIndex];
            string columnName = dataGridViewTransactions.Columns[e.ColumnIndex].Name;
            if (columnName == "TransactionID" && e.Value != null)
            {
                string transactionIdStr = e.Value.ToString();
                if (int.TryParse(transactionIdStr, out int transactionId))
                {
                    e.Value = IDFormatter.FormatTransactionID(transactionId);
                }
                e.FormattingApplied = true;
            }
            if (columnName == "Fine" && e.Value != null)
            {
                if (row.DataBoundItem != null)
                {
                    DataRowView rowView = row.DataBoundItem as DataRowView;
                    if (rowView != null)
                    {
                        decimal fineAmount = 0;
                        if (rowView["Fine"] != DBNull.Value)
                        {
                            fineAmount = Convert.ToDecimal(rowView["Fine"]);
                        }
                        if (fineAmount > 0)
                        {
                            e.Value = Project5LMS.Helpers.IDFormatter.FormatCurrency(fineAmount);
                        }
                        else
                        {
                            e.Value = "-";
                        }
                    }
                }
                e.FormattingApplied = true;
            }
        }
        private void DataGridViewTransactions_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            string columnName = dataGridViewTransactions.Columns[e.ColumnIndex].Name;
            if (columnName == "Type")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                string value = e.Value?.ToString() ?? "";
                string icon = "";
                Color iconColor = Color.Black;
                switch (value.ToLower())
                {
                    case "borrow":
                        icon = "📖";
                        iconColor = Color.FromArgb(13, 110, 253);
                        break;
                    case "return":
                        icon = "↩️";
                        iconColor = Color.FromArgb(40, 167, 69);
                        break;
                    case "renew":
                        icon = "📋";
                        iconColor = Color.FromArgb(221, 160, 221);
                        break;
                }
                if (!string.IsNullOrEmpty(icon))
                {
                    Rectangle iconRect = new Rectangle(
                        e.CellBounds.X + 10,
                        e.CellBounds.Y + (e.CellBounds.Height - 20) / 2,
                        20,
                        20
                    );
                    using (Font font = new Font("Segoe UI", Constants.DefaultRowHeight / 3, FontStyle.Bold))
                    using (Brush brush = new SolidBrush(iconColor))
                    {
                        e.Graphics.DrawString(icon, font, brush, iconRect);
                    }
                    Rectangle textRect = new Rectangle(
                        e.CellBounds.X + 35,
                        e.CellBounds.Y,
                        e.CellBounds.Width - 35,
                        e.CellBounds.Height
                    );
                    TextRenderer.DrawText(
                        e.Graphics,
                        value,
                        dataGridViewTransactions.DefaultCellStyle.Font,
                        textRect,
                        Color.Black,
                        TextFormatFlags.VerticalCenter
                    );
                }
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
                    case "active":
                    case "borrowed":
                        bgColor = Color.FromArgb(13, 110, 253);
                        textColor = Color.White;
                        break;
                    case "overdue":
                        bgColor = Color.FromArgb(220, 53, 69);
                        textColor = Color.White;
                        break;
                    case "returned":
                        bgColor = Color.FromArgb(40, 167, 69);
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
                    dataGridViewTransactions.DefaultCellStyle.Font,
                    badgeRect,
                    textColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                );
                e.Handled = true;
            }
        }
        private void LoadMetrics()
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string queryToday = @"SELECT COUNT(*) FROM Transactions
                                        WHERE DATE(BorrowDate) = CURDATE()
                                        OR DATE(ReturnDate) = CURDATE()";
                    using (MySqlCommand cmd = new MySqlCommand(queryToday, conn))
                    {
                        int todayActivity = Convert.ToInt32(cmd.ExecuteScalar());
                        lblMetricTodayActivityValue.Text = todayActivity.ToString();
                    }
                    string queryBorrowed = @"SELECT COUNT(*) FROM Transactions
                                            WHERE Status = 'Borrowed' OR Status = 'Active'";
                    using (MySqlCommand cmd = new MySqlCommand(queryBorrowed, conn))
                    {
                        int borrowed = Convert.ToInt32(cmd.ExecuteScalar());
                        lblMetricCurrentlyBorrowedValue.Text = borrowed.ToString();
                    }
                    string queryOverdue = @"SELECT COUNT(*) FROM Transactions
                                          WHERE (Status = 'Borrowed' OR Status = 'Active')
                                          AND DueDate < CURDATE()";
                    using (MySqlCommand cmd = new MySqlCommand(queryOverdue, conn))
                    {
                        int overdue = Convert.ToInt32(cmd.ExecuteScalar());
                        lblMetricOverdueValue.Text = overdue.ToString();
                    }
                    string queryTotal = "SELECT COUNT(*) FROM Transactions";
                    using (MySqlCommand cmd = new MySqlCommand(queryTotal, conn))
                    {
                        int total = Convert.ToInt32(cmd.ExecuteScalar());
                        lblMetricTotalTransactionsValue.Text = total.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading metrics: {ex.Message}");
            }
        }
        private void LoadTransactions()
        {
            try
            {
                allTransactionsData = GetTransactionsData();
                
                if (allTransactionsData == null)
                {
                    System.Diagnostics.Debug.WriteLine("GetTransactionsData returned null");
                    allTransactionsData = new DataTable();
                }
                
                if (allTransactionsData.Rows.Count == 0)
                {
                    System.Diagnostics.Debug.WriteLine("No transactions found in database");
                    // Still set the data source so the grid shows (empty)
                    dataGridViewTransactions.DataSource = allTransactionsData;
                    return;
                }
                
                if (!allTransactionsData.Columns.Contains("Type"))
                {
                    allTransactionsData.Columns.Add("Type", typeof(string));
                }
                if (!allTransactionsData.Columns.Contains("Member"))
                {
                    allTransactionsData.Columns.Add("Member", typeof(string));
                }
                if (!allTransactionsData.Columns.Contains("Book"))
                {
                    allTransactionsData.Columns.Add("Book", typeof(string));
                }
                
                // Convert TransactionID to string for search compatibility
                if (allTransactionsData.Columns.Contains("TransactionID") && allTransactionsData.Columns["TransactionID"].DataType != typeof(string))
                {
                    DataColumn transIdCol = allTransactionsData.Columns["TransactionID"];
                    string transIdName = transIdCol.ColumnName;
                    int transIdIndex = transIdCol.Ordinal;
                    
                    // Store original values
                    var originalTransIdValues = new List<string>();
                    foreach (DataRow row in allTransactionsData.Rows)
                    {
                        object originalValue = row["TransactionID"];
                        string transIdValue = originalValue != null && originalValue != DBNull.Value 
                            ? originalValue.ToString() 
                            : "0";
                        originalTransIdValues.Add(transIdValue);
                    }
                    
                    allTransactionsData.Columns.Remove(transIdCol);
                    DataColumn newTransIdCol = new DataColumn(transIdName, typeof(string));
                    allTransactionsData.Columns.Add(newTransIdCol);
                    if (transIdIndex < allTransactionsData.Columns.Count - 1)
                    {
                        newTransIdCol.SetOrdinal(transIdIndex);
                    }
                    
                    // Populate with string values
                    for (int i = 0; i < allTransactionsData.Rows.Count && i < originalTransIdValues.Count; i++)
                    {
                        allTransactionsData.Rows[i]["TransactionID"] = originalTransIdValues[i];
                    }
                }
                
                foreach (DataRow row in allTransactionsData.Rows)
                {
                    string transactionType = row.Table.Columns.Contains("TransactionType") && row["TransactionType"] != DBNull.Value ? row["TransactionType"].ToString() : "Borrow";
                    row["Type"] = transactionType;
                    string firstName = row.Table.Columns.Contains("FirstName") && row["FirstName"] != DBNull.Value ? row["FirstName"].ToString() : "";
                    string lastName = row.Table.Columns.Contains("LastName") && row["LastName"] != DBNull.Value ? row["LastName"].ToString() : "";
                    int memberId = Convert.ToInt32(row["MemberID"]);
                    row["Member"] = IDFormatter.FormatMemberDisplay(firstName, lastName, memberId);
                    string bookTitle = row.Table.Columns.Contains("Title") && row["Title"] != DBNull.Value ? row["Title"].ToString() : "";
                    int bookId = Convert.ToInt32(row["BookID"]);
                    string barcode = "";
                    if (row.Table.Columns.Contains("Barcode") && row["Barcode"] != DBNull.Value)
                    {
                        barcode = row["Barcode"].ToString();
                    }
                    else if (row.Table.Columns.Contains("AccessionNo") && row["AccessionNo"] != DBNull.Value)
                    {
                        barcode = row["AccessionNo"].ToString();
                    }
                    string accessionNo = !string.IsNullOrEmpty(barcode) ? IDFormatter.FormatAccessionNumber(barcode) : IDFormatter.FormatAccessionNumber(bookId.ToString());
                    row["Book"] = IDFormatter.FormatBookDisplay(bookTitle, accessionNo);
                    if (row.Table.Columns.Contains("Status") && row["Status"] != DBNull.Value)
                    {
                        string status = row["Status"].ToString();
                        if (status == "Borrowed" || status == "Active")
                        {
                            if (row.Table.Columns.Contains("DueDate") && row["DueDate"] != DBNull.Value)
                            {
                                DateTime dueDate = Convert.ToDateTime(row["DueDate"]);
                                if (dueDate < DateTime.Now)
                                {
                                    row["Status"] = "Overdue";
                                }
                                else
                                {
                                    row["Status"] = "Active";
                                }
                            }
                        }
                    }
                }
                
                dataGridViewTransactions.DataSource = allTransactionsData;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading transactions: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
                ErrorHandler.ShowDatabaseError("loading transactions", ex);
            }
        }
        private DataTable GetTransactionsData()
        {
            using (var conn = _dbContext.GetConnection())
            {
                conn.Open();
                bool hasTransactionType = DatabaseSchemaHelper.CheckColumnExists(conn, "Transactions", "TransactionType");
                bool hasFine = DatabaseSchemaHelper.CheckColumnExists(conn, "Transactions", "Fine");
                bool hasBarcode = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "Barcode");
                bool hasAccessionNo = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "AccessionNo");
                string bookIdentifier = hasBarcode ? "b.Barcode" : (hasAccessionNo ? "b.AccessionNo" : "CAST(b.BookID AS CHAR)");
                string bookIdentifierAlias = hasBarcode ? "Barcode" : (hasAccessionNo ? "AccessionNo" : "BookID");
                string query;
                if (hasTransactionType && hasFine)
                {
                    query = $@"SELECT
                                t.TransactionID,
                                t.MemberID,
                                t.BookID,
                                t.BorrowDate,
                                t.DueDate,
                                t.ReturnDate,
                                t.Status,
                                t.TransactionType,
                                t.Fine,
                                m.FirstName,
                                m.LastName,
                                b.Title,
                                {bookIdentifier} as {bookIdentifierAlias}
                             FROM Transactions t
                             INNER JOIN Members m ON t.MemberID = m.MemberID
                             INNER JOIN Books b ON t.BookID = b.BookID
                             ORDER BY t.TransactionID DESC
                             LIMIT {Constants.MaxQueryLimit}";
                }
                else if (hasTransactionType)
                {
                    query = $@"SELECT
                                t.TransactionID,
                                t.MemberID,
                                t.BookID,
                                t.BorrowDate,
                                t.DueDate,
                                t.ReturnDate,
                                t.Status,
                                t.TransactionType,
                                0.00 as Fine,
                                m.FirstName,
                                m.LastName,
                                b.Title,
                                {bookIdentifier} as {bookIdentifierAlias}
                             FROM Transactions t
                             INNER JOIN Members m ON t.MemberID = m.MemberID
                             INNER JOIN Books b ON t.BookID = b.BookID
                             ORDER BY t.TransactionID DESC
                             LIMIT {Constants.MaxQueryLimit}";
                }
                else
                {
                    query = $@"SELECT
                                t.TransactionID,
                                t.MemberID,
                                t.BookID,
                                t.BorrowDate,
                                t.DueDate,
                                t.ReturnDate,
                                t.Status,
                                'Borrow' as TransactionType,
                                0.00 as Fine,
                                m.FirstName,
                                m.LastName,
                                b.Title,
                                {bookIdentifier} as {bookIdentifierAlias}
                             FROM Transactions t
                             INNER JOIN Members m ON t.MemberID = m.MemberID
                             INNER JOIN Books b ON t.BookID = b.BookID
                             ORDER BY t.TransactionID DESC
                             LIMIT {Constants.MaxQueryLimit}";
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
        private bool CheckColumnExists(MySqlConnection conn, string tableName, string columnName)
        {
            try
            {
                string query = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS
                                WHERE TABLE_SCHEMA = DATABASE()
                                AND TABLE_NAME = @tableName
                                AND COLUMN_NAME = @columnName";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@tableName", tableName);
                    cmd.Parameters.AddWithValue("@columnName", columnName);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
            catch
            {
                return false;
            }
        }
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowTabContent(tabControl.SelectedIndex);
        }
        private void ShowTabContent(int tabIndex)
        {
            try
            {
                switch (tabIndex)
                {
                    case 0: // Borrow Book tab
                        // Content is already in the tab, no action needed
                        break;
                    case 1: // Return Book tab
                        // Content is already in the tab, no action needed
                        break;
                    case 2: // Renew Book tab
                        // Content is already in the tab, no action needed
                        break;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error showing tab content: {ex.Message}");
            }
        }
        private void txtBorrowMemberID_Enter(object sender, EventArgs e)
        {
            if (txtBorrowMemberID.Text == "Format: MEM-000001 or 1" || txtBorrowMemberID.Text.Contains("Format:"))
            {
                txtBorrowMemberID.Text = "";
                txtBorrowMemberID.ForeColor = Color.Black;
            }
        }
        private void txtBorrowMemberID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBorrowMemberID.Text))
            {
                txtBorrowMemberID.Text = "Format: MEM-000001 or 1";
                txtBorrowMemberID.ForeColor = Color.Gray;
            }
        }
        private void txtBorrowMemberID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBorrowBookAccession.Focus();
                e.SuppressKeyPress = true;
            }
        }
        private void txtBorrowBookAccession_Enter(object sender, EventArgs e)
        {
            if (txtBorrowBookAccession.Text == "Format: ACC-000001 or 1" || txtBorrowBookAccession.Text.Contains("Format:"))
            {
                txtBorrowBookAccession.Text = "";
                txtBorrowBookAccession.ForeColor = Color.Black;
            }
        }
        private void txtBorrowBookAccession_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBorrowBookAccession.Text))
            {
                txtBorrowBookAccession.Text = "Format: ACC-000001 or 1";
                txtBorrowBookAccession.ForeColor = Color.Gray;
            }
        }
        private void txtBorrowBookAccession_TextChanged(object sender, EventArgs e)
        {
            // Display book information when accession is entered
            string bookAccessionText = txtBorrowBookAccession.Text.Trim();
            if (string.IsNullOrWhiteSpace(bookAccessionText) || bookAccessionText == "Format: ACC-000001 or 1" || bookAccessionText.Contains("Format:"))
            {
                ClearBookInfoDisplay("Borrow");
                return;
            }
            
            try
            {
                DisplayBookInfo(bookAccessionText, "Borrow");
            }
            catch
            {
                ClearBookInfoDisplay("Borrow");
            }
        }
        private void txtBorrowBookAccession_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnProcessBorrowing_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }
        private void txtReturnBookAccession_Enter(object sender, EventArgs e)
        {
            if (txtReturnBookAccession.Text == "Format: ACC-000001 or 1" || txtReturnBookAccession.Text.Contains("Format:"))
            {
                txtReturnBookAccession.Text = "";
                txtReturnBookAccession.ForeColor = Color.Black;
            }
        }
        private void txtReturnBookAccession_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtReturnBookAccession.Text))
            {
                txtReturnBookAccession.Text = "Format: ACC-000001 or 1";
                txtReturnBookAccession.ForeColor = Color.Gray;
            }
        }
        private void txtReturnBookAccession_TextChanged(object sender, EventArgs e)
        {
            // Display book and transaction information when accession is entered
            string bookAccessionText = txtReturnBookAccession.Text.Trim();
            if (string.IsNullOrWhiteSpace(bookAccessionText) || bookAccessionText == "Format: ACC-000001 or 1" || bookAccessionText.Contains("Format:"))
            {
                ClearBookInfoDisplay("Return");
                return;
            }
            
            try
            {
                DisplayBookInfo(bookAccessionText, "Return");
            }
            catch
            {
                ClearBookInfoDisplay("Return");
            }
        }
        private void txtReturnBookAccession_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnProcessReturn_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }
        
        private void btnProcessBorrowing_Click(object sender, EventArgs e)
        {
            string memberIdText = txtBorrowMemberID.Text.Trim();
            string bookAccessionText = txtBorrowBookAccession.Text.Trim();
            if (memberIdText == "Format: MEM-000001 or 1" || memberIdText.Contains("Format:") || string.IsNullOrWhiteSpace(memberIdText))
            {
                ErrorHandler.ShowValidationError("Please enter a Member ID.");
                txtBorrowMemberID.Focus();
                return;
            }
            if (bookAccessionText == "Format: ACC-000001 or 1" || bookAccessionText.Contains("Format:") || string.IsNullOrWhiteSpace(bookAccessionText))
            {
                MessageBox.Show("Please enter a Book Accession Number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBorrowBookAccession.Focus();
                return;
            }
            try
            {
                int memberId = Project5LMS.Helpers.IDFormatter.ParseMemberID(memberIdText);
                if (memberId == 0)
                {
                    ErrorHandler.ShowValidationError("Invalid Member ID format.");
                    return;
                }
                int bookId = GetBookIdFromAccession(bookAccessionText);
                if (bookId == 0)
                {
                    MessageBox.Show("Book not found with the provided accession number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // Use BorrowingValidator for centralized validation
                var validationResult = _borrowingValidator.ValidateBorrowing(memberId, bookId);
                if (!validationResult.IsValid)
                {
                    ErrorHandler.ShowValidationError(validationResult.GetErrorMessage());
                    return;
                }
                
                // All validations passed - proceed with borrowing
                var member = validationResult.Member;
                var book = validationResult.Book;
                int borrowDays = validationResult.BorrowingPeriodDays;
                
                // Additional checks before attempting to borrow
                if (book == null)
                {
                    MessageBox.Show("Book not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                if (!book.IsAvailable)
                {
                    MessageBox.Show($"Book is not available for borrowing.\n\nTitle: {book.Title}\nAvailable Copies: {book.Available}/{book.TotalCopies}", 
                        "Book Not Available", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                if (string.IsNullOrWhiteSpace(book.AccessionNo))
                {
                    MessageBox.Show("Book accession number is missing. Please update the book information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                // Note: We don't check for active transactions by BookID here because
                // books can have multiple copies. As long as Available > 0, a copy can be borrowed.
                // The availability check (book.IsAvailable) already ensures there are copies available.
                
                if (_circulationService.BorrowBook(memberId, bookId, borrowDays))
                {
                    var transaction = _circulationService.GetActiveTransactionByBook(bookId);
                    DateTime dueDate = transaction != null ? transaction.DueDate : DateTime.Now.AddDays(borrowDays);
                    
                    // Generate borrowing receipt
                    string receiptNumber = IDFormatter.FormatReceiptNumber(DateTime.Now, new Random().Next(100000, 999999));
                    string receiptMessage = _receiptService.GenerateBorrowingReceipt(member, book, dueDate, receiptNumber);
                    
                    AuditLogger.LogCirculation("Book Borrowed",
                        $"BookID: {bookId}, MemberID: {memberId}, DueDate: {dueDate:yyyy-MM-dd}, Receipt: {receiptNumber}",
                        "Success");
                    
                    MessageBox.Show($"Book borrowed successfully!\n\n{receiptMessage}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Refresh catalog form if it's open to reflect updated availability
                    RefreshCatalogForm();
                }
                else
                {
                    AuditLogger.LogCirculation("Book Borrow Failed",
                        $"BookID: {bookId}, MemberID: {memberId}, BookAvailable: {book.IsAvailable}, AccessionNo: {book.AccessionNo}",
                        "Failed");
                    
                    // Provide more specific error message
                    string errorMessage = "Failed to borrow book.";
                    if (!book.IsAvailable)
                    {
                        errorMessage = $"Book is not available. Available copies: {book.Available}/{book.TotalCopies}";
                    }
                    else if (string.IsNullOrWhiteSpace(book.AccessionNo))
                    {
                        errorMessage = "Book accession number is missing. Please update the book information.";
                    }
                    else
                    {
                        errorMessage = "Unable to process borrowing. The book may have been borrowed by another user or there was a database error.";
                    }
                    
                    MessageBox.Show(errorMessage, "Borrowing Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Don't throw exception, just show message and return
                }
                txtBorrowMemberID.Text = "Format: MEM-000001 or 1";
                txtBorrowMemberID.ForeColor = Color.Gray;
                txtBorrowBookAccession.Text = "Format: ACC-000001 or 1";
                txtBorrowBookAccession.ForeColor = Color.Gray;
                ClearMemberEligibilityDisplay();
                ClearBookInfoDisplay("Borrow");
                LoadMetrics();
                LoadTransactions();
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError($"Error processing borrowing: {ex.Message}", "Error", ex);
            }
        }
        private void btnProcessReturn_Click(object sender, EventArgs e)
        {
            string bookAccessionText = txtReturnBookAccession.Text.Trim();
            if (bookAccessionText == "Format: ACC-000001 or 1" || bookAccessionText.Contains("Format:") || string.IsNullOrWhiteSpace(bookAccessionText))
            {
                ErrorHandler.ShowValidationError("Please enter a Book Accession Number.");
                txtReturnBookAccession.Focus();
                return;
            }
            try
            {
                int bookId = GetBookIdFromAccession(bookAccessionText);
                if (bookId == 0)
                {
                    ErrorHandler.ShowError("Book not found with the provided accession number.", "Error");
                    return;
                }
                var activeTransaction = _circulationService.GetActiveTransactionByBook(bookId);
                if (activeTransaction == null)
                {
                    ErrorHandler.ShowError("No active borrowing found for this book.", "Error");
                    return;
                }
                int transactionId = activeTransaction.TransactionID;
                
                // Get transaction details for receipt
                var transaction = _transactionRepository.GetById(transactionId);
                var member = _membersService.GetMember(transaction.MemberID);
                var book = _bookService.GetBook(bookId);
                
                // Calculate fine before return
                decimal fine = _finesService.CalculateFine(transactionId);
                bool isOverdue = transaction.DueDate < DateTime.Now;
                int daysOverdue = isOverdue ? (DateTime.Now - transaction.DueDate).Days : 0;
                
                using (var statusForm = new TransactionStatusForm("Book Return"))
                {
                    statusForm.Show();
                    Application.DoEvents();
                    try
                    {
                        statusForm.UpdateStatus("Calculating fine...");
                        if (fine > 0)
                        {
                            statusForm.UpdateStatus($"Fine calculated: {Project5LMS.Helpers.IDFormatter.FormatCurrency(fine)}");
                        }
                        statusForm.UpdateStatus("Processing book return...");
                if (_circulationService.ReturnBook(transactionId))
                {
                    if (fine > 0)
                    {
                                statusForm.UpdateStatus("Updating fine record...");
                        _finesService.UpdateTransactionFine(transactionId, fine);
                    }
                    
                    // Generate return receipt
                    string receiptNumber = IDFormatter.FormatReceiptNumber(DateTime.Now, new Random().Next(100000, 999999));
                    string receiptMessage = _receiptService.GenerateReturnReceipt(member, book, transaction, fine, daysOverdue, receiptNumber);
                            
                            statusForm.UpdateStatus("Transaction completed successfully!");
                            System.Threading.Thread.Sleep(Constants.ThreadSleepShort);
                            statusForm.Close();
                    
                    string fineInfo = fine > 0 ? $", Fine: {Project5LMS.Helpers.IDFormatter.FormatCurrency(fine)}" : "";
                    AuditLogger.LogCirculation("Book Returned",
                        $"BookID: {bookId}, TransactionID: {transactionId}{fineInfo}, Receipt: {receiptNumber}",
                        "Success");
                    
                    MessageBox.Show($"Book returned successfully!\n\n{receiptMessage}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Refresh catalog form if it's open to reflect updated availability
                    RefreshCatalogForm();
                }
                else
                {
                            statusForm.Close();
                    AuditLogger.LogCirculation("Book Return Failed",
                        $"BookID: {bookId}, TransactionID: {transactionId}",
                        "Failed");
                    throw new InvalidOperationException("Failed to return book. Please try again.");
                        }
                    }
                    catch
                    {
                        statusForm.Close();
                        throw;
                    }
                }
                txtReturnBookAccession.Text = "Format: ACC-000001 or 1";
                txtReturnBookAccession.ForeColor = Color.Gray;
                ClearBookInfoDisplay("Return");
                LoadMetrics();
                LoadTransactions();
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError($"Error processing return: {ex.Message}", "Error", ex);
            }
        }
        private int GetBookIdFromAccession(string accessionNo)
        {
            try
            {
                var book = _bookService.GetBookByAccessionNumber(accessionNo);
                if (book != null)
                    return book.BookID;
                int parsedAccession = IDFormatter.ParseAccessionNumber(accessionNo);
                if (parsedAccession > 0)
                {
                    var bookByAcc = _bookService.GetBookByAccessionNumber(IDFormatter.FormatAccessionNumber(parsedAccession.ToString()));
                    if (bookByAcc != null)
                        return bookByAcc.BookID;
                }
                int bookId = IDFormatter.ParseBookID(accessionNo);
                if (bookId > 0)
                {
                    var bookById = _bookService.GetBook(bookId);
                    return bookById != null ? bookId : 0;
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }
        private void txtBorrowMemberID_TextChanged(object sender, EventArgs e)
        {
            // Display member eligibility when member ID is entered (supports partial matching)
            string memberIdText = txtBorrowMemberID.Text.Trim();
            if (string.IsNullOrWhiteSpace(memberIdText) || memberIdText == "Format: MEM-000001 or 1" || memberIdText.Contains("Format:"))
            {
                ClearMemberEligibilityDisplay();
                return;
            }
            
            try
            {
                // Try parsing as complete member ID first
                int memberId = IDFormatter.ParseMemberID(memberIdText);
                if (memberId > 0)
                {
                    DisplayMemberEligibility(memberId);
                    return;
                }
                
                // If parsing fails, try partial search
                // Extract numeric part for partial matching
                string numericPart = System.Text.RegularExpressions.Regex.Replace(memberIdText, @"[^\d]", "");
                if (!string.IsNullOrWhiteSpace(numericPart) && numericPart.Length >= 1)
                {
                    // Search for members with matching ID pattern
                    var searchResults = _membersService.SearchMembers(memberIdText);
                    var matchingMember = searchResults.FirstOrDefault(m => 
                        IDFormatter.FormatMemberID(m.MemberID).IndexOf(memberIdText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        m.MemberID.ToString().Contains(numericPart));
                    
                    if (matchingMember != null)
                    {
                        DisplayMemberEligibility(matchingMember.MemberID);
                        return;
                    }
                }
                
                ClearMemberEligibilityDisplay();
            }
            catch
            {
                ClearMemberEligibilityDisplay();
            }
        }
        
        private void DisplayMemberEligibility(int memberId)
        {
            try
            {
                // Use BorrowingValidator for centralized eligibility checking
                var eligibilityInfo = _borrowingValidator.GetMemberEligibility(memberId);
                if (eligibilityInfo == null)
                {
                    ClearMemberEligibilityDisplay();
                    return;
                }
                
                var member = eligibilityInfo.Member;
                var privileges = MemberTypePrivileges.GetDefaultPrivileges(member.Type);
                int activeBorrowings = eligibilityInfo.ActiveBorrowings;
                int overdueCount = eligibilityInfo.OverdueCount;
                decimal totalFines = eligibilityInfo.TotalFines;
                bool isActive = eligibilityInfo.IsActive;
                bool withinLimit = eligibilityInfo.WithinLimit;
                bool noOverdue = eligibilityInfo.NoOverdue;
                bool finesPaid = eligibilityInfo.FinesPaid;
                bool isEligible = eligibilityInfo.IsEligible;
                
                // Display member information
                if (panelMemberEligibility != null)
                {
                    panelMemberEligibility.Visible = true;
                    
                    // Member Name
                    if (lblMemberName != null)
                    {
                        lblMemberName.Text = $"Name: {eligibilityInfo.Member.FullName}";
                    }
                    
                    // Member Type
                    if (lblMemberType != null)
                    {
                        lblMemberType.Text = $"Type: {eligibilityInfo.Member.Type}";
                    }
                    
                    // Member Status - Display actual Status field from database to match AdminMembersForm
                    if (lblMemberStatus != null)
                    {
                        string actualStatus = member.Status ?? "Active";
                        // If member is expired (ExpirationDate passed), show "Expired" regardless of Status field
                        if (member.IsExpired && (actualStatus.Equals("Active", StringComparison.OrdinalIgnoreCase) || string.IsNullOrWhiteSpace(actualStatus)))
                        {
                            actualStatus = "Expired";
                        }
                        string statusText = $"Status: {actualStatus}";
                        lblMemberStatus.Text = statusText;
                        
                        // Color coding to match AdminMembersForm display
                        switch (actualStatus.ToLower())
                        {
                            case "active":
                                lblMemberStatus.ForeColor = Color.FromArgb(40, 167, 69); // Green
                                break;
                            case "suspended":
                                lblMemberStatus.ForeColor = Color.FromArgb(220, 53, 69); // Red
                                break;
                            case "expired":
                                lblMemberStatus.ForeColor = Color.FromArgb(255, 193, 7); // Yellow/Orange
                                break;
                            default:
                                lblMemberStatus.ForeColor = Color.FromArgb(220, 53, 69); // Red for unknown/inactive
                                break;
                        }
                    }
                    
                    // Current Borrowings
                    if (lblMemberBorrowings != null)
                    {
                        lblMemberBorrowings.Text = $"Borrowings: {activeBorrowings}/{privileges.MaxBooksAllowed}";
                        lblMemberBorrowings.ForeColor = withinLimit ? Color.FromArgb(Constants.ColorNeutralGray, Constants.ColorNeutralGray, Constants.ColorNeutralGray) : Color.FromArgb(Constants.ColorErrorRed, Constants.ColorErrorRed2, Constants.ColorErrorRed3);
                    }
                    
                    // Overdue Books
                    if (lblMemberOverdue != null)
                    {
                        lblMemberOverdue.Text = overdueCount > 0 ? $"Overdue: {overdueCount} book(s)" : "Overdue: None";
                        lblMemberOverdue.ForeColor = noOverdue ? Constants.GetNeutralColor() : Constants.GetErrorColor();
                    }
                    
                    // Fines
                    if (lblMemberFines != null)
                    {
                        lblMemberFines.Text = totalFines > 0 ? $"Fines: {IDFormatter.FormatCurrency(totalFines)}" : "Fines: None";
                        lblMemberFines.ForeColor = finesPaid ? Constants.GetNeutralColor() : Constants.GetErrorColor();
                    }
                    
                    // Eligibility Status - Use eligibility logic (considers Status AND ExpirationDate)
                    if (lblEligibilityStatus != null)
                    {
                        if (isEligible)
                        {
                            lblEligibilityStatus.Text = "✓ ELIGIBLE";
                            lblEligibilityStatus.ForeColor = Constants.GetSuccessColor();
                        }
                        else
                        {
                            var reasons = new List<string>();
                            // Check actual status and expiration separately for clearer messaging
                            string actualStatus = member.Status ?? "Active";
                            if (member.IsExpired && (actualStatus.Equals("Active", StringComparison.OrdinalIgnoreCase) || string.IsNullOrWhiteSpace(actualStatus)))
                            {
                                actualStatus = "Expired";
                            }
                            if (actualStatus.Equals("Suspended", StringComparison.OrdinalIgnoreCase))
                            {
                                reasons.Add("Account suspended");
                            }
                            else if (member.IsExpired)
                            {
                                reasons.Add("Membership expired");
                            }
                            else if (!isActive)
                            {
                                reasons.Add("Account inactive");
                            }
                            if (!withinLimit) reasons.Add("Borrowing limit reached");
                            if (!noOverdue) reasons.Add("Has overdue books");
                            if (!finesPaid) reasons.Add("Unpaid fines");
                            
                            lblEligibilityStatus.Text = $"✗ NOT ELIGIBLE - {string.Join(", ", reasons)}";
                            lblEligibilityStatus.ForeColor = Constants.GetErrorColor();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error displaying member eligibility: {ex.Message}");
                ClearMemberEligibilityDisplay();
            }
        }
        
        private void ClearMemberEligibilityDisplay()
        {
            if (panelMemberEligibility != null)
            {
                panelMemberEligibility.Visible = false;
            }
            
            if (lblMemberName != null) lblMemberName.Text = "";
            if (lblMemberType != null) lblMemberType.Text = "";
            if (lblMemberStatus != null) lblMemberStatus.Text = "";
            if (lblMemberBorrowings != null) lblMemberBorrowings.Text = "";
            if (lblMemberOverdue != null) lblMemberOverdue.Text = "";
            if (lblMemberFines != null) lblMemberFines.Text = "";
            if (lblEligibilityStatus != null) lblEligibilityStatus.Text = "";
        }
        private void lblBorrowBookAccession_Click(object sender, EventArgs e)
        {
        }
        private void dataGridViewTransactions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        private void dataGridViewTransactions_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void panelTableContainer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridViewTransactions_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {

        }
        
        private void txtRenewBookAccession_Enter(object sender, EventArgs e)
        {
            if (txtRenewBookAccession.Text == "Format: ACC-000001 or 1" || txtRenewBookAccession.Text.Contains("Format:"))
            {
                txtRenewBookAccession.Text = "";
                txtRenewBookAccession.ForeColor = Color.Black;
            }
        }
        
        private void txtRenewBookAccession_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRenewBookAccession.Text))
            {
                txtRenewBookAccession.Text = "Scan or enter book accession number...";
                txtRenewBookAccession.ForeColor = Color.Gray;
            }
        }
        
        private void txtRenewBookAccession_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnProcessRenewal_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }
        
        private void txtRenewBookAccession_TextChanged(object sender, EventArgs e)
        {
            // Display book and transaction information when accession is entered
            string bookAccessionText = txtRenewBookAccession.Text.Trim();
            if (string.IsNullOrWhiteSpace(bookAccessionText) || bookAccessionText == "Format: ACC-000001 or 1" || bookAccessionText.Contains("Format:"))
            {
                ClearBookInfoDisplay("Renew");
                return;
            }
            
            try
            {
                DisplayBookInfo(bookAccessionText, "Renew");
            }
            catch
            {
                ClearBookInfoDisplay("Renew");
            }
        }
        
        private void btnProcessRenewal_Click(object sender, EventArgs e)
        {
            string bookAccessionText = txtRenewBookAccession.Text.Trim();
            if (bookAccessionText == "Format: ACC-000001 or 1" || bookAccessionText.Contains("Format:") || string.IsNullOrWhiteSpace(bookAccessionText))
            {
                MessageBox.Show("Please enter a Book Accession Number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRenewBookAccession.Focus();
                return;
            }
            
            try
            {
                int bookId = GetBookIdFromAccession(bookAccessionText);
                if (bookId == 0)
                {
                    MessageBox.Show("Book not found with the provided accession number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // Get active transaction for this book
                var activeTransaction = _circulationService.GetActiveTransactionByBook(bookId);
                if (activeTransaction == null)
                {
                    ErrorHandler.ShowError("No active borrowing found for this book.", "Error");
                    return;
                }
                
                int transactionId = activeTransaction.TransactionID;
                
                // Check renewal eligibility
                if (!_circulationService.CanRenew(transactionId))
                {
                    var transaction = _transactionRepository.GetById(transactionId);
                    var member = _membersService.GetMember(transaction.MemberID);
                    if (member != null)
                    {
                        var privileges = MemberTypePrivileges.GetDefaultPrivileges(member.Type);
                        if (transaction.RenewalCount >= privileges.RenewalLimit)
                        {
                            ErrorHandler.ShowWarning($"Renewal limit reached ({transaction.RenewalCount}/{privileges.RenewalLimit} renewals).");
                            return;
                        }
                    }
                    MessageBox.Show("This book cannot be renewed. Please check renewal eligibility.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // Check for active reservations
                if (_reservationService.HasActiveReservations(bookId))
                {
                    MessageBox.Show("This book has active reservations and cannot be renewed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // Process renewal
                var transactionForRenewal = _transactionRepository.GetById(transactionId);
                var memberForRenewal = _membersService.GetMember(transactionForRenewal.MemberID);
                var memberPrivileges = MemberTypePrivileges.GetDefaultPrivileges(memberForRenewal.Type);
                
                if (_circulationService.RenewBook(transactionId, memberPrivileges.BorrowingPeriodDays))
                {
                    var updatedTransaction = _transactionRepository.GetById(transactionId);
                    var book = _bookService.GetBook(bookId);
                    
                    AuditLogger.LogCirculation("Book Renewed",
                        $"BookID: {bookId}, TransactionID: {transactionId}, NewDueDate: {updatedTransaction.DueDate:yyyy-MM-dd}, RenewalCount: {updatedTransaction.RenewalCount}",
                        "Success");
                    
                    MessageBox.Show($"Book renewed successfully!\n\n" +
                        $"New Due Date: {updatedTransaction.DueDate:yyyy-MM-dd}\n" +
                        $"Renewals Used: {updatedTransaction.RenewalCount}/{memberPrivileges.RenewalLimit}",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Refresh catalog form if it's open to reflect updated status
                    RefreshCatalogForm();
                    
                    txtRenewBookAccession.Text = "Format: ACC-000001 or 1";
                    txtRenewBookAccession.ForeColor = Color.Gray;
                    ClearBookInfoDisplay("Renew");
                    LoadMetrics();
                    LoadTransactions();
                }
                else
                {
                    AuditLogger.LogCirculation("Book Renewal Failed",
                        $"BookID: {bookId}, TransactionID: {transactionId}",
                        "Failed");
                    throw new InvalidOperationException("Failed to renew book. Please try again.");
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError($"Error processing renewal: {ex.Message}", "Error", ex);
            }
        }
        
        private void DisplayBookInfo(string bookAccessionText, string tabName)
        {
            try
            {
                // Try to get book by accession number (supports partial matching)
                Book book = null;
                int bookId = 0;
                
                // First try exact match
                book = _bookService.GetBookByAccessionNumber(bookAccessionText);
                if (book != null)
                {
                    bookId = book.BookID;
                }
                else
                {
                    // Try parsing as ID
                    bookId = GetBookIdFromAccession(bookAccessionText);
                    if (bookId > 0)
                    {
                        book = _bookService.GetBook(bookId);
                    }
                    else
                    {
                        // Try partial search
                        var searchResults = _bookService.SearchBooks(bookAccessionText);
                        book = searchResults.FirstOrDefault(b => 
                            (b.AccessionNo != null && b.AccessionNo.IndexOf(bookAccessionText, StringComparison.OrdinalIgnoreCase) >= 0) ||
                            (b.Title != null && b.Title.IndexOf(bookAccessionText, StringComparison.OrdinalIgnoreCase) >= 0));
                        if (book != null)
                        {
                            bookId = book.BookID;
                        }
                    }
                }
                
                if (book == null || bookId == 0)
                {
                    ClearBookInfoDisplay(tabName);
                    return;
                }
                
                // Display book information based on tab
                if (tabName == "Borrow")
                {
                    DisplayBorrowBookInfo(book);
                }
                else if (tabName == "Return")
                {
                    DisplayReturnBookInfo(book, bookId);
                }
                else if (tabName == "Renew")
                {
                    DisplayRenewBookInfo(book, bookId);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error displaying book info: {ex.Message}");
                ClearBookInfoDisplay(tabName);
            }
        }
        
        private void DisplayBorrowBookInfo(Book book)
        {
            // For borrow tab, show book availability and basic info
            if (panelBorrowBookInfo != null)
            {
                panelBorrowBookInfo.Visible = true;
                
                if (lblBorrowBookTitle != null)
                    lblBorrowBookTitle.Text = $"Title: {book.Title}";
                if (lblBorrowBookAuthor != null)
                    lblBorrowBookAuthor.Text = $"Author: {book.Author}";
                if (lblBorrowBookStatus != null)
                {
                    lblBorrowBookStatus.Text = book.IsAvailable ? "Status: Available" : "Status: Not Available";
                    lblBorrowBookStatus.ForeColor = book.IsAvailable ? Constants.GetSuccessColor() : Constants.GetErrorColor();
                }
                if (lblBorrowBookCopies != null)
                    lblBorrowBookCopies.Text = $"Copies: {book.Available}/{book.TotalCopies}";
            }
        }
        
        // Helper class to hold active borrower information
        private class ActiveBorrowerInfo
        {
            public int TransactionID { get; set; }
            public int MemberID { get; set; }
            public string MemberName { get; set; }
            public DateTime DueDate { get; set; }
            public decimal FineAmount { get; set; }
            public string AccessionNumber { get; set; }
            public string Barcode { get; set; }
            public DateTime BorrowDate { get; set; }
        }
        
        // Fetch all active borrowers for a book from the catalog
        private List<ActiveBorrowerInfo> GetAllActiveBorrowersForBook(int bookId)
        {
            List<ActiveBorrowerInfo> borrowers = new List<ActiveBorrowerInfo>();
            
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    // Query all active transactions for this book with member information
                    // Note: BookCopies are not directly linked to Transactions, so we'll get copies separately
                    string query = @"SELECT 
                                        t.TransactionID,
                                        t.MemberID,
                                        t.BorrowDate,
                                        t.DueDate,
                                        t.Fine,
                                        CONCAT(m.FirstName, ' ', m.LastName) AS MemberName
                                    FROM Transactions t
                                    INNER JOIN Members m ON t.MemberID = m.MemberID
                                    WHERE t.BookID = @BookID 
                                    AND (t.Status = 'Borrowed' OR t.Status = 'Active')
                                    ORDER BY t.DueDate ASC";
                    
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookID", bookId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var borrower = new ActiveBorrowerInfo
                                {
                                    TransactionID = reader.GetInt32("TransactionID"),
                                    MemberID = reader.GetInt32("MemberID"),
                                    MemberName = reader["MemberName"]?.ToString() ?? "Unknown",
                                    BorrowDate = reader.GetDateTime("BorrowDate"),
                                    DueDate = reader.GetDateTime("DueDate"),
                                    FineAmount = reader["Fine"] != DBNull.Value ? reader.GetDecimal("Fine") : 0m,
                                    AccessionNumber = null, // Will be populated separately if needed
                                    Barcode = null // Will be populated separately if needed
                                };
                                
                                // Calculate fine if not already set in transaction
                                if (borrower.FineAmount == 0)
                                {
                                    borrower.FineAmount = _finesService.CalculateFine(borrower.TransactionID);
                                }
                                
                                borrowers.Add(borrower);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error fetching active borrowers for book {bookId}: {ex.Message}");
            }
            
            return borrowers;
        }
        
        private void DisplayReturnBookInfo(Book book, int bookId)
        {
            // For return tab, show book info and ALL active transaction details (fetching updated catalog, reservations, and copies)
            if (panelReturnBookInfo != null)
            {
                panelReturnBookInfo.Controls.Clear();
                
                // Refresh book data from catalog to get latest information
                book = _bookService.GetBook(bookId);
                if (book == null)
                {
                    Label lblError = new Label
                    {
                        Text = "Book not found in catalog.",
                        Font = new Font("Segoe UI", 9F),
                        ForeColor = Color.FromArgb(220, 20, 60),
                        AutoSize = false,
                        Location = new Point(5, 5),
                        Size = new Size(panelReturnBookInfo.Width - 10, 20)
                    };
                    panelReturnBookInfo.Controls.Add(lblError);
                panelReturnBookInfo.Visible = true;
                    return;
                }
                
                // Get updated book copies information
                var bookCopies = _bookCopyRepository.GetByBookId(bookId).ToList();
                int totalCopies = bookCopies.Count;
                int availableCopies = bookCopies.Count(c => c.IsAvailable);
                int borrowedCopies = bookCopies.Count(c => c.IsBorrowed);
                int reservedCopies = bookCopies.Count(c => c.IsReserved);
                
                // Get reservations for this book
                var reservations = _reservationService.GetBookReservations(bookId).ToList();
                var activeReservations = reservations.Where(r => r.Status == "Pending" || r.Status == "Active" || r.Status == "Ready").ToList();
                
                int yPos = 5;
                int labelHeight = 20;
                int spacing = 6;
                int labelWidth = panelReturnBookInfo.Width - 10;
                
                // Book Information (from updated catalog)
                Label lblTitle = new Label
                {
                    Text = $"Title: {book.Title}",
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(64, 64, 64),
                    AutoSize = false,
                    Location = new Point(5, yPos),
                    Size = new Size(labelWidth, labelHeight),
                    MaximumSize = new Size(labelWidth, 0)
                };
                panelReturnBookInfo.Controls.Add(lblTitle);
                yPos += labelHeight + spacing;
                
                Label lblAuthor = new Label
                {
                    Text = $"Author: {book.Author}",
                    Font = new Font("Segoe UI", 9F),
                    ForeColor = Color.FromArgb(64, 64, 64),
                    AutoSize = false,
                    Location = new Point(5, yPos),
                    Size = new Size(labelWidth, labelHeight),
                    MaximumSize = new Size(labelWidth, 0)
                };
                panelReturnBookInfo.Controls.Add(lblAuthor);
                yPos += labelHeight + spacing;
                
                // Updated copies information from BookCopies table
                Label lblCopies = new Label
                {
                    Text = $"Copies: {availableCopies}/{totalCopies} Available | {borrowedCopies} Borrowed | {reservedCopies} Reserved",
                    Font = new Font("Segoe UI", 9F),
                    ForeColor = Color.FromArgb(64, 64, 64),
                    AutoSize = false,
                    Location = new Point(5, yPos),
                    Size = new Size(labelWidth, labelHeight)
                };
                panelReturnBookInfo.Controls.Add(lblCopies);
                yPos += labelHeight + spacing;
                
                // Show reservations if any
                if (activeReservations.Count > 0)
                {
                    Label lblReservations = new Label
                    {
                        Text = $"Active Reservations ({activeReservations.Count}):",
                        Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                        ForeColor = Color.FromArgb(255, 140, 0),
                        AutoSize = false,
                        Location = new Point(5, yPos),
                        Size = new Size(labelWidth, labelHeight)
                    };
                    panelReturnBookInfo.Controls.Add(lblReservations);
                    yPos += labelHeight + spacing;
                    
                    foreach (var reservation in activeReservations.Take(5)) // Show max 5 reservations
                    {
                        var reservingMember = _membersService.GetMember(reservation.MemberID);
                        string reservationInfo = $"• {reservingMember?.FullName ?? "Unknown"} - {reservation.Status}";
                        if (reservation.PickupDate.HasValue)
                        {
                            reservationInfo += $" (Pickup: {reservation.PickupDate.Value:yyyy-MM-dd})";
                        }
                        
                        Label lblReservation = new Label
                        {
                            Text = reservationInfo,
                            Font = new Font("Segoe UI", 8.5F),
                            ForeColor = Color.FromArgb(255, 140, 0),
                            AutoSize = false,
                            Location = new Point(10, yPos),
                            Size = new Size(labelWidth - 5, labelHeight)
                        };
                        panelReturnBookInfo.Controls.Add(lblReservation);
                        yPos += labelHeight + 2;
                    }
                    if (activeReservations.Count > 5)
                    {
                        Label lblMoreReservations = new Label
                        {
                            Text = $"  ... and {activeReservations.Count - 5} more",
                            Font = new Font("Segoe UI", 8F, FontStyle.Italic),
                            ForeColor = Color.FromArgb(128, 128, 128),
                            AutoSize = false,
                            Location = new Point(10, yPos),
                            Size = new Size(labelWidth - 5, labelHeight)
                        };
                        panelReturnBookInfo.Controls.Add(lblMoreReservations);
                        yPos += labelHeight + 2;
                    }
                    yPos += spacing;
                }
                
                yPos += spacing; // Extra spacing before borrowers
                
                // Show borrowed copies information
                var borrowedCopiesList = bookCopies.Where(c => c.IsBorrowed).ToList();
                if (borrowedCopiesList.Count > 0)
                {
                    Label lblBorrowedCopiesHeader = new Label
                    {
                        Text = $"Borrowed Copies ({borrowedCopiesList.Count}):",
                        Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                        ForeColor = Color.FromArgb(0, 102, 204),
                        AutoSize = false,
                        Location = new Point(5, yPos),
                        Size = new Size(labelWidth, labelHeight)
                    };
                    panelReturnBookInfo.Controls.Add(lblBorrowedCopiesHeader);
                    yPos += labelHeight + spacing;
                    
                    foreach (var copy in borrowedCopiesList)
                    {
                        string copyInfo = !string.IsNullOrEmpty(copy.AccessionNumber) 
                            ? $"• {copy.AccessionNumber}" 
                            : $"• Copy ID: {copy.CopyID}";
                        if (!string.IsNullOrEmpty(copy.Barcode))
                        {
                            copyInfo += $" (Barcode: {copy.Barcode})";
                        }
                        if (copy.LastCheckedOut.HasValue)
                        {
                            copyInfo += $" - Borrowed: {copy.LastCheckedOut.Value:yyyy-MM-dd}";
                        }
                        
                        Label lblCopy = new Label
                        {
                            Text = copyInfo,
                            Font = new Font("Segoe UI", 8.5F),
                            ForeColor = Color.FromArgb(64, 64, 64),
                            AutoSize = false,
                            Location = new Point(10, yPos),
                            Size = new Size(labelWidth - 5, labelHeight)
                        };
                        panelReturnBookInfo.Controls.Add(lblCopy);
                        yPos += labelHeight + 2;
                    }
                    yPos += spacing;
                }
                
                // Fetch ALL active transactions for this book from the catalog
                List<ActiveBorrowerInfo> activeBorrowers = GetAllActiveBorrowersForBook(bookId);
                
                if (activeBorrowers != null && activeBorrowers.Count > 0)
                {
                    // Add separator label
                    Label lblBorrowersHeader = new Label
                    {
                        Text = $"Active Borrowers ({activeBorrowers.Count}):",
                        Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                        ForeColor = Color.FromArgb(0, 102, 204),
                        AutoSize = false,
                        Location = new Point(5, yPos),
                        Size = new Size(labelWidth, labelHeight)
                    };
                    panelReturnBookInfo.Controls.Add(lblBorrowersHeader);
                    yPos += labelHeight + spacing;
                    
                    // Display each active borrower
                    foreach (var borrower in activeBorrowers)
                    {
                        // Borrower name
                        Label lblBorrower = new Label
                        {
                            Text = $"• {borrower.MemberName}",
                            Font = new Font("Segoe UI", 9F),
                            ForeColor = Color.FromArgb(64, 64, 64),
                            AutoSize = false,
                            Location = new Point(10, yPos),
                            Size = new Size(labelWidth - 5, labelHeight)
                        };
                        panelReturnBookInfo.Controls.Add(lblBorrower);
                        yPos += labelHeight + 2;
                        
                        // Due date and overdue status
                        bool isOverdue = borrower.DueDate < DateTime.Now;
                        int daysOverdue = isOverdue ? (DateTime.Now - borrower.DueDate).Days : 0;
                        string statusText = isOverdue ? $"Due: {borrower.DueDate:yyyy-MM-dd} ({daysOverdue} days overdue)" 
                                                      : $"Due: {borrower.DueDate:yyyy-MM-dd}";
                        
                        Label lblBorrowerDetails = new Label
                        {
                            Text = $"  {statusText}",
                            Font = new Font("Segoe UI", 8.5F),
                            ForeColor = isOverdue ? Color.FromArgb(220, 20, 60) : Color.FromArgb(64, 64, 64),
                            AutoSize = false,
                            Location = new Point(15, yPos),
                            Size = new Size(labelWidth - 10, labelHeight)
                        };
                        panelReturnBookInfo.Controls.Add(lblBorrowerDetails);
                        yPos += labelHeight + 2;
                        
                        // Fine amount if applicable
                        if (borrower.FineAmount > 0)
                        {
                            Label lblBorrowerFine = new Label
                            {
                                Text = $"  Fine: {IDFormatter.FormatCurrency(borrower.FineAmount)}",
                                Font = new Font("Segoe UI", 8.5F),
                                ForeColor = Color.FromArgb(220, 20, 60),
                                AutoSize = false,
                                Location = new Point(15, yPos),
                                Size = new Size(labelWidth - 10, labelHeight)
                            };
                            panelReturnBookInfo.Controls.Add(lblBorrowerFine);
                            yPos += labelHeight + 2;
                        }
                        
                        yPos += spacing; // Extra spacing between borrowers
                    }
                    
                    // Adjust panel height to fit all content
                    panelReturnBookInfo.Height = Math.Min(yPos + 10, 400); // Max height 400, scroll if needed
                    panelReturnBookInfo.AutoScroll = true;
                }
                else
                {
                    Label lblNoBorrowing = new Label
                    {
                        Text = "Status: No active borrowing",
                        Font = new Font("Segoe UI", 9F),
                        ForeColor = Color.FromArgb(128, 128, 128),
                        AutoSize = false,
                        Location = new Point(5, yPos),
                        Size = new Size(labelWidth, labelHeight)
                    };
                    panelReturnBookInfo.Controls.Add(lblNoBorrowing);
                }
                
                panelReturnBookInfo.Visible = true;
            }
        }
        
        private void DisplayRenewBookInfo(Book book, int bookId)
        {
            // For renew tab, show book info and renewal eligibility
            // First refresh book data from catalog to ensure we have latest information
            book = _bookService.GetBook(bookId);
            if (book == null)
            {
                if (panelRenewBookInfo != null)
                {
                    panelRenewBookInfo.Visible = true;
                    if (lblRenewBookTitle != null)
                        lblRenewBookTitle.Text = "Book not found in catalog.";
                    if (lblRenewEligibility != null)
                        lblRenewEligibility.Text = "";
                }
                return;
            }
            
            if (panelRenewBookInfo != null)
            {
                panelRenewBookInfo.Visible = true;
                
                var activeTransaction = _circulationService.GetActiveTransactionByBook(bookId);
                
                if (lblRenewBookTitle != null)
                    lblRenewBookTitle.Text = $"Title: {book.Title}";
                if (lblRenewBookAuthor != null)
                    lblRenewBookAuthor.Text = $"Author: {book.Author}";
                
                if (activeTransaction != null)
                {
                    var member = _membersService.GetMember(activeTransaction.MemberID);
                    var privileges = MemberTypePrivileges.GetDefaultPrivileges(member?.Type ?? "Student");
                    bool canRenew = _circulationService.CanRenew(activeTransaction.TransactionID);
                    
                    // Explicitly fetch and check reservations from database before displaying eligibility
                    var allBookReservations = _reservationService.GetBookReservations(bookId).ToList();
                    // Filter to only truly active reservations (not expired, cancelled, or fulfilled)
                    var activeReservations = allBookReservations.Where(r => 
                        !r.IsCancelled && 
                        !r.IsFulfilled && 
                        !r.IsExpired && 
                        (r.IsPending || r.IsActive || r.IsReady)).ToList();
                    
                    bool hasReservations = activeReservations.Count > 0;
                    
                    if (lblRenewMemberName != null)
                        lblRenewMemberName.Text = $"Borrower: {member?.FullName ?? "Unknown"}";
                    if (lblRenewDueDate != null)
                        lblRenewDueDate.Text = $"Current Due Date: {activeTransaction.DueDate:yyyy-MM-dd}";
                    if (lblRenewRenewals != null)
                        lblRenewRenewals.Text = $"Renewals: {activeTransaction.RenewalCount}/{privileges.RenewalLimit}";
                    if (lblRenewEligibility != null)
                    {
                        if (canRenew && !hasReservations)
                        {
                            lblRenewEligibility.Text = "✓ Eligible for Renewal";
                            lblRenewEligibility.ForeColor = Constants.GetSuccessColor();
                        }
                        else
                        {
                            var reasons = new List<string>();
                            if (!canRenew) reasons.Add("Renewal limit reached");
                            // Only show "Has active reservations" if there are actually active reservations
                            if (hasReservations)
                            {
                                reasons.Add($"Has {activeReservations.Count} active reservation(s)");
                            }
                            lblRenewEligibility.Text = reasons.Count > 0 
                                ? $"✗ Not Eligible - {string.Join(", ", reasons)}"
                                : "✓ Eligible for Renewal";
                            lblRenewEligibility.ForeColor = reasons.Count > 0 
                                ? Constants.GetErrorColor() 
                                : Constants.GetSuccessColor();
                        }
                    }
                }
                else
                {
                    if (lblRenewMemberName != null)
                        lblRenewMemberName.Text = "Status: No active borrowing";
                    if (lblRenewDueDate != null)
                        lblRenewDueDate.Text = "";
                    if (lblRenewRenewals != null)
                        lblRenewRenewals.Text = "";
                    if (lblRenewEligibility != null)
                        lblRenewEligibility.Text = "";
                }
            }
        }
        
        /// <summary>
        /// Refreshes the AdminCatalogForm if it's currently open
        /// This ensures catalog data grid and KPI cards reflect latest borrowing/reservation status
        /// </summary>
        private void RefreshCatalogForm()
        {
            try
            {
                // Find the AdminCatalogForm in the open forms
                foreach (Form form in Application.OpenForms)
                {
                    if (form is Project5LMS.Forms.Admin.Catalog.AdminCatalogForm catalogForm)
                    {
                        catalogForm.RefreshCatalog();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error refreshing catalog form: {ex.Message}");
            }
        }
        
        private void ClearBookInfoDisplay(string tabName)
        {
            if (tabName == "Borrow")
            {
                if (panelBorrowBookInfo != null) panelBorrowBookInfo.Visible = false;
                if (lblBorrowBookTitle != null) lblBorrowBookTitle.Text = "";
                if (lblBorrowBookAuthor != null) lblBorrowBookAuthor.Text = "";
                if (lblBorrowBookStatus != null) lblBorrowBookStatus.Text = "";
                if (lblBorrowBookCopies != null) lblBorrowBookCopies.Text = "";
            }
            else if (tabName == "Return")
            {
                if (panelReturnBookInfo != null)
                {
                    panelReturnBookInfo.Controls.Clear();
                    panelReturnBookInfo.Visible = false;
                }
            }
            else if (tabName == "Renew")
            {
                if (panelRenewBookInfo != null) panelRenewBookInfo.Visible = false;
                if (lblRenewBookTitle != null) lblRenewBookTitle.Text = "";
                if (lblRenewBookAuthor != null) lblRenewBookAuthor.Text = "";
                if (lblRenewMemberName != null) lblRenewMemberName.Text = "";
                if (lblRenewDueDate != null) lblRenewDueDate.Text = "";
                if (lblRenewRenewals != null) lblRenewRenewals.Text = "";
                    if (lblRenewEligibility != null) lblRenewEligibility.Text = "";
            }
        }
        
    }
}