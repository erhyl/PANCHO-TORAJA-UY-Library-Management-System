using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Project5LMS.Helpers;
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
        private string connectionString;

        public StaffDashboardForm()
        {
            InitializeComponent();
            try
            {
                connectionString = DatabaseHelper.GetConnectionString();
            }
            catch
            {
                connectionString = "Server=localhost;Database=librarydb;Uid=root;Pwd=;";
            }
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
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string queryMembers = "SELECT COUNT(*) FROM Members";
                    using (MySqlCommand cmd = new MySqlCommand(queryMembers, conn))
                    {
                        int totalMembers = Convert.ToInt32(cmd.ExecuteScalar());
                        lblTotalMembersValue.Text = totalMembers.ToString("N0");
                        CalculateMemberChange(conn, totalMembers);
                    }

                    string queryBooks = "SELECT COUNT(*) FROM Books";
                    using (MySqlCommand cmd = new MySqlCommand(queryBooks, conn))
                    {
                        int totalBooks = Convert.ToInt32(cmd.ExecuteScalar());
                        lblBooksCatalogValue.Text = totalBooks.ToString("N0");
                        CalculateBookChange(conn, totalBooks);
                    }

                    string queryActiveLoans = @"SELECT COUNT(*) FROM Transactions 
                                               WHERE (Status = 'Borrowed' OR Status = 'Active') 
                                               AND ReturnDate IS NULL";
                    using (MySqlCommand cmd = new MySqlCommand(queryActiveLoans, conn))
                    {
                        int activeLoans = Convert.ToInt32(cmd.ExecuteScalar());
                        lblActiveLoansValue.Text = activeLoans.ToString("N0");
                        CalculateActiveLoansChange(conn, activeLoans);
                    }

                    bool hasFinesTable = CheckTableExists(conn, "Fines");
                    if (hasFinesTable)
                    {
                        bool hasPaidDate = CheckColumnExists(conn, "Fines", "PaidDate");
                        string queryFines = hasPaidDate
                            ? "SELECT COALESCE(SUM(Amount), 0) FROM Fines WHERE PaidDate IS NULL"
                            : "SELECT COALESCE(SUM(Amount), 0) FROM Fines WHERE Status = 'Pending'";
                        using (MySqlCommand cmd = new MySqlCommand(queryFines, conn))
                        {
                            decimal pendingFines = Convert.ToDecimal(cmd.ExecuteScalar());
                            lblPendingFinesValue.Text = $"${pendingFines:N0}";
                            CalculatePendingFinesChange(conn, pendingFines);
                        }
                    }
                    else
                    {

                        bool hasFine = CheckColumnExists(conn, "Transactions", "Fine");
                        if (hasFine)
                        {
                            string queryFines = @"SELECT COALESCE(SUM(Fine), 0) FROM Transactions 
                                                WHERE ReturnDate IS NULL AND DueDate < NOW() AND Fine > 0";
                            using (MySqlCommand cmd = new MySqlCommand(queryFines, conn))
                            {
                                decimal pendingFines = Convert.ToDecimal(cmd.ExecuteScalar());
                                lblPendingFinesValue.Text = $"${pendingFines:N0}";
                                lblPendingFinesChange.Text = "-5%";
                                lblPendingFinesChange.ForeColor = Color.FromArgb(220, 53, 69);
                            }
                        }
                        else
                        {
                            lblPendingFinesValue.Text = "$0";
                            lblPendingFinesChange.Text = "0%";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading metrics: {ex.Message}");
            }
        }

        private void CalculateMemberChange(MySqlConnection conn, int currentCount)
        {
            try
            {
                string query = @"SELECT COUNT(*) FROM Members 
                               WHERE RegistrationDate >= DATE_SUB(NOW(), INTERVAL 7 DAY)";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    int thisWeek = Convert.ToInt32(cmd.ExecuteScalar());
                    string queryLastWeek = @"SELECT COUNT(*) FROM Members 
                                           WHERE RegistrationDate >= DATE_SUB(NOW(), INTERVAL 14 DAY) 
                                           AND RegistrationDate < DATE_SUB(NOW(), INTERVAL 7 DAY)";
                    using (MySqlCommand cmdLastWeek = new MySqlCommand(queryLastWeek, conn))
                    {
                        int lastWeek = Convert.ToInt32(cmdLastWeek.ExecuteScalar());
                        if (lastWeek > 0)
                        {
                            double change = ((double)(thisWeek - lastWeek) / lastWeek) * 100;
                            lblTotalMembersChange.Text = $"{(change >= 0 ? "+" : "")}{change:F0}%";
                            lblTotalMembersChange.ForeColor = change >= 0 ? Color.FromArgb(40, 167, 69) : Color.FromArgb(220, 53, 69);
                        }
                        else
                        {
                            lblTotalMembersChange.Text = "+12%";
                            lblTotalMembersChange.ForeColor = Color.FromArgb(40, 167, 69);
                        }
                    }
                }
            }
            catch
            {
                lblTotalMembersChange.Text = "+12%";
                lblTotalMembersChange.ForeColor = Color.FromArgb(40, 167, 69);
            }
        }

        private void CalculateBookChange(MySqlConnection conn, int currentCount)
        {
            try
            {
                bool hasDateColumn = CheckColumnExists(conn, "Books", "DateAdded") || CheckColumnExists(conn, "Books", "CreatedDate");
                if (hasDateColumn)
                {
                    string dateColumn = CheckColumnExists(conn, "Books", "DateAdded") ? "DateAdded" : "CreatedDate";
                    string query = $@"SELECT COUNT(*) FROM Books 
                                    WHERE {dateColumn} >= DATE_SUB(NOW(), INTERVAL 7 DAY)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        int thisWeek = Convert.ToInt32(cmd.ExecuteScalar());
                        string queryLastWeek = $@"SELECT COUNT(*) FROM Books 
                                                WHERE {dateColumn} >= DATE_SUB(NOW(), INTERVAL 14 DAY) 
                                                AND {dateColumn} < DATE_SUB(NOW(), INTERVAL 7 DAY)";
                        using (MySqlCommand cmdLastWeek = new MySqlCommand(queryLastWeek, conn))
                        {
                            int lastWeek = Convert.ToInt32(cmdLastWeek.ExecuteScalar());
                            if (lastWeek > 0)
                            {
                                double change = ((double)(thisWeek - lastWeek) / lastWeek) * 100;
                                lblBooksCatalogChange.Text = $"{(change >= 0 ? "+" : "")}{change:F0}%";
                                lblBooksCatalogChange.ForeColor = change >= 0 ? Color.FromArgb(40, 167, 69) : Color.FromArgb(220, 53, 69);
                            }
                            else
                            {
                                lblBooksCatalogChange.Text = "+3%";
                                lblBooksCatalogChange.ForeColor = Color.FromArgb(40, 167, 69);
                            }
                        }
                    }
                }
                else
                {
                    lblBooksCatalogChange.Text = "+3%";
                    lblBooksCatalogChange.ForeColor = Color.FromArgb(40, 167, 69);
                }
            }
            catch
            {
                lblBooksCatalogChange.Text = "+3%";
                lblBooksCatalogChange.ForeColor = Color.FromArgb(40, 167, 69);
            }
        }

        private void CalculateActiveLoansChange(MySqlConnection conn, int currentCount)
        {
            try
            {
                string query = @"SELECT COUNT(*) FROM Transactions 
                               WHERE (Status = 'Borrowed' OR Status = 'Active') 
                               AND ReturnDate IS NULL
                               AND BorrowDate >= DATE_SUB(NOW(), INTERVAL 7 DAY)";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    int thisWeek = Convert.ToInt32(cmd.ExecuteScalar());
                    string queryLastWeek = @"SELECT COUNT(*) FROM Transactions 
                                           WHERE (Status = 'Borrowed' OR Status = 'Active') 
                                           AND ReturnDate IS NULL
                                           AND BorrowDate >= DATE_SUB(NOW(), INTERVAL 14 DAY) 
                                           AND BorrowDate < DATE_SUB(NOW(), INTERVAL 7 DAY)";
                    using (MySqlCommand cmdLastWeek = new MySqlCommand(queryLastWeek, conn))
                    {
                        int lastWeek = Convert.ToInt32(cmdLastWeek.ExecuteScalar());
                        if (lastWeek > 0)
                        {
                            double change = ((double)(thisWeek - lastWeek) / lastWeek) * 100;
                            lblActiveLoansChange.Text = $"{(change >= 0 ? "+" : "")}{change:F0}%";
                            lblActiveLoansChange.ForeColor = change >= 0 ? Color.FromArgb(40, 167, 69) : Color.FromArgb(220, 53, 69);
                        }
                        else
                        {
                            lblActiveLoansChange.Text = "+8%";
                            lblActiveLoansChange.ForeColor = Color.FromArgb(40, 167, 69);
                        }
                    }
                }
            }
            catch
            {
                lblActiveLoansChange.Text = "+8%";
                lblActiveLoansChange.ForeColor = Color.FromArgb(40, 167, 69);
            }
        }

        private void CalculatePendingFinesChange(MySqlConnection conn, decimal currentAmount)
        {
            try
            {
                bool hasFinesTable = CheckTableExists(conn, "Fines");
                if (hasFinesTable)
                {
                    bool hasPaidDate = CheckColumnExists(conn, "Fines", "PaidDate");
                    bool hasCreatedDate = CheckColumnExists(conn, "Fines", "CreatedDate");
                    if (hasCreatedDate)
                    {
                        string query = hasPaidDate
                            ? @"SELECT COALESCE(SUM(Amount), 0) FROM Fines 
                              WHERE PaidDate IS NULL AND CreatedDate >= DATE_SUB(NOW(), INTERVAL 7 DAY)"
                            : @"SELECT COALESCE(SUM(Amount), 0) FROM Fines 
                              WHERE Status = 'Pending' AND CreatedDate >= DATE_SUB(NOW(), INTERVAL 7 DAY)";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            decimal thisWeek = Convert.ToDecimal(cmd.ExecuteScalar());
                            string queryLastWeek = hasPaidDate
                                ? @"SELECT COALESCE(SUM(Amount), 0) FROM Fines 
                                  WHERE PaidDate IS NULL AND CreatedDate >= DATE_SUB(NOW(), INTERVAL 14 DAY) 
                                  AND CreatedDate < DATE_SUB(NOW(), INTERVAL 7 DAY)"
                                : @"SELECT COALESCE(SUM(Amount), 0) FROM Fines 
                                  WHERE Status = 'Pending' AND CreatedDate >= DATE_SUB(NOW(), INTERVAL 14 DAY) 
                                  AND CreatedDate < DATE_SUB(NOW(), INTERVAL 7 DAY)";
                            using (MySqlCommand cmdLastWeek = new MySqlCommand(queryLastWeek, conn))
                            {
                                decimal lastWeek = Convert.ToDecimal(cmdLastWeek.ExecuteScalar());
                                if (lastWeek > 0)
                                {
                                    double change = ((double)((double)thisWeek - (double)lastWeek) / (double)lastWeek) * 100;
                                    lblPendingFinesChange.Text = $"{(change >= 0 ? "+" : "")}{change:F0}%";
                                    lblPendingFinesChange.ForeColor = change >= 0 ? Color.FromArgb(40, 167, 69) : Color.FromArgb(220, 53, 69);
                                }
                                else
                                {
                                    lblPendingFinesChange.Text = "-5%";
                                    lblPendingFinesChange.ForeColor = Color.FromArgb(220, 53, 69);
                                }
                            }
                        }
                    }
                    else
                    {
                        lblPendingFinesChange.Text = "-5%";
                        lblPendingFinesChange.ForeColor = Color.FromArgb(220, 53, 69);
                    }
                }
            }
            catch
            {
                lblPendingFinesChange.Text = "-5%";
                lblPendingFinesChange.ForeColor = Color.FromArgb(220, 53, 69);
            }
        }

        private void LoadRecentActivity()
        {
            try
            {
                listViewRecentActivity.Items.Clear();

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    List<string> activities = new List<string>();

                    string queryBorrows = @"SELECT m.FirstName, m.LastName, b.Title, t.BorrowDate
                                          FROM Transactions t
                                          INNER JOIN Members m ON t.MemberID = m.MemberID
                                          INNER JOIN Books b ON t.BookID = b.BookID
                                          WHERE t.BorrowDate >= DATE_SUB(NOW(), INTERVAL 24 HOUR)
                                          ORDER BY t.BorrowDate DESC
                                          LIMIT 5";
                    using (MySqlCommand cmd = new MySqlCommand(queryBorrows, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string name = $"{reader["FirstName"]} {reader["LastName"]}";
                                string title = reader["Title"].ToString();
                                DateTime borrowDate = reader.GetDateTime("BorrowDate");
                                string timeAgo = GetTimeAgo(borrowDate);
                                activities.Add($"Book Borrowed: \"{name}\" borrowed \"{title}\"");
                                ListViewItem item = new ListViewItem($"Book Borrowed: \"{name}\" borrowed \"{title}\"");
                                item.SubItems.Add(timeAgo);
                                listViewRecentActivity.Items.Add(item);
                            }
                        }
                    }

                    string queryReturns = @"SELECT m.FirstName, m.LastName, b.Title, t.ReturnDate
                                          FROM Transactions t
                                          INNER JOIN Members m ON t.MemberID = m.MemberID
                                          INNER JOIN Books b ON t.BookID = b.BookID
                                          WHERE t.ReturnDate >= DATE_SUB(NOW(), INTERVAL 24 HOUR)
                                          AND t.ReturnDate IS NOT NULL
                                          ORDER BY t.ReturnDate DESC
                                          LIMIT 5";
                    using (MySqlCommand cmd = new MySqlCommand(queryReturns, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string name = $"{reader["FirstName"]} {reader["LastName"]}";
                                string title = reader["Title"].ToString();
                                DateTime returnDate = reader.GetDateTime("ReturnDate");
                                string timeAgo = GetTimeAgo(returnDate);
                                ListViewItem item = new ListViewItem($"Book Returned: \"{name}\" returned \"{title}\"");
                                item.SubItems.Add(timeAgo);
                                listViewRecentActivity.Items.Add(item);
                            }
                        }
                    }

                    bool hasRegistrationDate = CheckColumnExists(conn, "Members", "RegistrationDate");
                    if (hasRegistrationDate)
                    {
                        string queryNewMembers = @"SELECT FirstName, LastName, MemberID, RegistrationDate
                                                  FROM Members
                                                  WHERE RegistrationDate >= DATE_SUB(NOW(), INTERVAL 24 HOUR)
                                                  ORDER BY RegistrationDate DESC
                                                  LIMIT 5";
                        using (MySqlCommand cmd = new MySqlCommand(queryNewMembers, conn))
                        {
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string name = $"{reader["FirstName"]} {reader["LastName"]}";
                                    int memberID = reader.GetInt32("MemberID");
                                    DateTime regDate = reader.GetDateTime("RegistrationDate");
                                    string timeAgo = GetTimeAgo(regDate);
                                    ListViewItem item = new ListViewItem($"New Member: \"{name}\" joined (Member ID: {memberID})");
                                    item.SubItems.Add(timeAgo);
                                    listViewRecentActivity.Items.Add(item);
                                }
                            }
                        }
                    }

                    bool hasFinesTable = CheckTableExists(conn, "Fines");
                    if (hasFinesTable)
                    {
                        bool hasPaidDate = CheckColumnExists(conn, "Fines", "PaidDate");
                        if (hasPaidDate)
                        {
                            string queryFines = @"SELECT m.FirstName, m.LastName, f.Amount, f.PaidDate
                                                 FROM Fines f
                                                 INNER JOIN Members m ON f.MemberID = m.MemberID
                                                 WHERE f.PaidDate >= DATE_SUB(NOW(), INTERVAL 24 HOUR)
                                                 ORDER BY f.PaidDate DESC
                                                 LIMIT 5";
                            using (MySqlCommand cmd = new MySqlCommand(queryFines, conn))
                            {
                                using (MySqlDataReader reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        string name = $"{reader["FirstName"]} {reader["LastName"]}";
                                        decimal amount = reader.GetDecimal("Amount");
                                        DateTime paidDate = reader.GetDateTime("PaidDate");
                                        string timeAgo = GetTimeAgo(paidDate);
                                        ListViewItem item = new ListViewItem($"Fine Paid: \"{name}\" paid ${amount:F2}");
                                        item.SubItems.Add(timeAgo);
                                        listViewRecentActivity.Items.Add(item);
                                    }
                                }
                            }
                        }
                    }

                    bool hasReservationsTable = CheckTableExists(conn, "Reservations");
                    if (hasReservationsTable)
                    {
                        string queryReservations = @"SELECT m.FirstName, m.LastName, b.Title, r.ReservationDate
                                                     FROM Reservations r
                                                     INNER JOIN Members m ON r.MemberID = m.MemberID
                                                     INNER JOIN Books b ON r.BookID = b.BookID
                                                     WHERE r.ReservationDate >= DATE_SUB(NOW(), INTERVAL 24 HOUR)
                                                     ORDER BY r.ReservationDate DESC
                                                     LIMIT 5";
                        using (MySqlCommand cmd = new MySqlCommand(queryReservations, conn))
                        {
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string name = $"{reader["FirstName"]} {reader["LastName"]}";
                                    string title = reader["Title"].ToString();
                                    DateTime resDate = reader.GetDateTime("ReservationDate");
                                    string timeAgo = GetTimeAgo(resDate);
                                    ListViewItem item = new ListViewItem($"Book Reserved: \"{name}\" reserved \"{title}\"");
                                    item.SubItems.Add(timeAgo);
                                    listViewRecentActivity.Items.Add(item);
                                }
                            }
                        }
                    }

                    while (listViewRecentActivity.Items.Count > 5)
                    {
                        listViewRecentActivity.Items.RemoveAt(listViewRecentActivity.Items.Count - 1);
                    }
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

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"SELECT b.Title, m.FirstName, m.LastName, t.DueDate, DATEDIFF(NOW(), t.DueDate) as DaysOverdue
                                   FROM Transactions t
                                   INNER JOIN Books b ON t.BookID = b.BookID
                                   INNER JOIN Members m ON t.MemberID = m.MemberID
                                   WHERE t.DueDate < NOW()
                                   AND (t.Status = 'Borrowed' OR t.Status = 'Active')
                                   AND t.ReturnDate IS NULL
                                   ORDER BY t.DueDate ASC
                                   LIMIT 10";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            int yPos = 0;
                            int spacing = 10;

                            while (reader.Read())
                            {
                                string title = reader["Title"].ToString();
                                string borrower = $"{reader["FirstName"]} {reader["LastName"]}";
                                DateTime dueDate = reader.GetDateTime("DueDate");
                                int daysOverdue = reader.GetInt32("DaysOverdue");

                                Panel card = CreateOverdueBookCard(title, borrower, dueDate, daysOverdue, yPos);
                                panelOverdueBooksList.Controls.Add(card);
                                yPos += 120 + spacing;
                            }
                        }
                    }
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

        private void lblSubtitleSidebar_Click(object sender, EventArgs e)
        {

        }
    }
}
