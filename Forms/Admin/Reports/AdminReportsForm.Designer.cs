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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tabControlReports = new System.Windows.Forms.TabControl();
            this.tabPageCirculation = new System.Windows.Forms.TabPage();
            this.panelContentCirculation = new System.Windows.Forms.Panel();
            this.tabPageMember = new System.Windows.Forms.TabPage();
            this.panelContentMember = new System.Windows.Forms.Panel();
            this.tabPageCollection = new System.Windows.Forms.TabPage();
            this.panelContentCollection = new System.Windows.Forms.Panel();
            this.tabPageFinancial = new System.Windows.Forms.TabPage();
            this.panelContentFinancial = new System.Windows.Forms.Panel();
            this.tabPageStatistical = new System.Windows.Forms.TabPage();
            this.panelContentStatistical = new System.Windows.Forms.Panel();
            this.panelDateFilter = new System.Windows.Forms.Panel();
            this.btnExportReport = new System.Windows.Forms.Button();
            this.cmbDateRange = new System.Windows.Forms.ComboBox();
            this.panelMainContainer = new System.Windows.Forms.Panel();
            this.panelHeader.SuspendLayout();
            this.tabControlReports.SuspendLayout();
            this.tabPageCirculation.SuspendLayout();
            this.tabPageMember.SuspendLayout();
            this.tabPageCollection.SuspendLayout();
            this.tabPageFinancial.SuspendLayout();
            this.tabPageStatistical.SuspendLayout();
            this.panelDateFilter.SuspendLayout();
            this.panelMainContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.lblSubtitle);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(24, 24);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1152, 70);
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
            // tabControlReports
            // 
            this.tabControlReports.Controls.Add(this.tabPageCirculation);
            this.tabControlReports.Controls.Add(this.tabPageMember);
            this.tabControlReports.Controls.Add(this.tabPageCollection);
            this.tabControlReports.Controls.Add(this.tabPageFinancial);
            this.tabControlReports.Controls.Add(this.tabPageStatistical);
            this.tabControlReports.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlReports.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlReports.Location = new System.Drawing.Point(24, 111);
            this.tabControlReports.Name = "tabControlReports";
            this.tabControlReports.SelectedIndex = 0;
            this.tabControlReports.Size = new System.Drawing.Size(1152, 665);
            this.tabControlReports.TabIndex = 2;
            this.tabControlReports.SelectedIndexChanged += new System.EventHandler(this.tabControlReports_SelectedIndexChanged);
            // 
            // tabPageCirculation
            // 
            this.tabPageCirculation.Controls.Add(this.panelContentCirculation);
            this.tabPageCirculation.Location = new System.Drawing.Point(4, 26);
            this.tabPageCirculation.Name = "tabPageCirculation";
            this.tabPageCirculation.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCirculation.Size = new System.Drawing.Size(1144, 635);
            this.tabPageCirculation.TabIndex = 0;
            this.tabPageCirculation.Text = "🔄 Circulation";
            this.tabPageCirculation.UseVisualStyleBackColor = true;
            // 
            // panelContentCirculation
            // 
            this.panelContentCirculation.BackColor = System.Drawing.Color.White;
            this.panelContentCirculation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContentCirculation.Location = new System.Drawing.Point(3, 3);
            this.panelContentCirculation.Name = "panelContentCirculation";
            this.panelContentCirculation.Padding = new System.Windows.Forms.Padding(16);
            this.panelContentCirculation.Size = new System.Drawing.Size(1138, 629);
            this.panelContentCirculation.TabIndex = 0;
            // 
            // tabPageMember
            // 
            this.tabPageMember.Controls.Add(this.panelContentMember);
            this.tabPageMember.Location = new System.Drawing.Point(4, 26);
            this.tabPageMember.Name = "tabPageMember";
            this.tabPageMember.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMember.Size = new System.Drawing.Size(1144, 635);
            this.tabPageMember.TabIndex = 1;
            this.tabPageMember.Text = "👥 Member";
            this.tabPageMember.UseVisualStyleBackColor = true;
            // 
            // panelContentMember
            // 
            this.panelContentMember.BackColor = System.Drawing.Color.White;
            this.panelContentMember.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContentMember.Location = new System.Drawing.Point(3, 3);
            this.panelContentMember.Name = "panelContentMember";
            this.panelContentMember.Padding = new System.Windows.Forms.Padding(16);
            this.panelContentMember.Size = new System.Drawing.Size(1138, 629);
            this.panelContentMember.TabIndex = 0;
            // 
            // tabPageCollection
            // 
            this.tabPageCollection.Controls.Add(this.panelContentCollection);
            this.tabPageCollection.Location = new System.Drawing.Point(4, 26);
            this.tabPageCollection.Name = "tabPageCollection";
            this.tabPageCollection.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCollection.Size = new System.Drawing.Size(1144, 635);
            this.tabPageCollection.TabIndex = 2;
            this.tabPageCollection.Text = "📚 Collection";
            this.tabPageCollection.UseVisualStyleBackColor = true;
            // 
            // panelContentCollection
            // 
            this.panelContentCollection.BackColor = System.Drawing.Color.White;
            this.panelContentCollection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContentCollection.Location = new System.Drawing.Point(3, 3);
            this.panelContentCollection.Name = "panelContentCollection";
            this.panelContentCollection.Padding = new System.Windows.Forms.Padding(16);
            this.panelContentCollection.Size = new System.Drawing.Size(1138, 629);
            this.panelContentCollection.TabIndex = 0;
            // 
            // tabPageFinancial
            // 
            this.tabPageFinancial.Controls.Add(this.panelContentFinancial);
            this.tabPageFinancial.Location = new System.Drawing.Point(4, 26);
            this.tabPageFinancial.Name = "tabPageFinancial";
            this.tabPageFinancial.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFinancial.Size = new System.Drawing.Size(1144, 635);
            this.tabPageFinancial.TabIndex = 3;
            this.tabPageFinancial.Text = "💰 Financial";
            this.tabPageFinancial.UseVisualStyleBackColor = true;
            // 
            // panelContentFinancial
            // 
            this.panelContentFinancial.BackColor = System.Drawing.Color.White;
            this.panelContentFinancial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContentFinancial.Location = new System.Drawing.Point(3, 3);
            this.panelContentFinancial.Name = "panelContentFinancial";
            this.panelContentFinancial.Padding = new System.Windows.Forms.Padding(16);
            this.panelContentFinancial.Size = new System.Drawing.Size(1138, 629);
            this.panelContentFinancial.TabIndex = 0;
            // 
            // tabPageStatistical
            // 
            this.tabPageStatistical.Controls.Add(this.panelContentStatistical);
            this.tabPageStatistical.Location = new System.Drawing.Point(4, 26);
            this.tabPageStatistical.Name = "tabPageStatistical";
            this.tabPageStatistical.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStatistical.Size = new System.Drawing.Size(1144, 635);
            this.tabPageStatistical.TabIndex = 4;
            this.tabPageStatistical.Text = "📊 Statistical";
            this.tabPageStatistical.UseVisualStyleBackColor = true;
            // 
            // panelContentStatistical
            // 
            this.panelContentStatistical.BackColor = System.Drawing.Color.White;
            this.panelContentStatistical.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContentStatistical.Location = new System.Drawing.Point(3, 3);
            this.panelContentStatistical.Name = "panelContentStatistical";
            this.panelContentStatistical.Padding = new System.Windows.Forms.Padding(16);
            this.panelContentStatistical.Size = new System.Drawing.Size(1138, 629);
            this.panelContentStatistical.TabIndex = 0;
            // 
            // panelDateFilter
            // 
            this.panelDateFilter.Controls.Add(this.btnExportReport);
            this.panelDateFilter.Controls.Add(this.cmbDateRange);
            this.panelDateFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDateFilter.Location = new System.Drawing.Point(24, 94);
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
            // btnExportReport
            // 
            this.btnExportReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportReport.BackColor = System.Drawing.Color.Green;
            this.btnExportReport.FlatAppearance.BorderSize = 0;
            this.btnExportReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportReport.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportReport.ForeColor = System.Drawing.Color.White;
            this.btnExportReport.Location = new System.Drawing.Point(978, 4);
            this.btnExportReport.Name = "btnExportReport";
            this.btnExportReport.Size = new System.Drawing.Size(156, 30);
            this.btnExportReport.TabIndex = 1;
            this.btnExportReport.Text = "📤 Export Report";
            this.btnExportReport.UseVisualStyleBackColor = false;
            this.btnExportReport.Click += new System.EventHandler(this.btnExportReport_Click);
            // 
            // panelMainContainer
            // 
            this.panelMainContainer.AutoScroll = true;
            this.panelMainContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelMainContainer.Controls.Add(this.tabControlReports);
            this.panelMainContainer.Controls.Add(this.panelDateFilter);
            this.panelMainContainer.Controls.Add(this.panelHeader);
            this.panelMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainContainer.Location = new System.Drawing.Point(0, 0);
            this.panelMainContainer.Name = "panelMainContainer";
            this.panelMainContainer.Padding = new System.Windows.Forms.Padding(24);
            this.panelMainContainer.Size = new System.Drawing.Size(1200, 800);
            this.panelMainContainer.TabIndex = 0;
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
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.tabControlReports.ResumeLayout(false);
            this.tabPageCirculation.ResumeLayout(false);
            this.tabPageMember.ResumeLayout(false);
            this.tabPageCollection.ResumeLayout(false);
            this.tabPageFinancial.ResumeLayout(false);
            this.tabPageStatistical.ResumeLayout(false);
            this.panelDateFilter.ResumeLayout(false);
            this.panelMainContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TabControl tabControlReports;
        private System.Windows.Forms.TabPage tabPageCirculation;
        private System.Windows.Forms.Panel panelContentCirculation;
        private System.Windows.Forms.TabPage tabPageMember;
        private System.Windows.Forms.Panel panelContentMember;
        private System.Windows.Forms.TabPage tabPageCollection;
        private System.Windows.Forms.Panel panelContentCollection;
        private System.Windows.Forms.TabPage tabPageFinancial;
        private System.Windows.Forms.Panel panelContentFinancial;
        private System.Windows.Forms.TabPage tabPageStatistical;
        private System.Windows.Forms.Panel panelContentStatistical;
        private System.Windows.Forms.Panel panelDateFilter;
        private System.Windows.Forms.ComboBox cmbDateRange;
        private System.Windows.Forms.Button btnExportReport;
        private System.Windows.Forms.Panel panelMainContainer;
    }
}