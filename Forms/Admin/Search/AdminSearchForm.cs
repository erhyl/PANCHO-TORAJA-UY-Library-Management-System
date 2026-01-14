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
        private DataGridView dataGridViewAllResults;
        private DataGridView dataGridViewBooksResults;
        private DataGridView dataGridViewMembersResults;
        private Panel panelAllResults;
        private Panel panelBooksResults;
        private Panel panelMembersResults;
        private readonly ISearchService _searchService;
        private string currentFilter = "All";
        private Button btnFilterAll;
        private Button btnFilterBooks;
        private Button btnFilterMembers;
        private Panel panelQuickExamples;
        public AdminSearchForm()
        {
            InitializeComponent();
            _searchService = ServiceFactory.CreateSearchService();
        }
        private void AdminSearchForm_Load(object sender, EventArgs e)
        {
            InitializeFilterButtons();
            LoadCategories();
            SetupResultsPanel();
            SetupQuickExamples();
        }
        private void InitializeFilterButtons()
        {
            currentFilter = "All";
            panelFilters.Controls.Clear();
            Label lblFilter = new Label
            {
                Text = "Search in:",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(64, 64, 64),
                AutoSize = true,
                Location = new Point(0, 10)
            };
            panelFilters.Controls.Add(lblFilter);
            btnFilterAll = new Button
            {
                Text = "All",
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.FromArgb(70, 130, 180),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                AutoSize = true,
                Padding = new Padding(15, 8, 15, 8),
                Location = new Point(100, 5),
                Cursor = Cursors.Hand
            };
            btnFilterAll.Click += (s, ev) => SetFilter("All", btnFilterAll);
            panelFilters.Controls.Add(btnFilterAll);
            btnFilterBooks = new Button
            {
                Text = "Books Only",
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.FromArgb(248, 249, 250),
                ForeColor = Color.FromArgb(64, 64, 64),
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                AutoSize = true,
                Padding = new Padding(15, 8, 15, 8),
                Location = new Point(btnFilterAll.Right + 10, 5),
                Cursor = Cursors.Hand
            };
            btnFilterBooks.Click += (s, ev) => SetFilter("Books Only", btnFilterBooks);
            panelFilters.Controls.Add(btnFilterBooks);
            btnFilterMembers = new Button
            {
                Text = "Members Only",
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.FromArgb(248, 249, 250),
                ForeColor = Color.FromArgb(64, 64, 64),
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                AutoSize = true,
                Padding = new Padding(15, 8, 15, 8),
                Location = new Point(btnFilterBooks.Right + 10, 5),
                Cursor = Cursors.Hand
            };
            btnFilterMembers.Click += (s, ev) => SetFilter("Members Only", btnFilterMembers);
            panelFilters.Controls.Add(btnFilterMembers);
        }
        private void SetFilter(string filter, Button activeButton)
        {
            currentFilter = filter;
            btnFilterAll.BackColor = Color.FromArgb(248, 249, 250);
            btnFilterAll.ForeColor = Color.FromArgb(64, 64, 64);
            btnFilterBooks.BackColor = Color.FromArgb(248, 249, 250);
            btnFilterBooks.ForeColor = Color.FromArgb(64, 64, 64);
            btnFilterMembers.BackColor = Color.FromArgb(248, 249, 250);
            btnFilterMembers.ForeColor = Color.FromArgb(64, 64, 64);
            activeButton.BackColor = Color.FromArgb(70, 130, 180);
            activeButton.ForeColor = Color.White;
            
            // Show/hide appropriate panels
            if (panelAllResults != null) panelAllResults.Visible = (filter == "All");
            if (panelBooksResults != null) panelBooksResults.Visible = (filter == "Books Only");
            if (panelMembersResults != null) panelMembersResults.Visible = (filter == "Members Only");
        }
        private void LoadCategories()
        {
            try
            {
                cmbCategory.Items.Clear();
                cmbCategory.Items.Add("All Categories");
                var categories = ServiceFactory.CreateBookService().GetAllCategories();
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
            // Clear any existing controls in panelSearchOutput
            panelSearchOutput.Controls.Clear();
            
            // Create 3 separate panels for different filters
            panelAllResults = CreateResultsPanel("Search Results - All");
            panelBooksResults = CreateResultsPanel("Search Results - Books");
            panelMembersResults = CreateResultsPanel("Search Results - Members");
            
            // Create DataGridViews for each panel
            dataGridViewAllResults = CreateDataGridView(panelAllResults);
            dataGridViewBooksResults = CreateDataGridView(panelBooksResults);
            dataGridViewMembersResults = CreateDataGridView(panelMembersResults);
            
            // Initially show only All panel
            panelAllResults.Visible = true;
            panelBooksResults.Visible = false;
            panelMembersResults.Visible = false;
            
            panelSearchOutput.Controls.Add(panelAllResults);
            panelSearchOutput.Controls.Add(panelBooksResults);
            panelSearchOutput.Controls.Add(panelMembersResults);
            panelSearchOutput.Visible = false;
        }
        
        private Panel CreateResultsPanel(string title)
        {
            Panel panel = new Panel
            {
                Dock = DockStyle.Fill,
                Visible = false
            };
            
            Label lblResultsTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                AutoSize = true,
                Location = new Point(0, 0),
                Padding = new Padding(0, 0, 0, 10),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            
            panel.Controls.Add(lblResultsTitle);
            return panel;
        }
        
        private DataGridView CreateDataGridView(Panel parentPanel)
        {
            DataGridView dgv = new DataGridView
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AutoGenerateColumns = false, // Prevent auto-generation of columns
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToOrderColumns = true,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                Location = new Point(0, 35),
                Size = new Size(parentPanel.Width - 20, parentPanel.Height - 45),
                MultiSelect = false,
                RowHeadersVisible = false,
                RowHeadersWidth = 51,
                RowTemplate = { Height = 60 },
                SelectionMode = DataGridViewSelectionMode.RowHeaderSelect,
                ColumnHeadersVisible = true,
                ColumnHeadersHeight = 40,
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
                    SelectionBackColor = Color.FromArgb(248, 249, 250),
                    SelectionForeColor = Color.FromArgb(64, 64, 64),
                    WrapMode = DataGridViewTriState.True
                },
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleLeft,
                    BackColor = SystemColors.Window,
                    Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                    ForeColor = SystemColors.ControlText,
                    Padding = new Padding(10, 5, 10, 5),
                    SelectionBackColor = SystemColors.Window,
                    SelectionForeColor = SystemColors.ControlText,
                    WrapMode = DataGridViewTriState.False
                }
            };
            
            parentPanel.Controls.Add(dgv);
            return dgv;
        }
        private void SetupQuickExamples()
        {
            panelQuickExamples = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(20),
                Location = new Point(24, 688),
                Size = new Size(1777, 120),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            Label lblQuickTitle = new Label
            {
                Text = "Quick Search Examples",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                AutoSize = true,
                Location = new Point(20, 20)
            };
            panelQuickExamples.Controls.Add(lblQuickTitle);
            
            // Add Boolean search help label
            Label lblBooleanHelp = new Label
            {
                Text = "💡 Tip: Use Boolean operators for advanced search - Example: 'Java AND Programming NOT JavaScript'",
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                ForeColor = Color.FromArgb(128, 128, 128),
                AutoSize = true,
                Location = new Point(20, 50)
            };
            panelQuickExamples.Controls.Add(lblBooleanHelp);
            
            string[] examples = { "The Great Gatsby", "Orwell", "978-0", "Sarah Johnson", "M1001", "Fiction", "Java AND Programming", "Fiction OR Novel" };
            int xPos = 20;
            int yPos = 80; // Adjusted for Boolean help label
            foreach (string example in examples)
            {
                Button btnExample = new Button
                {
                    Text = example,
                    Font = new Font("Segoe UI", 10F),
                    BackColor = Color.FromArgb(248, 249, 250),
                    ForeColor = Color.FromArgb(64, 64, 64),
                    FlatStyle = FlatStyle.Flat,
                    FlatAppearance = { BorderSize = 0 },
                    AutoSize = true,
                    Padding = new Padding(15, 8, 15, 8),
                    Location = new Point(xPos, yPos),
                    Cursor = Cursors.Hand
                };
                btnExample.FlatAppearance.MouseOverBackColor = Color.FromArgb(230, 230, 230);
                btnExample.Click += (s, e) =>
                {
                    txtSearch.Text = example;
                    txtSearch.ForeColor = Color.Black;
                    PerformSearch();
                };
                panelQuickExamples.Controls.Add(btnExample);
                xPos += btnExample.Width + 10;
                if (xPos + 150 > panelQuickExamples.Width - 40)
                {
                    xPos = 20;
                    yPos += 45;
                }
            }
            panelMainContainer.Controls.Add(panelQuickExamples);
        }
        private void txtSearch_Enter(object sender, EventArgs e)
        {
            string placeholder = "Search by title, author, ISBN, member name, email, or ID...";
            if (txtSearch.Text == placeholder || txtSearch.Text == "Search by title, author, ISBN, member name, email, or ID...")
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
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }
        private void PerformSearch()
        {
            string searchTerm = txtSearch.Text.Trim();
            string placeholder = "Search by title, author, ISBN, member name, email, or ID...";
            if (string.IsNullOrWhiteSpace(searchTerm) || searchTerm == placeholder)
            {
                MessageBox.Show("Please enter a search term.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                var results = _searchService.SearchAll(searchTerm);
                searchResults = ConvertSearchResultsToDataTable(results);
                DisplaySearchResults();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error performing search: {ex.Message}", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            dt.Columns.Add("AccessionNo", typeof(string));
            dt.Columns.Add("MemberID", typeof(string));
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
                row["AccessionNo"] = book.AccessionNo ?? "";
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
                row["AccessionNo"] = DBNull.Value;
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
                panelSearchOutput.Visible = false;
                return;
            }
            
            // Ensure results panels are set up
            if (dataGridViewAllResults == null || !panelSearchOutput.Controls.Contains(panelAllResults))
            {
                SetupResultsPanel();
            }
            
            // Filter data based on type
            DataTable allData = searchResults.Copy();
            DataTable booksData = searchResults.Clone();
            DataTable membersData = searchResults.Clone();
            
            foreach (DataRow row in searchResults.Rows)
            {
                string type = row["Type"]?.ToString();
                if (type == "Book")
                {
                    booksData.ImportRow(row);
                }
                else if (type == "Member")
                {
                    membersData.ImportRow(row);
                }
            }
            
            // Format and display results for each panel
            // Disable auto-generation of columns before setting data source
            dataGridViewAllResults.AutoGenerateColumns = false;
            dataGridViewBooksResults.AutoGenerateColumns = false;
            dataGridViewMembersResults.AutoGenerateColumns = false;
            
            FormatResultsGrid(dataGridViewAllResults, allData, "All");
            FormatResultsGrid(dataGridViewBooksResults, booksData, "Books");
            FormatResultsGrid(dataGridViewMembersResults, membersData, "Members");
            
            // Set data sources
            dataGridViewAllResults.DataSource = allData;
            dataGridViewBooksResults.DataSource = booksData;
            dataGridViewMembersResults.DataSource = membersData;
            
            // Refresh all grids
            RefreshDataGridView(dataGridViewAllResults);
            RefreshDataGridView(dataGridViewBooksResults);
            RefreshDataGridView(dataGridViewMembersResults);
            
            panelSearchOutput.Visible = true;
            panelSearchOutput.BringToFront();
        }
        
        private void RefreshDataGridView(DataGridView dgv)
        {
            dgv.ColumnHeadersVisible = true;
            dgv.ColumnHeadersHeight = 40;
            dgv.PerformLayout();
            if (dgv.Rows.Count > 0)
            {
                dgv.FirstDisplayedScrollingRowIndex = 0;
            }
            dgv.Refresh();
            dgv.Update();
        }
        
        private void FormatResultsGrid(DataGridView dgv, DataTable data, string filterType)
        {
            dgv.Columns.Clear();
            
            var columns = new List<(string Key, string Header, int Width, DataGridViewAutoSizeColumnMode AutoSize)>();
            
            if (filterType == "All")
            {
                // All results: ID, Member, Title/Name, Type, Author/Email, ISBN, Category/Type, Status
                columns.Add(("ID", "ID", 80, DataGridViewAutoSizeColumnMode.None));
                columns.Add(("MemberID", "Member", 100, DataGridViewAutoSizeColumnMode.None));
                columns.Add(("Title", "Title/Name", 0, DataGridViewAutoSizeColumnMode.Fill));
                columns.Add(("Type", "Type", 80, DataGridViewAutoSizeColumnMode.None));
                columns.Add(("Author", "Author/Email", 0, DataGridViewAutoSizeColumnMode.Fill));
                columns.Add(("ISBN", "ISBN", 150, DataGridViewAutoSizeColumnMode.None));
                columns.Add(("Category", "Category/Type", 120, DataGridViewAutoSizeColumnMode.None));
                columns.Add(("Status", "Status", 100, DataGridViewAutoSizeColumnMode.None));
            }
            else if (filterType == "Books")
            {
                // Books only: accession no, book title, type, author, isbn, category, and status
                columns.Add(("AccessionNo", "Accession No", 120, DataGridViewAutoSizeColumnMode.None));
                columns.Add(("Title", "Book Title", 0, DataGridViewAutoSizeColumnMode.Fill));
                columns.Add(("Type", "Type", 80, DataGridViewAutoSizeColumnMode.None));
                columns.Add(("Author", "Author", 0, DataGridViewAutoSizeColumnMode.Fill));
                columns.Add(("ISBN", "ISBN", 150, DataGridViewAutoSizeColumnMode.None));
                columns.Add(("Category", "Category", 120, DataGridViewAutoSizeColumnMode.None));
                columns.Add(("Status", "Status", 100, DataGridViewAutoSizeColumnMode.None));
            }
            else if (filterType == "Members")
            {
                // Members only: member id, type, member name, email, and status
                columns.Add(("MemberID", "Member ID", 100, DataGridViewAutoSizeColumnMode.None));
                columns.Add(("Type", "Type", 80, DataGridViewAutoSizeColumnMode.None));
                columns.Add(("MemberName", "Member Name", 0, DataGridViewAutoSizeColumnMode.Fill));
                columns.Add(("Author", "Email", 0, DataGridViewAutoSizeColumnMode.Fill));
                columns.Add(("Status", "Status", 100, DataGridViewAutoSizeColumnMode.None));
            }
            
            foreach (var col in columns)
            {
                if (data.Columns.Contains(col.Key))
                {
                    dgv.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = col.Key,
                        HeaderText = col.Header,
                        DataPropertyName = col.Key,
                        Width = col.Width,
                        AutoSizeMode = col.AutoSize,
                        MinimumWidth = col.Width > 0 ? col.Width : 100
                    });
                }
            }
            
            dgv.ColumnHeadersVisible = true;
            dgv.ColumnHeadersHeight = 40;
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
                    ListBox listBox = new ListBox
                    {
                        Dock = DockStyle.Fill,
                        Font = new Font("Segoe UI", 10F)
                    };
                    listBox.Items.AddRange(authors.ToArray());
                    authorDialog.Controls.Add(listBox);
                    if (authorDialog.ShowDialog() == DialogResult.OK && listBox.SelectedItem != null)
                    {
                        string selectedAuthor = listBox.SelectedItem.ToString();
                        txtSearch.Text = selectedAuthor;
                        txtSearch.ForeColor = Color.Black;
                        PerformSearch();
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
            try
            {
                // Open dedicated form for new arrivals
                using (var newBooksForm = new NewBooksBrowseForm())
                {
                    newBooksForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading new arrivals: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lblPopularLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                // Open dedicated form for popular books
                using (var popularBooksForm = new PopularBooksBrowseForm())
                {
                    popularBooksForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading popular books: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying results: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadFormInParentPanel(Form formToLoad)
        {
            try
            {
                if (this.Parent is Panel parentPanel)
                {
                    formToLoad.TopLevel = false;
                    formToLoad.FormBorderStyle = FormBorderStyle.None;
                    formToLoad.Dock = DockStyle.Fill;
                    parentPanel.Controls.Clear();
                    parentPanel.Controls.Add(formToLoad);
                    formToLoad.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DrawBookIcon(Graphics g, Panel panel, Color iconColor)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int size = Math.Min(panel.Width, panel.Height) - 20;
            int x = (panel.Width - size) / 2;
            int y = (panel.Height - size) / 2;
            using (Pen pen = new Pen(iconColor, 3))
            {
                g.DrawRectangle(pen, x + size * 0.2f, y + size * 0.1f, size * 0.6f, size * 0.8f);
                g.DrawLine(pen, x + size * 0.2f, y + size * 0.3f, x + size * 0.8f, y + size * 0.3f);
                g.DrawLine(pen, x + size * 0.2f, y + size * 0.5f, x + size * 0.8f, y + size * 0.5f);
            }
        }
        private void DrawCalendarIcon(Graphics g, Panel panel, Color iconColor)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int size = Math.Min(panel.Width, panel.Height) - 20;
            int x = (panel.Width - size) / 2;
            int y = (panel.Height - size) / 2;
            using (Pen pen = new Pen(iconColor, 3))
            {
                g.DrawRectangle(pen, x + size * 0.2f, y + size * 0.1f, size * 0.6f, size * 0.7f);
                g.DrawLine(pen, x + size * 0.2f, y + size * 0.3f, x + size * 0.8f, y + size * 0.3f);
                g.DrawLine(pen, x + size * 0.4f, y + size * 0.1f, x + size * 0.4f, y + size * 0.3f);
                g.DrawLine(pen, x + size * 0.6f, y + size * 0.1f, x + size * 0.6f, y + size * 0.3f);
            }
        }
        private void DrawFilterIcon(Graphics g, Panel panel, Color iconColor)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int size = Math.Min(panel.Width, panel.Height) - 20;
            int x = (panel.Width - size) / 2;
            int y = (panel.Height - size) / 2;
            using (Pen pen = new Pen(iconColor, 3))
            {
                g.DrawLine(pen, x + size * 0.2f, y + size * 0.3f, x + size * 0.8f, y + size * 0.3f);
                g.DrawLine(pen, x + size * 0.3f, y + size * 0.5f, x + size * 0.7f, y + size * 0.5f);
                g.DrawLine(pen, x + size * 0.4f, y + size * 0.7f, x + size * 0.6f, y + size * 0.7f);
            }
        }
        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void panelCards_Paint(object sender, PaintEventArgs e)
        {
        }

        private void panelMainContainer_Paint(object sender, PaintEventArgs e)
        {
        }

        private void panelSearchInput_Paint(object sender, PaintEventArgs e)
        {
        }

        private void cardPopularBooks_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}
