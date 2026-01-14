using System;
using System.Windows.Forms;
using Project5LMS.Models;
using Project5LMS.Data;
using Project5LMS.Services;
namespace Project5LMS.Forms.Admin.Catalog
{
    public partial class AudioVisualForm : ResourceTypeForm
    {
        private DatabaseContext _dbContext;
        public AudioVisualForm(Book book = null) : base(book, "AudioVisual")
        {
            _dbContext = ServiceFactory.GetDbContext();
            InitializeComponent();
            if (cmbMediaType.Items.Count > 0) cmbMediaType.SelectedIndex = 0;
            if (cmbFormat.Items.Count > 0) cmbFormat.SelectedIndex = 0;
            if (cmbAudioLanguage.Items.Count > 0) cmbAudioLanguage.SelectedIndex = 0;
            if (cmbRating.Items.Count > 0) cmbRating.SelectedIndex = 0;
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
            if (string.IsNullOrWhiteSpace(txtDuration.Text) || !int.TryParse(txtDuration.Text, out int duration))
            {
                MessageBox.Show("Please enter a valid duration in minutes.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        public AudioVisual GetAudioVisual(Book baseBook)
        {
            return new AudioVisual
            {
                BookID = baseBook.BookID,
                Title = baseBook.Title,
                Author = baseBook.Author,
                ISBN = baseBook.ISBN,
                Publisher = baseBook.Publisher,
                PublicationYear = baseBook.PublicationYear,
                Category = baseBook.Category,
                Language = baseBook.Language,
                MediaType = cmbMediaType.SelectedItem?.ToString() ?? "DVD",
                DurationMinutes = int.TryParse(txtDuration.Text, out int dur) ? dur : 0,
                Format = cmbFormat.SelectedItem?.ToString() ?? "NTSC",
                AudioLanguage = cmbAudioLanguage.SelectedItem?.ToString() ?? "English",
                HasSubtitles = chkHasSubtitles.Checked,
                Rating = cmbRating.SelectedItem?.ToString() ?? "NR"
            };
        }
    }
}