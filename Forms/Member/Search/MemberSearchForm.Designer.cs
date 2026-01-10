namespace Project5LMS.Forms.Member.Search
{
    partial class MemberSearchForm
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
            this.panelSearchResults = new System.Windows.Forms.Panel();
            this.lblResultsCount = new System.Windows.Forms.Label();
            this.panelSearchSection = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cmbSearchBy = new System.Windows.Forms.ComboBox();
            this.lblSearchBy = new System.Windows.Forms.Label();
            this.panelSearchInput = new System.Windows.Forms.Panel();
            this.panelSearchIcon = new System.Windows.Forms.Panel();
            this.txtSearchQuery = new System.Windows.Forms.TextBox();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dtaGridMemberSearchCatalog = new System.Windows.Forms.DataGridView();
            this.panelMainContainer.SuspendLayout();
            this.panelSearchResults.SuspendLayout();
            this.panelSearchSection.SuspendLayout();
            this.panelSearchInput.SuspendLayout();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtaGridMemberSearchCatalog)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMainContainer
            // 
            this.panelMainContainer.AutoScroll = true;
            this.panelMainContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelMainContainer.Controls.Add(this.panelSearchResults);
            this.panelMainContainer.Controls.Add(this.lblResultsCount);
            this.panelMainContainer.Controls.Add(this.panelSearchSection);
            this.panelMainContainer.Controls.Add(this.panelHeader);
            this.panelMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainContainer.Location = new System.Drawing.Point(0, 0);
            this.panelMainContainer.Name = "panelMainContainer";
            this.panelMainContainer.Padding = new System.Windows.Forms.Padding(30, 24, 30, 24);
            this.panelMainContainer.Size = new System.Drawing.Size(1200, 800);
            this.panelMainContainer.TabIndex = 0;
            // 
            // panelSearchResults
            // 
            this.panelSearchResults.AutoScroll = true;
            this.panelSearchResults.BackColor = System.Drawing.Color.Transparent;
            this.panelSearchResults.Controls.Add(this.dtaGridMemberSearchCatalog);
            this.panelSearchResults.Location = new System.Drawing.Point(30, 284);
            this.panelSearchResults.Name = "panelSearchResults";
            this.panelSearchResults.Size = new System.Drawing.Size(930, 492);
            this.panelSearchResults.TabIndex = 3;
            // 
            // lblResultsCount
            // 
            this.lblResultsCount.AutoSize = true;
            this.lblResultsCount.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResultsCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblResultsCount.Location = new System.Drawing.Point(30, 252);
            this.lblResultsCount.Name = "lblResultsCount";
            this.lblResultsCount.Size = new System.Drawing.Size(138, 25);
            this.lblResultsCount.TabIndex = 2;
            this.lblResultsCount.Text = "Search Results";
            // 
            // panelSearchSection
            // 
            this.panelSearchSection.BackColor = System.Drawing.Color.White;
            this.panelSearchSection.Controls.Add(this.btnSearch);
            this.panelSearchSection.Controls.Add(this.cmbSearchBy);
            this.panelSearchSection.Controls.Add(this.lblSearchBy);
            this.panelSearchSection.Controls.Add(this.panelSearchInput);
            this.panelSearchSection.Location = new System.Drawing.Point(30, 146);
            this.panelSearchSection.Name = "panelSearchSection";
            this.panelSearchSection.Padding = new System.Windows.Forms.Padding(22, 20, 22, 20);
            this.panelSearchSection.Size = new System.Drawing.Size(930, 89);
            this.panelSearchSection.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Maroon;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(788, 28);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(117, 32);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cmbSearchBy
            // 
            this.cmbSearchBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchBy.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSearchBy.FormattingEnabled = true;
            this.cmbSearchBy.Location = new System.Drawing.Point(675, 32);
            this.cmbSearchBy.Name = "cmbSearchBy";
            this.cmbSearchBy.Size = new System.Drawing.Size(98, 28);
            this.cmbSearchBy.TabIndex = 2;
            // 
            // lblSearchBy
            // 
            this.lblSearchBy.AutoSize = true;
            this.lblSearchBy.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchBy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblSearchBy.Location = new System.Drawing.Point(600, 37);
            this.lblSearchBy.Name = "lblSearchBy";
            this.lblSearchBy.Size = new System.Drawing.Size(76, 20);
            this.lblSearchBy.TabIndex = 1;
            this.lblSearchBy.Text = "Search By:";
            // 
            // panelSearchInput
            // 
            this.panelSearchInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelSearchInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSearchInput.Controls.Add(this.panelSearchIcon);
            this.panelSearchInput.Controls.Add(this.txtSearchQuery);
            this.panelSearchInput.Location = new System.Drawing.Point(22, 28);
            this.panelSearchInput.Name = "panelSearchInput";
            this.panelSearchInput.Size = new System.Drawing.Size(563, 33);
            this.panelSearchInput.TabIndex = 0;
            // 
            // panelSearchIcon
            // 
            this.panelSearchIcon.BackColor = System.Drawing.Color.Transparent;
            this.panelSearchIcon.Location = new System.Drawing.Point(8, 4);
            this.panelSearchIcon.Name = "panelSearchIcon";
            this.panelSearchIcon.Size = new System.Drawing.Size(22, 24);
            this.panelSearchIcon.TabIndex = 1;
            this.panelSearchIcon.Paint += new System.Windows.Forms.PaintEventHandler(this.panelSearchIcon_Paint);
            // 
            // txtSearchQuery
            // 
            this.txtSearchQuery.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearchQuery.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchQuery.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.txtSearchQuery.Location = new System.Drawing.Point(38, 6);
            this.txtSearchQuery.Name = "txtSearchQuery";
            this.txtSearchQuery.Size = new System.Drawing.Size(518, 20);
            this.txtSearchQuery.TabIndex = 0;
            this.txtSearchQuery.Text = "Enter search term...";
            this.txtSearchQuery.Enter += new System.EventHandler(this.txtSearchQuery_Enter);
            this.txtSearchQuery.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchQuery_KeyDown);
            this.txtSearchQuery.Leave += new System.EventHandler(this.txtSearchQuery_Leave);
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.lblSubtitle);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Location = new System.Drawing.Point(30, 24);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(930, 106);
            this.panelHeader.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblSubtitle.Location = new System.Drawing.Point(0, 57);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(384, 25);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Find books by title, author, ISBN, or category";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(287, 51);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Search Catalog";
            // 
            // dtaGridMemberSearchCatalog
            // 
            this.dtaGridMemberSearchCatalog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtaGridMemberSearchCatalog.Location = new System.Drawing.Point(5, 3);
            this.dtaGridMemberSearchCatalog.Name = "dtaGridMemberSearchCatalog";
            this.dtaGridMemberSearchCatalog.Size = new System.Drawing.Size(922, 486);
            this.dtaGridMemberSearchCatalog.TabIndex = 0;
            // 
            // MemberSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.panelMainContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MemberSearchForm";
            this.Text = "Search Catalog";
            this.Load += new System.EventHandler(this.MemberSearchForm_Load);
            this.panelMainContainer.ResumeLayout(false);
            this.panelMainContainer.PerformLayout();
            this.panelSearchResults.ResumeLayout(false);
            this.panelSearchSection.ResumeLayout(false);
            this.panelSearchSection.PerformLayout();
            this.panelSearchInput.ResumeLayout(false);
            this.panelSearchInput.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtaGridMemberSearchCatalog)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.Panel panelMainContainer;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel panelSearchSection;
        private System.Windows.Forms.Panel panelSearchInput;
        private System.Windows.Forms.Panel panelSearchIcon;
        private System.Windows.Forms.TextBox txtSearchQuery;
        private System.Windows.Forms.Label lblSearchBy;
        private System.Windows.Forms.ComboBox cmbSearchBy;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblResultsCount;
        private System.Windows.Forms.Panel panelSearchResults;
        private System.Windows.Forms.DataGridView dtaGridMemberSearchCatalog;
    }
}