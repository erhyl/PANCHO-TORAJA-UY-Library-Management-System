using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Project5LMS.Helpers;
using Project5LMS.Services;
using Project5LMS.Data;
using Project5LMS.Interfaces;
namespace Project5LMS.Forms.Member.Profile
{
    public partial class MemberProfileForm : Form
    {
        private readonly DatabaseContext _dbContext;
        private readonly IMembersService _membersService;
        public MemberProfileForm()
        {
            InitializeComponent();
            _dbContext = ServiceFactory.GetDbContext();
            _membersService = ServiceFactory.CreateMembersService();
            this.Load += MemberProfileForm_Load;
            this.VisibleChanged += MemberProfileForm_VisibleChanged;
            this.BackColor = Color.FromArgb(250, 250, 250);
            this.Visible = true;
            if (this.Visible)
            {
                LoadProfileData();
            }
        }
        private void MemberProfileForm_Load(object sender, EventArgs e)
        {
            LoadProfileData();
        }
        private void MemberProfileForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                LoadProfileData();
            }
        }
        private void LoadProfileData()
        {
            int memberID = CurrentUser.GetMemberID();
            if (memberID == 0)
            {
                MessageBox.Show("Unable to identify your member account.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    LoadMemberInfo(conn, memberID);
                    LoadBorrowingPrivileges(conn, memberID);
                    LoadAccountStatistics(conn, memberID);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading profile data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadMemberInfo(MySqlConnection conn, int memberID)
        {
            try
            {
                bool hasContact = DatabaseSchemaHelper.CheckColumnExists(conn, "Members", "Contact");
                bool hasAddress = DatabaseSchemaHelper.CheckColumnExists(conn, "Members", "Address");
                string query = @"SELECT
                                    MemberID,
                                    FirstName,
                                    LastName,
                                    Email,
                                    COALESCE(Type, MemberType) as MemberType,
                                    RegistrationDate,
                                    ExpirationDate,
                                    Status" +
                                    (hasContact ? ", Contact" : "") +
                                    (hasAddress ? ", Address" : "") +
                                @" FROM Members
                                WHERE MemberID = @MemberID";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MemberID", memberID);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string firstName = reader["FirstName"] != DBNull.Value ? reader["FirstName"].ToString() : "";
                            string lastName = reader["LastName"] != DBNull.Value ? reader["LastName"].ToString() : "";
                            string fullName = $"{firstName} {lastName}".Trim();
                            if (string.IsNullOrEmpty(fullName))
                            {
                                fullName = CurrentUser.FullName;
                            }
                            lblProfileName.Text = fullName;
                            string memberIDStr = reader["MemberID"] != DBNull.Value ? reader["MemberID"].ToString() : memberID.ToString();
                            int parsedMemberID = int.TryParse(memberIDStr, out int id) ? id : memberID;
                            lblMemberID.Text = $"Member ID: {Project5LMS.Helpers.IDFormatter.FormatMemberID(parsedMemberID)}";
                            string status = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : "Active";
                            lblStatusBadge.Text = status;
                            if (status.ToLower() == "active")
                            {
                                lblStatusBadge.BackColor = Color.FromArgb(76, 175, 80);
                            }
                            else
                            {
                                lblStatusBadge.BackColor = Color.FromArgb(158, 158, 158);
                            }
                            lblGoodStanding.Text = "Good Standing";
                            lblFullNameValue.Text = fullName;
                            lblEmailValue.Text = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : CurrentUser.Email;
                            if (hasContact)
                            {
                                lblPhoneValue.Text = reader["Contact"] != DBNull.Value ? reader["Contact"].ToString() : "N/A";
                            }
                            else
                            {
                                lblPhoneValue.Text = "N/A";
                            }
                            if (hasAddress)
                            {
                                lblAddressValue.Text = reader["Address"] != DBNull.Value ? reader["Address"].ToString() : "N/A";
                            }
                            else
                            {
                                lblAddressValue.Text = "N/A";
                            }
                            lblMemberTypeValue.Text = reader["MemberType"] != DBNull.Value ? reader["MemberType"].ToString() : "N/A";
                            if (reader["RegistrationDate"] != DBNull.Value)
                            {
                                DateTime regDate = Convert.ToDateTime(reader["RegistrationDate"]);
                                lblRegistrationDateValue.Text = regDate.ToString("yyyy-MM-dd");
                            }
                            else
                            {
                                lblRegistrationDateValue.Text = "N/A";
                            }
                            if (reader["ExpirationDate"] != DBNull.Value)
                            {
                                DateTime expDate = Convert.ToDateTime(reader["ExpirationDate"]);
                                lblExpirationDateValue.Text = expDate.ToString("yyyy-MM-dd");
                            }
                            else
                            {
                                lblExpirationDateValue.Text = "N/A";
                            }
                            lblAccountStatusValue.Text = status;
                            if (status.ToLower() == "active")
                            {
                                lblAccountStatusBadge.Text = "Active";
                                lblAccountStatusBadge.BackColor = Color.FromArgb(76, 175, 80);
                            }
                            else
                            {
                                lblAccountStatusBadge.Text = status;
                                lblAccountStatusBadge.BackColor = Color.FromArgb(158, 158, 158);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading member information: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadBorrowingPrivileges(MySqlConnection conn, int memberID)
        {
            try
            {
                int maxBooks = 5;
                int borrowingPeriod = 14;
                int renewalLimit = 3;
                int reservationLimit = 5;
                decimal fineRate = 1.00m;
                string memberTypeQuery = "SELECT COALESCE(Type, MemberType) FROM Members WHERE MemberID = @MemberID";
                using (MySqlCommand cmd = new MySqlCommand(memberTypeQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@MemberID", memberID);
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        string memberType = result.ToString().ToLower();
                        if (memberType.Contains("faculty") || memberType.Contains("staff"))
                        {
                            maxBooks = 10;
                            borrowingPeriod = 21;
                        }
                    }
                }
                lblMaxBooksValue.Text = $"{maxBooks} books";
                lblBorrowingPeriodValue.Text = $"{borrowingPeriod} days";
                lblRenewalLimitValue.Text = $"{renewalLimit} times";
                lblReservationLimitValue.Text = $"{reservationLimit} books";
                lblFineRateValue.Text = $"${fineRate:F2} per day";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading borrowing privileges: {ex.Message}");
            }
        }
        private void LoadAccountStatistics(MySqlConnection conn, int memberID)
        {
            try
            {
                int totalBorrowed = 0;
                try
                {
                    string query1 = @"SELECT COUNT(*) FROM CirculationRecords
                                     WHERE MemberID = @MemberID";
                    using (MySqlCommand cmd = new MySqlCommand(query1, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", memberID);
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            totalBorrowed = Convert.ToInt32(result);
                        }
                    }
                }
                catch
                {
                    try
                    {
                        string query2 = @"SELECT COUNT(*) FROM Transactions
                                         WHERE MemberID = @MemberID";
                        using (MySqlCommand cmd = new MySqlCommand(query2, conn))
                        {
                            cmd.Parameters.AddWithValue("@MemberID", memberID);
                            object result = cmd.ExecuteScalar();
                            if (result != null && result != DBNull.Value)
                            {
                                totalBorrowed = Convert.ToInt32(result);
                            }
                        }
                    }
                    catch { }
                }
                lblTotalBorrowedValue.Text = totalBorrowed.ToString();
                int currentBorrowings = 0;
                try
                {
                    string query = @"SELECT COUNT(*) FROM CirculationRecords
                                   WHERE MemberID = @MemberID
                                   AND Status = 'CheckedOut'";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", memberID);
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            currentBorrowings = Convert.ToInt32(result);
                        }
                    }
                }
                catch
                {
                    try
                    {
                        string query = @"SELECT COUNT(*) FROM Transactions
                                       WHERE MemberID = @MemberID
                                       AND (Status = 'Borrowed' OR Status IS NULL OR ReturnDate IS NULL)";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MemberID", memberID);
                            object result = cmd.ExecuteScalar();
                            if (result != null && result != DBNull.Value)
                            {
                                currentBorrowings = Convert.ToInt32(result);
                            }
                        }
                    }
                    catch { }
                }
                lblCurrentBorrowingsValue.Text = currentBorrowings.ToString();
                int totalReservations = 0;
                try
                {
                    string query = @"SELECT COUNT(*) FROM Reservations
                                   WHERE MemberID = @MemberID";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", memberID);
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            totalReservations = Convert.ToInt32(result);
                        }
                    }
                }
                catch { }
                lblTotalReservationsValue.Text = totalReservations.ToString();
                int activeReservations = 0;
                try
                {
                    string query = @"SELECT COUNT(*) FROM Reservations
                                   WHERE MemberID = @MemberID
                                   AND (Status = 'Pending' OR Status = 'Active' OR Status = 'Ready' OR Status IS NULL)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", memberID);
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            activeReservations = Convert.ToInt32(result);
                        }
                    }
                }
                catch { }
                lblActiveReservationsValue.Text = activeReservations.ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading account statistics: {ex.Message}");
            }
        }
        private void panelAvatar_Paint(object sender, PaintEventArgs e)
        {
            DrawPersonIcon(e.Graphics, panelAvatar.ClientRectangle);
        }
        private void DrawPersonIcon(Graphics g, Rectangle rect)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int centerX = rect.Width / 2;
            int centerY = rect.Height / 2;
            int size = Math.Min(rect.Width, rect.Height) - 20;
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(230, 240, 255)))
            {
                g.FillEllipse(brush, centerX - size / 2, centerY - size / 2, size, size);
            }
            using (Pen pen = new Pen(Color.FromArgb(100, 150, 200), 3))
            {
                int headRadius = size / 6;
                g.DrawEllipse(pen, centerX - headRadius, centerY - size / 3, headRadius * 2, headRadius * 2);
                Point[] body = new Point[]
                {
                    new Point(centerX, centerY - size / 3 + headRadius * 2),
                    new Point(centerX - size / 4, centerY + size / 3),
                    new Point(centerX + size / 4, centerY + size / 3)
                };
                g.DrawPolygon(pen, body);
            }
        }
        private void panelPersonIcon_Paint(object sender, PaintEventArgs e)
        {
            DrawSmallPersonIcon(e.Graphics, panelPersonIcon.ClientRectangle);
        }
        private void DrawSmallPersonIcon(Graphics g, Rectangle rect)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int centerX = rect.Width / 2;
            int centerY = rect.Height / 2;
            int size = Math.Min(rect.Width, rect.Height) - 4;
            using (Pen pen = new Pen(Color.FromArgb(128, 128, 128), 2))
            {
                int headRadius = size / 6;
                g.DrawEllipse(pen, centerX - headRadius, centerY - size / 3, headRadius * 2, headRadius * 2);
                Point[] body = new Point[]
                {
                    new Point(centerX, centerY - size / 3 + headRadius * 2),
                    new Point(centerX - size / 4, centerY + size / 3),
                    new Point(centerX + size / 4, centerY + size / 3)
                };
                g.DrawPolygon(pen, body);
            }
        }
        private void panelEnvelopeIcon_Paint(object sender, PaintEventArgs e)
        {
            DrawEnvelopeIcon(e.Graphics, panelEnvelopeIcon.ClientRectangle);
        }
        private void DrawEnvelopeIcon(Graphics g, Rectangle rect)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int centerX = rect.Width / 2;
            int centerY = rect.Height / 2;
            int width = rect.Width - 4;
            int height = rect.Height - 4;
            using (Pen pen = new Pen(Color.FromArgb(128, 128, 128), 2))
            {
                g.DrawRectangle(pen, centerX - width / 2, centerY - height / 2, width, height);
                Point[] flap = new Point[]
                {
                    new Point(centerX, centerY - height / 2),
                    new Point(centerX - width / 2, centerY - height / 2 + height / 3),
                    new Point(centerX + width / 2, centerY - height / 2 + height / 3)
                };
                g.DrawPolygon(pen, flap);
            }
        }
        private void panelPhoneIcon_Paint(object sender, PaintEventArgs e)
        {
            DrawPhoneIcon(e.Graphics, panelPhoneIcon.ClientRectangle);
        }
        private void DrawPhoneIcon(Graphics g, Rectangle rect)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int centerX = rect.Width / 2;
            int centerY = rect.Height / 2;
            int width = rect.Width - 4;
            int height = rect.Height - 4;
            using (Pen pen = new Pen(Color.FromArgb(128, 128, 128), 2))
            {
                Rectangle phoneRect = new Rectangle(centerX - width / 2, centerY - height / 2, width, height);
                g.DrawRectangle(pen, phoneRect);
                Rectangle screenRect = new Rectangle(centerX - width / 2 + 2, centerY - height / 2 + 4, width - 4, height - 8);
                g.DrawRectangle(pen, screenRect);
            }
        }
        private void panelLocationIcon_Paint(object sender, PaintEventArgs e)
        {
            DrawLocationIcon(e.Graphics, panelLocationIcon.ClientRectangle);
        }
        private void DrawLocationIcon(Graphics g, Rectangle rect)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int centerX = rect.Width / 2;
            int centerY = rect.Height / 2;
            int size = Math.Min(rect.Width, rect.Height) - 4;
            using (Pen pen = new Pen(Color.FromArgb(128, 128, 128), 2))
            {
                g.DrawEllipse(pen, centerX - size / 4, centerY - size / 2, size / 2, size / 2);
                Point[] triangle = new Point[]
                {
                    new Point(centerX, centerY + size / 3),
                    new Point(centerX - size / 3, centerY - size / 6),
                    new Point(centerX + size / 3, centerY - size / 6)
                };
                g.DrawPolygon(pen, triangle);
            }
        }
        private void panelCardIcon_Paint(object sender, PaintEventArgs e)
        {
            DrawCardIcon(e.Graphics, panelCardIcon.ClientRectangle);
        }
        private void DrawCardIcon(Graphics g, Rectangle rect)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int centerX = rect.Width / 2;
            int centerY = rect.Height / 2;
            int width = rect.Width - 4;
            int height = rect.Height - 4;
            using (Pen pen = new Pen(Color.FromArgb(128, 128, 128), 2))
            {
                g.DrawRectangle(pen, centerX - width / 2, centerY - height / 2, width, height);
                g.DrawLine(pen, centerX - width / 2 + 4, centerY - height / 4, centerX + width / 2 - 4, centerY - height / 4);
                g.DrawLine(pen, centerX - width / 2 + 4, centerY, centerX + width / 2 - 4, centerY);
            }
        }
        private void panelCalendarIcon_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            if (panel != null)
            {
                DrawCalendarIcon(e.Graphics, panel.ClientRectangle);
            }
        }
        private void DrawCalendarIcon(Graphics g, Rectangle rect)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int centerX = rect.Width / 2;
            int centerY = rect.Height / 2;
            int width = rect.Width - 4;
            int height = rect.Height - 4;
            using (Pen pen = new Pen(Color.FromArgb(128, 128, 128), 2))
            {
                g.DrawRectangle(pen, centerX - width / 2, centerY - height / 2, width, height);
                Rectangle bindingRect = new Rectangle(centerX - width / 2, centerY - height / 2, width, height / 4);
                g.DrawRectangle(pen, bindingRect);
                g.DrawEllipse(pen, centerX - width / 2 - 2, centerY - height / 2 + 2, 4, 4);
                g.DrawEllipse(pen, centerX + width / 2 - 2, centerY - height / 2 + 2, 4, 4);
            }
        }
        private void panelShieldIcon_Paint(object sender, PaintEventArgs e)
        {
            DrawShieldIcon(e.Graphics, panelShieldIcon.ClientRectangle);
        }
        private void DrawShieldIcon(Graphics g, Rectangle rect)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int centerX = rect.Width / 2;
            int centerY = rect.Height / 2;
            int width = rect.Width - 4;
            int height = rect.Height - 4;
            using (Pen pen = new Pen(Color.FromArgb(128, 128, 128), 2))
            {
                Point[] shield = new Point[]
                {
                    new Point(centerX, centerY - height / 2),
                    new Point(centerX - width / 2, centerY - height / 4),
                    new Point(centerX - width / 2, centerY + height / 4),
                    new Point(centerX, centerY + height / 2),
                    new Point(centerX + width / 2, centerY + height / 4),
                    new Point(centerX + width / 2, centerY - height / 4)
                };
                g.DrawPolygon(pen, shield);
            }
        }
        private void lblAddressLabel_Click(object sender, EventArgs e)
        {
        }
        private void lblPhoneValue_Click(object sender, EventArgs e)
        {
        }
    }
}