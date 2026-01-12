namespace Project5LMS.Forms.Admin.Fines
{
    public partial class AdminFinesForm
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
            this.panelTableContainer = new System.Windows.Forms.Panel();
            this.dataGridViewFines = new System.Windows.Forms.DataGridView();
            this.colFineID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMember = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBookItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDaysOverdue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPaid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActions = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelFilters = new System.Windows.Forms.Panel();
            this.btnFilterType = new System.Windows.Forms.Button();
            this.btnFilterStatus = new System.Windows.Forms.Button();
            this.panelMetrics = new System.Windows.Forms.Panel();
            this.panelMetricTotalFines = new System.Windows.Forms.Panel();
            this.lblMetricTotalFinesTitle = new System.Windows.Forms.Label();
            this.lblMetricTotalFinesValue = new System.Windows.Forms.Label();
            this.panelMetricWaived = new System.Windows.Forms.Panel();
            this.lblMetricWaivedTitle = new System.Windows.Forms.Label();
            this.lblMetricWaivedValue = new System.Windows.Forms.Label();
            this.panelMetricCollected = new System.Windows.Forms.Panel();
            this.lblMetricCollectedTitle = new System.Windows.Forms.Label();
            this.lblMetricCollectedValue = new System.Windows.Forms.Label();
            this.panelMetricPending = new System.Windows.Forms.Panel();
            this.lblMetricPendingTitle = new System.Windows.Forms.Label();
            this.lblMetricPendingValue = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelMainContainer.SuspendLayout();
            this.panelTableContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFines)).BeginInit();
            this.panelFilters.SuspendLayout();
            this.panelMetrics.SuspendLayout();
            this.panelMetricTotalFines.SuspendLayout();
            this.panelMetricWaived.SuspendLayout();
            this.panelMetricCollected.SuspendLayout();
            this.panelMetricPending.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMainContainer
            // 
            this.panelMainContainer.AutoScroll = true;
            this.panelMainContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelMainContainer.Controls.Add(this.panelTableContainer);
            this.panelMainContainer.Controls.Add(this.panelFilters);
            this.panelMainContainer.Controls.Add(this.panelMetrics);
            this.panelMainContainer.Controls.Add(this.panelHeader);
            this.panelMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainContainer.Location = new System.Drawing.Point(0, 0);
            this.panelMainContainer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelMainContainer.Name = "panelMainContainer";
            this.panelMainContainer.Padding = new System.Windows.Forms.Padding(32, 30, 32, 30);
            this.panelMainContainer.Size = new System.Drawing.Size(1738, 953);
            this.panelMainContainer.TabIndex = 0;
            // 
            // panelTableContainer
            // 
            this.panelTableContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTableContainer.BackColor = System.Drawing.Color.White;
            this.panelTableContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTableContainer.Controls.Add(this.dataGridViewFines);
            this.panelTableContainer.Location = new System.Drawing.Point(32, 271);
            this.panelTableContainer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelTableContainer.Name = "panelTableContainer";
            this.panelTableContainer.Padding = new System.Windows.Forms.Padding(21, 20, 21, 20);
            this.panelTableContainer.Size = new System.Drawing.Size(1738, 711);
            this.panelTableContainer.TabIndex = 3;
            // 
            // dataGridViewFines
            // 
            this.dataGridViewFines.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.dataGridViewFines.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewFines.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewFines.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewFines.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewFines.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewFines.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFines.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFineID,
            this.colMember,
            this.colBookItem,
            this.colType,
            this.colDaysOverdue,
            this.colAmount,
            this.colPaid,
            this.colStatus,
            this.colActions});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewFines.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewFines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewFines.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dataGridViewFines.Location = new System.Drawing.Point(21, 20);
            this.dataGridViewFines.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridViewFines.MultiSelect = false;
            this.dataGridViewFines.Name = "dataGridViewFines";
            this.dataGridViewFines.RowHeadersVisible = false;
            this.dataGridViewFines.RowHeadersWidth = 51;
            this.dataGridViewFines.RowTemplate.Height = 60;
            this.dataGridViewFines.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewFines.Size = new System.Drawing.Size(1694, 669);
            this.dataGridViewFines.TabIndex = 0;
            this.dataGridViewFines.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewFines_CellContentClick_3);
            // 
            // colFineID
            // 
            this.colFineID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colFineID.DataPropertyName = "FineID";
            this.colFineID.HeaderText = "FINE ID";
            this.colFineID.MinimumWidth = 6;
            this.colFineID.Name = "colFineID";
            // 
            // colMember
            // 
            this.colMember.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colMember.DataPropertyName = "Member";
            this.colMember.HeaderText = "MEMBER";
            this.colMember.MinimumWidth = 6;
            this.colMember.Name = "colMember";
            // 
            // colBookItem
            // 
            this.colBookItem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colBookItem.DataPropertyName = "BookItem";
            this.colBookItem.HeaderText = "BOOK/ITEM";
            this.colBookItem.MinimumWidth = 6;
            this.colBookItem.Name = "colBookItem";
            // 
            // colType
            // 
            this.colType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colType.DataPropertyName = "Type";
            this.colType.HeaderText = "TYPE";
            this.colType.MinimumWidth = 6;
            this.colType.Name = "colType";
            // 
            // colDaysOverdue
            // 
            this.colDaysOverdue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDaysOverdue.DataPropertyName = "DaysOverdue";
            this.colDaysOverdue.HeaderText = "DAYS OVERDUE";
            this.colDaysOverdue.MinimumWidth = 6;
            this.colDaysOverdue.Name = "colDaysOverdue";
            // 
            // colAmount
            // 
            this.colAmount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colAmount.DataPropertyName = "Amount";
            this.colAmount.HeaderText = "AMOUNT";
            this.colAmount.MinimumWidth = 6;
            this.colAmount.Name = "colAmount";
            // 
            // colPaid
            // 
            this.colPaid.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colPaid.DataPropertyName = "Paid";
            this.colPaid.HeaderText = "PAID";
            this.colPaid.MinimumWidth = 6;
            this.colPaid.Name = "colPaid";
            // 
            // colStatus
            // 
            this.colStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colStatus.DataPropertyName = "Status";
            this.colStatus.HeaderText = "STATUS";
            this.colStatus.MinimumWidth = 6;
            this.colStatus.Name = "colStatus";
            // 
            // colActions
            // 
            this.colActions.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colActions.DataPropertyName = "Actions";
            this.colActions.HeaderText = "ACTIONS";
            this.colActions.MinimumWidth = 6;
            this.colActions.Name = "colActions";
            // 
            // panelFilters
            // 
            this.panelFilters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelFilters.Controls.Add(this.btnFilterType);
            this.panelFilters.Controls.Add(this.btnFilterStatus);
            this.panelFilters.Location = new System.Drawing.Point(32, 226);
            this.panelFilters.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Size = new System.Drawing.Size(1706, 43);
            this.panelFilters.TabIndex = 2;
            // 
            // btnFilterType
            // 
            this.btnFilterType.BackColor = System.Drawing.Color.White;
            this.btnFilterType.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnFilterType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilterType.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilterType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnFilterType.Location = new System.Drawing.Point(1503, 7);
            this.btnFilterType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFilterType.Name = "btnFilterType";
            this.btnFilterType.Size = new System.Drawing.Size(149, 31);
            this.btnFilterType.TabIndex = 1;
            this.btnFilterType.Text = "🔽 All Types";
            this.btnFilterType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFilterType.UseVisualStyleBackColor = false;
            this.btnFilterType.Click += new System.EventHandler(this.btnFilterType_Click);
            // 
            // btnFilterStatus
            // 
            this.btnFilterStatus.BackColor = System.Drawing.Color.White;
            this.btnFilterStatus.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnFilterStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilterStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilterStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnFilterStatus.Location = new System.Drawing.Point(1345, 7);
            this.btnFilterStatus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFilterStatus.Name = "btnFilterStatus";
            this.btnFilterStatus.Size = new System.Drawing.Size(149, 31);
            this.btnFilterStatus.TabIndex = 0;
            this.btnFilterStatus.Text = "🔽 All Status";
            this.btnFilterStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFilterStatus.UseVisualStyleBackColor = false;
            this.btnFilterStatus.Click += new System.EventHandler(this.btnFilterStatus_Click);
            // 
            // panelMetrics
            // 
            this.panelMetrics.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMetrics.BackColor = System.Drawing.Color.White;
            this.panelMetrics.Controls.Add(this.panelMetricTotalFines);
            this.panelMetrics.Controls.Add(this.panelMetricWaived);
            this.panelMetrics.Controls.Add(this.panelMetricCollected);
            this.panelMetrics.Controls.Add(this.panelMetricPending);
            this.panelMetrics.Location = new System.Drawing.Point(32, 140);
            this.panelMetrics.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelMetrics.Name = "panelMetrics";
            this.panelMetrics.Size = new System.Drawing.Size(1706, 86);
            this.panelMetrics.TabIndex = 1;
            // 
            // panelMetricTotalFines
            // 
            this.panelMetricTotalFines.BackColor = System.Drawing.Color.White;
            this.panelMetricTotalFines.Controls.Add(this.lblMetricTotalFinesTitle);
            this.panelMetricTotalFines.Controls.Add(this.lblMetricTotalFinesValue);
            this.panelMetricTotalFines.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelMetricTotalFines.Location = new System.Drawing.Point(1227, 0);
            this.panelMetricTotalFines.Margin = new System.Windows.Forms.Padding(0, 0, 21, 0);
            this.panelMetricTotalFines.Name = "panelMetricTotalFines";
            this.panelMetricTotalFines.Padding = new System.Windows.Forms.Padding(21, 20, 21, 20);
            this.panelMetricTotalFines.Size = new System.Drawing.Size(320, 86);
            this.panelMetricTotalFines.TabIndex = 3;
            // 
            // lblMetricTotalFinesTitle
            // 
            this.lblMetricTotalFinesTitle.AutoSize = true;
            this.lblMetricTotalFinesTitle.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTotalFinesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricTotalFinesTitle.Location = new System.Drawing.Point(29, 15);
            this.lblMetricTotalFinesTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricTotalFinesTitle.Name = "lblMetricTotalFinesTitle";
            this.lblMetricTotalFinesTitle.Size = new System.Drawing.Size(126, 25);
            this.lblMetricTotalFinesTitle.TabIndex = 1;
            this.lblMetricTotalFinesTitle.Text = "💰 Total Fines";
            this.lblMetricTotalFinesTitle.Click += new System.EventHandler(this.lblMetricTotalFinesTitle_Click);
            // 
            // lblMetricTotalFinesValue
            // 
            this.lblMetricTotalFinesValue.AutoSize = true;
            this.lblMetricTotalFinesValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTotalFinesValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricTotalFinesValue.Location = new System.Drawing.Point(44, 34);
            this.lblMetricTotalFinesValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricTotalFinesValue.Name = "lblMetricTotalFinesValue";
            this.lblMetricTotalFinesValue.Size = new System.Drawing.Size(46, 54);
            this.lblMetricTotalFinesValue.TabIndex = 2;
            this.lblMetricTotalFinesValue.Text = "0";
            // 
            // panelMetricWaived
            // 
            this.panelMetricWaived.BackColor = System.Drawing.Color.White;
            this.panelMetricWaived.Controls.Add(this.lblMetricWaivedTitle);
            this.panelMetricWaived.Controls.Add(this.lblMetricWaivedValue);
            this.panelMetricWaived.Location = new System.Drawing.Point(867, 0);
            this.panelMetricWaived.Margin = new System.Windows.Forms.Padding(0, 0, 21, 0);
            this.panelMetricWaived.Name = "panelMetricWaived";
            this.panelMetricWaived.Padding = new System.Windows.Forms.Padding(21, 20, 21, 20);
            this.panelMetricWaived.Size = new System.Drawing.Size(320, 86);
            this.panelMetricWaived.TabIndex = 2;
            // 
            // lblMetricWaivedTitle
            // 
            this.lblMetricWaivedTitle.AutoSize = true;
            this.lblMetricWaivedTitle.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricWaivedTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricWaivedTitle.Location = new System.Drawing.Point(21, 15);
            this.lblMetricWaivedTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricWaivedTitle.Name = "lblMetricWaivedTitle";
            this.lblMetricWaivedTitle.Size = new System.Drawing.Size(100, 25);
            this.lblMetricWaivedTitle.TabIndex = 1;
            this.lblMetricWaivedTitle.Text = "✅ Waived";
            this.lblMetricWaivedTitle.Click += new System.EventHandler(this.lblMetricWaivedTitle_Click);
            // 
            // lblMetricWaivedValue
            // 
            this.lblMetricWaivedValue.AutoSize = true;
            this.lblMetricWaivedValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricWaivedValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricWaivedValue.Location = new System.Drawing.Point(45, 34);
            this.lblMetricWaivedValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricWaivedValue.Name = "lblMetricWaivedValue";
            this.lblMetricWaivedValue.Size = new System.Drawing.Size(46, 54);
            this.lblMetricWaivedValue.TabIndex = 2;
            this.lblMetricWaivedValue.Text = "0";
            this.lblMetricWaivedValue.Click += new System.EventHandler(this.lblMetricWaivedValue_Click);
            // 
            // panelMetricCollected
            // 
            this.panelMetricCollected.BackColor = System.Drawing.Color.White;
            this.panelMetricCollected.Controls.Add(this.lblMetricCollectedTitle);
            this.panelMetricCollected.Controls.Add(this.lblMetricCollectedValue);
            this.panelMetricCollected.Location = new System.Drawing.Point(499, 0);
            this.panelMetricCollected.Margin = new System.Windows.Forms.Padding(0, 0, 21, 0);
            this.panelMetricCollected.Name = "panelMetricCollected";
            this.panelMetricCollected.Padding = new System.Windows.Forms.Padding(21, 20, 21, 20);
            this.panelMetricCollected.Size = new System.Drawing.Size(320, 86);
            this.panelMetricCollected.TabIndex = 1;
            // 
            // lblMetricCollectedTitle
            // 
            this.lblMetricCollectedTitle.AutoSize = true;
            this.lblMetricCollectedTitle.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricCollectedTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricCollectedTitle.Location = new System.Drawing.Point(25, 10);
            this.lblMetricCollectedTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricCollectedTitle.Name = "lblMetricCollectedTitle";
            this.lblMetricCollectedTitle.Size = new System.Drawing.Size(197, 25);
            this.lblMetricCollectedTitle.TabIndex = 1;
            this.lblMetricCollectedTitle.Text = "💵 Collected                ";
            this.lblMetricCollectedTitle.Click += new System.EventHandler(this.lblMetricCollectedTitle_Click);
            // 
            // lblMetricCollectedValue
            // 
            this.lblMetricCollectedValue.AutoSize = true;
            this.lblMetricCollectedValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricCollectedValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricCollectedValue.Location = new System.Drawing.Point(53, 34);
            this.lblMetricCollectedValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricCollectedValue.Name = "lblMetricCollectedValue";
            this.lblMetricCollectedValue.Size = new System.Drawing.Size(46, 54);
            this.lblMetricCollectedValue.TabIndex = 2;
            this.lblMetricCollectedValue.Text = "0";
            // 
            // panelMetricPending
            // 
            this.panelMetricPending.BackColor = System.Drawing.Color.White;
            this.panelMetricPending.Controls.Add(this.lblMetricPendingTitle);
            this.panelMetricPending.Controls.Add(this.lblMetricPendingValue);
            this.panelMetricPending.Location = new System.Drawing.Point(138, 0);
            this.panelMetricPending.Margin = new System.Windows.Forms.Padding(0, 0, 21, 0);
            this.panelMetricPending.Name = "panelMetricPending";
            this.panelMetricPending.Padding = new System.Windows.Forms.Padding(21, 20, 21, 20);
            this.panelMetricPending.Size = new System.Drawing.Size(320, 86);
            this.panelMetricPending.TabIndex = 0;
            // 
            // lblMetricPendingTitle
            // 
            this.lblMetricPendingTitle.AutoSize = true;
            this.lblMetricPendingTitle.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricPendingTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricPendingTitle.Location = new System.Drawing.Point(57, 15);
            this.lblMetricPendingTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricPendingTitle.Name = "lblMetricPendingTitle";
            this.lblMetricPendingTitle.Size = new System.Drawing.Size(152, 25);
            this.lblMetricPendingTitle.TabIndex = 1;
            this.lblMetricPendingTitle.Text = "⏳ Total Pending";
            this.lblMetricPendingTitle.Click += new System.EventHandler(this.lblMetricPendingTitle_Click);
            // 
            // lblMetricPendingValue
            // 
            this.lblMetricPendingValue.AutoSize = true;
            this.lblMetricPendingValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricPendingValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricPendingValue.Location = new System.Drawing.Point(81, 34);
            this.lblMetricPendingValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricPendingValue.Name = "lblMetricPendingValue";
            this.lblMetricPendingValue.Size = new System.Drawing.Size(46, 54);
            this.lblMetricPendingValue.TabIndex = 2;
            this.lblMetricPendingValue.Text = "0";
            this.lblMetricPendingValue.Click += new System.EventHandler(this.lblMetricPendingValue_Click);
            // 
            // panelHeader
            // 
            this.panelHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelHeader.Controls.Add(this.lblSubtitle);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Location = new System.Drawing.Point(32, 30);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1706, 110);
            this.panelHeader.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblSubtitle.Location = new System.Drawing.Point(19, 73);
            this.lblSubtitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(349, 23);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Track and manage library fines and penalties";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitle.Location = new System.Drawing.Point(13, 9);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(598, 54);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Fine and Penalty Management";
            // 
            // AdminFinesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1688, 980);
            this.Controls.Add(this.panelMainContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "AdminFinesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fine & Penalty Management";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AdminFinesForm_Load);
            this.panelMainContainer.ResumeLayout(false);
            this.panelTableContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFines)).EndInit();
            this.panelFilters.ResumeLayout(false);
            this.panelMetrics.ResumeLayout(false);
            this.panelMetricTotalFines.ResumeLayout(false);
            this.panelMetricTotalFines.PerformLayout();
            this.panelMetricWaived.ResumeLayout(false);
            this.panelMetricWaived.PerformLayout();
            this.panelMetricCollected.ResumeLayout(false);
            this.panelMetricCollected.PerformLayout();
            this.panelMetricPending.ResumeLayout(false);
            this.panelMetricPending.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.Panel panelMainContainer;
        private System.Windows.Forms.Panel panelFilters;
        private System.Windows.Forms.Button btnFilterType;
        private System.Windows.Forms.Button btnFilterStatus;
        private System.Windows.Forms.Panel panelMetrics;
        private System.Windows.Forms.Panel panelMetricTotalFines;
        private System.Windows.Forms.Label lblMetricTotalFinesTitle;
        private System.Windows.Forms.Label lblMetricTotalFinesValue;
        private System.Windows.Forms.Panel panelMetricWaived;
        private System.Windows.Forms.Label lblMetricWaivedTitle;
        private System.Windows.Forms.Label lblMetricWaivedValue;
        private System.Windows.Forms.Panel panelMetricCollected;
        private System.Windows.Forms.Label lblMetricCollectedTitle;
        private System.Windows.Forms.Label lblMetricCollectedValue;
        private System.Windows.Forms.Panel panelMetricPending;
        private System.Windows.Forms.Label lblMetricPendingTitle;
        private System.Windows.Forms.Label lblMetricPendingValue;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelTableContainer;
        private System.Windows.Forms.DataGridView dataGridViewFines;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFineID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMember;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBookItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDaysOverdue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colActions;
    }
}