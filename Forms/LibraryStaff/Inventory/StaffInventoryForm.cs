using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Project5LMS.Helpers;
using Project5LMS.Data;

namespace Project5LMS.Forms.LibraryStaff.Inventory
{
    public partial class StaffInventoryForm : Form
    {
        private DataTable inventoryData;
        private string currentCategoryFilter = "All";
        private readonly DatabaseContext _dbContext;

        public StaffInventoryForm()
        {
            InitializeComponent();
            _dbContext = new DatabaseContext();
        }

        private void StaffInventoryForm_Load(object sender, EventArgs e)
        {
            EnsureInventoryTableExists();
            SetupDataGridView();
            LoadCategories();
            LoadMetrics();
            LoadInventory();
        }

        private void EnsureInventoryTableExists()
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string checkTableQuery = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES 
                                              WHERE TABLE_SCHEMA = DATABASE() 
                                              AND TABLE_NAME = 'Inventory'";
                    using (var checkCmd = new MySqlCommand(checkTableQuery, conn))
                    {
                        int tableExists = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (tableExists == 0)
                        {
                            string createTableQuery = @"CREATE TABLE IF NOT EXISTS Inventory (
                                                        InventoryID INT AUTO_INCREMENT PRIMARY KEY,
                                                        BookID INT NOT NULL,
                                                        CopyNumber INT NOT NULL,
                                                        Location VARCHAR(50) NULL,
                                                        Condition VARCHAR(50) DEFAULT 'Good',
                                                        Status VARCHAR(50) DEFAULT 'Available',
                                                        LastVerified DATETIME NULL,
                                                        Notes VARCHAR(255) NULL,
                                                        FOREIGN KEY (BookID) REFERENCES Books(BookID)
                                                        )";
                            _dbContext.ExecuteNonQuery(createTableQuery);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error ensuring Inventory table exists: {ex.Message}");
            }
        }

