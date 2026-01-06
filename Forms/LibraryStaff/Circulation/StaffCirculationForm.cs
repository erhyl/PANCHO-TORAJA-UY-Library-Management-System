using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Project5LMS.Helpers;

namespace Project5LMS.Forms.LibraryStaff.Circulation
{
    public partial class StaffCirculationForm : Form
    {
        private string connectionString;
        private string currentFilter = "All";

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

            try
            {
                connectionString = DatabaseHelper.GetConnectionString();
            }
            catch
            {
                connectionString = "Server=localhost;Database=librarydb;Uid=root;Pwd=;";
            }
        }

        private void StaffCirculationForm_Load(object sender, EventArgs e)
        {
            EnsureTransactionsTableExists();
            UpdateReturnDate();
            LoadTransactions();
            tabControl.SelectedIndex = 0;
        }

        private void EnsureTransactionsTableExists()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
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
                System.Diagnostics.Debug.WriteLine($"Error ensuring transactions table: {ex.Message}");
            }
        }

        private void UpdateReturnDate()
        {
            txtReturnDate.Text = DateTime.Now.ToString("MMMM d, yyyy");
        }

        private void txtCheckOutMemberID_Enter(object sender, EventArgs e)
        {
            if (txtCheckOutMemberID.Text == "Enter member ID (e.g., M1001)")
            {
                txtCheckOutMemberID.Text = "";
                txtCheckOutMemberID.ForeColor = Color.Black;
            }
        }

        private void txtCheckOutMemberID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCheckOutMemberID.Text))
            {
                txtCheckOutMemberID.Text = "Enter member ID (e.g., M1001)";
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
            if (txtCheckOutBookID.Text == "Enter book ID (e.g., B1001)")
            {
                txtCheckOutBookID.Text = "";
                txtCheckOutBookID.ForeColor = Color.Black;
            }
        }

        private void txtCheckOutBookID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCheckOutBookID.Text))
            {
                txtCheckOutBookID.Text = "Enter book ID (e.g., B1001)";
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

                int memberId = 0;
                if (memberIdText.StartsWith("M") || memberIdText.StartsWith("MEM-"))
                {
                    string idPart = memberIdText.Replace("M", "").Replace("MEM-", "");
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

                int bookId = 0;
                if (bookIdText.StartsWith("B"))
                {
                    string idPart = bookIdText.Replace("B", "");
                    int.TryParse(idPart, out bookId);
                }
                else
                {
                    int.TryParse(bookIdText, out bookId);
                }

                if (bookId == 0)
                {

                    bookId = GetBookIdFromAccession(bookIdText);
                    if (bookId == 0)
                    {
                        MessageBox.Show("Book not found with the provided ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (!MemberExists(memberId))
                {
                    MessageBox.Show("Member not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!IsBookAvailable(bookId))
                {
                    MessageBox.Show("Book is not available for borrowing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!CanMemberBorrow(memberId))
                {
                    MessageBox.Show("Member has reached their borrowing limit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DateTime borrowDate = DateTime.Now;
                DateTime dueDate = borrowDate.AddDays(14);

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    bool hasTransactionType = CheckColumnExists(conn, "Transactions", "TransactionType");

                    string insertQuery;
                    if (hasTransactionType)
                    {
                        insertQuery = @"INSERT INTO Transactions (MemberID, BookID, BorrowDate, DueDate, Status, TransactionType)
                                       VALUES (@MemberID, @BookID, @BorrowDate, @DueDate, 'Borrowed', 'Borrow')";
                    }
                    else
                    {
                        insertQuery = @"INSERT INTO Transactions (MemberID, BookID, BorrowDate, DueDate, Status)
                                       VALUES (@MemberID, @BookID, @BorrowDate, @DueDate, 'Borrowed')";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", memberId);
                        cmd.Parameters.AddWithValue("@BookID", bookId);
                        cmd.Parameters.AddWithValue("@BorrowDate", borrowDate);
                        cmd.Parameters.AddWithValue("@DueDate", dueDate);
                        cmd.ExecuteNonQuery();
                    }

                    string updateBookQuery = "UPDATE Books SET Available = Available - 1 WHERE BookID = @BookID AND Available > 0";
                    using (MySqlCommand cmd = new MySqlCommand(updateBookQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookID", bookId);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            throw new InvalidOperationException("Book availability could not be updated. The book may no longer be available.");
                        }
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
                if (bookIdText.StartsWith("B"))
                {
                    string idPart = bookIdText.Replace("B", "");
                    int.TryParse(idPart, out bookId);
                }
                else
                {
                    int.TryParse(bookIdText, out bookId);
                }

                if (bookId == 0)
                {

                    bookId = GetBookIdFromAccession(bookIdText);
                    if (bookId == 0)
                    {
                        MessageBox.Show("Book not found with the provided ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                int transactionId = GetActiveTransactionId(bookId);
                if (transactionId == 0)
                {
                    MessageBox.Show("No active borrowing found for this book.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal fine = CalculateFine(transactionId);

                DateTime returnDate = DateTime.Now;

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    bool hasTransactionType = CheckColumnExists(conn, "Transactions", "TransactionType");
                    bool hasFine = CheckColumnExists(conn, "Transactions", "Fine");

                    string updateQuery;
                    if (hasTransactionType && hasFine)
                    {
                        updateQuery = @"UPDATE Transactions 
                                       SET ReturnDate = @ReturnDate, Status = 'Returned', TransactionType = 'Return', Fine = @Fine
                                       WHERE TransactionID = @TransactionID";
                    }
                    else if (hasFine)
                    {
                        updateQuery = @"UPDATE Transactions 
                                       SET ReturnDate = @ReturnDate, Status = 'Returned', Fine = @Fine
                                       WHERE TransactionID = @TransactionID";
                    }
                    else if (hasTransactionType)
                    {
                        updateQuery = @"UPDATE Transactions 
                                       SET ReturnDate = @ReturnDate, Status = 'Returned', TransactionType = 'Return'
                                       WHERE TransactionID = @TransactionID";
                    }
                    else
                    {
                        updateQuery = @"UPDATE Transactions 
                                       SET ReturnDate = @ReturnDate, Status = 'Returned'
                                       WHERE TransactionID = @TransactionID";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@ReturnDate", returnDate);
                        cmd.Parameters.AddWithValue("@TransactionID", transactionId);
                        if (hasFine)
                        {
                            cmd.Parameters.AddWithValue("@Fine", fine);
                        }
                        cmd.ExecuteNonQuery();
                    }

                    string updateBookQuery = "UPDATE Books SET Available = Available + 1 WHERE BookID = @BookID";
                    using (MySqlCommand cmd = new MySqlCommand(updateBookQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookID", bookId);
                        cmd.ExecuteNonQuery();
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
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT t.TransactionID, t.MemberID, t.BookID, t.BorrowDate, t.DueDate, t.ReturnDate, 
                                    t.Status, t.TransactionType, t.Fine,
                                    b.Title as BookTitle, b.AccessionNo,
                                    m.FirstName, m.LastName, m.MemberID as MemberIDNum
                                    FROM Transactions t
                                    INNER JOIN Books b ON t.BookID = b.BookID
                                    INNER JOIN Members m ON t.MemberID = m.MemberID";

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

            Panel card = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(1500, 80),
                Margin = new Padding(0, 5, 0, 5)
            };

            Panel iconPanel = new Panel
            {
                BackColor = status == "Returned" ? Color.FromArgb(13, 110, 253) : Color.FromArgb(40, 167, 69),
                Size = new Size(50, 50),
                Location = new Point(20, 15)
            };
            iconPanel.Paint += (s, e) => DrawTransactionIcon(e.Graphics, iconPanel, status == "Returned");
            card.Controls.Add(iconPanel);

            Label lblBookTitle = new Label
            {
                Text = bookTitle,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                AutoSize = true,
                Location = new Point(90, 15)
            };
            card.Controls.Add(lblBookTitle);

            if (status == "Borrowed" || status == "Active")
            {
                Label lblStatus = new Label
                {
                    Text = "Active",
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    ForeColor = Color.White,
                    BackColor = Color.FromArgb(40, 167, 69),
                    AutoSize = true,
                    Padding = new Padding(8, 4, 8, 4),
                    Location = new Point(90, 40)
                };
                card.Controls.Add(lblStatus);
            }
            else if (returnDate.HasValue && dueDate.HasValue && returnDate.Value > dueDate.Value)
            {
                Label lblStatus = new Label
                {
                    Text = "Overdue",
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    ForeColor = Color.White,
                    BackColor = Color.FromArgb(220, 53, 69),
                    AutoSize = true,
                    Padding = new Padding(8, 4, 8, 4),
                    Location = new Point(90, 40)
                };
                card.Controls.Add(lblStatus);
            }

            Label lblMember = new Label
            {
                Text = $"{memberName} (M{memberId})",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(128, 128, 128),
                AutoSize = true,
                Location = new Point(200, 25)
            };
            card.Controls.Add(lblMember);

            string dateText = "";
            if (status == "Returned" && returnDate.HasValue)
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
                ForeColor = Color.FromArgb(64, 64, 64),
                AutoSize = true,
                Location = new Point(1200, 25)
            };
            card.Controls.Add(lblDate);

            Label lblCheckmark = new Label
            {
                Text = "?",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = status == "Returned" ? Color.FromArgb(40, 167, 69) : Color.FromArgb(40, 167, 69),
                AutoSize = true,
                Location = new Point(1450, 25)
            };
            card.Controls.Add(lblCheckmark);

            return card;
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

        private int GetBookIdFromAccession(string accessionNo)
        {
            try
            {
                string cleanAccession = accessionNo.Replace("ACC-", "").Trim();
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT BookID FROM Books 
                                    WHERE Barcode = @Accession 
                                    OR AccessionNo = @Accession
                                    OR BookID = @BookID";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Accession", accessionNo);
                        if (int.TryParse(cleanAccession, out int bookId))
                        {
                            cmd.Parameters.AddWithValue("@BookID", bookId);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@BookID", 0);
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
                using (MySqlConnection conn = new MySqlConnection(connectionString))
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
                using (MySqlConnection conn = new MySqlConnection(connectionString))
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
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT m.MemberType, 
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
                using (MySqlConnection conn = new MySqlConnection(connectionString))
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

        private decimal CalculateFine(int transactionId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT DueDate FROM Transactions WHERE TransactionID = @TransactionID";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TransactionID", transactionId);
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            DateTime dueDate = Convert.ToDateTime(result);
                            DateTime currentDate = DateTime.Now;

                            if (dueDate < currentDate)
                            {

                                TimeSpan overdueTime = currentDate - dueDate;
                                int daysOverdue = overdueTime.Days;

                                if (daysOverdue == 0 && overdueTime.TotalHours > 0)
                                {
                                    daysOverdue = 1;
                                }

                                decimal finePerDay = 0.50m;
                                decimal calculatedFine = daysOverdue * finePerDay;

                                return Math.Round(calculatedFine, 2, MidpointRounding.AwayFromZero);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error calculating fine for transaction {transactionId}: {ex.Message}");

                return 0m;
            }
            return 0m;
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
    }
}
