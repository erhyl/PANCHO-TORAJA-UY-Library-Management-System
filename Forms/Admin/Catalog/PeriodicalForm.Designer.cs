namespace Project5LMS.Forms.Admin.Catalog
{
    partial class PeriodicalForm
    {
        private System.ComponentModel.IContainer components = null;
        private new System.Windows.Forms.Panel panelMain;
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
        private new System.Windows.Forms.Button btnSave;
        private new System.Windows.Forms.Button btnCancel;
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
            this.panelMain = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
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
            this.SuspendLayout();
            this.panelMain.Controls.Add(this.btnCancel);
            this.panelMain.Controls.Add(this.btnSave);
            this.panelMain.Controls.Add(this.cmbFrequency);
            this.panelMain.Controls.Add(this.lblFrequency);
            this.panelMain.Controls.Add(this.dtpPublicationDate);
            this.panelMain.Controls.Add(this.lblPubDate);
            this.panelMain.Controls.Add(this.txtIssue);
            this.panelMain.Controls.Add(this.lblIssue);
            this.panelMain.Controls.Add(this.txtVolume);
            this.panelMain.Controls.Add(this.lblVolume);
            this.panelMain.Controls.Add(this.txtISSN);
            this.panelMain.Controls.Add(this.lblISSN);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(20);
            this.panelMain.Size = new System.Drawing.Size(500, 350);
            this.panelMain.TabIndex = 0;
            this.btnCancel.BackColor = System.Drawing.Color.Maroon;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(130, 278);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 49);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnSave.BackColor = System.Drawing.Color.Maroon;
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(330, 278);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 49);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
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
            this.cmbFrequency.Location = new System.Drawing.Point(134, 210);
            this.cmbFrequency.Name = "cmbFrequency";
            this.cmbFrequency.Size = new System.Drawing.Size(296, 24);
            this.cmbFrequency.TabIndex = 9;
            this.lblFrequency.AutoSize = true;
            this.lblFrequency.Location = new System.Drawing.Point(20, 210);
            this.lblFrequency.Name = "lblFrequency";
            this.lblFrequency.Size = new System.Drawing.Size(74, 16);
            this.lblFrequency.TabIndex = 8;
            this.lblFrequency.Text = "Frequency:";
            this.dtpPublicationDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPublicationDate.Location = new System.Drawing.Point(134, 167);
            this.dtpPublicationDate.Name = "dtpPublicationDate";
            this.dtpPublicationDate.Size = new System.Drawing.Size(296, 22);
            this.dtpPublicationDate.TabIndex = 7;
            this.lblPubDate.AutoSize = true;
            this.lblPubDate.Location = new System.Drawing.Point(20, 172);
            this.lblPubDate.Name = "lblPubDate";
            this.lblPubDate.Size = new System.Drawing.Size(108, 16);
            this.lblPubDate.TabIndex = 6;
            this.lblPubDate.Text = "Publication Date:";
            this.txtIssue.Location = new System.Drawing.Point(130, 97);
            this.txtIssue.Multiline = true;
            this.txtIssue.Name = "txtIssue";
            this.txtIssue.Size = new System.Drawing.Size(300, 48);
            this.txtIssue.TabIndex = 5;
            this.lblIssue.AutoSize = true;
            this.lblIssue.Location = new System.Drawing.Point(20, 100);
            this.lblIssue.Name = "lblIssue";
            this.lblIssue.Size = new System.Drawing.Size(42, 16);
            this.lblIssue.TabIndex = 4;
            this.lblIssue.Text = "Issue:";
            this.txtVolume.Location = new System.Drawing.Point(130, 57);
            this.txtVolume.Name = "txtVolume";
            this.txtVolume.Size = new System.Drawing.Size(300, 22);
            this.txtVolume.TabIndex = 3;
            this.lblVolume.AutoSize = true;
            this.lblVolume.Location = new System.Drawing.Point(20, 60);
            this.lblVolume.Name = "lblVolume";
            this.lblVolume.Size = new System.Drawing.Size(56, 16);
            this.lblVolume.TabIndex = 2;
            this.lblVolume.Text = "Volume:";
            this.txtISSN.Location = new System.Drawing.Point(130, 17);
            this.txtISSN.Name = "txtISSN";
            this.txtISSN.Size = new System.Drawing.Size(300, 22);
            this.txtISSN.TabIndex = 1;
            this.lblISSN.AutoSize = true;
            this.lblISSN.Location = new System.Drawing.Point(20, 20);
            this.lblISSN.Name = "lblISSN";
            this.lblISSN.Size = new System.Drawing.Size(41, 16);
            this.lblISSN.TabIndex = 0;
            this.lblISSN.Text = "ISSN:";
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 350);
            this.Controls.Add(this.panelMain);
            this.Name = "PeriodicalForm";
            this.Text = "Add Periodical";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);
        }
        #endregion
    }
}