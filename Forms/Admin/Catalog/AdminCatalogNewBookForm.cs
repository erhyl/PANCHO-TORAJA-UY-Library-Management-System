using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Project5LMS.Helpers;
using Project5LMS.Data;
using Project5LMS.Services;
using Project5LMS.Models;

namespace Project5LMS.Forms.Admin.Catalog
{
    public partial class AdminCatalogNewBookForm : Form
    {
        private readonly DatabaseContext _dbContext;
        private string bookCoverImagePath = null;

        public AdminCatalogNewBookForm()
        {
            InitializeComponent();
            _dbContext = ServiceFactory.GetDbContext();
        }

        // Resource type is determined dynamically from cmbResourceType selection

        private void AdminCatalogNewBookForm_Load(object sender, EventArgs e)
        {
            try
            {
                LoadCategories();
                LoadLanguages();
                LoadResourceTypes();
                GenerateAccessionNumber();
                radioCirculationBook.Checked = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadResourceTypes()
        {
            // Resource type selection will be handled via a combo box or radio buttons
            // For now, default to Book. Specialized forms can be opened based on selection.
        }

        private void LoadCategories()
        {
            try
            {
                cmbCategory.Items.Clear();
                cmbCategory.Items.Add("Select category...");

                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT DISTINCT Category FROM Books WHERE Category IS NOT NULL AND Category != '' ORDER BY Category";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbCategory.Items.Add(reader["Category"].ToString());
                        }
                    }
                }

                if (cmbCategory.Items.Count > 0)
                {
                    cmbCategory.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading categories: {ex.Message}");
            }
        }

        private void LoadLanguages()
        {
            try
            {
                cmbLanguage.Items.Clear();
                cmbLanguage.Items.AddRange(new string[] { "English", "Spanish", "French", "German", "Chinese", "Japanese", "Korean", "Arabic", "Hindi", "Other" });
                cmbLanguage.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading languages: {ex.Message}");
            }
        }

