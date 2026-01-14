using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Project5LMS.Services;
using Project5LMS.Models;
using Project5LMS.Interfaces;
using System.Data.Common;
namespace Project5LMS.Forms.Admin.Search
{
    public partial class PopularBooksBrowseForm : Form
    {
        private IBookService _bookService;
        private IBookService BookService
        {
            get
            {
                if (_bookService == null && !this.DesignMode)
                    _bookService = ServiceFactory.CreateBookService();
                return _bookService;
            }
        }
        public PopularBooksBrowseForm()
        {
            InitializeComponent();
        }
        private void PopularBooksBrowseForm_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadPopularBooks();
        }
        private void SetupDataGridView()
        {
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
            DataGridViewTextBoxColumn colStatus = new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "STATUS",
                DataPropertyName = "Status",
                Width = 120,
                ReadOnly = true
            };
            dataGridViewBooks.Columns.Add(colStatus);
            DataGridViewButtonColumn colView = new DataGridViewButtonColumn
            {
                Name = "View",
                HeaderText = "ACTIONS",
                Text = "View Details",
                UseColumnTextForButtonValue = true,
                Width = 150,
                ReadOnly = true
            };
            dataGridViewBooks.Columns.Add(colView);
            dataGridViewBooks.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridViewBooks.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridViewBooks.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dataGridViewBooks.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            dataGridViewBooks.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            dataGridViewBooks.RowTemplate.Height = 50;
            dataGridViewBooks.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dataGridViewBooks.CellContentClick += DataGridViewBooks_CellContentClick;
        }
        private void LoadPopularBooks()
        {
            try
            {
                // Use weighted popularity (recent borrowings count more)
                var popularBooks = BookService.GetPopularBooks(50, weightedByRecency: true);
                if (popularBooks != null && popularBooks.Any())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("BookID", typeof(int));
                    dt.Columns.Add("Title", typeof(string));
                    dt.Columns.Add("Author", typeof(string));
                    dt.Columns.Add("ISBN", typeof(string));
                    dt.Columns.Add("Category", typeof(string));
                    dt.Columns.Add("Status", typeof(string));
                    foreach (var book in popularBooks)
                    {
                        DataRow row = dt.NewRow();
                        row["BookID"] = book.BookID;
                        row["Title"] = book.Title;
                        row["Author"] = book.Author ?? "";
                        row["ISBN"] = book.ISBN ?? "";
                        row["Category"] = book.Category ?? "";
                        row["Status"] = book.Status ?? "Available";
                        dt.Rows.Add(row);
                    }
                    dataGridViewBooks.DataSource = dt;
                }
                else
                {
                    dataGridViewBooks.DataSource = null;
                    MessageBox.Show("No popular books found.", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading popular books: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DataGridViewBooks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dataGridViewBooks.Columns[e.ColumnIndex].Name == "View")
            {
                try
                {
                    // Get BookID from the DataTable row, not the DataGridView cell
                    DataRowView rowView = dataGridViewBooks.Rows[e.RowIndex].DataBoundItem as DataRowView;
                    if (rowView != null)
                    {
                        int bookId = Convert.ToInt32(rowView["BookID"]);
                        var book = BookService.GetBook(bookId);
                        if (book != null)
                        {
                            using (var browseForm = new BrowseResultsForm($"Book Details", new List<Book> { book }))
                            {
                                browseForm.ShowDialog();
                            }
                        }
                    }
                    else
                    {
                        // Fallback: try to get from cell directly
                        if (dataGridViewBooks.Rows[e.RowIndex].Cells["BookID"].Value != null)
                        {
                            int bookId = Convert.ToInt32(dataGridViewBooks.Rows[e.RowIndex].Cells["BookID"].Value);
                            var book = BookService.GetBook(bookId);
                            if (book != null)
                            {
                                using (var browseForm = new BrowseResultsForm($"Book Details", new List<Book> { book }))
                                {
                                    browseForm.ShowDialog();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error viewing book details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
