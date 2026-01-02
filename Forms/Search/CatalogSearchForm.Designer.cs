namespace Project5LMS.Admin_Dashboard
{
    partial class CatalogSearchForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CatalogSearchForm));
            this.panelMainContainer = new System.Windows.Forms.Panel();
            this.panelBottomSection = new System.Windows.Forms.Panel();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblDiscoverTitle = new System.Windows.Forms.Label();
            this.picBookIcon = new System.Windows.Forms.PictureBox();
            this.panelSearchContainer = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cmbSearchField = new System.Windows.Forms.ComboBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.picSearchIcon = new System.Windows.Forms.PictureBox();
            this.lblSearchTitle = new System.Windows.Forms.Label();
            this.panelMainContainer.SuspendLayout();
            this.panelBottomSection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBookIcon)).BeginInit();
            this.panelSearchContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSearchIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMainContainer
            // 
            this.panelMainContainer.BackColor = System.Drawing.Color.White;
            this.panelMainContainer.Controls.Add(this.panelBottomSection);
            this.panelMainContainer.Controls.Add(this.panelSearchContainer);
            this.panelMainContainer.Controls.Add(this.lblSearchTitle);
            this.panelMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainContainer.Location = new System.Drawing.Point(0, 0);
            this.panelMainContainer.Margin = new System.Windows.Forms.Padding(4);
            this.panelMainContainer.Name = "panelMainContainer";
            this.panelMainContainer.Padding = new System.Windows.Forms.Padding(50);
            this.panelMainContainer.Size = new System.Drawing.Size(1942, 1102);
            this.panelMainContainer.TabIndex = 0;
            // 
            // panelBottomSection
            // 
            this.panelBottomSection.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelBottomSection.Controls.Add(this.lblDescription);
            this.panelBottomSection.Controls.Add(this.lblDiscoverTitle);
            this.panelBottomSection.Controls.Add(this.picBookIcon);
            this.panelBottomSection.Location = new System.Drawing.Point(50, 400);
            this.panelBottomSection.Margin = new System.Windows.Forms.Padding(4);
            this.panelBottomSection.Name = "panelBottomSection";
            this.panelBottomSection.Size = new System.Drawing.Size(1842, 652);
            this.panelBottomSection.TabIndex = 2;
            // 
            // lblDescription
            // 
            this.lblDescription.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.ForeColor = System.Drawing.Color.Gray;
            this.lblDescription.Location = new System.Drawing.Point(700, 200);
            this.lblDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(929, 25);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Search our collection of books by title, author, ISBN, or category. Start typing " +
    "to find what you\'re looking for.";
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDiscoverTitle
            // 
            this.lblDiscoverTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDiscoverTitle.AutoSize = true;
            this.lblDiscoverTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiscoverTitle.ForeColor = System.Drawing.Color.Black;
            this.lblDiscoverTitle.Location = new System.Drawing.Point(700, 150);
            this.lblDiscoverTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDiscoverTitle.Name = "lblDiscoverTitle";
            this.lblDiscoverTitle.Size = new System.Drawing.Size(533, 39);
            this.lblDiscoverTitle.TabIndex = 1;
            this.lblDiscoverTitle.Text = "DISCOVER YOUR NEXT READ";
            this.lblDiscoverTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picBookIcon
            // 
            this.picBookIcon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picBookIcon.BackColor = System.Drawing.Color.Transparent;
            this.picBookIcon.Image = ((System.Drawing.Image)(resources.GetObject("picBookIcon.Image")));
            this.picBookIcon.Location = new System.Drawing.Point(850, 50);
            this.picBookIcon.Margin = new System.Windows.Forms.Padding(4);
            this.picBookIcon.Name = "picBookIcon";
            this.picBookIcon.Size = new System.Drawing.Size(100, 100);
            this.picBookIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBookIcon.TabIndex = 0;
            this.picBookIcon.TabStop = false;
            // 
            // panelSearchContainer
            // 
            this.panelSearchContainer.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelSearchContainer.BackColor = System.Drawing.Color.White;
            this.panelSearchContainer.Controls.Add(this.btnSearch);
            this.panelSearchContainer.Controls.Add(this.cmbSearchField);
            this.panelSearchContainer.Controls.Add(this.txtSearch);
            this.panelSearchContainer.Controls.Add(this.picSearchIcon);
            this.panelSearchContainer.Location = new System.Drawing.Point(50, 150);
            this.panelSearchContainer.Margin = new System.Windows.Forms.Padding(4);
            this.panelSearchContainer.Name = "panelSearchContainer";
            this.panelSearchContainer.Padding = new System.Windows.Forms.Padding(20);
            this.panelSearchContainer.Size = new System.Drawing.Size(1842, 200);
            this.panelSearchContainer.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(1400, 60);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(150, 50);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cmbSearchField
            // 
            this.cmbSearchField.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmbSearchField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchField.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSearchField.FormattingEnabled = true;
            this.cmbSearchField.Items.AddRange(new object[] {
            "All Fields",
            "Title",
            "Author",
            "ISBN",
            "Category",
            "Publisher"});
            this.cmbSearchField.Location = new System.Drawing.Point(1200, 60);
            this.cmbSearchField.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSearchField.Name = "cmbSearchField";
            this.cmbSearchField.Size = new System.Drawing.Size(180, 30);
            this.cmbSearchField.TabIndex = 2;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.Color.Gray;
            this.txtSearch.Location = new System.Drawing.Point(60, 60);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(1100, 30);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.Text = "Search for books, authors, ISBN...";
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            // 
            // picSearchIcon
            // 
            this.picSearchIcon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picSearchIcon.BackColor = System.Drawing.Color.Transparent;
            this.picSearchIcon.Image = ((System.Drawing.Image)(resources.GetObject("picSearchIcon.Image")));
            this.picSearchIcon.Location = new System.Drawing.Point(20, 60);
            this.picSearchIcon.Margin = new System.Windows.Forms.Padding(4);
            this.picSearchIcon.Name = "picSearchIcon";
            this.picSearchIcon.Size = new System.Drawing.Size(30, 30);
            this.picSearchIcon.TabIndex = 0;
            this.picSearchIcon.TabStop = false;
            // 
            // lblSearchTitle
            // 
            this.lblSearchTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSearchTitle.AutoSize = true;
            this.lblSearchTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchTitle.ForeColor = System.Drawing.Color.Black;
            this.lblSearchTitle.Location = new System.Drawing.Point(750, 0);
            this.lblSearchTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSearchTitle.Name = "lblSearchTitle";
            this.lblSearchTitle.Size = new System.Drawing.Size(413, 63);
            this.lblSearchTitle.TabIndex = 0;
            this.lblSearchTitle.Text = "Search Catalog";
            this.lblSearchTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CatalogSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1942, 1102);
            this.Controls.Add(this.panelMainContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CatalogSearchForm";
            this.Text = "Search Catalog";
            this.Load += new System.EventHandler(this.CatalogSearchForm_Load);
            this.panelMainContainer.ResumeLayout(false);
            this.panelMainContainer.PerformLayout();
            this.panelBottomSection.ResumeLayout(false);
            this.panelBottomSection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBookIcon)).EndInit();
            this.panelSearchContainer.ResumeLayout(false);
            this.panelSearchContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSearchIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMainContainer;
        private System.Windows.Forms.Label lblSearchTitle;
        private System.Windows.Forms.Panel panelSearchContainer;
        private System.Windows.Forms.PictureBox picSearchIcon;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cmbSearchField;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Panel panelBottomSection;
        private System.Windows.Forms.PictureBox picBookIcon;
        private System.Windows.Forms.Label lblDiscoverTitle;
        private System.Windows.Forms.Label lblDescription;
    }
}
