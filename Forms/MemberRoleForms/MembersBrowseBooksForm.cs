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
    public partial class MembersBrowseBooksForm : Form
    {
        private string connectionString;

        public MembersBrowseBooksForm()
        {
            InitializeComponent();
            connectionString = DatabaseHelper.GetConnectionString();
            
            // Wire up event handlers
            txtSearch.TextChanged += txtSearch_TextChanged;
            cmbStatus.SelectedIndexChanged += cmbStatus_SelectedIndexChanged;
        }

        private void MembersBrowseBooksForm_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadStatusFilter();
            LoadBooks();
        }

        private void SetupDataGridView()
        {
            dta_GridMembersBrowseBooks.AutoGenerateColumns = false;
            dta_GridMembersBrowseBooks.Columns.Clear();
            dta_GridMembersBrowseBooks.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10F);
            dta_GridMembersBrowseBooks.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            dta_GridMembersBrowseBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dta_GridMembersBrowseBooks.MultiSelect = false;
            dta_GridMembersBrowseBooks.CellContentClick += Dta_GridMembersBrowseBooks_CellContentClick;
        }

        private void LoadStatusFilter()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("All Status");
            cmbStatus.Items.Add("Available");
            cmbStatus.Items.Add("Unavailable");
            cmbStatus.SelectedIndex = 0;
        }

        private void LoadBooks()
        {
            try
            {
                dta_GridMembersBrowseBooks.Rows.Clear();
                dta_GridMembersBrowseBooks.Columns.Clear();

                // Add columns
                dta_GridMembersBrowseBooks.Columns.Add("BookID", "BookID");
                dta_GridMembersBrowseBooks.Columns["BookID"].Visible = false;
                dta_GridMembersBrowseBooks.Columns.Add("Accession", "Accession#");
                dta_GridMembersBrowseBooks.Columns["Accession"].Width = 100;
                dta_GridMembersBrowseBooks.Columns.Add("Title", "Title");
                dta_GridMembersBrowseBooks.Columns["Title"].Width = 250;
                dta_GridMembersBrowseBooks.Columns.Add("Author", "Author");
                dta_GridMembersBrowseBooks.Columns["Author"].Width = 180;
                dta_GridMembersBrowseBooks.Columns.Add("ISBN", "ISBN");
                dta_GridMembersBrowseBooks.Columns["ISBN"].Width = 140;
                dta_GridMembersBrowseBooks.Columns.Add("Category", "Category");
                dta_GridMembersBrowseBooks.Columns["Category"].Width = 120;
                dta_GridMembersBrowseBooks.Columns.Add("Available", "Available");
                dta_GridMembersBrowseBooks.Columns["Available"].Width = 80;

                // Add Reserve button column
                DataGridViewButtonColumn reserveColumn = new DataGridViewButtonColumn();
                reserveColumn.Name = "Reserve";
                reserveColumn.Text = "Reserve";
                reserveColumn.UseColumnTextForButtonValue = true;
                reserveColumn.Width = 90;
                dta_GridMembersBrowseBooks.Columns.Add(reserveColumn);

                // Add Borrow button column
                DataGridViewButtonColumn borrowColumn = new DataGridViewButtonColumn();
                borrowColumn.Name = "Borrow";
                borrowColumn.Text = "Borrow";
                borrowColumn.UseColumnTextForButtonValue = true;
                borrowColumn.Width = 90;
                dta_GridMembersBrowseBooks.Columns.Add(borrowColumn);

                string keyword = txtSearch.Text.Trim();
                string status = cmbStatus.Text == "All Status" ? "" : cmbStatus.Text;

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"SELECT 
                                    b.BookID,
                                    b.Title,
                                    b.Author,
                                    b.ISBN,
                                    b.Category,
                                    b.Available
                                FROM Books b
                                WHERE (@Keyword = '' 
                                       OR b.Title LIKE @Keyword 
                                       OR b.Author LIKE @Keyword
                                       OR b.ISBN LIKE @Keyword
                                       OR CAST(b.BookID AS CHAR) LIKE @Keyword)
                                AND (@Status = '' OR 
                                     (@Status = 'Available' AND b.Available > 0) OR
                                     (@Status = 'Unavailable' AND b.Available = 0))
                                ORDER BY b.Title
                                LIMIT 500";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                        cmd.Parameters.AddWithValue("@Status", status);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dta_GridMembersBrowseBooks.Rows.Add(
                                    reader["BookID"],
                                    reader["BookID"],  // Accession = BookID
                                    reader["Title"] != DBNull.Value ? reader["Title"] : "",
                                    reader["Author"] != DBNull.Value ? reader["Author"] : "",
                                    reader["ISBN"] != DBNull.Value ? reader["ISBN"] : "",
                                    reader["Category"] != DBNull.Value ? reader["Category"] : "",
                                    reader["Available"] != DBNull.Value ? reader["Available"] : 0,
                                    "Reserve",  // Reserve button
                                    "Borrow"    // Borrow button
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading books: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Dta_GridMembersBrowseBooks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if it's a button column click
            if (e.RowIndex < 0) return;

            DataGridView grid = (DataGridView)sender;
            string columnName = grid.Columns[e.ColumnIndex].Name;

            if (columnName == "Reserve")
            {
                ReserveBook(e.RowIndex);
            }
            else if (columnName == "Borrow")
            {
                BorrowBook(e.RowIndex);
            }
        }

        private void ReserveBook(int rowIndex)
        {
            try
            {
                int bookID = Convert.ToInt32(dta_GridMembersBrowseBooks.Rows[rowIndex].Cells["BookID"].Value);
                int memberID = CurrentUser.GetMemberID();

                if (memberID == 0)
                {
                    MessageBox.Show("Unable to identify your member account. Please contact an administrator.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Check if Reservations table exists, create if not
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

                    // Check if already reserved
                    string checkReservationQuery = @"SELECT COUNT(*) FROM Reservations 
                                                    WHERE MemberID = @MemberID 
                                                    AND BookID = @BookID 
                                                    AND (Status = 'Pending' OR Status = 'Active')";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkReservationQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MemberID", memberID);
                        checkCmd.Parameters.AddWithValue("@BookID", bookID);
                        int existingReservations = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (existingReservations > 0)
                        {
                            MessageBox.Show("You have already reserved this book.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    // Create reservation
                    string insertQuery = @"INSERT INTO Reservations (MemberID, BookID, ReservationDate, PickupDate, Status)
                                          VALUES (@MemberID, @BookID, @ReservationDate, @PickupDate, 'Pending')";
                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", memberID);
                        cmd.Parameters.AddWithValue("@BookID", bookID);
                        cmd.Parameters.AddWithValue("@ReservationDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@PickupDate", DateTime.Now.AddDays(7));
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Book reserved successfully! You can pick it up within 7 days.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reserving book: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BorrowBook(int rowIndex)
        {
            try
            {
                int bookID = Convert.ToInt32(dta_GridMembersBrowseBooks.Rows[rowIndex].Cells["BookID"].Value);
                int available = Convert.ToInt32(dta_GridMembersBrowseBooks.Rows[rowIndex].Cells["Available"].Value);
                int memberID = CurrentUser.GetMemberID();

                if (memberID == 0)
                {
                    MessageBox.Show("Unable to identify your member account. Please contact an administrator.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (available <= 0)
                {
                    MessageBox.Show("This book is not available for borrowing. You can reserve it instead.", "Not Available", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Note: In a real system, borrowing should go through library staff approval.
                // For now, we'll show a message that they need to visit the library.
                MessageBox.Show("To borrow a book, please visit the library circulation desk with your member ID.\n\n" +
                               "Book ID: " + bookID + "\n" +
                               "Your Member ID: " + memberID,
                               "Borrow Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing borrow request: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Add event handlers for search and filter
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadBooks();
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBooks();
        }
    }
}
