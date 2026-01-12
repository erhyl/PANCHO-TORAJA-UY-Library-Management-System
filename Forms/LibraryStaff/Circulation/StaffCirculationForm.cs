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
using Project5LMS.Forms.Admin.Search;
namespace Project5LMS.Forms.LibraryStaff.Circulation
{
    public partial class StaffCirculationForm : Form
    {
        private string currentFilter = "All";
        private readonly ICirculationService _circulationService;
        private readonly IFinesService _finesService;
        private readonly IBookService _bookService;
        private readonly IMembersService _membersService;
        private readonly DatabaseContext _dbContext;
        public StaffCirculationForm()
        {
            InitializeComponent();
            try
            {
                AccessControlHelper.RequireAnyRole("LibraryStaff", "Admin");
                AuditLogger.LogAccessControl("StaffCirculationForm accessed", $"User: {CurrentUser.Email}", "Success");
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                AuditLogger.LogAccessControl("StaffCirculationForm access denied", $"User: {CurrentUser.Email}", "Failed");
                this.Close();
                return;
            }
            _dbContext = ServiceFactory.GetDbContext();
            _circulationService = ServiceFactory.CreateCirculationService();
            _finesService = ServiceFactory.CreateFinesService();
            _bookService = ServiceFactory.CreateBookService();
            _membersService = ServiceFactory.CreateMembersService();
            this.ResizeRedraw = true;
        }
        private void StaffCirculationForm_Load(object sender, EventArgs e)
        {
            this.Shown += StaffCirculationForm_Shown;
        }
        
        private void StaffCirculationForm_Shown(object sender, EventArgs e)
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
            this.Resize += StaffCirculationForm_Resize;
            EnsureTransactionsTableExists();
            UpdateReturnDate();
            LoadTransactions();
            tabControl.SelectedIndex = 0;
        }
        
        private void StaffCirculationForm_Resize(object sender, EventArgs e)
        {
            // Re-adjust panel sizes when form resizes
            if (this.Width > 0 && this.Height > 0)
            {
                AdjustPanelSizes();
            }
        }
        
        private void panelCirculationManagement_Resize(object sender, EventArgs e)
        {
            // Adjust panel sizes when circulation management panel resizes
            AdjustPanelSizes();
        }
        
