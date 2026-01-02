using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Project5LMS.Forms.Catalog;


namespace Project5LMS.Admin_Dashboard
{
    public partial class CatalogForm : Form
    {
        string connectionString = "Server=localhost;Database=librarydb;Uid=root;Pwd=;";

        public CatalogForm()
        {
            InitializeComponent();
        }

        private void Catalog_Load(object sender, EventArgs e)
        {
            LoadCatalog();
        }

        private void LoadCatalog()
        {
            try
            {
                string query = @"SELECT BookID, AccessionNumber, Title, Author, ISBN, Publisher, YearPublished, 
                                Category, Copies, Available, Barcode, CoverImagePath
                         FROM Books
                         ORDER BY BookID";

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dta_Grid1.DataSource = dt;

                    // Optional: adjust headers
                    dta_Grid1.Columns["BookID"].Visible = false; // hide internal ID
                    dta_Grid1.Columns["AccessionNumber"].HeaderText = "Accession Number";
                    dta_Grid1.Columns["Title"].HeaderText = "Title";
                    dta_Grid1.Columns["Author"].HeaderText = "Author";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading catalog: " + ex.Message);
            }
        }

        private void LoadBooks()
        {
            try
            {
                string query = @"SELECT BookID, AccessionNumber, Title, Author, ISBN, Publisher, YearPublished, 
                                Category, Copies, Available, Barcode, CoverImagePath
                         FROM Books
                         ORDER BY BookID";

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dta_Grid1.DataSource = dt;

                    // Optional: adjust headers
                    dta_Grid1.Columns["BookID"].Visible = false;
                    dta_Grid1.Columns["AccessionNumber"].HeaderText = "Accession Number";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading books: " + ex.Message);
            }
        }

        private void ApplyGridStyle()
        {
            dta_Grid1.ReadOnly = true;
            dta_Grid1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dta_Grid1.MultiSelect = false;
            dta_Grid1.RowHeadersVisible = false;

            dta_Grid1.AllowUserToAddRows = false;
            dta_Grid1.AllowUserToDeleteRows = false;
            dta_Grid1.AllowUserToResizeRows = false;

            // Row height
            dta_Grid1.RowTemplate.Height = 35;

            // Header style
            dta_Grid1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dta_Grid1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dta_Grid1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dta_Grid1.EnableHeadersVisualStyles = false;

            // Cell style
            dta_Grid1.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dta_Grid1.DefaultCellStyle.Padding = new Padding(5, 0, 5, 0);

            // Alternate row color
            dta_Grid1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);

            // Auto-size
            dta_Grid1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Hide cover image path column
            if (dta_Grid1.Columns["CoverImagePath"] != null)
                dta_Grid1.Columns["CoverImagePath"].Visible = false;
        }





        private void btn_addBook_Click(object sender, EventArgs e)
        {
            AddBookForm addForm = new AddBookForm(connectionString);

            if (addForm.ShowDialog() == DialogResult.OK)

            {
                LoadBooks();
            }

        }

        private void btn_EditBook_Click(object sender, EventArgs e)
        {
            // future code
        }

        private void btn_AddCopies_Click(object sender, EventArgs e)
        {
            // future code
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            // future code
        }

        private void btn_Import_Click(object sender, EventArgs e)
        {
            // future code
        }

        private void dta_Grid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedStatus = cmbStatus.Text; // get selected value

            DataView dv = ((DataTable)dta_Grid1.DataSource).DefaultView;
            if (selectedStatus == "All")
                dv.RowFilter = ""; // show all
            else
                dv.RowFilter = $"Available = '{selectedStatus}'"; // filter by Available column
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();

            DataView dv = ((DataTable)dta_Grid1.DataSource).DefaultView;
            dv.RowFilter = $"Title LIKE '%{searchText}%' OR Author LIKE '%{searchText}%' OR AccessionNumber LIKE '%{searchText}%'";
        }

        private void cmbTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedType = cmbTypes.Text;

            DataView dv = ((DataTable)dta_Grid1.DataSource).DefaultView;
            if (selectedType == "All")
                dv.RowFilter = "";
            else
                dv.RowFilter = $"Category = '{selectedType}'";
        }
    }
}
