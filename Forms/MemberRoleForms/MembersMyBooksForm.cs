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
    public partial class MembersMyBooksForm : Form
    {
        private string connectionString;

        public MembersMyBooksForm()
        {
            InitializeComponent();
            connectionString = DatabaseHelper.GetConnectionString();
            this.Load += MembersMyBooksForm_Load;
        }

        private void MembersMyBooksForm_Load(object sender, EventArgs e)
        {
            // This form will be populated via code
            // For now, we'll create a simple layout programmatically
            SetupForm();
            LoadMyBooks();
        }

        private void SetupForm()
        {
            this.AutoScroll = true;
            this.BackColor = Color.White;
        }

        private void LoadMyBooks()
        {
            int memberID = CurrentUser.GetMemberID();
            if (memberID == 0)
            {
                Label lblError = new Label();
                lblError.Text = "Unable to identify your member account. Please contact an administrator.";
                lblError.AutoSize = true;
                lblError.Location = new Point(20, 20);
                lblError.Font = new Font("Microsoft Sans Serif", 11F);
                lblError.ForeColor = Color.Red;
                this.Controls.Add(lblError);
                return;
            }

            try
            {
                int yPos = 20;

                // Title
                Label lblTitle = new Label();
                lblTitle.Text = "My Books";
                lblTitle.Font = new Font("Microsoft Sans Serif", 20F, FontStyle.Bold);
                lblTitle.Location = new Point(20, yPos);
                lblTitle.AutoSize = true;
                this.Controls.Add(lblTitle);
                yPos += 50;

                // Create tabs using panels
                Panel pnlBorrowed = new Panel();
                pnlBorrowed.Location = new Point(20, yPos);
                pnlBorrowed.Size = new Size(this.ClientSize.Width - 40, this.ClientSize.Height - yPos - 20);
                pnlBorrowed.AutoScroll = true;
                pnlBorrowed.BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(pnlBorrowed);

                Label lblBorrowedTitle = new Label();
                lblBorrowedTitle.Text = "Borrowed Books";
                lblBorrowedTitle.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold);
                lblBorrowedTitle.Location = new Point(10, 10);
                lblBorrowedTitle.AutoSize = true;
                pnlBorrowed.Controls.Add(lblBorrowedTitle);

                // DataGridView for borrowed books
                DataGridView dgvBorrowed = new DataGridView();
                dgvBorrowed.Location = new Point(10, 50);
                dgvBorrowed.Size = new Size(pnlBorrowed.Width - 20, 300);
                dgvBorrowed.AutoGenerateColumns = false;
                dgvBorrowed.AllowUserToAddRows = false;
                dgvBorrowed.ReadOnly = true;
                dgvBorrowed.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                pnlBorrowed.Controls.Add(dgvBorrowed);

                // Load borrowed books
                LoadBorrowedBooks(dgvBorrowed, memberID);

                yPos += 380;

                // Reservations panel
                Panel pnlReservations = new Panel();
                pnlReservations.Location = new Point(20, yPos);
                pnlReservations.Size = new Size(this.ClientSize.Width - 40, this.ClientSize.Height - yPos - 20);
                pnlReservations.AutoScroll = true;
                pnlReservations.BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(pnlReservations);

                Label lblReservationsTitle = new Label();
                lblReservationsTitle.Text = "My Reservations";
                lblReservationsTitle.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold);
                lblReservationsTitle.Location = new Point(10, 10);
                lblReservationsTitle.AutoSize = true;
                pnlReservations.Controls.Add(lblReservationsTitle);

                // DataGridView for reservations
                DataGridView dgvReservations = new DataGridView();
                dgvReservations.Location = new Point(10, 50);
                dgvReservations.Size = new Size(pnlReservations.Width - 20, 250);
                dgvReservations.AutoGenerateColumns = false;
                dgvReservations.AllowUserToAddRows = false;
                dgvReservations.ReadOnly = true;
                dgvReservations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                pnlReservations.Controls.Add(dgvReservations);

                // Load reservations
                LoadReservations(dgvReservations, memberID);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading books: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadBorrowedBooks(DataGridView dgv, int memberID)
        {
            try
            {
                dgv.Columns.Clear();

                dgv.Columns.Add("TransactionID", "ID");
                dgv.Columns["TransactionID"].Visible = false;
                dgv.Columns.Add("BookTitle", "Book Title");
                dgv.Columns["BookTitle"].Width = 300;
                dgv.Columns.Add("BorrowDate", "Borrowed Date");
                dgv.Columns["BorrowDate"].Width = 150;
                dgv.Columns.Add("DueDate", "Due Date");
                dgv.Columns["DueDate"].Width = 150;
                dgv.Columns.Add("Status", "Status");
                dgv.Columns["Status"].Width = 120;

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Check if Transactions table exists
                    string checkTableQuery = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES 
                                              WHERE TABLE_SCHEMA = DATABASE() 
                                              AND TABLE_NAME = 'Transactions'";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkTableQuery, conn))
                    {
                        int tableExists = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (tableExists == 0)
                        {
                            return; // Table doesn't exist
                        }
                    }

                    string query = @"SELECT 
                                    t.TransactionID,
                                    b.Title as BookTitle,
                                    t.BorrowDate,
                                    t.DueDate,
                                    t.Status
                                FROM Transactions t
                                INNER JOIN Books b ON t.BookID = b.BookID
                                WHERE t.MemberID = @MemberID
                                AND t.Status = 'Borrowed'
                                ORDER BY t.DueDate ASC";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", memberID);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DateTime borrowDate = reader["BorrowDate"] != DBNull.Value ? Convert.ToDateTime(reader["BorrowDate"]) : DateTime.MinValue;
                                DateTime dueDate = reader["DueDate"] != DBNull.Value ? Convert.ToDateTime(reader["DueDate"]) : DateTime.MinValue;

                                dgv.Rows.Add(
                                    reader["TransactionID"],
                                    reader["BookTitle"] != DBNull.Value ? reader["BookTitle"] : "",
                                    borrowDate != DateTime.MinValue ? borrowDate.ToString("yyyy-MM-dd") : "N/A",
                                    dueDate != DateTime.MinValue ? dueDate.ToString("yyyy-MM-dd") : "N/A",
                                    reader["Status"] != DBNull.Value ? reader["Status"] : ""
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading borrowed books: {ex.Message}");
            }
        }

        private void LoadReservations(DataGridView dgv, int memberID)
        {
            try
            {
                dgv.Columns.Clear();

                dgv.Columns.Add("ReservationID", "ID");
                dgv.Columns["ReservationID"].Visible = false;
                dgv.Columns.Add("BookTitle", "Book Title");
                dgv.Columns["BookTitle"].Width = 300;
                dgv.Columns.Add("ReservationDate", "Reservation Date");
                dgv.Columns["ReservationDate"].Width = 150;
                dgv.Columns.Add("PickupDate", "Pickup Date");
                dgv.Columns["PickupDate"].Width = 150;
                dgv.Columns.Add("Status", "Status");
                dgv.Columns["Status"].Width = 120;

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
                            return; // Table doesn't exist
                        }
                    }

                    string query = @"SELECT 
                                    r.ReservationID,
                                    b.Title as BookTitle,
                                    r.ReservationDate,
                                    r.PickupDate,
                                    r.Status
                                FROM Reservations r
                                INNER JOIN Books b ON r.BookID = b.BookID
                                WHERE r.MemberID = @MemberID
                                AND (r.Status = 'Pending' OR r.Status = 'Active')
                                ORDER BY r.ReservationDate DESC";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", memberID);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DateTime resDate = reader["ReservationDate"] != DBNull.Value ? Convert.ToDateTime(reader["ReservationDate"]) : DateTime.MinValue;
                                DateTime pickupDate = reader["PickupDate"] != DBNull.Value ? Convert.ToDateTime(reader["PickupDate"]) : DateTime.MinValue;

                                dgv.Rows.Add(
                                    reader["ReservationID"],
                                    reader["BookTitle"] != DBNull.Value ? reader["BookTitle"] : "",
                                    resDate != DateTime.MinValue ? resDate.ToString("yyyy-MM-dd") : "N/A",
                                    pickupDate != DateTime.MinValue ? pickupDate.ToString("yyyy-MM-dd") : "N/A",
                                    reader["Status"] != DBNull.Value ? reader["Status"] : ""
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
    }
}