        private void AdjustPanelSizes()
        {
            try
            {
                // Adjust checkout and return panels to be side-by-side with no gaps
                if (panelCheckOutBook != null && panelReturnBook != null && panelCirculationManagement != null)
                {
                    int availableWidth = panelCirculationManagement.Width;
                    int gap = 8; // Small gap between panels
                    int panelWidth = (availableWidth - gap) / 2; // Each panel gets half the width minus gap
                    
                    // Set checkout panel width and position
                    panelCheckOutBook.Width = panelWidth;
                    panelCheckOutBook.Location = new Point(0, panelCheckOutBook.Top);
                    
                    // Set return panel width and position (right next to checkout panel)
                    panelReturnBook.Width = panelWidth;
                    panelReturnBook.Location = new Point(panelCheckOutBook.Right + gap, panelReturnBook.Top);
                }
                
                // Adjust transaction history panel height (35% of available height, minimum 200px)
                if (panelTransactionHistory != null)
                {
                    int optimalHeight = PanelSizeHelper.CalculateHeight(this, 0.35, padding: 48, minHeight: 200);
                    if (panelTransactionHistory.Height < 200 && optimalHeight >= 200)
                    {
                        panelTransactionHistory.Height = optimalHeight;
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
                    using (var checkCmd = new MySqlCommand(checkTableQuery, conn))
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
                            _dbContext.ExecuteNonQuery(createTableQuery);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error ensuring transactions table: {ex.Message}");
            }
        }
        private void UpdateReturnDate()
        {
            txtReturnDate.Text = DateTime.Now.ToString("MMMM d, yyyy");
        }
        private void txtCheckOutMemberID_Enter(object sender, EventArgs e)
        {
            if (txtCheckOutMemberID.Text == "Enter member ID (e.g., MEM-000001)")
            {
                txtCheckOutMemberID.Text = "";
                txtCheckOutMemberID.ForeColor = Color.Black;
            }
        }
        private void txtCheckOutMemberID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCheckOutMemberID.Text))
            {
                txtCheckOutMemberID.Text = "Enter member ID (e.g., MEM-000001)";
                txtCheckOutMemberID.ForeColor = Color.Gray;
            }
        }
        private void txtCheckOutMemberID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCheckOutBookID.Focus();
                e.SuppressKeyPress = true;
            }
        }
        private void txtCheckOutBookID_Enter(object sender, EventArgs e)
        {
            if (txtCheckOutBookID.Text == "Enter book ID or accession (e.g., ACC-000001)")
            {
                txtCheckOutBookID.Text = "";
                txtCheckOutBookID.ForeColor = Color.Black;
            }
        }
        private void txtCheckOutBookID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCheckOutBookID.Text))
            {
                txtCheckOutBookID.Text = "Enter book ID or accession (e.g., ACC-000001)";
                txtCheckOutBookID.ForeColor = Color.Gray;
            }
        }
        private void txtCheckOutBookID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnProcessCheckOut_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }
        private void txtReturnBookID_Enter(object sender, EventArgs e)
        {
            if (txtReturnBookID.Text == "Scan or enter book ID")
            {
                txtReturnBookID.Text = "";
                txtReturnBookID.ForeColor = Color.Black;
            }
        }
        private void txtReturnBookID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtReturnBookID.Text))
            {
                txtReturnBookID.Text = "Scan or enter book ID";
                txtReturnBookID.ForeColor = Color.Gray;
            }
        }
        private void txtReturnBookID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnProcessReturn_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }
        private void btnProcessCheckOut_Click(object sender, EventArgs e)
        {
            string memberIdText = txtCheckOutMemberID.Text.Trim();
            string bookIdText = txtCheckOutBookID.Text.Trim();
            if (memberIdText == "Enter member ID (e.g., M1001)" || string.IsNullOrWhiteSpace(memberIdText))
            {
                MessageBox.Show("Please enter a Member ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCheckOutMemberID.Focus();
                return;
            }
            if (bookIdText == "Enter book ID (e.g., B1001)" || string.IsNullOrWhiteSpace(bookIdText))
            {
                MessageBox.Show("Please enter a Book ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCheckOutBookID.Focus();
                return;
            }
            try
            {
                int memberId = Project5LMS.Helpers.IDFormatter.ParseMemberID(memberIdText);
                if (memberId == 0)
                {
                    MessageBox.Show("Invalid Member ID format.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int bookId = 0;
                var book = _bookService.GetBookByAccessionNumber(bookIdText);
                if (book != null)
                {
                    bookId = book.BookID;
                }
                else
                {
                    bookId = Project5LMS.Helpers.IDFormatter.ParseBookID(bookIdText);
                    if (bookId > 0)
                    {
                        var bookById = _bookService.GetBook(bookId);
                        if (bookById == null)
                            bookId = 0;
                    }
                }
                if (bookId == 0)
                {
                    MessageBox.Show("Book not found with the provided ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                using (var statusForm = new TransactionStatusForm("Book Borrow"))
                {
                    statusForm.Show();
                    Application.DoEvents();
                    try
                    {
                        statusForm.UpdateStatus("Validating member eligibility...");
                        if (!_circulationService.CanBorrow(memberId))
                        {
                            statusForm.Close();
                            throw new InvalidOperationException("Member is not eligible to borrow books.");
                        }
                        statusForm.UpdateStatus("Processing book checkout...");
                        if (!_circulationService.BorrowBook(memberId, bookId, 14))
                        {
                            statusForm.Close();
                            throw new InvalidOperationException("Failed to borrow book. Please try again.");
                        }
                        statusForm.UpdateStatus("Transaction completed successfully!");
                        System.Threading.Thread.Sleep(500);
                        statusForm.Close();
                    }
                    catch
                    {
                        statusForm.Close();
                        throw;
                    }
                }
                MessageBox.Show("Book borrowed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCheckOutMemberID.Text = "Enter member ID (e.g., M1001)";
                txtCheckOutMemberID.ForeColor = Color.Gray;
                txtCheckOutBookID.Text = "Enter book ID (e.g., B1001)";
                txtCheckOutBookID.ForeColor = Color.Gray;
                LoadTransactions();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing checkout: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnProcessReturn_Click(object sender, EventArgs e)
        {
            string bookIdText = txtReturnBookID.Text.Trim();
            if (bookIdText == "Scan or enter book ID" || string.IsNullOrWhiteSpace(bookIdText))
            {
                MessageBox.Show("Please enter a Book ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtReturnBookID.Focus();
                return;
            }
            try
            {
                int bookId = 0;
                var book = _bookService.GetBookByAccessionNumber(bookIdText);
                if (book != null)
                {
                    bookId = book.BookID;
                }
                else
                {
                    bookId = Project5LMS.Helpers.IDFormatter.ParseBookID(bookIdText);
                    if (bookId > 0)
                    {
                        var bookById = _bookService.GetBook(bookId);
                        if (bookById == null)
                            bookId = 0;
                    }
                }
                if (bookId == 0)
                {
                    MessageBox.Show("Book not found with the provided ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                using (var statusForm = new TransactionStatusForm("Book Return"))
                {
                    statusForm.Show();
                    Application.DoEvents();
                    try
                    {
                        statusForm.UpdateStatus("Calculating fine...");
                        if (fine > 0)
                        {
                            statusForm.UpdateStatus($"Fine calculated: ${fine:F2}");
                        }
                        statusForm.UpdateStatus("Processing book return...");
                        if (_circulationService.ReturnBook(transactionId))
                        {
                            if (fine > 0)
                            {
                                statusForm.UpdateStatus("Updating fine record...");
                                _finesService.UpdateTransactionFine(transactionId, fine);
                            }
                            statusForm.UpdateStatus("Transaction completed successfully!");
                            System.Threading.Thread.Sleep(500);
                            statusForm.Close();
                        }
                        else
                        {
                            statusForm.Close();
                            throw new InvalidOperationException("Failed to return book. Please try again.");
                        }
                    }
                    catch
                    {
                        statusForm.Close();
                        throw;
                    }
                }
                string message = fine > 0
                    ? $"Book returned successfully!\n\nFine: ${fine:F2}"
                    : "Book returned successfully!";
                MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtReturnBookID.Text = "Scan or enter book ID";
                txtReturnBookID.ForeColor = Color.Gray;
                LoadTransactions();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing return: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl.SelectedIndex)
            {
                case 0:
                    currentFilter = "All";
                    break;
                case 1:
                    currentFilter = "Checkouts";
                    break;
                case 2:
                    currentFilter = "Returns";
                    break;
            }
            LoadTransactions();
        }
        private void LoadTransactions()
        {
            flowLayoutTransactions.Controls.Clear();
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    bool hasBarcode = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "Barcode");
                    string bookIdentifier = hasBarcode ? "b.Barcode" : "b.AccessionNo";
                    string bookIdentifierAlias = hasBarcode ? "Barcode" : "AccessionNo";
                    bool hasBookCopies = false;
                    try
                    {
                        string checkTableQuery = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES
                                                  WHERE TABLE_SCHEMA = DATABASE()
                                                  AND TABLE_NAME = 'BookCopies'";
                        using (var checkCmd = new MySqlCommand(checkTableQuery, conn))
                        {
                            hasBookCopies = Convert.ToInt32(checkCmd.ExecuteScalar()) > 0;
                        }
                    }
                    catch { hasBookCopies = false; }
                    string copyJoin = hasBookCopies
                        ? "LEFT JOIN BookCopies bc ON b.BookID = bc.BookID AND bc.CopyStatus = 'Borrowed'"
                        : "";
                    string copySelect = hasBookCopies
                        ? ", bc.AccessionNumber as CopyAccessionNumber, bc.Barcode as CopyBarcode, bc.CopyStatus as CopyStatus"
                        : "";
                    string query = $@"SELECT t.TransactionID, t.MemberID, t.BookID, t.BorrowDate, t.DueDate, t.ReturnDate,
                                    t.Status, t.TransactionType, t.Fine, t.RenewalCount,
                                    b.Title as BookTitle, {bookIdentifier} as {bookIdentifierAlias}{copySelect},
                                    m.FirstName, m.LastName, m.MemberID as MemberIDNum
                                    FROM Transactions t
                                    INNER JOIN Books b ON t.BookID = b.BookID
                                    INNER JOIN Members m ON t.MemberID = m.MemberID
                                    {copyJoin}";
                    if (currentFilter == "Checkouts")
                    {
                        query += " WHERE (t.Status = 'Borrowed' OR t.Status = 'Active')";
                    }
                    else if (currentFilter == "Returns")
                    {
                        query += " WHERE t.Status = 'Returned'";
                    }
                    query += " ORDER BY t.BorrowDate DESC, t.ReturnDate DESC LIMIT 50";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Panel transactionCard = CreateTransactionCard(reader);
                                flowLayoutTransactions.Controls.Add(transactionCard);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading transactions: {ex.Message}");
            }
        }
        private Panel CreateTransactionCard(MySqlDataReader reader)
        {
            int transactionId = reader.GetInt32("TransactionID");
            string bookTitle = reader["BookTitle"]?.ToString() ?? "Unknown";
            string memberName = $"{reader["FirstName"]?.ToString()} {reader["LastName"]?.ToString()}";
            int memberId = reader.GetInt32("MemberIDNum");
            DateTime borrowDate = reader.GetDateTime("BorrowDate");
            DateTime? dueDate = reader["DueDate"] != DBNull.Value ? (DateTime?)reader.GetDateTime("DueDate") : null;
            DateTime? returnDate = reader["ReturnDate"] != DBNull.Value ? (DateTime?)reader.GetDateTime("ReturnDate") : null;
            string status = reader["Status"]?.ToString() ?? "";
            string transactionType = reader["TransactionType"]?.ToString() ?? "";
            int renewalCount = HasColumn(reader, "RenewalCount") && reader["RenewalCount"] != DBNull.Value
                ? Convert.ToInt32(reader["RenewalCount"]) : 0;
            string copyAccession = HasColumn(reader, "CopyAccessionNumber") && reader["CopyAccessionNumber"] != DBNull.Value
                ? reader["CopyAccessionNumber"].ToString() : "";
            string copyBarcode = HasColumn(reader, "CopyBarcode") && reader["CopyBarcode"] != DBNull.Value
                ? reader["CopyBarcode"].ToString() : "";
            string copyStatus = HasColumn(reader, "CopyStatus") && reader["CopyStatus"] != DBNull.Value
                ? reader["CopyStatus"].ToString() : "";
            
            bool isReturned = status == "Returned";
            bool isActive = status == "Borrowed" || status == "Active";
            bool isOverdue = dueDate.HasValue && DateTime.Now > dueDate.Value && !isReturned;
            
            int cardWidth = flowLayoutTransactions.Width > 0 
                ? flowLayoutTransactions.Width - 40 
                : 1100;
            if (cardWidth < 600) cardWidth = 600;
            
            Panel card = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                Size = new Size(cardWidth, 100),
                Margin = new Padding(10, 8, 10, 8),
                Padding = new Padding(16, 12, 16, 12)
            };
            
            // Add subtle border effect
            card.Paint += (s, e) =>
            {
                e.Graphics.DrawRectangle(new Pen(Color.FromArgb(240, 240, 240), 1), 0, 0, card.Width - 1, card.Height - 1);
            };
            
            // Icon button on the left
            Panel iconPanel = new Panel
            {
                BackColor = isReturned ? Color.FromArgb(13, 110, 253) : Color.FromArgb(40, 167, 69),
                Size = new Size(48, 48),
                Location = new Point(16, 26)
            };
            iconPanel.Paint += (s, e) => DrawTransactionIcon(e.Graphics, iconPanel, isReturned);
            card.Controls.Add(iconPanel);
            
            // Book title (bold, larger)
            Label lblBookTitle = new Label
            {
                Text = bookTitle.Length > 60 ? bookTitle.Substring(0, 57) + "..." : bookTitle,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 37, 41),
                AutoSize = false,
                Location = new Point(80, 20),
                Size = new Size(400, 24),
                TextAlign = ContentAlignment.MiddleLeft
            };
            card.Controls.Add(lblBookTitle);
            
            // Member name below title
            Label lblMember = new Label
            {
                Text = $"{memberName} (M{memberId})",
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                ForeColor = Color.FromArgb(108, 117, 125),
                AutoSize = false,
                Location = new Point(80, 48),
                Size = new Size(400, 20),
                TextAlign = ContentAlignment.MiddleLeft
            };
            card.Controls.Add(lblMember);
            
            // Active status badge
            int leftOffset = 80;
            if (isActive)
            {
                Label lblStatus = new Label
                {
                    Text = "Active",
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    ForeColor = Color.White,
                    BackColor = Color.FromArgb(40, 167, 69),
                    AutoSize = false,
                    Size = new Size(55, 22),
                    Location = new Point(leftOffset, 72),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Padding = new Padding(0)
                };
                card.Controls.Add(lblStatus);
                leftOffset += 65;
                
                if (renewalCount > 0)
                {
                    Label lblRenewalCount = new Label
                    {
                        Text = $"Renewed: {renewalCount}x",
                        Font = new Font("Segoe UI", 9F),
                        ForeColor = Color.FromArgb(108, 117, 125),
                        AutoSize = true,
                        Location = new Point(leftOffset, 75)
                    };
                    card.Controls.Add(lblRenewalCount);
                }
            }
            else if (isOverdue)
            {
                Label lblStatus = new Label
                {
                    Text = "Overdue",
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    ForeColor = Color.White,
                    BackColor = Color.FromArgb(220, 53, 69),
                    AutoSize = false,
                    Size = new Size(70, 22),
                    Location = new Point(leftOffset, 72),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Padding = new Padding(0)
                };
                card.Controls.Add(lblStatus);
            }
            
            // Date and time on the right
            string dateText = "";
            if (isReturned && returnDate.HasValue)
            {
                dateText = returnDate.Value.ToString("yyyy-MM-dd HH:mm");
            }
            else
            {
                dateText = borrowDate.ToString("yyyy-MM-dd HH:mm");
                if (dueDate.HasValue)
                {
                    dateText += $" | Due: {dueDate.Value:yyyy-MM-dd}";
                }
            }
            
            Label lblDate = new Label
            {
                Text = dateText,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(33, 37, 41),
                AutoSize = false,
                Size = new Size(250, 20),
                Location = new Point(card.Width - 320, 40),
                TextAlign = ContentAlignment.MiddleRight
            };
            card.Controls.Add(lblDate);
            
            // Checkmark on far right
            Panel checkmarkPanel = new Panel
            {
                BackColor = Color.FromArgb(40, 167, 69),
                Size = new Size(32, 32),
                Location = new Point(card.Width - 48, 34)
            };
            checkmarkPanel.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (Pen pen = new Pen(Color.White, 3))
                {
                    e.Graphics.DrawLine(pen, 10, 16, 14, 20);
                    e.Graphics.DrawLine(pen, 14, 20, 22, 12);
                }
            };
            card.Controls.Add(checkmarkPanel);
            
            // Renew button for active transactions
            if (isActive)
            {
                bool canRenew = _circulationService.CanRenew(transactionId);
                Button btnRenew = new Button
                {
                    Text = "Renew",
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    ForeColor = Color.White,
                    BackColor = canRenew ? Color.FromArgb(13, 110, 253) : Color.FromArgb(200, 200, 200),
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(75, 28),
                    Location = new Point(card.Width - 130, 66),
                    Enabled = canRenew,
                    Cursor = Cursors.Hand
                };
                btnRenew.FlatAppearance.BorderSize = 0;
                btnRenew.Click += (s, e) => RenewTransaction(transactionId);
                card.Controls.Add(btnRenew);
            }
            
            return card;
        }
        private bool HasColumn(MySqlDataReader reader, string columnName)
        {
            try
            {
                return reader.GetOrdinal(columnName) >= 0;
            }
            catch
            {
                return false;
            }
        }
        private void DrawTransactionIcon(Graphics g, Panel panel, bool isReturn)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int size = Math.Min(panel.Width, panel.Height) - 10;
            int x = (panel.Width - size) / 2;
            int y = (panel.Height - size) / 2;
            using (Pen pen = new Pen(Color.White, 3))
            {
                if (isReturn)
                {
                    g.DrawLine(pen, x + size * 0.5f, y + size * 0.3f, x + size * 0.5f, y + size * 0.7f);
                    g.DrawLine(pen, x + size * 0.3f, y + size * 0.5f, x + size * 0.5f, y + size * 0.7f);
                    g.DrawLine(pen, x + size * 0.7f, y + size * 0.5f, x + size * 0.5f, y + size * 0.7f);
                }
                else
                {
                    g.DrawLine(pen, x + size * 0.5f, y + size * 0.7f, x + size * 0.5f, y + size * 0.3f);
                    g.DrawLine(pen, x + size * 0.3f, y + size * 0.5f, x + size * 0.5f, y + size * 0.3f);
                    g.DrawLine(pen, x + size * 0.7f, y + size * 0.5f, x + size * 0.5f, y + size * 0.3f);
                }
            }
        }
        private void DrawCheckOutIcon(Graphics g, Panel panel)
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
        private void DrawReturnIcon(Graphics g, Panel panel)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int size = Math.Min(panel.Width, panel.Height) - 10;
            int x = (panel.Width - size) / 2;
            int y = (panel.Height - size) / 2;
            using (Pen pen = new Pen(Color.White, 3))
            {
                g.DrawLine(pen, x + size * 0.5f, y + size * 0.3f, x + size * 0.5f, y + size * 0.7f);
                g.DrawLine(pen, x + size * 0.3f, y + size * 0.5f, x + size * 0.5f, y + size * 0.7f);
                g.DrawLine(pen, x + size * 0.7f, y + size * 0.5f, x + size * 0.5f, y + size * 0.7f);
            }
        }
        private void RenewTransaction(int transactionId)
        {
            try
            {
                if (!_circulationService.CanRenew(transactionId))
                {
                    MessageBox.Show("This book cannot be renewed. Maximum renewal limit reached or member is not eligible.",
                        "Renewal Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                bool success = false;
                using (var statusForm = new TransactionStatusForm("Book Renewal"))
                {
                    statusForm.Show();
                    Application.DoEvents();
                    try
                    {
                        statusForm.UpdateStatus("Processing renewal...");
                        success = _circulationService.RenewBook(transactionId);
                        if (success)
                        {
                            statusForm.UpdateStatus("Book renewed successfully!");
                            System.Threading.Thread.Sleep(500);
                            statusForm.Close();
                            MessageBox.Show("Book renewed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadTransactions();
                        }
                        else
                        {
                            statusForm.Close();
                            MessageBox.Show("Failed to renew book. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch
                    {
                        statusForm.Close();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error renewing book: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private int GetBookIdFromAccession(string accessionNo)
        {
            try
            {
                var book = _bookService.GetBookByAccessionNumber(accessionNo);
                if (book != null)
                    return book.BookID;
                int parsedAccession = Project5LMS.Helpers.IDFormatter.ParseAccessionNumber(accessionNo);
                if (parsedAccession > 0)
                {
                    var bookByAcc = _bookService.GetBookByAccessionNumber(Project5LMS.Helpers.IDFormatter.FormatAccessionNumber(parsedAccession.ToString()));
                    if (bookByAcc != null)
                        return bookByAcc.BookID;
                }
                int bookId = Project5LMS.Helpers.IDFormatter.ParseBookID(accessionNo);
                if (bookId > 0)
                {
                    var bookById = _bookService.GetBook(bookId);
                    return bookById != null ? bookId : 0;
                }
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT BookID FROM Books
                                    WHERE Barcode = @Accession
                                    OR AccessionNo = @Accession
                                    OR BookID = @BookID";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Accession", accessionNo);
                        int parsedBookId = Project5LMS.Helpers.IDFormatter.ParseBookID(accessionNo);
                        if (parsedBookId > 0)
                        {
                            cmd.Parameters.AddWithValue("@BookID", parsedBookId);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@BookID", DBNull.Value);
                        }
                        object result = cmd.ExecuteScalar();
                        return result != null ? Convert.ToInt32(result) : 0;
                    }
                }
            }
            catch
            {
                return 0;
            }
        }
        private bool MemberExists(int memberId)
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Members WHERE MemberID = @MemberID";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", memberId);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
        private bool IsBookAvailable(int bookId)
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT Available FROM Books WHERE BookID = @BookID";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookID", bookId);
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            int available = Convert.ToInt32(result);
                            return available > 0;
                        }
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
        private bool CanMemberBorrow(int memberId)
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT COALESCE(m.Type, m.MemberType) as MemberType,
                                    (SELECT COUNT(*) FROM Transactions t
                                     WHERE t.MemberID = m.MemberID
                                     AND (t.Status = 'Borrowed' OR t.Status = 'Active')) as CurrentBorrowings
                                    FROM Members m
                                    WHERE m.MemberID = @MemberID";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", memberId);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string memberType = reader["MemberType"] != DBNull.Value ? reader["MemberType"].ToString() : "";
                                int currentBorrowings = reader["CurrentBorrowings"] != DBNull.Value ? Convert.ToInt32(reader["CurrentBorrowings"]) : 0;
                                int maxBorrowings = GetMaxBorrowingsForType(memberType);
                                return currentBorrowings < maxBorrowings;
                            }
                        }
                    }
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
        private int GetMaxBorrowingsForType(string memberType)
        {
            switch (memberType?.ToLower())
            {
                case "student":
                    return 5;
                case "faculty":
                    return 10;
                case "staff":
                    return 7;
                case "guest":
                    return 3;
                default:
                    return 5;
            }
        }
        private int GetActiveTransactionId(int bookId)
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT TransactionID FROM Transactions
                                    WHERE BookID = @BookID
                                    AND (Status = 'Borrowed' OR Status = 'Active')
                                    ORDER BY TransactionID DESC
                                    LIMIT 1";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookID", bookId);
                        object result = cmd.ExecuteScalar();
                        return result != null ? Convert.ToInt32(result) : 0;
                    }
                }
            }
            catch
            {
                return 0;
            }
        }
        private bool CheckColumnExists(MySqlConnection conn, string tableName, string columnName)
        {
            try
            {
                string query = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS
                                WHERE TABLE_SCHEMA = DATABASE()
                                AND TABLE_NAME = @TableName
                                AND COLUMN_NAME = @ColumnName";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TableName", tableName);
                    cmd.Parameters.AddWithValue("@ColumnName", columnName);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
            catch
            {
                return false;
            }
        }

        private void flowLayoutTransactions_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelReturnBook_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelMainContainer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelTransactionHistory_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelTransactionsList_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabAll_Click(object sender, EventArgs e)
        {

        }

        private void tabCheckouts_Click(object sender, EventArgs e)
        {

        }

        private void tabReturns_Click(object sender, EventArgs e)
        {

        }

        private void lblTransactionHistory_Click(object sender, EventArgs e)
        {

        }

        private void panelCirculationManagement_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblReturnDate_Click(object sender, EventArgs e)
        {

        }

        private void txtReturnDate_Click(object sender, EventArgs e)
        {

        }

        private void txtReturnBookID_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblReturnBookID_Click(object sender, EventArgs e)
        {

        }

        private void lblReturnBookTitle_Click(object sender, EventArgs e)
        {

        }

        private void panelCheckOutBook_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtCheckOutBookID_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblCheckOutBookID_Click(object sender, EventArgs e)
        {

        }

        private void txtCheckOutMemberID_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblCheckOutMemberID_Click(object sender, EventArgs e)
        {

        }

        private void lblCheckOutBookTitle_Click(object sender, EventArgs e)
        {

        }

        private void panelHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void lblSubtitle_Click(object sender, EventArgs e)
        {

        }
    }
}