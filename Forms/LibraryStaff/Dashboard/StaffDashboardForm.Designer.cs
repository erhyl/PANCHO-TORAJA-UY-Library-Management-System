namespace Project5LMS.Forms.LibraryStaff.Dashboard
{
    partial class StaffDashboardForm
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
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.lblLibraryStaff = new System.Windows.Forms.Label();
            this.panelSeparator = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnInventory = new System.Windows.Forms.Button();
            this.btnFines = new System.Windows.Forms.Button();
            this.btnReservations = new System.Windows.Forms.Button();
            this.btnCirculation = new System.Windows.Forms.Button();
            this.btnCatalog = new System.Windows.Forms.Button();
            this.btnMembers = new System.Windows.Forms.Button();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.lblSubtitleSidebar = new System.Windows.Forms.Label();
            this.lblTitleSidebar = new System.Windows.Forms.Label();
            this.panelMainContent = new System.Windows.Forms.Panel();
            this.panelMainContainer = new System.Windows.Forms.Panel();
            this.panelBottomSection = new System.Windows.Forms.Panel();
            this.panelOverdueBooks = new System.Windows.Forms.Panel();
            this.panelOverdueBooksList = new System.Windows.Forms.Panel();
            this.lblOverdueBooksTitle = new System.Windows.Forms.Label();
            this.panelRecentActivity = new System.Windows.Forms.Panel();
            this.lblRecentActivityTitle = new System.Windows.Forms.Label();
            this.listViewRecentActivity = new System.Windows.Forms.ListView();
            this.panelMetricsContainer = new System.Windows.Forms.Panel();
            this.panelMetricPendingFines = new System.Windows.Forms.Panel();
            this.lblPendingFinesChange = new System.Windows.Forms.Label();
            this.lblPendingFinesValue = new System.Windows.Forms.Label();
            this.lblPendingFinesTitle = new System.Windows.Forms.Label();
            this.panelMetricActiveLoans = new System.Windows.Forms.Panel();
            this.lblActiveLoansChange = new System.Windows.Forms.Label();
            this.lblActiveLoansValue = new System.Windows.Forms.Label();
            this.lblActiveLoansTitle = new System.Windows.Forms.Label();
            this.panelMetricBooksCatalog = new System.Windows.Forms.Panel();
            this.lblBooksCatalogChange = new System.Windows.Forms.Label();
            this.lblBooksCatalogValue = new System.Windows.Forms.Label();
            this.lblBooksCatalogTitle = new System.Windows.Forms.Label();
            this.panelMetricTotalMembers = new System.Windows.Forms.Panel();
            this.lblTotalMembersChange = new System.Windows.Forms.Label();
            this.lblTotalMembersValue = new System.Windows.Forms.Label();
            this.lblTotalMembersTitle = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelSidebar.SuspendLayout();
            this.panelMainContent.SuspendLayout();
            this.panelMainContainer.SuspendLayout();
            this.panelBottomSection.SuspendLayout();
            this.panelOverdueBooks.SuspendLayout();
            this.panelOverdueBooksList.SuspendLayout();
            this.panelRecentActivity.SuspendLayout();
            this.panelMetricsContainer.SuspendLayout();
            this.panelMetricPendingFines.SuspendLayout();
            this.panelMetricActiveLoans.SuspendLayout();
            this.panelMetricBooksCatalog.SuspendLayout();
            this.panelMetricTotalMembers.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSidebar
            // 
            this.panelSidebar.BackColor = System.Drawing.Color.Maroon;
            this.panelSidebar.Controls.Add(this.btnLogout);
            this.panelSidebar.Controls.Add(this.lblLibraryStaff);
            this.panelSidebar.Controls.Add(this.panelSeparator);
            this.panelSidebar.Controls.Add(this.btnSearch);
            this.panelSidebar.Controls.Add(this.btnInventory);
            this.panelSidebar.Controls.Add(this.btnFines);
            this.panelSidebar.Controls.Add(this.btnReservations);
            this.panelSidebar.Controls.Add(this.btnCirculation);
            this.panelSidebar.Controls.Add(this.btnCatalog);
            this.panelSidebar.Controls.Add(this.btnMembers);
            this.panelSidebar.Controls.Add(this.btnDashboard);
            this.panelSidebar.Controls.Add(this.lblSubtitleSidebar);
            this.panelSidebar.Controls.Add(this.lblTitleSidebar);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 0);
            this.panelSidebar.Margin = new System.Windows.Forms.Padding(4);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Padding = new System.Windows.Forms.Padding(20, 30, 20, 30);
            this.panelSidebar.Size = new System.Drawing.Size(280, 985);
            this.panelSidebar.TabIndex = 0;
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.Maroon;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(20, 920);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(4);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(240, 45);
            this.btnLogout.TabIndex = 12;
            this.btnLogout.Text = "🔓 Logout";
            this.btnLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // lblLibraryStaff
            // 
            this.lblLibraryStaff.AutoSize = true;
            this.lblLibraryStaff.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLibraryStaff.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblLibraryStaff.Location = new System.Drawing.Point(20, 850);
            this.lblLibraryStaff.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLibraryStaff.Name = "lblLibraryStaff";
            this.lblLibraryStaff.Size = new System.Drawing.Size(99, 23);
            this.lblLibraryStaff.TabIndex = 11;
            this.lblLibraryStaff.Text = "Library Staff";
            // 
            // panelSeparator
            // 
            this.panelSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panelSeparator.Location = new System.Drawing.Point(20, 830);
            this.panelSeparator.Margin = new System.Windows.Forms.Padding(4);
            this.panelSeparator.Name = "panelSeparator";
            this.panelSeparator.Size = new System.Drawing.Size(240, 1);
            this.panelSeparator.TabIndex = 10;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Maroon;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(20, 550);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(240, 50);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "🔍 Search";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnInventory
            // 
            this.btnInventory.BackColor = System.Drawing.Color.Maroon;
            this.btnInventory.FlatAppearance.BorderSize = 0;
            this.btnInventory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInventory.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInventory.ForeColor = System.Drawing.Color.White;
            this.btnInventory.Location = new System.Drawing.Point(20, 500);
            this.btnInventory.Margin = new System.Windows.Forms.Padding(4);
            this.btnInventory.Name = "btnInventory";
            this.btnInventory.Size = new System.Drawing.Size(240, 50);
            this.btnInventory.TabIndex = 8;
            this.btnInventory.Text = "📦 Inventory";
            this.btnInventory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInventory.UseVisualStyleBackColor = false;
            this.btnInventory.Click += new System.EventHandler(this.btnInventory_Click);
            // 
            // btnFines
            // 
            this.btnFines.BackColor = System.Drawing.Color.Maroon;
            this.btnFines.FlatAppearance.BorderSize = 0;
            this.btnFines.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFines.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFines.ForeColor = System.Drawing.Color.White;
            this.btnFines.Location = new System.Drawing.Point(20, 450);
            this.btnFines.Margin = new System.Windows.Forms.Padding(4);
            this.btnFines.Name = "btnFines";
            this.btnFines.Size = new System.Drawing.Size(240, 50);
            this.btnFines.TabIndex = 7;
            this.btnFines.Text = "💰 Fines";
            this.btnFines.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFines.UseVisualStyleBackColor = false;
            this.btnFines.Click += new System.EventHandler(this.btnFines_Click);
            // 
            // btnReservations
            // 
            this.btnReservations.BackColor = System.Drawing.Color.Maroon;
            this.btnReservations.FlatAppearance.BorderSize = 0;
            this.btnReservations.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReservations.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReservations.ForeColor = System.Drawing.Color.White;
            this.btnReservations.Location = new System.Drawing.Point(20, 400);
            this.btnReservations.Margin = new System.Windows.Forms.Padding(4);
            this.btnReservations.Name = "btnReservations";
            this.btnReservations.Size = new System.Drawing.Size(240, 50);
            this.btnReservations.TabIndex = 6;
            this.btnReservations.Text = "📅 Reservations";
            this.btnReservations.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReservations.UseVisualStyleBackColor = false;
            this.btnReservations.Click += new System.EventHandler(this.btnReservations_Click);
            // 
            // btnCirculation
            // 
            this.btnCirculation.BackColor = System.Drawing.Color.Maroon;
            this.btnCirculation.FlatAppearance.BorderSize = 0;
            this.btnCirculation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCirculation.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCirculation.ForeColor = System.Drawing.Color.White;
            this.btnCirculation.Location = new System.Drawing.Point(20, 350);
            this.btnCirculation.Margin = new System.Windows.Forms.Padding(4);
            this.btnCirculation.Name = "btnCirculation";
            this.btnCirculation.Size = new System.Drawing.Size(240, 50);
            this.btnCirculation.TabIndex = 5;
            this.btnCirculation.Text = "🔄 Circulation";
            this.btnCirculation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCirculation.UseVisualStyleBackColor = false;
            this.btnCirculation.Click += new System.EventHandler(this.btnCirculation_Click);
            // 
            // btnCatalog
            // 
            this.btnCatalog.BackColor = System.Drawing.Color.Maroon;
            this.btnCatalog.FlatAppearance.BorderSize = 0;
            this.btnCatalog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCatalog.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCatalog.ForeColor = System.Drawing.Color.White;
            this.btnCatalog.Location = new System.Drawing.Point(20, 300);
            this.btnCatalog.Margin = new System.Windows.Forms.Padding(4);
            this.btnCatalog.Name = "btnCatalog";
            this.btnCatalog.Size = new System.Drawing.Size(240, 50);
            this.btnCatalog.TabIndex = 4;
            this.btnCatalog.Text = "📖 Catalog";
            this.btnCatalog.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCatalog.UseVisualStyleBackColor = false;
            this.btnCatalog.Click += new System.EventHandler(this.btnCatalog_Click);
            // 
            // btnMembers
            // 
            this.btnMembers.BackColor = System.Drawing.Color.Maroon;
            this.btnMembers.FlatAppearance.BorderSize = 0;
            this.btnMembers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMembers.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMembers.ForeColor = System.Drawing.Color.White;
            this.btnMembers.Location = new System.Drawing.Point(20, 250);
            this.btnMembers.Margin = new System.Windows.Forms.Padding(4);
            this.btnMembers.Name = "btnMembers";
            this.btnMembers.Size = new System.Drawing.Size(240, 50);
            this.btnMembers.TabIndex = 3;
            this.btnMembers.Text = "👥 Members";
            this.btnMembers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMembers.UseVisualStyleBackColor = false;
            this.btnMembers.Click += new System.EventHandler(this.btnMembers_Click);
            // 
            // btnDashboard
            // 
            this.btnDashboard.BackColor = System.Drawing.Color.Maroon;
            this.btnDashboard.FlatAppearance.BorderSize = 0;
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDashboard.ForeColor = System.Drawing.Color.White;
            this.btnDashboard.Location = new System.Drawing.Point(20, 200);
            this.btnDashboard.Margin = new System.Windows.Forms.Padding(4);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(240, 50);
            this.btnDashboard.TabIndex = 2;
            this.btnDashboard.Text = "🏠 Dashboard";
            this.btnDashboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDashboard.UseVisualStyleBackColor = false;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // lblSubtitleSidebar
            // 
            this.lblSubtitleSidebar.AutoSize = true;
            this.lblSubtitleSidebar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitleSidebar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblSubtitleSidebar.Location = new System.Drawing.Point(24, 71);
            this.lblSubtitleSidebar.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubtitleSidebar.Name = "lblSubtitleSidebar";
            this.lblSubtitleSidebar.Size = new System.Drawing.Size(171, 23);
            this.lblSubtitleSidebar.TabIndex = 1;
            this.lblSubtitleSidebar.Text = "Management System";
            this.lblSubtitleSidebar.Click += new System.EventHandler(this.lblSubtitleSidebar_Click);
            // 
            // lblTitleSidebar
            // 
            this.lblTitleSidebar.AutoSize = true;
            this.lblTitleSidebar.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleSidebar.ForeColor = System.Drawing.Color.White;
            this.lblTitleSidebar.Location = new System.Drawing.Point(20, 30);
            this.lblTitleSidebar.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitleSidebar.Name = "lblTitleSidebar";
            this.lblTitleSidebar.Size = new System.Drawing.Size(185, 41);
            this.lblTitleSidebar.TabIndex = 0;
            this.lblTitleSidebar.Text = "UM Library ";
            // 
            // panelMainContent
            // 
            this.panelMainContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelMainContent.Controls.Add(this.panelMainContainer);
            this.panelMainContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainContent.Location = new System.Drawing.Point(280, 0);
            this.panelMainContent.Margin = new System.Windows.Forms.Padding(0);
            this.panelMainContent.Name = "panelMainContent";
            this.panelMainContent.Size = new System.Drawing.Size(1320, 985);
            this.panelMainContent.TabIndex = 1;
            // 
            // panelMainContainer
            // 
            this.panelMainContainer.AutoScroll = true;
            this.panelMainContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelMainContainer.Controls.Add(this.panelBottomSection);
            this.panelMainContainer.Controls.Add(this.panelMetricsContainer);
            this.panelMainContainer.Controls.Add(this.panelHeader);
            this.panelMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainContainer.Location = new System.Drawing.Point(0, 0);
            this.panelMainContainer.Margin = new System.Windows.Forms.Padding(0);
            this.panelMainContainer.Name = "panelMainContainer";
            this.panelMainContainer.Padding = new System.Windows.Forms.Padding(20);
            this.panelMainContainer.Size = new System.Drawing.Size(1320, 985);
            this.panelMainContainer.TabIndex = 0;
            // 
            // panelBottomSection
            // 
            this.panelBottomSection.Controls.Add(this.panelOverdueBooks);
            this.panelBottomSection.Controls.Add(this.panelRecentActivity);
            this.panelBottomSection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBottomSection.Location = new System.Drawing.Point(20, 337);
            this.panelBottomSection.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.panelBottomSection.Name = "panelBottomSection";
            this.panelBottomSection.Size = new System.Drawing.Size(1280, 628);
            this.panelBottomSection.TabIndex = 2;
            // 
            // panelOverdueBooks
            // 
            this.panelOverdueBooks.BackColor = System.Drawing.Color.White;
            this.panelOverdueBooks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelOverdueBooks.Controls.Add(this.panelOverdueBooksList);
            this.panelOverdueBooks.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelOverdueBooks.Location = new System.Drawing.Point(645, 0);
            this.panelOverdueBooks.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.panelOverdueBooks.Name = "panelOverdueBooks";
            this.panelOverdueBooks.Padding = new System.Windows.Forms.Padding(20);
            this.panelOverdueBooks.Size = new System.Drawing.Size(635, 628);
            this.panelOverdueBooks.TabIndex = 1;
            // 
            // panelOverdueBooksList
            // 
            this.panelOverdueBooksList.AutoScroll = true;
            this.panelOverdueBooksList.Controls.Add(this.lblOverdueBooksTitle);
            this.panelOverdueBooksList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOverdueBooksList.Location = new System.Drawing.Point(20, 20);
            this.panelOverdueBooksList.Margin = new System.Windows.Forms.Padding(4);
            this.panelOverdueBooksList.Name = "panelOverdueBooksList";
            this.panelOverdueBooksList.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.panelOverdueBooksList.Size = new System.Drawing.Size(593, 586);
            this.panelOverdueBooksList.TabIndex = 0;
            // 
            // lblOverdueBooksTitle
            // 
            this.lblOverdueBooksTitle.AutoSize = true;
            this.lblOverdueBooksTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOverdueBooksTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblOverdueBooksTitle.Location = new System.Drawing.Point(4, 7);
            this.lblOverdueBooksTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOverdueBooksTitle.Name = "lblOverdueBooksTitle";
            this.lblOverdueBooksTitle.Size = new System.Drawing.Size(228, 32);
            this.lblOverdueBooksTitle.TabIndex = 1;
            this.lblOverdueBooksTitle.Text = "⚠️ Overdue Books";
            // 
            // panelRecentActivity
            // 
            this.panelRecentActivity.BackColor = System.Drawing.Color.White;
            this.panelRecentActivity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelRecentActivity.Controls.Add(this.lblRecentActivityTitle);
            this.panelRecentActivity.Controls.Add(this.listViewRecentActivity);
            this.panelRecentActivity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRecentActivity.Location = new System.Drawing.Point(0, 0);
            this.panelRecentActivity.Margin = new System.Windows.Forms.Padding(0);
            this.panelRecentActivity.Name = "panelRecentActivity";
            this.panelRecentActivity.Padding = new System.Windows.Forms.Padding(20);
            this.panelRecentActivity.Size = new System.Drawing.Size(1280, 628);
            this.panelRecentActivity.TabIndex = 0;
            // 
            // lblRecentActivityTitle
            // 
            this.lblRecentActivityTitle.AutoSize = true;
            this.lblRecentActivityTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecentActivityTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblRecentActivityTitle.Location = new System.Drawing.Point(25, 27);
            this.lblRecentActivityTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRecentActivityTitle.Name = "lblRecentActivityTitle";
            this.lblRecentActivityTitle.Size = new System.Drawing.Size(185, 32);
            this.lblRecentActivityTitle.TabIndex = 1;
            this.lblRecentActivityTitle.Text = "Recent Activity";
            // 
            // listViewRecentActivity
            // 
            this.listViewRecentActivity.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewRecentActivity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewRecentActivity.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewRecentActivity.FullRowSelect = true;
            this.listViewRecentActivity.HideSelection = false;
            this.listViewRecentActivity.Location = new System.Drawing.Point(20, 20);
            this.listViewRecentActivity.Margin = new System.Windows.Forms.Padding(4);
            this.listViewRecentActivity.MultiSelect = false;
            this.listViewRecentActivity.Name = "listViewRecentActivity";
            this.listViewRecentActivity.OwnerDraw = true;
            this.listViewRecentActivity.Size = new System.Drawing.Size(1238, 586);
            this.listViewRecentActivity.TabIndex = 0;
            this.listViewRecentActivity.UseCompatibleStateImageBehavior = false;
            this.listViewRecentActivity.View = System.Windows.Forms.View.Details;
            // 
            // panelMetricsContainer
            // 
            this.panelMetricsContainer.Controls.Add(this.panelMetricPendingFines);
            this.panelMetricsContainer.Controls.Add(this.panelMetricActiveLoans);
            this.panelMetricsContainer.Controls.Add(this.panelMetricBooksCatalog);
            this.panelMetricsContainer.Controls.Add(this.panelMetricTotalMembers);
            this.panelMetricsContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMetricsContainer.Location = new System.Drawing.Point(20, 117);
            this.panelMetricsContainer.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.panelMetricsContainer.Name = "panelMetricsContainer";
            this.panelMetricsContainer.Size = new System.Drawing.Size(1280, 220);
            this.panelMetricsContainer.TabIndex = 1;
            // 
            // panelMetricPendingFines
            // 
            this.panelMetricPendingFines.BackColor = System.Drawing.Color.White;
            this.panelMetricPendingFines.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMetricPendingFines.Controls.Add(this.lblPendingFinesChange);
            this.panelMetricPendingFines.Controls.Add(this.lblPendingFinesValue);
            this.panelMetricPendingFines.Controls.Add(this.lblPendingFinesTitle);
            this.panelMetricPendingFines.Location = new System.Drawing.Point(966, 0);
            this.panelMetricPendingFines.Margin = new System.Windows.Forms.Padding(4);
            this.panelMetricPendingFines.Name = "panelMetricPendingFines";
            this.panelMetricPendingFines.Padding = new System.Windows.Forms.Padding(20);
            this.panelMetricPendingFines.Size = new System.Drawing.Size(314, 200);
            this.panelMetricPendingFines.TabIndex = 3;
            // 
            // lblPendingFinesChange
            // 
            this.lblPendingFinesChange.AutoSize = true;
            this.lblPendingFinesChange.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPendingFinesChange.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.lblPendingFinesChange.Location = new System.Drawing.Point(20, 150);
            this.lblPendingFinesChange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPendingFinesChange.Name = "lblPendingFinesChange";
            this.lblPendingFinesChange.Size = new System.Drawing.Size(40, 23);
            this.lblPendingFinesChange.TabIndex = 2;
            this.lblPendingFinesChange.Text = "-5%";
            // 
            // lblPendingFinesValue
            // 
            this.lblPendingFinesValue.AutoSize = true;
            this.lblPendingFinesValue.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPendingFinesValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPendingFinesValue.Location = new System.Drawing.Point(20, 80);
            this.lblPendingFinesValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPendingFinesValue.Name = "lblPendingFinesValue";
            this.lblPendingFinesValue.Size = new System.Drawing.Size(175, 62);
            this.lblPendingFinesValue.TabIndex = 1;
            this.lblPendingFinesValue.Text = "$1,240";
            // 
            // lblPendingFinesTitle
            // 
            this.lblPendingFinesTitle.AutoSize = true;
            this.lblPendingFinesTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPendingFinesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblPendingFinesTitle.Location = new System.Drawing.Point(20, 20);
            this.lblPendingFinesTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPendingFinesTitle.Name = "lblPendingFinesTitle";
            this.lblPendingFinesTitle.Size = new System.Drawing.Size(176, 28);
            this.lblPendingFinesTitle.TabIndex = 0;
            this.lblPendingFinesTitle.Text = "💰 Pending Fines";
            // 
            // panelMetricActiveLoans
            // 
            this.panelMetricActiveLoans.BackColor = System.Drawing.Color.White;
            this.panelMetricActiveLoans.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMetricActiveLoans.Controls.Add(this.lblActiveLoansChange);
            this.panelMetricActiveLoans.Controls.Add(this.lblActiveLoansValue);
            this.panelMetricActiveLoans.Controls.Add(this.lblActiveLoansTitle);
            this.panelMetricActiveLoans.Location = new System.Drawing.Point(644, 0);
            this.panelMetricActiveLoans.Margin = new System.Windows.Forms.Padding(4);
            this.panelMetricActiveLoans.Name = "panelMetricActiveLoans";
            this.panelMetricActiveLoans.Padding = new System.Windows.Forms.Padding(20);
            this.panelMetricActiveLoans.Size = new System.Drawing.Size(314, 200);
            this.panelMetricActiveLoans.TabIndex = 2;
            // 
            // lblActiveLoansChange
            // 
            this.lblActiveLoansChange.AutoSize = true;
            this.lblActiveLoansChange.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActiveLoansChange.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.lblActiveLoansChange.Location = new System.Drawing.Point(20, 150);
            this.lblActiveLoansChange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblActiveLoansChange.Name = "lblActiveLoansChange";
            this.lblActiveLoansChange.Size = new System.Drawing.Size(45, 23);
            this.lblActiveLoansChange.TabIndex = 2;
            this.lblActiveLoansChange.Text = "+8%";
            // 
            // lblActiveLoansValue
            // 
            this.lblActiveLoansValue.AutoSize = true;
            this.lblActiveLoansValue.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActiveLoansValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblActiveLoansValue.Location = new System.Drawing.Point(20, 80);
            this.lblActiveLoansValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblActiveLoansValue.Name = "lblActiveLoansValue";
            this.lblActiveLoansValue.Size = new System.Drawing.Size(108, 62);
            this.lblActiveLoansValue.TabIndex = 1;
            this.lblActiveLoansValue.Text = "342";
            // 
            // lblActiveLoansTitle
            // 
            this.lblActiveLoansTitle.AutoSize = true;
            this.lblActiveLoansTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActiveLoansTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblActiveLoansTitle.Location = new System.Drawing.Point(20, 20);
            this.lblActiveLoansTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblActiveLoansTitle.Name = "lblActiveLoansTitle";
            this.lblActiveLoansTitle.Size = new System.Drawing.Size(166, 28);
            this.lblActiveLoansTitle.TabIndex = 0;
            this.lblActiveLoansTitle.Text = "📈 Active Loans";
            // 
            // panelMetricBooksCatalog
            // 
            this.panelMetricBooksCatalog.BackColor = System.Drawing.Color.White;
            this.panelMetricBooksCatalog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMetricBooksCatalog.Controls.Add(this.lblBooksCatalogChange);
            this.panelMetricBooksCatalog.Controls.Add(this.lblBooksCatalogValue);
            this.panelMetricBooksCatalog.Controls.Add(this.lblBooksCatalogTitle);
            this.panelMetricBooksCatalog.Location = new System.Drawing.Point(322, 0);
            this.panelMetricBooksCatalog.Margin = new System.Windows.Forms.Padding(4);
            this.panelMetricBooksCatalog.Name = "panelMetricBooksCatalog";
            this.panelMetricBooksCatalog.Padding = new System.Windows.Forms.Padding(20);
            this.panelMetricBooksCatalog.Size = new System.Drawing.Size(314, 200);
            this.panelMetricBooksCatalog.TabIndex = 1;
            // 
            // lblBooksCatalogChange
            // 
            this.lblBooksCatalogChange.AutoSize = true;
            this.lblBooksCatalogChange.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBooksCatalogChange.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.lblBooksCatalogChange.Location = new System.Drawing.Point(20, 150);
            this.lblBooksCatalogChange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBooksCatalogChange.Name = "lblBooksCatalogChange";
            this.lblBooksCatalogChange.Size = new System.Drawing.Size(45, 23);
            this.lblBooksCatalogChange.TabIndex = 2;
            this.lblBooksCatalogChange.Text = "+3%";
            // 
            // lblBooksCatalogValue
            // 
            this.lblBooksCatalogValue.AutoSize = true;
            this.lblBooksCatalogValue.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBooksCatalogValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblBooksCatalogValue.Location = new System.Drawing.Point(20, 80);
            this.lblBooksCatalogValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBooksCatalogValue.Name = "lblBooksCatalogValue";
            this.lblBooksCatalogValue.Size = new System.Drawing.Size(148, 62);
            this.lblBooksCatalogValue.TabIndex = 1;
            this.lblBooksCatalogValue.Text = "8,932";
            // 
            // lblBooksCatalogTitle
            // 
            this.lblBooksCatalogTitle.AutoSize = true;
            this.lblBooksCatalogTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBooksCatalogTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblBooksCatalogTitle.Location = new System.Drawing.Point(20, 20);
            this.lblBooksCatalogTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBooksCatalogTitle.Name = "lblBooksCatalogTitle";
            this.lblBooksCatalogTitle.Size = new System.Drawing.Size(199, 28);
            this.lblBooksCatalogTitle.TabIndex = 0;
            this.lblBooksCatalogTitle.Text = "📖Books in Catalog";
            // 
            // panelMetricTotalMembers
            // 
            this.panelMetricTotalMembers.BackColor = System.Drawing.Color.White;
            this.panelMetricTotalMembers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMetricTotalMembers.Controls.Add(this.lblTotalMembersChange);
            this.panelMetricTotalMembers.Controls.Add(this.lblTotalMembersValue);
            this.panelMetricTotalMembers.Controls.Add(this.lblTotalMembersTitle);
            this.panelMetricTotalMembers.Location = new System.Drawing.Point(0, 0);
            this.panelMetricTotalMembers.Margin = new System.Windows.Forms.Padding(4);
            this.panelMetricTotalMembers.Name = "panelMetricTotalMembers";
            this.panelMetricTotalMembers.Padding = new System.Windows.Forms.Padding(20);
            this.panelMetricTotalMembers.Size = new System.Drawing.Size(314, 200);
            this.panelMetricTotalMembers.TabIndex = 0;
            // 
            // lblTotalMembersChange
            // 
            this.lblTotalMembersChange.AutoSize = true;
            this.lblTotalMembersChange.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalMembersChange.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.lblTotalMembersChange.Location = new System.Drawing.Point(20, 150);
            this.lblTotalMembersChange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalMembersChange.Name = "lblTotalMembersChange";
            this.lblTotalMembersChange.Size = new System.Drawing.Size(54, 23);
            this.lblTotalMembersChange.TabIndex = 2;
            this.lblTotalMembersChange.Text = "+12%";
            // 
            // lblTotalMembersValue
            // 
            this.lblTotalMembersValue.AutoSize = true;
            this.lblTotalMembersValue.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalMembersValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTotalMembersValue.Location = new System.Drawing.Point(20, 80);
            this.lblTotalMembersValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalMembersValue.Name = "lblTotalMembersValue";
            this.lblTotalMembersValue.Size = new System.Drawing.Size(148, 62);
            this.lblTotalMembersValue.TabIndex = 1;
            this.lblTotalMembersValue.Text = "1,247";
            // 
            // lblTotalMembersTitle
            // 
            this.lblTotalMembersTitle.AutoSize = true;
            this.lblTotalMembersTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalMembersTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblTotalMembersTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTotalMembersTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalMembersTitle.Name = "lblTotalMembersTitle";
            this.lblTotalMembersTitle.Size = new System.Drawing.Size(187, 28);
            this.lblTotalMembersTitle.TabIndex = 0;
            this.lblTotalMembersTitle.Text = "👥 Total Members";
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.lblSubtitle);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(20, 20);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1280, 97);
            this.panelHeader.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblSubtitle.Location = new System.Drawing.Point(17, 64);
            this.lblSubtitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(333, 23);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Monitor library operations and key metrics";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitle.Location = new System.Drawing.Point(12, 10);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(229, 54);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Dashboard";
            // 
            // StaffDashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1600, 985);
            this.Controls.Add(this.panelMainContent);
            this.Controls.Add(this.panelSidebar);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "StaffDashboardForm";
            this.Text = "LibraryMS - Management System";
            this.Load += new System.EventHandler(this.StaffDashboardForm_Load);
            this.panelSidebar.ResumeLayout(false);
            this.panelSidebar.PerformLayout();
            this.panelMainContent.ResumeLayout(false);
            this.panelMainContainer.ResumeLayout(false);
            this.panelBottomSection.ResumeLayout(false);
            this.panelOverdueBooks.ResumeLayout(false);
            this.panelOverdueBooksList.ResumeLayout(false);
            this.panelOverdueBooksList.PerformLayout();
            this.panelRecentActivity.ResumeLayout(false);
            this.panelRecentActivity.PerformLayout();
            this.panelMetricsContainer.ResumeLayout(false);
            this.panelMetricPendingFines.ResumeLayout(false);
            this.panelMetricPendingFines.PerformLayout();
            this.panelMetricActiveLoans.ResumeLayout(false);
            this.panelMetricActiveLoans.PerformLayout();
            this.panelMetricBooksCatalog.ResumeLayout(false);
            this.panelMetricBooksCatalog.PerformLayout();
            this.panelMetricTotalMembers.ResumeLayout(false);
            this.panelMetricTotalMembers.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Label lblTitleSidebar;
        private System.Windows.Forms.Label lblSubtitleSidebar;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button btnMembers;
        private System.Windows.Forms.Button btnCatalog;
        private System.Windows.Forms.Button btnCirculation;
        private System.Windows.Forms.Button btnReservations;
        private System.Windows.Forms.Button btnFines;
        private System.Windows.Forms.Button btnInventory;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Panel panelSeparator;
        private System.Windows.Forms.Label lblLibraryStaff;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Panel panelMainContent;
        private System.Windows.Forms.Panel panelMainContainer;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel panelMetricsContainer;
        private System.Windows.Forms.Panel panelMetricTotalMembers;
        private System.Windows.Forms.Label lblTotalMembersTitle;
        private System.Windows.Forms.Label lblTotalMembersValue;
        private System.Windows.Forms.Label lblTotalMembersChange;
        private System.Windows.Forms.Panel panelMetricBooksCatalog;
        private System.Windows.Forms.Label lblBooksCatalogTitle;
        private System.Windows.Forms.Label lblBooksCatalogValue;
        private System.Windows.Forms.Label lblBooksCatalogChange;
        private System.Windows.Forms.Panel panelMetricActiveLoans;
        private System.Windows.Forms.Label lblActiveLoansTitle;
        private System.Windows.Forms.Label lblActiveLoansValue;
        private System.Windows.Forms.Label lblActiveLoansChange;
        private System.Windows.Forms.Panel panelMetricPendingFines;
        private System.Windows.Forms.Label lblPendingFinesTitle;
        private System.Windows.Forms.Label lblPendingFinesValue;
        private System.Windows.Forms.Label lblPendingFinesChange;
        private System.Windows.Forms.Panel panelBottomSection;
        private System.Windows.Forms.Panel panelRecentActivity;
        private System.Windows.Forms.Label lblRecentActivityTitle;
        private System.Windows.Forms.ListView listViewRecentActivity;
        private System.Windows.Forms.Panel panelOverdueBooks;
        private System.Windows.Forms.Label lblOverdueBooksTitle;
        private System.Windows.Forms.Panel panelOverdueBooksList;
    }
}
