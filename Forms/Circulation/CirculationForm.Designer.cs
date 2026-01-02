namespace Project5LMS.Admin_Dashboard
{
    partial class CirculationForm
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
            this.panelBottomSection = new System.Windows.Forms.Panel();
            this.panelGuidelines = new System.Windows.Forms.Panel();
            this.lblRequirementsTitle = new System.Windows.Forms.Label();
            this.lblRequirement3 = new System.Windows.Forms.Label();
            this.lblRequirement2 = new System.Windows.Forms.Label();
            this.lblRequirement1 = new System.Windows.Forms.Label();
            this.lblLoanPeriodsTitle = new System.Windows.Forms.Label();
            this.lblGuestPeriod = new System.Windows.Forms.Label();
            this.lblStaffPeriod = new System.Windows.Forms.Label();
            this.lblFacultyPeriod = new System.Windows.Forms.Label();
            this.lblStudentPeriod = new System.Windows.Forms.Label();
            this.lblGuidelinesTitle = new System.Windows.Forms.Label();
            this.panelCheckoutForm = new System.Windows.Forms.Panel();
            this.btnProcessCheckout = new System.Windows.Forms.Button();
            this.lblDueDate = new System.Windows.Forms.Label();
            this.txtBookISBN = new System.Windows.Forms.TextBox();
            this.lblBookISBN = new System.Windows.Forms.Label();
            this.txtMemberID = new System.Windows.Forms.TextBox();
            this.lblMemberID = new System.Windows.Forms.Label();
            this.lblCheckoutSubtitle = new System.Windows.Forms.Label();
            this.lblCheckoutTitle = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabCheckout = new System.Windows.Forms.TabPage();
            this.tabReturn = new System.Windows.Forms.TabPage();
            this.tabRenewal = new System.Windows.Forms.TabPage();
            this.tabHistory = new System.Windows.Forms.TabPage();
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
            this.panelBottomSection.SuspendLayout();
            this.panelGuidelines.SuspendLayout();
            this.panelCheckoutForm.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabCheckout.SuspendLayout();
            this.panelMetrics.SuspendLayout();
            this.panelMetricCard4.SuspendLayout();
            this.panelMetricCard3.SuspendLayout();
            this.panelMetricCard2.SuspendLayout();
            this.panelMetricCard1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMainContainer
            // 
            this.panelMainContainer.AutoScroll = true;
            this.panelMainContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.panelMainContainer.Controls.Add(this.panelBottomSection);
            this.panelMainContainer.Controls.Add(this.panelMetrics);
            this.panelMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainContainer.Location = new System.Drawing.Point(0, 0);
            this.panelMainContainer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelMainContainer.Name = "panelMainContainer";
            this.panelMainContainer.Padding = new System.Windows.Forms.Padding(30, 30, 30, 30);
            this.panelMainContainer.Size = new System.Drawing.Size(1942, 1102);
            this.panelMainContainer.TabIndex = 0;
            // 
            // panelBottomSection
            // 
            this.panelBottomSection.Controls.Add(this.panelGuidelines);
            this.panelBottomSection.Controls.Add(this.panelCheckoutForm);
            this.panelBottomSection.Controls.Add(this.tabControl);
            this.panelBottomSection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBottomSection.Location = new System.Drawing.Point(30, 200);
            this.panelBottomSection.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelBottomSection.Name = "panelBottomSection";
            this.panelBottomSection.Size = new System.Drawing.Size(1882, 872);
            this.panelBottomSection.TabIndex = 1;
            // 
            // panelGuidelines
            // 
            this.panelGuidelines.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelGuidelines.BackColor = System.Drawing.Color.White;
            this.panelGuidelines.Controls.Add(this.lblRequirementsTitle);
            this.panelGuidelines.Controls.Add(this.lblRequirement3);
            this.panelGuidelines.Controls.Add(this.lblRequirement2);
            this.panelGuidelines.Controls.Add(this.lblRequirement1);
            this.panelGuidelines.Controls.Add(this.lblLoanPeriodsTitle);
            this.panelGuidelines.Controls.Add(this.lblGuestPeriod);
            this.panelGuidelines.Controls.Add(this.lblStaffPeriod);
            this.panelGuidelines.Controls.Add(this.lblFacultyPeriod);
            this.panelGuidelines.Controls.Add(this.lblStudentPeriod);
            this.panelGuidelines.Controls.Add(this.lblGuidelinesTitle);
            this.panelGuidelines.Location = new System.Drawing.Point(950, 60);
            this.panelGuidelines.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelGuidelines.Name = "panelGuidelines";
            this.panelGuidelines.Padding = new System.Windows.Forms.Padding(30, 30, 30, 30);
            this.panelGuidelines.Size = new System.Drawing.Size(932, 800);
            this.panelGuidelines.TabIndex = 2;
            // 
            // lblRequirementsTitle
            // 
            this.lblRequirementsTitle.AutoSize = true;
            this.lblRequirementsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRequirementsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblRequirementsTitle.Location = new System.Drawing.Point(30, 450);
            this.lblRequirementsTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRequirementsTitle.Name = "lblRequirementsTitle";
            this.lblRequirementsTitle.Size = new System.Drawing.Size(120, 25);
            this.lblRequirementsTitle.TabIndex = 9;
            this.lblRequirementsTitle.Text = "Requirements";
            // 
            // lblRequirement3
            // 
            this.lblRequirement3.AutoSize = true;
            this.lblRequirement3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRequirement3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblRequirement3.Location = new System.Drawing.Point(50, 550);
            this.lblRequirement3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRequirement3.Name = "lblRequirement3";
            this.lblRequirement3.Size = new System.Drawing.Size(180, 24);
            this.lblRequirement3.TabIndex = 8;
            this.lblRequirement3.Text = "• Book must be available";
            // 
            // lblRequirement2
            // 
            this.lblRequirement2.AutoSize = true;
            this.lblRequirement2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRequirement2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblRequirement2.Location = new System.Drawing.Point(50, 520);
            this.lblRequirement2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRequirement2.Name = "lblRequirement2";
            this.lblRequirement2.Size = new System.Drawing.Size(250, 24);
            this.lblRequirement2.TabIndex = 7;
            this.lblRequirement2.Text = "• No outstanding fines over ₱*";
            // 
            // lblRequirement1
            // 
            this.lblRequirement1.AutoSize = true;
            this.lblRequirement1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRequirement1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblRequirement1.Location = new System.Drawing.Point(50, 490);
            this.lblRequirement1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRequirement1.Name = "lblRequirement1";
            this.lblRequirement1.Size = new System.Drawing.Size(220, 24);
            this.lblRequirement1.TabIndex = 6;
            this.lblRequirement1.Text = "• Valid membership status";
            // 
            // lblLoanPeriodsTitle
            // 
            this.lblLoanPeriodsTitle.AutoSize = true;
            this.lblLoanPeriodsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoanPeriodsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblLoanPeriodsTitle.Location = new System.Drawing.Point(30, 150);
            this.lblLoanPeriodsTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLoanPeriodsTitle.Name = "lblLoanPeriodsTitle";
            this.lblLoanPeriodsTitle.Size = new System.Drawing.Size(130, 25);
            this.lblLoanPeriodsTitle.TabIndex = 5;
            this.lblLoanPeriodsTitle.Text = "Loan Periods";
            // 
            // lblGuestPeriod
            // 
            this.lblGuestPeriod.AutoSize = true;
            this.lblGuestPeriod.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGuestPeriod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblGuestPeriod.Location = new System.Drawing.Point(50, 380);
            this.lblGuestPeriod.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGuestPeriod.Name = "lblGuestPeriod";
            this.lblGuestPeriod.Size = new System.Drawing.Size(200, 24);
            this.lblGuestPeriod.TabIndex = 4;
            this.lblGuestPeriod.Text = "Guest: 7 days (max 3 books)";
            // 
            // lblStaffPeriod
            // 
            this.lblStaffPeriod.AutoSize = true;
            this.lblStaffPeriod.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStaffPeriod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblStaffPeriod.Location = new System.Drawing.Point(50, 320);
            this.lblStaffPeriod.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStaffPeriod.Name = "lblStaffPeriod";
            this.lblStaffPeriod.Size = new System.Drawing.Size(200, 24);
            this.lblStaffPeriod.TabIndex = 3;
            this.lblStaffPeriod.Text = "Staff: 21 days (max 7 books)";
            // 
            // lblFacultyPeriod
            // 
            this.lblFacultyPeriod.AutoSize = true;
            this.lblFacultyPeriod.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFacultyPeriod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblFacultyPeriod.Location = new System.Drawing.Point(50, 260);
            this.lblFacultyPeriod.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFacultyPeriod.Name = "lblFacultyPeriod";
            this.lblFacultyPeriod.Size = new System.Drawing.Size(220, 24);
            this.lblFacultyPeriod.TabIndex = 2;
            this.lblFacultyPeriod.Text = "Faculty: 30 days (max 10 books)";
            // 
            // lblStudentPeriod
            // 
            this.lblStudentPeriod.AutoSize = true;
            this.lblStudentPeriod.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStudentPeriod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblStudentPeriod.Location = new System.Drawing.Point(50, 200);
            this.lblStudentPeriod.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStudentPeriod.Name = "lblStudentPeriod";
            this.lblStudentPeriod.Size = new System.Drawing.Size(220, 24);
            this.lblStudentPeriod.TabIndex = 1;
            this.lblStudentPeriod.Text = "Students: 14 days (max 5 books)";
            // 
            // lblGuidelinesTitle
            // 
            this.lblGuidelinesTitle.AutoSize = true;
            this.lblGuidelinesTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGuidelinesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblGuidelinesTitle.Location = new System.Drawing.Point(30, 30);
            this.lblGuidelinesTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGuidelinesTitle.Name = "lblGuidelinesTitle";
            this.lblGuidelinesTitle.Size = new System.Drawing.Size(240, 31);
            this.lblGuidelinesTitle.TabIndex = 0;
            this.lblGuidelinesTitle.Text = "Checkout Guidelines";
            // 
            // panelCheckoutForm
            // 
            this.panelCheckoutForm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelCheckoutForm.BackColor = System.Drawing.Color.White;
            this.panelCheckoutForm.Controls.Add(this.btnProcessCheckout);
            this.panelCheckoutForm.Controls.Add(this.lblDueDate);
            this.panelCheckoutForm.Controls.Add(this.txtBookISBN);
            this.panelCheckoutForm.Controls.Add(this.lblBookISBN);
            this.panelCheckoutForm.Controls.Add(this.txtMemberID);
            this.panelCheckoutForm.Controls.Add(this.lblMemberID);
            this.panelCheckoutForm.Controls.Add(this.lblCheckoutSubtitle);
            this.panelCheckoutForm.Controls.Add(this.lblCheckoutTitle);
            this.panelCheckoutForm.Location = new System.Drawing.Point(0, 60);
            this.panelCheckoutForm.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelCheckoutForm.Name = "panelCheckoutForm";
            this.panelCheckoutForm.Padding = new System.Windows.Forms.Padding(30, 30, 30, 30);
            this.panelCheckoutForm.Size = new System.Drawing.Size(930, 800);
            this.panelCheckoutForm.TabIndex = 1;
            // 
            // btnProcessCheckout
            // 
            this.btnProcessCheckout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnProcessCheckout.FlatAppearance.BorderSize = 0;
            this.btnProcessCheckout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcessCheckout.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProcessCheckout.ForeColor = System.Drawing.Color.White;
            this.btnProcessCheckout.Location = new System.Drawing.Point(30, 350);
            this.btnProcessCheckout.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnProcessCheckout.Name = "btnProcessCheckout";
            this.btnProcessCheckout.Size = new System.Drawing.Size(870, 50);
            this.btnProcessCheckout.TabIndex = 7;
            this.btnProcessCheckout.Text = "Process Checkout";
            this.btnProcessCheckout.UseVisualStyleBackColor = false;
            this.btnProcessCheckout.Click += new System.EventHandler(this.btnProcessCheckout_Click);
            // 
            // lblDueDate
            // 
            this.lblDueDate.AutoSize = true;
            this.lblDueDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDueDate.ForeColor = System.Drawing.Color.Gray;
            this.lblDueDate.Location = new System.Drawing.Point(30, 300);
            this.lblDueDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDueDate.Name = "lblDueDate";
            this.lblDueDate.Size = new System.Drawing.Size(100, 24);
            this.lblDueDate.TabIndex = 6;
            this.lblDueDate.Text = "Due Date: --";
            // 
            // txtBookISBN
            // 
            this.txtBookISBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBookISBN.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBookISBN.Location = new System.Drawing.Point(30, 250);
            this.txtBookISBN.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBookISBN.Name = "txtBookISBN";
            this.txtBookISBN.Size = new System.Drawing.Size(870, 28);
            this.txtBookISBN.TabIndex = 5;
            this.txtBookISBN.TextChanged += new System.EventHandler(this.txtBookISBN_TextChanged);
            this.txtBookISBN.Leave += new System.EventHandler(this.txtBookISBN_Leave);
            // 
            // lblBookISBN
            // 
            this.lblBookISBN.AutoSize = true;
            this.lblBookISBN.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBookISBN.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblBookISBN.Location = new System.Drawing.Point(30, 220);
            this.lblBookISBN.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBookISBN.Name = "lblBookISBN";
            this.lblBookISBN.Size = new System.Drawing.Size(180, 24);
            this.lblBookISBN.TabIndex = 4;
            this.lblBookISBN.Text = "Book ISBN or Title";
            // 
            // txtMemberID
            // 
            this.txtMemberID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMemberID.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMemberID.Location = new System.Drawing.Point(30, 170);
            this.txtMemberID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtMemberID.Name = "txtMemberID";
            this.txtMemberID.Size = new System.Drawing.Size(870, 28);
            this.txtMemberID.TabIndex = 3;
            this.txtMemberID.TextChanged += new System.EventHandler(this.txtMemberID_TextChanged);
            this.txtMemberID.Leave += new System.EventHandler(this.txtMemberID_Leave);
            // 
            // lblMemberID
            // 
            this.lblMemberID.AutoSize = true;
            this.lblMemberID.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMemberID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMemberID.Location = new System.Drawing.Point(30, 140);
            this.lblMemberID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMemberID.Name = "lblMemberID";
            this.lblMemberID.Size = new System.Drawing.Size(200, 24);
            this.lblMemberID.TabIndex = 2;
            this.lblMemberID.Text = "Member ID or Name";
            // 
            // lblCheckoutSubtitle
            // 
            this.lblCheckoutSubtitle.AutoSize = true;
            this.lblCheckoutSubtitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckoutSubtitle.ForeColor = System.Drawing.Color.Gray;
            this.lblCheckoutSubtitle.Location = new System.Drawing.Point(30, 80);
            this.lblCheckoutSubtitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCheckoutSubtitle.Name = "lblCheckoutSubtitle";
            this.lblCheckoutSubtitle.Size = new System.Drawing.Size(200, 24);
            this.lblCheckoutSubtitle.TabIndex = 1;
            this.lblCheckoutSubtitle.Text = "Issue a book to a member";
            // 
            // lblCheckoutTitle
            // 
            this.lblCheckoutTitle.AutoSize = true;
            this.lblCheckoutTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckoutTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCheckoutTitle.Location = new System.Drawing.Point(30, 30);
            this.lblCheckoutTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCheckoutTitle.Name = "lblCheckoutTitle";
            this.lblCheckoutTitle.Size = new System.Drawing.Size(180, 31);
            this.lblCheckoutTitle.TabIndex = 0;
            this.lblCheckoutTitle.Text = "Checkout Book";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabCheckout);
            this.tabControl.Controls.Add(this.tabReturn);
            this.tabControl.Controls.Add(this.tabRenewal);
            this.tabControl.Controls.Add(this.tabHistory);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1882, 60);
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabCheckout
            // 
            this.tabCheckout.Controls.Add(this.panelCheckoutForm);
            this.tabCheckout.Location = new System.Drawing.Point(4, 33);
            this.tabCheckout.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabCheckout.Name = "tabCheckout";
            this.tabCheckout.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabCheckout.Size = new System.Drawing.Size(1874, 865);
            this.tabCheckout.TabIndex = 0;
            this.tabCheckout.Text = "Checkout";
            this.tabCheckout.UseVisualStyleBackColor = true;
            // 
            // tabReturn
            // 
            this.tabReturn.Location = new System.Drawing.Point(4, 33);
            this.tabReturn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabReturn.Name = "tabReturn";
            this.tabReturn.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabReturn.Size = new System.Drawing.Size(1874, 865);
            this.tabReturn.TabIndex = 1;
            this.tabReturn.Text = "Return";
            this.tabReturn.UseVisualStyleBackColor = true;
            // 
            // tabRenewal
            // 
            this.tabRenewal.Location = new System.Drawing.Point(4, 33);
            this.tabRenewal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabRenewal.Name = "tabRenewal";
            this.tabRenewal.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabRenewal.Size = new System.Drawing.Size(1874, 865);
            this.tabRenewal.TabIndex = 2;
            this.tabRenewal.Text = "Renewal";
            this.tabRenewal.UseVisualStyleBackColor = true;
            // 
            // tabHistory
            // 
            this.tabHistory.Location = new System.Drawing.Point(4, 33);
            this.tabHistory.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabHistory.Name = "tabHistory";
            this.tabHistory.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabHistory.Size = new System.Drawing.Size(1874, 865);
            this.tabHistory.TabIndex = 3;
            this.tabHistory.Text = "History";
            this.tabHistory.UseVisualStyleBackColor = true;
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
            this.panelMetricCard4.Location = new System.Drawing.Point(1410, 10);
            this.panelMetricCard4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelMetricCard4.Name = "panelMetricCard4";
            this.panelMetricCard4.Padding = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.panelMetricCard4.Size = new System.Drawing.Size(450, 150);
            this.panelMetricCard4.TabIndex = 3;
            // 
            // lblMetricValue4
            // 
            this.lblMetricValue4.AutoSize = true;
            this.lblMetricValue4.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricValue4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricValue4.Location = new System.Drawing.Point(20, 20);
            this.lblMetricValue4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricValue4.Name = "lblMetricValue4";
            this.lblMetricValue4.Size = new System.Drawing.Size(55, 63);
            this.lblMetricValue4.TabIndex = 1;
            this.lblMetricValue4.Text = "0";
            // 
            // lblMetricTitle4
            // 
            this.lblMetricTitle4.AutoSize = true;
            this.lblMetricTitle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTitle4.ForeColor = System.Drawing.Color.Gray;
            this.lblMetricTitle4.Location = new System.Drawing.Point(20, 90);
            this.lblMetricTitle4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricTitle4.Name = "lblMetricTitle4";
            this.lblMetricTitle4.Size = new System.Drawing.Size(150, 25);
            this.lblMetricTitle4.TabIndex = 0;
            this.lblMetricTitle4.Text = "Active Members";
            // 
            // panelMetricCard3
            // 
            this.panelMetricCard3.BackColor = System.Drawing.Color.White;
            this.panelMetricCard3.Controls.Add(this.lblMetricValue3);
            this.panelMetricCard3.Controls.Add(this.lblMetricTitle3);
            this.panelMetricCard3.Location = new System.Drawing.Point(940, 10);
            this.panelMetricCard3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelMetricCard3.Name = "panelMetricCard3";
            this.panelMetricCard3.Padding = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.panelMetricCard3.Size = new System.Drawing.Size(450, 150);
            this.panelMetricCard3.TabIndex = 2;
            // 
            // lblMetricValue3
            // 
            this.lblMetricValue3.AutoSize = true;
            this.lblMetricValue3.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricValue3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricValue3.Location = new System.Drawing.Point(20, 20);
            this.lblMetricValue3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricValue3.Name = "lblMetricValue3";
            this.lblMetricValue3.Size = new System.Drawing.Size(55, 63);
            this.lblMetricValue3.TabIndex = 1;
            this.lblMetricValue3.Text = "0";
            // 
            // lblMetricTitle3
            // 
            this.lblMetricTitle3.AutoSize = true;
            this.lblMetricTitle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTitle3.ForeColor = System.Drawing.Color.Gray;
            this.lblMetricTitle3.Location = new System.Drawing.Point(20, 90);
            this.lblMetricTitle3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricTitle3.Name = "lblMetricTitle3";
            this.lblMetricTitle3.Size = new System.Drawing.Size(140, 25);
            this.lblMetricTitle3.TabIndex = 0;
            this.lblMetricTitle3.Text = "Available Books";
            // 
            // panelMetricCard2
            // 
            this.panelMetricCard2.BackColor = System.Drawing.Color.White;
            this.panelMetricCard2.Controls.Add(this.lblMetricValue2);
            this.panelMetricCard2.Controls.Add(this.lblMetricTitle2);
            this.panelMetricCard2.Location = new System.Drawing.Point(470, 10);
            this.panelMetricCard2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelMetricCard2.Name = "panelMetricCard2";
            this.panelMetricCard2.Padding = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.panelMetricCard2.Size = new System.Drawing.Size(450, 150);
            this.panelMetricCard2.TabIndex = 1;
            // 
            // lblMetricValue2
            // 
            this.lblMetricValue2.AutoSize = true;
            this.lblMetricValue2.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricValue2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricValue2.Location = new System.Drawing.Point(20, 20);
            this.lblMetricValue2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricValue2.Name = "lblMetricValue2";
            this.lblMetricValue2.Size = new System.Drawing.Size(55, 63);
            this.lblMetricValue2.TabIndex = 1;
            this.lblMetricValue2.Text = "0";
            // 
            // lblMetricTitle2
            // 
            this.lblMetricTitle2.AutoSize = true;
            this.lblMetricTitle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTitle2.ForeColor = System.Drawing.Color.Gray;
            this.lblMetricTitle2.Location = new System.Drawing.Point(20, 90);
            this.lblMetricTitle2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricTitle2.Name = "lblMetricTitle2";
            this.lblMetricTitle2.Size = new System.Drawing.Size(80, 25);
            this.lblMetricTitle2.TabIndex = 0;
            this.lblMetricTitle2.Text = "Overdue";
            // 
            // panelMetricCard1
            // 
            this.panelMetricCard1.BackColor = System.Drawing.Color.White;
            this.panelMetricCard1.Controls.Add(this.lblMetricValue1);
            this.panelMetricCard1.Controls.Add(this.lblMetricTitle1);
            this.panelMetricCard1.Location = new System.Drawing.Point(0, 10);
            this.panelMetricCard1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelMetricCard1.Name = "panelMetricCard1";
            this.panelMetricCard1.Padding = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.panelMetricCard1.Size = new System.Drawing.Size(450, 150);
            this.panelMetricCard1.TabIndex = 0;
            // 
            // lblMetricValue1
            // 
            this.lblMetricValue1.AutoSize = true;
            this.lblMetricValue1.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricValue1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricValue1.Location = new System.Drawing.Point(20, 20);
            this.lblMetricValue1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricValue1.Name = "lblMetricValue1";
            this.lblMetricValue1.Size = new System.Drawing.Size(55, 63);
            this.lblMetricValue1.TabIndex = 1;
            this.lblMetricValue1.Text = "0";
            // 
            // lblMetricTitle1
            // 
            this.lblMetricTitle1.AutoSize = true;
            this.lblMetricTitle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTitle1.ForeColor = System.Drawing.Color.Gray;
            this.lblMetricTitle1.Location = new System.Drawing.Point(20, 90);
            this.lblMetricTitle1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricTitle1.Name = "lblMetricTitle1";
            this.lblMetricTitle1.Size = new System.Drawing.Size(120, 25);
            this.lblMetricTitle1.TabIndex = 0;
            this.lblMetricTitle1.Text = "Active Loans";
            // 
            // CirculationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1942, 1102);
            this.Controls.Add(this.panelMainContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "CirculationForm";
            this.Text = "Circulation";
            this.Load += new System.EventHandler(this.CirculationForm_Load);
            this.panelMainContainer.ResumeLayout(false);
            this.panelBottomSection.ResumeLayout(false);
            this.panelGuidelines.ResumeLayout(false);
            this.panelGuidelines.PerformLayout();
            this.panelCheckoutForm.ResumeLayout(false);
            this.panelCheckoutForm.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabCheckout.ResumeLayout(false);
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
        private System.Windows.Forms.Panel panelBottomSection;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabCheckout;
        private System.Windows.Forms.TabPage tabReturn;
        private System.Windows.Forms.TabPage tabRenewal;
        private System.Windows.Forms.TabPage tabHistory;
        private System.Windows.Forms.Panel panelCheckoutForm;
        private System.Windows.Forms.Label lblCheckoutTitle;
        private System.Windows.Forms.Label lblCheckoutSubtitle;
        private System.Windows.Forms.Label lblMemberID;
        private System.Windows.Forms.TextBox txtMemberID;
        private System.Windows.Forms.Label lblBookISBN;
        private System.Windows.Forms.TextBox txtBookISBN;
        private System.Windows.Forms.Label lblDueDate;
        private System.Windows.Forms.Button btnProcessCheckout;
        private System.Windows.Forms.Panel panelGuidelines;
        private System.Windows.Forms.Label lblGuidelinesTitle;
        private System.Windows.Forms.Label lblStudentPeriod;
        private System.Windows.Forms.Label lblFacultyPeriod;
        private System.Windows.Forms.Label lblStaffPeriod;
        private System.Windows.Forms.Label lblGuestPeriod;
        private System.Windows.Forms.Label lblLoanPeriodsTitle;
        private System.Windows.Forms.Label lblRequirement1;
        private System.Windows.Forms.Label lblRequirement2;
        private System.Windows.Forms.Label lblRequirement3;
        private System.Windows.Forms.Label lblRequirementsTitle;
    }
}
