namespace Project5LMS.Forms.Admin.Catalog
{
    partial class EBookForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblFileSize;
        private System.Windows.Forms.TextBox txtFileSize;
        private System.Windows.Forms.Label lblFormat;
        private System.Windows.Forms.ComboBox cmbFormat;
        private System.Windows.Forms.Label lblDownloadLink;
        private System.Windows.Forms.TextBox txtDownloadLink;
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
            this.txtDownloadLink = new System.Windows.Forms.TextBox();
            this.lblDownloadLink = new System.Windows.Forms.Label();
            this.cmbFormat = new System.Windows.Forms.ComboBox();
            this.lblFormat = new System.Windows.Forms.Label();
            this.txtFileSize = new System.Windows.Forms.TextBox();
            this.lblFileSize = new System.Windows.Forms.Label();
            base.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.txtFileSize.Location = new System.Drawing.Point(130, 17);
            this.txtFileSize.Name = "txtFileSize";
            this.txtFileSize.Size = new System.Drawing.Size(300, 22);
            this.txtFileSize.TabIndex = 1;
            this.lblFileSize.AutoSize = true;
            this.lblFileSize.Location = new System.Drawing.Point(20, 20);
            this.lblFileSize.Name = "lblFileSize";
            this.lblFileSize.Size = new System.Drawing.Size(92, 16);
            this.lblFileSize.TabIndex = 0;
            this.lblFileSize.Text = "File Size (MB):";
            this.cmbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormat.FormattingEnabled = true;
            this.cmbFormat.Items.AddRange(new object[] {
            "PDF",
            "EPUB",
            "MOBI",
            "AZW",
            "TXT",
            "Other"});
            this.cmbFormat.Location = new System.Drawing.Point(130, 57);
            this.cmbFormat.Name = "cmbFormat";
            this.cmbFormat.Size = new System.Drawing.Size(300, 24);
            this.cmbFormat.TabIndex = 3;
            this.lblFormat.AutoSize = true;
            this.lblFormat.Location = new System.Drawing.Point(20, 60);
            this.lblFormat.Name = "lblFormat";
            this.lblFormat.Size = new System.Drawing.Size(52, 16);
            this.lblFormat.TabIndex = 2;
            this.lblFormat.Text = "Format:";
            this.txtDownloadLink.Location = new System.Drawing.Point(130, 97);
            this.txtDownloadLink.Multiline = true;
            this.txtDownloadLink.Name = "txtDownloadLink";
            this.txtDownloadLink.Size = new System.Drawing.Size(300, 47);
            this.txtDownloadLink.TabIndex = 5;
            this.lblDownloadLink.AutoSize = true;
            this.lblDownloadLink.Location = new System.Drawing.Point(20, 100);
            this.lblDownloadLink.Name = "lblDownloadLink";
            this.lblDownloadLink.Size = new System.Drawing.Size(98, 16);
            this.lblDownloadLink.TabIndex = 4;
            this.lblDownloadLink.Text = "Download Link:";
            this.panelMain.Controls.Add(this.txtDownloadLink);
            this.panelMain.Controls.Add(this.lblDownloadLink);
            this.panelMain.Controls.Add(this.cmbFormat);
            this.panelMain.Controls.Add(this.lblFormat);
            this.panelMain.Controls.Add(this.txtFileSize);
            this.panelMain.Controls.Add(this.lblFileSize);
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 300);
            this.Name = "EBookForm";
            this.Text = "Add E-Book";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            base.ResumeLayout(false);
        }
        #endregion
    }
}