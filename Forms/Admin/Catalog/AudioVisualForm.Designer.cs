namespace Project5LMS.Forms.Admin.Catalog
{
    partial class AudioVisualForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblMediaType;
        private System.Windows.Forms.ComboBox cmbMediaType;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.TextBox txtDuration;
        private System.Windows.Forms.Label lblFormat;
        private System.Windows.Forms.ComboBox cmbFormat;
        private System.Windows.Forms.Label lblAudioLang;
        private System.Windows.Forms.ComboBox cmbAudioLanguage;
        private System.Windows.Forms.CheckBox chkHasSubtitles;
        private System.Windows.Forms.Label lblRating;
        private System.Windows.Forms.ComboBox cmbRating;
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
            this.cmbRating = new System.Windows.Forms.ComboBox();
            this.lblRating = new System.Windows.Forms.Label();
            this.chkHasSubtitles = new System.Windows.Forms.CheckBox();
            this.cmbAudioLanguage = new System.Windows.Forms.ComboBox();
            this.lblAudioLang = new System.Windows.Forms.Label();
            this.cmbFormat = new System.Windows.Forms.ComboBox();
            this.lblFormat = new System.Windows.Forms.Label();
            this.txtDuration = new System.Windows.Forms.TextBox();
            this.lblDuration = new System.Windows.Forms.Label();
            this.cmbMediaType = new System.Windows.Forms.ComboBox();
            this.lblMediaType = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panelFieldsContainer.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelFieldsContainer
            // 
            this.panelFieldsContainer.Controls.Add(this.cmbRating);
            this.panelFieldsContainer.Controls.Add(this.lblRating);
            this.panelFieldsContainer.Controls.Add(this.chkHasSubtitles);
            this.panelFieldsContainer.Controls.Add(this.cmbAudioLanguage);
            this.panelFieldsContainer.Controls.Add(this.lblAudioLang);
            this.panelFieldsContainer.Controls.Add(this.cmbFormat);
            this.panelFieldsContainer.Controls.Add(this.lblFormat);
            this.panelFieldsContainer.Controls.Add(this.txtDuration);
            this.panelFieldsContainer.Controls.Add(this.lblDuration);
            this.panelFieldsContainer.Controls.Add(this.cmbMediaType);
            this.panelFieldsContainer.Controls.Add(this.lblMediaType);
            // 
            // cmbRating
            // 
            this.cmbRating.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRating.FormattingEnabled = true;
            this.cmbRating.Items.AddRange(new object[] {
            "G",
            "PG",
            "PG-13",
            "R",
            "NC-17",
            "NR"});
            this.cmbRating.Location = new System.Drawing.Point(143, 212);
            this.cmbRating.Name = "cmbRating";
            this.cmbRating.Size = new System.Drawing.Size(300, 24);
            this.cmbRating.TabIndex = 10;
            // 
            // lblRating
            // 
            this.lblRating.AutoSize = true;
            this.lblRating.Location = new System.Drawing.Point(20, 220);
            this.lblRating.Name = "lblRating";
            this.lblRating.Size = new System.Drawing.Size(49, 16);
            this.lblRating.TabIndex = 9;
            this.lblRating.Text = "Rating:";
            // 
            // chkHasSubtitles
            // 
            this.chkHasSubtitles.AutoSize = true;
            this.chkHasSubtitles.Location = new System.Drawing.Point(143, 179);
            this.chkHasSubtitles.Name = "chkHasSubtitles";
            this.chkHasSubtitles.Size = new System.Drawing.Size(108, 20);
            this.chkHasSubtitles.TabIndex = 8;
            this.chkHasSubtitles.Text = "Has Subtitles";
            this.chkHasSubtitles.UseVisualStyleBackColor = true;
            // 
            // cmbAudioLanguage
            // 
            this.cmbAudioLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAudioLanguage.FormattingEnabled = true;
            this.cmbAudioLanguage.Items.AddRange(new object[] {
            "English",
            "Spanish",
            "French",
            "German",
            "Chinese",
            "Japanese",
            "Korean",
            "Other"});
            this.cmbAudioLanguage.Location = new System.Drawing.Point(143, 137);
            this.cmbAudioLanguage.Name = "cmbAudioLanguage";
            this.cmbAudioLanguage.Size = new System.Drawing.Size(300, 24);
            this.cmbAudioLanguage.TabIndex = 7;
            // 
            // lblAudioLang
            // 
            this.lblAudioLang.AutoSize = true;
            this.lblAudioLang.Location = new System.Drawing.Point(20, 140);
            this.lblAudioLang.Name = "lblAudioLang";
            this.lblAudioLang.Size = new System.Drawing.Size(109, 16);
            this.lblAudioLang.TabIndex = 6;
            this.lblAudioLang.Text = "Audio Language:";
            // 
            // cmbFormat
            // 
            this.cmbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormat.FormattingEnabled = true;
            this.cmbFormat.Items.AddRange(new object[] {
            "NTSC",
            "PAL",
            "SECAM",
            "Digital",
            "Other"});
            this.cmbFormat.Location = new System.Drawing.Point(143, 97);
            this.cmbFormat.Name = "cmbFormat";
            this.cmbFormat.Size = new System.Drawing.Size(300, 24);
            this.cmbFormat.TabIndex = 5;
            // 
            // lblFormat
            // 
            this.lblFormat.AutoSize = true;
            this.lblFormat.Location = new System.Drawing.Point(20, 100);
            this.lblFormat.Name = "lblFormat";
            this.lblFormat.Size = new System.Drawing.Size(52, 16);
            this.lblFormat.TabIndex = 4;
            this.lblFormat.Text = "Format:";
            // 
            // txtDuration
            // 
            this.txtDuration.Location = new System.Drawing.Point(143, 60);
            this.txtDuration.Name = "txtDuration";
            this.txtDuration.Size = new System.Drawing.Size(300, 22);
            this.txtDuration.TabIndex = 3;
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.Location = new System.Drawing.Point(20, 60);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(117, 16);
            this.lblDuration.TabIndex = 2;
            this.lblDuration.Text = "Duration (minutes):";
            // 
            // cmbMediaType
            // 
            this.cmbMediaType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMediaType.FormattingEnabled = true;
            this.cmbMediaType.Items.AddRange(new object[] {
            "DVD",
            "CD",
            "Blu-ray",
            "VHS",
            "Digital",
            "Other"});
            this.cmbMediaType.Location = new System.Drawing.Point(143, 20);
            this.cmbMediaType.Name = "cmbMediaType";
            this.cmbMediaType.Size = new System.Drawing.Size(300, 24);
            this.cmbMediaType.TabIndex = 1;
            // 
            // lblMediaType
            // 
            this.lblMediaType.AutoSize = true;
            this.lblMediaType.Location = new System.Drawing.Point(20, 20);
            this.lblMediaType.Name = "lblMediaType";
            this.lblMediaType.Size = new System.Drawing.Size(83, 16);
            this.lblMediaType.TabIndex = 0;
            this.lblMediaType.Text = "Media Type:";
            // 
            // AudioVisualForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Name = "AudioVisualForm";
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