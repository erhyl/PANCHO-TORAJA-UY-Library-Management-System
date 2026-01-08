namespace Project5LMS.Forms.Admin.Fines
{
    public partial class AdminFinesForm
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
            this.panelMainContainer = new System.Windows.Forms.Panel();
            this.panelTableContainer = new System.Windows.Forms.Panel();
            this.dataGridViewFines = new System.Windows.Forms.DataGridView();
            this.panelFilters = new System.Windows.Forms.Panel();
            this.btnFilterType = new System.Windows.Forms.Button();
            this.btnFilterStatus = new System.Windows.Forms.Button();
            this.panelMetrics = new System.Windows.Forms.Panel();
            this.panelMetricTotalFines = new System.Windows.Forms.Panel();
            this.lblMetricTotalFinesValue = new System.Windows.Forms.Label();
            this.lblMetricTotalFinesTitle = new System.Windows.Forms.Label();
            this.panelMetricWaived = new System.Windows.Forms.Panel();
            this.lblMetricWaivedValue = new System.Windows.Forms.Label();
            this.lblMetricWaivedTitle = new System.Windows.Forms.Label();
            this.panelMetricCollected = new System.Windows.Forms.Panel();
            this.lblMetricCollectedValue = new System.Windows.Forms.Label();
            this.lblMetricCollectedTitle = new System.Windows.Forms.Label();
            this.panelMetricPending = new System.Windows.Forms.Panel();
            this.lblMetricPendingValue = new System.Windows.Forms.Label();
            this.lblMetricPendingTitle = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelMainContainer.SuspendLayout();
            this.panelTableContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFines)).BeginInit();
            this.panelFilters.SuspendLayout();
            this.panelMetrics.SuspendLayout();
            this.panelMetricTotalFines.SuspendLayout();
            this.panelMetricWaived.SuspendLayout();
            this.panelMetricCollected.SuspendLayout();
            this.panelMetricPending.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMainContainer
            // 
            this.panelMainContainer.AutoScroll = true;
            this.panelMainContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelMainContainer.Controls.Add(this.panelTableContainer);
            this.panelMainContainer.Controls.Add(this.panelFilters);
            this.panelMainContainer.Controls.Add(this.panelMetrics);
            this.panelMainContainer.Controls.Add(this.panelHeader);
            this.panelMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainContainer.Location = new System.Drawing.Point(0, 0);
            this.panelMainContainer.Name = "panelMainContainer";
            this.panelMainContainer.Padding = new System.Windows.Forms.Padding(24, 24, 24, 24);
            this.panelMainContainer.Size = new System.Drawing.Size(1293, 796);
            this.panelMainContainer.TabIndex = 0;
            // 
            // panelTableContainer
            // 
            this.panelTableContainer.BackColor = System.Drawing.Color.White;
            this.panelTableContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTableContainer.Controls.Add(this.dataGridViewFines);
            this.panelTableContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTableContainer.Location = new System.Drawing.Point(24, 218);
            this.panelTableContainer.Name = "panelTableContainer";
            this.panelTableContainer.Padding = new System.Windows.Forms.Padding(16, 16, 16, 16);
            this.panelTableContainer.Size = new System.Drawing.Size(1245, 554);
            this.panelTableContainer.TabIndex = 3;
            // 
            // dataGridViewFines
            // 
            this.dataGridViewFines.AllowUserToAddRows = false;
            this.dataGridViewFines.AllowUserToDeleteRows = false;
            this.dataGridViewFines.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewFines.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewFines.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewFines.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewFines.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dataGridViewFines.Location = new System.Drawing.Point(16, 16);
            this.dataGridViewFines.MultiSelect = false;
            this.dataGridViewFines.Name = "dataGridViewFines";
            this.dataGridViewFines.ReadOnly = true;
            this.dataGridViewFines.RowHeadersVisible = false;
            this.dataGridViewFines.RowHeadersWidth = 51;
            this.dataGridViewFines.RowTemplate.Height = 60;
            this.dataGridViewFines.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewFines.Size = new System.Drawing.Size(1211, 520);
            this.dataGridViewFines.TabIndex = 0;
            // 
            // panelFilters
            // 
            this.panelFilters.Controls.Add(this.btnFilterType);
            this.panelFilters.Controls.Add(this.btnFilterStatus);
            this.panelFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilters.Location = new System.Drawing.Point(24, 183);
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Size = new System.Drawing.Size(1245, 35);
            this.panelFilters.TabIndex = 2;
            // 
            // btnFilterType
            // 
            this.btnFilterType.BackColor = System.Drawing.Color.White;
            this.btnFilterType.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnFilterType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilterType.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilterType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnFilterType.Location = new System.Drawing.Point(1127, 6);
            this.btnFilterType.Name = "btnFilterType";
            this.btnFilterType.Size = new System.Drawing.Size(112, 25);
            this.btnFilterType.TabIndex = 1;
            this.btnFilterType.Text = "🔽 All Types";
            this.btnFilterType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFilterType.UseVisualStyleBackColor = false;
            this.btnFilterType.Click += new System.EventHandler(this.btnFilterType_Click);
            // 
            // btnFilterStatus
            // 
            this.btnFilterStatus.BackColor = System.Drawing.Color.White;
            this.btnFilterStatus.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnFilterStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilterStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilterStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnFilterStatus.Location = new System.Drawing.Point(1009, 6);
            this.btnFilterStatus.Name = "btnFilterStatus";
            this.btnFilterStatus.Size = new System.Drawing.Size(112, 25);
            this.btnFilterStatus.TabIndex = 0;
            this.btnFilterStatus.Text = "🔽 All Status";
            this.btnFilterStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFilterStatus.UseVisualStyleBackColor = false;
            this.btnFilterStatus.Click += new System.EventHandler(this.btnFilterStatus_Click);
            // 
            // panelMetrics
            // 
            this.panelMetrics.BackColor = System.Drawing.Color.White;
            this.panelMetrics.Controls.Add(this.panelMetricTotalFines);
            this.panelMetrics.Controls.Add(this.panelMetricWaived);
            this.panelMetrics.Controls.Add(this.panelMetricCollected);
            this.panelMetrics.Controls.Add(this.panelMetricPending);
            this.panelMetrics.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMetrics.Location = new System.Drawing.Point(24, 113);
            this.panelMetrics.Name = "panelMetrics";
            this.panelMetrics.Size = new System.Drawing.Size(1245, 70);
            this.panelMetrics.TabIndex = 1;
            // 
            // panelMetricTotalFines
            // 
            this.panelMetricTotalFines.BackColor = System.Drawing.Color.White;
            this.panelMetricTotalFines.Controls.Add(this.lblMetricTotalFinesValue);
            this.panelMetricTotalFines.Controls.Add(this.lblMetricTotalFinesTitle);
            this.panelMetricTotalFines.Location = new System.Drawing.Point(911, 0);
            this.panelMetricTotalFines.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.panelMetricTotalFines.Name = "panelMetricTotalFines";
            this.panelMetricTotalFines.Padding = new System.Windows.Forms.Padding(16, 16, 16, 16);
            this.panelMetricTotalFines.Size = new System.Drawing.Size(240, 70);
            this.panelMetricTotalFines.TabIndex = 3;
            // 
            // lblMetricTotalFinesValue
            // 
            this.lblMetricTotalFinesValue.AutoSize = true;
            this.lblMetricTotalFinesValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTotalFinesValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricTotalFinesValue.Location = new System.Drawing.Point(19, 32);
            this.lblMetricTotalFinesValue.Name = "lblMetricTotalFinesValue";
            this.lblMetricTotalFinesValue.Size = new System.Drawing.Size(33, 37);
            this.lblMetricTotalFinesValue.TabIndex = 2;
            this.lblMetricTotalFinesValue.Text = "0";
            // 
            // lblMetricTotalFinesTitle
            // 
            this.lblMetricTotalFinesTitle.AutoSize = true;
            this.lblMetricTotalFinesTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTotalFinesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricTotalFinesTitle.Location = new System.Drawing.Point(12, 12);
            this.lblMetricTotalFinesTitle.Name = "lblMetricTotalFinesTitle";
            this.lblMetricTotalFinesTitle.Size = new System.Drawing.Size(78, 15);
            this.lblMetricTotalFinesTitle.TabIndex = 1;
            this.lblMetricTotalFinesTitle.Text = "💰 Total Fines";
            this.lblMetricTotalFinesTitle.Click += new System.EventHandler(this.lblMetricTotalFinesTitle_Click);
            // 
            // panelMetricWaived
            // 
            this.panelMetricWaived.BackColor = System.Drawing.Color.White;
            this.panelMetricWaived.Controls.Add(this.lblMetricWaivedValue);
            this.panelMetricWaived.Controls.Add(this.lblMetricWaivedTitle);
            this.panelMetricWaived.Location = new System.Drawing.Point(614, 0);
            this.panelMetricWaived.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.panelMetricWaived.Name = "panelMetricWaived";
            this.panelMetricWaived.Padding = new System.Windows.Forms.Padding(16, 16, 16, 16);
            this.panelMetricWaived.Size = new System.Drawing.Size(240, 70);
            this.panelMetricWaived.TabIndex = 2;
            // 
            // lblMetricWaivedValue
            // 
            this.lblMetricWaivedValue.AutoSize = true;
            this.lblMetricWaivedValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricWaivedValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricWaivedValue.Location = new System.Drawing.Point(14, 32);
            this.lblMetricWaivedValue.Name = "lblMetricWaivedValue";
            this.lblMetricWaivedValue.Size = new System.Drawing.Size(33, 37);
            this.lblMetricWaivedValue.TabIndex = 2;
            this.lblMetricWaivedValue.Text = "0";
            this.lblMetricWaivedValue.Click += new System.EventHandler(this.lblMetricWaivedValue_Click);
            // 
            // lblMetricWaivedTitle
            // 
            this.lblMetricWaivedTitle.AutoSize = true;
            this.lblMetricWaivedTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricWaivedTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricWaivedTitle.Location = new System.Drawing.Point(16, 12);
            this.lblMetricWaivedTitle.Name = "lblMetricWaivedTitle";
            this.lblMetricWaivedTitle.Size = new System.Drawing.Size(61, 15);
            this.lblMetricWaivedTitle.TabIndex = 1;
            this.lblMetricWaivedTitle.Text = "❌ Waived";
            this.lblMetricWaivedTitle.Click += new System.EventHandler(this.lblMetricWaivedTitle_Click);
            // 
            // panelMetricCollected
            // 
            this.panelMetricCollected.BackColor = System.Drawing.Color.White;
            this.panelMetricCollected.Controls.Add(this.lblMetricCollectedValue);
            this.panelMetricCollected.Controls.Add(this.lblMetricCollectedTitle);
            this.panelMetricCollected.Location = new System.Drawing.Point(308, 0);
            this.panelMetricCollected.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.panelMetricCollected.Name = "panelMetricCollected";
            this.panelMetricCollected.Padding = new System.Windows.Forms.Padding(16, 16, 16, 16);
            this.panelMetricCollected.Size = new System.Drawing.Size(240, 70);
            this.panelMetricCollected.TabIndex = 1;
            // 
            // lblMetricCollectedValue
            // 
            this.lblMetricCollectedValue.AutoSize = true;
            this.lblMetricCollectedValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricCollectedValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricCollectedValue.Location = new System.Drawing.Point(19, 28);
            this.lblMetricCollectedValue.Name = "lblMetricCollectedValue";
            this.lblMetricCollectedValue.Size = new System.Drawing.Size(33, 37);
            this.lblMetricCollectedValue.TabIndex = 2;
            this.lblMetricCollectedValue.Text = "0";
            // 
            // lblMetricCollectedTitle
            // 
            this.lblMetricCollectedTitle.AutoSize = true;
            this.lblMetricCollectedTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricCollectedTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricCollectedTitle.Location = new System.Drawing.Point(19, 12);
            this.lblMetricCollectedTitle.Name = "lblMetricCollectedTitle";
            this.lblMetricCollectedTitle.Size = new System.Drawing.Size(120, 15);
            this.lblMetricCollectedTitle.TabIndex = 1;
            this.lblMetricCollectedTitle.Text = "✅ Collected                ";
            this.lblMetricCollectedTitle.Click += new System.EventHandler(this.lblMetricCollectedTitle_Click);
            // 
            // panelMetricPending
            // 
            this.panelMetricPending.BackColor = System.Drawing.Color.White;
            this.panelMetricPending.Controls.Add(this.lblMetricPendingValue);
            this.panelMetricPending.Controls.Add(this.lblMetricPendingTitle);
            this.panelMetricPending.Location = new System.Drawing.Point(0, 0);
            this.panelMetricPending.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.panelMetricPending.Name = "panelMetricPending";
            this.panelMetricPending.Padding = new System.Windows.Forms.Padding(16, 16, 16, 16);
            this.panelMetricPending.Size = new System.Drawing.Size(240, 70);
            this.panelMetricPending.TabIndex = 0;
            // 
            // lblMetricPendingValue
            // 
            this.lblMetricPendingValue.AutoSize = true;
            this.lblMetricPendingValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricPendingValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricPendingValue.Location = new System.Drawing.Point(19, 28);
            this.lblMetricPendingValue.Name = "lblMetricPendingValue";
            this.lblMetricPendingValue.Size = new System.Drawing.Size(33, 37);
            this.lblMetricPendingValue.TabIndex = 2;
            this.lblMetricPendingValue.Text = "0";
            this.lblMetricPendingValue.Click += new System.EventHandler(this.lblMetricPendingValue_Click);
            // 
            // lblMetricPendingTitle
            // 
            this.lblMetricPendingTitle.AutoSize = true;
            this.lblMetricPendingTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricPendingTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricPendingTitle.Location = new System.Drawing.Point(19, 12);
            this.lblMetricPendingTitle.Name = "lblMetricPendingTitle";
            this.lblMetricPendingTitle.Size = new System.Drawing.Size(95, 15);
            this.lblMetricPendingTitle.TabIndex = 1;
            this.lblMetricPendingTitle.Text = "⏳ Total Pending";
            this.lblMetricPendingTitle.Click += new System.EventHandler(this.lblMetricPendingTitle_Click);
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.lblSubtitle);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(24, 24);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1245, 89);
            this.panelHeader.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblSubtitle.Location = new System.Drawing.Point(14, 59);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(279, 19);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Track and manage library fines and penalties";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitle.Location = new System.Drawing.Point(10, 7);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(473, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Fine and Penalty Management";
            // 
            // AdminFinesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1293, 796);
            this.Controls.Add(this.panelMainContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AdminFinesForm";
            this.Text = "Fine & Penalty Management";
            this.Load += new System.EventHandler(this.AdminFinesForm_Load);
            this.panelMainContainer.ResumeLayout(false);
            this.panelTableContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFines)).EndInit();
            this.panelFilters.ResumeLayout(false);
            this.panelMetrics.ResumeLayout(false);
            this.panelMetricTotalFines.ResumeLayout(false);
            this.panelMetricTotalFines.PerformLayout();
            this.panelMetricWaived.ResumeLayout(false);
            this.panelMetricWaived.PerformLayout();
            this.panelMetricCollected.ResumeLayout(false);
            this.panelMetricCollected.PerformLayout();
            this.panelMetricPending.ResumeLayout(false);
            this.panelMetricPending.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMainContainer;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel panelMetrics;
        private System.Windows.Forms.Panel panelMetricPending;
        private System.Windows.Forms.Label lblMetricPendingValue;
        private System.Windows.Forms.Label lblMetricPendingTitle;
        private System.Windows.Forms.Panel panelMetricCollected;
        private System.Windows.Forms.Label lblMetricCollectedValue;
        private System.Windows.Forms.Label lblMetricCollectedTitle;
        private System.Windows.Forms.Panel panelMetricWaived;
        private System.Windows.Forms.Label lblMetricWaivedValue;
        private System.Windows.Forms.Label lblMetricWaivedTitle;
        private System.Windows.Forms.Panel panelMetricTotalFines;
        private System.Windows.Forms.Label lblMetricTotalFinesValue;
        private System.Windows.Forms.Label lblMetricTotalFinesTitle;
        private System.Windows.Forms.Panel panelFilters;
        private System.Windows.Forms.Button btnFilterStatus;
        private System.Windows.Forms.Button btnFilterType;
        private System.Windows.Forms.Panel panelTableContainer;
        private System.Windows.Forms.DataGridView dataGridViewFines;
    }
}
