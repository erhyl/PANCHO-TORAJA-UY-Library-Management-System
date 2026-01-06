namespace Project5LMS.Forms.LibraryStaff.Reservations
{
    partial class StaffReservationsForm
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
            this.panelReservationsList = new System.Windows.Forms.Panel();
            this.panelCreateReservation = new System.Windows.Forms.Panel();
            this.btnCreateReservation = new System.Windows.Forms.Button();
            this.txtBookID = new System.Windows.Forms.TextBox();
            this.lblBookID = new System.Windows.Forms.Label();
            this.txtMemberID = new System.Windows.Forms.TextBox();
            this.lblMemberID = new System.Windows.Forms.Label();
            this.lblCreateReservationTitle = new System.Windows.Forms.Label();
            this.panelFilterTabs = new System.Windows.Forms.Panel();
            this.btnFilterExpired = new System.Windows.Forms.Button();
            this.btnFilterReady = new System.Windows.Forms.Button();
            this.btnFilterActive = new System.Windows.Forms.Button();
            this.btnFilterAll = new System.Windows.Forms.Button();
            this.panelMetrics = new System.Windows.Forms.Panel();
            this.panelMetricExpired = new System.Windows.Forms.Panel();
            this.lblMetricExpiredValue = new System.Windows.Forms.Label();
            this.lblMetricExpiredTitle = new System.Windows.Forms.Label();
            this.panelMetricReady = new System.Windows.Forms.Panel();
            this.lblMetricReadyValue = new System.Windows.Forms.Label();
            this.lblMetricReadyTitle = new System.Windows.Forms.Label();
            this.panelMetricActive = new System.Windows.Forms.Panel();
            this.lblMetricActiveValue = new System.Windows.Forms.Label();
            this.lblMetricActiveTitle = new System.Windows.Forms.Label();
            this.panelMetricTotal = new System.Windows.Forms.Panel();
            this.lblMetricTotalValue = new System.Windows.Forms.Label();
            this.lblMetricTotalTitle = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelMainContainer.SuspendLayout();
            this.panelReservationsList.SuspendLayout();
            this.panelCreateReservation.SuspendLayout();
            this.panelFilterTabs.SuspendLayout();
            this.panelMetrics.SuspendLayout();
            this.panelMetricExpired.SuspendLayout();
            this.panelMetricReady.SuspendLayout();
            this.panelMetricActive.SuspendLayout();
            this.panelMetricTotal.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMainContainer
            // 
            this.panelMainContainer.AutoScroll = true;
            this.panelMainContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelMainContainer.Controls.Add(this.panelReservationsList);
            this.panelMainContainer.Controls.Add(this.panelFilterTabs);
            this.panelMainContainer.Controls.Add(this.panelMetrics);
            this.panelMainContainer.Controls.Add(this.panelHeader);
            this.panelMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainContainer.Location = new System.Drawing.Point(0, 0);
            this.panelMainContainer.Margin = new System.Windows.Forms.Padding(4);
            this.panelMainContainer.Name = "panelMainContainer";
            this.panelMainContainer.Padding = new System.Windows.Forms.Padding(32, 30, 32, 30);
            this.panelMainContainer.Size = new System.Drawing.Size(1600, 985);
            this.panelMainContainer.TabIndex = 0;
            // 
            // panelReservationsList
            // 
            this.panelReservationsList.AutoScroll = true;
            this.panelReservationsList.BackColor = System.Drawing.Color.White;
            this.panelReservationsList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelReservationsList.Controls.Add(this.panelCreateReservation);
            this.panelReservationsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelReservationsList.Location = new System.Drawing.Point(32, 275);
            this.panelReservationsList.Margin = new System.Windows.Forms.Padding(4);
            this.panelReservationsList.Name = "panelReservationsList";
            this.panelReservationsList.Padding = new System.Windows.Forms.Padding(20);
            this.panelReservationsList.Size = new System.Drawing.Size(1536, 680);
            this.panelReservationsList.TabIndex = 3;
            // 
            // panelCreateReservation
            // 
            this.panelCreateReservation.BackColor = System.Drawing.Color.White;
            this.panelCreateReservation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCreateReservation.Controls.Add(this.btnCreateReservation);
            this.panelCreateReservation.Controls.Add(this.txtBookID);
            this.panelCreateReservation.Controls.Add(this.lblBookID);
            this.panelCreateReservation.Controls.Add(this.txtMemberID);
            this.panelCreateReservation.Controls.Add(this.lblMemberID);
            this.panelCreateReservation.Controls.Add(this.lblCreateReservationTitle);
            this.panelCreateReservation.Location = new System.Drawing.Point(-1, -1);
            this.panelCreateReservation.Margin = new System.Windows.Forms.Padding(4);
            this.panelCreateReservation.Name = "panelCreateReservation";
            this.panelCreateReservation.Padding = new System.Windows.Forms.Padding(30);
            this.panelCreateReservation.Size = new System.Drawing.Size(1536, 119);
            this.panelCreateReservation.TabIndex = 4;
            // 
            // btnCreateReservation
            // 
            this.btnCreateReservation.BackColor = System.Drawing.Color.Maroon;
            this.btnCreateReservation.FlatAppearance.BorderSize = 0;
            this.btnCreateReservation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateReservation.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateReservation.ForeColor = System.Drawing.Color.White;
            this.btnCreateReservation.Location = new System.Drawing.Point(604, 58);
            this.btnCreateReservation.Margin = new System.Windows.Forms.Padding(4);
            this.btnCreateReservation.Name = "btnCreateReservation";
            this.btnCreateReservation.Size = new System.Drawing.Size(200, 45);
            this.btnCreateReservation.TabIndex = 5;
            this.btnCreateReservation.Text = "Create Reservation";
            this.btnCreateReservation.UseVisualStyleBackColor = false;
            this.btnCreateReservation.Click += new System.EventHandler(this.btnCreateReservation_Click);
            // 
            // txtBookID
            // 
            this.txtBookID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBookID.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBookID.ForeColor = System.Drawing.Color.Gray;
            this.txtBookID.Location = new System.Drawing.Point(320, 71);
            this.txtBookID.Margin = new System.Windows.Forms.Padding(4);
            this.txtBookID.Name = "txtBookID";
            this.txtBookID.Size = new System.Drawing.Size(190, 32);
            this.txtBookID.TabIndex = 4;
            this.txtBookID.Text = "Enter book ID";
            this.txtBookID.Enter += new System.EventHandler(this.txtBookID_Enter);
            this.txtBookID.Leave += new System.EventHandler(this.txtBookID_Leave);
            // 
            // lblBookID
            // 
            this.lblBookID.AutoSize = true;
            this.lblBookID.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBookID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblBookID.Location = new System.Drawing.Point(316, 44);
            this.lblBookID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBookID.Name = "lblBookID";
            this.lblBookID.Size = new System.Drawing.Size(70, 23);
            this.lblBookID.TabIndex = 3;
            this.lblBookID.Text = "Book ID";
            // 
            // txtMemberID
            // 
            this.txtMemberID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMemberID.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMemberID.ForeColor = System.Drawing.Color.Gray;
            this.txtMemberID.Location = new System.Drawing.Point(12, 71);
            this.txtMemberID.Margin = new System.Windows.Forms.Padding(4);
            this.txtMemberID.Name = "txtMemberID";
            this.txtMemberID.Size = new System.Drawing.Size(180, 32);
            this.txtMemberID.TabIndex = 2;
            this.txtMemberID.Text = "Enter member ID";
            this.txtMemberID.Enter += new System.EventHandler(this.txtMemberID_Enter);
            this.txtMemberID.Leave += new System.EventHandler(this.txtMemberID_Leave);
            // 
            // lblMemberID
            // 
            this.lblMemberID.AutoSize = true;
            this.lblMemberID.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMemberID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMemberID.Location = new System.Drawing.Point(8, 44);
            this.lblMemberID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMemberID.Name = "lblMemberID";
            this.lblMemberID.Size = new System.Drawing.Size(96, 23);
            this.lblMemberID.TabIndex = 1;
            this.lblMemberID.Text = "Member ID";
            // 
            // lblCreateReservationTitle
            // 
            this.lblCreateReservationTitle.AutoSize = true;
            this.lblCreateReservationTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreateReservationTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCreateReservationTitle.Location = new System.Drawing.Point(1, -1);
            this.lblCreateReservationTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCreateReservationTitle.Name = "lblCreateReservationTitle";
            this.lblCreateReservationTitle.Size = new System.Drawing.Size(287, 32);
            this.lblCreateReservationTitle.TabIndex = 0;
            this.lblCreateReservationTitle.Text = "Create New Reservation";
            // 
            // panelFilterTabs
            // 
            this.panelFilterTabs.BackColor = System.Drawing.Color.White;
            this.panelFilterTabs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFilterTabs.Controls.Add(this.btnFilterExpired);
            this.panelFilterTabs.Controls.Add(this.btnFilterReady);
            this.panelFilterTabs.Controls.Add(this.btnFilterActive);
            this.panelFilterTabs.Controls.Add(this.btnFilterAll);
            this.panelFilterTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFilterTabs.Location = new System.Drawing.Point(32, 275);
            this.panelFilterTabs.Margin = new System.Windows.Forms.Padding(4);
            this.panelFilterTabs.Name = "panelFilterTabs";
            this.panelFilterTabs.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);
            this.panelFilterTabs.Size = new System.Drawing.Size(1536, 680);
            this.panelFilterTabs.TabIndex = 2;
            // 
            // btnFilterExpired
            // 
            this.btnFilterExpired.BackColor = System.Drawing.Color.Transparent;
            this.btnFilterExpired.FlatAppearance.BorderSize = 0;
            this.btnFilterExpired.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilterExpired.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilterExpired.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnFilterExpired.Location = new System.Drawing.Point(300, 15);
            this.btnFilterExpired.Margin = new System.Windows.Forms.Padding(4);
            this.btnFilterExpired.Name = "btnFilterExpired";
            this.btnFilterExpired.Size = new System.Drawing.Size(100, 20);
            this.btnFilterExpired.TabIndex = 3;
            this.btnFilterExpired.Text = "Expired (0)";
            this.btnFilterExpired.UseVisualStyleBackColor = false;
            this.btnFilterExpired.Click += new System.EventHandler(this.btnFilterExpired_Click);
            // 
            // btnFilterReady
            // 
            this.btnFilterReady.BackColor = System.Drawing.Color.Transparent;
            this.btnFilterReady.FlatAppearance.BorderSize = 0;
            this.btnFilterReady.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilterReady.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilterReady.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnFilterReady.Location = new System.Drawing.Point(200, 15);
            this.btnFilterReady.Margin = new System.Windows.Forms.Padding(4);
            this.btnFilterReady.Name = "btnFilterReady";
            this.btnFilterReady.Size = new System.Drawing.Size(100, 20);
            this.btnFilterReady.TabIndex = 2;
            this.btnFilterReady.Text = "Ready (0)";
            this.btnFilterReady.UseVisualStyleBackColor = false;
            this.btnFilterReady.Click += new System.EventHandler(this.btnFilterReady_Click);
            // 
            // btnFilterActive
            // 
            this.btnFilterActive.BackColor = System.Drawing.Color.Transparent;
            this.btnFilterActive.FlatAppearance.BorderSize = 0;
            this.btnFilterActive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilterActive.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilterActive.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnFilterActive.Location = new System.Drawing.Point(100, 15);
            this.btnFilterActive.Margin = new System.Windows.Forms.Padding(4);
            this.btnFilterActive.Name = "btnFilterActive";
            this.btnFilterActive.Size = new System.Drawing.Size(100, 20);
            this.btnFilterActive.TabIndex = 1;
            this.btnFilterActive.Text = "Active (0)";
            this.btnFilterActive.UseVisualStyleBackColor = false;
            this.btnFilterActive.Click += new System.EventHandler(this.btnFilterActive_Click);
            // 
            // btnFilterAll
            // 
            this.btnFilterAll.BackColor = System.Drawing.Color.Transparent;
            this.btnFilterAll.FlatAppearance.BorderSize = 0;
            this.btnFilterAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilterAll.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilterAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.btnFilterAll.Location = new System.Drawing.Point(20, 15);
            this.btnFilterAll.Margin = new System.Windows.Forms.Padding(4);
            this.btnFilterAll.Name = "btnFilterAll";
            this.btnFilterAll.Size = new System.Drawing.Size(80, 20);
            this.btnFilterAll.TabIndex = 0;
            this.btnFilterAll.Text = "All (0)";
            this.btnFilterAll.UseVisualStyleBackColor = false;
            this.btnFilterAll.Click += new System.EventHandler(this.btnFilterAll_Click);
            // 
            // panelMetrics
            // 
            this.panelMetrics.Controls.Add(this.panelMetricExpired);
            this.panelMetrics.Controls.Add(this.panelMetricReady);
            this.panelMetrics.Controls.Add(this.panelMetricActive);
            this.panelMetrics.Controls.Add(this.panelMetricTotal);
            this.panelMetrics.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMetrics.Location = new System.Drawing.Point(32, 125);
            this.panelMetrics.Margin = new System.Windows.Forms.Padding(4);
            this.panelMetrics.Name = "panelMetrics";
            this.panelMetrics.Size = new System.Drawing.Size(1536, 150);
            this.panelMetrics.TabIndex = 1;
            // 
            // panelMetricExpired
            // 
            this.panelMetricExpired.BackColor = System.Drawing.Color.White;
            this.panelMetricExpired.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMetricExpired.Controls.Add(this.lblMetricExpiredValue);
            this.panelMetricExpired.Controls.Add(this.lblMetricExpiredTitle);
            this.panelMetricExpired.Location = new System.Drawing.Point(1170, 0);
            this.panelMetricExpired.Margin = new System.Windows.Forms.Padding(4);
            this.panelMetricExpired.Name = "panelMetricExpired";
            this.panelMetricExpired.Padding = new System.Windows.Forms.Padding(20);
            this.panelMetricExpired.Size = new System.Drawing.Size(366, 120);
            this.panelMetricExpired.TabIndex = 3;
            // 
            // lblMetricExpiredValue
            // 
            this.lblMetricExpiredValue.AutoSize = true;
            this.lblMetricExpiredValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricExpiredValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricExpiredValue.Location = new System.Drawing.Point(24, 16);
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
            this.lblMetricExpiredTitle.Location = new System.Drawing.Point(29, 70);
            this.lblMetricExpiredTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricExpiredTitle.Name = "lblMetricExpiredTitle";
            this.lblMetricExpiredTitle.Size = new System.Drawing.Size(94, 23);
            this.lblMetricExpiredTitle.TabIndex = 1;
            this.lblMetricExpiredTitle.Text = "❌ Expired";
            // 
            // panelMetricReady
            // 
            this.panelMetricReady.BackColor = System.Drawing.Color.White;
            this.panelMetricReady.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMetricReady.Controls.Add(this.lblMetricReadyValue);
            this.panelMetricReady.Controls.Add(this.lblMetricReadyTitle);
            this.panelMetricReady.Location = new System.Drawing.Point(780, 0);
            this.panelMetricReady.Margin = new System.Windows.Forms.Padding(4);
            this.panelMetricReady.Name = "panelMetricReady";
            this.panelMetricReady.Padding = new System.Windows.Forms.Padding(20);
            this.panelMetricReady.Size = new System.Drawing.Size(366, 120);
            this.panelMetricReady.TabIndex = 2;
            // 
            // lblMetricReadyValue
            // 
            this.lblMetricReadyValue.AutoSize = true;
            this.lblMetricReadyValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricReadyValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricReadyValue.Location = new System.Drawing.Point(24, 16);
            this.lblMetricReadyValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricReadyValue.Name = "lblMetricReadyValue";
            this.lblMetricReadyValue.Size = new System.Drawing.Size(46, 54);
            this.lblMetricReadyValue.TabIndex = 2;
            this.lblMetricReadyValue.Text = "0";
            // 
            // lblMetricReadyTitle
            // 
            this.lblMetricReadyTitle.AutoSize = true;
            this.lblMetricReadyTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricReadyTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricReadyTitle.Location = new System.Drawing.Point(29, 75);
            this.lblMetricReadyTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricReadyTitle.Name = "lblMetricReadyTitle";
            this.lblMetricReadyTitle.Size = new System.Drawing.Size(165, 23);
            this.lblMetricReadyTitle.TabIndex = 1;
            this.lblMetricReadyTitle.Text = "📌 Ready for Pickup";
            // 
            // panelMetricActive
            // 
            this.panelMetricActive.BackColor = System.Drawing.Color.White;
            this.panelMetricActive.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMetricActive.Controls.Add(this.lblMetricActiveValue);
            this.panelMetricActive.Controls.Add(this.lblMetricActiveTitle);
            this.panelMetricActive.Location = new System.Drawing.Point(390, 0);
            this.panelMetricActive.Margin = new System.Windows.Forms.Padding(4);
            this.panelMetricActive.Name = "panelMetricActive";
            this.panelMetricActive.Padding = new System.Windows.Forms.Padding(20);
            this.panelMetricActive.Size = new System.Drawing.Size(366, 120);
            this.panelMetricActive.TabIndex = 1;
            // 
            // lblMetricActiveValue
            // 
            this.lblMetricActiveValue.AutoSize = true;
            this.lblMetricActiveValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricActiveValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricActiveValue.Location = new System.Drawing.Point(24, 20);
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
            this.lblMetricActiveTitle.Location = new System.Drawing.Point(29, 75);
            this.lblMetricActiveTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricActiveTitle.Name = "lblMetricActiveTitle";
            this.lblMetricActiveTitle.Size = new System.Drawing.Size(84, 23);
            this.lblMetricActiveTitle.TabIndex = 1;
            this.lblMetricActiveTitle.Text = "✅ Active";
            // 
            // panelMetricTotal
            // 
            this.panelMetricTotal.BackColor = System.Drawing.Color.White;
            this.panelMetricTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMetricTotal.Controls.Add(this.lblMetricTotalValue);
            this.panelMetricTotal.Controls.Add(this.lblMetricTotalTitle);
            this.panelMetricTotal.Location = new System.Drawing.Point(0, 0);
            this.panelMetricTotal.Margin = new System.Windows.Forms.Padding(4);
            this.panelMetricTotal.Name = "panelMetricTotal";
            this.panelMetricTotal.Padding = new System.Windows.Forms.Padding(20);
            this.panelMetricTotal.Size = new System.Drawing.Size(366, 120);
            this.panelMetricTotal.TabIndex = 0;
            // 
            // lblMetricTotalValue
            // 
            this.lblMetricTotalValue.AutoSize = true;
            this.lblMetricTotalValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTotalValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricTotalValue.Location = new System.Drawing.Point(27, 20);
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
            this.lblMetricTotalTitle.Location = new System.Drawing.Point(32, 74);
            this.lblMetricTotalTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricTotalTitle.Name = "lblMetricTotalTitle";
            this.lblMetricTotalTitle.Size = new System.Drawing.Size(174, 23);
            this.lblMetricTotalTitle.TabIndex = 1;
            this.lblMetricTotalTitle.Text = "📅 Total Reservations";
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.lblSubtitle);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(32, 30);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(4);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1536, 95);
            this.panelHeader.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblSubtitle.Location = new System.Drawing.Point(0, 60);
            this.lblSubtitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(289, 23);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Track and manage book reservations";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitle.Location = new System.Drawing.Point(4, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(264, 54);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Reservations";
            // 
            // StaffReservationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1600, 985);
            this.Controls.Add(this.panelMainContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "StaffReservationsForm";
            this.Text = "Reservations";
            this.Load += new System.EventHandler(this.StaffReservationsForm_Load);
            this.panelMainContainer.ResumeLayout(false);
            this.panelReservationsList.ResumeLayout(false);
            this.panelCreateReservation.ResumeLayout(false);
            this.panelCreateReservation.PerformLayout();
            this.panelFilterTabs.ResumeLayout(false);
            this.panelMetrics.ResumeLayout(false);
            this.panelMetricExpired.ResumeLayout(false);
            this.panelMetricExpired.PerformLayout();
            this.panelMetricReady.ResumeLayout(false);
            this.panelMetricReady.PerformLayout();
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
        private System.Windows.Forms.Panel panelMetrics;
        private System.Windows.Forms.Panel panelMetricTotal;
        private System.Windows.Forms.Label lblMetricTotalValue;
        private System.Windows.Forms.Label lblMetricTotalTitle;
        private System.Windows.Forms.Panel panelMetricActive;
        private System.Windows.Forms.Label lblMetricActiveValue;
        private System.Windows.Forms.Label lblMetricActiveTitle;
        private System.Windows.Forms.Panel panelMetricReady;
        private System.Windows.Forms.Label lblMetricReadyValue;
        private System.Windows.Forms.Label lblMetricReadyTitle;
        private System.Windows.Forms.Panel panelMetricExpired;
        private System.Windows.Forms.Label lblMetricExpiredValue;
        private System.Windows.Forms.Label lblMetricExpiredTitle;
        private System.Windows.Forms.Panel panelFilterTabs;
        private System.Windows.Forms.Button btnFilterAll;
        private System.Windows.Forms.Button btnFilterActive;
        private System.Windows.Forms.Button btnFilterReady;
        private System.Windows.Forms.Button btnFilterExpired;
        private System.Windows.Forms.Panel panelReservationsList;
        private System.Windows.Forms.Panel panelCreateReservation;
        private System.Windows.Forms.Label lblCreateReservationTitle;
        private System.Windows.Forms.Label lblMemberID;
        private System.Windows.Forms.TextBox txtMemberID;
        private System.Windows.Forms.Label lblBookID;
        private System.Windows.Forms.TextBox txtBookID;
        private System.Windows.Forms.Button btnCreateReservation;
    }
}
