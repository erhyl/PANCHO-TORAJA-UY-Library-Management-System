using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Project5LMS.Admin_Dashboard
{
    public partial class InventoryForm : Form
    {
        private string connectionString;

        public InventoryForm()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["MySqlConnectionString"]?.ConnectionString 
                ?? "Server=localhost;Database=librarydb;Uid=root;Pwd=;";
        }

        private void InventoryForm_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadMetrics();
            LoadFilters();
            LoadInventory();
        }

        private void SetupDataGridView()
        {
            dtaGridInventory.AutoGenerateColumns = false;
            dtaGridInventory.Columns.Clear();
            dtaGridInventory.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 11F);
            dtaGridInventory.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Bold);
            dtaGridInventory.RowTemplate.Height = 40;
            dtaGridInventory.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dtaGridInventory.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            dtaGridInventory.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dtaGridInventory.EnableHeadersVisualStyles = false;
            dtaGridInventory.AllowUserToAddRows = false;
            dtaGridInventory.ReadOnly = true;
        }

        private void LoadMetrics()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Total Books
                    string queryTotal = "SELECT COUNT(*) FROM Books";
                    using (MySqlCommand cmd = new MySqlCommand(queryTotal, conn))
                    {
                        label2.Text = Convert.ToInt32(cmd.ExecuteScalar()).ToString();
                    }

                    // Available Books
                    string queryAvailable = "SELECT SUM(Available) FROM Books WHERE Available > 0";
                    using (MySqlCommand cmd = new MySqlCommand(queryAvailable, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        label4.Text = result != DBNull.Value && result != null ? Convert.ToInt32(result).ToString() : "0";
                    }

                    // Borrowed Books
                    string queryBorrowed = @"SELECT COUNT(*) FROM Transactions 
                                           WHERE Status = 'Borrowed' OR Status = 'Active'";
                    using (MySqlCommand cmd = new MySqlCommand(queryBorrowed, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        label6.Text = result != DBNull.Value && result != null ? Convert.ToInt32(result).ToString() : "0";
                    }

                    // Lost/Damaged (assuming this is books with status or a separate field)
                    // For now, using a placeholder calculation
                    label8.Text = "0";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading metrics: {ex.Message}");
                label2.Text = "0";
                label4.Text = "0";
                label6.Text = "0";
                label8.Text = "0";
            }
        }

        private void LoadFilters()
        {
            try
            {
                cmbStatus.Items.Clear();
                cmbStatus.Items.Add("All Status");
                cmbStatus.Items.Add("Available");
                cmbStatus.Items.Add("Unavailable");
                cmbStatus.SelectedIndex = 0;

                cmbCategory.Items.Clear();
                cmbCategory.Items.Add("All Categories");
                using (MySqlConnection conn = new MySqlConnection(connectionString))
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
                cmbCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading filters: {ex.Message}");
            }
        }

        private void LoadInventory()
        {
            try
            {
                dtaGridInventory.Rows.Clear();
                dtaGridInventory.Columns.Clear();

                // Add columns
                dtaGridInventory.Columns.Add("BookID", "BookID");
                dtaGridInventory.Columns["BookID"].Visible = false;
                dtaGridInventory.Columns.Add("Title", "Title");
                dtaGridInventory.Columns["Title"].Width = 300;
                dtaGridInventory.Columns.Add("Author", "Author");
                dtaGridInventory.Columns["Author"].Width = 200;
                dtaGridInventory.Columns.Add("ISBN", "ISBN");
                dtaGridInventory.Columns["ISBN"].Width = 150;
                dtaGridInventory.Columns.Add("Category", "Category");
                dtaGridInventory.Columns["Category"].Width = 150;
                dtaGridInventory.Columns.Add("TotalCopies", "Total Copies");
                dtaGridInventory.Columns["TotalCopies"].Width = 100;
                dtaGridInventory.Columns.Add("Available", "Available");
                dtaGridInventory.Columns["Available"].Width = 100;
                dtaGridInventory.Columns.Add("Borrowed", "Borrowed");
                dtaGridInventory.Columns["Borrowed"].Width = 100;

                string keyword = txtSearch.Text.Trim();
                if (keyword == "search books") keyword = "";

                string category = cmbCategory.Text == "All Categories" ? "" : cmbCategory.Text;
                string status = cmbStatus.Text == "All Status" ? "" : cmbStatus.Text;

                string query = @"SELECT 
                                    b.BookID,
                                    b.Title,
                                    b.Author,
                                    b.ISBN,
                                    b.Category,
                                    b.Copies as TotalCopies,
                                    b.Available,
                                    (b.Copies - b.Available) as Borrowed
                                FROM Books b
                                WHERE (@Keyword = '' 
                                       OR b.Title LIKE @Keyword 
                                       OR b.Author LIKE @Keyword
                                       OR b.ISBN LIKE @Keyword
                                       OR CAST(b.BookID AS CHAR) LIKE @Keyword)
                                AND (@Category = '' OR b.Category = @Category)
                                AND (@Status = '' OR 
                                     (@Status = 'Available' AND b.Available > 0) OR
                                     (@Status = 'Unavailable' AND b.Available = 0))
                                ORDER BY b.Title
                                LIMIT 500";

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                    cmd.Parameters.AddWithValue("@Category", category);
                    cmd.Parameters.AddWithValue("@Status", status);

                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int rowIndex = dtaGridInventory.Rows.Add(
                                reader["BookID"],
                                reader["Title"],
                                reader["Author"],
                                reader["ISBN"],
                                reader["Category"],
                                reader["TotalCopies"],
                                reader["Available"],
                                reader["Borrowed"]
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading inventory: {ex.Message}");
                MessageBox.Show($"Error loading inventory: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadInventory();
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadInventory();
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadInventory();
        }

        private void btnAddStock_Click(object sender, EventArgs e)
        {
            if (dtaGridInventory.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a book to add stock.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                int bookId = Convert.ToInt32(dtaGridInventory.SelectedRows[0].Cells["BookID"].Value);
                string bookTitle = dtaGridInventory.SelectedRows[0].Cells["Title"].Value.ToString();

                using (InputDialog inputDialog = new InputDialog($"Add stock for: {bookTitle}", "Enter number of copies to add:", "0"))
                {
                    if (inputDialog.ShowDialog() == DialogResult.OK)
                    {
                        if (int.TryParse(inputDialog.InputText, out int copies) && copies > 0)
                        {
                            using (MySqlConnection conn = new MySqlConnection(connectionString))
                            {
                                conn.Open();
                                string query = "UPDATE Books SET Copies = Copies + @Copies, Available = Available + @Copies WHERE BookID = @BookID";
                                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                                {
                                    cmd.Parameters.AddWithValue("@Copies", copies);
                                    cmd.Parameters.AddWithValue("@BookID", bookId);
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            MessageBox.Show($"Successfully added {copies} copies to stock.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadMetrics();
                            LoadInventory();
                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid number greater than 0.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding stock: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtaGridInventory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle cell clicks if needed
        }
    }

    // Simple input dialog for adding stock
    public class InputDialog : Form
    {
        private Label label;
        private TextBox textBox;
        private Button okButton;
        private Button cancelButton;

        public string InputText => textBox.Text;

        public InputDialog(string title, string prompt, string defaultValue = "")
        {
            this.Text = title;
            this.Size = new System.Drawing.Size(400, 150);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            label = new Label { Text = prompt, Location = new System.Drawing.Point(12, 15), AutoSize = true };
            textBox = new TextBox { Text = defaultValue, Location = new System.Drawing.Point(12, 40), Width = 360 };
            okButton = new Button { Text = "OK", DialogResult = DialogResult.OK, Location = new System.Drawing.Point(216, 75) };
            cancelButton = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Location = new System.Drawing.Point(297, 75) };

            this.Controls.Add(label);
            this.Controls.Add(textBox);
            this.Controls.Add(okButton);
            this.Controls.Add(cancelButton);
            this.AcceptButton = okButton;
            this.CancelButton = cancelButton;
        }
    }
}
