using MySql.Data.MySqlClient;
using Project5LMS.Forms.Dashboard; // Import the new AdminMainForm
using Project5LMS.Helpers;
using Project5LMS.Services;
using System;
using System.Windows.Forms;

namespace Project5LMS
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
        }

        private void btnSignin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            UserService userService = new UserService();
            var user = userService.Login(username, password);

            if (user == null)
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Set session
            CurrentUser.Set(user);

            // Role-based redirection
            if (user.Role == "Admin")
            {
                AdminMainForm adminForm = new AdminMainForm(); // use the renamed form
                adminForm.Show();
            }

            else if (user.Role == "Librarian" || user.Role == "LibraryStaff")
            {
                User_Dashboard staffForm = new User_Dashboard();
                staffForm.Show();
            }
            else
            {
                MessageBox.Show("Members cannot log in to this system yet.", "Access Denied",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.Hide();
        }

        private void cmbRole_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txtUsername_TextChanged(object sender, EventArgs e) { }
        private void txtPassword_TextChanged(object sender, EventArgs e) { }
        private void btnSignUp_Click(object sender, EventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void picBoxProfileIcon_Click(object sender, EventArgs e) { }
        private void pictureBox2_Click(object sender, EventArgs e) { }
    }
}
