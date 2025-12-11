using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Project5LMS.Admin_Dashboard
{
    public partial class Catalog : Form
    {
        string connectionString = "Server=localhost;Database=librarydb;Uid=root;Pwd=;";

        public Catalog()
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
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    string query = "SELECT Title, Author, ISBN, Publisher, Category, YearPublished, Copies, AccessionNumber, Available, Barcode FROM Catalog";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dta_Grid1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading catalog: " + ex.Message);
            }
        }

        // Added event handler for CellContentClick
        private void dta_Grid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure a valid row is clicked
            {
                DataGridViewRow row = dta_Grid1.Rows[e.RowIndex];

                // Example: get the value of the clicked row's cells
                string title = row.Cells["Title"].Value.ToString();
                string author = row.Cells["Author"].Value.ToString();
                string isbn = row.Cells["ISBN"].Value.ToString();

                // For now, just display info (you can replace with populating textboxes)
                MessageBox.Show($"Title: {title}\nAuthor: {author}\nISBN: {isbn}");
            }
        }

        private void btn_addBook_Click(object sender, EventArgs e)
        {
            AddBookForm addForm = new AddBookForm(); // same namespace, so no extra using needed
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                LoadCatalog(); // Refresh GridView after adding
            }
        }

        private void btn_EditBook_Click(object sender, EventArgs e)
        {

        }

        private void btn_AddCopies_Click(object sender, EventArgs e)
        {

        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {

        }

        private void btn_Import_Click(object sender, EventArgs e)
        {

        }
    }
}
