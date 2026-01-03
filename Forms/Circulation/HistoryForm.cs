using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Project5LMS.Admin_Dashboard
{
    public partial class HistoryForm : Form
    {
        private string connectionString;

        public HistoryForm()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["MySqlConnectionString"]?.ConnectionString 
                ?? "Server=localhost;Database=librarydb;Uid=root;Pwd=;";
        }

        private void HistoryForm_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadTransactionHistory();
        }

        private void SetupDataGridView()
        {
            dta_History.AutoGenerateColumns = false;
            dta_History.Columns.Clear();
            dta_History.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 11F);
            dta_History.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Bold);
            dta_History.RowTemplate.Height = 50;
            dta_History.DefaultCellStyle.Padding = new Padding(15, 5, 15, 5);
            dta_History.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            dta_History.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dta_History.EnableHeadersVisualStyles = false;
            dta_History.AllowUserToAddRows = false;
            dta_History.AllowUserToDeleteRows = false;
            dta_History.ReadOnly = true;
            dta_History.RowHeadersVisible = false;
            dta_History.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dta_History.BackgroundColor = Color.White;
            dta_History.BorderStyle = BorderStyle.None;
            dta_History.GridColor = Color.FromArgb(240, 240, 240);

            // Add columns
            dta_History.Columns.Add("TransactionID", "TransactionID");
            dta_History.Columns["TransactionID"].Visible = false;

            dta_History.Columns.Add("Type", "Type");
            dta_History.Columns["Type"].Width = 120;

            dta_History.Columns.Add("Book", "Book");
            dta_History.Columns["Book"].Width = 300;

            dta_History.Columns.Add("Member", "Member");
            dta_History.Columns["Member"].Width = 250;

            dta_History.Columns.Add("Checkout", "Checkout");
            dta_History.Columns["Checkout"].Width = 150;

            dta_History.Columns.Add("DueReturn", "Due/Return");
            dta_History.Columns["DueReturn"].Width = 150;

            dta_History.Columns.Add("Status", "Status");
            dta_History.Columns["Status"].Width = 120;
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

        private void LoadTransactionHistory()
        {
            try
            {
                EnsureTransactionsTableExists();
                
                dta_History.Rows.Clear();

                string keyword = txtSearch.Text.Trim();
                if (keyword == "Search transactions...") keyword = "";

                string query = @"SELECT 
                                    t.TransactionID,
                                    t.Status,
                                    t.BorrowDate,
                                    t.DueDate,
                                    t.ReturnDate,
                                    b.Title as BookTitle,
                                    b.ISBN,
                                    CONCAT(m.FirstName, ' ', m.LastName) as MemberName,
                                    m.MemberID
                                FROM Transactions t
                                INNER JOIN Books b ON t.BookID = b.BookID
                                INNER JOIN Members m ON t.MemberID = m.MemberID
                                WHERE (@Keyword = '' 
                                       OR b.Title LIKE @Keyword 
                                       OR b.ISBN LIKE @Keyword
                                       OR m.FirstName LIKE @Keyword
                                       OR m.LastName LIKE @Keyword
                                       OR CAST(t.TransactionID AS CHAR) LIKE @Keyword)
                                ORDER BY t.BorrowDate DESC, t.TransactionID DESC
                                LIMIT 500";

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int transactionId = reader.GetInt32("TransactionID");
                            string status = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : "";
                            DateTime borrowDate = reader["BorrowDate"] != DBNull.Value ? Convert.ToDateTime(reader["BorrowDate"]) : DateTime.MinValue;
                            DateTime dueDate = reader["DueDate"] != DBNull.Value ? Convert.ToDateTime(reader["DueDate"]) : DateTime.MinValue;
                            DateTime returnDate = reader["ReturnDate"] != DBNull.Value ? Convert.ToDateTime(reader["ReturnDate"]) : DateTime.MinValue;
                            string bookTitle = reader["BookTitle"].ToString();
                            string memberName = reader["MemberName"].ToString();

                            // Determine transaction type
                            string transactionType = "Checkout";
                            if (status == "Returned")
                            {
                                transactionType = "Return";
                            }
                            else if (status == "Renewed" || status.Contains("Renew"))
                            {
                                transactionType = "Renewal";
                            }

                            // Format dates
                            string checkoutDate = borrowDate != DateTime.MinValue ? borrowDate.ToString("yyyy-MM-dd") : "N/A";
                            string dueReturnDate = "";
                            if (status == "Returned" && returnDate != DateTime.MinValue)
                            {
                                dueReturnDate = returnDate.ToString("yyyy-MM-dd");
                            }
                            else if (dueDate != DateTime.MinValue)
                            {
                                dueReturnDate = dueDate.ToString("yyyy-MM-dd");
                            }
                            else
                            {
                                dueReturnDate = "N/A";
                            }

                            int rowIndex = dta_History.Rows.Add(
                                transactionId,
                                transactionType,
                                bookTitle,
                                memberName,
                                checkoutDate,
                                dueReturnDate,
                                status
                            );

                            DataGridViewRow row = dta_History.Rows[rowIndex];

                            // Style Type column (light gray button-like)
                            row.Cells["Type"].Style.BackColor = Color.FromArgb(240, 240, 240);
                            row.Cells["Type"].Style.ForeColor = Color.FromArgb(100, 100, 100);
                            row.Cells["Type"].Style.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular);
                            row.Cells["Type"].Style.Padding = new Padding(10, 5, 10, 5);
                            row.Cells["Type"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                            // Style Status column (green pill for Active, gray for others)
                            if (status == "Active" || status == "Borrowed")
                            {
                                row.Cells["Status"].Style.ForeColor = Color.FromArgb(76, 175, 80);
                                row.Cells["Status"].Style.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
                            }
                            else if (status == "Returned")
                            {
                                row.Cells["Status"].Style.ForeColor = Color.Gray;
                                row.Cells["Status"].Style.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular);
                            }
                            else if (status == "Overdue")
                            {
                                row.Cells["Status"].Style.ForeColor = Color.FromArgb(200, 0, 0);
                                row.Cells["Status"].Style.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
                            }
                            else
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
                MessageBox.Show($"Error loading transaction history: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                LoadTransactionHistory();
            }
        }
    }
}
