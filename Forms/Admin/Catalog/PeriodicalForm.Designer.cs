namespace Project5LMS.Forms.Admin.Catalog
{
    partial class PeriodicalForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblISSN;
        private System.Windows.Forms.TextBox txtISSN;
        private System.Windows.Forms.Label lblVolume;
        private System.Windows.Forms.TextBox txtVolume;
        private System.Windows.Forms.Label lblIssue;
        private System.Windows.Forms.TextBox txtIssue;
        private System.Windows.Forms.Label lblPubDate;
        private System.Windows.Forms.DateTimePicker dtpPublicationDate;
        private System.Windows.Forms.Label lblFrequency;
        private System.Windows.Forms.ComboBox cmbFrequency;
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
            this.cmbFrequency = new System.Windows.Forms.ComboBox();
            this.lblFrequency = new System.Windows.Forms.Label();
            this.dtpPublicationDate = new System.Windows.Forms.DateTimePicker();
            this.lblPubDate = new System.Windows.Forms.Label();
            this.txtIssue = new System.Windows.Forms.TextBox();
            this.lblIssue = new System.Windows.Forms.Label();
            this.txtVolume = new System.Windows.Forms.TextBox();
            this.lblVolume = new System.Windows.Forms.Label();
            this.txtISSN = new System.Windows.Forms.TextBox();
            this.lblISSN = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panelFieldsContainer.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelFieldsContainer
            // 
            this.panelFieldsContainer.Controls.Add(this.cmbFrequency);
            this.panelFieldsContainer.Controls.Add(this.lblFrequency);
            this.panelFieldsContainer.Controls.Add(this.dtpPublicationDate);
            this.panelFieldsContainer.Controls.Add(this.lblPubDate);
            this.panelFieldsContainer.Controls.Add(this.txtIssue);
            this.panelFieldsContainer.Controls.Add(this.lblIssue);
            this.panelFieldsContainer.Controls.Add(this.txtVolume);
            this.panelFieldsContainer.Controls.Add(this.lblVolume);
            this.panelFieldsContainer.Controls.Add(this.txtISSN);
            this.panelFieldsContainer.Controls.Add(this.lblISSN);
            // 
            // cmbFrequency
            // 
            this.cmbFrequency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFrequency.FormattingEnabled = true;
            this.cmbFrequency.Items.AddRange(new object[] {
            "Daily",
            "Weekly",
            "Bi-weekly",
            "Monthly",
            "Quarterly",
            "Semi-annually",
            "Annually"});
            this.cmbFrequency.Location = new System.Drawing.Point(134, 202);
            this.cmbFrequency.Name = "cmbFrequency";
            this.cmbFrequency.Size = new System.Drawing.Size(296, 24);
            this.cmbFrequency.TabIndex = 9;
            this.cmbFrequency.SelectedIndexChanged += new System.EventHandler(this.cmbFrequency_SelectedIndexChanged);
            // 
            // lblFrequency
            // 
            this.lblFrequency.AutoSize = true;
            this.lblFrequency.Location = new System.Drawing.Point(20, 210);
            this.lblFrequency.Name = "lblFrequency";
            this.lblFrequency.Size = new System.Drawing.Size(74, 16);
            this.lblFrequency.TabIndex = 8;
            this.lblFrequency.Text = "Frequency:";
            // 
            // dtpPublicationDate
            // 
            this.dtpPublicationDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPublicationDate.Location = new System.Drawing.Point(134, 167);
            this.dtpPublicationDate.Name = "dtpPublicationDate";
            this.dtpPublicationDate.Size = new System.Drawing.Size(296, 22);
            this.dtpPublicationDate.TabIndex = 7;
            // 
            // lblPubDate
            // 
            this.lblPubDate.AutoSize = true;
            this.lblPubDate.Location = new System.Drawing.Point(20, 172);
            this.lblPubDate.Name = "lblPubDate";
            this.lblPubDate.Size = new System.Drawing.Size(108, 16);
            this.lblPubDate.TabIndex = 6;
            this.lblPubDate.Text = "Publication Date:";
            // 
            // txtIssue
            // 
            this.txtIssue.Location = new System.Drawing.Point(130, 97);
            this.txtIssue.Multiline = true;
            this.txtIssue.Name = "txtIssue";
            this.txtIssue.Size = new System.Drawing.Size(300, 48);
            this.txtIssue.TabIndex = 5;
            // 
            // lblIssue
            // 
            this.lblIssue.AutoSize = true;
            this.lblIssue.Location = new System.Drawing.Point(20, 100);
            this.lblIssue.Name = "lblIssue";
            this.lblIssue.Size = new System.Drawing.Size(42, 16);
            this.lblIssue.TabIndex = 4;
            this.lblIssue.Text = "Issue:";
            // 
            // txtVolume
            // 
            this.txtVolume.Location = new System.Drawing.Point(130, 57);
            this.txtVolume.Name = "txtVolume";
            this.txtVolume.Size = new System.Drawing.Size(300, 22);
            this.txtVolume.TabIndex = 3;
            // 
            // lblVolume
            // 
            this.lblVolume.AutoSize = true;
            this.lblVolume.Location = new System.Drawing.Point(20, 60);
            this.lblVolume.Name = "lblVolume";
            this.lblVolume.Size = new System.Drawing.Size(56, 16);
            this.lblVolume.TabIndex = 2;
            this.lblVolume.Text = "Volume:";
            // 
            // txtISSN
            // 
            this.txtISSN.Location = new System.Drawing.Point(130, 17);
            this.txtISSN.Name = "txtISSN";
            this.txtISSN.Size = new System.Drawing.Size(300, 22);
            this.txtISSN.TabIndex = 1;
            // 
            // lblISSN
            // 
            this.lblISSN.AutoSize = true;
            this.lblISSN.Location = new System.Drawing.Point(20, 20);
            this.lblISSN.Name = "lblISSN";
            this.lblISSN.Size = new System.Drawing.Size(41, 16);
            this.lblISSN.TabIndex = 0;
            this.lblISSN.Text = "ISSN:";
            // 
            // PeriodicalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Name = "PeriodicalForm";
            this.panelMain.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.panelContent.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelFieldsContainer.ResumeLayout(false);
            this.panelFieldsContainer.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion
    }
}