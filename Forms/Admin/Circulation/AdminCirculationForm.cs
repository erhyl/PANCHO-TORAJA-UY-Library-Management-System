using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Project5LMS.Helpers;
using Project5LMS.Services;
using Project5LMS.Data;
using Project5LMS.Interfaces;

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
        }

        private void AdminCirculationForm_Load(object sender, EventArgs e)
        {
            EnsureTransactionsTableExists();
            SetupDataGridView();
            LoadMetrics();
            LoadTransactions();
            tabControl.SelectedIndex = 0;
            ShowTabContent(0);
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
                Width = 120,
                ReadOnly = true
            };
            dataGridViewTransactions.Columns.Add(colTransactionID);

            DataGridViewColumn colType = new DataGridViewTextBoxColumn
            {
                Name = "Type",
                HeaderText = "TYPE",
                DataPropertyName = "Type",
                Width = 120,
                ReadOnly = true
            };
            dataGridViewTransactions.Columns.Add(colType);

            DataGridViewTextBoxColumn colMember = new DataGridViewTextBoxColumn
            {
                Name = "Member",
                HeaderText = "MEMBER",
                DataPropertyName = "Member",
                Width = 200,
                ReadOnly = true
            };
            dataGridViewTransactions.Columns.Add(colMember);

            DataGridViewTextBoxColumn colBook = new DataGridViewTextBoxColumn
            {
                Name = "Book",
                HeaderText = "BOOK",
                DataPropertyName = "Book",
                Width = 250,
                ReadOnly = true
            };
            dataGridViewTransactions.Columns.Add(colBook);

            DataGridViewTextBoxColumn colBorrowDate = new DataGridViewTextBoxColumn
            {
                Name = "BorrowDate",
                HeaderText = "BORROW DATE",
                DataPropertyName = "BorrowDate",
                Width = 120,
                ReadOnly = true
            };
            dataGridViewTransactions.Columns.Add(colBorrowDate);

            DataGridViewTextBoxColumn colDueDate = new DataGridViewTextBoxColumn
            {
                Name = "DueDate",
                HeaderText = "DUE DATE",
                DataPropertyName = "DueDate",
                Width = 120,
                ReadOnly = true
            };
            dataGridViewTransactions.Columns.Add(colDueDate);

            DataGridViewColumn colStatus = new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "STATUS",
                DataPropertyName = "Status",
                Width = 120,
                ReadOnly = true
            };
            dataGridViewTransactions.Columns.Add(colStatus);

            DataGridViewTextBoxColumn colFine = new DataGridViewTextBoxColumn
            {
                Name = "Fine",
                HeaderText = "FINE",
                DataPropertyName = "Fine",
                Width = 100,
                ReadOnly = true
            };
            dataGridViewTransactions.Columns.Add(colFine);

            dataGridViewTransactions.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridViewTransactions.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridViewTransactions.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dataGridViewTransactions.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            dataGridViewTransactions.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            dataGridViewTransactions.RowTemplate.Height = 50;
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
                    e.Value = $"TXN-{transactionIdStr.PadLeft(3, '0')}";
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
                        icon = "?";
                        iconColor = Color.FromArgb(13, 110, 253);
                        break;
                    case "return":
                        icon = "?";
                        iconColor = Color.FromArgb(40, 167, 69);
                        break;
                    case "renew":
                        icon = "??";
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

                    using (Font font = new Font("Segoe UI", 14, FontStyle.Bold))
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
                if (!allTransactionsData.Columns.Contains("Fine"))
                {
                    allTransactionsData.Columns.Add("Fine", typeof(string));
                }

                foreach (DataRow row in allTransactionsData.Rows)
                {

                    string transactionType = row["TransactionType"] != DBNull.Value ? row["TransactionType"].ToString() : "Borrow";
                    row["Type"] = transactionType;

                    string firstName = row["FirstName"] != DBNull.Value ? row["FirstName"].ToString() : "";
                    string lastName = row["LastName"] != DBNull.Value ? row["LastName"].ToString() : "";
                    int memberId = Convert.ToInt32(row["MemberID"]);
                    row["Member"] = $"{firstName} {lastName} (MEM-{memberId.ToString().PadLeft(3, '0')})".Trim();

                    string bookTitle = row["Title"] != DBNull.Value ? row["Title"].ToString() : "";
                    int bookId = Convert.ToInt32(row["BookID"]);
                    // Try to get Barcode or AccessionNo from the row
                    string barcode = "";
                    if (row.Table.Columns.Contains("Barcode") && row["Barcode"] != DBNull.Value)
                    {
                        barcode = row["Barcode"].ToString();
                    }
                    else if (row.Table.Columns.Contains("AccessionNo") && row["AccessionNo"] != DBNull.Value)
                    {
                        barcode = row["AccessionNo"].ToString();
                    }
                    string accessionNo = !string.IsNullOrEmpty(barcode) ? barcode : $"ACC-{bookId.ToString().PadLeft(4, '0')}";
                    row["Book"] = $"{bookTitle} ({accessionNo})";

                    if (row["Fine"] != DBNull.Value && Convert.ToDecimal(row["Fine"]) > 0)
                    {
                        row["Fine"] = $"${Convert.ToDecimal(row["Fine"]):F2}";
                    }
                    else
                    {
                        row["Fine"] = "-";
                    }

                    if (row["Status"].ToString() == "Borrowed" || row["Status"].ToString() == "Active")
                    {
                        if (row["DueDate"] != DBNull.Value)
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

                dataGridViewTransactions.DataSource = allTransactionsData;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading transactions: {ex.Message}");
                MessageBox.Show($"Error loading transactions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                
                // Use Barcode if it exists, otherwise use AccessionNo if it exists, otherwise use BookID
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
                             LIMIT 100";
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
                             LIMIT 100";
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
                             LIMIT 100";
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

        }

        private void txtBorrowMemberID_Enter(object sender, EventArgs e)
        {
            if (txtBorrowMemberID.Text == "Scan or enter member ID...")
            {
                txtBorrowMemberID.Text = "";
                txtBorrowMemberID.ForeColor = Color.Black;
            }
        }

        private void txtBorrowMemberID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBorrowMemberID.Text))
            {
                txtBorrowMemberID.Text = "Scan or enter member ID...";
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
            if (txtBorrowBookAccession.Text == "Scan or enter book accession number...")
            {
                txtBorrowBookAccession.Text = "";
                txtBorrowBookAccession.ForeColor = Color.Black;
            }
        }

        private void txtBorrowBookAccession_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBorrowBookAccession.Text))
            {
                txtBorrowBookAccession.Text = "Scan or enter book accession number...";
                txtBorrowBookAccession.ForeColor = Color.Gray;
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
            if (txtReturnBookAccession.Text == "Scan or enter book accession number...")
            {
                txtReturnBookAccession.Text = "";
                txtReturnBookAccession.ForeColor = Color.Black;
            }
        }

        private void txtReturnBookAccession_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtReturnBookAccession.Text))
            {
                txtReturnBookAccession.Text = "Scan or enter book accession number...";
                txtReturnBookAccession.ForeColor = Color.Gray;
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

            if (memberIdText == "Scan or enter member ID..." || string.IsNullOrWhiteSpace(memberIdText))
            {
                MessageBox.Show("Please enter a Member ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBorrowMemberID.Focus();
                return;
            }

            if (bookAccessionText == "Scan or enter book accession number..." || string.IsNullOrWhiteSpace(bookAccessionText))
            {
                MessageBox.Show("Please enter a Book Accession Number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBorrowBookAccession.Focus();
                return;
            }

            try
            {

                int memberId = 0;
                if (memberIdText.StartsWith("MEM-"))
                {
                    string idPart = memberIdText.Replace("MEM-", "");
                    int.TryParse(idPart, out memberId);
                }
                else
                {
                    int.TryParse(memberIdText, out memberId);
                }

                if (memberId == 0)
                {
                    MessageBox.Show("Invalid Member ID format.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int bookId = GetBookIdFromAccession(bookAccessionText);
                if (bookId == 0)
                {
                    MessageBox.Show("Book not found with the provided accession number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!_membersService.MemberExists(memberId))
                {
                    MessageBox.Show("Member not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!_bookService.IsBookAvailable(bookId))
                {
                    MessageBox.Show("Book is not available for borrowing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!_circulationService.CanBorrow(memberId))
                {
                    MessageBox.Show("Member has reached their borrowing limit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_circulationService.BorrowBook(memberId, bookId, 14))
                {
                    var transaction = _circulationService.GetActiveTransactionByBook(bookId);
                    DateTime dueDate = transaction != null ? transaction.DueDate : DateTime.Now.AddDays(14);

                    AuditLogger.LogCirculation("Book Borrowed", 
                        $"BookID: {bookId}, MemberID: {memberId}, DueDate: {dueDate:yyyy-MM-dd}", 
                        "Success");
                }
                else
                {
                    AuditLogger.LogCirculation("Book Borrow Failed", 
                        $"BookID: {bookId}, MemberID: {memberId}", 
                        "Failed");
                    throw new InvalidOperationException("Failed to borrow book. Please try again.");
                }

                MessageBox.Show("Book borrowed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtBorrowMemberID.Text = "Scan or enter member ID...";
                txtBorrowMemberID.ForeColor = Color.Gray;
                txtBorrowBookAccession.Text = "Scan or enter book accession number...";
                txtBorrowBookAccession.ForeColor = Color.Gray;

                LoadMetrics();
                LoadTransactions();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing borrowing: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnProcessReturn_Click(object sender, EventArgs e)
        {
            string bookAccessionText = txtReturnBookAccession.Text.Trim();

            if (bookAccessionText == "Scan or enter book accession number..." || string.IsNullOrWhiteSpace(bookAccessionText))
            {
                MessageBox.Show("Please enter a Book Accession Number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtReturnBookAccession.Focus();
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

                var activeTransaction = _circulationService.GetActiveTransactionByBook(bookId);
                if (activeTransaction == null)
                {
                    MessageBox.Show("No active borrowing found for this book.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int transactionId = activeTransaction.TransactionID;
                decimal fine = _finesService.CalculateFine(transactionId);

                if (_circulationService.ReturnBook(transactionId))
                {
                    if (fine > 0)
                    {
                        _finesService.UpdateTransactionFine(transactionId, fine);
                    }

                    string fineInfo = fine > 0 ? $", Fine: ${fine:F2}" : "";
                    AuditLogger.LogCirculation("Book Returned", 
                        $"BookID: {bookId}, TransactionID: {transactionId}{fineInfo}", 
                        "Success");

                    string message = fine > 0 
                        ? $"Book returned successfully!\n\nFine: ${fine:F2}" 
                        : "Book returned successfully!";
                    MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    AuditLogger.LogCirculation("Book Return Failed", 
                        $"BookID: {bookId}, TransactionID: {transactionId}", 
                        "Failed");
                    throw new InvalidOperationException("Failed to return book. Please try again.");
                }

                txtReturnBookAccession.Text = "Scan or enter book accession number...";
                txtReturnBookAccession.ForeColor = Color.Gray;

                LoadMetrics();
                LoadTransactions();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing return: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetBookIdFromAccession(string accessionNo)
        {
            try
            {
                string cleanAccession = accessionNo.Replace("ACC-", "").Trim();
                var book = _bookService.GetBookByAccessionNumber(accessionNo);
                if (book != null)
                    return book.BookID;

                if (int.TryParse(cleanAccession, out int bookId))
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

        }

        private void lblBorrowBookAccession_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewTransactions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
