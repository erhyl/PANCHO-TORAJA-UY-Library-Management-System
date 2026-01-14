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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
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
            this.panelReservationsManagement = new System.Windows.Forms.Panel();
            this.panelCreateReservation = new System.Windows.Forms.Panel();
            this.panelStaffReservationBookInfo = new System.Windows.Forms.Panel();
            this.lblBookEligibilityStatus = new System.Windows.Forms.Label();
            this.lblBookEligibilityTitle = new System.Windows.Forms.Label();
            this.lblBookCopies = new System.Windows.Forms.Label();
            this.lblBookStatus = new System.Windows.Forms.Label();
            this.lblBookAuthor = new System.Windows.Forms.Label();
            this.lblBookTitle = new System.Windows.Forms.Label();
            this.lblBookInformationHeader = new System.Windows.Forms.Label();
            this.panelStaffReservationMemberInfo = new System.Windows.Forms.Panel();
            this.lblMemberEligibilityStatus = new System.Windows.Forms.Label();
            this.lblMemberEligibilityTitle = new System.Windows.Forms.Label();
            this.lblMemberFines = new System.Windows.Forms.Label();
            this.lblMemberOverdue = new System.Windows.Forms.Label();
            this.lblMemberReservations = new System.Windows.Forms.Label();
            this.lblMemberStatus = new System.Windows.Forms.Label();
            this.lblMemberType = new System.Windows.Forms.Label();
            this.lblMemberName = new System.Windows.Forms.Label();
            this.lblMemberEligibilityHeader = new System.Windows.Forms.Label();
            this.btnCreateReservation = new System.Windows.Forms.Button();
            this.txtBookID = new System.Windows.Forms.TextBox();
            this.lblBookID = new System.Windows.Forms.Label();
            this.txtMemberID = new System.Windows.Forms.TextBox();
            this.lblMemberID = new System.Windows.Forms.Label();
            this.lblCreateReservationTitle = new System.Windows.Forms.Label();
            this.panelReservationsList = new System.Windows.Forms.Panel();
            this.panelFilterTabs = new System.Windows.Forms.Panel();
            this.btnFilterExpired = new System.Windows.Forms.Button();
            this.btnFilterReady = new System.Windows.Forms.Button();
            this.btnFilterActive = new System.Windows.Forms.Button();
            this.btnFilterAll = new System.Windows.Forms.Button();
            this.lblReservationsHistory = new System.Windows.Forms.Label();
            this.panelMainContainer = new System.Windows.Forms.Panel();
            this.panelHeader.SuspendLayout();
            this.panelMetrics.SuspendLayout();
            this.panelMetricExpired.SuspendLayout();
            this.panelMetricReady.SuspendLayout();
            this.panelMetricActive.SuspendLayout();
            this.panelMetricTotal.SuspendLayout();
            this.panelReservationsManagement.SuspendLayout();
            this.panelCreateReservation.SuspendLayout();
            this.panelStaffReservationBookInfo.SuspendLayout();
            this.panelStaffReservationMemberInfo.SuspendLayout();
            this.panelFilterTabs.SuspendLayout();
            this.panelMainContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.lblSubtitle);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(24, 24);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1163, 77);
            this.panelHeader.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblSubtitle.Location = new System.Drawing.Point(0, 49);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(233, 19);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Track and manage book reservations";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitle.Location = new System.Drawing.Point(3, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(210, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Reservations";
            // 
            // panelMetrics
            // 
            this.panelMetrics.Controls.Add(this.panelMetricExpired);
            this.panelMetrics.Controls.Add(this.panelMetricReady);
            this.panelMetrics.Controls.Add(this.panelMetricActive);
            this.panelMetrics.Controls.Add(this.panelMetricTotal);
            this.panelMetrics.Location = new System.Drawing.Point(11, 5);
            this.panelMetrics.Margin = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.panelMetrics.Name = "panelMetrics";
            this.panelMetrics.Size = new System.Drawing.Size(1163, 105);
            this.panelMetrics.TabIndex = 1;
            // 
            // panelMetricExpired
            // 
            this.panelMetricExpired.BackColor = System.Drawing.Color.White;
            this.panelMetricExpired.Controls.Add(this.lblMetricExpiredValue);
            this.panelMetricExpired.Controls.Add(this.lblMetricExpiredTitle);
            this.panelMetricExpired.Location = new System.Drawing.Point(890, 0);
            this.panelMetricExpired.Name = "panelMetricExpired";
            this.panelMetricExpired.Padding = new System.Windows.Forms.Padding(15, 16, 15, 16);
            this.panelMetricExpired.Size = new System.Drawing.Size(270, 98);
            this.panelMetricExpired.TabIndex = 3;
            // 
            // lblMetricExpiredValue
            // 
            this.lblMetricExpiredValue.AutoSize = true;
            this.lblMetricExpiredValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricExpiredValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricExpiredValue.Location = new System.Drawing.Point(18, 13);
            this.lblMetricExpiredValue.Name = "lblMetricExpiredValue";
            this.lblMetricExpiredValue.Size = new System.Drawing.Size(38, 45);
            this.lblMetricExpiredValue.TabIndex = 2;
            this.lblMetricExpiredValue.Text = "0";
            // 
            // lblMetricExpiredTitle
            // 
            this.lblMetricExpiredTitle.AutoSize = true;
            this.lblMetricExpiredTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricExpiredTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricExpiredTitle.Location = new System.Drawing.Point(22, 57);
            this.lblMetricExpiredTitle.Name = "lblMetricExpiredTitle";
            this.lblMetricExpiredTitle.Size = new System.Drawing.Size(76, 19);
            this.lblMetricExpiredTitle.TabIndex = 1;
            this.lblMetricExpiredTitle.Text = "⏰ Expired";
            // 
            // panelMetricReady
            // 
            this.panelMetricReady.BackColor = System.Drawing.Color.White;
            this.panelMetricReady.Controls.Add(this.lblMetricReadyValue);
            this.panelMetricReady.Controls.Add(this.lblMetricReadyTitle);
            this.panelMetricReady.Location = new System.Drawing.Point(585, 0);
            this.panelMetricReady.Name = "panelMetricReady";
            this.panelMetricReady.Padding = new System.Windows.Forms.Padding(15, 16, 15, 16);
            this.panelMetricReady.Size = new System.Drawing.Size(285, 98);
            this.panelMetricReady.TabIndex = 2;
            // 
            // lblMetricReadyValue
            // 
            this.lblMetricReadyValue.AutoSize = true;
            this.lblMetricReadyValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricReadyValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricReadyValue.Location = new System.Drawing.Point(18, 13);
            this.lblMetricReadyValue.Name = "lblMetricReadyValue";
            this.lblMetricReadyValue.Size = new System.Drawing.Size(38, 45);
            this.lblMetricReadyValue.TabIndex = 2;
            this.lblMetricReadyValue.Text = "0";
            // 
            // lblMetricReadyTitle
            // 
            this.lblMetricReadyTitle.AutoSize = true;
            this.lblMetricReadyTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricReadyTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricReadyTitle.Location = new System.Drawing.Point(22, 61);
            this.lblMetricReadyTitle.Name = "lblMetricReadyTitle";
            this.lblMetricReadyTitle.Size = new System.Drawing.Size(134, 19);
            this.lblMetricReadyTitle.TabIndex = 1;
            this.lblMetricReadyTitle.Text = "✅ Ready for Pickup";
            // 
            // panelMetricActive
            // 
            this.panelMetricActive.BackColor = System.Drawing.Color.White;
            this.panelMetricActive.Controls.Add(this.lblMetricActiveValue);
            this.panelMetricActive.Controls.Add(this.lblMetricActiveTitle);
            this.panelMetricActive.Location = new System.Drawing.Point(292, 0);
            this.panelMetricActive.Name = "panelMetricActive";
            this.panelMetricActive.Padding = new System.Windows.Forms.Padding(15, 16, 15, 16);
            this.panelMetricActive.Size = new System.Drawing.Size(275, 98);
            this.panelMetricActive.TabIndex = 1;
            // 
            // lblMetricActiveValue
            // 
            this.lblMetricActiveValue.AutoSize = true;
            this.lblMetricActiveValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricActiveValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricActiveValue.Location = new System.Drawing.Point(18, 16);
            this.lblMetricActiveValue.Name = "lblMetricActiveValue";
            this.lblMetricActiveValue.Size = new System.Drawing.Size(38, 45);
            this.lblMetricActiveValue.TabIndex = 2;
            this.lblMetricActiveValue.Text = "0";
            // 
            // lblMetricActiveTitle
            // 
            this.lblMetricActiveTitle.AutoSize = true;
            this.lblMetricActiveTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricActiveTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricActiveTitle.Location = new System.Drawing.Point(22, 61);
            this.lblMetricActiveTitle.Name = "lblMetricActiveTitle";
            this.lblMetricActiveTitle.Size = new System.Drawing.Size(69, 19);
            this.lblMetricActiveTitle.TabIndex = 1;
            this.lblMetricActiveTitle.Text = "📋 Active";
            // 
            // panelMetricTotal
            // 
            this.panelMetricTotal.BackColor = System.Drawing.Color.White;
            this.panelMetricTotal.Controls.Add(this.lblMetricTotalValue);
            this.panelMetricTotal.Controls.Add(this.lblMetricTotalTitle);
            this.panelMetricTotal.Location = new System.Drawing.Point(0, 0);
            this.panelMetricTotal.Name = "panelMetricTotal";
            this.panelMetricTotal.Padding = new System.Windows.Forms.Padding(15, 16, 15, 16);
            this.panelMetricTotal.Size = new System.Drawing.Size(275, 98);
            this.panelMetricTotal.TabIndex = 0;
            // 
            // lblMetricTotalValue
            // 
            this.lblMetricTotalValue.AutoSize = true;
            this.lblMetricTotalValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTotalValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricTotalValue.Location = new System.Drawing.Point(20, 16);
            this.lblMetricTotalValue.Name = "lblMetricTotalValue";
            this.lblMetricTotalValue.Size = new System.Drawing.Size(38, 45);
            this.lblMetricTotalValue.TabIndex = 2;
            this.lblMetricTotalValue.Text = "0";
            // 
            // lblMetricTotalTitle
            // 
            this.lblMetricTotalTitle.AutoSize = true;
            this.lblMetricTotalTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTotalTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricTotalTitle.Location = new System.Drawing.Point(24, 60);
            this.lblMetricTotalTitle.Name = "lblMetricTotalTitle";
            this.lblMetricTotalTitle.Size = new System.Drawing.Size(142, 19);
            this.lblMetricTotalTitle.TabIndex = 1;
            this.lblMetricTotalTitle.Text = "📋 Total Reservations";
            // 
            // panelReservationsManagement
            // 
            this.panelReservationsManagement.Controls.Add(this.panelCreateReservation);
            this.panelReservationsManagement.Controls.Add(this.panelMetrics);
            this.panelReservationsManagement.Location = new System.Drawing.Point(24, 101);
            this.panelReservationsManagement.Margin = new System.Windows.Forms.Padding(0);
            this.panelReservationsManagement.Name = "panelReservationsManagement";
            this.panelReservationsManagement.Size = new System.Drawing.Size(1187, 806);
            this.panelReservationsManagement.TabIndex = 2;
            // 
            // panelCreateReservation
            // 
            this.panelCreateReservation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelCreateReservation.BackColor = System.Drawing.Color.White;
            this.panelCreateReservation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCreateReservation.Controls.Add(this.panelStaffReservationBookInfo);
            this.panelCreateReservation.Controls.Add(this.panelStaffReservationMemberInfo);
            this.panelCreateReservation.Controls.Add(this.btnCreateReservation);
            this.panelCreateReservation.Controls.Add(this.txtBookID);
            this.panelCreateReservation.Controls.Add(this.lblBookID);
            this.panelCreateReservation.Controls.Add(this.txtMemberID);
            this.panelCreateReservation.Controls.Add(this.lblMemberID);
            this.panelCreateReservation.Controls.Add(this.lblCreateReservationTitle);
            this.panelCreateReservation.Location = new System.Drawing.Point(0, 117);
            this.panelCreateReservation.Margin = new System.Windows.Forms.Padding(0);
            this.panelCreateReservation.Name = "panelCreateReservation";
            this.panelCreateReservation.Padding = new System.Windows.Forms.Padding(22, 24, 22, 24);
            this.panelCreateReservation.Size = new System.Drawing.Size(1187, 306);
            this.panelCreateReservation.TabIndex = 4;
            // 
            // panelStaffReservationBookInfo
            // 
            this.panelStaffReservationBookInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelStaffReservationBookInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.panelStaffReservationBookInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelStaffReservationBookInfo.Controls.Add(this.lblBookEligibilityStatus);
            this.panelStaffReservationBookInfo.Controls.Add(this.lblBookEligibilityTitle);
            this.panelStaffReservationBookInfo.Controls.Add(this.lblBookCopies);
            this.panelStaffReservationBookInfo.Controls.Add(this.lblBookStatus);
            this.panelStaffReservationBookInfo.Controls.Add(this.lblBookAuthor);
            this.panelStaffReservationBookInfo.Controls.Add(this.lblBookTitle);
            this.panelStaffReservationBookInfo.Controls.Add(this.lblBookInformationHeader);
            this.panelStaffReservationBookInfo.Location = new System.Drawing.Point(29, 208);
            this.panelStaffReservationBookInfo.Name = "panelStaffReservationBookInfo";
            this.panelStaffReservationBookInfo.Size = new System.Drawing.Size(703, 69);
            this.panelStaffReservationBookInfo.TabIndex = 7;
            this.panelStaffReservationBookInfo.Visible = false;
            // 
            // lblBookEligibilityStatus
            // 
            this.lblBookEligibilityStatus.AutoSize = true;
            this.lblBookEligibilityStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBookEligibilityStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(139)))), ((int)(((byte)(34)))));
            this.lblBookEligibilityStatus.Location = new System.Drawing.Point(444, 27);
            this.lblBookEligibilityStatus.Name = "lblBookEligibilityStatus";
            this.lblBookEligibilityStatus.Size = new System.Drawing.Size(0, 19);
            this.lblBookEligibilityStatus.TabIndex = 6;
            // 
            // lblBookEligibilityTitle
            // 
            this.lblBookEligibilityTitle.AutoSize = true;
            this.lblBookEligibilityTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBookEligibilityTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblBookEligibilityTitle.Location = new System.Drawing.Point(384, 27);
            this.lblBookEligibilityTitle.Name = "lblBookEligibilityTitle";
            this.lblBookEligibilityTitle.Size = new System.Drawing.Size(58, 15);
            this.lblBookEligibilityTitle.TabIndex = 5;
            this.lblBookEligibilityTitle.Text = "Eligibility:";
            // 
            // lblBookCopies
            // 
            this.lblBookCopies.AutoSize = true;
            this.lblBookCopies.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBookCopies.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblBookCopies.Location = new System.Drawing.Point(219, 43);
            this.lblBookCopies.Name = "lblBookCopies";
            this.lblBookCopies.Size = new System.Drawing.Size(0, 15);
            this.lblBookCopies.TabIndex = 4;
            // 
            // lblBookStatus
            // 
            this.lblBookStatus.AutoSize = true;
            this.lblBookStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBookStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblBookStatus.Location = new System.Drawing.Point(219, 27);
            this.lblBookStatus.Name = "lblBookStatus";
            this.lblBookStatus.Size = new System.Drawing.Size(0, 15);
            this.lblBookStatus.TabIndex = 3;
            // 
            // lblBookAuthor
            // 
            this.lblBookAuthor.AutoSize = true;
            this.lblBookAuthor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBookAuthor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblBookAuthor.Location = new System.Drawing.Point(9, 43);
            this.lblBookAuthor.Name = "lblBookAuthor";
            this.lblBookAuthor.Size = new System.Drawing.Size(0, 15);
            this.lblBookAuthor.TabIndex = 2;
            // 
            // lblBookTitle
            // 
            this.lblBookTitle.AutoSize = true;
            this.lblBookTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBookTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblBookTitle.Location = new System.Drawing.Point(9, 27);
            this.lblBookTitle.Name = "lblBookTitle";
            this.lblBookTitle.Size = new System.Drawing.Size(0, 15);
            this.lblBookTitle.TabIndex = 1;
            // 
            // lblBookInformationHeader
            // 
            this.lblBookInformationHeader.AutoSize = true;
            this.lblBookInformationHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBookInformationHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblBookInformationHeader.Location = new System.Drawing.Point(6, 6);
            this.lblBookInformationHeader.Name = "lblBookInformationHeader";
            this.lblBookInformationHeader.Size = new System.Drawing.Size(115, 17);
            this.lblBookInformationHeader.TabIndex = 0;
            this.lblBookInformationHeader.Text = "Book Information";
            // 
            // panelStaffReservationMemberInfo
            // 
            this.panelStaffReservationMemberInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelStaffReservationMemberInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.panelStaffReservationMemberInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelStaffReservationMemberInfo.Controls.Add(this.lblMemberEligibilityStatus);
            this.panelStaffReservationMemberInfo.Controls.Add(this.lblMemberEligibilityTitle);
            this.panelStaffReservationMemberInfo.Controls.Add(this.lblMemberFines);
            this.panelStaffReservationMemberInfo.Controls.Add(this.lblMemberOverdue);
            this.panelStaffReservationMemberInfo.Controls.Add(this.lblMemberReservations);
            this.panelStaffReservationMemberInfo.Controls.Add(this.lblMemberStatus);
            this.panelStaffReservationMemberInfo.Controls.Add(this.lblMemberType);
            this.panelStaffReservationMemberInfo.Controls.Add(this.lblMemberName);
            this.panelStaffReservationMemberInfo.Controls.Add(this.lblMemberEligibilityHeader);
            this.panelStaffReservationMemberInfo.Location = new System.Drawing.Point(29, 71);
            this.panelStaffReservationMemberInfo.Name = "panelStaffReservationMemberInfo";
            this.panelStaffReservationMemberInfo.Size = new System.Drawing.Size(703, 69);
            this.panelStaffReservationMemberInfo.TabIndex = 6;
            this.panelStaffReservationMemberInfo.Visible = false;
            // 
            // lblMemberEligibilityStatus
            // 
            this.lblMemberEligibilityStatus.AutoSize = true;
            this.lblMemberEligibilityStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMemberEligibilityStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(139)))), ((int)(((byte)(34)))));
            this.lblMemberEligibilityStatus.Location = new System.Drawing.Point(444, 27);
            this.lblMemberEligibilityStatus.Name = "lblMemberEligibilityStatus";
            this.lblMemberEligibilityStatus.Size = new System.Drawing.Size(0, 19);
            this.lblMemberEligibilityStatus.TabIndex = 8;
            // 
            // lblMemberEligibilityTitle
            // 
            this.lblMemberEligibilityTitle.AutoSize = true;
            this.lblMemberEligibilityTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMemberEligibilityTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMemberEligibilityTitle.Location = new System.Drawing.Point(384, 27);
            this.lblMemberEligibilityTitle.Name = "lblMemberEligibilityTitle";
            this.lblMemberEligibilityTitle.Size = new System.Drawing.Size(58, 15);
            this.lblMemberEligibilityTitle.TabIndex = 7;
            this.lblMemberEligibilityTitle.Text = "Eligibility:";
            // 
            // lblMemberFines
            // 
            this.lblMemberFines.AutoSize = true;
            this.lblMemberFines.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMemberFines.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMemberFines.Location = new System.Drawing.Point(219, 43);
            this.lblMemberFines.Name = "lblMemberFines";
            this.lblMemberFines.Size = new System.Drawing.Size(0, 15);
            this.lblMemberFines.TabIndex = 6;
            // 
            // lblMemberOverdue
            // 
            this.lblMemberOverdue.AutoSize = true;
            this.lblMemberOverdue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMemberOverdue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMemberOverdue.Location = new System.Drawing.Point(219, 27);
            this.lblMemberOverdue.Name = "lblMemberOverdue";
            this.lblMemberOverdue.Size = new System.Drawing.Size(0, 15);
            this.lblMemberOverdue.TabIndex = 5;
            // 
            // lblMemberReservations
            // 
            this.lblMemberReservations.AutoSize = true;
            this.lblMemberReservations.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMemberReservations.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMemberReservations.Location = new System.Drawing.Point(9, 43);
            this.lblMemberReservations.Name = "lblMemberReservations";
            this.lblMemberReservations.Size = new System.Drawing.Size(0, 15);
            this.lblMemberReservations.TabIndex = 4;
            // 
            // lblMemberStatus
            // 
            this.lblMemberStatus.AutoSize = true;
            this.lblMemberStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMemberStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMemberStatus.Location = new System.Drawing.Point(9, 27);
            this.lblMemberStatus.Name = "lblMemberStatus";
            this.lblMemberStatus.Size = new System.Drawing.Size(0, 15);
            this.lblMemberStatus.TabIndex = 3;
            // 
            // lblMemberType
            // 
            this.lblMemberType.AutoSize = true;
            this.lblMemberType.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMemberType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMemberType.Location = new System.Drawing.Point(9, 43);
            this.lblMemberType.Name = "lblMemberType";
            this.lblMemberType.Size = new System.Drawing.Size(0, 15);
            this.lblMemberType.TabIndex = 2;
            // 
            // lblMemberName
            // 
            this.lblMemberName.AutoSize = true;
            this.lblMemberName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMemberName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMemberName.Location = new System.Drawing.Point(9, 27);
            this.lblMemberName.Name = "lblMemberName";
            this.lblMemberName.Size = new System.Drawing.Size(0, 15);
            this.lblMemberName.TabIndex = 1;
            // 
            // lblMemberEligibilityHeader
            // 
            this.lblMemberEligibilityHeader.AutoSize = true;
            this.lblMemberEligibilityHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMemberEligibilityHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMemberEligibilityHeader.Location = new System.Drawing.Point(6, 6);
            this.lblMemberEligibilityHeader.Name = "lblMemberEligibilityHeader";
            this.lblMemberEligibilityHeader.Size = new System.Drawing.Size(116, 17);
            this.lblMemberEligibilityHeader.TabIndex = 0;
            this.lblMemberEligibilityHeader.Text = "Member Eligibility";
            // 
            // btnCreateReservation
            // 
            this.btnCreateReservation.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCreateReservation.BackColor = System.Drawing.Color.Maroon;
            this.btnCreateReservation.FlatAppearance.BorderSize = 0;
            this.btnCreateReservation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateReservation.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateReservation.ForeColor = System.Drawing.Color.White;
            this.btnCreateReservation.Location = new System.Drawing.Point(790, 239);
            this.btnCreateReservation.Name = "btnCreateReservation";
            this.btnCreateReservation.Size = new System.Drawing.Size(345, 38);
            this.btnCreateReservation.TabIndex = 5;
            this.btnCreateReservation.Text = "Create Reservation";
            this.btnCreateReservation.UseVisualStyleBackColor = false;
            this.btnCreateReservation.Click += new System.EventHandler(this.btnCreateReservation_Click);
            // 
            // txtBookID
            // 
            this.txtBookID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtBookID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBookID.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBookID.ForeColor = System.Drawing.Color.Gray;
            this.txtBookID.Location = new System.Drawing.Point(29, 177);
            this.txtBookID.Name = "txtBookID";
            this.txtBookID.Size = new System.Drawing.Size(703, 25);
            this.txtBookID.TabIndex = 4;
            this.txtBookID.Text = "Enter book ID or title";
            this.txtBookID.TextChanged += new System.EventHandler(this.txtBookID_TextChanged);
            this.txtBookID.Enter += new System.EventHandler(this.txtBookID_Enter);
            this.txtBookID.Leave += new System.EventHandler(this.txtBookID_Leave);
            // 
            // lblBookID
            // 
            this.lblBookID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblBookID.AutoSize = true;
            this.lblBookID.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBookID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblBookID.Location = new System.Drawing.Point(25, 155);
            this.lblBookID.Name = "lblBookID";
            this.lblBookID.Size = new System.Drawing.Size(104, 19);
            this.lblBookID.TabIndex = 3;
            this.lblBookID.Text = "Book ID or Title";
            // 
            // txtMemberID
            // 
            this.txtMemberID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtMemberID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMemberID.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMemberID.ForeColor = System.Drawing.Color.Gray;
            this.txtMemberID.Location = new System.Drawing.Point(29, 40);
            this.txtMemberID.Name = "txtMemberID";
            this.txtMemberID.Size = new System.Drawing.Size(706, 25);
            this.txtMemberID.TabIndex = 2;
            this.txtMemberID.Text = "Enter member ID (e.g., M1001)";
            this.txtMemberID.TextChanged += new System.EventHandler(this.txtMemberID_TextChanged);
            this.txtMemberID.Enter += new System.EventHandler(this.txtMemberID_Enter);
            this.txtMemberID.Leave += new System.EventHandler(this.txtMemberID_Leave);
            // 
            // lblMemberID
            // 
            this.lblMemberID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblMemberID.AutoSize = true;
            this.lblMemberID.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMemberID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMemberID.Location = new System.Drawing.Point(25, 20);
            this.lblMemberID.Name = "lblMemberID";
            this.lblMemberID.Size = new System.Drawing.Size(79, 19);
            this.lblMemberID.TabIndex = 1;
            this.lblMemberID.Text = "Member ID";
            // 
            // lblCreateReservationTitle
            // 
            this.lblCreateReservationTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCreateReservationTitle.AutoSize = true;
            this.lblCreateReservationTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreateReservationTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCreateReservationTitle.Location = new System.Drawing.Point(11, -1);
            this.lblCreateReservationTitle.Name = "lblCreateReservationTitle";
            this.lblCreateReservationTitle.Size = new System.Drawing.Size(193, 21);
            this.lblCreateReservationTitle.TabIndex = 0;
            this.lblCreateReservationTitle.Text = "Create New Reservation";
            // 
            // panelReservationsList
            // 
            this.panelReservationsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelReservationsList.AutoScroll = true;
            this.panelReservationsList.BackColor = System.Drawing.Color.White;
            this.panelReservationsList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelReservationsList.Location = new System.Drawing.Point(15, 101);
            this.panelReservationsList.Name = "panelReservationsList";
            this.panelReservationsList.Padding = new System.Windows.Forms.Padding(15, 16, 15, 16);
            this.panelReservationsList.Size = new System.Drawing.Size(1152, 267);
            this.panelReservationsList.TabIndex = 3;
            // 
            // panelFilterTabs
            // 
            this.panelFilterTabs.BackColor = System.Drawing.Color.White;
            this.panelFilterTabs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFilterTabs.Controls.Add(this.panelReservationsList);
            this.panelFilterTabs.Controls.Add(this.btnFilterExpired);
            this.panelFilterTabs.Controls.Add(this.btnFilterReady);
            this.panelFilterTabs.Controls.Add(this.btnFilterActive);
            this.panelFilterTabs.Controls.Add(this.btnFilterAll);
            this.panelFilterTabs.Controls.Add(this.lblReservationsHistory);
            this.panelFilterTabs.Location = new System.Drawing.Point(24, 527);
            this.panelFilterTabs.Name = "panelFilterTabs";
            this.panelFilterTabs.Padding = new System.Windows.Forms.Padding(15, 12, 15, 12);
            this.panelFilterTabs.Size = new System.Drawing.Size(1187, 380);
            this.panelFilterTabs.TabIndex = 3;
            this.panelFilterTabs.Paint += new System.Windows.Forms.PaintEventHandler(this.panelFilterTabs_Paint);
            // 
            // btnFilterExpired
            // 
            this.btnFilterExpired.BackColor = System.Drawing.Color.Transparent;
            this.btnFilterExpired.FlatAppearance.BorderSize = 0;
            this.btnFilterExpired.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilterExpired.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilterExpired.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnFilterExpired.Location = new System.Drawing.Point(555, 49);
            this.btnFilterExpired.Name = "btnFilterExpired";
            this.btnFilterExpired.Size = new System.Drawing.Size(180, 40);
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
            this.btnFilterReady.Location = new System.Drawing.Point(375, 49);
            this.btnFilterReady.Name = "btnFilterReady";
            this.btnFilterReady.Size = new System.Drawing.Size(180, 40);
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
            this.btnFilterActive.Location = new System.Drawing.Point(195, 49);
            this.btnFilterActive.Name = "btnFilterActive";
            this.btnFilterActive.Size = new System.Drawing.Size(180, 40);
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
            this.btnFilterAll.Location = new System.Drawing.Point(15, 49);
            this.btnFilterAll.Name = "btnFilterAll";
            this.btnFilterAll.Size = new System.Drawing.Size(180, 40);
            this.btnFilterAll.TabIndex = 0;
            this.btnFilterAll.Text = "All (0)";
            this.btnFilterAll.UseVisualStyleBackColor = false;
            this.btnFilterAll.Click += new System.EventHandler(this.btnFilterAll_Click);
            // 
            // lblReservationsHistory
            // 
            this.lblReservationsHistory.AutoSize = true;
            this.lblReservationsHistory.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReservationsHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblReservationsHistory.Location = new System.Drawing.Point(15, 12);
            this.lblReservationsHistory.Name = "lblReservationsHistory";
            this.lblReservationsHistory.Size = new System.Drawing.Size(251, 32);
            this.lblReservationsHistory.TabIndex = 4;
            this.lblReservationsHistory.Text = "Reservations History";
            // 
            // panelMainContainer
            // 
            this.panelMainContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelMainContainer.Controls.Add(this.panelFilterTabs);
            this.panelMainContainer.Controls.Add(this.panelReservationsManagement);
            this.panelMainContainer.Controls.Add(this.panelHeader);
            this.panelMainContainer.Location = new System.Drawing.Point(0, 0);
            this.panelMainContainer.Name = "panelMainContainer";
            this.panelMainContainer.Padding = new System.Windows.Forms.Padding(24);
            this.panelMainContainer.Size = new System.Drawing.Size(1211, 931);
            this.panelMainContainer.TabIndex = 0;
            // 
            // StaffReservationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1262, 943);
            this.Controls.Add(this.panelMainContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "StaffReservationsForm";
            this.Text = "Reservations";
            this.Load += new System.EventHandler(this.StaffReservationsForm_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelMetrics.ResumeLayout(false);
            this.panelMetricExpired.ResumeLayout(false);
            this.panelMetricExpired.PerformLayout();
            this.panelMetricReady.ResumeLayout(false);
            this.panelMetricReady.PerformLayout();
            this.panelMetricActive.ResumeLayout(false);
            this.panelMetricActive.PerformLayout();
            this.panelMetricTotal.ResumeLayout(false);
            this.panelMetricTotal.PerformLayout();
            this.panelReservationsManagement.ResumeLayout(false);
            this.panelCreateReservation.ResumeLayout(false);
            this.panelCreateReservation.PerformLayout();
            this.panelStaffReservationBookInfo.ResumeLayout(false);
            this.panelStaffReservationBookInfo.PerformLayout();
            this.panelStaffReservationMemberInfo.ResumeLayout(false);
            this.panelStaffReservationMemberInfo.PerformLayout();
            this.panelFilterTabs.ResumeLayout(false);
            this.panelFilterTabs.PerformLayout();
            this.panelMainContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelMetrics;
        private System.Windows.Forms.Panel panelMetricExpired;
        private System.Windows.Forms.Label lblMetricExpiredValue;
        private System.Windows.Forms.Label lblMetricExpiredTitle;
        private System.Windows.Forms.Panel panelMetricReady;
        private System.Windows.Forms.Label lblMetricReadyValue;
        private System.Windows.Forms.Label lblMetricReadyTitle;
        private System.Windows.Forms.Panel panelMetricActive;
        private System.Windows.Forms.Label lblMetricActiveValue;
        private System.Windows.Forms.Label lblMetricActiveTitle;
        private System.Windows.Forms.Panel panelMetricTotal;
        private System.Windows.Forms.Label lblMetricTotalValue;
        private System.Windows.Forms.Label lblMetricTotalTitle;
        private System.Windows.Forms.Panel panelReservationsManagement;
        private System.Windows.Forms.Panel panelCreateReservation;
        private System.Windows.Forms.Panel panelFilterTabs;
        private System.Windows.Forms.Panel panelReservationsList;
        private System.Windows.Forms.Button btnCreateReservation;
        private System.Windows.Forms.TextBox txtBookID;
        private System.Windows.Forms.Label lblBookID;
        private System.Windows.Forms.TextBox txtMemberID;
        private System.Windows.Forms.Label lblMemberID;
        private System.Windows.Forms.Label lblCreateReservationTitle;
        private System.Windows.Forms.Button btnFilterExpired;
        private System.Windows.Forms.Button btnFilterReady;
        private System.Windows.Forms.Button btnFilterActive;
        private System.Windows.Forms.Button btnFilterAll;
        private System.Windows.Forms.Label lblReservationsHistory;
        private System.Windows.Forms.Panel panelMainContainer;
        private System.Windows.Forms.Panel panelStaffReservationMemberInfo;
        private System.Windows.Forms.Label lblMemberEligibilityHeader;
        private System.Windows.Forms.Label lblMemberName;
        private System.Windows.Forms.Label lblMemberType;
        private System.Windows.Forms.Label lblMemberStatus;
        private System.Windows.Forms.Label lblMemberReservations;
        private System.Windows.Forms.Label lblMemberOverdue;
        private System.Windows.Forms.Label lblMemberFines;
        private System.Windows.Forms.Label lblMemberEligibilityTitle;
        private System.Windows.Forms.Label lblMemberEligibilityStatus;
        private System.Windows.Forms.Panel panelStaffReservationBookInfo;
        private System.Windows.Forms.Label lblBookInformationHeader;
        private System.Windows.Forms.Label lblBookTitle;
        private System.Windows.Forms.Label lblBookAuthor;
        private System.Windows.Forms.Label lblBookStatus;
        private System.Windows.Forms.Label lblBookCopies;
        private System.Windows.Forms.Label lblBookEligibilityTitle;
        private System.Windows.Forms.Label lblBookEligibilityStatus;
    }
}