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
            this.Resize += AdminDashboardForm_Resize;
            if (panelMetrics != null)
            {
                panelMetrics.Resize += PanelMetrics_Resize;
            }
            if (panelBarChart != null)
            {
                panelBarChart.Resize += panelBarChart_Resize;
            }
            if (panelPieChart != null)
            {
                panelPieChart.Resize += panelPieChart_Resize;
            }
        }
        private void AdminDashboardForm_Resize(object sender, EventArgs e)
        {
            try
            {
                if (panelDashboardContainer != null && panelDashboardContainer.Visible)
                {
                    ArrangeMetrics();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in form resize: {ex.Message}");
            }
        }
        private void PanelMetrics_Resize(object sender, EventArgs e)
        {
            try
            {
                ArrangeMetrics();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in metrics panel resize: {ex.Message}");
            }
        }
        private void AdminDashboardForm_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDashboard();
                SetActiveButton(btnDashboard);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading dashboard: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Dashboard load error: {ex}");
            }
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
            ArrangeMetrics();
            LoadMetrics();
            LoadRecentActivities();
        }
        private void ArrangeMetrics()
        {
            try
            {
                if (panelMetrics == null || panelMetrics.Width <= 0) return;
                if (panelMetricBooks != null && !panelMetrics.Controls.Contains(panelMetricBooks))
                {
                    panelMetrics.Controls.Add(panelMetricBooks);
                }
                if (panelMetricMembers != null && !panelMetrics.Controls.Contains(panelMetricMembers))
                {
                    panelMetrics.Controls.Add(panelMetricMembers);
                }
                if (panelMetricBorrowed != null && !panelMetrics.Controls.Contains(panelMetricBorrowed))
                {
                    panelMetrics.Controls.Add(panelMetricBorrowed);
                }
                if (panelMetricFines != null && !panelMetrics.Controls.Contains(panelMetricFines))
                {
                    panelMetrics.Controls.Add(panelMetricFines);
                }
                if (panelMetricOverdue != null && !panelMetrics.Controls.Contains(panelMetricOverdue))
                {
                    panelMetrics.Controls.Add(panelMetricOverdue);
                }
                if (panelMetricReservations != null && !panelMetrics.Controls.Contains(panelMetricReservations))
                {
                    panelMetrics.Controls.Add(panelMetricReservations);
                }
                int panelWidth = panelMetrics.Width;
                int panelHeight = 400;
                int spacing = 10;
                int cardWidth = Math.Max(300, (panelWidth - (spacing * 4)) / 3);
                int cardHeight = (panelHeight - spacing) / 2;
                int remainingWidth = panelWidth - (cardWidth * 2 + spacing * 2);
                if (panelMetricBooks != null)
                {
                    panelMetricBooks.Location = new Point(0, 0);
                    panelMetricBooks.Size = new Size(cardWidth, cardHeight);
                    panelMetricBooks.Visible = true;
                }
                if (panelMetricMembers != null)
                {
                    panelMetricMembers.Location = new Point(cardWidth + spacing, 0);
                    panelMetricMembers.Size = new Size(cardWidth, cardHeight);
                    panelMetricMembers.Visible = true;
                }
                if (panelMetricBorrowed != null)
                {
                    panelMetricBorrowed.Location = new Point((cardWidth + spacing) * 2, 0);
                    panelMetricBorrowed.Size = new Size(remainingWidth, cardHeight);
                    panelMetricBorrowed.Visible = true;
                }
                if (panelMetricFines != null)
                {
                    panelMetricFines.Location = new Point(0, cardHeight + spacing);
                    panelMetricFines.Size = new Size(cardWidth, cardHeight);
                    panelMetricFines.Visible = true;
                }
                if (panelMetricOverdue != null)
                {
                    panelMetricOverdue.Location = new Point(cardWidth + spacing, cardHeight + spacing);
                    panelMetricOverdue.Size = new Size(cardWidth, cardHeight);
                    panelMetricOverdue.Visible = true;
                }
                if (panelMetricReservations != null)
                {
                    panelMetricReservations.Location = new Point((cardWidth + spacing) * 2, cardHeight + spacing);
                    panelMetricReservations.Size = new Size(remainingWidth, cardHeight);
                    panelMetricReservations.Visible = true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error arranging metrics: {ex.Message}");
            }
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
                int booksThisMonth = _dashboardService.GetBooksAddedThisMonth();
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
        private void panelBarChart_Resize(object sender, EventArgs e)
        {
            if (panelBarChart != null)
            {
                panelBarChart.Invalidate();
            }
        }
        private void panelPieChart_Resize(object sender, EventArgs e)
        {
            if (panelPieChart != null)
            {
                panelPieChart.Invalidate();
            }
        }
        private void DrawBarChart(Graphics g, Rectangle rect)
        {
            if (rect.Width <= 0 || rect.Height <= 0) return;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);
            try
            {
                if (_dashboardService == null) return;
                var borrowedData = _dashboardService.GetWeeklyBorrowData();
                var returnedData = _dashboardService.GetWeeklyReturnData();
                if (borrowedData == null || returnedData == null) return;
                string[] days = { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
                int padding = 70;
                int chartWidth = Math.Max(100, rect.Width - padding * 2);
                int chartHeight = Math.Max(100, rect.Height - padding * 2 - 30);
                int startX = padding;
                int startY = padding;
                int endY = startY + chartHeight;
                using (Pen axisPen = new Pen(Color.FromArgb(200, 200, 200), 2))
                {
                    g.DrawLine(axisPen, startX, endY, startX + chartWidth, endY);
                    g.DrawLine(axisPen, startX, startY, startX, endY);
                }
                int maxBorrowed = borrowedData.Values != null && borrowedData.Values.Count > 0 ? borrowedData.Values.Max() : 0;
                int maxReturned = returnedData.Values != null && returnedData.Values.Count > 0 ? returnedData.Values.Max() : 0;
                int maxValue = Math.Max(maxBorrowed, maxReturned);
                // Ensure minimum scale for visibility, but allow dynamic scaling
                maxValue = Math.Max(maxValue, 1);
                // Use more granular steps for better readability
                int step = maxValue > 0 ? Math.Max(1, (int)Math.Ceiling((double)maxValue / 5)) : 1;
                if (step == 0) step = 1;
                using (Font axisFont = new Font("Segoe UI", 9F, FontStyle.Regular))
                using (Brush axisBrush = new SolidBrush(Color.FromArgb(100, 100, 100)))
                {
                    // Draw more grid lines for better readability (up to 6 lines)
                    int numGridLines = Math.Min(6, maxValue > 0 ? Math.Max(2, (int)Math.Ceiling((double)maxValue / step)) : 2);
                    for (int i = 0; i <= numGridLines; i++)
                    {
                        int value = i * step;
                        if (value > maxValue) value = maxValue;
                        int y = endY - (value * chartHeight / maxValue);
                        if (i > 0)
                        {
                            g.DrawLine(new Pen(Color.FromArgb(240, 240, 240), 1), startX, y, startX + chartWidth, y);
                        }
                        string valueText = value.ToString();
                        SizeF textSize = g.MeasureString(valueText, axisFont);
                        g.DrawString(valueText, axisFont, axisBrush, startX - textSize.Width - 8, y - textSize.Height / 2);
                    }
                    g.DrawString("Count", axisFont, axisBrush, 10, startY - 15);
                }
                int groupWidth = chartWidth / days.Length;
                int barWidth = (groupWidth - 20) / 2;
                int spacing = 10;
                int xPos = startX + (groupWidth - (barWidth * 2 + spacing)) / 2;
                Color borrowedColor = Color.FromArgb(0, 123, 255);
                Color returnedColor = Color.FromArgb(40, 167, 69);
                using (Font dayFont = new Font("Segoe UI", 10F, FontStyle.Regular))
                using (Font valueFont = new Font("Segoe UI", 8F, FontStyle.Bold))
                using (Brush dayBrush = new SolidBrush(Color.FromArgb(64, 64, 64)))
                {
                    foreach (string day in days)
                    {
                        int borrowed = borrowedData.ContainsKey(day) ? borrowedData[day] : 0;
                        int returned = returnedData.ContainsKey(day) ? returnedData[day] : 0;
                        int borrowedHeight = borrowed > 0 ? Math.Max(2, (int)((double)borrowed / maxValue * chartHeight)) : 0;
                        int returnedHeight = returned > 0 ? Math.Max(2, (int)((double)returned / maxValue * chartHeight)) : 0;
                        if (borrowed > 0)
                        {
                            Rectangle borrowedRect = new Rectangle(xPos, endY - borrowedHeight, barWidth, borrowedHeight);
                            using (GraphicsPath path = RoundedRectangle(borrowedRect, 3))
                            {
                                g.FillPath(new SolidBrush(borrowedColor), path);
                                g.DrawPath(new Pen(Color.FromArgb(0, 100, 200), 1), path);
                            }
                            if (borrowedHeight > 15)
                            {
                                string valueText = borrowed.ToString();
                                SizeF textSize = g.MeasureString(valueText, valueFont);
                                g.DrawString(valueText, valueFont, Brushes.White, xPos + (barWidth - textSize.Width) / 2, endY - borrowedHeight - textSize.Height - 2);
                            }
                        }
                        if (returned > 0)
                        {
                            Rectangle returnedRect = new Rectangle(xPos + barWidth + spacing, endY - returnedHeight, barWidth, returnedHeight);
                            using (GraphicsPath path = RoundedRectangle(returnedRect, 3))
                            {
                                g.FillPath(new SolidBrush(returnedColor), path);
                                g.DrawPath(new Pen(Color.FromArgb(30, 140, 60), 1), path);
                            }
                            if (returnedHeight > 15)
                            {
                                string valueText = returned.ToString();
                                SizeF textSize = g.MeasureString(valueText, valueFont);
                                g.DrawString(valueText, valueFont, Brushes.White, xPos + barWidth + spacing + (barWidth - textSize.Width) / 2, endY - returnedHeight - textSize.Height - 2);
                            }
                        }
                        SizeF daySize = g.MeasureString(day, dayFont);
                        g.DrawString(day, dayFont, dayBrush, xPos + (barWidth * 2 + spacing - daySize.Width) / 2, endY + 8);
                        xPos += groupWidth;
                    }
                }
                int legendY = startY + 10;
                int legendX = startX + chartWidth - 140;
                using (Font legendFont = new Font("Segoe UI", 9F, FontStyle.Regular))
                {
                    g.FillRectangle(new SolidBrush(borrowedColor), legendX, legendY, 16, 16);
                    g.DrawRectangle(new Pen(Color.FromArgb(0, 100, 200), 1), legendX, legendY, 16, 16);
                    g.DrawString("Borrowed", legendFont, Brushes.Black, legendX + 20, legendY + 2);
                    g.FillRectangle(new SolidBrush(returnedColor), legendX, legendY + 22, 16, 16);
                    g.DrawRectangle(new Pen(Color.FromArgb(30, 140, 60), 1), legendX, legendY + 22, 16, 16);
                    g.DrawString("Returned", legendFont, Brushes.Black, legendX + 20, legendY + 24);
                }
            }
            catch (Exception ex)
            {
                using (Font errorFont = new Font("Segoe UI", 10F))
                {
                    g.DrawString($"Error loading chart: {ex.Message}", errorFont, Brushes.Red, 20, 20);
                }
            }
        }
        private GraphicsPath RoundedRectangle(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return path;
        }
        private void DrawPieChart(Graphics g, Rectangle rect)
        {
            if (rect.Width <= 0 || rect.Height <= 0) return;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);
            try
            {
                if (_dashboardService == null) return;
                var categoryData = _dashboardService.GetCategoryDistribution();
                if (categoryData == null || categoryData.Count == 0)
                {
                    using (Font noDataFont = new Font("Segoe UI", 11F, FontStyle.Italic))
                    {
                        string noDataText = "No category data available";
                        SizeF textSize = g.MeasureString(noDataText, noDataFont);
                        g.DrawString(noDataText, noDataFont, new SolidBrush(Color.FromArgb(150, 150, 150)), 
                            rect.Width / 2 - textSize.Width / 2, rect.Height / 2 - textSize.Height / 2);
                    }
                    return;
                }
                int padding = 60;
                int titleHeight = 50;
                int size = Math.Min(rect.Width - padding * 2, rect.Height - padding * 2 - titleHeight - 100);
                int centerX = rect.Width / 2;
                int centerY = padding + titleHeight + size / 2;
                Rectangle pieRect = new Rectangle(centerX - size / 2, centerY - size / 2, size, size);
                int total = categoryData.Values.Sum();
                float startAngle = -90;
                Color[] colors = new Color[]
                {
                    Color.FromArgb(0, 123, 255),
                    Color.FromArgb(40, 167, 69),
                    Color.FromArgb(255, 152, 0),
                    Color.FromArgb(156, 39, 176),
                    Color.FromArgb(233, 30, 99),
                    Color.FromArgb(63, 81, 181),
                    Color.FromArgb(255, 87, 34),
                    Color.FromArgb(76, 175, 80)
                };
                int colorIndex = 0;
                using (Font legendFont = new Font("Segoe UI", 9F, FontStyle.Regular))
                using (Font percentageFont = new Font("Segoe UI", 10F, FontStyle.Bold))
                using (Font countFont = new Font("Segoe UI", 8F, FontStyle.Regular))
                {
                    int legendStartY = centerY + size / 2 + 30;
                    int legendX = Math.Max(20, centerX - 250);
                    int legendY = legendStartY;
                    // Show ALL categories in legend, not just top 8
                    int maxLegendItems = categoryData.Count;
                    int itemsPerColumn = (int)Math.Ceiling(maxLegendItems / 2.0);
                    int columnWidth = 280;
                    int currentColumn = 0;
                    int itemsInCurrentColumn = 0;
                    // Show all categories, sorted by value descending
                    foreach (var kvp in categoryData.OrderByDescending(x => x.Value))
                    {
                        float percentage = (float)kvp.Value / total * 100;
                        float sweepAngle = percentage / 100 * 360;
                        Color color = colors[colorIndex % colors.Length];
                        float midAngle = startAngle + sweepAngle / 2;
                        float explodeDistance = percentage > 5 ? 8 : 0;
                        float radian = (float)(midAngle * Math.PI / 180);
                        int offsetX = (int)(explodeDistance * Math.Cos(radian));
                        int offsetY = (int)(explodeDistance * Math.Sin(radian));
                        Rectangle explodedRect = new Rectangle(pieRect.X + offsetX, pieRect.Y + offsetY, pieRect.Width, pieRect.Height);
                        using (SolidBrush brush = new SolidBrush(color))
                        {
                            g.FillPie(brush, explodedRect, startAngle, sweepAngle);
                        }
                        using (Pen whitePen = new Pen(Color.White, 2))
                        {
                            g.DrawPie(whitePen, explodedRect, startAngle, sweepAngle);
                        }
                        // Only show on-chart labels for slices >= 10% to avoid overlapping
                        // All data is visible in the legend below
                        if (percentage >= 10)
                        {
                            float labelAngle = (float)(midAngle * Math.PI / 180);
                            // Position label further out to avoid overlap
                            int labelDistance = size / 2 + 35;
                            int labelX = centerX + offsetX + (int)(labelDistance * Math.Cos(labelAngle));
                            int labelY = centerY + offsetY + (int)(labelDistance * Math.Sin(labelAngle));
                            string percentText = $"{percentage:F0}%";
                            SizeF textSize = g.MeasureString(percentText, percentageFont);
                            // Draw background for better visibility
                            RectangleF textRect = new RectangleF(labelX - textSize.Width / 2 - 2, labelY - textSize.Height / 2 - 2, 
                                textSize.Width + 4, textSize.Height + 4);
                            g.FillRectangle(new SolidBrush(Color.FromArgb(240, 240, 240)), textRect);
                            g.DrawRectangle(new Pen(Color.FromArgb(200, 200, 200), 1), textRect.X, textRect.Y, textRect.Width, textRect.Height);
                            g.DrawString(percentText, percentageFont, new SolidBrush(Color.FromArgb(64, 64, 64)), 
                                labelX - textSize.Width / 2, labelY - textSize.Height / 2);
                        }
                        int currentLegendX = legendX + (currentColumn * columnWidth);
                        g.FillRectangle(new SolidBrush(color), currentLegendX, legendY, 16, 16);
                        g.DrawRectangle(new Pen(Color.FromArgb(200, 200, 200), 1), currentLegendX, legendY, 16, 16);
                        string legendText = $"{kvp.Key}: {percentage:F1}% ({kvp.Value})";
                        g.DrawString(legendText, legendFont, Brushes.Black, currentLegendX + 20, legendY + 1);
                        startAngle += sweepAngle;
                        colorIndex++;
                        itemsInCurrentColumn++;
                        if (itemsInCurrentColumn >= itemsPerColumn)
                        {
                            currentColumn++;
                            itemsInCurrentColumn = 0;
                            legendY = legendStartY;
                        }
                        else
                        {
                            legendY += 22;
                        }
                    }
                    if (total > 0)
                    {
                        string totalText = $"Total: {total} books";
                        SizeF totalSize = g.MeasureString(totalText, countFont);
                        g.DrawString(totalText, countFont, new SolidBrush(Color.FromArgb(100, 100, 100)), 
                            centerX - totalSize.Width / 2, centerY + size / 2 + 15);
                    }
                }
            }
            catch (Exception ex)
            {
                using (Font errorFont = new Font("Segoe UI", 10F))
                {
                    g.DrawString($"Error loading chart: {ex.Message}", errorFont, Brushes.Red, 20, 20);
                }
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