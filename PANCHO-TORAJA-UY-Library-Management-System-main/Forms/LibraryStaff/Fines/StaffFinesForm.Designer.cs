namespace Project5LMS.Forms.LibraryStaff.Fines
{
    public partial class StaffFinesForm
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
            this.panelContentContainer = new System.Windows.Forms.Panel();
            this.panelProcessPayment = new System.Windows.Forms.Panel();
            this.lblProcessPaymentPlaceholder = new System.Windows.Forms.Label();
            this.lblProcessPaymentTitle = new System.Windows.Forms.Label();
            this.panelActiveFines = new System.Windows.Forms.Panel();
            this.panelActiveFinesList = new System.Windows.Forms.FlowLayoutPanel();
            this.lblActiveFinesTitle = new System.Windows.Forms.Label();
            this.panelMetrics = new System.Windows.Forms.Panel();
            this.panelMetricCollected = new System.Windows.Forms.Panel();
            this.lblMetricCollectedValue = new System.Windows.Forms.Label();
            this.lblMetricCollectedTitle = new System.Windows.Forms.Label();
            this.panelMetricOverdue = new System.Windows.Forms.Panel();
            this.lblMetricOverdueValue = new System.Windows.Forms.Label();
            this.lblMetricOverdueTitle = new System.Windows.Forms.Label();
            this.panelMetricPending = new System.Windows.Forms.Panel();
            this.lblMetricPendingValue = new System.Windows.Forms.Label();
            this.lblMetricPendingTitle = new System.Windows.Forms.Label();
            this.panelMetricTotalFines = new System.Windows.Forms.Panel();
            this.lblMetricTotalFinesValue = new System.Windows.Forms.Label();
            this.lblMetricTotalFinesTitle = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelMainContainer.SuspendLayout();
            this.panelContentContainer.SuspendLayout();
            this.panelProcessPayment.SuspendLayout();
            this.panelActiveFines.SuspendLayout();
            this.panelMetrics.SuspendLayout();
            this.panelMetricCollected.SuspendLayout();
            this.panelMetricOverdue.SuspendLayout();
            this.panelMetricPending.SuspendLayout();
            this.panelMetricTotalFines.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMainContainer
            // 
            this.panelMainContainer.AutoScroll = true;
            this.panelMainContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelMainContainer.Controls.Add(this.panelContentContainer);
            this.panelMainContainer.Controls.Add(this.panelMetrics);
            this.panelMainContainer.Controls.Add(this.panelHeader);
            this.panelMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainContainer.Location = new System.Drawing.Point(0, 0);
            this.panelMainContainer.Name = "panelMainContainer";
            this.panelMainContainer.Padding = new System.Windows.Forms.Padding(24, 24, 24, 24);
            this.panelMainContainer.Size = new System.Drawing.Size(1200, 800);
            this.panelMainContainer.TabIndex = 0;
            // 
            // panelContentContainer
            // 
            this.panelContentContainer.Controls.Add(this.panelProcessPayment);
            this.panelContentContainer.Controls.Add(this.panelActiveFines);
            this.panelContentContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContentContainer.Location = new System.Drawing.Point(24, 195);
            this.panelContentContainer.Name = "panelContentContainer";
            this.panelContentContainer.Size = new System.Drawing.Size(1152, 581);
            this.panelContentContainer.TabIndex = 2;
            // 
            // panelProcessPayment
            // 
            this.panelProcessPayment.BackColor = System.Drawing.Color.White;
            this.panelProcessPayment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelProcessPayment.Controls.Add(this.lblProcessPaymentPlaceholder);
            this.panelProcessPayment.Controls.Add(this.lblProcessPaymentTitle);
            this.panelProcessPayment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelProcessPayment.Location = new System.Drawing.Point(472, 0);
            this.panelProcessPayment.Name = "panelProcessPayment";
            this.panelProcessPayment.Padding = new System.Windows.Forms.Padding(15, 16, 15, 16);
            this.panelProcessPayment.Size = new System.Drawing.Size(680, 581);
            this.panelProcessPayment.TabIndex = 1;
            // 
            // lblProcessPaymentPlaceholder
            // 
            this.lblProcessPaymentPlaceholder.AutoSize = true;
            this.lblProcessPaymentPlaceholder.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcessPaymentPlaceholder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblProcessPaymentPlaceholder.Location = new System.Drawing.Point(15, 49);
            this.lblProcessPaymentPlaceholder.Name = "lblProcessPaymentPlaceholder";
            this.lblProcessPaymentPlaceholder.Size = new System.Drawing.Size(224, 20);
            this.lblProcessPaymentPlaceholder.TabIndex = 1;
            this.lblProcessPaymentPlaceholder.Text = "Select a fine to process payment";
            // 
            // lblProcessPaymentTitle
            // 
            this.lblProcessPaymentTitle.AutoSize = true;
            this.lblProcessPaymentTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcessPaymentTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblProcessPaymentTitle.Location = new System.Drawing.Point(15, 16);
            this.lblProcessPaymentTitle.Name = "lblProcessPaymentTitle";
            this.lblProcessPaymentTitle.Size = new System.Drawing.Size(189, 25);
            this.lblProcessPaymentTitle.TabIndex = 0;
            this.lblProcessPaymentTitle.Text = "📅 Process Payment";
            // 
            // panelActiveFines
            // 
            this.panelActiveFines.BackColor = System.Drawing.Color.White;
            this.panelActiveFines.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelActiveFines.Controls.Add(this.panelActiveFinesList);
            this.panelActiveFines.Controls.Add(this.lblActiveFinesTitle);
            this.panelActiveFines.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelActiveFines.Location = new System.Drawing.Point(0, 0);
            this.panelActiveFines.Name = "panelActiveFines";
            this.panelActiveFines.Padding = new System.Windows.Forms.Padding(15, 16, 15, 16);
            this.panelActiveFines.Size = new System.Drawing.Size(472, 581);
            this.panelActiveFines.TabIndex = 0;
            // 
            // panelActiveFinesList
            // 
            this.panelActiveFinesList.AutoScroll = true;
            this.panelActiveFinesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelActiveFinesList.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panelActiveFinesList.Location = new System.Drawing.Point(15, 16);
            this.panelActiveFinesList.Name = "panelActiveFinesList";
            this.panelActiveFinesList.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelActiveFinesList.Size = new System.Drawing.Size(440, 547);
            this.panelActiveFinesList.TabIndex = 1;
            this.panelActiveFinesList.WrapContents = false;
            // 
            // lblActiveFinesTitle
            // 
            this.lblActiveFinesTitle.AutoSize = true;
            this.lblActiveFinesTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActiveFinesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblActiveFinesTitle.Location = new System.Drawing.Point(15, 16);
            this.lblActiveFinesTitle.Name = "lblActiveFinesTitle";
            this.lblActiveFinesTitle.Size = new System.Drawing.Size(116, 25);
            this.lblActiveFinesTitle.TabIndex = 0;
            this.lblActiveFinesTitle.Text = "Active Fines";
            // 
            // panelMetrics
            // 
            this.panelMetrics.Controls.Add(this.panelMetricCollected);
            this.panelMetrics.Controls.Add(this.panelMetricOverdue);
            this.panelMetrics.Controls.Add(this.panelMetricPending);
            this.panelMetrics.Controls.Add(this.panelMetricTotalFines);
            this.panelMetrics.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMetrics.Location = new System.Drawing.Point(24, 97);
            this.panelMetrics.Name = "panelMetrics";
            this.panelMetrics.Size = new System.Drawing.Size(1152, 98);
            this.panelMetrics.TabIndex = 1;
            // 
            // panelMetricCollected
            // 
            this.panelMetricCollected.BackColor = System.Drawing.Color.White;
            this.panelMetricCollected.Controls.Add(this.lblMetricCollectedValue);
            this.panelMetricCollected.Controls.Add(this.lblMetricCollectedTitle);
            this.panelMetricCollected.Location = new System.Drawing.Point(916, 0);
            this.panelMetricCollected.Name = "panelMetricCollected";
            this.panelMetricCollected.Padding = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.panelMetricCollected.Size = new System.Drawing.Size(236, 98);
            this.panelMetricCollected.TabIndex = 3;
            // 
            // lblMetricCollectedValue
            // 
            this.lblMetricCollectedValue.AutoSize = true;
            this.lblMetricCollectedValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricCollectedValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricCollectedValue.Location = new System.Drawing.Point(11, 41);
            this.lblMetricCollectedValue.Name = "lblMetricCollectedValue";
            this.lblMetricCollectedValue.Size = new System.Drawing.Size(89, 37);
            this.lblMetricCollectedValue.TabIndex = 1;
            this.lblMetricCollectedValue.Text = "P0.00";
            // 
            // lblMetricCollectedTitle
            // 
            this.lblMetricCollectedTitle.AutoSize = true;
            this.lblMetricCollectedTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricCollectedTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricCollectedTitle.Location = new System.Drawing.Point(11, 12);
            this.lblMetricCollectedTitle.Name = "lblMetricCollectedTitle";
            this.lblMetricCollectedTitle.Size = new System.Drawing.Size(79, 19);
            this.lblMetricCollectedTitle.TabIndex = 0;
            this.lblMetricCollectedTitle.Text = "✓ Collected";
            // 
            // panelMetricOverdue
            // 
            this.panelMetricOverdue.BackColor = System.Drawing.Color.White;
            this.panelMetricOverdue.Controls.Add(this.lblMetricOverdueValue);
            this.panelMetricOverdue.Controls.Add(this.lblMetricOverdueTitle);
            this.panelMetricOverdue.Location = new System.Drawing.Point(608, 0);
            this.panelMetricOverdue.Name = "panelMetricOverdue";
            this.panelMetricOverdue.Padding = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.panelMetricOverdue.Size = new System.Drawing.Size(236, 98);
            this.panelMetricOverdue.TabIndex = 2;
            // 
            // lblMetricOverdueValue
            // 
            this.lblMetricOverdueValue.AutoSize = true;
            this.lblMetricOverdueValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricOverdueValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricOverdueValue.Location = new System.Drawing.Point(11, 41);
            this.lblMetricOverdueValue.Name = "lblMetricOverdueValue";
            this.lblMetricOverdueValue.Size = new System.Drawing.Size(89, 37);
            this.lblMetricOverdueValue.TabIndex = 1;
            this.lblMetricOverdueValue.Text = "P0.00";
            // 
            // lblMetricOverdueTitle
            // 
            this.lblMetricOverdueTitle.AutoSize = true;
            this.lblMetricOverdueTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricOverdueTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricOverdueTitle.Location = new System.Drawing.Point(11, 12);
            this.lblMetricOverdueTitle.Name = "lblMetricOverdueTitle";
            this.lblMetricOverdueTitle.Size = new System.Drawing.Size(85, 19);
            this.lblMetricOverdueTitle.TabIndex = 0;
            this.lblMetricOverdueTitle.Text = "⚠️ Overdue";
            // 
            // panelMetricPending
            // 
            this.panelMetricPending.BackColor = System.Drawing.Color.White;
            this.panelMetricPending.Controls.Add(this.lblMetricPendingValue);
            this.panelMetricPending.Controls.Add(this.lblMetricPendingTitle);
            this.panelMetricPending.Location = new System.Drawing.Point(298, 0);
            this.panelMetricPending.Name = "panelMetricPending";
            this.panelMetricPending.Padding = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.panelMetricPending.Size = new System.Drawing.Size(236, 98);
            this.panelMetricPending.TabIndex = 1;
            // 
            // lblMetricPendingValue
            // 
            this.lblMetricPendingValue.AutoSize = true;
            this.lblMetricPendingValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricPendingValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricPendingValue.Location = new System.Drawing.Point(11, 41);
            this.lblMetricPendingValue.Name = "lblMetricPendingValue";
            this.lblMetricPendingValue.Size = new System.Drawing.Size(89, 37);
            this.lblMetricPendingValue.TabIndex = 1;
            this.lblMetricPendingValue.Text = "P0.00";
            // 
            // lblMetricPendingTitle
            // 
            this.lblMetricPendingTitle.AutoSize = true;
            this.lblMetricPendingTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricPendingTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricPendingTitle.Location = new System.Drawing.Point(11, 12);
            this.lblMetricPendingTitle.Name = "lblMetricPendingTitle";
            this.lblMetricPendingTitle.Size = new System.Drawing.Size(81, 19);
            this.lblMetricPendingTitle.TabIndex = 0;
            this.lblMetricPendingTitle.Text = "⚠️ Pending";
            // 
            // panelMetricTotalFines
            // 
            this.panelMetricTotalFines.BackColor = System.Drawing.Color.White;
            this.panelMetricTotalFines.Controls.Add(this.lblMetricTotalFinesValue);
            this.panelMetricTotalFines.Controls.Add(this.lblMetricTotalFinesTitle);
            this.panelMetricTotalFines.Location = new System.Drawing.Point(0, 0);
            this.panelMetricTotalFines.Name = "panelMetricTotalFines";
            this.panelMetricTotalFines.Padding = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.panelMetricTotalFines.Size = new System.Drawing.Size(236, 98);
            this.panelMetricTotalFines.TabIndex = 0;
            // 
            // lblMetricTotalFinesValue
            // 
            this.lblMetricTotalFinesValue.AutoSize = true;
            this.lblMetricTotalFinesValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTotalFinesValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricTotalFinesValue.Location = new System.Drawing.Point(11, 41);
            this.lblMetricTotalFinesValue.Name = "lblMetricTotalFinesValue";
            this.lblMetricTotalFinesValue.Size = new System.Drawing.Size(89, 37);
            this.lblMetricTotalFinesValue.TabIndex = 1;
            this.lblMetricTotalFinesValue.Text = "P0.00";
            // 
            // lblMetricTotalFinesTitle
            // 
            this.lblMetricTotalFinesTitle.AutoSize = true;
            this.lblMetricTotalFinesTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTotalFinesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricTotalFinesTitle.Location = new System.Drawing.Point(11, 12);
            this.lblMetricTotalFinesTitle.Name = "lblMetricTotalFinesTitle";
            this.lblMetricTotalFinesTitle.Size = new System.Drawing.Size(96, 19);
            this.lblMetricTotalFinesTitle.TabIndex = 0;
            this.lblMetricTotalFinesTitle.Text = "💰 Total Fines";
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
            this.lblSubtitle.Location = new System.Drawing.Point(3, 44);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(183, 19);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Track and collect library fines";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitle.Location = new System.Drawing.Point(3, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(96, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Fines";
            // 
            // StaffFinesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.panelMainContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "StaffFinesForm";
            this.Text = "Fines Management";
            this.Load += new System.EventHandler(this.StaffFinesForm_Load);
            this.panelMainContainer.ResumeLayout(false);
            this.panelContentContainer.ResumeLayout(false);
            this.panelProcessPayment.ResumeLayout(false);
            this.panelProcessPayment.PerformLayout();
            this.panelActiveFines.ResumeLayout(false);
            this.panelActiveFines.PerformLayout();
            this.panelMetrics.ResumeLayout(false);
            this.panelMetricCollected.ResumeLayout(false);
            this.panelMetricCollected.PerformLayout();
            this.panelMetricOverdue.ResumeLayout(false);
            this.panelMetricOverdue.PerformLayout();
            this.panelMetricPending.ResumeLayout(false);
            this.panelMetricPending.PerformLayout();
            this.panelMetricTotalFines.ResumeLayout(false);
            this.panelMetricTotalFines.PerformLayout();
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
        private System.Windows.Forms.Panel panelMetricTotalFines;
        private System.Windows.Forms.Label lblMetricTotalFinesTitle;
        private System.Windows.Forms.Label lblMetricTotalFinesValue;
        private System.Windows.Forms.Panel panelMetricPending;
        private System.Windows.Forms.Label lblMetricPendingTitle;
        private System.Windows.Forms.Label lblMetricPendingValue;
        private System.Windows.Forms.Panel panelMetricOverdue;
        private System.Windows.Forms.Label lblMetricOverdueTitle;
        private System.Windows.Forms.Label lblMetricOverdueValue;
        private System.Windows.Forms.Panel panelMetricCollected;
        private System.Windows.Forms.Label lblMetricCollectedTitle;
        private System.Windows.Forms.Label lblMetricCollectedValue;
        private System.Windows.Forms.Panel panelContentContainer;
        private System.Windows.Forms.Panel panelActiveFines;
        private System.Windows.Forms.Label lblActiveFinesTitle;
        private System.Windows.Forms.FlowLayoutPanel panelActiveFinesList;
        private System.Windows.Forms.Panel panelProcessPayment;
        private System.Windows.Forms.Label lblProcessPaymentTitle;
        private System.Windows.Forms.Label lblProcessPaymentPlaceholder;
    }
}

