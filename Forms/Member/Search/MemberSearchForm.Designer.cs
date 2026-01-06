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
            this.panelMainContainer.SuspendLayout();
            this.panelSearchSection.SuspendLayout();
            this.panelSearchInput.SuspendLayout();
            this.panelHeader.SuspendLayout();
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
            this.panelMainContainer.Margin = new System.Windows.Forms.Padding(4);
            this.panelMainContainer.Name = "panelMainContainer";
            this.panelMainContainer.Padding = new System.Windows.Forms.Padding(40, 30, 40, 30);
            this.panelMainContainer.Size = new System.Drawing.Size(1600, 985);
            this.panelMainContainer.TabIndex = 0;
            // 
            // panelSearchResults
            // 
            this.panelSearchResults.AutoScroll = true;
            this.panelSearchResults.BackColor = System.Drawing.Color.Transparent;
            this.panelSearchResults.Location = new System.Drawing.Point(40, 350);
            this.panelSearchResults.Margin = new System.Windows.Forms.Padding(4);
            this.panelSearchResults.Name = "panelSearchResults";
            this.panelSearchResults.Size = new System.Drawing.Size(1240, 605);
            this.panelSearchResults.TabIndex = 3;
            // 
            // lblResultsCount
            // 
            this.lblResultsCount.AutoSize = true;
            this.lblResultsCount.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResultsCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblResultsCount.Location = new System.Drawing.Point(40, 310);
            this.lblResultsCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblResultsCount.Name = "lblResultsCount";
            this.lblResultsCount.Size = new System.Drawing.Size(177, 32);
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
            this.panelSearchSection.Location = new System.Drawing.Point(40, 180);
            this.panelSearchSection.Margin = new System.Windows.Forms.Padding(4);
            this.panelSearchSection.Name = "panelSearchSection";
            this.panelSearchSection.Padding = new System.Windows.Forms.Padding(30, 25, 30, 25);
            this.panelSearchSection.Size = new System.Drawing.Size(1240, 110);
            this.panelSearchSection.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Maroon;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(1050, 35);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(156, 40);
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
            this.cmbSearchBy.Location = new System.Drawing.Point(900, 40);
            this.cmbSearchBy.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSearchBy.Name = "cmbSearchBy";
            this.cmbSearchBy.Size = new System.Drawing.Size(130, 33);
            this.cmbSearchBy.TabIndex = 2;
            // 
            // lblSearchBy
            // 
            this.lblSearchBy.AutoSize = true;
            this.lblSearchBy.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchBy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblSearchBy.Location = new System.Drawing.Point(800, 45);
            this.lblSearchBy.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSearchBy.Name = "lblSearchBy";
            this.lblSearchBy.Size = new System.Drawing.Size(98, 25);
            this.lblSearchBy.TabIndex = 1;
            this.lblSearchBy.Text = "Search By:";
            // 
            // panelSearchInput
            // 
            this.panelSearchInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelSearchInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSearchInput.Controls.Add(this.panelSearchIcon);
            this.panelSearchInput.Controls.Add(this.txtSearchQuery);
            this.panelSearchInput.Location = new System.Drawing.Point(30, 35);
            this.panelSearchInput.Margin = new System.Windows.Forms.Padding(4);
            this.panelSearchInput.Name = "panelSearchInput";
            this.panelSearchInput.Size = new System.Drawing.Size(750, 40);
            this.panelSearchInput.TabIndex = 0;
            // 
            // panelSearchIcon
            // 
            this.panelSearchIcon.BackColor = System.Drawing.Color.Transparent;
            this.panelSearchIcon.Location = new System.Drawing.Point(10, 5);
            this.panelSearchIcon.Margin = new System.Windows.Forms.Padding(4);
            this.panelSearchIcon.Name = "panelSearchIcon";
            this.panelSearchIcon.Size = new System.Drawing.Size(30, 30);
            this.panelSearchIcon.TabIndex = 1;
            this.panelSearchIcon.Paint += new System.Windows.Forms.PaintEventHandler(this.panelSearchIcon_Paint);
            // 
            // txtSearchQuery
            // 
            this.txtSearchQuery.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearchQuery.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchQuery.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.txtSearchQuery.Location = new System.Drawing.Point(50, 8);
            this.txtSearchQuery.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearchQuery.Name = "txtSearchQuery";
            this.txtSearchQuery.Size = new System.Drawing.Size(690, 25);
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
            this.panelHeader.Location = new System.Drawing.Point(40, 30);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(4);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1240, 130);
            this.panelHeader.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblSubtitle.Location = new System.Drawing.Point(0, 70);
            this.lblSubtitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(490, 32);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Find books by title, author, ISBN, or category";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(354, 62);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Search Catalog";
            // 
            // MemberSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1600, 985);
            this.Controls.Add(this.panelMainContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MemberSearchForm";
            this.Text = "Search Catalog";
            this.Load += new System.EventHandler(this.MemberSearchForm_Load);
            this.panelMainContainer.ResumeLayout(false);
            this.panelMainContainer.PerformLayout();
            this.panelSearchSection.ResumeLayout(false);
            this.panelSearchSection.PerformLayout();
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
    }
}
