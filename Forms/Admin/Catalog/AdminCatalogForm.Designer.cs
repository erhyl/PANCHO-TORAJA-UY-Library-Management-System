namespace Project5LMS.Forms.Admin.Catalog
{
    partial class AdminCatalogForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelMainContainer = new System.Windows.Forms.Panel();
            this.panelTableContainer = new System.Windows.Forms.Panel();
            this.dataGridViewBooks = new System.Windows.Forms.DataGridView();
            this.AccessionNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BookDetails = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Publisher = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Copies = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.View = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Delete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panelSearchFilter = new System.Windows.Forms.Panel();
            this.cmbResourceTypeFilter = new System.Windows.Forms.ComboBox();
            this.lblResourceTypeFilter = new System.Windows.Forms.Label();
            this.cmbCategoryFilter = new System.Windows.Forms.ComboBox();
            this.lblCategoryFilter = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.panelMetrics = new System.Windows.Forms.Panel();
            this.panelMetricOnLoan = new System.Windows.Forms.Panel();
            this.lblMetricOnLoanValue = new System.Windows.Forms.Label();
            this.lblMetricOnLoanTitle = new System.Windows.Forms.Label();
            this.panelMetricAvailable = new System.Windows.Forms.Panel();
            this.lblMetricAvailableValue = new System.Windows.Forms.Label();
            this.lblMetricAvailableTitle = new System.Windows.Forms.Label();
            this.panelMetricTotalCopies = new System.Windows.Forms.Panel();
            this.lblMetricTotalCopiesValue = new System.Windows.Forms.Label();
            this.lblMetricTotalCopiesTitle = new System.Windows.Forms.Label();
            this.panelMetricTotalTitles = new System.Windows.Forms.Panel();
            this.lblMetricTotalTitlesValue = new System.Windows.Forms.Label();
            this.lblMetricTotalTitlesTitle = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.btnImportCSV = new System.Windows.Forms.Button();
            this.btnAddNewBook = new System.Windows.Forms.Button();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelMainContainer.SuspendLayout();
            this.panelTableContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBooks)).BeginInit();
            this.panelSearchFilter.SuspendLayout();
            this.panelMetrics.SuspendLayout();
            this.panelMetricOnLoan.SuspendLayout();
            this.panelMetricAvailable.SuspendLayout();
            this.panelMetricTotalCopies.SuspendLayout();
            this.panelMetricTotalTitles.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMainContainer
            // 
            this.panelMainContainer.AutoScroll = true;
            this.panelMainContainer.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelMainContainer.Controls.Add(this.panelTableContainer);
            this.panelMainContainer.Controls.Add(this.panelSearchFilter);
            this.panelMainContainer.Controls.Add(this.panelMetrics);
            this.panelMainContainer.Controls.Add(this.panelHeader);
            this.panelMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainContainer.Location = new System.Drawing.Point(0, 0);
            this.panelMainContainer.Margin = new System.Windows.Forms.Padding(4);
            this.panelMainContainer.Name = "panelMainContainer";
            this.panelMainContainer.Padding = new System.Windows.Forms.Padding(32, 30, 32, 30);
            this.panelMainContainer.Size = new System.Drawing.Size(1942, 1102);
            this.panelMainContainer.TabIndex = 0;
            // 
            // panelTableContainer
            // 
            this.panelTableContainer.BackColor = System.Drawing.Color.White;
            this.panelTableContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTableContainer.Controls.Add(this.dataGridViewBooks);
            this.panelTableContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTableContainer.Location = new System.Drawing.Point(32, 298);
            this.panelTableContainer.Margin = new System.Windows.Forms.Padding(4);
            this.panelTableContainer.Name = "panelTableContainer";
            this.panelTableContainer.Padding = new System.Windows.Forms.Padding(21, 20, 21, 20);
            this.panelTableContainer.Size = new System.Drawing.Size(1878, 774);
            this.panelTableContainer.TabIndex = 3;
            // 
            // dataGridViewBooks
            // 
            this.dataGridViewBooks.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.dataGridViewBooks.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewBooks.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewBooks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewBooks.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBooks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AccessionNo,
            this.BookDetails,
            this.Category,
            this.Publisher,
            this.Copies,
            this.colLocation,
            this.Status,
            this.Edit,
            this.View,
            this.Delete});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewBooks.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewBooks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewBooks.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dataGridViewBooks.Location = new System.Drawing.Point(21, 20);
            this.dataGridViewBooks.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewBooks.MultiSelect = false;
            this.dataGridViewBooks.Name = "dataGridViewBooks";
            this.dataGridViewBooks.RowHeadersVisible = false;
            this.dataGridViewBooks.RowHeadersWidth = 51;
            this.dataGridViewBooks.RowTemplate.Height = 60;
            this.dataGridViewBooks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewBooks.Size = new System.Drawing.Size(1834, 732);
            this.dataGridViewBooks.TabIndex = 0;
            this.dataGridViewBooks.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewBooks_CellContentClick);
            this.dataGridViewBooks.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DataGridViewBooks_CellFormatting);
            this.dataGridViewBooks.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.DataGridViewBooks_CellPainting);
            // 
            // AccessionNo
            // 
            this.AccessionNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.AccessionNo.DataPropertyName = "AccessionNo";
            this.AccessionNo.FillWeight = 120F;
            this.AccessionNo.HeaderText = "ACCESSION NO.";
            this.AccessionNo.MinimumWidth = 100;
            this.AccessionNo.Name = "AccessionNo";
            this.AccessionNo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.AccessionNo.Width = 120;
            // 
            // BookDetails
            // 
            this.BookDetails.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.BookDetails.DataPropertyName = "BookDetails";
            this.BookDetails.FillWeight = 300F;
            this.BookDetails.HeaderText = "BOOK DETAILS";
            this.BookDetails.MinimumWidth = 200;
            this.BookDetails.Name = "BookDetails";
            this.BookDetails.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Category
            // 
            this.Category.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Category.DataPropertyName = "Category";
            this.Category.FillWeight = 120F;
            this.Category.HeaderText = "CATEGORY";
            this.Category.MinimumWidth = 100;
            this.Category.Name = "Category";
            this.Category.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Category.Width = 120;
            // 
            // Publisher
            // 
            this.Publisher.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Publisher.DataPropertyName = "Publisher";
            this.Publisher.FillWeight = 200F;
            this.Publisher.HeaderText = "PUBLISHER";
            this.Publisher.MinimumWidth = 150;
            this.Publisher.Name = "Publisher";
            this.Publisher.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Publisher.Width = 200;
            // 
            // Copies
            // 
            this.Copies.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Copies.DataPropertyName = "Copies";
            this.Copies.HeaderText = "COPIES";
            this.Copies.MinimumWidth = 80;
            this.Copies.Name = "Copies";
            this.Copies.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // colLocation
            // 
            this.colLocation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colLocation.DataPropertyName = "Location";
            this.colLocation.HeaderText = "LOCATION";
            this.colLocation.MinimumWidth = 80;
            this.colLocation.Name = "colLocation";
            this.colLocation.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Status
            // 
            this.Status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Status.DataPropertyName = "Status";
            this.Status.FillWeight = 120F;
            this.Status.HeaderText = "STATUS";
            this.Status.MinimumWidth = 100;
            this.Status.Name = "Status";
            this.Status.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Status.Width = 120;
            // 
            // Edit
            // 
            this.Edit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.Edit.DefaultCellStyle = dataGridViewCellStyle3;
            this.Edit.FillWeight = 50F;
            this.Edit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Edit.HeaderText = "ACTIONS";
            this.Edit.MinimumWidth = 50;
            this.Edit.Name = "Edit";
            this.Edit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Edit.Text = "✏";
            this.Edit.UseColumnTextForButtonValue = true;
            this.Edit.Width = 50;
            // 
            // View
            // 
            this.View.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.View.DefaultCellStyle = dataGridViewCellStyle4;
            this.View.FillWeight = 50F;
            this.View.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.View.MinimumWidth = 50;
            this.View.Name = "View";
            this.View.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.View.Text = "📖";
            this.View.UseColumnTextForButtonValue = true;
            this.View.Width = 50;
            // 
            // Delete
            // 
            this.Delete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.Delete.DefaultCellStyle = dataGridViewCellStyle5;
            this.Delete.FillWeight = 50F;
            this.Delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Delete.MinimumWidth = 50;
            this.Delete.Name = "Delete";
            this.Delete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Delete.Text = "🗑";
            this.Delete.UseColumnTextForButtonValue = true;
            this.Delete.Width = 50;
            // 
            // panelSearchFilter
            // 
            this.panelSearchFilter.BackColor = System.Drawing.Color.White;
            this.panelSearchFilter.Controls.Add(this.cmbResourceTypeFilter);
            this.panelSearchFilter.Controls.Add(this.lblResourceTypeFilter);
            this.panelSearchFilter.Controls.Add(this.cmbCategoryFilter);
            this.panelSearchFilter.Controls.Add(this.lblCategoryFilter);
            this.panelSearchFilter.Controls.Add(this.txtSearch);
            this.panelSearchFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearchFilter.Location = new System.Drawing.Point(32, 224);
            this.panelSearchFilter.Margin = new System.Windows.Forms.Padding(4);
            this.panelSearchFilter.Name = "panelSearchFilter";
            this.panelSearchFilter.Padding = new System.Windows.Forms.Padding(21, 20, 21, 20);
            this.panelSearchFilter.Size = new System.Drawing.Size(1878, 74);
            this.panelSearchFilter.TabIndex = 2;
            // 
            // cmbResourceTypeFilter
            // 
            this.cmbResourceTypeFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbResourceTypeFilter.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbResourceTypeFilter.FormattingEnabled = true;
            this.cmbResourceTypeFilter.Location = new System.Drawing.Point(1030, 19);
            this.cmbResourceTypeFilter.Margin = new System.Windows.Forms.Padding(4);
            this.cmbResourceTypeFilter.Name = "cmbResourceTypeFilter";
            this.cmbResourceTypeFilter.Size = new System.Drawing.Size(240, 31);
            this.cmbResourceTypeFilter.TabIndex = 3;
            this.cmbResourceTypeFilter.SelectedIndexChanged += new System.EventHandler(this.cmbResourceTypeFilter_SelectedIndexChanged);
            // 
            // lblResourceTypeFilter
            // 
            this.lblResourceTypeFilter.AutoSize = true;
            this.lblResourceTypeFilter.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResourceTypeFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblResourceTypeFilter.Location = new System.Drawing.Point(891, 27);
            this.lblResourceTypeFilter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblResourceTypeFilter.Name = "lblResourceTypeFilter";
            this.lblResourceTypeFilter.Size = new System.Drawing.Size(154, 23);
            this.lblResourceTypeFilter.TabIndex = 4;
            this.lblResourceTypeFilter.Text = "All Resource Types:";
            // 
            // cmbCategoryFilter
            // 
            this.cmbCategoryFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategoryFilter.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCategoryFilter.FormattingEnabled = true;
            this.cmbCategoryFilter.Location = new System.Drawing.Point(1434, 19);
            this.cmbCategoryFilter.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCategoryFilter.Name = "cmbCategoryFilter";
            this.cmbCategoryFilter.Size = new System.Drawing.Size(385, 31);
            this.cmbCategoryFilter.TabIndex = 2;
            this.cmbCategoryFilter.SelectedIndexChanged += new System.EventHandler(this.cmbCategoryFilter_SelectedIndexChanged);
            // 
            // lblCategoryFilter
            // 
            this.lblCategoryFilter.AutoSize = true;
            this.lblCategoryFilter.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategoryFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCategoryFilter.Location = new System.Drawing.Point(1295, 27);
            this.lblCategoryFilter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCategoryFilter.Name = "lblCategoryFilter";
            this.lblCategoryFilter.Size = new System.Drawing.Size(119, 23);
            this.lblCategoryFilter.TabIndex = 1;
            this.lblCategoryFilter.Text = "All Categories:";
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.Color.Gray;
            this.txtSearch.Location = new System.Drawing.Point(21, 20);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(670, 30);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.Text = "🔍 Search by title, author, ISBN, or accession number...";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            // 
            // panelMetrics
            // 
            this.panelMetrics.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelMetrics.Controls.Add(this.panelMetricOnLoan);
            this.panelMetrics.Controls.Add(this.panelMetricAvailable);
            this.panelMetrics.Controls.Add(this.panelMetricTotalCopies);
            this.panelMetrics.Controls.Add(this.panelMetricTotalTitles);
            this.panelMetrics.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMetrics.Location = new System.Drawing.Point(32, 124);
            this.panelMetrics.Margin = new System.Windows.Forms.Padding(4);
            this.panelMetrics.Name = "panelMetrics";
            this.panelMetrics.Size = new System.Drawing.Size(1878, 100);
            this.panelMetrics.TabIndex = 1;
            // 
            // panelMetricOnLoan
            // 
            this.panelMetricOnLoan.BackColor = System.Drawing.Color.White;
            this.panelMetricOnLoan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMetricOnLoan.Controls.Add(this.lblMetricOnLoanValue);
            this.panelMetricOnLoan.Controls.Add(this.lblMetricOnLoanTitle);
            this.panelMetricOnLoan.Location = new System.Drawing.Point(1434, 0);
            this.panelMetricOnLoan.Margin = new System.Windows.Forms.Padding(0, 0, 21, 0);
            this.panelMetricOnLoan.Name = "panelMetricOnLoan";
            this.panelMetricOnLoan.Padding = new System.Windows.Forms.Padding(21, 20, 21, 20);
            this.panelMetricOnLoan.Size = new System.Drawing.Size(385, 99);
            this.panelMetricOnLoan.TabIndex = 3;
            // 
            // lblMetricOnLoanValue
            // 
            this.lblMetricOnLoanValue.AutoSize = true;
            this.lblMetricOnLoanValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricOnLoanValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricOnLoanValue.Location = new System.Drawing.Point(21, 43);
            this.lblMetricOnLoanValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricOnLoanValue.Name = "lblMetricOnLoanValue";
            this.lblMetricOnLoanValue.Size = new System.Drawing.Size(46, 54);
            this.lblMetricOnLoanValue.TabIndex = 2;
            this.lblMetricOnLoanValue.Text = "0";
            // 
            // lblMetricOnLoanTitle
            // 
            this.lblMetricOnLoanTitle.AutoSize = true;
            this.lblMetricOnLoanTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricOnLoanTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricOnLoanTitle.Location = new System.Drawing.Point(16, 20);
            this.lblMetricOnLoanTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricOnLoanTitle.Name = "lblMetricOnLoanTitle";
            this.lblMetricOnLoanTitle.Size = new System.Drawing.Size(103, 23);
            this.lblMetricOnLoanTitle.TabIndex = 1;
            this.lblMetricOnLoanTitle.Text = "⏳ On Loan";
            // 
            // panelMetricAvailable
            // 
            this.panelMetricAvailable.BackColor = System.Drawing.Color.White;
            this.panelMetricAvailable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMetricAvailable.Controls.Add(this.lblMetricAvailableValue);
            this.panelMetricAvailable.Controls.Add(this.lblMetricAvailableTitle);
            this.panelMetricAvailable.Location = new System.Drawing.Point(930, 0);
            this.panelMetricAvailable.Margin = new System.Windows.Forms.Padding(0, 0, 21, 0);
            this.panelMetricAvailable.Name = "panelMetricAvailable";
            this.panelMetricAvailable.Padding = new System.Windows.Forms.Padding(21, 20, 21, 20);
            this.panelMetricAvailable.Size = new System.Drawing.Size(386, 102);
            this.panelMetricAvailable.TabIndex = 2;
            // 
            // lblMetricAvailableValue
            // 
            this.lblMetricAvailableValue.AutoSize = true;
            this.lblMetricAvailableValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricAvailableValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricAvailableValue.Location = new System.Drawing.Point(25, 45);
            this.lblMetricAvailableValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricAvailableValue.Name = "lblMetricAvailableValue";
            this.lblMetricAvailableValue.Size = new System.Drawing.Size(46, 54);
            this.lblMetricAvailableValue.TabIndex = 2;
            this.lblMetricAvailableValue.Text = "0";
            // 
            // lblMetricAvailableTitle
            // 
            this.lblMetricAvailableTitle.AutoSize = true;
            this.lblMetricAvailableTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricAvailableTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricAvailableTitle.Location = new System.Drawing.Point(25, 20);
            this.lblMetricAvailableTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricAvailableTitle.Name = "lblMetricAvailableTitle";
            this.lblMetricAvailableTitle.Size = new System.Drawing.Size(106, 23);
            this.lblMetricAvailableTitle.TabIndex = 1;
            this.lblMetricAvailableTitle.Text = "📕 Available";
            // 
            // panelMetricTotalCopies
            // 
            this.panelMetricTotalCopies.BackColor = System.Drawing.Color.White;
            this.panelMetricTotalCopies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMetricTotalCopies.Controls.Add(this.lblMetricTotalCopiesValue);
            this.panelMetricTotalCopies.Controls.Add(this.lblMetricTotalCopiesTitle);
            this.panelMetricTotalCopies.Location = new System.Drawing.Point(455, 0);
            this.panelMetricTotalCopies.Margin = new System.Windows.Forms.Padding(0, 0, 21, 0);
            this.panelMetricTotalCopies.Name = "panelMetricTotalCopies";
            this.panelMetricTotalCopies.Padding = new System.Windows.Forms.Padding(21, 20, 21, 20);
            this.panelMetricTotalCopies.Size = new System.Drawing.Size(360, 100);
            this.panelMetricTotalCopies.TabIndex = 1;
            // 
            // lblMetricTotalCopiesValue
            // 
            this.lblMetricTotalCopiesValue.AutoSize = true;
            this.lblMetricTotalCopiesValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTotalCopiesValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricTotalCopiesValue.Location = new System.Drawing.Point(25, 47);
            this.lblMetricTotalCopiesValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricTotalCopiesValue.Name = "lblMetricTotalCopiesValue";
            this.lblMetricTotalCopiesValue.Size = new System.Drawing.Size(46, 54);
            this.lblMetricTotalCopiesValue.TabIndex = 2;
            this.lblMetricTotalCopiesValue.Text = "0";
            // 
            // lblMetricTotalCopiesTitle
            // 
            this.lblMetricTotalCopiesTitle.AutoSize = true;
            this.lblMetricTotalCopiesTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTotalCopiesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricTotalCopiesTitle.Location = new System.Drawing.Point(21, 20);
            this.lblMetricTotalCopiesTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricTotalCopiesTitle.Name = "lblMetricTotalCopiesTitle";
            this.lblMetricTotalCopiesTitle.Size = new System.Drawing.Size(130, 23);
            this.lblMetricTotalCopiesTitle.TabIndex = 1;
            this.lblMetricTotalCopiesTitle.Text = "📕 Total Copies";
            // 
            // panelMetricTotalTitles
            // 
            this.panelMetricTotalTitles.BackColor = System.Drawing.Color.White;
            this.panelMetricTotalTitles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMetricTotalTitles.Controls.Add(this.lblMetricTotalTitlesValue);
            this.panelMetricTotalTitles.Controls.Add(this.lblMetricTotalTitlesTitle);
            this.panelMetricTotalTitles.Location = new System.Drawing.Point(0, 0);
            this.panelMetricTotalTitles.Margin = new System.Windows.Forms.Padding(0, 0, 21, 0);
            this.panelMetricTotalTitles.Name = "panelMetricTotalTitles";
            this.panelMetricTotalTitles.Padding = new System.Windows.Forms.Padding(21, 20, 21, 20);
            this.panelMetricTotalTitles.Size = new System.Drawing.Size(331, 102);
            this.panelMetricTotalTitles.TabIndex = 0;
            // 
            // lblMetricTotalTitlesValue
            // 
            this.lblMetricTotalTitlesValue.AutoSize = true;
            this.lblMetricTotalTitlesValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTotalTitlesValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricTotalTitlesValue.Location = new System.Drawing.Point(25, 45);
            this.lblMetricTotalTitlesValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricTotalTitlesValue.Name = "lblMetricTotalTitlesValue";
            this.lblMetricTotalTitlesValue.Size = new System.Drawing.Size(46, 54);
            this.lblMetricTotalTitlesValue.TabIndex = 2;
            this.lblMetricTotalTitlesValue.Text = "0";
            // 
            // lblMetricTotalTitlesTitle
            // 
            this.lblMetricTotalTitlesTitle.AutoSize = true;
            this.lblMetricTotalTitlesTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTotalTitlesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricTotalTitlesTitle.Location = new System.Drawing.Point(21, 20);
            this.lblMetricTotalTitlesTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricTotalTitlesTitle.Name = "lblMetricTotalTitlesTitle";
            this.lblMetricTotalTitlesTitle.Size = new System.Drawing.Size(118, 23);
            this.lblMetricTotalTitlesTitle.TabIndex = 1;
            this.lblMetricTotalTitlesTitle.Text = "📕 Total Titles";
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.btnImportCSV);
            this.panelHeader.Controls.Add(this.btnAddNewBook);
            this.panelHeader.Controls.Add(this.lblSubtitle);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(32, 30);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(4);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1878, 94);
            this.panelHeader.TabIndex = 0;
            // 
            // btnImportCSV
            // 
            this.btnImportCSV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImportCSV.BackColor = System.Drawing.Color.Green;
            this.btnImportCSV.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnImportCSV.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportCSV.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportCSV.ForeColor = System.Drawing.Color.White;
            this.btnImportCSV.Location = new System.Drawing.Point(1244, 20);
            this.btnImportCSV.Margin = new System.Windows.Forms.Padding(4);
            this.btnImportCSV.Name = "btnImportCSV";
            this.btnImportCSV.Size = new System.Drawing.Size(258, 49);
            this.btnImportCSV.TabIndex = 3;
            this.btnImportCSV.Text = "📥 Import CSV";
            this.btnImportCSV.UseVisualStyleBackColor = false;
            this.btnImportCSV.Click += new System.EventHandler(this.btnImportCSV_Click);
            // 
            // btnAddNewBook
            // 
            this.btnAddNewBook.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNewBook.BackColor = System.Drawing.Color.Maroon;
            this.btnAddNewBook.FlatAppearance.BorderSize = 0;
            this.btnAddNewBook.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNewBook.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddNewBook.ForeColor = System.Drawing.Color.White;
            this.btnAddNewBook.Location = new System.Drawing.Point(1549, 20);
            this.btnAddNewBook.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddNewBook.Name = "btnAddNewBook";
            this.btnAddNewBook.Size = new System.Drawing.Size(270, 49);
            this.btnAddNewBook.TabIndex = 2;
            this.btnAddNewBook.Text = "➕ Add New Book";
            this.btnAddNewBook.UseVisualStyleBackColor = false;
            this.btnAddNewBook.Click += new System.EventHandler(this.btnAddNewBook_Click);
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblSubtitle.Location = new System.Drawing.Point(17, 62);
            this.lblSubtitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(285, 23);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Manage library books and resources";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitle.Location = new System.Drawing.Point(12, 8);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(275, 54);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Book Catalog";
            // 
            // AdminCatalogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1942, 1102);
            this.Controls.Add(this.panelMainContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AdminCatalogForm";
            this.Text = "Book Catalog";
            this.Load += new System.EventHandler(this.AdminCatalogForm_Load);
            this.panelMainContainer.ResumeLayout(false);
            this.panelTableContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBooks)).EndInit();
            this.panelSearchFilter.ResumeLayout(false);
            this.panelSearchFilter.PerformLayout();
            this.panelMetrics.ResumeLayout(false);
            this.panelMetricOnLoan.ResumeLayout(false);
            this.panelMetricOnLoan.PerformLayout();
            this.panelMetricAvailable.ResumeLayout(false);
            this.panelMetricAvailable.PerformLayout();
            this.panelMetricTotalCopies.ResumeLayout(false);
            this.panelMetricTotalCopies.PerformLayout();
            this.panelMetricTotalTitles.ResumeLayout(false);
            this.panelMetricTotalTitles.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMainContainer;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Button btnAddNewBook;
        private System.Windows.Forms.Button btnImportCSV;
        private System.Windows.Forms.Panel panelMetrics;
        private System.Windows.Forms.Panel panelMetricTotalTitles;
        private System.Windows.Forms.Label lblMetricTotalTitlesValue;
        private System.Windows.Forms.Label lblMetricTotalTitlesTitle;
        private System.Windows.Forms.Panel panelMetricTotalCopies;
        private System.Windows.Forms.Label lblMetricTotalCopiesValue;
        private System.Windows.Forms.Label lblMetricTotalCopiesTitle;
        private System.Windows.Forms.Panel panelMetricAvailable;
        private System.Windows.Forms.Label lblMetricAvailableValue;
        private System.Windows.Forms.Label lblMetricAvailableTitle;
        private System.Windows.Forms.Panel panelMetricOnLoan;
        private System.Windows.Forms.Label lblMetricOnLoanValue;
        private System.Windows.Forms.Label lblMetricOnLoanTitle;
        private System.Windows.Forms.Panel panelSearchFilter;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblCategoryFilter;
        private System.Windows.Forms.ComboBox cmbCategoryFilter;
        private System.Windows.Forms.Label lblResourceTypeFilter;
        private System.Windows.Forms.ComboBox cmbResourceTypeFilter;
        private System.Windows.Forms.Panel panelTableContainer;
        private System.Windows.Forms.DataGridView dataGridViewBooks;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccessionNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn BookDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn Category;
        private System.Windows.Forms.DataGridViewTextBoxColumn Publisher;
        private System.Windows.Forms.DataGridViewTextBoxColumn Copies;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewButtonColumn Edit;
        private System.Windows.Forms.DataGridViewButtonColumn View;
        private System.Windows.Forms.DataGridViewButtonColumn Delete;
    }
}
