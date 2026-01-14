using System;
using System.Collections.Generic;
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
using Project5LMS.Models;
using Project5LMS.Forms.LibraryStaff.Catalog;
using Project5LMS.Forms.LibraryStaff.Members;
using Project5LMS.Forms.Admin.Catalog;
using Project5LMS.Forms.Admin.Members;
namespace Project5LMS.Forms.LibraryStaff.Search
{
    public partial class StaffSearchForm : Form
    {
        private DataTable searchResults;
        private string currentFilter = "All";
        private List<string> quickSearchExamples = new List<string> { "The Great Gatsby", "Orwell", "978-0", "Sarah Johnson", "M1001", "Fiction" };
        private readonly ISearchService _searchService;
        public StaffSearchForm()
        {
            InitializeComponent();
            _searchService = ServiceFactory.CreateSearchService();
        }
        private void StaffSearchForm_Load(object sender, EventArgs e)
        {
            SetupQuickSearchExamples();
            panelResults.Visible = false;
            panelSearchGuidance.Visible = true;
            SetActiveFilter(btnAll);
            SetupSearchIcon();
        }
        private void SetupSearchIcon()
        {
            panelSearchGuidance.Paint += (s, e) =>
            {
                Panel panel = s as Panel;
                if (panel != null)
                {
                    DrawLargeSearchIcon(e.Graphics, panel);
                }
            };
        }
        private void SetupQuickSearchExamples()
        {
            flowLayoutExamples.Controls.Clear();
            foreach (string example in quickSearchExamples)
            {
                Button btnExample = new Button
                {
                    Text = example,
                    BackColor = Color.FromArgb(248, 249, 250),
                    ForeColor = Color.FromArgb(64, 64, 64),
                    FlatStyle = FlatStyle.Flat,
                    FlatAppearance = { BorderSize = 0 },
                    Font = new Font("Segoe UI", 10F),
                    AutoSize = true,
                    Padding = new Padding(15, 8, 15, 8),
                    Margin = new Padding(5),
                    Cursor = Cursors.Hand
                };
                btnExample.FlatAppearance.MouseOverBackColor = Color.FromArgb(230, 230, 230);
                btnExample.Click += (s, e) => QuickSearchExample_Click(example);
                flowLayoutExamples.Controls.Add(btnExample);
            }
        }
        private void QuickSearchExample_Click(string searchText)
        {
            txtSearch.Text = searchText;
            txtSearch.ForeColor = Color.Black;
            PerformSearch();
        }
        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search by title, author, ISBN, member name, email, or ID...")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }
        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Search by title, author, ISBN, member name, email, or ID...";
                txtSearch.ForeColor = Color.Gray;
            }
        }
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformSearch();
                e.SuppressKeyPress = true;
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }
        private void btnAll_Click(object sender, EventArgs e)
        {
            SetActiveFilter(btnAll);
            currentFilter = "All";
        }
        private void btnBooksOnly_Click(object sender, EventArgs e)
        {
            SetActiveFilter(btnBooksOnly);
            currentFilter = "Books Only";
        }
        private void btnMembersOnly_Click(object sender, EventArgs e)
        {
            SetActiveFilter(btnMembersOnly);
            currentFilter = "Members Only";
        }
        private void SetActiveFilter(Button activeButton)
        {
            btnAll.BackColor = Color.FromArgb(248, 249, 250);
            btnAll.ForeColor = Color.FromArgb(64, 64, 64);
            btnBooksOnly.BackColor = Color.FromArgb(248, 249, 250);
            btnBooksOnly.ForeColor = Color.FromArgb(64, 64, 64);
            btnMembersOnly.BackColor = Color.FromArgb(248, 249, 250);
            btnMembersOnly.ForeColor = Color.FromArgb(64, 64, 64);
            activeButton.BackColor = Color.FromArgb(139, 0, 0);
            activeButton.ForeColor = Color.White;
        }
        private void PerformSearch()
        {
            string searchText = txtSearch.Text.Trim();
            if (searchText == "Search by title, author, ISBN, member name, email, or ID..." || string.IsNullOrWhiteSpace(searchText))
            {
                MessageBox.Show("Please enter a search term.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                string searchIn = currentFilter;
                SearchResults results;
                if (searchIn == "Books Only")
                {
                    results = _searchService.SearchBooks(searchText);
                }
                else if (searchIn == "Members Only")
                {
                    results = _searchService.SearchMembers(searchText);
                }
                else
                {
                    results = _searchService.SearchAll(searchText);
                }
                System.Diagnostics.Debug.WriteLine($"StaffSearchForm.PerformSearch: Search term='{searchText}', Filter='{searchIn}'");
                System.Diagnostics.Debug.WriteLine($"StaffSearchForm.PerformSearch: Results - {results.Books.Count} books, {results.Members.Count} members, Total={results.TotalResults}");
                
                searchResults = ConvertSearchResultsToDataTable(results);
                System.Diagnostics.Debug.WriteLine($"StaffSearchForm.PerformSearch: DataTable has {searchResults.Rows.Count} rows");
                
                DisplaySearchResults();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error performing search: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Search error: {ex.Message}");
            }
        }
        private DataTable ConvertSearchResultsToDataTable(SearchResults results)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Type", typeof(string));
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Title", typeof(string));
            dt.Columns.Add("Author", typeof(string));
            dt.Columns.Add("ISBN", typeof(string));
            dt.Columns.Add("Category", typeof(string));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("MemberID", typeof(int));
            dt.Columns.Add("MemberName", typeof(string));
            dt.Columns.Add("MemberType", typeof(string));
            foreach (var book in results.Books)
            {
                DataRow row = dt.NewRow();
                row["Type"] = "Book";
                row["ID"] = book.BookID;
                row["Title"] = book.Title;
                row["Author"] = book.Author;
                row["ISBN"] = book.ISBN;
                row["Category"] = book.Category;
                row["Status"] = book.Status;
                row["MemberID"] = DBNull.Value;
                row["MemberName"] = DBNull.Value;
                row["MemberType"] = DBNull.Value;
                dt.Rows.Add(row);
            }
            foreach (var member in results.Members)
            {
                DataRow row = dt.NewRow();
                row["Type"] = "Member";
                row["ID"] = member.MemberID;
                row["Title"] = member.FullName;
                row["Author"] = member.Email;
                row["ISBN"] = DBNull.Value;
                row["Category"] = member.Type;
                row["Status"] = member.Status;
                row["MemberID"] = member.MemberID;
                row["MemberName"] = member.FullName;
                row["MemberType"] = member.Type;
                dt.Rows.Add(row);
            }
            return dt;
        }
        private string BuildSearchQuery(string filter, string searchText)
        {
            List<string> queries = new List<string>();
            if (filter == "All" || filter == "Books")
            {
                string bookQuery = @"SELECT
                                    'Book' as Type,
                                    b.BookID as ID,
                                    b.Title as Title,
                                    b.Author as Author,
                                    b.ISBN as ISBN,
                                    b.Category as Category,
                                    b.Status as Status,
                                    NULL as MemberID,
                                    NULL as MemberName,
                                    NULL as MemberType
                                    FROM Books b
                                    WHERE (b.Title LIKE @SearchText
                                    OR b.Author LIKE @SearchText
                                    OR b.ISBN LIKE @SearchText
                                    OR b.AccessionNo LIKE @SearchText
                                    OR CAST(b.BookID AS CHAR) LIKE @SearchText)";
                queries.Add(bookQuery);
            }
            if (filter == "All" || filter == "Members")
            {
                string memberQuery = @"SELECT
                                      'Member' as Type,
                                      m.MemberID as ID,
                                      CONCAT(m.FirstName, ' ', m.LastName) as Title,
                                      m.Email as Author,
                                      NULL as ISBN,
                                      COALESCE(m.Type, m.MemberType) as Category,
                                      m.Status as Status,
                                      m.MemberID as MemberID,
                                      CONCAT(m.FirstName, ' ', m.LastName) as MemberName,
                                      COALESCE(m.Type, m.MemberType) as MemberType
                                      FROM Members m
                                      WHERE (m.FirstName LIKE @SearchText
                                      OR m.LastName LIKE @SearchText
                                      OR m.Email LIKE @SearchText
                                      OR CAST(m.MemberID AS CHAR) LIKE @SearchText)";
                queries.Add(memberQuery);
            }
            if (queries.Count == 0)
            {
                return "SELECT 'No Results' as Type, 0 as ID, 'No search criteria specified' as Title, NULL as Author, NULL as ISBN, NULL as Category, NULL as Status, NULL as MemberID, NULL as MemberName, NULL as MemberType WHERE 1=0";
            }
            return string.Join(" UNION ALL ", queries) + " ORDER BY Type, Title LIMIT 100";
        }
        private void DisplaySearchResults()
        {
            if (searchResults == null || searchResults.Rows.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("StaffSearchForm.DisplaySearchResults: No results found");
                panelResults.Visible = false;
                panelSearchGuidance.Visible = true;
                lblGuidanceTitle.Text = "No results found";
                lblGuidanceSubtext.Text = "Try:\n- Different keywords\n- Checking spelling\n- Using fewer words";
                return;
            }
            System.Diagnostics.Debug.WriteLine($"StaffSearchForm.DisplaySearchResults: Displaying {searchResults.Rows.Count} results");
            SetupResultsGrid();
            dataGridViewResults.DataSource = searchResults;
            // Ensure column headers are visible and properly displayed
            dataGridViewResults.ColumnHeadersVisible = true;
            dataGridViewResults.ColumnHeadersHeight = 40;
            // Scroll to top to ensure headers are visible
            if (dataGridViewResults.Rows.Count > 0)
            {
                dataGridViewResults.FirstDisplayedScrollingRowIndex = 0;
            }
            dataGridViewResults.Refresh();
            dataGridViewResults.Update();
            panelResults.Visible = true;
            panelSearchGuidance.Visible = false;
        }
        private void SetupResultsGrid()
        {
            dataGridViewResults.AutoGenerateColumns = false;
            dataGridViewResults.Columns.Clear();
            dataGridViewResults.AllowUserToAddRows = false;
            dataGridViewResults.AllowUserToDeleteRows = false;
            dataGridViewResults.ReadOnly = true;
            dataGridViewResults.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect; // Disable row selection - search results are not clickable
            dataGridViewResults.BackgroundColor = Color.White;
            dataGridViewResults.BorderStyle = BorderStyle.None;
            dataGridViewResults.ColumnHeadersVisible = true; // Ensure column headers are visible
            dataGridViewResults.ColumnHeadersHeight = 40; // Set explicit height for column headers
            dataGridViewResults.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridViewResults.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridViewResults.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dataGridViewResults.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            dataGridViewResults.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            dataGridViewResults.RowTemplate.Height = 40;
            dataGridViewResults.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            // Disable selection highlighting - search results are read-only
            dataGridViewResults.DefaultCellStyle.SelectionBackColor = SystemColors.Window;
            dataGridViewResults.DefaultCellStyle.SelectionForeColor = SystemColors.ControlText;
            dataGridViewResults.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Type",
                HeaderText = "Type",
                DataPropertyName = "Type",
                Width = 80
            });
            dataGridViewResults.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Title",
                HeaderText = "Title/Name",
                DataPropertyName = "Title",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dataGridViewResults.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Author",
                HeaderText = "Author/Email",
                DataPropertyName = "Author",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dataGridViewResults.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ISBN",
                HeaderText = "ISBN",
                DataPropertyName = "ISBN",
                Width = 150
            });
            dataGridViewResults.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Category",
                HeaderText = "Category/Type",
                DataPropertyName = "Category",
                Width = 120
            });
            dataGridViewResults.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Status",
                DataPropertyName = "Status",
                Width = 100
            });
        }
        private void dataGridViewResults_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Disabled - search results should not be clickable or redirect to other forms
            // No action - search results are read-only
            return;
        }
        private void LoadFormInParentPanel(Form formToLoad)
        {
            try
            {
                Control parent = this.Parent;
                while (parent != null && !(parent is Form))
                {
                    parent = parent.Parent;
                }
                if (parent is Form mainForm)
                {
                    var method = mainForm.GetType().GetMethod("LoadFormInPanel");
                    if (method != null)
                    {
                        method.Invoke(mainForm, new object[] { formToLoad });
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading form in panel: {ex.Message}");
            }
        }
        private void DrawSearchIcon(Graphics g, Panel panel, int size)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int x = (panel.Width - size) / 2;
            int y = (panel.Height - size) / 2;
            using (Pen pen = new Pen(Color.FromArgb(128, 128, 128), 2))
            {
                int circleSize = (int)(size * 0.6f);
                int circleX = x + (size - circleSize) / 2;
                int circleY = y + (size - circleSize) / 2;
                g.DrawEllipse(pen, circleX, circleY, circleSize, circleSize);
                float handleStartX = circleX + circleSize * 0.7f;
                float handleStartY = circleY + circleSize * 0.7f;
                float handleEndX = handleStartX + size * 0.3f;
                float handleEndY = handleStartY + size * 0.3f;
                g.DrawLine(pen, handleStartX, handleStartY, handleEndX, handleEndY);
            }
        }
        private void DrawLargeSearchIcon(Graphics g, Panel panel)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int iconSize = 120;
            int x = (panel.Width - iconSize) / 2;
            int y = 20;
            using (Pen pen = new Pen(Color.FromArgb(200, 200, 200), 3))
            {
                float centerX = x + iconSize * 0.5f;
                float centerY = y + iconSize * 0.5f;
                float radius = iconSize * 0.35f;
                g.DrawEllipse(pen, centerX - radius, centerY - radius, radius * 2, radius * 2);
                float handleAngle = 45f * (float)Math.PI / 180f;
                float handleLength = iconSize * 0.25f;
                float handleX = centerX + radius * (float)Math.Cos(handleAngle);
                float handleY = centerY + radius * (float)Math.Sin(handleAngle);
                g.DrawLine(pen, handleX, handleY, handleX + handleLength * (float)Math.Cos(handleAngle), handleY + handleLength * (float)Math.Sin(handleAngle));
            }
            lblGuidanceTitle.Location = new Point((panel.Width - lblGuidanceTitle.Width) / 2, y + iconSize + 10);
            lblGuidanceSubtext.Location = new Point((panel.Width - lblGuidanceSubtext.Width) / 2, y + iconSize + 50);
        }
        private void lblQuickSearchTitle_Click(object sender, EventArgs e)
        {
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
        private void panelSearchGuidance_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            if (panel != null)
            {
                DrawLargeSearchIcon(e.Graphics, panel);
            }
        }
    }
}