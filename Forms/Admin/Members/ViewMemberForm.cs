using Project5LMS.Controllers;
using Project5LMS.Helpers;
using Project5LMS.Services;
using Project5LMS.Data;
using System;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;
namespace Project5LMS.Forms.Admin.Members
{
    public partial class ViewMemberForm : Form
    {
        private MembersController membersController;
        private int memberId = 0;
        private string connectionString;
        
        public ViewMemberForm(int memberId)
        {
            InitializeComponent();
            membersController = new MembersController();
            this.memberId = memberId;
            connectionString = ConfigurationManager.ConnectionStrings["MySqlConnectionString"]?.ConnectionString
                ?? "Server=localhost;Database=librarydb;Uid=root;Pwd=;";
        }
        
        private void ViewMemberForm_Load(object sender, EventArgs e)
        {
            // Populate member type dropdown
            cmbMemberType.Items.Clear();
            cmbMemberType.Items.AddRange(new string[] { "Staff", "Student", "Faculty", "Guest" });
            
            // Populate status dropdown
            cmbStatus.Items.Clear();
            cmbStatus.Items.AddRange(new string[] { "Active", "Inactive", "Suspended", "Expired" });
            
            StyleControls();
            SetControlsReadOnly();
            PreventDataGridViewEditing();
            LoadMemberData();
        }
        
        private void PreventDataGridViewEditing()
        {
            // Prevent any editing attempts on DataGridViews
            if (dataGridViewBorrowingHistory != null)
            {
                dataGridViewBorrowingHistory.CellBeginEdit += (s, e) => { e.Cancel = true; };
                dataGridViewBorrowingHistory.UserDeletingRow += (s, e) => { e.Cancel = true; };
                dataGridViewBorrowingHistory.DataError += (s, e) => { e.ThrowException = false; };
            }
            
            if (dataGridViewFines != null)
            {
                dataGridViewFines.CellBeginEdit += (s, e) => { e.Cancel = true; };
                dataGridViewFines.UserDeletingRow += (s, e) => { e.Cancel = true; };
                dataGridViewFines.DataError += (s, e) => { e.ThrowException = false; };
            }
        }
        
        private void StyleControls()
        {
            int radius = 10;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius * 2, radius * 2, 180, 90);
            path.AddArc(panelMainContainer.Width - radius * 2, 0, radius * 2, radius * 2, 270, 90);
            path.AddArc(panelMainContainer.Width - radius * 2, panelMainContainer.Height - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(0, panelMainContainer.Height - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseAllFigures();
            panelMainContainer.Region = new Region(path);
            
            foreach (Control control in panelFormContent.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.BackColor = Color.FromArgb(248, 249, 250);
                    textBox.BorderStyle = BorderStyle.FixedSingle;
                    textBox.ReadOnly = true;
                }
                else if (control is ComboBox comboBox)
                {
                    comboBox.BackColor = Color.FromArgb(248, 249, 250);
                    comboBox.FlatStyle = FlatStyle.Flat;
                    comboBox.Enabled = false;
                }
                else if (control is DateTimePicker dateTimePicker)
                {
                    dateTimePicker.BackColor = Color.FromArgb(248, 249, 250);
                    dateTimePicker.CalendarForeColor = Color.Black;
                    dateTimePicker.Enabled = false;
                }
            }
        }
        
        private void SetControlsReadOnly()
        {
            // Make all input controls read-only - VIEW ONLY MODE
            txtFirstName.ReadOnly = true;
            txtFirstName.TabStop = false;
            txtLastName.ReadOnly = true;
            txtLastName.TabStop = false;
            txtEmail.ReadOnly = true;
            txtEmail.TabStop = false;
            txtContact.ReadOnly = true;
            txtContact.TabStop = false;
            txtAddress.ReadOnly = true;
            txtAddress.TabStop = false;
            cmbMemberType.Enabled = false;
            cmbMemberType.TabStop = false;
            dtpRegistration.Enabled = false;
            dtpRegistration.TabStop = false;
            dtpExpiration.Enabled = false;
            dtpExpiration.TabStop = false;
            cmbStatus.Enabled = false;
            cmbStatus.TabStop = false;
        }
        
