using System;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

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

                    // Active Loans
                    try
                    {
                        string query = @"SELECT COUNT(*) FROM Transactions 
                                       WHERE Status = 'Borrowed' OR Status = 'Active'";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            int count = Convert.ToInt32(cmd.ExecuteScalar());
                            lblMetricValue1.Text = count.ToString();
                        }
                    }
                    catch { }

                    // Overdue
                    try
                    {
                        string query = @"SELECT COUNT(*) FROM Transactions 
                                       WHERE (Status = 'Borrowed' OR Status = 'Active') 
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
                    string query = @"SELECT MemberID, FullName, MemberType, Status, ExpirationDate 
                                   FROM Members 
                                   WHERE MemberID = @Search OR FullName LIKE @Search 
                                   OR Email LIKE @Search
                                   LIMIT 1";
                    
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Search", "%" + txtMemberID.Text.Trim() + "%");
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                selectedMemberId = reader.GetInt32("MemberID");
                                selectedMemberType = reader["MemberType"] != DBNull.Value ? reader["MemberType"].ToString() : "";
                                string status = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : "";
                                DateTime expirationDate = reader["ExpirationDate"] != DBNull.Value ? Convert.ToDateTime(reader["ExpirationDate"]) : DateTime.MinValue;

                                if (status != "Active" || expirationDate < DateTime.Now)
                                {
                                    MessageBox.Show("Member is not active or membership has expired.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    selectedMemberId = 0;
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Member not found.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error validating member: {ex.Message}");
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
                    string query = @"SELECT COUNT(*) FROM Transactions 
                                   WHERE MemberID = @MemberID 
                                   AND (Status = 'Borrowed' OR Status = 'Active')";
                    
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", selectedMemberId);
                        int currentBooks = Convert.ToInt32(cmd.ExecuteScalar());
                        int maxBooks = GetMaxBooks(selectedMemberType);

                        if (currentBooks >= maxBooks)
                        {
                            MessageBox.Show($"Member has reached the maximum limit of {maxBooks} books.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Check for outstanding fines
                    string finesQuery = @"SELECT SUM(Amount) FROM Fines 
                                        WHERE MemberID = @MemberID 
                                        AND (Status = 'Pending' OR Status = 'Unpaid')";
                    
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

                    // Process checkout
                    int loanDays = GetLoanPeriod(selectedMemberType);
                    DateTime dueDate = DateTime.Now.AddDays(loanDays);

                    string insertQuery = @"INSERT INTO Transactions 
                                         (MemberID, BookID, BorrowDate, DueDate, Status) 
                                         VALUES 
                                         (@MemberID, @BookID, @BorrowDate, @DueDate, 'Borrowed')";
                    
                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", selectedMemberId);
                        cmd.Parameters.AddWithValue("@BookID", selectedBookId);
                        cmd.Parameters.AddWithValue("@BorrowDate", DateTime.Now);
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
    }
}
