using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Project5LMS.Helpers;
using Project5LMS.Data;

namespace Project5LMS.Forms.Admin.Reservations
{
    public partial class AdminReservationsForm : Form
    {
        private readonly DatabaseContext _dbContext;
        private DataTable allReservationsData;
        private string currentFilter = "All Status";

        public AdminReservationsForm()
        {
            InitializeComponent();
            _dbContext = new DatabaseContext();
        }

        private void AdminReservationsForm_Load(object sender, EventArgs e)
        {
            EnsureReservationsTableExists();
            SetupDataGridView();
            LoadMetrics();
            LoadReservations();
        }

        private void EnsureReservationsTableExists()
        {
            try
            {
                var dbContext = new DatabaseContext();
                using (var conn = dbContext.GetConnection())
                {
                    conn.Open();

                    string checkTableQuery = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES 
                                              WHERE TABLE_SCHEMA = DATABASE() 
                                              AND TABLE_NAME = 'Reservations'";
                    using (var checkCmd = new MySqlCommand(checkTableQuery, conn))
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
                            dbContext.ExecuteNonQuery(createTableQuery);
                        }
                        else
                        {
                            DatabaseSchemaHelper.AddColumnIfNotExists(conn, "Reservations", "ExpiryDate", "DATETIME NULL");
                            DatabaseSchemaHelper.AddColumnIfNotExists(conn, "Reservations", "Priority", "INT DEFAULT 0");
                            DatabaseSchemaHelper.AddColumnIfNotExists(conn, "Reservations", "FulfilledDate", "DATETIME NULL");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error ensuring Reservations table exists: {ex.Message}");
            }
        }

        private void DrawMetricIcon(Graphics g, Panel panel, string icon)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (Font font = new Font("Segoe UI", 18, FontStyle.Bold))
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
            dataGridViewReservations.Columns.Clear();
            dataGridViewReservations.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn colReservationID = new DataGridViewTextBoxColumn
            {
                Name = "ReservationID",
                HeaderText = "RESERVATION ID",
                DataPropertyName = "ReservationID",
                Width = 150,
                ReadOnly = true
            };
            dataGridViewReservations.Columns.Add(colReservationID);

            DataGridViewTextBoxColumn colMember = new DataGridViewTextBoxColumn
            {
                Name = "Member",
                HeaderText = "MEMBER",
                DataPropertyName = "Member",
                Width = 200,
                ReadOnly = true
            };
            dataGridViewReservations.Columns.Add(colMember);

            DataGridViewTextBoxColumn colBook = new DataGridViewTextBoxColumn
            {
                Name = "Book",
                HeaderText = "BOOK",
                DataPropertyName = "Book",
                Width = 300,
                ReadOnly = true
            };
            dataGridViewReservations.Columns.Add(colBook);

            DataGridViewTextBoxColumn colReservedDate = new DataGridViewTextBoxColumn
            {
                Name = "ReservedDate",
                HeaderText = "RESERVED DATE",
                DataPropertyName = "ReservedDate",
                Width = 130,
                ReadOnly = true
            };
            dataGridViewReservations.Columns.Add(colReservedDate);

            DataGridViewTextBoxColumn colExpiryDate = new DataGridViewTextBoxColumn
            {
                Name = "ExpiryDate",
                HeaderText = "EXPIRY DATE",
                DataPropertyName = "ExpiryDate",
                Width = 130,
                ReadOnly = true
            };
            dataGridViewReservations.Columns.Add(colExpiryDate);

            DataGridViewColumn colPriority = new DataGridViewTextBoxColumn
            {
                Name = "Priority",
                HeaderText = "PRIORITY",
                DataPropertyName = "Priority",
                Width = 100,
                ReadOnly = true
            };
            dataGridViewReservations.Columns.Add(colPriority);

            DataGridViewColumn colStatus = new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "STATUS",
                DataPropertyName = "Status",
                Width = 120,
                ReadOnly = true
            };
            dataGridViewReservations.Columns.Add(colStatus);

            DataGridViewColumn colActions = new DataGridViewTextBoxColumn
            {
                Name = "Actions",
                HeaderText = "ACTIONS",
                DataPropertyName = "Actions",
                Width = 200,
                ReadOnly = true
            };
            dataGridViewReservations.Columns.Add(colActions);