        private void GenerateAccessionNumber()
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT MAX(BookID) as MaxID FROM Books";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        int maxId = result != DBNull.Value && result != null ? Convert.ToInt32(result) : 0;
                        txtAccessionNumber.Text = $"ACC-{(maxId + 1).ToString().PadLeft(4, '0')}";
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error generating accession number: {ex.Message}");
                txtAccessionNumber.Text = "ACC-0001";
            }
        }

        private void panelUploadArea_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png|All Files|*.*";
                    openFileDialog.FilterIndex = 1;
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = openFileDialog.FileName;
                        FileInfo fileInfo = new FileInfo(filePath);

                        if (fileInfo.Length > 5 * 1024 * 1024)
                        {
                            MessageBox.Show("File size exceeds 5MB limit.", "File Too Large", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        bookCoverImagePath = filePath;
                        picBookCover.Image = Image.FromFile(filePath);
                        picBookCover.Visible = true;
                        lblUploadText.Visible = false;
                        lblFileInfo.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panelUploadArea_Paint(object sender, PaintEventArgs e)
        {
            if (picBookCover.Visible == false)
            {

                using (Pen pen = new Pen(Color.FromArgb(200, 200, 200), 2))
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    e.Graphics.DrawRectangle(pen, 0, 0, panelUploadArea.Width - 1, panelUploadArea.Height - 1);
                }

                int centerX = panelUploadArea.Width / 2;
                int centerY = panelUploadArea.Height / 2 - 10;
                using (Pen pen = new Pen(Color.FromArgb(150, 150, 150), 2))
                {

                    e.Graphics.DrawLine(pen, centerX, centerY - 15, centerX, centerY + 5);
                    e.Graphics.DrawLine(pen, centerX - 5, centerY - 5, centerX, centerY - 15);
                    e.Graphics.DrawLine(pen, centerX + 5, centerY - 5, centerX, centerY - 15);
                }
            }
        }

        private void btnIncreaseCopies_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(txtNumberOfCopies.Text, out int copies))
                {
                    copies++;
                    txtNumberOfCopies.Text = copies.ToString();
                }
                else
                {
                    txtNumberOfCopies.Text = "1";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error increasing copies: {ex.Message}");
            }
        }

        private void btnDecreaseCopies_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(txtNumberOfCopies.Text, out int copies) && copies > 1)
                {
                    copies--;
                    txtNumberOfCopies.Text = copies.ToString();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error decreasing copies: {ex.Message}");
            }
        }

        private void radioCirculationBook_CheckedChanged(object sender, EventArgs e)
        {
            if (radioCirculationBook.Checked)
            {
                panelCirculationBookCard.BackColor = Color.FromArgb(230, 240, 255);
                panelReferenceBookCard.BackColor = Color.FromArgb(248, 249, 250);
            }
        }

        private void radioReferenceBook_CheckedChanged(object sender, EventArgs e)
        {
            if (radioReferenceBook.Checked)
            {
                panelReferenceBookCard.BackColor = Color.FromArgb(230, 240, 255);
                panelCirculationBookCard.BackColor = Color.FromArgb(248, 249, 250);
            }
        }

        private void panelCirculationBookCard_Click(object sender, EventArgs e)
        {
            radioCirculationBook.Checked = true;
        }

        private void panelReferenceBookCard_Click(object sender, EventArgs e)
        {
            radioReferenceBook.Checked = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnAddBook_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInputs())
                {
                    return;
                }

                SaveBook();
                MessageBox.Show("Book added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding book: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInputs()
        {

            if (string.IsNullOrWhiteSpace(txtAccessionNumber.Text))
            {
                MessageBox.Show("Accession Number is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAccessionNumber.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtISBN.Text))
            {
                MessageBox.Show("ISBN is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtISBN.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Title is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTitle.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAuthors.Text))
            {
                MessageBox.Show("Author(s) is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAuthors.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPublisher.Text))
            {
                MessageBox.Show("Publisher is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPublisher.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPublicationYear.Text))
            {
                MessageBox.Show("Publication Year is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPublicationYear.Focus();
                return false;
            }

            if (cmbCategory.SelectedIndex <= 0 || cmbCategory.SelectedItem.ToString() == "Select category...")
            {
                MessageBox.Show("Please select a category.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCategory.Focus();
                return false;
            }

            if (cmbLanguage.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a language.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbLanguage.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtShelfLocation.Text))
            {
                MessageBox.Show("Shelf Location is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtShelfLocation.Focus();
                return false;
            }

            if (!int.TryParse(txtNumberOfCopies.Text, out int copies) || copies < 1)
            {
                MessageBox.Show("Number of Copies must be at least 1.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNumberOfCopies.Focus();
                return false;
            }

            return true;
        }

        private void SaveBook()
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();

                    string bookType = radioCirculationBook.Checked ? "Circulation" : "Reference";
                    int copies = int.Parse(txtNumberOfCopies.Text);
                    int pages = int.TryParse(txtNumberOfPages.Text, out int p) ? p : 0;

                    string insertQuery = @"INSERT INTO Books 
                        (Title, Subtitle, Author, Editor, ISBN, Publisher, YearPublished, Edition, 
                         Category, SubjectClassification, Language, NumberOfPages, PhysicalDescription, 
                         Location, Copies, Available, BookType, Barcode, CallNumber)
                        VALUES 
                        (@Title, @Subtitle, @Author, @Editor, @ISBN, @Publisher, @YearPublished, @Edition,
                         @Category, @SubjectClassification, @Language, @NumberOfPages, @PhysicalDescription,
                         @Location, @Copies, @Available, @BookType, @Barcode, @CallNumber)";

                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Title", txtTitle.Text.Trim());
                        cmd.Parameters.AddWithValue("@Subtitle", string.IsNullOrWhiteSpace(txtSubtitle.Text) ? DBNull.Value : (object)txtSubtitle.Text.Trim());
                        cmd.Parameters.AddWithValue("@Author", txtAuthors.Text.Trim());
                        cmd.Parameters.AddWithValue("@Editor", string.IsNullOrWhiteSpace(txtEditors.Text) ? DBNull.Value : (object)txtEditors.Text.Trim());
                        cmd.Parameters.AddWithValue("@ISBN", txtISBN.Text.Trim());
                        cmd.Parameters.AddWithValue("@Publisher", txtPublisher.Text.Trim());
                        cmd.Parameters.AddWithValue("@YearPublished", int.Parse(txtPublicationYear.Text));
                        cmd.Parameters.AddWithValue("@Edition", string.IsNullOrWhiteSpace(txtEdition.Text) ? DBNull.Value : (object)txtEdition.Text.Trim());
                        cmd.Parameters.AddWithValue("@Category", cmbCategory.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@SubjectClassification", string.IsNullOrWhiteSpace(txtSubjectClassification.Text) ? DBNull.Value : (object)txtSubjectClassification.Text.Trim());
                        cmd.Parameters.AddWithValue("@Language", cmbLanguage.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@NumberOfPages", pages);
                        cmd.Parameters.AddWithValue("@PhysicalDescription", string.IsNullOrWhiteSpace(txtPhysicalDescription.Text) ? DBNull.Value : (object)txtPhysicalDescription.Text.Trim());
                        cmd.Parameters.AddWithValue("@Location", txtShelfLocation.Text.Trim());
                        cmd.Parameters.AddWithValue("@Copies", copies);
                        cmd.Parameters.AddWithValue("@Available", copies);
                        cmd.Parameters.AddWithValue("@BookType", bookType);
                        
                        // Auto-generate barcode from accession number or ISBN
                        string barcode = string.IsNullOrWhiteSpace(txtAccessionNumber.Text) 
                            ? BarcodeGenerator.GenerateFromISBN(txtISBN.Text.Trim())
                            : BarcodeGenerator.GenerateFromAccession(txtAccessionNumber.Text.Trim());
                        
                        cmd.Parameters.AddWithValue("@Barcode", barcode);
                        cmd.Parameters.AddWithValue("@CallNumber", string.IsNullOrWhiteSpace(txtCallNumber.Text) ? DBNull.Value : (object)txtCallNumber.Text.Trim());

                        cmd.ExecuteNonQuery();
                    }

                    if (!string.IsNullOrEmpty(bookCoverImagePath) && File.Exists(bookCoverImagePath))
                    {

                        System.Diagnostics.Debug.WriteLine($"Book cover image: {bookCoverImagePath}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to save book: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Opens specialized form for resource type and saves the resource
        /// </summary>
        public void OpenResourceTypeForm(string resourceType)
        {
            try
            {
                if (!ValidateInputs())
                {
                    return;
                }

                // Save base book first to get BookID
                SaveBook();
                
                // Get the saved book ID
                int bookId = 0;
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT MAX(BookID) as LastID FROM Books";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        object maxIdResult = cmd.ExecuteScalar();
                        if (maxIdResult != DBNull.Value && maxIdResult != null)
                        {
                            bookId = Convert.ToInt32(maxIdResult);
                        }
                    }
                }

                if (bookId == 0)
                {
                    MessageBox.Show("Error: Could not retrieve book ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Create base book object
                Book baseBook = new Book
                {
                    BookID = bookId,
                    Title = txtTitle.Text.Trim(),
                    Author = txtAuthors.Text.Trim(),
                    ISBN = txtISBN.Text.Trim(),
                    Publisher = txtPublisher.Text.Trim(),
                    PublicationYear = int.Parse(txtPublicationYear.Text),
                    Category = cmbCategory.SelectedItem?.ToString(),
                    Language = cmbLanguage.SelectedItem?.ToString()
                };

                // Open appropriate specialized form
                DialogResult dialogResult = DialogResult.Cancel;
                switch (resourceType.ToLower())
                {
                    case "periodical":
                        using (var form = new PeriodicalForm(baseBook))
                        {
                            dialogResult = form.ShowDialog();
                            if (dialogResult == DialogResult.OK)
                            {
                                var periodical = form.GetPeriodical(baseBook);
                                SavePeriodical(periodical);
                            }
                        }
                        break;
                    case "thesis":
                        using (var form = new ThesisForm(baseBook))
                        {
                            dialogResult = form.ShowDialog();
                            if (dialogResult == DialogResult.OK)
                            {
                                var thesis = form.GetThesis(baseBook);
                                SaveThesis(thesis);
                            }
                        }
                        break;
                    case "audiovisual":
                        using (var form = new AudioVisualForm(baseBook))
                        {
                            dialogResult = form.ShowDialog();
                            if (dialogResult == DialogResult.OK)
                            {
                                var av = form.GetAudioVisual(baseBook);
                                SaveAudioVisual(av);
                            }
                        }
                        break;
                    case "ebook":
                        using (var form = new EBookForm(baseBook))
                        {
                            dialogResult = form.ShowDialog();
                            if (dialogResult == DialogResult.OK)
                            {
                                var ebook = form.GetEBook(baseBook);
                                SaveEBook(ebook);
                            }
                        }
                        break;
                }

                if (dialogResult == DialogResult.OK)
                {
                    MessageBox.Show($"{resourceType} added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding {resourceType}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SavePeriodical(Periodical periodical)
        {
            // Additional periodical-specific data can be saved to a separate table or added to Books table
            // For now, the base book is already saved
        }

        private void SaveThesis(Thesis thesis)
        {
            // Additional thesis-specific data can be saved
        }

        private void SaveAudioVisual(AudioVisual av)
        {
            // Additional audio-visual-specific data can be saved
        }

        private void SaveEBook(EBook ebook)
        {
            // Additional ebook-specific data can be saved
        }
    }
}