        private void LoadMemberData()
        {
            try
            {
                DataTable dt = membersController.GetMembers();
                DataRow row = dt.AsEnumerable().FirstOrDefault(r => Convert.ToInt32(r["MemberID"]) == memberId);
                
                if (row != null)
                {
                    // Personal Information
                    txtFirstName.Text = row["FirstName"] != DBNull.Value ? row["FirstName"].ToString() : "";
                    txtLastName.Text = row["LastName"] != DBNull.Value ? row["LastName"].ToString() : "";
                    txtEmail.Text = row["Email"] != DBNull.Value ? row["Email"].ToString() : "";
                    
                    try
                    {
                        if (row.Table.Columns.Contains("Contact"))
                            txtContact.Text = row["Contact"] != DBNull.Value ? row["Contact"].ToString() : "";
                    }
                    catch { }
                    
                    try
                    {
                        if (row.Table.Columns.Contains("Address"))
                            txtAddress.Text = row["Address"] != DBNull.Value ? row["Address"].ToString() : "";
                    }
                    catch { }
                    
                    // Membership Information
                    if (row.Table.Columns.Contains("Type") && row["Type"] != DBNull.Value)
                    {
                        cmbMemberType.Text = row["Type"].ToString();
                    }
                    else if (row.Table.Columns.Contains("MemberType") && row["MemberType"] != DBNull.Value)
                    {
                        cmbMemberType.Text = row["MemberType"].ToString();
                    }
                    
                    if (row["RegistrationDate"] != DBNull.Value)
                    {
                        dtpRegistration.Value = Convert.ToDateTime(row["RegistrationDate"]);
                    }
                    
                    if (row["ExpirationDate"] != DBNull.Value)
                    {
                        dtpExpiration.Value = Convert.ToDateTime(row["ExpirationDate"]);
                    }
                    
                    string status = row["Status"] != DBNull.Value ? row["Status"].ToString() : "Active";
                    
                    // Check if member is expired
                    if (row["ExpirationDate"] != DBNull.Value)
                    {
                        DateTime expirationDate = Convert.ToDateTime(row["ExpirationDate"]);
                        if (expirationDate < DateTime.Now && (status == "Active" || string.IsNullOrEmpty(status)))
                        {
                            status = "Expired";
                        }
                    }
                    
                    cmbStatus.Text = status;
                    
                    // Load additional statistics
                    LoadMemberStatistics();
                    
                    // Load borrowing history and fines
                    LoadBorrowingHistory();
                    LoadFinesAndPenalties();
                }
                else
                {
                    MessageBox.Show("Member not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading member data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"LoadMemberData error: {ex}");
            }
        }
        
        private void LoadMemberStatistics()
        {
            try
            {
                var dbContext = ServiceFactory.GetDbContext();
                using (var conn = dbContext.GetConnection())
                {
                    conn.Open();
                    
                    // Get active borrowing count
                    int activeBorrowings = 0;
                    string queryBorrowings = @"SELECT COUNT(*) FROM Transactions 
                                               WHERE MemberID = @MemberID 
                                               AND (Status = 'Borrowed' OR Status = 'Active')";
                    using (var cmd = new MySqlCommand(queryBorrowings, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", memberId);
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            activeBorrowings = Convert.ToInt32(result);
                        }
                    }
                    
                    // Get total fines
                    decimal totalFines = 0;
                    bool hasFinesTable = DatabaseSchemaHelper.CheckColumnExists(conn, "Fines", "FineID");
                    if (hasFinesTable)
                    {
                        string queryFines = @"SELECT COALESCE(SUM(Amount - Paid), 0) FROM Fines 
                                             WHERE MemberID = @MemberID 
                                             AND Status != 'Paid' AND Status != 'Waived'";
                        using (var cmd = new MySqlCommand(queryFines, conn))
                        {
                            cmd.Parameters.AddWithValue("@MemberID", memberId);
                            object result = cmd.ExecuteScalar();
                            if (result != null)
                            {
                                totalFines = Convert.ToDecimal(result);
                            }
                        }
                    }
                    
                    // Get active reservations
                    int activeReservations = 0;
                    bool hasReservationsTable = DatabaseSchemaHelper.CheckColumnExists(conn, "Reservations", "ReservationID");
                    if (hasReservationsTable)
                    {
                        string queryReservations = @"SELECT COUNT(*) FROM Reservations 
                                                     WHERE MemberID = @MemberID 
                                                     AND (Status = 'Pending' OR Status = 'Active' OR Status = 'Ready')";
                        using (var cmd = new MySqlCommand(queryReservations, conn))
                        {
                            cmd.Parameters.AddWithValue("@MemberID", memberId);
                            object result = cmd.ExecuteScalar();
                            if (result != null)
                            {
                                activeReservations = Convert.ToInt32(result);
                            }
                        }
                    }
                    
                    // Display statistics in labels if they exist
                    try
                    {
                        if (lblActiveBorrowings != null)
                            lblActiveBorrowings.Text = $"Active Borrowings: {activeBorrowings}";
                        if (lblTotalFines != null)
                            lblTotalFines.Text = $"Total Fines: {IDFormatter.FormatCurrency(totalFines)}";
                        if (lblActiveReservations != null)
                            lblActiveReservations.Text = $"Active Reservations: {activeReservations}";
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error updating statistics labels: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading member statistics: {ex.Message}");
            }
        }
        
        private void LoadBorrowingHistory()
        {
            try
            {
                var dbContext = ServiceFactory.GetDbContext();
                using (var conn = dbContext.GetConnection())
                {
                    conn.Open();
                    
                    string query = @"SELECT 
                                        t.TransactionID,
                                        t.BorrowDate,
                                        t.DueDate,
                                        t.ReturnDate,
                                        t.Status,
                                        t.Fine,
                                        b.Title as BookTitle,
                                        b.ISBN,
                                        CASE 
                                            WHEN t.ReturnDate IS NULL AND t.DueDate < NOW() THEN 'Overdue'
                                            WHEN t.ReturnDate IS NOT NULL THEN 'Returned'
                                            ELSE 'Active'
                                        END as LoanStatus
                                    FROM Transactions t
                                    INNER JOIN Books b ON t.BookID = b.BookID
                                    WHERE t.MemberID = @MemberID
                                    ORDER BY t.BorrowDate DESC
                                    LIMIT 50";
                    
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", memberId);
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            
                            if (dataGridViewBorrowingHistory != null)
                            {
                                dataGridViewBorrowingHistory.DataSource = dt;
                                dataGridViewBorrowingHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                                dataGridViewBorrowingHistory.ReadOnly = true;
                                dataGridViewBorrowingHistory.AllowUserToAddRows = false;
                                dataGridViewBorrowingHistory.AllowUserToDeleteRows = false;
                                dataGridViewBorrowingHistory.AllowUserToOrderColumns = false;
                                dataGridViewBorrowingHistory.EditMode = DataGridViewEditMode.EditProgrammatically;
                                dataGridViewBorrowingHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                                dataGridViewBorrowingHistory.MultiSelect = false;
                                
                                // Format columns
                                if (dataGridViewBorrowingHistory.Columns.Contains("BorrowDate"))
                                    dataGridViewBorrowingHistory.Columns["BorrowDate"].DefaultCellStyle.Format = "MM/dd/yyyy";
                                if (dataGridViewBorrowingHistory.Columns.Contains("DueDate"))
                                    dataGridViewBorrowingHistory.Columns["DueDate"].DefaultCellStyle.Format = "MM/dd/yyyy";
                                if (dataGridViewBorrowingHistory.Columns.Contains("ReturnDate"))
                                    dataGridViewBorrowingHistory.Columns["ReturnDate"].DefaultCellStyle.Format = "MM/dd/yyyy";
                                
                                // Format currency columns using CellFormatting event
                                dataGridViewBorrowingHistory.CellFormatting += DataGridViewBorrowingHistory_CellFormatting;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading borrowing history: {ex.Message}");
            }
        }
        
        private void LoadFinesAndPenalties()
        {
            try
            {
                var dbContext = ServiceFactory.GetDbContext();
                using (var conn = dbContext.GetConnection())
                {
                    conn.Open();
                    
                    // Try to get fines from Fines table first
                    bool hasFinesTable = DatabaseSchemaHelper.CheckColumnExists(conn, "Fines", "FineID");
                    DataTable dt = new DataTable();
                    
                    if (hasFinesTable)
                    {
                        string query = @"SELECT 
                                            FineID,
                                            TransactionID,
                                            Amount,
                                            Paid,
                                            (Amount - COALESCE(Paid, 0)) as Outstanding,
                                            Status,
                                            CreatedDate,
                                            DueDate
                                        FROM Fines
                                        WHERE MemberID = @MemberID
                                        ORDER BY CreatedDate DESC
                                        LIMIT 50";
                        
                        using (var cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MemberID", memberId);
                            using (var adapter = new MySqlDataAdapter(cmd))
                            {
                                adapter.Fill(dt);
                            }
                        }
                    }
                    else
                    {
                        // Check if DueDate column exists in Transactions table
                        bool hasDueDate = DatabaseSchemaHelper.CheckColumnExists(conn, "Transactions", "DueDate");
                        
                        // Get fines from Transactions table
                        string dueDateSelect = hasDueDate ? "t.DueDate," : "NULL as DueDate,";
                        string dueDateCondition = hasDueDate ? "t.DueDate < NOW()" : "1=0"; // If no DueDate, never match overdue condition
                        string dueDateInWhere = hasDueDate ? "OR (t.ReturnDate IS NULL AND t.DueDate < NOW())" : "";
                        string dueDateAlias = hasDueDate ? "t.DueDate as DueDate" : "NULL as DueDate";
                        
                        string query = $@"SELECT 
                                            t.TransactionID,
                                            t.BorrowDate,
                                            {dueDateSelect}
                                            t.ReturnDate,
                                            t.Fine as Amount,
                                            CASE 
                                                WHEN t.ReturnDate IS NOT NULL AND t.Fine > 0 THEN 'Unpaid'
                                                WHEN t.ReturnDate IS NULL AND {dueDateCondition} THEN 'Overdue'
                                                ELSE 'Paid'
                                            END as Status,
                                            {dueDateAlias}
                                        FROM Transactions t
                                        WHERE t.MemberID = @MemberID
                                        AND (t.Fine > 0 {dueDateInWhere})
                                        ORDER BY t.BorrowDate DESC
                                        LIMIT 50";
                        
                        using (var cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MemberID", memberId);
                            using (var adapter = new MySqlDataAdapter(cmd))
                            {
                                adapter.Fill(dt);
                            }
                        }
                    }
                    
                    if (dataGridViewFines != null)
                    {
                        dataGridViewFines.DataSource = dt;
                        dataGridViewFines.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dataGridViewFines.ReadOnly = true;
                        dataGridViewFines.AllowUserToAddRows = false;
                        dataGridViewFines.AllowUserToDeleteRows = false;
                        dataGridViewFines.AllowUserToOrderColumns = false;
                        dataGridViewFines.EditMode = DataGridViewEditMode.EditProgrammatically;
                        dataGridViewFines.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        dataGridViewFines.MultiSelect = false;
                        
                        // Format date columns
                        if (dataGridViewFines.Columns.Contains("CreatedDate"))
                            dataGridViewFines.Columns["CreatedDate"].DefaultCellStyle.Format = "MM/dd/yyyy";
                        if (dataGridViewFines.Columns.Contains("DueDate"))
                            dataGridViewFines.Columns["DueDate"].DefaultCellStyle.Format = "MM/dd/yyyy";
                        if (dataGridViewFines.Columns.Contains("BorrowDate"))
                            dataGridViewFines.Columns["BorrowDate"].DefaultCellStyle.Format = "MM/dd/yyyy";
                        if (dataGridViewFines.Columns.Contains("ReturnDate"))
                            dataGridViewFines.Columns["ReturnDate"].DefaultCellStyle.Format = "MM/dd/yyyy";
                        
                        // Format currency columns using CellFormatting event
                        dataGridViewFines.CellFormatting += DataGridViewFines_CellFormatting;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading fines and penalties: {ex.Message}");
            }
        }
        
        private void DataGridViewBorrowingHistory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            
            DataGridView dgv = sender as DataGridView;
            if (dgv == null) return;
            
            string columnName = dgv.Columns[e.ColumnIndex].Name;
            
            // Format currency columns with PHP symbol
            if (columnName == "Fine" && e.Value != null && e.Value != DBNull.Value)
            {
                if (decimal.TryParse(e.Value.ToString(), out decimal fineAmount))
                {
                    e.Value = IDFormatter.FormatCurrency(fineAmount);
                    e.FormattingApplied = true;
                }
            }
        }
        
        private void DataGridViewFines_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            
            DataGridView dgv = sender as DataGridView;
            if (dgv == null) return;
            
            string columnName = dgv.Columns[e.ColumnIndex].Name;
            
            // Format currency columns with PHP symbol
            if ((columnName == "Amount" || columnName == "Outstanding" || columnName == "Paid") && 
                e.Value != null && e.Value != DBNull.Value)
            {
                if (decimal.TryParse(e.Value.ToString(), out decimal amount))
                {
                    e.Value = IDFormatter.FormatCurrency(amount);
                    e.FormattingApplied = true;
                }
            }
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
