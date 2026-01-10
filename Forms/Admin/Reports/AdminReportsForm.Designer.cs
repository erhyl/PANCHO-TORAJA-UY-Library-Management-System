namespace Project5LMS.Forms.Admin.Reports
{
    partial class AdminReportsForm
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
            this.panelContent = new System.Windows.Forms.Panel();
            this.lblContentPlaceholder = new System.Windows.Forms.Label();
            this.panelDateFilter = new System.Windows.Forms.Panel();
            this.cmbDateRange = new System.Windows.Forms.ComboBox();
            this.panelReportButtons = new System.Windows.Forms.Panel();
            this.btnExportReport = new System.Windows.Forms.Button();
            this.btnStatisticalReports = new System.Windows.Forms.Button();
            this.btnFinancialReports = new System.Windows.Forms.Button();
            this.btnCollectionReports = new System.Windows.Forms.Button();
            this.btnMemberReports = new System.Windows.Forms.Button();
            this.btnCirculationReports = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelMainContainer.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.panelDateFilter.SuspendLayout();
            this.panelReportButtons.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMainContainer
            // 
            this.panelMainContainer.AutoScroll = true;
            this.panelMainContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelMainContainer.Controls.Add(this.panelContent);
            this.panelMainContainer.Controls.Add(this.panelDateFilter);
            this.panelMainContainer.Controls.Add(this.panelReportButtons);
            this.panelMainContainer.Controls.Add(this.panelHeader);
            this.panelMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainContainer.Location = new System.Drawing.Point(0, 0);
            this.panelMainContainer.Name = "panelMainContainer";
            this.panelMainContainer.Padding = new System.Windows.Forms.Padding(24, 24, 24, 24);
            this.panelMainContainer.Size = new System.Drawing.Size(1200, 800);
            this.panelMainContainer.TabIndex = 0;
            // 
            // panelContent
            // 
            this.panelContent.BackColor = System.Drawing.Color.White;
            this.panelContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelContent.Controls.Add(this.lblContentPlaceholder);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(24, 184);
            this.panelContent.Name = "panelContent";
            this.panelContent.Padding = new System.Windows.Forms.Padding(16, 16, 16, 16);
            this.panelContent.Size = new System.Drawing.Size(1152, 592);
            this.panelContent.TabIndex = 3;
            // 
            // lblContentPlaceholder
            // 
            this.lblContentPlaceholder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblContentPlaceholder.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContentPlaceholder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblContentPlaceholder.Location = new System.Drawing.Point(16, 16);
            this.lblContentPlaceholder.Name = "lblContentPlaceholder";
            this.lblContentPlaceholder.Size = new System.Drawing.Size(1120, 559);
            this.lblContentPlaceholder.TabIndex = 0;
            this.lblContentPlaceholder.Text = "Report content will be displayed here.\r\nSelect a report category above to view st" +
    "atistics and data.";
            this.lblContentPlaceholder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelDateFilter
            // 
            this.panelDateFilter.Controls.Add(this.cmbDateRange);
            this.panelDateFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDateFilter.Location = new System.Drawing.Point(24, 146);
            this.panelDateFilter.Name = "panelDateFilter";
            this.panelDateFilter.Size = new System.Drawing.Size(1152, 38);
            this.panelDateFilter.TabIndex = 1;
            // 
            // cmbDateRange
            // 
            this.cmbDateRange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDateRange.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDateRange.FormattingEnabled = true;
            this.cmbDateRange.Location = new System.Drawing.Point(14, 6);
            this.cmbDateRange.Name = "cmbDateRange";
            this.cmbDateRange.Size = new System.Drawing.Size(151, 25);
            this.cmbDateRange.TabIndex = 0;
            this.cmbDateRange.SelectedIndexChanged += new System.EventHandler(this.cmbDateRange_SelectedIndexChanged);
            // 
            // panelReportButtons
            // 
            this.panelReportButtons.Controls.Add(this.btnExportReport);
            this.panelReportButtons.Controls.Add(this.btnStatisticalReports);
            this.panelReportButtons.Controls.Add(this.btnFinancialReports);
            this.panelReportButtons.Controls.Add(this.btnCollectionReports);
            this.panelReportButtons.Controls.Add(this.btnMemberReports);
            this.panelReportButtons.Controls.Add(this.btnCirculationReports);
            this.panelReportButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelReportButtons.Location = new System.Drawing.Point(24, 97);
            this.panelReportButtons.Name = "panelReportButtons";
            this.panelReportButtons.Size = new System.Drawing.Size(1152, 49);
            this.panelReportButtons.TabIndex = 2;
            // 
            // btnExportReport
            // 
            this.btnExportReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportReport.BackColor = System.Drawing.Color.Green;
            this.btnExportReport.FlatAppearance.BorderSize = 0;
            this.btnExportReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportReport.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportReport.ForeColor = System.Drawing.Color.White;
            this.btnExportReport.Location = new System.Drawing.Point(978, 6);
            this.btnExportReport.Name = "btnExportReport";
            this.btnExportReport.Size = new System.Drawing.Size(156, 34);
            this.btnExportReport.TabIndex = 2;
            this.btnExportReport.Text = "Export Report";
            this.btnExportReport.UseVisualStyleBackColor = false;
            this.btnExportReport.Click += new System.EventHandler(this.btnExportReport_Click);
            // 
            // btnStatisticalReports
            // 
            this.btnStatisticalReports.BackColor = System.Drawing.Color.Maroon;
            this.btnStatisticalReports.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnStatisticalReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStatisticalReports.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStatisticalReports.ForeColor = System.Drawing.Color.White;
            this.btnStatisticalReports.Location = new System.Drawing.Point(591, 0);
            this.btnStatisticalReports.Name = "btnStatisticalReports";
            this.btnStatisticalReports.Size = new System.Drawing.Size(150, 49);
            this.btnStatisticalReports.TabIndex = 4;
            this.btnStatisticalReports.Text = "📋 Statistical Reports";
            this.btnStatisticalReports.UseVisualStyleBackColor = false;
            this.btnStatisticalReports.Click += new System.EventHandler(this.btnStatisticalReports_Click);
            // 
            // btnFinancialReports
            // 
            this.btnFinancialReports.BackColor = System.Drawing.Color.Maroon;
            this.btnFinancialReports.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnFinancialReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFinancialReports.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFinancialReports.ForeColor = System.Drawing.Color.White;
            this.btnFinancialReports.Location = new System.Drawing.Point(444, 0);
            this.btnFinancialReports.Name = "btnFinancialReports";
            this.btnFinancialReports.Size = new System.Drawing.Size(150, 49);
            this.btnFinancialReports.TabIndex = 3;
            this.btnFinancialReports.Text = "📋 Financial Reports";
            this.btnFinancialReports.UseVisualStyleBackColor = false;
            this.btnFinancialReports.Click += new System.EventHandler(this.btnFinancialReports_Click);
            // 
            // btnCollectionReports
            // 
            this.btnCollectionReports.BackColor = System.Drawing.Color.Maroon;
            this.btnCollectionReports.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnCollectionReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCollectionReports.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCollectionReports.ForeColor = System.Drawing.Color.White;
            this.btnCollectionReports.Location = new System.Drawing.Point(298, 0);
            this.btnCollectionReports.Name = "btnCollectionReports";
            this.btnCollectionReports.Size = new System.Drawing.Size(150, 49);
            this.btnCollectionReports.TabIndex = 2;
            this.btnCollectionReports.Text = "📋 Collection Reports";
            this.btnCollectionReports.UseVisualStyleBackColor = false;
            this.btnCollectionReports.Click += new System.EventHandler(this.btnCollectionReports_Click);
            // 
            // btnMemberReports
            // 
            this.btnMemberReports.BackColor = System.Drawing.Color.Maroon;
            this.btnMemberReports.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnMemberReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMemberReports.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMemberReports.ForeColor = System.Drawing.Color.White;
            this.btnMemberReports.Location = new System.Drawing.Point(148, 0);
            this.btnMemberReports.Name = "btnMemberReports";
            this.btnMemberReports.Size = new System.Drawing.Size(150, 49);
            this.btnMemberReports.TabIndex = 1;
            this.btnMemberReports.Text = "📋 Member Reports";
            this.btnMemberReports.UseVisualStyleBackColor = false;
            this.btnMemberReports.Click += new System.EventHandler(this.btnMemberReports_Click);
            // 
            // btnCirculationReports
            // 
            this.btnCirculationReports.BackColor = System.Drawing.Color.Maroon;
            this.btnCirculationReports.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.btnCirculationReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCirculationReports.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCirculationReports.ForeColor = System.Drawing.Color.White;
            this.btnCirculationReports.Location = new System.Drawing.Point(0, 0);
            this.btnCirculationReports.Name = "btnCirculationReports";
            this.btnCirculationReports.Size = new System.Drawing.Size(151, 49);
            this.btnCirculationReports.TabIndex = 0;
            this.btnCirculationReports.Text = "📋 Circulation Reports";
            this.btnCirculationReports.UseVisualStyleBackColor = false;
            this.btnCirculationReports.Click += new System.EventHandler(this.btnCirculationReports_Click);
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.lblSubtitle);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(24, 24);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1152, 73);
            this.panelHeader.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblSubtitle.Location = new System.Drawing.Point(11, 44);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(213, 19);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Generate and view library reports";
            this.lblSubtitle.Click += new System.EventHandler(this.lblSubtitle_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitle.Location = new System.Drawing.Point(3, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(346, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Reports and Analytics";
            // 
            // AdminReportsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.panelMainContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AdminReportsForm";
            this.Text = "Reports & Analytics";
            this.Load += new System.EventHandler(this.AdminReportsForm_Load);
            this.panelMainContainer.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.panelDateFilter.ResumeLayout(false);
            this.panelReportButtons.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.Panel panelMainContainer;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Button btnExportReport;
        private System.Windows.Forms.Panel panelReportButtons;
        private System.Windows.Forms.Button btnCirculationReports;
        private System.Windows.Forms.Button btnMemberReports;
        private System.Windows.Forms.Button btnCollectionReports;
        private System.Windows.Forms.Button btnFinancialReports;
        private System.Windows.Forms.Button btnStatisticalReports;
        private System.Windows.Forms.Panel panelDateFilter;
        private System.Windows.Forms.ComboBox cmbDateRange;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Label lblContentPlaceholder;
    }
}