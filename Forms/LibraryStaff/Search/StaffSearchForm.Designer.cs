namespace Project5LMS.Forms.LibraryStaff.Search
{
    partial class StaffSearchForm
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
            this.panelResults = new System.Windows.Forms.Panel();
            this.dataGridViewResults = new System.Windows.Forms.DataGridView();
            this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAuthor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colISBN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelQuickSearchExamples = new System.Windows.Forms.Panel();
            this.flowLayoutExamples = new System.Windows.Forms.FlowLayoutPanel();
            this.lblQuickSearchTitle = new System.Windows.Forms.Label();
            this.panelSearchGuidance = new System.Windows.Forms.Panel();
            this.lblGuidanceSubtext = new System.Windows.Forms.Label();
            this.lblGuidanceTitle = new System.Windows.Forms.Label();
            this.panelAdvancedSearch = new System.Windows.Forms.Panel();
            this.panelFilterButtons = new System.Windows.Forms.Panel();
            this.btnMembersOnly = new System.Windows.Forms.Button();
            this.btnBooksOnly = new System.Windows.Forms.Button();
            this.btnAll = new System.Windows.Forms.Button();
            this.lblSearchIn = new System.Windows.Forms.Label();
            this.panelSearchInput = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblAdvancedSearchSubtitle = new System.Windows.Forms.Label();
            this.panelMainContainer.SuspendLayout();
            this.panelResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResults)).BeginInit();
            this.panelQuickSearchExamples.SuspendLayout();
            this.panelSearchGuidance.SuspendLayout();
            this.panelAdvancedSearch.SuspendLayout();
            this.panelFilterButtons.SuspendLayout();
            this.panelSearchInput.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMainContainer
            // 
            this.panelMainContainer.AutoScroll = true;
            this.panelMainContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelMainContainer.Controls.Add(this.panelResults);
            this.panelMainContainer.Controls.Add(this.panelQuickSearchExamples);
            this.panelMainContainer.Controls.Add(this.panelSearchGuidance);
            this.panelMainContainer.Controls.Add(this.panelAdvancedSearch);
            this.panelMainContainer.Controls.Add(this.panelHeader);
            this.panelMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainContainer.Location = new System.Drawing.Point(0, 0);
            this.panelMainContainer.Margin = new System.Windows.Forms.Padding(4);
            this.panelMainContainer.Name = "panelMainContainer";
            this.panelMainContainer.Padding = new System.Windows.Forms.Padding(32, 30, 32, 30);
            this.panelMainContainer.Size = new System.Drawing.Size(1600, 985);
            this.panelMainContainer.TabIndex = 0;
            // 
            // panelResults
            // 
            this.panelResults.BackColor = System.Drawing.Color.White;
            this.panelResults.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelResults.Controls.Add(this.dataGridViewResults);
            this.panelResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelResults.Location = new System.Drawing.Point(32, 527);
            this.panelResults.Margin = new System.Windows.Forms.Padding(4);
            this.panelResults.Name = "panelResults";
            this.panelResults.Padding = new System.Windows.Forms.Padding(20);
            this.panelResults.Size = new System.Drawing.Size(1536, 428);
            this.panelResults.TabIndex = 4;
            this.panelResults.Visible = false;
            // 
            // dataGridViewResults
            // 
            this.dataGridViewResults.AllowUserToAddRows = false;
            this.dataGridViewResults.AllowUserToDeleteRows = false;
            this.dataGridViewResults.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.dataGridViewResults.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewResults.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewResults.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colType,
            this.colTitle,
            this.colAuthor,
            this.colISBN,
            this.colCategory,
            this.colStatus});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewResults.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewResults.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dataGridViewResults.Location = new System.Drawing.Point(20, 20);
            this.dataGridViewResults.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewResults.MultiSelect = false;
            this.dataGridViewResults.Name = "dataGridViewResults";
            this.dataGridViewResults.ReadOnly = true;
            this.dataGridViewResults.RowHeadersVisible = false;
            this.dataGridViewResults.RowHeadersWidth = 51;
            this.dataGridViewResults.RowTemplate.Height = 60;
            this.dataGridViewResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewResults.Size = new System.Drawing.Size(1494, 386);
            this.dataGridViewResults.TabIndex = 0;
            this.dataGridViewResults.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewResults_CellDoubleClick);
            // 
            // colType
            // 
            this.colType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colType.DataPropertyName = "Type";
            this.colType.HeaderText = "Type";
            this.colType.Name = "colType";
            this.colType.ReadOnly = true;
            this.colType.Width = 80;
            // 
            // colTitle
            // 
            this.colTitle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTitle.DataPropertyName = "Title";
            this.colTitle.HeaderText = "Title/Name";
            this.colTitle.Name = "colTitle";
            this.colTitle.ReadOnly = true;
            // 
            // colAuthor
            // 
            this.colAuthor.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colAuthor.DataPropertyName = "Author";
            this.colAuthor.HeaderText = "Author/Email";
            this.colAuthor.Name = "colAuthor";
            this.colAuthor.ReadOnly = true;
            // 
            // colISBN
            // 
            this.colISBN.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colISBN.DataPropertyName = "ISBN";
            this.colISBN.HeaderText = "ISBN";
            this.colISBN.Name = "colISBN";
            this.colISBN.ReadOnly = true;
            this.colISBN.Width = 150;
            // 
            // colCategory
            // 
            this.colCategory.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colCategory.DataPropertyName = "Category";
            this.colCategory.HeaderText = "Category/Type";
            this.colCategory.Name = "colCategory";
            this.colCategory.ReadOnly = true;
            this.colCategory.Width = 120;
            // 
            // colStatus
            // 
            this.colStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colStatus.DataPropertyName = "Status";
            this.colStatus.HeaderText = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.Width = 100;
            // 
            // panelQuickSearchExamples
            // 
            this.panelQuickSearchExamples.BackColor = System.Drawing.Color.White;
            this.panelQuickSearchExamples.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelQuickSearchExamples.Controls.Add(this.flowLayoutExamples);
            this.panelQuickSearchExamples.Controls.Add(this.lblQuickSearchTitle);
            this.panelQuickSearchExamples.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelQuickSearchExamples.Location = new System.Drawing.Point(32, 377);
            this.panelQuickSearchExamples.Margin = new System.Windows.Forms.Padding(4);
            this.panelQuickSearchExamples.Name = "panelQuickSearchExamples";
            this.panelQuickSearchExamples.Padding = new System.Windows.Forms.Padding(30);
            this.panelQuickSearchExamples.Size = new System.Drawing.Size(1536, 150);
            this.panelQuickSearchExamples.TabIndex = 3;
            // 
            // flowLayoutExamples
            // 
            this.flowLayoutExamples.AutoSize = true;
            this.flowLayoutExamples.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutExamples.Location = new System.Drawing.Point(30, 30);
            this.flowLayoutExamples.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutExamples.Name = "flowLayoutExamples";
            this.flowLayoutExamples.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.flowLayoutExamples.Size = new System.Drawing.Size(1474, 10);
            this.flowLayoutExamples.TabIndex = 1;
            // 
            // lblQuickSearchTitle
            // 
            this.lblQuickSearchTitle.AutoSize = true;
            this.lblQuickSearchTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuickSearchTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblQuickSearchTitle.Location = new System.Drawing.Point(29, 44);
            this.lblQuickSearchTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQuickSearchTitle.Name = "lblQuickSearchTitle";
            this.lblQuickSearchTitle.Size = new System.Drawing.Size(230, 28);
            this.lblQuickSearchTitle.TabIndex = 0;
            this.lblQuickSearchTitle.Text = "Quick Search Examples";
            this.lblQuickSearchTitle.Click += new System.EventHandler(this.lblQuickSearchTitle_Click);
            // 
            // panelSearchGuidance
            // 
            this.panelSearchGuidance.BackColor = System.Drawing.Color.White;
            this.panelSearchGuidance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSearchGuidance.Controls.Add(this.lblGuidanceSubtext);
            this.panelSearchGuidance.Controls.Add(this.lblGuidanceTitle);
            this.panelSearchGuidance.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearchGuidance.Location = new System.Drawing.Point(32, 283);
            this.panelSearchGuidance.Margin = new System.Windows.Forms.Padding(4);
            this.panelSearchGuidance.Name = "panelSearchGuidance";
            this.panelSearchGuidance.Padding = new System.Windows.Forms.Padding(30);
            this.panelSearchGuidance.Size = new System.Drawing.Size(1536, 94);
            this.panelSearchGuidance.TabIndex = 2;
            // 
            // lblGuidanceSubtext
            // 
            this.lblGuidanceSubtext.AutoSize = true;
            this.lblGuidanceSubtext.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGuidanceSubtext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblGuidanceSubtext.Location = new System.Drawing.Point(16, 55);
            this.lblGuidanceSubtext.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGuidanceSubtext.Name = "lblGuidanceSubtext";
            this.lblGuidanceSubtext.Size = new System.Drawing.Size(432, 23);
            this.lblGuidanceSubtext.TabIndex = 2;
            this.lblGuidanceSubtext.Text = "Enter a search query above to find books and members";
            // 
            // lblGuidanceTitle
            // 
            this.lblGuidanceTitle.AutoSize = true;
            this.lblGuidanceTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGuidanceTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblGuidanceTitle.Location = new System.Drawing.Point(14, 14);
            this.lblGuidanceTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGuidanceTitle.Name = "lblGuidanceTitle";
            this.lblGuidanceTitle.Size = new System.Drawing.Size(187, 32);
            this.lblGuidanceTitle.TabIndex = 1;
            this.lblGuidanceTitle.Text = "Start Searching";
            // 
            // panelAdvancedSearch
            // 
            this.panelAdvancedSearch.BackColor = System.Drawing.Color.White;
            this.panelAdvancedSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelAdvancedSearch.Controls.Add(this.panelFilterButtons);
            this.panelAdvancedSearch.Controls.Add(this.panelSearchInput);
            this.panelAdvancedSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelAdvancedSearch.Location = new System.Drawing.Point(32, 113);
            this.panelAdvancedSearch.Margin = new System.Windows.Forms.Padding(4);
            this.panelAdvancedSearch.Name = "panelAdvancedSearch";
            this.panelAdvancedSearch.Padding = new System.Windows.Forms.Padding(30);
            this.panelAdvancedSearch.Size = new System.Drawing.Size(1536, 170);
            this.panelAdvancedSearch.TabIndex = 1;
            // 
            // panelFilterButtons
            // 
            this.panelFilterButtons.Controls.Add(this.btnMembersOnly);
            this.panelFilterButtons.Controls.Add(this.btnBooksOnly);
            this.panelFilterButtons.Controls.Add(this.btnAll);
            this.panelFilterButtons.Controls.Add(this.lblSearchIn);
            this.panelFilterButtons.Location = new System.Drawing.Point(30, 103);
            this.panelFilterButtons.Margin = new System.Windows.Forms.Padding(4);
            this.panelFilterButtons.Name = "panelFilterButtons";
            this.panelFilterButtons.Size = new System.Drawing.Size(1476, 50);
            this.panelFilterButtons.TabIndex = 3;
            // 
            // btnMembersOnly
            // 
            this.btnMembersOnly.BackColor = System.Drawing.Color.Transparent;
            this.btnMembersOnly.FlatAppearance.BorderSize = 0;
            this.btnMembersOnly.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMembersOnly.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMembersOnly.ForeColor = System.Drawing.Color.Black;
            this.btnMembersOnly.Location = new System.Drawing.Point(435, 3);
            this.btnMembersOnly.Margin = new System.Windows.Forms.Padding(4);
            this.btnMembersOnly.Name = "btnMembersOnly";
            this.btnMembersOnly.Size = new System.Drawing.Size(150, 43);
            this.btnMembersOnly.TabIndex = 3;
            this.btnMembersOnly.Text = "Members Only";
            this.btnMembersOnly.UseVisualStyleBackColor = false;
            this.btnMembersOnly.Click += new System.EventHandler(this.btnMembersOnly_Click);
            // 
            // btnBooksOnly
            // 
            this.btnBooksOnly.BackColor = System.Drawing.Color.Transparent;
            this.btnBooksOnly.FlatAppearance.BorderSize = 0;
            this.btnBooksOnly.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBooksOnly.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBooksOnly.ForeColor = System.Drawing.Color.Black;
            this.btnBooksOnly.Location = new System.Drawing.Point(250, 6);
            this.btnBooksOnly.Margin = new System.Windows.Forms.Padding(4);
            this.btnBooksOnly.Name = "btnBooksOnly";
            this.btnBooksOnly.Size = new System.Drawing.Size(150, 40);
            this.btnBooksOnly.TabIndex = 2;
            this.btnBooksOnly.Text = "Books Only";
            this.btnBooksOnly.UseVisualStyleBackColor = false;
            this.btnBooksOnly.Click += new System.EventHandler(this.btnBooksOnly_Click);
            // 
            // btnAll
            // 
            this.btnAll.FlatAppearance.BorderSize = 0;
            this.btnAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAll.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAll.ForeColor = System.Drawing.Color.White;
            this.btnAll.Location = new System.Drawing.Point(92, 6);
            this.btnAll.Margin = new System.Windows.Forms.Padding(4);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(150, 40);
            this.btnAll.TabIndex = 1;
            this.btnAll.Text = "All";
            this.btnAll.UseVisualStyleBackColor = false;
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // lblSearchIn
            // 
            this.lblSearchIn.AutoSize = true;
            this.lblSearchIn.BackColor = System.Drawing.Color.White;
            this.lblSearchIn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchIn.ForeColor = System.Drawing.Color.Black;
            this.lblSearchIn.Location = new System.Drawing.Point(6, 15);
            this.lblSearchIn.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSearchIn.Name = "lblSearchIn";
            this.lblSearchIn.Size = new System.Drawing.Size(112, 23);
            this.lblSearchIn.TabIndex = 0;
            this.lblSearchIn.Text = "🔍 Search in:";
            // 
            // panelSearchInput
            // 
            this.panelSearchInput.Controls.Add(this.btnSearch);
            this.panelSearchInput.Controls.Add(this.txtSearch);
            this.panelSearchInput.Location = new System.Drawing.Point(30, 16);
            this.panelSearchInput.Margin = new System.Windows.Forms.Padding(4);
            this.panelSearchInput.Name = "panelSearchInput";
            this.panelSearchInput.Size = new System.Drawing.Size(1476, 60);
            this.panelSearchInput.TabIndex = 2;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Maroon;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(1291, 10);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(185, 40);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.Color.Gray;
            this.txtSearch.Location = new System.Drawing.Point(10, 15);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(1258, 32);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.Text = "Search by title, author, ISBN, member name, email, or ID...";
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Controls.Add(this.lblAdvancedSearchSubtitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(32, 30);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(4);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1536, 83);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(148, 54);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Search";
            // 
            // lblAdvancedSearchSubtitle
            // 
            this.lblAdvancedSearchSubtitle.AutoSize = true;
            this.lblAdvancedSearchSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdvancedSearchSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblAdvancedSearchSubtitle.Location = new System.Drawing.Point(4, 54);
            this.lblAdvancedSearchSubtitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAdvancedSearchSubtitle.Name = "lblAdvancedSearchSubtitle";
            this.lblAdvancedSearchSubtitle.Size = new System.Drawing.Size(304, 23);
            this.lblAdvancedSearchSubtitle.TabIndex = 1;
            this.lblAdvancedSearchSubtitle.Text = "Search for books, members, and more.";
            // 
            // StaffSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1600, 985);
            this.Controls.Add(this.panelMainContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "StaffSearchForm";
            this.Text = "Search";
            this.Load += new System.EventHandler(this.StaffSearchForm_Load);
            this.panelMainContainer.ResumeLayout(false);
            this.panelResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResults)).EndInit();
            this.panelQuickSearchExamples.ResumeLayout(false);
            this.panelQuickSearchExamples.PerformLayout();
            this.panelSearchGuidance.ResumeLayout(false);
            this.panelSearchGuidance.PerformLayout();
            this.panelAdvancedSearch.ResumeLayout(false);
            this.panelFilterButtons.ResumeLayout(false);
            this.panelFilterButtons.PerformLayout();
            this.panelSearchInput.ResumeLayout(false);
            this.panelSearchInput.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMainContainer;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelAdvancedSearch;
        private System.Windows.Forms.Label lblAdvancedSearchSubtitle;
        private System.Windows.Forms.Panel panelSearchInput;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Panel panelFilterButtons;
        private System.Windows.Forms.Label lblSearchIn;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.Button btnBooksOnly;
        private System.Windows.Forms.Button btnMembersOnly;
        private System.Windows.Forms.Panel panelSearchGuidance;
        private System.Windows.Forms.Label lblGuidanceTitle;
        private System.Windows.Forms.Label lblGuidanceSubtext;
        private System.Windows.Forms.Panel panelQuickSearchExamples;
        private System.Windows.Forms.Label lblQuickSearchTitle;
        private System.Windows.Forms.Panel panelResults;
        private System.Windows.Forms.DataGridView dataGridViewResults;
        private System.Windows.Forms.DataGridViewTextBoxColumn colType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAuthor;
        private System.Windows.Forms.DataGridViewTextBoxColumn colISBN;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutExamples;
    }
}
