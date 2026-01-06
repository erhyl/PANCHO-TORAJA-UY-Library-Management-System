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

                    cmbMemberType.Text = row["MemberType"].ToString();
                    dtpRegistration.Value = Convert.ToDateTime(row["RegistrationDate"]);
                    dtpExpiration.Value = Convert.ToDateTime(row["ExpirationDate"]);
                    cmbStatus.Text = row["Status"].ToString();
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

            bool success = false;

            try
            {
                if (memberId == 0)
                {
                    success = AddMemberWithOptionalFields(firstName, lastName, email, contact, address, type, regDate, expDate, status);
                }
                else
                {
                    success = UpdateMemberWithOptionalFields(memberId, firstName, lastName, email, contact, address, type, regDate, expDate, status);
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
            string type, DateTime regDate, DateTime expDate, string status)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    bool hasContact = CheckColumnExists(conn, "Members", "Contact");
                    bool hasAddress = CheckColumnExists(conn, "Members", "Address");

                    string query;
                    if (hasContact && hasAddress)
                    {
                        query = @"INSERT INTO Members (FirstName, LastName, Email, Contact, Address, MemberType, RegistrationDate, ExpirationDate, Status)
                                 VALUES (@FirstName, @LastName, @Email, @Contact, @Address, @Type, @RegDate, @ExpDate, @Status)";
                    }
                    else if (hasContact)
                    {
                        query = @"INSERT INTO Members (FirstName, LastName, Email, Contact, MemberType, RegistrationDate, ExpirationDate, Status)
                                 VALUES (@FirstName, @LastName, @Email, @Contact, @Type, @RegDate, @ExpDate, @Status)";
                    }
                    else
                    {
                        query = @"INSERT INTO Members (FirstName, LastName, Email, MemberType, RegistrationDate, ExpirationDate, Status)
                                 VALUES (@FirstName, @LastName, @Email, @Type, @RegDate, @ExpDate, @Status)";
                    }

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
            string type, DateTime regDate, DateTime expDate, string status)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    bool hasContact = CheckColumnExists(conn, "Members", "Contact");
                    bool hasAddress = CheckColumnExists(conn, "Members", "Address");

                    string query;
                    if (hasContact && hasAddress)
                    {
                        query = @"UPDATE Members 
                                 SET FirstName=@FirstName, LastName=@LastName, Email=@Email, Contact=@Contact, Address=@Address, 
                                     MemberType=@Type, RegistrationDate=@RegDate, ExpirationDate=@ExpDate, Status=@Status
                                 WHERE MemberID=@ID";
                    }
                    else if (hasContact)
                    {
                        query = @"UPDATE Members 
                                 SET FirstName=@FirstName, LastName=@LastName, Email=@Email, Contact=@Contact, 
                                     MemberType=@Type, RegistrationDate=@RegDate, ExpirationDate=@ExpDate, Status=@Status
                                 WHERE MemberID=@ID";
                    }
                    else
                    {
                        query = @"UPDATE Members 
                                 SET FirstName=@FirstName, LastName=@LastName, Email=@Email, 
                                     MemberType=@Type, RegistrationDate=@RegDate, ExpirationDate=@ExpDate, Status=@Status
                                 WHERE MemberID=@ID";
                    }

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
