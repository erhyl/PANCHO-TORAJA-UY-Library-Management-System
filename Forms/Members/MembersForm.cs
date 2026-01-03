using Project5LMS.Controllers;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Project5LMS.Admin_Dashboard
{
    public partial class MembersForm : Form
    {
        private MembersController membersController;
        private string connectionString;

        public MembersForm()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["MySqlConnectionString"]?.ConnectionString 
                ?? "Server=localhost;Database=librarydb;Uid=root;Pwd=;";
            membersController = new MembersController();
            SetupDataGridView();
        }

        private void MembersForm_Load(object sender, EventArgs e)
        {
            AutoExpireMembers();
            LoadFilters();
            LoadMetrics();
            LoadMembers();
            LoadSearchIcon();
        }

        private void LoadSearchIcon()
        {
            try
            {
                // Create a simple search icon using Graphics
                Bitmap searchIcon = new Bitmap(24, 24);
                using (Graphics g = Graphics.FromImage(searchIcon))
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    Pen pen = new Pen(Color.Gray, 2);
                    
                    // Draw magnifying glass
                    g.DrawEllipse(pen, 4, 4, 12, 12);
                    g.DrawLine(pen, 14, 14, 20, 20);
                }
                picSearchIcon.Image = searchIcon;
                picSearchIcon.BackColor = Color.Transparent;
            }
            catch
            {
                // If icon creation fails, leave it empty
                picSearchIcon.BackColor = Color.Transparent;
            }
        }

        private void SetupDataGridView()
        {
            dta_Members.AutoGenerateColumns = false;
            dta_Members.Columns.Clear();
            dta_Members.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 11F);
            dta_Members.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Bold);
            dta_Members.RowTemplate.Height = 60;
            dta_Members.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            
            dta_Members.Columns.Add("MemberID", "MemberID");
            dta_Members.Columns["MemberID"].Visible = false;
            
            dta_Members.Columns.Add("Member", "Member");
            dta_Members.Columns["Member"].Width = 300;
            
            dta_Members.Columns.Add("Type", "Type");
            dta_Members.Columns["Type"].Width = 120;
            
            dta_Members.Columns.Add("Email", "Email");
            dta_Members.Columns["Email"].Width = 250;
            
            dta_Members.Columns.Add("BooksBorrowed", "Books Borrowed");
            dta_Members.Columns["BooksBorrowed"].Width = 150;
            
            dta_Members.Columns.Add("Fines", "Fines");
            dta_Members.Columns["Fines"].Width = 100;
            
            dta_Members.Columns.Add("Registration", "Registration");
            dta_Members.Columns["Registration"].Width = 150;
            
            dta_Members.Columns.Add("Expiration", "Expiration");
            dta_Members.Columns["Expiration"].Width = 150;
            
            dta_Members.Columns.Add("Status", "Status");
            dta_Members.Columns["Status"].Width = 120;
            
            // Add Actions column with button cells
            DataGridViewButtonColumn actionsColumn = new DataGridViewButtonColumn();
            actionsColumn.Name = "Actions";
            actionsColumn.HeaderText = "Actions";
            actionsColumn.Width = 120;
            actionsColumn.Text = "View Edit";
            actionsColumn.UseColumnTextForButtonValue = true;
            actionsColumn.FlatStyle = FlatStyle.Flat;
            dta_Members.Columns.Add(actionsColumn);
        }

        private void dta_Members_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dta_Members.Columns[e.ColumnIndex].Name == "Actions")
            {
                int memberId = Convert.ToInt32(dta_Members.Rows[e.RowIndex].Cells["MemberID"].Value);
                AddMemberForm editForm = new AddMemberForm(memberId);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadMembers();
                    LoadMetrics();
                }
            }
        }

        private void AutoExpireMembers()
        {
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE Members SET Status='Expired' WHERE ExpirationDate < CURDATE() AND Status != 'Expired'";
                    var cmd = new MySqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error auto-expiring members: {ex.Message}");
            }
        }

        private void LoadMetrics()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    lblMetricValue1.Text = GetCount(conn, "SELECT COUNT(*) FROM Members");
                    lblMetricValue2.Text = GetCount(conn, "SELECT COUNT(*) FROM Members WHERE Status = 'Active'");
                    lblMetricValue3.Text = GetCount(conn, "SELECT COUNT(*) FROM Members WHERE Status = 'Inactive'");
                    lblMetricValue4.Text = GetCount(conn, "SELECT COUNT(*) FROM Members WHERE Status = 'Suspended'");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading metrics: {ex.Message}");
            }
        }

        private string GetCount(MySqlConnection conn, string query)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    object result = cmd.ExecuteScalar();
                    return result != null && result != DBNull.Value ? Convert.ToInt32(result).ToString() : "0";
                }
            }
            catch
            {
                return "0";
            }
        }

        private void LoadMembers()
        {
            try
            {
                dta_Members.Rows.Clear();

                string keyword = txtSearch.Text.Trim();
                if (keyword == "Search by MemberID or email") keyword = "";

                string type = cmbTypes.Text == "All Types" ? "All" : cmbTypes.Text;
                string status = cmbStatus.Text == "All Status" ? "All" : cmbStatus.Text;

                // Query using CirculationRecords table (correct table name)
                string query = @"SELECT 
                                    m.MemberID,
                                    m.FirstName,
                                    m.LastName,
                                    m.MemberType,
                                    m.Email,
                                    m.Status,
                                    m.RegistrationDate,
                                    m.ExpirationDate,
                                    COALESCE(COUNT(DISTINCT CASE WHEN cr.ReturnDate IS NULL THEN cr.RecordID END), 0) as BooksBorrowed,
                                    COALESCE(SUM(CASE WHEN f.Paid = FALSE THEN f.FineAmount ELSE 0 END), 0) as TotalFines
                                FROM Members m
                                LEFT JOIN CirculationRecords cr ON m.MemberID = cr.MemberID
                                LEFT JOIN Fines f ON m.MemberID = f.MemberID
                                WHERE 
                                    (@Keyword = '' OR m.Email LIKE @Keyword OR CAST(m.MemberID AS CHAR) LIKE @Keyword OR m.FirstName LIKE @Keyword OR m.LastName LIKE @Keyword)
                                    AND (@Type = 'All' OR m.MemberType = @Type)
                                    AND (@Status = 'All' OR m.Status = @Status)
                                GROUP BY m.MemberID, m.FirstName, m.LastName, m.MemberType, m.Email, m.Status, m.RegistrationDate, m.ExpirationDate
                                ORDER BY m.LastName, m.FirstName";

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                    cmd.Parameters.AddWithValue("@Type", type);
                    cmd.Parameters.AddWithValue("@Status", status);

                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int memberId = reader.GetInt32("MemberID");
                            string firstName = reader["FirstName"] != DBNull.Value ? reader["FirstName"].ToString() : "";
                            string lastName = reader["LastName"] != DBNull.Value ? reader["LastName"].ToString() : "";
                            string fullName = $"{firstName} {lastName}".Trim();
                            string memberType = reader["MemberType"].ToString();
                            string email = reader["Email"].ToString();
                            string statusValue = reader["Status"].ToString();
                            DateTime registrationDate = reader["RegistrationDate"] != DBNull.Value ? Convert.ToDateTime(reader["RegistrationDate"]) : DateTime.MinValue;
                            DateTime expirationDate = reader["ExpirationDate"] != DBNull.Value ? Convert.ToDateTime(reader["ExpirationDate"]) : DateTime.MinValue;
                            int booksBorrowed = Convert.ToInt32(reader["BooksBorrowed"]);
                            decimal totalFines = Convert.ToDecimal(reader["TotalFines"]);

                            // Get initials
                            string initials = "";
                            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
                            {
                                initials = (firstName[0].ToString() + lastName[0].ToString()).ToUpper();
                            }
                            else if (!string.IsNullOrEmpty(fullName))
                            {
                                initials = fullName.Substring(0, Math.Min(2, fullName.Length)).ToUpper();
                            }

                            // Format member display: Initials, Name, ID
                            string memberDisplay = $"{initials}\n{fullName}\nID: {memberId:D5}";

                            string booksDisplay = $"{booksBorrowed}/5";
                            string finesDisplay = $"₱{totalFines:F0}";
                            string registrationDisplay = registrationDate != DateTime.MinValue ? registrationDate.ToString("MM/dd/yyyy") : "N/A";
                            string expirationDisplay = expirationDate != DateTime.MinValue ? expirationDate.ToString("MM/dd/yyyy") : "N/A";

                            int rowIndex = dta_Members.Rows.Add(
                                memberId,
                                memberDisplay,
                                memberType,
                                email,
                                booksDisplay,
                                finesDisplay,
                                registrationDisplay,
                                expirationDisplay,
                                statusValue,
                                "View Edit"
                            );

                            DataGridViewRow row = dta_Members.Rows[rowIndex];
                            
                            if (statusValue == "Active")
                            {
                                row.Cells["Status"].Style.ForeColor = Color.FromArgb(76, 175, 80);
                                row.Cells["Status"].Style.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
                            }
                            else
                            {
                                row.Cells["Status"].Style.ForeColor = Color.Gray;
                            }

                            row.Cells["Type"].Style.BackColor = Color.FromArgb(240, 240, 240);
                            row.Cells["Type"].Style.Padding = new Padding(10, 5, 10, 5);
                            row.Cells["Member"].Style.WrapMode = DataGridViewTriState.True;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading members: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadFilters()
        {
            cmbTypes.Items.Clear();
            cmbTypes.Items.AddRange(new string[] { "All Types", "Student", "Faculty", "Staff", "Guest" });
            cmbTypes.SelectedIndex = 0;

            cmbStatus.Items.Clear();
            cmbStatus.Items.AddRange(new string[] { "All Status", "Active", "Inactive", "Suspended", "Expired" });
            cmbStatus.SelectedIndex = 0;
        }

        private void ApplyFilters()
        {
            LoadMembers();
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search by MemberID or email")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Search by MemberID or email";
                txtSearch.ForeColor = Color.Gray;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "Search by MemberID or email")
            {
                ApplyFilters();
            }
        }

        private void cmbTypes_SelectedIndexChanged(object sender, EventArgs e) => ApplyFilters();
        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e) => ApplyFilters();

        private void btnAddMember_Click(object sender, EventArgs e)
        {
            AddMemberForm addForm = new AddMemberForm();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                LoadMembers();
                LoadMetrics();
            }
        }

        private void dta_Members_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex != dta_Members.Columns["Actions"].Index)
            {
                int memberId = Convert.ToInt32(dta_Members.Rows[e.RowIndex].Cells["MemberID"].Value);
                AddMemberForm editForm = new AddMemberForm(memberId);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadMembers();
                    LoadMetrics();
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadMembers();
            LoadMetrics();
        }
    }
}