            dataGridViewReservations.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridViewReservations.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridViewReservations.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dataGridViewReservations.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            dataGridViewReservations.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            dataGridViewReservations.RowTemplate.Height = 60;
            dataGridViewReservations.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dataGridViewReservations.CellFormatting += DataGridViewReservations_CellFormatting;
            dataGridViewReservations.CellPainting += DataGridViewReservations_CellPainting;
            dataGridViewReservations.CellContentClick += DataGridViewReservations_CellContentClick;
        }

        private void DataGridViewReservations_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dataGridViewReservations.Rows[e.RowIndex];
            string columnName = dataGridViewReservations.Columns[e.ColumnIndex].Name;

            if (columnName == "ReservationID" && e.Value != null)
            {
                string reservationIdStr = e.Value.ToString();
                if (int.TryParse(reservationIdStr, out int reservationId))
                {
                    e.Value = $"RES-{reservationIdStr.PadLeft(3, '0')}";
                }
                e.FormattingApplied = true;
            }
        }

        private void DataGridViewReservations_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string columnName = dataGridViewReservations.Columns[e.ColumnIndex].Name;
            DataGridViewRow row = dataGridViewReservations.Rows[e.RowIndex];

            if (columnName == "Priority")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                string value = e.Value?.ToString() ?? "";
                if (!string.IsNullOrEmpty(value) && value != "0")
                {

                    Rectangle badgeRect = new Rectangle(
                        e.CellBounds.X + 5,
                        e.CellBounds.Y + (e.CellBounds.Height - 25) / 2,
                        Math.Min(60, e.CellBounds.Width - 10),
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

                        using (SolidBrush brush = new SolidBrush(Color.FromArgb(221, 160, 221)))
                        {
                            e.Graphics.FillPath(brush, path);
                        }
                    }

                    TextRenderer.DrawText(
                        e.Graphics,
                        $"#{value}",
                        dataGridViewReservations.DefaultCellStyle.Font,
                        badgeRect,
                        Color.White,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                    );
                }

                e.Handled = true;
            }

            if (columnName == "Status")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                string value = e.Value?.ToString() ?? "";
                Color bgColor = Color.LightGray;
                Color textColor = Color.Black;
                string icon = "";

                switch (value.ToLower())
                {
                    case "pending":
                        bgColor = Color.FromArgb(255, 193, 7);
                        textColor = Color.Black;
                        icon = "?";
                        break;
                    case "ready":
                        bgColor = Color.FromArgb(40, 167, 69);
                        textColor = Color.White;
                        icon = "?";
                        break;
                    case "fulfilled":
                        bgColor = Color.FromArgb(13, 110, 253);
                        textColor = Color.White;
                        icon = "?";
                        break;
                    case "expired":
                        bgColor = Color.FromArgb(220, 53, 69);
                        textColor = Color.White;
                        icon = "?";
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

                if (!string.IsNullOrEmpty(icon))
                {
                    Rectangle iconRect = new Rectangle(badgeRect.X + 5, badgeRect.Y, 15, badgeRect.Height);
                    using (Font iconFont = new Font("Segoe UI", 12, FontStyle.Bold))
                    using (Brush brush = new SolidBrush(textColor))
                    {
                        e.Graphics.DrawString(icon, iconFont, brush, iconRect);
                    }

                    Rectangle textRect = new Rectangle(badgeRect.X + 20, badgeRect.Y, badgeRect.Width - 25, badgeRect.Height);
                    TextRenderer.DrawText(
                        e.Graphics,
                        value,
                        dataGridViewReservations.DefaultCellStyle.Font,
                        textRect,
                        textColor,
                        TextFormatFlags.VerticalCenter
                    );
                }
                else
                {
                    TextRenderer.DrawText(
                        e.Graphics,
                        value,
                        dataGridViewReservations.DefaultCellStyle.Font,
                        badgeRect,
                        textColor,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                    );
                }

                e.Handled = true;
            }

            if (columnName == "Actions")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                string status = row.Cells["Status"]?.Value?.ToString() ?? "";
                int reservationId = Convert.ToInt32(row.Cells["ReservationID"]?.Value ?? 0);

                int buttonY = e.CellBounds.Y + (e.CellBounds.Height - 30) / 2;
                int buttonHeight = 30;
                int buttonWidth = 80;
                int spacing = 5;
                int xOffset = e.CellBounds.X + 5;

                if (status.ToLower() == "pending")
                {

                    Rectangle btnReadyRect = new Rectangle(xOffset, buttonY, buttonWidth, buttonHeight);
                    DrawButton(e.Graphics, btnReadyRect, "Mark Ready", Color.FromArgb(40, 167, 69), Color.White);

                    Rectangle btnCancelRect = new Rectangle(xOffset + buttonWidth + spacing, buttonY, buttonWidth, buttonHeight);
                    DrawButton(e.Graphics, btnCancelRect, "Cancel", Color.FromArgb(220, 53, 69), Color.White);
                }
                else if (status.ToLower() == "ready")
                {

                    Rectangle btnFulfillRect = new Rectangle(xOffset, buttonY, buttonWidth, buttonHeight);
                    DrawButton(e.Graphics, btnFulfillRect, "Fulfill", Color.FromArgb(13, 110, 253), Color.White);

                    Rectangle btnCancelRect = new Rectangle(xOffset + buttonWidth + spacing, buttonY, buttonWidth, buttonHeight);
                    DrawButton(e.Graphics, btnCancelRect, "Cancel", Color.FromArgb(220, 53, 69), Color.White);
                }

                e.Handled = true;
            }
        }

        private void DrawButton(Graphics g, Rectangle rect, string text, Color bgColor, Color textColor)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                int radius = 5;
                path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
                path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
                path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
                path.CloseAllFigures();

                using (SolidBrush brush = new SolidBrush(bgColor))
                {
                    g.FillPath(brush, path);
                }
            }

            TextRenderer.DrawText(
                g,
                text,
                new Font("Segoe UI", 9, FontStyle.Bold),
                rect,
                textColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
            );
        }

        private void DataGridViewReservations_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string columnName = dataGridViewReservations.Columns[e.ColumnIndex].Name;
            if (columnName != "Actions") return;

            DataGridViewRow row = dataGridViewReservations.Rows[e.RowIndex];

            int reservationId = 0;
            if (row.DataBoundItem is DataRowView drv)
            {
                reservationId = Convert.ToInt32(drv["ReservationID"]);
            }
            else if (row.DataBoundItem is DataRow dr)
            {
                reservationId = Convert.ToInt32(dr["ReservationID"]);
            }
            else
            {

                object reservationIdObj = row.Cells["ReservationID"].Value;
                if (reservationIdObj != null)
                {
                    int.TryParse(reservationIdObj.ToString(), out reservationId);
                }
            }

            string status = row.Cells["Status"].Value?.ToString() ?? "";

            Point clickPoint = dataGridViewReservations.PointToClient(Control.MousePosition);
            Rectangle cellRect = dataGridViewReservations.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);

            int buttonY = cellRect.Y + (cellRect.Height - 30) / 2;
            int buttonWidth = 80;
            int spacing = 5;
            int xOffset = cellRect.X + 5;

            if (status.ToLower() == "pending")
            {
                Rectangle btnReadyRect = new Rectangle(xOffset, buttonY, buttonWidth, 30);
                Rectangle btnCancelRect = new Rectangle(xOffset + buttonWidth + spacing, buttonY, buttonWidth, 30);

                if (btnReadyRect.Contains(clickPoint))
                {
                    MarkAsReady(reservationId);
                }
                else if (btnCancelRect.Contains(clickPoint))
                {
                    CancelReservation(reservationId);
                }
            }
            else if (status.ToLower() == "ready")
            {
                Rectangle btnFulfillRect = new Rectangle(xOffset, buttonY, buttonWidth, 30);
                Rectangle btnCancelRect = new Rectangle(xOffset + buttonWidth + spacing, buttonY, buttonWidth, 30);

                if (btnFulfillRect.Contains(clickPoint))
                {
                    FulfillReservation(reservationId);
                }
                else if (btnCancelRect.Contains(clickPoint))
                {
                    CancelReservation(reservationId);
                }
            }
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

                    string queryPending = "SELECT COUNT(*) FROM Reservations WHERE Status = 'Pending'";
                    using (MySqlCommand cmd = new MySqlCommand(queryPending, conn))
                    {
                        int pending = Convert.ToInt32(cmd.ExecuteScalar());
                        lblMetricPendingValue.Text = pending.ToString();
                    }

                    string queryReady = "SELECT COUNT(*) FROM Reservations WHERE Status = 'Ready'";
                    using (MySqlCommand cmd = new MySqlCommand(queryReady, conn))
                    {
                        int ready = Convert.ToInt32(cmd.ExecuteScalar());
                        lblMetricReadyValue.Text = ready.ToString();
                    }

                    string queryFulfilled = "SELECT COUNT(*) FROM Reservations WHERE Status = 'Fulfilled'";
                    using (MySqlCommand cmd = new MySqlCommand(queryFulfilled, conn))
                    {
                        int fulfilled = Convert.ToInt32(cmd.ExecuteScalar());
                        lblMetricFulfilledValue.Text = fulfilled.ToString();
                    }

                    string queryExpired = "SELECT COUNT(*) FROM Reservations WHERE Status = 'Expired'";
                    using (MySqlCommand cmd = new MySqlCommand(queryExpired, conn))
                    {
                        int expired = Convert.ToInt32(cmd.ExecuteScalar());
                        lblMetricExpiredValue.Text = expired.ToString();
                    }
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

                if (!allReservationsData.Columns.Contains("Member"))
                {
                    allReservationsData.Columns.Add("Member", typeof(string));
                }
                if (!allReservationsData.Columns.Contains("Book"))
                {
                    allReservationsData.Columns.Add("Book", typeof(string));
                }
                if (!allReservationsData.Columns.Contains("Priority"))
                {
                    allReservationsData.Columns.Add("Priority", typeof(int));
                }

                List<DataRow> pendingRows = new List<DataRow>();
                foreach (DataRow row in allReservationsData.Rows)
                {

                    string firstName = row["FirstName"] != DBNull.Value ? row["FirstName"].ToString() : "";
                    string lastName = row["LastName"] != DBNull.Value ? row["LastName"].ToString() : "";
                    int memberId = Convert.ToInt32(row["MemberID"]);
                    row["Member"] = $"{firstName} {lastName} (MEM-{memberId.ToString().PadLeft(3, '0')})".Trim();

                    string bookTitle = row["Title"] != DBNull.Value ? row["Title"].ToString() : "";
                    int bookId = Convert.ToInt32(row["BookID"]);
                    string barcode = row["Barcode"] != DBNull.Value ? row["Barcode"].ToString() : "";
                    string accessionNo = !string.IsNullOrEmpty(barcode) ? barcode : $"ACC-{bookId.ToString().PadLeft(4, '0')}";
                    row["Book"] = $"{bookTitle} ({accessionNo})";

                    if (row["ExpiryDate"] == DBNull.Value && row["ReservationDate"] != DBNull.Value)
                    {
                        DateTime reservationDate = Convert.ToDateTime(row["ReservationDate"]);
                        row["ExpiryDate"] = reservationDate.AddDays(7);
                    }

                    string currentStatus = row["Status"] != DBNull.Value ? row["Status"].ToString() : "Pending";
                    if ((currentStatus == "Pending" || currentStatus == "Ready") && row["ExpiryDate"] != DBNull.Value)
                    {
                        DateTime expiryDate = Convert.ToDateTime(row["ExpiryDate"]);
                        if (expiryDate < DateTime.Now)
                        {
                            row["Status"] = "Expired";
                        }
                    }

                    if (row["Status"].ToString() == "Pending")
                    {
                        pendingRows.Add(row);
                    }
                }

                pendingRows = pendingRows.OrderBy(r => Convert.ToDateTime(r["ReservationDate"])).ToList();
                for (int i = 0; i < pendingRows.Count; i++)
                {
                    pendingRows[i]["Priority"] = i + 1;
                }

                DataView dv = allReservationsData.DefaultView;
                if (currentFilter != "All Status")
                {
                    dv.RowFilter = $"Status = '{currentFilter}'";
                }
                else
                {
                    dv.RowFilter = "";
                }
                dataGridViewReservations.DataSource = dv;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading reservations: {ex.Message}");
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
                bool hasFulfilledDate = DatabaseSchemaHelper.CheckColumnExists(conn, "Reservations", "FulfilledDate");

                string query;
                if (hasExpiryDate && hasPriority && hasFulfilledDate)
                {
                    query = @"SELECT 
                                r.ReservationID,
                                r.MemberID,
                                r.BookID,
                                r.ReservationDate,
                                r.PickupDate,
                                r.ExpiryDate,
                                r.Status,
                                r.Priority,
                                r.FulfilledDate,
                                m.FirstName,
                                m.LastName,
                                b.Title,
                                b.Barcode
                             FROM Reservations r
                             INNER JOIN Members m ON r.MemberID = m.MemberID
                             INNER JOIN Books b ON r.BookID = b.BookID
                             ORDER BY r.ReservationDate DESC";
                }
                else
                {
                    query = @"SELECT 
                                r.ReservationID,
                                r.MemberID,
                                r.BookID,
                                r.ReservationDate,
                                r.PickupDate,
                                NULL as ExpiryDate,
                                r.Status,
                                0 as Priority,
                                NULL as FulfilledDate,
                                m.FirstName,
                                m.LastName,
                                b.Title,
                                b.Barcode
                             FROM Reservations r
                             INNER JOIN Members m ON r.MemberID = m.MemberID
                             INNER JOIN Books b ON r.BookID = b.BookID
                             ORDER BY r.ReservationDate DESC";
                }

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }


        private void ApplyFilter()
        {
            if (allReservationsData == null) return;

            DataView dv = allReservationsData.DefaultView;
            if (currentFilter == "All Status")
            {
                dv.RowFilter = "";
            }
            else
            {
                dv.RowFilter = $"Status = '{currentFilter}'";
            }
            dataGridViewReservations.DataSource = dv;
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            ContextMenuStrip filterMenu = new ContextMenuStrip();
            filterMenu.Items.Add("All Status", null, (s, args) => { currentFilter = "All Status"; btnFilter.Text = "?? All Status"; LoadReservations(); });
            filterMenu.Items.Add("Pending", null, (s, args) => { currentFilter = "Pending"; btnFilter.Text = "?? Pending"; LoadReservations(); });
            filterMenu.Items.Add("Ready", null, (s, args) => { currentFilter = "Ready"; btnFilter.Text = "?? Ready"; LoadReservations(); });
            filterMenu.Items.Add("Fulfilled", null, (s, args) => { currentFilter = "Fulfilled"; btnFilter.Text = "?? Fulfilled"; LoadReservations(); });
            filterMenu.Items.Add("Expired", null, (s, args) => { currentFilter = "Expired"; btnFilter.Text = "?? Expired"; LoadReservations(); });

            filterMenu.Show(btnFilter, new Point(0, btnFilter.Height));
        }

        private void MarkAsReady(int reservationId)
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE Reservations SET Status = 'Ready', PickupDate = @PickupDate WHERE ReservationID = @ReservationID";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ReservationID", reservationId);
                        cmd.Parameters.AddWithValue("@PickupDate", DateTime.Now);
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

        private void FulfillReservation(int reservationId)
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

                    bool hasFulfilledDate = DatabaseSchemaHelper.CheckColumnExists(conn, "Reservations", "FulfilledDate");
                    string updateQuery;
                    if (hasFulfilledDate)
                    {
                        updateQuery = "UPDATE Reservations SET Status = 'Fulfilled', FulfilledDate = @FulfilledDate WHERE ReservationID = @ReservationID";
                    }
                    else
                    {
                        updateQuery = "UPDATE Reservations SET Status = 'Fulfilled' WHERE ReservationID = @ReservationID";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@ReservationID", reservationId);
                        if (hasFulfilledDate)
                        {
                            cmd.Parameters.AddWithValue("@FulfilledDate", DateTime.Now);
                        }
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

                MessageBox.Show("Reservation fulfilled successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadMetrics();
                LoadReservations();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fulfilling reservation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelReservation(int reservationId)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to cancel this reservation?", "Confirm Cancellation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "DELETE FROM Reservations WHERE ReservationID = @ReservationID";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
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

        private void lblSubtitle_Click(object sender, EventArgs e)
        {

        }

        private void lblMetricReadyValue_Click(object sender, EventArgs e)
        {

        }
    }
}
