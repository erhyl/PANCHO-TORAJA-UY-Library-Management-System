using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Project5LMS.Helpers;
using Project5LMS.Forms.Admin.Catalog;
using Project5LMS.Forms.Admin.Members;

namespace Project5LMS.Forms.Admin.Search
{
    public partial class AdminSearchForm : Form
    {
        private string connectionString;
        private DataTable searchResults;
        private Panel panelResults;
        private DataGridView dataGridViewResults;

        public AdminSearchForm()
        {
            InitializeComponent();
            try
            {
                connectionString = DatabaseHelper.GetConnectionString();
            }
            catch
            {
                connectionString = "Server=localhost;Database=librarydb;Uid=root;Pwd=;";
            }
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

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT DISTINCT Category FROM Books WHERE Category IS NOT NULL AND Category != '' ORDER BY Category";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cmbCategory.Items.Add(reader.GetString("Category"));
                            }
                        }
                    }
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
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                Margin = new Padding(0, 50, 0, 0)
            };

            panelResults.Controls.Add(lblResultsTitle);
            panelResults.Controls.Add(dataGridViewResults);
            panelMainContainer.Controls.Add(panelResults);
            panelResults.BringToFront();
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "?? Search by title, author, ISBN, member name, or ID...")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "?? Search by title, author, ISBN, member name, or ID...";
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
            if (searchText == "?? Search by title, author, ISBN, member name, or ID..." || string.IsNullOrWhiteSpace(searchText))
            {
                MessageBox.Show("Please enter a search term.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string searchIn = cmbSearchIn.SelectedItem?.ToString() ?? "All";
            string category = cmbCategory.SelectedItem?.ToString();
            if (category == "All Categories")
                category = null;

            System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();

            try
            {
                searchResults = new DataTable();
                string query = BuildSearchQuery(searchIn, category, searchText);

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SearchText", $"%{searchText}%");
                        if (category != null)
                        {
                            cmd.Parameters.AddWithValue("@Category", category);
                        }

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(searchResults);
                        }
                    }
                }

                stopwatch.Stop();
                long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

                if (elapsedMilliseconds > 2000)
                {
                    System.Diagnostics.Debug.WriteLine($"WARNING: Search took {elapsedMilliseconds}ms (>2000ms target)");
                    AuditLogger.Log("Search Performance Warning", 
                        $"Search: '{searchText}', Type: {searchIn}, Time: {elapsedMilliseconds}ms, Results: {searchResults.Rows.Count}", 
                        "Performance Warning");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Search completed in {elapsedMilliseconds}ms ({searchResults.Rows.Count} results)");
                }

                DisplaySearchResults();
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                AuditLogger.Log("Search Error", 
                    $"Search: '{searchText}', Type: {searchIn}, Time: {stopwatch.ElapsedMilliseconds}ms, Error: {ex.Message}", 
                    "Failed");
                MessageBox.Show($"Error performing search: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Search error: {ex.Message}");
            }
        }

        private string BuildSearchQuery(string searchIn, string category, string searchText)
        {
            List<string> queries = new List<string>();

            if (searchIn == "All" || searchIn == "Books")
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
                                    OR b.AccessionNo LIKE @SearchText)";
                if (category != null)
                {
                    bookQuery += " AND b.Category = @Category";
                }
                bookQuery += " LIMIT 100";
                queries.Add(bookQuery);
            }

            if (searchIn == "All" || searchIn == "Members")
            {
                string memberQuery = @"SELECT 
                                      'Member' as Type,
                                      m.MemberID as ID,
                                      CONCAT(m.FirstName, ' ', m.LastName) as Title,
                                      m.Email as Author,
                                      NULL as ISBN,
                                      m.Type as Category,
                                      m.Status as Status,
                                      m.MemberID as MemberID,
                                      CONCAT(m.FirstName, ' ', m.LastName) as MemberName,
                                      m.Type as MemberType
                                      FROM Members m
                                      WHERE (m.FirstName LIKE @SearchText 
                                      OR m.LastName LIKE @SearchText 
                                      OR m.Email LIKE @SearchText 
                                      OR CAST(m.MemberID AS CHAR) LIKE @SearchText)
                                      LIMIT 100";
                queries.Add(memberQuery);
            }

            if (queries.Count == 0)
            {
                return "SELECT 'No Results' as Type, 0 as ID, 'No search criteria specified' as Title, NULL as Author, NULL as ISBN, NULL as Category, NULL as Status, NULL as MemberID, NULL as MemberName, NULL as MemberType WHERE 1=0";
            }

            return string.Join(" UNION ALL ", queries);
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
            LoadFormInParentPanel(new AdminCatalogForm());
        }

        private void lblNewLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoadFormInParentPanel(new AdminCatalogForm());
        }

        private void lblPopularLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoadFormInParentPanel(new AdminCatalogForm());
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
    }
}
