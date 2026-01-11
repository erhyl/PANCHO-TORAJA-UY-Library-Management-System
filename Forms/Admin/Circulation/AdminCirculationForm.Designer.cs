namespace Project5LMS.Forms.Admin.Circulation
{
    partial class AdminCirculationForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelMainContainer = new System.Windows.Forms.Panel();
            this.panelTableContainer = new System.Windows.Forms.Panel();
            this.lblRecentTransactions = new System.Windows.Forms.Label();
            this.dataGridViewTransactions = new System.Windows.Forms.DataGridView();
            this.colTransactionID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMember = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBook = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBorrowDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDueDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelActionTabs = new System.Windows.Forms.Panel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabBorrow = new System.Windows.Forms.TabPage();
            this.panelBorrowContent = new System.Windows.Forms.Panel();
            this.btnProcessBorrowing = new System.Windows.Forms.Button();
            this.lblBorrowBookAccession = new System.Windows.Forms.Label();
            this.txtBorrowBookAccession = new System.Windows.Forms.TextBox();
            this.lblBorrowMemberID = new System.Windows.Forms.Label();
            this.txtBorrowMemberID = new System.Windows.Forms.TextBox();
            this.tabReturn = new System.Windows.Forms.TabPage();
            this.panelReturnContent = new System.Windows.Forms.Panel();
            this.btnProcessReturn = new System.Windows.Forms.Button();
            this.lblReturnBookAccession = new System.Windows.Forms.Label();
            this.txtReturnBookAccession = new System.Windows.Forms.TextBox();
            this.tabHistory = new System.Windows.Forms.TabPage();
            this.panelMetrics = new System.Windows.Forms.Panel();
            this.panelMetricTotalTransactions = new System.Windows.Forms.Panel();
            this.lblMetricTotalTransactionsValue = new System.Windows.Forms.Label();
            this.lblMetricTotalTransactionsTitle = new System.Windows.Forms.Label();
            this.panelMetricTodayActivity = new System.Windows.Forms.Panel();
            this.lblMetricTodayActivityValue = new System.Windows.Forms.Label();
            this.lblMetricTodayActivityTitle = new System.Windows.Forms.Label();
            this.panelMetricCurrentlyBorrowed = new System.Windows.Forms.Panel();
            this.lblMetricCurrentlyBorrowedValue = new System.Windows.Forms.Label();
            this.lblMetricCurrentlyBorrowedTitle = new System.Windows.Forms.Label();
            this.panelMetricOverdue = new System.Windows.Forms.Panel();
            this.lblMetricOverdueValue = new System.Windows.Forms.Label();
            this.lblMetricOverdueTitle = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelMainContainer.SuspendLayout();
            this.panelTableContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTransactions)).BeginInit();
            this.panelActionTabs.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabBorrow.SuspendLayout();
            this.panelBorrowContent.SuspendLayout();
            this.tabReturn.SuspendLayout();
            this.panelReturnContent.SuspendLayout();
            this.panelMetrics.SuspendLayout();
            this.panelMetricTotalTransactions.SuspendLayout();
            this.panelMetricTodayActivity.SuspendLayout();
            this.panelMetricCurrentlyBorrowed.SuspendLayout();
            this.panelMetricOverdue.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMainContainer
            // 
            this.panelMainContainer.AutoScroll = true;
            this.panelMainContainer.BackColor = System.Drawing.Color.Transparent;
            this.panelMainContainer.Controls.Add(this.panelTableContainer);
            this.panelMainContainer.Controls.Add(this.panelActionTabs);
            this.panelMainContainer.Controls.Add(this.panelMetrics);
            this.panelMainContainer.Controls.Add(this.panelHeader);
            this.panelMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainContainer.Location = new System.Drawing.Point(0, 0);
            this.panelMainContainer.Name = "panelMainContainer";
            this.panelMainContainer.Padding = new System.Windows.Forms.Padding(24);
            this.panelMainContainer.Size = new System.Drawing.Size(1200, 800);
            this.panelMainContainer.TabIndex = 0;
            // 
            // panelTableContainer
            // 
            this.panelTableContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTableContainer.BackColor = System.Drawing.Color.White;
            this.panelTableContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTableContainer.Controls.Add(this.lblRecentTransactions);
            this.panelTableContainer.Controls.Add(this.dataGridViewTransactions);
            this.panelTableContainer.Location = new System.Drawing.Point(24, 472);
            this.panelTableContainer.Name = "panelTableContainer";
            this.panelTableContainer.Padding = new System.Windows.Forms.Padding(16);
            this.panelTableContainer.Size = new System.Drawing.Size(1016, 355);
            this.panelTableContainer.TabIndex = 3;
            this.panelTableContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.panelTableContainer_Paint);
            // 
            // lblRecentTransactions
            // 
            this.lblRecentTransactions.AutoSize = true;
            this.lblRecentTransactions.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecentTransactions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblRecentTransactions.Location = new System.Drawing.Point(16, -1);
            this.lblRecentTransactions.Name = "lblRecentTransactions";
            this.lblRecentTransactions.Size = new System.Drawing.Size(130, 17);
            this.lblRecentTransactions.TabIndex = 1;
            this.lblRecentTransactions.Text = "Recent Transactions";
            // 
            // dataGridViewTransactions
            // 
            this.dataGridViewTransactions.AllowUserToOrderColumns = true;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.dataGridViewTransactions.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTransactions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewTransactions.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewTransactions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTransactions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTransactions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTransactionID,
            this.colType,
            this.colMember,
            this.colBook,
            this.colBorrowDate,
            this.colDueDate,
            this.colStatus,
            this.colFine});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTransactions.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTransactions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewTransactions.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dataGridViewTransactions.Location = new System.Drawing.Point(16, 16);
            this.dataGridViewTransactions.Margin = new System.Windows.Forms.Padding(16, 0, 16, 16);
            this.dataGridViewTransactions.MultiSelect = false;
            this.dataGridViewTransactions.Name = "dataGridViewTransactions";
            this.dataGridViewTransactions.RowHeadersVisible = false;
            this.dataGridViewTransactions.RowHeadersWidth = 51;
            this.dataGridViewTransactions.RowTemplate.Height = 60;
            this.dataGridViewTransactions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewTransactions.Size = new System.Drawing.Size(982, 321);
            this.dataGridViewTransactions.TabIndex = 0;
            // 
            // colTransactionID
            // 
            this.colTransactionID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colTransactionID.DataPropertyName = "TransactionID";
            this.colTransactionID.HeaderText = "TRANSACTION ID";
            this.colTransactionID.MinimumWidth = 6;
            this.colTransactionID.Name = "colTransactionID";
            this.colTransactionID.ReadOnly = true;
            this.colTransactionID.Width = 120;
            // 
            // colType
            // 
            this.colType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colType.DataPropertyName = "Type";
            this.colType.HeaderText = "TYPE";
            this.colType.MinimumWidth = 6;
            this.colType.Name = "colType";
            this.colType.Width = 120;
            // 
            // colMember
            // 
            this.colMember.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colMember.DataPropertyName = "Member";
            this.colMember.HeaderText = "MEMBER";
            this.colMember.MinimumWidth = 6;
            this.colMember.Name = "colMember";
            this.colMember.Width = 200;
            // 
            // colBook
            // 
            this.colBook.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colBook.DataPropertyName = "Book";
            this.colBook.HeaderText = "BOOK";
            this.colBook.MinimumWidth = 6;
            this.colBook.Name = "colBook";
            this.colBook.Width = 250;
            // 
            // colBorrowDate
            // 
            this.colBorrowDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colBorrowDate.DataPropertyName = "BorrowDate";
            this.colBorrowDate.HeaderText = "BORROW DATE";
            this.colBorrowDate.MinimumWidth = 6;
            this.colBorrowDate.Name = "colBorrowDate";
            this.colBorrowDate.Width = 120;
            // 
            // colDueDate
            // 
            this.colDueDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colDueDate.DataPropertyName = "DueDate";
            this.colDueDate.HeaderText = "DUE DATE";
            this.colDueDate.MinimumWidth = 6;
            this.colDueDate.Name = "colDueDate";
            this.colDueDate.Width = 120;
            // 
            // colStatus
            // 
            this.colStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colStatus.DataPropertyName = "Status";
            this.colStatus.HeaderText = "STATUS";
            this.colStatus.MinimumWidth = 6;
            this.colStatus.Name = "colStatus";
            this.colStatus.Width = 120;
            // 
            // colFine
            // 
            this.colFine.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colFine.DataPropertyName = "Fine";
            this.colFine.FillWeight = 20F;
            this.colFine.HeaderText = "FINE";
            this.colFine.MinimumWidth = 6;
            this.colFine.Name = "colFine";
            // 
            // panelActionTabs
            // 
            this.panelActionTabs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelActionTabs.BackColor = System.Drawing.Color.White;
            this.panelActionTabs.Controls.Add(this.tabControl);
            this.panelActionTabs.Location = new System.Drawing.Point(24, 209);
            this.panelActionTabs.Name = "panelActionTabs";
            this.panelActionTabs.Padding = new System.Windows.Forms.Padding(16);
            this.panelActionTabs.Size = new System.Drawing.Size(1005, 260);
            this.panelActionTabs.TabIndex = 2;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabBorrow);
            this.tabControl.Controls.Add(this.tabReturn);
            this.tabControl.Controls.Add(this.tabHistory);
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl.Location = new System.Drawing.Point(16, 16);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1157, 228);
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabBorrow
            // 
            this.tabBorrow.BackColor = System.Drawing.Color.White;
            this.tabBorrow.Controls.Add(this.panelBorrowContent);
            this.tabBorrow.Location = new System.Drawing.Point(4, 26);
            this.tabBorrow.Name = "tabBorrow";
            this.tabBorrow.Padding = new System.Windows.Forms.Padding(3);
            this.tabBorrow.Size = new System.Drawing.Size(1025, 198);
            this.tabBorrow.TabIndex = 0;
            this.tabBorrow.Text = "Borrow Book";
            // 
            // panelBorrowContent
            // 
            this.panelBorrowContent.Controls.Add(this.btnProcessBorrowing);
            this.panelBorrowContent.Controls.Add(this.lblBorrowBookAccession);
            this.panelBorrowContent.Controls.Add(this.txtBorrowBookAccession);
            this.panelBorrowContent.Controls.Add(this.lblBorrowMemberID);
            this.panelBorrowContent.Controls.Add(this.txtBorrowMemberID);
            this.panelBorrowContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBorrowContent.Location = new System.Drawing.Point(3, 3);
            this.panelBorrowContent.Name = "panelBorrowContent";
            this.panelBorrowContent.Padding = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.panelBorrowContent.Size = new System.Drawing.Size(1019, 192);
            this.panelBorrowContent.TabIndex = 0;
            // 
            // btnProcessBorrowing
            // 
            this.btnProcessBorrowing.BackColor = System.Drawing.Color.Maroon;
            this.btnProcessBorrowing.FlatAppearance.BorderSize = 0;
            this.btnProcessBorrowing.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcessBorrowing.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProcessBorrowing.ForeColor = System.Drawing.Color.White;
            this.btnProcessBorrowing.Location = new System.Drawing.Point(8, 138);
            this.btnProcessBorrowing.Name = "btnProcessBorrowing";
            this.btnProcessBorrowing.Size = new System.Drawing.Size(1118, 45);
            this.btnProcessBorrowing.TabIndex = 4;
            this.btnProcessBorrowing.Text = "Process Borrowing";
            this.btnProcessBorrowing.UseVisualStyleBackColor = false;
            this.btnProcessBorrowing.Click += new System.EventHandler(this.btnProcessBorrowing_Click);
            // 
            // lblBorrowBookAccession
            // 
            this.lblBorrowBookAccession.AutoSize = true;
            this.lblBorrowBookAccession.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBorrowBookAccession.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblBorrowBookAccession.Location = new System.Drawing.Point(6, 72);
            this.lblBorrowBookAccession.Name = "lblBorrowBookAccession";
            this.lblBorrowBookAccession.Size = new System.Drawing.Size(161, 19);
            this.lblBorrowBookAccession.TabIndex = 3;
            this.lblBorrowBookAccession.Text = "Book Accession Number";
            this.lblBorrowBookAccession.Click += new System.EventHandler(this.lblBorrowBookAccession_Click);
            // 
            // txtBorrowBookAccession
            // 
            this.txtBorrowBookAccession.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBorrowBookAccession.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBorrowBookAccession.ForeColor = System.Drawing.Color.Gray;
            this.txtBorrowBookAccession.Location = new System.Drawing.Point(9, 94);
            this.txtBorrowBookAccession.Name = "txtBorrowBookAccession";
            this.txtBorrowBookAccession.Size = new System.Drawing.Size(1118, 25);
            this.txtBorrowBookAccession.TabIndex = 2;
            this.txtBorrowBookAccession.Text = "Scan or enter book accession number...";
            this.txtBorrowBookAccession.Enter += new System.EventHandler(this.txtBorrowBookAccession_Enter);
            this.txtBorrowBookAccession.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBorrowBookAccession_KeyDown);
            this.txtBorrowBookAccession.Leave += new System.EventHandler(this.txtBorrowBookAccession_Leave);
            // 
            // lblBorrowMemberID
            // 
            this.lblBorrowMemberID.AutoSize = true;
            this.lblBorrowMemberID.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBorrowMemberID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblBorrowMemberID.Location = new System.Drawing.Point(5, 9);
            this.lblBorrowMemberID.Name = "lblBorrowMemberID";
            this.lblBorrowMemberID.Size = new System.Drawing.Size(79, 19);
            this.lblBorrowMemberID.TabIndex = 1;
            this.lblBorrowMemberID.Text = "Member ID";
            // 
            // txtBorrowMemberID
            // 
            this.txtBorrowMemberID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBorrowMemberID.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBorrowMemberID.ForeColor = System.Drawing.Color.Gray;
            this.txtBorrowMemberID.Location = new System.Drawing.Point(9, 31);
            this.txtBorrowMemberID.Name = "txtBorrowMemberID";
            this.txtBorrowMemberID.Size = new System.Drawing.Size(1118, 25);
            this.txtBorrowMemberID.TabIndex = 0;
            this.txtBorrowMemberID.Text = "Scan or enter member ID...";
            this.txtBorrowMemberID.TextChanged += new System.EventHandler(this.txtBorrowMemberID_TextChanged);
            this.txtBorrowMemberID.Enter += new System.EventHandler(this.txtBorrowMemberID_Enter);
            this.txtBorrowMemberID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBorrowMemberID_KeyDown);
            this.txtBorrowMemberID.Leave += new System.EventHandler(this.txtBorrowMemberID_Leave);
            // 
            // tabReturn
            // 
            this.tabReturn.BackColor = System.Drawing.Color.White;
            this.tabReturn.Controls.Add(this.panelReturnContent);
            this.tabReturn.Location = new System.Drawing.Point(4, 26);
            this.tabReturn.Name = "tabReturn";
            this.tabReturn.Padding = new System.Windows.Forms.Padding(3);
            this.tabReturn.Size = new System.Drawing.Size(1149, 198);
            this.tabReturn.TabIndex = 1;
            this.tabReturn.Text = "Return Book";
            // 
            // panelReturnContent
            // 
            this.panelReturnContent.Controls.Add(this.btnProcessReturn);
            this.panelReturnContent.Controls.Add(this.txtReturnBookAccession);
            this.panelReturnContent.Controls.Add(this.lblReturnBookAccession);
            this.panelReturnContent.Location = new System.Drawing.Point(4, 3);
            this.panelReturnContent.Name = "panelReturnContent";
            this.panelReturnContent.Padding = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.panelReturnContent.Size = new System.Drawing.Size(1140, 192);
            this.panelReturnContent.TabIndex = 1;
            // 
            // btnProcessReturn
            // 
            this.btnProcessReturn.BackColor = System.Drawing.Color.Maroon;
            this.btnProcessReturn.FlatAppearance.BorderSize = 0;
            this.btnProcessReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcessReturn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProcessReturn.ForeColor = System.Drawing.Color.White;
            this.btnProcessReturn.Location = new System.Drawing.Point(8, 103);
            this.btnProcessReturn.Name = "btnProcessReturn";
            this.btnProcessReturn.Size = new System.Drawing.Size(1118, 45);
            this.btnProcessReturn.TabIndex = 2;
            this.btnProcessReturn.Text = "Process Return";
            this.btnProcessReturn.UseVisualStyleBackColor = false;
            this.btnProcessReturn.Click += new System.EventHandler(this.btnProcessReturn_Click);
            // 
            // lblReturnBookAccession
            // 
            this.lblReturnBookAccession.AutoSize = true;
            this.lblReturnBookAccession.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReturnBookAccession.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblReturnBookAccession.Location = new System.Drawing.Point(6, 31);
            this.lblReturnBookAccession.Name = "lblReturnBookAccession";
            this.lblReturnBookAccession.Size = new System.Drawing.Size(161, 19);
            this.lblReturnBookAccession.TabIndex = 1;
            this.lblReturnBookAccession.Text = "Book Accession Number";
            // 
            // txtReturnBookAccession
            // 
            this.txtReturnBookAccession.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReturnBookAccession.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReturnBookAccession.ForeColor = System.Drawing.Color.Gray;
            this.txtReturnBookAccession.Location = new System.Drawing.Point(8, 53);
            this.txtReturnBookAccession.Name = "txtReturnBookAccession";
            this.txtReturnBookAccession.Size = new System.Drawing.Size(1118, 25);
            this.txtReturnBookAccession.TabIndex = 0;
            this.txtReturnBookAccession.Text = "Scan or enter book accession number...";
            this.txtReturnBookAccession.Enter += new System.EventHandler(this.txtReturnBookAccession_Enter);
            this.txtReturnBookAccession.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReturnBookAccession_KeyDown);
            this.txtReturnBookAccession.Leave += new System.EventHandler(this.txtReturnBookAccession_Leave);
            // 
            // tabHistory
            // 
            this.tabHistory.BackColor = System.Drawing.Color.White;
            this.tabHistory.Location = new System.Drawing.Point(4, 26);
            this.tabHistory.Name = "tabHistory";
            this.tabHistory.Padding = new System.Windows.Forms.Padding(3);
            this.tabHistory.Size = new System.Drawing.Size(1093, 198);
            this.tabHistory.TabIndex = 2;
            this.tabHistory.Text = "Transaction History";
            // 
            // panelMetrics
            // 
            this.panelMetrics.BackColor = System.Drawing.Color.White;
            this.panelMetrics.Controls.Add(this.panelMetricTotalTransactions);
            this.panelMetrics.Controls.Add(this.panelMetricTodayActivity);
            this.panelMetrics.Controls.Add(this.panelMetricCurrentlyBorrowed);
            this.panelMetrics.Controls.Add(this.panelMetricOverdue);
            this.panelMetrics.Location = new System.Drawing.Point(24, 106);
            this.panelMetrics.Name = "panelMetrics";
            this.panelMetrics.Size = new System.Drawing.Size(1152, 100);
            this.panelMetrics.TabIndex = 1;
            // 
            // panelMetricTotalTransactions
            // 
            this.panelMetricTotalTransactions.BackColor = System.Drawing.Color.White;
            this.panelMetricTotalTransactions.Controls.Add(this.lblMetricTotalTransactionsValue);
            this.panelMetricTotalTransactions.Controls.Add(this.lblMetricTotalTransactionsTitle);
            this.panelMetricTotalTransactions.Location = new System.Drawing.Point(931, 0);
            this.panelMetricTotalTransactions.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.panelMetricTotalTransactions.Name = "panelMetricTotalTransactions";
            this.panelMetricTotalTransactions.Padding = new System.Windows.Forms.Padding(16);
            this.panelMetricTotalTransactions.Size = new System.Drawing.Size(220, 100);
            this.panelMetricTotalTransactions.TabIndex = 3;
            // 
            // lblMetricTotalTransactionsValue
            // 
            this.lblMetricTotalTransactionsValue.AutoSize = true;
            this.lblMetricTotalTransactionsValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTotalTransactionsValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricTotalTransactionsValue.Location = new System.Drawing.Point(16, 50);
            this.lblMetricTotalTransactionsValue.Name = "lblMetricTotalTransactionsValue";
            this.lblMetricTotalTransactionsValue.Size = new System.Drawing.Size(38, 45);
            this.lblMetricTotalTransactionsValue.TabIndex = 2;
            this.lblMetricTotalTransactionsValue.Text = "0";
            // 
            // lblMetricTotalTransactionsTitle
            // 
            this.lblMetricTotalTransactionsTitle.AutoSize = true;
            this.lblMetricTotalTransactionsTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTotalTransactionsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricTotalTransactionsTitle.Location = new System.Drawing.Point(16, 16);
            this.lblMetricTotalTransactionsTitle.Name = "lblMetricTotalTransactionsTitle";
            this.lblMetricTotalTransactionsTitle.Size = new System.Drawing.Size(140, 19);
            this.lblMetricTotalTransactionsTitle.TabIndex = 1;
            this.lblMetricTotalTransactionsTitle.Text = "📋 Total Transactions";
            // 
            // panelMetricTodayActivity
            // 
            this.panelMetricTodayActivity.BackColor = System.Drawing.Color.White;
            this.panelMetricTodayActivity.Controls.Add(this.lblMetricTodayActivityValue);
            this.panelMetricTodayActivity.Controls.Add(this.lblMetricTodayActivityTitle);
            this.panelMetricTodayActivity.Location = new System.Drawing.Point(0, 0);
            this.panelMetricTodayActivity.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.panelMetricTodayActivity.Name = "panelMetricTodayActivity";
            this.panelMetricTodayActivity.Padding = new System.Windows.Forms.Padding(16);
            this.panelMetricTodayActivity.Size = new System.Drawing.Size(220, 100);
            this.panelMetricTodayActivity.TabIndex = 0;
            // 
            // lblMetricTodayActivityValue
            // 
            this.lblMetricTodayActivityValue.AutoSize = true;
            this.lblMetricTodayActivityValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTodayActivityValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricTodayActivityValue.Location = new System.Drawing.Point(16, 50);
            this.lblMetricTodayActivityValue.Name = "lblMetricTodayActivityValue";
            this.lblMetricTodayActivityValue.Size = new System.Drawing.Size(38, 45);
            this.lblMetricTodayActivityValue.TabIndex = 2;
            this.lblMetricTodayActivityValue.Text = "0";
            // 
            // lblMetricTodayActivityTitle
            // 
            this.lblMetricTodayActivityTitle.AutoSize = true;
            this.lblMetricTodayActivityTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTodayActivityTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricTodayActivityTitle.Location = new System.Drawing.Point(16, 16);
            this.lblMetricTodayActivityTitle.Name = "lblMetricTodayActivityTitle";
            this.lblMetricTodayActivityTitle.Size = new System.Drawing.Size(126, 19);
            this.lblMetricTodayActivityTitle.TabIndex = 1;
            this.lblMetricTodayActivityTitle.Text = "📅 Today\'s Activity";
            // 
            // panelMetricCurrentlyBorrowed
            // 
            this.panelMetricCurrentlyBorrowed.BackColor = System.Drawing.Color.White;
            this.panelMetricCurrentlyBorrowed.Controls.Add(this.lblMetricCurrentlyBorrowedValue);
            this.panelMetricCurrentlyBorrowed.Controls.Add(this.lblMetricCurrentlyBorrowedTitle);
            this.panelMetricCurrentlyBorrowed.Location = new System.Drawing.Point(311, 0);
            this.panelMetricCurrentlyBorrowed.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.panelMetricCurrentlyBorrowed.Name = "panelMetricCurrentlyBorrowed";
            this.panelMetricCurrentlyBorrowed.Padding = new System.Windows.Forms.Padding(16);
            this.panelMetricCurrentlyBorrowed.Size = new System.Drawing.Size(220, 100);
            this.panelMetricCurrentlyBorrowed.TabIndex = 1;
            // 
            // lblMetricCurrentlyBorrowedValue
            // 
            this.lblMetricCurrentlyBorrowedValue.AutoSize = true;
            this.lblMetricCurrentlyBorrowedValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricCurrentlyBorrowedValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricCurrentlyBorrowedValue.Location = new System.Drawing.Point(16, 50);
            this.lblMetricCurrentlyBorrowedValue.Name = "lblMetricCurrentlyBorrowedValue";
            this.lblMetricCurrentlyBorrowedValue.Size = new System.Drawing.Size(38, 45);
            this.lblMetricCurrentlyBorrowedValue.TabIndex = 2;
            this.lblMetricCurrentlyBorrowedValue.Text = "0";
            // 
            // lblMetricCurrentlyBorrowedTitle
            // 
            this.lblMetricCurrentlyBorrowedTitle.AutoSize = true;
            this.lblMetricCurrentlyBorrowedTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricCurrentlyBorrowedTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricCurrentlyBorrowedTitle.Location = new System.Drawing.Point(16, 16);
            this.lblMetricCurrentlyBorrowedTitle.Name = "lblMetricCurrentlyBorrowedTitle";
            this.lblMetricCurrentlyBorrowedTitle.Size = new System.Drawing.Size(152, 19);
            this.lblMetricCurrentlyBorrowedTitle.TabIndex = 1;
            this.lblMetricCurrentlyBorrowedTitle.Text = "📖 Currently Borrowed";
            // 
            // panelMetricOverdue
            // 
            this.panelMetricOverdue.BackColor = System.Drawing.Color.White;
            this.panelMetricOverdue.Controls.Add(this.lblMetricOverdueValue);
            this.panelMetricOverdue.Controls.Add(this.lblMetricOverdueTitle);
            this.panelMetricOverdue.Location = new System.Drawing.Point(622, 0);
            this.panelMetricOverdue.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.panelMetricOverdue.Name = "panelMetricOverdue";
            this.panelMetricOverdue.Padding = new System.Windows.Forms.Padding(16);
            this.panelMetricOverdue.Size = new System.Drawing.Size(220, 100);
            this.panelMetricOverdue.TabIndex = 2;
            // 
            // lblMetricOverdueValue
            // 
            this.lblMetricOverdueValue.AutoSize = true;
            this.lblMetricOverdueValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricOverdueValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricOverdueValue.Location = new System.Drawing.Point(16, 50);
            this.lblMetricOverdueValue.Name = "lblMetricOverdueValue";
            this.lblMetricOverdueValue.Size = new System.Drawing.Size(38, 45);
            this.lblMetricOverdueValue.TabIndex = 2;
            this.lblMetricOverdueValue.Text = "0";
            // 
            // lblMetricOverdueTitle
            // 
            this.lblMetricOverdueTitle.AutoSize = true;
            this.lblMetricOverdueTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricOverdueTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricOverdueTitle.Location = new System.Drawing.Point(16, 16);
            this.lblMetricOverdueTitle.Name = "lblMetricOverdueTitle";
            this.lblMetricOverdueTitle.Size = new System.Drawing.Size(126, 19);
            this.lblMetricOverdueTitle.TabIndex = 1;
            this.lblMetricOverdueTitle.Text = "⚠️ Overdue Books";
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.lblSubtitle);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHeader.Location = new System.Drawing.Point(24, 24);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1152, 752);
            this.panelHeader.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblSubtitle.Location = new System.Drawing.Point(14, 52);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(295, 19);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Process book borrowing, returns, and renewals";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitle.Location = new System.Drawing.Point(3, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(389, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Circulation Management";
            // 
            // AdminCirculationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.panelMainContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "AdminCirculationForm";
            this.Text = "Circulation Management";
            this.Load += new System.EventHandler(this.AdminCirculationForm_Load);
            this.panelMainContainer.ResumeLayout(false);
            this.panelTableContainer.ResumeLayout(false);
            this.panelTableContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTransactions)).EndInit();
            this.panelActionTabs.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabBorrow.ResumeLayout(false);
            this.panelBorrowContent.ResumeLayout(false);
            this.panelBorrowContent.PerformLayout();
            this.tabReturn.ResumeLayout(false);
            this.panelReturnContent.ResumeLayout(false);
            this.panelReturnContent.PerformLayout();
            this.panelMetrics.ResumeLayout(false);
            this.panelMetricTotalTransactions.ResumeLayout(false);
            this.panelMetricTotalTransactions.PerformLayout();
            this.panelMetricTodayActivity.ResumeLayout(false);
            this.panelMetricTodayActivity.PerformLayout();
            this.panelMetricCurrentlyBorrowed.ResumeLayout(false);
            this.panelMetricCurrentlyBorrowed.PerformLayout();
            this.panelMetricOverdue.ResumeLayout(false);
            this.panelMetricOverdue.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.Panel panelMainContainer;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel panelMetrics;
        private System.Windows.Forms.Panel panelMetricTodayActivity;
        private System.Windows.Forms.Label lblMetricTodayActivityValue;
        private System.Windows.Forms.Label lblMetricTodayActivityTitle;
        private System.Windows.Forms.Panel panelMetricCurrentlyBorrowed;
        private System.Windows.Forms.Label lblMetricCurrentlyBorrowedValue;
        private System.Windows.Forms.Label lblMetricCurrentlyBorrowedTitle;
        private System.Windows.Forms.Panel panelMetricOverdue;
        private System.Windows.Forms.Label lblMetricOverdueValue;
        private System.Windows.Forms.Label lblMetricOverdueTitle;
        private System.Windows.Forms.Panel panelMetricTotalTransactions;
        private System.Windows.Forms.Label lblMetricTotalTransactionsValue;
        private System.Windows.Forms.Label lblMetricTotalTransactionsTitle;
        private System.Windows.Forms.Panel panelActionTabs;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabBorrow;
        private System.Windows.Forms.TabPage tabReturn;
        private System.Windows.Forms.TabPage tabHistory;
        private System.Windows.Forms.Panel panelBorrowContent;
        private System.Windows.Forms.Label lblBorrowMemberID;
        private System.Windows.Forms.TextBox txtBorrowMemberID;
        private System.Windows.Forms.Label lblBorrowBookAccession;
        private System.Windows.Forms.TextBox txtBorrowBookAccession;
        private System.Windows.Forms.Button btnProcessBorrowing;
        private System.Windows.Forms.Panel panelReturnContent;
        private System.Windows.Forms.Label lblReturnBookAccession;
        private System.Windows.Forms.TextBox txtReturnBookAccession;
        private System.Windows.Forms.Button btnProcessReturn;
        private System.Windows.Forms.Panel panelTableContainer;
        private System.Windows.Forms.DataGridView dataGridViewTransactions;
        private System.Windows.Forms.Label lblRecentTransactions;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTransactionID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMember;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBook;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBorrowDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDueDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFine;
    }
}