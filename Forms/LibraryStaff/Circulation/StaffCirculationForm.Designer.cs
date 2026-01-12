namespace Project5LMS.Forms.LibraryStaff.Circulation
{
    partial class StaffCirculationForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.panelMainContainer = new System.Windows.Forms.Panel();
            this.panelTransactionHistory = new System.Windows.Forms.Panel();
            this.panelTransactionsList = new System.Windows.Forms.Panel();
            this.flowLayoutTransactions = new System.Windows.Forms.FlowLayoutPanel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabAll = new System.Windows.Forms.TabPage();
            this.tabCheckouts = new System.Windows.Forms.TabPage();
            this.tabReturns = new System.Windows.Forms.TabPage();
            this.lblTransactionHistory = new System.Windows.Forms.Label();
            this.panelCirculationManagement = new System.Windows.Forms.Panel();
            this.panelReturnBook = new System.Windows.Forms.Panel();
            this.btnProcessReturn = new System.Windows.Forms.Button();
            this.lblReturnDate = new System.Windows.Forms.Label();
            this.txtReturnDate = new System.Windows.Forms.Label();
            this.txtReturnBookID = new System.Windows.Forms.TextBox();
            this.lblReturnBookID = new System.Windows.Forms.Label();
            this.lblReturnBookTitle = new System.Windows.Forms.Label();
            this.panelCheckOutBook = new System.Windows.Forms.Panel();
            this.btnProcessCheckOut = new System.Windows.Forms.Button();
            this.txtCheckOutBookID = new System.Windows.Forms.TextBox();
            this.lblCheckOutBookID = new System.Windows.Forms.Label();
            this.txtCheckOutMemberID = new System.Windows.Forms.TextBox();
            this.lblCheckOutMemberID = new System.Windows.Forms.Label();
            this.lblCheckOutBookTitle = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panelMainContainer.SuspendLayout();
            this.panelTransactionHistory.SuspendLayout();
            this.panelTransactionsList.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.panelCirculationManagement.SuspendLayout();
            this.panelReturnBook.SuspendLayout();
            this.panelCheckOutBook.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMainContainer
            // 
            this.panelMainContainer.AutoScroll = true;
            this.panelMainContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelMainContainer.Controls.Add(this.panelTransactionHistory);
            this.panelMainContainer.Controls.Add(this.panelCirculationManagement);
            this.panelMainContainer.Controls.Add(this.panelHeader);
            this.panelMainContainer.Location = new System.Drawing.Point(0, 0);
            this.panelMainContainer.Name = "panelMainContainer";
            this.panelMainContainer.Padding = new System.Windows.Forms.Padding(24);
            this.panelMainContainer.Size = new System.Drawing.Size(1259, 800);
            this.panelMainContainer.TabIndex = 0;
            this.panelMainContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMainContainer_Paint);
            // 
            // panelTransactionHistory
            // 
            this.panelTransactionHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTransactionHistory.Controls.Add(this.panelTransactionsList);
            this.panelTransactionHistory.Controls.Add(this.tabControl);
            this.panelTransactionHistory.Controls.Add(this.lblTransactionHistory);
            this.panelTransactionHistory.Location = new System.Drawing.Point(24, 370);
            this.panelTransactionHistory.Name = "panelTransactionHistory";
            this.panelTransactionHistory.Size = new System.Drawing.Size(1211, 325);
            this.panelTransactionHistory.TabIndex = 2;
            this.panelTransactionHistory.Paint += new System.Windows.Forms.PaintEventHandler(this.panelTransactionHistory_Paint);
            // 
            // panelTransactionsList
            // 
            this.panelTransactionsList.AutoScroll = true;
            this.panelTransactionsList.Controls.Add(this.flowLayoutTransactions);
            this.panelTransactionsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTransactionsList.Location = new System.Drawing.Point(0, 0);
            this.panelTransactionsList.Name = "panelTransactionsList";
            this.panelTransactionsList.Size = new System.Drawing.Size(1211, 325);
            this.panelTransactionsList.TabIndex = 2;
            this.panelTransactionsList.Paint += new System.Windows.Forms.PaintEventHandler(this.panelTransactionsList_Paint);
            // 
            // flowLayoutTransactions
            // 
            this.flowLayoutTransactions.AutoScroll = true;
            this.flowLayoutTransactions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutTransactions.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutTransactions.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutTransactions.Name = "flowLayoutTransactions";
            this.flowLayoutTransactions.Padding = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this.flowLayoutTransactions.Size = new System.Drawing.Size(1211, 325);
            this.flowLayoutTransactions.TabIndex = 0;
            this.flowLayoutTransactions.WrapContents = false;
            this.flowLayoutTransactions.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutTransactions_Paint);
            // 
            // tabControl
            // 
            this.tabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl.Controls.Add(this.tabAll);
            this.tabControl.Controls.Add(this.tabCheckouts);
            this.tabControl.Controls.Add(this.tabReturns);
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl.ItemSize = new System.Drawing.Size(180, 40);
            this.tabControl.Location = new System.Drawing.Point(0, 49);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1152, 32);
            this.tabControl.TabIndex = 1;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabAll
            // 
            this.tabAll.Location = new System.Drawing.Point(4, 44);
            this.tabAll.Name = "tabAll";
            this.tabAll.Padding = new System.Windows.Forms.Padding(3);
            this.tabAll.Size = new System.Drawing.Size(1144, 0);
            this.tabAll.TabIndex = 0;
            this.tabAll.Text = "All Transactions";
            this.tabAll.UseVisualStyleBackColor = true;
            this.tabAll.Click += new System.EventHandler(this.tabAll_Click);
            // 
            // tabCheckouts
            // 
            this.tabCheckouts.Location = new System.Drawing.Point(4, 44);
            this.tabCheckouts.Name = "tabCheckouts";
            this.tabCheckouts.Padding = new System.Windows.Forms.Padding(3);
            this.tabCheckouts.Size = new System.Drawing.Size(1144, 0);
            this.tabCheckouts.TabIndex = 1;
            this.tabCheckouts.Text = "Checkouts";
            this.tabCheckouts.UseVisualStyleBackColor = true;
            this.tabCheckouts.Click += new System.EventHandler(this.tabCheckouts_Click);
            // 
            // tabReturns
            // 
            this.tabReturns.Location = new System.Drawing.Point(4, 44);
            this.tabReturns.Name = "tabReturns";
            this.tabReturns.Padding = new System.Windows.Forms.Padding(3);
            this.tabReturns.Size = new System.Drawing.Size(1144, 0);
            this.tabReturns.TabIndex = 2;
            this.tabReturns.Text = "Returns";
            this.tabReturns.UseVisualStyleBackColor = true;
            this.tabReturns.Click += new System.EventHandler(this.tabReturns_Click);
            // 
            // lblTransactionHistory
            // 
            this.lblTransactionHistory.AutoSize = true;
            this.lblTransactionHistory.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransactionHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTransactionHistory.Location = new System.Drawing.Point(0, 0);
            this.lblTransactionHistory.Name = "lblTransactionHistory";
            this.lblTransactionHistory.Size = new System.Drawing.Size(237, 32);
            this.lblTransactionHistory.TabIndex = 0;
            this.lblTransactionHistory.Text = "Transaction History";
            this.lblTransactionHistory.Click += new System.EventHandler(this.lblTransactionHistory_Click);
            // 
            // panelCirculationManagement
            // 
            this.panelCirculationManagement.Controls.Add(this.label1);
            this.panelCirculationManagement.Controls.Add(this.label2);
            this.panelCirculationManagement.Controls.Add(this.panelReturnBook);
            this.panelCirculationManagement.Controls.Add(this.panelCheckOutBook);
            this.panelCirculationManagement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCirculationManagement.Location = new System.Drawing.Point(24, 24);
            this.panelCirculationManagement.Margin = new System.Windows.Forms.Padding(0);
            this.panelCirculationManagement.Name = "panelCirculationManagement";
            this.panelCirculationManagement.Size = new System.Drawing.Size(1211, 752);
            this.panelCirculationManagement.TabIndex = 1;
            this.panelCirculationManagement.Paint += new System.Windows.Forms.PaintEventHandler(this.panelCirculationManagement_Paint);
            this.panelCirculationManagement.Resize += new System.EventHandler(this.panelCirculationManagement_Resize);
            // 
            // panelReturnBook
            // 
            this.panelReturnBook.BackColor = System.Drawing.Color.White;
            this.panelReturnBook.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelReturnBook.Controls.Add(this.btnProcessReturn);
            this.panelReturnBook.Controls.Add(this.lblReturnDate);
            this.panelReturnBook.Controls.Add(this.txtReturnDate);
            this.panelReturnBook.Controls.Add(this.txtReturnBookID);
            this.panelReturnBook.Controls.Add(this.lblReturnBookID);
            this.panelReturnBook.Controls.Add(this.lblReturnBookTitle);
            this.panelReturnBook.Location = new System.Drawing.Point(609, 99);
            this.panelReturnBook.Margin = new System.Windows.Forms.Padding(0);
            this.panelReturnBook.Name = "panelReturnBook";
            this.panelReturnBook.Padding = new System.Windows.Forms.Padding(22, 24, 22, 24);
            this.panelReturnBook.Size = new System.Drawing.Size(602, 225);
            this.panelReturnBook.TabIndex = 3;
            this.panelReturnBook.Paint += new System.Windows.Forms.PaintEventHandler(this.panelReturnBook_Paint);
            // 
            // btnProcessReturn
            // 
            this.btnProcessReturn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnProcessReturn.BackColor = System.Drawing.Color.Maroon;
            this.btnProcessReturn.FlatAppearance.BorderSize = 0;
            this.btnProcessReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcessReturn.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProcessReturn.ForeColor = System.Drawing.Color.White;
            this.btnProcessReturn.Location = new System.Drawing.Point(25, 161);
            this.btnProcessReturn.Name = "btnProcessReturn";
            this.btnProcessReturn.Size = new System.Drawing.Size(534, 38);
            this.btnProcessReturn.TabIndex = 6;
            this.btnProcessReturn.Text = "Process Return";
            this.btnProcessReturn.UseVisualStyleBackColor = false;
            this.btnProcessReturn.Click += new System.EventHandler(this.btnProcessReturn_Click);
            // 
            // lblReturnDate
            // 
            this.lblReturnDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblReturnDate.AutoSize = true;
            this.lblReturnDate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReturnDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblReturnDate.Location = new System.Drawing.Point(25, 84);
            this.lblReturnDate.Name = "lblReturnDate";
            this.lblReturnDate.Size = new System.Drawing.Size(83, 19);
            this.lblReturnDate.TabIndex = 5;
            this.lblReturnDate.Text = "Return Date";
            this.lblReturnDate.Click += new System.EventHandler(this.lblReturnDate_Click);
            // 
            // txtReturnDate
            // 
            this.txtReturnDate.AutoSize = true;
            this.txtReturnDate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReturnDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtReturnDate.Location = new System.Drawing.Point(345, 347);
            this.txtReturnDate.Name = "txtReturnDate";
            this.txtReturnDate.Size = new System.Drawing.Size(0, 19);
            this.txtReturnDate.TabIndex = 4;
            this.txtReturnDate.Click += new System.EventHandler(this.txtReturnDate_Click);
            // 
            // txtReturnBookID
            // 
            this.txtReturnBookID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtReturnBookID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReturnBookID.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReturnBookID.ForeColor = System.Drawing.Color.Gray;
            this.txtReturnBookID.Location = new System.Drawing.Point(25, 52);
            this.txtReturnBookID.Name = "txtReturnBookID";
            this.txtReturnBookID.Size = new System.Drawing.Size(531, 25);
            this.txtReturnBookID.TabIndex = 3;
            this.txtReturnBookID.Text = "Scan or enter book ID";
            this.txtReturnBookID.TextChanged += new System.EventHandler(this.txtReturnBookID_TextChanged);
            this.txtReturnBookID.Enter += new System.EventHandler(this.txtReturnBookID_Enter);
            this.txtReturnBookID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReturnBookID_KeyDown);
            this.txtReturnBookID.Leave += new System.EventHandler(this.txtReturnBookID_Leave);
            // 
            // lblReturnBookID
            // 
            this.lblReturnBookID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblReturnBookID.AutoSize = true;
            this.lblReturnBookID.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReturnBookID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblReturnBookID.Location = new System.Drawing.Point(21, 29);
            this.lblReturnBookID.Name = "lblReturnBookID";
            this.lblReturnBookID.Size = new System.Drawing.Size(58, 19);
            this.lblReturnBookID.TabIndex = 2;
            this.lblReturnBookID.Text = "Book ID";
            this.lblReturnBookID.Click += new System.EventHandler(this.lblReturnBookID_Click);
            // 
            // lblReturnBookTitle
            // 
            this.lblReturnBookTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblReturnBookTitle.AutoSize = true;
            this.lblReturnBookTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReturnBookTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblReturnBookTitle.Location = new System.Drawing.Point(21, 6);
            this.lblReturnBookTitle.Name = "lblReturnBookTitle";
            this.lblReturnBookTitle.Size = new System.Drawing.Size(104, 21);
            this.lblReturnBookTitle.TabIndex = 0;
            this.lblReturnBookTitle.Text = "Return Book";
            this.lblReturnBookTitle.Click += new System.EventHandler(this.lblReturnBookTitle_Click);
            // 
            // panelCheckOutBook
            // 
            this.panelCheckOutBook.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelCheckOutBook.BackColor = System.Drawing.Color.White;
            this.panelCheckOutBook.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCheckOutBook.Controls.Add(this.btnProcessCheckOut);
            this.panelCheckOutBook.Controls.Add(this.txtCheckOutBookID);
            this.panelCheckOutBook.Controls.Add(this.lblCheckOutBookID);
            this.panelCheckOutBook.Controls.Add(this.txtCheckOutMemberID);
            this.panelCheckOutBook.Controls.Add(this.lblCheckOutMemberID);
            this.panelCheckOutBook.Controls.Add(this.lblCheckOutBookTitle);
            this.panelCheckOutBook.Location = new System.Drawing.Point(0, 99);
            this.panelCheckOutBook.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.panelCheckOutBook.Name = "panelCheckOutBook";
            this.panelCheckOutBook.Padding = new System.Windows.Forms.Padding(22, 24, 22, 24);
            this.panelCheckOutBook.Size = new System.Drawing.Size(601, 225);
            this.panelCheckOutBook.TabIndex = 2;
            this.panelCheckOutBook.Paint += new System.Windows.Forms.PaintEventHandler(this.panelCheckOutBook_Paint);
            // 
            // btnProcessCheckOut
            // 
            this.btnProcessCheckOut.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnProcessCheckOut.BackColor = System.Drawing.Color.Maroon;
            this.btnProcessCheckOut.FlatAppearance.BorderSize = 0;
            this.btnProcessCheckOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcessCheckOut.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProcessCheckOut.ForeColor = System.Drawing.Color.White;
            this.btnProcessCheckOut.Location = new System.Drawing.Point(30, 161);
            this.btnProcessCheckOut.Name = "btnProcessCheckOut";
            this.btnProcessCheckOut.Size = new System.Drawing.Size(525, 38);
            this.btnProcessCheckOut.TabIndex = 6;
            this.btnProcessCheckOut.Text = "Process Check Out";
            this.btnProcessCheckOut.UseVisualStyleBackColor = false;
            this.btnProcessCheckOut.Click += new System.EventHandler(this.btnProcessCheckOut_Click);
            // 
            // txtCheckOutBookID
            // 
            this.txtCheckOutBookID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtCheckOutBookID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCheckOutBookID.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCheckOutBookID.ForeColor = System.Drawing.Color.Gray;
            this.txtCheckOutBookID.Location = new System.Drawing.Point(30, 107);
            this.txtCheckOutBookID.Name = "txtCheckOutBookID";
            this.txtCheckOutBookID.Size = new System.Drawing.Size(526, 25);
            this.txtCheckOutBookID.TabIndex = 5;
            this.txtCheckOutBookID.Text = "Enter book ID (e.g., B1001)";
            this.txtCheckOutBookID.TextChanged += new System.EventHandler(this.txtCheckOutBookID_TextChanged);
            this.txtCheckOutBookID.Enter += new System.EventHandler(this.txtCheckOutBookID_Enter);
            this.txtCheckOutBookID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCheckOutBookID_KeyDown);
            this.txtCheckOutBookID.Leave += new System.EventHandler(this.txtCheckOutBookID_Leave);
            // 
            // lblCheckOutBookID
            // 
            this.lblCheckOutBookID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCheckOutBookID.AutoSize = true;
            this.lblCheckOutBookID.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckOutBookID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCheckOutBookID.Location = new System.Drawing.Point(30, 84);
            this.lblCheckOutBookID.Name = "lblCheckOutBookID";
            this.lblCheckOutBookID.Size = new System.Drawing.Size(58, 19);
            this.lblCheckOutBookID.TabIndex = 4;
            this.lblCheckOutBookID.Text = "Book ID";
            this.lblCheckOutBookID.Click += new System.EventHandler(this.lblCheckOutBookID_Click);
            // 
            // txtCheckOutMemberID
            // 
            this.txtCheckOutMemberID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtCheckOutMemberID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCheckOutMemberID.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCheckOutMemberID.ForeColor = System.Drawing.Color.Gray;
            this.txtCheckOutMemberID.Location = new System.Drawing.Point(30, 52);
            this.txtCheckOutMemberID.Name = "txtCheckOutMemberID";
            this.txtCheckOutMemberID.Size = new System.Drawing.Size(526, 25);
            this.txtCheckOutMemberID.TabIndex = 3;
            this.txtCheckOutMemberID.Text = "Enter member ID (e.g., M1001)";
            this.txtCheckOutMemberID.TextChanged += new System.EventHandler(this.txtCheckOutMemberID_TextChanged);
            this.txtCheckOutMemberID.Enter += new System.EventHandler(this.txtCheckOutMemberID_Enter);
            this.txtCheckOutMemberID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCheckOutMemberID_KeyDown);
            this.txtCheckOutMemberID.Leave += new System.EventHandler(this.txtCheckOutMemberID_Leave);
            // 
            // lblCheckOutMemberID
            // 
            this.lblCheckOutMemberID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCheckOutMemberID.AutoSize = true;
            this.lblCheckOutMemberID.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckOutMemberID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCheckOutMemberID.Location = new System.Drawing.Point(30, 29);
            this.lblCheckOutMemberID.Name = "lblCheckOutMemberID";
            this.lblCheckOutMemberID.Size = new System.Drawing.Size(79, 19);
            this.lblCheckOutMemberID.TabIndex = 2;
            this.lblCheckOutMemberID.Text = "Member ID";
            this.lblCheckOutMemberID.Click += new System.EventHandler(this.lblCheckOutMemberID_Click);
            // 
            // lblCheckOutBookTitle
            // 
            this.lblCheckOutBookTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCheckOutBookTitle.AutoSize = true;
            this.lblCheckOutBookTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckOutBookTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCheckOutBookTitle.Location = new System.Drawing.Point(30, 6);
            this.lblCheckOutBookTitle.Name = "lblCheckOutBookTitle";
            this.lblCheckOutBookTitle.Size = new System.Drawing.Size(131, 21);
            this.lblCheckOutBookTitle.TabIndex = 0;
            this.lblCheckOutBookTitle.Text = "Check Out Book";
            this.lblCheckOutBookTitle.Click += new System.EventHandler(this.lblCheckOutBookTitle_Click);
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Controls.Add(this.lblSubtitle);
            this.panelHeader.Location = new System.Drawing.Point(24, 24);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1211, 75);
            this.panelHeader.TabIndex = 0;
            this.panelHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.panelHeader_Paint);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitle.Location = new System.Drawing.Point(3, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(389, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Circulation Management";
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click);
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblSubtitle.Location = new System.Drawing.Point(14, 44);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(234, 19);
            this.lblSubtitle.TabIndex = 0;
            this.lblSubtitle.Text = "Manage book checkouts and returns";
            this.lblSubtitle.Click += new System.EventHandler(this.lblSubtitle_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(16, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(389, 45);
            this.label1.TabIndex = 4;
            this.label1.Text = "Circulation Management";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.label2.Location = new System.Drawing.Point(27, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(234, 19);
            this.label2.TabIndex = 5;
            this.label2.Text = "Manage book checkouts and returns";
            // 
            // StaffCirculationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1259, 800);
            this.Controls.Add(this.panelMainContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "StaffCirculationForm";
            this.Text = "Circulation";
            this.Load += new System.EventHandler(this.StaffCirculationForm_Load);
            this.panelMainContainer.ResumeLayout(false);
            this.panelTransactionHistory.ResumeLayout(false);
            this.panelTransactionHistory.PerformLayout();
            this.panelTransactionsList.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.panelCirculationManagement.ResumeLayout(false);
            this.panelCirculationManagement.PerformLayout();
            this.panelReturnBook.ResumeLayout(false);
            this.panelReturnBook.PerformLayout();
            this.panelCheckOutBook.ResumeLayout(false);
            this.panelCheckOutBook.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.Panel panelMainContainer;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelCirculationManagement;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel panelCheckOutBook;
        private System.Windows.Forms.Label lblCheckOutBookTitle;
        private System.Windows.Forms.TextBox txtCheckOutMemberID;
        private System.Windows.Forms.Label lblCheckOutMemberID;
        private System.Windows.Forms.TextBox txtCheckOutBookID;
        private System.Windows.Forms.Label lblCheckOutBookID;
        private System.Windows.Forms.Button btnProcessCheckOut;
        private System.Windows.Forms.Panel panelReturnBook;
        private System.Windows.Forms.Label lblReturnBookTitle;
        private System.Windows.Forms.TextBox txtReturnBookID;
        private System.Windows.Forms.Label lblReturnBookID;
        private System.Windows.Forms.Label txtReturnDate;
        private System.Windows.Forms.Label lblReturnDate;
        private System.Windows.Forms.Button btnProcessReturn;
        private System.Windows.Forms.Panel panelTransactionHistory;
        private System.Windows.Forms.Label lblTransactionHistory;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabAll;
        private System.Windows.Forms.TabPage tabCheckouts;
        private System.Windows.Forms.TabPage tabReturns;
        private System.Windows.Forms.Panel panelTransactionsList;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutTransactions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}