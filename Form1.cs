using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
// Remove the ambiguous namespace import
// using Project5LMS.Admin_Dashboard;

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
            string role = cmbRole.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            string connStr = "server=localhost;database=libraryDB;uid=root;pwd=;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();

                    string query = "SELECT role FROM users WHERE username=@username AND password=@password AND role=@role";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.Parameters.AddWithValue("@role", role);

                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            string userRole = result.ToString();

                            MessageBox.Show("✅ Login Successful!");

                            if (userRole.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                            {
                                // Use the full namespace for the AdminDashboard form
                                Project5LMS.Admin_Dashboard.Admin_Dashboard adminForm = new Project5LMS.Admin_Dashboard.Admin_Dashboard();
                                adminForm.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Access not configured for this role yet.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("❌ Invalid username, password, or role.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Database Error: " + ex.Message);
            }
        }

        private void cmbRole_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {

        }
    }
}
