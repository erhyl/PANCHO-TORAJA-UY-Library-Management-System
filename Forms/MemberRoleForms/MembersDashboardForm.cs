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
using System.Configuration;
using Project5LMS.Helpers;

namespace Project5LMS.Forms.MemberRoleForms
{
    public partial class MembersDashboardForm : Form
    {
        private string connectionString;

        public MembersDashboardForm()
        {
            InitializeComponent();
            connectionString = DatabaseHelper.GetConnectionString();
            
            // Wire up button click event
            if (btnMembersBookRenew != null)
            {
                btnMembersBookRenew.Click += btnMembersBookRenew_Click;
            }
        }

        private void MembersDashboardForm_Load(object sender, EventArgs e)
        {
            LoadDashboard();
        }

        private void LoadDashboard()
        {
            int memberID = CurrentUser.GetMemberID();
            if (memberID == 0)
            {
                MessageBox.Show("Unable to identify your member account. Please contact an administrator.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LoadMetrics(memberID);
            LoadCurrentBooks(memberID);
        }

        private void LoadMetrics(int memberID)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Books Borrowed (Active loans)
                    try
                    {
                        string query = @"SELECT COUNT(*) FROM CirculationRecords 
                                       WHERE MemberID = @MemberID 
                                       AND Status = 'CheckedOut'";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MemberID", memberID);
                            int borrowed = Convert.ToInt32(cmd.ExecuteScalar());
                            lblMetricValue1.Text = borrowed.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error loading borrowed books: {ex.Message}");
                        lblMetricValue1.Text = "0";
                    }

                    // Overdue Books
                    try
                    {
                        string query = @"SELECT COUNT(*) FROM CirculationRecords 
                                       WHERE MemberID = @MemberID 
                                       AND Status = 'CheckedOut' 
                                       AND DueDate < CURDATE()";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MemberID", memberID);
                            int overdue = Convert.ToInt32(cmd.ExecuteScalar());
                            lblMetricValue2.Text = overdue.ToString();
                            
                            // Highlight if overdue
                            if (overdue > 0)
                            {
                                lblMetricValue2.ForeColor = Color.Red;
                                lblMetricValue2.Font = new Font("Microsoft Sans Serif", 24F, FontStyle.Bold);
                            }
                            else
                            {
                                lblMetricValue2.ForeColor = Color.FromArgb(64, 64, 64);
                                lblMetricValue2.Font = new Font("Microsoft Sans Serif", 24F, FontStyle.Bold);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error loading overdue books: {ex.Message}");
                        lblMetricValue2.Text = "0";
                    }

                    // Total Fines (Pending fines)
                    try
                    {
                        string query = @"SELECT COALESCE(SUM(FineAmount), 0) FROM Fines 
                                       WHERE MemberID = @MemberID 
                                       AND Status = 'Pending'";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MemberID", memberID);
                            object result = cmd.ExecuteScalar();
                            decimal totalFines = result != DBNull.Value && result != null ? Convert.ToDecimal(result) : 0;
                            lblMetricValue3.Text = $"₱{totalFines:F0}";
                            
                            // Highlight if has fines
                            if (totalFines > 0)
                            {
                                lblMetricValue3.ForeColor = Color.Red;
                                lblMetricValue3.Font = new Font("Microsoft Sans Serif", 24F, FontStyle.Bold);
                            }
                            else
                            {
                                lblMetricValue3.ForeColor = Color.FromArgb(64, 64, 64);
                                lblMetricValue3.Font = new Font("Microsoft Sans Serif", 24F, FontStyle.Bold);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error loading fines: {ex.Message}");
                        lblMetricValue3.Text = "₱0";
                    }

                    // Active Reservations
                    try
                    {
                        string query = @"SELECT COUNT(*) FROM Reservations 
                                       WHERE MemberID = @MemberID 
                                       AND (Status = 'Pending' OR Status = 'Fulfilled')";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MemberID", memberID);
                            int reservations = Convert.ToInt32(cmd.ExecuteScalar());
                            lblMetricValue4.Text = reservations.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error loading reservations: {ex.Message}");
                        lblMetricValue4.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading dashboard metrics: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCurrentBooks(int memberID)
        {
            try
            {
                dta_GridMembersCurrentBooks.Rows.Clear();
                dta_GridMembersCurrentBooks.Columns.Clear();

                // Setup DataGridView
                dta_GridMembersCurrentBooks.AutoGenerateColumns = false;
                dta_GridMembersCurrentBooks.AllowUserToAddRows = false;
                dta_GridMembersCurrentBooks.ReadOnly = true;
                dta_GridMembersCurrentBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dta_GridMembersCurrentBooks.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10F);
                dta_GridMembersCurrentBooks.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);

                // Add columns
                dta_GridMembersCurrentBooks.Columns.Add("RecordID", "ID");
                dta_GridMembersCurrentBooks.Columns["RecordID"].Visible = false;

                dta_GridMembersCurrentBooks.Columns.Add("Title", "Book Title");
                dta_GridMembersCurrentBooks.Columns["Title"].Width = 300;

                dta_GridMembersCurrentBooks.Columns.Add("Author", "Author");
                dta_GridMembersCurrentBooks.Columns["Author"].Width = 200;

                dta_GridMembersCurrentBooks.Columns.Add("CheckoutDate", "Checkout Date");
                dta_GridMembersCurrentBooks.Columns["CheckoutDate"].Width = 150;

                dta_GridMembersCurrentBooks.Columns.Add("DueDate", "Due Date");
                dta_GridMembersCurrentBooks.Columns["DueDate"].Width = 150;

                dta_GridMembersCurrentBooks.Columns.Add("DaysRemaining", "Days Remaining");
                dta_GridMembersCurrentBooks.Columns["DaysRemaining"].Width = 120;

                dta_GridMembersCurrentBooks.Columns.Add("Status", "Status");
                dta_GridMembersCurrentBooks.Columns["Status"].Width = 100;

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"SELECT 
                                        cr.RecordID,
                                        b.Title,
                                        b.Author,
                                        cr.CheckoutDate,
                                        cr.DueDate,
                                        cr.Status
                                    FROM CirculationRecords cr
                                    INNER JOIN Books b ON cr.BookID = b.BookID
                                    WHERE cr.MemberID = @MemberID
                                    AND cr.Status = 'CheckedOut'
                                    ORDER BY cr.DueDate ASC";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", memberID);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int recordID = reader.GetInt32("RecordID");
                                string title = reader["Title"].ToString();
                                string author = reader["Author"] != DBNull.Value ? reader["Author"].ToString() : "N/A";
                                DateTime checkoutDate = Convert.ToDateTime(reader["CheckoutDate"]);
                                DateTime dueDate = Convert.ToDateTime(reader["DueDate"]);
                                string status = reader["Status"].ToString();

