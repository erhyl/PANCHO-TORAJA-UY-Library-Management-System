using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Project5LMS.Helpers;
using Project5LMS.Data;
using Project5LMS.Services;

namespace Project5LMS.Forms.LibraryStaff.Settings
{
    public partial class StaffChangePasswordForm : Form
    {
        private readonly DatabaseContext _dbContext;

        public StaffChangePasswordForm()
        {
            InitializeComponent();
            _dbContext = ServiceFactory.GetDbContext();
        }

        private void StaffChangePasswordForm_Load(object sender, EventArgs e)
        {
            // Clear all fields on load
            txtCurrentPassword.Clear();
            txtNewPassword.Clear();
            txtConfirmPassword.Clear();
            txtCurrentPassword.Focus();
        }

        private void btnUpdatePassword_Click(object sender, EventArgs e)
        {
            try
            {
                UpdatePassword();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating password: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdatePassword()
        {
            // Validate all fields are filled
            if (string.IsNullOrWhiteSpace(txtCurrentPassword.Text) || 
                string.IsNullOrWhiteSpace(txtNewPassword.Text) || 
                string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                MessageBox.Show("Please fill in all password fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCurrentPassword.Focus();
                return;
            }

            // Validate new password and confirmation match
            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("New password and confirmation do not match.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Focus();
                return;
            }

            // Validate new password is different from current
            if (txtCurrentPassword.Text == txtNewPassword.Text)
            {
                MessageBox.Show("New password must be different from your current password.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPassword.Focus();
                return;
            }

            // Validate minimum password length
            if (txtNewPassword.Text.Length < 6)
            {
                MessageBox.Show("New password must be at least 6 characters long.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPassword.Focus();
                return;
            }

            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();

                    // Get current password hash from database
                    string query = "SELECT PasswordHash FROM Users WHERE UserID = @UserID";
                    string currentHash = "";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", CurrentUser.UserID);
                        object result = cmd.ExecuteScalar();
                        if (result != null) currentHash = result.ToString();
                    }

                    // Verify current password
                    if (!PasswordHasher.Verify(txtCurrentPassword.Text, currentHash))
                    {
                        MessageBox.Show("Current password is incorrect.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCurrentPassword.Focus();
                        txtCurrentPassword.SelectAll();
                        return;
                    }

                    // Hash new password
                    string newHash = PasswordHasher.HashPassword(txtNewPassword.Text);

                    // Update password in database
                    string updateQuery = "UPDATE Users SET PasswordHash = @Hash WHERE UserID = @UserID";
                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Hash", newHash);
                        cmd.Parameters.AddWithValue("@UserID", CurrentUser.UserID);
                        cmd.ExecuteNonQuery();
                    }

                    // Log the password change
                    AuditLogger.LogSecurity("Password Changed", 
                        $"User: {CurrentUser.Email} changed their password", 
                        "Success");

                    MessageBox.Show("Password updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Clear fields
                    txtCurrentPassword.Clear();
                    txtNewPassword.Clear();
                    txtConfirmPassword.Clear();

                    // Close form
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating password: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AuditLogger.LogSecurity("Password Change Failed", 
                    $"User: {CurrentUser.Email}, Error: {ex.Message}", 
                    "Failed");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
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
                btnUpdatePassword_Click(sender, e);
            }
        }

        private void txtCurrentPassword_Enter(object sender, EventArgs e)
        {
            panelCurrentPassword.BackColor = System.Drawing.Color.FromArgb(255, 255, 250);
        }

        private void txtCurrentPassword_Leave(object sender, EventArgs e)
        {
            panelCurrentPassword.BackColor = System.Drawing.Color.White;
        }

        private void txtNewPassword_Enter(object sender, EventArgs e)
        {
            panelNewPassword.BackColor = System.Drawing.Color.FromArgb(255, 255, 250);
        }

        private void txtNewPassword_Leave(object sender, EventArgs e)
        {
            panelNewPassword.BackColor = System.Drawing.Color.White;
        }

        private void txtConfirmPassword_Enter(object sender, EventArgs e)
        {
            panelConfirmPassword.BackColor = System.Drawing.Color.FromArgb(255, 255, 250);
        }

        private void txtConfirmPassword_Leave(object sender, EventArgs e)
        {
            panelConfirmPassword.BackColor = System.Drawing.Color.White;
        }
    }
}
