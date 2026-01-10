using System;
using System.Windows.Forms;
using Project5LMS.Models;
using Project5LMS.Data;
using Project5LMS.Services;

namespace Project5LMS.Forms.Admin.Catalog
{
    /// <summary>
    /// Form for adding Thesis/Dissertation resources.
    /// </summary>
    public partial class ThesisForm : ResourceTypeForm
    {
        private DatabaseContext _dbContext;

        public ThesisForm(Book book = null) : base(book, "Thesis")
        {
            _dbContext = ServiceFactory.GetDbContext();
            InitializeComponent();
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
            if (string.IsNullOrWhiteSpace(txtStudentName.Text))
            {
                MessageBox.Show("Student Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtDegree.Text))
            {
                MessageBox.Show("Degree is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        public Thesis GetThesis(Book baseBook)
        {
            return new Thesis
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
                // Thesis-specific properties
                StudentName = txtStudentName.Text.Trim(),
                StudentID = txtStudentID.Text.Trim(),
                Degree = txtDegree.Text.Trim(),
                Department = txtDepartment.Text.Trim(),
                Advisor = txtAdvisor.Text.Trim(),
                DefenseDate = dtpDefenseDate.Value,
                Abstract = txtAbstract.Text.Trim(),
                IsRestricted = chkIsRestricted.Checked
            };
        }
    }
}

