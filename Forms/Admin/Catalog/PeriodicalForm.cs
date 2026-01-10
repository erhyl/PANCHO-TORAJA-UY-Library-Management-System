using System;
using System.Windows.Forms;
using Project5LMS.Models;
using Project5LMS.Data;
using Project5LMS.Services;
namespace Project5LMS.Forms.Admin.Catalog
{
    public partial class PeriodicalForm : ResourceTypeForm
    {
        private DatabaseContext _dbContext;
        public PeriodicalForm(Book book = null) : base(book, "Periodical")
        {
            _dbContext = ServiceFactory.GetDbContext();
            InitializeComponent();
            if (cmbFrequency.Items.Count > 0) cmbFrequency.SelectedIndex = 0;
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
            if (string.IsNullOrWhiteSpace(txtISSN.Text))
            {
                MessageBox.Show("ISSN is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        public Periodical GetPeriodical(Book baseBook)
        {
            return new Periodical
            {
                BookID = baseBook.BookID,
                Title = baseBook.Title,
                Author = baseBook.Author,
                ISBN = baseBook.ISBN,
                Publisher = baseBook.Publisher,
                PublicationYear = baseBook.PublicationYear,
                Category = baseBook.Category,
                Language = baseBook.Language,
                ISSN = txtISSN.Text.Trim(),
                VolumeNumber = txtVolume.Text.Trim(),
                IssueNumber = txtIssue.Text.Trim(),
                PublicationDate = dtpPublicationDate.Value,
                Frequency = cmbFrequency.SelectedItem?.ToString() ?? "Monthly"
            };
        }
    }
}