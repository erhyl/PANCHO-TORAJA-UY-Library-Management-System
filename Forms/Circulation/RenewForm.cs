using System;
using System.Configuration;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Project5LMS.Admin_Dashboard
{
    public partial class RenewForm : Form
    {
        private string connectionString;
        private int selectedTransactionId = 0;
        private int renewalDays = 7; // Default renewal period in days
        private int maxRenewals = 2; // Maximum number of renewals allowed

        public RenewForm()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["MySqlConnectionString"]?.ConnectionString 
                ?? "Server=localhost;Database=librarydb;Uid=root;Pwd=;";
        }

        private void RenewForm_Load(object sender, EventArgs e)
        {
            LoadRenewalPolicy();
        }

        private void LoadRenewalPolicy()
        {
            // Update policy label with actual values
            lblRenewalPolicy.Text = $"Max {maxRenewals} renewals per loan {renewalDays}+{renewalDays} days each";
        }

        private void txtTransactionID_Enter(object sender, EventArgs e)
        {
            if (txtTransactionID.Text == "Enter Transaction ID...")
            {
                txtTransactionID.Text = "";
                txtTransactionID.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void txtTransactionID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTransactionID.Text))
            {
                txtTransactionID.Text = "Enter Transaction ID...";
                txtTransactionID.ForeColor = System.Drawing.Color.Gray;
            }
        }

        private void txtTransactionID_TextChanged(object sender, EventArgs e)
        {
            if (txtTransactionID.Text != "Enter Transaction ID...")
            {
                ValidateTransaction();
            }
        }

        private void ValidateTransaction()
        {
            selectedTransactionId = 0;
            string searchText = txtTransactionID.Text.Trim();
            
            if (string.IsNullOrWhiteSpace(searchText) || searchText == "Enter Transaction ID...")
            {
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    
                    // Try to find transaction by TransactionID or Book ISBN
                    string query = @"SELECT t.TransactionID, t.BookID, t.MemberID, t.DueDate, t.BorrowDate,
                                   b.ISBN, b.Title, b.Category
                                   FROM Transactions t
                                   INNER JOIN Books b ON t.BookID = b.BookID
                                   WHERE ((t.TransactionID = @Search AND @IsNumeric = 1) 
                                          OR b.ISBN LIKE @Search 
                                          OR b.Title LIKE @Search)
                                   AND (t.Status = 'Borrowed' OR t.Status = 'Active')
                                   ORDER BY t.TransactionID DESC
                                   LIMIT 1";
                    
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        int isNumeric = 0;
                        if (int.TryParse(searchText, out int transactionId))
                        {
                            isNumeric = 1;
                            cmd.Parameters.AddWithValue("@Search", transactionId);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Search", "%" + searchText + "%");
                        }
                        cmd.Parameters.AddWithValue("@IsNumeric", isNumeric);
                        
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                selectedTransactionId = reader.GetInt32("TransactionID");
                                
                                // Try to read RenewalCount if column exists
                                int renewalCount = 0;
                                try
                                {
                                    renewalCount = reader["RenewalCount"] != DBNull.Value ? Convert.ToInt32(reader["RenewalCount"]) : 0;
                                }
                                catch
                                {
                                    // Column doesn't exist, default to 0
                                    renewalCount = 0;
                                }
                                
                                // Check if renewal is allowed
                                if (renewalCount >= maxRenewals)
                                {
                                    MessageBox.Show($"Maximum renewals ({maxRenewals}) already reached for this transaction.", 
                                        "Renewal Limit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    selectedTransactionId = 0;
                                }
                            }
                            else
                            {
                                // Transaction not found - this is okay, user might still be typing
                                selectedTransactionId = 0;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error validating transaction: {ex.Message}");
            }
        }

        private void btnProcessRenewal_Click(object sender, EventArgs e)
        {
            if (selectedTransactionId == 0)
            {
                MessageBox.Show("Please enter a valid Transaction ID or Book ISBN.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTransactionID.Focus();
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Get transaction details
                    string getTransactionQuery = @"SELECT t.TransactionID, t.BookID, t.MemberID, t.DueDate, 
                                                 b.ISBN, b.Title, b.Category,
                                                 m.MemberType, m.Status as MemberStatus
                                                 FROM Transactions t
                                                 INNER JOIN Books b ON t.BookID = b.BookID
                                                 INNER JOIN Members m ON t.MemberID = m.MemberID
                                                 WHERE t.TransactionID = @TransactionID";
                    
                    int bookId = 0;
                    int memberId = 0;
                    DateTime currentDueDate = DateTime.MinValue;
                    int currentRenewalCount = 0;
                    string memberStatus = "";
                    string bookCategory = "";

                    using (MySqlCommand cmd = new MySqlCommand(getTransactionQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@TransactionID", selectedTransactionId);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                bookId = reader.GetInt32("BookID");
                                memberId = reader.GetInt32("MemberID");
                                currentDueDate = reader["DueDate"] != DBNull.Value ? Convert.ToDateTime(reader["DueDate"]) : DateTime.MinValue;
                                
                                // Try to read RenewalCount if column exists
                                try
                                {
                                    currentRenewalCount = reader["RenewalCount"] != DBNull.Value ? Convert.ToInt32(reader["RenewalCount"]) : 0;
                                }
                                catch
                                {
                                    // Column doesn't exist, default to 0
                                    currentRenewalCount = 0;
                                }
                                
                                memberStatus = reader["MemberStatus"] != DBNull.Value ? reader["MemberStatus"].ToString() : "";
                                bookCategory = reader["Category"] != DBNull.Value ? reader["Category"].ToString() : "";
                            }
                            else
                            {
                                MessageBox.Show("Transaction not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }

                    // Validate renewal conditions
                    if (currentRenewalCount >= maxRenewals)
                    {
                        MessageBox.Show($"Maximum renewals ({maxRenewals}) already reached for this transaction.", 
                            "Renewal Limit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (memberStatus != "Active")
                    {
                        MessageBox.Show("Member is not in good standing. Renewal cannot be processed.", 
                            "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (currentDueDate < DateTime.Now)
                    {
                        MessageBox.Show("This book is overdue. Please process the return first.", 
                            "Overdue Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Check if book is reserved (if Reservations table exists)
                    try
                    {
                        string checkTableQuery = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES 
                                                  WHERE TABLE_SCHEMA = DATABASE() 
                                                  AND TABLE_NAME = 'Reservations'";
                        using (MySqlCommand checkCmd = new MySqlCommand(checkTableQuery, conn))
                        {
                            int tableExists = Convert.ToInt32(checkCmd.ExecuteScalar());
                            if (tableExists > 0)
                            {
                                string checkReservationQuery = @"SELECT COUNT(*) FROM Reservations 
                                                               WHERE BookID = @BookID 
                                                               AND Status = 'Active' 
                                                               AND MemberID != @MemberID";
                                using (MySqlCommand cmd = new MySqlCommand(checkReservationQuery, conn))
                                {
                                    cmd.Parameters.AddWithValue("@BookID", bookId);
                                    cmd.Parameters.AddWithValue("@MemberID", memberId);
                                    int reservationCount = Convert.ToInt32(cmd.ExecuteScalar());
                                    
                                    if (reservationCount > 0)
                                    {
                                        MessageBox.Show("This book is reserved by another member. Renewal cannot be processed.", 
                                            "Book Reserved", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {
                        // Reservations table doesn't exist or error checking, skip reservation check
                        System.Diagnostics.Debug.WriteLine("Reservations table check skipped");
                    }

                    // Calculate new due date
                    DateTime newDueDate = currentDueDate.AddDays(renewalDays);
                    int newRenewalCount = currentRenewalCount + 1;

                    // Update transaction - handle optional columns
                    string updateTransactionQuery = "UPDATE Transactions SET DueDate = @NewDueDate WHERE TransactionID = @TransactionID";
                    
                    // Try to update RenewalCount and LastRenewedDate if columns exist
                    try
                    {
                        string checkColumnQuery = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS 
                                                   WHERE TABLE_SCHEMA = DATABASE() 
                                                   AND TABLE_NAME = 'Transactions' 
                                                   AND COLUMN_NAME = 'RenewalCount'";
                        using (MySqlCommand checkCmd = new MySqlCommand(checkColumnQuery, conn))
                        {
                            int columnExists = Convert.ToInt32(checkCmd.ExecuteScalar());
                            if (columnExists > 0)
                            {
                                updateTransactionQuery = @"UPDATE Transactions 
                                                         SET DueDate = @NewDueDate, 
                                                             RenewalCount = @RenewalCount,
                                                             LastRenewedDate = @LastRenewedDate
                                                         WHERE TransactionID = @TransactionID";
                            }
                        }
                    }
                    catch
                    {
                        // If check fails, use basic update
                    }
                    
                    using (MySqlCommand cmd = new MySqlCommand(updateTransactionQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@TransactionID", selectedTransactionId);
                        cmd.Parameters.AddWithValue("@NewDueDate", newDueDate);
                        
                        if (updateTransactionQuery.Contains("RenewalCount"))
                        {
                            cmd.Parameters.AddWithValue("@RenewalCount", newRenewalCount);
                            cmd.Parameters.AddWithValue("@LastRenewedDate", DateTime.Now);
                        }
                        
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show($"Book renewed successfully!\nNew due date: {newDueDate:MM/dd/yyyy}\nRenewal count: {newRenewalCount}/{maxRenewals}", 
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Clear form
                    txtTransactionID.Text = "Enter Transaction ID...";
                    txtTransactionID.ForeColor = System.Drawing.Color.Gray;
                    selectedTransactionId = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing renewal: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
