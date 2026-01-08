namespace Project5LMS.Forms.Admin.Members
{
    partial class AdminMembersForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelMainContainer = new System.Windows.Forms.Panel();
            this.panelTableContainer = new System.Windows.Forms.Panel();
            this.dataGridViewMembers = new System.Windows.Forms.DataGridView();
            this.colMemberID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colContact = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMemberType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBooks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colExpires = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActions = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelSearchFilter = new System.Windows.Forms.Panel();
            this.cmbStatusFilter = new System.Windows.Forms.ComboBox();
            this.lblStatusFilter = new System.Windows.Forms.Label();
            this.cmbTypeFilter = new System.Windows.Forms.ComboBox();
            this.lblTypeFilter = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.panelMetrics = new System.Windows.Forms.Panel();
            this.panelMetricExpired = new System.Windows.Forms.Panel();
            this.lblMetricExpiredValue = new System.Windows.Forms.Label();
            this.lblMetricExpiredTitle = new System.Windows.Forms.Label();
            this.panelMetricIconExpired = new System.Windows.Forms.Panel();
            this.panelMetricSuspended = new System.Windows.Forms.Panel();
            this.lblMetricSuspendedValue = new System.Windows.Forms.Label();
            this.lblMetricSuspendedTitle = new System.Windows.Forms.Label();
            this.panelMetricIconSuspended = new System.Windows.Forms.Panel();
            this.panelMetricActive = new System.Windows.Forms.Panel();
            this.lblMetricActiveValue = new System.Windows.Forms.Label();
            this.lblMetricActiveTitle = new System.Windows.Forms.Label();
            this.panelMetricIconActive = new System.Windows.Forms.Panel();
            this.panelMetricTotal = new System.Windows.Forms.Panel();
            this.lblMetricTotalValue = new System.Windows.Forms.Label();
            this.lblMetricTotalTitle = new System.Windows.Forms.Label();
            this.panelMetricIconTotal = new System.Windows.Forms.Panel();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.btnAddNewMember = new System.Windows.Forms.Button();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelMainContainer.SuspendLayout();
            this.panelTableContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMembers)).BeginInit();
            this.panelSearchFilter.SuspendLayout();
            this.panelMetrics.SuspendLayout();
            this.panelMetricExpired.SuspendLayout();
            this.panelMetricSuspended.SuspendLayout();
            this.panelMetricActive.SuspendLayout();
            this.panelMetricTotal.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMainContainer
            // 
            this.panelMainContainer.AutoScroll = true;
            this.panelMainContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelMainContainer.Controls.Add(this.panelTableContainer);
            this.panelMainContainer.Controls.Add(this.panelSearchFilter);
            this.panelMainContainer.Controls.Add(this.panelMetrics);
            this.panelMainContainer.Controls.Add(this.panelHeader);
            this.panelMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainContainer.Location = new System.Drawing.Point(0, 0);
            this.panelMainContainer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelMainContainer.Name = "panelMainContainer";
            this.panelMainContainer.Padding = new System.Windows.Forms.Padding(32, 30, 32, 30);
            this.panelMainContainer.Size = new System.Drawing.Size(1924, 1055);
            this.panelMainContainer.TabIndex = 0;
            // 
            // panelTableContainer
            // 
            this.panelTableContainer.BackColor = System.Drawing.Color.White;
            this.panelTableContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTableContainer.Controls.Add(this.dataGridViewMembers);
            this.panelTableContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTableContainer.Location = new System.Drawing.Point(32, 104);
            this.panelTableContainer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelTableContainer.Name = "panelTableContainer";
            this.panelTableContainer.Padding = new System.Windows.Forms.Padding(21, 20, 21, 20);
            this.panelTableContainer.Size = new System.Drawing.Size(1860, 921);
            this.panelTableContainer.TabIndex = 3;
            // 
            // dataGridViewMembers
            // 
            this.dataGridViewMembers.AllowUserToAddRows = false;
            this.dataGridViewMembers.AllowUserToDeleteRows = false;
            this.dataGridViewMembers.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.dataGridViewMembers.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewMembers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewMembers.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewMembers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewMembers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewMembers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMembers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMemberID,
            this.colName,
            this.colContact,
            this.colMemberType,
            this.colStatus,
            this.colBooks,
            this.colExpires,
            this.colActions});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewMembers.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewMembers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewMembers.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dataGridViewMembers.Location = new System.Drawing.Point(21, 20);
            this.dataGridViewMembers.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridViewMembers.MultiSelect = false;
            this.dataGridViewMembers.Name = "dataGridViewMembers";
            this.dataGridViewMembers.ReadOnly = true;
            this.dataGridViewMembers.RowHeadersVisible = false;
            this.dataGridViewMembers.RowHeadersWidth = 51;
            this.dataGridViewMembers.RowTemplate.Height = 60;
            this.dataGridViewMembers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewMembers.Size = new System.Drawing.Size(1816, 879);
            this.dataGridViewMembers.TabIndex = 0;
            this.dataGridViewMembers.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMembers_CellClick);
            // 
            // colMemberID
            // 
            this.colMemberID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colMemberID.DataPropertyName = "MemberID";
            this.colMemberID.HeaderText = "MEMBER ID";
            this.colMemberID.Name = "colMemberID";
            this.colMemberID.ReadOnly = true;
            this.colMemberID.Width = 120;
            // 
            // colName
            // 
            this.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colName.DataPropertyName = "Name";
            this.colName.HeaderText = "NAME";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            this.colName.Width = 250;
            // 
            // colContact
            // 
            this.colContact.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colContact.DataPropertyName = "Contact";
            this.colContact.HeaderText = "CONTACT";
            this.colContact.Name = "colContact";
            this.colContact.ReadOnly = true;
            this.colContact.Width = 150;
            // 
            // colMemberType
            // 
            this.colMemberType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colMemberType.DataPropertyName = "MemberType";
            this.colMemberType.HeaderText = "TYPE";
            this.colMemberType.Name = "colMemberType";
            this.colMemberType.ReadOnly = true;
            this.colMemberType.Width = 120;
            // 
            // colStatus
            // 
            this.colStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colStatus.DataPropertyName = "Status";
            this.colStatus.HeaderText = "STATUS";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.Width = 120;
            // 
            // colBooks
            // 
            this.colBooks.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colBooks.DataPropertyName = "Books";
            this.colBooks.HeaderText = "BOOKS";
            this.colBooks.Name = "colBooks";
            this.colBooks.ReadOnly = true;
            this.colBooks.Width = 100;
            // 
            // colExpires
            // 
            this.colExpires.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colExpires.DataPropertyName = "Expires";
            this.colExpires.HeaderText = "EXPIRES";
            this.colExpires.Name = "colExpires";
            this.colExpires.ReadOnly = true;
            this.colExpires.Width = 120;
            // 
            // colActions
            // 
            this.colActions.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colActions.DataPropertyName = "Actions";
            this.colActions.HeaderText = "ACTIONS";
            this.colActions.Name = "colActions";
            this.colActions.ReadOnly = true;
            this.colActions.Width = 150;
            // 
            // panelSearchFilter
            // 
            this.panelSearchFilter.BackColor = System.Drawing.Color.White;
            this.panelSearchFilter.Controls.Add(this.cmbStatusFilter);
            this.panelSearchFilter.Controls.Add(this.lblStatusFilter);
            this.panelSearchFilter.Controls.Add(this.cmbTypeFilter);
            this.panelSearchFilter.Controls.Add(this.lblTypeFilter);
            this.panelSearchFilter.Controls.Add(this.txtSearch);
            this.panelSearchFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearchFilter.Location = new System.Drawing.Point(32, 30);
            this.panelSearchFilter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelSearchFilter.Name = "panelSearchFilter";
            this.panelSearchFilter.Padding = new System.Windows.Forms.Padding(21, 20, 21, 20);
            this.panelSearchFilter.Size = new System.Drawing.Size(1860, 74);
            this.panelSearchFilter.TabIndex = 2;
            // 
            // cmbStatusFilter
            // 
            this.cmbStatusFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatusFilter.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatusFilter.FormattingEnabled = true;
            this.cmbStatusFilter.Items.AddRange(new object[] {
            "All Status",
            "Active",
            "Suspended",
            "Expired"});
            this.cmbStatusFilter.Location = new System.Drawing.Point(1200, 20);
            this.cmbStatusFilter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbStatusFilter.Name = "cmbStatusFilter";
            this.cmbStatusFilter.Size = new System.Drawing.Size(313, 31);
            this.cmbStatusFilter.TabIndex = 4;
            this.cmbStatusFilter.SelectedIndexChanged += new System.EventHandler(this.cmbStatusFilter_SelectedIndexChanged);
            // 
            // lblStatusFilter
            // 
            this.lblStatusFilter.AutoSize = true;
            this.lblStatusFilter.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblStatusFilter.Location = new System.Drawing.Point(1067, 23);
            this.lblStatusFilter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatusFilter.Name = "lblStatusFilter";
            this.lblStatusFilter.Size = new System.Drawing.Size(84, 23);
            this.lblStatusFilter.TabIndex = 3;
            this.lblStatusFilter.Text = "All Status:";
            // 
            // cmbTypeFilter
            // 
            this.cmbTypeFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypeFilter.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTypeFilter.FormattingEnabled = true;
            this.cmbTypeFilter.Items.AddRange(new object[] {
            "All Types",
            "Student",
            "Faculty",
            "Staff",
            "Guest"});
            this.cmbTypeFilter.Location = new System.Drawing.Point(733, 20);
            this.cmbTypeFilter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbTypeFilter.Name = "cmbTypeFilter";
            this.cmbTypeFilter.Size = new System.Drawing.Size(313, 31);
            this.cmbTypeFilter.TabIndex = 2;
            this.cmbTypeFilter.SelectedIndexChanged += new System.EventHandler(this.cmbTypeFilter_SelectedIndexChanged);
            // 
            // lblTypeFilter
            // 
            this.lblTypeFilter.AutoSize = true;
            this.lblTypeFilter.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTypeFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTypeFilter.Location = new System.Drawing.Point(600, 23);
            this.lblTypeFilter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTypeFilter.Name = "lblTypeFilter";
            this.lblTypeFilter.Size = new System.Drawing.Size(80, 23);
            this.lblTypeFilter.TabIndex = 1;
            this.lblTypeFilter.Text = "All Types:";
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.Color.Gray;
            this.txtSearch.Location = new System.Drawing.Point(21, 20);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(399, 30);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.Text = "🔍 Search members...";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            // 
            // panelMetrics
            // 
            this.panelMetrics.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMetrics.Controls.Add(this.panelMetricExpired);
            this.panelMetrics.Controls.Add(this.panelMetricSuspended);
            this.panelMetrics.Controls.Add(this.panelMetricActive);
            this.panelMetrics.Controls.Add(this.panelMetricTotal);
            this.panelMetrics.Location = new System.Drawing.Point(32, 172);
            this.panelMetrics.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelMetrics.Name = "panelMetrics";
            this.panelMetrics.Size = new System.Drawing.Size(1860, 123);
            this.panelMetrics.TabIndex = 1;
            // 
            // panelMetricExpired
            // 
            this.panelMetricExpired.BackColor = System.Drawing.Color.White;
            this.panelMetricExpired.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMetricExpired.Controls.Add(this.lblMetricExpiredValue);
            this.panelMetricExpired.Controls.Add(this.lblMetricExpiredTitle);
            this.panelMetricExpired.Controls.Add(this.panelMetricIconExpired);
            this.panelMetricExpired.Location = new System.Drawing.Point(1152, 0);
            this.panelMetricExpired.Margin = new System.Windows.Forms.Padding(0, 0, 21, 0);
            this.panelMetricExpired.Name = "panelMetricExpired";
            this.panelMetricExpired.Padding = new System.Windows.Forms.Padding(21, 20, 21, 20);
            this.panelMetricExpired.Size = new System.Drawing.Size(266, 123);
            this.panelMetricExpired.TabIndex = 3;
            // 
            // lblMetricExpiredValue
            // 
            this.lblMetricExpiredValue.AutoSize = true;
            this.lblMetricExpiredValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricExpiredValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricExpiredValue.Location = new System.Drawing.Point(21, 62);
            this.lblMetricExpiredValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricExpiredValue.Name = "lblMetricExpiredValue";
            this.lblMetricExpiredValue.Size = new System.Drawing.Size(46, 54);
            this.lblMetricExpiredValue.TabIndex = 2;
            this.lblMetricExpiredValue.Text = "0";
            // 
            // lblMetricExpiredTitle
            // 
            this.lblMetricExpiredTitle.AutoSize = true;
            this.lblMetricExpiredTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricExpiredTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricExpiredTitle.Location = new System.Drawing.Point(21, 20);
            this.lblMetricExpiredTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricExpiredTitle.Name = "lblMetricExpiredTitle";
            this.lblMetricExpiredTitle.Size = new System.Drawing.Size(66, 23);
            this.lblMetricExpiredTitle.TabIndex = 1;
            this.lblMetricExpiredTitle.Text = "Expired";
            // 
            // panelMetricIconExpired
            // 
            this.panelMetricIconExpired.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.panelMetricIconExpired.Location = new System.Drawing.Point(187, 20);
            this.panelMetricIconExpired.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelMetricIconExpired.Name = "panelMetricIconExpired";
            this.panelMetricIconExpired.Size = new System.Drawing.Size(53, 49);
            this.panelMetricIconExpired.TabIndex = 0;
            // 
            // panelMetricSuspended
            // 
            this.panelMetricSuspended.BackColor = System.Drawing.Color.White;
            this.panelMetricSuspended.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMetricSuspended.Controls.Add(this.lblMetricSuspendedValue);
            this.panelMetricSuspended.Controls.Add(this.lblMetricSuspendedTitle);
            this.panelMetricSuspended.Controls.Add(this.panelMetricIconSuspended);
            this.panelMetricSuspended.Location = new System.Drawing.Point(768, 0);
            this.panelMetricSuspended.Margin = new System.Windows.Forms.Padding(0, 0, 21, 0);
            this.panelMetricSuspended.Name = "panelMetricSuspended";
            this.panelMetricSuspended.Padding = new System.Windows.Forms.Padding(21, 20, 21, 20);
            this.panelMetricSuspended.Size = new System.Drawing.Size(266, 123);
            this.panelMetricSuspended.TabIndex = 2;
            // 
            // lblMetricSuspendedValue
            // 
            this.lblMetricSuspendedValue.AutoSize = true;
            this.lblMetricSuspendedValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricSuspendedValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricSuspendedValue.Location = new System.Drawing.Point(21, 62);
            this.lblMetricSuspendedValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricSuspendedValue.Name = "lblMetricSuspendedValue";
            this.lblMetricSuspendedValue.Size = new System.Drawing.Size(46, 54);
            this.lblMetricSuspendedValue.TabIndex = 2;
            this.lblMetricSuspendedValue.Text = "0";
            // 
            // lblMetricSuspendedTitle
            // 
            this.lblMetricSuspendedTitle.AutoSize = true;
            this.lblMetricSuspendedTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricSuspendedTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricSuspendedTitle.Location = new System.Drawing.Point(21, 20);
            this.lblMetricSuspendedTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricSuspendedTitle.Name = "lblMetricSuspendedTitle";
            this.lblMetricSuspendedTitle.Size = new System.Drawing.Size(94, 23);
            this.lblMetricSuspendedTitle.TabIndex = 1;
            this.lblMetricSuspendedTitle.Text = "Suspended";
            // 
            // panelMetricIconSuspended
            // 
            this.panelMetricIconSuspended.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.panelMetricIconSuspended.Location = new System.Drawing.Point(187, 20);
            this.panelMetricIconSuspended.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelMetricIconSuspended.Name = "panelMetricIconSuspended";
            this.panelMetricIconSuspended.Size = new System.Drawing.Size(53, 49);
            this.panelMetricIconSuspended.TabIndex = 0;
            // 
            // panelMetricActive
            // 
            this.panelMetricActive.BackColor = System.Drawing.Color.White;
            this.panelMetricActive.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMetricActive.Controls.Add(this.lblMetricActiveValue);
            this.panelMetricActive.Controls.Add(this.lblMetricActiveTitle);
            this.panelMetricActive.Controls.Add(this.panelMetricIconActive);
            this.panelMetricActive.Location = new System.Drawing.Point(384, 0);
            this.panelMetricActive.Margin = new System.Windows.Forms.Padding(0, 0, 21, 0);
            this.panelMetricActive.Name = "panelMetricActive";
            this.panelMetricActive.Padding = new System.Windows.Forms.Padding(21, 20, 21, 20);
            this.panelMetricActive.Size = new System.Drawing.Size(266, 123);
            this.panelMetricActive.TabIndex = 1;
            // 
            // lblMetricActiveValue
            // 
            this.lblMetricActiveValue.AutoSize = true;
            this.lblMetricActiveValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricActiveValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricActiveValue.Location = new System.Drawing.Point(21, 62);
            this.lblMetricActiveValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricActiveValue.Name = "lblMetricActiveValue";
            this.lblMetricActiveValue.Size = new System.Drawing.Size(46, 54);
            this.lblMetricActiveValue.TabIndex = 2;
            this.lblMetricActiveValue.Text = "0";
            // 
            // lblMetricActiveTitle
            // 
            this.lblMetricActiveTitle.AutoSize = true;
            this.lblMetricActiveTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricActiveTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricActiveTitle.Location = new System.Drawing.Point(21, 20);
            this.lblMetricActiveTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricActiveTitle.Name = "lblMetricActiveTitle";
            this.lblMetricActiveTitle.Size = new System.Drawing.Size(132, 23);
            this.lblMetricActiveTitle.TabIndex = 1;
            this.lblMetricActiveTitle.Text = "Active Members";
            // 
            // panelMetricIconActive
            // 
            this.panelMetricIconActive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.panelMetricIconActive.Location = new System.Drawing.Point(187, 20);
            this.panelMetricIconActive.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelMetricIconActive.Name = "panelMetricIconActive";
            this.panelMetricIconActive.Size = new System.Drawing.Size(53, 49);
            this.panelMetricIconActive.TabIndex = 0;
            // 
            // panelMetricTotal
            // 
            this.panelMetricTotal.BackColor = System.Drawing.Color.White;
            this.panelMetricTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMetricTotal.Controls.Add(this.lblMetricTotalValue);
            this.panelMetricTotal.Controls.Add(this.lblMetricTotalTitle);
            this.panelMetricTotal.Controls.Add(this.panelMetricIconTotal);
            this.panelMetricTotal.Location = new System.Drawing.Point(0, 0);
            this.panelMetricTotal.Margin = new System.Windows.Forms.Padding(0, 0, 21, 0);
            this.panelMetricTotal.Name = "panelMetricTotal";
            this.panelMetricTotal.Padding = new System.Windows.Forms.Padding(21, 20, 21, 20);
            this.panelMetricTotal.Size = new System.Drawing.Size(266, 123);
            this.panelMetricTotal.TabIndex = 0;
            // 
            // lblMetricTotalValue
            // 
            this.lblMetricTotalValue.AutoSize = true;
            this.lblMetricTotalValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTotalValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricTotalValue.Location = new System.Drawing.Point(21, 62);
            this.lblMetricTotalValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricTotalValue.Name = "lblMetricTotalValue";
            this.lblMetricTotalValue.Size = new System.Drawing.Size(46, 54);
            this.lblMetricTotalValue.TabIndex = 2;
            this.lblMetricTotalValue.Text = "0";
            // 
            // lblMetricTotalTitle
            // 
            this.lblMetricTotalTitle.AutoSize = true;
            this.lblMetricTotalTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTotalTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricTotalTitle.Location = new System.Drawing.Point(21, 20);
            this.lblMetricTotalTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricTotalTitle.Name = "lblMetricTotalTitle";
            this.lblMetricTotalTitle.Size = new System.Drawing.Size(122, 23);
            this.lblMetricTotalTitle.TabIndex = 1;
            this.lblMetricTotalTitle.Text = "Total Members";
            // 
            // panelMetricIconTotal
            // 
            this.panelMetricIconTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.panelMetricIconTotal.Location = new System.Drawing.Point(187, 20);
            this.panelMetricIconTotal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelMetricIconTotal.Name = "panelMetricIconTotal";
            this.panelMetricIconTotal.Size = new System.Drawing.Size(53, 49);
            this.panelMetricIconTotal.TabIndex = 0;
            // 
            // panelHeader
            // 
            this.panelHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelHeader.Controls.Add(this.btnAddNewMember);
            this.panelHeader.Controls.Add(this.lblSubtitle);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Location = new System.Drawing.Point(32, 30);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1860, 143);
            this.panelHeader.TabIndex = 0;
            // 
            // btnAddNewMember
            // 
            this.btnAddNewMember.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNewMember.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.btnAddNewMember.FlatAppearance.BorderSize = 0;
            this.btnAddNewMember.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNewMember.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddNewMember.ForeColor = System.Drawing.Color.White;
            this.btnAddNewMember.Location = new System.Drawing.Point(1593, 20);
            this.btnAddNewMember.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAddNewMember.Name = "btnAddNewMember";
            this.btnAddNewMember.Size = new System.Drawing.Size(267, 49);
            this.btnAddNewMember.TabIndex = 2;
            this.btnAddNewMember.Text = "+ Add New Member";
            this.btnAddNewMember.UseVisualStyleBackColor = false;
            this.btnAddNewMember.Click += new System.EventHandler(this.btnAddNewMember_Click);
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblSubtitle.Location = new System.Drawing.Point(0, 62);
            this.lblSubtitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(350, 23);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Manage library members and their privileges";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(446, 54);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Member Management";
            // 
            // AdminMembersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1924, 1055);
            this.Controls.Add(this.panelMainContainer);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "AdminMembersForm";
            this.Text = "Member Management";
            this.Load += new System.EventHandler(this.AdminMembersForm_Load);
            this.panelMainContainer.ResumeLayout(false);
            this.panelTableContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMembers)).EndInit();
            this.panelSearchFilter.ResumeLayout(false);
            this.panelSearchFilter.PerformLayout();
            this.panelMetrics.ResumeLayout(false);
            this.panelMetricExpired.ResumeLayout(false);
            this.panelMetricExpired.PerformLayout();
            this.panelMetricSuspended.ResumeLayout(false);
            this.panelMetricSuspended.PerformLayout();
            this.panelMetricActive.ResumeLayout(false);
            this.panelMetricActive.PerformLayout();
            this.panelMetricTotal.ResumeLayout(false);
            this.panelMetricTotal.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMainContainer;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Button btnAddNewMember;
        private System.Windows.Forms.Panel panelMetrics;
        private System.Windows.Forms.Panel panelMetricTotal;
        private System.Windows.Forms.Label lblMetricTotalValue;
        private System.Windows.Forms.Label lblMetricTotalTitle;
        private System.Windows.Forms.Panel panelMetricIconTotal;
        private System.Windows.Forms.Panel panelMetricActive;
        private System.Windows.Forms.Label lblMetricActiveValue;
        private System.Windows.Forms.Label lblMetricActiveTitle;
        private System.Windows.Forms.Panel panelMetricIconActive;
        private System.Windows.Forms.Panel panelMetricSuspended;
        private System.Windows.Forms.Label lblMetricSuspendedValue;
        private System.Windows.Forms.Label lblMetricSuspendedTitle;
        private System.Windows.Forms.Panel panelMetricIconSuspended;
        private System.Windows.Forms.Panel panelMetricExpired;
        private System.Windows.Forms.Label lblMetricExpiredValue;
        private System.Windows.Forms.Label lblMetricExpiredTitle;
        private System.Windows.Forms.Panel panelMetricIconExpired;
        private System.Windows.Forms.Panel panelSearchFilter;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblTypeFilter;
        private System.Windows.Forms.ComboBox cmbTypeFilter;
        private System.Windows.Forms.Label lblStatusFilter;
        private System.Windows.Forms.ComboBox cmbStatusFilter;
        private System.Windows.Forms.Panel panelTableContainer;
        private System.Windows.Forms.DataGridView dataGridViewMembers;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMemberID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContact;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMemberType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBooks;
        private System.Windows.Forms.DataGridViewTextBoxColumn colExpires;
        private System.Windows.Forms.DataGridViewTextBoxColumn colActions;
    }
}
