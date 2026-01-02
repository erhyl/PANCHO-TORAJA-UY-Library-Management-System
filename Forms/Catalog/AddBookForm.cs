using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace Project5LMS.Admin_Dashboard
{
    public partial class AddBookForm : Form
    {
        private string connectionString;
        private string selectedImagePath = "";

        public AddBookForm(string conString)
        {
            InitializeComponent();
            connectionString = conString ?? ConfigurationManager.ConnectionStrings["MySqlConnectionString"]?.ConnectionString 
                ?? "Server=localhost;Database=librarydb;Uid=root;Pwd=;";
        }

        private void AddBookForm_Load(object sender, EventArgs e)
        {
            EnsureAccessionNumberColumnExists();
            LoadCategories();
            LoadPublishers();
            GenerateAccessionNumber();
            StyleControls();
        }

        private void StyleControls()
        {
            int radius = 10;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius * 2, radius * 2, 180, 90);
            path.AddArc(panelMainContainer.Width - radius * 2, 0, radius * 2, radius * 2, 270, 90);
            path.AddArc(panelMainContainer.Width - radius * 2, panelMainContainer.Height - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(0, panelMainContainer.Height - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseAllFigures();
            panelMainContainer.Region = new Region(path);
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
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error ensuring AccessionNumber column: {ex.Message}");
            }
        }

        private void GenerateAccessionNumber()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT COALESCE(MAX(BookID), 0) + 1 as NextID FROM Books";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        int nextId = Convert.ToInt32(cmd.ExecuteScalar());
                        txtAccessionNumber.Text = $"ACC-{nextId:D4}";
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error generating accession number: {ex.Message}");
                txtAccessionNumber.Text = "ACC-0001";
            }
        }

        private void LoadCategories()
        {
            try
            {
                string query = "SELECT DISTINCT Category FROM Books WHERE Category IS NOT NULL AND Category != '' ORDER BY Category";
                
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        cmbCategory.Items.Clear();
                        cmbCategory.Items.Add("Select Category");
                        while (reader.Read())
                        {
                            cmbCategory.Items.Add(reader["Category"].ToString());
                        }
                    }
                }

                // Also try Categories table if it exists
                try
                {
                    string categoryQuery = "SELECT CategoryName FROM Categories ORDER BY CategoryName";
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    using (MySqlCommand cmd = new MySqlCommand(categoryQuery, conn))
                    {
                        conn.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (cmbCategory.Items.Count == 1) // Only "Select Category"
                            {
                                while (reader.Read())
                                {
                                    cmbCategory.Items.Add(reader["CategoryName"].ToString());
                                }
                            }
                        }
                    }
                }
                catch { }

                if (cmbCategory.Items.Count > 0 && cmbCategory.SelectedIndex == -1)
                {
                    cmbCategory.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading categories: {ex.Message}");
                cmbCategory.Items.Clear();
                cmbCategory.Items.Add("Select Category");
                cmbCategory.SelectedIndex = 0;
            }
        }

        private void LoadPublishers()
        {
            try
            {
                string query = "SELECT DISTINCT Publisher FROM Books WHERE Publisher IS NOT NULL AND Publisher != '' ORDER BY Publisher";
                
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        cmbPublisher.Items.Clear();
                        cmbPublisher.Items.Add("Select Publisher");
                        while (reader.Read())
                        {
                            cmbPublisher.Items.Add(reader["Publisher"].ToString());
                        }
                        cmbPublisher.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading publishers: {ex.Message}");
                cmbPublisher.Items.Clear();
                cmbPublisher.Items.Add("Select Publisher");
                cmbPublisher.SelectedIndex = 0;
            }
        }

        private void picCoverPhoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                ofd.Title = "Select Book Cover";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    selectedImagePath = ofd.FileName;
                    picCoverPhoto.Image = Image.FromFile(selectedImagePath);
                    picCoverPhoto.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            string coverImagePathInDb = "";
            if (!string.IsNullOrEmpty(selectedImagePath))
            {
                try
                {
                    string imagesFolder = Path.Combine(Application.StartupPath, "Images");
                    if (!Directory.Exists(imagesFolder))
                        Directory.CreateDirectory(imagesFolder);

                    string fileName = Path.GetFileName(selectedImagePath);
                    string destPath = Path.Combine(imagesFolder, fileName);

                    File.Copy(selectedImagePath, destPath, true);
                    coverImagePathInDb = Path.Combine("Images", fileName);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error copying image: {ex.Message}");
                }
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string insertQuery = @"INSERT INTO Books 
                    (Title, Author, ISBN, Publisher, YearPublished, Category, Copies, Available, AccessionNumber, CoverImagePath)
                    VALUES
                    (@Title, @Author, @ISBN, @Publisher, @Year, @Category, @Copies, @Available, @AccessionNumber, @CoverImage)";

                using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Title", txtTitle.Text.Trim());
                    cmd.Parameters.AddWithValue("@Author", txtAuthor.Text.Trim());
                    cmd.Parameters.AddWithValue("@ISBN", txtISBN.Text.Trim());
                    cmd.Parameters.AddWithValue("@Publisher", cmbPublisher.Text);
                    cmd.Parameters.AddWithValue("@Year", txtPublicationYear.Text.Trim());
                    cmd.Parameters.AddWithValue("@Category", cmbCategory.Text);
                    cmd.Parameters.AddWithValue("@Copies", Convert.ToInt32(txtCopies.Text));
                    cmd.Parameters.AddWithValue("@Available", Convert.ToInt32(txtAvailable.Text));
                    cmd.Parameters.AddWithValue("@AccessionNumber", txtAccessionNumber.Text.Trim());
                    cmd.Parameters.AddWithValue("@CoverImage", coverImagePathInDb);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Book saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error saving book: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Please enter Title.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTitle.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAuthor.Text))
            {
                MessageBox.Show("Please enter Author.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAuthor.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtISBN.Text))
            {
                MessageBox.Show("Please enter ISBN.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtISBN.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cmbPublisher.Text) || cmbPublisher.Text == "Select Publisher")
            {
                MessageBox.Show("Please enter or select Publisher.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbPublisher.Focus();
                return false;
            }

            if (cmbCategory.SelectedIndex <= 0 || cmbCategory.Text == "Select Category")
            {
                MessageBox.Show("Please select Category.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCategory.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPublicationYear.Text))
            {
                MessageBox.Show("Please enter Publication Year.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPublicationYear.Focus();
                return false;
            }

            if (!int.TryParse(txtPublicationYear.Text, out int year) || year < 1000 || year > 9999)
            {
                MessageBox.Show("Please enter a valid year (4 digits).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPublicationYear.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCopies.Text) || !int.TryParse(txtCopies.Text, out int copies) || copies < 0)
            {
                MessageBox.Show("Please enter a valid number of Copies.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCopies.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAvailable.Text) || !int.TryParse(txtAvailable.Text, out int available) || available < 0)
            {
                MessageBox.Show("Please enter a valid number of Available copies.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAvailable.Focus();
                return false;
            }

            if (available > copies)
            {
                MessageBox.Show("Available copies cannot exceed total Copies.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAvailable.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAccessionNumber.Text))
            {
                MessageBox.Show("Please enter Accession Number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAccessionNumber.Focus();
                return false;
            }

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtPublicationYear_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
