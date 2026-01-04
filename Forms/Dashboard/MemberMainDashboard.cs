using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project5LMS.Forms.MemberRoleForms;
using Project5LMS.Forms.Settings;

namespace Project5LMS.Forms.Dashboard
{
    public partial class MemberMainDashboard : Form
    {
        public MemberMainDashboard()
        {
            InitializeComponent();
        }

        // Helper method to load forms inside MembersMainPanel
        private void LoadFormInPanel(Form form)
        {
            MembersMainPanel.Controls.Clear();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            MembersMainPanel.Controls.Add(form);
            MembersMainPanel.Tag = form;
            form.Show();
        }

        private void MemberDashboard_Load(object sender, EventArgs e)
        {
            // Load Browse Books by default
            LoadFormInPanel(new Project5LMS.Forms.MemberRoleForms.MembersBrowseBooksForm());
        }

        private void btnMembersDashboard_Click(object sender, EventArgs e)
        {
            // For now, show Browse Books as the default dashboard view
            LoadFormInPanel(new Project5LMS.Forms.MemberRoleForms.MembersDashboardForm());
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            LoadFormInPanel(new Project5LMS.Forms.Settings.SettingsForm());
        }

        private void MembersMainPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnBrowseBooks_Click(object sender, EventArgs e)
        {
            LoadFormInPanel(new Project5LMS.Forms.MemberRoleForms.MembersBrowseBooksForm());
        }

        private void btnMybooks_Click(object sender, EventArgs e)
        {
            LoadFormInPanel(new Project5LMS.Forms.MemberRoleForms.MembersMyBooksForm());
        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
