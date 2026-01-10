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
using Project5LMS.Interfaces;
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
            SetActiveFilter(btnAll);
        }
        private void SetupQuickSearchExamples()
        {
            flowLayoutExamples.Controls.Clear();
            foreach (string example in quickSearchExamples)
            {
                Button btnExample = new Button
                {
                    Text = example,
                    BackColor = Color.White,
                    ForeColor = Color.FromArgb(64, 64, 64),
                    FlatStyle = FlatStyle.Flat,
                    FlatAppearance = { BorderColor = Color.FromArgb(200, 200, 200), BorderSize = 1 },
                    Font = new Font("Segoe UI", 10F),
                    AutoSize = true,
                    Padding = new Padding(15, 8, 15, 8),
                    Margin = new Padding(5),
                    Cursor = Cursors.Hand
                };
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
            if (!string.IsNullOrWhiteSpace(txtSearch.Text) && txtSearch.Text != "Search by title, author, ISBN, member name, email, or ID...")
            {
                PerformSearch();
            }
        }
        private void btnBooksOnly_Click(object sender, EventArgs e)
        {
            SetActiveFilter(btnBooksOnly);
            currentFilter = "Books";
            if (!string.IsNullOrWhiteSpace(txtSearch.Text) && txtSearch.Text != "Search by title, author, ISBN, member name, email, or ID...")
            {
                PerformSearch();
            }
        }
        private void btnMembersOnly_Click(object sender, EventArgs e)
        {
            SetActiveFilter(btnMembersOnly);
            currentFilter = "Members";
            if (!string.IsNullOrWhiteSpace(txtSearch.Text) && txtSearch.Text != "Search by title, author, ISBN, member name, email, or ID...")
            {
                PerformSearch();
            }
        }
        private void SetActiveFilter(Button activeButton)
        {
            btnAll.BackColor = Color.Transparent;
            btnAll.ForeColor = Color.FromArgb(128, 128, 128);
            btnBooksOnly.BackColor = Color.Transparent;
            btnBooksOnly.ForeColor = Color.FromArgb(128, 128, 128);
            btnMembersOnly.BackColor = Color.Transparent;
            btnMembersOnly.ForeColor = Color.FromArgb(128, 128, 128);
            activeButton.BackColor = Color.FromArgb(178, 34, 34);
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
                string searchIn = currentFilter == "Books" ? "Books" : (currentFilter == "Members" ? "Members" : "All");
                string category = "";
                SearchResults results;
                if (searchIn == "Books")
                {
                    results = _searchService.SearchBooks(searchText);
                }
                else if (searchIn == "Members")
                {
                    results = _searchService.SearchMembers(searchText);
                }
                else
                {
                    results = _searchService.SearchAll(searchText);
                }
                if (category != null && results.Books != null)
                {
                    results.Books = results.Books.Where(b => b.Category == category).ToList();
                    results.TotalResults = results.Books.Count + results.Members.Count;
                }
                searchResults = ConvertSearchResultsToDataTable(results);
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
                MessageBox.Show("No results found.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                panelResults.Visible = false;
                panelSearchGuidance.Visible = true;
                return;
            }
            SetupResultsGrid();
            dataGridViewResults.DataSource = searchResults;
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
            dataGridViewResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewResults.BackgroundColor = Color.White;
            dataGridViewResults.BorderStyle = BorderStyle.None;
            dataGridViewResults.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridViewResults.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridViewResults.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dataGridViewResults.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            dataGridViewResults.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            dataGridViewResults.RowTemplate.Height = 40;
            dataGridViewResults.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
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
            if (e.RowIndex < 0) return;
            DataRowView row = dataGridViewResults.Rows[e.RowIndex].DataBoundItem as DataRowView;
            if (row == null) return;
            string type = row["Type"]?.ToString();
            Form formToLoad = null;
            if (type == "Book")
            {
                formToLoad = new AdminCatalogForm();
            }
            else if (type == "Member")
            {
                formToLoad = new AdminMembersForm();
            }
            if (formToLoad != null)
            {
                LoadFormInParentPanel(formToLoad);
            }
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
            int size = Math.Min(panel.Width, panel.Height) - 10;
            int x = (panel.Width - size) / 2;
            int y = (panel.Height - size) / 2;
            using (Pen pen = new Pen(Color.FromArgb(200, 200, 200), 3))
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
        private void lblQuickSearchTitle_Click(object sender, EventArgs e)
        {
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
    }
}