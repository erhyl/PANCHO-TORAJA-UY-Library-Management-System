using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Project5LMS.Admin_Dashboard
{
    public partial class AddBookForm : Form
    {
        private string connectionString;
        private string selectedImagePath = ""; // For storing chosen cover image

        public AddBookForm(string conString)
        {
            InitializeComponent();
            connectionString = conString;
        }

        private void LoadCategories()
        {
            string query = "SELECT CategoryName FROM Categories ORDER BY CategoryName";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                cboCategory.Items.Clear();

                while (reader.Read())
                {
                    cboCategory.Items.Add(reader.GetString("CategoryName"));
                }

                reader.Close();
            }

            if (cboCategory.Items.Count > 0)
                cboCategory.SelectedIndex = 0;
        }

        private void AddBookForm_Load(object sender, EventArgs e)
        {
            LoadCategories();
        }

        private void btnChooseCover_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                ofd.Title = "Select Book Cover";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    selectedImagePath = ofd.FileName;
                    pictureBoxCover.Image = Image.FromFile(selectedImagePath);
                    pictureBoxCover.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(txtTitle.Text) ||
                string.IsNullOrWhiteSpace(txtAuthor.Text) ||
                string.IsNullOrWhiteSpace(txtISBN.Text) ||
                string.IsNullOrWhiteSpace(txtPublisher.Text) ||
                string.IsNullOrWhiteSpace(txtYear.Text) ||
                cboCategory.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(txtCopies.Text) ||
                string.IsNullOrWhiteSpace(txtBarcode.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Handle image
            string coverImagePathInDb = "";
            if (!string.IsNullOrEmpty(selectedImagePath))
            {
                string imagesFolder = Path.Combine(Application.StartupPath, "Images");
                if (!Directory.Exists(imagesFolder))
                    Directory.CreateDirectory(imagesFolder);

                string fileName = Path.GetFileName(selectedImagePath);
                string destPath = Path.Combine(imagesFolder, fileName);

                File.Copy(selectedImagePath, destPath, true);
                coverImagePathInDb = Path.Combine("Images", fileName);
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                // 1️⃣ Insert the book WITHOUT AccessionNumber
                string insertQuery = @"INSERT INTO Books 
                    (Title, Author, ISBN, Publisher, YearPublished, Category, Copies, Available, Barcode, CoverImagePath)
                    VALUES
                    (@Title, @Author, @ISBN, @Publisher, @Year, @Category, @Copies, @Available, @Barcode, @CoverImage)";

                using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                    cmd.Parameters.AddWithValue("@Author", txtAuthor.Text);
                    cmd.Parameters.AddWithValue("@ISBN", txtISBN.Text);
                    cmd.Parameters.AddWithValue("@Publisher", txtPublisher.Text);
                    cmd.Parameters.AddWithValue("@Year", txtYear.Text);
                    cmd.Parameters.AddWithValue("@Category", cboCategory.Text);
                    cmd.Parameters.AddWithValue("@Copies", txtCopies.Text);
                    cmd.Parameters.AddWithValue("@Available", txtCopies.Text);
                    cmd.Parameters.AddWithValue("@Barcode", txtBarcode.Text);
                    cmd.Parameters.AddWithValue("@CoverImage", coverImagePathInDb);

                    try
                    {
                        cmd.ExecuteNonQuery();

                        // 2️⃣ Get last inserted BookID
                        long bookId = cmd.LastInsertedId;

                        // 3️⃣ Generate accession number
                        string accessionNumber = "ACC" + bookId;

                        // 4️⃣ Update the row with accession number
                        string updateQuery = "UPDATE Books SET AccessionNumber = @Accession WHERE BookID = @BookID";
                        using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn))
                        {
                            updateCmd.Parameters.AddWithValue("@Accession", accessionNumber);
                            updateCmd.Parameters.AddWithValue("@BookID", bookId);
                            updateCmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Book saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error saving book: " + ex.Message);
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Empty event handlers (optional, can remove if unused)
        private void txtTitle_TextChanged(object sender, EventArgs e) { }
        private void txtAuthor_TextChanged(object sender, EventArgs e) { }
        private void txtISBN_TextChanged(object sender, EventArgs e) { }
        private void txtPublisher_TextChanged(object sender, EventArgs e) { }
        private void txtYear_TextChanged(object sender, EventArgs e) { }
        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txtCopies_TextChanged(object sender, EventArgs e) { }
        private void txtBarcode_TextChanged(object sender, EventArgs e) { }
    }
}
