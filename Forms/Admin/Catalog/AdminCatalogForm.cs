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

namespace Project5LMS.Forms.Admin.Catalog
{
    public partial class AdminCatalogForm : Form
    {
        private DataTable allBooksData;
        private readonly IBookService _bookService;
        private readonly DatabaseContext _dbContext;

        public AdminCatalogForm()
        {
            InitializeComponent();
            _bookService = ServiceFactory.CreateBookService();
            _dbContext = ServiceFactory.GetDbContext();
        }

        private void AdminCatalogForm_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadResourceTypes();
            LoadCategories();

            if (cmbResourceTypeFilter.Items.Count > 0)
            {
                cmbResourceTypeFilter.SelectedIndex = 0;
            }
            if (cmbCategoryFilter.Items.Count > 0)
            {
                cmbCategoryFilter.SelectedIndex = 0;
            }
            LoadMetrics();
            LoadBooks();
        }

        private void SetupDataGridView()
        {

            dataGridViewBooks.AutoGenerateColumns = false;

            if (dataGridViewBooks.Columns.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("Warning: DataGridView columns not found. Columns should be defined in Designer.");
                return;
            }

            dataGridViewBooks.CellFormatting -= DataGridViewBooks_CellFormatting;
            dataGridViewBooks.CellFormatting += DataGridViewBooks_CellFormatting;

            dataGridViewBooks.CellPainting -= DataGridViewBooks_CellPainting;
            dataGridViewBooks.CellPainting += DataGridViewBooks_CellPainting;
        }

        private void DataGridViewBooks_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                DataGridViewRow row = dataGridViewBooks.Rows[e.RowIndex];
                string columnName = dataGridViewBooks.Columns[e.ColumnIndex].Name;

            if (columnName == "AccessionNo" && e.Value != null)
            {
                string accessionNo = e.Value.ToString();
                if (!accessionNo.StartsWith("ACC-"))
                {
                    if (int.TryParse(accessionNo, out int accNum))
                    {
                        e.Value = $"ACC-{accNum}";
                    }
                    else
                    {
                        e.Value = $"ACC-{accessionNo}";
                    }
                }
                e.FormattingApplied = true;
            }

            if (columnName == "BookDetails" && e.Value != null)
            {
                string details = e.Value.ToString();
                e.FormattingApplied = true;
            }

            if (columnName == "Publisher" && e.Value != null)
            {
                string publisher = e.Value.ToString();
                e.FormattingApplied = true;
            }

