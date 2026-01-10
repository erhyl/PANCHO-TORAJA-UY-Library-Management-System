using System;
using System.Windows.Forms;
using Project5LMS.Models;

namespace Project5LMS.Forms.Admin.Catalog
{
    /// <summary>
    /// Base form for specialized resource types (Periodical, Thesis, AudioVisual, EBook)
    /// </summary>
    public partial class ResourceTypeForm : Form
    {
        protected Book baseBook;
        protected string resourceType;

        // Parameterless constructor for designer support
        public ResourceTypeForm() : this(null, "Book")
        {
        }

        public ResourceTypeForm(Book book = null, string type = "Book")
        {
            baseBook = book;
            resourceType = type;
            InitializeComponent();
            // Update form properties after InitializeComponent
            if (!string.IsNullOrEmpty(resourceType))
            {
                this.Text = $"Add {resourceType}";
            }
            // Wire up Save button click event
            this.btnSave.Click += BtnSave_Click;
        }

        protected virtual void BtnSave_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        public virtual Book GetBook()
        {
            return baseBook;
        }

        protected virtual bool ValidateInputs()
        {
            return true;
        }
    }
}

