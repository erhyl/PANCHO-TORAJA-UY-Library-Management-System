namespace Project5LMS.Forms.MemberRoleForms
{
    partial class MembersBrowseBooksForm
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
            this.lblActiveTransactionsTitle = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblMetricTitle1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dta_GridMembersBrowseBooks = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dta_GridMembersBrowseBooks)).BeginInit();
            this.SuspendLayout();
            // 
            // lblActiveTransactionsTitle
            // 
            this.lblActiveTransactionsTitle.AutoSize = true;
            this.lblActiveTransactionsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActiveTransactionsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblActiveTransactionsTitle.Location = new System.Drawing.Point(21, 27);
            this.lblActiveTransactionsTitle.Name = "lblActiveTransactionsTitle";
            this.lblActiveTransactionsTitle.Size = new System.Drawing.Size(200, 31);
            this.lblActiveTransactionsTitle.TabIndex = 5;
            this.lblActiveTransactionsTitle.Text = "Browse Books";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(27, 124);
            this.txtSearch.Multiline = true;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(418, 20);
            this.txtSearch.TabIndex = 6;
            // 
            // cmbStatus
            // 
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(461, 123);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(121, 21);
            this.cmbStatus.TabIndex = 7;
            // 
            // lblMetricTitle1
            // 
            this.lblMetricTitle1.AutoSize = true;
            this.lblMetricTitle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTitle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricTitle1.Location = new System.Drawing.Point(24, 103);
            this.lblMetricTitle1.Name = "lblMetricTitle1";
            this.lblMetricTitle1.Size = new System.Drawing.Size(103, 18);
            this.lblMetricTitle1.TabIndex = 8;
            this.lblMetricTitle1.Text = "Search Books";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.label1.Location = new System.Drawing.Point(458, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 9;
            this.label1.Text = "Book Filter";
            // 
            // dta_GridMembersBrowseBooks
            // 
            this.dta_GridMembersBrowseBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dta_GridMembersBrowseBooks.Location = new System.Drawing.Point(27, 150);
            this.dta_GridMembersBrowseBooks.Name = "dta_GridMembersBrowseBooks";
            this.dta_GridMembersBrowseBooks.Size = new System.Drawing.Size(1195, 470);
            this.dta_GridMembersBrowseBooks.TabIndex = 10;
            // 
            // MembersBrowseBooksForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1234, 632);
            this.Controls.Add(this.dta_GridMembersBrowseBooks);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblMetricTitle1);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblActiveTransactionsTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MembersBrowseBooksForm";
            this.Text = "MembersBrowseBooksForm";
            this.Load += new System.EventHandler(this.MembersBrowseBooksForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dta_GridMembersBrowseBooks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblActiveTransactionsTitle;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label lblMetricTitle1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dta_GridMembersBrowseBooks;
    }
}