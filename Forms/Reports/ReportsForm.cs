using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Project5LMS.Forms.Reports
{
    public partial class ReportsForm : Form
    {
        private string connectionString;
        private string selectedReportType = "Circulation";

        public ReportsForm()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["MySqlConnectionString"]?.ConnectionString 
                ?? "Server=localhost;Database=librarydb;Uid=root;Pwd=;";
            dtpFromDate.Value = DateTime.Now.AddMonths(-1);
            dtpToDate.Value = DateTime.Now;
        }

        private void ReportsForm_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            LoadReportTypes();
            LoadFilters();
        }

        private void LoadReportTypes()
        {
            radioButton1.CheckedChanged += (s, e) => { if (radioButton1.Checked) selectedReportType = "Circulation"; };
            radioButton2.CheckedChanged += (s, e) => { if (radioButton2.Checked) selectedReportType = "Member"; };
            radioButton3.CheckedChanged += (s, e) => { if (radioButton3.Checked) selectedReportType = "Book"; };
            radioButton4.CheckedChanged += (s, e) => { if (radioButton4.Checked) selectedReportType = "Fine"; };
            radioButton5.CheckedChanged += (s, e) => { if (radioButton5.Checked) selectedReportType = "Inventory"; };
        }

        private void LoadFilters()
        {
            cmbCategoryFilter.Items.Clear();
            cmbCategoryFilter.Items.Add("All Categories");
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT DISTINCT Category FROM Books WHERE Category IS NOT NULL AND Category != '' ORDER BY Category";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbCategoryFilter.Items.Add(reader["Category"].ToString());
                        }
                    }
                }
            }
            catch { }
            cmbCategoryFilter.SelectedIndex = 0;

            cmbMemberTypeFilter.Items.Clear();
            cmbMemberTypeFilter.Items.Add("All Types");
            cmbMemberTypeFilter.Items.Add("Student");
            cmbMemberTypeFilter.Items.Add("Faculty");
            cmbMemberTypeFilter.Items.Add("Staff");
            cmbMemberTypeFilter.SelectedIndex = 0;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            GenerateReport();
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

        private void GenerateReport()
        {
            try
            {
                // Ensure Transactions table exists for reports that need it
                if (selectedReportType == "Circulation" || selectedReportType == "Member" || selectedReportType == "Book")
                {
                    EnsureTransactionsTableExists();
                }
                
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();

                string query = "";
                string category = cmbCategoryFilter.Text == "All Categories" ? "" : cmbCategoryFilter.Text;
                string memberType = cmbMemberTypeFilter.Text == "All Types" ? "" : cmbMemberTypeFilter.Text;
                DateTime fromDate = dtpFromDate.Value.Date;
                DateTime toDate = dtpToDate.Value.Date.AddDays(1);

                switch (selectedReportType)
                {
                    case "Circulation":
                        query = GenerateCirculationReport(category, fromDate, toDate);
                        break;
                    case "Member":
                        query = GenerateMemberReport(memberType, fromDate, toDate);
                        break;
                    case "Book":
                        query = GenerateBookReport(category, fromDate, toDate);
                        break;
                    case "Fine":
                        query = GenerateFineReport(fromDate, toDate);
                        break;
                    case "Inventory":
                        query = GenerateInventoryReport(category);
                        break;
                }

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (query.Contains("@Category"))
                            cmd.Parameters.AddWithValue("@Category", category);
                        if (query.Contains("@MemberType"))
                            cmd.Parameters.AddWithValue("@MemberType", memberType);
                        if (query.Contains("@FromDate"))
                            cmd.Parameters.AddWithValue("@FromDate", fromDate);
                        if (query.Contains("@ToDate"))
                            cmd.Parameters.AddWithValue("@ToDate", toDate);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dataGridView1.DataSource = dt;
                            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GenerateCirculationReport(string category, DateTime fromDate, DateTime toDate)
        {
            return @"SELECT 
                        t.TransactionID as 'Transaction ID',
                        b.Title as 'Book Title',
                        b.Author,
                        CONCAT(m.FirstName, ' ', m.LastName) as 'Member Name',
                        t.BorrowDate as 'Borrow Date',
                        t.DueDate as 'Due Date',
                        t.ReturnDate as 'Return Date',
                        t.Status
                    FROM Transactions t
                    INNER JOIN Books b ON t.BookID = b.BookID
                    INNER JOIN Members m ON t.MemberID = m.MemberID
                    WHERE t.BorrowDate >= @FromDate AND t.BorrowDate < @ToDate
                    " + (category != "" ? "AND b.Category = @Category" : "") + @"
                    ORDER BY t.BorrowDate DESC";
        }

        private string GenerateMemberReport(string memberType, DateTime fromDate, DateTime toDate)
        {
            return @"SELECT 
                        m.MemberID,
                        CONCAT(m.FirstName, ' ', m.LastName) as 'Member Name',
                        m.MemberType as 'Type',
                        m.Email,
                        m.RegistrationDate as 'Registration Date',
                        m.Status,
                        COUNT(DISTINCT t.TransactionID) as 'Total Borrowings'
                    FROM Members m
                    LEFT JOIN Transactions t ON m.MemberID = t.MemberID 
                        AND t.BorrowDate >= @FromDate AND t.BorrowDate < @ToDate
                    WHERE m.RegistrationDate >= @FromDate AND m.RegistrationDate < @ToDate
                    " + (memberType != "" ? "AND m.MemberType = @MemberType" : "") + @"
                    GROUP BY m.MemberID, m.FirstName, m.LastName, m.MemberType, m.Email, m.RegistrationDate, m.Status
                    ORDER BY m.LastName, m.FirstName";
        }

        private string GenerateBookReport(string category, DateTime fromDate, DateTime toDate)
        {
            return @"SELECT 
                        b.BookID,
                        b.Title,
                        b.Author,
                        b.ISBN,
                        b.Category,
                        b.Copies as 'Total Copies',
                        b.Available,
                        COUNT(DISTINCT t.TransactionID) as 'Times Borrowed'
                    FROM Books b
                    LEFT JOIN Transactions t ON b.BookID = t.BookID 
                        AND t.BorrowDate >= @FromDate AND t.BorrowDate < @ToDate
                    WHERE 1=1
                    " + (category != "" ? "AND b.Category = @Category" : "") + @"
                    GROUP BY b.BookID, b.Title, b.Author, b.ISBN, b.Category, b.Copies, b.Available
                    ORDER BY b.Title";
        }

        private string GenerateFineReport(DateTime fromDate, DateTime toDate)
        {
            return @"SELECT 
                        f.FineID,
                        CONCAT(m.FirstName, ' ', m.LastName) as 'Member Name',
                        b.Title as 'Book Title',
                        f.FineAmount as 'Amount',
                        f.Status as 'Status',
                        cr.CheckoutDate as 'Borrow Date',
                        cr.DueDate as 'Due Date'
                    FROM Fines f
                    INNER JOIN Members m ON f.MemberID = m.MemberID
                    LEFT JOIN CirculationRecords cr ON f.RecordID = cr.RecordID
                    LEFT JOIN Books b ON cr.BookID = b.BookID
                    WHERE cr.BorrowDate >= @FromDate AND cr.BorrowDate < @ToDate
                    ORDER BY cr.BorrowDate DESC";
        }

        private string GenerateInventoryReport(string category)
        {
            return @"SELECT 
                        b.BookID,
                        b.Title,
                        b.Author,
                        b.ISBN,
                        b.Category,
                        b.Copies as 'Total Copies',
                        b.Available,
                        (b.Copies - b.Available) as 'Borrowed'
                    FROM Books b
                    WHERE 1=1
                    " + (category != "" ? "AND b.Category = @Category" : "") + @"
                    ORDER BY b.Title";
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Excel export functionality would be implemented here.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            MessageBox.Show("PDF export functionality would be implemented here.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Print functionality would be implemented here.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
