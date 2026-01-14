using Project5LMS.Controllers;
using Project5LMS.Helpers;
using Project5LMS.Services;
using Project5LMS.Interfaces;
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
    public partial class AddMemberForm : Form
    {
        private MembersController membersController;
        private int memberId = 0;
        private string connectionString;
        public AddMemberForm(int memberId = 0)
        {
            InitializeComponent();
            membersController = new MembersController();
            this.memberId = memberId;
            connectionString = ConfigurationManager.ConnectionStrings["MySqlConnectionString"]?.ConnectionString
                ?? "Server=localhost;Database=librarydb;Uid=root;Pwd=;";
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
                    textBox.BackColor = Color.White;
                    textBox.BorderStyle = BorderStyle.FixedSingle;
                }
                else if (control is ComboBox comboBox)
                {
                    comboBox.BackColor = Color.White;
                    comboBox.FlatStyle = FlatStyle.Flat;
                }
                else if (control is DateTimePicker dateTimePicker)
                {
                    dateTimePicker.BackColor = Color.White;
                    dateTimePicker.CalendarForeColor = Color.Black;
                }
            }
        }
        private void AddMemberForm_Load(object sender, EventArgs e)
        {
            cmbMemberType.Items.Clear();
            cmbMemberType.Items.AddRange(new string[] { "Staff", "Student", "Faculty", "Guest" });
            cmbMemberType.SelectedIndex = 0;
            cmbStatus.Items.Clear();
            // Staff role restriction: Only Admin can suspend members
            if (CurrentUser.Role != null && CurrentUser.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                cmbStatus.Items.AddRange(new string[] { "Active", "Inactive", "Suspended" });
            }
            else
            {
                // Staff can only set Active or Inactive status
                cmbStatus.Items.AddRange(new string[] { "Active", "Inactive" });
            }
            cmbStatus.SelectedIndex = 0;
            if (memberId > 0)
            {
                lblTitle.Text = "Edit Member";
                LoadMemberData();
            }
            else
            {
                // For new members, make Member ID read-only and show it will be auto-generated
                if (txtMemberCardNumber != null)
                {
                    txtMemberCardNumber.ReadOnly = true;
                    txtMemberCardNumber.Text = "Auto-generated (MEM-XXXXXX)";
                    txtMemberCardNumber.BackColor = Color.FromArgb(240, 240, 240);
                    txtMemberCardNumber.ForeColor = Color.FromArgb(128, 128, 128);
                }
                // Set default registration date to today
                if (dtpRegistration != null)
                {
                    dtpRegistration.Value = DateTime.Now;
                }
                // Set default expiration date to 1 year from today
                if (dtpExpiration != null)
                {
                    dtpExpiration.Value = DateTime.Now.AddYears(1);
                }
            }
            
            // Always make Member ID read-only to prevent manual editing (for both new and existing members)
            if (txtMemberCardNumber != null)
            {
                txtMemberCardNumber.ReadOnly = true;
                txtMemberCardNumber.BackColor = Color.FromArgb(240, 240, 240);
            }
            StyleControls();
        }
        private void LoadMemberData()
        {
            try
            {
                DataTable dt = membersController.GetMembers();
                DataRow row = dt.AsEnumerable().FirstOrDefault(r => Convert.ToInt32(r["MemberID"]) == memberId);
                if (row != null)
                {
                    txtFirstName.Text = row["FirstName"] != DBNull.Value ? row["FirstName"].ToString() : "";
                    txtLastName.Text = row["LastName"] != DBNull.Value ? row["LastName"].ToString() : "";
                    txtEmail.Text = row["Email"].ToString();
                    try
                    {
                        if (row.Table.Columns.Contains("Contact"))
                            txtContact.Text = row["Contact"].ToString();
                    }
                    catch { }
                    try
                    {
                        if (row.Table.Columns.Contains("Address"))
                            txtAddress.Text = row["Address"].ToString();
                    }
                    catch { }
                    if (row.Table.Columns.Contains("Type") && row["Type"] != DBNull.Value)
                    {
                        cmbMemberType.Text = row["Type"].ToString();
                    }
                    else if (row.Table.Columns.Contains("MemberType") && row["MemberType"] != DBNull.Value)
                    {
                        cmbMemberType.Text = row["MemberType"].ToString();
                    }
                    // Staff role restriction: Disable member type editing for Staff
                    if (CurrentUser.Role != null && !CurrentUser.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                    {
                        cmbMemberType.Enabled = false;
                        lblMemberType.Text = "Member Type (Cannot be changed by Staff):";
                    }
                    dtpRegistration.Value = Convert.ToDateTime(row["RegistrationDate"]);
                    dtpExpiration.Value = Convert.ToDateTime(row["ExpirationDate"]);
                    string currentStatus = row["Status"].ToString();
                    // If Staff is editing and status is Suspended, don't allow changing it
                    if (CurrentUser.Role != null && !CurrentUser.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                    {
                        if (currentStatus.Equals("Suspended", StringComparison.OrdinalIgnoreCase))
                        {
                            // Staff cannot change suspended status - keep it but disable the dropdown
                            cmbStatus.Text = currentStatus;
                            cmbStatus.Enabled = false;
                            // Show message that only Admin can change suspended status
                            MessageBox.Show("This member is suspended. Only administrators can change the status.", 
                                "Status Restricted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            cmbStatus.Text = currentStatus;
                        }
                    }
                    else
                    {
                        cmbStatus.Text = currentStatus;
                    }
                    // Display Member ID using consistent system format (MEM-000001)
                    // Always use the MemberID from the database to ensure consistent formatting
                    int dbMemberId = Convert.ToInt32(row["MemberID"]);
                    string formattedMemberId = IDFormatter.FormatMemberID(dbMemberId);
                    txtMemberCardNumber.Text = formattedMemberId;
                    txtMemberCardNumber.ReadOnly = true;
                    txtMemberCardNumber.BackColor = Color.FromArgb(240, 240, 240);
                    txtMemberCardNumber.ForeColor = Color.Black;
                    
                    // Also update the database MemberCardNumber column if it exists and doesn't match the format
                    // This ensures consistency across the system
                    try
                    {
                        using (MySqlConnection conn = new MySqlConnection(connectionString))
                        {
                            conn.Open();
                            bool hasMemberCardNumber = CheckColumnExists(conn, "Members", "MemberCardNumber");
                            if (hasMemberCardNumber)
                            {
                                string updateQuery = "UPDATE Members SET MemberCardNumber = @MemberCardNumber WHERE MemberID = @MemberID";
                                using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn))
                                {
                                    updateCmd.Parameters.AddWithValue("@MemberCardNumber", formattedMemberId);
                                    updateCmd.Parameters.AddWithValue("@MemberID", dbMemberId);
                                    updateCmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                    catch
                    {
                        // Silently fail - the display is already correct
                    }
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
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;
            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string contact = txtContact.Text.Trim();
            string address = txtAddress.Text.Trim();
            string type = cmbMemberType.Text;
            DateTime regDate = dtpRegistration.Value;
            DateTime expDate = dtpExpiration.Value;
            string status = cmbStatus.Text;
            // Staff role restriction: Prevent Staff from setting status to Suspended
            // Staff role restriction: Prevent Staff from editing member type/privileges
            string originalType = "";
            if (CurrentUser.Role != null && !CurrentUser.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                if (memberId > 0) // Editing existing member
                {
                    // Get original member type to prevent Staff from changing it
                    try
                    {
                        var membersService = ServiceFactory.CreateMembersService();
                        var originalMember = membersService.GetMember(memberId);
                        if (originalMember != null)
                        {
                            originalType = originalMember.Type;
                            // Prevent Staff from changing member type (privileges)
                            if (!string.IsNullOrEmpty(originalType) && !type.Equals(originalType, StringComparison.OrdinalIgnoreCase))
                            {
                                MessageBox.Show("Only administrators can change member type/privileges. Member type will remain unchanged.", 
                                    "Access Restricted", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                type = originalType;
                                cmbMemberType.Text = originalType;
                            }
                        }
                    }
                    catch
                    {
                        // If we can't get original type, allow the change (fallback)
                    }
                }
                
                if (status.Equals("Suspended", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Only administrators can suspend members. Status will be set to 'Active'.", 
                        "Access Restricted", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    status = "Active";
                    cmbStatus.Text = "Active";
                }
            }
            // For new members, don't use the "Auto-generated" text as the memberCardNumber
            string memberCardNumber = null;
            if (memberId > 0 && txtMemberCardNumber != null)
            {
                string cardNumberText = txtMemberCardNumber.Text?.Trim();
                // Only use it if it's not the placeholder text
                if (!string.IsNullOrWhiteSpace(cardNumberText) && 
                    !cardNumberText.Equals("Auto-generated", StringComparison.OrdinalIgnoreCase) &&
                    !cardNumberText.StartsWith("Auto-generated", StringComparison.OrdinalIgnoreCase))
                {
                    memberCardNumber = cardNumberText;
                }
            }
            bool success = false;
            try
            {
                if (memberId == 0)
                {
                    success = AddMemberWithOptionalFields(firstName, lastName, email, contact, address, type, regDate, expDate, status, memberCardNumber);
                }
                else
                {
                    success = UpdateMemberWithOptionalFields(memberId, firstName, lastName, email, contact, address, type, regDate, expDate, status, memberCardNumber);
                }
                if (success)
                {
                    // Member ID is now auto-generated and displayed in txtMemberCardNumber
                    string successMessage = "Member saved successfully!";
                    if (memberId == 0 && txtMemberCardNumber != null && !string.IsNullOrEmpty(txtMemberCardNumber.Text) && txtMemberCardNumber.Text != "Auto-generated")
                    {
                        successMessage += $"\n\nMember ID: {txtMemberCardNumber.Text}";
                    }
                    MessageBox.Show(successMessage, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to save member.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving member: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Please enter First Name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFirstName.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Please enter Last Name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLastName.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Please enter Email.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }
            if (!txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }
            return true;
        }
        private bool AddMemberWithOptionalFields(string firstName, string lastName, string email, string contact, string address,
            string type, DateTime regDate, DateTime expDate, string status, string memberCardNumber = null)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    bool hasContact = CheckColumnExists(conn, "Members", "Contact");
                    bool hasAddress = CheckColumnExists(conn, "Members", "Address");
                    bool hasMemberCardNumber = CheckColumnExists(conn, "Members", "MemberCardNumber");
                    string query;
                    bool hasType = CheckColumnExists(conn, "Members", "Type");
                    string typeColumn = hasType ? "Type" : "MemberType";
                    string columns = $"FirstName, LastName, Email, {typeColumn}, RegistrationDate, ExpirationDate, Status";
                    string values = "@FirstName, @LastName, @Email, @Type, @RegDate, @ExpDate, @Status";
                    if (hasContact) { columns += ", Contact"; values += ", @Contact"; }
                    if (hasAddress) { columns += ", Address"; values += ", @Address"; }
                    if (hasMemberCardNumber && !string.IsNullOrWhiteSpace(memberCardNumber))
                    {
                        columns += ", MemberCardNumber";
                        values += ", @MemberCardNumber";
                    }
                    query = $"INSERT INTO Members ({columns}) VALUES ({values})";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", firstName);
                        cmd.Parameters.AddWithValue("@LastName", lastName);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Type", type);
                        cmd.Parameters.AddWithValue("@RegDate", regDate);
                        cmd.Parameters.AddWithValue("@ExpDate", expDate);
                        cmd.Parameters.AddWithValue("@Status", status);
                        if (hasContact)
                            cmd.Parameters.AddWithValue("@Contact", contact);
                        if (hasAddress)
                            cmd.Parameters.AddWithValue("@Address", address);
                        if (hasMemberCardNumber && !string.IsNullOrWhiteSpace(memberCardNumber))
                            cmd.Parameters.AddWithValue("@MemberCardNumber", memberCardNumber);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            // Get the auto-generated MemberID and format it
                            long lastInsertedId = cmd.LastInsertedId;
                            int newMemberId = (int)lastInsertedId;
                            string formattedMemberId = IDFormatter.FormatMemberID(newMemberId);
                            
                            // Always update MemberCardNumber with formatted Member ID using system-wide format
                            if (hasMemberCardNumber)
                            {
                                string updateQuery = "UPDATE Members SET MemberCardNumber = @MemberCardNumber WHERE MemberID = @MemberID";
                                using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn))
                                {
                                    updateCmd.Parameters.AddWithValue("@MemberCardNumber", formattedMemberId);
                                    updateCmd.Parameters.AddWithValue("@MemberID", newMemberId);
                                    updateCmd.ExecuteNonQuery();
                                }
                            }
                            
                            // Update the form field to show the generated Member ID in system format
                            if (txtMemberCardNumber != null)
                            {
                                txtMemberCardNumber.Text = formattedMemberId;
                                txtMemberCardNumber.ForeColor = Color.Black; // Change from gray to black to show it's been generated
                            }
                            
                            return true;
                        }
                        return false;
                    }
                }
            }
            catch
            {
                return membersController.AddMember(firstName, lastName, email, type, regDate, expDate, status);
            }
        }
        private bool UpdateMemberWithOptionalFields(int memberId, string firstName, string lastName, string email, string contact, string address,
            string type, DateTime regDate, DateTime expDate, string status, string memberCardNumber = null)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    bool hasContact = CheckColumnExists(conn, "Members", "Contact");
                    bool hasAddress = CheckColumnExists(conn, "Members", "Address");
                    bool hasMemberCardNumber = CheckColumnExists(conn, "Members", "MemberCardNumber");
                    bool hasType = CheckColumnExists(conn, "Members", "Type");
                    string typeColumn = hasType ? "Type" : "MemberType";
                    string setClause = $"FirstName=@FirstName, LastName=@LastName, Email=@Email, {typeColumn}=@Type, RegistrationDate=@RegDate, ExpirationDate=@ExpDate, Status=@Status";
                    if (hasContact) setClause += ", Contact=@Contact";
                    if (hasAddress) setClause += ", Address=@Address";
                    if (hasMemberCardNumber && !string.IsNullOrWhiteSpace(memberCardNumber))
                        setClause += ", MemberCardNumber=@MemberCardNumber";
                    string query = $"UPDATE Members SET {setClause} WHERE MemberID=@ID";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", memberId);
                        cmd.Parameters.AddWithValue("@FirstName", firstName);
                        cmd.Parameters.AddWithValue("@LastName", lastName);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Type", type);
                        cmd.Parameters.AddWithValue("@RegDate", regDate);
                        cmd.Parameters.AddWithValue("@ExpDate", expDate);
                        cmd.Parameters.AddWithValue("@Status", status);
                        if (hasContact)
                            cmd.Parameters.AddWithValue("@Contact", contact);
                        if (hasAddress)
                            cmd.Parameters.AddWithValue("@Address", address);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch
            {
                return membersController.UpdateMember(memberId, firstName, lastName, email, type, regDate, expDate, status);
            }
        }
        private bool CheckColumnExists(MySqlConnection conn, string tableName, string columnName)
        {
            try
            {
                string query = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS
                                WHERE TABLE_SCHEMA = DATABASE()
                                AND TABLE_NAME = @TableName
                                AND COLUMN_NAME = @ColumnName";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TableName", tableName);
                    cmd.Parameters.AddWithValue("@ColumnName", columnName);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
            catch
            {
                return false;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void panelMainContainer_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}