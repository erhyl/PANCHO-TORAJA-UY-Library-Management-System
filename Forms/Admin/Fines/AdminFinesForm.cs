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
using Project5LMS.Models;
namespace Project5LMS.Forms.Admin.Fines
{
    public partial class AdminFinesForm : Form
    {
        private DataTable allFinesData;
        private string currentStatusFilter = "All Status";
        private string currentTypeFilter = "All Types";
        private string currentSearchText = "";
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
                this.WindowState = FormWindowState.Maximized;
                EnsureFinesTableExists();
                SetupDataGridView();
                
                LoadMetrics();
                LoadFines();
                
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError($"Error loading fines form: {ex.Message}", "Error", ex);
            }
        }
        private void EnsureFinesTableExists()
        {
            try
            {
                var dbContext = ServiceFactory.GetDbContext();
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
                
                bool wasCreated = TableCreationHelper.EnsureTableExists(dbContext, "Fines", createTableQuery, conn =>
                {
                    // Add columns if table already existed
                    DatabaseSchemaHelper.AddColumnIfNotExists(conn, "Fines", "FineType", "VARCHAR(50) DEFAULT 'Overdue'");
                    DatabaseSchemaHelper.AddColumnIfNotExists(conn, "Fines", "DaysOverdue", "INT DEFAULT 0");
                    DatabaseSchemaHelper.AddColumnIfNotExists(conn, "Fines", "CreatedDate", "DATETIME DEFAULT CURRENT_TIMESTAMP");
                    DatabaseSchemaHelper.AddColumnIfNotExists(conn, "Fines", "PaidDate", "DATETIME NULL");
                    DatabaseSchemaHelper.AddColumnIfNotExists(conn, "Fines", "WaivedDate", "DATETIME NULL");
                    DatabaseSchemaHelper.AddColumnIfNotExists(conn, "Fines", "Description", "VARCHAR(255) NULL");
                    DatabaseSchemaHelper.AddColumnIfNotExists(conn, "Fines", "TransactionID", "INT NULL");
                });
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
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            };
            dataGridViewFines.Columns.Add(colFineID);
            DataGridViewTextBoxColumn colMember = new DataGridViewTextBoxColumn
            {
                Name = "Member",
                HeaderText = "MEMBER",
                DataPropertyName = "Member",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            };
            dataGridViewFines.Columns.Add(colMember);
            DataGridViewTextBoxColumn colBook = new DataGridViewTextBoxColumn
            {
                Name = "BookItem",
                HeaderText = "BOOK/ITEM",
                DataPropertyName = "BookItem",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            };
            dataGridViewFines.Columns.Add(colBook);
            DataGridViewColumn colType = new DataGridViewTextBoxColumn
            {
                Name = "Type",
                HeaderText = "TYPE",
                DataPropertyName = "Type",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            };
            dataGridViewFines.Columns.Add(colType);
            DataGridViewTextBoxColumn colDaysOverdue = new DataGridViewTextBoxColumn
            {
                Name = "DaysOverdue",
                HeaderText = "DAYS OVERDUE",
                DataPropertyName = "DaysOverdue",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            };
            dataGridViewFines.Columns.Add(colDaysOverdue);
            DataGridViewTextBoxColumn colAmount = new DataGridViewTextBoxColumn
            {
                Name = "Amount",
                HeaderText = "AMOUNT",
                DataPropertyName = "Amount",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            };
            dataGridViewFines.Columns.Add(colAmount);
            DataGridViewTextBoxColumn colPaid = new DataGridViewTextBoxColumn
            {
                Name = "Paid",
                HeaderText = "PAID",
                DataPropertyName = "Paid",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            };
            dataGridViewFines.Columns.Add(colPaid);
            DataGridViewColumn colStatus = new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "STATUS",
                DataPropertyName = "Status",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            };
            dataGridViewFines.Columns.Add(colStatus);
            DataGridViewColumn colActions = new DataGridViewTextBoxColumn
            {
                Name = "Actions",
                HeaderText = "ACTIONS",
                DataPropertyName = "Actions",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
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
            dataGridViewFines.CellClick += DataGridViewFines_CellClick;
            dataGridViewFines.DataError += DataGridViewFines_DataError;
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
                        e.Value = IDFormatter.FormatCurrency(decimalValue);
                        e.FormattingApplied = true;
                    }
                    else if (originalValue is double doubleValue)
                    {
                        e.Value = IDFormatter.FormatCurrency((decimal)doubleValue);
                        e.FormattingApplied = true;
                    }
                    else if (decimal.TryParse(originalValue.ToString(), out decimal parsedValue))
                    {
                        e.Value = IDFormatter.FormatCurrency(parsedValue);
                        e.FormattingApplied = true;
                    }
                }
                else if (columnName == "DaysOverdue")
                {
                    // Format DaysOverdue as string for display
                    if (e.Value == null || e.Value == DBNull.Value)
                    {
                        e.Value = "0";
                        e.FormattingApplied = true;
                    }
                    else
                    {
                        try
                        {
                            int daysOverdue = 0;
                            if (e.Value is int intValue)
                            {
                                daysOverdue = intValue;
                            }
                            else if (e.Value is long longValue)
                            {
                                daysOverdue = (int)longValue;
                            }
                            else if (e.Value is decimal decimalValue)
                            {
                                daysOverdue = (int)decimalValue;
                            }
                            else if (e.Value is double doubleValue)
                            {
                                daysOverdue = (int)doubleValue;
                            }
                            else if (int.TryParse(e.Value.ToString(), out int parsedValue))
                            {
                                daysOverdue = parsedValue;
                            }
                            e.Value = daysOverdue.ToString();
                            e.FormattingApplied = true;
                        }
                        catch
                        {
                            e.Value = "0";
                            e.FormattingApplied = true;
                        }
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
                    amount = IDFormatter.ParseCurrency(row.Cells["Amount"].Value.ToString());
                }
                if (row.Cells["Paid"]?.Value != null)
                {
                    paid = IDFormatter.ParseCurrency(row.Cells["Paid"].Value.ToString());
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
                amount = IDFormatter.ParseCurrency(row.Cells["Amount"].Value.ToString());
            }
            if (row.Cells["Paid"]?.Value != null)
            {
                paid = IDFormatter.ParseCurrency(row.Cells["Paid"].Value.ToString());
            }
            Point clickPoint = dataGridViewFines.PointToClient(Cursor.Position);
            Rectangle cellRect = dataGridViewFines.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            
            // Calculate relative position within the cell
            int relativeX = clickPoint.X - cellRect.X;
            int relativeY = clickPoint.Y - cellRect.Y;
            
            int buttonY = (cellRect.Height - 30) / 2;
            int buttonWidth = 80;
            int spacing = 5;
            int xOffset = 5;
            
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
            
            if (fineId == 0)
            {
                System.Diagnostics.Debug.WriteLine("Could not determine FineID from row");
                return;
            }
            
            if (status.ToLower() == "pending")
            {
                Rectangle btnCollectRect = new Rectangle(xOffset, buttonY, buttonWidth, 30);
                Rectangle btnWaiveRect = new Rectangle(xOffset + buttonWidth + spacing, buttonY, buttonWidth, 30);
                Rectangle btnHistoryRect = new Rectangle(xOffset + (buttonWidth + spacing) * 2, buttonY, buttonWidth, 30);
                
                if (btnCollectRect.Contains(relativeX, relativeY))
                {
                    CollectFine(fineId, amount);
                }
                else if (btnWaiveRect.Contains(relativeX, relativeY))
                {
                    WaiveFine(fineId);
                }
                else if (btnHistoryRect.Contains(relativeX, relativeY) && memberId > 0)
                {
                    ShowFineDetails(fineId, memberId);
                }
            }
            else if (status.ToLower() == "partial")
            {
                Rectangle btnPayBalanceRect = new Rectangle(xOffset, buttonY, buttonWidth + 20, 30);
                Rectangle btnHistoryRect = new Rectangle(xOffset + buttonWidth + 25, buttonY, buttonWidth, 30);
                
                if (btnPayBalanceRect.Contains(relativeX, relativeY))
                {
                    decimal balance = amount - paid;
                    PayBalance(fineId, balance, amount, paid);
                }
                else if (btnHistoryRect.Contains(relativeX, relativeY) && memberId > 0)
                {
                    ShowFineDetails(fineId, memberId);
                }
            }
            else
            {
                // For Paid, Waived, or other statuses - show fine details
                Rectangle btnHistoryRect = new Rectangle(xOffset, buttonY, buttonWidth, 30);
                if (btnHistoryRect.Contains(relativeX, relativeY))
                {
                    ShowFineDetails(fineId, memberId);
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
                
                // Load metrics using simplified queries
                LoadMetricValue(dbContext, @"SELECT COALESCE(SUM(Amount - Paid), 0) FROM Fines WHERE Status = 'Pending' OR Status = 'Partial'", 
                    lblMetricPendingValue, true);
                LoadMetricValue(dbContext, @"SELECT COALESCE(SUM(Paid), 0) FROM Fines WHERE Status = 'Paid' OR Status = 'Partial'", 
                    lblMetricCollectedValue, true);
                LoadMetricValue(dbContext, @"SELECT COALESCE(SUM(Amount), 0) FROM Fines WHERE Status = 'Waived'", 
                    lblMetricWaivedValue, true);
                LoadMetricValue(dbContext, "SELECT COUNT(*) FROM Fines", 
                    lblMetricTotalFinesValue, false);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading metrics: {ex.Message}");
            }
        }
        
        private void LoadMetricValue(Data.DatabaseContext dbContext, string query, Label label, bool formatAsCurrency)
        {
            try
            {
                var result = dbContext.ExecuteQuery(query);
                if (result.Rows.Count > 0 && result.Rows[0][0] != DBNull.Value)
                {
                    if (formatAsCurrency)
                    {
                        decimal value = Convert.ToDecimal(result.Rows[0][0]);
                        label.Text = IDFormatter.FormatCurrency(value);
                    }
                    else
                    {
                        label.Text = result.Rows[0][0].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading metric: {ex.Message}");
            }
        }
        private void LoadFines()
        {
            try
            {
                // Automatically create fine records for overdue transactions
                CreateOverdueFines();
                
                allFinesData = GetFinesData();
                
                System.Diagnostics.Debug.WriteLine($"GetFinesData returned {allFinesData?.Rows.Count ?? 0} rows");
                if (allFinesData != null && allFinesData.Rows.Count > 0)
                {
                    System.Diagnostics.Debug.WriteLine($"Column names: {string.Join(", ", allFinesData.Columns.Cast<DataColumn>().Select(c => c.ColumnName))}");
                    if (allFinesData.Columns.Contains("Amount") && allFinesData.Columns.Contains("Paid"))
                    {
                        DataRow firstRow = allFinesData.Rows[0];
                        System.Diagnostics.Debug.WriteLine($"First row - Amount: {firstRow["Amount"]} (type: {firstRow["Amount"]?.GetType()}), Paid: {firstRow["Paid"]} (type: {firstRow["Paid"]?.GetType()}), Status: {firstRow["Status"]}");
                    }
                }
                
                // Ensure DaysOverdue column exists
                if (!allFinesData.Columns.Contains("DaysOverdue"))
                {
                    allFinesData.Columns.Add("DaysOverdue", typeof(string));
                }
                // Convert DaysOverdue column to string type for display (similar to Amount/Paid)
                if (allFinesData.Columns.Contains("DaysOverdue") && allFinesData.Columns["DaysOverdue"].DataType != typeof(string))
                {
                    DataColumn daysOverdueCol = allFinesData.Columns["DaysOverdue"];
                    string daysOverdueName = daysOverdueCol.ColumnName;
                    int daysOverdueIndex = daysOverdueCol.Ordinal;
                    // Store original values before removing column
                    var originalValues = new List<string>();
                    foreach (DataRow row in allFinesData.Rows)
                    {
                        object originalValue = row["DaysOverdue"];
                        if (originalValue != null && originalValue != DBNull.Value)
                        {
                            int daysValue = 0;
                            if (originalValue is int intVal)
                            {
                                daysValue = intVal;
                            }
                            else if (originalValue is long longVal)
                            {
                                daysValue = (int)longVal;
                            }
                            else if (originalValue is decimal decVal)
                            {
                                daysValue = (int)decVal;
                            }
                            else if (int.TryParse(originalValue.ToString(), out int parsedDays))
                            {
                                daysValue = parsedDays;
                            }
                            originalValues.Add(daysValue.ToString());
                        }
                        else
                        {
                            originalValues.Add("0");
                        }
                    }
                    allFinesData.Columns.Remove(daysOverdueCol);
                    DataColumn newDaysOverdueCol = new DataColumn(daysOverdueName, typeof(string));
                    allFinesData.Columns.Add(newDaysOverdueCol);
                    if (daysOverdueIndex < allFinesData.Columns.Count - 1)
                    {
                        newDaysOverdueCol.SetOrdinal(daysOverdueIndex);
                    }
                    // Populate the new string column with converted values
                    for (int i = 0; i < allFinesData.Rows.Count && i < originalValues.Count; i++)
                    {
                        allFinesData.Rows[i]["DaysOverdue"] = originalValues[i];
                    }
                }
                // If DaysOverdue column doesn't exist, add it as string
                else if (!allFinesData.Columns.Contains("DaysOverdue"))
                {
                    allFinesData.Columns.Add("DaysOverdue", typeof(string));
                    // Initialize all rows with "0"
                    foreach (DataRow row in allFinesData.Rows)
                    {
                        row["DaysOverdue"] = "0";
                    }
                }
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
                // Convert FineID to string for search compatibility
                if (allFinesData.Columns.Contains("FineID") && allFinesData.Columns["FineID"].DataType != typeof(string))
                {
                    DataColumn fineIdCol = allFinesData.Columns["FineID"];
                    string fineIdName = fineIdCol.ColumnName;
                    int fineIdIndex = fineIdCol.Ordinal;
                    
                    // Store original values
                    var originalFineIdValues = new List<string>();
                    foreach (DataRow row in allFinesData.Rows)
                    {
                        object originalValue = row["FineID"];
                        string fineIdValue = originalValue != null && originalValue != DBNull.Value 
                            ? originalValue.ToString() 
                            : "0";
                        originalFineIdValues.Add(fineIdValue);
                    }
                    
                    allFinesData.Columns.Remove(fineIdCol);
                    DataColumn newFineIdCol = new DataColumn(fineIdName, typeof(string));
                    allFinesData.Columns.Add(newFineIdCol);
                    if (fineIdIndex < allFinesData.Columns.Count - 1)
                    {
                        newFineIdCol.SetOrdinal(fineIdIndex);
                    }
                    
                    // Populate with string values
                    for (int i = 0; i < allFinesData.Rows.Count && i < originalFineIdValues.Count; i++)
                    {
                        allFinesData.Rows[i]["FineID"] = originalFineIdValues[i];
                    }
                }
                // Convert Amount column to string while preserving values
                if (allFinesData.Columns.Contains("Amount") && allFinesData.Columns["Amount"].DataType != typeof(string))
                {
                    DataColumn amountCol = allFinesData.Columns["Amount"];
                    string amountName = amountCol.ColumnName;
                    int amountIndex = amountCol.Ordinal;
                    
                    // Store original values before removing column
                    var originalAmountValues = new List<decimal>();
                    foreach (DataRow row in allFinesData.Rows)
                    {
                        object originalValue = row["Amount"];
                        decimal amountValue = 0;
                        if (originalValue != null && originalValue != DBNull.Value)
                        {
                            try
                            {
                                if (originalValue is decimal decVal)
                                {
                                    amountValue = decVal;
                                }
                                else if (originalValue is double doubleVal)
                                {
                                    amountValue = (decimal)doubleVal;
                                }
                                else if (originalValue is int intVal)
                                {
                                    amountValue = (decimal)intVal;
                                }
                                else if (originalValue is long longVal)
                                {
                                    amountValue = (decimal)longVal;
                                }
                                else if (decimal.TryParse(originalValue.ToString(), out decimal parsedAmount))
                                {
                                    amountValue = parsedAmount;
                                }
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($"Error parsing Amount value '{originalValue}': {ex.Message}");
                                amountValue = 0;
                            }
                        }
                        originalAmountValues.Add(amountValue);
                    }
                    
                    allFinesData.Columns.Remove(amountCol);
                    DataColumn newAmountCol = new DataColumn(amountName, typeof(string));
                    allFinesData.Columns.Add(newAmountCol);
                    if (amountIndex < allFinesData.Columns.Count - 1)
                    {
                        newAmountCol.SetOrdinal(amountIndex);
                    }
                    
                    // Populate the new string column with formatted currency values
                    for (int i = 0; i < allFinesData.Rows.Count && i < originalAmountValues.Count; i++)
                    {
                        allFinesData.Rows[i]["Amount"] = IDFormatter.FormatCurrency(originalAmountValues[i]);
                    }
                }
                
                // Convert Paid column to string while preserving values
                if (allFinesData.Columns.Contains("Paid") && allFinesData.Columns["Paid"].DataType != typeof(string))
                {
                    DataColumn paidCol = allFinesData.Columns["Paid"];
                    string paidName = paidCol.ColumnName;
                    int paidIndex = paidCol.Ordinal;
                    
                    // Store original values before removing column
                    var originalPaidValues = new List<decimal>();
                    foreach (DataRow row in allFinesData.Rows)
                    {
                        object originalValue = row["Paid"];
                        decimal paidValue = 0;
                        if (originalValue != null && originalValue != DBNull.Value)
                        {
                            try
                            {
                                if (originalValue is decimal decVal)
                                {
                                    paidValue = decVal;
                                }
                                else if (originalValue is double doubleVal)
                                {
                                    paidValue = (decimal)doubleVal;
                                }
                                else if (originalValue is int intVal)
                                {
                                    paidValue = (decimal)intVal;
                                }
                                else if (originalValue is long longVal)
                                {
                                    paidValue = (decimal)longVal;
                                }
                                else if (decimal.TryParse(originalValue.ToString(), out decimal parsedPaid))
                                {
                                    paidValue = parsedPaid;
                                }
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($"Error parsing Paid value '{originalValue}': {ex.Message}");
                                paidValue = 0;
                            }
                        }
                        originalPaidValues.Add(paidValue);
                    }
                    
                    allFinesData.Columns.Remove(paidCol);
                    DataColumn newPaidCol = new DataColumn(paidName, typeof(string));
                    allFinesData.Columns.Add(newPaidCol);
                    if (paidIndex < allFinesData.Columns.Count - 1)
                    {
                        newPaidCol.SetOrdinal(paidIndex);
                    }
                    
                    // Populate the new string column with formatted currency values
                    for (int i = 0; i < allFinesData.Rows.Count && i < originalPaidValues.Count; i++)
                    {
                        allFinesData.Rows[i]["Paid"] = IDFormatter.FormatCurrency(originalPaidValues[i]);
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
                    // Ensure DaysOverdue is populated and formatted as string
                    if (row.Table.Columns.Contains("DaysOverdue"))
                    {
                        if (row["DaysOverdue"] == DBNull.Value || string.IsNullOrEmpty(row["DaysOverdue"]?.ToString()))
                        {
                            // Calculate days overdue if not set
                            int daysOverdue = 0;
                            if (row.Table.Columns.Contains("TransactionID") && row["TransactionID"] != DBNull.Value)
                            {
                                // Try to get from transaction if available
                                try
                                {
                                    var dbContext = ServiceFactory.GetDbContext();
                                    using (var conn = dbContext.GetConnection())
                                    {
                                        conn.Open();
                                        int transactionId = Convert.ToInt32(row["TransactionID"]);
                                        string dueDateQuery = "SELECT DueDate FROM Transactions WHERE TransactionID = @TransactionID";
                                        using (MySqlCommand cmd = new MySqlCommand(dueDateQuery, conn))
                                        {
                                            cmd.Parameters.AddWithValue("@TransactionID", transactionId);
                                            object dueDateObj = cmd.ExecuteScalar();
                                            if (dueDateObj != null && dueDateObj != DBNull.Value)
                                            {
                                                DateTime dueDate = Convert.ToDateTime(dueDateObj);
                                                daysOverdue = Math.Max(0, (DateTime.Now - dueDate).Days);
                                            }
                                        }
                                    }
                                }
                                catch { }
                            }
                            row["DaysOverdue"] = daysOverdue.ToString();
                        }
                        else if (row.Table.Columns["DaysOverdue"].DataType == typeof(string))
                        {
                            // Already string type, just ensure it's not empty
                            if (string.IsNullOrEmpty(row["DaysOverdue"].ToString()))
                            {
                                row["DaysOverdue"] = "0";
                            }
                        }
                        else
                        {
                            // Convert to string if not already
                            int daysValue = 0;
                            if (int.TryParse(row["DaysOverdue"].ToString(), out int parsedDays))
                            {
                                daysValue = parsedDays;
                            }
                            row["DaysOverdue"] = daysValue.ToString();
                        }
                    }
                    // Read Amount and Paid values - handle both decimal and string types
                    decimal amount = 0;
                    decimal paid = 0;
                    
                    if (row.Table.Columns.Contains("Amount") && row["Amount"] != DBNull.Value && row["Amount"] != null)
                    {
                        object amountValue = row["Amount"];
                        if (amountValue is decimal)
                        {
                            amount = (decimal)amountValue;
                        }
                        else if (amountValue is double)
                        {
                            amount = (decimal)(double)amountValue;
                        }
                        else if (amountValue is int)
                        {
                            amount = (decimal)(int)amountValue;
                        }
                        else
                        {
                            // Try to parse from string (might already be formatted currency)
                            string amountStr = amountValue.ToString();
                            amount = IDFormatter.ParseCurrency(amountStr);
                        }
                    }
                    
                    if (row.Table.Columns.Contains("Paid") && row["Paid"] != DBNull.Value && row["Paid"] != null)
                    {
                        object paidValue = row["Paid"];
                        if (paidValue is decimal)
                        {
                            paid = (decimal)paidValue;
                        }
                        else if (paidValue is double)
                        {
                            paid = (decimal)(double)paidValue;
                        }
                        else if (paidValue is int)
                        {
                            paid = (decimal)(int)paidValue;
                        }
                        else
                        {
                            // Try to parse from string (might already be formatted currency)
                            string paidStr = paidValue.ToString();
                            paid = IDFormatter.ParseCurrency(paidStr);
                        }
                    }
                    
                    // Update formatted string values if columns are string type
                    if (allFinesData.Columns.Contains("Amount") && allFinesData.Columns["Amount"].DataType == typeof(string))
                    {
                        row["Amount"] = IDFormatter.FormatCurrency(amount);
                    }
                    if (allFinesData.Columns.Contains("Paid") && allFinesData.Columns["Paid"].DataType == typeof(string))
                    {
                        row["Paid"] = IDFormatter.FormatCurrency(paid);
                    }
                    
                    // Determine Status based on Amount and Paid values
                    string currentStatus = row["Status"] != DBNull.Value && row["Status"] != null ? row["Status"].ToString() : "Pending";
                    
                    // Only recalculate status if not already Waived
                    if (currentStatus == "Waived")
                    {
                        row["Status"] = "Waived";
                    }
                    else if (amount > 0 && paid >= amount)
                    {
                        row["Status"] = "Paid";
                    }
                    else if (amount > 0 && paid > 0)
                    {
                        row["Status"] = "Partial";
                    }
                    else if (amount > 0)
                    {
                        row["Status"] = "Pending";
                    }
                    else
                    {
                        // If amount is 0, keep original status or set to Pending
                        row["Status"] = currentStatus;
                    }
                }
                DataView dv = allFinesData.DefaultView;
                string rowFilter = "";
                
                // Apply status filter
                if (currentStatusFilter != "All Status")
                {
                    rowFilter = $"Status = '{currentStatusFilter.Replace("'", "''")}'";
                }
                
                // Apply type filter
                if (currentTypeFilter != "All Types")
                {
                    if (!string.IsNullOrEmpty(rowFilter))
                        rowFilter += " AND ";
                    rowFilter += $"Type = '{currentTypeFilter.Replace("'", "''")}'";
                }
                
                // Apply search filter
                if (!string.IsNullOrWhiteSpace(currentSearchText))
                {
                    string searchText = currentSearchText.Trim().Replace("'", "''");
                    if (!string.IsNullOrEmpty(rowFilter))
                        rowFilter += " AND ";
                    
                    // Build search filter - all columns are now strings, so we can use LIKE on all
                    List<string> searchConditions = new List<string>();
                    
                    // Search on all string columns (FineID, Member, BookItem, Type, Status, Amount, Paid, DaysOverdue are all strings now)
                    searchConditions.Add($"FineID LIKE '%{searchText}%'");
                    searchConditions.Add($"Member LIKE '%{searchText}%'");
                    searchConditions.Add($"BookItem LIKE '%{searchText}%'");
                    searchConditions.Add($"Type LIKE '%{searchText}%'");
                    searchConditions.Add($"Status LIKE '%{searchText}%'");
                    searchConditions.Add($"Amount LIKE '%{searchText}%'");
                    searchConditions.Add($"Paid LIKE '%{searchText}%'");
                    searchConditions.Add($"DaysOverdue LIKE '%{searchText}%'");
                    
                    rowFilter += "(" + string.Join(" OR ", searchConditions) + ")";
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
                ErrorHandler.ShowError(errorMessage, "Error");
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
        private void CreateOverdueFines()
        {
            try
            {
                var overdueTransactions = _finesService.GetOverdueTransactions().ToList();
                var dbContext = ServiceFactory.GetDbContext();
                
                using (var conn = dbContext.GetConnection())
                {
                    conn.Open();
                    bool hasFineType = DatabaseSchemaHelper.CheckColumnExists(conn, "Fines", "FineType");
                    bool hasDaysOverdue = DatabaseSchemaHelper.CheckColumnExists(conn, "Fines", "DaysOverdue");
                    bool hasTransactionID = DatabaseSchemaHelper.CheckColumnExists(conn, "Fines", "TransactionID");
                    
                    foreach (var transaction in overdueTransactions)
                    {
                        // Check if fine already exists for this transaction
                        string checkQuery = hasTransactionID
                            ? "SELECT COUNT(*) FROM Fines WHERE TransactionID = @TransactionID AND Status IN ('Pending', 'Partial')"
                            : "SELECT COUNT(*) FROM Fines WHERE MemberID = @MemberID AND BookID = @BookID AND Status IN ('Pending', 'Partial')";
                        
                        bool fineExists = false;
                        using (var checkCmd = new MySqlCommand(checkQuery, conn))
                        {
                            if (hasTransactionID)
                            {
                                checkCmd.Parameters.AddWithValue("@TransactionID", transaction.TransactionID);
                            }
                            else
                            {
                                checkCmd.Parameters.AddWithValue("@MemberID", transaction.MemberID);
                                checkCmd.Parameters.AddWithValue("@BookID", transaction.BookID);
                            }
                            fineExists = Convert.ToInt32(checkCmd.ExecuteScalar()) > 0;
                        }
                        
                        if (!fineExists)
                        {
                            // Calculate fine using member type-based rates
                            var member = _membersService.GetMember(transaction.MemberID);
                            if (member != null)
                            {
                                var privileges = MemberTypePrivileges.GetDefaultPrivileges(member.Type);
                                int daysOverdue = (DateTime.Now - transaction.DueDate).Days;
                                if (daysOverdue == 0 && (DateTime.Now - transaction.DueDate).TotalHours > 0)
                                    daysOverdue = 1;
                                
                                // Formula: Fine = Days Overdue × Daily Rate (up to maximum)
                                decimal calculatedFine = daysOverdue * privileges.FineRatePerDay;
                                decimal fineAmount = Math.Min(calculatedFine, privileges.MaxFineCap);
                                fineAmount = Math.Round(fineAmount, 2, MidpointRounding.AwayFromZero);
                                
                                if (fineAmount > 0)
                                {
                                    // Insert fine record
                                    string insertQuery = hasFineType && hasDaysOverdue && hasTransactionID
                                        ? @"INSERT INTO Fines (MemberID, BookID, TransactionID, FineType, Amount, Paid, Status, DaysOverdue, CreatedDate)
                                           VALUES (@MemberID, @BookID, @TransactionID, 'Overdue', @Amount, 0, 'Pending', @DaysOverdue, @CreatedDate)"
                                        : @"INSERT INTO Fines (MemberID, BookID, Amount, Paid, Status, CreatedDate)
                                           VALUES (@MemberID, @BookID, @Amount, 0, 'Pending', @CreatedDate)";
                                    
                                    using (var insertCmd = new MySqlCommand(insertQuery, conn))
                                    {
                                        insertCmd.Parameters.AddWithValue("@MemberID", transaction.MemberID);
                                        insertCmd.Parameters.AddWithValue("@BookID", transaction.BookID);
                                        insertCmd.Parameters.AddWithValue("@Amount", fineAmount);
                                        if (hasTransactionID)
                                        {
                                            insertCmd.Parameters.AddWithValue("@TransactionID", transaction.TransactionID);
                                        }
                                        if (hasDaysOverdue)
                                        {
                                            insertCmd.Parameters.AddWithValue("@DaysOverdue", daysOverdue);
                                        }
                                        insertCmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                                        insertCmd.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating overdue fines: {ex.Message}");
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
                // Show payment mode selection dialog
                string paymentMode = ShowPaymentModeDialog();
                if (string.IsNullOrEmpty(paymentMode))
                    return; // User cancelled
                
                var payment = new Project5LMS.Models.FinePayment
                {
                    TransactionID = transactionId > 0 ? transactionId : 0,
                    MemberID = memberId,
                    AmountPaid = amount,
                    PaymentMode = paymentMode,
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
                    // Generate and show receipt
                    string receiptMessage = GeneratePaymentReceipt(payment, amount, "Full Payment");
                    MessageBox.Show($"Fine of {IDFormatter.FormatCurrency(amount)} collected successfully.\n\n{receiptMessage}",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadMetrics();
                    LoadFines();
                }
                else
                {
                    ErrorHandler.ShowError("Failed to process payment.", "Error");
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError($"Error collecting fine: {ex.Message}", "Error", ex);
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
                // Show payment mode selection dialog
                string paymentMode = ShowPaymentModeDialog();
                if (string.IsNullOrEmpty(paymentMode))
                    return; // User cancelled
                
                var payment = new Project5LMS.Models.FinePayment
                {
                    TransactionID = transactionId > 0 ? transactionId : 0,
                    MemberID = memberId,
                    AmountPaid = balance,
                    PaymentMode = paymentMode,
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
                            System.Threading.Thread.Sleep(Constants.ThreadSleepShort);
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
                    // Generate and show receipt
                    string receiptMessage = GeneratePaymentReceipt(payment, balance, "Balance Payment");
                    MessageBox.Show($"Balance of {IDFormatter.FormatCurrency(balance)} paid successfully.\n\n{receiptMessage}",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadMetrics();
                    LoadFines();
                }
                else
                {
                    ErrorHandler.ShowError("Failed to process payment.", "Error");
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError($"Error paying balance: {ex.Message}", "Error", ex);
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
                ErrorHandler.ShowValidationError("Please provide a reason for the waiver.");
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
                            System.Threading.Thread.Sleep(Constants.ThreadSleepShort);
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
                    ErrorHandler.ShowError("Failed to waive fine.", "Error");
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError($"Error waiving fine: {ex.Message}", "Error", ex);
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
        
        private void ShowFineDetails(int fineId, int memberId)
        {
            try
            {
                var dbContext = ServiceFactory.GetDbContext();
                using (var conn = dbContext.GetConnection())
                {
                    conn.Open();
                    
                    // Check which columns exist
                    bool hasPhone = DatabaseSchemaHelper.CheckColumnExists(conn, "Members", "Phone");
                    bool hasContact = DatabaseSchemaHelper.CheckColumnExists(conn, "Members", "Contact");
                    bool hasEmail = DatabaseSchemaHelper.CheckColumnExists(conn, "Members", "Email");
                    bool hasBarcode = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "Barcode");
                    bool hasAccessionNo = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "AccessionNo");
                    bool hasTitle = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "Title");
                    
                    // Build query dynamically based on available columns
                    string phoneSelect = hasPhone ? "m.Phone," : (hasContact ? "m.Contact as Phone," : "NULL as Phone,");
                    string emailSelect = hasEmail ? "m.Email," : "NULL as Email,";
                    string bookTitleSelect = hasTitle ? "b.Title as BookTitle," : "NULL as BookTitle,";
                    string barcodeSelect = hasBarcode ? "b.Barcode," : (hasAccessionNo ? "b.AccessionNo as Barcode," : "NULL as Barcode,");
                    string accessionNoSelect = hasAccessionNo ? "b.AccessionNo" : "NULL as AccessionNo"; // No comma on last item
                    
                    // Check if FineAdjustments table exists
                    bool hasFineAdjustments = DatabaseSchemaHelper.CheckTableExists(conn, "FineAdjustments");
                    string waiverReasonSelect = hasFineAdjustments 
                        ? "fa.Reason as WaiverReason" 
                        : "NULL as WaiverReason";
                    string fineAdjustmentsJoin = hasFineAdjustments
                        ? "LEFT JOIN FineAdjustments fa ON (fa.TransactionID = f.TransactionID OR (fa.TransactionID IS NULL AND fa.MemberID = f.MemberID)) AND fa.AdjustmentType = 'Waiver'"
                        : "";
                    
                    string query = $@"SELECT 
                        f.FineID,
                        f.MemberID,
                        f.BookID,
                        f.TransactionID,
                        f.FineType,
                        f.Amount,
                        f.Paid,
                        f.Status,
                        f.DaysOverdue,
                        f.CreatedDate,
                        f.PaidDate,
                        f.WaivedDate,
                        f.Description,
                        m.FirstName,
                        m.LastName,
                        {emailSelect}
                        {phoneSelect}
                        {bookTitleSelect}
                        {barcodeSelect}
                        {accessionNoSelect},
                        {waiverReasonSelect}
                    FROM Fines f
                    INNER JOIN Members m ON f.MemberID = m.MemberID
                    LEFT JOIN Books b ON f.BookID = b.BookID
                    {fineAdjustmentsJoin}
                    WHERE f.FineID = @FineID";
                    
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FineID", fineId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                using (Form detailsForm = new Form())
                                {
                                    detailsForm.Text = "Fine Details";
                                    detailsForm.Size = new Size(600, 600);
                                    detailsForm.StartPosition = FormStartPosition.CenterParent;
                                    detailsForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                                    detailsForm.MaximizeBox = false;
                                    detailsForm.MinimizeBox = false;
                                    detailsForm.AutoScroll = true;
                                    
                                    int yPos = 20;
                                    int labelWidth = 150;
                                    int valueWidth = 400;
                                    
                                    // Fine ID
                                    Label lblFineID = new Label { Text = "Fine ID:", Location = new Point(20, yPos), Width = labelWidth, AutoSize = false };
                                    Label lblFineIDValue = new Label 
                                    { 
                                        Text = $"FINE-{fineId.ToString().PadLeft(3, '0')}", 
                                        Location = new Point(180, yPos), 
                                        Width = valueWidth,
                                        Font = new Font("Segoe UI", 10, FontStyle.Bold)
                                    };
                                    detailsForm.Controls.AddRange(new Control[] { lblFineID, lblFineIDValue });
                                    yPos += 35;
                                    
                                    // Member
                                    string firstName = reader["FirstName"] != DBNull.Value ? reader["FirstName"].ToString() : "";
                                    string lastName = reader["LastName"] != DBNull.Value ? reader["LastName"].ToString() : "";
                                    string memberName = $"{firstName} {lastName}".Trim();
                                    if (string.IsNullOrEmpty(memberName))
                                        memberName = "Unknown Member";
                                    Label lblMember = new Label { Text = "Member:", Location = new Point(20, yPos), Width = labelWidth };
                                    Label lblMemberValue = new Label { Text = $"{memberName} ({IDFormatter.FormatMemberID(memberId)})", Location = new Point(180, yPos), Width = valueWidth };
                                    detailsForm.Controls.AddRange(new Control[] { lblMember, lblMemberValue });
                                    yPos += 35;
                                    
                                    // Book/Item
                                    string bookTitle = reader["BookTitle"] != DBNull.Value ? reader["BookTitle"].ToString() : "N/A";
                                    string barcode = reader["Barcode"] != DBNull.Value ? reader["Barcode"].ToString() : 
                                                   (reader["AccessionNo"] != DBNull.Value ? reader["AccessionNo"].ToString() : "");
                                    Label lblBook = new Label { Text = "Book/Item:", Location = new Point(20, yPos), Width = labelWidth };
                                    Label lblBookValue = new Label { Text = !string.IsNullOrEmpty(barcode) ? $"{bookTitle} ({barcode})" : bookTitle, Location = new Point(180, yPos), Width = valueWidth };
                                    detailsForm.Controls.AddRange(new Control[] { lblBook, lblBookValue });
                                    yPos += 35;
                                    
                                    // Type
                                    string fineType = reader["FineType"] != DBNull.Value ? reader["FineType"].ToString() : "Overdue";
                                    Label lblType = new Label { Text = "Type:", Location = new Point(20, yPos), Width = labelWidth };
                                    Label lblTypeValue = new Label { Text = fineType, Location = new Point(180, yPos), Width = valueWidth };
                                    detailsForm.Controls.AddRange(new Control[] { lblType, lblTypeValue });
                                    yPos += 35;
                                    
                                    // Days Overdue
                                    int daysOverdue = reader["DaysOverdue"] != DBNull.Value ? Convert.ToInt32(reader["DaysOverdue"]) : 0;
                                    Label lblDays = new Label { Text = "Days Overdue:", Location = new Point(20, yPos), Width = labelWidth };
                                    Label lblDaysValue = new Label { Text = daysOverdue.ToString(), Location = new Point(180, yPos), Width = valueWidth };
                                    detailsForm.Controls.AddRange(new Control[] { lblDays, lblDaysValue });
                                    yPos += 35;
                                    
                                    // Amount
                                    decimal amount = reader["Amount"] != DBNull.Value ? Convert.ToDecimal(reader["Amount"]) : 0;
                                    Label lblAmount = new Label { Text = "Amount:", Location = new Point(20, yPos), Width = labelWidth };
                                    Label lblAmountValue = new Label { Text = IDFormatter.FormatCurrency(amount), Location = new Point(180, yPos), Width = valueWidth, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
                                    detailsForm.Controls.AddRange(new Control[] { lblAmount, lblAmountValue });
                                    yPos += 35;
                                    
                                    // Paid
                                    decimal paid = reader["Paid"] != DBNull.Value ? Convert.ToDecimal(reader["Paid"]) : 0;
                                    Label lblPaid = new Label { Text = "Paid:", Location = new Point(20, yPos), Width = labelWidth };
                                    Label lblPaidValue = new Label { Text = IDFormatter.FormatCurrency(paid), Location = new Point(180, yPos), Width = valueWidth, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
                                    detailsForm.Controls.AddRange(new Control[] { lblPaid, lblPaidValue });
                                    yPos += 35;
                                    
                                    // Balance
                                    decimal balance = amount - paid;
                                    Label lblBalance = new Label { Text = "Balance:", Location = new Point(20, yPos), Width = labelWidth };
                                    Label lblBalanceValue = new Label 
                                    { 
                                        Text = IDFormatter.FormatCurrency(balance), 
                                        Location = new Point(180, yPos), 
                                        Width = valueWidth,
                                        Font = new Font("Segoe UI", 10, FontStyle.Bold),
                                        ForeColor = balance > 0 ? Color.Red : Color.Green
                                    };
                                    detailsForm.Controls.AddRange(new Control[] { lblBalance, lblBalanceValue });
                                    yPos += 35;
                                    
                                    // Status
                                    string status = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : "Pending";
                                    Label lblStatus = new Label { Text = "Status:", Location = new Point(20, yPos), Width = labelWidth };
                                    Label lblStatusValue = new Label { Text = status, Location = new Point(180, yPos), Width = valueWidth, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
                                    detailsForm.Controls.AddRange(new Control[] { lblStatus, lblStatusValue });
                                    yPos += 35;
                                    
                                    // Created Date
                                    if (reader["CreatedDate"] != DBNull.Value)
                                    {
                                        DateTime createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                                        Label lblCreated = new Label { Text = "Created Date:", Location = new Point(20, yPos), Width = labelWidth };
                                        Label lblCreatedValue = new Label { Text = createdDate.ToString("yyyy-MM-dd HH:mm:ss"), Location = new Point(180, yPos), Width = valueWidth };
                                        detailsForm.Controls.AddRange(new Control[] { lblCreated, lblCreatedValue });
                                        yPos += 35;
                                    }
                                    
                                    // Paid Date
                                    if (reader["PaidDate"] != DBNull.Value)
                                    {
                                        DateTime paidDate = Convert.ToDateTime(reader["PaidDate"]);
                                        Label lblPaidDate = new Label { Text = "Paid Date:", Location = new Point(20, yPos), Width = labelWidth };
                                        Label lblPaidDateValue = new Label { Text = paidDate.ToString("yyyy-MM-dd HH:mm:ss"), Location = new Point(180, yPos), Width = valueWidth };
                                        detailsForm.Controls.AddRange(new Control[] { lblPaidDate, lblPaidDateValue });
                                        yPos += 35;
                                    }
                                    
                                    // Waived Date
                                    if (reader["WaivedDate"] != DBNull.Value)
                                    {
                                        DateTime waivedDate = Convert.ToDateTime(reader["WaivedDate"]);
                                        Label lblWaivedDate = new Label { Text = "Waived Date:", Location = new Point(20, yPos), Width = labelWidth };
                                        Label lblWaivedDateValue = new Label { Text = waivedDate.ToString("yyyy-MM-dd HH:mm:ss"), Location = new Point(180, yPos), Width = valueWidth };
                                        detailsForm.Controls.AddRange(new Control[] { lblWaivedDate, lblWaivedDateValue });
                                        yPos += 35;
                                    }
                                    
                                    // Waiver Reason (only show if status is Waived and reason exists)
                                    string currentStatus = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : "";
                                    if (currentStatus.Equals("Waived", StringComparison.OrdinalIgnoreCase))
                                    {
                                        string waiverReason = "";
                                        try
                                        {
                                            // Try to get WaiverReason from the reader
                                            if (reader["WaiverReason"] != DBNull.Value && reader["WaiverReason"] != null)
                                            {
                                                waiverReason = reader["WaiverReason"].ToString();
                                            }
                                        }
                                        catch
                                        {
                                            // Column doesn't exist or can't be accessed, leave waiverReason empty
                                            waiverReason = "";
                                        }
                                        
                                        if (!string.IsNullOrWhiteSpace(waiverReason))
                                        {
                                            Label lblWaiverReason = new Label { Text = "Waiver Reason:", Location = new Point(20, yPos), Width = labelWidth, AutoSize = false };
                                            TextBox txtWaiverReason = new TextBox 
                                            { 
                                                Text = waiverReason, 
                                                Location = new Point(180, yPos), 
                                                Width = valueWidth,
                                                Height = 60,
                                                Multiline = true,
                                                ReadOnly = true,
                                                ScrollBars = ScrollBars.Vertical
                                            };
                                            detailsForm.Controls.AddRange(new Control[] { lblWaiverReason, txtWaiverReason });
                                            yPos += 70;
                                        }
                                    }
                                    
                                    // Description
                                    if (reader["Description"] != DBNull.Value && !string.IsNullOrEmpty(reader["Description"].ToString()))
                                    {
                                        string description = reader["Description"].ToString();
                                        Label lblDesc = new Label { Text = "Description:", Location = new Point(20, yPos), Width = labelWidth };
                                        TextBox txtDesc = new TextBox 
                                        { 
                                            Text = description, 
                                            Location = new Point(180, yPos), 
                                            Width = valueWidth,
                                            Height = 60,
                                            Multiline = true,
                                            ReadOnly = true,
                                            ScrollBars = ScrollBars.Vertical
                                        };
                                        detailsForm.Controls.AddRange(new Control[] { lblDesc, txtDesc });
                                        yPos += 70;
                                    }
                                    
                                    // Payment History button
                                    Button btnPaymentHistory = new Button
                                    {
                                        Text = "View Payment History",
                                        Location = new Point(180, yPos + 20),
                                        Size = new Size(200, 35),
                                        DialogResult = DialogResult.None
                                    };
                                    btnPaymentHistory.Click += (s, args) =>
                                    {
                                        if (memberId > 0)
                                        {
                                            detailsForm.DialogResult = DialogResult.None;
                                            detailsForm.Hide();
                                            ShowPaymentHistory(memberId);
                                            detailsForm.Show();
                                        }
                                    };
                                    detailsForm.Controls.Add(btnPaymentHistory);
                                    
                                    // Close button
                                    Button btnClose = new Button
                                    {
                                        Text = "Close",
                                        Location = new Point(400, yPos + 20),
                                        Size = new Size(100, 35),
                                        DialogResult = DialogResult.OK
                                    };
                                    detailsForm.Controls.Add(btnClose);
                                    detailsForm.AcceptButton = btnClose;
                                    
                                    // Adjust form height based on content
                                    int minHeight = yPos + 100;
                                    if (detailsForm.Height < minHeight)
                                    {
                                        detailsForm.Height = Math.Min(minHeight, 700); // Max height of 700
                                    }
                                    
                                    detailsForm.ShowDialog();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Fine details not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading fine details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"ShowFineDetails error: {ex}");
            }
        }
        
        private void DataGridViewFines_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle cell clicks for Actions column (same logic as CellContentClick)
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            string columnName = dataGridViewFines.Columns[e.ColumnIndex].Name;
            if (columnName == "Actions")
            {
                DataGridViewFines_CellContentClick(sender, e);
            }
        }
        
        private void btnAddCharge_Click(object sender, EventArgs e)
        {
            try
            {
                using (var addChargeForm = new AddChargeForm())
                {
                    if (addChargeForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadMetrics();
                        LoadFines();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening add charge form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lblMetricWaivedTitle_Click(object sender, EventArgs e)
        {
        }
        private void FineSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (sender is TextBox txtSearch)
                {
                    currentSearchText = txtSearch.Text ?? "";
                    LoadFines();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in search: {ex.Message}");
            }
        }
        
        private void FineSearch_KeyUp(object sender, KeyEventArgs e)
        {
            // Allow Enter key to trigger search
            if (e.KeyCode == Keys.Enter)
            {
                FineSearch_TextChanged(sender, e);
            }
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

        private void panelPaymentHistory_Paint(object sender, PaintEventArgs e)
        {                    }

        private void dataGridViewFines_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panelTableContainer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelFilters_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainerMain_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainerMain_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void dataGridViewFines_CellContentClick_3(object sender, DataGridViewCellEventArgs e)
        {

        }
        
        private string ShowPaymentModeDialog()
        {
            using (Form paymentModeForm = new Form())
            {
                paymentModeForm.Text = "Select Payment Mode";
                paymentModeForm.Size = new Size(350, 180);
                paymentModeForm.StartPosition = FormStartPosition.CenterParent;
                paymentModeForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                paymentModeForm.MaximizeBox = false;
                paymentModeForm.MinimizeBox = false;
                
                Label lblTitle = new Label
                {
                    Text = "Payment Mode:",
                    Location = new Point(20, 20),
                    AutoSize = true,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                };
                paymentModeForm.Controls.Add(lblTitle);
                
                ComboBox cmbPaymentMode = new ComboBox
                {
                    Location = new Point(20, 50),
                    Size = new Size(290, 30),
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Font = new Font("Segoe UI", 10F)
                };
                cmbPaymentMode.Items.Add("Cash");
                cmbPaymentMode.Items.Add("Online");
                cmbPaymentMode.SelectedIndex = 0;
                paymentModeForm.Controls.Add(cmbPaymentMode);
                
                Button btnOK = new Button
                {
                    Text = "OK",
                    DialogResult = DialogResult.OK,
                    Location = new Point(150, 100),
                    Size = new Size(80, 35),
                    BackColor = Color.Maroon,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat
                };
                paymentModeForm.Controls.Add(btnOK);
                
                Button btnCancel = new Button
                {
                    Text = "Cancel",
                    DialogResult = DialogResult.Cancel,
                    Location = new Point(240, 100),
                    Size = new Size(80, 35)
                };
                paymentModeForm.Controls.Add(btnCancel);
                
                paymentModeForm.AcceptButton = btnOK;
                paymentModeForm.CancelButton = btnCancel;
                
                if (paymentModeForm.ShowDialog() == DialogResult.OK)
                {
                    return cmbPaymentMode.SelectedItem?.ToString() ?? "Cash";
                }
                return null;
            }
        }
        
        private string GeneratePaymentReceipt(FinePayment payment, decimal amountPaid, string paymentType)
        {
            var member = _membersService.GetMember(payment.MemberID);
            string memberName = member != null ? member.FullName : "Unknown";
            string memberId = member != null ? IDFormatter.FormatMemberID(member.MemberID) : payment.MemberID.ToString();
            
            return $"═══════════════════════════════════\n" +
                   $"   PAYMENT RECEIPT\n" +
                   $"═══════════════════════════════════\n" +
                   $"Receipt No: {payment.ReceiptNumber}\n" +
                   $"Date: {payment.PaymentDate:yyyy-MM-dd HH:mm:ss}\n\n" +
                   $"Member:\n" +
                   $"  ID: {memberId}\n" +
                   $"  Name: {memberName}\n\n" +
                   $"Payment Details:\n" +
                   $"  Type: {paymentType}\n" +
                   $"  Amount: {IDFormatter.FormatCurrency(amountPaid)}\n" +
                   $"  Mode: {payment.PaymentMode}\n" +
                   $"  Processed By: {payment.ProcessedBy}\n" +
                   $"═══════════════════════════════════";
        }
    }
}