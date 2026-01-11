using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Project5LMS.Helpers;
using Project5LMS.Services;
using Project5LMS.Data;
namespace Project5LMS.Forms.Admin.UserManagement
{
    public partial class AddUserForm : Form
    {
        private readonly DatabaseContext _dbContext;
        private readonly int? _editUserId;
        private bool _isEditMode => _editUserId.HasValue;
        
        public AddUserForm(int? userId = null)
        {
            InitializeComponent();
            _dbContext = ServiceFactory.GetDbContext();
            _editUserId = userId;
        }
        private void AddUserForm_Load(object sender, EventArgs e)
        {
            cmbRole.Items.Clear();
            cmbRole.Items.AddRange(new string[] { "Admin", "LibraryStaff", "Member" });
            cmbRole.SelectedIndex = 0;
            StyleControls();
            
            // Set default permissions based on role
            cmbRole.SelectedIndexChanged += CmbRole_SelectedIndexChanged;
            SetDefaultPermissions();
            
            if (_isEditMode)
            {
                this.Text = "Edit User";
                if (lblTitle != null)
                    lblTitle.Text = "Edit User";
                LoadUserData();
                // Make password optional in edit mode - maintain same visual design
                if (lblPassword != null)
                    lblPassword.Text = "Password (optional - leave blank to keep current)";
                if (lblConfirmPassword != null)
                    lblConfirmPassword.Text = "Confirm Password (optional)";
                
                // Ensure all fields maintain the same visual style as add mode
                txtPassword.BackColor = Color.White;
                txtConfirmPassword.BackColor = Color.White;
                txtFirstName.BackColor = Color.White;
                txtLastName.BackColor = Color.White;
                cmbRole.BackColor = Color.White;
            }
        }
        
        private void CmbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDefaultPermissions();
        }
        
        private void SetDefaultPermissions()
        {
            // All checkboxes must be filled/checked by default
            chkSystemConfiguration.Checked = true;
            chkUserManagement.Checked = true;
            chkMemberManagement.Checked = true;
            chkCatalogManagement.Checked = true;
            chkCirculation.Checked = true;
            chkReservations.Checked = true;
            chkFineManagement.Checked = true;
            chkInventory.Checked = true;
            chkReports.Checked = true;
            chkSearch.Checked = true;
        }
        
        private void LoadUserData()
        {
            if (!_editUserId.HasValue) return;
            
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT UserID, Email, FirstName, LastName, Role FROM Users WHERE UserID = @UserId";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", _editUserId.Value);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtFirstName.Text = reader["FirstName"]?.ToString() ?? "";
                                txtLastName.Text = reader["LastName"]?.ToString() ?? "";
                                txtEmail.Text = reader["Email"]?.ToString() ?? "";
                                string role = reader["Role"]?.ToString() ?? "Member";
                                
                                int roleIndex = cmbRole.Items.IndexOf(role);
                                if (roleIndex >= 0)
                                    cmbRole.SelectedIndex = roleIndex;
                                
                                // Keep email read-only for data integrity but maintain same visual design
                                txtEmail.ReadOnly = true;
                                // Use same white background to match add mode design
                                txtEmail.BackColor = Color.White;
                                