        private void SetupDataGridView()
        {
            dataGridViewInventory.AutoGenerateColumns = false;
            dataGridViewInventory.AllowUserToAddRows = false;
            dataGridViewInventory.AllowUserToDeleteRows = false;
            dataGridViewInventory.ReadOnly = true;
            dataGridViewInventory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewInventory.MultiSelect = false;
            dataGridViewInventory.BackgroundColor = Color.White;
            dataGridViewInventory.BorderStyle = BorderStyle.None;
            dataGridViewInventory.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridViewInventory.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridViewInventory.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dataGridViewInventory.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            dataGridViewInventory.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            dataGridViewInventory.RowTemplate.Height = 40;
            dataGridViewInventory.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dataGridViewInventory.CellFormatting += DataGridViewInventory_CellFormatting;

            dataGridViewInventory.Columns.Clear();

            dataGridViewInventory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "BookID",
                HeaderText = "Book ID",
                DataPropertyName = "BookID",
                Width = 100
            });

            dataGridViewInventory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Title",
                HeaderText = "Title",
                DataPropertyName = "Title",
                Width = 250,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dataGridViewInventory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Category",
                HeaderText = "Category",
                DataPropertyName = "Category",
                Width = 120
            });

            dataGridViewInventory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Location",
                HeaderText = "Location",
                DataPropertyName = "Location",
                Width = 100
            });

            dataGridViewInventory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Total",
                HeaderText = "Total",
                DataPropertyName = "Total",
                Width = 80
            });

            dataGridViewInventory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Available",
                HeaderText = "Available",
                DataPropertyName = "Available",
                Width = 100
            });

            dataGridViewInventory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CheckedOut",
                HeaderText = "Checked Out",
                DataPropertyName = "CheckedOut",
                Width = 120
            });

            dataGridViewInventory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Damaged",
                HeaderText = "Damaged",
                DataPropertyName = "Damaged",
                Width = 100
            });

            dataGridViewInventory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Lost",
                HeaderText = "Lost",
                DataPropertyName = "Lost",
                Width = 80
            });

            dataGridViewInventory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "LastUpdated",
                HeaderText = "Last Updated",
                DataPropertyName = "LastUpdated",
                Width = 130
            });

            dataGridViewInventory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Status",
                DataPropertyName = "Status",
                Width = 120
            });
        }

        private void DataGridViewInventory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string columnName = dataGridViewInventory.Columns[e.ColumnIndex].Name;

            if (columnName == "BookID" && e.Value != null)
            {
                string bookIdStr = e.Value.ToString();
                if (int.TryParse(bookIdStr, out int bookId))
                {
                    e.Value = $"B{bookId}";
                }
                e.FormattingApplied = true;
            }

            if (columnName == "Available" && e.Value != null)
            {
                e.CellStyle.ForeColor = Color.FromArgb(40, 167, 69);
                e.FormattingApplied = true;
            }

            if (columnName == "CheckedOut" && e.Value != null)
            {
                e.CellStyle.ForeColor = Color.FromArgb(13, 110, 253);
                e.FormattingApplied = true;
            }

            if (columnName == "Status" && e.Value != null)
            {
                string status = e.Value.ToString();
                if (status == "Good" || status == "Available")
                {
                    e.Value = "? Good";
                    e.CellStyle.ForeColor = Color.FromArgb(40, 167, 69);
                }
                e.FormattingApplied = true;
            }
        }

        private void LoadCategories()
        {
            try
            {
                cmbCategoryFilter.Items.Clear();
                cmbCategoryFilter.Items.Add("All");

                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT DISTINCT Category FROM Books WHERE Category IS NOT NULL AND Category != '' ORDER BY Category";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cmbCategoryFilter.Items.Add(reader.GetString("Category"));
                            }
                        }
                    }
                }

                cmbCategoryFilter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading categories: {ex.Message}");
            }
        }

        private void LoadMetrics()
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();

                    string queryTotal = "SELECT COALESCE(SUM(Copies), 0) FROM Books";
                    using (MySqlCommand cmd = new MySqlCommand(queryTotal, conn))
                    {
                        int total = Convert.ToInt32(cmd.ExecuteScalar());
                        lblTotalCopiesValue.Text = total.ToString();
                    }

                    string queryAvailable = "SELECT COALESCE(SUM(Available), 0) FROM Books";
                    using (MySqlCommand cmd = new MySqlCommand(queryAvailable, conn))
                    {
                        int available = Convert.ToInt32(cmd.ExecuteScalar());
                        lblAvailableValue.Text = available.ToString();
                    }

                    int checkedOut = Convert.ToInt32(lblTotalCopiesValue.Text) - Convert.ToInt32(lblAvailableValue.Text);
                    lblCheckedOutValue.Text = checkedOut.ToString();

                    string queryDamaged = "SELECT COUNT(*) FROM Inventory WHERE Condition = 'Damaged'";
                    using (MySqlCommand cmd = new MySqlCommand(queryDamaged, conn))
                    {
                        int damaged = Convert.ToInt32(cmd.ExecuteScalar());
                        lblDamagedValue.Text = damaged.ToString();
                    }

                    string queryLost = "SELECT COUNT(*) FROM Inventory WHERE Status = 'Lost'";
                    using (MySqlCommand cmd = new MySqlCommand(queryLost, conn))
                    {
                        int lost = Convert.ToInt32(cmd.ExecuteScalar());
                        lblLostValue.Text = lost.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading metrics: {ex.Message}");
            }
        }

        private void LoadInventory()
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT 
                                    b.BookID,
                                    b.Title,
                                    b.Category,
                                    COALESCE(b.Location, 'N/A') as Location,
                                    b.Copies as Total,
                                    b.Available,
                                    (b.Copies - b.Available) as CheckedOut,
                                    COALESCE(damaged.DamagedCount, 0) as Damaged,
                                    COALESCE(lost.LostCount, 0) as Lost,
                                    COALESCE(MAX(i.LastVerified), b.DateAdded) as LastUpdated,
                                    CASE 
                                        WHEN COALESCE(damaged.DamagedCount, 0) = 0 AND COALESCE(lost.LostCount, 0) = 0 THEN 'Good'
                                        ELSE 'Needs Attention'
                                    END as Status
                                    FROM Books b
                                    LEFT JOIN (
                                        SELECT BookID, COUNT(*) as DamagedCount 
                                        FROM Inventory 
                                        WHERE Condition = 'Damaged' 
                                        GROUP BY BookID
                                    ) damaged ON b.BookID = damaged.BookID
                                    LEFT JOIN (
                                        SELECT BookID, COUNT(*) as LostCount 
                                        FROM Inventory 
                                        WHERE Status = 'Lost' 
                                        GROUP BY BookID
                                    ) lost ON b.BookID = lost.BookID
                                    LEFT JOIN Inventory i ON b.BookID = i.BookID";

                    if (currentCategoryFilter != "All")
                    {
                        query += " WHERE b.Category = @Category";
                    }

                    query += " GROUP BY b.BookID, b.Title, b.Category, b.Location, b.Copies, b.Available, damaged.DamagedCount, lost.LostCount, b.DateAdded";
                    query += " ORDER BY b.BookID";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (currentCategoryFilter != "All")
                        {
                            cmd.Parameters.AddWithValue("@Category", currentCategoryFilter);
                        }

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            inventoryData = new DataTable();
                            adapter.Fill(inventoryData);
                        }
                    }
                }

                dataGridViewInventory.DataSource = inventoryData;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading inventory: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Error loading inventory: {ex.Message}");
            }
        }

        private void cmbCategoryFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCategoryFilter.SelectedItem != null)
            {
                currentCategoryFilter = cmbCategoryFilter.SelectedItem.ToString();
                LoadInventory();
            }
        }

        private void txtUpdateStockBookID_Enter(object sender, EventArgs e)
        {
            if (txtUpdateStockBookID.Text == "Book ID")
            {
                txtUpdateStockBookID.Text = "";
                txtUpdateStockBookID.ForeColor = Color.Black;
            }
        }

        private void txtUpdateStockBookID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUpdateStockBookID.Text))
            {
                txtUpdateStockBookID.Text = "Book ID";
                txtUpdateStockBookID.ForeColor = Color.Gray;
            }
        }

        private void txtUpdateStockQuantity_Enter(object sender, EventArgs e)
        {
            if (txtUpdateStockQuantity.Text == "Quantity")
            {
                txtUpdateStockQuantity.Text = "";
                txtUpdateStockQuantity.ForeColor = Color.Black;
            }
        }

        private void txtUpdateStockQuantity_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUpdateStockQuantity.Text))
            {
                txtUpdateStockQuantity.Text = "Quantity";
                txtUpdateStockQuantity.ForeColor = Color.Gray;
            }
        }

        private void txtUpdateStockQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnAddCopies_Click(object sender, EventArgs e)
        {
            string bookIdText = txtUpdateStockBookID.Text.Trim();
            string quantityText = txtUpdateStockQuantity.Text.Trim();

            if (bookIdText == "Book ID" || string.IsNullOrWhiteSpace(bookIdText))
            {
                MessageBox.Show("Please enter a Book ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (quantityText == "Quantity" || string.IsNullOrWhiteSpace(quantityText))
            {
                MessageBox.Show("Please enter a Quantity.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(quantityText, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity (greater than 0).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {

                int bookId = 0;
                if (bookIdText.StartsWith("B"))
                {
                    string idPart = bookIdText.Replace("B", "");
                    int.TryParse(idPart, out bookId);
                }
                else
                {
                    int.TryParse(bookIdText, out bookId);
                }

                if (bookId == 0)
                {
                    MessageBox.Show("Invalid Book ID format.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string checkQuery = "SELECT COUNT(*) FROM Books WHERE BookID = @BookID";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@BookID", bookId);
                        int exists = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (exists == 0)
                        {
                            MessageBox.Show("Book not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string getBookQuery = "SELECT Copies, Location FROM Books WHERE BookID = @BookID";
                    int currentCopies = 0;
                    string location = "";
                    using (MySqlCommand getCmd = new MySqlCommand(getBookQuery, conn))
                    {
                        getCmd.Parameters.AddWithValue("@BookID", bookId);
                        using (MySqlDataReader reader = getCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                currentCopies = reader["Copies"] != DBNull.Value ? Convert.ToInt32(reader["Copies"]) : 0;
                                location = reader["Location"] != DBNull.Value ? reader["Location"].ToString() : "";
                            }
                        }
                    }

                    for (int i = 1; i <= quantity; i++)
                    {
                        string insertQuery = @"INSERT INTO Inventory (BookID, CopyNumber, Location, Condition, Status, LastVerified)
                                              VALUES (@BookID, @CopyNumber, @Location, 'Good', 'Available', @LastVerified)";
                        using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn))
                        {
                            insertCmd.Parameters.AddWithValue("@BookID", bookId);
                            insertCmd.Parameters.AddWithValue("@CopyNumber", currentCopies + i);
                            insertCmd.Parameters.AddWithValue("@Location", location);
                            insertCmd.Parameters.AddWithValue("@LastVerified", DateTime.Now);
                            insertCmd.ExecuteNonQuery();
                        }
                    }

                    string updateQuery = "UPDATE Books SET Copies = Copies + @Quantity, Available = Available + @Quantity WHERE BookID = @BookID";
                    using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@Quantity", quantity);
                        updateCmd.Parameters.AddWithValue("@BookID", bookId);
                        updateCmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show($"{quantity} copy/copies added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtUpdateStockBookID.Text = "Book ID";
                txtUpdateStockBookID.ForeColor = Color.Gray;
                txtUpdateStockQuantity.Text = "Quantity";
                txtUpdateStockQuantity.ForeColor = Color.Gray;

                LoadMetrics();
                LoadInventory();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding copies: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtReportDamageBookID_Enter(object sender, EventArgs e)
        {
            if (txtReportDamageBookID.Text == "Book ID")
            {
                txtReportDamageBookID.Text = "";
                txtReportDamageBookID.ForeColor = Color.Black;
            }
        }

        private void txtReportDamageBookID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtReportDamageBookID.Text))
            {
                txtReportDamageBookID.Text = "Book ID";
                txtReportDamageBookID.ForeColor = Color.Gray;
            }
        }

        private void txtReportDamageDescription_Enter(object sender, EventArgs e)
        {
            if (txtReportDamageDescription.Text == "Damage description")
            {
                txtReportDamageDescription.Text = "";
                txtReportDamageDescription.ForeColor = Color.Black;
            }
        }

        private void txtReportDamageDescription_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtReportDamageDescription.Text))
            {
                txtReportDamageDescription.Text = "Damage description";
                txtReportDamageDescription.ForeColor = Color.Gray;
            }
        }

        private void btnReportDamage_Click(object sender, EventArgs e)
        {
            string bookIdText = txtReportDamageBookID.Text.Trim();
            string description = txtReportDamageDescription.Text.Trim();

            if (bookIdText == "Book ID" || string.IsNullOrWhiteSpace(bookIdText))
            {
                MessageBox.Show("Please enter a Book ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (description == "Damage description" || string.IsNullOrWhiteSpace(description))
            {
                MessageBox.Show("Please enter a damage description.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {

                int bookId = 0;
                if (bookIdText.StartsWith("B"))
                {
                    string idPart = bookIdText.Replace("B", "");
                    int.TryParse(idPart, out bookId);
                }
                else
                {
                    int.TryParse(bookIdText, out bookId);
                }

                if (bookId == 0)
                {
                    MessageBox.Show("Invalid Book ID format.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();

                    string findQuery = @"SELECT InventoryID FROM Inventory 
                                        WHERE BookID = @BookID 
                                        AND Status = 'Available' 
                                        AND Condition = 'Good'
                                        LIMIT 1";
                    int inventoryId = 0;
                    using (MySqlCommand findCmd = new MySqlCommand(findQuery, conn))
                    {
                        findCmd.Parameters.AddWithValue("@BookID", bookId);
                        object result = findCmd.ExecuteScalar();
                        if (result != null)
                        {
                            inventoryId = Convert.ToInt32(result);
                        }
                    }

                    if (inventoryId == 0)
                    {
                        MessageBox.Show("No available copy found for this book.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string updateQuery = @"UPDATE Inventory 
                                          SET Condition = 'Damaged', 
                                              Status = 'For Repair',
                                              Notes = @Notes,
                                              LastVerified = @LastVerified
                                          WHERE InventoryID = @InventoryID";
                    using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@Notes", description);
                        updateCmd.Parameters.AddWithValue("@LastVerified", DateTime.Now);
                        updateCmd.Parameters.AddWithValue("@InventoryID", inventoryId);
                        updateCmd.ExecuteNonQuery();
                    }

                    string updateBookQuery = "UPDATE Books SET Available = Available - 1 WHERE BookID = @BookID AND Available > 0";
                    using (MySqlCommand updateBookCmd = new MySqlCommand(updateBookQuery, conn))
                    {
                        updateBookCmd.Parameters.AddWithValue("@BookID", bookId);
                        updateBookCmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Damage reported successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtReportDamageBookID.Text = "Book ID";
                txtReportDamageBookID.ForeColor = Color.Gray;
                txtReportDamageDescription.Text = "Damage description";
                txtReportDamageDescription.ForeColor = Color.Gray;

                LoadMetrics();
                LoadInventory();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reporting damage: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtReportLostBookID_Enter(object sender, EventArgs e)
        {
            if (txtReportLostBookID.Text == "Book ID")
            {
                txtReportLostBookID.Text = "";
                txtReportLostBookID.ForeColor = Color.Black;
            }
        }

        private void txtReportLostBookID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtReportLostBookID.Text))
            {
                txtReportLostBookID.Text = "Book ID";
                txtReportLostBookID.ForeColor = Color.Gray;
            }
        }

        private void txtReportLostNotes_Enter(object sender, EventArgs e)
        {
            if (txtReportLostNotes.Text == "Additional notes")
            {
                txtReportLostNotes.Text = "";
                txtReportLostNotes.ForeColor = Color.Black;
            }
        }

        private void txtReportLostNotes_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtReportLostNotes.Text))
            {
                txtReportLostNotes.Text = "Additional notes";
                txtReportLostNotes.ForeColor = Color.Gray;
            }
        }

        private void btnReportLost_Click(object sender, EventArgs e)
        {
            string bookIdText = txtReportLostBookID.Text.Trim();
            string notes = txtReportLostNotes.Text.Trim();

            if (bookIdText == "Book ID" || string.IsNullOrWhiteSpace(bookIdText))
            {
                MessageBox.Show("Please enter a Book ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (notes == "Additional notes" || string.IsNullOrWhiteSpace(notes))
            {
                notes = "Reported as lost";
            }

            try
            {

                int bookId = 0;
                if (bookIdText.StartsWith("B"))
                {
                    string idPart = bookIdText.Replace("B", "");
                    int.TryParse(idPart, out bookId);
                }
                else
                {
                    int.TryParse(bookIdText, out bookId);
                }

                if (bookId == 0)
                {
                    MessageBox.Show("Invalid Book ID format.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();

                    string findQuery = @"SELECT InventoryID FROM Inventory 
                                        WHERE BookID = @BookID 
                                        AND Status = 'Available' 
                                        AND Condition = 'Good'
                                        LIMIT 1";
                    int inventoryId = 0;
                    using (MySqlCommand findCmd = new MySqlCommand(findQuery, conn))
                    {
                        findCmd.Parameters.AddWithValue("@BookID", bookId);
                        object result = findCmd.ExecuteScalar();
                        if (result != null)
                        {
                            inventoryId = Convert.ToInt32(result);
                        }
                    }

                    if (inventoryId == 0)
                    {
                        MessageBox.Show("No available copy found for this book.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string updateQuery = @"UPDATE Inventory 
                                          SET Status = 'Lost',
                                              Notes = @Notes,
                                              LastVerified = @LastVerified
                                          WHERE InventoryID = @InventoryID";
                    using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@Notes", notes);
                        updateCmd.Parameters.AddWithValue("@LastVerified", DateTime.Now);
                        updateCmd.Parameters.AddWithValue("@InventoryID", inventoryId);
                        updateCmd.ExecuteNonQuery();
                    }

                    string updateBookQuery = @"UPDATE Books 
                                               SET Available = Available - 1, 
                                                   Copies = Copies - 1
                                               WHERE BookID = @BookID AND Available > 0 AND Copies > 0";
                    using (MySqlCommand updateBookCmd = new MySqlCommand(updateBookQuery, conn))
                    {
                        updateBookCmd.Parameters.AddWithValue("@BookID", bookId);
                        updateBookCmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Lost book reported successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtReportLostBookID.Text = "Book ID";
                txtReportLostBookID.ForeColor = Color.Gray;
                txtReportLostNotes.Text = "Additional notes";
                txtReportLostNotes.ForeColor = Color.Gray;

                LoadMetrics();
                LoadInventory();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reporting lost book: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddInventory_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Add Inventory feature - This would open a dialog to add new inventory items.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DrawBoxIcon(Graphics g, Panel panel)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int size = Math.Min(panel.Width, panel.Height) - 10;
            int x = (panel.Width - size) / 2;
            int y = (panel.Height - size) / 2;

            using (Pen pen = new Pen(Color.White, 3))
            {
                g.DrawRectangle(pen, x + size * 0.2f, y + size * 0.2f, size * 0.6f, size * 0.6f);
            }
        }

        private void DrawUpArrowIcon(Graphics g, Panel panel)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int size = Math.Min(panel.Width, panel.Height) - 10;
            int x = (panel.Width - size) / 2;
            int y = (panel.Height - size) / 2;

            using (Pen pen = new Pen(Color.White, 3))
            {
                g.DrawLine(pen, x + size * 0.5f, y + size * 0.7f, x + size * 0.5f, y + size * 0.3f);
                g.DrawLine(pen, x + size * 0.3f, y + size * 0.5f, x + size * 0.5f, y + size * 0.3f);
                g.DrawLine(pen, x + size * 0.7f, y + size * 0.5f, x + size * 0.5f, y + size * 0.3f);
            }
        }

        private void DrawWavyArrowIcon(Graphics g, Panel panel)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int size = Math.Min(panel.Width, panel.Height) - 10;
            int x = (panel.Width - size) / 2;
            int y = (panel.Height - size) / 2;

            using (Pen pen = new Pen(Color.White, 3))
            {

                PointF[] points = new PointF[]
                {
                    new PointF(x + size * 0.2f, y + size * 0.5f),
                    new PointF(x + size * 0.4f, y + size * 0.3f),
                    new PointF(x + size * 0.6f, y + size * 0.7f),
                    new PointF(x + size * 0.8f, y + size * 0.4f)
                };
                g.DrawLines(pen, points);
            }
        }

        private void DrawWarningIcon(Graphics g, Panel panel)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int size = Math.Min(panel.Width, panel.Height) - 10;
            int x = (panel.Width - size) / 2;
            int y = (panel.Height - size) / 2;

            using (Pen pen = new Pen(Color.White, 3))
            {

                PointF[] points = new PointF[]
                {
                    new PointF(x + size * 0.5f, y + size * 0.2f),
                    new PointF(x + size * 0.2f, y + size * 0.8f),
                    new PointF(x + size * 0.8f, y + size * 0.8f)
                };
                g.DrawPolygon(pen, points);

                g.DrawLine(pen, x + size * 0.5f, y + size * 0.4f, x + size * 0.5f, y + size * 0.6f);
                g.DrawLine(pen, x + size * 0.5f, y + size * 0.7f, x + size * 0.5f, y + size * 0.75f);
            }
        }

        private void lblSubtitle_Click(object sender, EventArgs e)
        {

        }

        private void lblTotalCopiesValue_Click(object sender, EventArgs e)
        {

        }

        private void panelMainContainer_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
