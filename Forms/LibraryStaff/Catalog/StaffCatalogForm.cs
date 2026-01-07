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

namespace Project5LMS.Forms.LibraryStaff.Catalog
{
    public partial class StaffCatalogForm : Form
    {
        private DataTable allBooksData;
        private readonly IBookService _bookService;
        private const string SearchPlaceholder = "Search by title, author, ISBN, or book ID...";
        private const int CardWidth = 600;
        private const int CardHeight = 280;
        private const int CardSpacing = 20;

        public StaffCatalogForm()
        {
            InitializeComponent();
            _bookService = ServiceFactory.CreateBookService();
        }

        private void StaffCatalogForm_Load(object sender, EventArgs e)
        {
            LoadCategories();
            LoadMetrics();
            LoadBooks();
        }

        private void LoadCategories()
        {
            try
            {
                cmbCategoryFilter.Items.Clear();
                cmbCategoryFilter.Items.Add("All");

                var categories = _bookService.GetAllCategories();
                foreach (var category in categories)
                {
                    cmbCategoryFilter.Items.Add(category);
                }

                cmbCategoryFilter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading categories: {ex.Message}");
            }
        }

        private void LoadMetrics()
        {
            try
            {
                var allBooks = _bookService.GetAllBooks();
                int totalBooks = allBooks.Count();
                lblMetricTotalBooksValue.Text = totalBooks.ToString();

                int totalCopies = allBooks.Sum(b => b.TotalCopies);
                lblMetricTotalCopiesValue.Text = totalCopies.ToString();

                int available = allBooks.Sum(b => b.Available);
                lblMetricAvailableValue.Text = available.ToString();

                int checkedOut = totalCopies - available;
                lblMetricCheckedOutValue.Text = checkedOut.ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading metrics: {ex.Message}");
            }
        }

        private void LoadBooks()
        {
            try
            {
                allBooksData = GetBooksData();
                DisplayBooks(allBooksData);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading books: {ex.Message}");
                MessageBox.Show($"Error loading books: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable GetBooksData()
        {
            var books = _bookService.GetAllBooks();
            return Helpers.DataTableHelper.BooksToDataTable(books);
        }

        private void DisplayBooks(DataTable booksData)
        {
            panelBooksContainer.Controls.Clear();

            if (booksData == null || booksData.Rows.Count == 0)
            {
                Label lblNoBooks = new Label
                {
                    Text = "No books found",
                    Font = new Font("Segoe UI", 12F),
                    ForeColor = Color.Gray,
                    AutoSize = true,
                    Margin = new Padding(20, 20, 0, 0)
                };
                panelBooksContainer.Controls.Add(lblNoBooks);
                return;
            }

            foreach (DataRow row in booksData.Rows)
            {
                Panel bookCard = CreateBookCard(row);
                panelBooksContainer.Controls.Add(bookCard);
            }
        }

        private Panel CreateBookCard(DataRow bookRow)
        {
            Panel card = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(CardWidth, CardHeight),
                Margin = new Padding(0, 0, CardSpacing, CardSpacing),
                Padding = new Padding(20)
            };

            Panel iconPanel = new Panel
            {
                BackColor = Color.FromArgb(139, 0, 0),
                Size = new Size(60, 60),
                Location = new Point(20, 20)
            };
            iconPanel.Paint += (s, e) => DrawBookIcon(e.Graphics, iconPanel);
            card.Controls.Add(iconPanel);

            string category = bookRow["Category"] != DBNull.Value ? bookRow["Category"].ToString() : "General";
            Label lblGenre = new Label
            {
                Text = category,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(255, 192, 203),
                AutoSize = true,
                Padding = new Padding(8, 4, 8, 4),
                Location = new Point(CardWidth - 120, 20)
            };
            card.Controls.Add(lblGenre);

            string title = bookRow["Title"] != DBNull.Value ? bookRow["Title"].ToString() : "Unknown";
            Label lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                AutoSize = true,
                Location = new Point(100, 20),
                MaximumSize = new Size(CardWidth - 140, 0)
            };
            card.Controls.Add(lblTitle);

            string author = bookRow["Author"] != DBNull.Value ? bookRow["Author"].ToString() : "Unknown";
            Label lblAuthor = new Label
            {
                Text = $"by {author}",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(128, 128, 128),
                AutoSize = true,
                Location = new Point(100, 50),
                MaximumSize = new Size(CardWidth - 140, 0)
            };
            card.Controls.Add(lblAuthor);

            int detailY = 80;
            string isbn = bookRow["ISBN"] != DBNull.Value ? bookRow["ISBN"].ToString() : "N/A";
            Label lblISBN = new Label
            {
                Text = $"ISBN: {isbn}",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(128, 128, 128),
                AutoSize = true,
                Location = new Point(20, detailY)
            };
            card.Controls.Add(lblISBN);

            string publisher = bookRow["Publisher"] != DBNull.Value ? bookRow["Publisher"].ToString() : "N/A";
            Label lblPublisher = new Label
            {
                Text = $"Publisher: {publisher}",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(128, 128, 128),
                AutoSize = true,
                Location = new Point(20, detailY + 20)
            };
            card.Controls.Add(lblPublisher);

            string year = bookRow["YearPublished"] != DBNull.Value ? bookRow["YearPublished"].ToString() : "N/A";
            Label lblYear = new Label
            {
                Text = $"Year: {year}",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(128, 128, 128),
                AutoSize = true,
                Location = new Point(20, detailY + 40)
            };
            card.Controls.Add(lblYear);

            if (bookRow.Table.Columns.Contains("Location") && bookRow["Location"] != DBNull.Value)
            {
                string location = bookRow["Location"].ToString();
                Label lblLocation = new Label
                {
                    Text = $"Location: {location}",
                    Font = new Font("Segoe UI", 9F),
                    ForeColor = Color.FromArgb(128, 128, 128),
                    AutoSize = true,
                    Location = new Point(20, detailY + 60)
                };
                card.Controls.Add(lblLocation);
            }

            int totalCopies = bookRow["Copies"] != DBNull.Value ? Convert.ToInt32(bookRow["Copies"]) : 0;
            int available = bookRow["Available"] != DBNull.Value ? Convert.ToInt32(bookRow["Available"]) : 0;
            Label lblCopies = new Label
            {
                Text = $"Total Copies: {totalCopies}, ",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(128, 128, 128),
                AutoSize = true,
                Location = new Point(20, CardHeight - 60)
            };
            card.Controls.Add(lblCopies);

            Label lblAvailable = new Label
            {
                Text = $"{available} Available",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 167, 69),
                AutoSize = true,
                Location = new Point(lblCopies.Right, CardHeight - 60)
            };
            card.Controls.Add(lblAvailable);

            Button btnCheckOut = new Button
            {
                Text = "Check Out",
                BackColor = Color.FromArgb(139, 0, 0),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Size = new Size(120, 35),
                Location = new Point(CardWidth - 140, CardHeight - 50),
                Cursor = Cursors.Hand
            };
            btnCheckOut.FlatAppearance.BorderSize = 0;
            btnCheckOut.Click += (s, e) => CheckOutBook(Convert.ToInt32(bookRow["BookID"]), title);
            card.Controls.Add(btnCheckOut);

            return card;
        }

