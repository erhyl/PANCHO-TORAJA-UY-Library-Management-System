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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelMainContainer = new System.Windows.Forms.Panel();
            this.panelActionTabs = new System.Windows.Forms.Panel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabBorrow = new System.Windows.Forms.TabPage();
            this.panelBorrowContent = new System.Windows.Forms.Panel();
            this.panelBorrowBookInfo = new System.Windows.Forms.Panel();
            this.lblBorrowBookCopies = new System.Windows.Forms.Label();
            this.lblBorrowBookStatus = new System.Windows.Forms.Label();
            this.lblBorrowBookAuthor = new System.Windows.Forms.Label();
            this.lblBorrowBookTitle = new System.Windows.Forms.Label();
            this.lblBorrowBookInfoTitle = new System.Windows.Forms.Label();
            this.panelMemberEligibility = new System.Windows.Forms.Panel();
            this.lblEligibilityStatus = new System.Windows.Forms.Label();
            this.lblEligibilityTitle = new System.Windows.Forms.Label();
            this.lblMemberFines = new System.Windows.Forms.Label();
            this.lblMemberOverdue = new System.Windows.Forms.Label();
            this.lblMemberBorrowings = new System.Windows.Forms.Label();
            this.lblMemberStatus = new System.Windows.Forms.Label();
            this.lblMemberType = new System.Windows.Forms.Label();
            this.lblMemberName = new System.Windows.Forms.Label();
            this.lblMemberEligibilityTitle = new System.Windows.Forms.Label();
            this.btnProcessBorrowing = new System.Windows.Forms.Button();
            this.lblBorrowBookAccession = new System.Windows.Forms.Label();
            this.txtBorrowBookAccession = new System.Windows.Forms.TextBox();
            this.lblBorrowMemberID = new System.Windows.Forms.Label();
            this.txtBorrowMemberID = new System.Windows.Forms.TextBox();
            this.tabReturn = new System.Windows.Forms.TabPage();
            this.panelReturnContent = new System.Windows.Forms.Panel();
            this.panelReturnBookInfo = new System.Windows.Forms.Panel();
            this.label21 = new System.Windows.Forms.Label();
            this.btnProcessReturn = new System.Windows.Forms.Button();
            this.txtReturnBookAccession = new System.Windows.Forms.TextBox();
            this.lblReturnBookAccession = new System.Windows.Forms.Label();
            this.tabRenew = new System.Windows.Forms.TabPage();
            this.panelRenewContent = new System.Windows.Forms.Panel();
            this.panelRenewBookInfo = new System.Windows.Forms.Panel();
            this.lblRenewEligibility = new System.Windows.Forms.Label();
            this.lblRenewRenewals = new System.Windows.Forms.Label();
            this.lblRenewDueDate = new System.Windows.Forms.Label();
            this.lblRenewMemberName = new System.Windows.Forms.Label();
            this.lblRenewBookAuthor = new System.Windows.Forms.Label();
            this.lblRenewBookTitle = new System.Windows.Forms.Label();
            this.lblRenewBookInfoTitle = new System.Windows.Forms.Label();
            this.btnProcessRenewal = new System.Windows.Forms.Button();
            this.txtRenewBookAccession = new System.Windows.Forms.TextBox();
            this.lblRenewBookAccession = new System.Windows.Forms.Label();
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
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblReturnFine = new System.Windows.Forms.Label();
            this.lblReturnOverdue = new System.Windows.Forms.Label();
            this.lblReturnDueDate = new System.Windows.Forms.Label();
            this.lblReturnMemberName = new System.Windows.Forms.Label();
            this.lblReturnBookAuthor = new System.Windows.Forms.Label();
            this.lblReturnBookTitle = new System.Windows.Forms.Label();
            this.lblReturnBookInfoTitle = new System.Windows.Forms.Label();
            this.panelMainContainer.SuspendLayout();
            this.panelActionTabs.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabBorrow.SuspendLayout();
            this.panelBorrowContent.SuspendLayout();
            this.panelBorrowBookInfo.SuspendLayout();
            this.panelMemberEligibility.SuspendLayout();
            this.tabReturn.SuspendLayout();
            this.panelReturnContent.SuspendLayout();
            this.panelReturnBookInfo.SuspendLayout();
            this.tabRenew.SuspendLayout();
            this.panelRenewContent.SuspendLayout();
            this.panelRenewBookInfo.SuspendLayout();
            this.panelMetrics.SuspendLayout();
            this.panelMetricTotalTransactions.SuspendLayout();
            this.panelMetricTodayActivity.SuspendLayout();
            this.panelMetricCurrentlyBorrowed.SuspendLayout();
            this.panelMetricOverdue.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panelTableContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTransactions)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMainContainer
            // 
            this.panelMainContainer.AutoScroll = true;
            this.panelMainContainer.BackColor = System.Drawing.Color.Transparent;
            this.panelMainContainer.Controls.Add(this.panelActionTabs);
            this.panelMainContainer.Controls.Add(this.panelMetrics);
            this.panelMainContainer.Controls.Add(this.panelHeader);
            this.panelMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainContainer.Location = new System.Drawing.Point(0, 0);
            this.panelMainContainer.Margin = new System.Windows.Forms.Padding(4);
            this.panelMainContainer.Name = "panelMainContainer";
            this.panelMainContainer.Padding = new System.Windows.Forms.Padding(32, 30, 32, 30);
            this.panelMainContainer.Size = new System.Drawing.Size(1720, 985);
            this.panelMainContainer.TabIndex = 0;
            // 
            // panelActionTabs
            // 
            this.panelActionTabs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelActionTabs.BackColor = System.Drawing.Color.White;
            this.panelActionTabs.Controls.Add(this.tabControl);
            this.panelActionTabs.Location = new System.Drawing.Point(32, 257);
            this.panelActionTabs.Margin = new System.Windows.Forms.Padding(4);
            this.panelActionTabs.Name = "panelActionTabs";
            this.panelActionTabs.Padding = new System.Windows.Forms.Padding(21, 20, 21, 20);
            this.panelActionTabs.Size = new System.Drawing.Size(1655, 458);
            this.panelActionTabs.TabIndex = 2;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabBorrow);
            this.tabControl.Controls.Add(this.tabReturn);
            this.tabControl.Controls.Add(this.tabRenew);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl.Location = new System.Drawing.Point(21, 20);
            this.tabControl.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1613, 418);
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabBorrow
            // 
            this.tabBorrow.BackColor = System.Drawing.Color.White;
            this.tabBorrow.Controls.Add(this.panelBorrowContent);
            this.tabBorrow.Location = new System.Drawing.Point(4, 32);
            this.tabBorrow.Margin = new System.Windows.Forms.Padding(4);
            this.tabBorrow.Name = "tabBorrow";
            this.tabBorrow.Padding = new System.Windows.Forms.Padding(4);
            this.tabBorrow.Size = new System.Drawing.Size(1605, 382);
            this.tabBorrow.TabIndex = 0;
            this.tabBorrow.Text = "Borrow Book";
            // 
            // panelBorrowContent
            // 
            this.panelBorrowContent.Controls.Add(this.panelBorrowBookInfo);
            this.panelBorrowContent.Controls.Add(this.panelMemberEligibility);
            this.panelBorrowContent.Controls.Add(this.btnProcessBorrowing);
            this.panelBorrowContent.Controls.Add(this.lblBorrowBookAccession);
            this.panelBorrowContent.Controls.Add(this.txtBorrowBookAccession);
            this.panelBorrowContent.Controls.Add(this.lblBorrowMemberID);
            this.panelBorrowContent.Controls.Add(this.txtBorrowMemberID);
            this.panelBorrowContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBorrowContent.Location = new System.Drawing.Point(4, 4);
            this.panelBorrowContent.Margin = new System.Windows.Forms.Padding(4);
            this.panelBorrowContent.Name = "panelBorrowContent";
            this.panelBorrowContent.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.panelBorrowContent.Size = new System.Drawing.Size(1597, 374);
            this.panelBorrowContent.TabIndex = 0;
            // 
            // panelBorrowBookInfo
            // 
            this.panelBorrowBookInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBorrowBookInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.panelBorrowBookInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBorrowBookInfo.Controls.Add(this.lblBorrowBookCopies);
            this.panelBorrowBookInfo.Controls.Add(this.lblBorrowBookStatus);
            this.panelBorrowBookInfo.Controls.Add(this.lblBorrowBookAuthor);
            this.panelBorrowBookInfo.Controls.Add(this.lblBorrowBookTitle);
            this.panelBorrowBookInfo.Controls.Add(this.lblBorrowBookInfoTitle);
            this.panelBorrowBookInfo.Location = new System.Drawing.Point(11, 244);
            this.panelBorrowBookInfo.Margin = new System.Windows.Forms.Padding(4);
            this.panelBorrowBookInfo.Name = "panelBorrowBookInfo";
            this.panelBorrowBookInfo.Size = new System.Drawing.Size(1570, 60);
            this.panelBorrowBookInfo.TabIndex = 6;
            this.panelBorrowBookInfo.Visible = false;
            // 
            // lblBorrowBookCopies
            // 
            this.lblBorrowBookCopies.AutoSize = true;
            this.lblBorrowBookCopies.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBorrowBookCopies.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblBorrowBookCopies.Location = new System.Drawing.Point(900, 33);
            this.lblBorrowBookCopies.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBorrowBookCopies.Name = "lblBorrowBookCopies";
            this.lblBorrowBookCopies.Size = new System.Drawing.Size(0, 20);
            this.lblBorrowBookCopies.TabIndex = 4;
            // 
            // lblBorrowBookStatus
            // 
            this.lblBorrowBookStatus.AutoSize = true;
            this.lblBorrowBookStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBorrowBookStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblBorrowBookStatus.Location = new System.Drawing.Point(600, 33);
            this.lblBorrowBookStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBorrowBookStatus.Name = "lblBorrowBookStatus";
            this.lblBorrowBookStatus.Size = new System.Drawing.Size(0, 20);
            this.lblBorrowBookStatus.TabIndex = 3;
            // 
            // lblBorrowBookAuthor
            // 
            this.lblBorrowBookAuthor.AutoSize = true;
            this.lblBorrowBookAuthor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBorrowBookAuthor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblBorrowBookAuthor.Location = new System.Drawing.Point(300, 33);
            this.lblBorrowBookAuthor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBorrowBookAuthor.Name = "lblBorrowBookAuthor";
            this.lblBorrowBookAuthor.Size = new System.Drawing.Size(0, 20);
            this.lblBorrowBookAuthor.TabIndex = 2;
            // 
            // lblBorrowBookTitle
            // 
            this.lblBorrowBookTitle.AutoSize = true;
            this.lblBorrowBookTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBorrowBookTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblBorrowBookTitle.Location = new System.Drawing.Point(12, 33);
            this.lblBorrowBookTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBorrowBookTitle.Name = "lblBorrowBookTitle";
            this.lblBorrowBookTitle.Size = new System.Drawing.Size(0, 20);
            this.lblBorrowBookTitle.TabIndex = 1;
            // 
            // lblBorrowBookInfoTitle
            // 
            this.lblBorrowBookInfoTitle.AutoSize = true;
            this.lblBorrowBookInfoTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBorrowBookInfoTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblBorrowBookInfoTitle.Location = new System.Drawing.Point(8, 8);
            this.lblBorrowBookInfoTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBorrowBookInfoTitle.Name = "lblBorrowBookInfoTitle";
            this.lblBorrowBookInfoTitle.Size = new System.Drawing.Size(139, 21);
            this.lblBorrowBookInfoTitle.TabIndex = 0;
            this.lblBorrowBookInfoTitle.Text = "Book Information";
            // 
            // panelMemberEligibility
            // 
            this.panelMemberEligibility.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMemberEligibility.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.panelMemberEligibility.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMemberEligibility.Controls.Add(this.lblEligibilityStatus);
            this.panelMemberEligibility.Controls.Add(this.lblEligibilityTitle);
            this.panelMemberEligibility.Controls.Add(this.lblMemberFines);
            this.panelMemberEligibility.Controls.Add(this.lblMemberOverdue);
            this.panelMemberEligibility.Controls.Add(this.lblMemberBorrowings);
            this.panelMemberEligibility.Controls.Add(this.lblMemberStatus);
            this.panelMemberEligibility.Controls.Add(this.lblMemberType);
            this.panelMemberEligibility.Controls.Add(this.lblMemberName);
            this.panelMemberEligibility.Controls.Add(this.lblMemberEligibilityTitle);
            this.panelMemberEligibility.Location = new System.Drawing.Point(11, 77);
            this.panelMemberEligibility.Margin = new System.Windows.Forms.Padding(4);
            this.panelMemberEligibility.Name = "panelMemberEligibility";
            this.panelMemberEligibility.Size = new System.Drawing.Size(1570, 85);
            this.panelMemberEligibility.TabIndex = 5;
            this.panelMemberEligibility.Visible = false;
            // 
            // lblEligibilityStatus
            // 
            this.lblEligibilityStatus.AutoSize = true;
            this.lblEligibilityStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEligibilityStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(139)))), ((int)(((byte)(34)))));
            this.lblEligibilityStatus.Location = new System.Drawing.Point(980, 33);
            this.lblEligibilityStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEligibilityStatus.Name = "lblEligibilityStatus";
            this.lblEligibilityStatus.Size = new System.Drawing.Size(0, 23);
            this.lblEligibilityStatus.TabIndex = 8;
            // 
            // lblEligibilityTitle
            // 
            this.lblEligibilityTitle.AutoSize = true;
            this.lblEligibilityTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEligibilityTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblEligibilityTitle.Location = new System.Drawing.Point(900, 33);
            this.lblEligibilityTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEligibilityTitle.Name = "lblEligibilityTitle";
            this.lblEligibilityTitle.Size = new System.Drawing.Size(76, 20);
            this.lblEligibilityTitle.TabIndex = 7;
            this.lblEligibilityTitle.Text = "Eligibility:";
            // 
            // lblMemberFines
            // 
            this.lblMemberFines.AutoSize = true;
            this.lblMemberFines.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMemberFines.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMemberFines.Location = new System.Drawing.Point(600, 53);
            this.lblMemberFines.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMemberFines.Name = "lblMemberFines";
            this.lblMemberFines.Size = new System.Drawing.Size(0, 20);
            this.lblMemberFines.TabIndex = 6;
            // 
            // lblMemberOverdue
            // 
            this.lblMemberOverdue.AutoSize = true;
            this.lblMemberOverdue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMemberOverdue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMemberOverdue.Location = new System.Drawing.Point(600, 33);
            this.lblMemberOverdue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMemberOverdue.Name = "lblMemberOverdue";
            this.lblMemberOverdue.Size = new System.Drawing.Size(0, 20);
            this.lblMemberOverdue.TabIndex = 5;
            // 
            // lblMemberBorrowings
            // 
            this.lblMemberBorrowings.AutoSize = true;
            this.lblMemberBorrowings.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMemberBorrowings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMemberBorrowings.Location = new System.Drawing.Point(300, 53);
            this.lblMemberBorrowings.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMemberBorrowings.Name = "lblMemberBorrowings";
            this.lblMemberBorrowings.Size = new System.Drawing.Size(0, 20);
            this.lblMemberBorrowings.TabIndex = 4;
            // 
            // lblMemberStatus
            // 
            this.lblMemberStatus.AutoSize = true;
            this.lblMemberStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMemberStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMemberStatus.Location = new System.Drawing.Point(300, 33);
            this.lblMemberStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMemberStatus.Name = "lblMemberStatus";
            this.lblMemberStatus.Size = new System.Drawing.Size(0, 20);
            this.lblMemberStatus.TabIndex = 3;
            // 
            // lblMemberType
            // 
            this.lblMemberType.AutoSize = true;
            this.lblMemberType.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMemberType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMemberType.Location = new System.Drawing.Point(12, 53);
            this.lblMemberType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMemberType.Name = "lblMemberType";
            this.lblMemberType.Size = new System.Drawing.Size(0, 20);
            this.lblMemberType.TabIndex = 2;
            // 
            // lblMemberName
            // 
            this.lblMemberName.AutoSize = true;
            this.lblMemberName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMemberName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMemberName.Location = new System.Drawing.Point(12, 33);
            this.lblMemberName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMemberName.Name = "lblMemberName";
            this.lblMemberName.Size = new System.Drawing.Size(0, 20);
            this.lblMemberName.TabIndex = 1;
            // 
            // lblMemberEligibilityTitle
            // 
            this.lblMemberEligibilityTitle.AutoSize = true;
            this.lblMemberEligibilityTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMemberEligibilityTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMemberEligibilityTitle.Location = new System.Drawing.Point(8, 8);
            this.lblMemberEligibilityTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMemberEligibilityTitle.Name = "lblMemberEligibilityTitle";
            this.lblMemberEligibilityTitle.Size = new System.Drawing.Size(143, 21);
            this.lblMemberEligibilityTitle.TabIndex = 0;
            this.lblMemberEligibilityTitle.Text = "Member Eligibility";
            // 
            // btnProcessBorrowing
            // 
            this.btnProcessBorrowing.BackColor = System.Drawing.Color.Maroon;
            this.btnProcessBorrowing.FlatAppearance.BorderSize = 0;
            this.btnProcessBorrowing.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcessBorrowing.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProcessBorrowing.ForeColor = System.Drawing.Color.White;
            this.btnProcessBorrowing.Location = new System.Drawing.Point(12, 312);
            this.btnProcessBorrowing.Margin = new System.Windows.Forms.Padding(4);
            this.btnProcessBorrowing.Name = "btnProcessBorrowing";
            this.btnProcessBorrowing.Size = new System.Drawing.Size(1569, 43);
            this.btnProcessBorrowing.TabIndex = 4;
            this.btnProcessBorrowing.Text = "Process Borrowing";
            this.btnProcessBorrowing.UseVisualStyleBackColor = false;
            this.btnProcessBorrowing.Click += new System.EventHandler(this.btnProcessBorrowing_Click);
            // 
            // lblBorrowBookAccession
            // 
            this.lblBorrowBookAccession.AutoSize = true;
            this.lblBorrowBookAccession.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBorrowBookAccession.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblBorrowBookAccession.Location = new System.Drawing.Point(12, 166);
            this.lblBorrowBookAccession.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBorrowBookAccession.Name = "lblBorrowBookAccession";
            this.lblBorrowBookAccession.Size = new System.Drawing.Size(452, 25);
            this.lblBorrowBookAccession.TabIndex = 3;
            this.lblBorrowBookAccession.Text = "Book Accession Number (Format: ACC-000001 or 1)";
            this.lblBorrowBookAccession.Click += new System.EventHandler(this.lblBorrowBookAccession_Click);
            // 
            // txtBorrowBookAccession
            // 
            this.txtBorrowBookAccession.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBorrowBookAccession.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBorrowBookAccession.ForeColor = System.Drawing.Color.Gray;
            this.txtBorrowBookAccession.Location = new System.Drawing.Point(11, 206);
            this.txtBorrowBookAccession.Margin = new System.Windows.Forms.Padding(4);
            this.txtBorrowBookAccession.Name = "txtBorrowBookAccession";
            this.txtBorrowBookAccession.Size = new System.Drawing.Size(1570, 30);
            this.txtBorrowBookAccession.TabIndex = 2;
            this.txtBorrowBookAccession.Text = "Format: ACC-000001 or 1";
            this.txtBorrowBookAccession.TextChanged += new System.EventHandler(this.txtBorrowBookAccession_TextChanged);
            this.txtBorrowBookAccession.Enter += new System.EventHandler(this.txtBorrowBookAccession_Enter);
            this.txtBorrowBookAccession.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBorrowBookAccession_KeyDown);
            this.txtBorrowBookAccession.Leave += new System.EventHandler(this.txtBorrowBookAccession_Leave);
            // 
            // lblBorrowMemberID
            // 
            this.lblBorrowMemberID.AutoSize = true;
            this.lblBorrowMemberID.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBorrowMemberID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblBorrowMemberID.Location = new System.Drawing.Point(12, 11);
            this.lblBorrowMemberID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBorrowMemberID.Name = "lblBorrowMemberID";
            this.lblBorrowMemberID.Size = new System.Drawing.Size(352, 25);
            this.lblBorrowMemberID.TabIndex = 1;
            this.lblBorrowMemberID.Text = "Member ID (Format: MEM-000001 or 1)";
            // 
            // txtBorrowMemberID
            // 
            this.txtBorrowMemberID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBorrowMemberID.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBorrowMemberID.ForeColor = System.Drawing.Color.Gray;
            this.txtBorrowMemberID.Location = new System.Drawing.Point(12, 38);
            this.txtBorrowMemberID.Margin = new System.Windows.Forms.Padding(4);
            this.txtBorrowMemberID.Name = "txtBorrowMemberID";
            this.txtBorrowMemberID.Size = new System.Drawing.Size(1569, 30);
            this.txtBorrowMemberID.TabIndex = 0;
            this.txtBorrowMemberID.Text = "Format: MEM-000001 or 1";
            this.txtBorrowMemberID.TextChanged += new System.EventHandler(this.txtBorrowMemberID_TextChanged);
            this.txtBorrowMemberID.Enter += new System.EventHandler(this.txtBorrowMemberID_Enter);
            this.txtBorrowMemberID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBorrowMemberID_KeyDown);
            this.txtBorrowMemberID.Leave += new System.EventHandler(this.txtBorrowMemberID_Leave);
            // 
            // tabReturn
            // 
            this.tabReturn.BackColor = System.Drawing.Color.White;
            this.tabReturn.Controls.Add(this.panelReturnContent);
            this.tabReturn.Location = new System.Drawing.Point(4, 32);
            this.tabReturn.Margin = new System.Windows.Forms.Padding(4);
            this.tabReturn.Name = "tabReturn";
            this.tabReturn.Padding = new System.Windows.Forms.Padding(4);
            this.tabReturn.Size = new System.Drawing.Size(1605, 382);
            this.tabReturn.TabIndex = 1;
            this.tabReturn.Text = "Return Book";
            // 
            // panelReturnContent
            // 
            this.panelReturnContent.Controls.Add(this.panelReturnBookInfo);
            this.panelReturnContent.Controls.Add(this.btnProcessReturn);
            this.panelReturnContent.Controls.Add(this.txtReturnBookAccession);
            this.panelReturnContent.Controls.Add(this.lblReturnBookAccession);
            this.panelReturnContent.Location = new System.Drawing.Point(5, 4);
            this.panelReturnContent.Margin = new System.Windows.Forms.Padding(4);
            this.panelReturnContent.Name = "panelReturnContent";
            this.panelReturnContent.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.panelReturnContent.Size = new System.Drawing.Size(1520, 319);
            this.panelReturnContent.TabIndex = 1;
            // 
            // panelReturnBookInfo
            // 
            this.panelReturnBookInfo.Controls.Add(this.label21);
            this.panelReturnBookInfo.Location = new System.Drawing.Point(11, 75);
            this.panelReturnBookInfo.Name = "panelReturnBookInfo";
            this.panelReturnBookInfo.Size = new System.Drawing.Size(1490, 184);
            this.panelReturnBookInfo.TabIndex = 0;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label21.Location = new System.Drawing.Point(15, 13);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(139, 21);
            this.label21.TabIndex = 1;
            this.label21.Text = "Book Information";
            // 
            // btnProcessReturn
            // 
            this.btnProcessReturn.BackColor = System.Drawing.Color.Maroon;
            this.btnProcessReturn.FlatAppearance.BorderSize = 0;
            this.btnProcessReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcessReturn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProcessReturn.ForeColor = System.Drawing.Color.White;
            this.btnProcessReturn.Location = new System.Drawing.Point(3, 260);
            this.btnProcessReturn.Margin = new System.Windows.Forms.Padding(4);
            this.btnProcessReturn.Name = "btnProcessReturn";
            this.btnProcessReturn.Size = new System.Drawing.Size(1491, 55);
            this.btnProcessReturn.TabIndex = 2;
            this.btnProcessReturn.Text = "Process Return";
            this.btnProcessReturn.UseVisualStyleBackColor = false;
            this.btnProcessReturn.Click += new System.EventHandler(this.btnProcessReturn_Click);
            // 
            // txtReturnBookAccession
            // 
            this.txtReturnBookAccession.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReturnBookAccession.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReturnBookAccession.ForeColor = System.Drawing.Color.Gray;
            this.txtReturnBookAccession.Location = new System.Drawing.Point(11, 38);
            this.txtReturnBookAccession.Margin = new System.Windows.Forms.Padding(4);
            this.txtReturnBookAccession.Name = "txtReturnBookAccession";
            this.txtReturnBookAccession.Size = new System.Drawing.Size(1490, 30);
            this.txtReturnBookAccession.TabIndex = 0;
            this.txtReturnBookAccession.Text = "Format: ACC-000001 or 1";
            this.txtReturnBookAccession.TextChanged += new System.EventHandler(this.txtReturnBookAccession_TextChanged);
            this.txtReturnBookAccession.Enter += new System.EventHandler(this.txtReturnBookAccession_Enter);
            this.txtReturnBookAccession.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReturnBookAccession_KeyDown);
            this.txtReturnBookAccession.Leave += new System.EventHandler(this.txtReturnBookAccession_Leave);
            // 
            // lblReturnBookAccession
            // 
            this.lblReturnBookAccession.AutoSize = true;
            this.lblReturnBookAccession.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReturnBookAccession.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblReturnBookAccession.Location = new System.Drawing.Point(11, 11);
            this.lblReturnBookAccession.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReturnBookAccession.Name = "lblReturnBookAccession";
            this.lblReturnBookAccession.Size = new System.Drawing.Size(452, 25);
            this.lblReturnBookAccession.TabIndex = 1;
            this.lblReturnBookAccession.Text = "Book Accession Number (Format: ACC-000001 or 1)";
            // 
            // tabRenew
            // 
            this.tabRenew.BackColor = System.Drawing.Color.White;
            this.tabRenew.Controls.Add(this.panelRenewContent);
            this.tabRenew.Location = new System.Drawing.Point(4, 32);
            this.tabRenew.Margin = new System.Windows.Forms.Padding(4);
            this.tabRenew.Name = "tabRenew";
            this.tabRenew.Padding = new System.Windows.Forms.Padding(4);
            this.tabRenew.Size = new System.Drawing.Size(1605, 382);
            this.tabRenew.TabIndex = 2;
            this.tabRenew.Text = "Renew Book";
            // 
            // panelRenewContent
            // 
            this.panelRenewContent.Controls.Add(this.panelRenewBookInfo);
            this.panelRenewContent.Controls.Add(this.btnProcessRenewal);
            this.panelRenewContent.Controls.Add(this.txtRenewBookAccession);
            this.panelRenewContent.Controls.Add(this.lblRenewBookAccession);
            this.panelRenewContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRenewContent.Location = new System.Drawing.Point(4, 4);
            this.panelRenewContent.Margin = new System.Windows.Forms.Padding(4);
            this.panelRenewContent.Name = "panelRenewContent";
            this.panelRenewContent.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.panelRenewContent.Size = new System.Drawing.Size(1597, 374);
            this.panelRenewContent.TabIndex = 0;
            // 
            // panelRenewBookInfo
            // 
            this.panelRenewBookInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRenewBookInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.panelRenewBookInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelRenewBookInfo.Controls.Add(this.lblRenewEligibility);
            this.panelRenewBookInfo.Controls.Add(this.lblRenewRenewals);
            this.panelRenewBookInfo.Controls.Add(this.lblRenewDueDate);
            this.panelRenewBookInfo.Controls.Add(this.lblRenewMemberName);
            this.panelRenewBookInfo.Controls.Add(this.lblRenewBookAuthor);
            this.panelRenewBookInfo.Controls.Add(this.lblRenewBookTitle);
            this.panelRenewBookInfo.Controls.Add(this.lblRenewBookInfoTitle);
            this.panelRenewBookInfo.Location = new System.Drawing.Point(12, 78);
            this.panelRenewBookInfo.Margin = new System.Windows.Forms.Padding(4);
            this.panelRenewBookInfo.Name = "panelRenewBookInfo";
            this.panelRenewBookInfo.Size = new System.Drawing.Size(1566, 85);
            this.panelRenewBookInfo.TabIndex = 3;
            this.panelRenewBookInfo.Visible = false;
            // 
            // lblRenewEligibility
            // 
            this.lblRenewEligibility.AutoSize = true;
            this.lblRenewEligibility.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRenewEligibility.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblRenewEligibility.Location = new System.Drawing.Point(600, 53);
            this.lblRenewEligibility.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRenewEligibility.Name = "lblRenewEligibility";
            this.lblRenewEligibility.Size = new System.Drawing.Size(0, 20);
            this.lblRenewEligibility.TabIndex = 6;
            // 
            // lblRenewRenewals
            // 
            this.lblRenewRenewals.AutoSize = true;
            this.lblRenewRenewals.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRenewRenewals.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblRenewRenewals.Location = new System.Drawing.Point(600, 33);
            this.lblRenewRenewals.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRenewRenewals.Name = "lblRenewRenewals";
            this.lblRenewRenewals.Size = new System.Drawing.Size(0, 20);
            this.lblRenewRenewals.TabIndex = 5;
            // 
            // lblRenewDueDate
            // 
            this.lblRenewDueDate.AutoSize = true;
            this.lblRenewDueDate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRenewDueDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblRenewDueDate.Location = new System.Drawing.Point(300, 53);
            this.lblRenewDueDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRenewDueDate.Name = "lblRenewDueDate";
            this.lblRenewDueDate.Size = new System.Drawing.Size(0, 20);
            this.lblRenewDueDate.TabIndex = 4;
            // 
            // lblRenewMemberName
            // 
            this.lblRenewMemberName.AutoSize = true;
            this.lblRenewMemberName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRenewMemberName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblRenewMemberName.Location = new System.Drawing.Point(300, 33);
            this.lblRenewMemberName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRenewMemberName.Name = "lblRenewMemberName";
            this.lblRenewMemberName.Size = new System.Drawing.Size(0, 20);
            this.lblRenewMemberName.TabIndex = 3;
            // 
            // lblRenewBookAuthor
            // 
            this.lblRenewBookAuthor.AutoSize = true;
            this.lblRenewBookAuthor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRenewBookAuthor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblRenewBookAuthor.Location = new System.Drawing.Point(12, 53);
            this.lblRenewBookAuthor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRenewBookAuthor.Name = "lblRenewBookAuthor";
            this.lblRenewBookAuthor.Size = new System.Drawing.Size(0, 20);
            this.lblRenewBookAuthor.TabIndex = 2;
            // 
            // lblRenewBookTitle
            // 
            this.lblRenewBookTitle.AutoSize = true;
            this.lblRenewBookTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRenewBookTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblRenewBookTitle.Location = new System.Drawing.Point(12, 33);
            this.lblRenewBookTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRenewBookTitle.Name = "lblRenewBookTitle";
            this.lblRenewBookTitle.Size = new System.Drawing.Size(0, 20);
            this.lblRenewBookTitle.TabIndex = 1;
            // 
            // lblRenewBookInfoTitle
            // 
            this.lblRenewBookInfoTitle.AutoSize = true;
            this.lblRenewBookInfoTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRenewBookInfoTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblRenewBookInfoTitle.Location = new System.Drawing.Point(8, 8);
            this.lblRenewBookInfoTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRenewBookInfoTitle.Name = "lblRenewBookInfoTitle";
            this.lblRenewBookInfoTitle.Size = new System.Drawing.Size(139, 21);
            this.lblRenewBookInfoTitle.TabIndex = 0;
            this.lblRenewBookInfoTitle.Text = "Book Information";
            // 
            // btnProcessRenewal
            // 
            this.btnProcessRenewal.BackColor = System.Drawing.Color.Maroon;
            this.btnProcessRenewal.FlatAppearance.BorderSize = 0;
            this.btnProcessRenewal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcessRenewal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProcessRenewal.ForeColor = System.Drawing.Color.White;
            this.btnProcessRenewal.Location = new System.Drawing.Point(11, 183);
            this.btnProcessRenewal.Margin = new System.Windows.Forms.Padding(4);
            this.btnProcessRenewal.Name = "btnProcessRenewal";
            this.btnProcessRenewal.Size = new System.Drawing.Size(1567, 55);
            this.btnProcessRenewal.TabIndex = 2;
            this.btnProcessRenewal.Text = "Process Renewal";
            this.btnProcessRenewal.UseVisualStyleBackColor = false;
            this.btnProcessRenewal.Click += new System.EventHandler(this.btnProcessRenewal_Click);
            // 
            // txtRenewBookAccession
            // 
            this.txtRenewBookAccession.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRenewBookAccession.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRenewBookAccession.ForeColor = System.Drawing.Color.Gray;
            this.txtRenewBookAccession.Location = new System.Drawing.Point(12, 38);
            this.txtRenewBookAccession.Margin = new System.Windows.Forms.Padding(4);
            this.txtRenewBookAccession.Name = "txtRenewBookAccession";
            this.txtRenewBookAccession.Size = new System.Drawing.Size(1566, 30);
            this.txtRenewBookAccession.TabIndex = 1;
            this.txtRenewBookAccession.Text = "Format: ACC-000001 or 1";
            this.txtRenewBookAccession.TextChanged += new System.EventHandler(this.txtRenewBookAccession_TextChanged);
            this.txtRenewBookAccession.Enter += new System.EventHandler(this.txtRenewBookAccession_Enter);
            this.txtRenewBookAccession.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRenewBookAccession_KeyDown);
            this.txtRenewBookAccession.Leave += new System.EventHandler(this.txtRenewBookAccession_Leave);
            // 
            // lblRenewBookAccession
            // 
            this.lblRenewBookAccession.AutoSize = true;
            this.lblRenewBookAccession.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRenewBookAccession.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblRenewBookAccession.Location = new System.Drawing.Point(12, 11);
            this.lblRenewBookAccession.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRenewBookAccession.Name = "lblRenewBookAccession";
            this.lblRenewBookAccession.Size = new System.Drawing.Size(452, 25);
            this.lblRenewBookAccession.TabIndex = 0;
            this.lblRenewBookAccession.Text = "Book Accession Number (Format: ACC-000001 or 1)";
            // 
            // panelMetrics
            // 
            this.panelMetrics.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMetrics.BackColor = System.Drawing.Color.White;
            this.panelMetrics.Controls.Add(this.panelMetricTotalTransactions);
            this.panelMetrics.Controls.Add(this.panelMetricTodayActivity);
            this.panelMetrics.Controls.Add(this.panelMetricCurrentlyBorrowed);
            this.panelMetrics.Controls.Add(this.panelMetricOverdue);
            this.panelMetrics.Location = new System.Drawing.Point(32, 130);
            this.panelMetrics.Margin = new System.Windows.Forms.Padding(4);
            this.panelMetrics.Name = "panelMetrics";
            this.panelMetrics.Size = new System.Drawing.Size(1655, 123);
            this.panelMetrics.TabIndex = 1;
            // 
            // panelMetricTotalTransactions
            // 
            this.panelMetricTotalTransactions.BackColor = System.Drawing.Color.White;
            this.panelMetricTotalTransactions.Controls.Add(this.lblMetricTotalTransactionsValue);
            this.panelMetricTotalTransactions.Controls.Add(this.lblMetricTotalTransactionsTitle);
            this.panelMetricTotalTransactions.Location = new System.Drawing.Point(1241, 0);
            this.panelMetricTotalTransactions.Margin = new System.Windows.Forms.Padding(0, 0, 21, 0);
            this.panelMetricTotalTransactions.Name = "panelMetricTotalTransactions";
            this.panelMetricTotalTransactions.Padding = new System.Windows.Forms.Padding(21, 20, 21, 20);
            this.panelMetricTotalTransactions.Size = new System.Drawing.Size(293, 123);
            this.panelMetricTotalTransactions.TabIndex = 3;
            // 
            // lblMetricTotalTransactionsValue
            // 
            this.lblMetricTotalTransactionsValue.AutoSize = true;
            this.lblMetricTotalTransactionsValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTotalTransactionsValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricTotalTransactionsValue.Location = new System.Drawing.Point(21, 62);
            this.lblMetricTotalTransactionsValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricTotalTransactionsValue.Name = "lblMetricTotalTransactionsValue";
            this.lblMetricTotalTransactionsValue.Size = new System.Drawing.Size(46, 54);
            this.lblMetricTotalTransactionsValue.TabIndex = 2;
            this.lblMetricTotalTransactionsValue.Text = "0";
            // 
            // lblMetricTotalTransactionsTitle
            // 
            this.lblMetricTotalTransactionsTitle.AutoSize = true;
            this.lblMetricTotalTransactionsTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTotalTransactionsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricTotalTransactionsTitle.Location = new System.Drawing.Point(21, 20);
            this.lblMetricTotalTransactionsTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricTotalTransactionsTitle.Name = "lblMetricTotalTransactionsTitle";
            this.lblMetricTotalTransactionsTitle.Size = new System.Drawing.Size(173, 23);
            this.lblMetricTotalTransactionsTitle.TabIndex = 1;
            this.lblMetricTotalTransactionsTitle.Text = "📋 Total Transactions";
            // 
            // panelMetricTodayActivity
            // 
            this.panelMetricTodayActivity.BackColor = System.Drawing.Color.White;
            this.panelMetricTodayActivity.Controls.Add(this.lblMetricTodayActivityValue);
            this.panelMetricTodayActivity.Controls.Add(this.lblMetricTodayActivityTitle);
            this.panelMetricTodayActivity.Location = new System.Drawing.Point(0, 0);
            this.panelMetricTodayActivity.Margin = new System.Windows.Forms.Padding(0, 0, 21, 0);
            this.panelMetricTodayActivity.Name = "panelMetricTodayActivity";
            this.panelMetricTodayActivity.Padding = new System.Windows.Forms.Padding(21, 20, 21, 20);
            this.panelMetricTodayActivity.Size = new System.Drawing.Size(293, 123);
            this.panelMetricTodayActivity.TabIndex = 0;
            // 
            // lblMetricTodayActivityValue
            // 
            this.lblMetricTodayActivityValue.AutoSize = true;
            this.lblMetricTodayActivityValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTodayActivityValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricTodayActivityValue.Location = new System.Drawing.Point(21, 62);
            this.lblMetricTodayActivityValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricTodayActivityValue.Name = "lblMetricTodayActivityValue";
            this.lblMetricTodayActivityValue.Size = new System.Drawing.Size(46, 54);
            this.lblMetricTodayActivityValue.TabIndex = 2;
            this.lblMetricTodayActivityValue.Text = "0";
            // 
            // lblMetricTodayActivityTitle
            // 
            this.lblMetricTodayActivityTitle.AutoSize = true;
            this.lblMetricTodayActivityTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTodayActivityTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricTodayActivityTitle.Location = new System.Drawing.Point(21, 20);
            this.lblMetricTodayActivityTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricTodayActivityTitle.Name = "lblMetricTodayActivityTitle";
            this.lblMetricTodayActivityTitle.Size = new System.Drawing.Size(153, 23);
            this.lblMetricTodayActivityTitle.TabIndex = 1;
            this.lblMetricTodayActivityTitle.Text = "📅 Today\'s Activity";
            // 
            // panelMetricCurrentlyBorrowed
            // 
            this.panelMetricCurrentlyBorrowed.BackColor = System.Drawing.Color.White;
            this.panelMetricCurrentlyBorrowed.Controls.Add(this.lblMetricCurrentlyBorrowedValue);
            this.panelMetricCurrentlyBorrowed.Controls.Add(this.lblMetricCurrentlyBorrowedTitle);
            this.panelMetricCurrentlyBorrowed.Location = new System.Drawing.Point(415, 0);
            this.panelMetricCurrentlyBorrowed.Margin = new System.Windows.Forms.Padding(0, 0, 21, 0);
            this.panelMetricCurrentlyBorrowed.Name = "panelMetricCurrentlyBorrowed";
            this.panelMetricCurrentlyBorrowed.Padding = new System.Windows.Forms.Padding(21, 20, 21, 20);
            this.panelMetricCurrentlyBorrowed.Size = new System.Drawing.Size(293, 123);
            this.panelMetricCurrentlyBorrowed.TabIndex = 1;
            // 
            // lblMetricCurrentlyBorrowedValue
            // 
            this.lblMetricCurrentlyBorrowedValue.AutoSize = true;
            this.lblMetricCurrentlyBorrowedValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricCurrentlyBorrowedValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricCurrentlyBorrowedValue.Location = new System.Drawing.Point(21, 62);
            this.lblMetricCurrentlyBorrowedValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricCurrentlyBorrowedValue.Name = "lblMetricCurrentlyBorrowedValue";
            this.lblMetricCurrentlyBorrowedValue.Size = new System.Drawing.Size(46, 54);
            this.lblMetricCurrentlyBorrowedValue.TabIndex = 2;
            this.lblMetricCurrentlyBorrowedValue.Text = "0";
            // 
            // lblMetricCurrentlyBorrowedTitle
            // 
            this.lblMetricCurrentlyBorrowedTitle.AutoSize = true;
            this.lblMetricCurrentlyBorrowedTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricCurrentlyBorrowedTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricCurrentlyBorrowedTitle.Location = new System.Drawing.Point(21, 20);
            this.lblMetricCurrentlyBorrowedTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricCurrentlyBorrowedTitle.Name = "lblMetricCurrentlyBorrowedTitle";
            this.lblMetricCurrentlyBorrowedTitle.Size = new System.Drawing.Size(186, 23);
            this.lblMetricCurrentlyBorrowedTitle.TabIndex = 1;
            this.lblMetricCurrentlyBorrowedTitle.Text = "📖 Currently Borrowed";
            // 
            // panelMetricOverdue
            // 
            this.panelMetricOverdue.BackColor = System.Drawing.Color.White;
            this.panelMetricOverdue.Controls.Add(this.lblMetricOverdueValue);
            this.panelMetricOverdue.Controls.Add(this.lblMetricOverdueTitle);
            this.panelMetricOverdue.Location = new System.Drawing.Point(829, 0);
            this.panelMetricOverdue.Margin = new System.Windows.Forms.Padding(0, 0, 21, 0);
            this.panelMetricOverdue.Name = "panelMetricOverdue";
            this.panelMetricOverdue.Padding = new System.Windows.Forms.Padding(21, 20, 21, 20);
            this.panelMetricOverdue.Size = new System.Drawing.Size(293, 123);
            this.panelMetricOverdue.TabIndex = 2;
            // 
            // lblMetricOverdueValue
            // 
            this.lblMetricOverdueValue.AutoSize = true;
            this.lblMetricOverdueValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricOverdueValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricOverdueValue.Location = new System.Drawing.Point(21, 62);
            this.lblMetricOverdueValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricOverdueValue.Name = "lblMetricOverdueValue";
            this.lblMetricOverdueValue.Size = new System.Drawing.Size(46, 54);
            this.lblMetricOverdueValue.TabIndex = 2;
            this.lblMetricOverdueValue.Text = "0";
            // 
            // lblMetricOverdueTitle
            // 
            this.lblMetricOverdueTitle.AutoSize = true;
            this.lblMetricOverdueTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricOverdueTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricOverdueTitle.Location = new System.Drawing.Point(21, 20);
            this.lblMetricOverdueTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricOverdueTitle.Name = "lblMetricOverdueTitle";
            this.lblMetricOverdueTitle.Size = new System.Drawing.Size(153, 23);
            this.lblMetricOverdueTitle.TabIndex = 1;
            this.lblMetricOverdueTitle.Text = "⚠️ Overdue Books";
            // 
            // panelHeader
            // 
            this.panelHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.panelTableContainer);
            this.panelHeader.Controls.Add(this.lblSubtitle);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Location = new System.Drawing.Point(32, 30);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(4);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1636, 994);
            this.panelHeader.TabIndex = 0;
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
            this.panelTableContainer.Location = new System.Drawing.Point(21, 688);
            this.panelTableContainer.Margin = new System.Windows.Forms.Padding(4);
            this.panelTableContainer.Name = "panelTableContainer";
            this.panelTableContainer.Padding = new System.Windows.Forms.Padding(21, 20, 21, 20);
            this.panelTableContainer.Size = new System.Drawing.Size(1632, 323);
            this.panelTableContainer.TabIndex = 3;
            this.panelTableContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.panelTableContainer_Paint);
            // 
            // lblRecentTransactions
            // 
            this.lblRecentTransactions.AutoSize = true;
            this.lblRecentTransactions.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecentTransactions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblRecentTransactions.Location = new System.Drawing.Point(21, 0);
            this.lblRecentTransactions.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRecentTransactions.Name = "lblRecentTransactions";
            this.lblRecentTransactions.Size = new System.Drawing.Size(167, 23);
            this.lblRecentTransactions.TabIndex = 1;
            this.lblRecentTransactions.Text = "Recent Transactions";
            // 
            // dataGridViewTransactions
            // 
            this.dataGridViewTransactions.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.dataGridViewTransactions.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTransactions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewTransactions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewTransactions.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewTransactions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTransactions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
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
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTransactions.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTransactions.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dataGridViewTransactions.Location = new System.Drawing.Point(21, 33);
            this.dataGridViewTransactions.Margin = new System.Windows.Forms.Padding(21, 0, 21, 20);
            this.dataGridViewTransactions.MultiSelect = false;
            this.dataGridViewTransactions.Name = "dataGridViewTransactions";
            this.dataGridViewTransactions.RowHeadersVisible = false;
            this.dataGridViewTransactions.RowHeadersWidth = 51;
            this.dataGridViewTransactions.RowTemplate.Height = 60;
            this.dataGridViewTransactions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewTransactions.Size = new System.Drawing.Size(1588, 268);
            this.dataGridViewTransactions.TabIndex = 0;
            this.dataGridViewTransactions.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTransactions_CellContentClick_2);
            // 
            // colTransactionID
            // 
            this.colTransactionID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTransactionID.DataPropertyName = "TransactionID";
            this.colTransactionID.HeaderText = "TRANSACTION ID";
            this.colTransactionID.MinimumWidth = 6;
            this.colTransactionID.Name = "colTransactionID";
            this.colTransactionID.ReadOnly = true;
            // 
            // colType
            // 
            this.colType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colType.DataPropertyName = "Type";
            this.colType.HeaderText = "TYPE";
            this.colType.MinimumWidth = 6;
            this.colType.Name = "colType";
            // 
            // colMember
            // 
            this.colMember.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colMember.DataPropertyName = "Member";
            this.colMember.HeaderText = "MEMBER";
            this.colMember.MinimumWidth = 6;
            this.colMember.Name = "colMember";
            // 
            // colBook
            // 
            this.colBook.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colBook.DataPropertyName = "Book";
            this.colBook.HeaderText = "BOOK";
            this.colBook.MinimumWidth = 6;
            this.colBook.Name = "colBook";
            // 
            // colBorrowDate
            // 
            this.colBorrowDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colBorrowDate.DataPropertyName = "BorrowDate";
            this.colBorrowDate.HeaderText = "BORROW DATE";
            this.colBorrowDate.MinimumWidth = 6;
            this.colBorrowDate.Name = "colBorrowDate";
            // 
            // colDueDate
            // 
            this.colDueDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDueDate.DataPropertyName = "DueDate";
            this.colDueDate.HeaderText = "DUE DATE";
            this.colDueDate.MinimumWidth = 6;
            this.colDueDate.Name = "colDueDate";
            // 
            // colStatus
            // 
            this.colStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colStatus.DataPropertyName = "Status";
            this.colStatus.HeaderText = "STATUS";
            this.colStatus.MinimumWidth = 6;
            this.colStatus.Name = "colStatus";
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
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblSubtitle.Location = new System.Drawing.Point(19, 64);
            this.lblSubtitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(365, 23);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Process book borrowing, returns, and renewals";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitle.Location = new System.Drawing.Point(4, 10);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(489, 54);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Circulation Management";
            // 
            // lblReturnFine
            // 
            this.lblReturnFine.Location = new System.Drawing.Point(0, 0);
            this.lblReturnFine.Name = "lblReturnFine";
            this.lblReturnFine.Size = new System.Drawing.Size(100, 23);
            this.lblReturnFine.TabIndex = 0;
            // 
            // lblReturnOverdue
            // 
            this.lblReturnOverdue.Location = new System.Drawing.Point(0, 0);
            this.lblReturnOverdue.Name = "lblReturnOverdue";
            this.lblReturnOverdue.Size = new System.Drawing.Size(100, 23);
            this.lblReturnOverdue.TabIndex = 0;
            // 
            // lblReturnDueDate
            // 
            this.lblReturnDueDate.Location = new System.Drawing.Point(0, 0);
            this.lblReturnDueDate.Name = "lblReturnDueDate";
            this.lblReturnDueDate.Size = new System.Drawing.Size(100, 23);
            this.lblReturnDueDate.TabIndex = 0;
            // 
            // lblReturnMemberName
            // 
            this.lblReturnMemberName.Location = new System.Drawing.Point(0, 0);
            this.lblReturnMemberName.Name = "lblReturnMemberName";
            this.lblReturnMemberName.Size = new System.Drawing.Size(100, 23);
            this.lblReturnMemberName.TabIndex = 0;
            // 
            // lblReturnBookAuthor
            // 
            this.lblReturnBookAuthor.Location = new System.Drawing.Point(0, 0);
            this.lblReturnBookAuthor.Name = "lblReturnBookAuthor";
            this.lblReturnBookAuthor.Size = new System.Drawing.Size(100, 23);
            this.lblReturnBookAuthor.TabIndex = 0;
            // 
            // lblReturnBookTitle
            // 
            this.lblReturnBookTitle.Location = new System.Drawing.Point(0, 0);
            this.lblReturnBookTitle.Name = "lblReturnBookTitle";
            this.lblReturnBookTitle.Size = new System.Drawing.Size(100, 23);
            this.lblReturnBookTitle.TabIndex = 0;
            // 
            // lblReturnBookInfoTitle
            // 
            this.lblReturnBookInfoTitle.Location = new System.Drawing.Point(0, 0);
            this.lblReturnBookInfoTitle.Name = "lblReturnBookInfoTitle";
            this.lblReturnBookInfoTitle.Size = new System.Drawing.Size(100, 23);
            this.lblReturnBookInfoTitle.TabIndex = 0;
            // 
            // AdminCirculationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1720, 985);
            this.Controls.Add(this.panelMainContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1067, 738);
            this.Name = "AdminCirculationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Circulation Management";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AdminCirculationForm_Load);
            this.panelMainContainer.ResumeLayout(false);
            this.panelActionTabs.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabBorrow.ResumeLayout(false);
            this.panelBorrowContent.ResumeLayout(false);
            this.panelBorrowContent.PerformLayout();
            this.panelBorrowBookInfo.ResumeLayout(false);
            this.panelBorrowBookInfo.PerformLayout();
            this.panelMemberEligibility.ResumeLayout(false);
            this.panelMemberEligibility.PerformLayout();
            this.tabReturn.ResumeLayout(false);
            this.panelReturnContent.ResumeLayout(false);
            this.panelReturnContent.PerformLayout();
            this.panelReturnBookInfo.ResumeLayout(false);
            this.panelReturnBookInfo.PerformLayout();
            this.tabRenew.ResumeLayout(false);
            this.panelRenewContent.ResumeLayout(false);
            this.panelRenewContent.PerformLayout();
            this.panelRenewBookInfo.ResumeLayout(false);
            this.panelRenewBookInfo.PerformLayout();
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
            this.panelTableContainer.ResumeLayout(false);
            this.panelTableContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTransactions)).EndInit();
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
        private System.Windows.Forms.TabPage tabRenew;
        private System.Windows.Forms.Panel panelBorrowContent;
        private System.Windows.Forms.Panel panelBorrowBookInfo;
        private System.Windows.Forms.Label lblBorrowBookInfoTitle;
        private System.Windows.Forms.Label lblBorrowBookTitle;
        private System.Windows.Forms.Label lblBorrowBookAuthor;
        private System.Windows.Forms.Label lblBorrowBookStatus;
        private System.Windows.Forms.Label lblBorrowBookCopies;
        private System.Windows.Forms.Panel panelMemberEligibility;
        private System.Windows.Forms.Label lblMemberEligibilityTitle;
        private System.Windows.Forms.Label lblMemberName;
        private System.Windows.Forms.Label lblMemberType;
        private System.Windows.Forms.Label lblMemberStatus;
        private System.Windows.Forms.Label lblMemberBorrowings;
        private System.Windows.Forms.Label lblMemberOverdue;
        private System.Windows.Forms.Label lblMemberFines;
        private System.Windows.Forms.Label lblEligibilityTitle;
        private System.Windows.Forms.Label lblEligibilityStatus;
        private System.Windows.Forms.Panel panelRenewContent;
        private System.Windows.Forms.Panel panelRenewBookInfo;
        private System.Windows.Forms.Label lblRenewBookInfoTitle;
        private System.Windows.Forms.Label lblRenewBookTitle;
        private System.Windows.Forms.Label lblRenewBookAuthor;
        private System.Windows.Forms.Label lblRenewMemberName;
        private System.Windows.Forms.Label lblRenewDueDate;
        private System.Windows.Forms.Label lblRenewRenewals;
        private System.Windows.Forms.Label lblRenewEligibility;
        private System.Windows.Forms.Label lblRenewBookAccession;
        private System.Windows.Forms.TextBox txtRenewBookAccession;
        private System.Windows.Forms.Button btnProcessRenewal;
        private System.Windows.Forms.Label lblBorrowMemberID;
        private System.Windows.Forms.TextBox txtBorrowMemberID;
        private System.Windows.Forms.Label lblBorrowBookAccession;
        private System.Windows.Forms.TextBox txtBorrowBookAccession;
        private System.Windows.Forms.Button btnProcessBorrowing;
        private System.Windows.Forms.Panel panelReturnContent;
        private System.Windows.Forms.Panel panelReturnBookInfo;
        private System.Windows.Forms.Label lblReturnBookInfoTitle;
        private System.Windows.Forms.Label lblReturnBookTitle;
        private System.Windows.Forms.Label lblReturnBookAuthor;
        private System.Windows.Forms.Label lblReturnMemberName;
        private System.Windows.Forms.Label lblReturnDueDate;
        private System.Windows.Forms.Label lblReturnOverdue;
        private System.Windows.Forms.Label lblReturnFine;
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
        private System.Windows.Forms.Label label21;
    }
}