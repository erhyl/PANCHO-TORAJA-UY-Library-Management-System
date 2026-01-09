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
        private Dictionary<int, UserData> userDataCache = new Dictionary<int, UserData>();

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
        }

        private void UserManagementForm_Load(object sender, EventArgs e)
        {
            // Remove sample cards at runtime
            if (sampleCard1 != null && sampleCard1.Parent != null)
            {
                panelUsersContainer.Controls.Remove(sampleCard1);
                sampleCard1.Dispose();
            }
            if (sampleCard2 != null && sampleCard2.Parent != null)
            {
                panelUsersContainer.Controls.Remove(sampleCard2);
                sampleCard2.Dispose();
            }
            
            cmbRoleFilter.SelectedIndex = 0;
            LoadMetrics();
            LoadUsers();
        }
        
        private void SampleCard_Paint(object sender, PaintEventArgs e)
        {
            Panel card = sender as Panel;
            if (card == null) return;
            
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            
            // Draw rounded border
            Rectangle rect = card.ClientRectangle;
            rect.Width -= 1;
            rect.Height -= 1;
            int radius = 8;
            
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
                path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
                path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
                path.CloseAllFigures();
                
                using (Pen pen = new Pen(Color.FromArgb(240, 240, 240), 1))
                {
                    g.DrawPath(pen, path);
                }
            }
            
            // Draw sample avatar (square, purple for first card, blue for second)
            Color avatarColor = card.Name == "sampleCard1" 
                ? Color.FromArgb(138, 43, 226) 
                : Color.FromArgb(13, 110, 253);
            
            using (Brush brush = new SolidBrush(avatarColor))
            {
                g.FillRectangle(brush, 20, 20, 60, 60);
            }
            
            // Draw initials
            string initials = card.Name == "sampleCard1" ? "AD" : "JW";
            using (Font font = new Font("Segoe UI", 18, FontStyle.Bold))
            using (Brush brush = new SolidBrush(Color.White))
            {
                SizeF textSize = g.MeasureString(initials, font);
                float x = 20 + (60 - textSize.Width) / 2;
                float y = 20 + (60 - textSize.Height) / 2;
                g.DrawString(initials, font, brush, x, y);
            }
            
            // Draw name
            string name = card.Name == "sampleCard1" ? "Adam Doe" : "Jane Williams";
            using (Font font = new Font("Segoe UI", 14, FontStyle.Bold))
            using (Brush brush = new SolidBrush(Color.FromArgb(33, 37, 41)))
            {
                g.DrawString(name, font, brush, 92, 20);
            }
            
            // Draw email
            string email = card.Name == "sampleCard1" 
                ? "adamdoe@admin.umindanao.edu.ph" 
                : "jane.williams@library.com";
            using (Font font = new Font("Segoe UI", 9.5F))
            using (Brush brush = new SolidBrush(Color.FromArgb(108, 117, 125)))
            {
                g.DrawString(email, font, brush, 92, 42);
            }
            
            // Draw edit and delete icons (top-right) - Made more visible
            int iconSize = 36;
            int iconX = card.Width - iconSize - 20;
            int editY = 16;
            int deleteY = 56;
            
            // Edit button background (light blue)
            RectangleF editRect = new RectangleF(iconX, editY, iconSize, iconSize);
            using (GraphicsPath editPath = new GraphicsPath())
            {
                int editBtnRadius = 6;
                editPath.AddArc(editRect.X, editRect.Y, editBtnRadius, editBtnRadius, 180, 90);
                editPath.AddArc(editRect.X + editRect.Width - editBtnRadius, editRect.Y, editBtnRadius, editBtnRadius, 270, 90);
                editPath.AddArc(editRect.X + editRect.Width - editBtnRadius, editRect.Y + editRect.Height - editBtnRadius, editBtnRadius, editBtnRadius, 0, 90);
                editPath.AddArc(editRect.X, editRect.Y + editRect.Height - editBtnRadius, editBtnRadius, editBtnRadius, 90, 90);
                editPath.CloseAllFigures();
                
                using (Brush brush = new SolidBrush(Color.FromArgb(240, 248, 255)))
                {
                    g.FillPath(brush, editPath);
                }
                using (Pen pen = new Pen(Color.FromArgb(207, 226, 255), 1))
                {
                    g.DrawPath(pen, editPath);
                }
            }
            
            // Edit icon (✏️)
            using (Font iconFont = new Font("Segoe UI", 16, FontStyle.Regular))
            using (Brush brush = new SolidBrush(Color.FromArgb(13, 110, 253)))
            {
                SizeF textSize = g.MeasureString("✏️", iconFont);
                float x = iconX + (iconSize - textSize.Width) / 2;
                float y = editY + (iconSize - textSize.Height) / 2;
                g.DrawString("✏️", iconFont, brush, x, y);
            }
            
            // Delete button background (light red)
            RectangleF deleteRect = new RectangleF(iconX, deleteY, iconSize, iconSize);
            using (GraphicsPath deletePath = new GraphicsPath())
            {
                int deleteBtnRadius = 6;
                deletePath.AddArc(deleteRect.X, deleteRect.Y, deleteBtnRadius, deleteBtnRadius, 180, 90);
                deletePath.AddArc(deleteRect.X + deleteRect.Width - deleteBtnRadius, deleteRect.Y, deleteBtnRadius, deleteBtnRadius, 270, 90);
                deletePath.AddArc(deleteRect.X + deleteRect.Width - deleteBtnRadius, deleteRect.Y + deleteRect.Height - deleteBtnRadius, deleteBtnRadius, deleteBtnRadius, 0, 90);
                deletePath.AddArc(deleteRect.X, deleteRect.Y + deleteRect.Height - deleteBtnRadius, deleteBtnRadius, deleteBtnRadius, 90, 90);
                deletePath.CloseAllFigures();
                
                using (Brush brush = new SolidBrush(Color.FromArgb(255, 240, 240)))
                {
                    g.FillPath(brush, deletePath);
                }
                using (Pen pen = new Pen(Color.FromArgb(248, 215, 218), 1))
                {
                    g.DrawPath(pen, deletePath);
                }
            }
            
            // Delete icon (🗑️)
            using (Font iconFont = new Font("Segoe UI", 16, FontStyle.Regular))
            using (Brush brush = new SolidBrush(Color.FromArgb(220, 53, 69)))
            {
                SizeF textSize = g.MeasureString("🗑️", iconFont);
                float x = iconX + (iconSize - textSize.Width) / 2;
                float y = deleteY + (iconSize - textSize.Height) / 2;
                g.DrawString("🗑️", iconFont, brush, x, y);
            }
            
            // Draw role tag
            string role = card.Name == "sampleCard1" ? "Admin" : "LibraryStaff";
            Color roleBg = card.Name == "sampleCard1" 
                ? Color.FromArgb(138, 43, 226) 
                : Color.FromArgb(207, 226, 255);
            Color roleText = card.Name == "sampleCard1" 
                ? Color.White 
                : Color.FromArgb(13, 110, 253);
            
            SizeF roleSize = g.MeasureString(role, new Font("Segoe UI", 9F));
            RectangleF roleRect = new RectangleF(92, 70, roleSize.Width + 24, roleSize.Height + 12);
            using (GraphicsPath rolePath = new GraphicsPath())
            {
                int rRadius = 4;
                rolePath.AddArc(roleRect.X, roleRect.Y, rRadius, rRadius, 180, 90);
                rolePath.AddArc(roleRect.X + roleRect.Width - rRadius, roleRect.Y, rRadius, rRadius, 270, 90);
                rolePath.AddArc(roleRect.X + roleRect.Width - rRadius, roleRect.Y + roleRect.Height - rRadius, rRadius, rRadius, 0, 90);
                rolePath.AddArc(roleRect.X, roleRect.Y + roleRect.Height - rRadius, rRadius, rRadius, 90, 90);
                rolePath.CloseAllFigures();
                
                using (Brush brush = new SolidBrush(roleBg))
                {
                    g.FillPath(brush, rolePath);
                }
                
                using (Font font = new Font("Segoe UI", 9F))
                using (Brush brush = new SolidBrush(roleText))
                {
                    StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                    g.DrawString(role, font, brush, roleRect, sf);
                }
            }
            
            // Draw status tag
            string status = "Active";
            RectangleF statusRect = new RectangleF(92 + roleSize.Width + 32, 70, 60, roleSize.Height + 12);
            using (GraphicsPath statusPath = new GraphicsPath())
            {
                int sRadius = 4;
                statusPath.AddArc(statusRect.X, statusRect.Y, sRadius, sRadius, 180, 90);
                statusPath.AddArc(statusRect.X + statusRect.Width - sRadius, statusRect.Y, sRadius, sRadius, 270, 90);
                statusPath.AddArc(statusRect.X + statusRect.Width - sRadius, statusRect.Y + statusRect.Height - sRadius, sRadius, sRadius, 0, 90);
                statusPath.AddArc(statusRect.X, statusRect.Y + statusRect.Height - sRadius, sRadius, sRadius, 90, 90);
                statusPath.CloseAllFigures();
                
                using (Brush brush = new SolidBrush(Color.FromArgb(212, 237, 218)))
                {
                    g.FillPath(brush, statusPath);
                }
                
                using (Font font = new Font("Segoe UI", 9F))
                using (Brush brush = new SolidBrush(Color.FromArgb(40, 167, 69)))
                {
                    StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                    g.DrawString(status, font, brush, statusRect, sf);
                }
            }
            
            // Draw user details
            using (Font labelFont = new Font("Segoe UI", 9F))
            using (Font valueFont = new Font("Segoe UI", 9F, FontStyle.Bold))
            using (Brush labelBrush = new SolidBrush(Color.FromArgb(108, 117, 125)))
            using (Brush valueBrush = new SolidBrush(Color.FromArgb(33, 37, 41)))
            {
                int yPos = 105;
                g.DrawString("User ID:", labelFont, labelBrush, 20, yPos);
                g.DrawString("USR-001", valueFont, valueBrush, 95, yPos);
                
                yPos += 20;
                g.DrawString("Created:", labelFont, labelBrush, 20, yPos);
                g.DrawString("2026-01-09", valueFont, valueBrush, 95, yPos);
                
                yPos += 20;
                g.DrawString("Last Login:", labelFont, labelBrush, 20, yPos);
                g.DrawString("Never", valueFont, valueBrush, 95, yPos);
            }
            
            // Draw permissions
            string[] permissions = card.Name == "sampleCard1" 
                ? new[] { "All Permissions", "System Configuration", "User Management", "Reports" }
                : new[] { "Circulation", "Member Management", "Catalog", "Reservations", "Fines" };
            
            using (Font permFont = new Font("Segoe UI", 8F))
            using (Brush permBrush = new SolidBrush(Color.FromArgb(108, 117, 125)))
            {
                float permX = 20;
                float permY = 173;
                foreach (string perm in permissions)
                {
                    SizeF permSize = g.MeasureString(perm, permFont);
                    RectangleF permRect = new RectangleF(permX, permY, permSize.Width + 20, permSize.Height + 10);
                    
                    using (GraphicsPath permPath = new GraphicsPath())
                    {
                        int pRadius = 4;
                        permPath.AddArc(permRect.X, permRect.Y, pRadius, pRadius, 180, 90);
                        permPath.AddArc(permRect.X + permRect.Width - pRadius, permRect.Y, pRadius, pRadius, 270, 90);
                        permPath.AddArc(permRect.X + permRect.Width - pRadius, permRect.Y + permRect.Height - pRadius, pRadius, pRadius, 0, 90);
                        permPath.AddArc(permRect.X, permRect.Y + permRect.Height - pRadius, pRadius, pRadius, 90, 90);
                        permPath.CloseAllFigures();
                        
                        using (Brush brush = new SolidBrush(Color.FromArgb(248, 249, 250)))
                        {
                            g.FillPath(brush, permPath);
                        }
                        
                        StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                        g.DrawString(perm, permFont, permBrush, permRect, sf);
                    }
                    
                    permX += permRect.Width + 6;
                    if (permX + 100 > 300)
                    {
                        permX = 20;
                        permY += 30;
                    }
                }
            }
            
            // Draw buttons
            RectangleF resetBtnRect = new RectangleF(20, 320, 140, 32);
            RectangleF suspendBtnRect = new RectangleF(170, 320, 100, 32);
            
            using (GraphicsPath btnPath = new GraphicsPath())
            {
                int bRadius = 4;
                btnPath.AddArc(resetBtnRect.X, resetBtnRect.Y, bRadius, bRadius, 180, 90);
                btnPath.AddArc(resetBtnRect.X + resetBtnRect.Width - bRadius, resetBtnRect.Y, bRadius, bRadius, 270, 90);
                btnPath.AddArc(resetBtnRect.X + resetBtnRect.Width - bRadius, resetBtnRect.Y + resetBtnRect.Height - bRadius, bRadius, bRadius, 0, 90);
                btnPath.AddArc(resetBtnRect.X, resetBtnRect.Y + resetBtnRect.Height - bRadius, bRadius, bRadius, 90, 90);
                btnPath.CloseAllFigures();
                
                using (Brush brush = new SolidBrush(Color.FromArgb(207, 226, 255)))
                {
                    g.FillPath(brush, btnPath);
                }
                
                using (Font font = new Font("Segoe UI", 9F))
                using (Brush brush = new SolidBrush(Color.FromArgb(13, 110, 253)))
                {
                    StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                    g.DrawString("Reset Password", font, brush, resetBtnRect, sf);
                }
            }
            
            using (GraphicsPath btnPath = new GraphicsPath())
            {
                int bRadius = 4;
                btnPath.AddArc(suspendBtnRect.X, suspendBtnRect.Y, bRadius, bRadius, 180, 90);
                btnPath.AddArc(suspendBtnRect.X + suspendBtnRect.Width - bRadius, suspendBtnRect.Y, bRadius, bRadius, 270, 90);
                btnPath.AddArc(suspendBtnRect.X + suspendBtnRect.Width - bRadius, suspendBtnRect.Y + suspendBtnRect.Height - bRadius, bRadius, bRadius, 0, 90);
                btnPath.AddArc(suspendBtnRect.X, suspendBtnRect.Y + suspendBtnRect.Height - bRadius, bRadius, bRadius, 90, 90);
                btnPath.CloseAllFigures();
                
                using (Brush brush = new SolidBrush(Color.FromArgb(248, 215, 218)))
                {
                    g.FillPath(brush, btnPath);
                }
                
                using (Font font = new Font("Segoe UI", 9F))
                using (Brush brush = new SolidBrush(Color.FromArgb(220, 53, 69)))
                {
                    StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                    g.DrawString("Suspend", font, brush, suspendBtnRect, sf);
                }
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
                
                // Try enhanced query first (with optional columns), fall back to basic if it fails
                DataTable result = null;
                string query = @"SELECT UserID, Email, FirstName, LastName, Role, 
                                CreatedDate, LastLoginDate, Status
                                FROM Users 
                                ORDER BY FirstName, LastName";
                
                try
                {
                    result = dbContext.ExecuteQuery(query);
                }
                catch
                {
                    // Fall back to basic query if optional columns don't exist
                    try
                    {
                        query = @"SELECT UserID, Email, FirstName, LastName, Role 
                                 FROM Users 
                                 ORDER BY FirstName, LastName";
                        result = dbContext.ExecuteQuery(query);
                    }
                    catch (Exception basicEx)
                    {
                        System.Diagnostics.Debug.WriteLine($"Basic query failed: {basicEx.Message}");
                        throw new Exception($"Unable to load users. Please verify:\n\n1. Database connection is active\n2. Users table exists\n3. Table has required columns: UserID, Email, FirstName, LastName, Role\n\nDatabase Error: {basicEx.Message}", basicEx);
                    }
                }
                userDataCache.Clear();
                
                foreach (DataRow row in result.Rows)
                {
                    try
                    {
                        int userId = Convert.ToInt32(row["UserID"]);
                        var user = new User
                        {
                            UserID = userId,
                            Email = row["Email"]?.ToString() ?? "",
                            FirstName = row["FirstName"]?.ToString() ?? "",
                            LastName = row["LastName"]?.ToString() ?? "",
                            Role = row["Role"]?.ToString() ?? ""
                        };
                        
                        // Store additional data in cache (handle missing columns gracefully)
                        UserData data = new UserData
                        {
                            CreatedDate = DateTime.Now,
                            LastLoginDate = null,
                            Status = "Active"
                        };
                        
                        // Try to get CreatedDate
                        if (result.Columns.Contains("CreatedDate"))
                        {
                            try
                            {
                                if (row["CreatedDate"] != DBNull.Value)
                                    data.CreatedDate = Convert.ToDateTime(row["CreatedDate"]);
                            }
                            catch { }
                        }
                        
                        // Try to get LastLoginDate
                        if (result.Columns.Contains("LastLoginDate"))
                        {
                            try
                            {
                                if (row["LastLoginDate"] != DBNull.Value)
                                    data.LastLoginDate = Convert.ToDateTime(row["LastLoginDate"]);
                            }
                            catch { }
                        }
                        
                        // Try to get Status
                        if (result.Columns.Contains("Status"))
                        {
                            try
                            {
                                if (row["Status"] != DBNull.Value)
                                    data.Status = row["Status"]?.ToString() ?? "Active";
                            }
                            catch { }
                        }
                        
                        userDataCache[userId] = data;
                        allUsers.Add(user);
                    }
                    catch (Exception rowEx)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error processing user row: {rowEx.Message}");
                        // Continue with next row
                    }
                }

                filteredUsers = allUsers.ToList();
                RenderUserCards();
            }
            catch (MySqlException sqlEx)
            {
                System.Diagnostics.Debug.WriteLine($"MySQL Error loading users: {sqlEx.Message}");
                System.Diagnostics.Debug.WriteLine($"Error Number: {sqlEx.Number}");
                MessageBox.Show($"Database error: {sqlEx.Message}\n\nPlease check your database connection and ensure the Users table exists.", 
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading users: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                MessageBox.Show($"Error loading users: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private class UserData
        {
            public DateTime CreatedDate { get; set; }
            public DateTime? LastLoginDate { get; set; }
            public string Status { get; set; }
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
            // Get user data from cache
            UserData userData = userDataCache.ContainsKey(user.UserID) 
                ? userDataCache[user.UserID] 
                : new UserData { CreatedDate = DateTime.Now, Status = "Active" };

            Panel card = new Panel
            {
                BackColor = Color.White,
                Size = new Size(340, 380),
                Margin = new Padding(15, 15, 15, 15),
                Padding = new Padding(20, 20, 20, 20),
                BorderStyle = BorderStyle.None
            };
            
            // Add rounded corners and border
            card.Paint += (s, e) =>
            {
                Rectangle rect = card.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                int radius = 8;
                
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                    path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
                    path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
                    path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
                    path.CloseAllFigures();
                    card.Region = new Region(path);
                    
                    // Draw subtle border
                    using (Pen pen = new Pen(Color.FromArgb(240, 240, 240), 1))
                    {
                        e.Graphics.DrawPath(pen, path);
                    }
                }
            };

            // Avatar Panel - Square shape
            Panel avatarPanel = new Panel
            {
                BackColor = GetRoleColor(user.Role),
                Size = new Size(60, 60),
                Location = new Point(20, 20)
            };
            avatarPanel.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                // Draw square avatar
                using (Brush brush = new SolidBrush(avatarPanel.BackColor))
                {
                    e.Graphics.FillRectangle(brush, 0, 0, avatarPanel.Width, avatarPanel.Height);
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

            // Name and Email
            Label lblName = new Label
            {
                Text = $"{user.FirstName} {user.LastName}".Trim(),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 37, 41),
                Location = new Point(92, 20),
                AutoSize = true
            };
            card.Controls.Add(lblName);

            Label lblEmail = new Label
            {
                Text = user.Email,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(108, 117, 125),
                Location = new Point(92, 42),
                AutoSize = true
            };
            card.Controls.Add(lblEmail);
            
            // Edit and Delete buttons in top-right - Made more visible
            // Position: card width (340) - icon size (36) - right padding (20) = 284
            int iconX = card.Width - 36 - 20;
            Button btnEdit = new Button
            {
                Text = "✏️",
                Font = new Font("Segoe UI", 16, FontStyle.Regular),
                ForeColor = Color.FromArgb(13, 110, 253),
                BackColor = Color.FromArgb(240, 248, 255),
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { 
                    BorderSize = 1, 
                    BorderColor = Color.FromArgb(207, 226, 255),
                    MouseOverBackColor = Color.FromArgb(220, 238, 255),
                    MouseDownBackColor = Color.FromArgb(200, 228, 255)
                },
                Size = new Size(36, 36),
                Location = new Point(iconX, 16),
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleCenter
            };
            btnEdit.Paint += (s, e) =>
            {
                // Draw rounded corners for edit button
                int editRadius = 6;
                Rectangle rect = btnEdit.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddArc(rect.X, rect.Y, editRadius, editRadius, 180, 90);
                    path.AddArc(rect.X + rect.Width - editRadius, rect.Y, editRadius, editRadius, 270, 90);
                    path.AddArc(rect.X + rect.Width - editRadius, rect.Y + rect.Height - editRadius, editRadius, editRadius, 0, 90);
                    path.AddArc(rect.X, rect.Y + rect.Height - editRadius, editRadius, editRadius, 90, 90);
                    path.CloseAllFigures();
                    btnEdit.Region = new Region(path);
                }
            };
            btnEdit.Click += (s, e) => EditUser(user);
            ToolTip editToolTip = new ToolTip();
            editToolTip.SetToolTip(btnEdit, "Edit User");
            card.Controls.Add(btnEdit);

            Button btnDelete = new Button
            {
                Text = "🗑️",
                Font = new Font("Segoe UI", 16, FontStyle.Regular),
                ForeColor = Color.FromArgb(220, 53, 69),
                BackColor = Color.FromArgb(255, 240, 240),
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { 
                    BorderSize = 1, 
                    BorderColor = Color.FromArgb(248, 215, 218),
                    MouseOverBackColor = Color.FromArgb(255, 220, 220),
                    MouseDownBackColor = Color.FromArgb(255, 200, 200)
                },
                Size = new Size(36, 36),
                Location = new Point(iconX, 56),
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleCenter
            };
            btnDelete.Paint += (s, e) =>
            {
                // Draw rounded corners for delete button
                int deleteRadius = 6;
                Rectangle rect = btnDelete.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddArc(rect.X, rect.Y, deleteRadius, deleteRadius, 180, 90);
                    path.AddArc(rect.X + rect.Width - deleteRadius, rect.Y, deleteRadius, deleteRadius, 270, 90);
                    path.AddArc(rect.X + rect.Width - deleteRadius, rect.Y + rect.Height - deleteRadius, deleteRadius, deleteRadius, 0, 90);
                    path.AddArc(rect.X, rect.Y + rect.Height - deleteRadius, deleteRadius, deleteRadius, 90, 90);
                    path.CloseAllFigures();
                    btnDelete.Region = new Region(path);
                }
            };
            btnDelete.Click += (s, e) => DeleteUser(user);
            ToolTip deleteToolTip = new ToolTip();
            deleteToolTip.SetToolTip(btnDelete, "Delete User");
            card.Controls.Add(btnDelete);

            // Role and Status Tags
            int tagY = 70;
            string displayRole = GetDisplayRole(user.Role);
            
            Label lblRole = new Label
            {
                Text = displayRole,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = GetRoleTextColor(user.Role),
                BackColor = GetRoleTagColor(user.Role),
                AutoSize = false,
                Size = new Size(0, 0),
                Padding = new Padding(12, 6, 12, 6),
                Location = new Point(92, tagY),
                TextAlign = ContentAlignment.MiddleCenter
            };
            lblRole.Size = TextRenderer.MeasureText(displayRole, lblRole.Font) + new Size(24, 12);
            // Make label rounded
            ApplyRoundedCorners(lblRole, 4);
            card.Controls.Add(lblRole);

            int statusX = 92 + lblRole.Width + 8;
            Label lblStatus = new Label
            {
                Text = userData.Status,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = Color.FromArgb(40, 167, 69),
                BackColor = Color.FromArgb(212, 237, 218),
                AutoSize = false,
                Size = new Size(0, 0),
                Padding = new Padding(12, 6, 12, 6),
                Location = new Point(statusX, tagY),
                TextAlign = ContentAlignment.MiddleCenter
            };
            lblStatus.Size = TextRenderer.MeasureText(userData.Status, lblStatus.Font) + new Size(24, 12);
            // Make label rounded
            ApplyRoundedCorners(lblStatus, 4);
            card.Controls.Add(lblStatus);

            // User Details Section
            int detailY = 105;
            int detailSpacing = 20;

            Label lblUserIdLabel = new Label
            {
                Text = "User ID:",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(108, 117, 125),
                Location = new Point(20, detailY),
                AutoSize = true
            };
            card.Controls.Add(lblUserIdLabel);

            Label lblUserId = new Label
            {
                Text = $"USR-{user.UserID:D3}",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 37, 41),
                Location = new Point(95, detailY),
                AutoSize = true
            };
            card.Controls.Add(lblUserId);

            Label lblCreatedLabel = new Label
            {
                Text = "Created:",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(108, 117, 125),
                Location = new Point(20, detailY + detailSpacing),
                AutoSize = true
            };
            card.Controls.Add(lblCreatedLabel);

            string createdDate = userData.CreatedDate.ToString("yyyy-MM-dd");
            Label lblCreated = new Label
            {
                Text = createdDate,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 37, 41),
                Location = new Point(95, detailY + detailSpacing),
                AutoSize = true
            };
            card.Controls.Add(lblCreated);

            Label lblLastLoginLabel = new Label
            {
                Text = "Last Login:",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(108, 117, 125),
                Location = new Point(20, detailY + detailSpacing * 2),
                AutoSize = true
            };
            card.Controls.Add(lblLastLoginLabel);

            string lastLogin = userData.LastLoginDate.HasValue 
                ? userData.LastLoginDate.Value.ToString("yyyy-MM-dd hh:mm tt") 
                : "Never";
            Label lblLastLogin = new Label
            {
                Text = lastLogin,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 37, 41),
                Location = new Point(95, detailY + detailSpacing * 2),
                AutoSize = true
            };
            card.Controls.Add(lblLastLogin);

            // Permissions Section
            string[] permissions = GetPermissionsArray(user.Role);
            int permX = 20;
            int permY = detailY + detailSpacing * 3 + 8;
            int maxWidth = 300;
            
            foreach (string perm in permissions)
            {
                Label lblPerm = new Label
                {
                    Text = perm,
                    Font = new Font("Segoe UI", 8F),
                    ForeColor = Color.FromArgb(108, 117, 125),
                    BackColor = Color.FromArgb(248, 249, 250),
                    AutoSize = false,
                    Size = new Size(0, 0),
                    Padding = new Padding(10, 5, 10, 5),
                    Location = new Point(permX, permY),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                lblPerm.Size = TextRenderer.MeasureText(perm, lblPerm.Font) + new Size(20, 10);
                // Make permission tags rounded
                ApplyRoundedCorners(lblPerm, 4);
                card.Controls.Add(lblPerm);
                permX += lblPerm.Width + 6;
                if (permX + 100 > maxWidth)
                {
                    permX = 20;
                    permY += 30;
                }
            }

            // Action Buttons
            int buttonY = 320;
            
            Button btnResetPassword = new Button
            {
                Text = "Reset Password",
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = Color.FromArgb(13, 110, 253),
                BackColor = Color.FromArgb(207, 226, 255),
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0, MouseOverBackColor = Color.FromArgb(190, 210, 240) },
                Size = new Size(140, 32),
                Location = new Point(20, buttonY),
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleCenter
            };
            // Make button rounded
            ApplyRoundedCorners(btnResetPassword, 4);
            btnResetPassword.Click += (s, e) => ResetPassword(user);
            card.Controls.Add(btnResetPassword);

            Button btnSuspend = new Button
            {
                Text = "Suspend",
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = Color.FromArgb(220, 53, 69),
                BackColor = Color.FromArgb(248, 215, 218),
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0, MouseOverBackColor = Color.FromArgb(240, 195, 200) },
                Size = new Size(100, 32),
                Location = new Point(170, buttonY),
                Cursor = Cursors.Hand
            };
            // Make button rounded
            ApplyRoundedCorners(btnSuspend, 4);
            btnSuspend.Click += (s, e) => SuspendUser(user);
            card.Controls.Add(btnSuspend);

            return card;
        }
        
        private string GetDisplayRole(string role)
        {
            if (string.IsNullOrEmpty(role))
                return "Member";
                
            switch (role.ToLower())
            {
                case "admin":
                    return "Admin";
                case "librarystaff":
                    return "LibraryStaff";
                case "member":
                    return "Member";
                default:
                    return role;
            }
        }
        
        private void ApplyRoundedCorners(Control control, int radius)
        {
            control.Paint += (s, e) =>
            {
                Rectangle rect = control.ClientRectangle;
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                    path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
                    path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
                    path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
                    path.CloseAllFigures();
                    control.Region = new Region(path);
                }
            };
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
                case "librarian":
                case "staff":
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

            MessageBox.Show($"Edit user: {user.FirstName} {user.LastName}", "Edit User", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

    }
}