        private void DrawBookIcon(Graphics g, Panel panel)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (Font font = new Font("Segoe UI", 24F, FontStyle.Bold))
            using (Brush brush = new SolidBrush(Color.White))
            {
                SizeF textSize = g.MeasureString("📖", font);
                float x = (panel.Width - textSize.Width) / 2;
                float y = (panel.Height - textSize.Height) / 2;
                g.DrawString("📖", font, brush, x, y);
            }
        }

        private void CheckOutBook(int bookId, string bookTitle)
        {

            MessageBox.Show($"Check out functionality for '{bookTitle}' would be implemented here.", 
                "Check Out", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (txtSearch.Text != SearchPlaceholder && allBooksData != null)
            {
                ApplyFilters();
            }
        }

        private void cmbCategoryFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (allBooksData != null)
            {
                ApplyFilters();
            }
        }

        private void ApplyFilters()
        {
            try
            {
                string searchText = txtSearch.Text.ToLower();
                if (searchText == SearchPlaceholder.ToLower())
                {
                    searchText = "";
                }

                string selectedCategory = cmbCategoryFilter.SelectedItem?.ToString() ?? "All";

                DataTable filteredData = allBooksData.Clone();
                foreach (DataRow row in allBooksData.Rows)
                {
                    bool matchesSearch = string.IsNullOrWhiteSpace(searchText);
                    if (!matchesSearch)
                    {
                        string title = row["Title"]?.ToString().ToLower() ?? "";
                        string author = row["Author"]?.ToString().ToLower() ?? "";
                        string isbn = row["ISBN"]?.ToString().ToLower() ?? "";
                        string bookId = row["BookID"]?.ToString() ?? "";

                        matchesSearch = title.Contains(searchText) || 
                                      author.Contains(searchText) || 
                                      isbn.Contains(searchText) || 
                                      bookId.Contains(searchText);
                    }

                    bool matchesCategory = selectedCategory == "All" || 
                                          (row["Category"] != DBNull.Value && 
                                           row["Category"].ToString() == selectedCategory);

                    if (matchesSearch && matchesCategory)
                    {
                        filteredData.ImportRow(row);
                    }
                }

                DisplayBooks(filteredData);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error applying filters: {ex.Message}");
            }
        }

        private void btnAddNewBook_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Add New Book functionality would be implemented here.", 
                "Add New Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
