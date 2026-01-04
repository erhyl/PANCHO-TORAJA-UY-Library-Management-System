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
            
            // Load status combo box
            LoadStatusComboBox();
            
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
            cmbBookStatus.Items.Add("Pending");
            cmbBookStatus.Items.Add("Fulfilled");
            cmbBookStatus.Items.Add("Cancelled");
            cmbBookStatus.SelectedIndex = 0;
        }

        private void LoadStatusComboBox()
        {
            if (comboBox1 != null)
            {
                comboBox1.Items.Clear();
                comboBox1.Items.Add("Pending");
                comboBox1.Items.Add("Fulfilled");
                comboBox1.Items.Add("Cancelled");
                comboBox1.SelectedIndex = 0; // Default to "Pending"
            }
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

                    // Active Reservations (Pending + Fulfilled)
                    string queryActive = @"SELECT COUNT(*) FROM Reservations 
                                         WHERE Status = 'Pending' OR Status = 'Fulfilled'";
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

                    // Cancelled
                    string queryCancelled = @"SELECT COUNT(*) FROM Reservations 
                                          WHERE Status = 'Cancelled'";
                    using (MySqlCommand cmd = new MySqlCommand(queryCancelled, conn))
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
                // Clear rows but keep columns if they exist
                dataGridView1.Rows.Clear();
                
                // Only clear and recreate columns if they don't exist
                if (dataGridView1.Columns.Count == 0)
                {
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

                    // Add Actions column with Approve/Decline buttons
                    DataGridViewButtonColumn approveColumn = new DataGridViewButtonColumn();
                    approveColumn.Name = "Approve";
                    approveColumn.HeaderText = "Approve";
                    approveColumn.Text = "Approve";
                    approveColumn.UseColumnTextForButtonValue = true;
                    approveColumn.Width = 100;
                    approveColumn.FlatStyle = FlatStyle.Flat;
                    dataGridView1.Columns.Add(approveColumn);

                    DataGridViewButtonColumn declineColumn = new DataGridViewButtonColumn();
                    declineColumn.Name = "Decline";
                    declineColumn.HeaderText = "Decline";
                    declineColumn.Text = "Decline";
                    declineColumn.UseColumnTextForButtonValue = true;
                    declineColumn.Width = 100;
                    declineColumn.FlatStyle = FlatStyle.Flat;
                    dataGridView1.Columns.Add(declineColumn);

                    // Wire up cell click event
                    dataGridView1.CellContentClick += DataGridView1_CellContentClick;
                }

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
                                string reservationStatus = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : "Pending";
                                int rowIndex = dataGridView1.Rows.Add(
                                    reader["ReservationID"],
                                    reader["MemberName"],
                                    reader["BookTitle"],
                                    Convert.ToDateTime(reader["ReservationDate"]).ToString("yyyy-MM-dd"),
                                    reader["PickupDate"] != DBNull.Value ? Convert.ToDateTime(reader["PickupDate"]).ToString("yyyy-MM-dd") : "",
                                    reservationStatus,
                                    "Approve",
                                    "Decline"
                                );

                                // Style the row based on status
                                DataGridViewRow row = dataGridView1.Rows[rowIndex];
                                
                                // Disable buttons if not pending
                                if (reservationStatus != "Pending")
                                {
                                    row.Cells["Approve"].ReadOnly = true;
                                    row.Cells["Decline"].ReadOnly = true;
                                    row.Cells["Approve"].Style.BackColor = Color.LightGray;
                                    row.Cells["Decline"].Style.BackColor = Color.LightGray;
                                }
                                else
                                {
                                    row.Cells["Approve"].Style.BackColor = Color.FromArgb(76, 175, 80); // Green
                                    row.Cells["Decline"].Style.BackColor = Color.FromArgb(244, 67, 54); // Red
                                }

                                // Color code status
                                if (reservationStatus == "Pending")
                                {
                                    row.Cells["Status"].Style.ForeColor = Color.Orange;
                                    row.Cells["Status"].Style.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
                                }
                                else if (reservationStatus == "Fulfilled")
                                {
                                    row.Cells["Status"].Style.ForeColor = Color.Green;
                                }
                                else if (reservationStatus == "Cancelled")
                                {
                                    row.Cells["Status"].Style.ForeColor = Color.Red;
                                }
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
            if (string.IsNullOrWhiteSpace(tbnMember.Text))
            {
                selectedMemberId = 0;
                return;
            }

            string searchText = tbnMember.Text.Trim();
            
            // Check if text already contains validated format "ID - Name" or just "Name"
            // If it does and selectedMemberId is already set, don't re-validate
            if (selectedMemberId > 0 && searchText.Contains(" - "))
            {
                // Extract ID from format "ID - Name"
                string[] parts = searchText.Split(new[] { " - " }, StringSplitOptions.None);
                if (parts.Length >= 2 && int.TryParse(parts[0], out int existingId))
                {
                    if (existingId == selectedMemberId)
                    {
                        // Already validated, don't re-validate
                        return;
                    }
                }
            }

            selectedMemberId = 0;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    
                    string query;
                    MySqlCommand cmd;
                    bool isNumericInput = false;
                    int memberId = 0;
                    
                    // Check if input is a number (MemberID) - handle leading zeros
                    // First, try to extract ID from "ID - Name" format
                    if (searchText.Contains(" - "))
                    {
                        string[] parts = searchText.Split(new[] { " - " }, StringSplitOptions.None);
                        if (parts.Length >= 2 && int.TryParse(parts[0], out memberId))
                        {
                            isNumericInput = true;
                            searchText = parts[0]; // Use just the ID part
                        }
                    }
                    else if (int.TryParse(searchText, out memberId))
                    {
                        isNumericInput = true;
                    }
                    
                    if (isNumericInput)
                    {
                        // Exact match for MemberID
                        query = @"SELECT MemberID, FirstName, LastName, Email FROM Members 
                                WHERE MemberID = @MemberID LIMIT 1";
                        cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@MemberID", memberId);
                    }
                    else
                    {
                        // LIKE search for name or email
                        query = @"SELECT MemberID, FirstName, LastName, Email FROM Members 
                                WHERE FirstName LIKE @Search 
                                OR LastName LIKE @Search
                                OR Email LIKE @Search LIMIT 1";
                        cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@Search", "%" + searchText + "%");
                    }
                    
                    using (cmd)
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                selectedMemberId = reader.GetInt32("MemberID");
                                string firstName = reader["FirstName"] != DBNull.Value ? reader["FirstName"].ToString() : "";
                                string lastName = reader["LastName"] != DBNull.Value ? reader["LastName"].ToString() : "";
                                string email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : "";
                                
                                // If user entered an ID, show "ID - Name (Email)" format
                                // If user entered name/email, show "Name (Email)" format
                                if (isNumericInput)
                                {
                                    // Keep the ID visible: "ID - FirstName LastName"
                                    string fullName = $"{firstName} {lastName}".Trim();
                                    if (!string.IsNullOrEmpty(email))
                                    {
                                        tbnMember.Text = $"{selectedMemberId} - {fullName} ({email})";
                                    }
                                    else
                                    {
                                        tbnMember.Text = $"{selectedMemberId} - {fullName}";
                                    }
                                }
                                else
                                {
                                    // User searched by name/email, show name format
                                    if (!string.IsNullOrEmpty(email))
                                    {
                                        tbnMember.Text = $"{firstName} {lastName} ({email})".Trim();
                                    }
                                    else
                                    {
                                        tbnMember.Text = $"{firstName} {lastName}".Trim();
                                    }
                                }
                            }
                            else
                            {
                                // Member not found - clear the selected ID but keep the text
                                selectedMemberId = 0;
                                // Don't change the text if member not found - let user see what they typed
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error validating member: {ex.Message}");
                selectedMemberId = 0;
            }
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
            // Re-validate member and book before creating reservation
            // Only validate if not already validated (selectedMemberId is 0)
            if (selectedMemberId == 0)
            {
                ValidateMember();
            }
            
            if (selectedBookId == 0)
            {
                ValidateBook();
            }
            
            if (selectedMemberId == 0)
            {
                MessageBox.Show("Please enter a valid member.\n\nYou can enter:\n- Member ID (e.g., 1, 2, 3, 00006)\n- Member Name (e.g., John Doe)\n- Email (e.g., member@library.edu)\n\nAfter entering, click outside the field or press Tab to validate.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbnMember.Focus();
                return;
            }

            if (selectedBookId == 0)
            {
                MessageBox.Show("Please enter a valid book.\n\nYou can enter:\n- Book Title\n- ISBN", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnBookTitle.Focus();
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
                                                        Status ENUM('Pending', 'Fulfilled', 'Cancelled') DEFAULT 'Pending',
                                                        NotificationDate DATETIME NULL,
                                                        FulfilledDate DATETIME NULL,
                                                        FOREIGN KEY (MemberID) REFERENCES Members(MemberID) ON DELETE CASCADE,
                                                        FOREIGN KEY (BookID) REFERENCES Books(BookID) ON DELETE CASCADE
                                                        )";
                            using (MySqlCommand createCmd = new MySqlCommand(createTableQuery, conn))
                            {
                                createCmd.ExecuteNonQuery();
                            }
                        }
                    }

                    // Check if member already has a pending reservation for this book
                    string checkExistingQuery = @"SELECT COUNT(*) FROM Reservations 
                                                 WHERE MemberID = @MemberID 
                                                 AND BookID = @BookID 
                                                 AND Status = 'Pending'";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkExistingQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MemberID", selectedMemberId);
                        checkCmd.Parameters.AddWithValue("@BookID", selectedBookId);
                        int existingCount = Convert.ToInt32(checkCmd.ExecuteScalar());
                        
                        if (existingCount > 0)
                        {
                            MessageBox.Show("This member already has a pending reservation for this book.", "Duplicate Reservation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Get selected status from combo box
                    string selectedStatus = comboBox1.SelectedItem?.ToString() ?? "Pending";
                    
                    // Validate status
                    if (selectedStatus != "Pending" && selectedStatus != "Fulfilled" && selectedStatus != "Cancelled")
                    {
                        selectedStatus = "Pending"; // Default to Pending if invalid
                    }
                    
                    // Create reservation with selected status
                    string query = @"INSERT INTO Reservations (MemberID, BookID, ReservationDate, PickupDate, Status";
                    string values = @"VALUES (@MemberID, @BookID, @ReservationDate, @PickupDate, @Status";
                    
                    // Add FulfilledDate and NotificationDate only if status is Fulfilled
                    if (selectedStatus == "Fulfilled")
                    {
                        query += @", FulfilledDate, NotificationDate)";
                        values += @", NOW(), NOW())";
                    }
                    else
                    {
                        query += @")";
                        values += @")";
                    }
                    
                    query = query + " " + values;
                    
                    int newReservationId = 0;
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", selectedMemberId);
                        cmd.Parameters.AddWithValue("@BookID", selectedBookId);
                        cmd.Parameters.AddWithValue("@ReservationDate", dtpReservationDate.Value);
                        cmd.Parameters.AddWithValue("@PickupDate", dateTimePicker2.Value);
                        cmd.Parameters.AddWithValue("@Status", selectedStatus);
                        cmd.ExecuteNonQuery();
                        
                        // Get the ID of the newly created reservation
                        newReservationId = (int)cmd.LastInsertedId;
                    }

                    string statusMessage = selectedStatus == "Fulfilled" 
                        ? "Reservation created successfully and marked as Fulfilled (auto-approved)." 
                        : $"Reservation created successfully with status: {selectedStatus}.";
                    
                    MessageBox.Show($"{statusMessage}\n\nThe reservation will appear in the grid below.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Clear form
                    ClearForm();
                    
                    // Force refresh - clear search and status filter to show all
                    txtSearch.Text = "";
                    cmbBookStatus.SelectedIndex = 0;
                    
                    // Refresh metrics and grid
                    LoadMetrics();
                    LoadReservations();
                    
                    // Force grid to update
                    dataGridView1.Refresh();
                    dataGridView1.Update();
                    
                    // Scroll to the newly created reservation if possible
                    if (newReservationId > 0 && dataGridView1.Rows.Count > 0)
                    {
                        // Find and select the new reservation (should be first since ordered by ReservationDate DESC)
                        if (dataGridView1.Rows.Count > 0)
                        {
                            DataGridViewRow firstRow = dataGridView1.Rows[0];
                            if (firstRow.Cells["ReservationID"].Value != null)
                            {
                                firstRow.Selected = true;
                                dataGridView1.FirstDisplayedScrollingRowIndex = 0;
                                
                                // Highlight the new row
                                firstRow.DefaultCellStyle.BackColor = Color.LightYellow;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating reservation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Reservation creation error: {ex}");
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
            if (comboBox1 != null && comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0; // Reset to "Pending"
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadReservations();
        }

        private void cmbBookStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadReservations();
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridView gridView = sender as DataGridView;
            if (gridView == null) return;

            string columnName = gridView.Columns[e.ColumnIndex].Name;
            int reservationId = Convert.ToInt32(gridView.Rows[e.RowIndex].Cells["ReservationID"].Value);
            string status = gridView.Rows[e.RowIndex].Cells["Status"].Value?.ToString() ?? "";

            // Only allow actions on Pending reservations
            if (status != "Pending")
            {
                MessageBox.Show("Only pending reservations can be approved or declined.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (columnName == "Approve")
            {
                ApproveReservation(reservationId);
            }
            else if (columnName == "Decline")
            {
                DeclineReservation(reservationId);
            }
        }

        private void ApproveReservation(int reservationId)
        {
            try
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to approve this reservation?\n\nThis will mark the reservation as fulfilled and notify the member that the book is ready for pickup.",
                    "Approve Reservation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result != DialogResult.Yes)
                    return;

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Get reservation details first
                    string getReservationQuery = @"SELECT r.MemberID, r.BookID, b.Title, b.Available, m.FirstName, m.LastName
                                                   FROM Reservations r
                                                   INNER JOIN Books b ON r.BookID = b.BookID
                                                   INNER JOIN Members m ON r.MemberID = m.MemberID
                                                   WHERE r.ReservationID = @ReservationID";
                    
                    int memberId = 0;
                    int bookId = 0;
                    int available = 0;
                    string bookTitle = "";
                    string memberName = "";

                    using (MySqlCommand cmd = new MySqlCommand(getReservationQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@ReservationID", reservationId);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                memberId = reader.GetInt32("MemberID");
                                bookId = reader.GetInt32("BookID");
                                available = reader["Available"] != DBNull.Value ? Convert.ToInt32(reader["Available"]) : 0;
                                bookTitle = reader["Title"].ToString();
                                memberName = $"{reader["FirstName"]} {reader["LastName"]}".Trim();
                            }
                            else
                            {
                                MessageBox.Show("Reservation not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }

                    // Check if book is available
                    if (available <= 0)
                    {
                        DialogResult proceed = MessageBox.Show(
                            $"The book '{bookTitle}' is currently not available (Available: {available}).\n\nDo you want to approve the reservation anyway? The member will be notified when the book becomes available.",
                            "Book Not Available",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning
                        );

                        if (proceed != DialogResult.Yes)
                            return;
                    }

                    // Update reservation status to Fulfilled
                    string updateQuery = @"UPDATE Reservations 
                                          SET Status = 'Fulfilled', 
                                              FulfilledDate = NOW(),
                                              NotificationDate = NOW()
                                          WHERE ReservationID = @ReservationID";
                    
                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@ReservationID", reservationId);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show(
                        $"Reservation approved successfully!\n\nMember: {memberName}\nBook: {bookTitle}\n\nStatus updated to 'Fulfilled'. Member can now pick up the book.",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Reload data
                    LoadMetrics();
                    LoadReservations();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error approving reservation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeclineReservation(int reservationId)
        {
            try
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to decline this reservation?\n\nThis will cancel the reservation and the member will be notified.",
                    "Decline Reservation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result != DialogResult.Yes)
                    return;

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Get reservation details for confirmation message
                    string getReservationQuery = @"SELECT b.Title, m.FirstName, m.LastName
                                                   FROM Reservations r
                                                   INNER JOIN Books b ON r.BookID = b.BookID
                                                   INNER JOIN Members m ON r.MemberID = m.MemberID
                                                   WHERE r.ReservationID = @ReservationID";
                    
                    string bookTitle = "";
                    string memberName = "";

                    using (MySqlCommand cmd = new MySqlCommand(getReservationQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@ReservationID", reservationId);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                bookTitle = reader["Title"].ToString();
                                memberName = $"{reader["FirstName"]} {reader["LastName"]}".Trim();
                            }
                        }
                    }

                    // Update reservation status to Cancelled
                    string updateQuery = @"UPDATE Reservations 
                                          SET Status = 'Cancelled'
                                          WHERE ReservationID = @ReservationID";
                    
                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@ReservationID", reservationId);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show(
                        $"Reservation declined successfully.\n\nMember: {memberName}\nBook: {bookTitle}\n\nStatus updated to 'Cancelled'.",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Reload data
                    LoadMetrics();
                    LoadReservations();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error declining reservation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
