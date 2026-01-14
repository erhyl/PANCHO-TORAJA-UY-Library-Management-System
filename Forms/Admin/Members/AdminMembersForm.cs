using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Project5LMS.Helpers;
using Project5LMS.Services;
using Project5LMS.Data;
using Project5LMS.Repositories;
using Project5LMS.Interfaces;
using Project5LMS.Forms.Admin.Members;
namespace Project5LMS.Forms.Admin.Members
{
    public partial class AdminMembersForm : Form
    {
        private DataTable allMembersData;
        private readonly IMembersService _membersService;
        private readonly ITransactionRepository _transactionRepository;
        public AdminMembersForm()
        {
            InitializeComponent();
            _membersService = ServiceFactory.CreateMembersService();
            var dbContext = ServiceFactory.GetDbContext();
            _transactionRepository = new TransactionRepository(dbContext);
        }
        private void AdminMembersForm_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            // Handle DataGridView errors gracefully
            dataGridViewMembers.DataError += DataGridViewMembers_DataError;
            if (cmbTypeFilter.Items.Count > 0)
            {
                cmbTypeFilter.SelectedIndex = 0;
            }
            if (cmbStatusFilter.Items.Count > 0)
            {
                cmbStatusFilter.SelectedIndex = 0;
            }
            LoadMetrics();
            LoadMembers();
        }
        private void DataGridViewMembers_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Suppress the default error dialog and log the error
            System.Diagnostics.Debug.WriteLine($"DataGridView DataError: {e.Exception.Message}");
            System.Diagnostics.Debug.WriteLine($"Column: {e.ColumnIndex}, Row: {e.RowIndex}");
            e.ThrowException = false;
            e.Cancel = true;
        }
        private void DrawMetricIcon(Graphics g, Panel panel, string icon)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (Font font = new Font("Segoe UI", 20, FontStyle.Bold))
            using (Brush brush = new SolidBrush(Color.White))
            {
                SizeF textSize = g.MeasureString(icon, font);
                float x = (panel.Width - textSize.Width) / 2;
                float y = (panel.Height - textSize.Height) / 2;
                g.DrawString(icon, font, brush, x, y);
            }
        }
        private void SetupDataGridView()
        {
            dataGridViewMembers.Columns.Clear();
            dataGridViewMembers.AutoGenerateColumns = false;
            dataGridViewMembers.AllowUserToAddRows = false;
            dataGridViewMembers.ReadOnly = true;
            DataGridViewTextBoxColumn colMemberId = new DataGridViewTextBoxColumn
            {
                Name = "MemberID",
                HeaderText = "MEMBER ID",
                DataPropertyName = "MemberID",
                Width = 120,
                ReadOnly = true
            };
            dataGridViewMembers.Columns.Add(colMemberId);
            DataGridViewTextBoxColumn colName = new DataGridViewTextBoxColumn
            {
                Name = "Name",
                HeaderText = "NAME",
                DataPropertyName = "Name",
                Width = 250,
                ReadOnly = true
            };
            dataGridViewMembers.Columns.Add(colName);
            DataGridViewTextBoxColumn colContact = new DataGridViewTextBoxColumn
            {
                Name = "Contact",
                HeaderText = "CONTACT",
                DataPropertyName = "Contact",
                Width = 150,
                ReadOnly = true
            };
            dataGridViewMembers.Columns.Add(colContact);
            DataGridViewColumn colType = new DataGridViewTextBoxColumn
            {
                Name = "MemberType",
                HeaderText = "TYPE",
                DataPropertyName = "MemberType",
                Width = 120,
                ReadOnly = true
            };
            dataGridViewMembers.Columns.Add(colType);
            DataGridViewColumn colStatus = new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "STATUS",
                DataPropertyName = "Status",
                Width = 120,
                ReadOnly = true
            };
            dataGridViewMembers.Columns.Add(colStatus);
            DataGridViewTextBoxColumn colBooks = new DataGridViewTextBoxColumn
            {
                Name = "Books",
                HeaderText = "BOOKS",
                DataPropertyName = "Books",
                Width = 100,
                ReadOnly = true
            };
            dataGridViewMembers.Columns.Add(colBooks);
            DataGridViewTextBoxColumn colExpires = new DataGridViewTextBoxColumn
            {
                Name = "Expires",
                HeaderText = "EXPIRES",
                DataPropertyName = "Expires",
                Width = 120,
                ReadOnly = true
            };
            dataGridViewMembers.Columns.Add(colExpires);
            DataGridViewTextBoxColumn colActions = new DataGridViewTextBoxColumn
            {
                Name = "Actions",
                HeaderText = "ACTIONS",
                Width = 150,
                ReadOnly = true
            };
            dataGridViewMembers.Columns.Add(colActions);
            dataGridViewMembers.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridViewMembers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridViewMembers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dataGridViewMembers.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            dataGridViewMembers.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            dataGridViewMembers.RowTemplate.Height = 50;
            dataGridViewMembers.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dataGridViewMembers.CellFormatting += DataGridViewMembers_CellFormatting;
            dataGridViewMembers.CellPainting += DataGridViewMembers_CellPainting;
            dataGridViewMembers.CellClick += dataGridViewMembers_CellClick;
        }
        private void DataGridViewMembers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                DataGridViewRow row = dataGridViewMembers.Rows[e.RowIndex];
                string columnName = dataGridViewMembers.Columns[e.ColumnIndex].Name;
            if (columnName == "MemberID" && e.Value != null)
            {
                string memberIdStr = e.Value.ToString();
                if (int.TryParse(memberIdStr, out int memberId))
                {
                    e.Value = Project5LMS.Helpers.IDFormatter.FormatMemberID(memberId);
                }
                e.FormattingApplied = true;
            }
            if (columnName == "Name" && e.Value != null)
            {
                string name = e.Value.ToString();
                string email = "";
                if (row.DataBoundItem != null)
                {
                    DataRowView rowView = row.DataBoundItem as DataRowView;
                    if (rowView != null && rowView.Row.Table.Columns.Contains("Email"))
                    {
                        email = rowView["Email"]?.ToString() ?? "";
                    }
                }
                if (!string.IsNullOrEmpty(email))
                {
                    e.Value = $"{name}\n{email}";
                }
                e.FormattingApplied = true;
            }
            if (columnName == "Books" && e.Value != null)
            {
                if (row.DataBoundItem != null)
                {
                    DataRowView rowView = row.DataBoundItem as DataRowView;
                    if (rowView != null)
                    {
                        // Books is now a string, parse it
                        int borrowedCount = int.TryParse(e.Value?.ToString(), out int bc) ? bc : 0;
                        int maxBooks = 10;
                        if (rowView.Row.Table.Columns.Contains("MaxBooks") && rowView["MaxBooks"] != DBNull.Value && rowView["MaxBooks"] != null)
                        {
                            maxBooks = int.TryParse(rowView["MaxBooks"]?.ToString(), out int mb) ? mb : 10;
                        }
                        e.Value = $"{borrowedCount}/{maxBooks}";
                    }
                }
                e.FormattingApplied = true;
            }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"CellFormatting error: {ex}");
            }
        }
        private void DataGridViewMembers_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                string columnName = dataGridViewMembers.Columns[e.ColumnIndex].Name;
            if (columnName == "Actions")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                int buttonSize = 32;
                int spacing = 8;
                int startX = e.CellBounds.X + (e.CellBounds.Width - (buttonSize * 3 + spacing * 2)) / 2;
                int startY = e.CellBounds.Y + (e.CellBounds.Height - buttonSize) / 2;
                Rectangle editRect = new Rectangle(startX, startY, buttonSize, buttonSize);
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(13, 110, 253)))
                {
                    e.Graphics.FillEllipse(brush, editRect);
                }
                TextRenderer.DrawText(e.Graphics, "✏️", dataGridViewMembers.DefaultCellStyle.Font, editRect, Color.White,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                Rectangle viewRect = new Rectangle(startX + buttonSize + spacing, startY, buttonSize, buttonSize);
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(13, 110, 253)))
                {
                    e.Graphics.FillEllipse(brush, viewRect);
                }
                TextRenderer.DrawText(e.Graphics, "👁️", dataGridViewMembers.DefaultCellStyle.Font, viewRect, Color.White,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                Rectangle deactivateRect = new Rectangle(startX + (buttonSize + spacing) * 2, startY, buttonSize, buttonSize);
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(220, 53, 69)))
                {
                    e.Graphics.FillEllipse(brush, deactivateRect);
                }
                TextRenderer.DrawText(e.Graphics, "🚫", dataGridViewMembers.DefaultCellStyle.Font, deactivateRect, Color.White,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                e.Handled = true;
                return;
            }
            if (columnName == "MemberType" || columnName == "Status")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                string value = e.Value?.ToString() ?? "";
                Color bgColor = Color.LightGray;
                Color textColor = Color.Black;
                if (columnName == "MemberType")
                {
                    switch (value.ToLower())
                    {
                        case "student":
                            bgColor = Color.FromArgb(173, 216, 230);
                            break;
                        case "faculty":
                            bgColor = Color.FromArgb(221, 160, 221);
                            break;
                        case "staff":
                            bgColor = Color.FromArgb(173, 216, 230);
                            break;
                        case "guest":
                            bgColor = Color.FromArgb(255, 192, 203);
                            break;
                    }
                }
                else if (columnName == "Status")
                {
                    switch (value.ToLower())
                    {
                        case "active":
                            bgColor = Color.FromArgb(40, 167, 69);
                            textColor = Color.White;
                            break;
                        case "suspended":
                            bgColor = Color.FromArgb(220, 53, 69);
                            textColor = Color.White;
                            break;
                        case "expired":
                            bgColor = Color.FromArgb(255, 193, 7);
                            textColor = Color.White;
                            break;
                    }
                }
                Rectangle badgeRect = new Rectangle(
                    e.CellBounds.X + 5,
                    e.CellBounds.Y + (e.CellBounds.Height - 25) / 2,
                    Math.Min(100, e.CellBounds.Width - 10),
                    25
                );
                using (GraphicsPath path = new GraphicsPath())
                {
                    int radius = 12;
                    path.AddArc(badgeRect.X, badgeRect.Y, radius, radius, 180, 90);
                    path.AddArc(badgeRect.Right - radius, badgeRect.Y, radius, radius, 270, 90);
                    path.AddArc(badgeRect.Right - radius, badgeRect.Bottom - radius, radius, radius, 0, 90);
                    path.AddArc(badgeRect.X, badgeRect.Bottom - radius, radius, radius, 90, 90);
                    path.CloseAllFigures();
                    using (SolidBrush brush = new SolidBrush(bgColor))
                    {
                        e.Graphics.FillPath(brush, path);
                    }
                }
                TextRenderer.DrawText(
                    e.Graphics,
                    value,
                    dataGridViewMembers.DefaultCellStyle.Font,
                    badgeRect,
                    textColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                );
                e.Handled = true;
            }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"CellPainting error: {ex}");
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                e.Handled = false;
            }
        }
        private void LoadMetrics()
        {
            try
            {
                var allMembers = _membersService.GetAllMembers().ToList();
                int total = allMembers.Count;
                lblMetricTotalValue.Text = total.ToString();
                int active = allMembers.Count(m => m.IsActive || string.IsNullOrWhiteSpace(m.Status));
                lblMetricActiveValue.Text = active.ToString();
                int suspended = allMembers.Count(m => m.Status?.Equals("Suspended", StringComparison.OrdinalIgnoreCase) == true);
                lblMetricSuspendedValue.Text = suspended.ToString();
                int expired = allMembers.Count(m => m.IsExpired && (m.IsActive || string.IsNullOrWhiteSpace(m.Status)));
                lblMetricExpiredValue.Text = expired.ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading metrics: {ex.Message}");
            }
        }
        private void LoadMembers()
        {
            try
            {
                // Test database connection first
                var dbContext = ServiceFactory.GetDbContext();
                using (var conn = dbContext.GetConnection())
                {
                    conn.Open();
                    // Check if Members table exists and has data
                    string checkQuery = "SELECT COUNT(*) FROM Members";
                    using (var cmd = new MySqlCommand(checkQuery, conn))
                    {
                        int memberCount = Convert.ToInt32(cmd.ExecuteScalar());
                        System.Diagnostics.Debug.WriteLine($"Members table count: {memberCount}");
                        if (memberCount == 0)
                        {
                            MessageBox.Show("No members found in the database.\n\nPlease add members using the 'Add Member' button or import sample data.", 
                                "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

                var members = _membersService.GetAllMembers()
                    .OrderBy(m => m.LastName)
                    .ThenBy(m => m.FirstName)
                    .ToList();
                
                System.Diagnostics.Debug.WriteLine($"GetAllMembers returned {members?.Count ?? 0} members");
                
                if (members == null || members.Count == 0)
                {
                    System.Diagnostics.Debug.WriteLine("No members found in database");
                    allMembersData = new DataTable();
                    // Add columns to empty table
                    allMembersData.Columns.Add("MemberID", typeof(int));
                    allMembersData.Columns.Add("Name", typeof(string));
                    allMembersData.Columns.Add("Contact", typeof(string));
                    allMembersData.Columns.Add("MemberType", typeof(string));
                    allMembersData.Columns.Add("Status", typeof(string));
                    allMembersData.Columns.Add("Books", typeof(int));
                    allMembersData.Columns.Add("Expires", typeof(string));
                    allMembersData.Columns.Add("Email", typeof(string));
                    dataGridViewMembers.DataSource = allMembersData;
                    return;
                }
                
                allMembersData = DataTableHelper.MembersToDataTable(members, m => _membersService.GetActiveBorrowingCount(m.MemberID));
                
                System.Diagnostics.Debug.WriteLine($"DataTable created with {allMembersData?.Rows.Count ?? 0} rows");
                
                if (allMembersData == null || allMembersData.Rows.Count == 0)
                {
                    System.Diagnostics.Debug.WriteLine("DataTable is empty after conversion");
                    allMembersData = new DataTable();
                    // Add columns to empty table
                    allMembersData.Columns.Add("MemberID", typeof(int));
                    allMembersData.Columns.Add("Name", typeof(string));
                    allMembersData.Columns.Add("Contact", typeof(string));
                    allMembersData.Columns.Add("MemberType", typeof(string));
                    allMembersData.Columns.Add("Status", typeof(string));
                    allMembersData.Columns.Add("Books", typeof(int));
                    allMembersData.Columns.Add("Expires", typeof(string));
                    allMembersData.Columns.Add("Email", typeof(string));
                    dataGridViewMembers.DataSource = allMembersData;
                    return;
                }
                
                // Convert all columns to string types to avoid DataGridView type mismatches
                // DataGridViewTextBoxColumn expects string values
                DataTable newTable = new DataTable();
                // Create new table with all string columns in the exact order needed
                newTable.Columns.Add("MemberID", typeof(string));
                newTable.Columns.Add("Name", typeof(string));
                newTable.Columns.Add("Contact", typeof(string));
                newTable.Columns.Add("MemberType", typeof(string));
                newTable.Columns.Add("Status", typeof(string));
                newTable.Columns.Add("Books", typeof(string));
                newTable.Columns.Add("Expires", typeof(string));
                newTable.Columns.Add("Email", typeof(string));
                if (allMembersData.Columns.Contains("RegistrationDate"))
                    newTable.Columns.Add("RegistrationDate", typeof(string));
                if (allMembersData.Columns.Contains("Address"))
                    newTable.Columns.Add("Address", typeof(string));
                
                // Copy data, converting all values to strings
                foreach (DataRow row in allMembersData.Rows)
                {
                    DataRow newRow = newTable.NewRow();
                    
                    // MemberID
                    newRow["MemberID"] = row["MemberID"]?.ToString() ?? "";
                    
                    // Name
                    newRow["Name"] = row["Name"]?.ToString() ?? "";
                    
                    // Contact
                    newRow["Contact"] = row["Contact"]?.ToString() ?? "";
                    
                    // MemberType
                    newRow["MemberType"] = row["MemberType"]?.ToString() ?? "";
                    
                    // Status
                    newRow["Status"] = row["Status"]?.ToString() ?? "Active";
                    
                    // Books
                    newRow["Books"] = row["Books"]?.ToString() ?? "0";
                    
                    // Expires
                    if (row["Expires"] != DBNull.Value && row["Expires"] != null)
                    {
                        if (row["Expires"] is DateTime)
                        {
                            newRow["Expires"] = ((DateTime)row["Expires"]).ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            newRow["Expires"] = row["Expires"].ToString();
                        }
                    }
                    else
                    {
                        newRow["Expires"] = "N/A";
                    }
                    
                    // Email
                    newRow["Email"] = row["Email"]?.ToString() ?? "";
                    
                    // Optional columns
                    if (newTable.Columns.Contains("RegistrationDate") && allMembersData.Columns.Contains("RegistrationDate"))
                    {
                        if (row["RegistrationDate"] != DBNull.Value && row["RegistrationDate"] != null)
                        {
                            if (row["RegistrationDate"] is DateTime)
                            {
                                newRow["RegistrationDate"] = ((DateTime)row["RegistrationDate"]).ToString("yyyy-MM-dd");
                            }
                            else
                            {
                                newRow["RegistrationDate"] = row["RegistrationDate"].ToString();
                            }
                        }
                    }
                    
                    if (newTable.Columns.Contains("Address") && allMembersData.Columns.Contains("Address"))
                    {
                        newRow["Address"] = row["Address"]?.ToString() ?? "";
                    }
                    
                    newTable.Rows.Add(newRow);
                }
                allMembersData = newTable;
                
                System.Diagnostics.Debug.WriteLine($"After conversion: DataTable has {allMembersData.Rows.Count} rows, {allMembersData.Columns.Count} columns");
                if (allMembersData.Rows.Count > 0)
                {
                    System.Diagnostics.Debug.WriteLine($"First row sample - MemberID: {allMembersData.Rows[0]["MemberID"]} (type: {allMembersData.Rows[0]["MemberID"]?.GetType()}), Name: {allMembersData.Rows[0]["Name"]} (type: {allMembersData.Rows[0]["Name"]?.GetType()}), Books: {allMembersData.Rows[0]["Books"]} (type: {allMembersData.Rows[0]["Books"]?.GetType()})");
                }
                
                foreach (DataRow row in allMembersData.Rows)
                {
                    string contact = row["Contact"]?.ToString() ?? "";
                    if (!string.IsNullOrEmpty(contact))
                    {
                        contact = new string(contact.Where(char.IsDigit).ToArray());
                        if (contact.Length == 10)
                        {
                            row["Contact"] = $"({contact.Substring(0, 3)}) {contact.Substring(3, 3)}-{contact.Substring(6, 4)}";
                        }
                        else if (contact.Length > 0)
                        {
                            row["Contact"] = contact;
                        }
                        else
                        {
                            row["Contact"] = "N/A";
                        }
                    }
                    else
                    {
                        row["Contact"] = "N/A";
                    }
                    // MemberID and Books are now strings, convert when needed
                    int memberId = int.TryParse(row["MemberID"]?.ToString(), out int mid) ? mid : 0;
                    int borrowedCount = int.TryParse(row["Books"]?.ToString(), out int bc) ? bc : 0;
                    string memberType = row["MemberType"]?.ToString() ?? "";
                    int maxBooks = GetMaxBooksForType(memberType);
                    if (!allMembersData.Columns.Contains("MaxBooks"))
                    {
                        allMembersData.Columns.Add("MaxBooks", typeof(string));
                    }
                    row["MaxBooks"] = maxBooks.ToString();
                    // Expires is already converted to string in the previous step
                    if (row["Expires"] == DBNull.Value || string.IsNullOrEmpty(row["Expires"]?.ToString()) || row["Expires"].ToString() == "")
                    {
                        row["Expires"] = "N/A";
                    }
                }
                
                System.Diagnostics.Debug.WriteLine($"After processing rows: DataTable has {allMembersData.Rows.Count} rows ready for display");
                
                // Ensure DataGridView is ready
                if (dataGridViewMembers.InvokeRequired)
                {
                    dataGridViewMembers.Invoke(new Action(() => ApplyFilters()));
                }
                else
                {
                    ApplyFilters();
                }
                
                // Force refresh to ensure data is displayed
                dataGridViewMembers.Invalidate();
                dataGridViewMembers.Update();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading members: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
                MessageBox.Show($"Error loading members: {ex.Message}\n\nPlease check:\n1. Database connection\n2. Members table exists\n3. Database permissions", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private int GetMaxBooksForType(string memberType)
        {
            switch (memberType?.ToLower())
            {
                case "student":
                    return 5;
                case "faculty":
                    return 10;
                case "staff":
                    return 7;
                case "guest":
                    return 3;
                default:
                    return 5;
            }
        }
        private void ApplyFilters()
        {
            if (allMembersData == null || allMembersData.Rows.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("ApplyFilters: allMembersData is null or empty");
                dataGridViewMembers.DataSource = null;
                return;
            }
            
            System.Diagnostics.Debug.WriteLine($"ApplyFilters: Starting with {allMembersData.Rows.Count} rows in allMembersData");
            
            // Get raw search text and check for placeholder
            string rawSearchText = txtSearch.Text?.Trim() ?? "";
            if (rawSearchText == "Search members..." || 
                rawSearchText.StartsWith("🔍", StringComparison.OrdinalIgnoreCase) ||
                string.IsNullOrWhiteSpace(rawSearchText))
            {
                rawSearchText = "";
            }
            
            string searchText = DataGridViewHelper.NormalizeSearchText(rawSearchText);
            string selectedType = cmbTypeFilter.SelectedItem?.ToString()?.Trim();
            if (string.IsNullOrEmpty(selectedType) || selectedType == "All Types")
                selectedType = null;
            
            string selectedStatus = cmbStatusFilter.SelectedItem?.ToString()?.Trim();
            if (string.IsNullOrEmpty(selectedStatus) || selectedStatus == "All Status")
                selectedStatus = null;
            
            System.Diagnostics.Debug.WriteLine($"ApplyFilters: rawSearchText='{rawSearchText}', searchText='{searchText}', selectedType='{selectedType}', selectedStatus='{selectedStatus}'");
            
            // If no filters are active, bind directly
            if (string.IsNullOrEmpty(searchText) && selectedType == null && selectedStatus == null)
            {
                System.Diagnostics.Debug.WriteLine("ApplyFilters: No filters active, binding data directly");
                System.Diagnostics.Debug.WriteLine($"ApplyFilters: allMembersData has {allMembersData.Rows.Count} rows, {allMembersData.Columns.Count} columns");
                System.Diagnostics.Debug.WriteLine($"ApplyFilters: Column names: {string.Join(", ", allMembersData.Columns.Cast<DataColumn>().Select(c => c.ColumnName))}");
                
                // Clear existing data source
                dataGridViewMembers.DataSource = null;
                dataGridViewMembers.Refresh();
                
                // Set new data source
                dataGridViewMembers.DataSource = allMembersData;
                
                // Force update
                dataGridViewMembers.Refresh();
                dataGridViewMembers.Update();
                dataGridViewMembers.Invalidate();
                
                System.Diagnostics.Debug.WriteLine($"ApplyFilters: DataSource set. Grid now has {dataGridViewMembers.Rows.Count} rows");
                System.Diagnostics.Debug.WriteLine($"ApplyFilters: Grid column count: {dataGridViewMembers.Columns.Count}");
                
                // Verify binding
                if (dataGridViewMembers.Rows.Count == 0 && allMembersData.Rows.Count > 0)
                {
                    System.Diagnostics.Debug.WriteLine("ApplyFilters: WARNING - DataTable has rows but Grid shows none. Checking column mapping...");
                    foreach (DataGridViewColumn col in dataGridViewMembers.Columns)
                    {
                        System.Diagnostics.Debug.WriteLine($"  Grid Column: {col.Name}, DataPropertyName: {col.DataPropertyName}");
                    }
                }
                return;
            }
            
            // Create search predicate
            var searchPredicate = DataGridViewHelper.CreateTextSearchPredicate(searchText, "Name", "Email");
            
            // Create filter predicates
            Func<DataRow, bool> typeFilter = DataGridViewHelper.CreateDropdownFilterPredicate(selectedType, "MemberType", "All Types");
            Func<DataRow, bool> statusFilter = row =>
            {
                if (selectedStatus == null) return true;
                if (selectedStatus == "Expired")
                {
                    if (row["Expires"] == DBNull.Value || row["Expires"] == null || row["Expires"].ToString() == "N/A")
                        return false;
                    if (DateTime.TryParse(row["Expires"].ToString(), out DateTime expDate))
                    {
                        string status = row["Status"]?.ToString() ?? "Active";
                        return expDate < DateTime.Now && (status == "Active" || string.IsNullOrEmpty(status));
                    }
                    return false;
                }
                string rowStatus = row["Status"] != DBNull.Value && row["Status"] != null ? row["Status"].ToString() : "Active";
                return rowStatus == selectedStatus;
            };
            
            // Combine filters
            Func<DataRow, bool> combinedFilter = row => typeFilter(row) && statusFilter(row);
            
            // Apply filters using helper
            DataGridViewHelper.ApplyFilters(dataGridViewMembers, allMembersData, searchText, searchPredicate, combinedFilter);
            System.Diagnostics.Debug.WriteLine($"ApplyFilters: After filtering, Grid has {dataGridViewMembers.Rows.Count} rows");
        }
        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search members...")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }
        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Search members...";
                txtSearch.ForeColor = Color.Gray;
            }
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }
        private void cmbTypeFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }
        private void cmbStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }
        private void dataGridViewMembers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                DataGridViewRow row = dataGridViewMembers.Rows[e.RowIndex];
                string columnName = dataGridViewMembers.Columns[e.ColumnIndex].Name;
                if (columnName == "Actions")
                {
                    Point clickPoint = dataGridViewMembers.PointToClient(Cursor.Position);
                    Rectangle cellBounds = dataGridViewMembers.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                    int buttonSize = 32;
                    int spacing = 8;
                    int totalWidth = buttonSize * 3 + spacing * 2;
                    int startX = cellBounds.X + (cellBounds.Width - totalWidth) / 2;
                    
                    // Get the raw MemberID from DataRowView instead of formatted cell value
                    int memberId = 0;
                    if (row.DataBoundItem != null && row.DataBoundItem is DataRowView rowView)
                    {
                        if (rowView.Row.Table.Columns.Contains("MemberID") && rowView["MemberID"] != DBNull.Value)
                        {
                            if (int.TryParse(rowView["MemberID"].ToString(), out memberId))
                            {
                                // Successfully parsed
                            }
                        }
                    }
                    
                    // Fallback: try to get from cell value if DataRowView approach failed
                    if (memberId == 0 && row.Cells["MemberID"]?.Value != null)
                    {
                        string memberIdStr = row.Cells["MemberID"].Value.ToString();
                        memberId = Project5LMS.Helpers.IDFormatter.ParseMemberID(memberIdStr);
                    }
                    
                    if (memberId == 0)
                    {
                        MessageBox.Show("Unable to identify member. Please select a valid member row.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    int relativeX = clickPoint.X - startX;
                    if (relativeX >= 0 && relativeX <= buttonSize)
                    {
                        EditMember(memberId);
                    }
                    else if (relativeX > buttonSize + spacing && relativeX <= (buttonSize + spacing) + buttonSize)
                    {
                        ViewMember(memberId);
                    }
                    else if (relativeX > (buttonSize + spacing) * 2 && relativeX <= (buttonSize + spacing) * 2 + buttonSize)
                    {
                        DeactivateMember(memberId, row);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while processing your request: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"CellClick error: {ex}");
            }
        }
        private void EditMember(int memberId)
        {
            try
            {
                var editForm = new AddMemberForm(memberId);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadMembers();
                    LoadMetrics();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening edit form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ViewMember(int memberId)
        {
            try
            {
                var viewForm = new ViewMemberForm(memberId);
                viewForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening view member form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"ViewMember error: {ex}");
            }
        }
        private void DeactivateMember(int memberId, DataGridViewRow row)
        {
            string memberName = row.Cells["Name"].Value?.ToString().Split('\n')[0] ?? "Unknown";
            int loanCount = 0;
            int fineCount = 0;
            int reservationCount = 0;
            try
            {
                var activeTransactions = _transactionRepository.GetByMemberId(memberId)
                    .Where(t => t.Status == "Borrowed" || t.Status == "Active")
                    .ToList();
                loanCount = activeTransactions.Count;
                fineCount = activeTransactions.Count(t => t.Fine.HasValue && t.Fine.Value > 0);
                try
                {
                    var dbContext = ServiceFactory.GetDbContext();
                    string queryReservations = "SELECT COUNT(*) FROM Reservations WHERE MemberID = @memberId AND Status = 'Active'";
                    using (var conn = dbContext.GetConnection())
                    {
                        conn.Open();
                        using (var cmd = new MySqlCommand(queryReservations, conn))
                        {
                            cmd.Parameters.AddWithValue("@memberId", memberId);
                            reservationCount = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                    }
                }
                catch { }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking member transactions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (loanCount > 0 || fineCount > 0 || reservationCount > 0)
            {
                var result = MessageBox.Show(
                    $"Member {memberName} has existing transactions:\n" +
                    $"� {loanCount} active loan(s)\n" +
                    $"� {fineCount} unpaid fine(s)\n" +
                    $"� {reservationCount} active reservation(s)\n\n" +
                    $"Cannot delete member with existing transactions.\n\n" +
                    $"Would you like to deactivate (suspend) this member instead❓",
                    "Cannot Delete Member",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var member = _membersService.GetMember(memberId);
                        if (member != null)
                        {
                            member.Status = "Suspended";
                            bool updated = _membersService.UpdateMember(member);
                            if (updated)
                            {
                                MessageBox.Show("Member deactivated (suspended) successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                AuditLogger.LogDataModification("Member suspended", $"MemberID: {memberId}, Name: {memberName}", "Success");
                                LoadMembers();
                                LoadMetrics();
                            }
                            else
                            {
                                MessageBox.Show("Failed to deactivate member.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deactivating member: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                var result = MessageBox.Show(
                    $"Are you sure you want to permanently delete member {memberName}❓\n\nThis action cannot be undone.",
                    "Delete Member",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        bool deleted = _membersService.DeleteMember(memberId);
                        if (deleted)
                        {
                            MessageBox.Show("Member deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            AuditLogger.LogDataModification("Member deleted", $"MemberID: {memberId}, Name: {memberName}", "Success");
                            LoadMembers();
                            LoadMetrics();
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete member.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting member: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        AuditLogger.LogDataModification("Member deletion failed", $"MemberID: {memberId}, Error: {ex.Message}", "Failed");
                    }
                }
            }
        }
        private void btnAddNewMember_Click(object sender, EventArgs e)
        {
            try
            {
                var addForm = new AddMemberForm();
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    LoadMembers();
                    LoadMetrics();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening add member form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewMembers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}