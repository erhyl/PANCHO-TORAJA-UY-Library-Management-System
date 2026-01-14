using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Project5LMS.Helpers;
using Project5LMS.Data;
using Project5LMS.Services;
using System.IO;
using ClosedXML.Excel;
namespace Project5LMS.Forms.Admin.Reports
{
    public partial class AdminReportsForm : Form
    {
        private readonly DatabaseContext _dbContext;
        private string currentReportType = "Circulation";
        private DateTime startDate = DateTime.MinValue;
        private DateTime endDate = DateTime.MaxValue;
        private DateTime memberStartDate = DateTime.MinValue;
        private DateTime memberEndDate = DateTime.MaxValue;
        private DateTime collectionStartDate = DateTime.MinValue;
        private DateTime collectionEndDate = DateTime.MaxValue;
        private DateTime financialStartDate = DateTime.MinValue;
        private DateTime financialEndDate = DateTime.MaxValue;
        private DateTime statisticalStartDate = DateTime.MinValue;
        private DateTime statisticalEndDate = DateTime.MaxValue;
        private DateTime circulationStartDate = DateTime.MinValue;
        private DateTime circulationEndDate = DateTime.MaxValue;
        private Panel panelChart;
        public AdminReportsForm()
        {
            InitializeComponent();
            _dbContext = ServiceFactory.GetDbContext();
        }
        private void AdminReportsForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Ensure all design-time controls are visible and properly initialized
                EnsureDesignTimeControlsVisible();
                LoadReportContent("Circulation");
            }
            catch (MySqlException ex)
            {
                ErrorHandler.ShowDatabaseError("loading reports", ex);
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError($"Error loading reports form: {ex.Message}", "Error", ex);
            }
        }
        
        private void EnsureDesignTimeControlsVisible()
        {
            MakeVisible(panelCirculationMainContainer, tableLayoutPanelCirculation, panelCirculationChartCard, lblCirculationChartTitle, panelCirculationMostBorrowedCard, lblCirculationMostBorrowedTitle, panelCirculationOverdueCard, lblCirculationOverdueTitle);
            MakeVisible(panelMemberMainContainer, tableLayoutPanelMember, panelMemberChartCard, lblMemberChartTitle, panelMemberTableCard, lblMemberTableTitle);
            MakeVisible(panelCollectionMainContainer, lblCollectionTitle, flowLayoutPanelCollection, panelCollectionCardTotal, panelCollectionCardAvailable, panelCollectionCardOnLoan);
            MakeVisible(panelFinancialMainContainer, lblFinancialTitle, flowLayoutPanelFinancial, panelFinancialCardCollected, panelFinancialCardPending, panelFinancialCardWaived);
            MakeVisible(panelStatisticalMainContainer, panelStatisticalContent, lblStatisticalTitle, flowLayoutPanelStatistical, panelStatisticalCardVisits, panelStatisticalCardBooksPerMember, panelStatisticalCardBorrowingPeriod, panelStatisticalCardTurnover);
        }
        
        private void MakeVisible(params Control[] controls)
        {
            foreach (var ctrl in controls)
            {
                if (ctrl != null)
                {
                    ctrl.Visible = true;
                    if (ctrl is Panel || ctrl is TableLayoutPanel || ctrl is FlowLayoutPanel)
                        ctrl.BringToFront();
                }
            }
        }
        private void tabControlReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Suspend layout for smooth transition
            this.SuspendLayout();
            
            try
            {
                // Hide all report panels first for smooth transition
                if (panelContentCirculation != null) panelContentCirculation.Visible = false;
                if (panelContentMember != null) panelContentMember.Visible = false;
                if (panelContentCollection != null) panelContentCollection.Visible = false;
                if (panelContentFinancial != null) panelContentFinancial.Visible = false;
                if (panelContentStatistical != null) panelContentStatistical.Visible = false;
                
                Application.DoEvents();
                
                switch (tabControlReports.SelectedIndex)
                {
                    case 0:
                        currentReportType = "Circulation";
                        break;
                    case 1:
                        currentReportType = "Member";
                        break;
                    case 2:
                        currentReportType = "Collection";
                        break;
                    case 3:
                        currentReportType = "Financial";
                        break;
                    case 4:
                        currentReportType = "Statistical";
                        break;
                }
                
                try
                {
                    LoadReportContent(currentReportType);
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show($"Database error loading report: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    System.Diagnostics.Debug.WriteLine($"tabControlReports_SelectedIndexChanged MySQL error: {ex}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    System.Diagnostics.Debug.WriteLine($"tabControlReports_SelectedIndexChanged error: {ex}");
                }
                
                // Show the selected panel
                Panel selectedPanel = GetCurrentPanel();
                if (selectedPanel != null)
                {
                    selectedPanel.Visible = true;
                    selectedPanel.BringToFront();
                }
            }
            finally
            {
                // Resume layout
                this.ResumeLayout(true);
                this.PerformLayout();
            }
        }
        private Panel GetCurrentPanel()
        {
            switch (currentReportType)
            {
                case "Circulation":
                    return panelContentCirculation;
                case "Member":
                    return panelContentMember;
                case "Collection":
                    return panelContentCollection;
                case "Financial":
                    return panelContentFinancial;
                case "Statistical":
                    return panelContentStatistical;
                default:
                    return panelContentCirculation;
            }
        }
        private void LoadReportContent(string reportType)
        {
            try
            {
                currentReportType = reportType;
                // Don't clear controls - preserve design-time controls
                // Individual Load methods will update existing controls or create them if needed
                
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
            catch (MySqlException ex)
            {
                MessageBox.Show($"Database error loading {reportType} report: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                System.Diagnostics.Debug.WriteLine($"LoadReportContent MySQL error for {reportType}: {ex}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading {reportType} report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                System.Diagnostics.Debug.WriteLine($"LoadReportContent error for {reportType}: {ex}");
            }
        }
        private void LoadCirculationReport()
        {
            InitializeDateRange(cmbCirculationDateRange, ref circulationStartDate, ref circulationEndDate, UpdateCirculationDateRange);
            EnsurePanelVisible(ref panelCirculationMainContainer, panelContentCirculation, "panelCirculationMainContainer");
            EnsureTableLayoutVisible(ref tableLayoutPanelCirculation, panelCirculationMainContainer, "tableLayoutPanelCirculation");
            LoadCirculationReportByType("Borrowing by Category", panelCirculationMainContainer);
            SetupDateRangeHandler(cmbCirculationDateRange, CmbCirculationDateRange_SelectedIndexChanged);
        }
        
        private void InitializeDateRange(ComboBox comboBox, ref DateTime startDate, ref DateTime endDate, Action updateAction)
        {
            if (comboBox != null && comboBox.SelectedIndex < 0)
                comboBox.SelectedIndex = 0;
            if (startDate == default(DateTime))
                updateAction();
        }
        
        private void EnsurePanelVisible(ref Panel panel, Control parent, string panelName)
        {
            if (panel == null)
                panel = FindControl<Panel>(parent, panelName);
            if (panel != null)
            {
                panel.Visible = true;
                panel.BringToFront();
                if (panelName.Contains("MainContainer"))
                    panel.AutoScroll = true;
            }
        }
        
        private void EnsureTableLayoutVisible(ref TableLayoutPanel tableLayout, Control parent, string layoutName)
        {
            if (tableLayout == null)
                tableLayout = FindControl<TableLayoutPanel>(parent, layoutName);
            if (tableLayout != null)
            {
                tableLayout.Visible = true;
                tableLayout.BringToFront();
            }
        }
        
        private void SetupDateRangeHandler(ComboBox comboBox, EventHandler handler)
        {
            if (comboBox != null)
            {
                comboBox.SelectedIndexChanged -= handler;
                comboBox.SelectedIndexChanged += handler;
            }
        }
        
        private void CmbCirculationDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCirculationDateRange();
            LoadCirculationReport();
        }
        
        private void UpdateCirculationDateRange()
        {
            UpdateDateRange(cmbCirculationDateRange, ref circulationStartDate, ref circulationEndDate);
        }
        
        private void UpdateDateRange(ComboBox dateRangeComboBox, ref DateTime startDate, ref DateTime endDate)
        {
            DateTime now = DateTime.Now;
            if (dateRangeComboBox == null) return;
            
            string selectedText = dateRangeComboBox.SelectedItem?.ToString() ?? "";
            int selectedIndex = dateRangeComboBox.SelectedIndex;
            
            // Check if "All" is selected (works for any combobox that has "All" as first item)
            if (selectedText.Equals("All", StringComparison.OrdinalIgnoreCase) || 
                (selectedIndex == 0 && dateRangeComboBox.Items.Count > 0 && dateRangeComboBox.Items[0].ToString().Equals("All", StringComparison.OrdinalIgnoreCase)))
            {
                startDate = DateTime.MinValue;
                endDate = DateTime.MaxValue;
                return;
            }
            
            // For comboboxes without "All", adjust index if needed
            int adjustedIndex = selectedIndex;
            if (dateRangeComboBox.Items.Count > 0 && dateRangeComboBox.Items[0].ToString().Equals("All", StringComparison.OrdinalIgnoreCase))
            {
                // This combobox has "All", so adjust index
                adjustedIndex = selectedIndex - 1;
            }
            
            switch (adjustedIndex)
            {
                case 0: // This Week (or index 1 if "All" is first)
                    int daysUntilMonday = ((int)now.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
                    startDate = now.Date.AddDays(-daysUntilMonday);
                    endDate = startDate.AddDays(7).AddSeconds(-1);
                    break;
                case 1: // This Month
                    startDate = new DateTime(now.Year, now.Month, 1);
                    endDate = startDate.AddMonths(1).AddSeconds(-1);
                    break;
                case 2: // This Quarter
                    int quarter = (now.Month - 1) / 3;
                    startDate = new DateTime(now.Year, quarter * 3 + 1, 1);
                    endDate = startDate.AddMonths(3).AddSeconds(-1);
                    break;
                case 3: // This Range (Custom)
                    using (var dateRangeForm = new DateRangePickerForm())
                    {
                        if (dateRangeForm.ShowDialog() == DialogResult.OK)
                        {
                            startDate = dateRangeForm.StartDate.Date;
                            endDate = dateRangeForm.EndDate.Date.AddDays(1).AddSeconds(-1);
                        }
                        else
                        {
                            // If cancelled, keep current selection or default
                            if (startDate == default(DateTime))
                            {
                                // Default based on whether combobox has "All"
                                if (dateRangeComboBox.Items.Count > 0 && dateRangeComboBox.Items[0].ToString().Equals("All", StringComparison.OrdinalIgnoreCase))
                                {
                                    startDate = DateTime.MinValue;
                                    endDate = DateTime.MaxValue;
                                }
                                else
                                {
                                    int daysUntilMondayDefault = ((int)now.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
                                    startDate = now.Date.AddDays(-daysUntilMondayDefault);
                                    endDate = startDate.AddDays(7).AddSeconds(-1);
                                }
                            }
                            return;
                        }
                    }
                    break;
                default:
                    // Default based on whether combobox has "All"
                    if (dateRangeComboBox.Items.Count > 0 && dateRangeComboBox.Items[0].ToString().Equals("All", StringComparison.OrdinalIgnoreCase))
                    {
                        startDate = DateTime.MinValue;
                        endDate = DateTime.MaxValue;
                    }
                    else
                    {
                        int daysUntilMondayDefault2 = ((int)now.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
                        startDate = now.Date.AddDays(-daysUntilMondayDefault2);
                        endDate = startDate.AddDays(7).AddSeconds(-1);
                    }
                    break;
            }
        }
        
        private void LoadCirculationReportByType(string reportType, Panel targetPanel = null)
        {
            if (targetPanel == null)
            {
                targetPanel = FindControl<Panel>(panelContentCirculation, "panelCirculationMainContainer");
                if (targetPanel == null) return;
            }
            
            if (reportType != "Borrowing by Category")
                ClearControlsExcept(targetPanel, new[] { "tableLayoutPanelCirculation" });
            
            if (reportType == "Borrowing by Category")
                LoadBorrowingByCategoryReport(targetPanel);
            else if (reportType == "Today's Borrowings")
                LoadTodaysBorrowingsReport(targetPanel);
            else if (reportType == "Today's Returns")
                LoadTodaysReturnsReport(targetPanel);
            else if (reportType == "Daily Borrowing/Return Chart")
            {
                panelChart = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(20) };
                panelChart.Paint += PanelChart_DailyActivity_Paint;
                targetPanel.Controls.Add(panelChart);
                panelChart.Invalidate();
            }
        }
        
        private void LoadTodaysBorrowingsReport(Panel targetPanel)
        {
            LoadTodaysReport(targetPanel, @"SELECT
                t.TransactionID as 'TRANSACTION ID',
                CONCAT(m.FirstName, ' ', m.LastName) as 'MEMBER',
                b.Title as 'BOOK TITLE',
                b.Author as 'AUTHOR',
                DATE_FORMAT(t.BorrowDate, '%Y-%m-%d %H:%i') as 'BORROW TIME',
                DATE_FORMAT(t.DueDate, '%Y-%m-%d') as 'DUE DATE'
                FROM Transactions t
                INNER JOIN Members m ON t.MemberID = m.MemberID
                INNER JOIN Books b ON t.BookID = b.BookID
                WHERE DATE(t.BorrowDate) = @Today AND t.ReturnDate IS NULL
                ORDER BY t.BorrowDate DESC", false);
        }
        
        private void LoadTodaysReturnsReport(Panel targetPanel)
        {
            LoadTodaysReport(targetPanel, @"SELECT
                t.TransactionID as 'TRANSACTION ID',
                CONCAT(m.FirstName, ' ', m.LastName) as 'MEMBER',
                b.Title as 'BOOK TITLE',
                b.Author as 'AUTHOR',
                DATE_FORMAT(t.BorrowDate, '%Y-%m-%d') as 'BORROW DATE',
                DATE_FORMAT(t.ReturnDate, '%Y-%m-%d %H:%i') as 'RETURN TIME',
                DATEDIFF(t.ReturnDate, t.DueDate) as 'DAYS OVERDUE',
                COALESCE(t.Fine, 0) as 'FINE'
                FROM Transactions t
                INNER JOIN Members m ON t.MemberID = m.MemberID
                INNER JOIN Books b ON t.BookID = b.BookID
                WHERE DATE(t.ReturnDate) = @Today
                ORDER BY t.ReturnDate DESC", true);
        }
        
        private void LoadTodaysReport(Panel targetPanel, string query, bool hasFineColumn)
        {
            DataGridView dgv = new DataGridView { Dock = DockStyle.Fill, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, ReadOnly = true, AllowUserToAddRows = false, BackgroundColor = Color.White };
            targetPanel.Controls.Add(dgv);
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Today", DateTime.Now.Date);
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dgv.DataSource = dt;
                            if (hasFineColumn && dgv.Columns.Contains("FINE"))
                            {
                                dgv.CellFormatting += (s, e) =>
                                {
                                    if (e.ColumnIndex >= 0 && dgv.Columns[e.ColumnIndex].Name == "FINE" && e.Value != null && e.Value != DBNull.Value && decimal.TryParse(e.Value.ToString(), out decimal amount))
                                    {
                                        e.Value = IDFormatter.FormatCurrency(amount);
                                        e.FormattingApplied = true;
                                    }
                                };
                                dgv.Columns["FINE"].DefaultCellStyle.ForeColor = Color.Red;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void PanelChart_DailyActivity_Paint(object sender, PaintEventArgs e)
        {
            var borrowData = GetDailyBorrowingData();
            var returnData = GetDailyReturnData();
            DrawDailyActivityChart(e.Graphics, (Panel)sender, borrowData, returnData);
        }
        private void LoadBorrowingByCategoryReport(Panel targetPanel)
        {
            if (targetPanel != panelCirculationMainContainer)
                targetPanel = panelCirculationMainContainer;
            
            if (panelCirculationMainContainer != null)
            {
                panelCirculationMainContainer.Visible = true;
                panelCirculationMainContainer.BringToFront();
                panelCirculationMainContainer.AutoScroll = true;
            }
            
            EnsureTableLayoutVisible(ref tableLayoutPanelCirculation, panelCirculationMainContainer, "tableLayoutPanelCirculation");
            if (tableLayoutPanelCirculation != null && tableLayoutPanelCirculation.RowStyles.Count < 3)
            {
                tableLayoutPanelCirculation.RowStyles.Clear();
                tableLayoutPanelCirculation.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
                tableLayoutPanelCirculation.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
                tableLayoutPanelCirculation.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            }
            
            ClearControlsExcept(tableLayoutPanelCirculation, new[] { "panelCirculationChartCard", "panelCirculationMostBorrowedCard", "panelCirculationOverdueCard" });
            
            SetupCirculationCard(ref panelCirculationChartCard, ref lblCirculationChartTitle, tableLayoutPanelCirculation, "panelCirculationChartCard", "lblCirculationChartTitle");
            Panel chartCard = panelCirculationChartCard;
            ClearControlsExcept(chartCard, new[] { "lblCirculationChartTitle" });
            if (chartCard.Padding.Top < 50) chartCard.Padding = new Padding(20, 50, 20, 20);
            chartCard.Margin = new Padding(3, 3, 3, 20);
            chartCard.Dock = DockStyle.Fill;
            
            Panel chartPanel = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };
            chartPanel.Paint += (s, e) => DrawCirculationBarChart(e.Graphics, chartPanel, GetBorrowingByCategoryData(circulationStartDate, circulationEndDate));
            chartCard.Controls.Add(chartPanel);
            
            SetupCirculationCard(ref panelCirculationMostBorrowedCard, ref lblCirculationMostBorrowedTitle, tableLayoutPanelCirculation, "panelCirculationMostBorrowedCard", "lblCirculationMostBorrowedTitle");
            Panel mostBorrowedCard = panelCirculationMostBorrowedCard;
            ClearControlsExcept(mostBorrowedCard, new[] { "lblCirculationMostBorrowedTitle" });
            if (mostBorrowedCard.Padding.Top < 50) mostBorrowedCard.Padding = new Padding(20, 50, 20, 20);
            mostBorrowedCard.Margin = new Padding(3, 0, 3, 20);
            mostBorrowedCard.Dock = DockStyle.Fill;
            if (mostBorrowedCard.Height < 200) mostBorrowedCard.MinimumSize = new Size(mostBorrowedCard.Width, 200);
            
            DataGridView dgvMostBorrowed = CreateStandardDataGridView();
            mostBorrowedCard.Controls.Add(dgvMostBorrowed);
            
            SetupCirculationCard(ref panelCirculationOverdueCard, ref lblCirculationOverdueTitle, tableLayoutPanelCirculation, "panelCirculationOverdueCard", "lblCirculationOverdueTitle");
            Panel overdueCard = panelCirculationOverdueCard;
            if (lblCirculationOverdueTitle == null && overdueCard != null)
            {
                lblCirculationOverdueTitle = new Label { Text = "Overdue Books Report", Font = new Font("Segoe UI", 12F, FontStyle.Bold), ForeColor = Color.FromArgb(64, 64, 64), Location = new Point(20, 20), AutoSize = true, Name = "lblCirculationOverdueTitle" };
                overdueCard.Controls.Add(lblCirculationOverdueTitle);
            }
            ClearControlsExcept(overdueCard, new[] { "lblCirculationOverdueTitle" });
            if (overdueCard.Padding.Top < 50) overdueCard.Padding = new Padding(20, 50, 20, 20);
            overdueCard.Margin = new Padding(3, 0, 3, 3);
            overdueCard.Dock = DockStyle.Fill;
            if (overdueCard.Height < 200) overdueCard.MinimumSize = new Size(overdueCard.Width, 200);
            
            DataGridView dgvOverdue = CreateStandardDataGridView();
            overdueCard.Controls.Add(dgvOverdue);
            
            LoadMostBorrowedBooksForCirculation(dgvMostBorrowed, circulationStartDate, circulationEndDate);
            LoadOverdueBooksReportForCirculation(dgvOverdue, circulationStartDate, circulationEndDate);
            
            EnsureTableLayoutPosition(tableLayoutPanelCirculation, chartCard, 0);
            EnsureTableLayoutPosition(tableLayoutPanelCirculation, mostBorrowedCard, 1);
            EnsureTableLayoutPosition(tableLayoutPanelCirculation, overdueCard, 2);
            
            if (panelCirculationMainContainer != null)
                panelCirculationMainContainer.AutoScrollMinSize = new Size(0, tableLayoutPanelCirculation != null ? tableLayoutPanelCirculation.Height + 100 : 1000);
            
            chartPanel.Invalidate();
        }
        
        private void SetupCirculationCard(ref Panel card, ref Label label, Control parent, string cardName, string labelName)
        {
            if (card == null)
                card = FindControl<Panel>(parent, cardName);
            if (card != null)
            {
                card.Visible = true;
                card.BringToFront();
                if (card.Padding.Top < 50)
                    card.Padding = new Padding(20, 50, 20, 20);
            }
            if (label == null && card != null)
                label = FindControl<Label>(card, labelName);
            if (label != null)
            {
                label.Visible = true;
                label.BringToFront();
            }
        }
        
        private void EnsureTableLayoutPosition(TableLayoutPanel tableLayout, Control control, int row)
        {
            if (tableLayout == null || control == null) return;
            if (control.Parent != tableLayout)
            {
                control.Parent?.Controls.Remove(control);
                tableLayout.Controls.Add(control, 0, row);
            }
            else
            {
                tableLayout.SetRow(control, row);
                tableLayout.SetColumn(control, 0);
            }
        }
        
        private void DrawCirculationBarChart(Graphics g, Panel panel, Dictionary<string, int> data)
        {
            ReportChartHelper.DrawCirculationBarChart(g, panel, data);
        }
        
        private void LoadMostBorrowedBooksForCirculation(DataGridView dgv, DateTime? start = null, DateTime? end = null)
        {
            DateTime startDateToUse = start ?? DateTime.MinValue;
            DateTime endDateToUse = end ?? DateTime.MaxValue;
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    if (!CheckTableExists(conn, "Transactions") || !DatabaseSchemaHelper.CheckColumnExists(conn, "Transactions", "BorrowDate"))
                    {
                        CreateEmptyDataTable(dgv, new[] { "RANK", "TITLE", "AUTHOR", "TIMES BORROWED" });
                        return;
                    }
                    
                    int totalTransactions = Convert.ToInt32(new MySqlCommand("SELECT COUNT(*) FROM Transactions", conn).ExecuteScalar() ?? 0);
                    if (totalTransactions == 0)
                    {
                        CreateEmptyDataTable(dgv, new[] { "RANK", "TITLE", "AUTHOR", "TIMES BORROWED" });
                        return;
                    }
                    
                    bool useDateFilter = startDateToUse != DateTime.MinValue && endDateToUse != DateTime.MaxValue;
                    if (useDateFilter)
                    {
                        string checkDateRangeQuery = "SELECT COUNT(*) FROM Transactions WHERE BorrowDate >= @StartDate AND BorrowDate <= @EndDate";
                        using (MySqlCommand checkCmd = new MySqlCommand(checkDateRangeQuery, conn))
                        {
                            checkCmd.Parameters.AddWithValue("@StartDate", startDateToUse);
                            checkCmd.Parameters.AddWithValue("@EndDate", endDateToUse);
                            if (Convert.ToInt32(checkCmd.ExecuteScalar() ?? 0) == 0)
                                useDateFilter = false;
                        }
                    }
                    
                    string query = useDateFilter
                        ? @"SELECT @row_number := @row_number + 1 as 'RANK', b.Title as 'TITLE', b.Author as 'AUTHOR', COUNT(*) as 'TIMES BORROWED'
                           FROM Transactions t INNER JOIN Books b ON t.BookID = b.BookID
                           CROSS JOIN (SELECT @row_number := 0) r
                           WHERE t.BorrowDate >= @StartDate AND t.BorrowDate <= @EndDate
                           GROUP BY b.BookID, b.Title, b.Author ORDER BY COUNT(*) DESC LIMIT 10"
                        : @"SELECT @row_number := @row_number + 1 as 'RANK', b.Title as 'TITLE', b.Author as 'AUTHOR', COUNT(*) as 'TIMES BORROWED'
                           FROM Transactions t INNER JOIN Books b ON t.BookID = b.BookID
                           CROSS JOIN (SELECT @row_number := 0) r
                           GROUP BY b.BookID, b.Title, b.Author ORDER BY COUNT(*) DESC LIMIT 10";
                    
                    DataTable dt = new DataTable();
                    try
                    {
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            if (useDateFilter)
                            {
                                cmd.Parameters.AddWithValue("@StartDate", startDateToUse);
                                cmd.Parameters.AddWithValue("@EndDate", endDateToUse);
                            }
                            using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                                adapter.Fill(dt);
                        }
                    }
                    catch
                    {
                        query = useDateFilter
                            ? @"SELECT b.Title as 'TITLE', b.Author as 'AUTHOR', COUNT(*) as 'TIMES BORROWED'
                               FROM Transactions t INNER JOIN Books b ON t.BookID = b.BookID
                               WHERE t.BorrowDate >= @StartDate AND t.BorrowDate <= @EndDate
                               GROUP BY b.BookID, b.Title, b.Author ORDER BY COUNT(*) DESC LIMIT 10"
                            : @"SELECT b.Title as 'TITLE', b.Author as 'AUTHOR', COUNT(*) as 'TIMES BORROWED'
                               FROM Transactions t INNER JOIN Books b ON t.BookID = b.BookID
                               GROUP BY b.BookID, b.Title, b.Author ORDER BY COUNT(*) DESC LIMIT 10";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            if (useDateFilter)
                            {
                                cmd.Parameters.AddWithValue("@StartDate", startDateToUse);
                                cmd.Parameters.AddWithValue("@EndDate", endDateToUse);
                            }
                            using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                                adapter.Fill(dt);
                        }
                        dt.Columns.Add("RANK", typeof(string));
                        dt.Columns["RANK"].SetOrdinal(0);
                        for (int i = 0; i < dt.Rows.Count; i++)
                            dt.Rows[i]["RANK"] = (i + 1).ToString();
                    }
                    
                    dt = ConvertToStringDataTable(dt);
                    if (dt.Rows.Count == 0)
                        CreateEmptyDataTable(dgv, new[] { "RANK", "TITLE", "AUTHOR", "TIMES BORROWED" });
                    else
                        SetupDataGridView(dgv, dt, new Dictionary<string, (int width, DataGridViewAutoSizeColumnMode mode, DataGridViewContentAlignment? alignment)>
                        {
                            { "RANK", (80, DataGridViewAutoSizeColumnMode.None, DataGridViewContentAlignment.MiddleCenter) },
                            { "TITLE", (200, DataGridViewAutoSizeColumnMode.Fill, null) },
                            { "AUTHOR", (150, DataGridViewAutoSizeColumnMode.Fill, null) },
                            { "TIMES BORROWED", (150, DataGridViewAutoSizeColumnMode.None, DataGridViewContentAlignment.MiddleCenter) }
                        });
                }
            }
            catch
            {
                CreateEmptyDataTable(dgv, new[] { "RANK", "TITLE", "AUTHOR", "TIMES BORROWED" });
            }
        }
        
        private DataTable ConvertToStringDataTable(DataTable dt)
        {
            if (dt.Rows.Count == 0) return dt;
            DataTable stringDataTable = new DataTable();
            foreach (DataColumn col in dt.Columns)
                stringDataTable.Columns.Add(col.ColumnName, typeof(string));
            foreach (DataRow row in dt.Rows)
            {
                DataRow newRow = stringDataTable.NewRow();
                foreach (DataColumn col in dt.Columns)
                    newRow[col.ColumnName] = row[col.ColumnName]?.ToString() ?? "";
                stringDataTable.Rows.Add(newRow);
            }
            return stringDataTable;
        }
        
        private void CreateEmptyDataTable(DataGridView dgv, string[] columnNames)
        {
            DataTable emptyTable = new DataTable();
            foreach (string colName in columnNames)
                emptyTable.Columns.Add(colName, typeof(string));
            dgv.DataSource = emptyTable;
        }
        
        private void SetupDataGridView(DataGridView dgv, DataTable dt, Dictionary<string, (int width, DataGridViewAutoSizeColumnMode mode, DataGridViewContentAlignment? alignment)> columnConfig)
        {
            dgv.DataSource = null;
            dgv.Columns.Clear();
            dgv.AutoGenerateColumns = true;
            dgv.DataSource = dt;
            dgv.ColumnHeadersVisible = true;
            dgv.ColumnHeadersHeight = 40;
            
            foreach (var kvp in columnConfig)
            {
                if (dgv.Columns.Contains(kvp.Key))
                {
                    var col = dgv.Columns[kvp.Key];
                    col.Width = kvp.Value.width;
                    col.AutoSizeMode = kvp.Value.mode;
                    if (kvp.Value.alignment.HasValue)
                        col.DefaultCellStyle.Alignment = kvp.Value.alignment.Value;
                    col.HeaderText = kvp.Key;
                    col.Visible = true;
                }
            }
            
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.Visible = true;
                if (string.IsNullOrEmpty(col.HeaderText))
                    col.HeaderText = col.Name;
                if (col.Width < 50)
                    col.Width = 100;
            }
        }
        
        private void LoadMemberReport()
        {
            InitializeDateRange(cmbMemberDateRange, ref memberStartDate, ref memberEndDate, UpdateMemberDateRange);
            EnsurePanelVisible(ref panelMemberMainContainer, panelContentMember, "panelMemberMainContainer");
            EnsureTableLayoutVisible(ref tableLayoutPanelMember, panelMemberMainContainer, "tableLayoutPanelMember");
            ClearControlsExcept(tableLayoutPanelMember, new[] { "panelMemberChartCard", "panelMemberTableCard" });
            
            EnsureCardVisible(ref panelMemberChartCard, tableLayoutPanelMember, "panelMemberChartCard");
            EnsureLabelVisible(ref lblMemberChartTitle, panelMemberChartCard, "lblMemberChartTitle");
            ClearControlsExcept(panelMemberChartCard, new[] { "lblMemberChartTitle" });
            
            var memberActivityData = GetMemberActivityData(memberStartDate, memberEndDate);
            Panel chartPanel = CreateChartPanel(memberActivityData, (g, p, d) => DrawMemberActivityChart(g, p, d));
            panelMemberChartCard.Controls.Add(chartPanel);
            
            EnsureCardVisible(ref panelMemberTableCard, tableLayoutPanelMember, "panelMemberTableCard");
            EnsureLabelVisible(ref lblMemberTableTitle, panelMemberTableCard, "lblMemberTableTitle");
            ClearControlsExcept(panelMemberTableCard, new[] { "lblMemberTableTitle" });
            
            DataGridView dgvNewMembers = CreateStandardDataGridView();
            panelMemberTableCard.Controls.Add(dgvNewMembers);
            LoadNewMemberRegistrationsForMemberReport(dgvNewMembers, memberStartDate, memberEndDate);
            chartPanel.Invalidate();
            SetupDateRangeHandler(cmbMemberDateRange, CmbMemberDateRange_SelectedIndexChanged);
        }
        
        private void EnsureCardVisible(ref Panel card, Control parent, string cardName)
        {
            if (card == null)
                card = FindControl<Panel>(parent, cardName);
            if (card != null)
            {
                card.Visible = true;
                card.BringToFront();
                if (card.Padding.Top < 50)
                    card.Padding = new Padding(20, 50, 20, 20);
            }
        }
        
        private void EnsureLabelVisible(ref Label label, Control parent, string labelName)
        {
            if (label == null)
                label = FindControl<Label>(parent, labelName);
            if (label != null)
            {
                label.Visible = true;
                label.BringToFront();
            }
        }
        
        private void ClearControlsExcept(Control parent, string[] keepNames)
        {
            var toRemove = parent.Controls.Cast<Control>().Where(c => !keepNames.Contains(c.Name)).ToList();
            foreach (var ctrl in toRemove)
            {
                parent.Controls.Remove(ctrl);
                ctrl.Dispose();
            }
        }
        
        private Panel CreateChartPanel(Dictionary<string, int> data, Action<Graphics, Panel, Dictionary<string, int>> drawAction)
        {
            Panel chartPanel = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };
            chartPanel.Tag = data;
            chartPanel.Paint += (s, e) => drawAction(e.Graphics, chartPanel, data);
            chartPanel.MouseMove += ChartPanel_MouseMove;
            chartPanel.MouseLeave += ChartPanel_MouseLeave;
            return chartPanel;
        }
        
        private DataGridView CreateStandardDataGridView()
        {
            return new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
                ReadOnly = true,
                AllowUserToAddRows = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                GridColor = Color.FromArgb(240, 240, 240),
                ScrollBars = ScrollBars.Both,
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None,
                ColumnHeadersVisible = true,
                ColumnHeadersHeight = 40,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 10F),
                    Padding = new Padding(10, 8, 10, 8),
                    ForeColor = Color.FromArgb(64, 64, 64),
                    SelectionForeColor = Color.FromArgb(64, 64, 64),
                    SelectionBackColor = Color.FromArgb(240, 240, 240),
                    Alignment = DataGridViewContentAlignment.MiddleLeft,
                    WrapMode = DataGridViewTriState.False
                },
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    BackColor = Color.FromArgb(248, 249, 250),
                    ForeColor = Color.FromArgb(64, 64, 64),
                    Padding = new Padding(10, 8, 10, 8),
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    WrapMode = DataGridViewTriState.False
                },
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(250, 250, 250) },
                RowHeadersVisible = false,
                EnableHeadersVisualStyles = false,
                RowTemplate = new DataGridViewRow { Height = 40 }
            };
        }
        
        private void CmbMemberDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMemberDateRange();
            LoadMemberReport();
        }
        
        private void UpdateMemberDateRange()
        {
            UpdateDateRange(cmbMemberDateRange, ref memberStartDate, ref memberEndDate);
        }
        
        private Dictionary<string, RectangleF> barRectangles = new Dictionary<string, RectangleF>();
        private string hoveredBar = null;
        private ToolTip chartTooltip = new ToolTip
        {
            IsBalloon = false,
            ToolTipTitle = "",
            UseAnimation = true,
            UseFading = true,
            BackColor = Color.FromArgb(245, 245, 245),
            ForeColor = Color.FromArgb(64, 64, 64)
        };
        
        private void ChartPanel_MouseMove(object sender, MouseEventArgs e)
        {
            Panel panel = sender as Panel;
            if (panel == null) return;
            
            string newHoveredBar = null;
            foreach (var kvp in barRectangles)
            {
                if (kvp.Value.Contains(e.Location))
                {
                    newHoveredBar = kvp.Key;
                    break;
                }
            }
            
            if (newHoveredBar != hoveredBar)
            {
                hoveredBar = newHoveredBar;
                if (hoveredBar != null && panel.Tag is Dictionary<string, int> data)
                {
                    // Normalize the key
                    string normalizedKey = hoveredBar;
                    string keyLower = normalizedKey.ToLowerInvariant();
                    if (keyLower.Contains("student"))
                        normalizedKey = "Student";
                    else if (keyLower.Contains("faculty"))
                        normalizedKey = "Faculty";
                    else if (keyLower.Contains("staff") && !keyLower.Contains("library"))
                        normalizedKey = "Staff";
                    else if (keyLower.Contains("guest"))
                        normalizedKey = "Guest";
                    
                    int value = data.ContainsKey(normalizedKey) ? data[normalizedKey] : 
                               (data.ContainsKey(hoveredBar) ? data[hoveredBar] : 0);
                    
                    chartTooltip.SetToolTip(panel, $"{normalizedKey}\nActive Members : {value}");
                }
                else
                {
                    chartTooltip.SetToolTip(panel, "");
                }
                panel.Invalidate();
            }
        }
        
        private void ChartPanel_MouseLeave(object sender, EventArgs e)
        {
            hoveredBar = null;
            Panel panel = sender as Panel;
            if (panel != null)
            {
                chartTooltip.SetToolTip(panel, "");
                panel.Invalidate();
            }
        }
        
        private void DrawMemberActivityChart(Graphics g, Panel panel, Dictionary<string, int> data)
        {
            if (data == null || data.Count == 0)
            {
                using (Font noDataFont = new Font("Segoe UI", 11F, FontStyle.Italic))
                {
                    string noDataText = "No member activity data available";
                    SizeF textSize = g.MeasureString(noDataText, noDataFont);
                    g.DrawString(noDataText, noDataFont, new SolidBrush(Color.FromArgb(150, 150, 150)),
                        panel.Width / 2 - textSize.Width / 2, panel.Height / 2 - textSize.Height / 2);
                }
                return;
            }
            
            var normalizedData = NormalizeMemberTypes(data);
            string[] memberTypes = { "Student", "Faculty", "Staff", "Guest" };
            var sortedData = memberTypes.Select(type => new KeyValuePair<string, int>(type, normalizedData.ContainsKey(type) ? normalizedData[type] : 0)).ToList();
            
            int padding = 60;
            int chartWidth = panel.Width - (padding * 2);
            int chartHeight = panel.Height - (padding * 2);
            int startX = padding;
            int startY = padding;
            int endX = panel.Width - padding;
            int endY = panel.Height - padding;
            
            int maxDataValue = sortedData.Count > 0 ? sortedData.Max(kvp => kvp.Value) : 0;
            int maxValue = maxDataValue > 0 ? ((maxDataValue / 350) + 1) * 350 : 1400;
            if (maxValue < 350) maxValue = 350;
            int gridStep = maxValue / 4;
            
            using (Pen axisPen = new Pen(Color.FromArgb(200, 200, 200), 2))
            {
                g.DrawLine(axisPen, startX, endY, endX, endY);
                g.DrawLine(axisPen, startX, startY, startX, endY);
            }
            
            using (Font axisFont = new Font("Segoe UI", 9F))
            using (Brush axisBrush = new SolidBrush(Color.FromArgb(100, 100, 100)))
            {
                for (int i = 0; i <= 4; i++)
                {
                    int value = i * gridStep;
                    int y = endY - (int)((double)value / maxValue * chartHeight);
                    if (i > 0)
                    {
                        using (Pen gridPen = new Pen(Color.FromArgb(240, 240, 240), 1) { DashStyle = DashStyle.Dash })
                            g.DrawLine(gridPen, startX, y, endX, y);
                    }
                    string valueText = value.ToString();
                    SizeF textSize = g.MeasureString(valueText, axisFont);
                    g.DrawString(valueText, axisFont, axisBrush, startX - textSize.Width - 10, y - textSize.Height / 2);
                }
            }
            
            barRectangles.Clear();
            int barCount = sortedData.Count;
            float barWidth = (float)chartWidth / (barCount + 1) - 30;
            float stepX = (float)chartWidth / (barCount + 1);
            Color barColor = Color.FromArgb(138, 43, 226);
            Color hoverColor = Color.FromArgb(120, 35, 200);
            
            using (Font valueFont = new Font("Segoe UI", 9F))
            using (Font labelFont = new Font("Segoe UI", 9F))
            {
                for (int i = 0; i < barCount; i++)
                {
                    var item = sortedData[i];
                    float x = startX + (i + 1) * stepX - barWidth / 2;
                    float barHeight = (float)item.Value / maxValue * chartHeight;
                    float y = endY - barHeight;
                    
                    RectangleF barRect = new RectangleF(x, y, barWidth, barHeight);
                    barRectangles[item.Key] = barRect;
                    
                    Color currentBarColor = (hoveredBar == item.Key) ? hoverColor : barColor;
                    using (SolidBrush barBrush = new SolidBrush(currentBarColor))
                        g.FillRectangle(barBrush, barRect);
                    
                    string valueText = item.Value.ToString();
                    SizeF valueSize = g.MeasureString(valueText, valueFont);
                    float valueX = x + (barWidth / 2) - (valueSize.Width / 2);
                    float valueY = barHeight > 25 ? y - valueSize.Height - 5 : y - valueSize.Height - 8;
                    
                    RectangleF valueRect = new RectangleF(valueX - 2, valueY - 1, valueSize.Width + 4, valueSize.Height + 2);
                    g.FillRectangle(Brushes.White, valueRect);
                    g.DrawString(valueText, valueFont, new SolidBrush(Color.FromArgb(64, 64, 64)), valueX, valueY);
                    
                    SizeF labelSize = g.MeasureString(item.Key, labelFont);
                    g.DrawString(item.Key, labelFont, new SolidBrush(Color.FromArgb(64, 64, 64)),
                        x + (barWidth / 2) - (labelSize.Width / 2), endY + 8);
                }
            }
        }
        
        private Dictionary<string, int> NormalizeMemberTypes(Dictionary<string, int> data)
        {
            var normalized = new Dictionary<string, int>();
            foreach (var kvp in data)
            {
                string key = kvp.Key.ToLowerInvariant();
                string normalizedKey = key.Contains("student") ? "Student" :
                    key.Contains("faculty") ? "Faculty" :
                    key.Contains("staff") && !key.Contains("library") ? "Staff" :
                    key.Contains("guest") ? "Guest" : kvp.Key;
                
                if (normalized.ContainsKey(normalizedKey))
                    normalized[normalizedKey] += kvp.Value;
                else
                    normalized[normalizedKey] = kvp.Value;
            }
            return normalized;
        }
        
        private void LoadNewMemberRegistrationsForMemberReport(DataGridView dgv, DateTime? start = null, DateTime? end = null)
        {
            DateTime startDateToUse = start ?? memberStartDate;
            DateTime endDateToUse = end ?? memberEndDate;
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    if (!CheckTableExists(conn, "Members")) return;
                    
                    int totalMembers = Convert.ToInt32(new MySqlCommand("SELECT COUNT(*) FROM Members", conn).ExecuteScalar() ?? 0);
                    bool useDateFilter = totalMembers > 0;
                    if (useDateFilter)
                    {
                        string countInRangeQuery = @"SELECT COUNT(*) FROM Members m WHERE m.RegistrationDate >= @StartDate AND m.RegistrationDate <= @EndDate";
                        using (MySqlCommand countInRangeCmd = new MySqlCommand(countInRangeQuery, conn))
                        {
                            countInRangeCmd.Parameters.AddWithValue("@StartDate", startDateToUse);
                            countInRangeCmd.Parameters.AddWithValue("@EndDate", endDateToUse);
                            if (Convert.ToInt32(countInRangeCmd.ExecuteScalar() ?? 0) == 0)
                                useDateFilter = false;
                        }
                    }
                    
                    string query = @"SELECT CONCAT('MEM-', LPAD(m.MemberID, 3, '0')) as 'MEMBER ID',
                                    CONCAT(m.FirstName, ' ', m.LastName) as 'NAME',
                                    COALESCE(m.Type, m.MemberType, 'N/A') as 'MEMBER TYPE',
                                    DATE(m.RegistrationDate) as 'REGISTRATION DATE'
                                    FROM Members m" + (useDateFilter ? " WHERE m.RegistrationDate >= @StartDate AND m.RegistrationDate <= @EndDate" : "") + $" ORDER BY m.RegistrationDate DESC LIMIT {Constants.DefaultQueryLimit}";
                    
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (useDateFilter)
                        {
                            cmd.Parameters.AddWithValue("@StartDate", startDateToUse);
                            cmd.Parameters.AddWithValue("@EndDate", endDateToUse);
                        }
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dt = ConvertToStringDataTable(dt);
                            SetupDataGridView(dgv, dt, new Dictionary<string, (int width, DataGridViewAutoSizeColumnMode mode, DataGridViewContentAlignment? alignment)>
                            {
                                { "MEMBER ID", (120, DataGridViewAutoSizeColumnMode.None, null) },
                                { "NAME", (200, DataGridViewAutoSizeColumnMode.Fill, null) },
                                { "MEMBER TYPE", (150, DataGridViewAutoSizeColumnMode.None, null) },
                                { "REGISTRATION DATE", (180, DataGridViewAutoSizeColumnMode.None, null) }
                            });
                            dgv.RowTemplate.Height = 40;
                            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading new member registrations: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        
        private void LoadCollectionReport()
        {
            InitializeDateRange(cmbCollectionDateRange, ref collectionStartDate, ref collectionEndDate, UpdateCollectionDateRange);
            EnsurePanelVisible(ref panelCollectionMainContainer, panelContentCollection, "panelCollectionMainContainer");
            EnsureLabelVisible(ref lblCollectionTitle, panelCollectionMainContainer, "lblCollectionTitle");
            EnsureFlowLayoutVisible(ref flowLayoutPanelCollection, panelCollectionMainContainer, "flowLayoutPanelCollection");
            LoadCollectionReportByType("Collection Summary", panelCollectionMainContainer);
            SetupDateRangeHandler(cmbCollectionDateRange, CmbCollectionDateRange_SelectedIndexChanged);
        }
        
        private void EnsureFlowLayoutVisible(ref FlowLayoutPanel flowLayout, Control parent, string layoutName)
        {
            if (flowLayout == null)
                flowLayout = FindControl<FlowLayoutPanel>(parent, layoutName);
            if (flowLayout != null)
            {
                flowLayout.Visible = true;
                flowLayout.BringToFront();
            }
        }
        
        private void CmbCollectionDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCollectionDateRange();
            LoadCollectionReport();
        }
        
        private void UpdateCollectionDateRange()
        {
            UpdateDateRange(cmbCollectionDateRange, ref collectionStartDate, ref collectionEndDate);
        }
        
        private void LoadCollectionReportByType(string reportType, Panel targetPanel = null)
        {
            if (targetPanel == null)
            {
                targetPanel = FindControl<Panel>(panelContentCollection, "panelCollectionMainContainer");
                if (targetPanel == null) return;
            }
            
            if (reportType != "Collection Summary")
                ClearControlsExcept(targetPanel, new[] { "lblCollectionTitle", "flowLayoutPanelCollection" });
            
            if (reportType == "Collection Summary")
                LoadCollectionSummaryReport(targetPanel);
            else if (reportType == "Shelf List")
                LoadShelfListReport(targetPanel);
        }
        
        private void LoadCollectionSummaryReport(Panel targetPanel)
        {
            EnsureLabelVisible(ref lblCollectionTitle, targetPanel, "lblCollectionTitle");
            EnsureFlowLayoutVisible(ref flowLayoutPanelCollection, targetPanel, "flowLayoutPanelCollection");
            ClearControlsExcept(flowLayoutPanelCollection, new[] { "panelCollectionCardTotal", "panelCollectionCardAvailable", "panelCollectionCardOnLoan" });
            
            var summaryData = GetCollectionSummary(collectionStartDate, collectionEndDate);
            UpdateOrCreateCollectionCard(flowLayoutPanelCollection, "panelCollectionCardTotal", "lblCollectionCardTotalValue",
                "Total Books", Convert.ToInt32(summaryData["Total"]).ToString("N0"), Color.FromArgb(13, 110, 253));
            UpdateOrCreateCollectionCard(flowLayoutPanelCollection, "panelCollectionCardAvailable", "lblCollectionCardAvailableValue",
                "Available", Convert.ToInt32(summaryData["Available"]).ToString("N0"), Color.FromArgb(40, 167, 69));
            UpdateOrCreateCollectionCard(flowLayoutPanelCollection, "panelCollectionCardOnLoan", "lblCollectionCardOnLoanValue",
                "On Loan", Convert.ToInt32(summaryData["OnLoan"]).ToString("N0"), Color.FromArgb(255, 193, 7));
            EnsureAccentBars(flowLayoutPanelCollection);
        }
        
        private void UpdateOrCreateCollectionCard(FlowLayoutPanel parent, string cardName, string labelName, string title, string value, Color accentColor)
        {
            Panel card = FindControl<Panel>(parent, cardName);
            if (card == null)
                CreateCollectionSummaryCard(parent, title, value, accentColor);
            else
            {
                Label lbl = FindControl<Label>(card, labelName);
                if (lbl != null) lbl.Text = value;
            }
        }
        
        private void UpdateOrCreateFinancialCard(FlowLayoutPanel parent, string cardName, string labelName, string title, decimal value, Color accentColor)
        {
            Panel card = FindControl<Panel>(parent, cardName);
            if (card == null)
                CreateFinancialSummaryCard(parent, title, IDFormatter.FormatCurrency(value), accentColor);
            else
            {
                Label lbl = FindControl<Label>(card, labelName);
                if (lbl != null) lbl.Text = IDFormatter.FormatCurrency(value);
            }
        }
        
        private void CreateSummaryCard(FlowLayoutPanel parent, string title, string value, Color accentColor, int cardWidth = 350)
        {
            Panel card = new Panel
            {
                Width = cardWidth,
                Height = 140,
                BackColor = Color.White,
                Margin = new Padding(0, 0, cardWidth == 350 ? 20 : 15, 0),
                Padding = new Padding(25, 20, 25, 20)
            };
            
            Label lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 11F, FontStyle.Regular),
                ForeColor = Color.FromArgb(128, 128, 128),
                AutoSize = true,
                Location = new Point(25, 20)
            };
            
            Label lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 32F, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                AutoSize = true,
                Location = new Point(25, 50)
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
        
        private void CreateCollectionSummaryCard(FlowLayoutPanel parent, string title, string value, Color accentColor)
        {
            CreateSummaryCard(parent, title, value, accentColor, 350);
        }
        
        private void LoadShelfListReport(Panel targetPanel)
        {
            DataGridView dgv = new DataGridView { Dock = DockStyle.Fill, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, ReadOnly = true, AllowUserToAddRows = false, BackgroundColor = Color.White };
            targetPanel.Controls.Add(dgv);
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT COALESCE(b.CallNumber, CONCAT(b.Category, '-', b.BookID)) as 'CALL NUMBER',
                                    COALESCE(b.AccessionNo, CONCAT('ACC-', LPAD(b.BookID, 4, '0'))) as 'ACCESSION NO',
                                    b.Title as 'TITLE', b.Author as 'AUTHOR', b.Category as 'CATEGORY',
                                    COALESCE(b.Location, 'N/A') as 'LOCATION', b.TotalCopies as 'TOTAL COPIES',
                                    b.Available as 'AVAILABLE', b.Status as 'STATUS',
                                    CASE WHEN b.PublicationYear IS NOT NULL THEN CAST(b.PublicationYear AS CHAR) ELSE 'N/A' END as 'YEAR'
                                    FROM Books b WHERE b.BookID IS NOT NULL
                                    ORDER BY COALESCE(b.CallNumber, CONCAT(b.Category, '-', b.BookID)), COALESCE(b.AccessionNo, CONCAT('ACC-', LPAD(b.BookID, 4, '0')))";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgv.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading shelf list: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void LoadFinancialReport()
        {
            InitializeDateRange(cmbFinancialDateRange, ref financialStartDate, ref financialEndDate, UpdateFinancialDateRange);
            EnsurePanelVisible(ref panelFinancialMainContainer, panelContentFinancial, "panelFinancialMainContainer");
            EnsureLabelVisible(ref lblFinancialTitle, panelFinancialMainContainer, "lblFinancialTitle");
            EnsureFlowLayoutVisible(ref flowLayoutPanelFinancial, panelFinancialMainContainer, "flowLayoutPanelFinancial");
            ClearControlsExcept(flowLayoutPanelFinancial, new[] { "panelFinancialCardCollected", "panelFinancialCardPending", "panelFinancialCardWaived" });
            
            var financialData = GetFinancialSummary(financialStartDate, financialEndDate);
            UpdateOrCreateFinancialCard(flowLayoutPanelFinancial, "panelFinancialCardCollected", "lblFinancialCardCollectedValue",
                "Fines Collected", Convert.ToDecimal(financialData["Collected"]), Color.FromArgb(40, 167, 69));
            UpdateOrCreateFinancialCard(flowLayoutPanelFinancial, "panelFinancialCardPending", "lblFinancialCardPendingValue",
                "Pending Fines", Convert.ToDecimal(financialData["Pending"]), Color.FromArgb(255, 193, 7));
            UpdateOrCreateFinancialCard(flowLayoutPanelFinancial, "panelFinancialCardWaived", "lblFinancialCardWaivedValue",
                "Waived Fines", Convert.ToDecimal(financialData["Waived"]), Color.FromArgb(220, 53, 69));
            EnsureAccentBars(flowLayoutPanelFinancial);
            SetupDateRangeHandler(cmbFinancialDateRange, CmbFinancialDateRange_SelectedIndexChanged);
        }
        
        private void EnsureAccentBars(FlowLayoutPanel summaryPanel)
        {
            foreach (Control card in summaryPanel.Controls)
            {
                if (card is Panel cardPanel)
                {
                    Panel accentBar = cardPanel.Controls.Cast<Control>().FirstOrDefault(c => c is Panel p && p.Dock == DockStyle.Bottom && p.Height == 4) as Panel;
                    Color accentColor = GetAccentColor(cardPanel.Name);
                    if (accentBar != null)
                    {
                        accentBar.BackColor = accentColor;
                        accentBar.Height = 4;
                        accentBar.Dock = DockStyle.Bottom;
                    }
                    else
                    {
                        cardPanel.Controls.Add(new Panel { Height = 4, BackColor = accentColor, Dock = DockStyle.Bottom });
                    }
                }
            }
        }
        
        private Color GetAccentColor(string cardName)
        {
            if (cardName.Contains("Total") || cardName.Contains("Visits")) return Color.FromArgb(13, 110, 253);
            if (cardName.Contains("Available") || cardName.Contains("Collected") || cardName.Contains("BooksPerMember")) return Color.FromArgb(40, 167, 69);
            if (cardName.Contains("OnLoan") || cardName.Contains("Pending") || cardName.Contains("BorrowingPeriod")) return Color.FromArgb(255, 193, 7);
            if (cardName.Contains("Waived") || cardName.Contains("Turnover")) return Color.FromArgb(220, 53, 69);
            return Color.FromArgb(13, 110, 253);
        }
        
        private void CmbFinancialDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFinancialDateRange();
            LoadFinancialReport();
        }
        
        private void UpdateFinancialDateRange()
        {
            UpdateDateRange(cmbFinancialDateRange, ref financialStartDate, ref financialEndDate);
        }
        
        private void CreateFinancialSummaryCard(FlowLayoutPanel parent, string title, string value, Color accentColor)
        {
            CreateSummaryCard(parent, title, value, accentColor, 350);
        }
            
            
          
        private void LoadStatisticalReport()
        {
            InitializeDateRange(cmbStatisticalDateRange, ref statisticalStartDate, ref statisticalEndDate, UpdateStatisticalDateRange);
            EnsurePanelVisible(ref panelStatisticalMainContainer, panelContentStatistical, "panelStatisticalMainContainer");
            RemoveControlByName(panelStatisticalMainContainer, "panelStatisticalHeader");
            EnsurePanelVisible(ref panelStatisticalContent, panelStatisticalMainContainer, "panelStatisticalContent");
            if (panelStatisticalContent != null)
                LoadUsageStatisticsReport(panelStatisticalContent);
            SetupDateRangeHandler(cmbStatisticalDateRange, CmbStatisticalDateRange_SelectedIndexChanged);
        }
        
        private void RemoveControlByName(Control parent, string controlName)
        {
            Control toRemove = FindControl<Control>(parent, controlName);
            if (toRemove != null)
            {
                parent.Controls.Remove(toRemove);
                toRemove.Dispose();
            }
        }
        
        private void CmbStatisticalDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateStatisticalDateRange();
            // Always load Usage Statistics report (no Report Type dropdown)
            if (panelStatisticalContent != null)
            {
                LoadUsageStatisticsReport(panelStatisticalContent);
            }
            else
            {
                LoadStatisticalReport();
            }
        }
        
        // CmbStatisticalReportType_SelectedIndexChanged removed - Report Type dropdown no longer exists
        
        private void UpdateStatisticalDateRange()
        {
            UpdateDateRange(cmbStatisticalDateRange, ref statisticalStartDate, ref statisticalEndDate);
        }
        
        private void LoadStatisticalReportByType(string reportType, Panel targetPanel = null)
        {
            if (targetPanel == null)
            {
                Panel mainContainer = FindControl<Panel>(panelContentStatistical, "panelStatisticalMainContainer");
                targetPanel = mainContainer != null ? FindControl<Panel>(mainContainer, "panelStatisticalContent") : null;
                if (targetPanel == null) return;
            }
            
            if (reportType != "Usage Statistics")
                ClearControlsExcept(targetPanel, new[] { "lblStatisticalTitle", "flowLayoutPanelStatistical" });
            
            if (reportType == "All")
                LoadAllStatisticalReports(targetPanel);
            else if (reportType == "Usage Statistics")
                LoadUsageStatisticsReport(targetPanel);
            else if (reportType == "Peak Borrowing Times")
                LoadPeakBorrowingTimesReport(targetPanel);
        }
        
        private void LoadUsageStatisticsReport(Panel targetPanel)
        {
            EnsureLabelVisible(ref lblStatisticalTitle, targetPanel, "lblStatisticalTitle");
            if (lblStatisticalTitle != null)
                lblStatisticalTitle.Text = "Library Usage Statistics";
            EnsureFlowLayoutVisible(ref flowLayoutPanelStatistical, targetPanel, "flowLayoutPanelStatistical");
            if (flowLayoutPanelStatistical != null)
                flowLayoutPanelStatistical.WrapContents = false;
            
            var statsData = GetUsageStatistics(statisticalStartDate, statisticalEndDate);
            string dailyVisits = Convert.ToInt32(statsData["DailyVisits"]).ToString();
            string booksPerMember = Convert.ToDouble(statsData["BooksPerMember"]).ToString("F1");
            string avgPeriod = Convert.ToInt32(statsData["AvgBorrowingPeriod"]).ToString() + " days";
            string turnover = Convert.ToDouble(statsData["CollectionTurnover"]).ToString("F0") + "%";
            
            if (panelStatisticalCardVisits != null && panelStatisticalCardBooksPerMember != null && 
                panelStatisticalCardBorrowingPeriod != null && panelStatisticalCardTurnover != null)
            {
                UpdateStatisticalCardValue("lblStatisticalCardVisitsValue", panelStatisticalCardVisits, dailyVisits);
                UpdateStatisticalCardValue("lblStatisticalCardBooksPerMemberValue", panelStatisticalCardBooksPerMember, booksPerMember);
                UpdateStatisticalCardValue("lblStatisticalCardBorrowingPeriodValue", panelStatisticalCardBorrowingPeriod, avgPeriod);
                UpdateStatisticalCardValue("lblStatisticalCardTurnoverValue", panelStatisticalCardTurnover, turnover);
                EnsureAccentBars(flowLayoutPanelStatistical);
            }
            else
            {
                CreateStatisticalSummaryCard(flowLayoutPanelStatistical, "Daily Average Visits", dailyVisits, Color.FromArgb(13, 110, 253));
                CreateStatisticalSummaryCard(flowLayoutPanelStatistical, "Books Per Member", booksPerMember, Color.FromArgb(40, 167, 69));
                CreateStatisticalSummaryCard(flowLayoutPanelStatistical, "Avg. Borrowing Period", avgPeriod, Color.FromArgb(255, 193, 7));
                CreateStatisticalSummaryCard(flowLayoutPanelStatistical, "Collection Turnover", turnover, Color.FromArgb(220, 53, 69));
            }
        }
        
        private void UpdateStatisticalCardValue(string labelName, Panel card, string value)
        {
            Label lbl = FindControl<Label>(card, labelName);
            if (lbl != null)
                lbl.Text = value;
        }
        
        private void CreateStatisticalSummaryCard(FlowLayoutPanel parent, string title, string value, Color accentColor)
        {
            int availableWidth = parent.Width > 0 ? parent.Width : 1400;
            int cardWidth = Math.Max(300, Math.Min(350, (availableWidth - 60) / 4));
            CreateSummaryCard(parent, title, value, accentColor, cardWidth);
        }
        
        private void LoadAllStatisticalReports(Panel targetPanel)
        {
            ClearControlsExcept(targetPanel, new[] { "lblStatisticalTitle", "flowLayoutPanelStatistical" });
            
            Panel scrollContainer = new Panel { Name = "panelAllReportsContainer", Dock = DockStyle.Fill, AutoScroll = true, Padding = new Padding(0, 0, 0, 20), BackColor = Color.Transparent };
            targetPanel.Controls.Add(scrollContainer);
            
            Panel allContentPanel = new Panel { Name = "panelAllContent", AutoSize = true, Dock = DockStyle.Top, BackColor = Color.Transparent, Padding = new Padding(20, 0, 20, 0) };
            scrollContainer.Controls.Add(allContentPanel);
            
            int currentY = 0;
            allContentPanel.Controls.Add(new Label { Name = "lblUsageStatsTitle", Text = "Library Usage Statistics", Font = new Font("Segoe UI", 14F, FontStyle.Bold), ForeColor = Color.FromArgb(64, 64, 64), Location = new Point(0, currentY), AutoSize = true });
            currentY += 40;
            
            FlowLayoutPanel usageStatsPanel = new FlowLayoutPanel { Name = "flowLayoutPanelUsageStats", Location = new Point(0, currentY), Size = new Size(allContentPanel.Width - 40, 160), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right, FlowDirection = FlowDirection.LeftToRight, WrapContents = true, Padding = new Padding(0, 10, 0, 10), Margin = new Padding(0, 10, 0, 20), BackColor = Color.Transparent };
            allContentPanel.Controls.Add(usageStatsPanel);
            currentY += 180;
            
            var statsData = GetUsageStatistics(statisticalStartDate, statisticalEndDate);
            CreateStatisticalSummaryCard(usageStatsPanel, "Daily Average Visits", Convert.ToInt32(statsData["DailyVisits"]).ToString(), Color.FromArgb(13, 110, 253));
            CreateStatisticalSummaryCard(usageStatsPanel, "Books Per Member", Convert.ToDouble(statsData["BooksPerMember"]).ToString("F1"), Color.FromArgb(40, 167, 69));
            CreateStatisticalSummaryCard(usageStatsPanel, "Avg. Borrowing Period", Convert.ToInt32(statsData["AvgBorrowingPeriod"]).ToString() + " days", Color.FromArgb(255, 193, 7));
            CreateStatisticalSummaryCard(usageStatsPanel, "Collection Turnover", Convert.ToDouble(statsData["CollectionTurnover"]).ToString("F0") + "%", Color.FromArgb(220, 53, 69));
            
            allContentPanel.Controls.Add(new Label { Name = "lblPeakTimesTitle", Text = "Peak Borrowing Times", Font = new Font("Segoe UI", 14F, FontStyle.Bold), ForeColor = Color.FromArgb(64, 64, 64), Location = new Point(0, currentY), AutoSize = true });
            currentY += 40;
            
            Panel peakChartPanel = new Panel { Name = "panelPeakTimesChart", Location = new Point(0, currentY), Size = new Size(allContentPanel.Width - 40, 400), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right, BackColor = Color.White, Margin = new Padding(0, 10, 0, 10) };
            peakChartPanel.Paint += (s, e) => DrawPeakTimesChart(e.Graphics, peakChartPanel);
            allContentPanel.Controls.Add(peakChartPanel);
            currentY += 420;
            
            DataGridView dgvPeakTimes = new DataGridView { Name = "dgvPeakTimes", Location = new Point(0, currentY), Size = new Size(allContentPanel.Width - 40, 300), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, ReadOnly = true, AllowUserToAddRows = false, BackgroundColor = Color.White, Margin = new Padding(0, 10, 0, 20) };
            allContentPanel.Controls.Add(dgvPeakTimes);
            currentY += 320;
            allContentPanel.Height = currentY;
            
            LoadPeakTimesData(dgvPeakTimes);
            peakChartPanel.Invalidate();
        }
        
        private void LoadPeakBorrowingTimesReport(Panel targetPanel)
        {
            ClearControlsExcept(targetPanel, new[] { "lblStatisticalTitle", "flowLayoutPanelStatistical" });
            
            TableLayoutPanel container = new TableLayoutPanel { Name = "tableLayoutPanelPeakTimes", Dock = DockStyle.Fill, RowCount = 2, ColumnCount = 1, Padding = new Padding(20, 60, 20, 20) };
            container.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            container.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            
            Panel chartPanel = new Panel { Name = "panelPeakTimesChart", Dock = DockStyle.Fill, BackColor = Color.White, Margin = new Padding(0, 0, 0, 10), Padding = new Padding(10) };
            chartPanel.Paint += (s, e) => DrawPeakTimesChart(e.Graphics, chartPanel);
            container.Controls.Add(chartPanel, 0, 0);
            
            DataGridView dgv = new DataGridView { Name = "dgvPeakTimes", Dock = DockStyle.Fill, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, ReadOnly = true, AllowUserToAddRows = false, BackgroundColor = Color.White, Margin = new Padding(0, 10, 0, 0) };
            container.Controls.Add(dgv, 0, 1);
            targetPanel.Controls.Add(container);
            
            LoadPeakTimesData(dgv, 20);
            chartPanel.Invalidate();
        }
        
        private void LoadPeakTimesData(DataGridView dgv, int limit = 50)
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string orderBy = limit == Constants.DefaultQueryLimit ? "COUNT(*) DESC, DAYNAME(BorrowDate), HOUR(BorrowDate)" : "COUNT(*) DESC, HOUR(BorrowDate) ASC";
                    string query = $@"SELECT HOUR(BorrowDate) as 'HOUR', DAYNAME(BorrowDate) as 'DAY', COUNT(*) as 'BORROW COUNT'
                                    FROM Transactions WHERE BorrowDate IS NOT NULL
                                    AND (@StartDate IS NULL OR DATE(BorrowDate) >= @StartDate)
                                    AND (@EndDate IS NULL OR DATE(BorrowDate) <= @EndDate)
                                    GROUP BY HOUR(BorrowDate), DAYNAME(BorrowDate)
                                    ORDER BY {orderBy} LIMIT {limit}";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        DateTime startDateToUse = statisticalStartDate != default(DateTime) ? statisticalStartDate : DateTime.MinValue;
                        DateTime endDateToUse = statisticalEndDate != default(DateTime) ? statisticalEndDate : DateTime.MaxValue;
                        cmd.Parameters.AddWithValue("@StartDate", startDateToUse != DateTime.MinValue ? startDateToUse : (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@EndDate", endDateToUse != DateTime.MaxValue ? endDateToUse : (object)DBNull.Value);
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dgv.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading peak borrowing times: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void DrawPeakTimesChart(Graphics g, Panel panel)
        {
            DateTime startDateToUse = statisticalStartDate != default(DateTime) ? statisticalStartDate : DateTime.MinValue;
            DateTime endDateToUse = statisticalEndDate != default(DateTime) ? statisticalEndDate : DateTime.MaxValue;
            ReportChartHelper.DrawPeakTimesChart(g, panel, 
                startDateToUse != DateTime.MinValue ? startDateToUse : (DateTime?)null,
                endDateToUse != DateTime.MaxValue ? endDateToUse : (DateTime?)null);
        }
        
        private void DrawDailyActivityChart(Graphics g, Panel panel, Dictionary<DateTime, int> borrowData, Dictionary<DateTime, int> returnData)
        {
            ReportChartHelper.DrawDailyActivityChart(g, panel, borrowData, returnData);
        }
        private Dictionary<DateTime, int> GetDailyBorrowingData()
        {
            return GetDailyTransactionData("BorrowDate", startDate, endDate);
        }
        
        private Dictionary<DateTime, int> GetDailyReturnData()
        {
            return GetDailyTransactionData("ReturnDate", startDate, endDate, "AND ReturnDate IS NOT NULL");
        }
        
        private Dictionary<DateTime, int> GetDailyTransactionData(string dateColumn, DateTime start, DateTime end, string additionalFilter = "")
        {
            Dictionary<DateTime, int> data = new Dictionary<DateTime, int>();
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = $@"SELECT DATE({dateColumn}) as Date, COUNT(*) as Count
                                   FROM Transactions
                                   WHERE {dateColumn} >= @StartDate AND {dateColumn} <= @EndDate
                                   {additionalFilter}
                                   GROUP BY DATE({dateColumn})
                                   ORDER BY Date";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@StartDate", start);
                        cmd.Parameters.AddWithValue("@EndDate", end);
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
            catch { }
            return data;
        }
        private Dictionary<string, int> GetMemberActivityData(DateTime? start = null, DateTime? end = null)
        {
            DateTime startDateToUse = start ?? startDate;
            DateTime endDateToUse = end ?? endDate;
            
            Dictionary<string, int> data = new Dictionary<string, int>();
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    
                    if (!CheckTableExists(conn, "Transactions"))
                        return data;
                    
                    int totalTransactions = Convert.ToInt32(new MySqlCommand("SELECT COUNT(*) FROM Transactions", conn).ExecuteScalar() ?? 0);
                    bool useDateFilter = true;
                    if (totalTransactions > 0)
                    {
                        string countInRangeQuery = @"SELECT COUNT(*) FROM Transactions t
                                                     WHERE t.BorrowDate >= @StartDate AND t.BorrowDate <= @EndDate";
                        using (MySqlCommand countInRangeCmd = new MySqlCommand(countInRangeQuery, conn))
                        {
                            countInRangeCmd.Parameters.AddWithValue("@StartDate", startDateToUse);
                            countInRangeCmd.Parameters.AddWithValue("@EndDate", endDateToUse);
                            if (Convert.ToInt32(countInRangeCmd.ExecuteScalar() ?? 0) == 0)
                                useDateFilter = false;
                        }
                    }
                    
                    string query = @"SELECT COALESCE(m.Type, m.MemberType) as MemberType, COUNT(DISTINCT t.TransactionID) as ActivityCount
                                   FROM Transactions t
                                   INNER JOIN Members m ON t.MemberID = m.MemberID";
                    
                    if (useDateFilter)
                    {
                        query += " WHERE t.BorrowDate >= @StartDate AND t.BorrowDate <= @EndDate";
                    }
                    
                    query += " GROUP BY COALESCE(m.Type, m.MemberType)";
                    
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (useDateFilter)
                        {
                            cmd.Parameters.AddWithValue("@StartDate", startDateToUse);
                            cmd.Parameters.AddWithValue("@EndDate", endDateToUse);
                        }
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
                MessageBox.Show($"Error loading member activity: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return data;
        }
        private Dictionary<string, int> GetBorrowingByCategoryData(DateTime? start = null, DateTime? end = null)
        {
            DateTime startDateToUse = start ?? DateTime.MinValue;
            DateTime endDateToUse = end ?? DateTime.MaxValue;
            
            Dictionary<string, int> data = new Dictionary<string, int>();
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    
                    // Check if required tables exist
                    bool hasTransactions = CheckTableExists(conn, "Transactions");
                    bool hasBooks = CheckTableExists(conn, "Books");
                    
                    if (!hasTransactions || !hasBooks || 
                        !DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "Category") ||
                        !DatabaseSchemaHelper.CheckColumnExists(conn, "Transactions", "BorrowDate"))
                        return data;
                    
                    int totalTransactions = Convert.ToInt32(new MySqlCommand("SELECT COUNT(*) FROM Transactions", conn).ExecuteScalar() ?? 0);
                    if (totalTransactions == 0) return data;
                    
                    bool useDateFilter = startDateToUse != DateTime.MinValue && endDateToUse != DateTime.MaxValue;
                    if (useDateFilter)
                    {
                        string checkDateRangeQuery = "SELECT COUNT(*) FROM Transactions WHERE BorrowDate >= @StartDate AND BorrowDate <= @EndDate";
                        using (MySqlCommand checkCmd = new MySqlCommand(checkDateRangeQuery, conn))
                        {
                            checkCmd.Parameters.AddWithValue("@StartDate", startDateToUse);
                            checkCmd.Parameters.AddWithValue("@EndDate", endDateToUse);
                            if (Convert.ToInt32(checkCmd.ExecuteScalar() ?? 0) == 0)
                                useDateFilter = false;
                        }
                    }
                    
                    string query = useDateFilter
                        ? @"SELECT b.Category, COUNT(*) as BorrowCount
                           FROM Transactions t
                           INNER JOIN Books b ON t.BookID = b.BookID
                           WHERE t.BorrowDate >= @StartDate AND t.BorrowDate <= @EndDate
                           GROUP BY b.Category
                           ORDER BY BorrowCount DESC"
                        : @"SELECT b.Category, COUNT(*) as BorrowCount
                           FROM Transactions t
                           INNER JOIN Books b ON t.BookID = b.BookID
                           GROUP BY b.Category
                           ORDER BY BorrowCount DESC";
                    
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (useDateFilter)
                        {
                            cmd.Parameters.AddWithValue("@StartDate", startDateToUse);
                            cmd.Parameters.AddWithValue("@EndDate", endDateToUse);
                        }
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string category = reader["Category"] == DBNull.Value ? "Others" : reader["Category"].ToString();
                                data[category] = Convert.ToInt32(reader["BorrowCount"]);
                            }
                        }
                    }
                }
            }
            catch { }
            return data;
        }
        private Dictionary<string, object> GetCollectionSummary(DateTime? start = null, DateTime? end = null)
        {
            DateTime startDateToUse = start ?? DateTime.MinValue;
            DateTime endDateToUse = end ?? DateTime.MaxValue;
            
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
                        : "(SELECT COUNT(*) FROM Books)";
                    string dateFilter = "";
                    if (startDateToUse != DateTime.MinValue && endDateToUse != DateTime.MaxValue)
                    {
                        dateFilter = " AND BorrowDate >= @StartDate AND BorrowDate <= @EndDate";
                    }
                    
                    string query = $@"SELECT
                                    (SELECT COUNT(*) FROM Books) as Total,
                                    {availableQuery} as Available,
                                    (SELECT COUNT(*) FROM Transactions WHERE Status = 'Borrowed' AND ReturnDate IS NULL{dateFilter}) as OnLoan";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (startDateToUse != DateTime.MinValue && endDateToUse != DateTime.MaxValue)
                        {
                            cmd.Parameters.AddWithValue("@StartDate", startDateToUse);
                            cmd.Parameters.AddWithValue("@EndDate", endDateToUse);
                        }
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
                MessageBox.Show($"Error loading collection summary: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return summary;
        }
        private Dictionary<string, decimal> GetFinancialSummary(DateTime? start = null, DateTime? end = null)
        {
            DateTime startDateToUse = start ?? DateTime.MinValue;
            DateTime endDateToUse = end ?? DateTime.MaxValue;
            Dictionary<string, decimal> summary = new Dictionary<string, decimal> { ["Collected"] = 0, ["Pending"] = 0, ["Waived"] = 0 };
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    bool hasFinesTable = CheckTableExists(conn, "Fines");
                    if (hasFinesTable && CheckColumnExists(conn, "Fines", "Amount"))
                    {
                        bool hasPaidDate = CheckColumnExists(conn, "Fines", "PaidDate");
                        bool hasWaivedDate = CheckColumnExists(conn, "Fines", "WaivedDate");
                        bool hasCreatedDate = CheckColumnExists(conn, "Fines", "CreatedDate");
                        string dateFilter = (startDateToUse != DateTime.MinValue && endDateToUse != DateTime.MaxValue && hasCreatedDate) ? " AND CreatedDate >= @StartDate AND CreatedDate <= @EndDate" : "";
                        
                        string[] queries = {
                            hasPaidDate ? $"SELECT COALESCE(SUM(Amount), 0) FROM Fines WHERE PaidDate IS NOT NULL{dateFilter}" : $"SELECT COALESCE(SUM(Amount), 0) FROM Fines WHERE Status = 'Paid'{dateFilter}",
                            hasPaidDate && hasWaivedDate ? $"SELECT COALESCE(SUM(Amount), 0) FROM Fines WHERE PaidDate IS NULL AND WaivedDate IS NULL{dateFilter}" : $"SELECT COALESCE(SUM(Amount), 0) FROM Fines WHERE Status = 'Pending'{dateFilter}",
                            hasWaivedDate ? $"SELECT COALESCE(SUM(Amount), 0) FROM Fines WHERE WaivedDate IS NOT NULL{dateFilter}" : $"SELECT COALESCE(SUM(Amount), 0) FROM Fines WHERE Status = 'Waived'{dateFilter}"
                        };
                        string[] keys = { "Collected", "Pending", "Waived" };
                        
                        for (int i = 0; i < 3; i++)
                        {
                            using (MySqlCommand cmd = new MySqlCommand(queries[i], conn))
                            {
                                if (!string.IsNullOrEmpty(dateFilter))
                                {
                                    cmd.Parameters.AddWithValue("@StartDate", startDateToUse);
                                    cmd.Parameters.AddWithValue("@EndDate", endDateToUse);
                                }
                                summary[keys[i]] = Convert.ToDecimal(cmd.ExecuteScalar());
                            }
                        }
                    }
                    else if (CheckColumnExists(conn, "Transactions", "Fine"))
                    {
                        string query = @"SELECT COALESCE(SUM(CASE WHEN ReturnDate IS NOT NULL AND Fine > 0 THEN Fine ELSE 0 END), 0) as Collected,
                                        COALESCE(SUM(CASE WHEN ReturnDate IS NULL AND DueDate < NOW() AND Fine > 0 THEN Fine ELSE 0 END), 0) as Pending, 0 as Waived FROM Transactions";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
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
            catch { }
            return summary;
        }
        private Dictionary<string, object> GetUsageStatistics(DateTime? start = null, DateTime? end = null)
        {
            DateTime startDateToUse = start ?? DateTime.MinValue;
            DateTime endDateToUse = end ?? DateTime.MaxValue;
            
            Dictionary<string, object> stats = new Dictionary<string, object>
            {
                ["DailyVisits"] = 0,
                ["BooksPerMember"] = 0.0,
                ["AvgBorrowingPeriod"] = 0,
                ["CollectionTurnover"] = 0.0
            };
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    
                    if (!CheckTableExists(conn, "Transactions"))
                        return stats;
                    
                    int totalTransactions = Convert.ToInt32(new MySqlCommand("SELECT COUNT(*) FROM Transactions", conn).ExecuteScalar() ?? 0);
                    bool useDateFilter = startDateToUse != DateTime.MinValue && endDateToUse != DateTime.MaxValue;
                    if (useDateFilter && totalTransactions > 0)
                    {
                        string countInRangeQuery = "SELECT COUNT(*) FROM Transactions WHERE BorrowDate >= @StartDate AND BorrowDate <= @EndDate";
                        using (MySqlCommand countInRangeCmd = new MySqlCommand(countInRangeQuery, conn))
                        {
                            countInRangeCmd.Parameters.AddWithValue("@StartDate", startDateToUse);
                            countInRangeCmd.Parameters.AddWithValue("@EndDate", endDateToUse);
                            if (Convert.ToInt32(countInRangeCmd.ExecuteScalar() ?? 0) == 0)
                                useDateFilter = false;
                        }
                    }
                    
                    string dateFilter = useDateFilter ? " AND t.BorrowDate >= @StartDate AND t.BorrowDate <= @EndDate" : "";
                    string query = $@"SELECT
                                    (SELECT COUNT(DISTINCT DATE(t.BorrowDate)) FROM Transactions t WHERE 1=1 {dateFilter}) as DailyVisits,
                                    (SELECT COUNT(*) FROM Transactions t WHERE 1=1 {dateFilter}) / GREATEST((SELECT COUNT(*) FROM Members), 1) as BooksPerMember,
                                    (SELECT AVG(DATEDIFF(COALESCE(t.ReturnDate, NOW()), t.BorrowDate)) FROM Transactions t WHERE t.ReturnDate IS NOT NULL {dateFilter}) as AvgBorrowingPeriod,
                                    (SELECT (COUNT(*) * 100.0 / GREATEST((SELECT COUNT(*) FROM Books), 1)) FROM Transactions t WHERE 1=1 {dateFilter}) as CollectionTurnover";
                    
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (useDateFilter)
                        {
                            cmd.Parameters.AddWithValue("@StartDate", startDateToUse);
                            cmd.Parameters.AddWithValue("@EndDate", endDateToUse);
                        }
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                stats["DailyVisits"] = reader["DailyVisits"] != DBNull.Value ? Convert.ToInt32(reader["DailyVisits"]) : 0;
                                stats["BooksPerMember"] = reader["BooksPerMember"] != DBNull.Value ? Convert.ToDouble(reader["BooksPerMember"]) : 0.0;
                                stats["AvgBorrowingPeriod"] = reader["AvgBorrowingPeriod"] != DBNull.Value ? Convert.ToInt32(reader["AvgBorrowingPeriod"]) : 0;
                                stats["CollectionTurnover"] = reader["CollectionTurnover"] != DBNull.Value ? Convert.ToDouble(reader["CollectionTurnover"]) : 0.0;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError($"Error loading usage statistics: {ex.Message}", "Error", ex);
            }
            return stats;
        }
        
        private void LoadOverdueBooksReportForCirculation(DataGridView dgv, DateTime? start = null, DateTime? end = null)
        {
            DateTime startDateToUse = start ?? DateTime.MinValue;
            DateTime endDateToUse = end ?? DateTime.MaxValue;
            string[] columns = { "MEMBER ID", "MEMBER NAME", "OVERDUE BOOKS", "DAYS OVERDUE", "FINE AMOUNT" };
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    if (!CheckTableExists(conn, "Transactions") || !CheckTableExists(conn, "Members") ||
                        !DatabaseSchemaHelper.CheckColumnExists(conn, "Transactions", "DueDate") ||
                        !DatabaseSchemaHelper.CheckColumnExists(conn, "Transactions", "BorrowDate"))
                    {
                        CreateEmptyDataTable(dgv, columns);
                        return;
                    }
                    
                    int totalTransactions = Convert.ToInt32(new MySqlCommand("SELECT COUNT(*) FROM Transactions", conn).ExecuteScalar() ?? 0);
                    if (totalTransactions == 0)
                    {
                        CreateEmptyDataTable(dgv, columns);
                        return;
                    }
                    
                    bool useDateFilter = startDateToUse != DateTime.MinValue && endDateToUse != DateTime.MaxValue;
                    if (useDateFilter)
                    {
                        string checkDateRangeQuery = "SELECT COUNT(*) FROM Transactions WHERE BorrowDate >= @StartDate AND BorrowDate <= @EndDate";
                        using (MySqlCommand checkCmd = new MySqlCommand(checkDateRangeQuery, conn))
                        {
                            checkCmd.Parameters.AddWithValue("@StartDate", startDateToUse);
                            checkCmd.Parameters.AddWithValue("@EndDate", endDateToUse);
                            if (Convert.ToInt32(checkCmd.ExecuteScalar() ?? 0) == 0)
                                useDateFilter = false;
                        }
                    }
                    
                    bool hasStatus = DatabaseSchemaHelper.CheckColumnExists(conn, "Transactions", "Status");
                    bool hasFine = DatabaseSchemaHelper.CheckColumnExists(conn, "Transactions", "Fine");
                    string statusCondition = hasStatus ? "(t.Status = 'Borrowed' OR t.ReturnDate IS NULL)" : "t.ReturnDate IS NULL";
                    string fineColumn = hasFine ? "COALESCE(SUM(t.Fine), 0)" : "0";
                    string dateFilter = useDateFilter ? " AND t.BorrowDate >= @StartDate AND t.BorrowDate <= @EndDate" : "";
                    
                    string query = $@"SELECT CONCAT('MEM-', LPAD(m.MemberID, 3, '0')) as 'MEMBER ID',
                                    CONCAT(m.FirstName, ' ', m.LastName) as 'MEMBER NAME',
                                    COUNT(*) as 'OVERDUE BOOKS',
                                    MAX(DATEDIFF(NOW(), t.DueDate)) as 'DAYS OVERDUE',
                                    {fineColumn} as 'FINE AMOUNT'
                                    FROM Transactions t INNER JOIN Members m ON t.MemberID = m.MemberID
                                    WHERE {statusCondition} AND t.DueDate < NOW(){dateFilter}
                                    GROUP BY m.MemberID, m.FirstName, m.LastName
                                    ORDER BY MAX(DATEDIFF(NOW(), t.DueDate)) DESC LIMIT {Constants.DefaultQueryLimit}";
                    
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (useDateFilter)
                        {
                            cmd.Parameters.AddWithValue("@StartDate", startDateToUse);
                            cmd.Parameters.AddWithValue("@EndDate", endDateToUse);
                        }
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dt = ConvertToStringDataTable(dt);
                            if (dt.Rows.Count == 0)
                                CreateEmptyDataTable(dgv, columns);
                            else
                            {
                                dgv.DataSource = dt;
                                SetupDataGridView(dgv, dt, new Dictionary<string, (int width, DataGridViewAutoSizeColumnMode mode, DataGridViewContentAlignment? alignment)>
                                {
                                    { "MEMBER ID", (120, DataGridViewAutoSizeColumnMode.None, null) },
                                    { "MEMBER NAME", (0, DataGridViewAutoSizeColumnMode.Fill, null) },
                                    { "OVERDUE BOOKS", (130, DataGridViewAutoSizeColumnMode.None, DataGridViewContentAlignment.MiddleCenter) },
                                    { "DAYS OVERDUE", (130, DataGridViewAutoSizeColumnMode.None, DataGridViewContentAlignment.MiddleCenter) },
                                    { "FINE AMOUNT", (130, DataGridViewAutoSizeColumnMode.None, DataGridViewContentAlignment.MiddleRight) }
                                });
                                if (dgv.Columns.Contains("FINE AMOUNT"))
                                {
                                    dgv.CellFormatting += (s, e) =>
                                    {
                                        if (e.ColumnIndex >= 0 && dgv.Columns[e.ColumnIndex].Name == "FINE AMOUNT" && e.Value != null && e.Value != DBNull.Value && !string.IsNullOrEmpty(e.Value.ToString()) && decimal.TryParse(e.Value.ToString().Replace("₱", "").Replace("$", "").Trim(), out decimal amount))
                                        {
                                            e.Value = IDFormatter.FormatCurrency(amount);
                                            e.FormattingApplied = true;
                                        }
                                    };
                                    dgv.Columns["FINE AMOUNT"].DefaultCellStyle.ForeColor = Color.Red;
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                CreateEmptyDataTable(dgv, columns);
            }
        }
        // Use DatabaseSchemaHelper instead of duplicate methods
        private bool CheckTableExists(MySqlConnection conn, string tableName)
        {
            return DatabaseSchemaHelper.CheckTableExists(conn, tableName);
        }
        private bool CheckColumnExists(MySqlConnection conn, string tableName, string columnName)
        {
            return DatabaseSchemaHelper.CheckColumnExists(conn, tableName, columnName);
        }
        private void btnExportReport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx|CSV files (*.csv)|*.csv|All files (*.*)|*.*",
                FileName = $"{currentReportType}Report_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
            };
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Panel currentPanel = GetCurrentPanel();
                    if (currentPanel == null)
                    {
                        MessageBox.Show("No data available to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string filePath = saveDialog.FileName;
                    string extension = System.IO.Path.GetExtension(filePath).ToLower();
                    
                    bool hasData = false;
                    
                    // Handle different report types
                    if (extension == ".xlsx")
                    {
                        // Export to Excel with charts
                        hasData = ExportToExcel(currentPanel, filePath);
                    }
                    else
                    {
                        // Export to CSV (legacy format)
                        System.IO.StreamWriter sw = new System.IO.StreamWriter(filePath, false, System.Text.Encoding.UTF8);
                        
                        // Write report header
                        sw.WriteLine($"{currentReportType} Report");
                        sw.WriteLine($"Generated on: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                        sw.WriteLine();
                        
                        switch (currentReportType)
                        {
                            case "Circulation":
                                hasData = ExportCirculationReport(sw, currentPanel, filePath);
                                break;
                            case "Member":
                                hasData = ExportMemberReport(sw, currentPanel, filePath);
                                break;
                            case "Collection":
                                hasData = ExportCollectionReport(sw, currentPanel);
                                break;
                            case "Financial":
                                hasData = ExportFinancialReport(sw, currentPanel);
                                break;
                            case "Statistical":
                                hasData = ExportStatisticalReport(sw, currentPanel);
                                break;
                        }
                        
                        sw.Close();
                    }
                    
                    if (hasData)
                    {
                        ErrorHandler.ShowSuccess("Report exported successfully!", "Export");
                    }
                    else
                    {
                        if (System.IO.File.Exists(filePath))
                            System.IO.File.Delete(filePath);
                        ErrorHandler.ShowWarning("No data available to export.", "Export");
                    }
                }
                catch (Exception ex)
                {
                    ErrorHandler.ShowError($"Error exporting report: {ex.Message}", "Error", ex);
                }
            }
        }
        
        private bool ExportToExcel(Panel panel, string filePath)
        {
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    bool hasData = false;
                    
                    switch (currentReportType)
                    {
                        case "Circulation":
                            hasData = ExportCirculationToExcel(workbook, panel);
                            break;
                        case "Member":
                            hasData = ExportMemberToExcel(workbook, panel);
                            break;
                        case "Collection":
                            hasData = ExportCollectionToExcel(workbook, panel);
                            break;
                        case "Financial":
                            hasData = ExportFinancialToExcel(workbook, panel);
                            break;
                        case "Statistical":
                            hasData = ExportStatisticalToExcel(workbook, panel);
                            break;
                    }
                    
                    if (hasData)
                    {
                        // Save workbook
                        workbook.SaveAs(filePath);
                        return true;
                    }
                }
            }
            catch { throw; }
            
            return false;
        }
        
        private bool ExportCirculationToExcel(XLWorkbook workbook, Panel panel)
        {
            return ExportDataGridViewsToExcel(workbook, panel);
        }
        
        private bool ExportMemberToExcel(XLWorkbook workbook, Panel panel)
        {
            return ExportDataGridViewsToExcel(workbook, panel);
        }
        
        private bool ExportDataGridViewsToExcel(XLWorkbook workbook, Panel panel)
        {
            bool hasData = false;
            List<DataGridView> dataGridViews = new List<DataGridView>();
            ReportHelper.FindDataGridViews(panel, dataGridViews);
            
            foreach (DataGridView dgv in dataGridViews)
            {
                if (dgv.DataSource != null && dgv.Rows.Count > 0)
                {
                    DataTable dt = dgv.DataSource as DataTable;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        hasData = true;
                        string sheetName = ReportHelper.GetDataGridViewSectionName(dgv);
                        sheetName = TextHelper.TruncateText(sheetName, 31);
                        sheetName = sheetName.Replace("/", "-").Replace("\\", "-").Replace("?", "").Replace("*", "").Replace("[", "").Replace("]", "").Replace(":", "");
                        
                        var ws = workbook.Worksheets.Add(sheetName);
                        for (int col = 0; col < dt.Columns.Count; col++)
                        {
                            ws.Cell(1, col + 1).Value = dt.Columns[col].ColumnName;
                            ws.Cell(1, col + 1).Style.Font.Bold = true;
                            ws.Cell(1, col + 1).Style.Fill.BackgroundColor = XLColor.LightGray;
                        }
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            for (int col = 0; col < dt.Columns.Count; col++)
                                ws.Cell(i + 2, col + 1).Value = dt.Rows[i][col]?.ToString() ?? "";
                        }
                        ws.Columns().AdjustToContents();
                    }
                }
            }
            return hasData;
        }
        
        private bool ExportCollectionToExcel(XLWorkbook workbook, Panel panel)
        {
            return ExportSummaryToExcel(workbook, "Collection Summary", new[] {
                ("Total Books", lblCollectionCardTotalValue?.Text),
                ("Available", lblCollectionCardAvailableValue?.Text),
                ("On Loan", lblCollectionCardOnLoanValue?.Text)
            });
        }
        
        private bool ExportFinancialToExcel(XLWorkbook workbook, Panel panel)
        {
            return ExportSummaryToExcel(workbook, "Financial Summary", new[] {
                ("Fines Collected", lblFinancialCardCollectedValue?.Text),
                ("Pending Fines", lblFinancialCardPendingValue?.Text),
                ("Waived Fines", lblFinancialCardWaivedValue?.Text)
            });
        }
        
        private bool ExportStatisticalToExcel(XLWorkbook workbook, Panel panel)
        {
            return ExportSummaryToExcel(workbook, "Library Usage Statistics", new[] {
                ("Daily Average Visits", lblStatisticalCardVisitsValue?.Text),
                ("Books Per Member", lblStatisticalCardBooksPerMemberValue?.Text),
                ("Avg. Borrowing Period", lblStatisticalCardBorrowingPeriodValue?.Text),
                ("Collection Turnover", lblStatisticalCardTurnoverValue?.Text)
            });
        }
        
        private bool ExportSummaryToExcel(XLWorkbook workbook, string sheetName, (string metric, string value)[] metrics)
        {
            bool hasData = false;
            var ws = workbook.Worksheets.Add(sheetName);
            ws.Cell(1, 1).Value = "Metric";
            ws.Cell(1, 2).Value = "Value";
            ws.Cell(1, 1).Style.Font.Bold = true;
            ws.Cell(1, 2).Style.Font.Bold = true;
            ws.Cell(1, 1).Style.Fill.BackgroundColor = XLColor.LightGray;
            ws.Cell(1, 2).Style.Fill.BackgroundColor = XLColor.LightGray;
            
            int row = 2;
            foreach (var (metric, value) in metrics)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    ws.Cell(row, 1).Value = metric;
                    ws.Cell(row, 2).Value = value;
                    row++;
                    hasData = true;
                }
            }
            ws.Columns().AdjustToContents();
            return hasData;
        }
        
        private bool ExportCirculationReport(System.IO.StreamWriter sw, Panel panel, string filePath)
        {
            return ExportDataGridViewsToCsv(sw, panel);
        }
        
        private bool ExportMemberReport(System.IO.StreamWriter sw, Panel panel, string filePath)
        {
            return ExportDataGridViewsToCsv(sw, panel);
        }
        
        private bool ExportDataGridViewsToCsv(System.IO.StreamWriter sw, Panel panel)
        {
            bool hasData = false;
            List<DataGridView> dataGridViews = new List<DataGridView>();
            ReportHelper.FindDataGridViews(panel, dataGridViews);
            
            foreach (DataGridView dgv in dataGridViews)
            {
                if (dgv.DataSource != null && dgv.Rows.Count > 0)
                {
                    DataTable dt = dgv.DataSource as DataTable;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        hasData = true;
                        sw.WriteLine($"{ReportHelper.GetDataGridViewSectionName(dgv)}");
                        sw.WriteLine();
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            sw.Write(EscapeCsvField(dt.Columns[i].ColumnName));
                            if (i < dt.Columns.Count - 1) sw.Write(",");
                        }
                        sw.WriteLine();
                        foreach (DataRow row in dt.Rows)
                        {
                            for (int i = 0; i < dt.Columns.Count; i++)
                            {
                                sw.Write(EscapeCsvField(row[i]?.ToString() ?? ""));
                                if (i < dt.Columns.Count - 1) sw.Write(",");
                            }
                            sw.WriteLine();
                        }
                        sw.WriteLine();
                    }
                }
            }
            return hasData;
        }
        
        private bool ExportCollectionReport(System.IO.StreamWriter sw, Panel panel)
        {
            bool hasData = ExportSummaryToCsv(sw, "Collection Summary", "Collection", new[] {
                ("Total Books", lblCollectionCardTotalValue?.Text),
                ("Available", lblCollectionCardAvailableValue?.Text),
                ("On Loan", lblCollectionCardOnLoanValue?.Text)
            });
            
            List<DataGridView> dataGridViews = new List<DataGridView>();
            ReportHelper.FindDataGridViews(panel, dataGridViews);
            foreach (DataGridView dgv in dataGridViews)
            {
                if (dgv.DataSource != null && dgv.Rows.Count > 0)
                {
                    DataTable dt = dgv.DataSource as DataTable;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        hasData = true;
                        sw.WriteLine();
                        sw.WriteLine("Shelf List");
                        sw.WriteLine();
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            sw.Write(EscapeCsvField(dt.Columns[i].ColumnName));
                            if (i < dt.Columns.Count - 1) sw.Write(",");
                        }
                        sw.WriteLine();
                        foreach (DataRow row in dt.Rows)
                        {
                            for (int i = 0; i < dt.Columns.Count; i++)
                            {
                                sw.Write(EscapeCsvField(row[i]?.ToString() ?? ""));
                                if (i < dt.Columns.Count - 1) sw.Write(",");
                            }
                            sw.WriteLine();
                        }
                    }
                }
            }
            return hasData;
        }
        
        private bool ExportFinancialReport(System.IO.StreamWriter sw, Panel panel)
        {
            return ExportSummaryToCsv(sw, "Financial Summary", "Financial", new[] {
                ("Fines Collected", lblFinancialCardCollectedValue?.Text),
                ("Pending Fines", lblFinancialCardPendingValue?.Text),
                ("Waived Fines", lblFinancialCardWaivedValue?.Text)
            });
        }
        
        private bool ExportStatisticalReport(System.IO.StreamWriter sw, Panel panel)
        {
            return ExportSummaryToCsv(sw, "Library Usage Statistics", "Statistical", new[] {
                ("Daily Average Visits", lblStatisticalCardVisitsValue?.Text),
                ("Books Per Member", lblStatisticalCardBooksPerMemberValue?.Text),
                ("Avg. Borrowing Period", lblStatisticalCardBorrowingPeriodValue?.Text),
                ("Collection Turnover", lblStatisticalCardTurnoverValue?.Text)
            });
        }
        
        private bool ExportSummaryToCsv(System.IO.StreamWriter sw, string sectionTitle, string panelName, (string metric, string value)[] metrics)
        {
            sw.WriteLine(sectionTitle);
            sw.WriteLine();
            sw.WriteLine("Metric,Value");
            
            bool hasData = false;
            FlowLayoutPanel summaryPanel = ReportHelper.FindFlowLayoutPanel(GetCurrentPanel(), panelName);
            
            if (summaryPanel != null)
            {
                foreach (Control card in summaryPanel.Controls)
                {
                    if (card is Panel cardPanel)
                    {
                        var (title, value) = ReportHelper.ExtractCardData(cardPanel);
                        if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(value))
                        {
                            hasData = true;
                            sw.WriteLine($"{EscapeCsvField(title)},{EscapeCsvField(value)}");
                        }
                    }
                }
            }
            
            if (!hasData)
            {
                foreach (var (metric, value) in metrics)
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        hasData = true;
                        sw.WriteLine($"{metric},{EscapeCsvField(value)}");
                    }
                }
            }
            return hasData;
        }
        
        private FlowLayoutPanel FindFlowLayoutPanel(string panelName)
        {
            foreach (Control ctrl in GetCurrentPanel().Controls)
            {
                if (ctrl is FlowLayoutPanel flowPanel && flowPanel.Name.Contains(panelName))
                    return flowPanel;
                if (ctrl is Panel mainPanel)
                {
                    foreach (Control subCtrl in mainPanel.Controls)
                    {
                        if (subCtrl is FlowLayoutPanel flowPanel2 && flowPanel2.Name.Contains(panelName))
                            return flowPanel2;
                    }
                }
            }
            return null;
        }
        
        // Use ReportHelper for card data extraction and DataGridView operations
        private (string title, string value) ExtractCardData(Panel cardPanel) => ReportHelper.ExtractCardData(cardPanel);
        private void FindDataGridViews(Control parent, List<DataGridView> dataGridViews) => ReportHelper.FindDataGridViews(parent, dataGridViews);
        private string GetDataGridViewSectionName(DataGridView dgv) => ReportHelper.GetDataGridViewSectionName(dgv);
        
        // Use ReportHelper for CSV escaping and control finding
        private string EscapeCsvField(string field) => ReportHelper.EscapeCsvField(field);
        private T FindControl<T>(Control parent, string name) where T : Control => ReportHelper.FindControl<T>(parent, name);
        
    }
}