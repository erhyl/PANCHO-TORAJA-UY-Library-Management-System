namespace Project5LMS.Forms.Admin.Search
{
    partial class AdminSearchForm
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
            this.panelCards = new System.Windows.Forms.Panel();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.cardPopularBooks = new System.Windows.Forms.Panel();
            this.lblPopularLink = new System.Windows.Forms.LinkLabel();
            this.lblPopularDescription = new System.Windows.Forms.Label();
            this.lblPopularTitle = new System.Windows.Forms.Label();
            this.cardNewArrivals = new System.Windows.Forms.Panel();
            this.lblNewLink = new System.Windows.Forms.LinkLabel();
            this.lblNewDescription = new System.Windows.Forms.Label();
            this.lblNewTitle = new System.Windows.Forms.Label();
            this.cmbSearchIn = new System.Windows.Forms.ComboBox();
            this.cardBrowseCategory = new System.Windows.Forms.Panel();
            this.lblCategoryLink = new System.Windows.Forms.LinkLabel();
            this.lblCategoryDescription = new System.Windows.Forms.Label();
            this.lblCategoryTitle = new System.Windows.Forms.Label();
            this.panelSearchForm = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.panelFilters = new System.Windows.Forms.Panel();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblSearchIn = new System.Windows.Forms.Label();
            this.panelSearchInput = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.cardBrowseAuthor = new System.Windows.Forms.Panel();
            this.lblAuthorLink = new System.Windows.Forms.LinkLabel();
            this.lblAuthorDescription = new System.Windows.Forms.Label();
            this.lblAuthorTitle = new System.Windows.Forms.Label();
            this.panelMainContainer.SuspendLayout();
            this.panelCards.SuspendLayout();
            this.cardPopularBooks.SuspendLayout();
            this.cardNewArrivals.SuspendLayout();
            this.cardBrowseCategory.SuspendLayout();
            this.panelSearchForm.SuspendLayout();
            this.panelFilters.SuspendLayout();
            this.panelSearchInput.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.cardBrowseAuthor.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMainContainer
            // 
            this.panelMainContainer.AutoScroll = true;
            this.panelMainContainer.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelMainContainer.Controls.Add(this.panelCards);
            this.panelMainContainer.Controls.Add(this.panelSearchForm);
            this.panelMainContainer.Controls.Add(this.panelHeader);
            this.panelMainContainer.Location = new System.Drawing.Point(0, 0);
            this.panelMainContainer.Name = "panelMainContainer";
            this.panelMainContainer.Padding = new System.Windows.Forms.Padding(24, 24, 24, 24);
            this.panelMainContainer.Size = new System.Drawing.Size(1233, 800);
            this.panelMainContainer.TabIndex = 0;
            // 
            // panelCards
            // 
            this.panelCards.Controls.Add(this.cmbCategory);
            this.panelCards.Controls.Add(this.panelFilters);
            this.panelCards.Controls.Add(this.cardPopularBooks);
            this.panelCards.Controls.Add(this.cardNewArrivals);
            this.panelCards.Controls.Add(this.cmbSearchIn);
            this.panelCards.Controls.Add(this.cardBrowseCategory);
            this.panelCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCards.Location = new System.Drawing.Point(24, 92);
            this.panelCards.Name = "panelCards";
            this.panelCards.Size = new System.Drawing.Size(1196, 162);
            this.panelCards.TabIndex = 2;
            // 
            // cmbCategory
            // 
            this.cmbCategory.BackColor = System.Drawing.Color.White;
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(956, 130);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(226, 25);
            this.cmbCategory.TabIndex = 3;
            this.cmbCategory.SelectedIndexChanged += new System.EventHandler(this.cmbCategory_SelectedIndexChanged);
            // 
            // cardPopularBooks
            // 
            this.cardPopularBooks.BackColor = System.Drawing.Color.White;
            this.cardPopularBooks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardPopularBooks.Controls.Add(this.lblPopularLink);
            this.cardPopularBooks.Controls.Add(this.lblPopularDescription);
            this.cardPopularBooks.Controls.Add(this.lblPopularTitle);
            this.cardPopularBooks.Location = new System.Drawing.Point(768, 0);
            this.cardPopularBooks.Name = "cardPopularBooks";
            this.cardPopularBooks.Padding = new System.Windows.Forms.Padding(15, 16, 15, 16);
            this.cardPopularBooks.Size = new System.Drawing.Size(417, 124);
            this.cardPopularBooks.TabIndex = 2;
            // 
            // lblPopularLink
            // 
            this.lblPopularLink.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.lblPopularLink.AutoSize = true;
            this.lblPopularLink.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPopularLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.lblPopularLink.Location = new System.Drawing.Point(15, 80);
            this.lblPopularLink.Name = "lblPopularLink";
            this.lblPopularLink.Size = new System.Drawing.Size(104, 19);
            this.lblPopularLink.TabIndex = 3;
            this.lblPopularLink.TabStop = true;
            this.lblPopularLink.Text = "View Popular ??";
            this.lblPopularLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblPopularLink_LinkClicked);
            // 
            // lblPopularDescription
            // 
            this.lblPopularDescription.AutoSize = true;
            this.lblPopularDescription.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPopularDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblPopularDescription.Location = new System.Drawing.Point(15, 50);
            this.lblPopularDescription.Name = "lblPopularDescription";
            this.lblPopularDescription.Size = new System.Drawing.Size(208, 19);
            this.lblPopularDescription.TabIndex = 2;
            this.lblPopularDescription.Text = "Most borrowed titles this month";
            // 
            // lblPopularTitle
            // 
            this.lblPopularTitle.AutoSize = true;
            this.lblPopularTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPopularTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPopularTitle.Location = new System.Drawing.Point(14, 21);
            this.lblPopularTitle.Name = "lblPopularTitle";
            this.lblPopularTitle.Size = new System.Drawing.Size(120, 21);
            this.lblPopularTitle.TabIndex = 1;
            this.lblPopularTitle.Text = "Popular Books";
            // 
            // cardNewArrivals
            // 
            this.cardNewArrivals.BackColor = System.Drawing.Color.White;
            this.cardNewArrivals.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardNewArrivals.Controls.Add(this.lblNewLink);
            this.cardNewArrivals.Controls.Add(this.lblNewDescription);
            this.cardNewArrivals.Controls.Add(this.lblNewTitle);
            this.cardNewArrivals.Location = new System.Drawing.Point(384, 0);
            this.cardNewArrivals.Name = "cardNewArrivals";
            this.cardNewArrivals.Padding = new System.Windows.Forms.Padding(15, 16, 15, 16);
            this.cardNewArrivals.Size = new System.Drawing.Size(384, 124);
            this.cardNewArrivals.TabIndex = 1;
            // 
            // lblNewLink
            // 
            this.lblNewLink.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.lblNewLink.AutoSize = true;
            this.lblNewLink.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.lblNewLink.Location = new System.Drawing.Point(18, 80);
            this.lblNewLink.Name = "lblNewLink";
            this.lblNewLink.Size = new System.Drawing.Size(114, 19);
            this.lblNewLink.TabIndex = 3;
            this.lblNewLink.TabStop = true;
            this.lblNewLink.Text = "View New Books ";
            this.lblNewLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblNewLink_LinkClicked);
            // 
            // lblNewDescription
            // 
            this.lblNewDescription.AutoSize = true;
            this.lblNewDescription.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblNewDescription.Location = new System.Drawing.Point(15, 50);
            this.lblNewDescription.Name = "lblNewDescription";
            this.lblNewDescription.Size = new System.Drawing.Size(193, 19);
            this.lblNewDescription.TabIndex = 2;
            this.lblNewDescription.Text = "Check out our latest additions";
            // 
            // lblNewTitle
            // 
            this.lblNewTitle.AutoSize = true;
            this.lblNewTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblNewTitle.Location = new System.Drawing.Point(14, 21);
            this.lblNewTitle.Name = "lblNewTitle";
            this.lblNewTitle.Size = new System.Drawing.Size(107, 21);
            this.lblNewTitle.TabIndex = 1;
            this.lblNewTitle.Text = "New Arrivals";
            // 
            // cmbSearchIn
            // 
            this.cmbSearchIn.BackColor = System.Drawing.Color.White;
            this.cmbSearchIn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchIn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSearchIn.FormattingEnabled = true;
            this.cmbSearchIn.Location = new System.Drawing.Point(631, 130);
            this.cmbSearchIn.Name = "cmbSearchIn";
            this.cmbSearchIn.Size = new System.Drawing.Size(319, 25);
            this.cmbSearchIn.TabIndex = 1;
            // 
            // cardBrowseCategory
            // 
            this.cardBrowseCategory.BackColor = System.Drawing.Color.White;
            this.cardBrowseCategory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardBrowseCategory.Controls.Add(this.lblCategoryLink);
            this.cardBrowseCategory.Controls.Add(this.lblCategoryDescription);
            this.cardBrowseCategory.Controls.Add(this.lblCategoryTitle);
            this.cardBrowseCategory.Location = new System.Drawing.Point(0, 0);
            this.cardBrowseCategory.Name = "cardBrowseCategory";
            this.cardBrowseCategory.Padding = new System.Windows.Forms.Padding(15, 16, 15, 16);
            this.cardBrowseCategory.Size = new System.Drawing.Size(384, 124);
            this.cardBrowseCategory.TabIndex = 0;
            // 
            // lblCategoryLink
            // 
            this.lblCategoryLink.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.lblCategoryLink.AutoSize = true;
            this.lblCategoryLink.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategoryLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.lblCategoryLink.Location = new System.Drawing.Point(22, 80);
            this.lblCategoryLink.Name = "lblCategoryLink";
            this.lblCategoryLink.Size = new System.Drawing.Size(126, 19);
            this.lblCategoryLink.TabIndex = 3;
            this.lblCategoryLink.TabStop = true;
            this.lblCategoryLink.Text = "View All Categories";
            this.lblCategoryLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblCategoryLink_LinkClicked);
            // 
            // lblCategoryDescription
            // 
            this.lblCategoryDescription.AutoSize = true;
            this.lblCategoryDescription.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategoryDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblCategoryDescription.Location = new System.Drawing.Point(18, 50);
            this.lblCategoryDescription.Name = "lblCategoryDescription";
            this.lblCategoryDescription.Size = new System.Drawing.Size(234, 19);
            this.lblCategoryDescription.TabIndex = 2;
            this.lblCategoryDescription.Text = "Explore books by different categories";
            // 
            // lblCategoryTitle
            // 
            this.lblCategoryTitle.AutoSize = true;
            this.lblCategoryTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategoryTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCategoryTitle.Location = new System.Drawing.Point(18, 16);
            this.lblCategoryTitle.Name = "lblCategoryTitle";
            this.lblCategoryTitle.Size = new System.Drawing.Size(162, 21);
            this.lblCategoryTitle.TabIndex = 1;
            this.lblCategoryTitle.Text = "Browse by Category";
            // 
            // panelSearchForm
            // 
            this.panelSearchForm.BackColor = System.Drawing.Color.White;
            this.panelSearchForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSearchForm.Controls.Add(this.btnSearch);
            this.panelSearchForm.Controls.Add(this.panelSearchInput);
            this.panelSearchForm.Location = new System.Drawing.Point(35, 278);
            this.panelSearchForm.Name = "panelSearchForm";
            this.panelSearchForm.Padding = new System.Windows.Forms.Padding(22, 24, 22, 24);
            this.panelSearchForm.Size = new System.Drawing.Size(1185, 183);
            this.panelSearchForm.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Maroon;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(18, 130);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(1140, 38);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "🔍︎  Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panelFilters
            // 
            this.panelFilters.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelFilters.Controls.Add(this.lblCategory);
            this.panelFilters.Controls.Add(this.lblSearchIn);
            this.panelFilters.Location = new System.Drawing.Point(11, 138);
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Size = new System.Drawing.Size(128, 42);
            this.panelFilters.TabIndex = 1;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCategory.Location = new System.Drawing.Point(63, 5);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(65, 19);
            this.lblCategory.TabIndex = 2;
            this.lblCategory.Text = "Category";
            // 
            // lblSearchIn
            // 
            this.lblSearchIn.AutoSize = true;
            this.lblSearchIn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchIn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblSearchIn.Location = new System.Drawing.Point(3, 5);
            this.lblSearchIn.Name = "lblSearchIn";
            this.lblSearchIn.Size = new System.Drawing.Size(65, 19);
            this.lblSearchIn.TabIndex = 0;
            this.lblSearchIn.Text = "Search In";
            // 
            // panelSearchInput
            // 
            this.panelSearchInput.Controls.Add(this.txtSearch);
            this.panelSearchInput.Location = new System.Drawing.Point(22, 24);
            this.panelSearchInput.Name = "panelSearchInput";
            this.panelSearchInput.Size = new System.Drawing.Size(1107, 41);
            this.panelSearchInput.TabIndex = 0;
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.Color.Gray;
            this.txtSearch.Location = new System.Drawing.Point(0, 0);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(1107, 27);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.Text = "🔍︎  Search by title, author, ISBN, member name, or ID...";
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.lblSubtitle);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(24, 24);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1196, 68);
            this.panelHeader.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblSubtitle.Location = new System.Drawing.Point(10, 44);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(267, 19);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Search for books, resources, and members";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitle.Location = new System.Drawing.Point(3, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(119, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Search";
            // 
            // cardBrowseAuthor
            // 
            this.cardBrowseAuthor.BackColor = System.Drawing.Color.White;
            this.cardBrowseAuthor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardBrowseAuthor.Controls.Add(this.lblAuthorLink);
            this.cardBrowseAuthor.Controls.Add(this.lblAuthorDescription);
            this.cardBrowseAuthor.Controls.Add(this.lblAuthorTitle);
            this.cardBrowseAuthor.Location = new System.Drawing.Point(0, 152);
            this.cardBrowseAuthor.Margin = new System.Windows.Forms.Padding(4);
            this.cardBrowseAuthor.Name = "cardBrowseAuthor";
            this.cardBrowseAuthor.Padding = new System.Windows.Forms.Padding(20);
            this.cardBrowseAuthor.Size = new System.Drawing.Size(511, 152);
            this.cardBrowseAuthor.TabIndex = 4;
            // 
            // lblAuthorLink
            // 
            this.lblAuthorLink.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.lblAuthorLink.AutoSize = true;
            this.lblAuthorLink.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblAuthorLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.lblAuthorLink.Location = new System.Drawing.Point(40, 118);
            this.lblAuthorLink.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAuthorLink.Name = "lblAuthorLink";
            this.lblAuthorLink.Size = new System.Drawing.Size(122, 19);
            this.lblAuthorLink.TabIndex = 3;
            this.lblAuthorLink.TabStop = true;
            this.lblAuthorLink.Text = "Browse Authors ??";
            this.lblAuthorLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblAuthorLink_LinkClicked);
            // 
            // lblAuthorDescription
            // 
            this.lblAuthorDescription.AutoSize = true;
            this.lblAuthorDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblAuthorDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblAuthorDescription.Location = new System.Drawing.Point(40, 82);
            this.lblAuthorDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAuthorDescription.Name = "lblAuthorDescription";
            this.lblAuthorDescription.Size = new System.Drawing.Size(196, 19);
            this.lblAuthorDescription.TabIndex = 2;
            this.lblAuthorDescription.Text = "Browse books by author name";
            // 
            // lblAuthorTitle
            // 
            this.lblAuthorTitle.AutoSize = true;
            this.lblAuthorTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblAuthorTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblAuthorTitle.Location = new System.Drawing.Point(40, 46);
            this.lblAuthorTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAuthorTitle.Name = "lblAuthorTitle";
            this.lblAuthorTitle.Size = new System.Drawing.Size(129, 21);
            this.lblAuthorTitle.TabIndex = 1;
            this.lblAuthorTitle.Text = "Browse Authors";
            // 
            // AdminSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1233, 800);
            this.Controls.Add(this.panelMainContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AdminSearchForm";
            this.Text = "Advanced Search";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AdminSearchForm_Load);
            this.panelMainContainer.ResumeLayout(false);
            this.panelCards.ResumeLayout(false);
            this.cardPopularBooks.ResumeLayout(false);
            this.cardPopularBooks.PerformLayout();
            this.cardNewArrivals.ResumeLayout(false);
            this.cardNewArrivals.PerformLayout();
            this.cardBrowseCategory.ResumeLayout(false);
            this.cardBrowseCategory.PerformLayout();
            this.panelSearchForm.ResumeLayout(false);
            this.panelFilters.ResumeLayout(false);
            this.panelFilters.PerformLayout();
            this.panelSearchInput.ResumeLayout(false);
            this.panelSearchInput.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.cardBrowseAuthor.ResumeLayout(false);
            this.cardBrowseAuthor.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.Panel panelMainContainer;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel panelSearchForm;
        private System.Windows.Forms.Panel panelSearchInput;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Panel panelFilters;
        private System.Windows.Forms.ComboBox cmbSearchIn;
        private System.Windows.Forms.Label lblSearchIn;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Panel panelCards;
        private System.Windows.Forms.Panel cardBrowseCategory;
        private System.Windows.Forms.Label lblCategoryTitle;
        private System.Windows.Forms.Label lblCategoryDescription;
        private System.Windows.Forms.LinkLabel lblCategoryLink;
        private System.Windows.Forms.Panel cardNewArrivals;
        private System.Windows.Forms.Label lblNewTitle;
        private System.Windows.Forms.Label lblNewDescription;
        private System.Windows.Forms.LinkLabel lblNewLink;
        private System.Windows.Forms.Panel cardPopularBooks;
        private System.Windows.Forms.Label lblPopularTitle;
        private System.Windows.Forms.Label lblPopularDescription;
        private System.Windows.Forms.LinkLabel lblPopularLink;
        private System.Windows.Forms.Panel cardBrowseAuthor;
        private System.Windows.Forms.Label lblAuthorTitle;
        private System.Windows.Forms.Label lblAuthorDescription;
        private System.Windows.Forms.LinkLabel lblAuthorLink;
    }
}