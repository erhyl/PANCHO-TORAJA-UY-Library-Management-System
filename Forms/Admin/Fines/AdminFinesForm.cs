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
using Project5LMS.Repositories;
using Project5LMS.Interfaces;
using Project5LMS.Forms.Admin.Search;
namespace Project5LMS.Forms.Admin.Fines
{
    public partial class AdminFinesForm : Form
    {
        private DataTable allFinesData;
        private string currentStatusFilter = "All Status";
        private string currentTypeFilter = "All Types";
        private readonly IFinesService _finesService;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMembersService _membersService;
        private readonly IBookService _bookService;
        private readonly IPaymentService _paymentService;
        public AdminFinesForm()
        {
            InitializeComponent();
            var dbContext = ServiceFactory.GetDbContext();
            _finesService = ServiceFactory.CreateFinesService();
            _transactionRepository = new TransactionRepository(dbContext);
            _membersService = ServiceFactory.CreateMembersService();
            _bookService = ServiceFactory.CreateBookService();
            _paymentService = ServiceFactory.CreatePaymentService();
        }
        private void AdminFinesForm_Load(object sender, EventArgs e)
        {
            try
            {
                EnsureFinesTableExists();
                SetupDataGridView();
                SetupPaymentHistoryGridView();
                LoadMetrics();
                LoadFines();
                LoadPaymentHistory();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading fines form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Fines form load error: {ex}");
            }
        }
        private void EnsureFinesTableExists()
        {
            try
            {
                var dbContext = ServiceFactory.GetDbContext();
                using (var conn = dbContext.GetConnection())
                {
                    conn.Open();
                    string checkTableQuery = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES
                                              WHERE TABLE_SCHEMA = DATABASE()
                                              AND TABLE_NAME = 'Fines'";
                    using (var checkCmd = new MySqlCommand(checkTableQuery, conn))
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
                            dbContext.ExecuteNonQuery(createTableQuery);
                        }
                        else
                        {
                            DatabaseSchemaHelper.AddColumnIfNotExists(conn, "Fines", "FineType", "VARCHAR(50) DEFAULT 'Overdue'");
                            DatabaseSchemaHelper.AddColumnIfNotExists(conn, "Fines", "DaysOverdue", "INT DEFAULT 0");
                            DatabaseSchemaHelper.AddColumnIfNotExists(conn, "Fines", "CreatedDate", "DATETIME DEFAULT CURRENT_TIMESTAMP");
                            DatabaseSchemaHelper.AddColumnIfNotExists(conn, "Fines", "PaidDate", "DATETIME NULL");
                            DatabaseSchemaHelper.AddColumnIfNotExists(conn, "Fines", "WaivedDate", "DATETIME NULL");
                            DatabaseSchemaHelper.AddColumnIfNotExists(conn, "Fines", "Description", "VARCHAR(255) NULL");
                            DatabaseSchemaHelper.AddColumnIfNotExists(conn, "Fines", "TransactionID", "INT NULL");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error ensuring Fines table exists: {ex.Message}");
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
            dataGridViewFines.Columns.Clear();
            dataGridViewFines.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn colFineID = new DataGridViewTextBoxColumn
            {
                Name = "FineID",
                HeaderText = "FINE ID",
                DataPropertyName = "FineID",
                Width = 120,
                ReadOnly = true
            };
            dataGridViewFines.Columns.Add(colFineID);
            DataGridViewTextBoxColumn colMember = new DataGridViewTextBoxColumn
            {
                Name = "Member",
                HeaderText = "MEMBER",
                DataPropertyName = "Member",
                Width = 200,
                ReadOnly = true
            };
            dataGridViewFines.Columns.Add(colMember);
            DataGridViewTextBoxColumn colBook = new DataGridViewTextBoxColumn
            {
                Name = "BookItem",
                HeaderText = "BOOK/ITEM",
                DataPropertyName = "BookItem",
                Width = 250,
                ReadOnly = true
            };
            dataGridViewFines.Columns.Add(colBook);
            DataGridViewColumn colType = new DataGridViewTextBoxColumn
            {
                Name = "Type",
                HeaderText = "TYPE",
                DataPropertyName = "Type",
                Width = 120,
                ReadOnly = true
            };
            dataGridViewFines.Columns.Add(colType);
            DataGridViewTextBoxColumn colDaysOverdue = new DataGridViewTextBoxColumn
            {
                Name = "DaysOverdue",
                HeaderText = "DAYS OVERDUE",
                DataPropertyName = "DaysOverdue",
                Width = 120,
                ReadOnly = true
            };
            dataGridViewFines.Columns.Add(colDaysOverdue);
            DataGridViewTextBoxColumn colAmount = new DataGridViewTextBoxColumn
            {
                Name = "Amount",
                HeaderText = "AMOUNT",
                DataPropertyName = "Amount",
                Width = 100,
                ReadOnly = true
            };
            dataGridViewFines.Columns.Add(colAmount);
            DataGridViewTextBoxColumn colPaid = new DataGridViewTextBoxColumn
            {
                Name = "Paid",
                HeaderText = "PAID",
                DataPropertyName = "Paid",
                Width = 100,
                ReadOnly = true
            };
            dataGridViewFines.Columns.Add(colPaid);
            DataGridViewColumn colStatus = new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "STATUS",
                DataPropertyName = "Status",
                Width = 120,
                ReadOnly = true
            };
            dataGridViewFines.Columns.Add(colStatus);
            DataGridViewColumn colActions = new DataGridViewTextBoxColumn
            {
                Name = "Actions",
                HeaderText = "ACTIONS",
                DataPropertyName = "Actions",
                Width = 200,
                ReadOnly = true
            };
            dataGridViewFines.Columns.Add(colActions);
            dataGridViewFines.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridViewFines.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridViewFines.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dataGridViewFines.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            dataGridViewFines.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            dataGridViewFines.RowTemplate.Height = 60;
            dataGridViewFines.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dataGridViewFines.CellFormatting += DataGridViewFines_CellFormatting;
            dataGridViewFines.CellPainting += DataGridViewFines_CellPainting;
            dataGridViewFines.CellContentClick += DataGridViewFines_CellContentClick;
            dataGridViewFines.DataError += DataGridViewFines_DataError;
        }
        private void SetupPaymentHistoryGridView()
        {
            if (dataGridViewPaymentHistory == null) return;
            dataGridViewPaymentHistory.Columns.Clear();
            dataGridViewPaymentHistory.AutoGenerateColumns = false;
            dataGridViewPaymentHistory.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dataGridViewPaymentHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dataGridViewPaymentHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dataGridViewPaymentHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            dataGridViewPaymentHistory.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            dataGridViewPaymentHistory.RowTemplate.Height = 40;
            dataGridViewPaymentHistory.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dataGridViewPaymentHistory.CellFormatting += DataGridViewPaymentHistory_CellFormatting;
        }
        private void DataGridViewPaymentHistory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string columnName = dataGridViewPaymentHistory.Columns[e.ColumnIndex].Name;
            if (columnName == "AmountPaid" && e.Value != null)
            {
                if (decimal.TryParse(e.Value.ToString(), out decimal amount))
                {
                    e.Value = $"${amount:F2}";
                }
                e.FormattingApplied = true;
            }
            if (columnName == "PaymentDate" && e.Value != null)
            {
                if (DateTime.TryParse(e.Value.ToString(), out DateTime date))
                {
                    e.Value = date.ToString("yyyy-MM-dd HH:mm");
                }
                e.FormattingApplied = true;
            }
        }
        private void LoadPaymentHistory()
        {
            try
            {
                if (dataGridViewPaymentHistory == null) return;
                var dbContext = ServiceFactory.GetDbContext();
                using (var conn = dbContext.GetConnection())
                {
                    conn.Open();
                    string checkTableQuery = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES
                                              WHERE TABLE_SCHEMA = DATABASE()
                                              AND TABLE_NAME = 'FinePayments'";
                    using (var checkCmd = new MySqlCommand(checkTableQuery, conn))
                    {
                        int tableExists = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (tableExists > 0 && !this.DesignMode)
                        {
                            string query = @"SELECT ReceiptNumber, AmountPaid, PaymentDate, PaymentMode, ProcessedBy
                                           FROM FinePayments
                                           ORDER BY PaymentDate DESC
                                           LIMIT 20";
                            using (var adapter = new MySqlDataAdapter(query, conn))
                            {
                                DataTable dt = new DataTable();
                                adapter.Fill(dt);
                                dataGridViewPaymentHistory.DataSource = dt;
                            }
                        }
                        else
                        {
                            var placeholderData = new List<object>
                            {
                                new { ReceiptNumber = "RCP-001", AmountPaid = 25.50m, PaymentDate = DateTime.Now, PaymentMode = "Cash", ProcessedBy = "Admin" },
                                new { ReceiptNumber = "RCP-002", AmountPaid = 15.00m, PaymentDate = DateTime.Now.AddDays(-1), PaymentMode = "Online", ProcessedBy = "Staff" },
                                new { ReceiptNumber = "RCP-003", AmountPaid = 30.00m, PaymentDate = DateTime.Now.AddDays(-2), PaymentMode = "Check", ProcessedBy = "Admin" }
                            };
                            dataGridViewPaymentHistory.DataSource = placeholderData;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading payment history: {ex.Message}");
                if (this.DesignMode && dataGridViewPaymentHistory != null)
                {
                    var placeholderData = new List<object>
                    {
                        new { ReceiptNumber = "RCP-001", AmountPaid = 25.50m, PaymentDate = DateTime.Now, PaymentMode = "Cash", ProcessedBy = "Admin" },
                        new { ReceiptNumber = "RCP-002", AmountPaid = 15.00m, PaymentDate = DateTime.Now.AddDays(-1), PaymentMode = "Online", ProcessedBy = "Staff" },
                        new { ReceiptNumber = "RCP-003", AmountPaid = 30.00m, PaymentDate = DateTime.Now.AddDays(-2), PaymentMode = "Check", ProcessedBy = "Admin" }
                    };
                    dataGridViewPaymentHistory.DataSource = placeholderData;
                }
            }
        }
        private void DataGridViewFines_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            System.Diagnostics.Debug.WriteLine($"DataGridView error in row {e.RowIndex}, column {e.ColumnIndex}: {e.Exception.Message}");
        }
        private void DataGridViewFines_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            try
            {
                DataGridViewRow row = dataGridViewFines.Rows[e.RowIndex];
                string columnName = dataGridViewFines.Columns[e.ColumnIndex].Name;
                if (columnName == "FineID" && e.Value != null)
                {
                    string fineIdStr = e.Value.ToString();
                    if (int.TryParse(fineIdStr, out int fineId))
                    {
                        e.Value = $"FINE-{fineIdStr.PadLeft(3, '0')}";
                        e.FormattingApplied = true;
                    }
                }
                else if ((columnName == "Amount" || columnName == "Paid") && e.Value != null)
                {
                    object originalValue = e.Value;
                    if (originalValue is decimal decimalValue)
                    {
                        e.Value = $"${decimalValue:F2}";
                        e.FormattingApplied = true;
                    }
                    else if (originalValue is double doubleValue)
                    {
                        e.Value = $"${(decimal)doubleValue:F2}";
                        e.FormattingApplied = true;
                    }
                    else if (decimal.TryParse(originalValue.ToString(), out decimal parsedValue))
                    {
                        e.Value = $"${parsedValue:F2}";
                        e.FormattingApplied = true;
                    }
                }
                else if (columnName == "DaysOverdue" && e.Value != null)
                {
                    if (e.Value is int || e.Value is DBNull)
                    {
                        e.FormattingApplied = true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in CellFormatting: {ex.Message}");
                e.FormattingApplied = false;
            }
        }
        private void DataGridViewFines_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            string columnName = dataGridViewFines.Columns[e.ColumnIndex].Name;
            DataGridViewRow row = dataGridViewFines.Rows[e.RowIndex];
            if (columnName == "Type")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                string value = e.Value?.ToString() ?? "";
                Color bgColor = Color.LightGray;
                Color textColor = Color.Black;
                switch (value.ToLower())
                {
                    case "overdue":
                        bgColor = Color.FromArgb(255, 193, 7);
                        textColor = Color.Black;
                        break;
                    case "lost book":
                        bgColor = Color.FromArgb(220, 53, 69);
                        textColor = Color.White;
                        break;
                    case "damaged book":
                        bgColor = Color.FromArgb(221, 160, 221);
                        textColor = Color.White;
                        break;
                    case "lost card":
                        bgColor = Color.FromArgb(13, 110, 253);
                        textColor = Color.White;
                        break;
                }
                Rectangle badgeRect = new Rectangle(
                    e.CellBounds.X + 5,
                    e.CellBounds.Y + (e.CellBounds.Height - 25) / 2,
                    Math.Min(120, e.CellBounds.Width - 10),
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
                    dataGridViewFines.DefaultCellStyle.Font,
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
                    case "pending":
                        bgColor = Color.FromArgb(220, 53, 69);
                        textColor = Color.White;
                        break;
                    case "partial":
                        bgColor = Color.FromArgb(255, 193, 7);
                        textColor = Color.Black;
                        break;
                    case "paid":
                        bgColor = Color.FromArgb(40, 167, 69);
                        textColor = Color.White;
                        break;
                    case "waived":
                        bgColor = Color.FromArgb(13, 110, 253);
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
                    dataGridViewFines.DefaultCellStyle.Font,
                    badgeRect,
                    textColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                );
                e.Handled = true;
            }
            if (columnName == "Actions")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                string status = row.Cells["Status"]?.Value?.ToString() ?? "";
                decimal amount = 0;
                decimal paid = 0;
                if (row.Cells["Amount"]?.Value != null)
                {
                    string amountStr = row.Cells["Amount"].Value.ToString().Replace("$", "");
                    decimal.TryParse(amountStr, out amount);
                }
                if (row.Cells["Paid"]?.Value != null)
                {
                    string paidStr = row.Cells["Paid"].Value.ToString().Replace("$", "");
                    decimal.TryParse(paidStr, out paid);
                }
                int buttonY = e.CellBounds.Y + (e.CellBounds.Height - 30) / 2;
                int buttonHeight = 30;
                int buttonWidth = 80;
                int spacing = 5;
                int xOffset = e.CellBounds.X + 5;
                if (status.ToLower() == "pending")
                {
                    Rectangle btnCollectRect = new Rectangle(xOffset, buttonY, buttonWidth, buttonHeight);
                    DrawButton(e.Graphics, btnCollectRect, "Collect", Color.FromArgb(40, 167, 69), Color.White);
                    Rectangle btnWaiveRect = new Rectangle(xOffset + buttonWidth + spacing, buttonY, buttonWidth, buttonHeight);
                    DrawButton(e.Graphics, btnWaiveRect, "Waive", Color.FromArgb(13, 110, 253), Color.White);
                    Rectangle btnHistoryRect = new Rectangle(xOffset + (buttonWidth + spacing) * 2, buttonY, buttonWidth, buttonHeight);
                    DrawButton(e.Graphics, btnHistoryRect, "History", Color.FromArgb(108, 117, 125), Color.White);
                }
                else if (status.ToLower() == "partial")
                {
                    Rectangle btnPayBalanceRect = new Rectangle(xOffset, buttonY, buttonWidth + 20, buttonHeight);
                    DrawButton(e.Graphics, btnPayBalanceRect, "Pay Balance", Color.FromArgb(40, 167, 69), Color.White);
                    Rectangle btnHistoryRect = new Rectangle(xOffset + buttonWidth + 25, buttonY, buttonWidth, buttonHeight);
                    DrawButton(e.Graphics, btnHistoryRect, "History", Color.FromArgb(108, 117, 125), Color.White);
                }
                else
                {
                    Rectangle btnHistoryRect = new Rectangle(xOffset, buttonY, buttonWidth, buttonHeight);
                    DrawButton(e.Graphics, btnHistoryRect, "History", Color.FromArgb(108, 117, 125), Color.White);
                }
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
        private void DataGridViewFines_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                string columnName = dataGridViewFines.Columns[e.ColumnIndex].Name;
                if (columnName != "Actions") return;
            DataGridViewRow row = dataGridViewFines.Rows[e.RowIndex];
            int fineId = 0;
            if (row.DataBoundItem is DataRowView drv1)
            {
                fineId = Convert.ToInt32(drv1["FineID"]);
            }
            else if (row.DataBoundItem is DataRow dr)
            {
                fineId = Convert.ToInt32(dr["FineID"]);
            }
            else
            {
                object fineIdObj = row.Cells["FineID"].Value;
                if (fineIdObj != null)
                {
                    string fineIdStr = fineIdObj.ToString().Replace("FINE-", "");
                    int.TryParse(fineIdStr, out fineId);
                }
            }
            string status = row.Cells["Status"].Value?.ToString() ?? "";
            decimal amount = 0;
            decimal paid = 0;
            if (row.Cells["Amount"]?.Value != null)
            {
                string amountStr = row.Cells["Amount"].Value.ToString().Replace("$", "");
                decimal.TryParse(amountStr, out amount);
            }
            if (row.Cells["Paid"]?.Value != null)
            {
                string paidStr = row.Cells["Paid"].Value.ToString().Replace("$", "");
                decimal.TryParse(paidStr, out paid);
            }
            Point clickPoint = dataGridViewFines.PointToClient(Control.MousePosition);
            Rectangle cellRect = dataGridViewFines.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            int buttonY = cellRect.Y + (cellRect.Height - 30) / 2;
            int buttonWidth = 80;
            int spacing = 5;
            int xOffset = cellRect.X + 5;
            int memberId = 0;
            if (row.DataBoundItem is DataRowView drv2)
            {
                memberId = Convert.ToInt32(drv2["MemberID"]);
            }
            else if (row.DataBoundItem is DataRow dr)
            {
                memberId = Convert.ToInt32(dr["MemberID"]);
            }
            else
            {
                var memberIdCell = row.Cells["MemberID"];
                if (memberIdCell?.Value != null)
                {
                    int.TryParse(memberIdCell.Value.ToString(), out memberId);
                }
            }
            if (status.ToLower() == "pending")
            {
                Rectangle btnCollectRect = new Rectangle(xOffset, buttonY, buttonWidth, 30);
                Rectangle btnWaiveRect = new Rectangle(xOffset + buttonWidth + spacing, buttonY, buttonWidth, 30);
                Rectangle btnHistoryRect = new Rectangle(xOffset + (buttonWidth + spacing) * 2, buttonY, buttonWidth, 30);
                if (btnCollectRect.Contains(clickPoint))
                {
                    CollectFine(fineId, amount);
                }
                else if (btnWaiveRect.Contains(clickPoint))
                {
                    WaiveFine(fineId);
                }
                else if (btnHistoryRect.Contains(clickPoint) && memberId > 0)
                {
                    ShowPaymentHistory(memberId);
                }
            }
            else if (status.ToLower() == "partial")
            {
                Rectangle btnPayBalanceRect = new Rectangle(xOffset, buttonY, buttonWidth + 20, 30);
                Rectangle btnHistoryRect = new Rectangle(xOffset + buttonWidth + 25, buttonY, buttonWidth, 30);
                if (btnPayBalanceRect.Contains(clickPoint))
                {
                    decimal balance = amount - paid;
                    PayBalance(fineId, balance, amount, paid);
                }
                else if (btnHistoryRect.Contains(clickPoint) && memberId > 0)
                {
                    ShowPaymentHistory(memberId);
                }
            }
            else if (memberId > 0)
            {
                Rectangle btnHistoryRect = new Rectangle(xOffset, buttonY, buttonWidth, 30);
                if (btnHistoryRect.Contains(clickPoint))
                {
                    ShowPaymentHistory(memberId);
                }
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while processing your request: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"CellContentClick error: {ex}");
            }
        }
        private void LoadMetrics()
        {
            try
            {
                var dbContext = ServiceFactory.GetDbContext();
                string queryPending = @"SELECT COALESCE(SUM(Amount - Paid), 0) FROM Fines
                                      WHERE Status = 'Pending' OR Status = 'Partial'";
                var pendingResult = dbContext.ExecuteQuery(queryPending);
                if (pendingResult.Rows.Count > 0)
                {
                    decimal pending = Convert.ToDecimal(pendingResult.Rows[0][0]);
                    lblMetricPendingValue.Text = $"${pending:F2}";
                }
                string queryCollected = @"SELECT COALESCE(SUM(Paid), 0) FROM Fines
                                         WHERE Status = 'Paid' OR Status = 'Partial'";
                var collectedResult = dbContext.ExecuteQuery(queryCollected);
                if (collectedResult.Rows.Count > 0)
                {
                    decimal collected = Convert.ToDecimal(collectedResult.Rows[0][0]);
                    lblMetricCollectedValue.Text = $"${collected:F2}";
                }
                string queryWaived = @"SELECT COALESCE(SUM(Amount), 0) FROM Fines
                                      WHERE Status = 'Waived'";
                var waivedResult = dbContext.ExecuteQuery(queryWaived);
                if (waivedResult.Rows.Count > 0)
                {
                    decimal waived = Convert.ToDecimal(waivedResult.Rows[0][0]);
                    lblMetricWaivedValue.Text = $"${waived:F2}";
                }
                string queryTotal = "SELECT COUNT(*) FROM Fines";
                var totalResult = dbContext.ExecuteQuery(queryTotal);
                if (totalResult.Rows.Count > 0)
                {
                    int total = Convert.ToInt32(totalResult.Rows[0][0]);
                    lblMetricTotalFinesValue.Text = total.ToString();
                }
                var overdueTransactions = _finesService.GetOverdueTransactions().ToList();
                int overdueCount = overdueTransactions.Count;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading metrics: {ex.Message}");
            }
        }
        private void LoadFines()
        {
            try
            {
                allFinesData = GetFinesData();
                if (!allFinesData.Columns.Contains("Member"))
                {
                    allFinesData.Columns.Add("Member", typeof(string));
                }
                if (!allFinesData.Columns.Contains("BookItem"))
                {
                    allFinesData.Columns.Add("BookItem", typeof(string));
                }
                if (!allFinesData.Columns.Contains("Type"))
                {
                    allFinesData.Columns.Add("Type", typeof(string));
                }
                if (!allFinesData.Columns.Contains("Status"))
                {
                    allFinesData.Columns.Add("Status", typeof(string));
                }
                if (allFinesData.Columns.Contains("Amount") && allFinesData.Columns["Amount"].DataType != typeof(string))
                {
                    DataColumn amountCol = allFinesData.Columns["Amount"];
                    string amountName = amountCol.ColumnName;
                    int amountIndex = amountCol.Ordinal;
                    allFinesData.Columns.Remove(amountCol);
                    DataColumn newAmountCol = new DataColumn(amountName, typeof(string));
                    allFinesData.Columns.Add(newAmountCol);
                    if (amountIndex < allFinesData.Columns.Count - 1)
                    {
                        newAmountCol.SetOrdinal(amountIndex);
                    }
                }
                if (allFinesData.Columns.Contains("Paid") && allFinesData.Columns["Paid"].DataType != typeof(string))
                {
                    DataColumn paidCol = allFinesData.Columns["Paid"];
                    string paidName = paidCol.ColumnName;
                    int paidIndex = paidCol.Ordinal;
                    allFinesData.Columns.Remove(paidCol);
                    DataColumn newPaidCol = new DataColumn(paidName, typeof(string));
                    allFinesData.Columns.Add(newPaidCol);
                    if (paidIndex < allFinesData.Columns.Count - 1)
                    {
                        newPaidCol.SetOrdinal(paidIndex);
                    }
                }
                foreach (DataRow row in allFinesData.Rows)
                {
                    string firstName = row["FirstName"] != DBNull.Value ? row["FirstName"].ToString() : "";
                    string lastName = row["LastName"] != DBNull.Value ? row["LastName"].ToString() : "";
                    int memberId = Convert.ToInt32(row["MemberID"]);
                    row["Member"] = Project5LMS.Helpers.IDFormatter.FormatMemberDisplay(firstName, lastName, memberId);
                    if (row["BookID"] != DBNull.Value && Convert.ToInt32(row["BookID"]) > 0)
                    {
                        string bookTitle = "";
                        if (row.Table.Columns.Contains("Title") && row["Title"] != DBNull.Value)
                        {
                            bookTitle = row["Title"].ToString();
                        }
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
                        else if (row.Table.Columns.Contains("BookID") && row["BookID"] != DBNull.Value)
                        {
                            barcode = $"BOOK-{bookId}";
                        }
                        string accessionNo = !string.IsNullOrEmpty(barcode) ? barcode : $"ACC-{bookId.ToString().PadLeft(4, '0')}";
                        string bookItem = !string.IsNullOrEmpty(bookTitle)
                            ? $"{bookTitle} ({accessionNo})"
                            : $"Book ID: {bookId} ({accessionNo})";
                        row["BookItem"] = bookItem;
                    }
                    else
                    {
                        string description = row["Description"] != DBNull.Value ? row["Description"].ToString() : "";
                        row["BookItem"] = !string.IsNullOrEmpty(description) ? description : "N/A";
                    }
                    string fineType = row["FineType"] != DBNull.Value ? row["FineType"].ToString() : "Overdue";
                    row["Type"] = fineType;
                    decimal amount = 0;
                    decimal paid = 0;
                    if (row.Table.Columns.Contains("Amount") && row["Amount"] != DBNull.Value)
                    {
                        if (row["Amount"] is decimal)
                        {
                            amount = (decimal)row["Amount"];
                        }
                        else if (decimal.TryParse(row["Amount"].ToString().Replace("$", ""), out decimal parsedAmount))
                        {
                            amount = parsedAmount;
                        }
                    }
                    if (row.Table.Columns.Contains("Paid") && row["Paid"] != DBNull.Value)
                    {
                        if (row["Paid"] is decimal)
                        {
                            paid = (decimal)row["Paid"];
                        }
                        else if (decimal.TryParse(row["Paid"].ToString().Replace("$", ""), out decimal parsedPaid))
                        {
                            paid = parsedPaid;
                        }
                    }
                    if (allFinesData.Columns["Amount"].DataType == typeof(string))
                    {
                        row["Amount"] = $"${amount:F2}";
                    }
                    if (allFinesData.Columns["Paid"].DataType == typeof(string))
                    {
                        row["Paid"] = $"${paid:F2}";
                    }
                    string currentStatus = row["Status"] != DBNull.Value ? row["Status"].ToString() : "Pending";
                    if (currentStatus == "Waived")
                    {
                        row["Status"] = "Waived";
                    }
                    else if (paid >= amount)
                    {
                        row["Status"] = "Paid";
                    }
                    else if (paid > 0)
                    {
                        row["Status"] = "Partial";
                    }
                    else
                    {
                        row["Status"] = "Pending";
                    }
                }
                DataView dv = allFinesData.DefaultView;
                string rowFilter = "";
                if (currentStatusFilter != "All Status")
                {
                    rowFilter = $"Status = '{currentStatusFilter}'";
                }
                if (currentTypeFilter != "All Types")
                {
                    if (!string.IsNullOrEmpty(rowFilter))
                        rowFilter += " AND ";
                    rowFilter += $"Type = '{currentTypeFilter}'";
                }
                dv.RowFilter = rowFilter;
                dataGridViewFines.DataSource = dv;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading fines: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
                string errorMessage = $"Error loading fines: {ex.Message}";
                if (ex.InnerException != null)
                {
                    errorMessage += $"\n\nInner Exception: {ex.InnerException.Message}";
                }
                MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private DataTable GetFinesData()
        {
            var dbContext = ServiceFactory.GetDbContext();
            using (var conn = dbContext.GetConnection())
            {
                conn.Open();
                bool hasFineType = DatabaseSchemaHelper.CheckColumnExists(conn, "Fines", "FineType");
                bool hasDaysOverdue = DatabaseSchemaHelper.CheckColumnExists(conn, "Fines", "DaysOverdue");
                bool hasDescription = DatabaseSchemaHelper.CheckColumnExists(conn, "Fines", "Description");
                bool hasTransactionID = DatabaseSchemaHelper.CheckColumnExists(conn, "Fines", "TransactionID");
                bool hasBookID = DatabaseSchemaHelper.CheckColumnExists(conn, "Fines", "BookID");
                bool hasBarcode = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "Barcode");
                bool hasAccessionNo = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "AccessionNo");
                bool hasTitle = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "Title");
                string bookIDSelect = hasBookID ? "f.BookID," : "NULL as BookID,";
                bool canJoinBooks = hasBookID || hasTransactionID;
                string bookJoin = hasBookID
                    ? "LEFT JOIN Books b ON f.BookID = b.BookID"
                    : (hasTransactionID
                        ? "LEFT JOIN Transactions t ON f.TransactionID = t.TransactionID LEFT JOIN Books b ON t.BookID = b.BookID"
                        : "");
                string bookIdentifier = canJoinBooks 
                    ? (hasBarcode ? "b.Barcode" : (hasAccessionNo ? "b.AccessionNo" : "CAST(b.BookID AS CHAR)"))
                    : "'N/A'";
                string bookIdentifierAlias = canJoinBooks
                    ? (hasBarcode ? "Barcode" : (hasAccessionNo ? "AccessionNo" : "BookID"))
                    : "BookID";
                string bookTitleSelect = canJoinBooks && hasTitle ? "b.Title" : "'N/A' as Title";
                string bookIdSelect = canJoinBooks ? $"{bookIdentifier} as {bookIdentifierAlias}" : "'N/A' as BookID";
                string query;
                if (hasFineType && hasDaysOverdue && hasDescription && hasTransactionID)
                {
                    query = $@"SELECT
                                f.FineID,
                                f.MemberID,
                                {bookIDSelect}
                                f.TransactionID,
                                f.FineType,
                                f.Amount,
                                f.Paid,
                                f.Status,
                                f.DaysOverdue,
                                f.Description,
                                m.FirstName,
                                m.LastName,
                                {bookTitleSelect},
                                {bookIdSelect}
                             FROM Fines f
                             INNER JOIN Members m ON f.MemberID = m.MemberID
                             {bookJoin}
                             ORDER BY f.FineID DESC";
                }
                else
                {
                    query = $@"SELECT
                                f.FineID,
                                f.MemberID,
                                {bookIDSelect}
                                NULL as TransactionID,
                                COALESCE(f.FineType, 'Overdue') as FineType,
                                f.Amount,
                                f.Paid,
                                f.Status,
                                0 as DaysOverdue,
                                NULL as Description,
                                m.FirstName,
                                m.LastName,
                                {bookTitleSelect},
                                {bookIdSelect}
                             FROM Fines f
                             INNER JOIN Members m ON f.MemberID = m.MemberID
                             {bookJoin}
                             ORDER BY f.FineID DESC";
                }
                return dbContext.ExecuteQuery(query);
            }
        }
        private void btnFilterStatus_Click(object sender, EventArgs e)
        {
            ContextMenuStrip filterMenu = new ContextMenuStrip();
            filterMenu.Items.Add("All Status", null, (s, args) => { currentStatusFilter = "All Status"; btnFilterStatus.Text = "🔍 All Status"; LoadFines(); });
            filterMenu.Items.Add("Pending", null, (s, args) => { currentStatusFilter = "Pending"; btnFilterStatus.Text = "⏳ Pending"; LoadFines(); });
            filterMenu.Items.Add("Partial", null, (s, args) => { currentStatusFilter = "Partial"; btnFilterStatus.Text = "💰 Partial"; LoadFines(); });
            filterMenu.Items.Add("Paid", null, (s, args) => { currentStatusFilter = "Paid"; btnFilterStatus.Text = "✅ Paid"; LoadFines(); });
            filterMenu.Items.Add("Waived", null, (s, args) => { currentStatusFilter = "Waived"; btnFilterStatus.Text = "🔓 Waived"; LoadFines(); });
            filterMenu.Show(btnFilterStatus, new Point(0, btnFilterStatus.Height));
        }
        private void btnFilterType_Click(object sender, EventArgs e)
        {
            ContextMenuStrip filterMenu = new ContextMenuStrip();
            filterMenu.Items.Add("All Types", null, (s, args) => { currentTypeFilter = "All Types"; btnFilterType.Text = "🔍 All Types"; LoadFines(); });
            filterMenu.Items.Add("Overdue", null, (s, args) => { currentTypeFilter = "Overdue"; btnFilterType.Text = "⚠️ Overdue"; LoadFines(); });
            filterMenu.Items.Add("Lost Book", null, (s, args) => { currentTypeFilter = "Lost Book"; btnFilterType.Text = "📕 Lost Book"; LoadFines(); });
            filterMenu.Items.Add("Damaged Book", null, (s, args) => { currentTypeFilter = "Damaged Book"; btnFilterType.Text = "🔧 Damaged Book"; LoadFines(); });
            filterMenu.Items.Add("Lost Card", null, (s, args) => { currentTypeFilter = "Lost Card"; btnFilterType.Text = "🪪 Lost Card"; LoadFines(); });
            filterMenu.Show(btnFilterType, new Point(0, btnFilterType.Height));
        }
        private void CollectFine(int fineId, decimal amount)
        {
            try
            {
                var dbContext = ServiceFactory.GetDbContext();
                int transactionId = 0;
                int memberId = 0;
                using (var conn = dbContext.GetConnection())
                {
                    conn.Open();
                    string getFineQuery = "SELECT TransactionID, MemberID FROM Fines WHERE FineID = @FineID";
                    using (var cmd = new MySqlCommand(getFineQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@FineID", fineId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                transactionId = reader["TransactionID"] != DBNull.Value ? Convert.ToInt32(reader["TransactionID"]) : 0;
                                memberId = Convert.ToInt32(reader["MemberID"]);
                            }
                        }
                    }
                }
                var payment = new Project5LMS.Models.FinePayment
                {
                    TransactionID = transactionId > 0 ? transactionId : 0,
                    MemberID = memberId,
                    AmountPaid = amount,
                    PaymentMode = "Cash",
                    ProcessedBy = Project5LMS.Helpers.CurrentUser.FullName ?? "Admin"
                };
                bool success = _paymentService.ProcessPayment(payment);
                if (success)
                {
                    using (var conn = dbContext.GetConnection())
                    {
                        conn.Open();
                        bool hasPaidDate = DatabaseSchemaHelper.CheckColumnExists(conn, "Fines", "PaidDate");
                        string updateQuery = hasPaidDate
                            ? "UPDATE Fines SET Paid = Amount, Status = 'Paid', PaidDate = @PaidDate WHERE FineID = @FineID"
                            : "UPDATE Fines SET Paid = Amount, Status = 'Paid' WHERE FineID = @FineID";
                        using (var cmd = new MySqlCommand(updateQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@FineID", fineId);
                            if (hasPaidDate)
                                cmd.Parameters.AddWithValue("@PaidDate", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show($"Fine of ${amount:F2} collected successfully.\nReceipt: {payment.ReceiptNumber}",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadMetrics();
                    LoadFines();
                }
                else
                {
                    MessageBox.Show("Failed to process payment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error collecting fine: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void PayBalance(int fineId, decimal balance, decimal amount, decimal paid)
        {
            try
            {
                var dbContext = ServiceFactory.GetDbContext();
                int transactionId = 0;
                int memberId = 0;
                using (var conn = dbContext.GetConnection())
                {
                    conn.Open();
                    string getFineQuery = "SELECT TransactionID, MemberID FROM Fines WHERE FineID = @FineID";
                    using (var cmd = new MySqlCommand(getFineQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@FineID", fineId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                transactionId = reader["TransactionID"] != DBNull.Value ? Convert.ToInt32(reader["TransactionID"]) : 0;
                                memberId = Convert.ToInt32(reader["MemberID"]);
                            }
                        }
                    }
                }
                var payment = new Project5LMS.Models.FinePayment
                {
                    TransactionID = transactionId > 0 ? transactionId : 0,
                    MemberID = memberId,
                    AmountPaid = balance,
                    PaymentMode = "Cash",
                    ProcessedBy = Project5LMS.Helpers.CurrentUser.FullName ?? "Admin"
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
                    decimal newPaid = paid + balance;
                    using (var conn = dbContext.GetConnection())
                    {
                        conn.Open();
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
                    MessageBox.Show($"Balance of ${balance:F2} paid successfully.\nReceipt: {payment.ReceiptNumber}",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadMetrics();
                    LoadFines();
                }
                else
                {
                    MessageBox.Show("Failed to process payment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error paying balance: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void WaiveFine(int fineId)
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
                var dbContext = ServiceFactory.GetDbContext();
                int transactionId = 0;
                int memberId = 0;
                decimal originalAmount = 0;
                using (var conn = dbContext.GetConnection())
                {
                    conn.Open();
                    string getFineQuery = "SELECT TransactionID, MemberID, Amount FROM Fines WHERE FineID = @FineID";
                    using (var cmd = new MySqlCommand(getFineQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@FineID", fineId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                transactionId = reader["TransactionID"] != DBNull.Value ? Convert.ToInt32(reader["TransactionID"]) : 0;
                                memberId = Convert.ToInt32(reader["MemberID"]);
                                originalAmount = Convert.ToDecimal(reader["Amount"]);
                            }
                        }
                    }
                }
                var adjustment = new Project5LMS.Models.FineAdjustment
                {
                    TransactionID = transactionId > 0 ? transactionId : 0,
                    MemberID = memberId,
                    OriginalAmount = originalAmount,
                    AdjustedAmount = 0,
                    AdjustmentAmount = originalAmount,
                    AdjustmentType = "Waiver",
                    Reason = reason,
                    AdjustedBy = Project5LMS.Helpers.CurrentUser.FullName ?? "Admin"
                };
                bool success = false;
                using (var statusForm = new TransactionStatusForm("Fine Waiver"))
                {
                    statusForm.Show();
                    Application.DoEvents();
                    try
                    {
                        statusForm.UpdateStatus("Processing fine waiver...");
                        success = _paymentService.WaiveFine(adjustment);
                        if (success)
                        {
                            statusForm.UpdateStatus("Fine waived successfully!");
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
                    using (var conn = dbContext.GetConnection())
                    {
                        conn.Open();
                        bool hasWaivedDate = DatabaseSchemaHelper.CheckColumnExists(conn, "Fines", "WaivedDate");
                        string updateQuery = hasWaivedDate
                            ? "UPDATE Fines SET Status = 'Waived', WaivedDate = @WaivedDate WHERE FineID = @FineID"
                            : "UPDATE Fines SET Status = 'Waived' WHERE FineID = @FineID";
                        using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@FineID", fineId);
                            if (hasWaivedDate)
                                cmd.Parameters.AddWithValue("@WaivedDate", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Fine waived successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadMetrics();
                    LoadFines();
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
        private void lblMetricPendingTitle_Click(object sender, EventArgs e)
        {
        }
        private void lblMetricCollectedTitle_Click(object sender, EventArgs e)
        {
        }
        private void lblMetricWaivedValue_Click(object sender, EventArgs e)
        {
        }
        private void ShowPaymentHistory(int memberId)
        {
            try
            {
                using (var historyForm = new PaymentHistoryForm(memberId))
                {
                    historyForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading payment history: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lblMetricWaivedTitle_Click(object sender, EventArgs e)
        {
        }
        private void lblMetricTotalFinesTitle_Click(object sender, EventArgs e)
        {
        }
        private void lblMetricPendingValue_Click(object sender, EventArgs e)
        {
        }
        private void dataGridViewFines_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}