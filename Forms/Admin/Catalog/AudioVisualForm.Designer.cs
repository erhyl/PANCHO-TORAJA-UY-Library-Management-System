namespace Project5LMS.Forms.Admin.Catalog
{
    partial class AudioVisualForm
    {
        private System.ComponentModel.IContainer components = null;
        private new System.Windows.Forms.Panel panelMain;
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
            this.SuspendLayout();
            this.panelMain.Controls.Add(this.btnCancel);
            this.panelMain.Controls.Add(this.btnSave);
            this.panelMain.Controls.Add(this.cmbRating);
            this.panelMain.Controls.Add(this.lblRating);
            this.panelMain.Controls.Add(this.chkHasSubtitles);
            this.panelMain.Controls.Add(this.cmbAudioLanguage);
            this.panelMain.Controls.Add(this.lblAudioLang);
            this.panelMain.Controls.Add(this.cmbFormat);
            this.panelMain.Controls.Add(this.lblFormat);
            this.panelMain.Controls.Add(this.txtDuration);
            this.panelMain.Controls.Add(this.lblDuration);
            this.panelMain.Controls.Add(this.cmbMediaType);
            this.panelMain.Controls.Add(this.lblMediaType);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(20);
            this.panelMain.Size = new System.Drawing.Size(500, 400);
            this.panelMain.TabIndex = 0;
            this.btnCancel.BackColor = System.Drawing.Color.Maroon;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(143, 320);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(136, 57);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnSave.BackColor = System.Drawing.Color.Maroon;
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(310, 320);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(133, 57);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            this.cmbRating.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRating.FormattingEnabled = true;
            this.cmbRating.Items.AddRange(new object[] {
            "G",
            "PG",
            "PG-13",
            "R",
            "NC-17",
            "NR"});
            this.cmbRating.Location = new System.Drawing.Point(143, 217);
            this.cmbRating.Name = "cmbRating";
            this.cmbRating.Size = new System.Drawing.Size(300, 24);
            this.cmbRating.TabIndex = 10;
            this.lblRating.AutoSize = true;
            this.lblRating.Location = new System.Drawing.Point(20, 220);
            this.lblRating.Name = "lblRating";
            this.lblRating.Size = new System.Drawing.Size(49, 16);
            this.lblRating.TabIndex = 9;
            this.lblRating.Text = "Rating:";
            this.chkHasSubtitles.AutoSize = true;
            this.chkHasSubtitles.Location = new System.Drawing.Point(143, 179);
            this.chkHasSubtitles.Name = "chkHasSubtitles";
            this.chkHasSubtitles.Size = new System.Drawing.Size(108, 20);
            this.chkHasSubtitles.TabIndex = 8;
            this.chkHasSubtitles.Text = "Has Subtitles";
            this.chkHasSubtitles.UseVisualStyleBackColor = true;
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
            this.lblAudioLang.AutoSize = true;
            this.lblAudioLang.Location = new System.Drawing.Point(20, 140);
            this.lblAudioLang.Name = "lblAudioLang";
            this.lblAudioLang.Size = new System.Drawing.Size(109, 16);
            this.lblAudioLang.TabIndex = 6;
            this.lblAudioLang.Text = "Audio Language:";
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
            this.lblFormat.AutoSize = true;
            this.lblFormat.Location = new System.Drawing.Point(20, 100);
            this.lblFormat.Name = "lblFormat";
            this.lblFormat.Size = new System.Drawing.Size(52, 16);
            this.lblFormat.TabIndex = 4;
            this.lblFormat.Text = "Format:";
            this.txtDuration.Location = new System.Drawing.Point(143, 60);
            this.txtDuration.Name = "txtDuration";
            this.txtDuration.Size = new System.Drawing.Size(300, 22);
            this.txtDuration.TabIndex = 3;
            this.lblDuration.AutoSize = true;
            this.lblDuration.Location = new System.Drawing.Point(20, 60);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(117, 16);
            this.lblDuration.TabIndex = 2;
            this.lblDuration.Text = "Duration (minutes):";
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
            this.lblMediaType.AutoSize = true;
            this.lblMediaType.Location = new System.Drawing.Point(20, 20);
            this.lblMediaType.Name = "lblMediaType";
            this.lblMediaType.Size = new System.Drawing.Size(83, 16);
            this.lblMediaType.TabIndex = 0;
            this.lblMediaType.Text = "Media Type:";
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 400);
            this.Controls.Add(this.panelMain);
            this.Name = "AudioVisualForm";
            this.Text = "Add Audio-Visual Material";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);
        }
        #endregion
    }
}