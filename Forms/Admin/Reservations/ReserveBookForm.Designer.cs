namespace Project5LMS.Forms.Admin.Reservations
{
    partial class ReserveBookForm
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
            this.btnProccessReservation = new System.Windows.Forms.Button();
            this.txtReservationMemberID = new System.Windows.Forms.TextBox();
            this.lblBorrowMemberID = new System.Windows.Forms.Label();
            this.txtReservationBookID = new System.Windows.Forms.TextBox();
            this.lblBookID = new System.Windows.Forms.Label();
            this.lblMemberID = new System.Windows.Forms.Label();
            this.dtpReservationDate = new System.Windows.Forms.DateTimePicker();
            this.lblReservationDate = new System.Windows.Forms.Label();
            this.lblExpirationDate = new System.Windows.Forms.Label();
            this.cmbPrioNo = new System.Windows.Forms.ComboBox();
            this.lblPrioityNo = new System.Windows.Forms.Label();
            this.panelReserveBookInfo = new System.Windows.Forms.Panel();
            this.lblReserveBookCopies = new System.Windows.Forms.Label();
            this.lblReserveBookStatus = new System.Windows.Forms.Label();
            this.lblReserveBookAuthor = new System.Windows.Forms.Label();
            this.lblReserveBookTitle = new System.Windows.Forms.Label();
            this.lblBorrowBookInfoTitle = new System.Windows.Forms.Label();
            this.panelMemberEligibility = new System.Windows.Forms.Panel();
            this.lblReserveEligibilityStatus = new System.Windows.Forms.Label();
            this.lblReserveMemberFines = new System.Windows.Forms.Label();
            this.lblReserveMemberOverdue = new System.Windows.Forms.Label();
            this.lblReserveMemberBorrowings = new System.Windows.Forms.Label();
            this.lblReserveMemberStatus = new System.Windows.Forms.Label();
            this.lblReserveMemberType = new System.Windows.Forms.Label();
            this.lblReserveMemberName = new System.Windows.Forms.Label();
            this.lblEligibilityTitle = new System.Windows.Forms.Label();
            this.lblMemberEligibilityTitle = new System.Windows.Forms.Label();
            this.dtpExpirationDate = new System.Windows.Forms.DateTimePicker();
            this.panelReserveBookInfo.SuspendLayout();
            this.panelMemberEligibility.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnProccessReservation
            // 
            this.btnProccessReservation.BackColor = System.Drawing.Color.Maroon;
            this.btnProccessReservation.FlatAppearance.BorderSize = 0;
            this.btnProccessReservation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProccessReservation.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProccessReservation.ForeColor = System.Drawing.Color.White;
            this.btnProccessReservation.Location = new System.Drawing.Point(68, 768);
            this.btnProccessReservation.Margin = new System.Windows.Forms.Padding(4);
            this.btnProccessReservation.Name = "btnProccessReservation";
            this.btnProccessReservation.Size = new System.Drawing.Size(527, 43);
            this.btnProccessReservation.TabIndex = 11;
            this.btnProccessReservation.Text = "Process Reservation";
            this.btnProccessReservation.UseVisualStyleBackColor = false;
            this.btnProccessReservation.Click += new System.EventHandler(this.btnProccessReservation_Click);
            // 
            // txtReservationMemberID
            // 
            this.txtReservationMemberID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReservationMemberID.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReservationMemberID.ForeColor = System.Drawing.Color.Gray;
            this.txtReservationMemberID.Location = new System.Drawing.Point(68, 292);
            this.txtReservationMemberID.Margin = new System.Windows.Forms.Padding(4);
            this.txtReservationMemberID.Name = "txtReservationMemberID";
            this.txtReservationMemberID.Size = new System.Drawing.Size(528, 30);
            this.txtReservationMemberID.TabIndex = 9;
            this.txtReservationMemberID.Text = "Format: MEM-000001 or 1";
            this.txtReservationMemberID.TextChanged += new System.EventHandler(this.txtReservationMemberID_TextChanged);
            this.txtReservationMemberID.Enter += new System.EventHandler(this.txtReservationMemberID_Enter);
            this.txtReservationMemberID.Leave += new System.EventHandler(this.txtReservationMemberID_Leave);
            // 
            // lblBorrowMemberID
            // 
            this.lblBorrowMemberID.AutoSize = true;
            this.lblBorrowMemberID.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBorrowMemberID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblBorrowMemberID.Location = new System.Drawing.Point(-384, 53);
            this.lblBorrowMemberID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBorrowMemberID.Name = "lblBorrowMemberID";
            this.lblBorrowMemberID.Size = new System.Drawing.Size(352, 25);
            this.lblBorrowMemberID.TabIndex = 8;
            this.lblBorrowMemberID.Text = "Member ID (Format: MEM-000001 or 1)";
            // 
            // txtReservationBookID
            // 
            this.txtReservationBookID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReservationBookID.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReservationBookID.ForeColor = System.Drawing.Color.Gray;
            this.txtReservationBookID.Location = new System.Drawing.Point(69, 68);
            this.txtReservationBookID.Margin = new System.Windows.Forms.Padding(4);
            this.txtReservationBookID.Name = "txtReservationBookID";
            this.txtReservationBookID.Size = new System.Drawing.Size(527, 30);
            this.txtReservationBookID.TabIndex = 7;
            this.txtReservationBookID.Text = "Format: 1";
            this.txtReservationBookID.TextChanged += new System.EventHandler(this.txtReservationBookID_TextChanged);
            this.txtReservationBookID.Enter += new System.EventHandler(this.txtReservationBookID_Enter);
            this.txtReservationBookID.Leave += new System.EventHandler(this.txtReservationBookID_Leave);
            // 
            // lblBookID
            // 
            this.lblBookID.AutoSize = true;
            this.lblBookID.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBookID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblBookID.Location = new System.Drawing.Point(65, 39);
            this.lblBookID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBookID.Name = "lblBookID";
            this.lblBookID.Size = new System.Drawing.Size(177, 25);
            this.lblBookID.TabIndex = 12;
            this.lblBookID.Text = "Book ID (Format: 1)";
            // 
            // lblMemberID
            // 
            this.lblMemberID.AutoSize = true;
            this.lblMemberID.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMemberID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMemberID.Location = new System.Drawing.Point(67, 263);
            this.lblMemberID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMemberID.Name = "lblMemberID";
            this.lblMemberID.Size = new System.Drawing.Size(352, 25);
            this.lblMemberID.TabIndex = 13;
            this.lblMemberID.Text = "Member ID (Format: MEM-000001 or 1)";
            // 
            // dtpReservationDate
            // 
            this.dtpReservationDate.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpReservationDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpReservationDate.Location = new System.Drawing.Point(71, 561);
            this.dtpReservationDate.Name = "dtpReservationDate";
            this.dtpReservationDate.Size = new System.Drawing.Size(280, 24);
            this.dtpReservationDate.TabIndex = 14;
            // 
            // lblReservationDate
            // 
            this.lblReservationDate.AutoSize = true;
            this.lblReservationDate.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReservationDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblReservationDate.Location = new System.Drawing.Point(67, 533);
            this.lblReservationDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReservationDate.Name = "lblReservationDate";
            this.lblReservationDate.Size = new System.Drawing.Size(159, 25);
            this.lblReservationDate.TabIndex = 16;
            this.lblReservationDate.Text = "Reservation Date";
            // 
            // lblExpirationDate
            // 
            this.lblExpirationDate.AutoSize = true;
            this.lblExpirationDate.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpirationDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblExpirationDate.Location = new System.Drawing.Point(65, 588);
            this.lblExpirationDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblExpirationDate.Name = "lblExpirationDate";
            this.lblExpirationDate.Size = new System.Drawing.Size(145, 25);
            this.lblExpirationDate.TabIndex = 17;
            this.lblExpirationDate.Text = "Expiration Date";
            // 
            // cmbPrioNo
            // 
            this.cmbPrioNo.FormattingEnabled = true;
            this.cmbPrioNo.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cmbPrioNo.Location = new System.Drawing.Point(418, 561);
            this.cmbPrioNo.Name = "cmbPrioNo";
            this.cmbPrioNo.Size = new System.Drawing.Size(121, 24);
            this.cmbPrioNo.TabIndex = 18;
            // 
            // lblPrioityNo
            // 
            this.lblPrioityNo.AutoSize = true;
            this.lblPrioityNo.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrioityNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPrioityNo.Location = new System.Drawing.Point(417, 535);
            this.lblPrioityNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPrioityNo.Name = "lblPrioityNo";
            this.lblPrioityNo.Size = new System.Drawing.Size(111, 25);
            this.lblPrioityNo.TabIndex = 19;
            this.lblPrioityNo.Text = "Priority No.";
            // 
            // panelReserveBookInfo
            // 
            this.panelReserveBookInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.panelReserveBookInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelReserveBookInfo.Controls.Add(this.lblReserveBookCopies);
            this.panelReserveBookInfo.Controls.Add(this.lblReserveBookStatus);
            this.panelReserveBookInfo.Controls.Add(this.lblReserveBookAuthor);
            this.panelReserveBookInfo.Controls.Add(this.lblReserveBookTitle);
            this.panelReserveBookInfo.Controls.Add(this.lblBorrowBookInfoTitle);
            this.panelReserveBookInfo.Location = new System.Drawing.Point(69, 105);
            this.panelReserveBookInfo.Name = "panelReserveBookInfo";
            this.panelReserveBookInfo.Size = new System.Drawing.Size(527, 130);
            this.panelReserveBookInfo.TabIndex = 20;
            this.panelReserveBookInfo.Visible = false;
            // 
            // lblReserveBookCopies
            // 
            this.lblReserveBookCopies.AutoSize = true;
            this.lblReserveBookCopies.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReserveBookCopies.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblReserveBookCopies.Location = new System.Drawing.Point(12, 105);
            this.lblReserveBookCopies.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReserveBookCopies.Name = "lblReserveBookCopies";
            this.lblReserveBookCopies.Size = new System.Drawing.Size(0, 20);
            this.lblReserveBookCopies.TabIndex = 5;
            // 
            // lblReserveBookStatus
            // 
            this.lblReserveBookStatus.AutoSize = true;
            this.lblReserveBookStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReserveBookStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblReserveBookStatus.Location = new System.Drawing.Point(12, 85);
            this.lblReserveBookStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReserveBookStatus.Name = "lblReserveBookStatus";
            this.lblReserveBookStatus.Size = new System.Drawing.Size(0, 20);
            this.lblReserveBookStatus.TabIndex = 4;
            // 
            // lblReserveBookAuthor
            // 
            this.lblReserveBookAuthor.AutoSize = true;
            this.lblReserveBookAuthor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReserveBookAuthor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblReserveBookAuthor.Location = new System.Drawing.Point(12, 60);
            this.lblReserveBookAuthor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReserveBookAuthor.Name = "lblReserveBookAuthor";
            this.lblReserveBookAuthor.Size = new System.Drawing.Size(0, 20);
            this.lblReserveBookAuthor.TabIndex = 3;
            // 
            // lblReserveBookTitle
            // 
            this.lblReserveBookTitle.AutoSize = true;
            this.lblReserveBookTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReserveBookTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblReserveBookTitle.Location = new System.Drawing.Point(12, 35);
            this.lblReserveBookTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReserveBookTitle.Name = "lblReserveBookTitle";
            this.lblReserveBookTitle.Size = new System.Drawing.Size(0, 20);
            this.lblReserveBookTitle.TabIndex = 2;
            // 
            // lblBorrowBookInfoTitle
            // 
            this.lblBorrowBookInfoTitle.AutoSize = true;
            this.lblBorrowBookInfoTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBorrowBookInfoTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblBorrowBookInfoTitle.Location = new System.Drawing.Point(12, 10);
            this.lblBorrowBookInfoTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBorrowBookInfoTitle.Name = "lblBorrowBookInfoTitle";
            this.lblBorrowBookInfoTitle.Size = new System.Drawing.Size(139, 21);
            this.lblBorrowBookInfoTitle.TabIndex = 1;
            this.lblBorrowBookInfoTitle.Text = "Book Information";
            // 
            // panelMemberEligibility
            // 
            this.panelMemberEligibility.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.panelMemberEligibility.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMemberEligibility.Controls.Add(this.lblReserveEligibilityStatus);
            this.panelMemberEligibility.Controls.Add(this.lblReserveMemberFines);
            this.panelMemberEligibility.Controls.Add(this.lblReserveMemberOverdue);
            this.panelMemberEligibility.Controls.Add(this.lblReserveMemberBorrowings);
            this.panelMemberEligibility.Controls.Add(this.lblReserveMemberStatus);
            this.panelMemberEligibility.Controls.Add(this.lblReserveMemberType);
            this.panelMemberEligibility.Controls.Add(this.lblReserveMemberName);
            this.panelMemberEligibility.Controls.Add(this.lblEligibilityTitle);
            this.panelMemberEligibility.Controls.Add(this.lblMemberEligibilityTitle);
            this.panelMemberEligibility.Location = new System.Drawing.Point(69, 329);
            this.panelMemberEligibility.Name = "panelMemberEligibility";
            this.panelMemberEligibility.Size = new System.Drawing.Size(527, 140);
            this.panelMemberEligibility.TabIndex = 21;
            this.panelMemberEligibility.Visible = false;
            // 
            // lblReserveEligibilityStatus
            // 
            this.lblReserveEligibilityStatus.AutoSize = true;
            this.lblReserveEligibilityStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReserveEligibilityStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(139)))), ((int)(((byte)(34)))));
            this.lblReserveEligibilityStatus.Location = new System.Drawing.Point(12, 115);
            this.lblReserveEligibilityStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReserveEligibilityStatus.Name = "lblReserveEligibilityStatus";
            this.lblReserveEligibilityStatus.Size = new System.Drawing.Size(0, 23);
            this.lblReserveEligibilityStatus.TabIndex = 9;
            // 
            // lblReserveMemberFines
            // 
            this.lblReserveMemberFines.AutoSize = true;
            this.lblReserveMemberFines.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReserveMemberFines.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblReserveMemberFines.Location = new System.Drawing.Point(280, 55);
            this.lblReserveMemberFines.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReserveMemberFines.Name = "lblReserveMemberFines";
            this.lblReserveMemberFines.Size = new System.Drawing.Size(0, 20);
            this.lblReserveMemberFines.TabIndex = 8;
            // 
            // lblReserveMemberOverdue
            // 
            this.lblReserveMemberOverdue.AutoSize = true;
            this.lblReserveMemberOverdue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReserveMemberOverdue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblReserveMemberOverdue.Location = new System.Drawing.Point(280, 35);
            this.lblReserveMemberOverdue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReserveMemberOverdue.Name = "lblReserveMemberOverdue";
            this.lblReserveMemberOverdue.Size = new System.Drawing.Size(0, 20);
            this.lblReserveMemberOverdue.TabIndex = 7;
            // 
            // lblReserveMemberBorrowings
            // 
            this.lblReserveMemberBorrowings.AutoSize = true;
            this.lblReserveMemberBorrowings.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReserveMemberBorrowings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblReserveMemberBorrowings.Location = new System.Drawing.Point(12, 95);
            this.lblReserveMemberBorrowings.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReserveMemberBorrowings.Name = "lblReserveMemberBorrowings";
            this.lblReserveMemberBorrowings.Size = new System.Drawing.Size(0, 20);
            this.lblReserveMemberBorrowings.TabIndex = 6;
            // 
            // lblReserveMemberStatus
            // 
            this.lblReserveMemberStatus.AutoSize = true;
            this.lblReserveMemberStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReserveMemberStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblReserveMemberStatus.Location = new System.Drawing.Point(12, 75);
            this.lblReserveMemberStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReserveMemberStatus.Name = "lblReserveMemberStatus";
            this.lblReserveMemberStatus.Size = new System.Drawing.Size(0, 20);
            this.lblReserveMemberStatus.TabIndex = 5;
            // 
            // lblReserveMemberType
            // 
            this.lblReserveMemberType.AutoSize = true;
            this.lblReserveMemberType.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReserveMemberType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblReserveMemberType.Location = new System.Drawing.Point(12, 55);
            this.lblReserveMemberType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReserveMemberType.Name = "lblReserveMemberType";
            this.lblReserveMemberType.Size = new System.Drawing.Size(0, 20);
            this.lblReserveMemberType.TabIndex = 4;
            // 
            // lblReserveMemberName
            // 
            this.lblReserveMemberName.AutoSize = true;
            this.lblReserveMemberName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReserveMemberName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblReserveMemberName.Location = new System.Drawing.Point(12, 35);
            this.lblReserveMemberName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReserveMemberName.Name = "lblReserveMemberName";
            this.lblReserveMemberName.Size = new System.Drawing.Size(0, 20);
            this.lblReserveMemberName.TabIndex = 3;
            // 
            // lblEligibilityTitle
            // 
            this.lblEligibilityTitle.AutoSize = true;
            this.lblEligibilityTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEligibilityTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblEligibilityTitle.Location = new System.Drawing.Point(280, 75);
            this.lblEligibilityTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEligibilityTitle.Name = "lblEligibilityTitle";
            this.lblEligibilityTitle.Size = new System.Drawing.Size(76, 20);
            this.lblEligibilityTitle.TabIndex = 8;
            this.lblEligibilityTitle.Text = "Eligibility:";
            // 
            // lblMemberEligibilityTitle
            // 
            this.lblMemberEligibilityTitle.AutoSize = true;
            this.lblMemberEligibilityTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMemberEligibilityTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMemberEligibilityTitle.Location = new System.Drawing.Point(12, 10);
            this.lblMemberEligibilityTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMemberEligibilityTitle.Name = "lblMemberEligibilityTitle";
            this.lblMemberEligibilityTitle.Size = new System.Drawing.Size(143, 21);
            this.lblMemberEligibilityTitle.TabIndex = 2;
            this.lblMemberEligibilityTitle.Text = "Member Eligibility";
            // 
            // dtpExpirationDate
            // 
            this.dtpExpirationDate.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpExpirationDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpExpirationDate.Location = new System.Drawing.Point(70, 628);
            this.dtpExpirationDate.Name = "dtpExpirationDate";
            this.dtpExpirationDate.Size = new System.Drawing.Size(280, 24);
            this.dtpExpirationDate.TabIndex = 22;
            // 
            // ReserveBookForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 842);
            this.Controls.Add(this.dtpExpirationDate);
            this.Controls.Add(this.panelMemberEligibility);
            this.Controls.Add(this.panelReserveBookInfo);
            this.Controls.Add(this.lblPrioityNo);
            this.Controls.Add(this.cmbPrioNo);
            this.Controls.Add(this.lblExpirationDate);
            this.Controls.Add(this.lblReservationDate);
            this.Controls.Add(this.dtpReservationDate);
            this.Controls.Add(this.lblMemberID);
            this.Controls.Add(this.lblBookID);
            this.Controls.Add(this.btnProccessReservation);
            this.Controls.Add(this.txtReservationMemberID);
            this.Controls.Add(this.lblBorrowMemberID);
            this.Controls.Add(this.txtReservationBookID);
            this.Name = "ReserveBookForm";
            this.Text = "ReserveBookForm";
            this.panelReserveBookInfo.ResumeLayout(false);
            this.panelReserveBookInfo.PerformLayout();
            this.panelMemberEligibility.ResumeLayout(false);
            this.panelMemberEligibility.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnProccessReservation;
        private System.Windows.Forms.TextBox txtReservationMemberID;
        private System.Windows.Forms.Label lblBorrowMemberID;
        private System.Windows.Forms.TextBox txtReservationBookID;
        private System.Windows.Forms.Label lblBookID;
        private System.Windows.Forms.Label lblMemberID;
        private System.Windows.Forms.DateTimePicker dtpReservationDate;
        private System.Windows.Forms.Label lblReservationDate;
        private System.Windows.Forms.Label lblExpirationDate;
        private System.Windows.Forms.ComboBox cmbPrioNo;
        private System.Windows.Forms.Label lblPrioityNo;
        private System.Windows.Forms.Panel panelReserveBookInfo;
        private System.Windows.Forms.Panel panelMemberEligibility;
        private System.Windows.Forms.Label lblMemberEligibilityTitle;
        private System.Windows.Forms.Label lblEligibilityTitle;
        private System.Windows.Forms.Label lblBorrowBookInfoTitle;
        private System.Windows.Forms.DateTimePicker dtpExpirationDate;
        private System.Windows.Forms.Label lblReserveBookTitle;
        private System.Windows.Forms.Label lblReserveBookAuthor;
        private System.Windows.Forms.Label lblReserveBookStatus;
        private System.Windows.Forms.Label lblReserveBookCopies;
        private System.Windows.Forms.Label lblReserveMemberName;
        private System.Windows.Forms.Label lblReserveMemberType;
        private System.Windows.Forms.Label lblReserveMemberStatus;
        private System.Windows.Forms.Label lblReserveMemberBorrowings;
        private System.Windows.Forms.Label lblReserveMemberOverdue;
        private System.Windows.Forms.Label lblReserveMemberFines;
        private System.Windows.Forms.Label lblReserveEligibilityStatus;
    }
}