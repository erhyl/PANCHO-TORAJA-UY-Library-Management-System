using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using MySql.Data.MySqlClient;
using Project5LMS.Forms.Member.Profile;
using Project5LMS.Forms.Member.Search;
using Project5LMS.Forms.Member.Borrowings;
using Project5LMS.Forms.Member.Fines;
using Project5LMS.Forms.Member.Reservations;
using Project5LMS.Helpers;
using Project5LMS.Data;
using Project5LMS.Services;
namespace Project5LMS.Forms.Member.Dashboard
{
    public partial class MemberDashboardForm : Form
    {
        private readonly DatabaseContext _dbContext;
        public MemberDashboardForm()
        {
            InitializeComponent();
            _dbContext = ServiceFactory.GetDbContext();
        }
        private void MemberDashboardForm_Load(object sender, EventArgs e)
        {
            ShowDashboard();
            SetActiveButton(btnDashboard);
        }
        private void LoadFormInPanel(Form form)
        {
            panelMainContent.Controls.Clear();
            panelDashboardContent.Visible = false;
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
            btnSearch.BackColor = Color.Transparent;
            btnMyBorrowings.BackColor = Color.Transparent;
            btnReservations.BackColor = Color.Transparent;
            btnFines.BackColor = Color.Transparent;
            btnProfile.BackColor = Color.Transparent;
            activeButton.BackColor = Color.FromArgb(178, 34, 34);
        }
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnDashboard);
            ShowDashboard();
        }
        private void ShowDashboard()
        {
            panelMainContent.Controls.Clear();
            panelMainContent.Controls.Add(panelDashboardContent);
            panelDashboardContent.Visible = true;
            panelDashboardContent.BringToFront();
            LoadDashboardData();
        }
        private void LoadDashboardData()
        {
            int memberID = CurrentUser.GetMemberID();
            if (memberID == 0)
            {
                lblWelcomeTitle.Text = "Welcome Back, " + CurrentUser.FullName;
                return;
            }
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    LoadMemberName(conn, memberID);
                    LoadSummaryCards(conn, memberID);
                    LoadCurrentlyBorrowed(conn, memberID);
                    LoadActiveReservations(conn, memberID);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading dashboard data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Dashboard load error: {ex}");
            }
        }
        private void LoadMemberName(MySqlConnection conn, int memberID)
        {
            try
            {
                string query = @"SELECT FirstName, LastName FROM Members WHERE MemberID = @MemberID LIMIT 1";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MemberID", memberID);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string firstName = reader["FirstName"] != DBNull.Value ? reader["FirstName"].ToString() : "";
                            string lastName = reader["LastName"] != DBNull.Value ? reader["LastName"].ToString() : "";
                            string fullName = $"{firstName} {lastName}".Trim();
                            if (string.IsNullOrEmpty(fullName))
                            {
                                fullName = CurrentUser.FullName;
                            }
                            lblWelcomeTitle.Text = "Welcome Back, " + fullName;
                        }
                        else
                        {
                            lblWelcomeTitle.Text = "Welcome Back, " + CurrentUser.FullName;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading member name: {ex.Message}");
                lblWelcomeTitle.Text = "Welcome Back, " + CurrentUser.FullName;
            }
        }
        private void LoadSummaryCards(MySqlConnection conn, int memberID)
        {
            try
            {
                // Currently Borrowed Count
                string queryBorrowed = @"SELECT COUNT(*) as Count
                                        FROM Transactions
                                        WHERE MemberID = @MemberID
                                        AND (Status = 'Borrowed' OR Status = 'Active' OR ReturnDate IS NULL)";
                using (MySqlCommand cmd = new MySqlCommand(queryBorrowed, conn))
                {
                    cmd.Parameters.AddWithValue("@MemberID", memberID);
                    object result = cmd.ExecuteScalar();
                    int count = result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
                    lblCurrentlyBorrowedValue.Text = count.ToString();
                }
                // Due Soon Count (within 3 days)
                string queryDueSoon = @"SELECT COUNT(*) as Count
                                       FROM Transactions
                                       WHERE MemberID = @MemberID
                                       AND (Status = 'Borrowed' OR Status = 'Active' OR ReturnDate IS NULL)
                                       AND DueDate >= CURDATE()
                                       AND DueDate <= DATE_ADD(CURDATE(), INTERVAL 3 DAY)";
                using (MySqlCommand cmd = new MySqlCommand(queryDueSoon, conn))
                {
                    cmd.Parameters.AddWithValue("@MemberID", memberID);
                    object result = cmd.ExecuteScalar();
                    int count = result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
                    lblDueSoonValue.Text = count.ToString();
                }
                // Outstanding Fines
                string queryFines = @"SELECT COALESCE(SUM(Amount - COALESCE(Paid, 0)), 0) as Total
                                     FROM Fines
                                     WHERE MemberID = @MemberID
                                     AND (Status IN ('Pending', 'Partial', 'Overdue', 'Unpaid')
                                          OR (Status IS NULL AND (Paid IS NULL OR Paid < Amount)))
                                     AND (Paid IS NULL OR Paid < Amount)";
                using (MySqlCommand cmd = new MySqlCommand(queryFines, conn))
                {
                    cmd.Parameters.AddWithValue("@MemberID", memberID);
                    object result = cmd.ExecuteScalar();
                    decimal total = result != null && result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                    lblOutstandingFinesValue.Text = $"₱{total:F2}";
                }
                // Active Reservations Count
                string queryReservations = @"SELECT COUNT(*) as Count
                                            FROM Reservations
                                            WHERE MemberID = @MemberID
                                            AND (Status = 'Active' OR Status = 'Pending' OR Status = 'Ready' OR Status IS NULL)";
                using (MySqlCommand cmd = new MySqlCommand(queryReservations, conn))
                {
                    cmd.Parameters.AddWithValue("@MemberID", memberID);
                    object result = cmd.ExecuteScalar();
                    int count = result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
                    lblActiveReservationsValue.Text = count.ToString();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading summary cards: {ex.Message}");
            }
        }
        private void LoadCurrentlyBorrowed(MySqlConnection conn, int memberID)
        {
            try
            {
                // Hide all book panels initially
                panelBook1.Visible = false;
                panelBook2.Visible = false;
                panelBook3.Visible = false;
                panelBookSeparator1.Visible = false;
                panelBookSeparator2.Visible = false;
                string query = @"SELECT t.TransactionID, b.BookID, b.Title, b.Author, t.DueDate,
                                DATEDIFF(t.DueDate, CURDATE()) as DaysRemaining
                                FROM Transactions t
                                INNER JOIN Books b ON t.BookID = b.BookID
                                WHERE t.MemberID = @MemberID
                                AND (t.Status = 'Borrowed' OR t.Status = 'Active' OR t.ReturnDate IS NULL)
                                ORDER BY t.DueDate ASC
                                LIMIT 3";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MemberID", memberID);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int index = 0;
                        while (reader.Read() && index < 3)
                        {
                            string title = reader["Title"] != DBNull.Value ? reader["Title"].ToString() : "Unknown";
                            string author = reader["Author"] != DBNull.Value ? reader["Author"].ToString() : "Unknown";
                            DateTime dueDate = reader["DueDate"] != DBNull.Value ? Convert.ToDateTime(reader["DueDate"]) : DateTime.Now;
                            int daysRemaining = reader["DaysRemaining"] != DBNull.Value ? Convert.ToInt32(reader["DaysRemaining"]) : 0;
                            if (index == 0)
                            {
                                lblBook1Title.Text = title;
                                lblBook1Author.Text = "by " + author;
                                lblBook1DueDate.Text = "Due: " + dueDate.ToString("yyyy-MM-dd");
                                lblBook1DaysLeft.Text = daysRemaining > 0 ? $"{daysRemaining} days left" : daysRemaining == 0 ? "Due today" : $"{Math.Abs(daysRemaining)} days overdue";
                                lblBook1DaysLeft.ForeColor = daysRemaining <= 3 ? Color.FromArgb(239, 68, 68) : Color.FromArgb(51, 51, 51);
                                panelBook1.Visible = true;
                            }
                            else if (index == 1)
                            {
                                lblBook2Title.Text = title;
                                lblBook2Author.Text = "by " + author;
                                lblBook2DueDate.Text = "Due: " + dueDate.ToString("yyyy-MM-dd");
                                lblBook2DaysLeft.Text = daysRemaining > 0 ? $"{daysRemaining} days left" : daysRemaining == 0 ? "Due today" : $"{Math.Abs(daysRemaining)} days overdue";
                                lblBook2DaysLeft.ForeColor = daysRemaining <= 3 ? Color.FromArgb(239, 68, 68) : Color.FromArgb(51, 51, 51);
                                panelBook2.Visible = true;
                                panelBookSeparator1.Visible = true;
                            }
                            else if (index == 2)
                            {
                                lblBook3Title.Text = title;
                                lblBook3Author.Text = "by " + author;
                                lblBook3DueDate.Text = "Due: " + dueDate.ToString("yyyy-MM-dd");
                                lblBook3DaysLeft.Text = daysRemaining > 0 ? $"{daysRemaining} days left" : daysRemaining == 0 ? "Due today" : $"{Math.Abs(daysRemaining)} days overdue";
                                lblBook3DaysLeft.ForeColor = daysRemaining <= 3 ? Color.FromArgb(239, 68, 68) : Color.FromArgb(51, 51, 51);
                                panelBook3.Visible = true;
                                panelBookSeparator2.Visible = true;
                            }
                            index++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading currently borrowed: {ex.Message}");
            }
        }
        private void LoadActiveReservations(MySqlConnection conn, int memberID)
        {
            try
            {
                // Hide all reservation panels initially
                panelReservation1.Visible = false;
                panelReservation2.Visible = false;
                panelReservationSeparator2.Visible = false;
                string query = @"SELECT r.ReservationID, b.BookID, b.Title, b.Author, r.ReservationDate,
                                COALESCE(r.PickupDate, DATE_ADD(r.ReservationDate, INTERVAL 7 DAY)) as EstAvailable,
                                (SELECT COUNT(*) FROM Reservations r2 
                                 WHERE r2.BookID = r.BookID 
                                 AND r2.Status IN ('Active', 'Pending', 'Ready')
                                 AND (r2.Priority > COALESCE(r.Priority, 0) 
                                      OR (r2.Priority = COALESCE(r.Priority, 0) AND r2.ReservationDate < r.ReservationDate))) + 1 as QueuePosition
                                FROM Reservations r
                                INNER JOIN Books b ON r.BookID = b.BookID
                                WHERE r.MemberID = @MemberID
                                AND (r.Status = 'Active' OR r.Status = 'Pending' OR r.Status = 'Ready' OR r.Status IS NULL)
                                ORDER BY COALESCE(r.Priority, 0) DESC, r.ReservationDate ASC
                                LIMIT 2";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MemberID", memberID);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int index = 0;
                        while (reader.Read() && index < 2)
                        {
                            string title = reader["Title"] != DBNull.Value ? reader["Title"].ToString() : "Unknown";
                            string author = reader["Author"] != DBNull.Value ? reader["Author"].ToString() : "Unknown";
                            DateTime estAvailable = reader["EstAvailable"] != DBNull.Value ? Convert.ToDateTime(reader["EstAvailable"]) : DateTime.Now.AddDays(7);
                            int queuePosition = reader["QueuePosition"] != DBNull.Value ? Convert.ToInt32(reader["QueuePosition"]) : 1;
                            if (index == 0)
                            {
                                lblReservation1Title.Text = title;
                                lblReservation1Author.Text = "by " + author;
                                lblReservation1Queue.Text = "Queue Position: #" + queuePosition;
                                lblReservation1EstAvailable.Text = "Est. Available: " + estAvailable.ToString("yyyy-MM-dd");
                                panelReservation1.Visible = true;
                            }
                            else if (index == 1)
                            {
                                lblReservation2Title.Text = title;
                                lblReservation2Author.Text = "by " + author;
                                lblReservation2Queue.Text = "Queue Position: #" + queuePosition;
                                lblReservation2EstAvailable.Text = "Est. Available: " + estAvailable.ToString("yyyy-MM-dd");
                                panelReservation2.Visible = true;
                                panelReservationSeparator2.Visible = true;
                            }
                            index++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading active reservations: {ex.Message}");
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnSearch);
            LoadFormInPanel(new MemberSearchForm());
        }
        private void btnMyBorrowings_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnMyBorrowings);
            LoadFormInPanel(new MyBorrowingsForm());
        }
        private void btnReservations_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnReservations);
            LoadFormInPanel(new MemberReservationsForm());
        }
        private void btnFines_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnFines);
            LoadFormInPanel(new MemberFinesForm());
        }
        private void btnProfile_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnProfile);
            LoadFormInPanel(new MemberProfileForm());
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Project5LMS.LoginForm login = new Project5LMS.LoginForm();
            login.Show();
        }
        private void lblBook1Title_Click(object sender, EventArgs e)
        {
        }
    }
}