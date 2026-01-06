using Project5LMS.Forms.Admin.Dashboard;
using Project5LMS.Forms.LibraryStaff.Dashboard;
using Project5LMS.Forms.Member.Dashboard;
using Project5LMS.Helpers;
using Project5LMS.Properties;
using Project5LMS.Services;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project5LMS
{
    public partial class LoginForm : Form
    {
        private bool isPasswordVisible = false;
        private LoginSecurityService securityService;
        private bool isProcessingLogin = false;

        public LoginForm()
        {
            InitializeComponent();
            securityService = new LoginSecurityService();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            LoadEyeIcon();
            SetupPasswordField();

            picEyeIcon.BringToFront();

            if (cmbRole.Items.Count > 0 && cmbRole.SelectedIndex == -1)
            {
                cmbRole.SelectedIndex = 0;
            }

            securityService.CleanupOldRecords();

            txtUsername.KeyDown += TextBox_KeyDown;
            txtPassword.KeyDown += TextBox_KeyDown;
            cmbRole.KeyDown += ComboBox_KeyDown;

            txtUsername.Focus();
        }

        private void SetupPasswordField()
        {

            int iconWidth = 25;
            int iconHeight = 22;
            int padding = 5;

            int iconX = txtPassword.Left + txtPassword.Width - iconWidth - padding;
            int iconY = txtPassword.Top + (txtPassword.Height - iconHeight) / 2;

            picEyeIcon.Location = new Point(iconX, iconY);
            picEyeIcon.Size = new Size(iconWidth, iconHeight);

            picEyeIcon.BringToFront();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !isProcessingLogin)
            {
                e.SuppressKeyPress = true;
                btnSignin_Click(sender, e);
            }
        }

        private void ComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !isProcessingLogin)
            {
                e.SuppressKeyPress = true;
                btnSignin_Click(sender, e);
            }
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

            ClearErrorIndicators();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

            ClearErrorIndicators();
        }

        private void ClearErrorIndicators()
        {

            txtUsername.BackColor = Color.White;
            txtPassword.BackColor = Color.White;
        }

        private void LoadEyeIcon()
        {
            bool iconLoaded = false;

            try
            {
                var eyeResource = Resources.ResourceManager.GetObject("eye");
                if (eyeResource != null && eyeResource is Bitmap)
                {
                    picEyeIcon.Image = (Bitmap)eyeResource;
                    picEyeIcon.BackColor = Color.Transparent;
                    picEyeIcon.Visible = true;
                    iconLoaded = true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to load eye from resources: {ex.Message}");
            }

            if (!iconLoaded)
            {
                try
                {

                    string[] possiblePaths = new string[]
                    {
                        Path.Combine(Application.StartupPath, "Resources", "Images", "Icons", "eye.png"),
                        Path.Combine(Directory.GetParent(Application.StartupPath).Parent.FullName, "Resources", "Images", "Icons", "eye.png"),
                        Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Images", "Icons", "eye.png"),
                        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Images", "Icons", "eye.png"),
                        Path.GetFullPath(Path.Combine(Application.StartupPath, "..", "..", "Resources", "Images", "Icons", "eye.png"))
                    };

                    string iconPath = null;
                    foreach (string path in possiblePaths)
                    {
                        try
                        {
                            string fullPath = Path.GetFullPath(path);
                            if (File.Exists(fullPath))
                            {
                                iconPath = fullPath;
                                System.Diagnostics.Debug.WriteLine($"Found eye icon at: {iconPath}");
                                break;
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    if (iconPath != null && File.Exists(iconPath))
                    {
                        picEyeIcon.Image = Image.FromFile(iconPath);
                        picEyeIcon.BackColor = Color.Transparent;
                        picEyeIcon.Visible = true;
                        iconLoaded = true;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Failed to load eye from file: {ex.Message}");
                }
            }

            if (!iconLoaded)
            {
                picEyeIcon.Visible = true;
                picEyeIcon.BackColor = Color.LightGray;
                picEyeIcon.Image = null;
                System.Diagnostics.Debug.WriteLine("Eye icon could not be loaded from any source.");
            }
        }

        private void picEyeIcon_Click(object sender, EventArgs e)
        {
            TogglePasswordVisibility();
        }

        private void TogglePasswordVisibility()
        {
            try
            {
                isPasswordVisible = !isPasswordVisible;

                int selectionStart = txtPassword.SelectionStart;
                int selectionLength = txtPassword.SelectionLength;

                txtPassword.UseSystemPasswordChar = !isPasswordVisible;

                txtPassword.SelectionStart = selectionStart;
                txtPassword.SelectionLength = selectionLength;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error toggling password visibility: {ex.Message}");
            }
        }

        private void picEyeIcon_MouseEnter(object sender, EventArgs e)
        {

            picEyeIcon.BackColor = Color.FromArgb(245, 245, 245);
        }

        private void picEyeIcon_MouseLeave(object sender, EventArgs e)
        {

            picEyeIcon.BackColor = Color.White;
        }

        private void LoginForm_Click(object sender, EventArgs e)
        {

            if (cmbRole.DroppedDown)
            {
                cmbRole.DroppedDown = false;
            }
        }

        private async void btnSignin_Click(object sender, EventArgs e)
        {

            if (isProcessingLogin)
                return;

            isProcessingLogin = true;
            btnSignin.Enabled = false;
            btnSignin.Text = "Signing in...";

            try
            {
                string email = txtUsername.Text.Trim();
                string password = txtPassword.Text.Trim();
                string selectedRole = cmbRole.SelectedItem?.ToString() ?? string.Empty;

                if (!ValidateInputs(email, password, selectedRole))
                {
                    return;
                }

                if (!PerformSecurityChecks(email))
                {
                    return;
                }

                email = InputValidator.SanitizeInput(email);
                if (InputValidator.ContainsSqlInjection(email) || InputValidator.ContainsSqlInjection(password))
                {
                    ShowError("Invalid characters detected in input.", txtUsername);
                    return;
                }

                await Task.Run(() =>
                {
                    try
                    {
                        UserService userService = new UserService();
                        var user = userService.Login(email, password);

                        this.Invoke((MethodInvoker)delegate
                        {
                            ProcessLoginResult(user, email, selectedRole);
                        });
                    }
                    catch (Exception ex)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            HandleLoginException(ex);
                        });
                    }
                });
            }
            finally
            {
                isProcessingLogin = false;
                btnSignin.Enabled = true;
                btnSignin.Text = "Sign in";
            }
        }

        private bool ValidateInputs(string email, string password, string selectedRole)
        {
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(email))
            {
                ShowError("Please enter your email address.", txtUsername);
                isValid = false;
            }
            else if (!InputValidator.IsValidEmail(email))
            {
                ShowError("Please enter a valid email address.", txtUsername);
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                ShowError("Please enter your password.", txtPassword);
                isValid = false;
            }
            else if (password.Length < 6)
            {
                ShowError("Password must be at least 6 characters long.", txtPassword);
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(selectedRole))
            {
                MessageBox.Show("Please select a role.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                isValid = false;
            }

            if (isValid && selectedRole.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                const string adminEmailDomain = "@admin.umindanao.edu.ph";
                if (!email.EndsWith(adminEmailDomain, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show($"Admin accounts must use an email ending with '{adminEmailDomain}'.\n\nYour email: {email}",
                        "Invalid Admin Email",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    isValid = false;
                }
            }

            return isValid;
        }

        private bool PerformSecurityChecks(string email)
        {

            if (securityService.IsAccountLockedOut(email, out string lockoutMessage))
            {
                MessageBox.Show(lockoutMessage, "Account Locked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (securityService.IsRateLimited(email, out string rateLimitMessage))
            {
                MessageBox.Show(rateLimitMessage, "Too Many Attempts", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void ProcessLoginResult(Models.User user, string email, string selectedRole)
        {
            if (user == null)
            {

                securityService.RecordFailedAttempt(email);
                int remainingAttempts = securityService.GetRemainingAttempts(email);

                string errorMessage = "Invalid email or password.";
                if (remainingAttempts < 5 && remainingAttempts > 0)
                {
                    errorMessage += $"\n\n{remainingAttempts} attempt(s) remaining before account lockout.";
                }
                else if (remainingAttempts == 0)
                {
                    errorMessage = "Account has been temporarily locked due to multiple failed login attempts. Please try again later.";
                }

                MessageBox.Show(errorMessage, "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ShowError("", txtPassword);
                return;
            }

            if (!user.Role.Equals(selectedRole, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show($"The selected role '{selectedRole}' does not match your account role '{user.Role}'.\n\nPlease select the correct role and try again.",
                    "Role Mismatch",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (user.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                const string adminEmailDomain = "@admin.umindanao.edu.ph";
                if (!user.Email.EndsWith(adminEmailDomain, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show($"Admin accounts must use an email ending with '{adminEmailDomain}'.\n\nYour account email: {user.Email}",
                        "Invalid Admin Account",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
            }

            securityService.RecordSuccessfulAttempt(email);

            CurrentUser.Set(user);

            AuditLogger.LogSecurity("User Login", $"Email: {email}, Role: {user.Role}", "Success");

            switch (user.Role)
            {
                case "Admin":
                    {
                        var adminForm = new AdminDashboardForm();
                        adminForm.Show();
                        this.Hide();
                        break;
                    }
                case "LibraryStaff":
                    {
                        var staffForm = new StaffDashboardForm();
                        staffForm.Show();
                        this.Hide();
                        break;
                    }
                case "Member":
                    {
                        var memberForm = new MemberDashboardForm();
                        memberForm.Show();
                        this.Hide();
                        break;
                    }
                default:
                    {
                        MessageBox.Show("Your account role is not recognized. Contact an administrator.", "Access Denied",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
            }
        }

        private void HandleLoginException(Exception ex)
        {

            MessageBox.Show("An error occurred during login. Please check your connection and try again.\n\nIf the problem persists, contact your system administrator.",
                "Login Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            System.Diagnostics.Debug.WriteLine($"Login exception: {ex}");
        }

        private void ShowError(string message, Control control)
        {
            if (!string.IsNullOrEmpty(message))
            {
                MessageBox.Show(message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            control.BackColor = Color.FromArgb(255, 240, 240);
            control.Focus();
        }

        private void cmbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearErrorIndicators();
        }

        private void cmbRole_MouseLeave(object sender, EventArgs e)
        {

            if (cmbRole.DroppedDown)
            {
                cmbRole.DroppedDown = false;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            txtUsername.BackColor = Color.FromArgb(255, 255, 250);
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
        }

        private void txtUsername_Leave(object sender, EventArgs e)
        {
            txtUsername.BackColor = Color.White;
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            txtPassword.BackColor = Color.FromArgb(255, 255, 250);
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            txtPassword.BackColor = Color.White;
        }

        private void btnSignin_MouseEnter(object sender, EventArgs e)
        {
            btnSignin.BackColor = Color.FromArgb(150, 0, 0);
            btnSignin.Cursor = Cursors.Hand;
        }

        private void btnSignin_MouseLeave(object sender, EventArgs e)
        {
            btnSignin.BackColor = Color.FromArgb(128, 0, 0);
            btnSignin.Cursor = Cursors.Default;
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void lnkForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                using (ForgotPasswordForm forgotPasswordForm = new ForgotPasswordForm())
                {
                    forgotPasswordForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening forgot password form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
