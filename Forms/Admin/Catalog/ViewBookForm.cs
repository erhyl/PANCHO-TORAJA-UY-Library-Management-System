using Project5LMS.Helpers;
using Project5LMS.Services;
using Project5LMS.Models;
using Project5LMS.Interfaces;
using Project5LMS.Repositories;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
namespace Project5LMS.Forms.Admin.Catalog
{
    public partial class ViewBookForm : Form
    {
        private readonly IBookService _bookService;
        private readonly IBookCopyRepository _copyRepository;
        private int bookId = 0;
        
        public ViewBookForm(int bookId)
        {
            InitializeComponent();
            _bookService = ServiceFactory.CreateBookService();
            _copyRepository = DependencyInjection.GetRequiredService<IBookCopyRepository>();
            this.bookId = bookId;
        }
        
        private void ViewBookForm_Load(object sender, EventArgs e)
        {
            StyleControls();
            SetControlsReadOnly();
            LoadBookData();
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
            
            foreach (Control control in panelFormContent.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.BackColor = Color.FromArgb(248, 249, 250);
                    textBox.BorderStyle = BorderStyle.FixedSingle;
                    textBox.ReadOnly = true;
                }
                else if (control is ComboBox comboBox)
                {
                    comboBox.BackColor = Color.FromArgb(248, 249, 250);
                    comboBox.FlatStyle = FlatStyle.Flat;
                    comboBox.Enabled = false;
                }
            }
        }
        
        private void SetControlsReadOnly()
        {
            // Make all input controls read-only - VIEW ONLY MODE
            txtTitle.ReadOnly = true;
            txtTitle.TabStop = false;
            txtAuthor.ReadOnly = true;
            txtAuthor.TabStop = false;
            txtISBN.ReadOnly = true;
            txtISBN.TabStop = false;
            txtCategory.ReadOnly = true;
            txtCategory.TabStop = false;
            txtPublisher.ReadOnly = true;
            txtPublisher.TabStop = false;
            txtYear.ReadOnly = true;
            txtYear.TabStop = false;
            txtAccessionNo.ReadOnly = true;
            txtAccessionNo.TabStop = false;
            txtLocation.ReadOnly = true;
            txtLocation.TabStop = false;
            txtTotalCopies.ReadOnly = true;
            txtTotalCopies.TabStop = false;
            txtAvailable.ReadOnly = true;
            txtAvailable.TabStop = false;
            txtStatus.ReadOnly = true;
            txtStatus.TabStop = false;
            
            // Optional fields
            if (txtSubtitle != null)
            {
                txtSubtitle.ReadOnly = true;
                txtSubtitle.TabStop = false;
            }
            if (txtEditor != null)
            {
                txtEditor.ReadOnly = true;
                txtEditor.TabStop = false;
            }
            if (txtEdition != null)
            {
                txtEdition.ReadOnly = true;
                txtEdition.TabStop = false;
            }
            if (txtLanguage != null)
            {
                txtLanguage.ReadOnly = true;
                txtLanguage.TabStop = false;
            }
            if (txtPages != null)
            {
                txtPages.ReadOnly = true;
                txtPages.TabStop = false;
            }
            if (txtCallNumber != null)
            {
                txtCallNumber.ReadOnly = true;
                txtCallNumber.TabStop = false;
            }
            if (txtBarcode != null)
            {
                txtBarcode.ReadOnly = true;
                txtBarcode.TabStop = false;
            }
            if (txtPhysicalDescription != null)
            {
                txtPhysicalDescription.ReadOnly = true;
                txtPhysicalDescription.TabStop = false;
            }
        }
        
        private void LoadBookData()
        {
            try
            {
                var book = _bookService.GetBook(bookId);
                
                if (book == null)
                {
                    MessageBox.Show("Book not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
                
                // Basic Information
                txtTitle.Text = book.Title ?? "N/A";
                txtAuthor.Text = book.Author ?? "N/A";
                txtISBN.Text = book.ISBN ?? "N/A";
                txtCategory.Text = book.Category ?? "N/A";
                txtPublisher.Text = book.Publisher ?? "N/A";
                txtYear.Text = book.PublicationYear > 0 ? book.PublicationYear.ToString() : "N/A";
                txtAccessionNo.Text = book.AccessionNo ?? "N/A";
                txtLocation.Text = book.Location ?? "N/A";
                
                // Calculate actual copies from BookCopies table for accuracy
                try
                {
                    var copies = _copyRepository.GetByBookId(bookId).ToList();
                    int actualTotalCopies = copies.Count;
                    int actualAvailable = copies.Count(c => c.IsAvailable);
                    txtTotalCopies.Text = actualTotalCopies.ToString();
                    txtAvailable.Text = actualAvailable.ToString();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error getting copy counts: {ex.Message}");
                    // Fallback to Books table values
                    txtTotalCopies.Text = book.TotalCopies.ToString();
                    txtAvailable.Text = book.Available.ToString();
                }
                
                txtStatus.Text = book.Status ?? "Available";
                
                // Optional fields
                if (txtSubtitle != null)
                    txtSubtitle.Text = book.Subtitle ?? "";
                if (txtEditor != null)
                    txtEditor.Text = book.Editor ?? "";
                if (txtEdition != null)
                    txtEdition.Text = book.Edition ?? "";
                if (txtLanguage != null)
                    txtLanguage.Text = book.Language ?? "";
                if (txtPages != null)
                    txtPages.Text = book.NumberOfPages > 0 ? book.NumberOfPages.ToString() : "";
                if (txtCallNumber != null)
                    txtCallNumber.Text = book.CallNumber ?? "";
                if (txtBarcode != null)
                    txtBarcode.Text = book.Barcode ?? "";
                if (txtPhysicalDescription != null)
                    txtPhysicalDescription.Text = book.PhysicalDescription ?? "";
                
                // Load book cover image
                System.Diagnostics.Debug.WriteLine($"Loading book cover image. Path: {book.CoverImagePath ?? "null"}");
                LoadBookCoverImage(book.CoverImagePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading book data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"LoadBookData error: {ex}");
            }
        }
        
        private void LoadBookCoverImage(string coverImagePath)
        {
            try
            {
                if (picBookCover != null)
                {
                    // Dispose of previous image to prevent memory leaks
                    if (picBookCover.Image != null)
                    {
                        var oldImage = picBookCover.Image;
                        picBookCover.Image = null;
                        oldImage.Dispose();
                    }
                    
                    if (!string.IsNullOrEmpty(coverImagePath) && File.Exists(coverImagePath))
                    {
                        try
                        {
                            picBookCover.Image = Image.FromFile(coverImagePath);
                            picBookCover.SizeMode = PictureBoxSizeMode.Zoom;
                            picBookCover.Visible = true;
                            if (lblNoCoverImage != null)
                                lblNoCoverImage.Visible = false;
                            System.Diagnostics.Debug.WriteLine($"Book cover image loaded successfully: {coverImagePath}");
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"Error loading cover image: {ex.Message}");
                            ShowNoCoverImage();
                        }
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"Cover image path is empty or file does not exist: {coverImagePath ?? "null"}");
                        ShowNoCoverImage();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in LoadBookCoverImage: {ex.Message}");
                ShowNoCoverImage();
            }
        }
        
        private void ShowNoCoverImage()
        {
            if (picBookCover != null)
            {
                picBookCover.Image = null;
                picBookCover.Visible = false;
            }
            if (lblNoCoverImage != null)
            {
                lblNoCoverImage.Visible = true;
                lblNoCoverImage.Text = "No Cover Image";
            }
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
