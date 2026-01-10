using System;
using System.Windows.Forms;
using Project5LMS.Models;
namespace Project5LMS.Forms.Admin.Catalog
{
    public partial class ResourceTypeForm : Form
    {
        protected Book baseBook;
        protected string resourceType;
        public ResourceTypeForm() : this(null, "Book")
        {
        }
        public ResourceTypeForm(Book book = null, string type = "Book")
        {
            baseBook = book;
            resourceType = type;
            InitializeComponent();
            if (!string.IsNullOrEmpty(resourceType))
            {
                this.Text = $"Add {resourceType}";
            }
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