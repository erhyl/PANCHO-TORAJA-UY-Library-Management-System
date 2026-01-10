using System;
using System.Windows.Forms;
using Project5LMS.Models;
using Project5LMS.Data;
using Project5LMS.Services;

namespace Project5LMS.Forms.Admin.Catalog
{
    /// <summary>
    /// Form for adding E-Book resources.
    /// </summary>
    public partial class EBookForm : ResourceTypeForm
    {
        private DatabaseContext _dbContext;

        public EBookForm(Book book = null) : base(book, "EBook")
        {
            _dbContext = ServiceFactory.GetDbContext();
            InitializeComponent();
            // Set default format selection
            if (cmbFormat.Items.Count > 0) cmbFormat.SelectedIndex = 0;
        }

        protected override void BtnSave_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        protected override bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtDownloadLink.Text))
            {
                MessageBox.Show("Download link is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        public EBook GetEBook(Book baseBook)
        {
            decimal fileSizeMB = decimal.TryParse(txtFileSize.Text, out decimal size) ? size : 0;
            long fileSizeBytes = (long)(fileSizeMB * 1024 * 1024); // Convert MB to bytes

            return new EBook
            {
                // Copy base book properties
                BookID = baseBook.BookID,
                Title = baseBook.Title,
                Author = baseBook.Author,
                ISBN = baseBook.ISBN,
                Publisher = baseBook.Publisher,
                PublicationYear = baseBook.PublicationYear,
                Category = baseBook.Category,
                Language = baseBook.Language,
                // EBook-specific properties
                FileSizeBytes = fileSizeBytes,
                FileFormat = cmbFormat.SelectedItem?.ToString() ?? "PDF",
                DownloadLink = txtDownloadLink.Text.Trim()
            };
        }
    }
}

