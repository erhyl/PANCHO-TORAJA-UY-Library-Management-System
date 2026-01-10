namespace Project5LMS.Forms.Admin.Reservations
{
    partial class AdminReservationsForm
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
            this.dataGridViewReservations = new System.Windows.Forms.DataGridView();
            this.colReservationID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMember = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBook = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReservedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colExpiryDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPriority = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActions = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelFilter = new System.Windows.Forms.Panel();
            this.btnFilter = new System.Windows.Forms.Button();
            this.panelMetrics = new System.Windows.Forms.Panel();
            this.panelMetricExpired = new System.Windows.Forms.Panel();
            this.lblMetricExpiredValue = new System.Windows.Forms.Label();
            this.lblMetricExpiredTitle = new System.Windows.Forms.Label();
            this.panelMetricFulfilled = new System.Windows.Forms.Panel();
            this.lblMetricFulfilledValue = new System.Windows.Forms.Label();
            this.lblMetricFulfilledTitle = new System.Windows.Forms.Label();
            this.panelMetricPending = new System.Windows.Forms.Panel();
            this.lblMetricPendingValue = new System.Windows.Forms.Label();
            this.lblMetricPendingTitle = new System.Windows.Forms.Label();
            this.panelMetricTotal = new System.Windows.Forms.Panel();
            this.lblMetricTotalValue = new System.Windows.Forms.Label();
            this.lblMetricTotalTitle = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelMetricReady = new System.Windows.Forms.Panel();
            this.lblMetricReadyValue = new System.Windows.Forms.Label();
            this.lblMetricReadyTitle = new System.Windows.Forms.Label();
            this.panelMainContainer.SuspendLayout();
            this.panelTableContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReservations)).BeginInit();
            this.panelFilter.SuspendLayout();
            this.panelMetrics.SuspendLayout();
            this.panelMetricExpired.SuspendLayout();
            this.panelMetricFulfilled.SuspendLayout();
            this.panelMetricPending.SuspendLayout();
            this.panelMetricTotal.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panelMetricReady.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMainContainer
            // 
            this.panelMainContainer.AutoScroll = true;
            this.panelMainContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelMainContainer.Controls.Add(this.panelTableContainer);
            this.panelMainContainer.Controls.Add(this.panelFilter);
            this.panelMainContainer.Controls.Add(this.panelMetrics);
            this.panelMainContainer.Controls.Add(this.panelHeader);
            this.panelMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainContainer.Location = new System.Drawing.Point(0, 0);
            this.panelMainContainer.Name = "panelMainContainer";
            this.panelMainContainer.Padding = new System.Windows.Forms.Padding(24, 24, 24, 24);
            this.panelMainContainer.Size = new System.Drawing.Size(1200, 800);
            this.panelMainContainer.TabIndex = 0;
            // 
            // panelTableContainer
            // 
            this.panelTableContainer.BackColor = System.Drawing.Color.White;
            this.panelTableContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTableContainer.Controls.Add(this.dataGridViewReservations);
            this.panelTableContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTableContainer.Location = new System.Drawing.Point(24, 194);
            this.panelTableContainer.Name = "panelTableContainer";
            this.panelTableContainer.Padding = new System.Windows.Forms.Padding(16, 16, 16, 16);
            this.panelTableContainer.Size = new System.Drawing.Size(1152, 582);
            this.panelTableContainer.TabIndex = 3;
            // 
            // dataGridViewReservations
            // 
            this.dataGridViewReservations.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.dataGridViewReservations.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewReservations.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewReservations.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewReservations.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewReservations.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewReservations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewReservations.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colReservationID,
            this.colMember,
            this.colBook,
            this.colReservedDate,
            this.colExpiryDate,
            this.colPriority,
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
            this.dataGridViewReservations.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewReservations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewReservations.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dataGridViewReservations.Location = new System.Drawing.Point(16, 16);
            this.dataGridViewReservations.MultiSelect = false;
            this.dataGridViewReservations.Name = "dataGridViewReservations";
            this.dataGridViewReservations.RowHeadersVisible = false;
            this.dataGridViewReservations.RowHeadersWidth = 51;
            this.dataGridViewReservations.RowTemplate.Height = 60;
            this.dataGridViewReservations.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewReservations.Size = new System.Drawing.Size(1118, 548);
            this.dataGridViewReservations.TabIndex = 0;
            // 
            // colReservationID
            // 
            this.colReservationID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colReservationID.DataPropertyName = "ReservationID";
            this.colReservationID.HeaderText = "RESERVATION ID";
            this.colReservationID.MinimumWidth = 6;
            this.colReservationID.Name = "colReservationID";
            this.colReservationID.Width = 150;
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
            this.colBook.Width = 300;
            // 
            // colReservedDate
            // 
            this.colReservedDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colReservedDate.DataPropertyName = "ReservedDate";
            this.colReservedDate.HeaderText = "RESERVED DATE";
            this.colReservedDate.MinimumWidth = 6;
            this.colReservedDate.Name = "colReservedDate";
            this.colReservedDate.Width = 130;
            // 
            // colExpiryDate
            // 
            this.colExpiryDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colExpiryDate.DataPropertyName = "ExpiryDate";
            this.colExpiryDate.HeaderText = "EXPIRY DATE";
            this.colExpiryDate.MinimumWidth = 6;
            this.colExpiryDate.Name = "colExpiryDate";
            this.colExpiryDate.Width = 130;
            // 
            // colPriority
            // 
            this.colPriority.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colPriority.DataPropertyName = "Priority";
            this.colPriority.HeaderText = "PRIORITY";
            this.colPriority.MinimumWidth = 6;
            this.colPriority.Name = "colPriority";
            this.colPriority.Width = 125;
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
            // colActions
            // 
            this.colActions.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colActions.DataPropertyName = "Actions";
            this.colActions.HeaderText = "ACTIONS";
            this.colActions.MinimumWidth = 6;
            this.colActions.Name = "colActions";
            this.colActions.Width = 200;
            // 
            // panelFilter
            // 
            this.panelFilter.Controls.Add(this.btnFilter);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Location = new System.Drawing.Point(24, 159);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Size = new System.Drawing.Size(1152, 35);
            this.panelFilter.TabIndex = 2;
            // 
            // btnFilter
            // 
            this.btnFilter.BackColor = System.Drawing.Color.White;
            this.btnFilter.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilter.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnFilter.Location = new System.Drawing.Point(1037, 3);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(112, 29);
            this.btnFilter.TabIndex = 0;
            this.btnFilter.Text = "All Status";
            this.btnFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFilter.UseVisualStyleBackColor = false;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // panelMetrics
            // 
            this.panelMetrics.BackColor = System.Drawing.Color.White;
            this.panelMetrics.Controls.Add(this.panelMetricExpired);
            this.panelMetrics.Controls.Add(this.panelMetricFulfilled);
            this.panelMetrics.Controls.Add(this.panelMetricPending);
            this.panelMetrics.Controls.Add(this.panelMetricTotal);
            this.panelMetrics.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMetrics.Location = new System.Drawing.Point(24, 96);
            this.panelMetrics.Name = "panelMetrics";
            this.panelMetrics.Size = new System.Drawing.Size(1152, 63);
            this.panelMetrics.TabIndex = 1;
            // 
            // panelMetricExpired
            // 
            this.panelMetricExpired.BackColor = System.Drawing.Color.White;
            this.panelMetricExpired.Controls.Add(this.lblMetricExpiredValue);
            this.panelMetricExpired.Controls.Add(this.lblMetricExpiredTitle);
            this.panelMetricExpired.Location = new System.Drawing.Point(938, 0);
            this.panelMetricExpired.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.panelMetricExpired.Name = "panelMetricExpired";
            this.panelMetricExpired.Padding = new System.Windows.Forms.Padding(16, 16, 16, 16);
            this.panelMetricExpired.Size = new System.Drawing.Size(214, 63);
            this.panelMetricExpired.TabIndex = 4;
            // 
            // lblMetricExpiredValue
            // 
            this.lblMetricExpiredValue.AutoSize = true;
            this.lblMetricExpiredValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricExpiredValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricExpiredValue.Location = new System.Drawing.Point(19, 24);
            this.lblMetricExpiredValue.Name = "lblMetricExpiredValue";
            this.lblMetricExpiredValue.Size = new System.Drawing.Size(33, 37);
            this.lblMetricExpiredValue.TabIndex = 2;
            this.lblMetricExpiredValue.Text = "0";
            // 
            // lblMetricExpiredTitle
            // 
            this.lblMetricExpiredTitle.AutoSize = true;
            this.lblMetricExpiredTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricExpiredTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricExpiredTitle.Location = new System.Drawing.Point(19, 8);
            this.lblMetricExpiredTitle.Name = "lblMetricExpiredTitle";
            this.lblMetricExpiredTitle.Size = new System.Drawing.Size(45, 15);
            this.lblMetricExpiredTitle.TabIndex = 1;
            this.lblMetricExpiredTitle.Text = "Expired";
            // 
            // panelMetricFulfilled
            // 
            this.panelMetricFulfilled.BackColor = System.Drawing.Color.White;
            this.panelMetricFulfilled.Controls.Add(this.lblMetricFulfilledValue);
            this.panelMetricFulfilled.Controls.Add(this.lblMetricFulfilledTitle);
            this.panelMetricFulfilled.Location = new System.Drawing.Point(634, 0);
            this.panelMetricFulfilled.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.panelMetricFulfilled.Name = "panelMetricFulfilled";
            this.panelMetricFulfilled.Padding = new System.Windows.Forms.Padding(16, 16, 16, 16);
            this.panelMetricFulfilled.Size = new System.Drawing.Size(214, 63);
            this.panelMetricFulfilled.TabIndex = 3;
            // 
            // lblMetricFulfilledValue
            // 
            this.lblMetricFulfilledValue.AutoSize = true;
            this.lblMetricFulfilledValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricFulfilledValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricFulfilledValue.Location = new System.Drawing.Point(19, 24);
            this.lblMetricFulfilledValue.Name = "lblMetricFulfilledValue";
            this.lblMetricFulfilledValue.Size = new System.Drawing.Size(33, 37);
            this.lblMetricFulfilledValue.TabIndex = 2;
            this.lblMetricFulfilledValue.Text = "0";
            // 
            // lblMetricFulfilledTitle
            // 
            this.lblMetricFulfilledTitle.AutoSize = true;
            this.lblMetricFulfilledTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricFulfilledTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricFulfilledTitle.Location = new System.Drawing.Point(12, 8);
            this.lblMetricFulfilledTitle.Name = "lblMetricFulfilledTitle";
            this.lblMetricFulfilledTitle.Size = new System.Drawing.Size(49, 15);
            this.lblMetricFulfilledTitle.TabIndex = 1;
            this.lblMetricFulfilledTitle.Text = "Fulfilled";
            // 
            // panelMetricPending
            // 
            this.panelMetricPending.BackColor = System.Drawing.Color.White;
            this.panelMetricPending.Controls.Add(this.lblMetricPendingValue);
            this.panelMetricPending.Controls.Add(this.lblMetricPendingTitle);
            this.panelMetricPending.Location = new System.Drawing.Point(328, 0);
            this.panelMetricPending.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.panelMetricPending.Name = "panelMetricPending";
            this.panelMetricPending.Padding = new System.Windows.Forms.Padding(16, 16, 16, 16);
            this.panelMetricPending.Size = new System.Drawing.Size(214, 63);
            this.panelMetricPending.TabIndex = 1;
            // 
            // lblMetricPendingValue
            // 
            this.lblMetricPendingValue.AutoSize = true;
            this.lblMetricPendingValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricPendingValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricPendingValue.Location = new System.Drawing.Point(19, 24);
            this.lblMetricPendingValue.Name = "lblMetricPendingValue";
            this.lblMetricPendingValue.Size = new System.Drawing.Size(33, 37);
            this.lblMetricPendingValue.TabIndex = 2;
            this.lblMetricPendingValue.Text = "0";
            // 
            // lblMetricPendingTitle
            // 
            this.lblMetricPendingTitle.AutoSize = true;
            this.lblMetricPendingTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricPendingTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricPendingTitle.Location = new System.Drawing.Point(12, 8);
            this.lblMetricPendingTitle.Name = "lblMetricPendingTitle";
            this.lblMetricPendingTitle.Size = new System.Drawing.Size(51, 15);
            this.lblMetricPendingTitle.TabIndex = 1;
            this.lblMetricPendingTitle.Text = "Pending";
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
            this.panelMetricTotal.Size = new System.Drawing.Size(214, 63);
            this.panelMetricTotal.TabIndex = 0;
            // 
            // lblMetricTotalValue
            // 
            this.lblMetricTotalValue.AutoSize = true;
            this.lblMetricTotalValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTotalValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricTotalValue.Location = new System.Drawing.Point(19, 24);
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
            this.lblMetricTotalTitle.Location = new System.Drawing.Point(15, 8);
            this.lblMetricTotalTitle.Name = "lblMetricTotalTitle";
            this.lblMetricTotalTitle.Size = new System.Drawing.Size(102, 15);
            this.lblMetricTotalTitle.TabIndex = 1;
            this.lblMetricTotalTitle.Text = "Total Reservations";
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.lblSubtitle);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Controls.Add(this.panelMetricReady);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(24, 24);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1152, 72);
            this.panelHeader.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblSubtitle.Location = new System.Drawing.Point(14, 44);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(286, 19);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Manage book reservations and hold requests";
            this.lblSubtitle.Click += new System.EventHandler(this.lblSubtitle_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitle.Location = new System.Drawing.Point(3, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(403, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Reservation Management";
            // 
            // panelMetricReady
            // 
            this.panelMetricReady.BackColor = System.Drawing.Color.White;
            this.panelMetricReady.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMetricReady.Controls.Add(this.lblMetricReadyValue);
            this.panelMetricReady.Controls.Add(this.lblMetricReadyTitle);
            this.panelMetricReady.Location = new System.Drawing.Point(490, 72);
            this.panelMetricReady.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.panelMetricReady.Name = "panelMetricReady";
            this.panelMetricReady.Padding = new System.Windows.Forms.Padding(16, 16, 16, 16);
            this.panelMetricReady.Size = new System.Drawing.Size(214, 63);
            this.panelMetricReady.TabIndex = 2;
            // 
            // lblMetricReadyValue
            // 
            this.lblMetricReadyValue.AutoSize = true;
            this.lblMetricReadyValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricReadyValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricReadyValue.Location = new System.Drawing.Point(13, 24);
            this.lblMetricReadyValue.Name = "lblMetricReadyValue";
            this.lblMetricReadyValue.Size = new System.Drawing.Size(33, 37);
            this.lblMetricReadyValue.TabIndex = 2;
            this.lblMetricReadyValue.Text = "0";
            this.lblMetricReadyValue.Click += new System.EventHandler(this.lblMetricReadyValue_Click);
            // 
            // lblMetricReadyTitle
            // 
            this.lblMetricReadyTitle.AutoSize = true;
            this.lblMetricReadyTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricReadyTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricReadyTitle.Location = new System.Drawing.Point(16, 8);
            this.lblMetricReadyTitle.Name = "lblMetricReadyTitle";
            this.lblMetricReadyTitle.Size = new System.Drawing.Size(96, 15);
            this.lblMetricReadyTitle.TabIndex = 1;
            this.lblMetricReadyTitle.Text = "Ready for Pickup";
            // 
            // AdminReservationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.panelMainContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AdminReservationsForm";
            this.Text = "Reservation Management";
            this.Load += new System.EventHandler(this.AdminReservationsForm_Load);
            this.panelMainContainer.ResumeLayout(false);
            this.panelTableContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReservations)).EndInit();
            this.panelFilter.ResumeLayout(false);
            this.panelMetrics.ResumeLayout(false);
            this.panelMetricExpired.ResumeLayout(false);
            this.panelMetricExpired.PerformLayout();
            this.panelMetricFulfilled.ResumeLayout(false);
            this.panelMetricFulfilled.PerformLayout();
            this.panelMetricPending.ResumeLayout(false);
            this.panelMetricPending.PerformLayout();
            this.panelMetricTotal.ResumeLayout(false);
            this.panelMetricTotal.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelMetricReady.ResumeLayout(false);
            this.panelMetricReady.PerformLayout();
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
        private System.Windows.Forms.Panel panelMetricPending;
        private System.Windows.Forms.Label lblMetricPendingValue;
        private System.Windows.Forms.Label lblMetricPendingTitle;
        private System.Windows.Forms.Panel panelMetricReady;
        private System.Windows.Forms.Label lblMetricReadyValue;
        private System.Windows.Forms.Label lblMetricReadyTitle;
        private System.Windows.Forms.Panel panelMetricFulfilled;
        private System.Windows.Forms.Label lblMetricFulfilledValue;
        private System.Windows.Forms.Label lblMetricFulfilledTitle;
        private System.Windows.Forms.Panel panelMetricExpired;
        private System.Windows.Forms.Label lblMetricExpiredValue;
        private System.Windows.Forms.Label lblMetricExpiredTitle;
        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.Panel panelTableContainer;
        private System.Windows.Forms.DataGridView dataGridViewReservations;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReservationID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMember;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBook;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReservedDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colExpiryDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPriority;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colActions;
        private System.Windows.Forms.Button btnFilter;
    }
}