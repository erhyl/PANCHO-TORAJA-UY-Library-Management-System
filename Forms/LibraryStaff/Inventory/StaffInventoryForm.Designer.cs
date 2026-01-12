namespace Project5LMS.Forms.LibraryStaff.Inventory
{
    partial class StaffInventoryForm
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
            this.panelInventoryTable = new System.Windows.Forms.Panel();
            this.panelFilter = new System.Windows.Forms.Panel();
            this.cmbCategoryFilter = new System.Windows.Forms.ComboBox();
            this.lblCategoryFilter = new System.Windows.Forms.Label();
            this.dataGridViewInventory = new System.Windows.Forms.DataGridView();
            this.colBookID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAvailable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCheckedOut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDamaged = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastUpdated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelMetrics = new System.Windows.Forms.Panel();
            this.cardLost = new System.Windows.Forms.Panel();
            this.lblLostValue = new System.Windows.Forms.Label();
            this.lblLostLabel = new System.Windows.Forms.Label();
            this.cardDamaged = new System.Windows.Forms.Panel();
            this.lblDamagedValue = new System.Windows.Forms.Label();
            this.lblDamagedLabel = new System.Windows.Forms.Label();
            this.cardCheckedOut = new System.Windows.Forms.Panel();
            this.lblCheckedOutValue = new System.Windows.Forms.Label();
            this.lblCheckedOutLabel = new System.Windows.Forms.Label();
            this.cardAvailable = new System.Windows.Forms.Panel();
            this.lblAvailableValue = new System.Windows.Forms.Label();
            this.lblAvailableLabel = new System.Windows.Forms.Label();
            this.cardTotalCopies = new System.Windows.Forms.Panel();
            this.lblTotalCopiesValue = new System.Windows.Forms.Label();
            this.lblTotalCopiesLabel = new System.Windows.Forms.Label();
            this.cardUpdateStock = new System.Windows.Forms.Panel();
            this.btnAddCopies = new System.Windows.Forms.Button();
            this.txtUpdateStockQuantity = new System.Windows.Forms.TextBox();
            this.txtUpdateStockBookID = new System.Windows.Forms.TextBox();
            this.lblUpdateStockTitle = new System.Windows.Forms.Label();
            this.panelInventoryManagement = new System.Windows.Forms.Panel();
            this.btnAddInventory = new System.Windows.Forms.Button();
            this.panelActionCards = new System.Windows.Forms.Panel();
            this.cardReportLost = new System.Windows.Forms.Panel();
            this.btnReportLost = new System.Windows.Forms.Button();
            this.txtReportLostNotes = new System.Windows.Forms.TextBox();
            this.txtReportLostBookID = new System.Windows.Forms.TextBox();
            this.lblReportLostTitle = new System.Windows.Forms.Label();
            this.cardReportDamage = new System.Windows.Forms.Panel();
            this.btnReportDamage = new System.Windows.Forms.Button();
            this.txtReportDamageDescription = new System.Windows.Forms.TextBox();
            this.txtReportDamageBookID = new System.Windows.Forms.TextBox();
            this.lblReportDamageTitle = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.panelMainContainer.SuspendLayout();
            this.panelInventoryTable.SuspendLayout();
            this.panelFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInventory)).BeginInit();
            this.panelMetrics.SuspendLayout();
            this.cardLost.SuspendLayout();
            this.cardDamaged.SuspendLayout();
            this.cardCheckedOut.SuspendLayout();
            this.cardAvailable.SuspendLayout();
            this.cardTotalCopies.SuspendLayout();
            this.cardUpdateStock.SuspendLayout();
            this.panelInventoryManagement.SuspendLayout();
            this.panelActionCards.SuspendLayout();
            this.cardReportLost.SuspendLayout();
            this.cardReportDamage.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMainContainer
            // 
            this.panelMainContainer.AutoScroll = true;
            this.panelMainContainer.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelMainContainer.Controls.Add(this.panelInventoryTable);
            this.panelMainContainer.Controls.Add(this.panelMetrics);
            this.panelMainContainer.Controls.Add(this.cardUpdateStock);
            this.panelMainContainer.Controls.Add(this.panelInventoryManagement);
            this.panelMainContainer.Controls.Add(this.panelHeader);
            this.panelMainContainer.Location = new System.Drawing.Point(0, 0);
            this.panelMainContainer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelMainContainer.Name = "panelMainContainer";
            this.panelMainContainer.Padding = new System.Windows.Forms.Padding(32, 30, 32, 30);
            this.panelMainContainer.Size = new System.Drawing.Size(1600, 985);
            this.panelMainContainer.TabIndex = 0;
            this.panelMainContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMainContainer_Paint);
            // 
            // panelInventoryTable
            // 
            this.panelInventoryTable.Controls.Add(this.panelFilter);
            this.panelInventoryTable.Controls.Add(this.dataGridViewInventory);
            this.panelInventoryTable.Location = new System.Drawing.Point(32, 524);
            this.panelInventoryTable.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelInventoryTable.Name = "panelInventoryTable";
            this.panelInventoryTable.Padding = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.panelInventoryTable.Size = new System.Drawing.Size(1536, 431);
            this.panelInventoryTable.TabIndex = 5;
            // 
            // panelFilter
            // 
            this.panelFilter.BackColor = System.Drawing.Color.White;
            this.panelFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFilter.Controls.Add(this.cmbCategoryFilter);
            this.panelFilter.Controls.Add(this.lblCategoryFilter);
            this.panelFilter.Location = new System.Drawing.Point(1, 16);
            this.panelFilter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Padding = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.panelFilter.Size = new System.Drawing.Size(1535, 51);
            this.panelFilter.TabIndex = 4;
            // 
            // cmbCategoryFilter
            // 
            this.cmbCategoryFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategoryFilter.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCategoryFilter.FormattingEnabled = true;
            this.cmbCategoryFilter.Location = new System.Drawing.Point(200, 10);
            this.cmbCategoryFilter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbCategoryFilter.Name = "cmbCategoryFilter";
            this.cmbCategoryFilter.Size = new System.Drawing.Size(300, 31);
            this.cmbCategoryFilter.TabIndex = 1;
            this.cmbCategoryFilter.SelectedIndexChanged += new System.EventHandler(this.cmbCategoryFilter_SelectedIndexChanged);
            // 
            // lblCategoryFilter
            // 
            this.lblCategoryFilter.AutoSize = true;
            this.lblCategoryFilter.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategoryFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCategoryFilter.Location = new System.Drawing.Point(20, 14);
            this.lblCategoryFilter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCategoryFilter.Name = "lblCategoryFilter";
            this.lblCategoryFilter.Size = new System.Drawing.Size(148, 23);
            this.lblCategoryFilter.TabIndex = 0;
            this.lblCategoryFilter.Text = "Filter by Category:";
            // 
            // dataGridViewInventory
            // 
            this.dataGridViewInventory.AllowUserToOrderColumns = true;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.dataGridViewInventory.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewInventory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewInventory.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewInventory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewInventory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewInventory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewInventory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colBookID,
            this.colTitle,
            this.colCategory,
            this.colLocation,
            this.colTotal,
            this.colAvailable,
            this.colCheckedOut,
            this.colDamaged,
            this.colLost,
            this.colLastUpdated,
            this.colStatus});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewInventory.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewInventory.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dataGridViewInventory.Location = new System.Drawing.Point(0, 65);
            this.dataGridViewInventory.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridViewInventory.MultiSelect = false;
            this.dataGridViewInventory.Name = "dataGridViewInventory";
            this.dataGridViewInventory.RowHeadersVisible = false;
            this.dataGridViewInventory.RowHeadersWidth = 51;
            this.dataGridViewInventory.RowTemplate.Height = 60;
            this.dataGridViewInventory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewInventory.Size = new System.Drawing.Size(1536, 590);
            this.dataGridViewInventory.TabIndex = 0;
            // 
            // colBookID
            // 
            this.colBookID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colBookID.DataPropertyName = "BookID";
            this.colBookID.HeaderText = "Book ID";
            this.colBookID.MinimumWidth = 6;
            this.colBookID.Name = "colBookID";
            this.colBookID.Width = 125;
            // 
            // colTitle
            // 
            this.colTitle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTitle.DataPropertyName = "Title";
            this.colTitle.HeaderText = "Title";
            this.colTitle.MinimumWidth = 250;
            this.colTitle.Name = "colTitle";
            // 
            // colCategory
            // 
            this.colCategory.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colCategory.DataPropertyName = "Category";
            this.colCategory.HeaderText = "Category";
            this.colCategory.MinimumWidth = 6;
            this.colCategory.Name = "colCategory";
            this.colCategory.Width = 120;
            // 
            // colLocation
            // 
            this.colLocation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colLocation.DataPropertyName = "Location";
            this.colLocation.HeaderText = "Location";
            this.colLocation.MinimumWidth = 6;
            this.colLocation.Name = "colLocation";
            this.colLocation.Width = 125;
            // 
            // colTotal
            // 
            this.colTotal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colTotal.DataPropertyName = "Total";
            this.colTotal.HeaderText = "Total";
            this.colTotal.MinimumWidth = 6;
            this.colTotal.Name = "colTotal";
            this.colTotal.Width = 80;
            // 
            // colAvailable
            // 
            this.colAvailable.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colAvailable.DataPropertyName = "Available";
            this.colAvailable.HeaderText = "Available";
            this.colAvailable.MinimumWidth = 6;
            this.colAvailable.Name = "colAvailable";
            this.colAvailable.Width = 125;
            // 
            // colCheckedOut
            // 
            this.colCheckedOut.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colCheckedOut.DataPropertyName = "CheckedOut";
            this.colCheckedOut.HeaderText = "Checked Out";
            this.colCheckedOut.MinimumWidth = 6;
            this.colCheckedOut.Name = "colCheckedOut";
            this.colCheckedOut.Width = 120;
            // 
            // colDamaged
            // 
            this.colDamaged.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colDamaged.DataPropertyName = "Damaged";
            this.colDamaged.HeaderText = "Damaged";
            this.colDamaged.MinimumWidth = 6;
            this.colDamaged.Name = "colDamaged";
            this.colDamaged.Width = 125;
            // 
            // colLost
            // 
            this.colLost.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colLost.DataPropertyName = "Lost";
            this.colLost.HeaderText = "Lost";
            this.colLost.MinimumWidth = 6;
            this.colLost.Name = "colLost";
            this.colLost.Width = 80;
            // 
            // colLastUpdated
            // 
            this.colLastUpdated.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colLastUpdated.DataPropertyName = "LastUpdated";
            this.colLastUpdated.HeaderText = "Last Updated";
            this.colLastUpdated.MinimumWidth = 6;
            this.colLastUpdated.Name = "colLastUpdated";
            this.colLastUpdated.Width = 130;
            // 
            // colStatus
            // 
            this.colStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colStatus.DataPropertyName = "Status";
            this.colStatus.HeaderText = "Status";
            this.colStatus.MinimumWidth = 6;
            this.colStatus.Name = "colStatus";
            this.colStatus.Width = 120;
            // 
            // panelMetrics
            // 
            this.panelMetrics.Controls.Add(this.cardLost);
            this.panelMetrics.Controls.Add(this.cardDamaged);
            this.panelMetrics.Controls.Add(this.cardCheckedOut);
            this.panelMetrics.Controls.Add(this.cardAvailable);
            this.panelMetrics.Controls.Add(this.cardTotalCopies);
            this.panelMetrics.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMetrics.Location = new System.Drawing.Point(32, 135);
            this.panelMetrics.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelMetrics.Name = "panelMetrics";
            this.panelMetrics.Size = new System.Drawing.Size(1536, 116);
            this.panelMetrics.TabIndex = 3;
            // 
            // cardLost
            // 
            this.cardLost.BackColor = System.Drawing.Color.White;
            this.cardLost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardLost.Controls.Add(this.lblLostValue);
            this.cardLost.Controls.Add(this.lblLostLabel);
            this.cardLost.Location = new System.Drawing.Point(1228, 0);
            this.cardLost.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cardLost.Name = "cardLost";
            this.cardLost.Padding = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.cardLost.Size = new System.Drawing.Size(309, 114);
            this.cardLost.TabIndex = 4;
            // 
            // lblLostValue
            // 
            this.lblLostValue.AutoSize = true;
            this.lblLostValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLostValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblLostValue.Location = new System.Drawing.Point(15, 20);
            this.lblLostValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLostValue.Name = "lblLostValue";
            this.lblLostValue.Size = new System.Drawing.Size(46, 54);
            this.lblLostValue.TabIndex = 2;
            this.lblLostValue.Text = "0";
            // 
            // lblLostLabel
            // 
            this.lblLostLabel.AutoSize = true;
            this.lblLostLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLostLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblLostLabel.Location = new System.Drawing.Point(24, 74);
            this.lblLostLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLostLabel.Name = "lblLostLabel";
            this.lblLostLabel.Size = new System.Drawing.Size(69, 23);
            this.lblLostLabel.TabIndex = 1;
            this.lblLostLabel.Text = "📕 Lost";
            // 
            // cardDamaged
            // 
            this.cardDamaged.BackColor = System.Drawing.Color.White;
            this.cardDamaged.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardDamaged.Controls.Add(this.lblDamagedValue);
            this.cardDamaged.Controls.Add(this.lblDamagedLabel);
            this.cardDamaged.Location = new System.Drawing.Point(920, 0);
            this.cardDamaged.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cardDamaged.Name = "cardDamaged";
            this.cardDamaged.Padding = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.cardDamaged.Size = new System.Drawing.Size(309, 114);
            this.cardDamaged.TabIndex = 3;
            // 
            // lblDamagedValue
            // 
            this.lblDamagedValue.AutoSize = true;
            this.lblDamagedValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDamagedValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDamagedValue.Location = new System.Drawing.Point(24, 20);
            this.lblDamagedValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDamagedValue.Name = "lblDamagedValue";
            this.lblDamagedValue.Size = new System.Drawing.Size(46, 54);
            this.lblDamagedValue.TabIndex = 2;
            this.lblDamagedValue.Text = "0";
            // 
            // lblDamagedLabel
            // 
            this.lblDamagedLabel.AutoSize = true;
            this.lblDamagedLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDamagedLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblDamagedLabel.Location = new System.Drawing.Point(29, 74);
            this.lblDamagedLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDamagedLabel.Name = "lblDamagedLabel";
            this.lblDamagedLabel.Size = new System.Drawing.Size(112, 23);
            this.lblDamagedLabel.TabIndex = 1;
            this.lblDamagedLabel.Text = "🔧 Damaged";
            // 
            // cardCheckedOut
            // 
            this.cardCheckedOut.BackColor = System.Drawing.Color.White;
            this.cardCheckedOut.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardCheckedOut.Controls.Add(this.lblCheckedOutValue);
            this.cardCheckedOut.Controls.Add(this.lblCheckedOutLabel);
            this.cardCheckedOut.Location = new System.Drawing.Point(612, 0);
            this.cardCheckedOut.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cardCheckedOut.Name = "cardCheckedOut";
            this.cardCheckedOut.Padding = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.cardCheckedOut.Size = new System.Drawing.Size(309, 114);
            this.cardCheckedOut.TabIndex = 2;
            // 
            // lblCheckedOutValue
            // 
            this.lblCheckedOutValue.AutoSize = true;
            this.lblCheckedOutValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckedOutValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCheckedOutValue.Location = new System.Drawing.Point(15, 20);
            this.lblCheckedOutValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCheckedOutValue.Name = "lblCheckedOutValue";
            this.lblCheckedOutValue.Size = new System.Drawing.Size(46, 54);
            this.lblCheckedOutValue.TabIndex = 2;
            this.lblCheckedOutValue.Text = "0";
            // 
            // lblCheckedOutLabel
            // 
            this.lblCheckedOutLabel.AutoSize = true;
            this.lblCheckedOutLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckedOutLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblCheckedOutLabel.Location = new System.Drawing.Point(24, 74);
            this.lblCheckedOutLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCheckedOutLabel.Name = "lblCheckedOutLabel";
            this.lblCheckedOutLabel.Size = new System.Drawing.Size(137, 23);
            this.lblCheckedOutLabel.TabIndex = 1;
            this.lblCheckedOutLabel.Text = "📖 Checked Out";
            // 
            // cardAvailable
            // 
            this.cardAvailable.BackColor = System.Drawing.Color.White;
            this.cardAvailable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardAvailable.Controls.Add(this.lblAvailableValue);
            this.cardAvailable.Controls.Add(this.lblAvailableLabel);
            this.cardAvailable.Location = new System.Drawing.Point(308, 0);
            this.cardAvailable.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cardAvailable.Name = "cardAvailable";
            this.cardAvailable.Padding = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.cardAvailable.Size = new System.Drawing.Size(309, 114);
            this.cardAvailable.TabIndex = 1;
            // 
            // lblAvailableValue
            // 
            this.lblAvailableValue.AutoSize = true;
            this.lblAvailableValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvailableValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblAvailableValue.Location = new System.Drawing.Point(15, 20);
            this.lblAvailableValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAvailableValue.Name = "lblAvailableValue";
            this.lblAvailableValue.Size = new System.Drawing.Size(46, 54);
            this.lblAvailableValue.TabIndex = 2;
            this.lblAvailableValue.Text = "0";
            // 
            // lblAvailableLabel
            // 
            this.lblAvailableLabel.AutoSize = true;
            this.lblAvailableLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvailableLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblAvailableLabel.Location = new System.Drawing.Point(20, 74);
            this.lblAvailableLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAvailableLabel.Name = "lblAvailableLabel";
            this.lblAvailableLabel.Size = new System.Drawing.Size(106, 23);
            this.lblAvailableLabel.TabIndex = 1;
            this.lblAvailableLabel.Text = "✅ Available";
            // 
            // cardTotalCopies
            // 
            this.cardTotalCopies.BackColor = System.Drawing.Color.White;
            this.cardTotalCopies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardTotalCopies.Controls.Add(this.lblTotalCopiesValue);
            this.cardTotalCopies.Controls.Add(this.lblTotalCopiesLabel);
            this.cardTotalCopies.Location = new System.Drawing.Point(0, 0);
            this.cardTotalCopies.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cardTotalCopies.Name = "cardTotalCopies";
            this.cardTotalCopies.Padding = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.cardTotalCopies.Size = new System.Drawing.Size(309, 114);
            this.cardTotalCopies.TabIndex = 0;
            // 
            // lblTotalCopiesValue
            // 
            this.lblTotalCopiesValue.AutoSize = true;
            this.lblTotalCopiesValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCopiesValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTotalCopiesValue.Location = new System.Drawing.Point(21, 20);
            this.lblTotalCopiesValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalCopiesValue.Name = "lblTotalCopiesValue";
            this.lblTotalCopiesValue.Size = new System.Drawing.Size(46, 54);
            this.lblTotalCopiesValue.TabIndex = 2;
            this.lblTotalCopiesValue.Text = "0";
            this.lblTotalCopiesValue.Click += new System.EventHandler(this.lblTotalCopiesValue_Click);
            // 
            // lblTotalCopiesLabel
            // 
            this.lblTotalCopiesLabel.AutoSize = true;
            this.lblTotalCopiesLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCopiesLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblTotalCopiesLabel.Location = new System.Drawing.Point(24, 74);
            this.lblTotalCopiesLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalCopiesLabel.Name = "lblTotalCopiesLabel";
            this.lblTotalCopiesLabel.Size = new System.Drawing.Size(130, 23);
            this.lblTotalCopiesLabel.TabIndex = 1;
            this.lblTotalCopiesLabel.Text = "📚 Total Copies";
            // 
            // cardUpdateStock
            // 
            this.cardUpdateStock.BackColor = System.Drawing.Color.White;
            this.cardUpdateStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardUpdateStock.Controls.Add(this.btnAddCopies);
            this.cardUpdateStock.Controls.Add(this.txtUpdateStockQuantity);
            this.cardUpdateStock.Controls.Add(this.txtUpdateStockBookID);
            this.cardUpdateStock.Controls.Add(this.lblUpdateStockTitle);
            this.cardUpdateStock.Location = new System.Drawing.Point(32, 260);
            this.cardUpdateStock.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cardUpdateStock.Name = "cardUpdateStock";
            this.cardUpdateStock.Padding = new System.Windows.Forms.Padding(29, 30, 29, 30);
            this.cardUpdateStock.Size = new System.Drawing.Size(483, 253);
            this.cardUpdateStock.TabIndex = 0;
            // 
            // btnAddCopies
            // 
            this.btnAddCopies.BackColor = System.Drawing.Color.Maroon;
            this.btnAddCopies.FlatAppearance.BorderSize = 0;
            this.btnAddCopies.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddCopies.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddCopies.ForeColor = System.Drawing.Color.White;
            this.btnAddCopies.Location = new System.Drawing.Point(29, 197);
            this.btnAddCopies.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAddCopies.Name = "btnAddCopies";
            this.btnAddCopies.Size = new System.Drawing.Size(400, 39);
            this.btnAddCopies.TabIndex = 3;
            this.btnAddCopies.Text = "Add Copies";
            this.btnAddCopies.UseVisualStyleBackColor = false;
            this.btnAddCopies.Click += new System.EventHandler(this.btnAddCopies_Click);
            // 
            // txtUpdateStockQuantity
            // 
            this.txtUpdateStockQuantity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUpdateStockQuantity.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUpdateStockQuantity.ForeColor = System.Drawing.Color.Gray;
            this.txtUpdateStockQuantity.Location = new System.Drawing.Point(28, 126);
            this.txtUpdateStockQuantity.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtUpdateStockQuantity.Name = "txtUpdateStockQuantity";
            this.txtUpdateStockQuantity.Size = new System.Drawing.Size(401, 30);
            this.txtUpdateStockQuantity.TabIndex = 2;
            this.txtUpdateStockQuantity.Text = "Quantity";
            this.txtUpdateStockQuantity.Enter += new System.EventHandler(this.txtUpdateStockQuantity_Enter);
            this.txtUpdateStockQuantity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUpdateStockQuantity_KeyPress);
            this.txtUpdateStockQuantity.Leave += new System.EventHandler(this.txtUpdateStockQuantity_Leave);
            // 
            // txtUpdateStockBookID
            // 
            this.txtUpdateStockBookID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUpdateStockBookID.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUpdateStockBookID.ForeColor = System.Drawing.Color.Gray;
            this.txtUpdateStockBookID.Location = new System.Drawing.Point(28, 86);
            this.txtUpdateStockBookID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtUpdateStockBookID.Name = "txtUpdateStockBookID";
            this.txtUpdateStockBookID.Size = new System.Drawing.Size(401, 30);
            this.txtUpdateStockBookID.TabIndex = 1;
            this.txtUpdateStockBookID.Text = "Book ID or Title";
            this.txtUpdateStockBookID.Enter += new System.EventHandler(this.txtUpdateStockBookID_Enter);
            this.txtUpdateStockBookID.Leave += new System.EventHandler(this.txtUpdateStockBookID_Leave);
            // 
            // lblUpdateStockTitle
            // 
            this.lblUpdateStockTitle.AutoSize = true;
            this.lblUpdateStockTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdateStockTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblUpdateStockTitle.Location = new System.Drawing.Point(29, 30);
            this.lblUpdateStockTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUpdateStockTitle.Name = "lblUpdateStockTitle";
            this.lblUpdateStockTitle.Size = new System.Drawing.Size(138, 28);
            this.lblUpdateStockTitle.TabIndex = 0;
            this.lblUpdateStockTitle.Text = "Update Stock";
            // 
            // panelInventoryManagement
            // 
            this.panelInventoryManagement.Controls.Add(this.btnAddInventory);
            this.panelInventoryManagement.Controls.Add(this.panelActionCards);
            this.panelInventoryManagement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelInventoryManagement.Location = new System.Drawing.Point(32, 135);
            this.panelInventoryManagement.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelInventoryManagement.Name = "panelInventoryManagement";
            this.panelInventoryManagement.Size = new System.Drawing.Size(1536, 820);
            this.panelInventoryManagement.TabIndex = 1;
            // 
            // btnAddInventory
            // 
            this.btnAddInventory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnAddInventory.FlatAppearance.BorderSize = 0;
            this.btnAddInventory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddInventory.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddInventory.ForeColor = System.Drawing.Color.White;
            this.btnAddInventory.Location = new System.Drawing.Point(1400, 30);
            this.btnAddInventory.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAddInventory.Name = "btnAddInventory";
            this.btnAddInventory.Size = new System.Drawing.Size(136, 50);
            this.btnAddInventory.TabIndex = 2;
            this.btnAddInventory.Text = "+ Add Inventory";
            this.btnAddInventory.UseVisualStyleBackColor = false;
            this.btnAddInventory.Click += new System.EventHandler(this.btnAddInventory_Click);
            // 
            // panelActionCards
            // 
            this.panelActionCards.Controls.Add(this.cardReportLost);
            this.panelActionCards.Controls.Add(this.cardReportDamage);
            this.panelActionCards.Location = new System.Drawing.Point(16, 122);
            this.panelActionCards.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelActionCards.Name = "panelActionCards";
            this.panelActionCards.Size = new System.Drawing.Size(1536, 257);
            this.panelActionCards.TabIndex = 2;
            // 
            // cardReportLost
            // 
            this.cardReportLost.BackColor = System.Drawing.Color.White;
            this.cardReportLost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardReportLost.Controls.Add(this.btnReportLost);
            this.cardReportLost.Controls.Add(this.txtReportLostNotes);
            this.cardReportLost.Controls.Add(this.txtReportLostBookID);
            this.cardReportLost.Controls.Add(this.lblReportLostTitle);
            this.cardReportLost.Location = new System.Drawing.Point(1036, 4);
            this.cardReportLost.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cardReportLost.Name = "cardReportLost";
            this.cardReportLost.Padding = new System.Windows.Forms.Padding(29, 30, 29, 30);
            this.cardReportLost.Size = new System.Drawing.Size(483, 257);
            this.cardReportLost.TabIndex = 2;
            this.cardReportLost.Paint += new System.Windows.Forms.PaintEventHandler(this.cardReportLost_Paint);
            // 
            // btnReportLost
            // 
            this.btnReportLost.BackColor = System.Drawing.Color.Maroon;
            this.btnReportLost.FlatAppearance.BorderSize = 0;
            this.btnReportLost.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReportLost.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReportLost.ForeColor = System.Drawing.Color.White;
            this.btnReportLost.Location = new System.Drawing.Point(29, 197);
            this.btnReportLost.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnReportLost.Name = "btnReportLost";
            this.btnReportLost.Size = new System.Drawing.Size(419, 39);
            this.btnReportLost.TabIndex = 3;
            this.btnReportLost.Text = "Report Lost";
            this.btnReportLost.UseVisualStyleBackColor = false;
            this.btnReportLost.Click += new System.EventHandler(this.btnReportLost_Click);
            // 
            // txtReportLostNotes
            // 
            this.txtReportLostNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReportLostNotes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReportLostNotes.ForeColor = System.Drawing.Color.Gray;
            this.txtReportLostNotes.Location = new System.Drawing.Point(29, 110);
            this.txtReportLostNotes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtReportLostNotes.Multiline = true;
            this.txtReportLostNotes.Name = "txtReportLostNotes";
            this.txtReportLostNotes.Size = new System.Drawing.Size(418, 70);
            this.txtReportLostNotes.TabIndex = 2;
            this.txtReportLostNotes.Text = "Additional notes";
            this.txtReportLostNotes.Enter += new System.EventHandler(this.txtReportLostNotes_Enter);
            this.txtReportLostNotes.Leave += new System.EventHandler(this.txtReportLostNotes_Leave);
            // 
            // txtReportLostBookID
            // 
            this.txtReportLostBookID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReportLostBookID.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReportLostBookID.ForeColor = System.Drawing.Color.Gray;
            this.txtReportLostBookID.Location = new System.Drawing.Point(29, 70);
            this.txtReportLostBookID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtReportLostBookID.Name = "txtReportLostBookID";
            this.txtReportLostBookID.Size = new System.Drawing.Size(418, 30);
            this.txtReportLostBookID.TabIndex = 1;
            this.txtReportLostBookID.Text = "Book ID or Title";
            this.txtReportLostBookID.Enter += new System.EventHandler(this.txtReportLostBookID_Enter);
            this.txtReportLostBookID.Leave += new System.EventHandler(this.txtReportLostBookID_Leave);
            // 
            // lblReportLostTitle
            // 
            this.lblReportLostTitle.AutoSize = true;
            this.lblReportLostTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReportLostTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblReportLostTitle.Location = new System.Drawing.Point(29, 30);
            this.lblReportLostTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReportLostTitle.Name = "lblReportLostTitle";
            this.lblReportLostTitle.Size = new System.Drawing.Size(122, 28);
            this.lblReportLostTitle.TabIndex = 0;
            this.lblReportLostTitle.Text = "Report Lost";
            // 
            // cardReportDamage
            // 
            this.cardReportDamage.BackColor = System.Drawing.Color.White;
            this.cardReportDamage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardReportDamage.Controls.Add(this.btnReportDamage);
            this.cardReportDamage.Controls.Add(this.txtReportDamageDescription);
            this.cardReportDamage.Controls.Add(this.txtReportDamageBookID);
            this.cardReportDamage.Controls.Add(this.lblReportDamageTitle);
            this.cardReportDamage.Location = new System.Drawing.Point(517, 4);
            this.cardReportDamage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cardReportDamage.Name = "cardReportDamage";
            this.cardReportDamage.Padding = new System.Windows.Forms.Padding(29, 30, 29, 30);
            this.cardReportDamage.Size = new System.Drawing.Size(483, 257);
            this.cardReportDamage.TabIndex = 1;
            // 
            // btnReportDamage
            // 
            this.btnReportDamage.BackColor = System.Drawing.Color.Maroon;
            this.btnReportDamage.FlatAppearance.BorderSize = 0;
            this.btnReportDamage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReportDamage.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReportDamage.ForeColor = System.Drawing.Color.White;
            this.btnReportDamage.Location = new System.Drawing.Point(29, 197);
            this.btnReportDamage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnReportDamage.Name = "btnReportDamage";
            this.btnReportDamage.Size = new System.Drawing.Size(419, 39);
            this.btnReportDamage.TabIndex = 3;
            this.btnReportDamage.Text = "Report Damage";
            this.btnReportDamage.UseVisualStyleBackColor = false;
            this.btnReportDamage.Click += new System.EventHandler(this.btnReportDamage_Click);
            // 
            // txtReportDamageDescription
            // 
            this.txtReportDamageDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReportDamageDescription.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReportDamageDescription.ForeColor = System.Drawing.Color.Gray;
            this.txtReportDamageDescription.Location = new System.Drawing.Point(29, 110);
            this.txtReportDamageDescription.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtReportDamageDescription.Multiline = true;
            this.txtReportDamageDescription.Name = "txtReportDamageDescription";
            this.txtReportDamageDescription.Size = new System.Drawing.Size(418, 70);
            this.txtReportDamageDescription.TabIndex = 2;
            this.txtReportDamageDescription.Text = "Damage description";
            this.txtReportDamageDescription.Enter += new System.EventHandler(this.txtReportDamageDescription_Enter);
            this.txtReportDamageDescription.Leave += new System.EventHandler(this.txtReportDamageDescription_Leave);
            // 
            // txtReportDamageBookID
            // 
            this.txtReportDamageBookID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReportDamageBookID.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReportDamageBookID.ForeColor = System.Drawing.Color.Gray;
            this.txtReportDamageBookID.Location = new System.Drawing.Point(16, 71);
            this.txtReportDamageBookID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtReportDamageBookID.Name = "txtReportDamageBookID";
            this.txtReportDamageBookID.Size = new System.Drawing.Size(431, 30);
            this.txtReportDamageBookID.TabIndex = 1;
            this.txtReportDamageBookID.Text = "Book ID or Title";
            this.txtReportDamageBookID.TextChanged += new System.EventHandler(this.txtReportDamageBookID_TextChanged);
            this.txtReportDamageBookID.Enter += new System.EventHandler(this.txtReportDamageBookID_Enter);
            this.txtReportDamageBookID.Leave += new System.EventHandler(this.txtReportDamageBookID_Leave);
            // 
            // lblReportDamageTitle
            // 
            this.lblReportDamageTitle.AutoSize = true;
            this.lblReportDamageTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReportDamageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblReportDamageTitle.Location = new System.Drawing.Point(29, 30);
            this.lblReportDamageTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReportDamageTitle.Name = "lblReportDamageTitle";
            this.lblReportDamageTitle.Size = new System.Drawing.Size(161, 28);
            this.lblReportDamageTitle.TabIndex = 0;
            this.lblReportDamageTitle.Text = "Report Damage";
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Controls.Add(this.lblSubtitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(32, 30);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1536, 105);
            this.panelHeader.TabIndex = 0;
            this.panelHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.panelHeader_Paint);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitle.Location = new System.Drawing.Point(4, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(471, 54);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Inventory Management";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblSubtitle.Location = new System.Drawing.Point(4, 63);
            this.lblSubtitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(320, 23);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Track book copies and physical inventory";
            this.lblSubtitle.Click += new System.EventHandler(this.lblSubtitle_Click);
            // 
            // StaffInventoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1600, 985);
            this.Controls.Add(this.panelMainContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "StaffInventoryForm";
            this.Text = "Inventory";
            this.Load += new System.EventHandler(this.StaffInventoryForm_Load);
            this.panelMainContainer.ResumeLayout(false);
            this.panelInventoryTable.ResumeLayout(false);
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInventory)).EndInit();
            this.panelMetrics.ResumeLayout(false);
            this.cardLost.ResumeLayout(false);
            this.cardLost.PerformLayout();
            this.cardDamaged.ResumeLayout(false);
            this.cardDamaged.PerformLayout();
            this.cardCheckedOut.ResumeLayout(false);
            this.cardCheckedOut.PerformLayout();
            this.cardAvailable.ResumeLayout(false);
            this.cardAvailable.PerformLayout();
            this.cardTotalCopies.ResumeLayout(false);
            this.cardTotalCopies.PerformLayout();
            this.cardUpdateStock.ResumeLayout(false);
            this.cardUpdateStock.PerformLayout();
            this.panelInventoryManagement.ResumeLayout(false);
            this.panelActionCards.ResumeLayout(false);
            this.cardReportLost.ResumeLayout(false);
            this.cardReportLost.PerformLayout();
            this.cardReportDamage.ResumeLayout(false);
            this.cardReportDamage.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.Panel panelMainContainer;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelInventoryManagement;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Button btnAddInventory;
        private System.Windows.Forms.Panel panelActionCards;
        private System.Windows.Forms.Panel cardUpdateStock;
        private System.Windows.Forms.Label lblUpdateStockTitle;
        private System.Windows.Forms.TextBox txtUpdateStockBookID;
        private System.Windows.Forms.TextBox txtUpdateStockQuantity;
        private System.Windows.Forms.Button btnAddCopies;
        private System.Windows.Forms.Panel cardReportDamage;
        private System.Windows.Forms.Label lblReportDamageTitle;
        private System.Windows.Forms.TextBox txtReportDamageBookID;
        private System.Windows.Forms.TextBox txtReportDamageDescription;
        private System.Windows.Forms.Button btnReportDamage;
        private System.Windows.Forms.Panel cardReportLost;
        private System.Windows.Forms.Label lblReportLostTitle;
        private System.Windows.Forms.TextBox txtReportLostBookID;
        private System.Windows.Forms.TextBox txtReportLostNotes;
        private System.Windows.Forms.Button btnReportLost;
        private System.Windows.Forms.Panel panelMetrics;
        private System.Windows.Forms.Panel cardTotalCopies;
        private System.Windows.Forms.Label lblTotalCopiesLabel;
        private System.Windows.Forms.Label lblTotalCopiesValue;
        private System.Windows.Forms.Panel cardAvailable;
        private System.Windows.Forms.Label lblAvailableLabel;
        private System.Windows.Forms.Label lblAvailableValue;
        private System.Windows.Forms.Panel cardCheckedOut;
        private System.Windows.Forms.Label lblCheckedOutLabel;
        private System.Windows.Forms.Label lblCheckedOutValue;
        private System.Windows.Forms.Panel cardDamaged;
        private System.Windows.Forms.Label lblDamagedLabel;
        private System.Windows.Forms.Label lblDamagedValue;
        private System.Windows.Forms.Panel cardLost;
        private System.Windows.Forms.Label lblLostLabel;
        private System.Windows.Forms.Label lblLostValue;
        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.Label lblCategoryFilter;
        private System.Windows.Forms.ComboBox cmbCategoryFilter;
        private System.Windows.Forms.Panel panelInventoryTable;
        private System.Windows.Forms.DataGridView dataGridViewInventory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBookID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAvailable;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCheckedOut;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDamaged;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLost;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastUpdated;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
    }
}