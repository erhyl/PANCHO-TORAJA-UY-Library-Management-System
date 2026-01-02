using System;
using System.Drawing;
using System.Windows.Forms;

namespace Project5LMS.Admin_Dashboard
{
    public partial class CatalogSearchForm : Form
    {
        public CatalogSearchForm()
        {
            InitializeComponent();
        }

        private void CatalogSearchForm_Load(object sender, EventArgs e)
        {
            cmbSearchField.SelectedIndex = 0;
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search for books, authors, ISBN...")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Search for books, authors, ISBN...";
                txtSearch.ForeColor = Color.Gray;
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformSearch();
                e.SuppressKeyPress = true;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void PerformSearch()
        {
            string searchText = txtSearch.Text.Trim();
            if (searchText == "Search for books, authors, ISBN..." || string.IsNullOrWhiteSpace(searchText))
            {
                MessageBox.Show("Please enter a search term.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string searchField = cmbSearchField.SelectedItem?.ToString() ?? "All Fields";
            
            try
            {
                // Navigate to CatalogForm - it will handle the search
                Form parentForm = this.ParentForm ?? this.MdiParent;
                Control parent = this.Parent;
                
                // Find the main form (AdminMainForm)
                while (parent != null && !(parent is Form))
                {
                    parent = parent.Parent;
                }
                
                if (parent is Form mainForm)
                {
                    // Create CatalogForm and load it
                    CatalogForm catalogForm = new CatalogForm();
                    
                    // Try to call LoadFormInPanel if it exists
                    var method = mainForm.GetType().GetMethod("LoadFormInPanel");
                    if (method != null)
                    {
                        method.Invoke(mainForm, new object[] { catalogForm });
                        
                        // Set search text in CatalogForm if possible
                        var searchTextBox = catalogForm.Controls.Find("txtSearch", true);
                        if (searchTextBox.Length > 0 && searchTextBox[0] is TextBox txt)
                        {
                            txt.Text = searchText;
                            txt.ForeColor = Color.Black;
                        }
                    }
                }
                else
                {
                    // Fallback: show CatalogForm as a new window
                    CatalogForm catalogForm = new CatalogForm();
                    catalogForm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error performing search: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
