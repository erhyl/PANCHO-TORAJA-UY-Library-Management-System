using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Project5LMS.Forms.Reservation
{
    public partial class ReservationForm : Form
    {
        private string connectionString;
        private int selectedMemberId = 0;
        private int selectedBookId = 0;

        public ReservationForm()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["MySqlConnectionString"]?.ConnectionString 
                ?? "Server=localhost;Database=librarydb;Uid=root;Pwd=;";
            dtpReservationDate.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now.AddDays(7);
            
            // Wire up event handlers
            tbnMember.Leave += tbnMember_Leave;
            btnBookTitle.Leave += btnBookTitle_Leave;
            txtSearch.TextChanged += txtSearch_TextChanged;
            cmbBookStatus.SelectedIndexChanged += cmbBookStatus_SelectedIndexChanged;
        }

        private void ReservationForm_Load(object sender, EventArgs e)
        {
            LoadMetrics();
            SetupDataGridView();
            LoadStatusFilter();
            LoadReservations();
        }

        private void SetupDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();
            dataGridView1.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 11F);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Bold);
        }

        private void LoadStatusFilter()
        {
            cmbBookStatus.Items.Clear();
            cmbBookStatus.Items.Add("All Status");
            cmbBookStatus.Items.Add("Active");
            cmbBookStatus.Items.Add("Pending");
            cmbBookStatus.Items.Add("Expired");
            cmbBookStatus.Items.Add("Cancelled");
            cmbBookStatus.Items.Add("Completed");
            cmbBookStatus.SelectedIndex = 0;
        }

        private void LoadMetrics()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Check if Reservations table exists
                    string checkTableQuery = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES 
                                              WHERE TABLE_SCHEMA = DATABASE() 
                                              AND TABLE_NAME = 'Reservations'";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkTableQuery, conn))
                    {
                        int tableExists = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (tableExists == 0)
                        {
                            // Table doesn't exist, set metrics to 0
                            label1.Text = "0";
                            label4.Text = "0";
                            label6.Text = "0";
                            return;
                        }
                    }

                    // Active Reservations
                    string queryActive = @"SELECT COUNT(*) FROM Reservations 
                                         WHERE Status = 'Active' OR Status = 'Pending'";
                    using (MySqlCommand cmd = new MySqlCommand(queryActive, conn))
                    {
                        label1.Text = Convert.ToInt32(cmd.ExecuteScalar()).ToString();
                    }

                    // Pending Approvals
                    string queryPending = @"SELECT COUNT(*) FROM Reservations 
                                           WHERE Status = 'Pending'";
                    using (MySqlCommand cmd = new MySqlCommand(queryPending, conn))
                    {
                        label4.Text = Convert.ToInt32(cmd.ExecuteScalar()).ToString();
                    }

                    // Expired
                    string queryExpired = @"SELECT COUNT(*) FROM Reservations 
                                          WHERE Status = 'Expired'";
                    using (MySqlCommand cmd = new MySqlCommand(queryExpired, conn))
                    {
                        label6.Text = Convert.ToInt32(cmd.ExecuteScalar()).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading metrics: {ex.Message}");
                label1.Text = "0";
                label4.Text = "0";
                label6.Text = "0";
            }
        }

        private void LoadReservations()
        {
            try
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();

                dataGridView1.Columns.Add("ReservationID", "ID");
                dataGridView1.Columns["ReservationID"].Width = 60;
                dataGridView1.Columns.Add("MemberName", "Member");
                dataGridView1.Columns["MemberName"].Width = 200;
                dataGridView1.Columns.Add("BookTitle", "Book");
                dataGridView1.Columns["BookTitle"].Width = 250;
                dataGridView1.Columns.Add("ReservationDate", "Reservation Date");
                dataGridView1.Columns["ReservationDate"].Width = 150;
                dataGridView1.Columns.Add("PickupDate", "Pickup Date");
                dataGridView1.Columns["PickupDate"].Width = 150;
                dataGridView1.Columns.Add("Status", "Status");
                dataGridView1.Columns["Status"].Width = 120;

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Check if table exists
                    string checkTableQuery = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES 
                                              WHERE TABLE_SCHEMA = DATABASE() 
                                              AND TABLE_NAME = 'Reservations'";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkTableQuery, conn))
                    {
                        int tableExists = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (tableExists == 0)
                        {
                            return; // Table doesn't exist, leave grid empty
                        }
                    }

                    string keyword = txtSearch.Text.Trim();
                    if (keyword == "Search Reservation") keyword = "";
                    string status = cmbBookStatus.Text == "All Status" ? "" : cmbBookStatus.Text;

                    string query = @"SELECT 
                                        r.ReservationID,
                                        CONCAT(m.FirstName, ' ', m.LastName) as MemberName,
                                        b.Title as BookTitle,
                                        r.ReservationDate,
                                        r.PickupDate,
                                        r.Status
                                    FROM Reservations r
                                    INNER JOIN Members m ON r.MemberID = m.MemberID
                                    INNER JOIN Books b ON r.BookID = b.BookID
                                    WHERE (@Keyword = '' 
                                           OR m.FirstName LIKE @Keyword 
                                           OR m.LastName LIKE @Keyword
                                           OR b.Title LIKE @Keyword
                                           OR CAST(r.ReservationID AS CHAR) LIKE @Keyword)
                                    AND (@Status = '' OR r.Status = @Status)
                                    ORDER BY r.ReservationDate DESC
                                    LIMIT 500";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                        cmd.Parameters.AddWithValue("@Status", status);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dataGridView1.Rows.Add(
                                    reader["ReservationID"],
                                    reader["MemberName"],
                                    reader["BookTitle"],
                                    Convert.ToDateTime(reader["ReservationDate"]).ToString("yyyy-MM-dd"),
                                    reader["PickupDate"] != DBNull.Value ? Convert.ToDateTime(reader["PickupDate"]).ToString("yyyy-MM-dd") : "",
                                    reader["Status"]
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading reservations: {ex.Message}");
            }
        }

        private void tbnMember_Leave(object sender, EventArgs e)
        {
            ValidateMember();
        }

        private void ValidateMember()
        {
            selectedMemberId = 0;
            if (string.IsNullOrWhiteSpace(tbnMember.Text)) return;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT MemberID, FirstName, LastName FROM Members 
                                   WHERE MemberID = @Search 
                                   OR FirstName LIKE @Search 
                                   OR LastName LIKE @Search
                                   OR Email LIKE @Search LIMIT 1";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Search", "%" + tbnMember.Text.Trim() + "%");
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                selectedMemberId = reader.GetInt32("MemberID");
                                string firstName = reader["FirstName"] != DBNull.Value ? reader["FirstName"].ToString() : "";
                                string lastName = reader["LastName"] != DBNull.Value ? reader["LastName"].ToString() : "";
                                tbnMember.Text = $"{firstName} {lastName}".Trim();
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void btnBookTitle_Leave(object sender, EventArgs e)
        {
            ValidateBook();
        }

        private void ValidateBook()
        {
            selectedBookId = 0;
            if (string.IsNullOrWhiteSpace(btnBookTitle.Text)) return;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT BookID, Title FROM Books 
                                   WHERE BookID = @Search OR Title LIKE @Search 
                                   OR ISBN LIKE @Search LIMIT 1";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Search", "%" + btnBookTitle.Text.Trim() + "%");
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                selectedBookId = reader.GetInt32("BookID");
                                btnBookTitle.Text = reader.GetString("Title");
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void btnCreateReservation_Click(object sender, EventArgs e)
        {
            if (selectedMemberId == 0)
            {
                MessageBox.Show("Please enter a valid member.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (selectedBookId == 0)
            {
                MessageBox.Show("Please enter a valid book.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Check if table exists, create if not
                    string checkTableQuery = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES 
                                              WHERE TABLE_SCHEMA = DATABASE() 
                                              AND TABLE_NAME = 'Reservations'";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkTableQuery, conn))
                    {
                        int tableExists = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (tableExists == 0)
                        {
                            string createTableQuery = @"CREATE TABLE IF NOT EXISTS Reservations (
                                                        ReservationID INT AUTO_INCREMENT PRIMARY KEY,
                                                        MemberID INT NOT NULL,
                                                        BookID INT NOT NULL,
                                                        ReservationDate DATETIME NOT NULL,
                                                        PickupDate DATETIME,
                                                        Status VARCHAR(50) DEFAULT 'Pending',
                                                        FOREIGN KEY (MemberID) REFERENCES Members(MemberID),
                                                        FOREIGN KEY (BookID) REFERENCES Books(BookID)
                                                        )";
                            using (MySqlCommand createCmd = new MySqlCommand(createTableQuery, conn))
                            {
                                createCmd.ExecuteNonQuery();
                            }
                        }
                    }

                    string query = @"INSERT INTO Reservations (MemberID, BookID, ReservationDate, PickupDate, Status)
                                   VALUES (@MemberID, @BookID, @ReservationDate, @PickupDate, 'Pending')";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", selectedMemberId);
                        cmd.Parameters.AddWithValue("@BookID", selectedBookId);
                        cmd.Parameters.AddWithValue("@ReservationDate", dtpReservationDate.Value);
                        cmd.Parameters.AddWithValue("@PickupDate", dateTimePicker2.Value);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Reservation created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadMetrics();
                    LoadReservations();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating reservation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            tbnMember.Clear();
            btnBookTitle.Clear();
            selectedMemberId = 0;
            selectedBookId = 0;
            dtpReservationDate.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now.AddDays(7);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadReservations();
        }

        private void cmbBookStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadReservations();
        }
    }
}
