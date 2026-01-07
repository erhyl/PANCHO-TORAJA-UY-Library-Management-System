using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Project5LMS.Helpers;
using Project5LMS.Data;
using Project5LMS.Services;

namespace Project5LMS.Forms.Member.Reservations
{
    public partial class MemberReservationsForm : Form
    {
        private readonly DatabaseContext _dbContext;

        public MemberReservationsForm()
        {
            InitializeComponent();
            _dbContext = ServiceFactory.GetDbContext();
            this.Load += MemberReservationsForm_Load;
            this.VisibleChanged += MemberReservationsForm_VisibleChanged;

            this.BackColor = Color.FromArgb(250, 250, 250);
            this.Visible = true;

            if (this.Visible)
            {
                LoadReservations();
            }
        }

        private void MemberReservationsForm_Load(object sender, EventArgs e)
        {
            LoadReservations();
        }

        private void MemberReservationsForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                LoadReservations();
            }
        }

        private void LoadReservations()
        {
            int memberID = CurrentUser.GetMemberID();
            if (memberID == 0)
            {
                MessageBox.Show("Unable to identify your member account.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                panelReservationsList.Controls.Clear();

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
                            lblActiveReservationsCount.Text = "Active Reservations (0)";
                            return;
                        }
                    }

                    string query = @"SELECT 
                                    r.ReservationID,
                                    r.BookID,
                                    b.Title,
                                    b.Author,
                                    r.ReservationDate,
                                    r.PickupDate,
                                    r.Status
                                FROM Reservations r
                                INNER JOIN Books b ON r.BookID = b.BookID
                                WHERE r.MemberID = @MemberID
                                AND (r.Status = 'Pending' OR r.Status = 'Active' OR r.Status = 'Ready' OR r.Status IS NULL)
                                ORDER BY r.ReservationDate ASC";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", memberID);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            int yPos = 0;
                            int count = 0;

                            while (reader.Read())
                            {
                                count++;
                                int reservationID = reader.GetInt32("ReservationID");
                                int bookID = reader.GetInt32("BookID");
                                string title = reader["Title"].ToString();
                                string author = reader["Author"] != DBNull.Value ? reader["Author"].ToString() : "Unknown";
                                DateTime reservationDate = reader["ReservationDate"] != DBNull.Value ? Convert.ToDateTime(reader["ReservationDate"]) : DateTime.MinValue;

                                int queuePosition = GetQueuePosition(conn, bookID, reservationID, reservationDate);
                                int totalInQueue = GetTotalInQueue(conn, bookID);
                                DateTime estimatedAvailable = GetEstimatedAvailability(conn, bookID);

                                Panel reservationCard = CreateReservationCard(reservationID, title, author, reservationDate, queuePosition, totalInQueue, estimatedAvailable);
                                reservationCard.Location = new Point(0, yPos);
                                reservationCard.Width = panelReservationsList.Width - 40;
                                panelReservationsList.Controls.Add(reservationCard);

                                yPos += reservationCard.Height + 15;
                            }

                            lblActiveReservationsCount.Text = $"Active Reservations ({count})";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading reservations: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetQueuePosition(MySqlConnection conn, int bookID, int currentReservationID, DateTime currentReservationDate)
        {
            try
            {
                string query = @"SELECT COUNT(*) + 1 as Position
                                FROM Reservations
                                WHERE BookID = @BookID
                                AND ReservationID != @ReservationID
                                AND ReservationDate < @ReservationDate
                                AND (Status = 'Pending' OR Status = 'Active' OR Status = 'Ready' OR Status IS NULL)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@BookID", bookID);
                    cmd.Parameters.AddWithValue("@ReservationID", currentReservationID);
                    cmd.Parameters.AddWithValue("@ReservationDate", currentReservationDate);
                    object result = cmd.ExecuteScalar();
                    return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 1;
                }
            }
            catch
            {
                return 1;
            }
        }

        private int GetTotalInQueue(MySqlConnection conn, int bookID)
        {
            try
            {
                string query = @"SELECT COUNT(*)
                                FROM Reservations
                                WHERE BookID = @BookID
                                AND (Status = 'Pending' OR Status = 'Active' OR Status = 'Ready' OR Status IS NULL)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@BookID", bookID);
                    object result = cmd.ExecuteScalar();
                    return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 1;
                }
            }
            catch
            {
                return 1;
            }
        }

        private DateTime GetEstimatedAvailability(MySqlConnection conn, int bookID)
        {
            try
            {

                string query = @"SELECT MAX(DueDate) as MaxDueDate
                                FROM Transactions
                                WHERE BookID = @BookID
                                AND (Status = 'Borrowed' OR Status IS NULL OR ReturnDate IS NULL)
                                AND ReturnDate IS NULL";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@BookID", bookID);
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        DateTime dueDate = Convert.ToDateTime(result);

                        return dueDate.AddDays(2);
                    }
                }

                return DateTime.Now.AddDays(14);
            }
            catch
            {
                return DateTime.Now.AddDays(14);
            }
        }

        private Panel CreateReservationCard(int reservationID, string title, string author, DateTime reservationDate, int queuePosition, int totalInQueue, DateTime estimatedAvailable)
        {
            Panel card = new Panel
            {
                BackColor = Color.White,
                Size = new Size(710, 180),
                Padding = new Padding(25, 20, 25, 20),
                Margin = new Padding(0, 0, 0, 15),
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblReservationID = new Label
            {
                Text = $"RES-{reservationID.ToString().PadLeft(3, '0')}",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(128, 128, 128),
                Location = new Point(500, 20),
                AutoSize = true
            };

            Button btnCancel = new Button
            {
                Text = "?",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                BackColor = Color.Transparent,
                ForeColor = Color.FromArgb(220, 20, 60),
                FlatStyle = FlatStyle.Flat,
                Location = new Point(650, 15),
                Size = new Size(30, 30),
                Cursor = Cursors.Hand
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, e) => CancelReservation(reservationID, title);

            Label lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                Location = new Point(25, 20),
                AutoSize = true,
                MaximumSize = new Size(450, 0)
            };

            Label lblAuthor = new Label
            {
                Text = $"by {author}",
                Font = new Font("Segoe UI", 12F),
                ForeColor = Color.FromArgb(128, 128, 128),
                Location = new Point(25, 50),
                AutoSize = true
            };

            Panel panelDetails = new Panel
            {
                BackColor = Color.FromArgb(248, 249, 250),
                Location = new Point(25, 85),
                Size = new Size(660, 70),
                Padding = new Padding(20, 15, 20, 15)
            };

            Label lblReservedLabel = new Label
            {
                Text = "Reserved Date",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(128, 128, 128),
                Location = new Point(20, 15),
                AutoSize = true
            };
            Label lblReservedDate = new Label
            {
                Text = reservationDate.ToString("yyyy-MM-dd"),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                Location = new Point(20, 35),
                AutoSize = true
            };

            Panel panelQueueIcon = new Panel { Size = new Size(20, 20), Location = new Point(200, 40) };
            panelQueueIcon.Paint += (s, e) => DrawQueueIcon(e.Graphics, panelQueueIcon.ClientRectangle);
            Label lblQueueLabel = new Label
            {
                Text = "Queue Position",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(128, 128, 128),
                Location = new Point(220, 15),
                AutoSize = true
            };
            Label lblQueuePosition = new Label
            {
                Text = $"#{queuePosition} of {totalInQueue}",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                Location = new Point(220, 35),
                AutoSize = true
            };

            Label lblEstimatedLabel = new Label
            {
                Text = "Estimated Available",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(128, 128, 128),
                Location = new Point(400, 15),
                AutoSize = true
            };
            Label lblEstimatedDate = new Label
            {
                Text = estimatedAvailable.ToString("yyyy-MM-dd"),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                Location = new Point(400, 35),
                AutoSize = true
            };

            panelDetails.Controls.Add(lblReservedLabel);
            panelDetails.Controls.Add(lblReservedDate);
            panelDetails.Controls.Add(panelQueueIcon);
            panelDetails.Controls.Add(lblQueueLabel);
            panelDetails.Controls.Add(lblQueuePosition);
            panelDetails.Controls.Add(lblEstimatedLabel);
            panelDetails.Controls.Add(lblEstimatedDate);

            card.Controls.Add(lblReservationID);
            card.Controls.Add(btnCancel);
            card.Controls.Add(lblTitle);
            card.Controls.Add(lblAuthor);
            card.Controls.Add(panelDetails);

            if (queuePosition == 1)
            {
                Panel panelNotification = new Panel
                {
                    BackColor = Color.FromArgb(76, 175, 80),
                    Location = new Point(25, 160),
                    Size = new Size(660, 40),
                    Padding = new Padding(15, 10, 15, 10)
                };
                Label lblNotification = new Label
                {
                    Text = "You're next in line! You'll be notified when the book becomes available.",
                    Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                    ForeColor = Color.White,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleLeft
                };
                panelNotification.Controls.Add(lblNotification);
                card.Controls.Add(panelNotification);
                card.Height = 220;
            }

            return card;
        }

        private void CancelReservation(int reservationID, string bookTitle)
        {
            DialogResult result = MessageBox.Show(
                $"Are you sure you want to cancel your reservation for '{bookTitle}'?",
                "Cancel Reservation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (var conn = _dbContext.GetConnection())
                    {
                        conn.Open();

                        string deleteQuery = "DELETE FROM Reservations WHERE ReservationID = @ReservationID";
                        using (MySqlCommand cmd = new MySqlCommand(deleteQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@ReservationID", reservationID);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Reservation cancelled successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadReservations();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error cancelling reservation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DrawQueueIcon(Graphics g, Rectangle rect)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int centerX = rect.Width / 2;
            int centerY = rect.Height / 2;
            int size = Math.Min(rect.Width, rect.Height) - 4;

            using (Pen pen = new Pen(Color.FromArgb(128, 128, 128), 2))
            {

                int person1X = centerX - size / 3;
                int person2X = centerX + size / 3;

                g.DrawEllipse(pen, person1X - 4, centerY - 6, 8, 8);

                Point[] person1Body = new Point[]
                {
                    new Point(person1X, centerY + 2),
                    new Point(person1X - 4, centerY + 8),
                    new Point(person1X + 4, centerY + 8)
                };
                g.DrawPolygon(pen, person1Body);

                g.DrawEllipse(pen, person2X - 4, centerY - 6, 8, 8);

                Point[] person2Body = new Point[]
                {
                    new Point(person2X, centerY + 2),
                    new Point(person2X - 4, centerY + 8),
                    new Point(person2X + 4, centerY + 8)
                };
                g.DrawPolygon(pen, person2Body);
            }
        }

        private void panelBookmarkIcon_Paint(object sender, PaintEventArgs e)
        {
            DrawBookmarkIcon(e.Graphics, panelBookmarkIcon.ClientRectangle);
        }

        private void DrawBookmarkIcon(Graphics g, Rectangle rect)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int centerX = rect.Width / 2;
            int centerY = rect.Height / 2;
            int width = rect.Width - 6;
            int height = rect.Height - 6;

            using (Pen pen = new Pen(Color.FromArgb(64, 64, 64), 2))
            {

                g.DrawRectangle(pen, centerX - width / 2, centerY - height / 2 + 4, width, height - 4);

                Point[] triangle = new Point[]
                {
                    new Point(centerX, centerY - height / 2),
                    new Point(centerX - width / 2, centerY - height / 2 + 4),
                    new Point(centerX + width / 2, centerY - height / 2 + 4)
                };
                g.DrawPolygon(pen, triangle);
            }
        }

        private void panelMainContainer_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