            if (columnName == "Copies" && e.Value != null)
            {
                string copies = e.Value.ToString();
                e.FormattingApplied = true;
            }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"CellFormatting error: {ex}");

            }
        }

        private void DataGridViewBooks_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

                string columnName = dataGridViewBooks.Columns[e.ColumnIndex].Name;

            if (columnName == "Status")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                string value = e.Value?.ToString() ?? "";
                Color bgColor = Color.LightGray;
                Color textColor = Color.Black;

                switch (value.ToLower())
                {
                    case "available":
                        bgColor = Color.FromArgb(40, 167, 69);
                        textColor = Color.White;
                        break;
                    case "limited":
                        bgColor = Color.FromArgb(255, 193, 7);
                        textColor = Color.White;
                        break;
                    case "out of stock":
                    case "outofstock":
                        bgColor = Color.FromArgb(220, 53, 69);
                        textColor = Color.White;
                        break;
                }

                Rectangle badgeRect = new Rectangle(
                    e.CellBounds.X + 5,
                    e.CellBounds.Y + (e.CellBounds.Height - 25) / 2,
                    Math.Min(100, e.CellBounds.Width - 10),
                    25
                );

                using (GraphicsPath path = new GraphicsPath())
                {
                    int radius = 12;
                    path.AddArc(badgeRect.X, badgeRect.Y, radius, radius, 180, 90);
                    path.AddArc(badgeRect.Right - radius, badgeRect.Y, radius, radius, 270, 90);
                    path.AddArc(badgeRect.Right - radius, badgeRect.Bottom - radius, radius, radius, 0, 90);
                    path.AddArc(badgeRect.X, badgeRect.Bottom - radius, radius, radius, 90, 90);
                    path.CloseAllFigures();

                    using (SolidBrush brush = new SolidBrush(bgColor))
                    {
                        e.Graphics.FillPath(brush, path);
                    }
                }

                TextRenderer.DrawText(
                    e.Graphics,
                    value,
                    dataGridViewBooks.DefaultCellStyle.Font,
                    badgeRect,
                    textColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                );

                e.Handled = true;
            }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"CellPainting error: {ex}");

                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                e.Handled = false;
            }
        }

        private void LoadResourceTypes()
        {
            try
            {
                cmbResourceTypeFilter.Items.Clear();
                cmbResourceTypeFilter.Items.Add("All Resource Types");
                cmbResourceTypeFilter.Items.Add("Books");
                cmbResourceTypeFilter.Items.Add("E-Books");
                cmbResourceTypeFilter.Items.Add("Journals");
                cmbResourceTypeFilter.Items.Add("Magazines");
                cmbResourceTypeFilter.Items.Add("Reference Materials");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading resource types: {ex.Message}");
            }
        }

        private void LoadCategories()
        {
            try
            {
                cmbCategoryFilter.Items.Clear();
                cmbCategoryFilter.Items.Add("All Categories");
                
                var categories = _bookService.GetAllBooks()
                    .Where(b => !string.IsNullOrWhiteSpace(b.Category))
                    .Select(b => b.Category)
                    .Distinct()
                    .OrderBy(c => c);
                
                foreach (var category in categories)
                {
                    cmbCategoryFilter.Items.Add(category);
                }
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
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();

                    string queryTotalTitles = "SELECT COUNT(DISTINCT BookID) FROM Books";
                    using (MySqlCommand cmd = new MySqlCommand(queryTotalTitles, conn))
                    {
                        int totalTitles = Convert.ToInt32(cmd.ExecuteScalar());
                        lblMetricTotalTitlesValue.Text = totalTitles.ToString();
                    }

                    string queryTotalCopies = "SELECT SUM(Copies) FROM Books";
                    using (MySqlCommand cmd = new MySqlCommand(queryTotalCopies, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        int totalCopies = result != DBNull.Value && result != null ? Convert.ToInt32(result) : 0;
                        lblMetricTotalCopiesValue.Text = totalCopies.ToString();
                    }

                    string queryAvailable = "SELECT SUM(Available) FROM Books";
                    using (MySqlCommand cmd = new MySqlCommand(queryAvailable, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        int available = result != DBNull.Value && result != null ? Convert.ToInt32(result) : 0;
                        lblMetricAvailableValue.Text = available.ToString();
                    }

                    string queryOnLoan = "SELECT SUM(Copies - Available) FROM Books";
                    using (MySqlCommand cmd = new MySqlCommand(queryOnLoan, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        int onLoan = result != DBNull.Value && result != null ? Convert.ToInt32(result) : 0;
                        lblMetricOnLoanValue.Text = onLoan.ToString();
                    }
                }
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

                if (!allBooksData.Columns.Contains("AccessionNo"))
                {
                    allBooksData.Columns.Add("AccessionNo", typeof(string));
                }
                if (!allBooksData.Columns.Contains("BookDetails"))
                {
                    allBooksData.Columns.Add("BookDetails", typeof(string));
                }
                if (!allBooksData.Columns.Contains("Publisher"))
                {
                    allBooksData.Columns.Add("Publisher", typeof(string));
                }
                if (!allBooksData.Columns.Contains("Copies"))
                {
                    allBooksData.Columns.Add("Copies", typeof(string));
                }
                if (!allBooksData.Columns.Contains("Location"))
                {
                    allBooksData.Columns.Add("Location", typeof(string));
                }
                if (!allBooksData.Columns.Contains("Status"))
                {
                    allBooksData.Columns.Add("Status", typeof(string));
                }

                foreach (DataRow row in allBooksData.Rows)
                {

                    int bookId = Convert.ToInt32(row["BookID"]);
                    string barcode = row["Barcode"] != DBNull.Value ? row["Barcode"].ToString() : "";
                    row["AccessionNo"] = !string.IsNullOrEmpty(barcode) ? barcode : bookId.ToString();

                    string title = row["Title"] != DBNull.Value ? row["Title"].ToString() : "";
                    string author = row["Author"] != DBNull.Value ? row["Author"].ToString() : "";
                    string isbn = row["ISBN"] != DBNull.Value ? row["ISBN"].ToString() : "";
                    row["BookDetails"] = $"{title}\n{author}\nISBN: {isbn}";

                    string publisher = row["Publisher"] != DBNull.Value ? row["Publisher"].ToString() : "";
                    string year = row["YearPublished"] != DBNull.Value ? row["YearPublished"].ToString() : "";
                    row["Publisher"] = !string.IsNullOrEmpty(year) ? $"{publisher}, {year}" : publisher;

                    int available = row["Available"] != DBNull.Value ? Convert.ToInt32(row["Available"]) : 0;
                    int totalCopies = row["Copies"] != DBNull.Value ? Convert.ToInt32(row["Copies"]) : 0;
                    row["Copies"] = $"{available}/{totalCopies} available";

                    if (allBooksData.Columns.Contains("Location") && row["Location"] != DBNull.Value)
                    {
                        row["Location"] = row["Location"].ToString();
                    }
                    else
                    {

                        string category = row["Category"] != DBNull.Value ? row["Category"].ToString() : "";
                        row["Location"] = GenerateLocation(category, bookId);
                    }

                    if (available == 0)
                    {
                        row["Status"] = "Out of Stock";
                    }
                    else if (available < totalCopies * 0.3)
                    {
                        row["Status"] = "Limited";
                    }
                    else
                    {
                        row["Status"] = "Available";
                    }
                }

                ApplyFilters();
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
            return DataTableHelper.BooksToDataTable(books);
        }

        private string GenerateLocation(string category, int bookId)
        {

            if (string.IsNullOrEmpty(category))
                return $"A-{bookId % 100}-{bookId % 10}";

            char section = 'A';
            if (category.ToLower().Contains("technology") || category.ToLower().Contains("tech"))
                section = 'C';
            else if (category.ToLower().Contains("fiction"))
                section = 'A';
            else
                section = 'B';

            return $"{section}-{(bookId % 100).ToString().PadLeft(2, '0')}-{bookId % 10}";
        }

        private void ApplyFilters()
        {
            if (allBooksData == null) return;

            string searchText = txtSearch.Text.ToLower();
            if (searchText == "search by title, author, isbn, or accession number...")
                searchText = "";

            string selectedCategory = cmbCategoryFilter.SelectedItem?.ToString();
            if (selectedCategory == "All Categories")
                selectedCategory = null;

            string selectedResourceType = cmbResourceTypeFilter?.SelectedItem?.ToString();
            if (selectedResourceType == "All Resource Types")
                selectedResourceType = null;

            DataTable filteredData = allBooksData.Clone();

            foreach (DataRow row in allBooksData.Rows)
            {
                bool matchesSearch = string.IsNullOrEmpty(searchText) ||
                    row["Title"].ToString().ToLower().Contains(searchText) ||
                    row["Author"].ToString().ToLower().Contains(searchText) ||
                    row["ISBN"].ToString().ToLower().Contains(searchText) ||
                    row["AccessionNo"].ToString().ToLower().Contains(searchText);

                bool matchesCategory = selectedCategory == null || row["Category"].ToString() == selectedCategory;

                bool matchesResourceType = selectedResourceType == null || selectedResourceType == "Books";

                if (matchesSearch && matchesCategory && matchesResourceType)
                {
                    filteredData.ImportRow(row);
                }
            }

            dataGridViewBooks.DataSource = filteredData;
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search by title, author, ISBN, or accession number...")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Search by title, author, ISBN, or accession number...";
                txtSearch.ForeColor = Color.Gray;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ApplyFilters();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Search text changed error: {ex}");

            }
        }

        private void cmbResourceTypeFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ApplyFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while filtering: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Filter error: {ex}");
            }
        }

        private void cmbCategoryFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ApplyFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while filtering: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Filter error: {ex}");
            }
        }

        private void dataGridViewBooks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

                DataGridViewRow row = dataGridViewBooks.Rows[e.RowIndex];
                string columnName = dataGridViewBooks.Columns[e.ColumnIndex].Name;

                int bookId = 0;
                if (row.DataBoundItem != null)
                {
                    DataRowView rowView = row.DataBoundItem as DataRowView;
                    if (rowView != null && rowView.Row.Table.Columns.Contains("BookID"))
                    {
                        object bookIdObj = rowView["BookID"];
                        if (bookIdObj != null && bookIdObj != DBNull.Value)
                        {
                            bookId = Convert.ToInt32(bookIdObj);
                        }
                    }
                }

                if (bookId == 0)
                {
                    MessageBox.Show("Unable to identify book.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (columnName == "Edit")
                {
                    EditBook(bookId);
                }
                else if (columnName == "View")
                {
                    ViewBook(bookId);
                }
                else if (columnName == "Delete")
                {
                    DeleteBook(bookId, row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while processing your request: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"CellContentClick error: {ex}");
            }
        }

        private void EditBook(int bookId)
        {
            try
            {

                MessageBox.Show($"Edit book with ID: {bookId}\n\nEditBookForm will be opened here.", "Edit Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening edit form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ViewBook(int bookId)
        {
            try
            {

                MessageBox.Show($"View book details for ID: {bookId}", "View Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error viewing book: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteBook(int bookId, DataGridViewRow row)
        {
            try
            {
                string bookTitle = "Unknown";
                if (row.Cells["BookDetails"]?.Value != null)
                {
                    string bookDetails = row.Cells["BookDetails"].Value.ToString();
                    if (!string.IsNullOrEmpty(bookDetails))
                    {
                        bookTitle = bookDetails.Split('\n')[0];
                    }
                }
            var result = MessageBox.Show(
                $"Are you sure you want to delete book \"{bookTitle}\"?\n\nThis action cannot be undone.",
                "Delete Book",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (var conn = _dbContext.GetConnection())
                    {
                        conn.Open();
                        string query = "DELETE FROM Books WHERE BookID = @bookId";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@bookId", bookId);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Book deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadBooks();
                    LoadMetrics();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting book: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"DeleteBook error: {ex}");
            }
        }

        private void btnAddNewBook_Click(object sender, EventArgs e)
        {
            try
            {
                using (AdminCatalogNewBookForm addBookForm = new AdminCatalogNewBookForm())
                {
                    if (addBookForm.ShowDialog() == DialogResult.OK)
                    {

                        LoadBooks();
                        LoadMetrics();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening add book form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImportCSV_Click(object sender, EventArgs e)
        {
            try
            {

                MessageBox.Show("Import CSV functionality will be implemented here.\n\nImportBooksForm will be opened.", "Import CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening import form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
