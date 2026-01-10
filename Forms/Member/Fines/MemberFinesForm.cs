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
namespace Project5LMS.Forms.Member.Fines
{
    public partial class MemberFinesForm : Form
    {
        private readonly DatabaseContext _dbContext;
        private readonly IFinesService _finesService;
        private decimal totalOutstanding = 0;
        public MemberFinesForm()
        {
            InitializeComponent();
            _dbContext = ServiceFactory.GetDbContext();
            _finesService = ServiceFactory.CreateFinesService();
            this.Load += MemberFinesForm_Load;
            this.VisibleChanged += MemberFinesForm_VisibleChanged;
            this.BackColor = Color.FromArgb(250, 250, 250);
            this.Visible = true;
            if (this.Visible)
            {
                LoadFines();
            }
        }
        private void MemberFinesForm_Load(object sender, EventArgs e)
        {
            LoadFines();
        }
        private void MemberFinesForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                LoadFines();
            }
        }
        private void LoadFines()
        {
            this.SuspendLayout();
            panelMainContainer.Visible = true;
            panelHeader.Visible = true;
            panelTotalOutstanding.Visible = true;
            panelOutstandingFines.Visible = true;
            panelPaymentHistory.Visible = true;
            panelFineRates.Visible = true;
            this.ResumeLayout();
            this.Invalidate();
            this.Refresh();
            int memberID = CurrentUser.GetMemberID();
            if (memberID == 0)
            {
                lblOutstandingFinesCount.Text = "Outstanding Fines (0)";
                lblTotalAmount.Text = "$0.00";
                btnPayNow.Enabled = false;
                return;
            }
            LoadOutstandingFines(memberID);
            LoadPaymentHistory(memberID);
            UpdateTotalOutstanding();
        }
        private void LoadOutstandingFines(int memberID)
        {
            try
            {
                panelOutstandingFinesList.Controls.Clear();
                totalOutstanding = 0;
                panelOutstandingFines.Visible = true;
                panelTotalOutstanding.Visible = true;
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
                            lblOutstandingFinesCount.Text = "Outstanding Fines (0)";
                            Label lblEmpty = new Label
                            {
                                Text = "No outstanding fines at this time.",
                                Font = new Font("Segoe UI", 12F),
                                ForeColor = Color.FromArgb(128, 128, 128),
                                Location = new Point(20, 20),
                                AutoSize = true
                            };
                            panelOutstandingFinesList.Controls.Add(lblEmpty);
                            return;
                        }
                    }
                    bool hasFineType = DatabaseSchemaHelper.CheckColumnExists(conn, "Fines", "FineType");
                    bool hasDescription = DatabaseSchemaHelper.CheckColumnExists(conn, "Fines", "Description");
                    bool hasIssueDate = DatabaseSchemaHelper.CheckColumnExists(conn, "Fines", "IssueDate");
                    bool hasCreatedDate = DatabaseSchemaHelper.CheckColumnExists(conn, "Fines", "CreatedDate");
                    bool hasStatus = DatabaseSchemaHelper.CheckColumnExists(conn, "Fines", "Status");
                    bool hasPaid = DatabaseSchemaHelper.CheckColumnExists(conn, "Fines", "Paid");
                    string dateColumn = hasIssueDate ? "IssueDate" : (hasCreatedDate ? "CreatedDate" : "NULL");
                    string statusColumn = hasStatus ? "Status" : "'Unpaid'";
                    string paidColumn = hasPaid ? "Paid" : "0";
                    string query = $@"SELECT
                                    f.FineID,
                                    b.Title as BookTitle,
                                    f.Amount,
                                    {paidColumn} as Paid,
                                    {dateColumn} as IssueDate,
                                    f.PaidDate,
                                    {statusColumn} as Status,
                                    " + (hasFineType ? "f.FineType" : "'Overdue'") + @" as FineType,
                                    " + (hasDescription ? "f.Description" : "NULL") + @" as Description
                                FROM Fines f
                                LEFT JOIN Books b ON f.BookID = b.BookID
                                WHERE f.MemberID = @MemberID
                                AND ({statusColumn} != 'Paid' AND {statusColumn} != 'Waived')
                                AND (f.PaidDate IS NULL OR {paidColumn} < f.Amount)
                                ORDER BY {dateColumn} DESC";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", memberID);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            int yPos = 0;
                            int count = 0;
                            while (reader.Read())
                            {
                                count++;
                                int fineID = reader.GetInt32("FineID");
                                string bookTitle = reader["BookTitle"] != DBNull.Value ? reader["BookTitle"].ToString() : "N/A";
                                decimal amount = reader["Amount"] != DBNull.Value ? Convert.ToDecimal(reader["Amount"]) : 0;
                                decimal paid = reader["Paid"] != DBNull.Value ? Convert.ToDecimal(reader["Paid"]) : 0;
                                DateTime issueDate = reader["IssueDate"] != DBNull.Value && reader["IssueDate"].ToString() != "" ? Convert.ToDateTime(reader["IssueDate"]) : DateTime.Now;
                                string fineType = reader["FineType"] != DBNull.Value ? reader["FineType"].ToString() : "Overdue";
                                string description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : "";
                                string status = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : "Unpaid";
                                decimal outstanding = amount - paid;
                                totalOutstanding += outstanding;
                                string reason = !string.IsNullOrEmpty(description) ? description : GetDefaultReason(fineType);
                                string fineIDFormatted = $"FINE-{fineID.ToString().PadLeft(3, '0')}";
                                Panel fineCard = CreateOutstandingFineCard(fineID, bookTitle, fineType, reason, fineIDFormatted, issueDate, outstanding, status);
                                fineCard.Location = new Point(0, yPos);
                                fineCard.Width = panelOutstandingFinesList.Width - 20;
                                panelOutstandingFinesList.Controls.Add(fineCard);
                                yPos += fineCard.Height + 15;
                            }
                            lblOutstandingFinesCount.Text = $"Outstanding Fines ({count})";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading outstanding fines: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadPaymentHistory(int memberID)
        {
            try
            {
                panelPaymentHistoryList.Controls.Clear();
                panelPaymentHistory.Visible = true;
                panelFineRates.Visible = true;
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
                            Label lblEmpty = new Label
                            {
                                Text = "No payment history available.",
                                Font = new Font("Segoe UI", 12F),
                                ForeColor = Color.FromArgb(128, 128, 128),
                                Location = new Point(20, 20),
                                AutoSize = true
                            };
                            panelPaymentHistoryList.Controls.Add(lblEmpty);
                            return;
                        }
                    }
                    bool hasFineType = DatabaseSchemaHelper.CheckColumnExists(conn, "Fines", "FineType");
                    bool hasIssueDate = DatabaseSchemaHelper.CheckColumnExists(conn, "Fines", "IssueDate");
                    bool hasCreatedDate = DatabaseSchemaHelper.CheckColumnExists(conn, "Fines", "CreatedDate");
                    bool hasStatus = DatabaseSchemaHelper.CheckColumnExists(conn, "Fines", "Status");
                    bool hasPaid = DatabaseSchemaHelper.CheckColumnExists(conn, "Fines", "Paid");
                    string dateColumn = hasIssueDate ? "IssueDate" : (hasCreatedDate ? "CreatedDate" : "NULL");
                    string statusColumn = hasStatus ? "Status" : "'Paid'";
                    string query = $@"SELECT
                                    f.FineID,
                                    b.Title as BookTitle,
                                    f.Amount,
                                    {dateColumn} as IssueDate,
                                    f.PaidDate,
                                    {statusColumn} as Status,
                                    " + (hasFineType ? "f.FineType" : "'Overdue'") + @" as FineType
                                FROM Fines f
                                LEFT JOIN Books b ON f.BookID = b.BookID
                                WHERE f.MemberID = @MemberID
                                AND (f.PaidDate IS NOT NULL OR {statusColumn} = 'Paid')
                                ORDER BY f.PaidDate DESC
                                LIMIT 50";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", memberID);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            int yPos = 0;
                            while (reader.Read())
                            {
                                int fineID = reader.GetInt32("FineID");
                                string bookTitle = reader["BookTitle"] != DBNull.Value ? reader["BookTitle"].ToString() : "N/A";
                                decimal amount = reader["Amount"] != DBNull.Value ? Convert.ToDecimal(reader["Amount"]) : 0;
                                DateTime issueDate = reader["IssueDate"] != DBNull.Value && reader["IssueDate"].ToString() != "" ? Convert.ToDateTime(reader["IssueDate"]) : DateTime.Now;
                                DateTime paidDate = reader["PaidDate"] != DBNull.Value ? Convert.ToDateTime(reader["PaidDate"]) : DateTime.Now;
                                string fineType = reader["FineType"] != DBNull.Value ? reader["FineType"].ToString() : "Overdue";
                                string paymentID = $"PAY-{fineID.ToString().PadLeft(3, '0')}";
                                Panel paymentCard = CreatePaymentHistoryCard(fineID, bookTitle, fineType, paymentID, paidDate, amount);
                                paymentCard.Location = new Point(0, yPos);
                                paymentCard.Width = panelPaymentHistoryList.Width - 40;
                                panelPaymentHistoryList.Controls.Add(paymentCard);
                                yPos += paymentCard.Height + 1;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading payment history: {ex.Message}");
            }
        }
        private void UpdateTotalOutstanding()
        {
            lblTotalAmount.Text = $"${totalOutstanding:F2}";
            btnPayNow.Enabled = totalOutstanding > 0;
        }
        private string GetDefaultReason(string fineType)
        {
            switch (fineType.ToLower())
            {
                case "overdue":
                    return "Book returned late";
                case "lost":
                    return "Book reported as lost";
                case "damaged":
                    return "Book returned damaged";
                default:
                    return "Fine issued";
            }
        }
        private Panel CreateOutstandingFineCard(int fineID, string bookTitle, string fineType, string reason, string fineIDFormatted, DateTime issueDate, decimal amount, string status)
        {
            Panel card = new Panel
            {
                BackColor = Color.White,
                Size = new Size(1180, 160),
                Padding = new Padding(25, 20, 25, 20),
                Margin = new Padding(0, 0, 0, 15),
                BorderStyle = BorderStyle.FixedSingle
            };
            Label lblBookTitle = new Label
            {
                Text = bookTitle,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                Location = new Point(25, 20),
                AutoSize = true,
                MaximumSize = new Size(600, 0)
            };
            Panel panelStatus = new Panel
            {
                BackColor = Color.FromArgb(255, 200, 200),
                Location = new Point(650, 20),
                Size = new Size(100, 30),
                Padding = new Padding(10, 5, 10, 5)
            };
            Label lblStatus = new Label
            {
                Text = fineType,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(220, 20, 60),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            panelStatus.Controls.Add(lblStatus);
            Label lblReason = new Label
            {
                Text = $"Reason: {reason}",
                Font = new Font("Segoe UI", 11F),
                ForeColor = Color.FromArgb(96, 96, 96),
                Location = new Point(25, 55),
                AutoSize = true
            };
            Label lblFineInfo = new Label
            {
                Text = $"{fineIDFormatted} � Issued: {issueDate:yyyy-MM-dd}",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(128, 128, 128),
                Location = new Point(25, 85),
                AutoSize = true
            };
            Label lblAmount = new Label
            {
                Text = $"${amount:F2}",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Color.FromArgb(220, 20, 60),
                Location = new Point(1000, 50),
                AutoSize = true
            };
            Panel panelPaymentStatus = new Panel
            {
                BackColor = Color.FromArgb(255, 200, 200),
                Location = new Point(1000, 100),
                Size = new Size(100, 30),
                Padding = new Padding(10, 5, 10, 5)
            };
            Label lblPaymentStatus = new Label
            {
                Text = "Unpaid",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(220, 20, 60),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            panelPaymentStatus.Controls.Add(lblPaymentStatus);
            card.Controls.Add(lblBookTitle);
            card.Controls.Add(panelStatus);
            card.Controls.Add(lblReason);
            card.Controls.Add(lblFineInfo);
            card.Controls.Add(lblAmount);
            card.Controls.Add(panelPaymentStatus);
            return card;
        }
        private Panel CreatePaymentHistoryCard(int fineID, string bookTitle, string fineType, string paymentID, DateTime paidDate, decimal amount)
        {
            Panel card = new Panel
            {
                BackColor = Color.White,
                Size = new Size(710, 120),
                Padding = new Padding(25, 20, 25, 20),
                Margin = new Padding(0, 0, 0, 0),
                BorderStyle = BorderStyle.FixedSingle
            };
            Label lblBookTitle = new Label
            {
                Text = bookTitle,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                Location = new Point(25, 20),
                AutoSize = true,
                MaximumSize = new Size(400, 0)
            };
            Panel panelStatus = new Panel
            {
                BackColor = Color.FromArgb(240, 240, 240),
                Location = new Point(450, 20),
                Size = new Size(120, 30),
                Padding = new Padding(10, 5, 10, 5)
            };
            Label lblStatus = new Label
            {
                Text = fineType,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            panelStatus.Controls.Add(lblStatus);
            Label lblPaymentDetails = new Label
            {
                Text = $"{paymentID} � Paid: {paidDate:yyyy-MM-dd} � Method: Cash",
                Font = new Font("Segoe UI", 11F),
                ForeColor = Color.FromArgb(128, 128, 128),
                Location = new Point(25, 55),
                AutoSize = true
            };
            Label lblAmount = new Label
            {
                Text = $"${amount:F2}",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(76, 175, 80),
                Location = new Point(550, 50),
                AutoSize = true
            };
            Panel panelPaidStatus = new Panel
            {
                BackColor = Color.FromArgb(200, 255, 200),
                Location = new Point(550, 85),
                Size = new Size(80, 30),
                Padding = new Padding(10, 5, 10, 5)
            };
            Label lblPaidStatus = new Label
            {
                Text = "Paid",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(34, 139, 34),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            panelPaidStatus.Controls.Add(lblPaidStatus);
            Panel separator = new Panel
            {
                BackColor = Color.FromArgb(230, 230, 230),
                Location = new Point(25, 119),
                Size = new Size(card.Width - 50, 1)
            };
            card.Controls.Add(lblBookTitle);
            card.Controls.Add(panelStatus);
            card.Controls.Add(lblPaymentDetails);
            card.Controls.Add(lblAmount);
            card.Controls.Add(panelPaidStatus);
            card.Controls.Add(separator);
            return card;
        }
        private void btnPayNow_Click(object sender, EventArgs e)
        {
            if (totalOutstanding <= 0)
            {
                MessageBox.Show("You have no outstanding fines to pay.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DialogResult result = MessageBox.Show(
                $"Do you want to pay ${totalOutstanding:F2} for all outstanding fines?\n\nNote: Payment processing will be handled at the library circulation desk.",
                "Pay Fines",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (result == DialogResult.Yes)
            {
                MessageBox.Show(
                    $"Please visit the library circulation desk to complete your payment of ${totalOutstanding:F2}.\n\nYour Member ID: {CurrentUser.GetMemberID()}",
                    "Payment Instructions",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }
        private void panelExclamationIcon_Paint(object sender, PaintEventArgs e)
        {
            DrawExclamationIcon(e.Graphics, panelExclamationIcon.ClientRectangle);
        }
        private void DrawExclamationIcon(Graphics g, Rectangle rect)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int centerX = rect.Width / 2;
            int centerY = rect.Height / 2;
            int radius = Math.Min(rect.Width, rect.Height) / 2 - 2;
            using (Brush brush = new SolidBrush(Color.FromArgb(220, 20, 60)))
            {
                g.FillEllipse(brush, centerX - radius, centerY - radius, radius * 2, radius * 2);
            }
            using (Font font = new Font("Segoe UI", radius, FontStyle.Bold))
            using (Brush brush = new SolidBrush(Color.White))
            {
                SizeF textSize = g.MeasureString("!", font);
                float x = centerX - textSize.Width / 2;
                float y = centerY - textSize.Height / 2;
                g.DrawString("!", font, brush, x, y);
            }
        }
        private void panelDollarIcon_Paint(object sender, PaintEventArgs e)
        {
            DrawDollarIcon(e.Graphics, panelDollarIcon.ClientRectangle);
        }
        private void DrawDollarIcon(Graphics g, Rectangle rect)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int centerX = rect.Width / 2;
            int centerY = rect.Height / 2;
            using (Pen pen = new Pen(Color.FromArgb(64, 64, 64), 2))
            {
                g.DrawString("$", new Font("Segoe UI", 20, FontStyle.Bold), new SolidBrush(Color.FromArgb(64, 64, 64)), centerX - 8, centerY - 12);
            }
        }
    }
}