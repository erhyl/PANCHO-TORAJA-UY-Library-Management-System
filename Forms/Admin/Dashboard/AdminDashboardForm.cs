using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Project5LMS.Helpers;
using Project5LMS.Services;
using Project5LMS.Data;
using Project5LMS.Interfaces;
using Project5LMS.Models;
using Project5LMS.Forms.Admin.UserManagement;
using Project5LMS.Forms.Admin.Settings;
using Project5LMS.Forms.Admin.Reports;
using Project5LMS.Forms.Admin.Members;
using Project5LMS.Forms.Admin.Circulation;
using Project5LMS.Forms.Admin.Catalog;
using Project5LMS.Forms.Admin.Reservations;
using Project5LMS.Forms.Admin.Fines;
using Project5LMS.Forms.Admin.Inventory;
using Project5LMS.Forms.Admin.Search;

namespace Project5LMS.Forms.Admin.Dashboard
{
    public partial class AdminDashboardForm : Form
    {
        private readonly IDashboardService _dashboardService;
        private readonly IFinesService _finesService;
        private readonly DatabaseContext _dbContext;

        public AdminDashboardForm()
        {
            InitializeComponent();
            _dbContext = ServiceFactory.GetDbContext();

            try
            {
                AccessControlHelper.RequireRole("Admin");
                AuditLogger.LogAccessControl("AdminDashboardForm accessed", $"User: {CurrentUser.Email}", "Success");
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                AuditLogger.LogAccessControl("AdminDashboardForm access denied", $"User: {CurrentUser.Email}", "Failed");
                this.Close();
                return;
            }

            _dashboardService = ServiceFactory.CreateDashboardService();
            _finesService = ServiceFactory.CreateFinesService();
        }

        private void AdminDashboardForm_Load(object sender, EventArgs e)
        {
            LoadDashboard();
            SetActiveButton(btnDashboard);
        }

        private void LoadFormInPanel(Form form)
        {
            panelDashboardContainer.Visible = false;
            panelMainContent.Controls.Clear();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            panelMainContent.Controls.Add(form);
            panelMainContent.Tag = form;
            form.Show();
        }

        private void SetActiveButton(Button activeButton)
        {

            btnDashboard.BackColor = Color.Transparent;
            btnUserManagement.BackColor = Color.Transparent;
            btnMembers.BackColor = Color.Transparent;
            btnCatalog.BackColor = Color.Transparent;
            btnCirculation.BackColor = Color.Transparent;
            btnReservations.BackColor = Color.Transparent;
            btnFines.BackColor = Color.Transparent;
            btnInventory.BackColor = Color.Transparent;
            btnReports.BackColor = Color.Transparent;
            btnSearch.BackColor = Color.Transparent;
            btnSettings.BackColor = Color.Transparent;

            activeButton.BackColor = Color.FromArgb(178, 34, 34);
        }

        private void LoadDashboard()
        {
            panelMainContent.Controls.Clear();
            panelDashboardContainer.Visible = true;
            panelMainContent.Controls.Add(panelDashboardContainer);
            LoadMetrics();
            LoadRecentActivities();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnDashboard);
            LoadDashboard();
        }

