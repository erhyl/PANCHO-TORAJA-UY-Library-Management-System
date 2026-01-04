using MySql.Data.MySqlClient;
using Project5LMS.Forms.Reports;
using System;
using System.Configuration;
using System.Data;
using System.Windows.Forms;

namespace Project5LMS.Admin_Dashboard
{
    public partial class CirculationForm : Form
    {
        private string connectionString;
        private int selectedMemberId = 0;
        private int selectedBookId = 0;
        private string selectedMemberType = "";

        public CirculationForm()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["MySqlConnectionString"]?.ConnectionString 
                ?? "Server=localhost;Database=librarydb;Uid=root;Pwd=;";
        }

        private void CirculationForm_Load(object sender, EventArgs e)
        {
            LoadMetrics();
        }

        // Removed EnsureTransactionsTableExists - using CirculationRecords table from schema

        private void LoadMetrics()
        {
            lblMetricValue1.Text = "0";
            lblMetricValue2.Text = "0";
            lblMetricValue3.Text = "0";
            lblMetricValue4.Text = "0";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Active Loans - Use CirculationRecords table
                    try
                    {
                        string query = @"SELECT COUNT(*) FROM CirculationRecords 
                                       WHERE Status = 'CheckedOut'";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            int count = Convert.ToInt32(cmd.ExecuteScalar());
                            lblMetricValue1.Text = count.ToString();
                        }
                    }
                    catch { }

