namespace Project5LMS.Admin_Dashboard
{
    partial class FinesForm
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
            this.panelRightSection = new System.Windows.Forms.Panel();
            this.panelCalculator = new System.Windows.Forms.Panel();
            this.lblLostBookFee = new System.Windows.Forms.Label();
            this.lblGracePeriod = new System.Windows.Forms.Label();
            this.lblMaxFine = new System.Windows.Forms.Label();
            this.lblFineRulesTitle = new System.Windows.Forms.Label();
            this.lblCalculatedFine = new System.Windows.Forms.Label();
            this.lblCalculatedFineTitle = new System.Windows.Forms.Label();
            this.cmbBookType = new System.Windows.Forms.ComboBox();
            this.lblBookType = new System.Windows.Forms.Label();
            this.txtDayOverdue = new System.Windows.Forms.TextBox();
            this.lblDayOverdue = new System.Windows.Forms.Label();
            this.lblCalculatorSubtitle = new System.Windows.Forms.Label();
            this.lblCalculatorTitle = new System.Windows.Forms.Label();
            this.panelLeftSection = new System.Windows.Forms.Panel();
            this.panelTableSection = new System.Windows.Forms.Panel();
            this.dta_Fines = new System.Windows.Forms.DataGridView();
            this.panelTableHeader = new System.Windows.Forms.Panel();
            this.cmbStatusFilter = new System.Windows.Forms.ComboBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.picSearchIcon = new System.Windows.Forms.PictureBox();
            this.lblRecordsSubtitle = new System.Windows.Forms.Label();
            this.lblRecordsTitle = new System.Windows.Forms.Label();
            this.panelMetrics = new System.Windows.Forms.Panel();
            this.panelMetricCard4 = new System.Windows.Forms.Panel();
            this.lblMetricValue4 = new System.Windows.Forms.Label();
            this.lblMetricTitle4 = new System.Windows.Forms.Label();
            this.panelMetricCard3 = new System.Windows.Forms.Panel();
            this.lblMetricValue3 = new System.Windows.Forms.Label();
            this.lblMetricTitle3 = new System.Windows.Forms.Label();
            this.panelMetricCard2 = new System.Windows.Forms.Panel();
            this.lblMetricValue2 = new System.Windows.Forms.Label();
            this.lblMetricTitle2 = new System.Windows.Forms.Label();
            this.panelMetricCard1 = new System.Windows.Forms.Panel();
            this.lblMetricValue1 = new System.Windows.Forms.Label();
            this.lblMetricTitle1 = new System.Windows.Forms.Label();
            this.panelMainContainer.SuspendLayout();
            this.panelRightSection.SuspendLayout();
            this.panelCalculator.SuspendLayout();
            this.panelLeftSection.SuspendLayout();
            this.panelTableSection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dta_Fines)).BeginInit();
            this.panelTableHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSearchIcon)).BeginInit();
            this.panelMetrics.SuspendLayout();
            this.panelMetricCard4.SuspendLayout();
            this.panelMetricCard3.SuspendLayout();
            this.panelMetricCard2.SuspendLayout();
            this.panelMetricCard1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMainContainer
            // 
            this.panelMainContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.panelMainContainer.Controls.Add(this.panelRightSection);
            this.panelMainContainer.Controls.Add(this.panelLeftSection);
            this.panelMainContainer.Controls.Add(this.panelMetrics);
            this.panelMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainContainer.Location = new System.Drawing.Point(0, 0);
            this.panelMainContainer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelMainContainer.Name = "panelMainContainer";
            this.panelMainContainer.Padding = new System.Windows.Forms.Padding(30, 30, 30, 30);
            this.panelMainContainer.Size = new System.Drawing.Size(1942, 1102);
            this.panelMainContainer.TabIndex = 0;
            // 
            // panelRightSection
            // 
            this.panelRightSection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRightSection.BackColor = System.Drawing.Color.White;
            this.panelRightSection.Controls.Add(this.panelCalculator);
            this.panelRightSection.Location = new System.Drawing.Point(1000, 200);
            this.panelRightSection.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelRightSection.Name = "panelRightSection";
            this.panelRightSection.Padding = new System.Windows.Forms.Padding(30, 30, 30, 30);
            this.panelRightSection.Size = new System.Drawing.Size(912, 872);
            this.panelRightSection.TabIndex = 2;
            // 
            // panelCalculator
            // 
            this.panelCalculator.BackColor = System.Drawing.Color.White;
            this.panelCalculator.Controls.Add(this.lblLostBookFee);
            this.panelCalculator.Controls.Add(this.lblGracePeriod);
            this.panelCalculator.Controls.Add(this.lblMaxFine);
            this.panelCalculator.Controls.Add(this.lblFineRulesTitle);
            this.panelCalculator.Controls.Add(this.lblCalculatedFine);
            this.panelCalculator.Controls.Add(this.lblCalculatedFineTitle);
            this.panelCalculator.Controls.Add(this.cmbBookType);
            this.panelCalculator.Controls.Add(this.lblBookType);
            this.panelCalculator.Controls.Add(this.txtDayOverdue);
            this.panelCalculator.Controls.Add(this.lblDayOverdue);
            this.panelCalculator.Controls.Add(this.lblCalculatorSubtitle);
            this.panelCalculator.Controls.Add(this.lblCalculatorTitle);
            this.panelCalculator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCalculator.Location = new System.Drawing.Point(30, 30);
            this.panelCalculator.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelCalculator.Name = "panelCalculator";
            this.panelCalculator.Padding = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.panelCalculator.Size = new System.Drawing.Size(852, 812);
            this.panelCalculator.TabIndex = 0;
            // 
            // lblLostBookFee
            // 
            this.lblLostBookFee.AutoSize = true;
            this.lblLostBookFee.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLostBookFee.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblLostBookFee.Location = new System.Drawing.Point(40, 400);
            this.lblLostBookFee.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLostBookFee.Name = "lblLostBookFee";
            this.lblLostBookFee.Size = new System.Drawing.Size(300, 24);
            this.lblLostBookFee.TabIndex = 11;
            this.lblLostBookFee.Text = "• Lost book fee: Replacement cost + ₱0";
            // 
            // lblGracePeriod
            // 
            this.lblGracePeriod.AutoSize = true;
            this.lblGracePeriod.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGracePeriod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblGracePeriod.Location = new System.Drawing.Point(40, 360);
            this.lblGracePeriod.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGracePeriod.Name = "lblGracePeriod";
            this.lblGracePeriod.Size = new System.Drawing.Size(200, 24);
            this.lblGracePeriod.TabIndex = 10;
            this.lblGracePeriod.Text = "• Grace period: 1 day";
            // 
            // lblMaxFine
            // 
            this.lblMaxFine.AutoSize = true;
            this.lblMaxFine.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxFine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMaxFine.Location = new System.Drawing.Point(40, 320);
            this.lblMaxFine.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMaxFine.Name = "lblMaxFine";
            this.lblMaxFine.Size = new System.Drawing.Size(250, 24);
            this.lblMaxFine.TabIndex = 9;
            this.lblMaxFine.Text = "• Maximum fine: ₱0 per book";
            // 
            // lblFineRulesTitle
            // 
            this.lblFineRulesTitle.AutoSize = true;
            this.lblFineRulesTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFineRulesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblFineRulesTitle.Location = new System.Drawing.Point(20, 280);
            this.lblFineRulesTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFineRulesTitle.Name = "lblFineRulesTitle";
            this.lblFineRulesTitle.Size = new System.Drawing.Size(200, 25);
            this.lblFineRulesTitle.TabIndex = 8;
            this.lblFineRulesTitle.Text = "Fine Calculation Rules:";
            // 
            // lblCalculatedFine
            // 
            this.lblCalculatedFine.AutoSize = true;
            this.lblCalculatedFine.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCalculatedFine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblCalculatedFine.Location = new System.Drawing.Point(20, 220);
            this.lblCalculatedFine.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCalculatedFine.Name = "lblCalculatedFine";
            this.lblCalculatedFine.Size = new System.Drawing.Size(50, 36);
            this.lblCalculatedFine.TabIndex = 7;
            this.lblCalculatedFine.Text = "₱0";
            // 
            // lblCalculatedFineTitle
            // 
            this.lblCalculatedFineTitle.AutoSize = true;
            this.lblCalculatedFineTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCalculatedFineTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCalculatedFineTitle.Location = new System.Drawing.Point(20, 190);
            this.lblCalculatedFineTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCalculatedFineTitle.Name = "lblCalculatedFineTitle";
            this.lblCalculatedFineTitle.Size = new System.Drawing.Size(180, 25);
            this.lblCalculatedFineTitle.TabIndex = 6;
            this.lblCalculatedFineTitle.Text = "Calculated fine:";
            // 
            // cmbBookType
            // 
            this.cmbBookType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBookType.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBookType.FormattingEnabled = true;
            this.cmbBookType.Items.AddRange(new object[] {
            "Regular (₱5/day)",
            "Reference (₱10/day)"});
            this.cmbBookType.Location = new System.Drawing.Point(20, 150);
            this.cmbBookType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbBookType.Name = "cmbBookType";
            this.cmbBookType.Size = new System.Drawing.Size(812, 30);
            this.cmbBookType.TabIndex = 5;
            this.cmbBookType.SelectedIndexChanged += new System.EventHandler(this.cmbBookType_SelectedIndexChanged);
            // 
            // lblBookType
            // 
            this.lblBookType.AutoSize = true;
            this.lblBookType.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBookType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblBookType.Location = new System.Drawing.Point(20, 120);
            this.lblBookType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBookType.Name = "lblBookType";
            this.lblBookType.Size = new System.Drawing.Size(90, 24);
            this.lblBookType.TabIndex = 4;
            this.lblBookType.Text = "Book Type:";
            // 
            // txtDayOverdue
            // 
            this.txtDayOverdue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDayOverdue.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDayOverdue.Location = new System.Drawing.Point(20, 80);
            this.txtDayOverdue.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDayOverdue.Name = "txtDayOverdue";
            this.txtDayOverdue.Size = new System.Drawing.Size(812, 28);
            this.txtDayOverdue.TabIndex = 3;
            this.txtDayOverdue.Text = "0";
            this.txtDayOverdue.TextChanged += new System.EventHandler(this.txtDayOverdue_TextChanged);
            // 
            // lblDayOverdue
            // 
            this.lblDayOverdue.AutoSize = true;
            this.lblDayOverdue.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDayOverdue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDayOverdue.Location = new System.Drawing.Point(20, 50);
            this.lblDayOverdue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDayOverdue.Name = "lblDayOverdue";
            this.lblDayOverdue.Size = new System.Drawing.Size(120, 24);
            this.lblDayOverdue.TabIndex = 2;
            this.lblDayOverdue.Text = "Day Overdue:";
            // 
            // lblCalculatorSubtitle
            // 
            this.lblCalculatorSubtitle.AutoSize = true;
            this.lblCalculatorSubtitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCalculatorSubtitle.ForeColor = System.Drawing.Color.Gray;
            this.lblCalculatorSubtitle.Location = new System.Drawing.Point(20, 30);
            this.lblCalculatorSubtitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCalculatorSubtitle.Name = "lblCalculatorSubtitle";
            this.lblCalculatorSubtitle.Size = new System.Drawing.Size(250, 24);
            this.lblCalculatorSubtitle.TabIndex = 1;
            this.lblCalculatorSubtitle.Text = "Calculate, overdue fines.";
            // 
            // lblCalculatorTitle
            // 
            this.lblCalculatorTitle.AutoSize = true;
            this.lblCalculatorTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCalculatorTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCalculatorTitle.Location = new System.Drawing.Point(20, 0);
            this.lblCalculatorTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCalculatorTitle.Name = "lblCalculatorTitle";
            this.lblCalculatorTitle.Size = new System.Drawing.Size(200, 31);
            this.lblCalculatorTitle.TabIndex = 0;
            this.lblCalculatorTitle.Text = "Fine Calculator";
            // 
            // panelLeftSection
            // 
            this.panelLeftSection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelLeftSection.BackColor = System.Drawing.Color.White;
            this.panelLeftSection.Controls.Add(this.panelTableSection);
            this.panelLeftSection.Controls.Add(this.panelTableHeader);
            this.panelLeftSection.Location = new System.Drawing.Point(30, 200);
            this.panelLeftSection.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelLeftSection.Name = "panelLeftSection";
            this.panelLeftSection.Padding = new System.Windows.Forms.Padding(30, 30, 30, 30);
            this.panelLeftSection.Size = new System.Drawing.Size(950, 872);
            this.panelLeftSection.TabIndex = 1;
            // 
            // panelTableSection
            // 
            this.panelTableSection.Controls.Add(this.dta_Fines);
            this.panelTableSection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTableSection.Location = new System.Drawing.Point(30, 100);
            this.panelTableSection.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelTableSection.Name = "panelTableSection";
            this.panelTableSection.Padding = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.panelTableSection.Size = new System.Drawing.Size(890, 742);
            this.panelTableSection.TabIndex = 1;
            // 
            // dta_Fines
            // 
            this.dta_Fines.AllowUserToAddRows = false;
            this.dta_Fines.AllowUserToDeleteRows = false;
            this.dta_Fines.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dta_Fines.BackgroundColor = System.Drawing.Color.White;
            this.dta_Fines.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dta_Fines.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dta_Fines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dta_Fines.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dta_Fines.Location = new System.Drawing.Point(0, 20);
            this.dta_Fines.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dta_Fines.MultiSelect = false;
            this.dta_Fines.Name = "dta_Fines";
            this.dta_Fines.ReadOnly = true;
            this.dta_Fines.RowHeadersVisible = false;
            this.dta_Fines.RowTemplate.Height = 50;
            this.dta_Fines.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dta_Fines.Size = new System.Drawing.Size(890, 722);
            this.dta_Fines.TabIndex = 0;
            this.dta_Fines.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dta_Fines_CellContentClick);
            // 
            // panelTableHeader
            // 
            this.panelTableHeader.Controls.Add(this.cmbStatusFilter);
            this.panelTableHeader.Controls.Add(this.txtSearch);
            this.panelTableHeader.Controls.Add(this.picSearchIcon);
            this.panelTableHeader.Controls.Add(this.lblRecordsSubtitle);
            this.panelTableHeader.Controls.Add(this.lblRecordsTitle);
            this.panelTableHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTableHeader.Location = new System.Drawing.Point(30, 30);
            this.panelTableHeader.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelTableHeader.Name = "panelTableHeader";
            this.panelTableHeader.Size = new System.Drawing.Size(890, 70);
            this.panelTableHeader.TabIndex = 0;
            // 
            // cmbStatusFilter
            // 
            this.cmbStatusFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbStatusFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatusFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatusFilter.FormattingEnabled = true;
            this.cmbStatusFilter.Items.AddRange(new object[] {
            "All Status",
            "Unpaid",
            "Paid"});
            this.cmbStatusFilter.Location = new System.Drawing.Point(700, 30);
            this.cmbStatusFilter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbStatusFilter.Name = "cmbStatusFilter";
            this.cmbStatusFilter.Size = new System.Drawing.Size(190, 30);
            this.cmbStatusFilter.TabIndex = 4;
            this.cmbStatusFilter.SelectedIndexChanged += new System.EventHandler(this.cmbStatusFilter_SelectedIndexChanged);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.Color.Gray;
            this.txtSearch.Location = new System.Drawing.Point(500, 30);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(192, 28);
            this.txtSearch.TabIndex = 3;
            this.txtSearch.Text = "Search transactions...";
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // picSearchIcon
            // 
            this.picSearchIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picSearchIcon.BackColor = System.Drawing.Color.Transparent;
            this.picSearchIcon.Location = new System.Drawing.Point(470, 30);
            this.picSearchIcon.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.picSearchIcon.Name = "picSearchIcon";
            this.picSearchIcon.Size = new System.Drawing.Size(30, 30);
            this.picSearchIcon.TabIndex = 2;
            this.picSearchIcon.TabStop = false;
            // 
            // lblRecordsSubtitle
            // 
            this.lblRecordsSubtitle.AutoSize = true;
            this.lblRecordsSubtitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordsSubtitle.ForeColor = System.Drawing.Color.Gray;
            this.lblRecordsSubtitle.Location = new System.Drawing.Point(0, 40);
            this.lblRecordsSubtitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRecordsSubtitle.Name = "lblRecordsSubtitle";
            this.lblRecordsSubtitle.Size = new System.Drawing.Size(250, 24);
            this.lblRecordsSubtitle.TabIndex = 1;
            this.lblRecordsSubtitle.Text = "View and manage all fines.";
            // 
            // lblRecordsTitle
            // 
            this.lblRecordsTitle.AutoSize = true;
            this.lblRecordsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblRecordsTitle.Location = new System.Drawing.Point(0, 0);
            this.lblRecordsTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRecordsTitle.Name = "lblRecordsTitle";
            this.lblRecordsTitle.Size = new System.Drawing.Size(200, 31);
            this.lblRecordsTitle.TabIndex = 0;
            this.lblRecordsTitle.Text = "Fine Records";
            // 
            // panelMetrics
            // 
            this.panelMetrics.Controls.Add(this.panelMetricCard4);
            this.panelMetrics.Controls.Add(this.panelMetricCard3);
            this.panelMetrics.Controls.Add(this.panelMetricCard2);
            this.panelMetrics.Controls.Add(this.panelMetricCard1);
            this.panelMetrics.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMetrics.Location = new System.Drawing.Point(30, 30);
            this.panelMetrics.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelMetrics.Name = "panelMetrics";
            this.panelMetrics.Size = new System.Drawing.Size(1882, 170);
            this.panelMetrics.TabIndex = 0;
            // 
            // panelMetricCard4
            // 
            this.panelMetricCard4.BackColor = System.Drawing.Color.White;
            this.panelMetricCard4.Controls.Add(this.lblMetricValue4);
            this.panelMetricCard4.Controls.Add(this.lblMetricTitle4);
            this.panelMetricCard4.Location = new System.Drawing.Point(1410, 0);
            this.panelMetricCard4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelMetricCard4.Name = "panelMetricCard4";
            this.panelMetricCard4.Padding = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.panelMetricCard4.Size = new System.Drawing.Size(450, 150);
            this.panelMetricCard4.TabIndex = 3;
            // 
            // lblMetricValue4
            // 
            this.lblMetricValue4.AutoSize = true;
            this.lblMetricValue4.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricValue4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricValue4.Location = new System.Drawing.Point(20, 20);
            this.lblMetricValue4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricValue4.Name = "lblMetricValue4";
            this.lblMetricValue4.Size = new System.Drawing.Size(42, 46);
            this.lblMetricValue4.TabIndex = 1;
            this.lblMetricValue4.Text = "0";
            // 
            // lblMetricTitle4
            // 
            this.lblMetricTitle4.AutoSize = true;
            this.lblMetricTitle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTitle4.ForeColor = System.Drawing.Color.Gray;
            this.lblMetricTitle4.Location = new System.Drawing.Point(20, 80);
            this.lblMetricTitle4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricTitle4.Name = "lblMetricTitle4";
            this.lblMetricTitle4.Size = new System.Drawing.Size(120, 24);
            this.lblMetricTitle4.TabIndex = 0;
            this.lblMetricTitle4.Text = "Total Records";
            // 
            // panelMetricCard3
            // 
            this.panelMetricCard3.BackColor = System.Drawing.Color.White;
            this.panelMetricCard3.Controls.Add(this.lblMetricValue3);
            this.panelMetricCard3.Controls.Add(this.lblMetricTitle3);
            this.panelMetricCard3.Location = new System.Drawing.Point(950, 0);
            this.panelMetricCard3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelMetricCard3.Name = "panelMetricCard3";
            this.panelMetricCard3.Padding = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.panelMetricCard3.Size = new System.Drawing.Size(450, 150);
            this.panelMetricCard3.TabIndex = 2;
            // 
            // lblMetricValue3
            // 
            this.lblMetricValue3.AutoSize = true;
            this.lblMetricValue3.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricValue3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricValue3.Location = new System.Drawing.Point(20, 20);
            this.lblMetricValue3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricValue3.Name = "lblMetricValue3";
            this.lblMetricValue3.Size = new System.Drawing.Size(42, 46);
            this.lblMetricValue3.TabIndex = 1;
            this.lblMetricValue3.Text = "0";
            // 
            // lblMetricTitle3
            // 
            this.lblMetricTitle3.AutoSize = true;
            this.lblMetricTitle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTitle3.ForeColor = System.Drawing.Color.Gray;
            this.lblMetricTitle3.Location = new System.Drawing.Point(20, 80);
            this.lblMetricTitle3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricTitle3.Name = "lblMetricTitle3";
            this.lblMetricTitle3.Size = new System.Drawing.Size(140, 24);
            this.lblMetricTitle3.TabIndex = 0;
            this.lblMetricTitle3.Text = "Unpaid Fines";
            // 
            // panelMetricCard2
            // 
            this.panelMetricCard2.BackColor = System.Drawing.Color.White;
            this.panelMetricCard2.Controls.Add(this.lblMetricValue2);
            this.panelMetricCard2.Controls.Add(this.lblMetricTitle2);
            this.panelMetricCard2.Location = new System.Drawing.Point(490, 0);
            this.panelMetricCard2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelMetricCard2.Name = "panelMetricCard2";
            this.panelMetricCard2.Padding = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.panelMetricCard2.Size = new System.Drawing.Size(450, 150);
            this.panelMetricCard2.TabIndex = 1;
            // 
            // lblMetricValue2
            // 
            this.lblMetricValue2.AutoSize = true;
            this.lblMetricValue2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricValue2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricValue2.Location = new System.Drawing.Point(20, 20);
            this.lblMetricValue2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricValue2.Name = "lblMetricValue2";
            this.lblMetricValue2.Size = new System.Drawing.Size(60, 46);
            this.lblMetricValue2.TabIndex = 1;
            this.lblMetricValue2.Text = "₱0";
            // 
            // lblMetricTitle2
            // 
            this.lblMetricTitle2.AutoSize = true;
            this.lblMetricTitle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTitle2.ForeColor = System.Drawing.Color.Gray;
            this.lblMetricTitle2.Location = new System.Drawing.Point(20, 80);
            this.lblMetricTitle2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricTitle2.Name = "lblMetricTitle2";
            this.lblMetricTitle2.Size = new System.Drawing.Size(100, 24);
            this.lblMetricTitle2.TabIndex = 0;
            this.lblMetricTitle2.Text = "Collected";
            // 
            // panelMetricCard1
            // 
            this.panelMetricCard1.BackColor = System.Drawing.Color.White;
            this.panelMetricCard1.Controls.Add(this.lblMetricValue1);
            this.panelMetricCard1.Controls.Add(this.lblMetricTitle1);
            this.panelMetricCard1.Location = new System.Drawing.Point(30, 0);
            this.panelMetricCard1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelMetricCard1.Name = "panelMetricCard1";
            this.panelMetricCard1.Padding = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.panelMetricCard1.Size = new System.Drawing.Size(450, 150);
            this.panelMetricCard1.TabIndex = 0;
            // 
            // lblMetricValue1
            // 
            this.lblMetricValue1.AutoSize = true;
            this.lblMetricValue1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricValue1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricValue1.Location = new System.Drawing.Point(20, 20);
            this.lblMetricValue1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricValue1.Name = "lblMetricValue1";
            this.lblMetricValue1.Size = new System.Drawing.Size(60, 46);
            this.lblMetricValue1.TabIndex = 1;
            this.lblMetricValue1.Text = "₱0";
            // 
            // lblMetricTitle1
            // 
            this.lblMetricTitle1.AutoSize = true;
            this.lblMetricTitle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTitle1.ForeColor = System.Drawing.Color.Gray;
            this.lblMetricTitle1.Location = new System.Drawing.Point(20, 80);
            this.lblMetricTitle1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricTitle1.Name = "lblMetricTitle1";
            this.lblMetricTitle1.Size = new System.Drawing.Size(130, 24);
            this.lblMetricTitle1.TabIndex = 0;
            this.lblMetricTitle1.Text = "Pending Fines";
            // 
            // FinesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1942, 1102);
            this.Controls.Add(this.panelMainContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FinesForm";
            this.Text = "Fines";
            this.Load += new System.EventHandler(this.FinesForm_Load);
            this.panelMainContainer.ResumeLayout(false);
            this.panelRightSection.ResumeLayout(false);
            this.panelCalculator.ResumeLayout(false);
            this.panelCalculator.PerformLayout();
            this.panelLeftSection.ResumeLayout(false);
            this.panelTableSection.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dta_Fines)).EndInit();
            this.panelTableHeader.ResumeLayout(false);
            this.panelTableHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSearchIcon)).EndInit();
            this.panelMetrics.ResumeLayout(false);
            this.panelMetricCard4.ResumeLayout(false);
            this.panelMetricCard4.PerformLayout();
            this.panelMetricCard3.ResumeLayout(false);
            this.panelMetricCard3.PerformLayout();
            this.panelMetricCard2.ResumeLayout(false);
            this.panelMetricCard2.PerformLayout();
            this.panelMetricCard1.ResumeLayout(false);
            this.panelMetricCard1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMainContainer;
        private System.Windows.Forms.Panel panelMetrics;
        private System.Windows.Forms.Panel panelMetricCard1;
        private System.Windows.Forms.Label lblMetricValue1;
        private System.Windows.Forms.Label lblMetricTitle1;
        private System.Windows.Forms.Panel panelMetricCard2;
        private System.Windows.Forms.Label lblMetricValue2;
        private System.Windows.Forms.Label lblMetricTitle2;
        private System.Windows.Forms.Panel panelMetricCard3;
        private System.Windows.Forms.Label lblMetricValue3;
        private System.Windows.Forms.Label lblMetricTitle3;
        private System.Windows.Forms.Panel panelMetricCard4;
        private System.Windows.Forms.Label lblMetricValue4;
        private System.Windows.Forms.Label lblMetricTitle4;
        private System.Windows.Forms.Panel panelLeftSection;
        private System.Windows.Forms.Panel panelTableHeader;
        private System.Windows.Forms.Label lblRecordsTitle;
        private System.Windows.Forms.Label lblRecordsSubtitle;
        private System.Windows.Forms.PictureBox picSearchIcon;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cmbStatusFilter;
        private System.Windows.Forms.Panel panelTableSection;
        private System.Windows.Forms.DataGridView dta_Fines;
        private System.Windows.Forms.Panel panelRightSection;
        private System.Windows.Forms.Panel panelCalculator;
        private System.Windows.Forms.Label lblCalculatorTitle;
        private System.Windows.Forms.Label lblCalculatorSubtitle;
        private System.Windows.Forms.Label lblDayOverdue;
        private System.Windows.Forms.TextBox txtDayOverdue;
        private System.Windows.Forms.Label lblBookType;
        private System.Windows.Forms.ComboBox cmbBookType;
        private System.Windows.Forms.Label lblCalculatedFineTitle;
        private System.Windows.Forms.Label lblCalculatedFine;
        private System.Windows.Forms.Label lblFineRulesTitle;
        private System.Windows.Forms.Label lblMaxFine;
        private System.Windows.Forms.Label lblGracePeriod;
        private System.Windows.Forms.Label lblLostBookFee;
    }
}