        private void btnUserManagement_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnUserManagement);
            LoadFormInPanel(new UserManagementForm());
        }

        private void btnMembers_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnMembers);
            LoadFormInPanel(new AdminMembersForm());
        }

        private void btnCatalog_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnCatalog);
            LoadFormInPanel(new AdminCatalogForm());
        }

        private void btnCirculation_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnCirculation);
            LoadFormInPanel(new AdminCirculationForm());
        }

        private void btnReservations_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnReservations);
            LoadFormInPanel(new AdminReservationsForm());
        }

        private void btnFines_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnFines);
            LoadFormInPanel(new AdminFinesForm());
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnInventory);
            LoadFormInPanel(new AdminInventoryForm());
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnReports);
            LoadFormInPanel(new AdminReportsForm());
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnSearch);
            LoadFormInPanel(new AdminSearchForm());
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnSettings);
            LoadFormInPanel(new AdminSettingsForm());
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Project5LMS.LoginForm login = new Project5LMS.LoginForm();
            login.Show();
        }

        private void LoadMetrics()
        {
            try
            {
                int totalBooks = _dashboardService.GetTotalBooks();
                lblBooksValue.Text = totalBooks.ToString("N0");
                int booksThisMonth = 0;
                lblBooksChange.Text = $"+{booksThisMonth} this month";

                int activeMembers = _dashboardService.GetActiveMembers();
                lblMembersValue.Text = activeMembers.ToString("N0");
                int membersThisWeek = _dashboardService.GetMembersAddedThisWeek();
                lblMembersChange.Text = $"+{membersThisWeek} this week";

                int booksBorrowed = _dashboardService.GetActiveBorrowings();
                lblBorrowedValue.Text = booksBorrowed.ToString("N0");
                int borrowedToday = _dashboardService.GetBorrowedToday();
                lblBorrowedChange.Text = $"+{borrowedToday} today";

                int overdueBooks = _dashboardService.GetOverdueBooks();
                lblOverdueValue.Text = overdueBooks.ToString();
                int overdueLastWeek = 0;
                int overdueChange = overdueBooks - overdueLastWeek;
                lblOverdueChange.Text = overdueChange >= 0 ? $"+{overdueChange} from last week" : $"{overdueChange} from last week";

                decimal pendingFines = _dashboardService.GetPendingFines();
                lblFinesValue.Text = $"${pendingFines:N2}";
                decimal finesCollectedToday = _dashboardService.GetFinesCollectedToday();
                lblFinesChange.Text = $"${finesCollectedToday:N2} collected today";

                int totalReservations = _dashboardService.GetTotalReservations();
                lblReservationsValue.Text = totalReservations.ToString();
                int pendingReservations = _dashboardService.GetPendingReservations();
                lblReservationsChange.Text = $"{pendingReservations} pending";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading metrics: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadRecentActivities()
        {
            try
            {
                panelActivitiesList.Controls.Clear();

                var activities = _dashboardService.GetRecentActivities(4);

                int yPos = 0;
                foreach (var activity in activities)
                {
                    var activityItem = new ActivityItem
                    {
                        Type = activity.Type,
                        Details = activity.Details,
                        Timestamp = activity.Timestamp
                    };
                    Panel activityPanel = CreateActivityPanel(activityItem, yPos);
                    panelActivitiesList.Controls.Add(activityPanel);
                    yPos += 70;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading recent activities: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Panel CreateActivityPanel(ActivityItem activity, int yPos)
        {
            Panel panel = new Panel
            {
                Location = new Point(0, yPos),
                Size = new Size(1200, 60),
                BackColor = Color.White
            };

            Label lblType = new Label
            {
                Text = activity.Type,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                Location = new Point(0, 5),
                Size = new Size(1200, 20),
                AutoSize = false
            };

            Label lblDetails = new Label
            {
                Text = activity.Details,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(128, 128, 128),
                Location = new Point(0, 25),
                Size = new Size(1000, 20),
                AutoSize = false
            };

            string timeAgo = GetTimeAgo(activity.Timestamp);
            Label lblTime = new Label
            {
                Text = timeAgo,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(128, 128, 128),
                Location = new Point(1090, 25),
                Size = new Size(110, 20),
                TextAlign = ContentAlignment.TopRight
            };

            panel.Controls.Add(lblType);
            panel.Controls.Add(lblDetails);
            panel.Controls.Add(lblTime);

            return panel;
        }

        private string GetTimeAgo(DateTime timestamp)
        {
            TimeSpan timeSpan = DateTime.Now - timestamp;
            if (timeSpan.TotalMinutes < 1)
                return "Just now";
            if (timeSpan.TotalMinutes < 60)
                return $"{(int)timeSpan.TotalMinutes} min ago";
            if (timeSpan.TotalHours < 24)
                return $"{(int)timeSpan.TotalHours} hour{(timeSpan.TotalHours >= 2 ? "s" : "")} ago";
            return $"{(int)timeSpan.TotalDays} day{(timeSpan.TotalDays >= 2 ? "s" : "")} ago";
        }

        private void panelBarChart_Paint(object sender, PaintEventArgs e)
        {
            DrawBarChart(e.Graphics, panelBarChart.ClientRectangle);
        }

        private void panelPieChart_Paint(object sender, PaintEventArgs e)
        {
            DrawPieChart(e.Graphics, panelPieChart.ClientRectangle);
        }

        private void DrawBarChart(Graphics g, Rectangle rect)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);

            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();

                    Dictionary<string, int> borrowedData = new Dictionary<string, int>();
                    Dictionary<string, int> returnedData = new Dictionary<string, int>();

                    string[] days = { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
                    foreach (string day in days)
                    {
                        borrowedData[day] = 0;
                        returnedData[day] = 0;
                    }

                    string borrowQuery = @"SELECT DAYNAME(BorrowDate) as day_name, COUNT(*) as count
                                         FROM Transactions
                                         WHERE BorrowDate >= DATE_SUB(CURDATE(), INTERVAL DAYOFWEEK(CURDATE())-1 DAY)
                                         AND BorrowDate < DATE_ADD(DATE_SUB(CURDATE(), INTERVAL DAYOFWEEK(CURDATE())-1 DAY), INTERVAL 7 DAY)
                                         AND (Status = 'Borrowed' OR Status = 'Active')
                                         GROUP BY DAYNAME(BorrowDate)";
                    using (MySqlCommand cmd = new MySqlCommand(borrowQuery, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string dayName = reader["day_name"].ToString();
                            string shortDay = GetShortDayName(dayName);
                            if (borrowedData.ContainsKey(shortDay))
                                borrowedData[shortDay] = Convert.ToInt32(reader["count"]);
                        }
                    }

                    string returnQuery = @"SELECT DAYNAME(ReturnDate) as day_name, COUNT(*) as count
                                          FROM Transactions
                                          WHERE ReturnDate >= DATE_SUB(CURDATE(), INTERVAL DAYOFWEEK(CURDATE())-1 DAY)
                                          AND ReturnDate < DATE_ADD(DATE_SUB(CURDATE(), INTERVAL DAYOFWEEK(CURDATE())-1 DAY), INTERVAL 7 DAY)
                                          AND Status = 'Returned' AND ReturnDate IS NOT NULL
                                          GROUP BY DAYNAME(ReturnDate)";
                    using (MySqlCommand cmd = new MySqlCommand(returnQuery, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string dayName = reader["day_name"].ToString();
                            string shortDay = GetShortDayName(dayName);
                            if (returnedData.ContainsKey(shortDay))
                                returnedData[shortDay] = Convert.ToInt32(reader["count"]);
                        }
                    }

                    int padding = 60;
                    int chartWidth = rect.Width - padding * 2;
                    int chartHeight = rect.Height - padding * 2;
                    int startX = padding;
                    int startY = padding;
                    int endY = startY + chartHeight;

                    g.DrawLine(Pens.Gray, startX, endY, startX + chartWidth, endY);
                    g.DrawLine(Pens.Gray, startX, startY, startX, endY);

                    int maxBorrowed = borrowedData.Values.Count > 0 ? borrowedData.Values.Max() : 0;
                    int maxReturned = returnedData.Values.Count > 0 ? returnedData.Values.Max() : 0;
                    int maxValue = Math.Max(maxBorrowed, maxReturned);
                    maxValue = Math.Max(maxValue, 240);
                    int step = maxValue / 4;
                    for (int i = 0; i <= 4; i++)
                    {
                        int value = i * step;
                        int y = endY - (value * chartHeight / maxValue);
                        g.DrawString(value.ToString(), new Font("Segoe UI", 9F), Brushes.Gray, startX - 40, y - 10);
                        g.DrawLine(new Pen(Color.LightGray, 1), startX, y, startX + chartWidth, y);
                    }

                    int barWidth = chartWidth / (days.Length * 3);
                    int spacing = barWidth;
                    int xPos = startX + spacing;

                    foreach (string day in days)
                    {
                        int borrowed = borrowedData.ContainsKey(day) ? borrowedData[day] : 0;
                        int returned = returnedData.ContainsKey(day) ? returnedData[day] : 0;

                        int borrowedHeight = (int)((double)borrowed / maxValue * chartHeight);
                        Rectangle borrowedRect = new Rectangle(xPos, endY - borrowedHeight, barWidth, borrowedHeight);
                        g.FillRectangle(new SolidBrush(Color.FromArgb(0, 123, 255)), borrowedRect);

                        int returnedHeight = (int)((double)returned / maxValue * chartHeight);
                        Rectangle returnedRect = new Rectangle(xPos + barWidth, endY - returnedHeight, barWidth, returnedHeight);
                        g.FillRectangle(new SolidBrush(Color.FromArgb(40, 167, 69)), returnedRect);

                        g.DrawString(day, new Font("Segoe UI", 9F), Brushes.Black, xPos + barWidth / 2 - 10, endY + 5);

                        xPos += barWidth * 2 + spacing;
                    }

                    int legendY = startY + 20;
                    g.FillRectangle(new SolidBrush(Color.FromArgb(0, 123, 255)), startX + chartWidth - 150, legendY, 15, 15);
                    g.DrawString("Borrowed", new Font("Segoe UI", 9F), Brushes.Black, startX + chartWidth - 130, legendY);

                    g.FillRectangle(new SolidBrush(Color.FromArgb(40, 167, 69)), startX + chartWidth - 150, legendY + 20, 15, 15);
                    g.DrawString("Returned", new Font("Segoe UI", 9F), Brushes.Black, startX + chartWidth - 130, legendY + 20);
                }
            }
            catch (Exception ex)
            {
                g.DrawString($"Error loading chart: {ex.Message}", new Font("Segoe UI", 10F), Brushes.Red, 20, 20);
            }
        }

        private void DrawPieChart(Graphics g, Rectangle rect)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);

            try
            {
                var categoryData = _dashboardService.GetCategoryDistribution();

                    if (categoryData.Count == 0) return;

                    int padding = 80;
                    int size = Math.Min(rect.Width, rect.Height) - padding * 2;
                    Rectangle pieRect = new Rectangle(rect.Width / 2 - size / 2, padding, size, size);

                    int total = categoryData.Values.Sum();
                    float startAngle = 0;

                    Color[] colors = new Color[]
                    {
                        Color.FromArgb(0, 123, 255),
                        Color.FromArgb(40, 167, 69),
                        Color.FromArgb(255, 152, 0),
                        Color.FromArgb(156, 39, 176),
                        Color.FromArgb(233, 30, 99),
                        Color.FromArgb(63, 81, 181)
                    };

                    int colorIndex = 0;
                    int legendY = padding + size + 20;
                    int legendX = rect.Width / 2 - 150;

                    foreach (var kvp in categoryData.Take(6))
                    {
                        float sweepAngle = (float)kvp.Value / total * 360;
                        Color color = colors[colorIndex % colors.Length];

                        g.FillPie(new SolidBrush(color), pieRect, startAngle, sweepAngle);
                        g.DrawPie(Pens.White, pieRect, startAngle, sweepAngle);

                        g.FillRectangle(new SolidBrush(color), legendX, legendY, 15, 15);
                        float percentage = (float)kvp.Value / total * 100;
                        g.DrawString($"{kvp.Key}: {percentage:F0}%", new Font("Segoe UI", 9F), Brushes.Black, legendX + 20, legendY);

                        startAngle += sweepAngle;
                        colorIndex++;
                        legendY += 20;
                    }
            }
            catch (Exception ex)
            {
                g.DrawString($"Error loading chart: {ex.Message}", new Font("Segoe UI", 10F), Brushes.Red, 20, 20);
            }
        }

        private string GetShortDayName(string fullDayName)
        {
            switch (fullDayName.ToLower())
            {
                case "monday": return "Mon";
                case "tuesday": return "Tue";
                case "wednesday": return "Wed";
                case "thursday": return "Thu";
                case "friday": return "Fri";
                case "saturday": return "Sat";
                case "sunday": return "Sun";
                default: return fullDayName.Substring(0, 3);
            }
        }

        private void panelRecentActivities_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int yPos = 60;
            for (int i = 0; i < panelActivitiesList.Controls.Count - 1; i++)
            {
                g.DrawLine(new Pen(Color.FromArgb(240, 240, 240), 1), 20, yPos, panelRecentActivities.Width - 40, yPos);
                yPos += 70;
            }
        }

        private class ActivityItem
        {
            public string Type { get; set; }
            public string Details { get; set; }
            public DateTime Timestamp { get; set; }
        }
    }
}
