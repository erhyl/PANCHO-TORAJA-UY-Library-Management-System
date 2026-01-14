namespace Project5LMS.Forms.Admin.Inventory
{
    partial class AdminInventoryForm
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
            this.dataGridViewInventory = new System.Windows.Forms.DataGridView();
            this.colInventoryID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBookDetails = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCopy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCondition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastVerified = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActions = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelSearchFilter = new System.Windows.Forms.Panel();
            this.btnFilterStatus = new System.Windows.Forms.Button();
            this.btnFilterCondition = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.panelMetrics = new System.Windows.Forms.Panel();
            this.panelMetricLost = new System.Windows.Forms.Panel();
            this.lblMetricLostValue = new System.Windows.Forms.Label();
            this.lblMetricLostTitle = new System.Windows.Forms.Label();
            this.panelMetricDamaged = new System.Windows.Forms.Panel();
            this.lblMetricDamagedValue = new System.Windows.Forms.Label();
            this.lblMetricDamagedTitle = new System.Windows.Forms.Label();
            this.panelMetricNeedsRepair = new System.Windows.Forms.Panel();
            this.lblMetricNeedsRepairValue = new System.Windows.Forms.Label();
            this.lblMetricNeedsRepairTitle = new System.Windows.Forms.Label();
            this.panelMetricTotal = new System.Windows.Forms.Panel();
            this.lblMetricTotalValue = new System.Windows.Forms.Label();
            this.lblMetricTotalTitle = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelMainContainer.SuspendLayout();
            this.panelTableContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInventory)).BeginInit();
            this.panelSearchFilter.SuspendLayout();
            this.panelMetrics.SuspendLayout();
            this.panelMetricLost.SuspendLayout();
            this.panelMetricDamaged.SuspendLayout();
            this.panelMetricNeedsRepair.SuspendLayout();
            this.panelMetricTotal.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMainContainer
            // 
            this.panelMainContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelMainContainer.Controls.Add(this.panelTableContainer);
            this.panelMainContainer.Controls.Add(this.panelSearchFilter);
            this.panelMainContainer.Controls.Add(this.panelMetrics);
            this.panelMainContainer.Controls.Add(this.panelHeader);
            this.panelMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainContainer.Location = new System.Drawing.Point(0, 0);
            this.panelMainContainer.Name = "panelMainContainer";
            this.panelMainContainer.Padding = new System.Windows.Forms.Padding(24, 24, 24, 24);
            this.panelMainContainer.Size = new System.Drawing.Size(1280, 800);
            this.panelMainContainer.TabIndex = 0;
            // 
            // panelTableContainer
            // 
            this.panelTableContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTableContainer.AutoScroll = true;
            this.panelTableContainer.BackColor = System.Drawing.Color.White;
            this.panelTableContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTableContainer.Controls.Add(this.dataGridViewInventory);
            this.panelTableContainer.Location = new System.Drawing.Point(24, 202);
            this.panelTableContainer.Name = "panelTableContainer";
            this.panelTableContainer.Padding = new System.Windows.Forms.Padding(16, 16, 16, 16);
            this.panelTableContainer.Size = new System.Drawing.Size(1233, 598);
            this.panelTableContainer.TabIndex = 3;
            this.panelTableContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.panelTableContainer_Paint);
            // 
            // dataGridViewInventory
            // 
            this.dataGridViewInventory.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.dataGridViewInventory.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewInventory.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewInventory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewInventory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewInventory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewInventory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colInventoryID,
            this.colBookDetails,
            this.colCategory,
            this.colLocation,
            this.colCopy,
            this.colCondition,
            this.colStatus,
            this.colLastVerified,
            this.colActions});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewInventory.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewInventory.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewInventory.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dataGridViewInventory.Location = new System.Drawing.Point(16, 16);
            this.dataGridViewInventory.MultiSelect = false;
            this.dataGridViewInventory.Name = "dataGridViewInventory";
            this.dataGridViewInventory.RowHeadersVisible = false;
            this.dataGridViewInventory.RowHeadersWidth = 51;
            this.dataGridViewInventory.RowTemplate.Height = 60;
            this.dataGridViewInventory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewInventory.Size = new System.Drawing.Size(1268, 564);
            this.dataGridViewInventory.TabIndex = 0;
            // CellContentClick is handled in SetupDataGridView() method
            // 
            // Column definitions are placeholders - columns are created programmatically
            // in SetupDataGridView() method to ensure proper sizing and content display
            // 
            // colInventoryID
            // 
            this.colInventoryID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colInventoryID.DataPropertyName = "InventoryID";
            this.colInventoryID.HeaderText = "INVENTORY ID";
            this.colInventoryID.MinimumWidth = 120;
            this.colInventoryID.Name = "colInventoryID";
            this.colInventoryID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // colBookDetails
            // 
            this.colBookDetails.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colBookDetails.DataPropertyName = "BookDetails";
            this.colBookDetails.HeaderText = "BOOK DETAILS";
            this.colBookDetails.MinimumWidth = 250;
            this.colBookDetails.Name = "colBookDetails";
            // 
            // colCategory
            // 
            this.colCategory.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colCategory.DataPropertyName = "Category";
            this.colCategory.HeaderText = "CATEGORY";
            this.colCategory.MinimumWidth = 120;
            this.colCategory.Name = "colCategory";
            // 
            // colLocation
            // 
            this.colLocation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colLocation.DataPropertyName = "Location";
            this.colLocation.HeaderText = "LOCATION";
            this.colLocation.MinimumWidth = 100;
            this.colLocation.Name = "colLocation";
            // 
            // colCopy
            // 
            this.colCopy.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colCopy.DataPropertyName = "Copy";
            this.colCopy.HeaderText = "COPY";
            this.colCopy.MinimumWidth = 80;
            this.colCopy.Name = "colCopy";
            // 
            // colCondition
            // 
            this.colCondition.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colCondition.DataPropertyName = "Condition";
            this.colCondition.HeaderText = "CONDITION";
            this.colCondition.MinimumWidth = 120;
            this.colCondition.Name = "colCondition";
            // 
            // colStatus
            // 
            this.colStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colStatus.DataPropertyName = "Status";
            this.colStatus.HeaderText = "STATUS";
            this.colStatus.MinimumWidth = 120;
            this.colStatus.Name = "colStatus";
            // 
            // colLastVerified
            // 
            this.colLastVerified.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colLastVerified.DataPropertyName = "LastVerified";
            this.colLastVerified.HeaderText = "LAST VERIFIED";
            this.colLastVerified.MinimumWidth = 130;
            this.colLastVerified.Name = "colLastVerified";
            // 
            // colActions
            // 
            this.colActions.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colActions.DataPropertyName = "Actions";
            this.colActions.HeaderText = "ACTIONS";
            this.colActions.Width = 180;
            this.colActions.MinimumWidth = 180;
            this.colActions.Name = "colActions";
            // 
            // panelSearchFilter
            // 
            this.panelSearchFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSearchFilter.Controls.Add(this.btnFilterStatus);
            this.panelSearchFilter.Controls.Add(this.btnFilterCondition);
            this.panelSearchFilter.Controls.Add(this.txtSearch);
            this.panelSearchFilter.Location = new System.Drawing.Point(24, 161);
            this.panelSearchFilter.Name = "panelSearchFilter";
            this.panelSearchFilter.Size = new System.Drawing.Size(1232, 35);
            this.panelSearchFilter.TabIndex = 2;
            // 
            // btnFilterStatus
            // 
            this.btnFilterStatus.BackColor = System.Drawing.Color.White;
            this.btnFilterStatus.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnFilterStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilterStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilterStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnFilterStatus.Location = new System.Drawing.Point(1035, 6);
            this.btnFilterStatus.Name = "btnFilterStatus";
            this.btnFilterStatus.Size = new System.Drawing.Size(117, 23);
            this.btnFilterStatus.TabIndex = 2;
            this.btnFilterStatus.Text = " All Status";
            this.btnFilterStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFilterStatus.UseVisualStyleBackColor = false;
            this.btnFilterStatus.Click += new System.EventHandler(this.btnFilterStatus_Click);
            // 
            // btnFilterCondition
            // 
            this.btnFilterCondition.BackColor = System.Drawing.Color.White;
            this.btnFilterCondition.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnFilterCondition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilterCondition.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilterCondition.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnFilterCondition.Location = new System.Drawing.Point(918, 6);
            this.btnFilterCondition.Name = "btnFilterCondition";
            this.btnFilterCondition.Size = new System.Drawing.Size(122, 23);
            this.btnFilterCondition.TabIndex = 1;
            this.btnFilterCondition.Text = " All Conditions";
            this.btnFilterCondition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFilterCondition.UseVisualStyleBackColor = false;
            this.btnFilterCondition.Click += new System.EventHandler(this.btnFilterCondition_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.Color.Gray;
            this.txtSearch.Location = new System.Drawing.Point(6, 4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(543, 25);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.Text = " Search inventory...";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            // 
            // panelMetrics
            // 
            this.panelMetrics.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMetrics.BackColor = System.Drawing.Color.White;
            this.panelMetrics.Controls.Add(this.panelMetricLost);
            this.panelMetrics.Controls.Add(this.panelMetricDamaged);
            this.panelMetrics.Controls.Add(this.panelMetricNeedsRepair);
            this.panelMetrics.Controls.Add(this.panelMetricTotal);
            this.panelMetrics.Location = new System.Drawing.Point(24, 98);
            this.panelMetrics.Name = "panelMetrics";
            this.panelMetrics.Size = new System.Drawing.Size(1232, 63);
            this.panelMetrics.TabIndex = 1;
            this.panelMetrics.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMetrics_Paint);
            // 
            // panelMetricLost
            // 
            this.panelMetricLost.BackColor = System.Drawing.Color.White;
            this.panelMetricLost.Controls.Add(this.lblMetricLostValue);
            this.panelMetricLost.Controls.Add(this.lblMetricLostTitle);
            this.panelMetricLost.Location = new System.Drawing.Point(918, 1);
            this.panelMetricLost.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.panelMetricLost.Name = "panelMetricLost";
            this.panelMetricLost.Padding = new System.Windows.Forms.Padding(16, 16, 16, 16);
            this.panelMetricLost.Size = new System.Drawing.Size(234, 63);
            this.panelMetricLost.TabIndex = 3;
            // 
            // lblMetricLostValue
            // 
            this.lblMetricLostValue.AutoSize = true;
            this.lblMetricLostValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricLostValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricLostValue.Location = new System.Drawing.Point(19, 24);
            this.lblMetricLostValue.Name = "lblMetricLostValue";
            this.lblMetricLostValue.Size = new System.Drawing.Size(33, 37);
            this.lblMetricLostValue.TabIndex = 2;
            this.lblMetricLostValue.Text = "0";
            // 
            // lblMetricLostTitle
            // 
            this.lblMetricLostTitle.AutoSize = true;
            this.lblMetricLostTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricLostTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricLostTitle.Location = new System.Drawing.Point(11, 8);
            this.lblMetricLostTitle.Name = "lblMetricLostTitle";
            this.lblMetricLostTitle.Size = new System.Drawing.Size(76, 15);
            this.lblMetricLostTitle.TabIndex = 1;
            this.lblMetricLostTitle.Text = "❌ Lost Items";
            // 
            // panelMetricDamaged
            // 
            this.panelMetricDamaged.BackColor = System.Drawing.Color.White;
            this.panelMetricDamaged.Controls.Add(this.lblMetricDamagedValue);
            this.panelMetricDamaged.Controls.Add(this.lblMetricDamagedTitle);
            this.panelMetricDamaged.Location = new System.Drawing.Point(620, 0);
            this.panelMetricDamaged.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.panelMetricDamaged.Name = "panelMetricDamaged";
            this.panelMetricDamaged.Padding = new System.Windows.Forms.Padding(16, 16, 16, 16);
            this.panelMetricDamaged.Size = new System.Drawing.Size(234, 63);
            this.panelMetricDamaged.TabIndex = 2;
            // 
            // lblMetricDamagedValue
            // 
            this.lblMetricDamagedValue.AutoSize = true;
            this.lblMetricDamagedValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricDamagedValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricDamagedValue.Location = new System.Drawing.Point(13, 24);
            this.lblMetricDamagedValue.Name = "lblMetricDamagedValue";
            this.lblMetricDamagedValue.Size = new System.Drawing.Size(33, 37);
            this.lblMetricDamagedValue.TabIndex = 2;
            this.lblMetricDamagedValue.Text = "0";
            // 
            // lblMetricDamagedTitle
            // 
            this.lblMetricDamagedTitle.AutoSize = true;
            this.lblMetricDamagedTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricDamagedTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricDamagedTitle.Location = new System.Drawing.Point(16, 8);
            this.lblMetricDamagedTitle.Name = "lblMetricDamagedTitle";
            this.lblMetricDamagedTitle.Size = new System.Drawing.Size(73, 15);
            this.lblMetricDamagedTitle.TabIndex = 1;
            this.lblMetricDamagedTitle.Text = "⚠️ Damaged";
            // 
            // panelMetricNeedsRepair
            // 
            this.panelMetricNeedsRepair.BackColor = System.Drawing.Color.White;
            this.panelMetricNeedsRepair.Controls.Add(this.lblMetricNeedsRepairValue);
            this.panelMetricNeedsRepair.Controls.Add(this.lblMetricNeedsRepairTitle);
            this.panelMetricNeedsRepair.Location = new System.Drawing.Point(314, 1);
            this.panelMetricNeedsRepair.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.panelMetricNeedsRepair.Name = "panelMetricNeedsRepair";
            this.panelMetricNeedsRepair.Padding = new System.Windows.Forms.Padding(16, 16, 16, 16);
            this.panelMetricNeedsRepair.Size = new System.Drawing.Size(234, 62);
            this.panelMetricNeedsRepair.TabIndex = 1;
            // 
            // lblMetricNeedsRepairValue
            // 
            this.lblMetricNeedsRepairValue.AutoSize = true;
            this.lblMetricNeedsRepairValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricNeedsRepairValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricNeedsRepairValue.Location = new System.Drawing.Point(13, 24);
            this.lblMetricNeedsRepairValue.Name = "lblMetricNeedsRepairValue";
            this.lblMetricNeedsRepairValue.Size = new System.Drawing.Size(33, 37);
            this.lblMetricNeedsRepairValue.TabIndex = 2;
            this.lblMetricNeedsRepairValue.Text = "0";
            // 
            // lblMetricNeedsRepairTitle
            // 
            this.lblMetricNeedsRepairTitle.AutoSize = true;
            this.lblMetricNeedsRepairTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricNeedsRepairTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricNeedsRepairTitle.Location = new System.Drawing.Point(16, 8);
            this.lblMetricNeedsRepairTitle.Name = "lblMetricNeedsRepairTitle";
            this.lblMetricNeedsRepairTitle.Size = new System.Drawing.Size(91, 15);
            this.lblMetricNeedsRepairTitle.TabIndex = 1;
            this.lblMetricNeedsRepairTitle.Text = "🔧 Needs Repair";
            // 
            // panelMetricTotal
            // 
            this.panelMetricTotal.BackColor = System.Drawing.Color.White;
            this.panelMetricTotal.Controls.Add(this.lblMetricTotalValue);
            this.panelMetricTotal.Controls.Add(this.lblMetricTotalTitle);
            this.panelMetricTotal.Location = new System.Drawing.Point(0, 0);
            this.panelMetricTotal.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.panelMetricTotal.Name = "panelMetricTotal";
            this.panelMetricTotal.Padding = new System.Windows.Forms.Padding(16, 16, 16, 16);
            this.panelMetricTotal.Size = new System.Drawing.Size(234, 63);
            this.panelMetricTotal.TabIndex = 0;
            // 
            // lblMetricTotalValue
            // 
            this.lblMetricTotalValue.AutoSize = true;
            this.lblMetricTotalValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTotalValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricTotalValue.Location = new System.Drawing.Point(16, 24);
            this.lblMetricTotalValue.Name = "lblMetricTotalValue";
            this.lblMetricTotalValue.Size = new System.Drawing.Size(33, 37);
            this.lblMetricTotalValue.TabIndex = 2;
            this.lblMetricTotalValue.Text = "0";
            // 
            // lblMetricTotalTitle
            // 
            this.lblMetricTotalTitle.AutoSize = true;
            this.lblMetricTotalTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTotalTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricTotalTitle.Location = new System.Drawing.Point(13, 8);
            this.lblMetricTotalTitle.Name = "lblMetricTotalTitle";
            this.lblMetricTotalTitle.Size = new System.Drawing.Size(80, 15);
            this.lblMetricTotalTitle.TabIndex = 1;
            this.lblMetricTotalTitle.Text = "📦 Total Items";
            // 
            // panelHeader
            // 
            this.panelHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelHeader.Controls.Add(this.lblSubtitle);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Location = new System.Drawing.Point(24, 24);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1232, 74);
            this.panelHeader.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblSubtitle.Location = new System.Drawing.Point(3, 44);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(347, 19);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Track and manage library book inventory and condition";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitle.Location = new System.Drawing.Point(3, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(372, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Inventory Management";
            // 
            // AdminInventoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1280, 800);
            this.Controls.Add(this.panelMainContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AdminInventoryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inventory Management";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AdminInventoryForm_Load);
            this.panelMainContainer.ResumeLayout(false);
            this.panelTableContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInventory)).EndInit();
            this.panelSearchFilter.ResumeLayout(false);
            this.panelSearchFilter.PerformLayout();
            this.panelMetrics.ResumeLayout(false);
            this.panelMetricLost.ResumeLayout(false);
            this.panelMetricLost.PerformLayout();
            this.panelMetricDamaged.ResumeLayout(false);
            this.panelMetricDamaged.PerformLayout();
            this.panelMetricNeedsRepair.ResumeLayout(false);
            this.panelMetricNeedsRepair.PerformLayout();
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
        private System.Windows.Forms.Panel panelMetrics;
        private System.Windows.Forms.Panel panelMetricTotal;
        private System.Windows.Forms.Label lblMetricTotalValue;
        private System.Windows.Forms.Label lblMetricTotalTitle;
        private System.Windows.Forms.Panel panelMetricNeedsRepair;
        private System.Windows.Forms.Label lblMetricNeedsRepairValue;
        private System.Windows.Forms.Label lblMetricNeedsRepairTitle;
        private System.Windows.Forms.Panel panelMetricDamaged;
        private System.Windows.Forms.Label lblMetricDamagedValue;
        private System.Windows.Forms.Label lblMetricDamagedTitle;
        private System.Windows.Forms.Panel panelMetricLost;
        private System.Windows.Forms.Label lblMetricLostValue;
        private System.Windows.Forms.Label lblMetricLostTitle;
        private System.Windows.Forms.Panel panelSearchFilter;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnFilterCondition;
        private System.Windows.Forms.Button btnFilterStatus;
        private System.Windows.Forms.Panel panelTableContainer;
        private System.Windows.Forms.DataGridView dataGridViewInventory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInventoryID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBookDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCopy;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCondition;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastVerified;
        private System.Windows.Forms.DataGridViewTextBoxColumn colActions;
    }
}