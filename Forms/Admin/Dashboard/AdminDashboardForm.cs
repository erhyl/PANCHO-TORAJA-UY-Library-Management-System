using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Data;
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
                var borrowedData = _dashboardService.GetWeeklyBorrowData();
                var returnedData = _dashboardService.GetWeeklyReturnData();
                string[] days = { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
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
                maxValue = Math.Max(maxValue, 10);
                int step = maxValue / 4;
                if (step == 0) step = 1;
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
        private void panelCharts_Paint(object sender, PaintEventArgs e)
        {
        }
        private void DrawLineChart(Graphics g, Rectangle rect, Dictionary<string, int> borrowData, Dictionary<string, int> returnData, string title)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);
            try
            {
                if (borrowData.Count == 0 && returnData.Count == 0) return;
                int padding = 60;
                int chartWidth = rect.Width - padding * 2;
                int chartHeight = rect.Height - padding * 2;
                int startX = padding;
                int startY = padding;
                int endY = startY + chartHeight;
                g.DrawLine(Pens.Gray, startX, endY, startX + chartWidth, endY);
                g.DrawLine(Pens.Gray, startX, startY, startX, endY);
                var allMonths = new HashSet<string>();
                foreach (var key in borrowData.Keys) allMonths.Add(key);
                foreach (var key in returnData.Keys) allMonths.Add(key);
                var sortedMonths = allMonths.OrderBy(m => m).ToList();
                if (sortedMonths.Count == 0) return;
                int maxBorrowed = borrowData.Values.Count > 0 ? borrowData.Values.Max() : 0;
                int maxReturned = returnData.Values.Count > 0 ? returnData.Values.Max() : 0;
                int maxValue = Math.Max(maxBorrowed, maxReturned);
                maxValue = Math.Max(maxValue, 10);
                int step = maxValue / 4;
                if (step == 0) step = 1;
                for (int i = 0; i <= 4; i++)
                {
                    int value = i * step;
                    int y = endY - (value * chartHeight / maxValue);
                    g.DrawString(value.ToString(), new Font("Segoe UI", 9F), Brushes.Gray, startX - 40, y - 10);
                    g.DrawLine(new Pen(Color.LightGray, 1), startX, y, startX + chartWidth, y);
                }
                int pointSpacing = chartWidth / Math.Max(sortedMonths.Count - 1, 1);
                PointF[] borrowPoints = new PointF[sortedMonths.Count];
                PointF[] returnPoints = new PointF[sortedMonths.Count];
                for (int i = 0; i < sortedMonths.Count; i++)
                {
                    int x = startX + i * pointSpacing;
                    int borrowValue = borrowData.ContainsKey(sortedMonths[i]) ? borrowData[sortedMonths[i]] : 0;
                    int returnValue = returnData.ContainsKey(sortedMonths[i]) ? returnData[sortedMonths[i]] : 0;
                    int borrowY = endY - (int)((double)borrowValue / maxValue * chartHeight);
                    int returnY = endY - (int)((double)returnValue / maxValue * chartHeight);
                    borrowPoints[i] = new PointF(x, borrowY);
                    returnPoints[i] = new PointF(x, returnY);
                    g.FillEllipse(new SolidBrush(Color.FromArgb(0, 123, 255)), x - 3, borrowY - 3, 6, 6);
                    g.FillEllipse(new SolidBrush(Color.FromArgb(40, 167, 69)), x - 3, returnY - 3, 6, 6);
                    g.DrawString(sortedMonths[i].Substring(0, 3), new Font("Segoe UI", 8F), Brushes.Black, x - 10, endY + 5);
                }
                if (borrowPoints.Length > 1)
                {
                    g.DrawLines(new Pen(Color.FromArgb(0, 123, 255), 2), borrowPoints);
                }
                if (returnPoints.Length > 1)
                {
                    g.DrawLines(new Pen(Color.FromArgb(40, 167, 69), 2), returnPoints);
                }
                int legendY = startY + 20;
                g.FillRectangle(new SolidBrush(Color.FromArgb(0, 123, 255)), startX + chartWidth - 150, legendY, 15, 15);
                g.DrawString("Borrowed", new Font("Segoe UI", 9F), Brushes.Black, startX + chartWidth - 130, legendY);
                g.FillRectangle(new SolidBrush(Color.FromArgb(40, 167, 69)), startX + chartWidth - 150, legendY + 20, 15, 15);
                g.DrawString("Returned", new Font("Segoe UI", 9F), Brushes.Black, startX + chartWidth - 130, legendY + 20);
            }
            catch (Exception ex)
            {
                g.DrawString($"Error loading chart: {ex.Message}", new Font("Segoe UI", 10F), Brushes.Red, 20, 20);
            }
        }
        private void DrawAreaChart(Graphics g, Rectangle rect, Dictionary<string, int> data, Color areaColor, string title)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);
            try
            {
                if (data.Count == 0) return;
                int padding = 60;
                int chartWidth = rect.Width - padding * 2;
                int chartHeight = rect.Height - padding * 2;
                int startX = padding;
                int startY = padding;
                int endY = startY + chartHeight;
                g.DrawLine(Pens.Gray, startX, endY, startX + chartWidth, endY);
                g.DrawLine(Pens.Gray, startX, startY, startX, endY);
                var sortedKeys = data.Keys.OrderBy(k => k).ToList();
                if (sortedKeys.Count == 0) return;
                int maxValue = data.Values.Max();
                maxValue = Math.Max(maxValue, 10);
                int pointSpacing = chartWidth / Math.Max(sortedKeys.Count - 1, 1);
                PointF[] points = new PointF[sortedKeys.Count + 2];
                points[0] = new PointF(startX, endY);
                for (int i = 0; i < sortedKeys.Count; i++)
                {
                    int x = startX + i * pointSpacing;
                    int value = data[sortedKeys[i]];
                    int y = endY - (int)((double)value / maxValue * chartHeight);
                    points[i + 1] = new PointF(x, y);
                    g.FillEllipse(new SolidBrush(areaColor), x - 3, y - 3, 6, 6);
                    g.DrawString(sortedKeys[i].Substring(0, 3), new Font("Segoe UI", 8F), Brushes.Black, x - 10, endY + 5);
                }
                points[sortedKeys.Count + 1] = new PointF(startX + chartWidth, endY);
                using (var brush = new SolidBrush(Color.FromArgb(100, areaColor.R, areaColor.G, areaColor.B)))
                {
                    g.FillPolygon(brush, points);
                }
                if (points.Length > 2)
                {
                    g.DrawLines(new Pen(areaColor, 2), points.Skip(1).Take(sortedKeys.Count).ToArray());
                }
            }
            catch (Exception ex)
            {
                g.DrawString($"Error loading chart: {ex.Message}", new Font("Segoe UI", 10F), Brushes.Red, 20, 20);
            }
        }
    }
}