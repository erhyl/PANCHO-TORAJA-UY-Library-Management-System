using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Project5LMS.Helpers;
using Project5LMS.Data;
using Project5LMS.Services;
using Project5LMS.Interfaces;
using Project5LMS.Forms.Admin.Search;
namespace Project5LMS.Forms.LibraryStaff.Fines
{
    public partial class StaffFinesForm : Form
    {
        private DataTable allFinesData;
        private const int CardWidth = 568;
        private const int CardHeight = 180;
        private const int CardSpacing = 15;
        private readonly DatabaseContext _dbContext;
        private readonly IPaymentService _paymentService;
        public StaffFinesForm()
        {
            InitializeComponent();
            _dbContext = ServiceFactory.GetDbContext();
            _paymentService = ServiceFactory.CreatePaymentService();
        }
        private void StaffFinesForm_Load(object sender, EventArgs e)
        {
            EnsureFinesTableExists();
            LoadMetrics();
            LoadActiveFines();
        }
        private void EnsureFinesTableExists()
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string checkTableQuery = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES 
                                              WHERE TABLE_SCHEMA = DATABASE() 
                                              AND TABLE_NAME = 'Fines'";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkTableQuery, conn))
                    {
                        int tableExists = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (tableExists == 0)
                        {
                            string createTableQuery = @"CREATE TABLE IF NOT EXISTS Fines (
                                                        FineID INT AUTO_INCREMENT PRIMARY KEY,
                                                        MemberID INT NOT NULL,
                                                        BookID INT NULL,
                                                        TransactionID INT NULL,
                                                        FineType VARCHAR(50) DEFAULT 'Overdue',
                                                        Amount DECIMAL(10,2) NOT NULL,
                                                        Paid DECIMAL(10,2) DEFAULT 0.00,
                                                        Status VARCHAR(50) DEFAULT 'Pending',
                                                        DaysOverdue INT DEFAULT 0,
                                                        CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
                                                        PaidDate DATETIME NULL,
                                                        WaivedDate DATETIME NULL,
                                                        Description VARCHAR(255) NULL,
                                                        FOREIGN KEY (MemberID) REFERENCES Members(MemberID),
                                                        FOREIGN KEY (BookID) REFERENCES Books(BookID),
                                                        FOREIGN KEY (TransactionID) REFERENCES Transactions(TransactionID)
                                                        )";
                            using (MySqlCommand createCmd = new MySqlCommand(createTableQuery, conn))
                            {
                                createCmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            AddColumnIfNotExists(conn, "Fines", "FineType", "VARCHAR(50) DEFAULT 'Overdue'");
                            AddColumnIfNotExists(conn, "Fines", "DaysOverdue", "INT DEFAULT 0");
                            AddColumnIfNotExists(conn, "Fines", "CreatedDate", "DATETIME DEFAULT CURRENT_TIMESTAMP");
                            AddColumnIfNotExists(conn, "Fines", "PaidDate", "DATETIME NULL");
                            AddColumnIfNotExists(conn, "Fines", "WaivedDate", "DATETIME NULL");
                            AddColumnIfNotExists(conn, "Fines", "Description", "VARCHAR(255) NULL");
                            AddColumnIfNotExists(conn, "Fines", "TransactionID", "INT NULL");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error ensuring Fines table exists: {ex.Message}");
            }
        }
        private void AddColumnIfNotExists(MySqlConnection conn, string tableName, string columnName, string columnDefinition)
        {
            try
            {
                if (!DatabaseSchemaHelper.CheckColumnExists(conn, tableName, columnName))
                {
                    string alterQuery = $"ALTER TABLE {tableName} ADD COLUMN {columnName} {columnDefinition}";
                    using (MySqlCommand cmd = new MySqlCommand(alterQuery, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error adding column {columnName}: {ex.Message}");
            }
        }
        private void LoadMetrics()
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    bool hasFinesTable = CheckTableExists(conn, "Fines");
                    if (hasFinesTable)
                    {
                        bool hasPaidDate = DatabaseSchemaHelper.CheckColumnExists(conn, "Fines", "PaidDate");
                        bool hasWaivedDate = DatabaseSchemaHelper.CheckColumnExists(conn, "Fines", "WaivedDate");
                        string queryTotal = "SELECT COALESCE(SUM(Amount), 0) FROM Fines";
                        using (MySqlCommand cmd = new MySqlCommand(queryTotal, conn))
                        {
                            decimal total = Convert.ToDecimal(cmd.ExecuteScalar());
                            lblMetricTotalFinesValue.Text = $"${total:F2}";
                        }
                        string queryPending = hasPaidDate && hasWaivedDate
                            ? "SELECT COALESCE(SUM(Amount), 0) FROM Fines WHERE PaidDate IS NULL AND WaivedDate IS NULL"
                            : "SELECT COALESCE(SUM(Amount), 0) FROM Fines WHERE Status = 'Pending'";
                        using (MySqlCommand cmd = new MySqlCommand(queryPending, conn))
                        {
                            decimal pending = Convert.ToDecimal(cmd.ExecuteScalar());
                            lblMetricPendingValue.Text = $"${pending:F2}";
                        }
                        bool hasDaysOverdue = DatabaseSchemaHelper.CheckColumnExists(conn, "Fines", "DaysOverdue");
                        string queryOverdue = hasDaysOverdue
                            ? "SELECT COALESCE(SUM(Amount), 0) FROM Fines WHERE DaysOverdue > 0 AND (PaidDate IS NULL OR WaivedDate IS NULL)"
                            : "SELECT COALESCE(SUM(Amount), 0) FROM Fines WHERE Status = 'Overdue'";
                        using (MySqlCommand cmd = new MySqlCommand(queryOverdue, conn))
                        {
                            decimal overdue = Convert.ToDecimal(cmd.ExecuteScalar());
                            lblMetricOverdueValue.Text = $"${overdue:F2}";
                        }
                        string queryCollected = hasPaidDate
                            ? "SELECT COALESCE(SUM(Amount), 0) FROM Fines WHERE PaidDate IS NOT NULL"
                            : "SELECT COALESCE(SUM(Paid), 0) FROM Fines WHERE Status = 'Paid'";
                        using (MySqlCommand cmd = new MySqlCommand(queryCollected, conn))
                        {
                            decimal collected = Convert.ToDecimal(cmd.ExecuteScalar());
                            lblMetricCollectedValue.Text = $"${collected:F2}";
                        }
                    }
                    else
                    {
                        bool hasFine = DatabaseSchemaHelper.CheckColumnExists(conn, "Transactions", "Fine");
                        if (hasFine)
                        {
                            string queryTotal = "SELECT COALESCE(SUM(Fine), 0) FROM Transactions WHERE Fine > 0";
                            using (MySqlCommand cmd = new MySqlCommand(queryTotal, conn))
                            {
                                decimal total = Convert.ToDecimal(cmd.ExecuteScalar());
                                lblMetricTotalFinesValue.Text = $"${total:F2}";
                            }
                            string queryPending = @"SELECT COALESCE(SUM(Fine), 0) FROM Transactions 
                                                  WHERE ReturnDate IS NULL AND DueDate < NOW() AND Fine > 0";
                            using (MySqlCommand cmd = new MySqlCommand(queryPending, conn))
                            {
                                decimal pending = Convert.ToDecimal(cmd.ExecuteScalar());
                                lblMetricPendingValue.Text = $"${pending:F2}";
                            }
                            lblMetricOverdueValue.Text = "$0.00";
                            lblMetricCollectedValue.Text = "$0.00";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading metrics: {ex.Message}");
            }
        }
        private bool CheckTableExists(MySqlConnection conn, string tableName)
        {
            string query = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES 
                           WHERE TABLE_SCHEMA = DATABASE() 
                           AND TABLE_NAME = @TableName";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TableName", tableName);
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }
        private void LoadActiveFines()
        {
            try
            {
                panelActiveFinesList.Controls.Clear();
                allFinesData = GetFinesData();
                if (allFinesData == null || allFinesData.Rows.Count == 0)
                {
                    Label lblNoFines = new Label
                    {
                        Text = "No active fines found",
                        Font = new Font("Segoe UI", 12F),
                        ForeColor = Color.Gray,
                        AutoSize = true,
                        Margin = new Padding(20, 20, 0, 0)
                    };
                    panelActiveFinesList.Controls.Add(lblNoFines);
                    return;
                }
                foreach (DataRow row in allFinesData.Rows)
                {
                    decimal amount = Convert.ToDecimal(row["Amount"]);
                    decimal paid = Convert.ToDecimal(row["Paid"]);
                    string status = DetermineFineStatus(row, amount, paid);
                    if (status == "Pending" || status == "Overdue" || status == "Partial")
                    {
                        Panel fineCard = CreateFineCard(row, status);
                        panelActiveFinesList.Controls.Add(fineCard);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading active fines: {ex.Message}");
                MessageBox.Show($"Error loading fines: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private DataTable GetFinesData()
        {
            using (var conn = _dbContext.GetConnection())
            {
                conn.Open();
                bool hasFineType = DatabaseSchemaHelper.CheckColumnExists(conn, "Fines", "FineType");
                bool hasDaysOverdue = DatabaseSchemaHelper.CheckColumnExists(conn, "Fines", "DaysOverdue");
                bool hasDescription = DatabaseSchemaHelper.CheckColumnExists(conn, "Fines", "Description");
                bool hasCreatedDate = DatabaseSchemaHelper.CheckColumnExists(conn, "Fines", "CreatedDate");
                bool hasPaidDate = DatabaseSchemaHelper.CheckColumnExists(conn, "Fines", "PaidDate");
                bool hasBookID = DatabaseSchemaHelper.CheckColumnExists(conn, "Fines", "BookID");
                bool hasTransactionID = DatabaseSchemaHelper.CheckColumnExists(conn, "Fines", "TransactionID");
                string bookIDSelect = hasBookID ? "f.BookID," : "NULL as BookID,";
                string bookJoin = hasBookID 
                    ? "LEFT JOIN Books b ON f.BookID = b.BookID" 
                    : (hasTransactionID 
                        ? "LEFT JOIN Transactions t ON f.TransactionID = t.TransactionID LEFT JOIN Books b ON t.BookID = b.BookID"
                        : "LEFT JOIN Books b ON 1=0");
                string query;
                if (hasFineType && hasDaysOverdue && hasDescription && hasCreatedDate)
                {
                    query = $@"SELECT 
                                f.FineID,
                                f.MemberID,
                                {bookIDSelect}
                                f.FineType,
                                f.Amount,
                                f.Paid,
                                f.Status,
                                f.DaysOverdue,
                                f.Description,
                                f.CreatedDate,
                                " + (hasPaidDate ? "f.PaidDate," : "NULL as PaidDate,") + @"
                                m.FirstName,
                                m.LastName,
                                b.Title as BookTitle
                             FROM Fines f
                             INNER JOIN Members m ON f.MemberID = m.MemberID
                             " + bookJoin + @"
                             ORDER BY f.CreatedDate DESC";
                }
                else
                {
                    query = $@"SELECT 
                                f.FineID,
                                f.MemberID,
                                {bookIDSelect}
                                COALESCE(f.FineType, 'Overdue') as FineType,
                                f.Amount,
                                f.Paid,
                                f.Status,
                                0 as DaysOverdue,
                                NULL as Description,
                                NOW() as CreatedDate,
                                NULL as PaidDate,
                                m.FirstName,
                                m.LastName,
                                b.Title as BookTitle
                             FROM Fines f
                             INNER JOIN Members m ON f.MemberID = m.MemberID
                             " + bookJoin + @"
                             ORDER BY f.FineID DESC";
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
        private string DetermineFineStatus(DataRow row, decimal amount, decimal paid)
        {
            string currentStatus = row["Status"] != DBNull.Value ? row["Status"].ToString() : "Pending";
            if (currentStatus == "Waived")
                return "Waived";
            else if (paid >= amount)
                return "Paid";
            else if (paid > 0)
                return "Partial";
            else
            {
                bool hasDaysOverdue = row.Table.Columns.Contains("DaysOverdue") && row["DaysOverdue"] != DBNull.Value;
                if (hasDaysOverdue && Convert.ToInt32(row["DaysOverdue"]) > 0)
                    return "Overdue";
                else
                    return "Pending";
            }
        }
        private Panel CreateFineCard(DataRow fineRow, string status)
        {
            Panel card = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(CardWidth, CardHeight),
                Margin = new Padding(0, 0, 0, CardSpacing),
                Padding = new Padding(20),
                Cursor = Cursors.Hand
            };
            card.Click += (s, e) => SelectFineForPayment(fineRow);
            string firstName = fineRow["FirstName"] != DBNull.Value ? fineRow["FirstName"].ToString() : "";
            string lastName = fineRow["LastName"] != DBNull.Value ? fineRow["LastName"].ToString() : "";
            int memberId = Convert.ToInt32(fineRow["MemberID"]);
            Label lblMember = new Label
            {
                Text = $"{firstName} {lastName} (M{memberId})",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                AutoSize = true,
                Location = new Point(20, 20)
            };
            card.Controls.Add(lblMember);
            Panel statusBadge = CreateStatusBadge(status, CardWidth - 120, 20);
            card.Controls.Add(statusBadge);
            string bookTitle = fineRow["BookTitle"] != DBNull.Value ? fineRow["BookTitle"].ToString() : "N/A";
            Label lblBook = new Label
            {
                Text = $"Book: {bookTitle}",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(128, 128, 128),
                AutoSize = true,
                Location = new Point(20, 50),
                MaximumSize = new Size(CardWidth - 40, 0)
            };
            card.Controls.Add(lblBook);
            string reason = GetFineReason(fineRow);
            Label lblReason = new Label
            {
                Text = $"Reason: {reason}",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(128, 128, 128),
                AutoSize = true,
                Location = new Point(20, 75),
                MaximumSize = new Size(CardWidth - 40, 0)
            };
            card.Controls.Add(lblReason);
            DateTime issuedDate = fineRow["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(fineRow["CreatedDate"]) : DateTime.Now;
            Label lblIssued = new Label
            {
                Text = $"Issued: {issuedDate:yyyy-MM-dd}",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(128, 128, 128),
                AutoSize = true,
                Location = new Point(20, 100)
            };
            card.Controls.Add(lblIssued);
            decimal amount = Convert.ToDecimal(fineRow["Amount"]);
            Label lblAmount = new Label
            {
                Text = $"Fine Amount: ${amount:F2}",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                AutoSize = true,
                Location = new Point(20, 125)
            };
            card.Controls.Add(lblAmount);
            if (fineRow.Table.Columns.Contains("PaidDate") && fineRow["PaidDate"] == DBNull.Value)
            {
                DateTime dueDate = issuedDate.AddDays(7);
                Label lblDue = new Label
                {
                    Text = $"Due: {dueDate:yyyy-MM-dd}",
                    Font = new Font("Segoe UI", 9F),
                    ForeColor = Color.FromArgb(128, 128, 128),
                    AutoSize = true,
                    Location = new Point(250, 100)
                };
                card.Controls.Add(lblDue);
            }
            int fineId = Convert.ToInt32(fineRow["FineID"]);
            Label lblFineID = new Label
            {
                Text = $"Fine ID: F{fineId}",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(128, 128, 128),
                AutoSize = true,
                Location = new Point(250, 125)
            };
            card.Controls.Add(lblFineID);
            if (status == "Pending" || status == "Partial" || status == "Overdue")
            {
                decimal Amount = Convert.ToDecimal(fineRow["Amount"]);
                decimal paid = Convert.ToDecimal(fineRow["Paid"]);
                decimal balance = amount - paid;
                Button btnPay = new Button
                {
                    Text = balance > 0 ? "Pay" : "Paid",
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    ForeColor = Color.White,
                    BackColor = Color.FromArgb(40, 167, 69),
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(70, 30),
                    Location = new Point(400, 120),
                    Enabled = balance > 0,
                    Cursor = Cursors.Hand,
                    Tag = fineRow
                };
                btnPay.FlatAppearance.BorderSize = 0;
                btnPay.Click += (s, e) => ProcessPayment(fineRow);
                card.Controls.Add(btnPay);
                Button btnWaive = new Button
                {
                    Text = "Waive",
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    ForeColor = Color.White,
                    BackColor = Color.FromArgb(13, 110, 253),
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(70, 30),
                    Location = new Point(480, 120),
                    Cursor = Cursors.Hand,
                    Tag = fineRow
                };
                btnWaive.FlatAppearance.BorderSize = 0;
                btnWaive.Click += (s, e) => WaiveFine(fineRow);
                card.Controls.Add(btnWaive);
            }
            return card;
        }
        private Panel CreateStatusBadge(string status, int x, int y)
        {
            Panel badge = new Panel
            {
                Size = new Size(100, 25),
                Location = new Point(x, y)
            };
            Color bgColor = Color.LightGray;
            Color textColor = Color.Black;
            switch (status.ToLower())
            {
                case "overdue":
                    bgColor = Color.FromArgb(220, 53, 69);
                    textColor = Color.White;
                    break;
                case "pending":
                    bgColor = Color.FromArgb(255, 193, 7);
                    textColor = Color.White;
                    break;
                case "paid":
                    bgColor = Color.FromArgb(40, 167, 69);
                    textColor = Color.White;
                    break;
            }
            badge.BackColor = bgColor;
            badge.Paint += (s, e) =>
            {
                using (GraphicsPath path = new GraphicsPath())
                {
                    int radius = 12;
                    path.AddArc(0, 0, radius, radius, 180, 90);
                    path.AddArc(badge.Width - radius, 0, radius, radius, 270, 90);
                    path.AddArc(badge.Width - radius, badge.Height - radius, radius, radius, 0, 90);
                    path.AddArc(0, badge.Height - radius, radius, radius, 90, 90);
                    path.CloseAllFigures();
                    badge.Region = new Region(path);
                }
                using (SolidBrush brush = new SolidBrush(textColor))
                using (StringFormat format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                {
                    e.Graphics.DrawString(status, new Font("Segoe UI", 9F, FontStyle.Bold), brush, badge.ClientRectangle, format);
                }
            };
            return badge;
        }
        private string GetFineReason(DataRow fineRow)
        {
            if (fineRow.Table.Columns.Contains("Description") && fineRow["Description"] != DBNull.Value && !string.IsNullOrEmpty(fineRow["Description"].ToString()))
            {
                return fineRow["Description"].ToString();
            }
            string fineType = fineRow["FineType"] != DBNull.Value ? fineRow["FineType"].ToString() : "Overdue";
            int daysOverdue = 0;
            if (fineRow.Table.Columns.Contains("DaysOverdue") && fineRow["DaysOverdue"] != DBNull.Value)
            {
                daysOverdue = Convert.ToInt32(fineRow["DaysOverdue"]);
            }
            if (fineType == "Overdue" && daysOverdue > 0)
            {
                return $"Late Return ({daysOverdue} day{(daysOverdue > 1 ? "s" : "")})";
            }
            return fineType;
        }
        private void SelectFineForPayment(DataRow fineRow)
        {
            int fineId = Convert.ToInt32(fineRow["FineID"]);
            string firstName = fineRow["FirstName"] != DBNull.Value ? fineRow["FirstName"].ToString() : "";
            string lastName = fineRow["LastName"] != DBNull.Value ? fineRow["LastName"].ToString() : "";
            decimal amount = Convert.ToDecimal(fineRow["Amount"]);
            decimal paid = Convert.ToDecimal(fineRow["Paid"]);
            decimal balance = amount - paid;
            lblProcessPaymentPlaceholder.Text = $"Fine ID: F{fineId}\nMember: {firstName} {lastName}\nAmount: ${amount:F2}\nPaid: ${paid:F2}\nBalance: ${balance:F2}";
            lblProcessPaymentPlaceholder.ForeColor = Color.FromArgb(64, 64, 64);
            lblProcessPaymentPlaceholder.Font = new Font("Segoe UI", 11F);
            panelProcessPayment.Tag = fineRow;
        }
        private void ProcessPayment(DataRow fineRow)
        {
            try
            {
                int fineId = Convert.ToInt32(fineRow["FineID"]);
                int memberId = Convert.ToInt32(fineRow["MemberID"]);
                int transactionId = fineRow.Table.Columns.Contains("TransactionID") && fineRow["TransactionID"] != DBNull.Value
                    ? Convert.ToInt32(fineRow["TransactionID"]) : 0;
                decimal amount = Convert.ToDecimal(fineRow["Amount"]);
                decimal paid = Convert.ToDecimal(fineRow["Paid"]);
                decimal balance = amount - paid;
                using (Form paymentForm = new Form())
                {
                    paymentForm.Text = "Process Payment";
                    paymentForm.Size = new Size(400, 250);
                    paymentForm.StartPosition = FormStartPosition.CenterParent;
                    Label lblAmount = new Label
                    {
                        Text = $"Amount to Pay: ${balance:F2}",
                        Location = new Point(10, 20),
                        AutoSize = true,
                        Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                    };
                    paymentForm.Controls.Add(lblAmount);
                    Label lblPaymentMode = new Label
                    {
                        Text = "Payment Mode:",
                        Location = new Point(10, 60),
                        AutoSize = true
                    };
                    paymentForm.Controls.Add(lblPaymentMode);
                    ComboBox cmbPaymentMode = new ComboBox
                    {
                        Location = new Point(10, 85),
                        Size = new Size(360, 30),
                        DropDownStyle = ComboBoxStyle.DropDownList
                    };
                    cmbPaymentMode.Items.AddRange(new string[] { "Cash", "Online", "Check" });
                    cmbPaymentMode.SelectedIndex = 0;
                    paymentForm.Controls.Add(cmbPaymentMode);
                    Button btnOK = new Button
                    {
                        Text = "Process Payment",
                        DialogResult = DialogResult.OK,
                        Location = new Point(200, 150),
                        Size = new Size(170, 35),
                        BackColor = Color.FromArgb(40, 167, 69),
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat
                    };
                    btnOK.FlatAppearance.BorderSize = 0;
                    paymentForm.Controls.Add(btnOK);
                    Button btnCancel = new Button
                    {
                        Text = "Cancel",
                        DialogResult = DialogResult.Cancel,
                        Location = new Point(10, 150),
                        Size = new Size(80, 35)
                    };
                    paymentForm.Controls.Add(btnCancel);
                    paymentForm.AcceptButton = btnOK;
                    paymentForm.CancelButton = btnCancel;
                    if (paymentForm.ShowDialog() != DialogResult.OK)
                        return;
                    var payment = new Project5LMS.Models.FinePayment
                    {
                        TransactionID = transactionId > 0 ? transactionId : 0,
                        MemberID = memberId,
                        AmountPaid = balance,
                        PaymentMode = cmbPaymentMode.SelectedItem?.ToString() ?? "Cash",
                        ProcessedBy = Project5LMS.Helpers.CurrentUser.FullName ?? "Staff"
                    };
                    bool success = false;
                    using (var statusForm = new TransactionStatusForm("Payment Processing"))
                    {
                        statusForm.Show();
                        Application.DoEvents();
                        try
                        {
                            statusForm.UpdateStatus("Processing payment...");
                            success = _paymentService.ProcessPayment(payment);
                            if (success)
                            {
                                statusForm.UpdateStatus("Payment recorded successfully!");
                                System.Threading.Thread.Sleep(500);
                            }
                            statusForm.Close();
                        }
                        catch
                        {
                            statusForm.Close();
                            throw;
                        }
                    }
                    if (success)
                    {
                        using (var conn = _dbContext.GetConnection())
                        {
                            conn.Open();
                            decimal newPaid = paid + balance;
                            bool hasPaidDate = DatabaseSchemaHelper.CheckColumnExists(conn, "Fines", "PaidDate");
                            string updateQuery = hasPaidDate
                                ? "UPDATE Fines SET Paid = @NewPaid, Status = 'Paid', PaidDate = @PaidDate WHERE FineID = @FineID"
                                : "UPDATE Fines SET Paid = @NewPaid, Status = 'Paid' WHERE FineID = @FineID";
                            using (var cmd = new MySqlCommand(updateQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@FineID", fineId);
                                cmd.Parameters.AddWithValue("@NewPaid", newPaid);
                                if (hasPaidDate)
                                    cmd.Parameters.AddWithValue("@PaidDate", DateTime.Now);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        MessageBox.Show($"Payment of ${balance:F2} processed successfully.\nReceipt: {payment.ReceiptNumber}",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadMetrics();
                        LoadActiveFines();
                    }
                    else
                    {
                        MessageBox.Show("Failed to process payment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing payment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void WaiveFine(DataRow fineRow)
        {
            string reason = "";
            using (Form inputForm = new Form())
            {
                inputForm.Text = "Waive Fine";
                inputForm.Size = new Size(400, 200);
                inputForm.StartPosition = FormStartPosition.CenterParent;
                Label lblReason = new Label
                {
                    Text = "Reason for waiver:",
                    Location = new Point(10, 20),
                    AutoSize = true
                };
                inputForm.Controls.Add(lblReason);
                TextBox txtReason = new TextBox
                {
                    Location = new Point(10, 45),
                    Size = new Size(360, 60),
                    Multiline = true,
                    ScrollBars = ScrollBars.Vertical
                };
                inputForm.Controls.Add(txtReason);
                Button btnOK = new Button
                {
                    Text = "OK",
                    DialogResult = DialogResult.OK,
                    Location = new Point(200, 120),
                    Size = new Size(80, 30)
                };
                inputForm.Controls.Add(btnOK);
                Button btnCancel = new Button
                {
                    Text = "Cancel",
                    DialogResult = DialogResult.Cancel,
                    Location = new Point(290, 120),
                    Size = new Size(80, 30)
                };
                inputForm.Controls.Add(btnCancel);
                inputForm.AcceptButton = btnOK;
                inputForm.CancelButton = btnCancel;
                if (inputForm.ShowDialog() != DialogResult.OK)
                    return;
                reason = txtReason.Text.Trim();
            }
            if (string.IsNullOrWhiteSpace(reason))
            {
                MessageBox.Show("Please provide a reason for the waiver.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                int fineId = Convert.ToInt32(fineRow["FineID"]);
                int memberId = Convert.ToInt32(fineRow["MemberID"]);
                int transactionId = fineRow.Table.Columns.Contains("TransactionID") && fineRow["TransactionID"] != DBNull.Value
                    ? Convert.ToInt32(fineRow["TransactionID"]) : 0;
                decimal originalAmount = Convert.ToDecimal(fineRow["Amount"]);
                var adjustment = new Project5LMS.Models.FineAdjustment
                {
                    TransactionID = transactionId > 0 ? transactionId : 0,
                    MemberID = memberId,
                    OriginalAmount = originalAmount,
                    AdjustedAmount = 0,
                    AdjustmentAmount = originalAmount,
                    AdjustmentType = "Waiver",
                    Reason = reason,
                    AdjustedBy = Project5LMS.Helpers.CurrentUser.FullName ?? "Staff"
                };
                bool success = _paymentService.WaiveFine(adjustment);
                if (success)
                {
                    using (var conn = _dbContext.GetConnection())
                    {
                        conn.Open();
                        bool hasWaivedDate = DatabaseSchemaHelper.CheckColumnExists(conn, "Fines", "WaivedDate");
                        string updateQuery = hasWaivedDate
                            ? "UPDATE Fines SET Status = 'Waived', WaivedDate = @WaivedDate WHERE FineID = @FineID"
                            : "UPDATE Fines SET Status = 'Waived' WHERE FineID = @FineID";
                        using (var cmd = new MySqlCommand(updateQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@FineID", fineId);
                            if (hasWaivedDate)
                                cmd.Parameters.AddWithValue("@WaivedDate", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Fine waived successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadMetrics();
                    LoadActiveFines();
                }
                else
                {
                    MessageBox.Show("Failed to waive fine.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error waiving fine: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}