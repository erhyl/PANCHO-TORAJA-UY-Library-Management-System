using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Project5LMS.Helpers;
using Project5LMS.Data;
using Project5LMS.Services;

namespace Project5LMS.Forms.Admin.Reports
{
    public partial class AdminReportsForm : Form
    {
        private readonly DatabaseContext _dbContext;
        private string currentReportType = "Circulation";
        private DateTime startDate;
        private DateTime endDate;

        private Panel panelChart;
        private DataGridView dataGridViewReports;

        public AdminReportsForm()
        {
            InitializeComponent();
            _dbContext = ServiceFactory.GetDbContext();
        }

        private void AdminReportsForm_Load(object sender, EventArgs e)
        {

            cmbDateRange.Items.AddRange(new string[] {
                "Today",
                "This Week",
                "This Month",
                "This Year",
                "Last Week",
                "Last Month",
                "Last Year",
                "Custom Range"
            });
            cmbDateRange.SelectedIndex = 1;
            UpdateDateRange();
            LoadReportContent("Circulation");
        }

        private void UpdateDateRange()
        {
            DateTime now = DateTime.Now;
            switch (cmbDateRange.SelectedIndex)
            {
                case 0:
                    startDate = now.Date;
                    endDate = now.Date.AddDays(1).AddSeconds(-1);
                    break;
                case 1:
                    int daysUntilMonday = ((int)now.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
                    startDate = now.Date.AddDays(-daysUntilMonday);
                    endDate = startDate.AddDays(7).AddSeconds(-1);
                    break;
                case 2:
                    startDate = new DateTime(now.Year, now.Month, 1);
                    endDate = startDate.AddMonths(1).AddSeconds(-1);
                    break;
                case 3:
                    startDate = new DateTime(now.Year, 1, 1);
                    endDate = startDate.AddYears(1).AddSeconds(-1);
                    break;
                case 4:
                    int daysUntilLastMonday = ((int)now.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
                    startDate = now.Date.AddDays(-daysUntilLastMonday - 7);
                    endDate = startDate.AddDays(7).AddSeconds(-1);
                    break;
                case 5:
                    startDate = new DateTime(now.Year, now.Month, 1).AddMonths(-1);
                    endDate = startDate.AddMonths(1).AddSeconds(-1);
                    break;
                case 6:
                    startDate = new DateTime(now.Year - 1, 1, 1);
                    endDate = startDate.AddYears(1).AddSeconds(-1);
                    break;
                default:
                    startDate = now.Date.AddDays(-7);
                    endDate = now.Date.AddDays(1).AddSeconds(-1);
                    break;
            }
        }

        private void cmbDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDateRange();
            LoadReportContent(currentReportType);
        }

        private void btnCirculationReports_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnCirculationReports);
            LoadReportContent("Circulation");
        }