                    // Overdue - Use CirculationRecords table
                    try
                    {
                        string query = @"SELECT COUNT(*) FROM CirculationRecords 
                                       WHERE Status = 'CheckedOut' 
                                       AND DueDate < CURDATE()";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            int count = Convert.ToInt32(cmd.ExecuteScalar());
                            lblMetricValue2.Text = count.ToString();
                        }
                    }
                    catch { }

                    // Available Books
                    try
                    {
                        string query = "SELECT SUM(Available) FROM Books WHERE Available > 0";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            object result = cmd.ExecuteScalar();
                            int count = result != DBNull.Value && result != null ? Convert.ToInt32(result) : 0;
                            lblMetricValue3.Text = count.ToString();
                        }
                    }
                    catch { }

                    // Active Members
                    try
                    {
                        string query = @"SELECT COUNT(*) FROM Members 
                                       WHERE Status = 'Active' 
                                       AND ExpirationDate >= CURDATE()";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            int count = Convert.ToInt32(cmd.ExecuteScalar());
                            lblMetricValue4.Text = count.ToString();
                        }
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading metrics: {ex.Message}");
            }
        }

        private void txtMemberID_TextChanged(object sender, EventArgs e)
        {
            UpdateDueDate();
        }

        private void txtMemberID_Leave(object sender, EventArgs e)
        {
            ValidateMember();
        }

        private void txtBookISBN_TextChanged(object sender, EventArgs e)
        {
            UpdateDueDate();
        }

        private void txtBookISBN_Leave(object sender, EventArgs e)
        {
            ValidateBook();
        }

        private void ValidateMember()
        {
            selectedMemberId = 0;
            selectedMemberType = "";

            if (string.IsNullOrWhiteSpace(txtMemberID.Text))
            {
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    
                    string searchText = txtMemberID.Text.Trim();
                    string query;
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    
                    // Check if input is numeric (MemberID) or text (Name/Email)
                    int memberIdSearch = 0;
                    bool isNumeric = int.TryParse(searchText, out memberIdSearch);
                    
                    if (isNumeric)
                    {
                        // Search by exact MemberID
                        query = @"SELECT MemberID, FirstName, LastName, MemberType, Status, ExpirationDate 
                                 FROM Members 
                                 WHERE MemberID = @MemberID
                                 LIMIT 1";
                        cmd.CommandText = query;
                        cmd.Parameters.AddWithValue("@MemberID", memberIdSearch);
                    }
                    else
                    {
                        // Search by Name or Email with LIKE
                        query = @"SELECT MemberID, FirstName, LastName, MemberType, Status, ExpirationDate 
                                 FROM Members 
                                 WHERE FirstName LIKE @Search 
                                 OR LastName LIKE @Search
                                 OR Email LIKE @Search
                                 OR CONCAT(FirstName, ' ', LastName) LIKE @Search
                                 LIMIT 1";
                        cmd.CommandText = query;
                        cmd.Parameters.AddWithValue("@Search", "%" + searchText + "%");
                    }
                    
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            selectedMemberId = reader.GetInt32("MemberID");
                            selectedMemberType = reader["MemberType"] != DBNull.Value ? reader["MemberType"].ToString() : "";
                            string status = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : "";
                            DateTime expirationDate = reader["ExpirationDate"] != DBNull.Value ? Convert.ToDateTime(reader["ExpirationDate"]) : DateTime.MinValue;

                            // Check if member is active
                            if (status != "Active")
                            {
                                MessageBox.Show($"Member status is '{status}'. Only active members can checkout books.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                selectedMemberId = 0;
                                selectedMemberType = "";
                                return;
                            }

                            // Check if membership has expired (if ExpirationDate is set)
                            if (expirationDate != DateTime.MinValue && expirationDate < DateTime.Now.Date)
                            {
                                MessageBox.Show($"Member's membership expired on {expirationDate:MM/dd/yyyy}. Please renew membership.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                selectedMemberId = 0;
                                selectedMemberType = "";
                                return;
                            }

                            // Show member found confirmation
                            string memberName = $"{reader["FirstName"]} {reader["LastName"]}".Trim();
                            System.Diagnostics.Debug.WriteLine($"Member found: {memberName} (ID: {selectedMemberId}, Type: {selectedMemberType})");
                        }
                        else
                        {
                            MessageBox.Show("Member not found. Please check the Member ID, Name, or Email.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            selectedMemberId = 0;
                            selectedMemberType = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error validating member: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Error validating member: {ex.Message}");
                selectedMemberId = 0;
                selectedMemberType = "";
            }

            UpdateDueDate();
        }

        private void ValidateBook()
        {
            selectedBookId = 0;

            if (string.IsNullOrWhiteSpace(txtBookISBN.Text))
            {
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT BookID, Title, ISBN, Available 
                                   FROM Books 
                                   WHERE ISBN LIKE @Search OR Title LIKE @Search
                                   LIMIT 1";
                    
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Search", "%" + txtBookISBN.Text.Trim() + "%");
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                selectedBookId = reader.GetInt32("BookID");
                                int available = reader["Available"] != DBNull.Value ? Convert.ToInt32(reader["Available"]) : 0;

                                if (available <= 0)
                                {
                                    MessageBox.Show("Book is not available.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    selectedBookId = 0;
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Book not found.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error validating book: {ex.Message}");
            }

            UpdateDueDate();
        }

        private void UpdateDueDate()
        {
            if (selectedMemberId == 0 || selectedBookId == 0 || string.IsNullOrEmpty(selectedMemberType))
            {
                lblDueDate.Text = "Due Date: --";
                return;
            }

            int loanDays = GetLoanPeriod(selectedMemberType);
            DateTime dueDate = DateTime.Now.AddDays(loanDays);
            lblDueDate.Text = $"Due Date: {dueDate.ToString("MM/dd/yyyy")}";
        }

        private int GetLoanPeriod(string memberType)
        {
            switch (memberType.ToLower())
            {
                case "student":
                    return 14;
                case "faculty":
                    return 30;
                case "staff":
                    return 21;
                case "guest":
                    return 7;
                default:
                    return 14;
            }
        }

        private int GetMaxBooks(string memberType)
        {
            switch (memberType.ToLower())
            {
                case "student":
                    return 5;
                case "faculty":
                    return 10;
                case "staff":
                    return 7;
                case "guest":
                    return 3;
                default:
                    return 5;
            }
        }

        private void btnProcessCheckout_Click(object sender, EventArgs e)
        {
            if (selectedMemberId == 0)
            {
                MessageBox.Show("Please enter a valid Member ID or Name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMemberID.Focus();
                return;
            }

            if (selectedBookId == 0)
            {
                MessageBox.Show("Please enter a valid Book ISBN or Title.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBookISBN.Focus();
                return;
            }

                    // Check if member has reached max books limit
                    try
                    {
                        using (MySqlConnection conn = new MySqlConnection(connectionString))
                        {
                            conn.Open();
                            // Use CirculationRecords table with correct Status values
                            string query = @"SELECT COUNT(*) FROM CirculationRecords 
                                           WHERE MemberID = @MemberID 
                                           AND Status = 'CheckedOut'";
                            
                            using (MySqlCommand cmd = new MySqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@MemberID", selectedMemberId);
                                int currentBooks = Convert.ToInt32(cmd.ExecuteScalar());
                                int maxBooks = GetMaxBooks(selectedMemberType);

                                if (currentBooks >= maxBooks)
                                {
                                    MessageBox.Show($"Member has reached the maximum limit of {maxBooks} books. Please return some books first.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }

                    // Check for outstanding fines
                    string finesQuery = @"SELECT SUM(FineAmount) FROM Fines 
                                        WHERE MemberID = @MemberID 
                                        AND Status = 'Pending'";
                    
                    using (MySqlCommand cmd = new MySqlCommand(finesQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", selectedMemberId);
                        object result = cmd.ExecuteScalar();
                        decimal totalFines = result != DBNull.Value && result != null ? Convert.ToDecimal(result) : 0;

                        if (totalFines > 0)
                        {
                            DialogResult dialogResult = MessageBox.Show($"Member has outstanding fines of ₱{totalFines:F2}. Do you want to proceed?", "Outstanding Fines", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (dialogResult == DialogResult.No)
                            {
                                return;
                            }
                        }
                    }

                    // Process checkout - Use CirculationRecords table (correct table name)
                    int loanDays = GetLoanPeriod(selectedMemberType);
                    DateTime dueDate = DateTime.Now.AddDays(loanDays);

                    string insertQuery = @"INSERT INTO CirculationRecords 
                                         (MemberID, BookID, CheckoutDate, DueDate, Status) 
                                         VALUES 
                                         (@MemberID, @BookID, @CheckoutDate, @DueDate, 'CheckedOut')";
                    
                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", selectedMemberId);
                        cmd.Parameters.AddWithValue("@BookID", selectedBookId);
                        cmd.Parameters.AddWithValue("@CheckoutDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@DueDate", dueDate);
                        cmd.ExecuteNonQuery();
                    }

                    // Update book availability
                    string updateQuery = "UPDATE Books SET Available = Available - 1 WHERE BookID = @BookID";
                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookID", selectedBookId);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Book checked out successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Clear form
                    txtMemberID.Clear();
                    txtBookISBN.Clear();
                    lblDueDate.Text = "Due Date: --";
                    selectedMemberId = 0;
                    selectedBookId = 0;
                    selectedMemberType = "";

                    // Reload metrics
                    LoadMetrics();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing checkout: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabReturn)
            {
                LoadReturnTab();
            }
            else if (tabControl.SelectedTab == tabRenewal)
            {
                LoadRenewalTab();
            }
            else if (tabControl.SelectedTab == tabHistory)
            {
                LoadHistoryTab();
            }
        }

        private void LoadReturnTab()
        {
            tabReturn.Controls.Clear();
            ReturnForm returnForm = new ReturnForm();
            returnForm.TopLevel = false;
            returnForm.FormBorderStyle = FormBorderStyle.None;
            returnForm.Dock = DockStyle.Fill;
            tabReturn.Controls.Add(returnForm);
            returnForm.Show();
        }

        private void LoadRenewalTab()
        {
            tabRenewal.Controls.Clear();
            RenewForm renewForm = new RenewForm();
            renewForm.TopLevel = false;
            renewForm.FormBorderStyle = FormBorderStyle.None;
            renewForm.Dock = DockStyle.Fill;
            tabRenewal.Controls.Add(renewForm);
            renewForm.Show();
        }

        private void LoadHistoryTab()
        {
            tabHistory.Controls.Clear();
            HistoryForm historyForm = new HistoryForm();
            historyForm.TopLevel = false;
            historyForm.FormBorderStyle = FormBorderStyle.None;
            historyForm.Dock = DockStyle.Fill;
            tabHistory.Controls.Add(historyForm);
            historyForm.Show();
        }

        private void panelBottomSection_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabCheckout_Click(object sender, EventArgs e)
        {
            
        }
    }
}
