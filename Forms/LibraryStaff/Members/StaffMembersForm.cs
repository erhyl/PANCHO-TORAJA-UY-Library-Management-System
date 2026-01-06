using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Project5LMS.Helpers;
using Project5LMS.Forms.Admin.Members;
using Project5LMS.Controllers;

namespace Project5LMS.Forms.LibraryStaff.Members
{
    public partial class StaffMembersForm : Form
    {
        private string connectionString;
        private DataTable allMembersData;
        private MembersController membersController;
        private const string SearchPlaceholder = "Search by name, ID, or email...";

        public StaffMembersForm()
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
            membersController = new MembersController();
        }

        private void StaffMembersForm_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadMembers();
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
                Width = 200,
                ReadOnly = true
            };
            dataGridViewMembers.Columns.Add(colName);

            DataGridViewTextBoxColumn colContact = new DataGridViewTextBoxColumn
            {
                Name = "Contact",
                HeaderText = "CONTACT",
                DataPropertyName = "Contact",
                Width = 250,
                ReadOnly = true
            };
            dataGridViewMembers.Columns.Add(colContact);

            DataGridViewTextBoxColumn colMemberSince = new DataGridViewTextBoxColumn
            {
                Name = "MemberSince",
                HeaderText = "MEMBER SINCE",
                DataPropertyName = "MemberSince",
                Width = 150,
                ReadOnly = true
            };
            dataGridViewMembers.Columns.Add(colMemberSince);

            DataGridViewTextBoxColumn colActiveLoans = new DataGridViewTextBoxColumn
            {
                Name = "ActiveLoans",
                HeaderText = "ACTIVE LOANS",
                DataPropertyName = "ActiveLoans",
                Width = 120,
                ReadOnly = true
            };
            dataGridViewMembers.Columns.Add(colActiveLoans);

            DataGridViewTextBoxColumn colStatus = new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "STATUS",
                DataPropertyName = "Status",
                Width = 120,
                ReadOnly = true
            };
            dataGridViewMembers.Columns.Add(colStatus);

            DataGridViewButtonColumn colEdit = new DataGridViewButtonColumn
            {
                Name = "Edit",
                HeaderText = "ACTIONS",
                Text = "?",
                UseColumnTextForButtonValue = true,
                Width = 50,
                FlatStyle = FlatStyle.Flat
            };
            colEdit.DefaultCellStyle.ForeColor = Color.FromArgb(13, 110, 253);
            colEdit.DefaultCellStyle.BackColor = Color.White;
            dataGridViewMembers.Columns.Add(colEdit);

            DataGridViewButtonColumn colDelete = new DataGridViewButtonColumn
            {
                Name = "Delete",
                Text = "??",
                UseColumnTextForButtonValue = true,
                Width = 50,
                FlatStyle = FlatStyle.Flat
            };
            colDelete.DefaultCellStyle.ForeColor = Color.FromArgb(220, 53, 69);
            colDelete.DefaultCellStyle.BackColor = Color.White;
            dataGridViewMembers.Columns.Add(colDelete);

            dataGridViewMembers.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridViewMembers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridViewMembers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dataGridViewMembers.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            dataGridViewMembers.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
        }

        private void LoadMembers()
        {
            try
            {
                allMembersData = GetMembersWithContact();

                if (!allMembersData.Columns.Contains("Name"))
                {
                    allMembersData.Columns.Add("Name", typeof(string));
                }
                if (!allMembersData.Columns.Contains("Contact"))
                {
                    allMembersData.Columns.Add("Contact", typeof(string));
                }
                if (!allMembersData.Columns.Contains("MemberSince"))
                {
                    allMembersData.Columns.Add("MemberSince", typeof(string));
                }
                if (!allMembersData.Columns.Contains("ActiveLoans"))
                {
                    allMembersData.Columns.Add("ActiveLoans", typeof(int));
                }

                foreach (DataRow row in allMembersData.Rows)
                {

                    string firstName = row["FirstName"] != DBNull.Value ? row["FirstName"].ToString() : "";
                    string lastName = row["LastName"] != DBNull.Value ? row["LastName"].ToString() : "";
                    row["Name"] = $"{firstName} {lastName}".Trim();

                    string email = row["Email"] != DBNull.Value ? row["Email"].ToString() : "";
                    string contact = "";
                    if (allMembersData.Columns.Contains("Contact") && row["Contact"] != DBNull.Value && !string.IsNullOrEmpty(row["Contact"].ToString()))
                    {
                        contact = row["Contact"].ToString();
                    }
                    row["Contact"] = FormatContact(email, contact);

                    if (row["RegistrationDate"] != DBNull.Value)
                    {
                        DateTime regDate = Convert.ToDateTime(row["RegistrationDate"]);
                        row["MemberSince"] = regDate.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        row["MemberSince"] = "N/A";
                    }

                    int memberId = Convert.ToInt32(row["MemberID"]);
                    row["ActiveLoans"] = GetActiveLoansCount(memberId);
                }

                dataGridViewMembers.DataSource = allMembersData;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading members: {ex.Message}");
                MessageBox.Show($"Error loading members: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable GetMembersWithContact()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                bool hasContact = CheckColumnExists(conn, "Members", "Contact");

                string query = hasContact
                    ? @"SELECT 
                            MemberID,
                            FirstName,
                            LastName,
                            MemberType,
                            Email,
                            Contact,
                            RegistrationDate,
                            ExpirationDate,
                            Status
                         FROM Members
                         ORDER BY LastName, FirstName"
                    : @"SELECT 
                            MemberID,
                            FirstName,
                            LastName,
                            MemberType,
                            Email,
                            RegistrationDate,
                            ExpirationDate,
                            Status
                         FROM Members
                         ORDER BY LastName, FirstName";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        private string FormatContact(string email, string contact)
        {
            string result = "";
            if (!string.IsNullOrEmpty(email))
            {
                result = $"?? {email}";
            }
            if (!string.IsNullOrEmpty(contact))
            {
                if (!string.IsNullOrEmpty(result))
                    result += "\n";
                result += $"?? {contact}";
            }
            return string.IsNullOrEmpty(result) ? "N/A" : result;
        }

        private int GetActiveLoansCount(int memberId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT COUNT(*) FROM Transactions 
                                   WHERE MemberID = @MemberID 
                                   AND (Status = 'Borrowed' OR Status = 'Active') 
                                   AND ReturnDate IS NULL";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", memberId);
                        object result = cmd.ExecuteScalar();
                        return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
                    }
                }
            }
            catch
            {
                return 0;
            }
        }

        private bool CheckColumnExists(MySqlConnection conn, string tableName, string columnName)
        {
            string query = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS 
                           WHERE TABLE_SCHEMA = DATABASE() 
                           AND TABLE_NAME = @TableName 
                           AND COLUMN_NAME = @ColumnName";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TableName", tableName);
                cmd.Parameters.AddWithValue("@ColumnName", columnName);
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == SearchPlaceholder)
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = SearchPlaceholder;
                txtSearch.ForeColor = Color.FromArgb(128, 128, 128);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != SearchPlaceholder && allMembersData != null)
            {
                ApplySearch();
            }
        }

        private void ApplySearch()
        {
            try
            {
                string searchText = txtSearch.Text.ToLower();
                if (string.IsNullOrWhiteSpace(searchText) || searchText == SearchPlaceholder.ToLower())
                {
                    dataGridViewMembers.DataSource = allMembersData;
                    return;
                }

                DataTable filteredData = allMembersData.Clone();
                foreach (DataRow row in allMembersData.Rows)
                {
                    string memberId = row["MemberID"]?.ToString() ?? "";
                    string name = row["Name"]?.ToString().ToLower() ?? "";
                    string email = row["Email"]?.ToString().ToLower() ?? "";

                    if (memberId.Contains(searchText) || name.Contains(searchText) || email.Contains(searchText))
                    {
                        filteredData.ImportRow(row);
                    }
                }

                dataGridViewMembers.DataSource = filteredData;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error applying search: {ex.Message}");
            }
        }

        private void dataGridViewMembers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dataGridViewMembers.Rows[e.RowIndex];
            string columnName = dataGridViewMembers.Columns[e.ColumnIndex].Name;

            if (columnName == "MemberID" && e.Value != null)
            {
                string memberIdStr = e.Value.ToString();
                if (int.TryParse(memberIdStr, out int memberId))
                {
                    e.Value = $"M{memberIdStr}";
                }
                e.FormattingApplied = true;
            }
        }

        private void dataGridViewMembers_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string columnName = dataGridViewMembers.Columns[e.ColumnIndex].Name;

            if (columnName == "Status")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                string value = e.Value?.ToString() ?? "";
                Color bgColor = Color.LightGray;
                Color textColor = Color.Black;

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

                using (SolidBrush textBrush = new SolidBrush(textColor))
                using (StringFormat format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                {
                    e.Graphics.DrawString(value, e.CellStyle.Font, textBrush, badgeRect, format);
                }

                e.Handled = true;
            }
        }

        private void dataGridViewMembers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string columnName = dataGridViewMembers.Columns[e.ColumnIndex].Name;
            int memberId = Convert.ToInt32(dataGridViewMembers.Rows[e.RowIndex].Cells["MemberID"].Value);

            if (columnName == "Edit")
            {
                EditMember(memberId);
            }
            else if (columnName == "Delete")
            {
                DeleteMember(memberId);
            }
        }

        private void EditMember(int memberId)
        {
            try
            {
                AddMemberForm editForm = new AddMemberForm(memberId);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadMembers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening edit form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteMember(int memberId)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this member?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {

                    DataRow memberRow = allMembersData.AsEnumerable()
                        .FirstOrDefault(r => Convert.ToInt32(r["MemberID"]) == memberId);

                    string memberName = memberRow != null 
                        ? $"{memberRow["FirstName"]} {memberRow["LastName"]}" 
                        : "this member";

                    int activeLoans = GetActiveLoansCount(memberId);
                    if (activeLoans > 0)
                    {
                        MessageBox.Show(
                            $"Cannot delete {memberName}. Member has {activeLoans} active loan(s). Please return all books first.",
                            "Cannot Delete",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "DELETE FROM Members WHERE MemberID = @MemberID";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MemberID", memberId);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show($"Member {memberName} has been deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadMembers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting member: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAddNewMember_Click(object sender, EventArgs e)
        {
            try
            {
                AddMemberForm addForm = new AddMemberForm();
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    LoadMembers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening add member form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
