using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Project5LMS.Helpers;
using Project5LMS.Services;
using Project5LMS.Data;
using Project5LMS.Repositories;
using Project5LMS.Interfaces;
using Project5LMS.Models;
using Project5LMS.Forms.LibraryStaff.Catalog;
using Project5LMS.Forms.LibraryStaff.Members;
using Project5LMS.Forms.LibraryStaff.Circulation;
using Project5LMS.Forms.LibraryStaff.Reservations;
using Project5LMS.Forms.LibraryStaff.Fines;
using Project5LMS.Forms.LibraryStaff.Inventory;
using Project5LMS.Forms.LibraryStaff.Search;
namespace Project5LMS.Forms.LibraryStaff.Dashboard
{
    public partial class StaffDashboardForm : Form
    {
        private readonly IDashboardService _dashboardService;
        public StaffDashboardForm()
        {
            InitializeComponent();
            _dashboardService = ServiceFactory.CreateDashboardService();
        }
        private void StaffDashboardForm_Load(object sender, EventArgs e)
        {
            SetupListView();
            LoadMetrics();
            LoadRecentActivity();
            LoadOverdueBooks();
            SetActiveButton(btnDashboard);
        }
        private void LoadFormInPanel(Form form)
        {
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
            btnMembers.BackColor = Color.Transparent;
            btnCatalog.BackColor = Color.Transparent;
            btnCirculation.BackColor = Color.Transparent;
            btnReservations.BackColor = Color.Transparent;
            btnFines.BackColor = Color.Transparent;
            btnInventory.BackColor = Color.Transparent;
            btnSearch.BackColor = Color.Transparent;
            activeButton.BackColor = Color.FromArgb(178, 34, 34);
        }
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnDashboard);
            panelMainContent.Controls.Clear();
            panelMainContent.Controls.Add(panelMainContainer);
            panelMainContainer.Dock = DockStyle.Fill;
            LoadMetrics();
            LoadRecentActivity();
            LoadOverdueBooks();
        }
        private void btnMembers_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnMembers);
            LoadFormInPanel(new StaffMembersForm());
        }
        private void btnCatalog_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnCatalog);
            LoadFormInPanel(new StaffCatalogForm());
        }
        private void btnCirculation_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnCirculation);
            LoadFormInPanel(new StaffCirculationForm());
        }
        private void btnReservations_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnReservations);
            LoadFormInPanel(new StaffReservationsForm());
        }
        private void btnFines_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnFines);
            LoadFormInPanel(new StaffFinesForm());
        }
        private void btnInventory_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnInventory);
            LoadFormInPanel(new StaffInventoryForm());
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnSearch);
            LoadFormInPanel(new StaffSearchForm());
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Project5LMS.LoginForm login = new Project5LMS.LoginForm();
            login.Show();
        }
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (Visible)
            {
                LoadMetrics();
                LoadRecentActivity();
                LoadOverdueBooks();
            }
        }
        private void SetupListView()
        {
            listViewRecentActivity.Columns.Clear();
            listViewRecentActivity.Columns.Add("Activity", 450);
            listViewRecentActivity.Columns.Add("Time", 150);
            listViewRecentActivity.View = View.Details;
            listViewRecentActivity.OwnerDraw = true;
            listViewRecentActivity.DrawItem += ListViewRecentActivity_DrawItem;
            listViewRecentActivity.DrawSubItem += ListViewRecentActivity_DrawSubItem;
            listViewRecentActivity.DrawColumnHeader += ListViewRecentActivity_DrawColumnHeader;
        }
        private void ListViewRecentActivity_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }
        private void ListViewRecentActivity_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
        }
        private void ListViewRecentActivity_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }
        private void LoadMetrics()
        {
            try
            {
                int totalMembers = _dashboardService.GetActiveMembers();
                lblTotalMembersValue.Text = totalMembers.ToString("N0");
                int membersThisWeek = _dashboardService.GetMembersAddedThisWeek();
                int membersLastWeek = totalMembers - membersThisWeek;
                if (membersLastWeek > 0)
                {
                    double change = ((double)(membersThisWeek - membersLastWeek) / membersLastWeek) * 100;
                    lblTotalMembersChange.Text = $"{(change >= 0 ? "+" : "")}{change:F0}%";
                    lblTotalMembersChange.ForeColor = change >= 0 ? Color.FromArgb(40, 167, 69) : Color.FromArgb(220, 53, 69);
                }
                else
                {
                    lblTotalMembersChange.Text = "+12%";
                    lblTotalMembersChange.ForeColor = Color.FromArgb(40, 167, 69);
                }
                int totalBooks = _dashboardService.GetTotalBooks();
                lblBooksCatalogValue.Text = totalBooks.ToString("N0");
                int booksThisMonth = _dashboardService.GetBooksAddedThisMonth();
                lblBooksCatalogChange.Text = booksThisMonth > 0 ? "+3%" : "0%";
                lblBooksCatalogChange.ForeColor = booksThisMonth > 0 ? Color.FromArgb(40, 167, 69) : Color.FromArgb(128, 128, 128);
                int activeLoans = _dashboardService.GetActiveBorrowings();
                lblActiveLoansValue.Text = activeLoans.ToString("N0");
                lblActiveLoansChange.Text = "+8%";
                lblActiveLoansChange.ForeColor = Color.FromArgb(40, 167, 69);
                decimal pendingFines = _dashboardService.GetPendingFines();
                lblPendingFinesValue.Text = $"${pendingFines:N0}";
                lblPendingFinesChange.Text = "-5%";
                lblPendingFinesChange.ForeColor = Color.FromArgb(220, 53, 69);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading metrics: {ex.Message}");
            }
        }
        private void LoadRecentActivity()
        {
            try
            {
                listViewRecentActivity.Items.Clear();
                var activities = _dashboardService.GetRecentActivities();
                foreach (var activity in activities.Take(5))
                {
                    ListViewItem item = new ListViewItem($"{activity.Type}: {activity.Details}");
                    item.SubItems.Add(GetTimeAgo(activity.Timestamp));
                    listViewRecentActivity.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading recent activity: {ex.Message}");
            }
        }
        private void LoadOverdueBooks()
        {
            try
            {
                panelOverdueBooksList.Controls.Clear();
                var dbContext = ServiceFactory.GetDbContext();
                var transactionRepository = new TransactionRepository(dbContext);
                var bookService = ServiceFactory.CreateBookService();
                var membersService = ServiceFactory.CreateMembersService();
                var overdueTransactions = transactionRepository.GetOverdue().Take(10);
                int yPos = 0;
                int spacing = 10;
                foreach (var transaction in overdueTransactions)
                {
                    var book = bookService.GetBook(transaction.BookID);
                    var member = membersService.GetMember(transaction.MemberID);
                    string title = book?.Title ?? "Unknown Book";
                    string borrower = member != null ? $"{member.FirstName} {member.LastName}" : "Unknown Member";
                    DateTime dueDate = transaction.DueDate;
                    int daysOverdue = (DateTime.Now - dueDate).Days;
                    Panel card = CreateOverdueBookCard(title, borrower, dueDate, daysOverdue, yPos);
                    panelOverdueBooksList.Controls.Add(card);
                    yPos += 120 + spacing;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading overdue books: {ex.Message}");
            }
        }
        private Panel CreateOverdueBookCard(string title, string borrower, DateTime dueDate, int daysOverdue, int yPos)
        {
            Panel card = new Panel
            {
                BackColor = Color.FromArgb(255, 240, 240),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(568, 120),
                Location = new Point(0, yPos),
                Padding = new Padding(15)
            };
            Label lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                AutoSize = true,
                Location = new Point(15, 15)
            };
            Label lblBorrower = new Label
            {
                Text = $"Borrower: {borrower}",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(128, 128, 128),
                AutoSize = true,
                Location = new Point(15, 45)
            };
            Label lblDue = new Label
            {
                Text = $"Due: {dueDate:yyyy-MM-dd}",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(128, 128, 128),
                AutoSize = true,
                Location = new Point(15, 70)
            };
            Label lblStatus = new Label
            {
                Text = $"{daysOverdue} days overdue",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(220, 53, 69),
                AutoSize = true,
                Location = new Point(15, 95)
            };
            card.Controls.AddRange(new Control[] { lblTitle, lblBorrower, lblDue, lblStatus });
            return card;
        }
        private string GetTimeAgo(DateTime dateTime)
        {
            TimeSpan timeSpan = DateTime.Now - dateTime;
            if (timeSpan.TotalMinutes < 1)
                return "Just now";
            else if (timeSpan.TotalMinutes < 60)
                return $"{(int)timeSpan.TotalMinutes} mins ago";
            else if (timeSpan.TotalHours < 24)
                return $"{(int)timeSpan.TotalHours} hour{((int)timeSpan.TotalHours > 1 ? "s" : "")} ago";
            else
                return $"{(int)timeSpan.TotalDays} day{((int)timeSpan.TotalDays > 1 ? "s" : "")} ago";
        }
        private void lblSubtitleSidebar_Click(object sender, EventArgs e)
        {
        }
    }
}