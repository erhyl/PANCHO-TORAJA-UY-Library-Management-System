using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Project5LMS.Admin_Dashboard;
using Project5LMS.Helpers;

namespace Project5LMS.Admin_Dashboard
{
    public partial class Dashboard_Home : Form
    {
        private string connectionString;

        public Dashboard_Home()
        {
            InitializeComponent();
            // Try to get connection string from App.config, fallback to default
            try
            {
                connectionString = DatabaseHelper.GetConnectionString();
            }
            catch
            {
                // Fallback to default connection string if App.config doesn't have LmsDb
                connectionString = "Server=localhost;Database=librarydb;Uid=root;Pwd=;";
            }
            SetupDataGridView();
            SetupListView();
        }

        private void Dashboard_Home_Load(object sender, EventArgs e)
        {
            LoadDashboardMetrics();
            LoadActiveTransactions();
            LoadRecentActivity();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (Visible)
            {
                // Refresh data when form becomes visible
                LoadDashboardMetrics();
                LoadActiveTransactions();
                LoadRecentActivity();
            }
        }

        private void SetupDataGridView()
        {
            dataGridViewActiveTransactions.Columns.Clear();
            dataGridViewActiveTransactions.Columns.Add("BookTitle", "Book Title");
            dataGridViewActiveTransactions.Columns.Add("Author", "Author");
            dataGridViewActiveTransactions.Columns.Add("Status", "Status");
            dataGridViewActiveTransactions.Columns.Add("DueDate", "Due");

            // Style the DataGridView
            dataGridViewActiveTransactions.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 11F);
            dataGridViewActiveTransactions.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Bold);
            dataGridViewActiveTransactions.RowTemplate.Height = 40;
            dataGridViewActiveTransactions.DefaultCellStyle.Padding = new Padding(5);
        }

        private void SetupListView()
        {
            listViewRecentActivity.Columns.Clear();
            listViewRecentActivity.Columns.Add("Activity", 400);
            listViewRecentActivity.Columns.Add("Timestamp", 200);
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

        private void EnsureTransactionsTableExists()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Check if table exists
                    string checkTableQuery = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES 
                                              WHERE TABLE_SCHEMA = DATABASE() 
                                              AND TABLE_NAME = 'Transactions'";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkTableQuery, conn))
                    {
                        int tableExists = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (tableExists == 0)
                        {
                            // Create Transactions table
                            string createTableQuery = @"CREATE TABLE IF NOT EXISTS Transactions (
                                                        TransactionID INT AUTO_INCREMENT PRIMARY KEY,
                                                        MemberID INT NOT NULL,
                                                        BookID INT NOT NULL,
                                                        BorrowDate DATETIME NOT NULL,
                                                        DueDate DATETIME NOT NULL,
                                                        ReturnDate DATETIME NULL,
                                                        Status VARCHAR(50) DEFAULT 'Borrowed',
                                                        FOREIGN KEY (MemberID) REFERENCES Members(MemberID),
                                                        FOREIGN KEY (BookID) REFERENCES Books(BookID)
                                                        )";
                            using (MySqlCommand createCmd = new MySqlCommand(createTableQuery, conn))
                            {
                                createCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error ensuring Transactions table exists: {ex.Message}");
            }
        }

        private void LoadDashboardMetrics()
        {
            // Initialize all metrics to 0 first
            lblMetricValue1.Text = "0";
            lblMetricValue2.Text = "0";
            lblMetricValue3.Text = "0";
            lblMetricValue4.Text = "0";
            lblMetricValue5.Text = "0";
            lblMetricValue6.Text = "0";
            lblMetricValue7.Text = "P0";
            lblMetricValue8.Text = "0";

            try
            {
                EnsureTransactionsTableExists();
                
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Total Books
                    try
                    {
                        string queryBooks = "SELECT COUNT(*) FROM Books";
                        using (MySqlCommand cmd = new MySqlCommand(queryBooks, conn))
                        {
                            object result = cmd.ExecuteScalar();
                            if (result != null && result != DBNull.Value)
                            {
                                lblMetricValue1.Text = Convert.ToInt32(result).ToString();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error loading Total Books: {ex.Message}");
                    }

                    // Total Members
                    try
                    {
                        string queryMembers = "SELECT COUNT(*) FROM Members";
                        using (MySqlCommand cmd = new MySqlCommand(queryMembers, conn))
                        {
                            object result = cmd.ExecuteScalar();
                            if (result != null && result != DBNull.Value)
                            {
                                lblMetricValue2.Text = Convert.ToInt32(result).ToString();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error loading Total Members: {ex.Message}");
                    }

                    // Borrowed Books (Active transactions)
                    try
                    {
                        string queryBorrowed = @"SELECT COUNT(*) FROM Transactions 
                                                WHERE Status = 'Borrowed' OR Status = 'Active'";
                        using (MySqlCommand cmd = new MySqlCommand(queryBorrowed, conn))
                        {
                            object result = cmd.ExecuteScalar();
                            if (result != null && result != DBNull.Value)
                            {
                                lblMetricValue3.Text = Convert.ToInt32(result).ToString();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error loading Borrowed Books: {ex.Message}");
                    }

                    // Overdue Books
                    try
                    {
                        string queryOverdue = @"SELECT COUNT(*) FROM Transactions 
                                              WHERE DueDate < CURDATE() 
                                              AND (Status = 'Borrowed' OR Status = 'Active')";
                        using (MySqlCommand cmd = new MySqlCommand(queryOverdue, conn))
                        {
                            object result = cmd.ExecuteScalar();
                            if (result != null && result != DBNull.Value)
                            {
                                lblMetricValue4.Text = Convert.ToInt32(result).ToString();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error loading Overdue Books: {ex.Message}");
                    }

                    // Today's Checkouts
                    try
                    {
                        string queryTodayCheckouts = @"SELECT COUNT(*) FROM Transactions 
                                                      WHERE DATE(BorrowDate) = CURDATE() 
                                                      AND (Status = 'Borrowed' OR Status = 'Active')";
                        using (MySqlCommand cmd = new MySqlCommand(queryTodayCheckouts, conn))
                        {
                            object result = cmd.ExecuteScalar();
                            if (result != null && result != DBNull.Value)
                            {
                                lblMetricValue5.Text = Convert.ToInt32(result).ToString();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error loading Today's Checkouts: {ex.Message}");
                    }

                    // Today's Returns
                    try
                    {
                        string queryTodayReturns = @"SELECT COUNT(*) FROM Transactions 
                                                    WHERE DATE(ReturnDate) = CURDATE() 
                                                    AND Status = 'Returned'";
                        using (MySqlCommand cmd = new MySqlCommand(queryTodayReturns, conn))
                        {
                            object result = cmd.ExecuteScalar();
                            if (result != null && result != DBNull.Value)
                            {
                                lblMetricValue6.Text = Convert.ToInt32(result).ToString();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error loading Today's Returns: {ex.Message}");
                    }

                    // Pending Fines
                    try
                    {
                        string queryFines = @"SELECT COALESCE(SUM(Amount), 0) FROM Fines 
                                             WHERE Status = 'Pending' OR Status = 'Unpaid'";
                        using (MySqlCommand cmd = new MySqlCommand(queryFines, conn))
                        {
                            object result = cmd.ExecuteScalar();
                            if (result != null && result != DBNull.Value)
                            {
                                decimal pendingFines = Convert.ToDecimal(result);
                                lblMetricValue7.Text = $"P{pendingFines:F0}";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error loading Pending Fines: {ex.Message}");
                    }

                    // Overdue Returns
                    try
                    {
                        string queryOverdueReturns = @"SELECT COUNT(*) FROM Transactions 
                                                      WHERE DueDate < CURDATE() 
                                                      AND (Status = 'Borrowed' OR Status = 'Active')";
                        using (MySqlCommand cmd = new MySqlCommand(queryOverdueReturns, conn))
                        {
                            object result = cmd.ExecuteScalar();
                            if (result != null && result != DBNull.Value)
                            {
                                lblMetricValue8.Text = Convert.ToInt32(result).ToString();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error loading Overdue Returns: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error connecting to database: {ex.Message}");
                // Metrics already set to 0, so they will display
            }
        }

        private void LoadActiveTransactions()
        {
            try
            {
                EnsureTransactionsTableExists();
                
                dataGridViewActiveTransactions.Rows.Clear();

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT 
                                        b.Title,
                                        b.Author,
                                        t.Status,
                                        t.DueDate
                                    FROM Transactions t
                                    INNER JOIN Books b ON t.BookID = b.BookID
                                    WHERE t.Status = 'Borrowed' OR t.Status = 'Active'
                                    ORDER BY t.DueDate ASC
                                    LIMIT 20";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string title = reader["Title"] != DBNull.Value ? reader["Title"].ToString() : "N/A";
                            string author = reader["Author"] != DBNull.Value ? reader["Author"].ToString() : "N/A";
                            string status = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : "Active";
                            DateTime dueDate = reader["DueDate"] != DBNull.Value ? Convert.ToDateTime(reader["DueDate"]) : DateTime.MinValue;

                            int rowIndex = dataGridViewActiveTransactions.Rows.Add(title, author, status, dueDate.ToString("yyyy-MM-dd"));

                            // Color code based on status
                            DataGridViewRow row = dataGridViewActiveTransactions.Rows[rowIndex];
                            if (status.Equals("Overdue", StringComparison.OrdinalIgnoreCase) || 
                                (dueDate < DateTime.Now && dueDate != DateTime.MinValue && (status.Equals("Borrowed", StringComparison.OrdinalIgnoreCase) || status.Equals("Active", StringComparison.OrdinalIgnoreCase))))
                            {
                                row.DefaultCellStyle.ForeColor = Color.Red;
                                row.Cells["Status"].Value = "Overdue";
                            }
                            else
                            {
                                row.DefaultCellStyle.ForeColor = Color.Gray;
                                row.Cells["Status"].Value = "Active";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading active transactions: {ex.Message}");
                // Leave grid empty if there's an error
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
                    string query = @"SELECT 
                                        'Book Checked Out' as ActivityType,
                                        b.Title,
                                        b.Author,
                                        t.BorrowDate as ActivityDate
                                    FROM Transactions t
                                    INNER JOIN Books b ON t.BookID = b.BookID
                                    WHERE t.Status = 'Borrowed' OR t.Status = 'Active'
                                    
                                    UNION ALL
                                    
                                    SELECT 
                                        'New Member Registered' as ActivityType,
                                        CONCAT(m.FirstName, ' ', m.LastName) as Title,
                                        m.MemberType as Author,
                                        m.RegistrationDate as ActivityDate
                                    FROM Members m
                                    
                                    UNION ALL
                                    
                                    SELECT 
                                        'Book Returned' as ActivityType,
                                        b.Title,
                                        '' as Author,
                                        t.ReturnDate as ActivityDate
                                    FROM Transactions t
                                    INNER JOIN Books b ON t.BookID = b.BookID
                                    WHERE t.Status = 'Returned' AND t.ReturnDate IS NOT NULL
                                    
                                    ORDER BY ActivityDate DESC
                                    LIMIT 10";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string activityType = reader["ActivityType"] != DBNull.Value ? reader["ActivityType"].ToString() : "";
                            string title = reader["Title"] != DBNull.Value ? reader["Title"].ToString() : "";
                            string author = reader["Author"] != DBNull.Value ? reader["Author"].ToString() : "";
                            DateTime activityDate = reader["ActivityDate"] != DBNull.Value ? Convert.ToDateTime(reader["ActivityDate"]) : DateTime.Now;

                            string activityText = "";
                            if (activityType == "Book Checked Out")
                            {
                                activityText = $"Book Checked Out - {title} by {author}";
                            }
                            else if (activityType == "New Member Registered")
                            {
                                activityText = $"New Member Registered - {title} ({author})";
                            }
                            else if (activityType == "Book Returned")
                            {
                                activityText = $"Book Returned - {title}";
                            }

                            if (!string.IsNullOrEmpty(activityText))
                            {
                                string timeAgo = GetTimeAgo(activityDate);
                                ListViewItem item = new ListViewItem(new string[] { activityText, timeAgo });
                                listViewRecentActivity.Items.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading recent activity: {ex.Message}");
                // Leave list empty if there's an error
            }
        }

        private string GetTimeAgo(DateTime dateTime)
        {
            TimeSpan timeSpan = DateTime.Now - dateTime;

            if (timeSpan.TotalMinutes < 1)
                return "Just now";
            else if (timeSpan.TotalMinutes < 60)
                return $"{(int)timeSpan.TotalMinutes} mins ago";
            else if (timeSpan.TotalHours < 24)
                return $"{(int)timeSpan.TotalHours} hours ago";
            else
                return $"{(int)timeSpan.TotalDays} days ago";
        }
    }
}
