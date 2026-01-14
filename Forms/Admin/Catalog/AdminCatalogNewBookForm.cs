using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Project5LMS.Helpers;
using Project5LMS.Data;
using Project5LMS.Services;
using Project5LMS.Models;
using Project5LMS.Interfaces;
using Project5LMS.Repositories;
namespace Project5LMS.Forms.Admin.Catalog
{
    public partial class AdminCatalogNewBookForm : Form
    {
        private readonly DatabaseContext _dbContext;
        private readonly IBookService _bookService;
        private string bookCoverImagePath = null;
        private int? bookId = null; // null for add mode, set for edit mode
        private bool isEditMode => bookId.HasValue;
        
        public AdminCatalogNewBookForm()
        {
            InitializeComponent();
            _dbContext = ServiceFactory.GetDbContext();
            _bookService = ServiceFactory.CreateBookService();
        }
        
        public AdminCatalogNewBookForm(int bookId) : this()
        {
            this.bookId = bookId;
        }
        private void AdminCatalogNewBookForm_Load(object sender, EventArgs e)
        {
            try
            {
                LoadCategories();
                LoadLanguages();
                LoadResourceTypes();
                
                if (isEditMode)
                {
                    LoadBookData();
                    lblFormTitle.Text = "Edit Book";
                    btnAddBook.Text = "Update Book";
                }
                else
                {
                    GenerateAccessionNumber();
                    radioCirculationBook.Checked = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void LoadBookData()
        {
            try
            {
                if (!bookId.HasValue) return;
                
                var book = _bookService.GetBook(bookId.Value);
                if (book == null)
                {
                    MessageBox.Show("Book not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
                
                // Load all book fields
                txtAccessionNumber.Text = book.AccessionNo ?? "";
                txtISBN.Text = book.ISBN ?? "";
                txtCallNumber.Text = book.CallNumber ?? "";
                txtTitle.Text = book.Title ?? "";
                txtSubtitle.Text = book.Subtitle ?? "";
                txtAuthors.Text = book.Author ?? "";
                txtEditors.Text = book.Editor ?? "";
                txtPublisher.Text = book.Publisher ?? "";
                txtPublicationYear.Text = book.PublicationYear > 0 ? book.PublicationYear.ToString() : "";
                txtEdition.Text = book.Edition ?? "";
                
                // Set category
                if (!string.IsNullOrEmpty(book.Category))
                {
                    int categoryIndex = cmbCategory.Items.IndexOf(book.Category);
                    if (categoryIndex >= 0)
                        cmbCategory.SelectedIndex = categoryIndex;
                }
                
                // Set language
                if (!string.IsNullOrEmpty(book.Language))
                {
                    int langIndex = cmbLanguage.Items.IndexOf(book.Language);
                    if (langIndex >= 0)
                        cmbLanguage.SelectedIndex = langIndex;
                }
                
                txtSubjectClassification.Text = ""; // SubjectClassification not in Book model
                txtNumberOfPages.Text = book.NumberOfPages > 0 ? book.NumberOfPages.ToString() : "";
                txtPhysicalDescription.Text = book.PhysicalDescription ?? "";
                txtShelfLocation.Text = book.Location ?? "";
                txtNumberOfCopies.Text = book.TotalCopies.ToString();
                
                // Set book type
                if (book.BookType?.Equals("Reference", StringComparison.OrdinalIgnoreCase) == true)
                {
                    radioReferenceBook.Checked = true;
                }
                else
                {
                    radioCirculationBook.Checked = true;
                }
                
                // Load cover image if exists
                if (!string.IsNullOrEmpty(book.CoverImagePath) && File.Exists(book.CoverImagePath))
                {
                    bookCoverImagePath = book.CoverImagePath;
                    picBookCover.Image = Image.FromFile(book.CoverImagePath);
                    picBookCover.Visible = true;
                    lblUploadText.Visible = false;
                    lblFileInfo.Visible = false;
                }
                
                // Disable accession number in edit mode (should not be changed)
                txtAccessionNumber.ReadOnly = true;
                txtAccessionNumber.BackColor = Color.FromArgb(240, 240, 240);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading book data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"LoadBookData error: {ex}");
            }
        }
        private void LoadResourceTypes()
        {
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
                
                if (isEditMode)
                {
                    UpdateBook();
                    MessageBox.Show("Book updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    SaveBook();
                    MessageBox.Show("Book added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                string action = isEditMode ? "updating" : "adding";
                MessageBox.Show($"Error {action} book: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    
                    // Check which columns exist before building insert query
                    bool hasSubtitle = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "Subtitle");
                    bool hasEditor = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "Editor");
                    bool hasEdition = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "Edition");
                    bool hasSubjectClassification = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "SubjectClassification");
                    bool hasPhysicalDescription = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "PhysicalDescription");
                    bool hasCallNumber = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "CallNumber");
                    bool hasCoverImagePath = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "CoverImagePath");
                    bool hasTotalCopies = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "TotalCopies");
                    bool hasCopies = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "Copies");
                    bool hasPublicationYear = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "PublicationYear");
                    bool hasYearPublished = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "YearPublished");
                    bool hasNumberOfPages = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "NumberOfPages");
                    string yearColumn = hasPublicationYear ? "PublicationYear" : (hasYearPublished ? "YearPublished" : "PublicationYear");
                    string copiesColumn = hasTotalCopies ? "TotalCopies" : (hasCopies ? "Copies" : "TotalCopies");
                    
                    string bookType = radioCirculationBook.Checked ? "Circulation" : "Reference";
                    int copies = int.Parse(txtNumberOfCopies.Text);
                    int pages = int.TryParse(txtNumberOfPages.Text, out int p) ? p : 0;
                    
                    // Build column and value lists dynamically
                    var columns = new List<string> { "Title", "Author", "ISBN", "Publisher", yearColumn, "Category", "Language", "Location", copiesColumn, "Available", "BookType", "Barcode", "AccessionNo" };
                    var values = new List<string> { "@Title", "@Author", "@ISBN", "@Publisher", "@YearPublished", "@Category", "@Language", "@Location", "@Copies", "@Available", "@BookType", "@Barcode", "@AccessionNo" };
                    
                    if (hasNumberOfPages) { columns.Add("NumberOfPages"); values.Add("@NumberOfPages"); }
                    if (hasSubtitle) { columns.Add("Subtitle"); values.Add("@Subtitle"); }
                    if (hasEditor) { columns.Add("Editor"); values.Add("@Editor"); }
                    if (hasEdition) { columns.Add("Edition"); values.Add("@Edition"); }
                    if (hasSubjectClassification) { columns.Add("SubjectClassification"); values.Add("@SubjectClassification"); }
                    if (hasPhysicalDescription) { columns.Add("PhysicalDescription"); values.Add("@PhysicalDescription"); }
                    if (hasCallNumber) { columns.Add("CallNumber"); values.Add("@CallNumber"); }
                    if (hasCoverImagePath) { columns.Add("CoverImagePath"); values.Add("@CoverImagePath"); }
                    
                    string insertQuery = $@"INSERT INTO Books ({string.Join(", ", columns)})
                        VALUES ({string.Join(", ", values)})";
                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Title", txtTitle.Text.Trim());
                        cmd.Parameters.AddWithValue("@Author", txtAuthors.Text.Trim());
                        cmd.Parameters.AddWithValue("@ISBN", txtISBN.Text.Trim());
                        cmd.Parameters.AddWithValue("@Publisher", txtPublisher.Text.Trim());
                        cmd.Parameters.AddWithValue("@YearPublished", int.Parse(txtPublicationYear.Text));
                        cmd.Parameters.AddWithValue("@Category", cmbCategory.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Language", cmbLanguage.SelectedItem.ToString());
                        if (hasNumberOfPages) cmd.Parameters.AddWithValue("@NumberOfPages", pages);
                        cmd.Parameters.AddWithValue("@Location", txtShelfLocation.Text.Trim());
                        cmd.Parameters.AddWithValue("@Copies", copies);
                        cmd.Parameters.AddWithValue("@Available", copies);
                        cmd.Parameters.AddWithValue("@BookType", bookType);
                        string barcode = string.IsNullOrWhiteSpace(txtAccessionNumber.Text)
                            ? BarcodeGenerator.GenerateFromISBN(txtISBN.Text.Trim())
                            : BarcodeGenerator.GenerateFromAccession(txtAccessionNumber.Text.Trim());
                        cmd.Parameters.AddWithValue("@Barcode", barcode);
                        cmd.Parameters.AddWithValue("@AccessionNo", txtAccessionNumber.Text.Trim());
                        
                        // Only add parameters for columns that exist
                        if (hasSubtitle) cmd.Parameters.AddWithValue("@Subtitle", string.IsNullOrWhiteSpace(txtSubtitle.Text) ? DBNull.Value : (object)txtSubtitle.Text.Trim());
                        if (hasEditor) cmd.Parameters.AddWithValue("@Editor", string.IsNullOrWhiteSpace(txtEditors.Text) ? DBNull.Value : (object)txtEditors.Text.Trim());
                        if (hasEdition) cmd.Parameters.AddWithValue("@Edition", string.IsNullOrWhiteSpace(txtEdition.Text) ? DBNull.Value : (object)txtEdition.Text.Trim());
                        if (hasSubjectClassification) cmd.Parameters.AddWithValue("@SubjectClassification", string.IsNullOrWhiteSpace(txtSubjectClassification.Text) ? DBNull.Value : (object)txtSubjectClassification.Text.Trim());
                        if (hasPhysicalDescription) cmd.Parameters.AddWithValue("@PhysicalDescription", string.IsNullOrWhiteSpace(txtPhysicalDescription.Text) ? DBNull.Value : (object)txtPhysicalDescription.Text.Trim());
                        if (hasCallNumber) cmd.Parameters.AddWithValue("@CallNumber", string.IsNullOrWhiteSpace(txtCallNumber.Text) ? DBNull.Value : (object)txtCallNumber.Text.Trim());
                        if (hasCoverImagePath) cmd.Parameters.AddWithValue("@CoverImagePath", string.IsNullOrEmpty(bookCoverImagePath) ? DBNull.Value : (object)bookCoverImagePath);
                        
                        cmd.ExecuteNonQuery();
                    }
                    
                    // Get the newly inserted BookID
                    int newBookId = 0;
                    string getBookIdQuery = "SELECT LAST_INSERT_ID() as BookID";
                    using (MySqlCommand getIdCmd = new MySqlCommand(getBookIdQuery, conn))
                    {
                        object bookIdObj = getIdCmd.ExecuteScalar();
                        if (bookIdObj != null && bookIdObj != DBNull.Value)
                        {
                            newBookId = Convert.ToInt32(bookIdObj);
                        }
                    }
                    
                    // Create initial BookCopy records for each copy
                    if (newBookId > 0 && copies > 0)
                    {
                        var copyRepository = DependencyInjection.GetRequiredService<IBookCopyRepository>();
                        string baseAccessionNo = txtAccessionNumber.Text.Trim();
                        
                        for (int i = 1; i <= copies; i++)
                        {
                            string copyAccessionNo = copies == 1 ? baseAccessionNo : $"{baseAccessionNo}-{i:D3}";
                            var bookCopy = BookCopy.Create(newBookId, copyAccessionNo);
                            bookCopy.Location = txtShelfLocation.Text.Trim();
                            bookCopy.Barcode = BarcodeGenerator.GenerateFromAccession(copyAccessionNo);
                            copyRepository.Add(bookCopy);
                        }
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
        
        private void UpdateBook()
        {
            try
            {
                if (!bookId.HasValue) return;
                
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string bookType = radioCirculationBook.Checked ? "Circulation" : "Reference";
                    int pages = int.TryParse(txtNumberOfPages.Text, out int p) ? p : 0;
                    
                    // Get current available count to preserve it when updating total copies
                    // Check which column exists: TotalCopies or Copies
                    bool hasTotalCopies = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "TotalCopies");
                    bool hasCopies = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "Copies");
                    string copiesColumn = hasTotalCopies ? "TotalCopies" : (hasCopies ? "Copies" : "1 as TotalCopies");
                    
                    int currentAvailable = 0;
                    int currentTotalCopies = 0;
                    string getCurrentQuery = $"SELECT Available, {copiesColumn} as Copies FROM Books WHERE BookID = @BookID";
                    using (MySqlCommand getCmd = new MySqlCommand(getCurrentQuery, conn))
                    {
                        getCmd.Parameters.AddWithValue("@BookID", bookId.Value);
                        using (MySqlDataReader reader = getCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                currentAvailable = reader["Available"] != DBNull.Value ? Convert.ToInt32(reader["Available"]) : 0;
                                currentTotalCopies = reader["Copies"] != DBNull.Value ? Convert.ToInt32(reader["Copies"]) : 0;
                            }
                        }
                    }
                    
                    int newTotalCopies = int.Parse(txtNumberOfCopies.Text);
                    int newAvailable = currentAvailable;
                    
                    // If total copies increased, add to available
                    if (newTotalCopies > currentTotalCopies)
                    {
                        newAvailable = currentAvailable + (newTotalCopies - currentTotalCopies);
                    }
                    // If total copies decreased, adjust available (but don't go below 0)
                    else if (newTotalCopies < currentTotalCopies)
                    {
                        int decrease = currentTotalCopies - newTotalCopies;
                        newAvailable = Math.Max(0, currentAvailable - decrease);
                    }
                    
                    // Check which columns exist before building update query
                    bool hasSubtitle = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "Subtitle");
                    bool hasEditor = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "Editor");
                    bool hasEdition = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "Edition");
                    bool hasSubjectClassification = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "SubjectClassification");
                    bool hasPhysicalDescription = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "PhysicalDescription");
                    bool hasCallNumber = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "CallNumber");
                    bool hasCoverImagePath = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "CoverImagePath");
                    bool hasPublicationYear = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "PublicationYear");
                    bool hasYearPublished = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "YearPublished");
                    bool hasNumberOfPages = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "NumberOfPages");
                    string yearColumn = hasPublicationYear ? "PublicationYear" : (hasYearPublished ? "YearPublished" : "PublicationYear");
                    
                    // Build update query with correct column name and only include columns that exist
                    string copiesColumnName = hasTotalCopies ? "TotalCopies" : (hasCopies ? "Copies" : "TotalCopies");
                    
                    // Build SET clause dynamically based on existing columns
                    var setClauses = new List<string>
                    {
                        "Title = @Title",
                        "Author = @Author",
                        "ISBN = @ISBN",
                        "Publisher = @Publisher",
                        $"{yearColumn} = @YearPublished",
                        "Category = @Category",
                        "Language = @Language",
                        $"Location = @Location",
                        $"{copiesColumnName} = @Copies",
                        "Available = @Available",
                        "BookType = @BookType",
                        "Barcode = @Barcode"
                    };
                    
                    if (hasNumberOfPages) setClauses.Add("NumberOfPages = @NumberOfPages");
                    if (hasSubtitle) setClauses.Add("Subtitle = @Subtitle");
                    if (hasEditor) setClauses.Add("Editor = @Editor");
                    if (hasEdition) setClauses.Add("Edition = @Edition");
                    if (hasSubjectClassification) setClauses.Add("SubjectClassification = @SubjectClassification");
                    if (hasPhysicalDescription) setClauses.Add("PhysicalDescription = @PhysicalDescription");
                    if (hasCallNumber) setClauses.Add("CallNumber = @CallNumber");
                    if (hasCoverImagePath) setClauses.Add("CoverImagePath = @CoverImagePath");
                    
                    string updateQuery = $@"UPDATE Books SET {string.Join(", ", setClauses)} WHERE BookID = @BookID";
                    
                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookID", bookId.Value);
                        cmd.Parameters.AddWithValue("@Title", txtTitle.Text.Trim());
                        cmd.Parameters.AddWithValue("@Author", txtAuthors.Text.Trim());
                        cmd.Parameters.AddWithValue("@ISBN", txtISBN.Text.Trim());
                        cmd.Parameters.AddWithValue("@Publisher", txtPublisher.Text.Trim());
                        cmd.Parameters.AddWithValue("@YearPublished", int.Parse(txtPublicationYear.Text));
                        cmd.Parameters.AddWithValue("@Category", cmbCategory.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Language", cmbLanguage.SelectedItem.ToString());
                        if (hasNumberOfPages) cmd.Parameters.AddWithValue("@NumberOfPages", pages);
                        cmd.Parameters.AddWithValue("@Location", txtShelfLocation.Text.Trim());
                        cmd.Parameters.AddWithValue("@Copies", newTotalCopies);
                        cmd.Parameters.AddWithValue("@Available", newAvailable);
                        cmd.Parameters.AddWithValue("@BookType", bookType);
                        string barcode = string.IsNullOrWhiteSpace(txtAccessionNumber.Text)
                            ? BarcodeGenerator.GenerateFromISBN(txtISBN.Text.Trim())
                            : BarcodeGenerator.GenerateFromAccession(txtAccessionNumber.Text.Trim());
                        cmd.Parameters.AddWithValue("@Barcode", barcode);
                        
                        // Only add parameters for columns that exist
                        if (hasSubtitle) cmd.Parameters.AddWithValue("@Subtitle", string.IsNullOrWhiteSpace(txtSubtitle.Text) ? DBNull.Value : (object)txtSubtitle.Text.Trim());
                        if (hasEditor) cmd.Parameters.AddWithValue("@Editor", string.IsNullOrWhiteSpace(txtEditors.Text) ? DBNull.Value : (object)txtEditors.Text.Trim());
                        if (hasEdition) cmd.Parameters.AddWithValue("@Edition", string.IsNullOrWhiteSpace(txtEdition.Text) ? DBNull.Value : (object)txtEdition.Text.Trim());
                        if (hasSubjectClassification) cmd.Parameters.AddWithValue("@SubjectClassification", string.IsNullOrWhiteSpace(txtSubjectClassification.Text) ? DBNull.Value : (object)txtSubjectClassification.Text.Trim());
                        if (hasPhysicalDescription) cmd.Parameters.AddWithValue("@PhysicalDescription", string.IsNullOrWhiteSpace(txtPhysicalDescription.Text) ? DBNull.Value : (object)txtPhysicalDescription.Text.Trim());
                        if (hasCallNumber) cmd.Parameters.AddWithValue("@CallNumber", string.IsNullOrWhiteSpace(txtCallNumber.Text) ? DBNull.Value : (object)txtCallNumber.Text.Trim());
                        if (hasCoverImagePath) cmd.Parameters.AddWithValue("@CoverImagePath", string.IsNullOrEmpty(bookCoverImagePath) ? DBNull.Value : (object)bookCoverImagePath);
                        
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update book: {ex.Message}", ex);
            }
        }
        public void OpenResourceTypeForm(string resourceType)
        {
            try
            {
                if (!ValidateInputs())
                {
                    return;
                }
                SaveBook();
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
                    
                    // Update BookType based on resource type
                    string bookTypeValue = GetBookTypeForResourceType(resourceType);
                    if (!string.IsNullOrEmpty(bookTypeValue))
                    {
                        string updateQuery = "UPDATE Books SET BookType = @BookType WHERE BookID = @BookID";
                        using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn))
                        {
                            updateCmd.Parameters.AddWithValue("@BookType", bookTypeValue);
                            updateCmd.Parameters.AddWithValue("@BookID", bookId);
                            updateCmd.ExecuteNonQuery();
                        }
                    }
                }
                if (bookId == 0)
                {
                    MessageBox.Show("Error: Could not retrieve book ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
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
        }
        private void SaveThesis(Thesis thesis)
        {
        }
        private void SaveAudioVisual(AudioVisual av)
        {
        }
        private void SaveEBook(EBook ebook)
        {
        }
        
        private string GetBookTypeForResourceType(string resourceType)
        {
            if (string.IsNullOrWhiteSpace(resourceType))
                return null;
                
            string lowerType = resourceType.ToLower();
            if (lowerType.Contains("ebook") || lowerType.Contains("e-book"))
                return "E-Books";
            else if (lowerType.Contains("journal"))
                return "Journals";
            else if (lowerType.Contains("magazine"))
                return "Magazines";
            else if (lowerType.Contains("reference"))
                return "Reference Materials";
            else if (lowerType.Contains("book"))
                return "Books";
            else
                return null;
        }
    }
}