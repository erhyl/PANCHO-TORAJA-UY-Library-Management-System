namespace Project5LMS.Admin_Dashboard
{
    partial class CatalogForm
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
            this.panelSearchFilters = new System.Windows.Forms.Panel();
            this.panelSearchBox = new System.Windows.Forms.Panel();
            this.picSearchIcon = new System.Windows.Forms.PictureBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.cmbTypes = new System.Windows.Forms.ComboBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEditBook = new System.Windows.Forms.Button();
            this.btnAddCopies = new System.Windows.Forms.Button();
            this.btnAddBook = new System.Windows.Forms.Button();
            this.dta_GridCatalog = new System.Windows.Forms.DataGridView();
            this.panelMainContainer.SuspendLayout();
            this.panelSearchFilters.SuspendLayout();
            this.panelSearchBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSearchIcon)).BeginInit();
            this.panelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dta_GridCatalog)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMainContainer
            // 
            this.panelMainContainer.AutoScroll = true;
            this.panelMainContainer.BackColor = System.Drawing.Color.White;
            this.panelMainContainer.Controls.Add(this.dta_GridCatalog);
            this.panelMainContainer.Controls.Add(this.panelSearchFilters);
            this.panelMainContainer.Controls.Add(this.panelButtons);
            this.panelMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainContainer.Location = new System.Drawing.Point(0, 0);
            this.panelMainContainer.Name = "panelMainContainer";
            this.panelMainContainer.Padding = new System.Windows.Forms.Padding(22, 24, 22, 24);
            this.panelMainContainer.Size = new System.Drawing.Size(1455, 894);
            this.panelMainContainer.TabIndex = 0;
            this.panelMainContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMainContainer_Paint);
            // 
            // panelSearchFilters
            // 
            this.panelSearchFilters.Controls.Add(this.panelSearchBox);
            this.panelSearchFilters.Controls.Add(this.cmbStatus);
            this.panelSearchFilters.Controls.Add(this.cmbTypes);
            this.panelSearchFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearchFilters.Location = new System.Drawing.Point(22, 73);
            this.panelSearchFilters.Name = "panelSearchFilters";
            this.panelSearchFilters.Size = new System.Drawing.Size(1411, 49);
            this.panelSearchFilters.TabIndex = 1;
            // 
            // panelSearchBox
            // 
            this.panelSearchBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSearchBox.BackColor = System.Drawing.Color.White;
            this.panelSearchBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSearchBox.Controls.Add(this.picSearchIcon);
            this.panelSearchBox.Controls.Add(this.txtSearch);
            this.panelSearchBox.Location = new System.Drawing.Point(0, 12);
            this.panelSearchBox.Name = "panelSearchBox";
            this.panelSearchBox.Size = new System.Drawing.Size(1012, 33);
            this.panelSearchBox.TabIndex = 3;
            // 
            // picSearchIcon
            // 
            this.picSearchIcon.BackColor = System.Drawing.Color.Transparent;
            this.picSearchIcon.Location = new System.Drawing.Point(8, 6);
            this.picSearchIcon.Name = "picSearchIcon";
            this.picSearchIcon.Size = new System.Drawing.Size(18, 20);
            this.picSearchIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picSearchIcon.TabIndex = 1;
            this.picSearchIcon.TabStop = false;
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.Color.Gray;
            this.txtSearch.Location = new System.Drawing.Point(34, 6);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(975, 17);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.Text = "Search by Accession or Title ";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            // 
            // cmbStatus
            // 
            this.cmbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(1139, 16);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(114, 26);
            this.cmbStatus.TabIndex = 2;
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);
            // 
            // cmbTypes
            // 
            this.cmbTypes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypes.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTypes.FormattingEnabled = true;
            this.cmbTypes.Location = new System.Drawing.Point(1019, 16);
            this.cmbTypes.Name = "cmbTypes";
            this.cmbTypes.Size = new System.Drawing.Size(114, 26);
            this.cmbTypes.TabIndex = 1;
            this.cmbTypes.SelectedIndexChanged += new System.EventHandler(this.cmbTypes_SelectedIndexChanged);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnImport);
            this.panelButtons.Controls.Add(this.btnDelete);
            this.panelButtons.Controls.Add(this.btnEditBook);
            this.panelButtons.Controls.Add(this.btnAddCopies);
            this.panelButtons.Controls.Add(this.btnAddBook);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelButtons.Location = new System.Drawing.Point(22, 24);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(1411, 49);
            this.panelButtons.TabIndex = 0;
            // 
            // btnImport
            // 
            this.btnImport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.btnImport.FlatAppearance.BorderSize = 0;
            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImport.ForeColor = System.Drawing.Color.White;
            this.btnImport.Location = new System.Drawing.Point(501, 10);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(82, 32);
            this.btnImport.TabIndex = 4;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = false;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(404, 10);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(82, 32);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "- Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEditBook
            // 
            this.btnEditBook.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.btnEditBook.FlatAppearance.BorderSize = 0;
            this.btnEditBook.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditBook.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditBook.ForeColor = System.Drawing.Color.White;
            this.btnEditBook.Location = new System.Drawing.Point(274, 10);
            this.btnEditBook.Name = "btnEditBook";
            this.btnEditBook.Size = new System.Drawing.Size(117, 32);
            this.btnEditBook.TabIndex = 2;
            this.btnEditBook.Text = "- Edit Book";
            this.btnEditBook.UseVisualStyleBackColor = false;
            this.btnEditBook.Click += new System.EventHandler(this.btnEditBook_Click);
            // 
            // btnAddCopies
            // 
            this.btnAddCopies.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.btnAddCopies.FlatAppearance.BorderSize = 0;
            this.btnAddCopies.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddCopies.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddCopies.ForeColor = System.Drawing.Color.White;
            this.btnAddCopies.Location = new System.Drawing.Point(139, 10);
            this.btnAddCopies.Name = "btnAddCopies";
            this.btnAddCopies.Size = new System.Drawing.Size(123, 32);
            this.btnAddCopies.TabIndex = 1;
            this.btnAddCopies.Text = "+ Add Copies";
            this.btnAddCopies.UseVisualStyleBackColor = false;
            this.btnAddCopies.Click += new System.EventHandler(this.btnAddCopies_Click);
            // 
            // btnAddBook
            // 
            this.btnAddBook.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.btnAddBook.FlatAppearance.BorderSize = 0;
            this.btnAddBook.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddBook.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddBook.ForeColor = System.Drawing.Color.White;
            this.btnAddBook.Location = new System.Drawing.Point(8, 10);
            this.btnAddBook.Name = "btnAddBook";
            this.btnAddBook.Size = new System.Drawing.Size(116, 32);
            this.btnAddBook.TabIndex = 0;
            this.btnAddBook.Text = "+ Add Book";
            this.btnAddBook.UseVisualStyleBackColor = false;
            this.btnAddBook.Click += new System.EventHandler(this.btnAddBook_Click);
            // 
            // dta_GridCatalog
            // 
            this.dta_GridCatalog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dta_GridCatalog.Location = new System.Drawing.Point(0, 124);
            this.dta_GridCatalog.Name = "dta_GridCatalog";
            this.dta_GridCatalog.Size = new System.Drawing.Size(1455, 770);
            this.dta_GridCatalog.TabIndex = 0;
            this.dta_GridCatalog.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dta_GridCatalog_CellContentClick);
            // 
            // CatalogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1455, 894);
            this.Controls.Add(this.panelMainContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CatalogForm";
            this.Text = "Catalog";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Catalog_Load);
            this.panelMainContainer.ResumeLayout(false);
            this.panelSearchFilters.ResumeLayout(false);
            this.panelSearchBox.ResumeLayout(false);
            this.panelSearchBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSearchIcon)).EndInit();
            this.panelButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dta_GridCatalog)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMainContainer;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnAddBook;
        private System.Windows.Forms.Button btnAddCopies;
        private System.Windows.Forms.Button btnEditBook;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Panel panelSearchFilters;
        private System.Windows.Forms.Panel panelSearchBox;
        private System.Windows.Forms.PictureBox picSearchIcon;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cmbTypes;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.DataGridView dta_GridCatalog;
    }
}
