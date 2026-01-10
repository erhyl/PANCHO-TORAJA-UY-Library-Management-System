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
using Project5LMS.Interfaces;
namespace Project5LMS.Forms.Member.Search
{
    public partial class MemberSearchForm : Form
    {
        private readonly ISearchService _searchService;
        public MemberSearchForm()
        {
            InitializeComponent();
            _searchService = ServiceFactory.CreateSearchService();
        }
        private void MemberSearchForm_Load(object sender, EventArgs e)
        {
            cmbSearchBy.Items.AddRange(new string[] { "Title", "Author", "ISBN", "Category" });
            cmbSearchBy.SelectedIndex = 0;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }
        private void txtSearchQuery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformSearch();
                e.Handled = true;
            }
        }
        private void txtSearchQuery_Enter(object sender, EventArgs e)
        {
            if (txtSearchQuery.Text == "Enter search term...")
            {
                txtSearchQuery.Text = "";
                txtSearchQuery.ForeColor = Color.FromArgb(64, 64, 64);
            }
        }
        private void txtSearchQuery_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearchQuery.Text))
            {
                txtSearchQuery.Text = "Enter search term...";
                txtSearchQuery.ForeColor = Color.FromArgb(128, 128, 128);
            }
        }
        private void PerformSearch()
        {
            string searchTerm = txtSearchQuery.Text.Trim();
            string searchBy = cmbSearchBy.SelectedItem?.ToString() ?? "Title";
            if (string.IsNullOrWhiteSpace(searchTerm) || searchTerm == "Enter search term...")
            {
                MessageBox.Show("Please enter a search term.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            LoadSearchResults(searchTerm, searchBy);
        }
        private void LoadSearchResults(string searchTerm, string searchBy)
        {
            try
            {
                panelSearchResults.Controls.Clear();
                SearchResults results = _searchService.SearchBooks(searchTerm);
                var filteredResults = results.Books;
                if (searchBy == "Author")
                {
                    filteredResults = results.Books.Where(b => b.Author != null && b.Author.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                }
                else if (searchBy == "ISBN")
                {
                    filteredResults = results.Books.Where(b => b.ISBN != null && b.ISBN.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                }
                else if (searchBy == "Category")
                {
                    filteredResults = results.Books.Where(b => b.Category != null && b.Category.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                }
                int yPos = 0;
                int resultCount = 0;
                foreach (var book in filteredResults)
                {
                    resultCount++;
                    int bookID = book.BookID;
                    string title = book.Title;
                    string author = book.Author ?? "Unknown";
                    string isbn = book.ISBN ?? "N/A";
                    string category = book.Category ?? "Uncategorized";
                    int available = book.Available;
                    bool isAvailable = available > 0;
                    string status = isAvailable ? "Available" : "Borrowed";
                    string location = $"Section A, Shelf {bookID % 20 + 1}";
                    Panel bookCard = CreateBookCard(bookID, title, author, isbn, category, location, available, status, isAvailable);
                    bookCard.Location = new Point(0, yPos);
                    bookCard.Width = panelSearchResults.Width - 20;
                    panelSearchResults.Controls.Add(bookCard);
                    yPos += bookCard.Height + 15;
                }
                lblResultsCount.Text = $"Search Results ({resultCount})";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error performing search: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private Panel CreateBookCard(int bookID, string title, string author, string isbn, string category, string location, int available, string status, bool isAvailable)
        {
            Panel card = new Panel
            {
                BackColor = Color.White,
                Size = new Size(1100, 120),
                Padding = new Padding(25, 20, 25, 20),
                Margin = new Padding(0, 0, 0, 15)
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
            Label lblISBN = new Label
            {
                Text = isbn,
                Font = new Font("Segoe UI", 11F),
                ForeColor = Color.FromArgb(96, 96, 96),
                Location = new Point(25, 75),
                AutoSize = true
            };
            Panel panelCategory = new Panel
            {
                BackColor = Color.FromArgb(240, 240, 240),
                Location = new Point(25, 95),
                Size = new Size(150, 25),
                Padding = new Padding(10, 5, 10, 5)
            };
            Label lblCategory = new Label
            {
                Text = category,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(64, 64, 64),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            panelCategory.Controls.Add(lblCategory);
            Label lblLocation = new Label
            {
                Text = $"?? {location} � {available} copy{(available != 1 ? "ies" : "")} available",
                Font = new Font("Segoe UI", 11F),
                ForeColor = Color.FromArgb(96, 96, 96),
                Location = new Point(600, 50),
                AutoSize = true
            };
            Panel panelStatus = new Panel
            {
                BackColor = isAvailable ? Color.FromArgb(200, 255, 200) : Color.FromArgb(255, 200, 200),
                Location = new Point(950, 20),
                Size = new Size(100, 30),
                Padding = new Padding(10, 5, 10, 5)
            };
            Label lblStatus = new Label
            {
                Text = status,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = isAvailable ? Color.FromArgb(34, 139, 34) : Color.FromArgb(220, 20, 60),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            panelStatus.Controls.Add(lblStatus);
            if (!isAvailable)
            {
                Button btnReserve = new Button
                {
                    Text = "Reserve Book",
                    Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                    BackColor = Color.FromArgb(33, 150, 243),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Location = new Point(950, 60),
                    Size = new Size(120, 35),
                    Cursor = Cursors.Hand
                };
                btnReserve.FlatAppearance.BorderSize = 0;
                btnReserve.Click += (s, e) => ReserveBook(bookID, title);
                card.Controls.Add(btnReserve);
            }
            card.Controls.Add(lblTitle);
            card.Controls.Add(lblAuthor);
            card.Controls.Add(lblISBN);
            card.Controls.Add(panelCategory);
            card.Controls.Add(lblLocation);
            card.Controls.Add(panelStatus);
            return card;
        }
        private void ReserveBook(int bookID, string bookTitle)
        {
            int memberID = CurrentUser.GetMemberID();
            if (memberID == 0)
            {
                MessageBox.Show("Unable to identify your member account.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using (var conn = ServiceFactory.GetDbContext().GetConnection())
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
                                                        PickupDate DATETIME,
                                                        ExpiryDate DATETIME,
                                                        Status VARCHAR(50) DEFAULT 'Pending',
                                                        FOREIGN KEY (MemberID) REFERENCES Members(MemberID),
                                                        FOREIGN KEY (BookID) REFERENCES Books(BookID)
                                                        )";
                            using (MySqlCommand createCmd = new MySqlCommand(createTableQuery, conn))
                            {
                                createCmd.ExecuteNonQuery();
                            }
                        }
                    }
                    var reservationService = ServiceFactory.CreateReservationService();
                    if (!reservationService.CanReserve(memberID, bookID))
                    {
                        MessageBox.Show("You cannot reserve this book. Please check your reservation limits or book availability.",
                            "Reservation Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (reservationService.CreateReservation(memberID, bookID))
                    {
                        MessageBox.Show($"Book '{bookTitle}' reserved successfully! You can pick it up within 7 days.",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to create reservation. The book may already be reserved by you.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    PerformSearch();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reserving book: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void panelSearchIcon_Paint(object sender, PaintEventArgs e)
        {
            DrawMagnifyingGlassIcon(e.Graphics, panelSearchIcon.ClientRectangle);
        }
        private void DrawMagnifyingGlassIcon(Graphics g, Rectangle rect)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int centerX = rect.Width / 2;
            int centerY = rect.Height / 2;
            int size = Math.Min(rect.Width, rect.Height) - 10;
            int radius = size / 2 - 5;
            g.DrawEllipse(new Pen(Color.FromArgb(128, 128, 128), 2), centerX - radius, centerY - radius, radius * 2, radius * 2);
            int handleLength = size / 3;
            int handleX = centerX + radius - 3;
            int handleY = centerY + radius - 3;
            g.DrawLine(new Pen(Color.FromArgb(128, 128, 128), 2), handleX, handleY, handleX + handleLength, handleY + handleLength);
        }
    }
}