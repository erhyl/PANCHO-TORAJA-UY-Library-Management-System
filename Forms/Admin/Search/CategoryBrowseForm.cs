using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Project5LMS.Helpers;
using Project5LMS.Services;
using Project5LMS.Data;
using Project5LMS.Models;
using Project5LMS.Interfaces;
namespace Project5LMS.Forms.Admin.Search
{
    public partial class CategoryBrowseForm : Form
    {
        private IBookService _bookService;
        private DatabaseContext _dbContext;
        private IBookService BookService
        {
            get
            {
                if (_bookService == null && !this.DesignMode)
                    _bookService = ServiceFactory.CreateBookService();
                return _bookService;
            }
        }
        private DatabaseContext DbContext
        {
            get
            {
                if (_dbContext == null && !this.DesignMode)
                    _dbContext = ServiceFactory.GetDbContext();
                return _dbContext;
            }
        }
        public CategoryBrowseForm()
        {
            InitializeComponent();
            if (IsDesignMode())
            {
                try
                {
                    this.designTimeData.Columns.Add("Category", typeof(string));
                    this.designTimeData.Columns.Add("BookCount", typeof(int));
                    this.designTimeData.Columns.Add("TotalCopies", typeof(int));
                    this.designTimeData.Columns.Add("Available", typeof(int));
                    this.designTimeData.Rows.Add("Fiction", 25, 50, 30);
                    this.designTimeData.Rows.Add("Science", 15, 30, 20);
                    this.designTimeData.Rows.Add("History", 10, 20, 15);
                    this.designTimeData.Rows.Add("Technology", 20, 40, 25);
                    this.designTimeData.Rows.Add("Literature", 18, 35, 22);
                    this.dataGridViewCategories.DataSource = this.designTimeData;
                }
                catch { }
            }
        }
        private bool IsDesignMode()
        {
            try
            {
                if (this.DesignMode) return true;
                if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return true;
                if (this.Site != null && this.Site.DesignMode) return true;
                string processName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                if (processName == "devenv" || processName == "XDesProc" || processName == "WDExpress" || processName == "VSWinExpress")
                    return true;
            }
            catch { }
            return false;
        }
        private void CategoryBrowseForm_Load(object sender, EventArgs e)
        {
            if (IsDesignMode())
            {
                SetupDataGridView();
                return;
            }
            SetupDataGridView();
            LoadCategories();
        }
        private void SetupDataGridView()
        {
            if (IsDesignMode())
            {
                try
                {
                    dataGridViewCategories.Columns.Clear();
                    dataGridViewCategories.AutoGenerateColumns = false;
                    dataGridViewCategories.Columns.Add(new DataGridViewTextBoxColumn { Name = "Category", HeaderText = "CATEGORY", Width = 300, ReadOnly = true });
                    dataGridViewCategories.Columns.Add(new DataGridViewTextBoxColumn { Name = "BookCount", HeaderText = "BOOK COUNT", Width = 150, ReadOnly = true });
                    dataGridViewCategories.Columns.Add(new DataGridViewTextBoxColumn { Name = "TotalCopies", HeaderText = "TOTAL COPIES", Width = 150, ReadOnly = true });
                    dataGridViewCategories.Columns.Add(new DataGridViewTextBoxColumn { Name = "Available", HeaderText = "AVAILABLE", Width = 150, ReadOnly = true });
                    dataGridViewCategories.Columns.Add(new DataGridViewButtonColumn { Name = "View", HeaderText = "ACTIONS", Text = "View Books", UseColumnTextForButtonValue = true, Width = 150, ReadOnly = true });
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Category", typeof(string));
                    dt.Columns.Add("BookCount", typeof(int));
                    dt.Columns.Add("TotalCopies", typeof(int));
                    dt.Columns.Add("Available", typeof(int));
                    dt.Columns.Add("View", typeof(string));
                    dt.Rows.Add("Fiction", 25, 50, 30, "View Books");
                    dt.Rows.Add("Science", 15, 30, 20, "View Books");
                    dt.Rows.Add("History", 10, 20, 15, "View Books");
                    dt.Rows.Add("Technology", 20, 40, 25, "View Books");
                    dt.Rows.Add("Literature", 18, 35, 22, "View Books");
                    dataGridViewCategories.DataSource = dt;
                }
                catch { }
                return;
            }
            dataGridViewCategories.Columns.Clear();
            dataGridViewCategories.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn colCategory = new DataGridViewTextBoxColumn
            {
                Name = "Category",
                HeaderText = "CATEGORY",
                DataPropertyName = "Category",
                Width = 300,
                ReadOnly = true
            };
            dataGridViewCategories.Columns.Add(colCategory);
            DataGridViewTextBoxColumn colBookCount = new DataGridViewTextBoxColumn
            {
                Name = "BookCount",
                HeaderText = "BOOK COUNT",
                DataPropertyName = "BookCount",
                Width = 150,
                ReadOnly = true
            };
            dataGridViewCategories.Columns.Add(colBookCount);
            DataGridViewTextBoxColumn colTotalCopies = new DataGridViewTextBoxColumn
            {
                Name = "TotalCopies",
                HeaderText = "TOTAL COPIES",
                DataPropertyName = "TotalCopies",
                Width = 150,
                ReadOnly = true
            };
            dataGridViewCategories.Columns.Add(colTotalCopies);
            DataGridViewTextBoxColumn colAvailable = new DataGridViewTextBoxColumn
            {
                Name = "Available",
                HeaderText = "AVAILABLE",
                DataPropertyName = "Available",
                Width = 150,
                ReadOnly = true
            };
            dataGridViewCategories.Columns.Add(colAvailable);
            DataGridViewButtonColumn colView = new DataGridViewButtonColumn
            {
                Name = "View",
                HeaderText = "ACTIONS",
                Text = "View Books",
                UseColumnTextForButtonValue = true,
                Width = 150,
                ReadOnly = true
            };
            dataGridViewCategories.Columns.Add(colView);
            dataGridViewCategories.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridViewCategories.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridViewCategories.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dataGridViewCategories.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            dataGridViewCategories.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            dataGridViewCategories.RowTemplate.Height = 50;
            dataGridViewCategories.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dataGridViewCategories.CellContentClick += DataGridViewCategories_CellContentClick;
        }
        private void LoadCategories()
        {
            if (IsDesignMode()) return;
            try
            {
                var dbContext = DbContext;
                using (var conn = dbContext.GetConnection())
                {
                    conn.Open();
                    bool hasCopies = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "Copies");
                    bool hasAvailable = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "Available");
                    string copiesColumn = hasCopies ? "COALESCE(SUM(b.Copies), 0)" : "COUNT(DISTINCT b.BookID)";
                    string availableColumn = hasAvailable ? "COALESCE(SUM(b.Available), 0)" : "0";
                    string query = $@"SELECT 
                                        b.Category,
                                        COUNT(DISTINCT b.BookID) as BookCount,
                                        {copiesColumn} as TotalCopies,
                                        {availableColumn} as Available
                                     FROM Books b
                                     WHERE b.Category IS NOT NULL AND b.Category != ''
                                     GROUP BY b.Category
                                     ORDER BY b.Category";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridViewCategories.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DataGridViewCategories_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dataGridViewCategories.Columns[e.ColumnIndex].Name == "View")
            {
                string category = dataGridViewCategories.Rows[e.RowIndex].Cells["Category"].Value?.ToString();
                if (!string.IsNullOrEmpty(category))
                {
                    var books = BookService.GetBooksByCategory(category);
                    if (books != null && books.Any())
                    {
                        using (var browseForm = new BrowseResultsForm($"Books in Category: {category}", books))
                        {
                            browseForm.ShowDialog();
                        }
                    }
                    else
                    {
                        MessageBox.Show($"No books found in category '{category}'.", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

    }
}
