using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Project5LMS.Helpers;
using Project5LMS.Services;
using Project5LMS.Data;
using Project5LMS.Repositories;
using Project5LMS.Interfaces;
namespace Project5LMS.Forms.Member.Borrowings
{
    public partial class MyBorrowingsForm : Form
    {
        private readonly DatabaseContext _dbContext;
        private readonly TransactionRepository _transactionRepository;
        private readonly IBookService _bookService;
        private const int MAX_RENEWALS = 3;
        public MyBorrowingsForm()
        {
            InitializeComponent();
            _dbContext = ServiceFactory.GetDbContext();
            _transactionRepository = new TransactionRepository(_dbContext);
            _bookService = ServiceFactory.CreateBookService();
        }
        private void MyBorrowingsForm_Load(object sender, EventArgs e)
        {
            LoadBorrowings();
        }
        private void LoadBorrowings()
        {
            int memberID = CurrentUser.GetMemberID();
            if (memberID == 0)
            {
                MessageBox.Show("Unable to identify your member account.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            LoadCurrentlyBorrowed(memberID);
            LoadBorrowingHistory(memberID);
        }
        private void LoadCurrentlyBorrowed(int memberID)
        {
            try
            {
                panelBorrowedList.Controls.Clear();
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string checkTableQuery = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES
                                              WHERE TABLE_SCHEMA = DATABASE()
                                              AND TABLE_NAME = 'Transactions'";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkTableQuery, conn))
                    {
                        int tableExists = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (tableExists == 0)
                        {
                            lblCurrentlyBorrowedCount.Text = "Currently Borrowed (0)";
                            return;
                        }
                    }
                    bool hasRenewedCount = DatabaseSchemaHelper.CheckColumnExists(conn, "Transactions", "RenewedCount");
                    string query = @"SELECT
                                    t.TransactionID,
                                    b.BookID,
                                    b.Title,
                                    b.Author,
                                    t.BorrowDate,
                                    t.DueDate,
                                    " + (hasRenewedCount ? "COALESCE(t.RenewedCount, 0) as RenewedCount" : "0 as RenewedCount") + @"
                                FROM Transactions t
                                INNER JOIN Books b ON t.BookID = b.BookID
                                WHERE t.MemberID = @MemberID
                                AND (t.Status = 'Borrowed' OR t.Status IS NULL OR t.ReturnDate IS NULL)
                                ORDER BY t.DueDate ASC";
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
                                int transactionID = reader.GetInt32("TransactionID");
                                int bookID = reader.GetInt32("BookID");
                                string title = reader["Title"].ToString();
                                string author = reader["Author"] != DBNull.Value ? reader["Author"].ToString() : "Unknown";
                                DateTime borrowDate = reader["BorrowDate"] != DBNull.Value ? Convert.ToDateTime(reader["BorrowDate"]) : DateTime.MinValue;
                                DateTime dueDate = reader["DueDate"] != DBNull.Value ? Convert.ToDateTime(reader["DueDate"]) : DateTime.MinValue;
                                int renewedCount = reader["RenewedCount"] != DBNull.Value ? Convert.ToInt32(reader["RenewedCount"]) : 0;
                                string accessionNo = $"ACC-2024-{bookID.ToString().PadLeft(6, '0')}";
                                int daysLeft = (dueDate - DateTime.Now).Days;
                                int renewalsLeft = MAX_RENEWALS - renewedCount;
                                bool dueSoon = daysLeft <= 5 && daysLeft >= 0;
                                Panel bookCard = CreateBorrowedBookCard(transactionID, title, author, accessionNo, borrowDate, dueDate, daysLeft, renewalsLeft, dueSoon);
                                bookCard.Location = new Point(0, yPos);
                                bookCard.Width = panelBorrowedList.Width - 20;
                                panelBorrowedList.Controls.Add(bookCard);
                                yPos += bookCard.Height + 15;
                            }
                            lblCurrentlyBorrowedCount.Text = $"Currently Borrowed ({count})";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading borrowed books: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private Panel CreateBorrowedBookCard(int transactionID, string title, string author, string accessionNo, DateTime borrowDate, DateTime dueDate, int daysLeft, int renewalsLeft, bool dueSoon)
        {
            Panel card = new Panel
            {
                BackColor = Color.White,
                Size = new Size(1100, 120),
                Padding = new Padding(25, 20, 25, 20),
                Margin = new Padding(0, 0, 0, 15),
                BorderStyle = BorderStyle.FixedSingle
            };
            Label lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                Location = new Point(25, 20),
                AutoSize = true
            };
            Label lblAuthor = new Label
            {
                Text = $"by {author}",
                Font = new Font("Segoe UI", 12F),
                ForeColor = Color.FromArgb(128, 128, 128),
                Location = new Point(25, 50),
                AutoSize = true
            };
            Label lblAccession = new Label
            {
                Text = $"Accession No: {accessionNo}",
                Font = new Font("Segoe UI", 11F),
                ForeColor = Color.FromArgb(96, 96, 96),
                Location = new Point(25, 75),
                AutoSize = true
            };
            if (dueSoon)
            {
                Panel panelDueSoon = new Panel
                {
                    BackColor = Color.FromArgb(255, 235, 59),
                    Location = new Point(950, 20),
                    Size = new Size(100, 30),
                    Padding = new Padding(10, 5, 10, 5)
                };
                Label lblDueSoon = new Label
                {
                    Text = "Due Soon",
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(64, 64, 64),
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                panelDueSoon.Controls.Add(lblDueSoon);
                card.Controls.Add(panelDueSoon);
            }
            int detailY = 95;
            int iconSize = 20;
            Panel panelBorrowedIcon = new Panel { Size = new Size(iconSize, iconSize), Location = new Point(25, detailY) };
            panelBorrowedIcon.Paint += (s, e) => DrawCalendarIcon(e.Graphics, panelBorrowedIcon.ClientRectangle);
            Label lblBorrowed = new Label
            {
                Text = $"Borrowed: {borrowDate:yyyy-MM-dd}",
                Font = new Font("Segoe UI", 11F),
                ForeColor = Color.FromArgb(96, 96, 96),
                Location = new Point(50, detailY - 2),
                AutoSize = true
            };
            Panel panelDueIcon = new Panel { Size = new Size(iconSize, iconSize), Location = new Point(250, detailY) };
            panelDueIcon.Paint += (s, e) => DrawCalendarIcon(e.Graphics, panelDueIcon.ClientRectangle);
            string daysLeftText = daysLeft < 0 ? $"({Math.Abs(daysLeft)} days overdue)" : $"({daysLeft} days left)";
            Label lblDue = new Label
            {
                Text = $"Due: {dueDate:yyyy-MM-dd} {daysLeftText}",
                Font = new Font("Segoe UI", 11F),
                ForeColor = daysLeft < 0 ? Color.Red : (daysLeft <= 5 ? Color.Orange : Color.FromArgb(96, 96, 96)),
                Location = new Point(275, detailY - 2),
                AutoSize = true
            };
            Panel panelRenewIcon = new Panel { Size = new Size(iconSize, iconSize), Location = new Point(500, detailY) };
            panelRenewIcon.Paint += (s, e) => DrawRenewIcon(e.Graphics, panelRenewIcon.ClientRectangle);
            Label lblRenewals = new Label
            {
                Text = $"{renewalsLeft} renewal{(renewalsLeft != 1 ? "s" : "")} left",
                Font = new Font("Segoe UI", 11F),
                ForeColor = Color.FromArgb(96, 96, 96),
                Location = new Point(525, detailY - 2),
                AutoSize = true
            };
            Button btnRenew = new Button
            {
                Text = "Renew Book",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                BackColor = Color.FromArgb(33, 150, 243),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(950, 60),
                Size = new Size(120, 35),
                Cursor = Cursors.Hand,
                Enabled = renewalsLeft > 0
            };
            btnRenew.FlatAppearance.BorderSize = 0;
            btnRenew.Click += (s, e) => RenewBook(transactionID, title, dueDate, renewalsLeft);
            card.Controls.Add(lblTitle);
            card.Controls.Add(lblAuthor);
            card.Controls.Add(lblAccession);
            card.Controls.Add(panelBorrowedIcon);
            card.Controls.Add(lblBorrowed);
            card.Controls.Add(panelDueIcon);
            card.Controls.Add(lblDue);
            card.Controls.Add(panelRenewIcon);
            card.Controls.Add(lblRenewals);
            card.Controls.Add(btnRenew);
            return card;
        }
        private void RenewBook(int transactionID, string bookTitle, DateTime currentDueDate, int renewalsLeft)
        {
            if (renewalsLeft <= 0)
            {
                MessageBox.Show("You have reached the maximum number of renewals for this book.", "Renewal Limit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult result = MessageBox.Show(
                $"Do you want to renew '{bookTitle}'?\n\nCurrent due date: {currentDueDate:yyyy-MM-dd}\nNew due date will be: {currentDueDate.AddDays(14):yyyy-MM-dd}",
                "Renew Book",
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
                        bool hasRenewedCount = DatabaseSchemaHelper.CheckColumnExists(conn, "Transactions", "RenewedCount");
                        DateTime newDueDate = currentDueDate.AddDays(14);
                        string updateQuery;
                        if (hasRenewedCount)
                        {
                            updateQuery = @"UPDATE Transactions
                                          SET DueDate = @NewDueDate,
                                              RenewedCount = COALESCE(RenewedCount, 0) + 1
                                          WHERE TransactionID = @TransactionID";
                        }
                        else
                        {
                            try
                            {
                                string alterQuery = "ALTER TABLE Transactions ADD COLUMN RenewedCount INT DEFAULT 0";
                                using (MySqlCommand alterCmd = new MySqlCommand(alterQuery, conn))
                                {
                                    alterCmd.ExecuteNonQuery();
                                }
                            }
                            catch
                            {
                            }
                            updateQuery = @"UPDATE Transactions
                                          SET DueDate = @NewDueDate,
                                              RenewedCount = COALESCE(RenewedCount, 0) + 1
                                          WHERE TransactionID = @TransactionID";
                        }
                        using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@NewDueDate", newDueDate);
                            cmd.Parameters.AddWithValue("@TransactionID", transactionID);
                            cmd.ExecuteNonQuery();
                        }
                        MessageBox.Show($"Book renewed successfully!\n\nNew due date: {newDueDate:yyyy-MM-dd}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadBorrowings();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error renewing book: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void LoadBorrowingHistory(int memberID)
        {
            try
            {
                panelHistoryList.Controls.Clear();
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string checkTableQuery = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES
                                              WHERE TABLE_SCHEMA = DATABASE()
                                              AND TABLE_NAME = 'Transactions'";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkTableQuery, conn))
                    {
                        int tableExists = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (tableExists == 0)
                        {
                            return;
                        }
                    }
                    string query = @"SELECT
                                    b.Title,
                                    b.Author,
                                    t.BorrowDate,
                                    t.ReturnDate
                                FROM Transactions t
                                INNER JOIN Books b ON t.BookID = b.BookID
                                WHERE t.MemberID = @MemberID
                                AND t.ReturnDate IS NOT NULL
                                ORDER BY t.ReturnDate DESC
                                LIMIT 50";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", memberID);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            int yPos = 0;
                            while (reader.Read())
                            {
                                string title = reader["Title"].ToString();
                                string author = reader["Author"] != DBNull.Value ? reader["Author"].ToString() : "Unknown";
                                DateTime borrowDate = reader["BorrowDate"] != DBNull.Value ? Convert.ToDateTime(reader["BorrowDate"]) : DateTime.MinValue;
                                DateTime returnDate = reader["ReturnDate"] != DBNull.Value ? Convert.ToDateTime(reader["ReturnDate"]) : DateTime.MinValue;
                                Panel historyCard = CreateHistoryCard(title, author, borrowDate, returnDate);
                                historyCard.Location = new Point(0, yPos);
                                historyCard.Width = panelHistoryList.Width - 40;
                                panelHistoryList.Controls.Add(historyCard);
                                yPos += historyCard.Height + 1;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading borrowing history: {ex.Message}");
            }
        }
        private Panel CreateHistoryCard(string title, string author, DateTime borrowDate, DateTime returnDate)
        {
            Panel card = new Panel
            {
                BackColor = Color.White,
                Size = new Size(1200, 80),
                Padding = new Padding(20, 15, 20, 15),
                Margin = new Padding(0, 0, 0, 0)
            };
            Label lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                Location = new Point(20, 15),
                AutoSize = true
            };
            Label lblAuthor = new Label
            {
                Text = $"by {author}",
                Font = new Font("Segoe UI", 12F),
                ForeColor = Color.FromArgb(128, 128, 128),
                Location = new Point(20, 40),
                AutoSize = true
            };
            Label lblBorrowed = new Label
            {
                Text = $"Borrowed: {borrowDate:yyyy-MM-dd}",
                Font = new Font("Segoe UI", 11F),
                ForeColor = Color.FromArgb(128, 128, 128),
                Location = new Point(400, 40),
                AutoSize = true
            };
            Label lblReturned = new Label
            {
                Text = $"Returned: {returnDate:yyyy-MM-dd}",
                Font = new Font("Segoe UI", 11F),
                ForeColor = Color.FromArgb(128, 128, 128),
                Location = new Point(600, 40),
                AutoSize = true
            };
            Panel panelReturned = new Panel
            {
                BackColor = Color.FromArgb(240, 240, 240),
                Location = new Point(1100, 20),
                Size = new Size(80, 30),
                Padding = new Padding(10, 5, 10, 5)
            };
            Label lblReturnedBadge = new Label
            {
                Text = "Returned",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            panelReturned.Controls.Add(lblReturnedBadge);
            Panel separator = new Panel
            {
                BackColor = Color.FromArgb(230, 230, 230),
                Location = new Point(20, 79),
                Size = new Size(card.Width - 40, 1)
            };
            card.Controls.Add(lblTitle);
            card.Controls.Add(lblAuthor);
            card.Controls.Add(lblBorrowed);
            card.Controls.Add(lblReturned);
            card.Controls.Add(panelReturned);
            card.Controls.Add(separator);
            return card;
        }
        private void DrawCalendarIcon(Graphics g, Rectangle rect)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int size = Math.Min(rect.Width, rect.Height) - 4;
            int x = (rect.Width - size) / 2;
            int y = (rect.Height - size) / 2;
            using (Pen pen = new Pen(Color.FromArgb(128, 128, 128), 2))
            {
                g.DrawRectangle(pen, x + 2, y + 6, size - 4, size - 6);
                g.DrawRectangle(pen, x, y, size, 8);
                g.DrawEllipse(pen, x + 3, y + 2, 3, 3);
                g.DrawEllipse(pen, x + size - 6, y + 2, 3, 3);
            }
        }
        private void DrawRenewIcon(Graphics g, Rectangle rect)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int centerX = rect.Width / 2;
            int centerY = rect.Height / 2;
            int radius = Math.Min(rect.Width, rect.Height) / 2 - 4;
            using (Pen pen = new Pen(Color.FromArgb(128, 128, 128), 2))
            {
                g.DrawArc(pen, centerX - radius, centerY - radius, radius * 2, radius * 2, 45, 270);
                Point[] arrowPoints = new Point[]
                {
                    new Point(centerX + radius - 2, centerY - radius + 2),
                    new Point(centerX + radius + 3, centerY - radius - 2),
                    new Point(centerX + radius - 2, centerY - radius - 2)
                };
                g.DrawLines(pen, arrowPoints);
            }
        }
        private void DrawHistoryIcon(Graphics g, Rectangle rect)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int centerX = rect.Width / 2;
            int centerY = rect.Height / 2;
            int radius = Math.Min(rect.Width, rect.Height) / 2 - 4;
            using (Pen pen = new Pen(Color.FromArgb(64, 64, 64), 2))
            {
                g.DrawEllipse(pen, centerX - radius, centerY - radius, radius * 2, radius * 2);
                g.DrawLine(pen, centerX, centerY, centerX + radius - 4, centerY);
                g.DrawLine(pen, centerX, centerY, centerX, centerY - radius / 2);
                int arrowRadius = radius + 4;
                g.DrawArc(pen, centerX - arrowRadius, centerY - arrowRadius, arrowRadius * 2, arrowRadius * 2, 0, 270);
            }
        }
    }
}