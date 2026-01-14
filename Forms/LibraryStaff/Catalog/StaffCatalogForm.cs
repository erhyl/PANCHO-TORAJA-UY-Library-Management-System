using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
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
        private const string SearchPlaceholder = "Search by Book ID, Accession Number, Title, or Author...";
        private const int CardWidth = 600;
        private const int CardHeight = 480; // Increased height further to ensure all details are visible
        private const int CardSpacing = 20;
        private readonly DatabaseContext _dbContext;
        private System.Windows.Forms.Timer searchDebounceTimer;
        private CancellationTokenSource searchCancellationTokenSource;
        public StaffCatalogForm()
        {
            InitializeComponent();
            _bookService = ServiceFactory.CreateBookService();
            _dbContext = ServiceFactory.GetDbContext();
            
            // Initialize debounce timer for search (300ms delay)
            searchDebounceTimer = new System.Windows.Forms.Timer();
            searchDebounceTimer.Interval = 300; // Wait 300ms after user stops typing
            searchDebounceTimer.Tick += SearchDebounceTimer_Tick;
        }
        private void StaffCatalogForm_Load(object sender, EventArgs e)
        {
            // Staff role restriction: Hide Add New Book button - Staff cannot add books
            if (btnAddNewBook != null)
            {
                btnAddNewBook.Visible = false;
                btnAddNewBook.Enabled = false;
            }
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
                // Load all books initially (no filters)
                allBooksData = GetBooksData("", "All");
                DisplayBooks(allBooksData);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading books: {ex.Message}");
                MessageBox.Show($"Error loading books: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private DataTable GetBooksData(string searchText = "", string categoryFilter = "All")
        {
            try
            {
                // Fetch books directly from database using BookColumnSchema to get all available columns
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    var columns = Helpers.BookColumnSchema.GetBookColumns(conn);
                    var selectColumns = Helpers.BookColumnSchema.BuildSelectColumns(columns);
                    
                    // Build WHERE clause for database-level filtering (much faster than in-memory filtering)
                    string whereClause = BuildWhereClause(searchText, categoryFilter, columns);
                    
                    string query = $@"SELECT {string.Join(", ", selectColumns)}
                                      FROM Books
                                      {whereClause}
                                      ORDER BY Title
                                      LIMIT 1000"; // Limit results for performance
                    
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        // Add parameters for search
                        if (!string.IsNullOrWhiteSpace(searchText))
                        {
                            string searchParam = $"%{searchText}%";
                            cmd.Parameters.AddWithValue("@searchText", searchParam);
                            
                            // Add bookId parameter if search text is numeric
                            if (int.TryParse(searchText, out int bookId))
                            {
                                cmd.Parameters.AddWithValue("@bookId", bookId);
                            }
                        }
                        if (categoryFilter != "All" && !string.IsNullOrWhiteSpace(categoryFilter))
                        {
                            cmd.Parameters.AddWithValue("@category", categoryFilter);
                        }
                        
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            
                            // Ensure all expected columns exist (add if missing)
                            string[] expectedColumns = {
                                "Subtitle", "Editor", "Edition", "Language", "NumberOfPages",
                                "PhysicalDescription", "CallNumber", "Barcode", "AccessionNo",
                                "PublicationYear"
                            };
                            
                            foreach (string colName in expectedColumns)
                            {
                                if (!dt.Columns.Contains(colName))
                                {
                                    if (colName == "NumberOfPages")
                                        dt.Columns.Add(colName, typeof(int));
                                    else if (colName == "PublicationYear")
                                        dt.Columns.Add(colName, typeof(int));
                                    else
                                        dt.Columns.Add(colName, typeof(string));
                                }
                            }
                            
                            // Add YearPublished column as alias for PublicationYear (for backward compatibility)
                            if (dt.Columns.Contains("PublicationYear") && !dt.Columns.Contains("YearPublished"))
                            {
                                dt.Columns.Add("YearPublished", typeof(int));
                                foreach (DataRow row in dt.Rows)
                                {
                                    row["YearPublished"] = row["PublicationYear"] != DBNull.Value ? row["PublicationYear"] : 0;
                                }
                            }
                            
                            // Add Copies column if not present (use TotalCopies)
                            if (!dt.Columns.Contains("Copies") && dt.Columns.Contains("TotalCopies"))
                            {
                                dt.Columns.Add("Copies", typeof(int));
                                foreach (DataRow row in dt.Rows)
                                {
                                    row["Copies"] = row["TotalCopies"] != DBNull.Value ? row["TotalCopies"] : 0;
                                }
                            }
                            
                            return dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error fetching books data: {ex.Message}");
                // Fallback to service method
                var books = _bookService.GetAllBooks();
                return Helpers.DataTableHelper.BooksToDataTable(books);
            }
        }
        
        private string BuildWhereClause(string searchText, string categoryFilter, Dictionary<string, bool> columns)
        {
            var conditions = new List<string>();
            
            // Search conditions (Book ID, Accession Number, Title, Author)
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                var searchConditions = new List<string>();
                
                // BookID (exact match or contains for numeric search)
                if (int.TryParse(searchText, out int bookId))
                {
                    searchConditions.Add("BookID = @bookId");
                }
                else
                {
                    searchConditions.Add("CAST(BookID AS CHAR) LIKE @searchText");
                }
                
                // Title
                searchConditions.Add("LOWER(Title) LIKE LOWER(@searchText)");
                
                // Author
                searchConditions.Add("LOWER(Author) LIKE LOWER(@searchText)");
                
                // Accession Number
                if (columns.ContainsKey("AccessionNo") && columns["AccessionNo"])
                {
                    searchConditions.Add("LOWER(AccessionNo) LIKE LOWER(@searchText)");
                }
                else if (columns.ContainsKey("Barcode") && columns["Barcode"])
                {
                    searchConditions.Add("LOWER(Barcode) LIKE LOWER(@searchText)");
                }
                
                if (searchConditions.Count > 0)
                {
                    conditions.Add($"({string.Join(" OR ", searchConditions)})");
                }
            }
            
            // Category filter
            if (categoryFilter != "All" && !string.IsNullOrWhiteSpace(categoryFilter))
            {
                conditions.Add("Category = @category");
            }
            
            return conditions.Count > 0 ? "WHERE " + string.Join(" AND ", conditions) : "";
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
            // Debug: Log available columns
            System.Diagnostics.Debug.WriteLine($"Creating card for book. Available columns: {string.Join(", ", bookRow.Table.Columns.Cast<DataColumn>().Select(c => c.ColumnName))}");
            
            Panel card = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(CardWidth, CardHeight),
                Margin = new Padding(0, 0, CardSpacing, CardSpacing),
                Padding = new Padding(20),
                AutoScroll = false, // Disable autoscroll on card itself
                AutoSize = false // Ensure card maintains fixed size
            };
            Panel iconPanel = new Panel
            {
                BackColor = Color.FromArgb(139, 0, 0),
                Size = new Size(60, 60),
                Location = new Point(20, 20)
            };
            iconPanel.Paint += (s, e) => DrawBookIcon(e.Graphics, iconPanel);
            card.Controls.Add(iconPanel);
            
            // Category tag - position it first to calculate available space for title
            string category = bookRow["Category"] != DBNull.Value ? bookRow["Category"].ToString() : "General";
            Label lblGenre = new Label
            {
                Text = category,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(255, 192, 203),
                AutoSize = true,
                Padding = new Padding(8, 4, 8, 4)
            };
            // Measure category tag width first
            using (Graphics g = card.CreateGraphics())
            {
                SizeF categorySize = g.MeasureString(category, lblGenre.Font);
                int categoryWidth = (int)categorySize.Width + 16; // Add padding
                lblGenre.Location = new Point(CardWidth - categoryWidth - 20, 20); // 20px margin from right
            }
            card.Controls.Add(lblGenre);
            
            // Title - limit width to prevent collision with category tag
            string title = bookRow["Title"] != DBNull.Value ? bookRow["Title"].ToString() : "Unknown";
            int titleMaxWidth = lblGenre.Location.X - 110; // Leave 10px gap between title and category tag
            Label lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                AutoSize = false, // Use fixed size for better control
                Location = new Point(100, 20),
                Size = new Size(titleMaxWidth, 0), // Height will auto-adjust
                MaximumSize = new Size(titleMaxWidth, 0)
            };
            // Calculate title height
            using (Graphics g = card.CreateGraphics())
            {
                SizeF titleSize = g.MeasureString(title, lblTitle.Font, titleMaxWidth);
                lblTitle.Height = (int)Math.Ceiling(titleSize.Height);
            }
            card.Controls.Add(lblTitle);
            
            // Author - position below title with proper spacing
            string author = bookRow["Author"] != DBNull.Value ? bookRow["Author"].ToString() : "Unknown";
            int authorY = lblTitle.Location.Y + lblTitle.Height + 5; // 5px spacing after title
            Label lblAuthor = new Label
            {
                Text = $"by {author}",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(128, 128, 128),
                AutoSize = false,
                Location = new Point(100, authorY),
                Size = new Size(titleMaxWidth, 20),
                MaximumSize = new Size(titleMaxWidth, 0)
            };
            card.Controls.Add(lblAuthor);
            
            // Subtitle (if available) - position below author
            int detailY = authorY + 25; // Start details below author with spacing
            System.Diagnostics.Debug.WriteLine($"Card layout - Title Y: {lblTitle.Location.Y}, Title Height: {lblTitle.Height}, Author Y: {authorY}, Detail Y: {detailY}");
            if (bookRow.Table.Columns.Contains("Subtitle") && bookRow["Subtitle"] != DBNull.Value && !string.IsNullOrWhiteSpace(bookRow["Subtitle"].ToString()))
            {
                string subtitle = bookRow["Subtitle"].ToString();
                Label lblSubtitle = new Label
                {
                    Text = subtitle,
                    Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                    ForeColor = Color.FromArgb(100, 100, 100),
                    AutoSize = false,
                    Location = new Point(100, detailY),
                    Size = new Size(titleMaxWidth, 0),
                    MaximumSize = new Size(titleMaxWidth, 0)
                };
                card.Controls.Add(lblSubtitle);
                detailY += 20; // Add spacing after subtitle
            }
            
            // Left column details - calculate column width to prevent overflow
            int leftColumnX = 20;
            int columnWidth = (CardWidth - 60) / 2; // Divide available width (minus margins) by 2
            int rightColumnX = CardWidth / 2 + 10; // Start right column at midpoint with small gap
            int lineHeight = 22;
            
            // Ensure detailY is at least 100px from top to leave room for title/author
            // But don't force it if author is already below 100
            if (detailY < 100 && authorY < 80)
            {
                detailY = 100;
            }
            
            int currentY = detailY;
            System.Diagnostics.Debug.WriteLine($"Final positioning - detailY: {detailY}, currentY: {currentY}, Card Height: {CardHeight}, Author Y: {authorY}");
            
            // Ensure we start details at a visible position - make it more conservative
            // Start details at a fixed position below author to ensure consistency
            if (currentY < 100)
            {
                currentY = 100; // Start details at 100px from top (after title/author area)
                System.Diagnostics.Debug.WriteLine($"Adjusted currentY to {currentY} to ensure visibility");
            }
            
            // Verify currentY is within card bounds
            if (currentY > CardHeight - 150)
            {
                System.Diagnostics.Debug.WriteLine($"WARNING: currentY {currentY} is too close to card bottom {CardHeight}");
                currentY = Math.Max(100, CardHeight - 200); // Adjust to leave room
            }
            
            // Details section starts here - labels will be positioned below
            
            // Helper function to create a constrained label
            Func<string, int, int, Label> createDetailLabel = (text, x, y) =>
            {
                // Ensure Y position is within card bounds (accounting for padding)
                if (y < 0) y = currentY;
                if (y > CardHeight - 100) 
                {
                    System.Diagnostics.Debug.WriteLine($"Warning: Label Y position {y} exceeds card height {CardHeight}, adjusting...");
                    y = CardHeight - 100; // Leave room for bottom labels
                }
                
                int labelWidth = Math.Max(100, columnWidth - 10);
                int labelHeight = Math.Max(22, lineHeight); // Ensure minimum height of 22px (same as lineHeight)
                
                // Create label first, then set properties to avoid initialization issues
                Label lbl = new Label();
                lbl.Text = text ?? "N/A";
                lbl.Font = new Font("Segoe UI", 9F);
                lbl.ForeColor = Color.FromArgb(64, 64, 64);
                lbl.AutoSize = false; // CRITICAL: Must be false for Size to work
                lbl.Location = new Point(x, y);
                lbl.BackColor = Color.Transparent;
                lbl.Visible = true;
                lbl.Enabled = true;
                // Set Size AFTER all other properties - use SetBounds for explicit control
                lbl.SetBounds(x, y, labelWidth, labelHeight);
                lbl.BringToFront();
                System.Diagnostics.Debug.WriteLine($"Creating detail label: '{text}' at ({x}, {y}), Size: {lbl.Size.Width}x{lbl.Size.Height}, Visible: {lbl.Visible}, Bounds: {lbl.Bounds}");
                return lbl;
            };
            
            // ISBN - Always show (with explicit creation to ensure it works)
            try
            {
                string isbn = bookRow.Table.Columns.Contains("ISBN") && bookRow["ISBN"] != DBNull.Value ? bookRow["ISBN"].ToString() : "N/A";
                int isbnWidth = columnWidth - 10;
                int isbnHeight = Math.Max(22, lineHeight); // Use at least 22px height
                
                // Create label step by step to ensure Size is properly set
                Label lblISBN = new Label();
                lblISBN.Text = $"ISBN: {isbn}";
                lblISBN.Font = new Font("Segoe UI", 9F);
                lblISBN.ForeColor = isbn == "N/A" || string.IsNullOrWhiteSpace(isbn) ? Color.FromArgb(200, 200, 200) : Color.FromArgb(64, 64, 64);
                lblISBN.AutoSize = false; // CRITICAL: Must be false
                lblISBN.Location = new Point(leftColumnX, currentY);
                lblISBN.BackColor = Color.Transparent;
                lblISBN.Visible = true;
                lblISBN.Enabled = true;
                // Set Size AFTER AutoSize and other properties - use SetBounds
                lblISBN.SetBounds(leftColumnX, currentY, isbnWidth, isbnHeight);
                card.Controls.Add(lblISBN);
                // Set bounds again after adding to ensure it sticks
                lblISBN.SetBounds(leftColumnX, currentY, isbnWidth, isbnHeight);
                lblISBN.BringToFront();
                System.Diagnostics.Debug.WriteLine($"Added ISBN label '{lblISBN.Text}' at Y={lblISBN.Location.Y}, Size={lblISBN.Size.Width}x{lblISBN.Size.Height}, Visible={lblISBN.Visible}, Bounds={lblISBN.Bounds}");
                currentY += lineHeight;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error adding ISBN: {ex.Message}\n{ex.StackTrace}");
            }
            
            // Publisher and Year - Always show
            string publisher = bookRow.Table.Columns.Contains("Publisher") && bookRow["Publisher"] != DBNull.Value ? bookRow["Publisher"].ToString() : "N/A";
            // Check for both PublicationYear and YearPublished columns
            string year = "N/A";
            if (bookRow.Table.Columns.Contains("PublicationYear") && bookRow["PublicationYear"] != DBNull.Value)
            {
                int yearValue = Convert.ToInt32(bookRow["PublicationYear"]);
                if (yearValue > 0)
                    year = yearValue.ToString();
            }
            else if (bookRow.Table.Columns.Contains("YearPublished") && bookRow["YearPublished"] != DBNull.Value)
            {
                int yearValue = Convert.ToInt32(bookRow["YearPublished"]);
                if (yearValue > 0)
                    year = yearValue.ToString();
            }
            string publisherYear = !string.IsNullOrWhiteSpace(year) && year != "N/A" && year != "0" ? $"{publisher}, {year}" : publisher;
            try
            {
                int pubWidth = columnWidth - 10;
                int pubHeight = Math.Max(20, lineHeight);
                Label lblPublisher = new Label
                {
                    Text = $"Publisher: {publisherYear}",
                    Font = new Font("Segoe UI", 9F),
                    ForeColor = publisher == "N/A" || string.IsNullOrWhiteSpace(publisher) ? Color.FromArgb(200, 200, 200) : Color.FromArgb(64, 64, 64),
                    AutoSize = false, // CRITICAL: Must be false
                    Location = new Point(leftColumnX, currentY),
                    Size = new Size(pubWidth, pubHeight),
                    BackColor = Color.Transparent,
                    Visible = true,
                    Enabled = true
                };
                // Use SetBounds to explicitly set bounds
                lblPublisher.SetBounds(leftColumnX, currentY, pubWidth, pubHeight);
                card.Controls.Add(lblPublisher);
                // Set bounds again after adding to ensure it sticks
                lblPublisher.SetBounds(leftColumnX, currentY, pubWidth, pubHeight);
                lblPublisher.BringToFront();
                System.Diagnostics.Debug.WriteLine($"Added Publisher label at Y={lblPublisher.Location.Y}, Size={lblPublisher.Size.Width}x{lblPublisher.Size.Height}");
                currentY += lineHeight;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error adding Publisher: {ex.Message}");
            }
            
            // Editor (if available)
            if (bookRow.Table.Columns.Contains("Editor") && bookRow["Editor"] != DBNull.Value && !string.IsNullOrWhiteSpace(bookRow["Editor"].ToString()))
            {
                Label lblEditor = createDetailLabel($"Editor: {bookRow["Editor"].ToString()}", leftColumnX, currentY);
                card.Controls.Add(lblEditor);
                // Use SetBounds to explicitly set size after adding to card
                int editorHeight = Math.Max(22, lineHeight);
                lblEditor.SetBounds(leftColumnX, currentY, columnWidth - 10, editorHeight);
                currentY += lineHeight;
            }
            
            // Edition (if available)
            if (bookRow.Table.Columns.Contains("Edition") && bookRow["Edition"] != DBNull.Value && !string.IsNullOrWhiteSpace(bookRow["Edition"].ToString()))
            {
                Label lblEdition = createDetailLabel($"Edition: {bookRow["Edition"].ToString()}", leftColumnX, currentY);
                card.Controls.Add(lblEdition);
                // Use SetBounds to explicitly set size after adding to card
                int editionHeight = Math.Max(22, lineHeight);
                lblEdition.SetBounds(leftColumnX, currentY, columnWidth - 10, editionHeight);
                currentY += lineHeight;
            }
            
            // Language (if available)
            if (bookRow.Table.Columns.Contains("Language") && bookRow["Language"] != DBNull.Value && !string.IsNullOrWhiteSpace(bookRow["Language"].ToString()))
            {
                Label lblLanguage = createDetailLabel($"Language: {bookRow["Language"].ToString()}", leftColumnX, currentY);
                card.Controls.Add(lblLanguage);
                // Use SetBounds to explicitly set size after adding to card
                int langHeight = Math.Max(22, lineHeight);
                lblLanguage.SetBounds(leftColumnX, currentY, columnWidth - 10, langHeight);
                currentY += lineHeight;
            }
            
            // Right column details - Always show Location
            int rightColumnY = detailY;
            
            // Location - Always show
            string location = bookRow.Table.Columns.Contains("Location") && bookRow["Location"] != DBNull.Value ? bookRow["Location"].ToString() : "N/A";
            try
            {
                int locWidth = columnWidth - 10;
                int locHeight = Math.Max(22, lineHeight);
                
                // Create label step by step
                Label lblLocation = new Label();
                lblLocation.Text = $"Location: {location}";
                lblLocation.Font = new Font("Segoe UI", 9F);
                lblLocation.ForeColor = location == "N/A" || string.IsNullOrWhiteSpace(location) ? Color.FromArgb(200, 200, 200) : Color.FromArgb(64, 64, 64);
                lblLocation.AutoSize = false;
                lblLocation.Location = new Point(rightColumnX, rightColumnY);
                lblLocation.BackColor = Color.Transparent;
                lblLocation.Visible = true;
                lblLocation.Enabled = true;
                // Set Size AFTER other properties
                lblLocation.Size = new Size(locWidth, locHeight);
                card.Controls.Add(lblLocation);
                // Use SetBounds to explicitly set bounds after adding to card
                lblLocation.SetBounds(rightColumnX, rightColumnY, locWidth, locHeight);
                lblLocation.BringToFront();
                System.Diagnostics.Debug.WriteLine($"Added Location label at Y={lblLocation.Location.Y}, Size={lblLocation.Size.Width}x{lblLocation.Size.Height}");
                rightColumnY += lineHeight;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error adding Location: {ex.Message}");
            }
            
            // Book ID - Always show (primary identifier)
            try
            {
                int bookId = bookRow.Table.Columns.Contains("BookID") && bookRow["BookID"] != DBNull.Value 
                    ? Convert.ToInt32(bookRow["BookID"]) : 0;
                if (bookId > 0)
                {
                    int bookIdWidth = columnWidth - 10;
                    int bookIdHeight = Math.Max(22, lineHeight);
                    
                    // Create label step by step
                    Label lblBookID = new Label();
                    lblBookID.Text = $"Book ID: {bookId}";
                    lblBookID.Font = new Font("Segoe UI", 9F);
                    lblBookID.ForeColor = Color.FromArgb(64, 64, 64);
                    lblBookID.AutoSize = false;
                    lblBookID.Location = new Point(rightColumnX, rightColumnY);
                    lblBookID.BackColor = Color.Transparent;
                    lblBookID.Visible = true;
                    lblBookID.Enabled = true;
                    // Use SetBounds to explicitly set bounds
                    lblBookID.SetBounds(rightColumnX, rightColumnY, bookIdWidth, bookIdHeight);
                    card.Controls.Add(lblBookID);
                    // Set bounds again after adding to ensure it sticks
                    lblBookID.SetBounds(rightColumnX, rightColumnY, bookIdWidth, bookIdHeight);
                    lblBookID.BringToFront();
                    System.Diagnostics.Debug.WriteLine($"Added Book ID label at Y={lblBookID.Location.Y}, Size={lblBookID.Size.Width}x{lblBookID.Size.Height}");
                    rightColumnY += lineHeight;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error adding Book ID: {ex.Message}");
            }
            
            // Call Number (if available)
            if (bookRow.Table.Columns.Contains("CallNumber") && bookRow["CallNumber"] != DBNull.Value && !string.IsNullOrWhiteSpace(bookRow["CallNumber"].ToString()))
            {
                Label lblCallNumber = createDetailLabel($"Call #: {bookRow["CallNumber"].ToString()}", rightColumnX, rightColumnY);
                card.Controls.Add(lblCallNumber);
                // Use SetBounds to explicitly set size after adding
                int callHeight = Math.Max(22, lineHeight);
                lblCallNumber.SetBounds(rightColumnX, rightColumnY, columnWidth - 10, callHeight);
                rightColumnY += lineHeight;
            }
            
            // Accession Number (if available)
            if (bookRow.Table.Columns.Contains("AccessionNo") && bookRow["AccessionNo"] != DBNull.Value && !string.IsNullOrWhiteSpace(bookRow["AccessionNo"].ToString()))
            {
                Label lblAccession = createDetailLabel($"Accession: {bookRow["AccessionNo"].ToString()}", rightColumnX, rightColumnY);
                card.Controls.Add(lblAccession);
                // Use SetBounds to explicitly set size after adding
                int accHeight = Math.Max(22, lineHeight);
                lblAccession.SetBounds(rightColumnX, rightColumnY, columnWidth - 10, accHeight);
                rightColumnY += lineHeight;
            }
            
            // Book Type (if available)
            if (bookRow.Table.Columns.Contains("BookType") && bookRow["BookType"] != DBNull.Value && !string.IsNullOrWhiteSpace(bookRow["BookType"].ToString()))
            {
                Label lblBookType = createDetailLabel($"Type: {bookRow["BookType"].ToString()}", rightColumnX, rightColumnY);
                card.Controls.Add(lblBookType);
                // Use SetBounds to explicitly set size after adding
                int typeHeight = Math.Max(22, lineHeight);
                lblBookType.SetBounds(rightColumnX, rightColumnY, columnWidth - 10, typeHeight);
                rightColumnY += lineHeight;
            }
            
            // Number of Pages (if available)
            if (bookRow.Table.Columns.Contains("NumberOfPages") && bookRow["NumberOfPages"] != DBNull.Value)
            {
                int pages = Convert.ToInt32(bookRow["NumberOfPages"]);
                if (pages > 0)
                {
                    Label lblPages = createDetailLabel($"Pages: {pages}", rightColumnX, rightColumnY);
                    card.Controls.Add(lblPages);
                    // Use SetBounds to explicitly set size after adding
                    int pagesHeight = Math.Max(22, lineHeight);
                    lblPages.SetBounds(rightColumnX, rightColumnY, columnWidth - 10, pagesHeight);
                    rightColumnY += lineHeight;
                }
            }
            
            // Copies and Availability at bottom - ensure they don't overflow
            int totalCopies = bookRow["Copies"] != DBNull.Value ? Convert.ToInt32(bookRow["Copies"]) : 
                            (bookRow.Table.Columns.Contains("TotalCopies") && bookRow["TotalCopies"] != DBNull.Value ? Convert.ToInt32(bookRow["TotalCopies"]) : 0);
            int available = bookRow["Available"] != DBNull.Value ? Convert.ToInt32(bookRow["Available"]) : 0;
            int bottomY = CardHeight - 60;
            int maxBottomWidth = CardWidth - 40; // Leave 20px margin on each side
            
            Label lblCopies = new Label
            {
                Text = $"Total Copies: {totalCopies}, ",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(128, 128, 128),
                AutoSize = false,
                Location = new Point(20, bottomY),
                Size = new Size(maxBottomWidth / 2, 20)
            };
            card.Controls.Add(lblCopies);
            Label lblAvailable = new Label
            {
                Text = $"{available} Available",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 167, 69),
                AutoSize = false,
                Location = new Point(lblCopies.Right, bottomY),
                Size = new Size(maxBottomWidth / 2, 20)
            };
            card.Controls.Add(lblAvailable);
            // Staff role restriction: Check Out button removed - Staff cannot check out books from Catalog
            // Check out functionality is only available in the Circulation module
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
            // Cancel previous search if still running
            if (searchCancellationTokenSource != null)
            {
                searchCancellationTokenSource.Cancel();
            }
            
            // Reset and start debounce timer
            searchDebounceTimer.Stop();
            searchDebounceTimer.Start();
        }
        
        private void SearchDebounceTimer_Tick(object sender, EventArgs e)
        {
            searchDebounceTimer.Stop();
            
            // Perform search asynchronously
            if (txtSearch.Text != SearchPlaceholder)
            {
                ApplyFiltersAsync();
            }
            else if (allBooksData != null)
            {
                // Show all books if search is cleared
                DisplayBooks(allBooksData);
            }
        }
        private void cmbCategoryFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Category filter changes don't need debouncing - apply immediately
            ApplyFilters();
        }
        private async void ApplyFiltersAsync()
        {
            try
            {
                // Show loading indicator
                panelBooksContainer.Controls.Clear();
                Label lblLoading = new Label
                {
                    Text = "Searching...",
                    Font = new Font("Segoe UI", 12F),
                    ForeColor = Color.Gray,
                    AutoSize = true,
                    Margin = new Padding(20, 20, 0, 0)
                };
                panelBooksContainer.Controls.Add(lblLoading);
                
                // Create new cancellation token
                searchCancellationTokenSource = new CancellationTokenSource();
                var token = searchCancellationTokenSource.Token;
                
                string searchText = txtSearch.Text;
                if (searchText == SearchPlaceholder)
                {
                    searchText = "";
                }
                string selectedCategory = cmbCategoryFilter.SelectedItem?.ToString() ?? "All";
                
                // Perform database query asynchronously
                DataTable filteredData = await Task.Run(() =>
                {
                    if (token.IsCancellationRequested) return null;
                    return GetBooksData(searchText, selectedCategory);
                }, token);
                
                // Check if operation was cancelled
                if (token.IsCancellationRequested || filteredData == null)
                {
                    return;
                }
                
                // Update UI on UI thread
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => DisplayBooks(filteredData)));
                }
                else
                {
                    DisplayBooks(filteredData);
                }
            }
            catch (OperationCanceledException)
            {
                // Search was cancelled, ignore
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error applying filters: {ex.Message}");
                MessageBox.Show($"Error searching books: {ex.Message}", "Search Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        
        private void ApplyFilters()
        {
            // Synchronous version for category filter changes (faster, no debounce needed)
            try
            {
                string searchText = txtSearch.Text;
                if (searchText == SearchPlaceholder)
                {
                    searchText = "";
                }
                string selectedCategory = cmbCategoryFilter.SelectedItem?.ToString() ?? "All";
                
                // Use database-level filtering for better performance
                DataTable filteredData = GetBooksData(searchText, selectedCategory);
                DisplayBooks(filteredData);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error applying filters: {ex.Message}");
            }
        }
        private void btnAddNewBook_Click(object sender, EventArgs e)
        {
            // Staff role restriction: Staff cannot add books
            MessageBox.Show("Only administrators can add new books to the catalog.", 
                "Access Restricted", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}