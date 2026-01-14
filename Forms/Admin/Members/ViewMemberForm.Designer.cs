namespace Project5LMS.Forms.Admin.Members
{
    partial class ViewMemberForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelMainContainer;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelFormContent;
        private System.Windows.Forms.Panel panelPersonalInfo;
        private System.Windows.Forms.Panel panelMembershipInfo;
        private System.Windows.Forms.Panel panelStatistics;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblContact;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblMemberType;
        private System.Windows.Forms.Label lblRegistrationDate;
        private System.Windows.Forms.Label lblExpirationDate;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtContact;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.ComboBox cmbMemberType;
        private System.Windows.Forms.DateTimePicker dtpRegistration;
        private System.Windows.Forms.DateTimePicker dtpExpiration;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblActiveBorrowings;
        private System.Windows.Forms.Label lblTotalFines;
        private System.Windows.Forms.Label lblActiveReservations;
        private System.Windows.Forms.Panel panelBorrowingHistory;
        private System.Windows.Forms.Label lblBorrowingHistory;
        private System.Windows.Forms.DataGridView dataGridViewBorrowingHistory;
        private System.Windows.Forms.Panel panelFines;
        private System.Windows.Forms.Label lblFines;
        private System.Windows.Forms.DataGridView dataGridViewFines;
        
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }
        
        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.panelMainContainer = new System.Windows.Forms.Panel();
            this.panelFormContent = new System.Windows.Forms.Panel();
            this.panelFines = new System.Windows.Forms.Panel();
            this.dataGridViewFines = new System.Windows.Forms.DataGridView();
            this.lblFines = new System.Windows.Forms.Label();
            this.panelBorrowingHistory = new System.Windows.Forms.Panel();
            this.dataGridViewBorrowingHistory = new System.Windows.Forms.DataGridView();
            this.lblBorrowingHistory = new System.Windows.Forms.Label();
            this.panelStatistics = new System.Windows.Forms.Panel();
            this.lblActiveReservations = new System.Windows.Forms.Label();
            this.lblTotalFines = new System.Windows.Forms.Label();
            this.lblActiveBorrowings = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelMembershipInfo = new System.Windows.Forms.Panel();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.dtpExpiration = new System.Windows.Forms.DateTimePicker();
            this.lblExpirationDate = new System.Windows.Forms.Label();
            this.dtpRegistration = new System.Windows.Forms.DateTimePicker();
            this.lblRegistrationDate = new System.Windows.Forms.Label();
            this.cmbMemberType = new System.Windows.Forms.ComboBox();
            this.lblMemberType = new System.Windows.Forms.Label();
            this.panelPersonalInfo = new System.Windows.Forms.Panel();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtContact = new System.Windows.Forms.TextBox();
            this.lblContact = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.lblLastName = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelMainContainer.SuspendLayout();
            this.panelFormContent.SuspendLayout();
            this.panelFines.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFines)).BeginInit();
            this.panelBorrowingHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBorrowingHistory)).BeginInit();
            this.panelStatistics.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.panelMembershipInfo.SuspendLayout();
            this.panelPersonalInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMainContainer
            // 
            this.panelMainContainer.BackColor = System.Drawing.Color.White;
            this.panelMainContainer.Controls.Add(this.panelFormContent);
            this.panelMainContainer.Controls.Add(this.lblTitle);
            this.panelMainContainer.Location = new System.Drawing.Point(0, 0);
            this.panelMainContainer.Margin = new System.Windows.Forms.Padding(4);
            this.panelMainContainer.Name = "panelMainContainer";
            this.panelMainContainer.Size = new System.Drawing.Size(1000, 800);
            this.panelMainContainer.TabIndex = 0;
            // 
            // panelFormContent
            // 
            this.panelFormContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelFormContent.AutoScroll = true;
            this.panelFormContent.Controls.Add(this.panelFines);
            this.panelFormContent.Controls.Add(this.panelBorrowingHistory);
            this.panelFormContent.Controls.Add(this.panelStatistics);
            this.panelFormContent.Controls.Add(this.panelButtons);
            this.panelFormContent.Controls.Add(this.panelMembershipInfo);
            this.panelFormContent.Controls.Add(this.panelPersonalInfo);
            this.panelFormContent.Location = new System.Drawing.Point(0, 80);
            this.panelFormContent.Margin = new System.Windows.Forms.Padding(4);
            this.panelFormContent.Name = "panelFormContent";
            this.panelFormContent.Padding = new System.Windows.Forms.Padding(29, 20, 29, 20);
            this.panelFormContent.Size = new System.Drawing.Size(1000, 720);
            this.panelFormContent.TabIndex = 2;
            // 
            // panelFines
            // 
            this.panelFines.Controls.Add(this.dataGridViewFines);
            this.panelFines.Controls.Add(this.lblFines);
            this.panelFines.Location = new System.Drawing.Point(29, 781);
            this.panelFines.Margin = new System.Windows.Forms.Padding(4);
            this.panelFines.Name = "panelFines";
            this.panelFines.Size = new System.Drawing.Size(924, 191);
            this.panelFines.TabIndex = 6;
            // 
            // dataGridViewFines
            // 
            this.dataGridViewFines.AllowUserToAddRows = false;
            this.dataGridViewFines.AllowUserToDeleteRows = false;
            this.dataGridViewFines.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewFines.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewFines.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewFines.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFines.Location = new System.Drawing.Point(0, 28);
            this.dataGridViewFines.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewFines.MultiSelect = false;
            this.dataGridViewFines.Name = "dataGridViewFines";
            this.dataGridViewFines.ReadOnly = true;
            this.dataGridViewFines.RowHeadersVisible = false;
            this.dataGridViewFines.RowHeadersWidth = 51;
            this.dataGridViewFines.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewFines.Size = new System.Drawing.Size(924, 153);
            this.dataGridViewFines.TabIndex = 1;
            // 
            // lblFines
            // 
            this.lblFines.AutoSize = true;
            this.lblFines.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFines.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblFines.Location = new System.Drawing.Point(-2, 0);
            this.lblFines.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFines.Name = "lblFines";
            this.lblFines.Size = new System.Drawing.Size(202, 25);
            this.lblFines.TabIndex = 0;
            this.lblFines.Text = "Fines and Penalties";
            // 
            // panelBorrowingHistory
            // 
            this.panelBorrowingHistory.Controls.Add(this.dataGridViewBorrowingHistory);
            this.panelBorrowingHistory.Controls.Add(this.lblBorrowingHistory);
            this.panelBorrowingHistory.Location = new System.Drawing.Point(30, 586);
            this.panelBorrowingHistory.Margin = new System.Windows.Forms.Padding(4);
            this.panelBorrowingHistory.Name = "panelBorrowingHistory";
            this.panelBorrowingHistory.Size = new System.Drawing.Size(924, 178);
            this.panelBorrowingHistory.TabIndex = 5;
            // 
            // dataGridViewBorrowingHistory
            // 
            this.dataGridViewBorrowingHistory.AllowUserToAddRows = false;
            this.dataGridViewBorrowingHistory.AllowUserToDeleteRows = false;
            this.dataGridViewBorrowingHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewBorrowingHistory.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewBorrowingHistory.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewBorrowingHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBorrowingHistory.Location = new System.Drawing.Point(0, 28);
            this.dataGridViewBorrowingHistory.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewBorrowingHistory.MultiSelect = false;
            this.dataGridViewBorrowingHistory.Name = "dataGridViewBorrowingHistory";
            this.dataGridViewBorrowingHistory.ReadOnly = true;
            this.dataGridViewBorrowingHistory.RowHeadersVisible = false;
            this.dataGridViewBorrowingHistory.RowHeadersWidth = 51;
            this.dataGridViewBorrowingHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewBorrowingHistory.Size = new System.Drawing.Size(924, 143);
            this.dataGridViewBorrowingHistory.TabIndex = 1;
            // 
            // lblBorrowingHistory
            // 
            this.lblBorrowingHistory.AutoSize = true;
            this.lblBorrowingHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBorrowingHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblBorrowingHistory.Location = new System.Drawing.Point(3, 0);
            this.lblBorrowingHistory.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBorrowingHistory.Name = "lblBorrowingHistory";
            this.lblBorrowingHistory.Size = new System.Drawing.Size(181, 25);
            this.lblBorrowingHistory.TabIndex = 0;
            this.lblBorrowingHistory.Text = "Borrowing History";
            // 
            // panelStatistics
            // 
            this.panelStatistics.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.panelStatistics.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelStatistics.Controls.Add(this.lblActiveReservations);
            this.panelStatistics.Controls.Add(this.lblTotalFines);
            this.panelStatistics.Controls.Add(this.lblActiveBorrowings);
            this.panelStatistics.Location = new System.Drawing.Point(30, 455);
            this.panelStatistics.Margin = new System.Windows.Forms.Padding(4);
            this.panelStatistics.Name = "panelStatistics";
            this.panelStatistics.Padding = new System.Windows.Forms.Padding(20, 18, 20, 18);
            this.panelStatistics.Size = new System.Drawing.Size(924, 123);
            this.panelStatistics.TabIndex = 4;
            // 
            // lblActiveReservations
            // 
            this.lblActiveReservations.AutoSize = true;
            this.lblActiveReservations.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActiveReservations.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblActiveReservations.Location = new System.Drawing.Point(20, 80);
            this.lblActiveReservations.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblActiveReservations.Name = "lblActiveReservations";
            this.lblActiveReservations.Size = new System.Drawing.Size(194, 24);
            this.lblActiveReservations.TabIndex = 2;
            this.lblActiveReservations.Text = "Active Reservations: 0";
            // 
            // lblTotalFines
            // 
            this.lblTotalFines.AutoSize = true;
            this.lblTotalFines.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalFines.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTotalFines.Location = new System.Drawing.Point(20, 49);
            this.lblTotalFines.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalFines.Name = "lblTotalFines";
            this.lblTotalFines.Size = new System.Drawing.Size(136, 24);
            this.lblTotalFines.TabIndex = 1;
            this.lblTotalFines.Text = "Total Fines: ₱0";
            // 
            // lblActiveBorrowings
            // 
            this.lblActiveBorrowings.AutoSize = true;
            this.lblActiveBorrowings.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActiveBorrowings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblActiveBorrowings.Location = new System.Drawing.Point(20, 18);
            this.lblActiveBorrowings.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblActiveBorrowings.Name = "lblActiveBorrowings";
            this.lblActiveBorrowings.Size = new System.Drawing.Size(181, 24);
            this.lblActiveBorrowings.TabIndex = 0;
            this.lblActiveBorrowings.Text = "Active Borrowings: 0";
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnClose);
            this.panelButtons.Location = new System.Drawing.Point(28, 994);
            this.panelButtons.Margin = new System.Windows.Forms.Padding(4);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(924, 50);
            this.panelButtons.TabIndex = 3;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(4, 4);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(912, 39);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelMembershipInfo
            // 
            this.panelMembershipInfo.Controls.Add(this.cmbStatus);
            this.panelMembershipInfo.Controls.Add(this.lblStatus);
            this.panelMembershipInfo.Controls.Add(this.dtpExpiration);
            this.panelMembershipInfo.Controls.Add(this.lblExpirationDate);
            this.panelMembershipInfo.Controls.Add(this.dtpRegistration);
            this.panelMembershipInfo.Controls.Add(this.lblRegistrationDate);
            this.panelMembershipInfo.Controls.Add(this.cmbMemberType);
            this.panelMembershipInfo.Controls.Add(this.lblMemberType);
            this.panelMembershipInfo.Location = new System.Drawing.Point(29, 281);
            this.panelMembershipInfo.Margin = new System.Windows.Forms.Padding(4);
            this.panelMembershipInfo.Name = "panelMembershipInfo";
            this.panelMembershipInfo.Size = new System.Drawing.Size(925, 156);
            this.panelMembershipInfo.TabIndex = 1;
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Enabled = false;
            this.cmbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "Active",
            "Inactive",
            "Suspended"});
            this.cmbStatus.Location = new System.Drawing.Point(521, 30);
            this.cmbStatus.Margin = new System.Windows.Forms.Padding(4);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(380, 30);
            this.cmbStatus.TabIndex = 8;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblStatus.Location = new System.Drawing.Point(518, 2);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(60, 24);
            this.lblStatus.TabIndex = 17;
            this.lblStatus.Text = "Status";
            // 
            // dtpExpiration
            // 
            this.dtpExpiration.Enabled = false;
            this.dtpExpiration.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpExpiration.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpExpiration.Location = new System.Drawing.Point(522, 100);
            this.dtpExpiration.Margin = new System.Windows.Forms.Padding(4);
            this.dtpExpiration.Name = "dtpExpiration";
            this.dtpExpiration.Size = new System.Drawing.Size(380, 28);
            this.dtpExpiration.TabIndex = 7;
            // 
            // lblExpirationDate
            // 
            this.lblExpirationDate.AutoSize = true;
            this.lblExpirationDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpirationDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblExpirationDate.Location = new System.Drawing.Point(518, 72);
            this.lblExpirationDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblExpirationDate.Name = "lblExpirationDate";
            this.lblExpirationDate.Size = new System.Drawing.Size(137, 24);
            this.lblExpirationDate.TabIndex = 15;
            this.lblExpirationDate.Text = "Expiration Date";
            // 
            // dtpRegistration
            // 
            this.dtpRegistration.Enabled = false;
            this.dtpRegistration.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpRegistration.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpRegistration.Location = new System.Drawing.Point(0, 100);
            this.dtpRegistration.Margin = new System.Windows.Forms.Padding(4);
            this.dtpRegistration.Name = "dtpRegistration";
            this.dtpRegistration.Size = new System.Drawing.Size(380, 28);
            this.dtpRegistration.TabIndex = 6;
            // 
            // lblRegistrationDate
            // 
            this.lblRegistrationDate.AutoSize = true;
            this.lblRegistrationDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegistrationDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblRegistrationDate.Location = new System.Drawing.Point(0, 71);
            this.lblRegistrationDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRegistrationDate.Name = "lblRegistrationDate";
            this.lblRegistrationDate.Size = new System.Drawing.Size(151, 24);
            this.lblRegistrationDate.TabIndex = 13;
            this.lblRegistrationDate.Text = "Registration Date";
            // 
            // cmbMemberType
            // 
            this.cmbMemberType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMemberType.Enabled = false;
            this.cmbMemberType.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMemberType.FormattingEnabled = true;
            this.cmbMemberType.Location = new System.Drawing.Point(0, 30);
            this.cmbMemberType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbMemberType.Name = "cmbMemberType";
            this.cmbMemberType.Size = new System.Drawing.Size(380, 30);
            this.cmbMemberType.TabIndex = 5;
            // 
            // lblMemberType
            // 
            this.lblMemberType.AutoSize = true;
            this.lblMemberType.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMemberType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMemberType.Location = new System.Drawing.Point(0, 2);
            this.lblMemberType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMemberType.Name = "lblMemberType";
            this.lblMemberType.Size = new System.Drawing.Size(129, 24);
            this.lblMemberType.TabIndex = 11;
            this.lblMemberType.Text = "Member Type";
            // 
            // panelPersonalInfo
            // 
            this.panelPersonalInfo.Controls.Add(this.txtAddress);
            this.panelPersonalInfo.Controls.Add(this.lblAddress);
            this.panelPersonalInfo.Controls.Add(this.txtContact);
            this.panelPersonalInfo.Controls.Add(this.lblContact);
            this.panelPersonalInfo.Controls.Add(this.txtEmail);
            this.panelPersonalInfo.Controls.Add(this.lblEmail);
            this.panelPersonalInfo.Controls.Add(this.txtLastName);
            this.panelPersonalInfo.Controls.Add(this.lblLastName);
            this.panelPersonalInfo.Controls.Add(this.txtFirstName);
            this.panelPersonalInfo.Controls.Add(this.lblFirstName);
            this.panelPersonalInfo.Location = new System.Drawing.Point(29, 16);
            this.panelPersonalInfo.Margin = new System.Windows.Forms.Padding(4);
            this.panelPersonalInfo.Name = "panelPersonalInfo";
            this.panelPersonalInfo.Size = new System.Drawing.Size(925, 242);
            this.panelPersonalInfo.TabIndex = 0;
            // 
            // txtAddress
            // 
            this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.Location = new System.Drawing.Point(1, 186);
            this.txtAddress.Margin = new System.Windows.Forms.Padding(4);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.ReadOnly = true;
            this.txtAddress.Size = new System.Drawing.Size(924, 52);
            this.txtAddress.TabIndex = 4;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblAddress.Location = new System.Drawing.Point(1, 158);
            this.lblAddress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(80, 24);
            this.lblAddress.TabIndex = 9;
            this.lblAddress.Text = "Address";
            // 
            // txtContact
            // 
            this.txtContact.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtContact.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContact.Location = new System.Drawing.Point(521, 109);
            this.txtContact.Margin = new System.Windows.Forms.Padding(4);
            this.txtContact.Name = "txtContact";
            this.txtContact.ReadOnly = true;
            this.txtContact.Size = new System.Drawing.Size(404, 28);
            this.txtContact.TabIndex = 3;
            // 
            // lblContact
            // 
            this.lblContact.AutoSize = true;
            this.lblContact.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContact.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblContact.Location = new System.Drawing.Point(517, 81);
            this.lblContact.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblContact.Name = "lblContact";
            this.lblContact.Size = new System.Drawing.Size(73, 24);
            this.lblContact.TabIndex = 7;
            this.lblContact.Text = "Contact";
            // 
            // txtEmail
            // 
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(-1, 109);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(4);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.ReadOnly = true;
            this.txtEmail.Size = new System.Drawing.Size(381, 28);
            this.txtEmail.TabIndex = 2;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblEmail.Location = new System.Drawing.Point(0, 81);
            this.lblEmail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(57, 24);
            this.lblEmail.TabIndex = 5;
            this.lblEmail.Text = "Email";
            // 
            // txtLastName
            // 
            this.txtLastName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLastName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLastName.Location = new System.Drawing.Point(521, 28);
            this.txtLastName.Margin = new System.Windows.Forms.Padding(4);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.ReadOnly = true;
            this.txtLastName.Size = new System.Drawing.Size(404, 28);
            this.txtLastName.TabIndex = 1;
            // 
            // lblLastName
            // 
            this.lblLastName.AutoSize = true;
            this.lblLastName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblLastName.Location = new System.Drawing.Point(517, 4);
            this.lblLastName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(99, 24);
            this.lblLastName.TabIndex = 3;
            this.lblLastName.Text = "Last Name";
            // 
            // txtFirstName
            // 
            this.txtFirstName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFirstName.Location = new System.Drawing.Point(-1, 28);
            this.txtFirstName.Margin = new System.Windows.Forms.Padding(4);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.ReadOnly = true;
            this.txtFirstName.Size = new System.Drawing.Size(381, 28);
            this.txtFirstName.TabIndex = 0;
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFirstName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblFirstName.Location = new System.Drawing.Point(0, 0);
            this.lblFirstName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(101, 24);
            this.lblFirstName.TabIndex = 1;
            this.lblFirstName.Text = "First Name";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(29, 20, 0, 20);
            this.lblTitle.Size = new System.Drawing.Size(265, 79);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "View Member";
            // 
            // ViewMemberForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1000, 800);
            this.Controls.Add(this.panelMainContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "ViewMemberForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View Member";
            this.Load += new System.EventHandler(this.ViewMemberForm_Load);
            this.panelMainContainer.ResumeLayout(false);
            this.panelMainContainer.PerformLayout();
            this.panelFormContent.ResumeLayout(false);
            this.panelFines.ResumeLayout(false);
            this.panelFines.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFines)).EndInit();
            this.panelBorrowingHistory.ResumeLayout(false);
            this.panelBorrowingHistory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBorrowingHistory)).EndInit();
            this.panelStatistics.ResumeLayout(false);
            this.panelStatistics.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.panelMembershipInfo.ResumeLayout(false);
            this.panelMembershipInfo.PerformLayout();
            this.panelPersonalInfo.ResumeLayout(false);
            this.panelPersonalInfo.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion
    }
}
