using System;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Project5LMS.Helpers;
using Project5LMS.Services;
using Project5LMS.Data;
using Project5LMS.Interfaces;
using Project5LMS.Models;
using Project5LMS.Repositories;
namespace Project5LMS.Forms.Admin.Fines
{
    public partial class AddChargeForm : Form
    {
        private readonly IMembersService _membersService;
        private readonly IBookService _bookService;
        private readonly DatabaseContext _dbContext;
        private ComboBox cmbChargeType;
        private TextBox txtMemberID;
        private TextBox txtBookAccession;
        private TextBox txtAmount;
        private TextBox txtDescription;
        private Label lblMemberName;
        private Label lblBookTitle;
        private Button btnCreate;
        private Button btnCancel;
        public AddChargeForm()
        {
            InitializeComponent();
            _dbContext = ServiceFactory.GetDbContext();
            _membersService = ServiceFactory.CreateMembersService();
            _bookService = ServiceFactory.CreateBookService();
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Text = "Add Charge";
            this.Size = new Size(500, 450);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;
            // Charge Type
            Label lblChargeType = new Label
            {
                Text = "Charge Type:",
                Location = new Point(20, 20),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            this.Controls.Add(lblChargeType);
            cmbChargeType = new ComboBox
            {
                Location = new Point(20, 45),
                Size = new Size(440, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F)
            };
            cmbChargeType.Items.Add("Lost Book");
            cmbChargeType.Items.Add("Damaged Book");
            cmbChargeType.Items.Add("Lost Card");
            cmbChargeType.SelectedIndex = 0;
            cmbChargeType.SelectedIndexChanged += CmbChargeType_SelectedIndexChanged;
            this.Controls.Add(cmbChargeType);
            // Member ID
            Label lblMemberID = new Label
            {
                Text = "Member ID (Format: MEM-000001 or 1):",
                Location = new Point(20, 90),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            this.Controls.Add(lblMemberID);
            txtMemberID = new TextBox
            {
                Location = new Point(20, 115),
                Size = new Size(440, 30),
                Font = new Font("Segoe UI", 10F),
                Text = "Format: MEM-000001 or 1",
                ForeColor = Color.Gray
            };
            txtMemberID.Enter += (s, ev) => {
                if (txtMemberID.Text == "Format: MEM-000001 or 1" || txtMemberID.Text.Contains("Format:"))
                {
                    txtMemberID.Text = "";
                    txtMemberID.ForeColor = Color.Black;
                }
            };
            txtMemberID.Leave += (s, ev) => {
                if (string.IsNullOrWhiteSpace(txtMemberID.Text))
                {
                    txtMemberID.Text = "Format: MEM-000001 or 1";
                    txtMemberID.ForeColor = Color.Gray;
                }
            };
            txtMemberID.TextChanged += TxtMemberID_TextChanged;
            this.Controls.Add(txtMemberID);
            lblMemberName = new Label
            {
                Text = "",
                Location = new Point(20, 150),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(64, 64, 64)
            };
            this.Controls.Add(lblMemberName);
            // Book Accession (for Lost/Damaged Book)
            Label lblBookAccession = new Label
            {
                Text = "Book Accession (Format: ACC-000001 or 1):",
                Location = new Point(20, 180),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Visible = true
            };
            this.Controls.Add(lblBookAccession);
            txtBookAccession = new TextBox
            {
                Location = new Point(20, 205),
                Size = new Size(440, 30),
                Font = new Font("Segoe UI", 10F),
                Text = "Format: ACC-000001 or 1",
                ForeColor = Color.Gray,
                Visible = true
            };
            txtBookAccession.Enter += (s, ev) => {
                if (txtBookAccession.Text == "Format: ACC-000001 or 1" || txtBookAccession.Text.Contains("Format:"))
                {
                    txtBookAccession.Text = "";
                    txtBookAccession.ForeColor = Color.Black;
                }
            };
            txtBookAccession.Leave += (s, ev) => {
                if (string.IsNullOrWhiteSpace(txtBookAccession.Text))
                {
                    txtBookAccession.Text = "Format: ACC-000001 or 1";
                    txtBookAccession.ForeColor = Color.Gray;
                }
            };
            txtBookAccession.TextChanged += TxtBookAccession_TextChanged;
            this.Controls.Add(txtBookAccession);
            lblBookTitle = new Label
            {
                Text = "",
                Location = new Point(20, 240),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(64, 64, 64),
                Visible = true
            };
            this.Controls.Add(lblBookTitle);
            // Amount
            Label lblAmount = new Label
            {
                Text = "Amount:",
                Location = new Point(20, 270),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            this.Controls.Add(lblAmount);
            txtAmount = new TextBox
            {
                Location = new Point(20, 295),
                Size = new Size(440, 30),
                Font = new Font("Segoe UI", 10F),
                Text = "0.00",
                ForeColor = Color.Gray
            };
            txtAmount.Enter += (s, ev) => {
                if (txtAmount.Text == "0.00")
                {
                    txtAmount.Text = "";
                    txtAmount.ForeColor = Color.Black;
                }
            };
            txtAmount.Leave += (s, ev) => {
                if (string.IsNullOrWhiteSpace(txtAmount.Text))
                {
                    txtAmount.Text = "0.00";
                    txtAmount.ForeColor = Color.Gray;
                }
            };
            this.Controls.Add(txtAmount);
            // Description
            Label lblDescription = new Label
            {
                Text = "Description (Optional):",
                Location = new Point(20, 330),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            this.Controls.Add(lblDescription);
            txtDescription = new TextBox
            {
                Location = new Point(20, 355),
                Size = new Size(440, 60),
                Multiline = true,
                Font = new Font("Segoe UI", 10F),
                ScrollBars = ScrollBars.Vertical
            };
            this.Controls.Add(txtDescription);
            // Buttons
            btnCreate = new Button
            {
                Text = "Create Charge",
                Location = new Point(280, 425),
                Size = new Size(100, 35),
                BackColor = Color.Maroon,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                DialogResult = DialogResult.OK
            };
            btnCreate.Click += BtnCreate_Click;
            this.Controls.Add(btnCreate);
            btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(390, 425),
                Size = new Size(80, 35),
                DialogResult = DialogResult.Cancel
            };
            this.Controls.Add(btnCancel);
            this.AcceptButton = btnCreate;
            this.CancelButton = btnCancel;
            this.ResumeLayout(false);
        }
        private void CmbChargeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Show/hide book accession field based on charge type
            bool showBookField = cmbChargeType.SelectedItem?.ToString() == "Lost Book" || 
                                cmbChargeType.SelectedItem?.ToString() == "Damaged Book";
            txtBookAccession.Visible = showBookField;
            lblBookTitle.Visible = showBookField;
            // Update label visibility
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Label lbl && lbl.Text.Contains("Book Accession"))
                {
                    lbl.Visible = showBookField;
                    break;
                }
            }
        }
        private void TxtMemberID_TextChanged(object sender, EventArgs e)
        {
            string memberIdText = txtMemberID.Text.Trim();
            if (string.IsNullOrWhiteSpace(memberIdText) || memberIdText.Contains("Format:"))
            {
                lblMemberName.Text = "";
                return;
            }
            try
            {
                int memberId = IDFormatter.ParseMemberID(memberIdText);
                if (memberId > 0)
                {
                    var member = _membersService.GetMember(memberId);
                    if (member != null)
                    {
                        lblMemberName.Text = $"Member: {member.FullName} ({member.Type})";
                        lblMemberName.ForeColor = Color.FromArgb(34, 139, 34);
                    }
                    else
                    {
                        lblMemberName.Text = "Member not found";
                        lblMemberName.ForeColor = Color.FromArgb(220, 20, 60);
                    }
                }
                else
                {
                    lblMemberName.Text = "";
                }
            }
            catch
            {
                lblMemberName.Text = "";
            }
        }
        private void TxtBookAccession_TextChanged(object sender, EventArgs e)
        {
            string bookAccessionText = txtBookAccession.Text.Trim();
            if (string.IsNullOrWhiteSpace(bookAccessionText) || bookAccessionText.Contains("Format:"))
            {
                lblBookTitle.Text = "";
                return;
            }
            try
            {
                var book = _bookService.GetBookByAccessionNumber(bookAccessionText);
                if (book == null)
                {
                    int bookId = IDFormatter.ParseBookID(bookAccessionText);
                    if (bookId > 0)
                    {
                        book = _bookService.GetBook(bookId);
                    }
                }
                if (book != null)
                {
                    lblBookTitle.Text = $"Book: {book.Title} by {book.Author}";
                    lblBookTitle.ForeColor = Color.FromArgb(34, 139, 34);
                }
                else
                {
                    lblBookTitle.Text = "Book not found";
                    lblBookTitle.ForeColor = Color.FromArgb(220, 20, 60);
                }
            }
            catch
            {
                lblBookTitle.Text = "";
            }
        }
        private void BtnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate inputs
                if (string.IsNullOrWhiteSpace(txtMemberID.Text) || txtMemberID.Text.Contains("Format:"))
                {
                    MessageBox.Show("Please enter a valid Member ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMemberID.Focus();
                    return;
                }
                int memberId = IDFormatter.ParseMemberID(txtMemberID.Text.Trim());
                if (memberId == 0)
                {
                    MessageBox.Show("Invalid Member ID format.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMemberID.Focus();
                    return;
                }
                var member = _membersService.GetMember(memberId);
                if (member == null)
                {
                    MessageBox.Show("Member not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string chargeType = cmbChargeType.SelectedItem?.ToString() ?? "Lost Book";
                bool requiresBook = chargeType == "Lost Book" || chargeType == "Damaged Book";
                int bookId = 0;
                if (requiresBook)
                {
                    if (string.IsNullOrWhiteSpace(txtBookAccession.Text) || txtBookAccession.Text.Contains("Format:"))
                    {
                        MessageBox.Show("Please enter a valid Book Accession Number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtBookAccession.Focus();
                        return;
                    }
                    var book = _bookService.GetBookByAccessionNumber(txtBookAccession.Text.Trim());
                    if (book == null)
                    {
                        bookId = IDFormatter.ParseBookID(txtBookAccession.Text.Trim());
                        if (bookId > 0)
                        {
                            book = _bookService.GetBook(bookId);
                        }
                    }
                    if (book == null)
                    {
                        MessageBox.Show("Book not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    bookId = book.BookID;
                }
                if (string.IsNullOrWhiteSpace(txtAmount.Text) || !decimal.TryParse(txtAmount.Text.Trim(), out decimal amount) || amount <= 0)
                {
                    MessageBox.Show("Please enter a valid amount greater than 0.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAmount.Focus();
                    return;
                }
                // Create fine record
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string insertQuery = @"INSERT INTO Fines (MemberID, BookID, TransactionID, FineType, Amount, Paid, Status, DaysOverdue, Description, CreatedDate)
                                        VALUES (@MemberID, @BookID, @TransactionID, @FineType, @Amount, 0, 'Pending', 0, @Description, @CreatedDate)";
                    using (var cmd = new MySqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", memberId);
                        cmd.Parameters.AddWithValue("@BookID", bookId > 0 ? (object)bookId : DBNull.Value);
                        cmd.Parameters.AddWithValue("@TransactionID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@FineType", chargeType);
                        cmd.Parameters.AddWithValue("@Amount", amount);
                        cmd.Parameters.AddWithValue("@Description", string.IsNullOrWhiteSpace(txtDescription.Text) ? (object)DBNull.Value : txtDescription.Text.Trim());
                        cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                }
                // Log audit trail
                try
                {
                    AuditLogger.LogCirculation("Charge Added",
                        $"Type: {chargeType}, MemberID: {memberId}, BookID: {(bookId > 0 ? bookId.ToString() : "N/A")}, Amount: {IDFormatter.FormatCurrency(amount)}",
                        "Success");
                }
                catch
                {
                    // Audit logging failed, but continue
                }
                MessageBox.Show($"{chargeType} charge of {IDFormatter.FormatCurrency(amount)} created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating charge: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