        private void btnMemberReports_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnMemberReports);
            LoadReportContent("Member");
        }

        private void btnCollectionReports_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnCollectionReports);
            LoadReportContent("Collection");
        }

        private void btnFinancialReports_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnFinancialReports);
            LoadReportContent("Financial");
        }

        private void btnStatisticalReports_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnStatisticalReports);
            LoadReportContent("Statistical");
        }

        private void SetActiveButton(Button activeButton)
        {

            btnCirculationReports.BackColor = Color.White;
            btnCirculationReports.ForeColor = Color.FromArgb(64, 64, 64);
            btnMemberReports.BackColor = Color.White;
            btnMemberReports.ForeColor = Color.FromArgb(64, 64, 64);
            btnCollectionReports.BackColor = Color.White;
            btnCollectionReports.ForeColor = Color.FromArgb(64, 64, 64);
            btnFinancialReports.BackColor = Color.White;
            btnFinancialReports.ForeColor = Color.FromArgb(64, 64, 64);
            btnStatisticalReports.BackColor = Color.White;
            btnStatisticalReports.ForeColor = Color.FromArgb(64, 64, 64);

            activeButton.BackColor = Color.FromArgb(13, 110, 253);
            activeButton.ForeColor = Color.White;
        }

        private void LoadReportContent(string reportType)
        {
            currentReportType = reportType;
            panelContent.Controls.Clear();
            // Remove placeholder label if it exists
            if (lblContentPlaceholder != null && panelContent.Controls.Contains(lblContentPlaceholder))
            {
                panelContent.Controls.Remove(lblContentPlaceholder);
            }

            switch (reportType)
            {
                case "Circulation":
                    LoadCirculationReport();
                    break;
                case "Member":
                    LoadMemberReport();
                    break;
                case "Collection":
                    LoadCollectionReport();
                    break;
                case "Financial":
                    LoadFinancialReport();
                    break;
                case "Statistical":
                    LoadStatisticalReport();
                    break;
            }
        }

        private void LoadCirculationReport()
        {

            panelChart = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(20)
            };
            panelChart.Paint += PanelChart_Circulation_Paint;
            panelContent.Controls.Add(panelChart);
            panelChart.Invalidate(); // Force repaint to load data
        }

        private void PanelChart_Circulation_Paint(object sender, PaintEventArgs e)
        {
            DrawLineChart(e.Graphics, panelChart, "Daily Borrowing & Return Trends", 
                GetDailyBorrowingData(), GetDailyReturnData());
        }

        private void LoadMemberReport()
        {

            TableLayoutPanel container = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1,
                Padding = new Padding(20)
            };
            container.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            container.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));

            panelChart = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Margin = new Padding(0, 0, 0, 10)
            };
            panelChart.Paint += PanelChart_Member_Paint;
            container.Controls.Add(panelChart, 0, 0);

            Panel tablePanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            dataGridViewReports = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None
            };
            tablePanel.Controls.Add(dataGridViewReports);
            container.Controls.Add(tablePanel, 0, 1);

            panelContent.Controls.Add(container);
            panelChart.Invalidate(); // Force repaint to load chart data
            LoadNewMemberRegistrations();
        }

        private void PanelChart_Member_Paint(object sender, PaintEventArgs e)
        {
            DrawBarChart(e.Graphics, panelChart, "Member Activity by Type", GetMemberActivityData());
        }

        private void LoadCollectionReport()
        {

            FlowLayoutPanel summaryPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 100,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(0, 0, 0, 20)
            };

            var summaryData = GetCollectionSummary();
            CreateSummaryCard(summaryPanel, "Total Books", summaryData["Total"].ToString(), Color.FromArgb(13, 110, 253));
            CreateSummaryCard(summaryPanel, "Available", summaryData["Available"].ToString(), Color.FromArgb(40, 167, 69));
            CreateSummaryCard(summaryPanel, "On Loan", summaryData["OnLoan"].ToString(), Color.FromArgb(255, 193, 7));
            panelContent.Controls.Add(summaryPanel);

            TableLayoutPanel container = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1,
                Padding = new Padding(0, 20, 0, 0)
            };
            container.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            container.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

            panelChart = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Margin = new Padding(0, 0, 0, 10)
            };
            panelChart.Paint += PanelChart_Collection_Paint;
            container.Controls.Add(panelChart, 0, 0);

            Panel tablePanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            dataGridViewReports = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None
            };
            tablePanel.Controls.Add(dataGridViewReports);
            container.Controls.Add(tablePanel, 0, 1);

            panelContent.Controls.Add(container);
            panelChart.Invalidate(); // Force repaint to load chart data
            LoadMostBorrowedBooks();
        }

        private void PanelChart_Collection_Paint(object sender, PaintEventArgs e)
        {
            DrawBarChart(e.Graphics, panelChart, "Borrowing by Category", GetBorrowingByCategoryData());
        }

        private void LoadFinancialReport()
        {

            FlowLayoutPanel summaryPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 100,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(0, 0, 0, 20)
            };

            var summaryData = GetFinancialSummary();
            CreateSummaryCard(summaryPanel, "Fines Collected", "$" + summaryData["Collected"].ToString("F2"), Color.FromArgb(40, 167, 69));
            CreateSummaryCard(summaryPanel, "Pending Fines", "$" + summaryData["Pending"].ToString("F2"), Color.FromArgb(255, 193, 7));
            CreateSummaryCard(summaryPanel, "Waived Fines", "$" + summaryData["Waived"].ToString("F2"), Color.FromArgb(220, 53, 69));
            panelContent.Controls.Add(summaryPanel);

            Panel tablePanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(0, 20, 0, 0)
            };
            dataGridViewReports = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None
            };
            tablePanel.Controls.Add(dataGridViewReports);
            panelContent.Controls.Add(tablePanel);
            LoadOverdueBooksReport();
        }

        private void LoadStatisticalReport()
        {

            FlowLayoutPanel summaryPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 100,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(0, 0, 0, 20)
            };

            var statsData = GetLibraryUsageStatistics();
            CreateSummaryCard(summaryPanel, "Daily Average Visits", Convert.ToString(statsData["DailyVisits"]), Color.FromArgb(13, 110, 253));
            CreateSummaryCard(summaryPanel, "Books Per Member", Convert.ToDouble(statsData["BooksPerMember"]).ToString("F1"), Color.FromArgb(40, 167, 69));
            CreateSummaryCard(summaryPanel, "Avg. Borrowing Period", Convert.ToString(statsData["AvgPeriod"]) + " days", Color.FromArgb(255, 193, 7));
            CreateSummaryCard(summaryPanel, "Collection Turnover", Convert.ToString(statsData["Turnover"]) + "%", Color.FromArgb(220, 53, 69));
            panelContent.Controls.Add(summaryPanel);
        }

        private void CreateSummaryCard(FlowLayoutPanel parent, string title, string value, Color accentColor)
        {
            Panel card = new Panel
            {
                Width = 300,
                Height = 100,
                BackColor = Color.White,
                Margin = new Padding(0, 0, 20, 0),
                Padding = new Padding(20)
            };

            Label lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                ForeColor = Color.FromArgb(128, 128, 128),
                AutoSize = true,
                Location = new Point(20, 20)
            };

            Label lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                AutoSize = true,
                Location = new Point(20, 50)
            };

            Panel accentBar = new Panel
            {
                Height = 4,
                Width = card.Width,
                BackColor = accentColor,
                Dock = DockStyle.Bottom
            };

            card.Controls.Add(lblTitle);
            card.Controls.Add(lblValue);
            card.Controls.Add(accentBar);
            parent.Controls.Add(card);
        }

        private Dictionary<DateTime, int> GetDailyBorrowingData()
        {
            Dictionary<DateTime, int> data = new Dictionary<DateTime, int>();
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT DATE(BorrowDate) as Date, COUNT(*) as Count
                                   FROM Transactions
                                   WHERE BorrowDate >= @StartDate AND BorrowDate <= @EndDate
                                   GROUP BY DATE(BorrowDate)
                                   ORDER BY Date";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DateTime date = reader.GetDateTime("Date");
                                int count = Convert.ToInt32(reader["Count"]);
                                data[date] = count;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading borrowing data: {ex.Message}");
                MessageBox.Show($"Error loading borrowing data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return data;
        }

        private Dictionary<DateTime, int> GetDailyReturnData()
        {
            Dictionary<DateTime, int> data = new Dictionary<DateTime, int>();
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT DATE(ReturnDate) as Date, COUNT(*) as Count
                                   FROM Transactions
                                   WHERE ReturnDate >= @StartDate AND ReturnDate <= @EndDate
                                   AND ReturnDate IS NOT NULL
                                   GROUP BY DATE(ReturnDate)
                                   ORDER BY Date";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DateTime date = reader.GetDateTime("Date");
                                int count = Convert.ToInt32(reader["Count"]);
                                data[date] = count;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading return data: {ex.Message}");
                MessageBox.Show($"Error loading return data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return data;
        }

        private Dictionary<string, int> GetMemberActivityData()
        {
            Dictionary<string, int> data = new Dictionary<string, int>();
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    // Check if MemberType or Type column exists
                    bool hasMemberType = DatabaseSchemaHelper.CheckColumnExists(conn, "Members", "MemberType");
                    bool hasType = DatabaseSchemaHelper.CheckColumnExists(conn, "Members", "Type");
                    
                    if (!hasMemberType && !hasType)
                    {
                        // If neither column exists, return empty data
                        return data;
                    }
                    
                    string typeColumn = hasMemberType ? "m.MemberType" : "m.Type";
                    
                    string query = $@"SELECT {typeColumn} as MemberType, COUNT(DISTINCT t.TransactionID) as ActivityCount
                                   FROM Transactions t
                                   INNER JOIN Members m ON t.MemberID = m.MemberID
                                   WHERE t.BorrowDate >= @StartDate AND t.BorrowDate <= @EndDate
                                   GROUP BY {typeColumn}";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string type = reader["MemberType"] == DBNull.Value ? "Unknown" : reader["MemberType"].ToString();
                                int count = Convert.ToInt32(reader["ActivityCount"]);
                                data[type] = count;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading member activity: {ex.Message}");
                MessageBox.Show($"Error loading member activity: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return data;
        }

        private Dictionary<string, int> GetBorrowingByCategoryData()
        {
            Dictionary<string, int> data = new Dictionary<string, int>();
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    bool hasCategory = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "Category");
                    
                    if (!hasCategory)
                    {
                        // If Category doesn't exist, return empty data or use a default
                        data["All Books"] = 0;
                        return data;
                    }
                    
                    string query = @"SELECT b.Category, COUNT(*) as BorrowCount
                                   FROM Transactions t
                                   INNER JOIN Books b ON t.BookID = b.BookID
                                   WHERE t.BorrowDate >= @StartDate AND t.BorrowDate <= @EndDate
                                   GROUP BY b.Category
                                   ORDER BY BorrowCount DESC";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string category = reader["Category"] == DBNull.Value ? "Others" : reader["Category"].ToString();
                                int count = Convert.ToInt32(reader["BorrowCount"]);
                                data[category] = count;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading borrowing by category: {ex.Message}");
                MessageBox.Show($"Error loading borrowing by category: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return data;
        }

        private Dictionary<string, object> GetCollectionSummary()
        {
            Dictionary<string, object> summary = new Dictionary<string, object>
            {
                ["Total"] = 0,
                ["Available"] = 0,
                ["OnLoan"] = 0
            };
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    bool hasStatus = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "Status");
                    
                    string availableQuery = hasStatus 
                        ? "(SELECT COUNT(*) FROM Books WHERE Status = 'Available' OR Status IS NULL)"
                        : "(SELECT COUNT(*) FROM Books)"; // If no Status column, assume all are available
                    
                    string query = $@"SELECT 
                                    (SELECT COUNT(*) FROM Books) as Total,
                                    {availableQuery} as Available,
                                    (SELECT COUNT(*) FROM Transactions WHERE Status = 'Borrowed' AND ReturnDate IS NULL) as OnLoan";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                summary["Total"] = Convert.ToInt32(reader["Total"]);
                                summary["Available"] = Convert.ToInt32(reader["Available"]);
                                summary["OnLoan"] = Convert.ToInt32(reader["OnLoan"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading collection summary: {ex.Message}");
                MessageBox.Show($"Error loading collection summary: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return summary;
        }

        private Dictionary<string, decimal> GetFinancialSummary()
        {
            Dictionary<string, decimal> summary = new Dictionary<string, decimal>
            {
                ["Collected"] = 0,
                ["Pending"] = 0,
                ["Waived"] = 0
            };
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    bool hasFinesTable = CheckTableExists(conn, "Fines");
                    if (hasFinesTable)
                    {
                        bool hasPaidDate = CheckColumnExists(conn, "Fines", "PaidDate");
                        bool hasWaivedDate = CheckColumnExists(conn, "Fines", "WaivedDate");
                        bool hasAmount = CheckColumnExists(conn, "Fines", "Amount");

                        if (hasAmount)
                        {
                            string collectedQuery = hasPaidDate
                                ? "SELECT COALESCE(SUM(Amount), 0) FROM Fines WHERE PaidDate IS NOT NULL"
                                : "SELECT COALESCE(SUM(Amount), 0) FROM Fines WHERE Status = 'Paid'";
                            string pendingQuery = hasPaidDate && hasWaivedDate
                                ? "SELECT COALESCE(SUM(Amount), 0) FROM Fines WHERE PaidDate IS NULL AND WaivedDate IS NULL"
                                : "SELECT COALESCE(SUM(Amount), 0) FROM Fines WHERE Status = 'Pending'";
                            string waivedQuery = hasWaivedDate
                                ? "SELECT COALESCE(SUM(Amount), 0) FROM Fines WHERE WaivedDate IS NOT NULL"
                                : "SELECT COALESCE(SUM(Amount), 0) FROM Fines WHERE Status = 'Waived'";

                            using (MySqlCommand cmd = new MySqlCommand(collectedQuery, conn))
                            {
                                summary["Collected"] = Convert.ToDecimal(cmd.ExecuteScalar());
                            }
                            using (MySqlCommand cmd = new MySqlCommand(pendingQuery, conn))
                            {
                                summary["Pending"] = Convert.ToDecimal(cmd.ExecuteScalar());
                            }
                            using (MySqlCommand cmd = new MySqlCommand(waivedQuery, conn))
                            {
                                summary["Waived"] = Convert.ToDecimal(cmd.ExecuteScalar());
                            }
                        }
                    }
                    else
                    {

                        bool hasFine = CheckColumnExists(conn, "Transactions", "Fine");
                        if (hasFine)
                        {
                            string query = @"SELECT 
                                            COALESCE(SUM(CASE WHEN ReturnDate IS NOT NULL AND Fine > 0 THEN Fine ELSE 0 END), 0) as Collected,
                                            COALESCE(SUM(CASE WHEN ReturnDate IS NULL AND DueDate < NOW() AND Fine > 0 THEN Fine ELSE 0 END), 0) as Pending,
                                            0 as Waived
                                            FROM Transactions";
                            using (MySqlCommand cmd = new MySqlCommand(query, conn))
                            {
                                using (MySqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        summary["Collected"] = reader.GetDecimal("Collected");
                                        summary["Pending"] = reader.GetDecimal("Pending");
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading financial summary: {ex.Message}");
            }
            return summary;
        }

        private Dictionary<string, object> GetLibraryUsageStatistics()
        {
            Dictionary<string, object> stats = new Dictionary<string, object>
            {
                ["DailyVisits"] = 0,
                ["BooksPerMember"] = 0.0,
                ["AvgPeriod"] = 0,
                ["Turnover"] = 0
            };
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT 
                                    (SELECT COUNT(DISTINCT DATE(BorrowDate)) FROM Transactions WHERE BorrowDate >= DATE_SUB(NOW(), INTERVAL 30 DAY)) * 30 / 30 as DailyVisits,
                                    (SELECT COUNT(*) FROM Transactions) / GREATEST((SELECT COUNT(*) FROM Members), 1) as BooksPerMember,
                                    (SELECT AVG(DATEDIFF(COALESCE(ReturnDate, NOW()), BorrowDate)) FROM Transactions WHERE ReturnDate IS NOT NULL) as AvgPeriod,
                                    (SELECT (COUNT(*) * 100.0 / GREATEST((SELECT COUNT(*) FROM Books), 1)) FROM Transactions WHERE BorrowDate >= DATE_SUB(NOW(), INTERVAL 30 DAY)) as Turnover";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                stats["DailyVisits"] = reader["DailyVisits"] != DBNull.Value ? Convert.ToInt32(reader["DailyVisits"]) : 0;
                                stats["BooksPerMember"] = reader["BooksPerMember"] != DBNull.Value ? Convert.ToDouble(reader["BooksPerMember"]) : 0.0;
                                stats["AvgPeriod"] = reader["AvgPeriod"] != DBNull.Value ? Convert.ToInt32(reader["AvgPeriod"]) : 0;
                                stats["Turnover"] = reader["Turnover"] != DBNull.Value ? Convert.ToInt32(reader["Turnover"]) : 0;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading usage statistics: {ex.Message}");
                MessageBox.Show($"Error loading usage statistics: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return stats;
        }

        private void LoadNewMemberRegistrations()
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    // Check if MemberType or Type column exists
                    bool hasMemberType = DatabaseSchemaHelper.CheckColumnExists(conn, "Members", "MemberType");
                    bool hasType = DatabaseSchemaHelper.CheckColumnExists(conn, "Members", "Type");
                    
                    string typeColumn;
                    if (hasMemberType)
                    {
                        typeColumn = "MemberType";
                    }
                    else if (hasType)
                    {
                        typeColumn = "Type";
                    }
                    else
                    {
                        typeColumn = "'N/A'";
                    }
                    
                    string query = $@"SELECT 
                                    MemberID as 'MEMBER ID',
                                    CONCAT(FirstName, ' ', LastName) as 'NAME',
                                    {typeColumn} as 'MEMBER TYPE',
                                    DATE(RegistrationDate) as 'REGISTRATION DATE'
                                    FROM Members
                                    WHERE RegistrationDate >= @StartDate AND RegistrationDate <= @EndDate
                                    ORDER BY RegistrationDate DESC
                                    LIMIT 20";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dataGridViewReports.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading new member registrations: {ex.Message}");
                MessageBox.Show($"Error loading new member registrations: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadMostBorrowedBooks()
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    // MySQL doesn't support ROW_NUMBER() in older versions, use alternative
                    string query = @"SELECT 
                                    @row_number := @row_number + 1 as '#',
                                    b.Title as 'TITLE',
                                    b.Author as 'AUTHOR',
                                    COUNT(*) as 'TIMES BORROWED'
                                    FROM Transactions t
                                    INNER JOIN Books b ON t.BookID = b.BookID
                                    CROSS JOIN (SELECT @row_number := 0) r
                                    WHERE t.BorrowDate >= @StartDate AND t.BorrowDate <= @EndDate
                                    GROUP BY b.BookID, b.Title, b.Author
                                    ORDER BY COUNT(*) DESC
                                    LIMIT 10";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dataGridViewReports.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading most borrowed books: {ex.Message}");
                // Try simpler query without ROW_NUMBER
                try
                {
                    using (var conn = _dbContext.GetConnection())
                    {
                        conn.Open();
                        string simpleQuery = @"SELECT 
                                            b.Title as 'TITLE',
                                            b.Author as 'AUTHOR',
                                            COUNT(*) as 'TIMES BORROWED'
                                            FROM Transactions t
                                            INNER JOIN Books b ON t.BookID = b.BookID
                                            WHERE t.BorrowDate >= @StartDate AND t.BorrowDate <= @EndDate
                                            GROUP BY b.BookID, b.Title, b.Author
                                            ORDER BY COUNT(*) DESC
                                            LIMIT 10";
                        using (MySqlCommand cmd = new MySqlCommand(simpleQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@StartDate", startDate);
                            cmd.Parameters.AddWithValue("@EndDate", endDate);
                            using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                adapter.Fill(dt);
                                dataGridViewReports.DataSource = dt;
                            }
                        }
                    }
                }
                catch (Exception ex2)
                {
                    MessageBox.Show($"Error loading most borrowed books: {ex2.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void LoadOverdueBooksReport()
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT 
                                    m.MemberID as 'MEMBER ID',
                                    CONCAT(m.FirstName, ' ', m.LastName) as 'MEMBER NAME',
                                    COUNT(*) as 'OVERDUE BOOKS',
                                    MAX(DATEDIFF(NOW(), t.DueDate)) as 'DAYS OVERDUE',
                                    COALESCE(SUM(t.Fine), 0) as 'FINE AMOUNT'
                                    FROM Transactions t
                                    INNER JOIN Members m ON t.MemberID = m.MemberID
                                    WHERE t.Status = 'Borrowed' 
                                    AND t.ReturnDate IS NULL
                                    AND t.DueDate < NOW()
                                    GROUP BY m.MemberID, m.FirstName, m.LastName
                                    ORDER BY MAX(DATEDIFF(NOW(), t.DueDate)) DESC";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dataGridViewReports.DataSource = dt;
                            if (dataGridViewReports.Columns.Contains("FINE AMOUNT"))
                            {
                                dataGridViewReports.Columns["FINE AMOUNT"].DefaultCellStyle.Format = "C2";
                                dataGridViewReports.Columns["FINE AMOUNT"].DefaultCellStyle.ForeColor = Color.Red;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading overdue books report: {ex.Message}");
                MessageBox.Show($"Error loading overdue books report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DrawLineChart(Graphics g, Panel panel, string title, Dictionary<DateTime, int> data1, Dictionary<DateTime, int> data2)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(panel.BackColor);

            int padding = 60;
            int chartWidth = panel.Width - (padding * 2);
            int chartHeight = panel.Height - (padding * 2) - 60;
            int startX = padding;
            int startY = padding;
            int endX = panel.Width - padding;
            int endY = panel.Height - padding - 60;

            using (Font titleFont = new Font("Segoe UI", 14F, FontStyle.Bold))
            {
                g.DrawString(title, titleFont, Brushes.Black, startX, 10);
            }

            var allDates = data1.Keys.Union(data2.Keys).OrderBy(d => d).ToList();
            if (allDates.Count == 0) return;

            int maxValue = Math.Max(
                data1.Values.DefaultIfEmpty(0).Max(),
                data2.Values.DefaultIfEmpty(0).Max()
            );
            maxValue = Math.Max(maxValue, 1);

            g.DrawLine(Pens.LightGray, startX, startY, startX, endY);
            g.DrawLine(Pens.LightGray, startX, endY, endX, endY);

            int gridLines = 5;
            for (int i = 0; i <= gridLines; i++)
            {
                int y = startY + (int)((endY - startY) * (1 - (double)i / gridLines));
                g.DrawLine(new Pen(Color.LightGray, 1) { DashStyle = DashStyle.Dash }, startX, y, endX, y);
                int value = (int)(maxValue * i / gridLines);
                g.DrawString(value.ToString(), new Font("Segoe UI", 9F), Brushes.Gray, 5, y - 10);
            }

            if (allDates.Count > 1)
            {
                float stepX = (float)chartWidth / (allDates.Count - 1);
                PointF[] points1 = new PointF[allDates.Count];
                PointF[] points2 = new PointF[allDates.Count];

                for (int i = 0; i < allDates.Count; i++)
                {
                    float x = startX + i * stepX;
                    float y1 = endY - (data1.ContainsKey(allDates[i]) ? (float)data1[allDates[i]] / maxValue * chartHeight : 0);
                    float y2 = endY - (data2.ContainsKey(allDates[i]) ? (float)data2[allDates[i]] / maxValue * chartHeight : 0);
                    points1[i] = new PointF(x, y1);
                    points2[i] = new PointF(x, y2);

                    string dateLabel = allDates[i].ToString("MM-dd");
                    g.DrawString(dateLabel, new Font("Segoe UI", 8F), Brushes.Gray, x - 15, endY + 5);
                }

                if (points1.Length > 1)
                {
                    g.DrawLines(new Pen(Color.FromArgb(13, 110, 253), 2), points1);
                    g.DrawLines(new Pen(Color.FromArgb(40, 167, 69), 2), points2);

                    foreach (var point in points1)
                    {
                        g.FillEllipse(Brushes.Blue, point.X - 4, point.Y - 4, 8, 8);
                    }
                    foreach (var point in points2)
                    {
                        g.FillEllipse(Brushes.Green, point.X - 4, point.Y - 4, 8, 8);
                    }
                }
            }

            int legendY = panel.Height - 50;
            g.FillEllipse(Brushes.Blue, startX, legendY, 10, 10);
            g.DrawString("Borrowed", new Font("Segoe UI", 9F), Brushes.Black, startX + 15, legendY - 2);
            g.FillEllipse(Brushes.Green, startX + 100, legendY, 10, 10);
            g.DrawString("Returned", new Font("Segoe UI", 9F), Brushes.Black, startX + 115, legendY - 2);
        }

        private void DrawBarChart(Graphics g, Panel panel, string title, Dictionary<string, int> data)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(panel.BackColor);

            int padding = 60;
            int chartWidth = panel.Width - (padding * 2);
            int chartHeight = panel.Height - (padding * 2) - 60;
            int startX = padding;
            int startY = padding;
            int endX = panel.Width - padding;
            int endY = panel.Height - padding - 60;

            using (Font titleFont = new Font("Segoe UI", 14F, FontStyle.Bold))
            {
                g.DrawString(title, titleFont, Brushes.Black, startX, 10);
            }

            if (data.Count == 0) return;

            int maxValue = data.Values.DefaultIfEmpty(0).Max();
            maxValue = Math.Max(maxValue, 1);

            g.DrawLine(Pens.LightGray, startX, startY, startX, endY);
            g.DrawLine(Pens.LightGray, startX, endY, endX, endY);

            int gridLines = 5;
            for (int i = 0; i <= gridLines; i++)
            {
                int y = startY + (int)((endY - startY) * (1 - (double)i / gridLines));
                g.DrawLine(new Pen(Color.LightGray, 1) { DashStyle = DashStyle.Dash }, startX, y, endX, y);
                int value = (int)(maxValue * i / gridLines);
                g.DrawString(value.ToString(), new Font("Segoe UI", 9F), Brushes.Gray, 5, y - 10);
            }

            var items = data.OrderByDescending(x => x.Value).ToList();
            float barWidth = (float)chartWidth / items.Count - 20;
            float stepX = (float)chartWidth / items.Count;
            Color barColor = title.Contains("Category") ? Color.FromArgb(13, 110, 253) : Color.FromArgb(128, 0, 128);

            for (int i = 0; i < items.Count; i++)
            {
                float x = startX + i * stepX + 10;
                float barHeight = (float)items[i].Value / maxValue * chartHeight;
                float y = endY - barHeight;

                g.FillRectangle(new SolidBrush(barColor), x, y, barWidth, barHeight);
                g.DrawRectangle(Pens.DarkBlue, x, y, barWidth, barHeight);

                g.DrawString(items[i].Value.ToString(), new Font("Segoe UI", 9F), Brushes.Black, x + barWidth / 2 - 10, y - 20);

                string label = items[i].Key.Length > 10 ? items[i].Key.Substring(0, 10) + "..." : items[i].Key;
                g.DrawString(label, new Font("Segoe UI", 9F), Brushes.Black, x, endY + 5);
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

        private bool CheckColumnExists(MySqlConnection conn, string tableName, string columnName)
        {
            string query = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS 
                           WHERE TABLE_SCHEMA = DATABASE() 
                           AND TABLE_NAME = @TableName 
                           AND COLUMN_NAME = @ColumnName";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TableName", tableName);
                cmd.Parameters.AddWithValue("@ColumnName", columnName);
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        private void btnExportReport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
                FileName = $"{currentReportType}Report_{DateTime.Now:yyyyMMdd}.csv"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (dataGridViewReports != null && dataGridViewReports.DataSource != null)
                    {
                        DataTable dt = (DataTable)dataGridViewReports.DataSource;
                        System.IO.StreamWriter sw = new System.IO.StreamWriter(saveDialog.FileName, false, System.Text.Encoding.UTF8);

                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            sw.Write(dt.Columns[i].ColumnName);
                            if (i < dt.Columns.Count - 1) sw.Write(",");
                        }
                        sw.WriteLine();

                        foreach (DataRow row in dt.Rows)
                        {
                            for (int i = 0; i < dt.Columns.Count; i++)
                            {
                                sw.Write(row[i].ToString());
                                if (i < dt.Columns.Count - 1) sw.Write(",");
                            }
                            sw.WriteLine();
                        }

                        sw.Close();
                        MessageBox.Show("Report exported successfully!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No data available to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error exporting report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void lblSubtitle_Click(object sender, EventArgs e)
        {

        }
    }
}
