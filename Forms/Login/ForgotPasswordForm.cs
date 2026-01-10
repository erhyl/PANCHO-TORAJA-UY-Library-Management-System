using System;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Project5LMS.Helpers;
using Project5LMS.Data;
namespace Project5LMS
{
    public partial class ForgotPasswordForm : Form
    {
        private string connectionString;
        public ForgotPasswordForm()
        {
            InitializeComponent();
            try
            {
                connectionString = DatabaseHelper.GetConnectionString();
            }
            catch
            {
                connectionString = "Server=localhost;Database=librarydb;Uid=root;Pwd=;";
            }
        }
        private void ForgotPasswordForm_Load(object sender, EventArgs e)
        {
            txtEmail.Focus();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            try
            {
                string email = txtEmail.Text.Trim();
                if (string.IsNullOrWhiteSpace(email))
                {
                    MessageBox.Show("Please enter your email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return;
                }
                if (!InputValidator.IsValidEmail(email))
                {
                    MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return;
                }
                if (!UserExists(email))
                {
                    MessageBox.Show("No account found with this email address.", "Account Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                string newPassword = GenerateRandomPassword();
                if (UpdatePassword(email, newPassword))
                {
                    MessageBox.Show($"Password reset successful!\n\nYour new password is: {newPassword}\n\nPlease change this password after logging in.",
                        "Password Reset",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to reset password. Please try again or contact support.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool UserExists(string email)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Users WHERE Email = @email";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@email", email);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error checking user existence: {ex.Message}");
                return false;
            }
        }
        private bool UpdatePassword(string email, string newPassword)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string hashedPassword = PasswordHasher.HashPassword(newPassword);
                    string query = "UPDATE Users SET PasswordHash = @passwordHash WHERE Email = @email";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@passwordHash", hashedPassword);
                        cmd.Parameters.AddWithValue("@email", email);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating password: {ex.Message}");
                return false;
            }
        }
        private string GenerateRandomPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            char[] password = new char[8];
            for (int i = 0; i < 8; i++)
            {
                password[i] = chars[random.Next(chars.Length)];
            }
            return new string(password);
        }
        private void txtEmail_Enter(object sender, EventArgs e)
        {
            panelEmailContainer.BackColor = Color.FromArgb(255, 255, 250);
            panelEmailContainer.BorderStyle = BorderStyle.FixedSingle;
        }
        private void txtEmail_Leave(object sender, EventArgs e)
        {
            panelEmailContainer.BackColor = Color.White;
            panelEmailContainer.BorderStyle = BorderStyle.FixedSingle;
        }
        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnResetPassword_Click(sender, e);
            }
        }
    }
}