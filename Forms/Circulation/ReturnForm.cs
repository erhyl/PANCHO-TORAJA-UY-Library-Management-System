using System;
using System.Configuration;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Project5LMS.Admin_Dashboard
{
    public partial class ReturnForm : Form
    {
        private string connectionString;
        private int selectedTransactionId = 0;
        private decimal calculatedFine = 0;

        public ReturnForm()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["MySqlConnectionString"]?.ConnectionString 
                ?? "Server=localhost;Database=librarydb;Uid=root;Pwd=;";
        }

        private void ReturnForm_Load(object sender, EventArgs e)
        {
        }

        private void txtBookISBNBarcode_Enter(object sender, EventArgs e)
        {
            if (txtBookISBNBarcode.Text == "Scan or enter ISBN")
            {
                txtBookISBNBarcode.Text = "";
                txtBookISBNBarcode.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void txtBookISBNBarcode_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBookISBNBarcode.Text))
            {
                txtBookISBNBarcode.Text = "Scan or enter ISBN";
                txtBookISBNBarcode.ForeColor = System.Drawing.Color.Gray;
            }
            else
            {
                ValidateReturnBook();
            }
        }

        private void txtBookISBNBarcode_TextChanged(object sender, EventArgs e)
        {
            if (txtBookISBNBarcode.Text != "Scan or enter ISBN")
            {
                ValidateReturnBook();
            }
        }

        private void ValidateReturnBook()
        {
            selectedTransactionId = 0;
            calculatedFine = 0;
            lblFineMessage.Text = "No overdue fines detected";
            lblFineMessage.ForeColor = System.Drawing.Color.Gray;

            string searchText = txtBookISBNBarcode.Text.Trim();
            if (string.IsNullOrWhiteSpace(searchText) || searchText == "Scan or enter ISBN")
            {
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT t.TransactionID, t.BookID, t.MemberID, t.DueDate, t.BorrowDate,
                                   b.Title, b.ISBN, b.Category, b.Barcode
                                   FROM Transactions t
                                   INNER JOIN Books b ON t.BookID = b.BookID
                                   WHERE (b.ISBN LIKE @Search OR b.Title LIKE @Search OR b.Barcode LIKE @Search)
                                   AND (t.Status = 'Borrowed' OR t.Status = 'Active')
                                   ORDER BY t.TransactionID DESC
                                   LIMIT 1";
                    
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Search", "%" + searchText + "%");
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                selectedTransactionId = reader.GetInt32("TransactionID");
                                DateTime dueDate = reader["DueDate"] != DBNull.Value ? Convert.ToDateTime(reader["DueDate"]) : DateTime.MinValue;
                                string category = reader["Category"] != DBNull.Value ? reader["Category"].ToString() : "";

                                if (dueDate < DateTime.Now)
                                {
                                    int daysOverdue = (DateTime.Now - dueDate).Days;
                                    calculatedFine = CalculateFine(category, daysOverdue);
                                    
                                    if (calculatedFine > 0)
                                    {
                                        lblFineMessage.Text = $"Overdue fine: ₱{calculatedFine:F2} ({daysOverdue} day(s) overdue)";
                                        lblFineMessage.ForeColor = System.Drawing.Color.FromArgb(200, 0, 0);
                                    }
                                    else
                                    {
                                        lblFineMessage.Text = "No overdue fines detected";
                                        lblFineMessage.ForeColor = System.Drawing.Color.Gray;
                                    }
                                }
                                else
                                {
                                    lblFineMessage.Text = "No overdue fines detected";
                                    lblFineMessage.ForeColor = System.Drawing.Color.Gray;
                                }
                            }
                            else
                            {
                                lblFineMessage.Text = "No active transaction found for this book";
                                lblFineMessage.ForeColor = System.Drawing.Color.Gray;
                                selectedTransactionId = 0;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error validating return book: {ex.Message}");
                lblFineMessage.Text = "Error validating book";
                lblFineMessage.ForeColor = System.Drawing.Color.Gray;
            }
        }

        private decimal CalculateFine(string category, int daysOverdue)
        {
            decimal dailyFine = 0;
            decimal maxFine = 0;

            if (category != null && category.ToLower().Contains("reference"))
            {
                dailyFine = 10;
                maxFine = 500;
            }
            else
            {
                dailyFine = 5;
                maxFine = 300;
            }

            decimal totalFine = dailyFine * daysOverdue;
            return totalFine > maxFine ? maxFine : totalFine;
        }

        private void btnProcessReturn_Click(object sender, EventArgs e)
        {
            if (selectedTransactionId == 0)
            {
                MessageBox.Show("Please enter a valid Book ISBN or Barcode.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBookISBNBarcode.Focus();
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string getTransactionQuery = @"SELECT t.BookID, t.MemberID, t.DueDate 
                                                 FROM Transactions t 
                                                 WHERE t.TransactionID = @TransactionID";
                    int bookId = 0;
                    int memberId = 0;

                    using (MySqlCommand cmd = new MySqlCommand(getTransactionQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@TransactionID", selectedTransactionId);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                bookId = reader.GetInt32("BookID");
                                memberId = reader.GetInt32("MemberID");
                            }
                        }
                    }

                    string updateTransactionQuery = @"UPDATE Transactions 
                                                    SET Status = 'Returned', 
                                                        ReturnDate = @ReturnDate 
                                                    WHERE TransactionID = @TransactionID";
                    
                    using (MySqlCommand cmd = new MySqlCommand(updateTransactionQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@TransactionID", selectedTransactionId);
                        cmd.Parameters.AddWithValue("@ReturnDate", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }

                    string updateBookQuery = "UPDATE Books SET Available = Available + 1 WHERE BookID = @BookID";
                    using (MySqlCommand cmd = new MySqlCommand(updateBookQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookID", bookId);
                        cmd.ExecuteNonQuery();
                    }

                    if (calculatedFine > 0)
                    {
                        string insertFineQuery = @"INSERT INTO Fines 
                                                  (MemberID, TransactionID, Amount, Status, DueDate) 
                                                  VALUES 
                                                  (@MemberID, @TransactionID, @Amount, 'Pending', @DueDate)";
                        
                        using (MySqlCommand cmd = new MySqlCommand(insertFineQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@MemberID", memberId);
                            cmd.Parameters.AddWithValue("@TransactionID", selectedTransactionId);
                            cmd.Parameters.AddWithValue("@Amount", calculatedFine);
                            cmd.Parameters.AddWithValue("@DueDate", DateTime.Now.AddDays(30));
                            cmd.ExecuteNonQuery();
                        }
                    }

                    string message = calculatedFine > 0 
                        ? $"Book returned successfully!\nOverdue fine of ₱{calculatedFine:F2} has been added to the member's account."
                        : "Book returned successfully!";
                    
                    MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    txtBookISBNBarcode.Text = "Scan or enter ISBN";
                    txtBookISBNBarcode.ForeColor = System.Drawing.Color.Gray;
                    lblFineMessage.Text = "No overdue fines detected";
                    lblFineMessage.ForeColor = System.Drawing.Color.Gray;
                    selectedTransactionId = 0;
                    calculatedFine = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing return: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
