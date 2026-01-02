using Project5LMS.Forms.Dashboard; // Import the new AdminMainForm
using Project5LMS.Helpers;
using Project5LMS.Properties;
using Project5LMS.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Project5LMS
{
    public partial class LoginForm : Form
    {
        private bool isPasswordVisible = false;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            LoadEyeIcon();
            // Ensure the eye icon is on top and clickable
            picEyeIcon.BringToFront();
            // Set default role selection
            if (cmbRole.Items.Count > 0 && cmbRole.SelectedIndex == -1)
            {
                cmbRole.SelectedIndex = 0;
            }
        }

        private void LoadEyeIcon()
        {
            bool iconLoaded = false;

            // Method 1: Try to load from embedded resources (Properties.Resources)
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

            // Method 2: If resource loading failed, try file paths
            if (!iconLoaded)
            {
                try
                {
                    // Try multiple possible paths
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

            // If still not loaded, show placeholder
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
                
                // Get current cursor position and selection
                int selectionStart = txtPassword.SelectionStart;
                int selectionLength = txtPassword.SelectionLength;
                
                // Toggle password visibility
                txtPassword.UseSystemPasswordChar = !isPasswordVisible;
                
                // Restore cursor position
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
            // Visual feedback when hovering
            picEyeIcon.BackColor = Color.FromArgb(240, 240, 240);
        }

        private void picEyeIcon_MouseLeave(object sender, EventArgs e)
        {
            // Restore transparent background
            picEyeIcon.BackColor = Color.Transparent;
        }

        private void LoginForm_Click(object sender, EventArgs e)
        {
            // Close combo box dropdown when clicking on the form
            if (cmbRole.DroppedDown)
            {
                cmbRole.DroppedDown = false;
            }
        }

        private void btnSignin_Click(object sender, EventArgs e)
        {
            string email = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string selectedRole = cmbRole.SelectedItem?.ToString() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Please enter your email address.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter your password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(selectedRole))
            {
                MessageBox.Show("Please select a role.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate Admin email domain
            if (selectedRole.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                const string adminEmailDomain = "@admin.umindanao.edu.ph";
                if (!email.EndsWith(adminEmailDomain, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show($"Admin accounts must use an email ending with '{adminEmailDomain}'.\n\nYour email: {email}", 
                        "Invalid Admin Email", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Warning);
                    return;
                }
            }

            UserService userService = new UserService();
            var user = userService.Login(email, password);

            if (user == null)
            {
                MessageBox.Show("Invalid email or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Verify that the user's role matches the selected role
            if (!user.Role.Equals(selectedRole, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show($"The selected role '{selectedRole}' does not match your account role '{user.Role}'.\n\nPlease select the correct role and try again.", 
                    "Role Mismatch", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning);
                return;
            }

            // Additional validation: Ensure Admin role has correct email domain
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

            // Set session
            CurrentUser.Set(user);

            // Role-based redirection (explicit and non-overlapping)
            switch (user.Role)
            {
                case "Admin":
                    {
                        var adminForm = new AdminMainForm();
                        adminForm.Show();
                        this.Hide();
                        break;
                    }
                case "LibraryStaff":
                    {
                        var staffForm = new User_Dashboard();
                        staffForm.Show();
                        this.Hide();
                        break;
                    }
                case "Member":
                    {
                        var memberForm = new User_Dashboard();
                        memberForm.Show();
                        this.Hide();
                        break;
                    }
                case "Librarian": // Backward compatibility
                    {
                        var staffForm = new User_Dashboard();
                        staffForm.Show();
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

        private void cmbRole_SelectedIndexChanged(object sender, EventArgs e) { }

        private void cmbRole_MouseLeave(object sender, EventArgs e)
        {
            // Close the dropdown when mouse leaves the combo box
            if (cmbRole.DroppedDown)
            {
                cmbRole.DroppedDown = false;
            }
        }
        private void txtUsername_TextChanged(object sender, EventArgs e) { }
        private void txtPassword_TextChanged(object sender, EventArgs e) { }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
