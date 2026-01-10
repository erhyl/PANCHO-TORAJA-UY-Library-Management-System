using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Project5LMS.Models;
using Project5LMS.Helpers;
namespace Project5LMS.Forms.Admin.Search
{
    public partial class BrowseResultsForm : Form
    {
        private readonly string _title;
        private readonly IEnumerable<Book> _books;
        public BrowseResultsForm(string title, IEnumerable<Book> books)
        {
            _title = title;
            _books = books ?? new List<Book>();
            InitializeComponent();
        }
        public BrowseResultsForm()
        {
            _title = "Browse Results";
            _books = new List<Book>();
            InitializeComponent();
            if (IsDesignMode())
            {
                lblTitle.Text = _title;
                try
                {
                    this.designTimeData.Columns.Add("Title", typeof(string));
                    this.designTimeData.Columns.Add("Author", typeof(string));
                    this.designTimeData.Columns.Add("ISBN", typeof(string));
                    this.designTimeData.Columns.Add("Category", typeof(string));
                    this.designTimeData.Columns.Add("Copies", typeof(int));
                    this.designTimeData.Columns.Add("Available", typeof(int));
                    this.designTimeData.Rows.Add("Introduction to Algorithms", "Thomas H. Cormen", "978-0262033848", "Computer Science", 5, 3);
                    this.designTimeData.Rows.Add("The Great Gatsby", "F. Scott Fitzgerald", "978-0743273565", "Literature", 8, 6);
                    this.designTimeData.Rows.Add("A Brief History of Time", "Stephen Hawking", "978-0553380163", "Science", 4, 2);
                    this.designTimeData.Rows.Add("To Kill a Mockingbird", "Harper Lee", "978-0061120084", "Literature", 6, 4);
                    this.designTimeData.Rows.Add("Clean Code", "Robert C. Martin", "978-0132350884", "Technology", 3, 1);
                    this.dataGridViewBooks.DataSource = this.designTimeData;
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
        private void BrowseResultsForm_Load(object sender, EventArgs e)
        {
            if (IsDesignMode())
            {
                lblTitle.Text = _title;
                SetupDataGridView();
                return;
            }
            lblTitle.Text = _title;
            this.Text = _title;
            SetupDataGridView();
            LoadBooks();
        }
        private void SetupDataGridView()
        {
            if (IsDesignMode())
            {
                try
                {
                    dataGridViewBooks.Columns.Clear();
                    dataGridViewBooks.AutoGenerateColumns = false;
                    dataGridViewBooks.Columns.Add(new DataGridViewTextBoxColumn { Name = "Title", HeaderText = "TITLE", Width = 300, ReadOnly = true });
                    dataGridViewBooks.Columns.Add(new DataGridViewTextBoxColumn { Name = "Author", HeaderText = "AUTHOR", Width = 200, ReadOnly = true });
                    dataGridViewBooks.Columns.Add(new DataGridViewTextBoxColumn { Name = "ISBN", HeaderText = "ISBN", Width = 150, ReadOnly = true });
                    dataGridViewBooks.Columns.Add(new DataGridViewTextBoxColumn { Name = "Category", HeaderText = "CATEGORY", Width = 150, ReadOnly = true });
                    dataGridViewBooks.Columns.Add(new DataGridViewTextBoxColumn { Name = "Copies", HeaderText = "COPIES", Width = 100, ReadOnly = true });
                    dataGridViewBooks.Columns.Add(new DataGridViewTextBoxColumn { Name = "Available", HeaderText = "AVAILABLE", Width = 100, ReadOnly = true });
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Title", typeof(string));
                    dt.Columns.Add("Author", typeof(string));
                    dt.Columns.Add("ISBN", typeof(string));
                    dt.Columns.Add("Category", typeof(string));
                    dt.Columns.Add("Copies", typeof(int));
                    dt.Columns.Add("Available", typeof(int));
                    dt.Rows.Add("Introduction to Algorithms", "Thomas H. Cormen", "978-0262033848", "Computer Science", 5, 3);
                    dt.Rows.Add("The Great Gatsby", "F. Scott Fitzgerald", "978-0743273565", "Literature", 8, 6);
                    dt.Rows.Add("A Brief History of Time", "Stephen Hawking", "978-0553380163", "Science", 4, 2);
                    dt.Rows.Add("To Kill a Mockingbird", "Harper Lee", "978-0061120084", "Literature", 6, 4);
                    dt.Rows.Add("Clean Code", "Robert C. Martin", "978-0132350884", "Technology", 3, 1);
                    dataGridViewBooks.DataSource = dt;
                    dataGridViewBooks.DefaultCellStyle.Font = new Font("Segoe UI", 10);
                    dataGridViewBooks.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    dataGridViewBooks.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
                    dataGridViewBooks.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
                    dataGridViewBooks.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
                    dataGridViewBooks.RowTemplate.Height = 50;
                    dataGridViewBooks.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
                }
                catch { }
                return;
            }
            dataGridViewBooks.Columns.Clear();
            dataGridViewBooks.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn colTitle = new DataGridViewTextBoxColumn
            {
                Name = "Title",
                HeaderText = "TITLE",
                DataPropertyName = "Title",
                Width = 300,
                ReadOnly = true
            };
            dataGridViewBooks.Columns.Add(colTitle);
            DataGridViewTextBoxColumn colAuthor = new DataGridViewTextBoxColumn
            {
                Name = "Author",
                HeaderText = "AUTHOR",
                DataPropertyName = "Author",
                Width = 200,
                ReadOnly = true
            };
            dataGridViewBooks.Columns.Add(colAuthor);
            DataGridViewTextBoxColumn colISBN = new DataGridViewTextBoxColumn
            {
                Name = "ISBN",
                HeaderText = "ISBN",
                DataPropertyName = "ISBN",
                Width = 150,
                ReadOnly = true
            };
            dataGridViewBooks.Columns.Add(colISBN);
            DataGridViewTextBoxColumn colCategory = new DataGridViewTextBoxColumn
            {
                Name = "Category",
                HeaderText = "CATEGORY",
                DataPropertyName = "Category",
                Width = 150,
                ReadOnly = true
            };
            dataGridViewBooks.Columns.Add(colCategory);
            DataGridViewTextBoxColumn colCopies = new DataGridViewTextBoxColumn
            {
                Name = "Copies",
                HeaderText = "COPIES",
                DataPropertyName = "Copies",
                Width = 100,
                ReadOnly = true
            };
            dataGridViewBooks.Columns.Add(colCopies);
            DataGridViewTextBoxColumn colAvailable = new DataGridViewTextBoxColumn
            {
                Name = "Available",
                HeaderText = "AVAILABLE",
                DataPropertyName = "Available",
                Width = 100,
                ReadOnly = true
            };
            dataGridViewBooks.Columns.Add(colAvailable);
            dataGridViewBooks.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridViewBooks.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridViewBooks.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dataGridViewBooks.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            dataGridViewBooks.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            dataGridViewBooks.RowTemplate.Height = 50;
            dataGridViewBooks.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
        }
        private void LoadBooks()
        {
            try
            {
                DataTable dt = DataTableHelper.BooksToDataTable(_books);
                dataGridViewBooks.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading books: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
