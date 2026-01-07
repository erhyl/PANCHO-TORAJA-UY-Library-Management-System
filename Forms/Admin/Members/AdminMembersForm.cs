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
                    e.Value = $"MEM-{memberIdStr.PadLeft(3, '0')}";
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
                TextRenderer.DrawText(e.Graphics, "?", dataGridViewMembers.DefaultCellStyle.Font, editRect, Color.White,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                Rectangle viewRect = new Rectangle(startX + buttonSize + spacing, startY, buttonSize, buttonSize);
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(13, 110, 253)))
                {
                    e.Graphics.FillEllipse(brush, viewRect);
                }
                TextRenderer.DrawText(e.Graphics, "??", dataGridViewMembers.DefaultCellStyle.Font, viewRect, Color.White,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                Rectangle deactivateRect = new Rectangle(startX + (buttonSize + spacing) * 2, startY, buttonSize, buttonSize);
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(220, 53, 69)))
                {
                    e.Graphics.FillEllipse(brush, deactivateRect);
                }
                TextRenderer.DrawText(e.Graphics, "??", dataGridViewMembers.DefaultCellStyle.Font, deactivateRect, Color.White,
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
                var members = _membersService.GetAllMembers()
                    .OrderBy(m => m.LastName)
                    .ThenBy(m => m.FirstName)
                    .ToList();

                allMembersData = DataTableHelper.MembersToDataTable(members, m => _membersService.GetActiveBorrowingCount(m.MemberID));

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

                    int memberId = Convert.ToInt32(row["MemberID"]);
                    int borrowedCount = Convert.ToInt32(row["Books"]);
                    string memberType = row["MemberType"]?.ToString() ?? "";
                    int maxBooks = GetMaxBooksForType(memberType);
                    row["Books"] = $"{borrowedCount}/{maxBooks}";

                    if (row["Expires"] != DBNull.Value)
                    {
                        DateTime expDate = Convert.ToDateTime(row["Expires"]);
                        row["Expires"] = expDate.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        row["Expires"] = "N/A";
                    }
                }

                ApplyFilters();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading members: {ex.Message}");
                MessageBox.Show($"Error loading members: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (allMembersData == null) return;

            string searchText = txtSearch.Text.ToLower();
            if (searchText == "search members...")
                searchText = "";

            string selectedType = cmbTypeFilter.SelectedItem?.ToString();
            if (selectedType == "All Types")
                selectedType = null;

            string selectedStatus = cmbStatusFilter.SelectedItem?.ToString();
            if (selectedStatus == "All Status")
                selectedStatus = null;

            DataTable filteredData = allMembersData.Clone();

            foreach (DataRow row in allMembersData.Rows)
            {
                bool matchesSearch = string.IsNullOrEmpty(searchText) ||
                    row["FirstName"].ToString().ToLower().Contains(searchText) ||
                    row["LastName"].ToString().ToLower().Contains(searchText) ||
                    row["Email"].ToString().ToLower().Contains(searchText);

                bool matchesType = selectedType == null || row["MemberType"].ToString() == selectedType;

                bool matchesStatus = selectedStatus == null;
                if (!matchesStatus)
                {
                    string status = row["Status"] != DBNull.Value ? row["Status"].ToString() : "Active";
                    if (selectedStatus == "Expired")
                    {

                        if (row["ExpirationDate"] != DBNull.Value)
                        {
                            DateTime expDate = Convert.ToDateTime(row["ExpirationDate"]);
                            matchesStatus = expDate < DateTime.Now && (status == "Active" || string.IsNullOrEmpty(status));
                        }
                    }
                    else
                    {
                        matchesStatus = status == selectedStatus;
                    }
                }

                if (matchesSearch && matchesType && matchesStatus)
                {
                    filteredData.ImportRow(row);
                }
            }

            dataGridViewMembers.DataSource = filteredData;
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

                    if (row.Cells["MemberID"]?.Value == null)
                    {
                        MessageBox.Show("Unable to identify member.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string memberIdStr = row.Cells["MemberID"].Value.ToString();
                    if (string.IsNullOrEmpty(memberIdStr) || !memberIdStr.Contains("MEM-"))
                    {
                        MessageBox.Show("Invalid member ID format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    int memberId = Convert.ToInt32(memberIdStr.Replace("MEM-", ""));

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

            MessageBox.Show($"View member details for ID: {memberId}", "View Member", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    $"Would you like to deactivate (suspend) this member instead?",
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
                    $"Are you sure you want to permanently delete member {memberName}?\n\nThis action cannot be undone.",
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
    }
}
