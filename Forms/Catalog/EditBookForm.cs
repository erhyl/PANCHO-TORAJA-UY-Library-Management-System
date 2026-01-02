using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Project5LMS.Forms.Catalog
{
    public partial class EditBookForm : Form
    {
        private string connectionString;
        private int bookId;
        private string selectedImagePath = "";

        public EditBookForm(int bookId, string conString = null)
        {
            InitializeComponent();
            this.bookId = bookId;
            connectionString = conString ?? ConfigurationManager.ConnectionStrings["MySqlConnectionString"]?.ConnectionString 
                ?? "Server=localhost;Database=librarydb;Uid=root;Pwd=;";
        }

        private void EditBookForm_Load(object sender, EventArgs e)
        {
            LoadCategories();
            LoadPublishers();
            LoadBookData();
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
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading categories: {ex.Message}");
                cmbCategory.Items.Clear();
                cmbCategory.Items.Add("Select Category");
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
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading publishers: {ex.Message}");
                cmbPublisher.Items.Clear();
                cmbPublisher.Items.Add("Select Publisher");
            }
        }

        private void LoadBookData()
        {
            try
            {
                string query = @"SELECT BookID, AccessionNumber, Title, Author, ISBN, Publisher, 
                                YearPublished, Category, Copies, Available, CoverImagePath
                         FROM Books
                         WHERE BookID = @BookID";

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@BookID", bookId);
                    conn.Open();
                    
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtAccessionNumber.Text = reader["AccessionNumber"] != DBNull.Value ? reader["AccessionNumber"].ToString() : "";
                            txtTitle.Text = reader["Title"] != DBNull.Value ? reader["Title"].ToString() : "";
                            txtAuthor.Text = reader["Author"] != DBNull.Value ? reader["Author"].ToString() : "";
                            txtISBN.Text = reader["ISBN"] != DBNull.Value ? reader["ISBN"].ToString() : "";
                            
                            string publisher = reader["Publisher"] != DBNull.Value ? reader["Publisher"].ToString() : "";
                            if (!string.IsNullOrEmpty(publisher))
                            {
                                if (!cmbPublisher.Items.Contains(publisher))
                                    cmbPublisher.Items.Add(publisher);
                                cmbPublisher.Text = publisher;
                            }
                            
                            string category = reader["Category"] != DBNull.Value ? reader["Category"].ToString() : "";
                            if (!string.IsNullOrEmpty(category))
                            {
                                if (!cmbCategory.Items.Contains(category))
                                    cmbCategory.Items.Add(category);
                                cmbCategory.Text = category;
                            }
                            
                            txtCopies.Text = reader["Copies"] != DBNull.Value ? reader["Copies"].ToString() : "0";
                            txtAvailable.Text = reader["Available"] != DBNull.Value ? reader["Available"].ToString() : "0";
                            txtPublicationYear.Text = reader["YearPublished"] != DBNull.Value ? reader["YearPublished"].ToString() : "";
                            
                            // Load cover image if exists
                            if (reader["CoverImagePath"] != DBNull.Value && !string.IsNullOrEmpty(reader["CoverImagePath"].ToString()))
                            {
                                string imagePath = reader["CoverImagePath"].ToString();
                                string fullPath = Path.Combine(Application.StartupPath, imagePath);
                                if (File.Exists(fullPath))
                                {
                                    picCoverPhoto.Image = Image.FromFile(fullPath);
                                    picCoverPhoto.SizeMode = PictureBoxSizeMode.StretchImage;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Book not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading book data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
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
            else
            {
                // Keep existing image path if no new image selected
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    using (MySqlCommand cmd = new MySqlCommand("SELECT CoverImagePath FROM Books WHERE BookID = @BookID", conn))
                    {
                        cmd.Parameters.AddWithValue("@BookID", bookId);
                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != DBNull.Value && result != null)
                        {
                            coverImagePathInDb = result.ToString();
                        }
                    }
                }
                catch { }
            }

            try
            {
                string updateQuery = @"UPDATE Books 
                    SET AccessionNumber = @AccessionNumber,
                        Title = @Title,
                        Author = @Author,
                        ISBN = @ISBN,
                        Publisher = @Publisher,
                        YearPublished = @Year,
                        Category = @Category,
                        Copies = @Copies,
                        Available = @Available,
                        CoverImagePath = @CoverImage
                    WHERE BookID = @BookID";

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@BookID", bookId);
                    cmd.Parameters.AddWithValue("@AccessionNumber", txtAccessionNumber.Text.Trim());
                    cmd.Parameters.AddWithValue("@Title", txtTitle.Text.Trim());
                    cmd.Parameters.AddWithValue("@Author", txtAuthor.Text.Trim());
                    cmd.Parameters.AddWithValue("@ISBN", txtISBN.Text.Trim());
                    cmd.Parameters.AddWithValue("@Publisher", cmbPublisher.Text == "Select Publisher" ? "" : cmbPublisher.Text);
                    cmd.Parameters.AddWithValue("@Year", txtPublicationYear.Text.Trim());
                    cmd.Parameters.AddWithValue("@Category", cmbCategory.Text == "Select Category" ? "" : cmbCategory.Text);
                    cmd.Parameters.AddWithValue("@Copies", Convert.ToInt32(txtCopies.Text));
                    cmd.Parameters.AddWithValue("@Available", Convert.ToInt32(txtAvailable.Text));
                    cmd.Parameters.AddWithValue("@CoverImage", coverImagePathInDb);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Book updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No changes were made.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating book: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            if (string.IsNullOrWhiteSpace(txtAccessionNumber.Text))
            {
                MessageBox.Show("Please enter Accession Number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAccessionNumber.Focus();
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

            if (string.IsNullOrWhiteSpace(cmbCategory.Text) || cmbCategory.Text == "Select Category")
            {
                MessageBox.Show("Please select Category.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCategory.Focus();
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

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
