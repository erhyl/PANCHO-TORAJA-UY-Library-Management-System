namespace Project5LMS.Forms.LibraryStaff.Catalog
{
    partial class StaffCatalogForm
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
            this.panelBooksContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.panelSearchFilter = new System.Windows.Forms.Panel();
            this.cmbCategoryFilter = new System.Windows.Forms.ComboBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.panelMetrics = new System.Windows.Forms.Panel();
            this.panelMetricCheckedOut = new System.Windows.Forms.Panel();
            this.lblMetricCheckedOutValue = new System.Windows.Forms.Label();
            this.lblMetricCheckedOutTitle = new System.Windows.Forms.Label();
            this.panelMetricAvailable = new System.Windows.Forms.Panel();
            this.lblMetricAvailableValue = new System.Windows.Forms.Label();
            this.lblMetricAvailableTitle = new System.Windows.Forms.Label();
            this.panelMetricTotalCopies = new System.Windows.Forms.Panel();
            this.lblMetricTotalCopiesValue = new System.Windows.Forms.Label();
            this.lblMetricTotalCopiesTitle = new System.Windows.Forms.Label();
            this.panelMetricTotalBooks = new System.Windows.Forms.Panel();
            this.lblMetricTotalBooksValue = new System.Windows.Forms.Label();
            this.lblMetricTotalBooksTitle = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.btnAddNewBook = new System.Windows.Forms.Button();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelMainContainer.SuspendLayout();
            this.panelSearchFilter.SuspendLayout();
            this.panelMetrics.SuspendLayout();
            this.panelMetricCheckedOut.SuspendLayout();
            this.panelMetricAvailable.SuspendLayout();
            this.panelMetricTotalCopies.SuspendLayout();
            this.panelMetricTotalBooks.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMainContainer
            // 
            this.panelMainContainer.AutoScroll = true;
            this.panelMainContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelMainContainer.Controls.Add(this.panelBooksContainer);
            this.panelMainContainer.Controls.Add(this.panelSearchFilter);
            this.panelMainContainer.Controls.Add(this.panelMetrics);
            this.panelMainContainer.Controls.Add(this.panelHeader);
            this.panelMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainContainer.Location = new System.Drawing.Point(0, 0);
            this.panelMainContainer.Name = "panelMainContainer";
            this.panelMainContainer.Padding = new System.Windows.Forms.Padding(24, 24, 24, 24);
            this.panelMainContainer.Size = new System.Drawing.Size(1200, 800);
            this.panelMainContainer.TabIndex = 0;
            // 
            // panelBooksContainer
            // 
            this.panelBooksContainer.AutoScroll = true;
            this.panelBooksContainer.BackColor = System.Drawing.Color.Transparent;
            this.panelBooksContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBooksContainer.Location = new System.Drawing.Point(24, 195);
            this.panelBooksContainer.Name = "panelBooksContainer";
            this.panelBooksContainer.Padding = new System.Windows.Forms.Padding(0, 16, 0, 0);
            this.panelBooksContainer.Size = new System.Drawing.Size(1152, 581);
            this.panelBooksContainer.TabIndex = 3;
            // 
            // panelSearchFilter
            // 
            this.panelSearchFilter.BackColor = System.Drawing.Color.White;
            this.panelSearchFilter.Controls.Add(this.cmbCategoryFilter);
            this.panelSearchFilter.Controls.Add(this.txtSearch);
            this.panelSearchFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearchFilter.Location = new System.Drawing.Point(24, 146);
            this.panelSearchFilter.Name = "panelSearchFilter";
            this.panelSearchFilter.Padding = new System.Windows.Forms.Padding(15, 16, 15, 16);
            this.panelSearchFilter.Size = new System.Drawing.Size(1152, 49);
            this.panelSearchFilter.TabIndex = 2;
            // 
            // cmbCategoryFilter
            // 
            this.cmbCategoryFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCategoryFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategoryFilter.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCategoryFilter.FormattingEnabled = true;
            this.cmbCategoryFilter.Location = new System.Drawing.Point(916, 16);
            this.cmbCategoryFilter.Name = "cmbCategoryFilter";
            this.cmbCategoryFilter.Size = new System.Drawing.Size(222, 25);
            this.cmbCategoryFilter.TabIndex = 1;
            this.cmbCategoryFilter.SelectedIndexChanged += new System.EventHandler(this.cmbCategoryFilter_SelectedIndexChanged);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.txtSearch.Location = new System.Drawing.Point(15, 15);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(831, 27);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.Text = "Search by title, author, ISBN, or book ID...";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            // 
            // panelMetrics
            // 
            this.panelMetrics.BackColor = System.Drawing.Color.White;
            this.panelMetrics.Controls.Add(this.panelMetricCheckedOut);
            this.panelMetrics.Controls.Add(this.panelMetricAvailable);
            this.panelMetrics.Controls.Add(this.panelMetricTotalCopies);
            this.panelMetrics.Controls.Add(this.panelMetricTotalBooks);
            this.panelMetrics.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMetrics.Location = new System.Drawing.Point(24, 97);
            this.panelMetrics.Name = "panelMetrics";
            this.panelMetrics.Size = new System.Drawing.Size(1152, 49);
            this.panelMetrics.TabIndex = 1;
            // 
            // panelMetricCheckedOut
            // 
            this.panelMetricCheckedOut.BackColor = System.Drawing.Color.White;
            this.panelMetricCheckedOut.Controls.Add(this.lblMetricCheckedOutValue);
            this.panelMetricCheckedOut.Controls.Add(this.lblMetricCheckedOutTitle);
            this.panelMetricCheckedOut.Location = new System.Drawing.Point(916, 0);
            this.panelMetricCheckedOut.Name = "panelMetricCheckedOut";
            this.panelMetricCheckedOut.Padding = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.panelMetricCheckedOut.Size = new System.Drawing.Size(236, 49);
            this.panelMetricCheckedOut.TabIndex = 3;
            // 
            // lblMetricCheckedOutValue
            // 
            this.lblMetricCheckedOutValue.AutoSize = true;
            this.lblMetricCheckedOutValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricCheckedOutValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricCheckedOutValue.Location = new System.Drawing.Point(11, 16);
            this.lblMetricCheckedOutValue.Name = "lblMetricCheckedOutValue";
            this.lblMetricCheckedOutValue.Size = new System.Drawing.Size(28, 32);
            this.lblMetricCheckedOutValue.TabIndex = 1;
            this.lblMetricCheckedOutValue.Text = "0";
            // 
            // lblMetricCheckedOutTitle
            // 
            this.lblMetricCheckedOutTitle.AutoSize = true;
            this.lblMetricCheckedOutTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricCheckedOutTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricCheckedOutTitle.Location = new System.Drawing.Point(11, 0);
            this.lblMetricCheckedOutTitle.Name = "lblMetricCheckedOutTitle";
            this.lblMetricCheckedOutTitle.Size = new System.Drawing.Size(91, 15);
            this.lblMetricCheckedOutTitle.TabIndex = 0;
            this.lblMetricCheckedOutTitle.Text = "📤 Checked Out";
            // 
            // panelMetricAvailable
            // 
            this.panelMetricAvailable.BackColor = System.Drawing.Color.White;
            this.panelMetricAvailable.Controls.Add(this.lblMetricAvailableValue);
            this.panelMetricAvailable.Controls.Add(this.lblMetricAvailableTitle);
            this.panelMetricAvailable.Location = new System.Drawing.Point(604, 0);
            this.panelMetricAvailable.Name = "panelMetricAvailable";
            this.panelMetricAvailable.Padding = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.panelMetricAvailable.Size = new System.Drawing.Size(236, 49);
            this.panelMetricAvailable.TabIndex = 2;
            // 
            // lblMetricAvailableValue
            // 
            this.lblMetricAvailableValue.AutoSize = true;
            this.lblMetricAvailableValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricAvailableValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricAvailableValue.Location = new System.Drawing.Point(11, 16);
            this.lblMetricAvailableValue.Name = "lblMetricAvailableValue";
            this.lblMetricAvailableValue.Size = new System.Drawing.Size(28, 32);
            this.lblMetricAvailableValue.TabIndex = 1;
            this.lblMetricAvailableValue.Text = "0";
            // 
            // lblMetricAvailableTitle
            // 
            this.lblMetricAvailableTitle.AutoSize = true;
            this.lblMetricAvailableTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricAvailableTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricAvailableTitle.Location = new System.Drawing.Point(11, 0);
            this.lblMetricAvailableTitle.Name = "lblMetricAvailableTitle";
            this.lblMetricAvailableTitle.Size = new System.Drawing.Size(70, 15);
            this.lblMetricAvailableTitle.TabIndex = 0;
            this.lblMetricAvailableTitle.Text = "🟢 Available";
            // 
            // panelMetricTotalCopies
            // 
            this.panelMetricTotalCopies.BackColor = System.Drawing.Color.White;
            this.panelMetricTotalCopies.Controls.Add(this.lblMetricTotalCopiesValue);
            this.panelMetricTotalCopies.Controls.Add(this.lblMetricTotalCopiesTitle);
            this.panelMetricTotalCopies.Location = new System.Drawing.Point(298, 0);
            this.panelMetricTotalCopies.Name = "panelMetricTotalCopies";
            this.panelMetricTotalCopies.Padding = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.panelMetricTotalCopies.Size = new System.Drawing.Size(236, 49);
            this.panelMetricTotalCopies.TabIndex = 1;
            // 
            // lblMetricTotalCopiesValue
            // 
            this.lblMetricTotalCopiesValue.AutoSize = true;
            this.lblMetricTotalCopiesValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTotalCopiesValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricTotalCopiesValue.Location = new System.Drawing.Point(11, 16);
            this.lblMetricTotalCopiesValue.Name = "lblMetricTotalCopiesValue";
            this.lblMetricTotalCopiesValue.Size = new System.Drawing.Size(28, 32);
            this.lblMetricTotalCopiesValue.TabIndex = 1;
            this.lblMetricTotalCopiesValue.Text = "0";
            // 
            // lblMetricTotalCopiesTitle
            // 
            this.lblMetricTotalCopiesTitle.AutoSize = true;
            this.lblMetricTotalCopiesTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTotalCopiesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricTotalCopiesTitle.Location = new System.Drawing.Point(11, 0);
            this.lblMetricTotalCopiesTitle.Name = "lblMetricTotalCopiesTitle";
            this.lblMetricTotalCopiesTitle.Size = new System.Drawing.Size(87, 15);
            this.lblMetricTotalCopiesTitle.TabIndex = 0;
            this.lblMetricTotalCopiesTitle.Text = "🗃️ Total Copies";
            // 
            // panelMetricTotalBooks
            // 
            this.panelMetricTotalBooks.BackColor = System.Drawing.Color.White;
            this.panelMetricTotalBooks.Controls.Add(this.lblMetricTotalBooksValue);
            this.panelMetricTotalBooks.Controls.Add(this.lblMetricTotalBooksTitle);
            this.panelMetricTotalBooks.Location = new System.Drawing.Point(0, 0);
            this.panelMetricTotalBooks.Name = "panelMetricTotalBooks";
            this.panelMetricTotalBooks.Padding = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.panelMetricTotalBooks.Size = new System.Drawing.Size(236, 49);
            this.panelMetricTotalBooks.TabIndex = 0;
            // 
            // lblMetricTotalBooksValue
            // 
            this.lblMetricTotalBooksValue.AutoSize = true;
            this.lblMetricTotalBooksValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTotalBooksValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricTotalBooksValue.Location = new System.Drawing.Point(11, 16);
            this.lblMetricTotalBooksValue.Name = "lblMetricTotalBooksValue";
            this.lblMetricTotalBooksValue.Size = new System.Drawing.Size(28, 32);
            this.lblMetricTotalBooksValue.TabIndex = 1;
            this.lblMetricTotalBooksValue.Text = "0";
            // 
            // lblMetricTotalBooksTitle
            // 
            this.lblMetricTotalBooksTitle.AutoSize = true;
            this.lblMetricTotalBooksTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTotalBooksTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricTotalBooksTitle.Location = new System.Drawing.Point(11, 0);
            this.lblMetricTotalBooksTitle.Name = "lblMetricTotalBooksTitle";
            this.lblMetricTotalBooksTitle.Size = new System.Drawing.Size(83, 15);
            this.lblMetricTotalBooksTitle.TabIndex = 0;
            this.lblMetricTotalBooksTitle.Text = "📖 Total Books";
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.btnAddNewBook);
            this.panelHeader.Controls.Add(this.lblSubtitle);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(24, 24);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1152, 73);
            this.panelHeader.TabIndex = 0;
            // 
            // btnAddNewBook
            // 
            this.btnAddNewBook.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNewBook.BackColor = System.Drawing.Color.Maroon;
            this.btnAddNewBook.FlatAppearance.BorderSize = 0;
            this.btnAddNewBook.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNewBook.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddNewBook.ForeColor = System.Drawing.Color.White;
            this.btnAddNewBook.Location = new System.Drawing.Point(1013, 9);
            this.btnAddNewBook.Name = "btnAddNewBook";
            this.btnAddNewBook.Size = new System.Drawing.Size(123, 54);
            this.btnAddNewBook.TabIndex = 2;
            this.btnAddNewBook.Text = "+ Add New Book";
            this.btnAddNewBook.UseVisualStyleBackColor = false;
            this.btnAddNewBook.Click += new System.EventHandler(this.btnAddNewBook_Click);
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblSubtitle.Location = new System.Drawing.Point(4, 38);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(271, 19);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Browse and manage library book collection";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitle.Location = new System.Drawing.Point(3, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(135, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Catalog";
            // 
            // StaffCatalogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.panelMainContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "StaffCatalogForm";
            this.Text = "Book Catalog";
            this.Load += new System.EventHandler(this.StaffCatalogForm_Load);
            this.panelMainContainer.ResumeLayout(false);
            this.panelSearchFilter.ResumeLayout(false);
            this.panelSearchFilter.PerformLayout();
            this.panelMetrics.ResumeLayout(false);
            this.panelMetricCheckedOut.ResumeLayout(false);
            this.panelMetricCheckedOut.PerformLayout();
            this.panelMetricAvailable.ResumeLayout(false);
            this.panelMetricAvailable.PerformLayout();
            this.panelMetricTotalCopies.ResumeLayout(false);
            this.panelMetricTotalCopies.PerformLayout();
            this.panelMetricTotalBooks.ResumeLayout(false);
            this.panelMetricTotalBooks.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMainContainer;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Button btnAddNewBook;
        private System.Windows.Forms.Panel panelMetrics;
        private System.Windows.Forms.Panel panelMetricTotalBooks;
        private System.Windows.Forms.Label lblMetricTotalBooksTitle;
        private System.Windows.Forms.Label lblMetricTotalBooksValue;
        private System.Windows.Forms.Panel panelMetricTotalCopies;
        private System.Windows.Forms.Label lblMetricTotalCopiesTitle;
        private System.Windows.Forms.Label lblMetricTotalCopiesValue;
        private System.Windows.Forms.Panel panelMetricAvailable;
        private System.Windows.Forms.Label lblMetricAvailableTitle;
        private System.Windows.Forms.Label lblMetricAvailableValue;
        private System.Windows.Forms.Panel panelMetricCheckedOut;
        private System.Windows.Forms.Label lblMetricCheckedOutTitle;
        private System.Windows.Forms.Label lblMetricCheckedOutValue;
        private System.Windows.Forms.Panel panelSearchFilter;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cmbCategoryFilter;
        private System.Windows.Forms.FlowLayoutPanel panelBooksContainer;
    }
}