                                // Load user permissions
                                LoadUserPermissions(conn, _editUserId.Value);
                            }
                            else
                            {
                                MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading user data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"LoadUserData error: {ex}");
            }
        }
        private void StyleControls()
        {
            int radius = 10;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius * 2, radius * 2, 180, 90);
            path.AddArc(panelMainContainer.Width - radius * 2, 0, radius * 2, radius * 2, 270, 90);
            path.AddArc(panelMainContainer.Width - radius * 2, panelMainContainer.Height - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(0, panelMainContainer.Height - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseAllFigures();
            panelMainContainer.Region = new Region(path);
            
            // Apply consistent styling to all controls - same design for both add and edit modes
            foreach (Control control in panelFormContent.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.BackColor = Color.White;
                    textBox.BorderStyle = BorderStyle.FixedSingle;
                    // Ensure read-only textboxes still look the same visually
                    if (textBox.ReadOnly)
                    {
                        textBox.BackColor = Color.White;
                        textBox.ForeColor = Color.Black;
                    }
                }
                else if (control is ComboBox comboBox)
                {
                    comboBox.BackColor = Color.White;
                    comboBox.FlatStyle = FlatStyle.Flat;
                }
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;
            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;
            string role = cmbRole.SelectedItem?.ToString() ?? "Member";
            try
            {
                if (_isEditMode)
                {
                    // Update existing user
                    using (var conn = _dbContext.GetConnection())
                    {
                        conn.Open();
                        string query = @"UPDATE Users 
                                       SET FirstName = @FirstName, LastName = @LastName, Role = @Role
                                       WHERE UserID = @UserId";
                        
                        // If password is provided, update it too
                        if (!string.IsNullOrWhiteSpace(password) && password.Length >= 6)
                        {
                            query = @"UPDATE Users 
                                     SET FirstName = @FirstName, LastName = @LastName, Role = @Role, PasswordHash = @PasswordHash
                                     WHERE UserID = @UserId";
                        }
                        
                        using (var cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@UserId", _editUserId.Value);
                            cmd.Parameters.AddWithValue("@FirstName", firstName);
                            cmd.Parameters.AddWithValue("@LastName", lastName);
                            cmd.Parameters.AddWithValue("@Role", role);
                            
                            if (!string.IsNullOrWhiteSpace(password) && password.Length >= 6)
                            {
                                string passwordHash = PasswordHasher.HashPassword(password);
                                cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                            }
                            
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                // Save permissions
                                SaveUserPermissions(conn, _editUserId.Value);
                                
                                AuditLogger.LogDataModification("User Updated",
                                    $"UserID: {_editUserId.Value}, Email: {email}, Role: {role}",
                                    "Success");
                                MessageBox.Show("User updated successfully!", "Success",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.DialogResult = DialogResult.OK;
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Failed to update user.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                else
                {
                    // Create new user
                    if (EmailExists(email))
                    {
                        MessageBox.Show("This email is already registered. Please use a different email.",
                            "Email Exists", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtEmail.Focus();
                        return;
                    }
                    string passwordHash = PasswordHasher.HashPassword(password);
                    using (var conn = _dbContext.GetConnection())
                    {
                        conn.Open();
                        string query = @"INSERT INTO Users (Email, PasswordHash, FirstName, LastName, Role, CreatedDate)
                                        VALUES (@Email, @PasswordHash, @FirstName, @LastName, @Role, NOW())";
                        using (var cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Email", email);
                            cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                            cmd.Parameters.AddWithValue("@FirstName", firstName);
                            cmd.Parameters.AddWithValue("@LastName", lastName);
                            cmd.Parameters.AddWithValue("@Role", role);
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                // Get the newly created user ID
                                int newUserId = (int)cmd.LastInsertedId;
                                
                                // Save permissions using the same connection
                                SaveUserPermissions(conn, newUserId);
                                
                                AuditLogger.LogDataModification("User Created",
                                    $"Email: {email}, Role: {role}",
                                    "Success");
                                MessageBox.Show("User created successfully!", "Success",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.DialogResult = DialogResult.OK;
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Failed to create user.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error {( _isEditMode ? "updating" : "creating")} user: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"AddUser error: {ex}");
            }
        }
        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Please enter First Name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFirstName.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Please enter Last Name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLastName.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Please enter Email.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }
            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }
            // Password validation - only required for new users
            if (!_isEditMode)
            {
                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Please enter Password.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return false;
                }
                if (txtPassword.Text.Length < 6)
                {
                    MessageBox.Show("Password must be at least 6 characters long.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return false;
                }
                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    MessageBox.Show("Passwords do not match. Please confirm your password.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtConfirmPassword.Focus();
                    return false;
                }
            }
            else
            {
                // In edit mode, if password is provided, validate it
                if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    if (txtPassword.Text.Length < 6)
                    {
                        MessageBox.Show("Password must be at least 6 characters long.", "Validation Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtPassword.Focus();
                        return false;
                    }
                    if (txtPassword.Text != txtConfirmPassword.Text)
                    {
                        MessageBox.Show("Passwords do not match. Please confirm your password.", "Validation Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtConfirmPassword.Focus();
                        return false;
                    }
                }
            }
            if (cmbRole.SelectedItem == null)
            {
                MessageBox.Show("Please select a Role.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbRole.Focus();
                return false;
            }
            return true;
        }
        private bool EmailExists(string email)
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        object result = cmd.ExecuteScalar();
                        return result != null && Convert.ToInt32(result) > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
        private void LoadUserPermissions(MySqlConnection conn, int userId)
        {
            try
            {
                // Check if UserPermissions table exists
                string checkTableQuery = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES
                                          WHERE TABLE_SCHEMA = DATABASE()
                                          AND TABLE_NAME = 'UserPermissions'";
                using (MySqlCommand checkCmd = new MySqlCommand(checkTableQuery, conn))
                {
                    int tableExists = Convert.ToInt32(checkCmd.ExecuteScalar());
                    if (tableExists == 0)
                    {
                        // Table doesn't exist, use default permissions based on role
                        SetDefaultPermissions();
                        return;
                    }
                }
                
                // Load permissions from database
                string query = @"SELECT PermissionName, IsGranted FROM UserPermissions WHERE UserID = @UserID";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        var permissions = new Dictionary<string, bool>();
                        while (reader.Read())
                        {
                            string permissionName = reader["PermissionName"]?.ToString() ?? "";
                            bool isGranted = reader["IsGranted"] != DBNull.Value && Convert.ToBoolean(reader["IsGranted"]);
                            permissions[permissionName] = isGranted;
                        }
                        
                        // Apply loaded permissions
                        chkSystemConfiguration.Checked = permissions.GetValueOrDefault("SystemConfiguration", true);
                        chkUserManagement.Checked = permissions.GetValueOrDefault("UserManagement", true);
                        chkMemberManagement.Checked = permissions.GetValueOrDefault("MemberManagement", true);
                        chkCatalogManagement.Checked = permissions.GetValueOrDefault("CatalogManagement", true);
                        chkCirculation.Checked = permissions.GetValueOrDefault("Circulation", true);
                        chkReservations.Checked = permissions.GetValueOrDefault("Reservations", true);
                        chkFineManagement.Checked = permissions.GetValueOrDefault("FineManagement", true);
                        chkInventory.Checked = permissions.GetValueOrDefault("Inventory", true);
                        chkReports.Checked = permissions.GetValueOrDefault("Reports", true);
                        chkSearch.Checked = permissions.GetValueOrDefault("Search", true);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading permissions: {ex.Message}");
                // On error, use default permissions
                SetDefaultPermissions();
            }
        }
        
        private void SaveUserPermissions(MySqlConnection conn, int userId)
        {
            try
            {
                // Check if UserPermissions table exists, create if not
                string checkTableQuery = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES
                                          WHERE TABLE_SCHEMA = DATABASE()
                                          AND TABLE_NAME = 'UserPermissions'";
                using (MySqlCommand checkCmd = new MySqlCommand(checkTableQuery, conn))
                {
                    int tableExists = Convert.ToInt32(checkCmd.ExecuteScalar());
                    if (tableExists == 0)
                    {
                        // Create table
                        string createTableQuery = @"CREATE TABLE IF NOT EXISTS UserPermissions (
                                                    PermissionID INT AUTO_INCREMENT PRIMARY KEY,
                                                    UserID INT NOT NULL,
                                                    PermissionName VARCHAR(100) NOT NULL,
                                                    IsGranted BOOLEAN DEFAULT FALSE,
                                                    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
                                                    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE,
                                                    UNIQUE KEY unique_user_permission (UserID, PermissionName),
                                                    INDEX idx_userpermissions_userid (UserID)
                                                  )";
                        using (MySqlCommand createCmd = new MySqlCommand(createTableQuery, conn))
                        {
                            createCmd.ExecuteNonQuery();
                        }
                    }
                }
                
                // Delete existing permissions for this user
                string deleteQuery = "DELETE FROM UserPermissions WHERE UserID = @UserID";
                using (MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, conn))
                {
                    deleteCmd.Parameters.AddWithValue("@UserID", userId);
                    deleteCmd.ExecuteNonQuery();
                }
                
                // Insert new permissions
                var permissions = new Dictionary<string, bool>
                {
                    { "SystemConfiguration", chkSystemConfiguration.Checked },
                    { "UserManagement", chkUserManagement.Checked },
                    { "MemberManagement", chkMemberManagement.Checked },
                    { "CatalogManagement", chkCatalogManagement.Checked },
                    { "Circulation", chkCirculation.Checked },
                    { "Reservations", chkReservations.Checked },
                    { "FineManagement", chkFineManagement.Checked },
                    { "Inventory", chkInventory.Checked },
                    { "Reports", chkReports.Checked },
                    { "Search", chkSearch.Checked }
                };
                
                string insertQuery = @"INSERT INTO UserPermissions (UserID, PermissionName, IsGranted)
                                      VALUES (@UserID, @PermissionName, @IsGranted)
                                      ON DUPLICATE KEY UPDATE IsGranted = @IsGranted";
                
                foreach (var permission in permissions)
                {
                    using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@UserID", userId);
                        insertCmd.Parameters.AddWithValue("@PermissionName", permission.Key);
                        insertCmd.Parameters.AddWithValue("@IsGranted", permission.Value);
                        insertCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving permissions: {ex.Message}");
                // Don't throw - permissions are optional
            }
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}