                                // Calculate days remaining
                                int daysRemaining = (dueDate.Date - DateTime.Now.Date).Days;
                                string daysRemainingText = daysRemaining > 0 ? $"{daysRemaining} days" : 
                                                          daysRemaining == 0 ? "Due today" : 
                                                          $"{Math.Abs(daysRemaining)} days overdue";

                                int rowIndex = dta_GridMembersCurrentBooks.Rows.Add(
                                    recordID,
                                    title,
                                    author,
                                    checkoutDate.ToString("yyyy-MM-dd"),
                                    dueDate.ToString("yyyy-MM-dd"),
                                    daysRemainingText,
                                    status
                                );

                                // Style row based on due date
                                DataGridViewRow row = dta_GridMembersCurrentBooks.Rows[rowIndex];
                                
                                if (daysRemaining < 0)
                                {
                                    // Overdue - red
                                    row.DefaultCellStyle.ForeColor = Color.Red;
                                    row.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
                                }
                                else if (daysRemaining <= 3)
                                {
                                    // Due soon - orange
                                    row.DefaultCellStyle.ForeColor = Color.Orange;
                                    row.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading current books: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMembersBookRenew_Click(object sender, EventArgs e)
        {
            if (dta_GridMembersCurrentBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a book to renew.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataGridViewRow selectedRow = dta_GridMembersCurrentBooks.SelectedRows[0];
            int recordID = Convert.ToInt32(selectedRow.Cells["RecordID"].Value);
            string bookTitle = selectedRow.Cells["Title"].Value.ToString();
            string daysRemaining = selectedRow.Cells["DaysRemaining"].Value.ToString();

            DialogResult result = MessageBox.Show(
                $"Do you want to renew '{bookTitle}'?\n\nCurrent due date information: {daysRemaining}",
                "Renew Book",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                RenewBook(recordID);
            }
        }

        private void RenewBook(int recordID)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Get current record info
                    string getRecordQuery = @"SELECT DueDate, RenewedCount FROM CirculationRecords 
                                            WHERE RecordID = @RecordID";
                    DateTime currentDueDate = DateTime.Now;
                    int renewedCount = 0;

                    using (MySqlCommand cmd = new MySqlCommand(getRecordQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@RecordID", recordID);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                currentDueDate = Convert.ToDateTime(reader["DueDate"]);
                                renewedCount = reader["RenewedCount"] != DBNull.Value ? Convert.ToInt32(reader["RenewedCount"]) : 0;
                            }
                        }
                    }

                    // Check renewal limit (max 2 renewals)
                    if (renewedCount >= 2)
                    {
                        MessageBox.Show("This book has already been renewed the maximum number of times (2).", "Renewal Limit Reached", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Extend due date by 14 days
                    DateTime newDueDate = currentDueDate.AddDays(14);

                    // Update record
                    string updateQuery = @"UPDATE CirculationRecords 
                                          SET DueDate = @NewDueDate, 
                                              RenewedCount = RenewedCount + 1
                                          WHERE RecordID = @RecordID";
                    
                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@NewDueDate", newDueDate);
                        cmd.Parameters.AddWithValue("@RecordID", recordID);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show($"Book renewed successfully!\n\nNew due date: {newDueDate:MM/dd/yyyy}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Reload data
                    int memberID = CurrentUser.GetMemberID();
                    LoadCurrentBooks(memberID);
                    LoadMetrics(memberID);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error renewing book: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblMetricValue1_Click(object sender, EventArgs e)
        {
            // Optional: Navigate to My Books when clicked
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            // Panel paint event
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            // Panel paint event
        }

        private void lblMetricTitle1_Click(object sender, EventArgs e)
        {
            // Optional: Navigate to My Books when clicked
        }

        private void lblBooksOverdue_Click(object sender, EventArgs e)
        {
            // Optional: Filter to show only overdue books
        }

        private void lblMembersFines_Click(object sender, EventArgs e)
        {
            // Optional: Navigate to fines view
        }

        private void lblBooksReservation_Click(object sender, EventArgs e)
        {
            // Optional: Navigate to reservations view
        }

        private void lblMetricValue2_Click(object sender, EventArgs e)
        {
            // Optional: Filter to show only overdue books
        }

        private void lblMetricValue3_Click(object sender, EventArgs e)
        {
            // Optional: Navigate to fines view
        }

        private void lblMetricValue4_Click(object sender, EventArgs e)
        {
            // Optional: Navigate to reservations view
        }

        private void dta_GridMembersCurrentBooks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Optional: Handle cell clicks if needed
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (this.Visible)
            {
                LoadDashboard();
            }
        }
    }
}
