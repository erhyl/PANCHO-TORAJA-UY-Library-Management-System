using System;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Project5LMS.Helpers;
using Project5LMS.Data;

namespace Project5LMS
{
    public partial class ResetPasswordForm : Form
    {
        private string connectionString;
        private string userEmail;

        public ResetPasswordForm(string email)
        {
            InitializeComponent();
            userEmail = email;
            try
            {
                connectionString = DatabaseHelper.GetConnectionString();
            }
            catch
            {
                connectionString = "Server=localhost;Database=librarydb;Uid=root;Pwd=;";
            }
        }

        private void ResetPasswordForm_Load(object sender, EventArgs e)
        {
            txtCurrentPassword.Focus();
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
                // Validate all fields are filled
                if (string.IsNullOrWhiteSpace(txtCurrentPassword.Text) ||
                    string.IsNullOrWhiteSpace(txtNewPassword.Text) ||
                    string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
                {
                    MessageBox.Show("Please fill in all password fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCurrentPassword.Focus();
                    return;
                }

                // Validate new password and confirmation match
                if (txtNewPassword.Text != txtConfirmPassword.Text)
                {
                    MessageBox.Show("New password and confirmation do not match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtConfirmPassword.Focus();
                    txtConfirmPassword.SelectAll();
                    return;
                }

                // Validate new password is different from current
                if (txtCurrentPassword.Text == txtNewPassword.Text)
                {
                    MessageBox.Show("New password must be different from your current password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNewPassword.Focus();
                    txtNewPassword.SelectAll();
                    return;
                }

                // Validate minimum password length
                if (txtNewPassword.Text.Length < 6)
                {
                    MessageBox.Show("New password must be at least 6 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNewPassword.Focus();
                    txtNewPassword.SelectAll();
                    return;
                }

                // Verify current password and update
                if (UpdatePassword())
                {
                    MessageBox.Show("Password reset successful!\n\nPlease login with your new password.",
                        "Password Reset",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool UpdatePassword()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Get current password hash from database
                    string query = "SELECT PasswordHash FROM Users WHERE Email = @email LIMIT 1";
                    string currentHash = "";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@email", userEmail);
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            currentHash = result.ToString();
                        }
                    }

                    // Verify current password
                    if (string.IsNullOrEmpty(currentHash) || !PasswordHasher.Verify(txtCurrentPassword.Text, currentHash))
                    {
                        MessageBox.Show("Current password is incorrect.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCurrentPassword.Focus();
                        txtCurrentPassword.SelectAll();
                        return false;
                    }

                    // Hash new password
                    string newHash = PasswordHasher.HashPassword(txtNewPassword.Text);

                    // Update password in database
                    string updateQuery = "UPDATE Users SET PasswordHash = @passwordHash WHERE Email = @email";
                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@passwordHash", newHash);
                        cmd.Parameters.AddWithValue("@email", userEmail);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        
                        if (rowsAffected > 0)
                        {
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Failed to update password. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating password: {ex.Message}");
                MessageBox.Show($"An error occurred while updating password: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void txtCurrentPassword_Enter(object sender, EventArgs e)
        {
            panelCurrentPassword.BackColor = Color.FromArgb(255, 255, 250);
            panelCurrentPassword.BorderStyle = BorderStyle.FixedSingle;
        }

        private void txtCurrentPassword_Leave(object sender, EventArgs e)
        {
            panelCurrentPassword.BackColor = Color.White;
            panelCurrentPassword.BorderStyle = BorderStyle.FixedSingle;
        }

        private void txtNewPassword_Enter(object sender, EventArgs e)
        {
            panelNewPassword.BackColor = Color.FromArgb(255, 255, 250);
            panelNewPassword.BorderStyle = BorderStyle.FixedSingle;
        }

        private void txtNewPassword_Leave(object sender, EventArgs e)
        {
            panelNewPassword.BackColor = Color.White;
            panelNewPassword.BorderStyle = BorderStyle.FixedSingle;
        }

        private void txtConfirmPassword_Enter(object sender, EventArgs e)
        {
            panelConfirmPassword.BackColor = Color.FromArgb(255, 255, 250);
            panelConfirmPassword.BorderStyle = BorderStyle.FixedSingle;
        }

        private void txtConfirmPassword_Leave(object sender, EventArgs e)
        {
            panelConfirmPassword.BackColor = Color.White;
            panelConfirmPassword.BorderStyle = BorderStyle.FixedSingle;
        }

        private void txtCurrentPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtNewPassword.Focus();
            }
        }

        private void txtNewPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtConfirmPassword.Focus();
            }
        }

        private void txtConfirmPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnResetPassword_Click(sender, e);
            }
        }
    }
}
