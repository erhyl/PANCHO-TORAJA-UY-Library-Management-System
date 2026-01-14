using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Project5LMS.Helpers;
using Project5LMS.Data;
using Project5LMS.Services;
using Project5LMS.Models;
using Project5LMS.Interfaces;
namespace Project5LMS.Forms.LibraryStaff.Reservations
{
    public partial class StaffReservationsForm : Form
    {
        private DataTable allReservationsData;
        private string currentFilter = "All";
        private Dictionary<int, Panel> reservationCards = new Dictionary<int, Panel>();
        private readonly DatabaseContext _dbContext;
        public StaffReservationsForm()
        {
            InitializeComponent();
            _dbContext = ServiceFactory.GetDbContext();
        }
        private void StaffReservationsForm_Load(object sender, EventArgs e)
        {
            EnsureReservationsTableExists();
            SetupMetricIcons();
            LoadMetrics();
            LoadReservations();
            SetActiveFilter(btnFilterAll);
        }
        private void EnsureReservationsTableExists()
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string checkTableQuery = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES
                                              WHERE TABLE_SCHEMA = DATABASE()
                                              AND TABLE_NAME = 'Reservations'";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkTableQuery, conn))
                    {
                        int tableExists = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (tableExists == 0)
                        {
                            string createTableQuery = @"CREATE TABLE IF NOT EXISTS Reservations (
                                                        ReservationID INT AUTO_INCREMENT PRIMARY KEY,
                                                        MemberID INT NOT NULL,
                                                        BookID INT NOT NULL,
                                                        ReservationDate DATETIME NOT NULL,
                                                        PickupDate DATETIME NULL,
                                                        ExpiryDate DATETIME NULL,
                                                        Status VARCHAR(50) DEFAULT 'Pending',
                                                        Priority INT DEFAULT 0,
                                                        FulfilledDate DATETIME NULL,
                                                        FOREIGN KEY (MemberID) REFERENCES Members(MemberID),
                                                        FOREIGN KEY (BookID) REFERENCES Books(BookID)
                                                        )";
                            using (MySqlCommand createCmd = new MySqlCommand(createTableQuery, conn))
                            {
                                createCmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            AddColumnIfNotExists(conn, "Reservations", "ExpiryDate", "DATETIME NULL");
                            AddColumnIfNotExists(conn, "Reservations", "Priority", "INT DEFAULT 0");
                            AddColumnIfNotExists(conn, "Reservations", "FulfilledDate", "DATETIME NULL");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error ensuring Reservations table exists: {ex.Message}");
            }
        }
        private void AddColumnIfNotExists(MySqlConnection conn, string tableName, string columnName, string columnDefinition)
        {
            try
            {
                string checkColumnQuery = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS
                                          WHERE TABLE_SCHEMA = DATABASE()
                                          AND TABLE_NAME = @tableName
                                          AND COLUMN_NAME = @columnName";
                using (MySqlCommand checkCmd = new MySqlCommand(checkColumnQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@tableName", tableName);
                    checkCmd.Parameters.AddWithValue("@columnName", columnName);
                    int columnExists = Convert.ToInt32(checkCmd.ExecuteScalar());
                    if (columnExists == 0)
                    {
                        string alterQuery = $"ALTER TABLE {tableName} ADD COLUMN {columnName} {columnDefinition}";
                        using (MySqlCommand alterCmd = new MySqlCommand(alterQuery, conn))
                        {
                            alterCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error adding column {columnName}: {ex.Message}");
            }
        }
        private void SetupMetricIcons()
        {
        }
        private void LoadMetrics()
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string queryTotal = "SELECT COUNT(*) FROM Reservations";
                    using (MySqlCommand cmd = new MySqlCommand(queryTotal, conn))
                    {
                        int total = Convert.ToInt32(cmd.ExecuteScalar());
                        lblMetricTotalValue.Text = total.ToString();
                    }
                    string queryActive = "SELECT COUNT(*) FROM Reservations WHERE Status = 'Pending' OR Status = 'Active'";
                    using (MySqlCommand cmd = new MySqlCommand(queryActive, conn))
                    {
                        int active = Convert.ToInt32(cmd.ExecuteScalar());
                        lblMetricActiveValue.Text = active.ToString();
                        btnFilterActive.Text = $"Active ({active})";
                    }
                    string queryReady = "SELECT COUNT(*) FROM Reservations WHERE Status = 'Ready'";
                    using (MySqlCommand cmd = new MySqlCommand(queryReady, conn))
                    {
                        int ready = Convert.ToInt32(cmd.ExecuteScalar());
                        lblMetricReadyValue.Text = ready.ToString();
                        btnFilterReady.Text = $"Ready ({ready})";
                    }
                    string queryExpired = "SELECT COUNT(*) FROM Reservations WHERE Status = 'Expired'";
                    using (MySqlCommand cmd = new MySqlCommand(queryExpired, conn))
                    {
                        int expired = Convert.ToInt32(cmd.ExecuteScalar());
                        lblMetricExpiredValue.Text = expired.ToString();
                        btnFilterExpired.Text = $"Expired ({expired})";
                    }
                    int totalCount = Convert.ToInt32(lblMetricTotalValue.Text);
                    btnFilterAll.Text = $"All ({totalCount})";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading metrics: {ex.Message}");
            }
        }
        private void LoadReservations()
        {
            try
            {
                allReservationsData = GetReservationsData();
                UpdateReservationStatuses();
                RenderReservationCards();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading reservations: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private DataTable GetReservationsData()
        {
            using (var conn = _dbContext.GetConnection())
            {
                conn.Open();
                bool hasExpiryDate = DatabaseSchemaHelper.CheckColumnExists(conn, "Reservations", "ExpiryDate");
                bool hasPriority = DatabaseSchemaHelper.CheckColumnExists(conn, "Reservations", "Priority");
                bool hasReservationDate = DatabaseSchemaHelper.CheckColumnExists(conn, "Reservations", "ReservationDate");
                string reservationDateColumn = hasReservationDate ? "r.ReservationDate" :
                    (DatabaseSchemaHelper.CheckColumnExists(conn, "Reservations", "ReservedDate") ? "r.ReservedDate" :
                    (DatabaseSchemaHelper.CheckColumnExists(conn, "Reservations", "Date") ? "r.Date" : "NOW()"));
                string reservationDateAlias = "ReservationDate";
                string query = $@"SELECT
                                r.ReservationID,
                                r.MemberID,
                                r.BookID,
                                {reservationDateColumn} as {reservationDateAlias},
                                r.PickupDate,
                                " + (hasExpiryDate ? "r.ExpiryDate," : "NULL as ExpiryDate,") + @"
                                r.Status,
                                " + (hasPriority ? "r.Priority," : "0 as Priority,") + @"
                                m.FirstName,
                                m.LastName,
                                b.Title,
                                b.AccessionNo
                                FROM Reservations r
                                INNER JOIN Members m ON r.MemberID = m.MemberID
                                INNER JOIN Books b ON r.BookID = b.BookID
                                ORDER BY " + reservationDateColumn + " DESC";
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }
        private void UpdateReservationStatuses()
        {
            foreach (DataRow row in allReservationsData.Rows)
            {
                string status = row["Status"]?.ToString() ?? "Pending";
                if ((status == "Pending" || status == "Ready") && row["ExpiryDate"] != DBNull.Value)
                {
                    DateTime expiryDate = Convert.ToDateTime(row["ExpiryDate"]);
                    if (expiryDate < DateTime.Now)
                    {
                        try
                        {
                            using (var conn = _dbContext.GetConnection())
                            {
                                conn.Open();
                                string updateQuery = "UPDATE Reservations SET Status = 'Expired' WHERE ReservationID = @ReservationID";
                                using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                                {
                                    cmd.Parameters.AddWithValue("@ReservationID", row["ReservationID"]);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            row["Status"] = "Expired";
                        }
                        catch { }
                    }
                }
                if (row["ExpiryDate"] == DBNull.Value && row["ReservationDate"] != DBNull.Value)
                {
                    DateTime reservationDate = Convert.ToDateTime(row["ReservationDate"]);
                    row["ExpiryDate"] = reservationDate.AddDays(7);
                }
            }
        }
        private void RenderReservationCards()
        {
            panelReservationsList.Controls.Clear();
            reservationCards.Clear();
            DataView dv = allReservationsData.DefaultView;
            if (currentFilter == "Active")
            {
                dv.RowFilter = "Status = 'Pending' OR Status = 'Active'";
            }
            else if (currentFilter == "Ready")
            {
                dv.RowFilter = "Status = 'Ready'";
            }
            else if (currentFilter == "Expired")
            {
                dv.RowFilter = "Status = 'Expired'";
            }
            else
            {
                dv.RowFilter = "";
            }
            
            // Position cards vertically with proper spacing
            int yPosition = 16; // Start after padding (top padding is 16)
            int cardHeight = 100;
            int cardSpacing = 10;
            // Calculate card width: panel width minus left padding (15) minus right padding (15) minus scrollbar (20)
            int cardWidth = Math.Max(1100, panelReservationsList.Width - 50);
            
            foreach (DataRowView rowView in dv)
            {
                DataRow row = rowView.Row;
                Panel card = CreateReservationCard(row, cardWidth);
                
                // Set card position
                card.Location = new Point(15, yPosition); // 15 for left padding
                card.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                
                panelReservationsList.Controls.Add(card);
                reservationCards[Convert.ToInt32(row["ReservationID"])] = card;
                
                // Move to next position
                yPosition += cardHeight + cardSpacing;
            }
            
            // Ensure panel can scroll if content exceeds visible area
            panelReservationsList.AutoScroll = true;
            
            // Refresh panel to ensure all cards are visible
            panelReservationsList.Refresh();
            Application.DoEvents();
            
            System.Diagnostics.Debug.WriteLine($"Rendered {dv.Count} reservation cards. Total height: {yPosition}, Panel height: {panelReservationsList.Height}");
        }
        private Panel CreateReservationCard(DataRow row, int cardWidth = 1100)
        {
            int reservationId = Convert.ToInt32(row["ReservationID"]);
            string status = row["Status"]?.ToString() ?? "Pending";
            string bookTitle = row["Title"]?.ToString() ?? "";
            string firstName = row["FirstName"]?.ToString() ?? "";
            string lastName = row["LastName"]?.ToString() ?? "";
            int memberId = Convert.ToInt32(row["MemberID"]);
            DateTime reservationDate = row["ReservationDate"] != DBNull.Value ? Convert.ToDateTime(row["ReservationDate"]) : DateTime.Now;
            DateTime expiryDate = row["ExpiryDate"] != DBNull.Value ? Convert.ToDateTime(row["ExpiryDate"]) : reservationDate.AddDays(7);
            int priority = row["Priority"] != DBNull.Value ? Convert.ToInt32(row["Priority"]) : 0;
            
            Panel card = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(cardWidth, 100),
                Margin = new Padding(0),
                Tag = reservationId
            };
            Panel iconPanel = new Panel
            {
                Location = new Point(20, 20),
                Size = new Size(40, 40),
                BackColor = Color.Transparent
            };
            iconPanel.Paint += (s, e) => DrawStatusIcon(e.Graphics, iconPanel, status);
            card.Controls.Add(iconPanel);
            // Truncate book title if too long to ensure all information is visible
            string displayTitle = TextHelper.TruncateText(bookTitle);
            Label lblBookTitle = new Label
            {
                Text = displayTitle,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                Location = new Point(80, 15),
                AutoSize = false,
                Size = new Size(600, 24),
                TextAlign = ContentAlignment.MiddleLeft
            };
            card.Controls.Add(lblBookTitle);
            Panel statusBadge = new Panel
            {
                Location = new Point(250, 15),
                Size = new Size(80, 25),
                BackColor = GetStatusColor(status)
            };
            statusBadge.Paint += (s, e) =>
            {
                using (GraphicsPath path = new GraphicsPath())
                {
                    int radius = 12;
                    path.AddArc(0, 0, radius, radius, 180, 90);
                    path.AddArc(statusBadge.Width - radius, 0, radius, radius, 270, 90);
                    path.AddArc(statusBadge.Width - radius, statusBadge.Height - radius, radius, radius, 0, 90);
                    path.AddArc(0, statusBadge.Height - radius, radius, radius, 90, 90);
                    path.CloseAllFigures();
                    e.Graphics.FillPath(new SolidBrush(GetStatusColor(status)), path);
                }
                TextRenderer.DrawText(e.Graphics, status, new Font("Segoe UI", 9, FontStyle.Bold),
                    statusBadge.ClientRectangle, Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            };
            card.Controls.Add(statusBadge);
            if (status == "Pending" || status == "Active")
            {
                Label lblQueue = new Label
                {
                    Text = $"Queue: #{priority}",
                    Font = new Font("Segoe UI", 9),
                    ForeColor = Color.FromArgb(128, 128, 128),
                    Location = new Point(340, 18),
                    AutoSize = true
                };
                card.Controls.Add(lblQueue);
            }
            Label lblMember = new Label
            {
                Text = $"{firstName} {lastName} (M{memberId})",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(128, 128, 128),
                Location = new Point(80, 45),
                AutoSize = false,
                Size = new Size(400, 20),
                TextAlign = ContentAlignment.MiddleLeft
            };
            card.Controls.Add(lblMember);
            
            // Calculate positions to prevent overlap
            // Layout from right to left: [Scrollbar 20px] [Buttons 190px] [Gap 15px] [Date Labels 160px] [Left Content]
            int scrollbarMargin = 20;
            int rightPadding = 15;
            int buttonWidth = 85; // Reduced to ensure visibility
            int buttonHeight = 35;
            int spacing = 10;
            int buttonAreaWidth = (buttonWidth * 2) + spacing; // Two buttons + spacing
            
            // Calculate button position from right edge
            int buttonX = cardWidth - scrollbarMargin - rightPadding - buttonAreaWidth;
            int buttonY = 30;
            
            // Ensure buttons are within card boundaries and fully visible
            if (buttonX < 0 || buttonX + buttonAreaWidth > cardWidth - scrollbarMargin - rightPadding)
            {
                // Recalculate if buttons would be cut off
                buttonX = Math.Max(10, cardWidth - scrollbarMargin - rightPadding - buttonAreaWidth);
                // If still too wide, reduce button size
                if (buttonX < 10)
                {
                    int availableWidth = cardWidth - scrollbarMargin - rightPadding - 20; // 20px for safety margin
                    buttonWidth = Math.Max(70, (availableWidth - spacing) / 2);
                    buttonAreaWidth = (buttonWidth * 2) + spacing;
                    buttonX = cardWidth - scrollbarMargin - rightPadding - buttonAreaWidth;
                }
            }
            
            // Date labels positioned before buttons with clear gap
            int dateLabelWidth = 160;
            int gapBetweenDatesAndButtons = 15;
            int leftContentEnd = 680; // Book title (80px start + 600px width)
            int dateLabelX = buttonX - dateLabelWidth - gapBetweenDatesAndButtons;
            
            // Ensure date labels don't overlap with left content
            if (dateLabelX < leftContentEnd)
            {
                // Reduce date label width to fit
                dateLabelWidth = Math.Max(140, buttonX - leftContentEnd - gapBetweenDatesAndButtons);
                dateLabelX = buttonX - dateLabelWidth - gapBetweenDatesAndButtons;
            }
            
            // Final validation: ensure date labels don't overlap with buttons
            if (dateLabelX + dateLabelWidth > buttonX - gapBetweenDatesAndButtons)
            {
                dateLabelWidth = Math.Max(120, buttonX - leftContentEnd - gapBetweenDatesAndButtons);
                dateLabelX = buttonX - dateLabelWidth - gapBetweenDatesAndButtons;
            }
            
            // Ensure buttons are fully visible (not cut off by scrollbar)
            int maxButtonX = cardWidth - scrollbarMargin - rightPadding - buttonAreaWidth;
            if (buttonX > maxButtonX)
            {
                buttonX = maxButtonX;
                // Recalculate date labels if button position changed
                dateLabelX = buttonX - dateLabelWidth - gapBetweenDatesAndButtons;
                if (dateLabelX < leftContentEnd)
                {
                    dateLabelWidth = Math.Max(120, buttonX - leftContentEnd - gapBetweenDatesAndButtons);
                    dateLabelX = buttonX - dateLabelWidth - gapBetweenDatesAndButtons;
                }
            }
            
            Label lblReserved = new Label
            {
                Text = $"Reserved: {reservationDate:yyyy-MM-dd}",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(64, 64, 64),
                Location = new Point(dateLabelX, 20),
                AutoSize = false,
                Size = new Size(dateLabelWidth, 20),
                TextAlign = ContentAlignment.MiddleLeft
            };
            card.Controls.Add(lblReserved);
            Label lblExpires = new Label
            {
                Text = $"Expires: {expiryDate:yyyy-MM-dd}",
                Font = new Font("Segoe UI", 9),
                ForeColor = status == "Expired" ? Color.FromArgb(220, 53, 69) : Color.FromArgb(64, 64, 64),
                Location = new Point(dateLabelX, 45),
                AutoSize = false,
                Size = new Size(dateLabelWidth, 20),
                TextAlign = ContentAlignment.MiddleLeft
            };
            card.Controls.Add(lblExpires);
            if (status == "Pending" || status == "Active")
            {
                // Staff role restriction: Only Admin can approve/manually mark reservations as ready
                // Staff can only view pending reservations and inform members, not approve them
                if (CurrentUser.Role != null && CurrentUser.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    Button btnMarkReady = new Button
                    {
                        Text = "Mark Ready",
                        BackColor = Color.FromArgb(40, 167, 69),
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat,
                        FlatAppearance = { BorderSize = 0 },
                        Font = new Font("Segoe UI", 9, FontStyle.Bold),
                        Location = new Point(buttonX, buttonY),
                        Size = new Size(buttonWidth, buttonHeight),
                        Tag = reservationId
                    };
                    btnMarkReady.Click += (s, e) => MarkAsReady(reservationId);
                    card.Controls.Add(btnMarkReady);
                }
                else
                {
                    // Staff can only view and inform - add "Contact Member" button
                    Button btnContactMember = new Button
                    {
                        Text = "📞 Contact",
                        BackColor = Color.FromArgb(13, 110, 253),
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat,
                        FlatAppearance = { BorderSize = 0 },
                        Font = new Font("Segoe UI", 9, FontStyle.Bold),
                        Location = new Point(buttonX, buttonY),
                        Size = new Size(buttonWidth, buttonHeight),
                        Tag = memberId,
                        Cursor = Cursors.Hand
                    };
                    btnContactMember.Click += (s, e) => ShowMemberContactInfo(memberId, firstName, lastName);
                    card.Controls.Add(btnContactMember);
                }
                Button btnCancel = new Button
                {
                    Text = "Cancel",
                    BackColor = Color.FromArgb(220, 220, 220),
                    ForeColor = Color.FromArgb(64, 64, 64),
                    FlatStyle = FlatStyle.Flat,
                    FlatAppearance = { BorderSize = 0 },
                    Font = new Font("Segoe UI", 9),
                    Location = new Point(buttonX + buttonWidth + spacing, buttonY),
                    Size = new Size(buttonWidth, buttonHeight),
                    Tag = reservationId
                };
                btnCancel.Click += (s, e) => CancelReservation(reservationId);
                card.Controls.Add(btnCancel);
            }
            else if (status == "Ready")
            {
                Button btnCheckOut = new Button
                {
                    Text = "Check Out",
                    BackColor = Color.FromArgb(178, 34, 34),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    FlatAppearance = { BorderSize = 0 },
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    Location = new Point(buttonX, buttonY),
                    Size = new Size(buttonWidth, buttonHeight),
                    Tag = reservationId
                };
                btnCheckOut.Click += (s, e) => CheckOutReservation(reservationId);
                card.Controls.Add(btnCheckOut);
                Button btnCancel = new Button
                {
                    Text = "Cancel",
                    BackColor = Color.FromArgb(220, 220, 220),
                    ForeColor = Color.FromArgb(64, 64, 64),
                    FlatStyle = FlatStyle.Flat,
                    FlatAppearance = { BorderSize = 0 },
                    Font = new Font("Segoe UI", 9),
                    Location = new Point(buttonX + buttonWidth + spacing, buttonY),
                    Size = new Size(buttonWidth, buttonHeight),
                    Tag = reservationId
                };
                btnCancel.Click += (s, e) => CancelReservation(reservationId);
                card.Controls.Add(btnCancel);
            }
            else if (status == "Expired")
            {
                Button btnRemove = new Button
                {
                    Text = "Remove",
                    BackColor = Color.FromArgb(108, 117, 125),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    FlatAppearance = { BorderSize = 0 },
                    Font = new Font("Segoe UI", 9),
                    Location = new Point(buttonX, buttonY),
                    Size = new Size(buttonWidth, buttonHeight),
                    Tag = reservationId
                };
                btnRemove.Click += (s, e) => RemoveReservation(reservationId);
                card.Controls.Add(btnRemove);
                Button btnCancel = new Button
                {
                    Text = "Cancel",
                    BackColor = Color.FromArgb(220, 220, 220),
                    ForeColor = Color.FromArgb(64, 64, 64),
                    FlatStyle = FlatStyle.Flat,
                    FlatAppearance = { BorderSize = 0 },
                    Font = new Font("Segoe UI", 9),
                    Location = new Point(buttonX + buttonWidth + spacing, buttonY),
                    Size = new Size(buttonWidth, buttonHeight),
                    Tag = reservationId
                };
                btnCancel.Click += (s, e) => CancelReservation(reservationId);
                card.Controls.Add(btnCancel);
            }
            return card;
        }
        private Color GetStatusColor(string status)
        {
            switch (status.ToLower())
            {
                case "pending":
                case "active":
                    return Color.FromArgb(13, 110, 253);
                case "ready":
                    return Color.FromArgb(40, 167, 69);
                case "expired":
                    return Color.FromArgb(220, 53, 69);
                default:
                    return Color.LightGray;
            }
        }
        private void DrawStatusIcon(Graphics g, Panel panel, string status)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int size = Math.Min(panel.Width, panel.Height) - 5;
            int x = (panel.Width - size) / 2;
            int y = (panel.Height - size) / 2;
            using (Pen pen = new Pen(GetStatusColor(status), 3))
            {
                switch (status.ToLower())
                {
                    case "pending":
                    case "active":
                        g.DrawEllipse(pen, x + size * 0.2f, y + size * 0.2f, size * 0.6f, size * 0.6f);
                        g.DrawLine(pen, x + size * 0.5f, y + size * 0.5f, x + size * 0.5f, y + size * 0.35f);
                        g.DrawLine(pen, x + size * 0.5f, y + size * 0.5f, x + size * 0.65f, y + size * 0.5f);
                        break;
                    case "ready":
                        g.DrawEllipse(pen, x + size * 0.2f, y + size * 0.2f, size * 0.6f, size * 0.6f);
                        g.DrawLine(pen, x + size * 0.35f, y + size * 0.5f, x + size * 0.45f, y + size * 0.6f);
                        g.DrawLine(pen, x + size * 0.45f, y + size * 0.6f, x + size * 0.65f, y + size * 0.4f);
                        break;
                    case "expired":
                        g.DrawEllipse(pen, x + size * 0.2f, y + size * 0.2f, size * 0.6f, size * 0.6f);
                        g.DrawLine(pen, x + size * 0.35f, y + size * 0.35f, x + size * 0.65f, y + size * 0.65f);
                        g.DrawLine(pen, x + size * 0.65f, y + size * 0.35f, x + size * 0.35f, y + size * 0.65f);
                        break;
                }
            }
        }
        private void btnFilterAll_Click(object sender, EventArgs e)
        {
            currentFilter = "All";
            SetActiveFilter(btnFilterAll);
            RenderReservationCards();
        }
        private void btnFilterActive_Click(object sender, EventArgs e)
        {
            currentFilter = "Active";
            SetActiveFilter(btnFilterActive);
            RenderReservationCards();
        }
        private void btnFilterReady_Click(object sender, EventArgs e)
        {
            currentFilter = "Ready";
            SetActiveFilter(btnFilterReady);
            RenderReservationCards();
        }
        private void btnFilterExpired_Click(object sender, EventArgs e)
        {
            currentFilter = "Expired";
            SetActiveFilter(btnFilterExpired);
            RenderReservationCards();
        }
        private void SetActiveFilter(Button activeButton)
        {
            btnFilterAll.BackColor = Color.Transparent;
            btnFilterAll.ForeColor = Color.FromArgb(128, 128, 128);
            btnFilterAll.Font = new Font("Segoe UI", 10F);
            btnFilterActive.BackColor = Color.Transparent;
            btnFilterActive.ForeColor = Color.FromArgb(128, 128, 128);
            btnFilterActive.Font = new Font("Segoe UI", 10F);
            btnFilterReady.BackColor = Color.Transparent;
            btnFilterReady.ForeColor = Color.FromArgb(128, 128, 128);
            btnFilterReady.Font = new Font("Segoe UI", 10F);
            btnFilterExpired.BackColor = Color.Transparent;
            btnFilterExpired.ForeColor = Color.FromArgb(128, 128, 128);
            btnFilterExpired.Font = new Font("Segoe UI", 10F);
            activeButton.BackColor = Color.Transparent;
            activeButton.ForeColor = Color.FromArgb(178, 34, 34);
            activeButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        }
        private void MarkAsReady(int reservationId)
        {
            // Staff role restriction: Only Admin can approve/manually mark reservations as ready
            if (CurrentUser.Role == null || !CurrentUser.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Only administrators can approve reservations. Staff can only view reservations and check out ready items.", 
                    "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                AuditLogger.LogAccessControl("Approve Reservation attempt denied", 
                    $"User: {CurrentUser.Email}, Role: {CurrentUser.Role}", "Failed");
                return;
            }
            
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string updateQuery = "UPDATE Reservations SET Status = 'Ready' WHERE ReservationID = @ReservationID";
                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@ReservationID", reservationId);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Reservation marked as ready for pickup.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadMetrics();
                LoadReservations();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error marking reservation as ready: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ShowMemberContactInfo(int memberId, string firstName, string lastName)
        {
            try
            {
                var membersService = ServiceFactory.CreateMembersService();
                var member = membersService.GetMember(memberId);
                
                if (member == null)
                {
                    MessageBox.Show("Member information not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // Build contact information message
                string contactInfo = $"Member Contact Information\n\n";
                contactInfo += $"Name: {member.FullName}\n";
                contactInfo += $"Member ID: {IDFormatter.FormatMemberID(member.MemberID)}\n";
                contactInfo += $"Member Type: {member.Type ?? "N/A"}\n\n";
                contactInfo += "Contact Details:\n";
                contactInfo += $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n";
                
                if (!string.IsNullOrWhiteSpace(member.Email))
                {
                    contactInfo += $"📧 Email: {member.Email}\n";
                }
                else
                {
                    contactInfo += $"📧 Email: Not provided\n";
                }
                
                if (!string.IsNullOrWhiteSpace(member.Contact))
                {
                    contactInfo += $"📱 Phone: {member.Contact}\n";
                }
                else
                {
                    contactInfo += $"📱 Phone: Not provided\n";
                }
                
                if (!string.IsNullOrWhiteSpace(member.Address))
                {
                    contactInfo += $"📍 Address: {member.Address}\n";
                }
                
                contactInfo += $"\n━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n";
                contactInfo += $"Use this information to inform the member about their reservation.";
                
                // Create a custom dialog to display contact info
                Form contactDialog = new Form
                {
                    Text = "Member Contact Information",
                    Size = new Size(500, 350),
                    StartPosition = FormStartPosition.CenterParent,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    MaximizeBox = false,
                    MinimizeBox = false,
                    ShowInTaskbar = false
                };
                
                Panel panelMain = new Panel
                {
                    Dock = DockStyle.Fill,
                    Padding = new Padding(20),
                    BackColor = Color.White
                };
                
                Label lblInfo = new Label
                {
                    Text = contactInfo,
                    Font = new Font("Segoe UI", 10),
                    ForeColor = Color.FromArgb(64, 64, 64),
                    Location = new Point(20, 20),
                    Size = new Size(440, 220),
                    AutoSize = false
                };
                panelMain.Controls.Add(lblInfo);
                
                // Add copy email button if email exists
                if (!string.IsNullOrWhiteSpace(member.Email))
                {
                    Button btnCopyEmail = new Button
                    {
                        Text = "📋 Copy Email",
                        BackColor = Color.FromArgb(13, 110, 253),
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat,
                        FlatAppearance = { BorderSize = 0 },
                        Font = new Font("Segoe UI", 9, FontStyle.Bold),
                        Location = new Point(20, 250),
                        Size = new Size(130, 35),
                        Cursor = Cursors.Hand
                    };
                    btnCopyEmail.Click += (s, e) =>
                    {
                        Clipboard.SetText(member.Email);
                        MessageBox.Show("Email address copied to clipboard!", "Copied", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    };
                    panelMain.Controls.Add(btnCopyEmail);
                }
                
                // Add copy phone button if phone exists
                if (!string.IsNullOrWhiteSpace(member.Contact))
                {
                    Button btnCopyPhone = new Button
                    {
                        Text = "📋 Copy Phone",
                        BackColor = Color.FromArgb(13, 110, 253),
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat,
                        FlatAppearance = { BorderSize = 0 },
                        Font = new Font("Segoe UI", 9, FontStyle.Bold),
                        Location = new Point(160, 250),
                        Size = new Size(130, 35),
                        Cursor = Cursors.Hand
                    };
                    btnCopyPhone.Click += (s, e) =>
                    {
                        Clipboard.SetText(member.Contact);
                        MessageBox.Show("Phone number copied to clipboard!", "Copied", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    };
                    panelMain.Controls.Add(btnCopyPhone);
                }
                
                Button btnClose = new Button
                {
                    Text = "Close",
                    BackColor = Color.FromArgb(108, 117, 125),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    FlatAppearance = { BorderSize = 0 },
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    Location = new Point(380, 250),
                    Size = new Size(80, 35),
                    DialogResult = DialogResult.OK,
                    Cursor = Cursors.Hand
                };
                panelMain.Controls.Add(btnClose);
                
                contactDialog.Controls.Add(panelMain);
                contactDialog.AcceptButton = btnClose;
                
                // Log access
                AuditLogger.LogAccessControl("Member contact info viewed", 
                    $"User: {CurrentUser.Email}, MemberID: {memberId}", "Success");
                
                contactDialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving member contact information: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Error in ShowMemberContactInfo: {ex.Message}");
            }
        }
        
        private void CheckOutReservation(int reservationId)
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string getBookQuery = "SELECT BookID FROM Reservations WHERE ReservationID = @ReservationID";
                    int bookId = 0;
                    using (MySqlCommand getCmd = new MySqlCommand(getBookQuery, conn))
                    {
                        getCmd.Parameters.AddWithValue("@ReservationID", reservationId);
                        object result = getCmd.ExecuteScalar();
                        if (result != null)
                        {
                            bookId = Convert.ToInt32(result);
                        }
                    }
                    string updateQuery = "UPDATE Reservations SET Status = 'Fulfilled', FulfilledDate = @FulfilledDate WHERE ReservationID = @ReservationID";
                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@ReservationID", reservationId);
                        cmd.Parameters.AddWithValue("@FulfilledDate", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                    if (bookId > 0)
                    {
                        string updateBookQuery = "UPDATE Books SET Available = Available - 1 WHERE BookID = @BookID AND Available > 0";
                        using (MySqlCommand cmd = new MySqlCommand(updateBookQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@BookID", bookId);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                MessageBox.Show("Reservation checked out successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadMetrics();
                LoadReservations();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking out reservation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CancelReservation(int reservationId)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to cancel this reservation❓", "Confirm Cancellation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string deleteQuery = "DELETE FROM Reservations WHERE ReservationID = @ReservationID";
                    using (MySqlCommand cmd = new MySqlCommand(deleteQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@ReservationID", reservationId);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Reservation cancelled successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadMetrics();
                LoadReservations();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cancelling reservation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void RemoveReservation(int reservationId)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to remove this expired reservation❓", "Confirm Removal", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string deleteQuery = "DELETE FROM Reservations WHERE ReservationID = @ReservationID";
                    using (MySqlCommand cmd = new MySqlCommand(deleteQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@ReservationID", reservationId);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Reservation removed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadMetrics();
                LoadReservations();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error removing reservation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtMemberID_Enter(object sender, EventArgs e)
        {
            if (txtMemberID.Text == "Enter member ID")
            {
                txtMemberID.Text = "";
                txtMemberID.ForeColor = Color.Black;
            }
        }
        private void txtMemberID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMemberID.Text))
            {
                txtMemberID.Text = "Enter member ID";
                txtMemberID.ForeColor = Color.Gray;
            }
        }
        private void txtBookID_Enter(object sender, EventArgs e)
        {
            if (txtBookID.Text == "Enter book ID or title")
            {
                txtBookID.Text = "";
                txtBookID.ForeColor = Color.Black;
            }
        }
        private void txtBookID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBookID.Text))
            {
                txtBookID.Text = "Enter book ID or title";
                txtBookID.ForeColor = Color.Gray;
            }
        }
        private void btnCreateReservation_Click(object sender, EventArgs e)
        {
            string memberIdText = txtMemberID.Text.Trim();
            string bookIdText = txtBookID.Text.Trim();
            if (memberIdText == "Enter member ID" || string.IsNullOrWhiteSpace(memberIdText))
            {
                MessageBox.Show("Please enter a Member ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (bookIdText == "Enter book ID or title" || string.IsNullOrWhiteSpace(bookIdText))
            {
                MessageBox.Show("Please enter a Book ID or Book Title.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                int memberId = Project5LMS.Helpers.IDFormatter.ParseMemberID(memberIdText);
                int bookId = 0;
                var bookService = ServiceFactory.CreateBookService();
                Book book = null;
                
                // Check if input looks like an ID (purely numeric, or accession pattern like ACC-####)
                // This helps prioritize ID search for ID-like inputs, but title search always works
                bool looksLikeId = (bookIdText.Length <= 10 && int.TryParse(bookIdText, out int parsedId)) || 
                                   (bookIdText.StartsWith("ACC-", StringComparison.OrdinalIgnoreCase) && bookIdText.Length <= 20) ||
                                   (bookIdText.StartsWith("B", StringComparison.OrdinalIgnoreCase) && bookIdText.Length <= 10 && int.TryParse(bookIdText.Substring(1), out _));
                
                // If it looks like an ID, try ID-based searches first (but title search is always available as fallback)
                if (looksLikeId)
                {
                    // Try to find by Accession Number
                    book = bookService.GetBookByAccessionNumber(bookIdText);
                    if (book != null)
                    {
                        bookId = book.BookID;
                    }
                    else
                    {
                        // Try to find by Book ID
                        bookId = Project5LMS.Helpers.IDFormatter.ParseBookID(bookIdText);
                        if (bookId > 0)
                        {
                            var bookById = bookService.GetBook(bookId);
                            if (bookById != null)
                            {
                                book = bookById;
                            }
                            else
                            {
                                bookId = 0;
                            }
                        }
                    }
                }
                
                // Always search by Title/Author/ISBN/AccessionNo if:
                // 1. Input doesn't look like an ID, OR
                // 2. ID search didn't find anything
                // This ensures title search works even when there's no Book ID
                if (bookId == 0 || book == null)
                {
                    var searchResults = bookService.SearchBooks(bookIdText);
                    var booksList = searchResults.ToList();
                    
                    if (booksList.Count == 0)
                    {
                        MessageBox.Show($"No book found matching '{bookIdText}'.\n\nPlease check:\n- Book ID format\n- Book Title spelling\n- Accession Number", "Book Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (booksList.Count == 1)
                    {
                        // Single match found - use it
                        book = booksList.First();
                        bookId = book.BookID;
                    }
                    else
                    {
                        // Multiple matches found - show selection dialog with more details
                        string bookList = string.Join("\n", booksList.Take(10).Select((b, idx) => 
                            $"{idx + 1}. \"{b.Title}\" by {b.Author} (ID: {b.BookID}, Accession: {b.AccessionNo ?? "N/A"})"));
                        string message = $"Multiple books found matching '{bookIdText}':\n\n{bookList}";
                        if (booksList.Count > 10)
                        {
                            message += $"\n\n... and {booksList.Count - 10} more. Please enter a more specific Book ID or Title.";
                        }
                        else
                        {
                            message += "\n\nPlease enter a more specific Book ID or Title, or use the Book ID from the list above.";
                        }
                        MessageBox.Show(message, "Multiple Books Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                
                if (memberId == 0 || bookId == 0 || book == null)
                {
                    MessageBox.Show("Invalid Member ID or Book ID/Title.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                bool memberExists = false;
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string checkMemberQuery = "SELECT COUNT(*) FROM Members WHERE MemberID = @MemberID";
                    using (MySqlCommand cmd = new MySqlCommand(checkMemberQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", memberId);
                        memberExists = Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                    }
                    if (!memberExists)
                    {
                        MessageBox.Show("Member not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    string checkBookQuery = "SELECT COUNT(*) FROM Books WHERE BookID = @BookID";
                    bool bookExists = false;
                    using (MySqlCommand cmd = new MySqlCommand(checkBookQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookID", bookId);
                        bookExists = Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                    }
                    if (!bookExists)
                    {
                        MessageBox.Show("Book not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    string checkReservationQuery = "SELECT COUNT(*) FROM Reservations WHERE MemberID = @MemberID AND BookID = @BookID AND Status IN ('Pending', 'Ready', 'Active')";
                    bool reservationExists = false;
                    using (MySqlCommand cmd = new MySqlCommand(checkReservationQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", memberId);
                        cmd.Parameters.AddWithValue("@BookID", bookId);
                        reservationExists = Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                    }
                    if (reservationExists)
                    {
                        MessageBox.Show("An active reservation already exists for this member and book.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    bool hasExpiryDate = DatabaseSchemaHelper.CheckColumnExists(conn, "Reservations", "ExpiryDate");
                    bool hasReservationDate = DatabaseSchemaHelper.CheckColumnExists(conn, "Reservations", "ReservationDate");
                    string dateColumn = hasReservationDate ? "ReservationDate" :
                        (DatabaseSchemaHelper.CheckColumnExists(conn, "Reservations", "ReservedDate") ? "ReservedDate" : "Date");
                    string insertQuery;
                    if (hasExpiryDate)
                    {
                        insertQuery = $@"INSERT INTO Reservations (MemberID, BookID, {dateColumn}, ExpiryDate, Status)
                                       VALUES (@MemberID, @BookID, @ReservationDate, @ExpiryDate, 'Pending')";
                    }
                    else
                    {
                        insertQuery = $@"INSERT INTO Reservations (MemberID, BookID, {dateColumn}, Status)
                                       VALUES (@MemberID, @BookID, @ReservationDate, 'Pending')";
                    }
                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", memberId);
                        cmd.Parameters.AddWithValue("@BookID", bookId);
                        cmd.Parameters.AddWithValue("@ReservationDate", DateTime.Now);
                        if (hasExpiryDate)
                        {
                            cmd.Parameters.AddWithValue("@ExpiryDate", DateTime.Now.AddDays(7));
                        }
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Reservation created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMemberID.Text = "Enter member ID";
                txtMemberID.ForeColor = Color.Gray;
                txtBookID.Text = "Enter book ID or title";
                txtBookID.ForeColor = Color.Gray;
                LoadMetrics();
                LoadReservations();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating reservation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DrawCalendarIcon(Graphics g, Panel panel)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int size = Math.Min(panel.Width, panel.Height) - 10;
            int x = (panel.Width - size) / 2;
            int y = (panel.Height - size) / 2;
            using (Pen pen = new Pen(Color.White, 2))
            {
                g.DrawRectangle(pen, x + size * 0.2f, y + size * 0.3f, size * 0.6f, size * 0.6f);
                g.DrawLine(pen, x + size * 0.2f, y + size * 0.45f, x + size * 0.8f, y + size * 0.45f);
            }
        }
        private void DrawClockIcon(Graphics g, Panel panel)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int size = Math.Min(panel.Width, panel.Height) - 10;
            int x = (panel.Width - size) / 2;
            int y = (panel.Height - size) / 2;
            using (Pen pen = new Pen(Color.White, 2))
            {
                g.DrawEllipse(pen, x + size * 0.2f, y + size * 0.2f, size * 0.6f, size * 0.6f);
                g.DrawLine(pen, x + size * 0.5f, y + size * 0.5f, x + size * 0.5f, y + size * 0.35f);
                g.DrawLine(pen, x + size * 0.5f, y + size * 0.5f, x + size * 0.65f, y + size * 0.5f);
            }
        }
        private void DrawCheckmarkIcon(Graphics g, Panel panel)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int size = Math.Min(panel.Width, panel.Height) - 10;
            int x = (panel.Width - size) / 2;
            int y = (panel.Height - size) / 2;
            using (Pen pen = new Pen(Color.White, 3))
            {
                g.DrawEllipse(pen, x + size * 0.2f, y + size * 0.2f, size * 0.6f, size * 0.6f);
                g.DrawLine(pen, x + size * 0.35f, y + size * 0.5f, x + size * 0.45f, y + size * 0.6f);
                g.DrawLine(pen, x + size * 0.45f, y + size * 0.6f, x + size * 0.65f, y + size * 0.4f);
            }
        }
        private void DrawXIcon(Graphics g, Panel panel)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int size = Math.Min(panel.Width, panel.Height) - 10;
            int x = (panel.Width - size) / 2;
            int y = (panel.Height - size) / 2;
            using (Pen pen = new Pen(Color.White, 3))
            {
                g.DrawEllipse(pen, x + size * 0.2f, y + size * 0.2f, size * 0.6f, size * 0.6f);
                g.DrawLine(pen, x + size * 0.35f, y + size * 0.35f, x + size * 0.65f, y + size * 0.65f);
                g.DrawLine(pen, x + size * 0.65f, y + size * 0.35f, x + size * 0.35f, y + size * 0.65f);
            }
        }
    }
}