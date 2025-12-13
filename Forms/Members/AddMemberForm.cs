using Project5LMS.Controllers;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Project5LMS.Admin_Dashboard
{
    public partial class AddMemberForm : Form
    {
        private MembersController membersController;
        private int memberId = 0; // 0 = new member, >0 = edit existing

        public AddMemberForm(int memberId = 0)
        {
            InitializeComponent();
            membersController = new MembersController(); // MUST initialize here
            this.memberId = memberId;
        }


        private void AddMemberForm_Load(object sender, EventArgs e)
        {
            // Load comboboxes
            cmbMemberType.Items.Clear();
            cmbMemberType.Items.AddRange(new string[] { "Student", "Faculty", "Staff", "Guest" });
            cmbMemberType.SelectedIndex = 0;

            cmbStatus.Items.Clear();
            cmbStatus.Items.AddRange(new string[] { "Active", "Inactive", "Suspended", "Expired" });
            cmbStatus.SelectedIndex = 0;

            // If editing, load member data
            if (memberId > 0)
                LoadMemberData();
        }

        private void LoadMemberData()
        {
            DataTable dt = membersController.GetMembers();
            DataRow row = dt.AsEnumerable().FirstOrDefault(r => Convert.ToInt32(r["MemberID"]) == memberId);

            if (row != null)
            {
                txtFullName.Text = row["FullName"].ToString();
                txtEmail.Text = row["Email"].ToString();
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            string fullName = txtFullName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string type = cmbMemberType.Text;
            DateTime regDate = dtpRegistration.Value;
            DateTime expDate = dtpExpiration.Value;
            string status = cmbStatus.Text;

            bool success;

            if (memberId == 0)
            {
                // Add new member
                success = membersController.AddMember(fullName, email, type, regDate, expDate, status);
            }
            else
            {
                // Update existing member
                success = membersController.UpdateMember(memberId, fullName, email, type, regDate, expDate, status);
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
    }
}
