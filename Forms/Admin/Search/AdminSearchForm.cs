using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel;
using MySql.Data.MySqlClient;
using Project5LMS.Helpers;
using Project5LMS.Services;
using Project5LMS.Data;
using Project5LMS.Interfaces;
using Project5LMS.Models;
using Project5LMS.Forms.Admin.Catalog;
using Project5LMS.Forms.Admin.Members;
namespace Project5LMS.Forms.Admin.Search
{
    public partial class AdminSearchForm : Form
    {
        private DataTable searchResults;
        private Panel panelResults;
        private DataGridView dataGridViewResults;
        private readonly ISearchService _searchService;
        public AdminSearchForm()
        {
            InitializeComponent();
            _searchService = ServiceFactory.CreateSearchService();
        }
        private void AdminSearchForm_Load(object sender, EventArgs e)
        {
            InitializeSearchInDropdown();
            LoadCategories();
            SetupResultsPanel();
        }
        private void InitializeSearchInDropdown()
        {
            cmbSearchIn.Items.AddRange(new string[] { "All", "Books", "Members", "Resources" });
            cmbSearchIn.SelectedIndex = 0;
        }
        private void LoadCategories()
        {
            try
            {
                cmbCategory.Items.Clear();
                cmbCategory.Items.Add("All Categories");
                var categories = _searchService.GetBookCategories();
                foreach (var category in categories)
                {
                    cmbCategory.Items.Add(category);
                }
                cmbCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading categories: {ex.Message}");
            }
        }
        private void SetupResultsPanel()
        {
            panelResults = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(20),
                Visible = false
            };
            Label lblResultsTitle = new Label
            {
                Text = "Search Results",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                AutoSize = true,
                Location = new Point(20, 20)
            };
            dataGridViewResults = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToOrderColumns = true,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                Margin = new Padding(0, 50, 0, 0),
                MultiSelect = false,
                RowHeadersVisible = false,
                RowHeadersWidth = 51,
                RowTemplate = { Height = 60 },
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                GridColor = Color.FromArgb(240, 240, 240),
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(250, 250, 250)
                },
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleLeft,
                    BackColor = Color.FromArgb(248, 249, 250),
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(64, 64, 64),
                    SelectionBackColor = SystemColors.Highlight,
                    SelectionForeColor = SystemColors.HighlightText,
                    WrapMode = DataGridViewTriState.True
                },
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleLeft,
                    BackColor = SystemColors.Window,
                    Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                    ForeColor = SystemColors.ControlText,
                    Padding = new Padding(10, 5, 10, 5),
                    SelectionBackColor = SystemColors.Highlight,
                    SelectionForeColor = SystemColors.HighlightText,
                    WrapMode = DataGridViewTriState.False
                }
            };
            panelResults.Controls.Add(lblResultsTitle);
            panelResults.Controls.Add(dataGridViewResults);
            panelMainContainer.Controls.Add(panelResults);
            panelResults.BringToFront();
        }
        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "🔍 Search by title, author, ISBN, member name, or ID...")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }
        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "🔍 Search by title, author, ISBN, member name, or ID...";
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
        private void PerformSearch()
        {
            string searchText = txtSearch.Text.Trim();
            if (searchText == "🔍 Search by title, author, ISBN, member name, or ID..." || string.IsNullOrWhiteSpace(searchText))
            {
                MessageBox.Show("Please enter a search term.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string searchIn = cmbSearchIn.SelectedItem?.ToString() ?? "All";
            string category = cmbCategory.SelectedItem?.ToString();
            if (category == "All Categories")
                category = null;
            try
            {
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
                if (results.SearchTime > 2000)
                {
                    AuditLogger.Log("Search Performance Warning",
                        $"Search: '{searchText}', Type: {searchIn}, Time: {results.SearchTime}ms, Results: {results.TotalResults}",
                        "Performance Warning");
                }
                searchResults = ConvertSearchResultsToDataTable(results);
                DisplaySearchResults();
            }
            catch (Exception ex)
            {
                AuditLogger.Log("Search Error",
                    $"Search: '{searchText}', Type: {searchIn}, Error: {ex.Message}",
                    "Failed");
                MessageBox.Show($"Error performing search: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void DisplaySearchResults()
        {
            if (searchResults == null || searchResults.Rows.Count == 0)
            {
                MessageBox.Show("No results found.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                panelResults.Visible = false;
                return;
            }
            dataGridViewResults.DataSource = searchResults;
            FormatResultsGrid();
            panelResults.Visible = true;
            panelResults.BringToFront();
        }
        private void FormatResultsGrid()
        {
            dataGridViewResults.Columns.Clear();
            var columns = new Dictionary<string, (string Header, int Width, DataGridViewAutoSizeColumnMode AutoSize)>
            {
                { "Type", ("Type", 80, DataGridViewAutoSizeColumnMode.None) },
                { "Title", ("Title/Name", 0, DataGridViewAutoSizeColumnMode.Fill) },
                { "Author", ("Author/Email", 0, DataGridViewAutoSizeColumnMode.Fill) },
                { "ISBN", ("ISBN", 150, DataGridViewAutoSizeColumnMode.None) },
                { "Category", ("Category/Type", 120, DataGridViewAutoSizeColumnMode.None) },
                { "Status", ("Status", 100, DataGridViewAutoSizeColumnMode.None) }
            };
            foreach (var col in columns)
            {
                if (searchResults.Columns.Contains(col.Key))
                {
                    dataGridViewResults.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = col.Key,
                        HeaderText = col.Value.Header,
                        DataPropertyName = col.Key,
                        Width = col.Value.Width,
                        AutoSizeMode = col.Value.AutoSize
                    });
                }
            }
            dataGridViewResults.CellDoubleClick += DataGridViewResults_CellDoubleClick;
        }
        private void DataGridViewResults_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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
        private void lblCategoryLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var categoryForm = new CategoryBrowseForm())
            {
                categoryForm.ShowDialog();
            }
        }
        private void lblAuthorLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                var authors = ServiceFactory.CreateBookService().GetAllAuthors().ToList();
                if (authors.Count == 0)
                {
                    MessageBox.Show("No authors found in the catalog.", "No Authors", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                using (var authorDialog = new Form())
                {
                    authorDialog.Text = "Select Author";
                    authorDialog.Size = new Size(400, 500);
                    authorDialog.StartPosition = FormStartPosition.CenterParent;
                    authorDialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                    authorDialog.MaximizeBox = false;
                    authorDialog.MinimizeBox = false;
                    var listBox = new ListBox
                    {
                        Dock = DockStyle.Fill,
                        Font = new Font("Segoe UI", 10F)
                    };
                    listBox.Items.AddRange(authors.ToArray());
                    var btnSelect = new Button
                    {
                        Text = "Browse",
                        Dock = DockStyle.Bottom,
                        Height = 40,
                        DialogResult = DialogResult.OK
                    };
                    authorDialog.Controls.Add(listBox);
                    authorDialog.Controls.Add(btnSelect);
                    if (authorDialog.ShowDialog() == DialogResult.OK && listBox.SelectedItem != null)
                    {
                        string selectedAuthor = listBox.SelectedItem.ToString();
                        var books = ServiceFactory.CreateBookService().GetBooksByAuthor(selectedAuthor);
                        ShowBrowseResults($"Books by {selectedAuthor}", books);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading authors: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lblNewLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var filterForm = new BrowseFiltersForm("New Arrivals"))
            {
                if (filterForm.ShowDialog() == DialogResult.OK)
                {
                    var newArrivals = ServiceFactory.CreateBookService().GetNewArrivals(
                        50,
                        filterForm.StartDate,
                        filterForm.EndDate);
                    ShowBrowseResults("New Arrivals", newArrivals);
                }
            }
        }
        private void lblPopularLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var filterForm = new BrowseFiltersForm("Popular Books"))
            {
                if (filterForm.ShowDialog() == DialogResult.OK)
                {
                    var popularBooks = ServiceFactory.CreateBookService().GetPopularBooks(
                        50,
                        filterForm.UseWeightedPopularity);
                    ShowBrowseResults("Popular Books", popularBooks);
                }
            }
        }
        private void ShowBrowseResults(string title, IEnumerable<Book> books)
        {
            try
            {
                searchResults = new DataTable();
                searchResults.Columns.Add("Type", typeof(string));
                searchResults.Columns.Add("ID", typeof(int));
                searchResults.Columns.Add("Title", typeof(string));
                searchResults.Columns.Add("Author", typeof(string));
                searchResults.Columns.Add("ISBN", typeof(string));
                searchResults.Columns.Add("Category", typeof(string));
                searchResults.Columns.Add("Status", typeof(string));
                foreach (var book in books)
                {
                    DataRow row = searchResults.NewRow();
                    row["Type"] = "Book";
                    row["ID"] = book.BookID;
                    row["Title"] = book.Title;
                    row["Author"] = book.Author;
                    row["ISBN"] = book.ISBN;
                    row["Category"] = book.Category;
                    row["Status"] = book.Status;
                    searchResults.Rows.Add(row);
                }
                DisplaySearchResults();
                txtSearch.Text = title;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading {title}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void DrawBookIcon(Graphics g, Panel panel, Color iconColor)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int size = Math.Min(panel.Width, panel.Height) - 10;
            int x = (panel.Width - size) / 2;
            int y = (panel.Height - size) / 2;
            using (Pen pen = new Pen(iconColor, 2))
            {
                g.DrawRectangle(pen, x + size * 0.2f, y + size * 0.1f, size * 0.6f, size * 0.8f);
                g.DrawLine(pen, x + size * 0.5f, y + size * 0.1f, x + size * 0.5f, y + size * 0.9f);
                g.DrawLine(pen, x + size * 0.3f, y + size * 0.3f, x + size * 0.7f, y + size * 0.3f);
                g.DrawLine(pen, x + size * 0.3f, y + size * 0.5f, x + size * 0.7f, y + size * 0.5f);
                g.DrawLine(pen, x + size * 0.3f, y + size * 0.7f, x + size * 0.7f, y + size * 0.7f);
            }
        }
        private void DrawCalendarIcon(Graphics g, Panel panel, Color iconColor)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int size = Math.Min(panel.Width, panel.Height) - 10;
            int x = (panel.Width - size) / 2;
            int y = (panel.Height - size) / 2;
            using (Pen pen = new Pen(iconColor, 2))
            {
                g.DrawRectangle(pen, x + size * 0.2f, y + size * 0.3f, size * 0.6f, size * 0.6f);
                g.DrawLine(pen, x + size * 0.2f, y + size * 0.45f, x + size * 0.8f, y + size * 0.45f);
                float centerX = x + size * 0.5f;
                float centerY = y + size * 0.65f;
                float lineLength = size * 0.15f;
                g.DrawLine(pen, centerX - lineLength, centerY, centerX + lineLength, centerY);
                g.DrawLine(pen, centerX, centerY - lineLength, centerX, centerY + lineLength);
            }
        }
        private void DrawFilterIcon(Graphics g, Panel panel, Color iconColor)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int size = Math.Min(panel.Width, panel.Height) - 10;
            int x = (panel.Width - size) / 2;
            int y = (panel.Height - size) / 2;
            using (Pen pen = new Pen(iconColor, 2))
            {
                float topWidth = size * 0.6f;
                float bottomWidth = size * 0.2f;
                float height = size * 0.6f;
                float topX = x + (size - topWidth) / 2;
                float bottomX = x + (size - bottomWidth) / 2;
                float topY = y + size * 0.2f;
                float bottomY = topY + height;
                g.DrawLine(pen, topX, topY, bottomX, bottomY);
                g.DrawLine(pen, topX + topWidth, topY, bottomX + bottomWidth, bottomY);
                g.DrawLine(pen, bottomX, bottomY, bottomX + bottomWidth, bottomY);
            }
        }
        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void panelCards_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}