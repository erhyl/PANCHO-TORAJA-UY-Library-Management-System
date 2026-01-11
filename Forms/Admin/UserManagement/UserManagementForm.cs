using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Project5LMS.Helpers;
using Project5LMS.Models;
using Project5LMS.Services;
using Project5LMS.Data;
using Project5LMS.Interfaces;
namespace Project5LMS.Forms.Admin.UserManagement
{
    public partial class UserManagementForm : Form
    {
        private List<User> allUsers = new List<User>();
        private List<User> filteredUsers = new List<User>();
        private readonly IUserService _userService;
        public UserManagementForm()
        {
            InitializeComponent();
            try
            {
                AccessControlHelper.RequireRole("Admin");
                AuditLogger.LogAccessControl("UserManagementForm accessed", $"User: {CurrentUser.Email}", "Success");
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                AuditLogger.LogAccessControl("UserManagementForm access denied", $"User: {CurrentUser.Email}", "Failed");
                this.Close();
                return;
            }
            _userService = ServiceFactory.CreateUserService();
            // Ensure form works when loaded in a panel
            this.ResizeRedraw = true;
        }
        private void UserManagementForm_Load(object sender, EventArgs e)
        {
            cmbRoleFilter.SelectedIndex = 0;
            this.Resize += UserManagementForm_Resize;
            // Delay loading to ensure form is properly sized
            this.Shown += UserManagementForm_Shown;
        }
        
        private void UserManagementForm_Shown(object sender, EventArgs e)
        {
            // Ensure form is properly sized before loading data
            if (this.Parent != null)
            {
                // Form is embedded in a panel, ensure it fills the panel
                this.Dock = DockStyle.Fill;
                // Adjust panelMainContainer to fit the parent
                if (this.Parent is Panel parentPanel)
                {
                    this.panelMainContainer.Dock = DockStyle.Fill;
                    // Update the form size to match parent
                    this.Size = parentPanel.Size;
                    // Force layout update
                    this.panelMainContainer.PerformLayout();
                    this.PerformLayout();
                }
            }
            LoadMetrics();
            LoadUsers();
        }
        
