using Project5LMS.Controllers;
using System;
using System.Configuration;
using System.Data;
using System.Windows.Forms;

namespace Project5LMS.Admin_Dashboard
{
    public partial class MembersForm : Form
    {
        private MembersController membersController;
        private string connectionString;

        public MembersForm()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["MySqlConnectionString"]?.ConnectionString;
            membersController = new MembersController();
        }

        private void MembersForm_Load(object sender, EventArgs e)
        {
            AutoExpireMembers();
            LoadFilters();
            LoadMembers();
        }

        private void dta_Members_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Optional: handle button clicks or special cells
        }

        // 🔹 Auto-expire members
        private void AutoExpireMembers()
        {
            using (var conn = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Members SET Status='Expired' WHERE ExpirationDate < CURDATE() AND Status != 'Expired'";
                var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
        }

        // 🔹 Load members into DataGridView
        private void LoadMembers()
        {
            dta_Members.DataSource = membersController.GetMembers();
            dta_Members.Columns["MemberID"].Visible = false;
            dta_Members.Columns["FullName"].HeaderText = "Member";
            dta_Members.Columns["MemberType"].HeaderText = "Type";
            dta_Members.Columns["Email"].HeaderText = "Email";
            dta_Members.Columns["RegistrationDate"].HeaderText = "Registered";
            dta_Members.Columns["ExpirationDate"].HeaderText = "Expires";
        }

        // 🔹 Load filter comboboxes
        private void LoadFilters()
        {
            cmbTypes.Items.Clear();
            cmbTypes.Items.AddRange(new string[] { "All", "Student", "Faculty", "Staff", "Guest" });
            cmbTypes.SelectedIndex = 0;

            cmbStatus.Items.Clear();
            cmbStatus.Items.AddRange(new string[] { "All", "Active", "Inactive", "Suspended", "Expired" });
            cmbStatus.SelectedIndex = 0;
        }

        // 🔹 Apply search and filter
        private void ApplyFilters()
        {
            string keyword = txtSearch.Text.Trim();
            string type = cmbTypes.Text;
            string status = cmbStatus.Text;

            dta_Members.DataSource = membersController.SearchMembers(keyword, type, status);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e) => ApplyFilters();
        private void cmbTypes_SelectedIndexChanged(object sender, EventArgs e) => ApplyFilters();
        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e) => ApplyFilters();

        // 🔹 Add Member
        private void btnAddMember_Click(object sender, EventArgs e)
        {
            AddMemberForm addForm = new AddMemberForm(); // memberId defaults to 0
            if (addForm.ShowDialog() == DialogResult.OK)
                LoadMembers();
        }

        // 🔹 Edit/View member on double-click
        private void dta_Members_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int memberId = Convert.ToInt32(dta_Members.Rows[e.RowIndex].Cells["MemberID"].Value);
                AddMemberForm editForm = new AddMemberForm(memberId);
                if (editForm.ShowDialog() == DialogResult.OK)
                    LoadMembers();
            }
        }

        private void btnDeleteMember_Click(object sender, EventArgs e)
        {

        }
    }
}
