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
        private readonly BorrowingValidator _borrowingValidator;
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
            _borrowingValidator = DependencyInjection.GetRequiredService<BorrowingValidator>();
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
            // Preserve designer-defined layout - only adjust if window is too small
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
                // Preserve designer-defined layout - don't override positions and sizes
                // The designer has:
                // - panelCheckOutBook: Location(8, 104), Size(894, 370)
                // - panelReturnBook: Location(941, 108), Size(829, 430)
                // These should be preserved as defined in the designer
                
                if (panelCheckOutBook != null && panelReturnBook != null && panelCirculationManagement != null)
                {
                    // Only adjust if window is too small (less than minimum required width)
                    int minRequiredWidth = 894 + 829 + 20; // Sum of panel widths + gap
                    if (panelCirculationManagement.Width < minRequiredWidth)
                    {
                        // Scale down proportionally only if absolutely necessary
                        double scale = (double)panelCirculationManagement.Width / minRequiredWidth;
                        if (scale < 0.8) // Only if window is less than 80% of required size
                        {
                            int gap = 8;
                            int panelWidth = (int)((panelCirculationManagement.Width - gap) / 2);
                            
                            panelCheckOutBook.Width = panelWidth;
                            panelReturnBook.Width = panelWidth;
                            panelReturnBook.Location = new Point(panelCheckOutBook.Right + gap, panelReturnBook.Top);
                        }
                    }
                    else
                    {
                        // Restore designer-defined positions and sizes
                        panelCheckOutBook.Location = new Point(8, 104);
                        panelCheckOutBook.Size = new Size(894, 370);
                        panelReturnBook.Location = new Point(941, 108);
                        panelReturnBook.Size = new Size(829, 430);
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
                txtBorrowBookAccession.Focus();
                e.SuppressKeyPress = true;
            }
        }
        private void txtCheckOutBookID_Enter(object sender, EventArgs e)
        {
            if (txtBorrowBookAccession.Text == "Enter book ID or accession (e.g., ACC-000001)")
            {
                txtBorrowBookAccession.Text = "";
                txtBorrowBookAccession.ForeColor = Color.Black;
            }
        }
        private void txtCheckOutBookID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBorrowBookAccession.Text))
            {
                txtBorrowBookAccession.Text = "Enter book ID or accession (e.g., ACC-000001)";
                txtBorrowBookAccession.ForeColor = Color.Gray;
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
            string bookIdText = txtBorrowBookAccession.Text.Trim();
            if (memberIdText == "Enter member ID (e.g., M1001)" || string.IsNullOrWhiteSpace(memberIdText))
            {
                ErrorHandler.ShowValidationError("Please enter a Member ID.");
                txtCheckOutMemberID.Focus();
                return;
            }
            if (bookIdText == "Enter book ID (e.g., B1001)" || string.IsNullOrWhiteSpace(bookIdText))
            {
                ErrorHandler.ShowValidationError("Please enter a Book ID.");
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
                    ErrorHandler.ShowError("Book not found with the provided ID.", "Error");
                    return;
                }
                
                // Use BorrowingValidator for comprehensive validation
                var validationResult = _borrowingValidator.ValidateBorrowing(memberId, bookId);
                if (!validationResult.IsValid)
                {
                    ErrorHandler.ShowValidationError(validationResult.GetErrorMessage());
                    return;
                }
                
                // All validations passed - proceed with borrowing
                int borrowDays = validationResult.BorrowingPeriodDays;
                
                using (var statusForm = new TransactionStatusForm("Book Borrow"))
                {
                    statusForm.Show();
                    Application.DoEvents();
                    try
                    {
                        statusForm.UpdateStatus("Processing book checkout...");
                        if (!_circulationService.BorrowBook(memberId, bookId, borrowDays))
                        {
                            statusForm.Close();
                            throw new InvalidOperationException("Failed to borrow book. Please try again.");
                        }
                        statusForm.UpdateStatus("Transaction completed successfully!");
                        System.Threading.Thread.Sleep(Constants.ThreadSleepShort);
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
                txtBorrowBookAccession.Text = "Enter book ID (e.g., B1001)";
                txtBorrowBookAccession.ForeColor = Color.Gray;
                LoadTransactions();
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError($"Error processing checkout: {ex.Message}", "Error", ex);
            }
        }
        private void btnProcessReturn_Click(object sender, EventArgs e)
        {
            string bookIdText = txtReturnBookID.Text.Trim();
            if (bookIdText == "Scan or enter book ID" || string.IsNullOrWhiteSpace(bookIdText))
            {
                ErrorHandler.ShowValidationError("Please enter a Book ID.");
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
                    ErrorHandler.ShowError("Book not found with the provided ID.", "Error");
                    return;
                }
                var activeTransaction = _circulationService.GetActiveTransactionByBook(bookId);
                if (activeTransaction == null)
                {
                    ErrorHandler.ShowError("No active borrowing found for this book.", "Error");
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
                            statusForm.UpdateStatus("Transaction completed successfully!");
                            System.Threading.Thread.Sleep(Constants.ThreadSleepShort);
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
                    ? $"Book returned successfully!\n\nFine: {Project5LMS.Helpers.IDFormatter.FormatCurrency(fine)}"
                    : "Book returned successfully!";
                MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtReturnBookID.Text = "Scan or enter book ID";
                txtReturnBookID.ForeColor = Color.Gray;
                LoadTransactions();
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError($"Error processing return: {ex.Message}", "Error", ex);
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
                    query += $" ORDER BY t.BorrowDate DESC, t.ReturnDate DESC LIMIT {Constants.TransactionQueryLimit}";
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
            // Safely get all required columns with proper checks
            int transactionId = HasColumn(reader, "TransactionID") && reader["TransactionID"] != DBNull.Value 
                ? reader.GetInt32("TransactionID") : 0;
            int bookId = HasColumn(reader, "BookID") && reader["BookID"] != DBNull.Value 
                ? reader.GetInt32("BookID") : 0;
            string bookTitle = HasColumn(reader, "BookTitle") && reader["BookTitle"] != DBNull.Value 
                ? reader["BookTitle"].ToString() : "Unknown";
            string firstName = HasColumn(reader, "FirstName") && reader["FirstName"] != DBNull.Value 
                ? reader["FirstName"].ToString() : "";
            string lastName = HasColumn(reader, "LastName") && reader["LastName"] != DBNull.Value 
                ? reader["LastName"].ToString() : "";
            string memberName = $"{firstName} {lastName}".Trim();
            int memberId = HasColumn(reader, "MemberIDNum") && reader["MemberIDNum"] != DBNull.Value 
                ? reader.GetInt32("MemberIDNum") : 0;
            DateTime borrowDate = HasColumn(reader, "BorrowDate") && reader["BorrowDate"] != DBNull.Value 
                ? reader.GetDateTime("BorrowDate") : DateTime.Now;
            DateTime? dueDate = HasColumn(reader, "DueDate") && reader["DueDate"] != DBNull.Value 
                ? (DateTime?)reader.GetDateTime("DueDate") : null;
            DateTime? returnDate = HasColumn(reader, "ReturnDate") && reader["ReturnDate"] != DBNull.Value 
                ? (DateTime?)reader.GetDateTime("ReturnDate") : null;
            string status = HasColumn(reader, "Status") && reader["Status"] != DBNull.Value 
                ? reader["Status"].ToString() : "";
            string transactionType = HasColumn(reader, "TransactionType") && reader["TransactionType"] != DBNull.Value 
                ? reader["TransactionType"].ToString() : "";
            int renewalCount = HasColumn(reader, "RenewalCount") && reader["RenewalCount"] != DBNull.Value
                ? Convert.ToInt32(reader["RenewalCount"]) : 0;
            string copyAccession = HasColumn(reader, "CopyAccessionNumber") && reader["CopyAccessionNumber"] != DBNull.Value
                ? reader["CopyAccessionNumber"].ToString() : "";
            string copyBarcode = HasColumn(reader, "CopyBarcode") && reader["CopyBarcode"] != DBNull.Value
                ? reader["CopyBarcode"].ToString() : "";
            string copyStatus = HasColumn(reader, "CopyStatus") && reader["CopyStatus"] != DBNull.Value
                ? reader["CopyStatus"].ToString() : "";
            
            // Get accession number or barcode if available for easier input
            string bookIdentifier = "";
            if (HasColumn(reader, "AccessionNo") && reader["AccessionNo"] != DBNull.Value && !string.IsNullOrWhiteSpace(reader["AccessionNo"].ToString()))
            {
                bookIdentifier = reader["AccessionNo"].ToString();
            }
            else if (HasColumn(reader, "Barcode") && reader["Barcode"] != DBNull.Value && !string.IsNullOrWhiteSpace(reader["Barcode"].ToString()))
            {
                bookIdentifier = reader["Barcode"].ToString();
            }
            else if (!string.IsNullOrWhiteSpace(copyAccession))
            {
                bookIdentifier = copyAccession;
            }
            else if (!string.IsNullOrWhiteSpace(copyBarcode))
            {
                bookIdentifier = copyBarcode;
            }
            
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
                Size = new Size(cardWidth, Constants.CardHeight),
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
            
            // Book title with Book ID (bold, larger)
            string bookTitleWithId = TextHelper.TruncateText(bookTitle);
            string bookIdDisplay = !string.IsNullOrWhiteSpace(bookIdentifier) ? bookIdentifier : $"B{bookId}";
            Label lblBookTitle = new Label
            {
                Text = $"{bookTitleWithId} ({bookIdDisplay})",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 37, 41),
                AutoSize = false,
                Location = new Point(80, 20),
                Size = new Size(450, 24),
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
                            System.Threading.Thread.Sleep(Constants.ThreadSleepShort);
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
                                string memberType = HasColumn(reader, "MemberType") && reader["MemberType"] != DBNull.Value 
                                    ? reader["MemberType"].ToString() : "";
                                int currentBorrowings = HasColumn(reader, "CurrentBorrowings") && reader["CurrentBorrowings"] != DBNull.Value 
                                    ? Convert.ToInt32(reader["CurrentBorrowings"]) : 0;
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
            // Display book information when return book ID is entered
            string bookIdText = txtReturnBookID.Text.Trim();
            if (string.IsNullOrWhiteSpace(bookIdText) || bookIdText == "Scan or enter book ID" || bookIdText.Contains("Scan or enter"))
            {
                ClearBookInfoDisplay("Return");
                return;
            }
            
            try
            {
                DisplayBookInfo(bookIdText, "Return");
            }
            catch
            {
                ClearBookInfoDisplay("Return");
            }
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
            // Display book information when book ID/accession is entered
            string bookIdText = txtBorrowBookAccession.Text.Trim();
            if (string.IsNullOrWhiteSpace(bookIdText) || bookIdText == "Enter book ID (e.g., B1001)" || bookIdText.Contains("Enter book"))
            {
                ClearBookInfoDisplay("CheckOut");
                return;
            }
            
            try
            {
                DisplayBookInfo(bookIdText, "CheckOut");
            }
            catch
            {
                ClearBookInfoDisplay("CheckOut");
            }
        }

        private void lblCheckOutBookID_Click(object sender, EventArgs e)
        {

        }

        private void txtCheckOutMemberID_TextChanged(object sender, EventArgs e)
        {
            // Display member eligibility when member ID is entered (supports partial matching)
            string memberIdText = txtCheckOutMemberID.Text.Trim();
            if (string.IsNullOrWhiteSpace(memberIdText) || memberIdText == "Enter member ID (e.g., M1001)" || memberIdText.Contains("Enter member"))
            {
                ClearMemberEligibilityDisplay();
                return;
            }
            
            try
            {
                // Try parsing as complete member ID first
                int memberId = Project5LMS.Helpers.IDFormatter.ParseMemberID(memberIdText);
                if (memberId > 0)
                {
                    DisplayMemberEligibility(memberId);
                    return;
                }
                
                // If parsing fails, try partial search
                string numericPart = System.Text.RegularExpressions.Regex.Replace(memberIdText, @"[^\d]", "");
                if (!string.IsNullOrWhiteSpace(numericPart) && numericPart.Length >= 1)
                {
                    var searchResults = _membersService.SearchMembers(memberIdText);
                    var matchingMember = searchResults.FirstOrDefault(m => 
                        Project5LMS.Helpers.IDFormatter.FormatMemberID(m.MemberID).IndexOf(memberIdText, StringComparison.OrdinalIgnoreCase) >= 0 ||
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

        private void lblCheckOutMemberID_Click(object sender, EventArgs e)
        {

        }

        private void lblCheckOutBookTitle_Click(object sender, EventArgs e)
        {

        }

        private void panelHeader_Paint(object sender, PaintEventArgs e)
        {

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
                
                // Display member information using predefined labels (matching AdminCirculationForm)
                if (panelMemberEligibility != null)
                {
                    panelMemberEligibility.Visible = true;
                    
                    // Member Name
                    if (lblMemberName != null)
                    {
                        lblMemberName.Text = $"Name: {member.FullName}";
                    }
                    
                    // Member Type
                    if (lblMemberType != null)
                    {
                        lblMemberType.Text = $"Type: {member.Type}";
                    }
                    
                    // Member Status - Display actual Status field from database to match StaffMembersForm
                    if (lblMemberStatus != null)
                    {
                        string actualStatus = member.Status ?? "Unknown";
                        string statusText = $"Status: {actualStatus}";
                        lblMemberStatus.Text = statusText;
                        
                        // Color coding to match StaffMembersForm display
                        switch (actualStatus.ToLower())
                        {
                            case "active":
                                lblMemberStatus.ForeColor = Color.FromArgb(34, 139, 34); // Green
                                break;
                            case "suspended":
                                lblMemberStatus.ForeColor = Color.FromArgb(220, 20, 60); // Red
                                break;
                            case "expired":
                                lblMemberStatus.ForeColor = Color.FromArgb(255, 193, 7); // Orange/Amber
                                break;
                            default:
                                lblMemberStatus.ForeColor = Color.FromArgb(220, 20, 60); // Red for unknown/inactive
                                break;
                        }
                    }
                    
                    // Current Borrowings
                    if (lblMemberBorrowings != null)
                    {
                        lblMemberBorrowings.Text = $"Borrowings: {activeBorrowings}/{privileges.MaxBooksAllowed}";
                        lblMemberBorrowings.ForeColor = withinLimit ? Color.FromArgb(64, 64, 64) : Color.FromArgb(220, 20, 60);
                    }
                    
                    // Overdue Books
                    if (lblMemberOverdue != null)
                    {
                        lblMemberOverdue.Text = overdueCount > 0 ? $"Overdue: {overdueCount} book(s)" : "Overdue: None";
                        lblMemberOverdue.ForeColor = noOverdue ? Color.FromArgb(64, 64, 64) : Color.FromArgb(220, 20, 60);
                    }
                    
                    // Fines
                    if (lblMemberFines != null)
                    {
                        lblMemberFines.Text = totalFines > 0 ? $"Fines: {Project5LMS.Helpers.IDFormatter.FormatCurrency(totalFines)}" : "Fines: None";
                        lblMemberFines.ForeColor = finesPaid ? Color.FromArgb(64, 64, 64) : Color.FromArgb(220, 20, 60);
                    }
                    
                    // Eligibility Status - Use eligibility logic (considers Status AND ExpirationDate)
                    if (lblEligibilityStatus != null)
                    {
                        if (isEligible)
                        {
                            lblEligibilityStatus.Text = "✓ ELIGIBLE";
                            lblEligibilityStatus.ForeColor = Color.FromArgb(34, 139, 34);
                        }
                        else
                        {
                            var reasons = new List<string>();
                            // Check actual status and expiration separately for clearer messaging
                            string actualStatus = member.Status ?? "Unknown";
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
                            lblEligibilityStatus.ForeColor = Color.FromArgb(220, 20, 60);
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
        
        private string GetEligibilityReasons(bool isActive, bool withinLimit, bool noOverdue, bool finesPaid)
        {
            var reasons = new List<string>();
            if (!isActive) reasons.Add("Account inactive/expired");
            if (!withinLimit) reasons.Add("Borrowing limit reached");
            if (!noOverdue) reasons.Add("Has overdue books");
            if (!finesPaid) reasons.Add("Unpaid fines");
            return string.Join(", ", reasons);
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
        
        private void DisplayBookInfo(string bookIdText, string context)
        {
            try
            {
                Book book = null;
                int bookId = 0;
                
                // First try exact match by accession
                book = _bookService.GetBookByAccessionNumber(bookIdText);
                if (book != null)
                {
                    bookId = book.BookID;
                }
                else
                {
                    // Try parsing as ID
                    bookId = GetBookIdFromAccession(bookIdText);
                    if (bookId > 0)
                    {
                        book = _bookService.GetBook(bookId);
                    }
                }
                
                if (book == null || bookId == 0)
                {
                    ClearBookInfoDisplay(context);
                    return;
                }
                
                // Display book information
                if (context == "CheckOut")
                {
                    DisplayCheckOutBookInfo(book);
                }
                else if (context == "Return")
                {
                    DisplayReturnBookInfo(book, bookId);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error displaying book info: {ex.Message}");
                ClearBookInfoDisplay(context);
            }
        }
        
        private void DisplayCheckOutBookInfo(Book book)
        {
            // Display book information using predefined labels (matching AdminCirculationForm)
            if (panelCheckoutBookInfo != null)
            {
                panelCheckoutBookInfo.Visible = true;
                
                if (lblBorrowBookTitle != null)
                    lblBorrowBookTitle.Text = $"Title: {book.Title}";
                if (lblBorrowBookAuthor != null)
                    lblBorrowBookAuthor.Text = $"Author: {book.Author}";
                if (lblBorrowBookStatus != null)
                {
                    bool isAvailable = _bookService.IsBookAvailable(book.BookID);
                    lblBorrowBookStatus.Text = isAvailable ? "Status: Available" : "Status: Not Available";
                    lblBorrowBookStatus.ForeColor = isAvailable ? Color.FromArgb(34, 139, 34) : Color.FromArgb(220, 20, 60);
                }
                if (lblBorrowBookCopies != null)
                    lblBorrowBookCopies.Text = $"Copies: {book.Available}/{book.TotalCopies}";
            }
        }
        
        private void DisplayReturnBookInfo(Book book, int bookId)
        {
            // For return, show book info and transaction details
            if (panelReturnBookInfo != null)
            {
                panelReturnBookInfo.Controls.Clear();
                
                // Increase panel height to accommodate all information
                panelReturnBookInfo.Height = 184;
                
                int yPos = 5;
                int labelHeight = 20;
                int spacing = 6; // Increased spacing
                int labelWidth = 690; // Fixed width to prevent overlap
                
                Label lblTitle = new Label
                {
                    Text = $"Title: {book.Title}",
                    Font = new Font("Segoe UI", 9F),
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
                
                var activeTransaction = _circulationService.GetActiveTransactionByBook(bookId);
                if (activeTransaction != null)
                {
                    var member = _membersService.GetMember(activeTransaction.MemberID);
                    decimal fine = _finesService.CalculateFine(activeTransaction.TransactionID);
                    bool isOverdue = activeTransaction.DueDate < DateTime.Now;
                    int daysOverdue = isOverdue ? (DateTime.Now - activeTransaction.DueDate).Days : 0;
                    
                    Label lblMember = new Label
                    {
                        Text = $"Borrower: {member?.FullName ?? "Unknown"}",
                        Font = new Font("Segoe UI", 9F),
                        ForeColor = Color.FromArgb(64, 64, 64),
                        AutoSize = false,
                        Location = new Point(5, yPos),
                        Size = new Size(labelWidth, labelHeight)
                    };
                    panelReturnBookInfo.Controls.Add(lblMember);
                    yPos += labelHeight + spacing;
                    
                    Label lblDueDate = new Label
                    {
                        Text = $"Due Date: {activeTransaction.DueDate:yyyy-MM-dd}",
                        Font = new Font("Segoe UI", 9F),
                        ForeColor = Color.FromArgb(64, 64, 64),
                        AutoSize = false,
                        Location = new Point(5, yPos),
                        Size = new Size(labelWidth, labelHeight)
                    };
                    panelReturnBookInfo.Controls.Add(lblDueDate);
                    yPos += labelHeight + spacing;
                    
                    Label lblOverdue = new Label
                    {
                        Text = isOverdue ? $"Days Overdue: {daysOverdue}" : "Status: On Time",
                        Font = new Font("Segoe UI", 9F),
                        ForeColor = isOverdue ? Color.FromArgb(220, 20, 60) : Color.FromArgb(34, 139, 34),
                        AutoSize = false,
                        Location = new Point(5, yPos),
                        Size = new Size(labelWidth, labelHeight)
                    };
                    panelReturnBookInfo.Controls.Add(lblOverdue);
                    yPos += labelHeight + spacing;
                    
                    Label lblFine = new Label
                    {
                        Text = fine > 0 ? $"Fine: {Project5LMS.Helpers.IDFormatter.FormatCurrency(fine)}" : "Fine: None",
                        Font = new Font("Segoe UI", 9F),
                        ForeColor = fine > 0 ? Color.FromArgb(220, 20, 60) : Color.FromArgb(64, 64, 64),
                        AutoSize = false,
                        Location = new Point(5, yPos),
                        Size = new Size(labelWidth, labelHeight)
                    };
                    panelReturnBookInfo.Controls.Add(lblFine);
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
        
        private void ClearBookInfoDisplay(string context)
        {
            if (context == "CheckOut" && panelCheckoutBookInfo != null)
            {
                panelCheckoutBookInfo.Visible = false;
            }
            
            if (lblBorrowBookTitle != null) lblBorrowBookTitle.Text = "";
            if (lblBorrowBookAuthor != null) lblBorrowBookAuthor.Text = "";
            if (lblBorrowBookStatus != null) lblBorrowBookStatus.Text = "";
            if (lblBorrowBookCopies != null) lblBorrowBookCopies.Text = "";
            
            if (context == "Return" && panelReturnBookInfo != null)
            {
                panelReturnBookInfo.Controls.Clear();
                panelReturnBookInfo.Visible = false;
            }
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void lblSubtitle_Click(object sender, EventArgs e)
        {

        }
    }
}