        private void UserManagementForm_Resize(object sender, EventArgs e)
        {
            // Re-render cards when form resizes to adjust card widths
            if (filteredUsers != null && filteredUsers.Count > 0 && panelUsersContainer.Width > 0)
            {
                // Use a small delay to ensure resize is complete
                System.Windows.Forms.Timer resizeTimer = new System.Windows.Forms.Timer();
                resizeTimer.Interval = 100;
                resizeTimer.Tick += (s, args) =>
                {
                    resizeTimer.Stop();
                    resizeTimer.Dispose();
                    if (panelUsersContainer.Width > 0)
                    {
                        RenderUserCards();
                    }
                };
                resizeTimer.Start();
            }
        }
        private void DrawMetricIcon(Graphics g, Panel panel, string icon)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using (Font font = new Font("Segoe UI", 20, FontStyle.Bold))
            using (Brush brush = new SolidBrush(Color.White))
            {
                SizeF textSize = g.MeasureString(icon, font);
                float x = (panel.Width - textSize.Width) / 2;
                float y = (panel.Height - textSize.Height) / 2;
                g.DrawString(icon, font, brush, x, y);
            }
        }
        private void LoadMetrics()
        {
            try
            {
                using (var conn = ServiceFactory.GetDbContext().GetConnection())
                {
                    conn.Open();
                    string queryTotal = "SELECT COUNT(*) FROM Users";
                    using (MySqlCommand cmd = new MySqlCommand(queryTotal, conn))
                    {
                        int total = Convert.ToInt32(cmd.ExecuteScalar());
                        lblMetricTotalUsersValue.Text = total.ToString();
                    }
                    string queryActive = "SELECT COUNT(*) FROM Users WHERE Role IS NOT NULL";
                    using (MySqlCommand cmd = new MySqlCommand(queryActive, conn))
                    {
                        int active = Convert.ToInt32(cmd.ExecuteScalar());
                        lblMetricActiveUsersValue.Text = active.ToString();
                    }
                    string queryAdmin = "SELECT COUNT(*) FROM Users WHERE Role = 'Admin'";
                    using (MySqlCommand cmd = new MySqlCommand(queryAdmin, conn))
                    {
                        int admin = Convert.ToInt32(cmd.ExecuteScalar());
                        lblMetricAdministratorsValue.Text = admin.ToString();
                    }
                    try
                    {
                        string querySuspended = "SELECT COUNT(*) FROM Users WHERE Status = 'Suspended'";
                        using (MySqlCommand cmd = new MySqlCommand(querySuspended, conn))
                        {
                            int suspended = Convert.ToInt32(cmd.ExecuteScalar());
                            lblMetricSuspendedValue.Text = suspended.ToString();
                        }
                    }
                    catch
                    {
                        lblMetricSuspendedValue.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading metrics: {ex.Message}");
            }
        }
        private void LoadUsers()
        {
            try
            {
                allUsers.Clear();
                panelUsersContainer.Controls.Clear();
                var dbContext = ServiceFactory.GetDbContext();
                string query = @"SELECT UserID, Email, FirstName, LastName, Role
                                FROM Users
                                ORDER BY FirstName, LastName";
                var result = dbContext.ExecuteQuery(query);
                foreach (DataRow row in result.Rows)
                {
                    var user = new User
                    {
                        UserID = Convert.ToInt32(row["UserID"]),
                        Email = row["Email"]?.ToString() ?? "",
                        FirstName = row["FirstName"]?.ToString() ?? "",
                        LastName = row["LastName"]?.ToString() ?? "",
                        Role = row["Role"]?.ToString() ?? ""
                    };
                    allUsers.Add(user);
                }
                filteredUsers = allUsers.ToList();
                RenderUserCards();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading users: {ex.Message}");
                MessageBox.Show($"Error loading users: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void RenderUserCards()
        {
            panelUsersContainer.Controls.Clear();
            foreach (var user in filteredUsers)
            {
                Panel userCard = CreateUserCard(user);
                panelUsersContainer.Controls.Add(userCard);
            }
        }
        private Panel CreateUserCard(User user)
        {
            // Calculate card width based on container width
            // Aim for 2 cards per row with margins, minimum 400px width
            int containerWidth = panelUsersContainer.Width > 0 
                ? panelUsersContainer.Width - panelUsersContainer.Padding.Left - panelUsersContainer.Padding.Right
                : 1200; // Default width if container not initialized
            int cardWidth = Math.Max(400, (containerWidth - 48) / 2); // 48 = margins (16*3)
            
            Panel card = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(cardWidth, 280),
                Margin = new Padding(0, 0, 16, 16),
                Padding = new Padding(20, 20, 20, 20)
            };
            Panel avatarPanel = new Panel
            {
                BackColor = GetRoleColor(user.Role),
                Size = new Size(60, 60),
                Location = new Point(20, 20)
            };
            avatarPanel.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (Brush brush = new SolidBrush(avatarPanel.BackColor))
                {
                    e.Graphics.FillEllipse(brush, 0, 0, avatarPanel.Width, avatarPanel.Height);
                }
                string initials = GetInitials(user.FirstName, user.LastName);
                using (Font font = new Font("Segoe UI", 18, FontStyle.Bold))
                using (Brush brush = new SolidBrush(Color.White))
                {
                    SizeF textSize = e.Graphics.MeasureString(initials, font);
                    float x = (avatarPanel.Width - textSize.Width) / 2;
                    float y = (avatarPanel.Height - textSize.Height) / 2;
                    e.Graphics.DrawString(initials, font, brush, x, y);
                }
            };
            card.Controls.Add(avatarPanel);
            Label lblName = new Label
            {
                Text = $"{user.FirstName} {user.LastName}".Trim(),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                Location = new Point(95, 20),
                AutoSize = true
            };
            card.Controls.Add(lblName);
            Label lblEmail = new Label
            {
                Text = user.Email,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(128, 128, 128),
                Location = new Point(95, 50),
                AutoSize = true
            };
            card.Controls.Add(lblEmail);
            int tagX = 95;
            int tagY = 80;
            Label lblRole = new Label
            {
                Text = user.Role ?? "Member",
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = GetRoleTextColor(user.Role),
                BackColor = GetRoleTagColor(user.Role),
                AutoSize = true,
                Padding = new Padding(8, 4, 8, 4),
                Location = new Point(tagX, tagY)
            };
            card.Controls.Add(lblRole);
            tagX += lblRole.Width + 8;
            string status = "Active";
            Label lblStatus = new Label
            {
                Text = status,
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = Color.FromArgb(40, 167, 69),
                BackColor = Color.FromArgb(212, 237, 218),
                AutoSize = true,
                Padding = new Padding(8, 4, 8, 4),
                Location = new Point(tagX, tagY)
            };
            card.Controls.Add(lblStatus);
            int detailY = 120;
            int detailSpacing = 25;
            Label lblUserIdLabel = new Label
            {
                Text = "User ID:",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(128, 128, 128),
                Location = new Point(20, detailY),
                AutoSize = true
            };
            card.Controls.Add(lblUserIdLabel);
            Label lblUserId = new Label
            {
                Text = $"USR-{user.UserID:D3}",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                Location = new Point(100, detailY),
                AutoSize = true
            };
            card.Controls.Add(lblUserId);
            Label lblCreatedLabel = new Label
            {
                Text = "Created:",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(128, 128, 128),
                Location = new Point(20, detailY + detailSpacing),
                AutoSize = true
            };
            card.Controls.Add(lblCreatedLabel);
            string createdDate = GetUserCreatedDate(user.UserID);
            Label lblCreated = new Label
            {
                Text = createdDate,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                Location = new Point(100, detailY + detailSpacing),
                AutoSize = true
            };
            card.Controls.Add(lblCreated);
            Label lblLastLoginLabel = new Label
            {
                Text = "Last Login:",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(128, 128, 128),
                Location = new Point(20, detailY + detailSpacing * 2),
                AutoSize = true
            };
            card.Controls.Add(lblLastLoginLabel);
            string lastLogin = GetUserLastLogin(user.UserID);
            Label lblLastLogin = new Label
            {
                Text = lastLogin,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                Location = new Point(100, detailY + detailSpacing * 2),
                AutoSize = true
            };
            card.Controls.Add(lblLastLogin);
            string[] permissions = GetPermissionsArray(user.Role);
            int permX = 20;
            int permY = detailY + detailSpacing * 3;
            foreach (string perm in permissions)
            {
                Label lblPerm = new Label
                {
                    Text = perm,
                    Font = new Font("Segoe UI", 8),
                    ForeColor = Color.FromArgb(128, 128, 128),
                    BackColor = Color.FromArgb(240, 240, 240),
                    AutoSize = true,
                    Padding = new Padding(6, 3, 6, 3),
                    Location = new Point(permX, permY)
                };
                card.Controls.Add(lblPerm);
                permX += lblPerm.Width + 6;
                if (permX > 500)
                {
                    permX = 20;
                    permY += 25;
                }
            }
            int buttonY = 240;
            Button btnResetPassword = new Button
            {
                Text = "Reset Password",
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = Color.FromArgb(13, 110, 253),
                BackColor = Color.FromArgb(207, 226, 255),
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                Size = new Size(140, 30),
                Location = new Point(20, buttonY),
                Cursor = Cursors.Hand
            };
            btnResetPassword.Click += (s, e) => ResetPassword(user);
            card.Controls.Add(btnResetPassword);
            Button btnSuspend = new Button
            {
                Text = "Suspend",
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = Color.FromArgb(220, 53, 69),
                BackColor = Color.FromArgb(248, 215, 218),
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                Size = new Size(100, 30),
                Location = new Point(170, buttonY),
                Cursor = Cursors.Hand
            };
            btnSuspend.Click += (s, e) => SuspendUser(user);
            card.Controls.Add(btnSuspend);
            // Calculate edit/delete button position relative to card width (accounting for padding)
            int rightEdgeX = cardWidth - card.Padding.Right - 30; // 30 = button width
            Button btnEdit = new Button
            {
                Text = "✏️",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.FromArgb(13, 110, 253),
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                Size = new Size(30, 30),
                Location = new Point(rightEdgeX, 20),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Cursor = Cursors.Hand
            };
            btnEdit.Click += (s, e) => EditUser(user);
            card.Controls.Add(btnEdit);
            Button btnDelete = new Button
            {
                Text = "👁️",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.FromArgb(220, 53, 69),
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                Size = new Size(30, 30),
                Location = new Point(rightEdgeX, 50),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Cursor = Cursors.Hand
            };
            btnDelete.Click += (s, e) => DeleteUser(user);
            card.Controls.Add(btnDelete);
            return card;
        }
        private string GetInitials(string firstName, string lastName)
        {
            string first = !string.IsNullOrWhiteSpace(firstName) ? firstName[0].ToString().ToUpper() : "";
            string last = !string.IsNullOrWhiteSpace(lastName) ? lastName[0].ToString().ToUpper() : "";
            return first + last;
        }
        private Color GetRoleColor(string role)
        {
            switch (role?.ToLower())
            {
                case "admin":
                    return Color.FromArgb(138, 43, 226);
                case "librarystaff":
                    return Color.FromArgb(13, 110, 253);
                default:
                    return Color.FromArgb(13, 110, 253);
            }
        }
        private Color GetRoleTagColor(string role)
        {
            switch (role?.ToLower())
            {
                case "admin":
                    return Color.FromArgb(138, 43, 226);
                case "librarystaff":
                    return Color.FromArgb(207, 226, 255);
                default:
                    return Color.FromArgb(207, 226, 255);
            }
        }
        private Color GetRoleTextColor(string role)
        {
            switch (role?.ToLower())
            {
                case "admin":
                    return Color.White;
                case "librarystaff":
                    return Color.FromArgb(13, 110, 253);
                default:
                    return Color.FromArgb(13, 110, 253);
            }
        }
        private string[] GetPermissionsArray(string role)
        {
            switch (role?.ToLower())
            {
                case "admin":
                    return new string[] { "All Permissions", "System Configuration", "User Management", "Reports" };
                case "librarystaff":
                    return new string[] { "Circulation", "Member Management", "Catalog", "Reservations", "Fines" };
                default:
                    return new string[] { "Basic Access" };
            }
        }
        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search users...")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }
        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Search users...";
                txtSearch.ForeColor = Color.Gray;
            }
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            FilterUsers();
        }
        private void cmbRoleFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterUsers();
        }
        private void FilterUsers()
        {
            string searchText = txtSearch.Text.ToLower();
            if (searchText == "search users...")
                searchText = "";
            string selectedRole = cmbRoleFilter.SelectedItem?.ToString();
            if (selectedRole == "All Roles")
                selectedRole = null;
            filteredUsers = allUsers.Where(u =>
                (string.IsNullOrEmpty(searchText) ||
                 u.FirstName.ToLower().Contains(searchText) ||
                 u.LastName.ToLower().Contains(searchText) ||
                 u.Email.ToLower().Contains(searchText)) &&
                (selectedRole == null || u.Role == selectedRole)
            ).ToList();
            RenderUserCards();
        }
        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            try
            {
                using (AddUserForm addUserForm = new AddUserForm())
                {
                    if (addUserForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadUsers();
                        LoadMetrics();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening add user form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void EditUser(User user)
        {
            try
            {
                using (AddUserForm editUserForm = new AddUserForm(user.UserID))
                {
                    if (editUserForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadUsers();
                        LoadMetrics();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening edit user form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DeleteUser(User user)
        {
            var result = MessageBox.Show(
                $"Are you sure you want to delete user {user.FirstName} {user.LastName}?\n\nThis action cannot be undone.",
                "Delete User",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                try
                {
                    var dbContext = ServiceFactory.GetDbContext();
                    using (var conn = dbContext.GetConnection())
                    {
                        conn.Open();
                        string query = "DELETE FROM Users WHERE UserID = @userId";
                        using (var cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@userId", user.UserID);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    AuditLogger.LogDataModification("User Deleted",
                        $"UserID: {user.UserID}, Email: {user.Email}, Role: {user.Role}",
                        "Success");
                    MessageBox.Show("User deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadUsers();
                    LoadMetrics();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void ResetPassword(User user)
        {
            var result = MessageBox.Show(
                $"Reset password for {user.FirstName} {user.LastName}?",
                "Reset Password",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                MessageBox.Show("Password reset functionality - to be implemented", "Reset Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void SuspendUser(User user)
        {
            var result = MessageBox.Show(
                $"Suspend user {user.FirstName} {user.LastName}?",
                "Suspend User",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    var dbContext = ServiceFactory.GetDbContext();
                    using (var conn = dbContext.GetConnection())
                    {
                        conn.Open();
                        try
                        {
                            string query = "UPDATE Users SET Status = 'Suspended' WHERE UserID = @userId";
                            using (var cmd = new MySqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@userId", user.UserID);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Status column not found in Users table. Please add a Status column to enable this feature.",
                                "Feature Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    MessageBox.Show("User suspended successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadUsers();
                    LoadMetrics();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error suspending user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private string GetUserCreatedDate(int userId)
        {
            try
            {
                var dbContext = ServiceFactory.GetDbContext();
                using (var conn = dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT CreatedDate FROM Users WHERE UserID = @userId";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            DateTime date = Convert.ToDateTime(result);
                            return date.ToString("yyyy-MM-dd");
                        }
                    }
                }
            }
            catch { }
            return DateTime.Now.ToString("yyyy-MM-dd");
        }
        private string GetUserLastLogin(int userId)
        {
            try
            {
                var dbContext = ServiceFactory.GetDbContext();
                using (var conn = dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT LastLoginDate FROM Users WHERE UserID = @userId";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            DateTime date = Convert.ToDateTime(result);
                            return date.ToString("yyyy-MM-dd hh:mm tt");
                        }
                    }
                }
            }
            catch { }
            return "Never";
        }


    }
}