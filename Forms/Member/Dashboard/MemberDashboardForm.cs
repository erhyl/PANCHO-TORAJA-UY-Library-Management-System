using System;
using System.Drawing;
using System.Windows.Forms;
using Project5LMS.Forms.Member.Profile;
using Project5LMS.Forms.Member.Search;
using Project5LMS.Forms.Member.Borrowings;
using Project5LMS.Forms.Member.Fines;
using Project5LMS.Forms.Member.Reservations;

namespace Project5LMS.Forms.Member.Dashboard
{
    public partial class MemberDashboardForm : Form
    {
        public MemberDashboardForm()
        {
            InitializeComponent();
        }

        private void MemberDashboardForm_Load(object sender, EventArgs e)
        {
            ShowDashboard();
            SetActiveButton(btnDashboard);
        }

        private void LoadFormInPanel(Form form)
        {
            panelMainContent.Controls.Clear();
            panelDashboardContent.Visible = false;
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            panelMainContent.Controls.Add(form);
            panelMainContent.Tag = form;
            form.Show();
        }

        private void SetActiveButton(Button activeButton)
        {
            btnDashboard.BackColor = Color.Transparent;
            btnSearch.BackColor = Color.Transparent;
            btnMyBorrowings.BackColor = Color.Transparent;
            btnReservations.BackColor = Color.Transparent;
            btnFines.BackColor = Color.Transparent;
            btnProfile.BackColor = Color.Transparent;

            activeButton.BackColor = Color.FromArgb(178, 34, 34);
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnDashboard);
            ShowDashboard();
        }

        private void ShowDashboard()
        {
            panelMainContent.Controls.Clear();
            panelMainContent.Controls.Add(panelDashboardContent);
            panelDashboardContent.Visible = true;
            panelDashboardContent.BringToFront();
            LoadDashboardData();
        }

        private void LoadDashboardData()
        {

            lblWelcomeTitle.Text = "Welcome Back, John Doe";

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnSearch);
            LoadFormInPanel(new MemberSearchForm());
        }

        private void btnMyBorrowings_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnMyBorrowings);
            LoadFormInPanel(new MyBorrowingsForm());
        }

        private void btnReservations_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnReservations);
            LoadFormInPanel(new MemberReservationsForm());
        }

        private void btnFines_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnFines);
            LoadFormInPanel(new MemberFinesForm());
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnProfile);
            LoadFormInPanel(new MemberProfileForm());
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Project5LMS.LoginForm login = new Project5LMS.LoginForm();
            login.Show();
        }

        private void lblBook1Title_Click(object sender, EventArgs e)
        {

        }
    }
}
