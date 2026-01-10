using Project5LMS.Controllers;
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
        private string photoPath = null;
        private string validIDPath = null;

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
            cmbStatus.Items.AddRange(new string[] { "Active", "Inactive", "Suspended" });
            cmbStatus.SelectedIndex = 0;

            if (memberId > 0)
            {
                lblTitle.Text = "Edit Member";
                LoadMemberData();
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

                    // Handle both Type and MemberType columns
                    if (row.Table.Columns.Contains("Type") && row["Type"] != DBNull.Value)
                    {
                        cmbMemberType.Text = row["Type"].ToString();
                    }
                    else if (row.Table.Columns.Contains("MemberType") && row["MemberType"] != DBNull.Value)
                    {
                        cmbMemberType.Text = row["MemberType"].ToString();
                    }
                    dtpRegistration.Value = Convert.ToDateTime(row["RegistrationDate"]);
                    dtpExpiration.Value = Convert.ToDateTime(row["ExpirationDate"]);
                    cmbStatus.Text = row["Status"].ToString();

                    // Load member card number if it exists
                    if (row.Table.Columns.Contains("MemberCardNumber") && row["MemberCardNumber"] != DBNull.Value)
                    {
                        txtMemberCardNumber.Text = row["MemberCardNumber"].ToString();
                    }
                    else if (memberId > 0)
                    {
                        // Auto-generate if editing and doesn't exist
                        txtMemberCardNumber.Text = $"MEM-{DateTime.Now:yyyyMMdd}-{memberId:D4}";
                    }

                    // Load photo and ID paths if they exist
                    if (row.Table.Columns.Contains("PhotoPath") && row["PhotoPath"] != DBNull.Value)
                    {
                        photoPath = row["PhotoPath"].ToString();
                        if (!string.IsNullOrEmpty(photoPath) && System.IO.File.Exists(photoPath))
                        {
                            // Display photo if PictureBox exists
                            if (this.Controls.Find("picMemberPhoto", true).Length > 0)
                            {
                                var picBox = this.Controls.Find("picMemberPhoto", true)[0] as PictureBox;
                                if (picBox != null)
                                    picBox.Image = Image.FromFile(photoPath);
                            }
                        }
                    }

                    if (row.Table.Columns.Contains("ValidIDPath") && row["ValidIDPath"] != DBNull.Value)
                    {
                        validIDPath = row["ValidIDPath"].ToString();
                        if (!string.IsNullOrEmpty(validIDPath) && System.IO.File.Exists(validIDPath))
                        {
                            // Display ID if PictureBox exists
                            if (this.Controls.Find("picValidID", true).Length > 0)
                            {
                                var picBox = this.Controls.Find("picValidID", true)[0] as PictureBox;
                                if (picBox != null)
                                    picBox.Image = Image.FromFile(validIDPath);
                            }
                        }
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
            string memberCardNumber = txtMemberCardNumber?.Text?.Trim();

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
                    MessageBox.Show("Member saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    bool hasPhotoPath = CheckColumnExists(conn, "Members", "PhotoPath");
                    bool hasValidIDPath = CheckColumnExists(conn, "Members", "ValidIDPath");
                    bool hasMemberCardNumber = CheckColumnExists(conn, "Members", "MemberCardNumber");

                    string query;
                    // Use Type column (standard), fallback to MemberType if Type doesn't exist
                    bool hasType = CheckColumnExists(conn, "Members", "Type");
                    string typeColumn = hasType ? "Type" : "MemberType";
                    string columns = $"FirstName, LastName, Email, {typeColumn}, RegistrationDate, ExpirationDate, Status";
                    string values = "@FirstName, @LastName, @Email, @Type, @RegDate, @ExpDate, @Status";

                    if (hasContact) { columns += ", Contact"; values += ", @Contact"; }
                    if (hasAddress) { columns += ", Address"; values += ", @Address"; }
                    if (hasPhotoPath) { columns += ", PhotoPath"; values += ", @PhotoPath"; }
                    if (hasValidIDPath) { columns += ", ValidIDPath"; values += ", @ValidIDPath"; }
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
                        if (hasPhotoPath)
                            cmd.Parameters.AddWithValue("@PhotoPath", photoPath ?? (object)DBNull.Value);
                        if (hasValidIDPath)
                            cmd.Parameters.AddWithValue("@ValidIDPath", validIDPath ?? (object)DBNull.Value);
                        if (hasMemberCardNumber && !string.IsNullOrWhiteSpace(memberCardNumber))
                            cmd.Parameters.AddWithValue("@MemberCardNumber", memberCardNumber);

                        return cmd.ExecuteNonQuery() > 0;
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
                    bool hasPhotoPath = CheckColumnExists(conn, "Members", "PhotoPath");
                    bool hasValidIDPath = CheckColumnExists(conn, "Members", "ValidIDPath");
                    bool hasMemberCardNumber = CheckColumnExists(conn, "Members", "MemberCardNumber");

                    // Use Type column (standard), fallback to MemberType if Type doesn't exist
                    bool hasType = CheckColumnExists(conn, "Members", "Type");
                    string typeColumn = hasType ? "Type" : "MemberType";
                    string setClause = $"FirstName=@FirstName, LastName=@LastName, Email=@Email, {typeColumn}=@Type, RegistrationDate=@RegDate, ExpirationDate=@ExpDate, Status=@Status";
                    
                    if (hasContact) setClause += ", Contact=@Contact";
                    if (hasAddress) setClause += ", Address=@Address";
                    if (hasPhotoPath) setClause += ", PhotoPath=@PhotoPath";
                    if (hasValidIDPath) setClause += ", ValidIDPath=@ValidIDPath";
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

        // Photo upload handler - wire this to a button or picture box click event
        private void btnUploadPhoto_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png|All Files|*.*";
                    openFileDialog.FilterIndex = 1;
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        photoPath = openFileDialog.FileName;
                        // Display in PictureBox if exists
                        if (this.Controls.Find("picMemberPhoto", true).Length > 0)
                        {
                            var picBox = this.Controls.Find("picMemberPhoto", true)[0] as PictureBox;
                            if (picBox != null)
                                picBox.Image = Image.FromFile(photoPath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading photo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Valid ID upload handler - wire this to a button or picture box click event
        private void btnUploadValidID_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png|PDF Files|*.pdf|All Files|*.*";
                    openFileDialog.FilterIndex = 1;
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        validIDPath = openFileDialog.FileName;
                        // Display in PictureBox if exists and is image
                        if (this.Controls.Find("picValidID", true).Length > 0 && validIDPath.ToLower().EndsWith(".jpg") || validIDPath.ToLower().EndsWith(".jpeg") || validIDPath.ToLower().EndsWith(".png"))
                        {
                            var picBox = this.Controls.Find("picValidID", true)[0] as PictureBox;
                            if (picBox != null)
                                picBox.Image = Image.FromFile(validIDPath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading valid ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
