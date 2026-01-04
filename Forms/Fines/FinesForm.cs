using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Project5LMS.Admin_Dashboard
{
    public partial class FinesForm : Form
    {
        private string connectionString;

        public FinesForm()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["MySqlConnectionString"]?.ConnectionString 
                ?? "Server=localhost;Database=librarydb;Uid=root;Pwd=;";
        }

        private void FinesForm_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadMetrics();
            LoadFines();
            cmbStatusFilter.SelectedIndex = 0;
            cmbBookType.SelectedIndex = 0;
            CalculateFine();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (this.Visible)
            {
                LoadMetrics();
                LoadFines();
            }
        }

        private void SetupDataGridView()
        {
            dta_Fines.AutoGenerateColumns = false;
            dta_Fines.Columns.Clear();
            dta_Fines.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 11F);
            dta_Fines.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Bold);
            dta_Fines.RowTemplate.Height = 50;
            dta_Fines.DefaultCellStyle.Padding = new Padding(15, 5, 15, 5);
            dta_Fines.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            dta_Fines.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dta_Fines.EnableHeadersVisualStyles = false;
            dta_Fines.AllowUserToAddRows = false;
            dta_Fines.AllowUserToDeleteRows = false;
            dta_Fines.ReadOnly = true;
            dta_Fines.RowHeadersVisible = false;
            dta_Fines.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dta_Fines.BackgroundColor = Color.White;
            dta_Fines.BorderStyle = BorderStyle.None;
            dta_Fines.GridColor = Color.FromArgb(240, 240, 240);

            // Add columns
            dta_Fines.Columns.Add("FineID", "FineID");
            dta_Fines.Columns["FineID"].Visible = false;

            dta_Fines.Columns.Add("Member", "Member");
            dta_Fines.Columns["Member"].Width = 200;

            dta_Fines.Columns.Add("Book", "Book");
            dta_Fines.Columns["Book"].Width = 250;

            dta_Fines.Columns.Add("Reason", "Reason");
            dta_Fines.Columns["Reason"].Width = 200;

            dta_Fines.Columns.Add("Amount", "Amount");
            dta_Fines.Columns["Amount"].Width = 100;

            dta_Fines.Columns.Add("Date", "Date");
            dta_Fines.Columns["Date"].Width = 150;

            dta_Fines.Columns.Add("Status", "Status");
            dta_Fines.Columns["Status"].Width = 120;

            DataGridViewButtonColumn actionsColumn = new DataGridViewButtonColumn();
            actionsColumn.Name = "Actions";
            actionsColumn.HeaderText = "Actions";
            actionsColumn.Text = "Pay";
            actionsColumn.UseColumnTextForButtonValue = true;
            actionsColumn.Width = 100;
            actionsColumn.FlatStyle = FlatStyle.Flat;
            dta_Fines.Columns.Add(actionsColumn);
        }

        private void LoadMetrics()
        {
            lblMetricValue1.Text = "₱0";
            lblMetricValue2.Text = "₱0";
            lblMetricValue3.Text = "0";
            lblMetricValue4.Text = "0";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Pending Fines (sum of unpaid fines) - Fines table uses Status enum
                    try
                    {
                        string query = @"SELECT COALESCE(SUM(FineAmount), 0) FROM Fines WHERE Status = 'Pending'";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            object result = cmd.ExecuteScalar();
                            decimal amount = result != DBNull.Value && result != null ? Convert.ToDecimal(result) : 0;
                            lblMetricValue1.Text = $"₱{amount:F0}";
                        }
                    }
                    catch { }

                    // Collected (sum of paid fines)
                    try
                    {
                        string query = "SELECT COALESCE(SUM(FineAmount), 0) FROM Fines WHERE Status = 'Paid'";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            object result = cmd.ExecuteScalar();
                            decimal amount = result != DBNull.Value && result != null ? Convert.ToDecimal(result) : 0;
                            lblMetricValue2.Text = $"₱{amount:F0}";
                        }
                    }
                    catch { }

                    // Unpaid Fines (count)
                    try
                    {
                        string query = "SELECT COUNT(*) FROM Fines WHERE Status = 'Pending'";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            int count = Convert.ToInt32(cmd.ExecuteScalar());
                            lblMetricValue3.Text = count.ToString();
                        }
                    }
                    catch { }

                    // Total Records (count)
                    try
                    {
                        string query = "SELECT COUNT(*) FROM Fines";
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

        private void LoadFines()
        {
            try
            {
                dta_Fines.Rows.Clear();

                string keyword = txtSearch.Text.Trim();
                if (keyword == "Search transactions...") keyword = "";

                string statusFilter = cmbStatusFilter.Text;
                if (statusFilter == "All Status") statusFilter = "";

                // Query using CirculationRecords table (correct table name)
                // Fines table uses FineAmount, Status (enum: 'Pending', 'Paid', 'Waived'), and RecordID
                string query = @"SELECT 
                                    f.FineID,
                                    f.FineAmount as Amount,
                                    f.Status,
                                    cr.DueDate,
                                    cr.CheckoutDate,
                                    CONCAT(m.FirstName, ' ', m.LastName) as MemberName,
                                    b.Title as BookTitle
                                FROM Fines f
                                INNER JOIN Members m ON f.MemberID = m.MemberID
                                LEFT JOIN CirculationRecords cr ON f.RecordID = cr.RecordID
                                LEFT JOIN Books b ON cr.BookID = b.BookID
                                WHERE (@Keyword = '' 
                                       OR m.FirstName LIKE @Keyword 
                                       OR m.LastName LIKE @Keyword
                                       OR COALESCE(b.Title, '') LIKE @Keyword
                                       OR CAST(f.FineID AS CHAR) LIKE @Keyword)
                                AND (@Status = '' OR 
                                     (@Status = 'Unpaid' AND f.Status = 'Pending') OR
                                     (@Status = 'Paid' AND f.Status = 'Paid'))
                                ORDER BY cr.CheckoutDate DESC, f.FineID DESC
                                LIMIT 500";

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                    cmd.Parameters.AddWithValue("@Status", statusFilter);
                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int fineId = reader.GetInt32("FineID");
                            decimal amount = reader["Amount"] != DBNull.Value ? Convert.ToDecimal(reader["Amount"]) : 0;
                            string statusValue = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : "Pending";
                            // Map Status enum to display: 'Pending' -> 'Unpaid', 'Paid' -> 'Paid', 'Waived' -> 'Waived'
                            string status = statusValue == "Paid" ? "Paid" : (statusValue == "Waived" ? "Waived" : "Unpaid");
                            DateTime? dueDate = reader["DueDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["DueDate"]) : null;
                            DateTime? checkoutDate = reader["CheckoutDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["CheckoutDate"]) : null;
                            string memberName = reader["MemberName"].ToString();
                            string bookTitle = reader["BookTitle"] != DBNull.Value ? reader["BookTitle"].ToString() : "N/A";

                            // Calculate reason (days overdue)
                            string reason = "Overdue";
                            if (dueDate.HasValue && checkoutDate.HasValue)
                            {
                                int daysOverdue = (DateTime.Now - dueDate.Value).Days;
                                if (daysOverdue > 0)
                                {
                                    reason = $"Overdue - {daysOverdue} day(s) late";
                                }
                            }
                            
                            DateTime createdDate = checkoutDate ?? DateTime.Now;

                            string dateDisplay = createdDate != DateTime.MinValue ? createdDate.ToString("yyyy-MM-dd") : "N/A";
                            string amountDisplay = $"₱{amount:F0}";

                            int rowIndex = dta_Fines.Rows.Add(
                                fineId,
                                memberName,
                                bookTitle,
                                reason,
                                amountDisplay,
                                dateDisplay,
                                status,
                                "Pay"
                            );

                            DataGridViewRow row = dta_Fines.Rows[rowIndex];

                            // Style Status column (green for Unpaid, gray for Paid)
                            if (status == "Unpaid")
                            {
                                row.Cells["Status"].Style.ForeColor = Color.FromArgb(76, 175, 80);
                                row.Cells["Status"].Style.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
                            }
                            else // Paid
                            {
                                row.Cells["Status"].Style.ForeColor = Color.Gray;
                                row.Cells["Status"].Style.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading fines: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search transactions...")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Search transactions...";
                txtSearch.ForeColor = Color.Gray;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "Search transactions...")
            {
                LoadFines();
            }
        }

        private void cmbStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadFines();
        }

        private void dta_Fines_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dta_Fines.Columns[e.ColumnIndex].Name == "Actions")
            {
                int fineId = Convert.ToInt32(dta_Fines.Rows[e.RowIndex].Cells["FineID"].Value);
                string status = dta_Fines.Rows[e.RowIndex].Cells["Status"].Value.ToString();
                string amount = dta_Fines.Rows[e.RowIndex].Cells["Amount"].Value.ToString();

                if (status == "Paid")
                {
                    MessageBox.Show("This fine has already been paid.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DialogResult result = MessageBox.Show($"Process payment for {amount}?", "Confirm Payment", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    ProcessPayment(fineId);
                }
            }
        }

        private void ProcessPayment(int fineId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    // Fines table uses Status enum ('Pending', 'Paid', 'Waived')
                    string query = "UPDATE Fines SET Status = 'Paid', PaidDate = NOW() WHERE FineID = @FineID";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FineID", fineId);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Payment processed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadMetrics();
                LoadFines();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing payment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDayOverdue_TextChanged(object sender, EventArgs e)
        {
            CalculateFine();
        }

        private void cmbBookType_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateFine();
        }

        private void CalculateFine()
        {
            try
            {
                int daysOverdue = 0;
                if (!string.IsNullOrWhiteSpace(txtDayOverdue.Text))
                {
                    if (!int.TryParse(txtDayOverdue.Text, out daysOverdue))
                    {
                        daysOverdue = 0;
                    }
                }

                decimal dailyFine = 0;
                decimal maxFine = 0;

                if (cmbBookType.SelectedIndex == 0) // Regular
                {
                    dailyFine = 5;
                    maxFine = 300;
                }
                else if (cmbBookType.SelectedIndex == 1) // Reference
                {
                    dailyFine = 10;
                    maxFine = 500;
                }

                // Apply grace period (1 day)
                int daysToCharge = Math.Max(0, daysOverdue - 1);

                decimal calculatedFine = dailyFine * daysToCharge;
                if (calculatedFine > maxFine)
                {
                    calculatedFine = maxFine;
                }

                lblCalculatedFine.Text = $"₱{calculatedFine:F0}";
                lblMaxFine.Text = $"• Maximum fine: ₱{maxFine:F0} per book";
            }
            catch
            {
                lblCalculatedFine.Text = "₱0";
            }
        }
    }
}
