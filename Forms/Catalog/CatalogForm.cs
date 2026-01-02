using System;
using System.Configuration;
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
        private string connectionString;

        public CatalogForm()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["MySqlConnectionString"]?.ConnectionString 
                ?? "Server=localhost;Database=librarydb;Uid=root;Pwd=;";
        }

        private void Catalog_Load(object sender, EventArgs e)
        {
            EnsureAccessionNumberColumnExists();
            SetupDataGridView();
            LoadFilters();
            LoadCatalog();
        }

        private void SetupDataGridView()
        {
            dta_Grid1.AutoGenerateColumns = false;
            dta_Grid1.Columns.Clear();
            dta_Grid1.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 11F);
            dta_Grid1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Bold);
            dta_Grid1.RowTemplate.Height = 40;
            dta_Grid1.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dta_Grid1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            dta_Grid1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dta_Grid1.EnableHeadersVisualStyles = false;
        }

        private void EnsureAccessionNumberColumnExists()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string checkColumnQuery = @"
                        SELECT COUNT(*) 
                        FROM INFORMATION_SCHEMA.COLUMNS 
                        WHERE TABLE_SCHEMA = DATABASE() 
                        AND TABLE_NAME = 'Books' 
                        AND COLUMN_NAME = 'AccessionNumber'";

                    using (MySqlCommand checkCmd = new MySqlCommand(checkColumnQuery, conn))
                    {
                        int columnExists = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (columnExists == 0)
                        {
                            string addColumnQuery = @"
                                ALTER TABLE Books 
                                ADD COLUMN AccessionNumber VARCHAR(50) NULL";

                            using (MySqlCommand addCmd = new MySqlCommand(addColumnQuery, conn))
                            {
                                addCmd.ExecuteNonQuery();
                            }

                            string generateAccessionQuery = @"
                                UPDATE Books 
                                SET AccessionNumber = CONCAT('ACC-', LPAD(BookID, 4, '0')) 
                                WHERE AccessionNumber IS NULL OR AccessionNumber = ''";

                            using (MySqlCommand genCmd = new MySqlCommand(generateAccessionQuery, conn))
                            {
                                genCmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            string generateAccessionQuery = @"
                                UPDATE Books 
                                SET AccessionNumber = CONCAT('ACC-', LPAD(BookID, 4, '0')) 
                                WHERE AccessionNumber IS NULL OR AccessionNumber = ''";

                            using (MySqlCommand genCmd = new MySqlCommand(generateAccessionQuery, conn))
                            {
                                genCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error ensuring AccessionNumber column: {ex.Message}");
            }
        }

        private void LoadFilters()
        {
            try
            {
                cmbTypes.Items.Clear();
                cmbTypes.Items.Add("All Types");
                
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT DISTINCT Category FROM Books WHERE Category IS NOT NULL AND Category != '' ORDER BY Category";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbTypes.Items.Add(reader["Category"].ToString());
                        }
                    }
                }
                cmbTypes.SelectedIndex = 0;

                cmbStatus.Items.Clear();
                cmbStatus.Items.Add("All Status");
                cmbStatus.Items.Add("Available");
                cmbStatus.Items.Add("Unavailable");
                cmbStatus.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading filters: {ex.Message}");
            }
        }

        private void LoadCatalog()
        {
            try
            {
                string keyword = txtSearch.Text.Trim();
                if (keyword == "Search by name, ID, or email") keyword = "";
                
                string type = cmbTypes.Text == "All Types" ? "All" : cmbTypes.Text;
                string status = cmbStatus.Text == "All Status" ? "All" : cmbStatus.Text;

                string query = @"SELECT 
                                    BookID,
                                    Title,
                                    Author,
                                    ISBN,
                                    Publisher,
                                    Category,
                                    YearPublished as Year,
                                    Copies,
                                    AccessionNumber as Accession,
                                    Available
                                FROM Books
                                WHERE 
                                    (@Keyword = '' OR Title LIKE @Keyword OR Author LIKE @Keyword OR ISBN LIKE @Keyword OR AccessionNumber LIKE @Keyword)
                                    AND (@Type = 'All' OR Category = @Type)
                                    AND (@Status = 'All' OR 
                                        (@Status = 'Available' AND Available > 0) OR 
                                        (@Status = 'Unavailable' AND Available = 0))
                                ORDER BY BookID";

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                    cmd.Parameters.AddWithValue("@Type", type);
                    cmd.Parameters.AddWithValue("@Status", status);
                    
                    conn.Open();
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dta_Grid1.DataSource = dt;

                        if (dta_Grid1.Columns["BookID"] != null)
                            dta_Grid1.Columns["BookID"].Visible = false;

                        // Set column headers
                        if (dta_Grid1.Columns["Accession"] != null)
                            dta_Grid1.Columns["Accession"].HeaderText = "Accession#";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading catalog: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddBook_Click(object sender, EventArgs e)
        {
            AddBookForm addForm = new AddBookForm(connectionString);
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                LoadCatalog();
            }
        }

        private void btnAddCopies_Click(object sender, EventArgs e)
        {
            if (dta_Grid1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a book to add copies.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int bookId = Convert.ToInt32(dta_Grid1.SelectedRows[0].Cells["BookID"].Value);
            string title = dta_Grid1.SelectedRows[0].Cells["Title"].Value?.ToString() ?? "";

            using (var inputForm = new Form())
            {
                inputForm.Text = "Add Copies";
                inputForm.Size = new Size(300, 150);
                inputForm.StartPosition = FormStartPosition.CenterParent;
                inputForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                inputForm.MaximizeBox = false;
                inputForm.MinimizeBox = false;

                var lblPrompt = new Label { Text = $"Enter number of copies to add for '{title}':", AutoSize = true, Location = new Point(10, 10) };
                var txtCopies = new TextBox { Location = new Point(10, 40), Width = 260, Text = "1" };
                var btnOK = new Button { Text = "OK", DialogResult = DialogResult.OK, Location = new Point(100, 75), Width = 80 };
                var btnCancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Location = new Point(190, 75), Width = 80 };

                inputForm.Controls.AddRange(new Control[] { lblPrompt, txtCopies, btnOK, btnCancel });
                inputForm.AcceptButton = btnOK;
                inputForm.CancelButton = btnCancel;

                if (inputForm.ShowDialog() == DialogResult.OK && int.TryParse(txtCopies.Text, out int copies) && copies > 0)
                {
                    try
                    {
                        using (MySqlConnection conn = new MySqlConnection(connectionString))
                        {
                            conn.Open();
                            string query = "UPDATE Books SET Copies = Copies + @Copies, Available = Available + @Copies WHERE BookID = @BookID";
                            using (MySqlCommand cmd = new MySqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@BookID", bookId);
                                cmd.Parameters.AddWithValue("@Copies", copies);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        MessageBox.Show($"{copies} copy/copies added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadCatalog();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error adding copies: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnEditBook_Click(object sender, EventArgs e)
        {
            if (dta_Grid1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a book to edit.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int bookId = Convert.ToInt32(dta_Grid1.SelectedRows[0].Cells["BookID"].Value);
            EditBookForm editForm = new EditBookForm(bookId, connectionString);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadCatalog();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dta_Grid1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a book to delete.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int bookId = Convert.ToInt32(dta_Grid1.SelectedRows[0].Cells["BookID"].Value);
            string title = dta_Grid1.SelectedRows[0].Cells["Title"].Value?.ToString() ?? "";

            DialogResult result = MessageBox.Show($"Are you sure you want to delete '{title}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            
            if (result == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "DELETE FROM Books WHERE BookID = @BookID";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@BookID", bookId);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Book deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCatalog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting book: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv|Excel Files (*.xlsx;*.xls)|*.xlsx;*.xls|All Files (*.*)|*.*",
                Title = "Import Books"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string filePath = openFileDialog.FileName;
                    string extension = Path.GetExtension(filePath).ToLower();

                    if (extension == ".csv")
                    {
                        MessageBox.Show("CSV import functionality can be implemented here.\nFile selected: " + filePath, "Import", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Excel import functionality can be implemented here.\nFile selected: " + filePath, "Import", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error importing file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search by name, ID, or email")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Search by name, ID, or email";
                txtSearch.ForeColor = Color.Gray;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "Search by name, ID, or email")
            {
                LoadCatalog();
            }
        }

        private void cmbTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCatalog();
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCatalog();
        }

        private void dta_Grid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle cell clicks if needed
        }
    }
}
