using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Project5LMS.Data;
using Project5LMS.Models;
using Project5LMS.Repositories;
using Project5LMS.Services;
using Project5LMS.Interfaces;
using Project5LMS.Helpers;
namespace Project5LMS.Forms.Admin.Catalog
{
    public partial class ManageBookCopiesForm : Form
    {
        private readonly DatabaseContext _dbContext;
        private readonly IBookCopyRepository _copyRepository;
        private readonly IBookService _bookService;
        private int bookId;
        private Book currentBook;
        
        public ManageBookCopiesForm(int bookId)
        {
            InitializeComponent();
            _dbContext = ServiceFactory.GetDbContext();
            _copyRepository = DependencyInjection.GetRequiredService<IBookCopyRepository>();
            _bookService = ServiceFactory.CreateBookService();
            this.bookId = bookId;
        }
        
        private void ManageBookCopiesForm_Load(object sender, EventArgs e)
        {
            try
            {
                StyleControls();
                LoadBookInfo();
                LoadCopies();
                SetupDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"ManageBookCopiesForm_Load error: {ex}");
            }
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
        
        private void LoadBookInfo()
        {
            try
            {
                currentBook = _bookService.GetBook(bookId);
                if (currentBook == null)
                {
                    MessageBox.Show("Book not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
                
                // Calculate actual copies from BookCopies table for accurate display
                var copies = _copyRepository.GetByBookId(bookId).ToList();
                int actualTotalCopies = copies.Count;
                int actualAvailableCopies = copies.Count(c => c.IsAvailable);
                
                lblBookTitle.Text = currentBook.Title ?? "N/A";
                lblBookAuthor.Text = $"Author: {currentBook.Author ?? "N/A"}";
                lblBookISBN.Text = $"ISBN: {currentBook.ISBN ?? "N/A"}";
                lblTotalCopies.Text = $"Total Copies: {actualTotalCopies}";
                lblAvailableCopies.Text = $"Available: {actualAvailableCopies}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading book info: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void LoadCopies()
        {
            try
            {
                var copies = _copyRepository.GetByBookId(bookId).ToList();
                
                DataTable dt = new DataTable();
                dt.Columns.Add("CopyID", typeof(int));
                dt.Columns.Add("AccessionNumber", typeof(string));
                dt.Columns.Add("Barcode", typeof(string));
                dt.Columns.Add("CopyStatus", typeof(string));
                dt.Columns.Add("Location", typeof(string));
                dt.Columns.Add("Notes", typeof(string));
                // DataSet doesn't support nullable types, use DateTime and handle nulls with DBNull.Value
                dt.Columns.Add("LastCheckedOut", typeof(DateTime));
                dt.Columns.Add("LastReturned", typeof(DateTime));
                
                foreach (var copy in copies)
                {
                    dt.Rows.Add(
                        copy.CopyID,
                        copy.AccessionNumber ?? "",
                        copy.Barcode ?? "",
                        copy.CopyStatus ?? "Available",
                        copy.Location ?? "",
                        copy.Notes ?? "",
                        copy.LastCheckedOut.HasValue ? (object)copy.LastCheckedOut.Value : DBNull.Value,
                        copy.LastReturned.HasValue ? (object)copy.LastReturned.Value : DBNull.Value
                    );
                }
                
                dataGridViewCopies.DataSource = dt;
                
                // Update summary
                int availableCount = copies.Count(c => c.IsAvailable);
                int borrowedCount = copies.Count(c => c.IsBorrowed);
                int reservedCount = copies.Count(c => c.IsReserved);
                int lostCount = copies.Count(c => c.IsLost);
                int damagedCount = copies.Count(c => c.IsDamaged);
                int forRepairCount = copies.Count(c => c.IsForRepair);
                
                lblSummary.Text = $"Summary: {availableCount} Available, {borrowedCount} Borrowed, {reservedCount} Reserved, {lostCount} Lost, {damagedCount} Damaged, {forRepairCount} For Repair";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading copies: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"LoadCopies error: {ex}");
            }
        }
        
        private void SetupDataGridView()
        {
            dataGridViewCopies.AutoGenerateColumns = false;
            dataGridViewCopies.ReadOnly = false;
            dataGridViewCopies.AllowUserToAddRows = false;
            dataGridViewCopies.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewCopies.MultiSelect = false;
            
            // Configure columns if not already set
            if (dataGridViewCopies.Columns.Count == 0)
            {
                dataGridViewCopies.AutoGenerateColumns = true;
            }
            
            // Make certain columns read-only
            foreach (DataGridViewColumn col in dataGridViewCopies.Columns)
            {
                if (col.Name == "CopyID" || col.Name == "AccessionNumber" || col.Name == "LastCheckedOut" || col.Name == "LastReturned")
                {
                    col.ReadOnly = true;
                    col.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
                }
            }
        }
        
        private void btnAddCopy_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the book's accession number automatically
                string accessionNumber = GetBookAccessionNumber();
                if (string.IsNullOrWhiteSpace(accessionNumber))
                {
                    MessageBox.Show("Cannot determine accession number for this book. Please ensure the book has an accession number or existing copies.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                // Only ask for location (optional) - locations can be the same or different per copy
                string location = ShowInputDialog($"Enter location for new copy:\n\nAccession Number: {accessionNumber} (automatically assigned)\n\nNote: Shelf locations can be the same or different per copy to reflect real library storage practices.", "Add New Copy", currentBook?.Location ?? "");
                if (string.IsNullOrWhiteSpace(location))
                {
                    location = currentBook?.Location ?? "";
                }
                
                var newCopy = BookCopy.Create(bookId, accessionNumber);
                newCopy.Location = location;
                newCopy.Barcode = BarcodeGenerator.GenerateFromAccession(accessionNumber);
                
                if (_copyRepository.Add(newCopy))
                {
                    // Update book total copies count
                    UpdateBookCopyCount();
                    MessageBox.Show($"Copy added successfully.\n\nAccession Number: {accessionNumber}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCopies();
                    LoadBookInfo();
                    // Set dialog result to OK so parent form can refresh
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Failed to add copy. Please check the database connection and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += $"\n\nDetails: {ex.InnerException.Message}";
                }
                MessageBox.Show($"Error adding copy: {errorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private string GetBookAccessionNumber()
        {
            // First, try to get from the book's AccessionNo property
            if (!string.IsNullOrWhiteSpace(currentBook?.AccessionNo))
            {
                return currentBook.AccessionNo;
            }
            
            // If book doesn't have AccessionNo, get from existing copies
            var existingCopies = _copyRepository.GetByBookId(bookId).ToList();
            if (existingCopies.Count > 0)
            {
                // Get the first copy's accession number (all copies should have the same)
                var firstCopy = existingCopies.FirstOrDefault(c => !string.IsNullOrWhiteSpace(c.AccessionNumber));
                if (firstCopy != null)
                {
                    return firstCopy.AccessionNumber;
                }
            }
            
            return null;
        }
        
        private string GetBookAccessionNumberFormat()
        {
            // First, try to get format from the book's AccessionNo
            if (!string.IsNullOrWhiteSpace(currentBook?.AccessionNo))
            {
                return ExtractAccessionNumberFormat(currentBook.AccessionNo);
            }
            
            // If book doesn't have AccessionNo, check existing copies
            var existingCopies = _copyRepository.GetByBookId(bookId).ToList();
            if (existingCopies.Count > 0)
            {
                // Get the first copy's accession number to determine format
                var firstCopy = existingCopies.FirstOrDefault(c => !string.IsNullOrWhiteSpace(c.AccessionNumber));
                if (firstCopy != null)
                {
                    return ExtractAccessionNumberFormat(firstCopy.AccessionNumber);
                }
            }
            
            return null;
        }
        
        private string ExtractAccessionNumberFormat(string accessionNumber)
        {
            if (string.IsNullOrWhiteSpace(accessionNumber))
                return null;
            
            // Extract the prefix part (e.g., "ACC-" from "ACC-0008")
            // Find the last occurrence of a dash or separator
            int lastDashIndex = accessionNumber.LastIndexOf('-');
            if (lastDashIndex > 0)
            {
                string prefix = accessionNumber.Substring(0, lastDashIndex + 1);
                // Count digits in the number part to determine format
                string numberPart = accessionNumber.Substring(lastDashIndex + 1);
                int digitCount = numberPart.Length;
                string format = prefix + new string('#', digitCount);
                return format;
            }
            
            // If no dash, check if it starts with a prefix followed by digits
            Regex regex = new Regex(@"^([A-Za-z]+)(\d+)$");
            var match = regex.Match(accessionNumber);
            if (match.Success)
            {
                string prefix = match.Groups[1].Value;
                int digitCount = match.Groups[2].Value.Length;
                return prefix + new string('#', digitCount);
            }
            
            // Return the original as fallback
            return accessionNumber;
        }
        
        private bool ValidateAccessionNumberFormat(string accessionNumber, string expectedFormat)
        {
            if (string.IsNullOrWhiteSpace(accessionNumber) || string.IsNullOrWhiteSpace(expectedFormat))
                return false;
            
            // Extract prefix from expected format
            int lastDashIndex = expectedFormat.LastIndexOf('-');
            if (lastDashIndex > 0)
            {
                string expectedPrefix = expectedFormat.Substring(0, lastDashIndex + 1);
                // Check if the accession number starts with the same prefix
                if (!accessionNumber.StartsWith(expectedPrefix, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
                
                // Check if the number part has the same length
                string expectedNumberPart = expectedFormat.Substring(lastDashIndex + 1);
                string actualNumberPart = accessionNumber.Substring(lastDashIndex + 1);
                
                if (expectedNumberPart.Length != actualNumberPart.Length)
                {
                    return false;
                }
                
                // Check if the number part is all digits
                return Regex.IsMatch(actualNumberPart, @"^\d+$");
            }
            
            // Handle format without dash (e.g., "ACC####")
            Regex formatRegex = new Regex(@"^([A-Za-z]+)(#+)$");
            var formatMatch = formatRegex.Match(expectedFormat);
            if (formatMatch.Success)
            {
                string expectedPrefix = formatMatch.Groups[1].Value;
                int expectedDigitCount = formatMatch.Groups[2].Value.Length;
                
                if (!accessionNumber.StartsWith(expectedPrefix, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
                
                string actualNumberPart = accessionNumber.Substring(expectedPrefix.Length);
                if (actualNumberPart.Length != expectedDigitCount)
                {
                    return false;
                }
                
                return Regex.IsMatch(actualNumberPart, @"^\d+$");
            }
            
            // If format doesn't match expected patterns, do exact match
            return accessionNumber.Equals(expectedFormat, StringComparison.OrdinalIgnoreCase);
        }
        
        private void UpdateBookCopyCount()
        {
            try
            {
                var copies = _copyRepository.GetByBookId(bookId).ToList();
                int totalCopies = copies.Count;
                int availableCopies = copies.Count(c => c.IsAvailable);
                
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    // Check which column exists: TotalCopies or Copies
                    bool hasTotalCopies = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "TotalCopies");
                    bool hasCopies = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "Copies");
                    string copiesColumnName = hasTotalCopies ? "TotalCopies" : (hasCopies ? "Copies" : "TotalCopies");
                    
                    string updateQuery = $"UPDATE Books SET {copiesColumnName} = @Copies, Available = @Available WHERE BookID = @BookID";
                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Copies", totalCopies);
                        cmd.Parameters.AddWithValue("@Available", availableCopies);
                        cmd.Parameters.AddWithValue("@BookID", bookId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating book copy count: {ex.Message}");
            }
        }
        
        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewCopies.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a copy to update.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                
                DataGridViewRow selectedRow = dataGridViewCopies.SelectedRows[0];
                int copyId = Convert.ToInt32(selectedRow.Cells["CopyID"].Value);
                string currentStatus = selectedRow.Cells["CopyStatus"].Value?.ToString() ?? "Available";
                
                // Show status selection dialog
                Form statusForm = new Form()
                {
                    Width = 300,
                    Height = 200,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = "Update Copy Status",
                    StartPosition = FormStartPosition.CenterParent,
                    MaximizeBox = false,
                    MinimizeBox = false
                };
                
                Label label = new Label() { Left = 20, Top = 20, Width = 250, Text = "Select new status:" };
                ComboBox cmbStatus = new ComboBox() { Left = 20, Top = 50, Width = 250 };
                cmbStatus.Items.AddRange(new string[] { "Available", "Borrowed", "Reserved", "Lost", "Damaged", "For Repair" });
                cmbStatus.SelectedItem = currentStatus;
                
                Button okButton = new Button() { Text = "OK", Left = 150, Width = 75, Top = 100, DialogResult = DialogResult.OK };
                Button cancelButton = new Button() { Text = "Cancel", Left = 230, Width = 75, Top = 100, DialogResult = DialogResult.Cancel };
                
                okButton.Click += (s, ev) => { statusForm.Close(); };
                cancelButton.Click += (s, ev) => { statusForm.Close(); };
                
                statusForm.Controls.Add(label);
                statusForm.Controls.Add(cmbStatus);
                statusForm.Controls.Add(okButton);
                statusForm.Controls.Add(cancelButton);
                statusForm.AcceptButton = okButton;
                statusForm.CancelButton = cancelButton;
                
                if (statusForm.ShowDialog() == DialogResult.OK)
                {
                    string newStatus = cmbStatus.SelectedItem?.ToString() ?? currentStatus;
                    if (_copyRepository.UpdateStatus(copyId, newStatus))
                    {
                        UpdateBookCopyCount();
                        MessageBox.Show("Status updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadCopies();
                        LoadBookInfo();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update status.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating status: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnUpdateLocation_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewCopies.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a copy to update.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                
                DataGridViewRow selectedRow = dataGridViewCopies.SelectedRows[0];
                int copyId = Convert.ToInt32(selectedRow.Cells["CopyID"].Value);
                string currentLocation = selectedRow.Cells["Location"].Value?.ToString() ?? "";
                
                string newLocation = ShowInputDialog("Enter new location:", "Update Location", currentLocation);
                
                if (!string.IsNullOrEmpty(newLocation) && newLocation != currentLocation)
                {
                    var copy = _copyRepository.GetById(copyId);
                    if (copy != null)
                    {
                        copy.Location = newLocation;
                        if (_copyRepository.Update(copy))
                        {
                            // Update the book's location in Books table to reflect the change
                            UpdateBookLocation(newLocation);
                            
                            MessageBox.Show("Location updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadCopies();
                            LoadBookInfo();
                            // Set dialog result to OK so parent form can refresh
                            this.DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            MessageBox.Show("Failed to update location.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating location: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void UpdateBookLocation(string newLocation)
        {
            try
            {
                if (currentBook == null) return;
                
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string updateQuery = "UPDATE Books SET Location = @Location WHERE BookID = @BookID";
                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Location", newLocation);
                        cmd.Parameters.AddWithValue("@BookID", bookId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating book location: {ex.Message}");
            }
        }
        
        private void btnDeleteCopy_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewCopies.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a copy to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                
                DataGridViewRow selectedRow = dataGridViewCopies.SelectedRows[0];
                int copyId = Convert.ToInt32(selectedRow.Cells["CopyID"].Value);
                string accessionNumber = selectedRow.Cells["AccessionNumber"].Value?.ToString() ?? "";
                
                var result = MessageBox.Show(
                    $"Are you sure you want to delete copy \"{accessionNumber}\"?\n\nThis action cannot be undone.",
                    "Delete Copy",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );
                
                if (result == DialogResult.Yes)
                {
                    if (_copyRepository.Delete(copyId))
                    {
                        // Update book total copies count in database
                        UpdateBookCopyCount();
                        MessageBox.Show("Copy deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadCopies();
                        LoadBookInfo();
                        // Set dialog result to OK so parent form can refresh
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete copy.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting copy: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void dataGridViewCopies_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = dataGridViewCopies.Rows[e.RowIndex];
                int copyId = Convert.ToInt32(row.Cells["CopyID"].Value);
                var copy = _copyRepository.GetById(copyId);
                
                if (copy != null)
                {
                    string columnName = dataGridViewCopies.Columns[e.ColumnIndex].Name;
                    
                    if (columnName == "CopyStatus")
                    {
                        copy.CopyStatus = row.Cells["CopyStatus"].Value?.ToString() ?? "Available";
                    }
                    else if (columnName == "Location")
                    {
                        string newLocation = row.Cells["Location"].Value?.ToString() ?? "";
                        copy.Location = newLocation;
                        // Also update the book's location in Books table
                        if (!string.IsNullOrEmpty(newLocation))
                        {
                            UpdateBookLocation(newLocation);
                        }
                    }
                    else if (columnName == "Notes")
                    {
                        copy.Notes = row.Cells["Notes"].Value?.ToString() ?? "";
                    }
                    
                    if (_copyRepository.Update(copy))
                    {
                        LoadBookInfo();
                        // Set dialog result to OK so parent form can refresh
                        this.DialogResult = DialogResult.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating copy: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private string ShowInputDialog(string text, string caption, string defaultValue = "")
        {
            Form prompt = new Form()
            {
                Width = 400,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false
            };
            
            Label textLabel = new Label() { Left = 20, Top = 20, Width = 350, Text = text };
            TextBox textBox = new TextBox() { Left = 20, Top = 50, Width = 350, Text = defaultValue };
            Button confirmation = new Button() { Text = "OK", Left = 250, Width = 75, Top = 80, DialogResult = DialogResult.OK };
            Button cancel = new Button() { Text = "Cancel", Left = 330, Width = 75, Top = 80, DialogResult = DialogResult.Cancel };
            
            confirmation.Click += (sender, e) => { prompt.Close(); };
            cancel.Click += (sender, e) => { prompt.Close(); };
            
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(cancel);
            prompt.AcceptButton = confirmation;
            prompt.CancelButton = cancel;
            
            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : defaultValue;
        }
    }
}
