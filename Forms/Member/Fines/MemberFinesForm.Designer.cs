namespace Project5LMS.Forms.Member.Fines
{
    partial class MemberFinesForm
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
            this.panelFineRates = new System.Windows.Forms.Panel();
            this.lblFineRatesTitle = new System.Windows.Forms.Label();
            this.lblRate3 = new System.Windows.Forms.Label();
            this.lblRate2 = new System.Windows.Forms.Label();
            this.lblRate1 = new System.Windows.Forms.Label();
            this.panelPaymentHistory = new System.Windows.Forms.Panel();
            this.panelPaymentHistoryHeader = new System.Windows.Forms.Panel();
            this.panelDollarIcon = new System.Windows.Forms.Panel();
            this.lblPaymentHistoryTitle = new System.Windows.Forms.Label();
            this.panelPaymentHistoryList = new System.Windows.Forms.Panel();
            this.panelOutstandingFines = new System.Windows.Forms.Panel();
            this.lblOutstandingFinesCount = new System.Windows.Forms.Label();
            this.panelOutstandingFinesList = new System.Windows.Forms.Panel();
            this.panelTotalOutstanding = new System.Windows.Forms.Panel();
            this.btnPayNow = new System.Windows.Forms.Button();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.panelExclamationIcon = new System.Windows.Forms.Panel();
            this.lblTotalOutstandingLabel = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelMainContainer.SuspendLayout();
            this.panelFineRates.SuspendLayout();
            this.panelPaymentHistory.SuspendLayout();
            this.panelPaymentHistoryHeader.SuspendLayout();
            this.panelOutstandingFines.SuspendLayout();
            this.panelTotalOutstanding.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMainContainer
            // 
            this.panelMainContainer.AutoScroll = true;
            this.panelMainContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelMainContainer.Controls.Add(this.panelFineRates);
            this.panelMainContainer.Controls.Add(this.panelPaymentHistory);
            this.panelMainContainer.Controls.Add(this.panelOutstandingFines);
            this.panelMainContainer.Controls.Add(this.panelTotalOutstanding);
            this.panelMainContainer.Controls.Add(this.panelHeader);
            this.panelMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainContainer.Location = new System.Drawing.Point(0, 0);
            this.panelMainContainer.Margin = new System.Windows.Forms.Padding(4);
            this.panelMainContainer.Name = "panelMainContainer";
            this.panelMainContainer.Padding = new System.Windows.Forms.Padding(40, 30, 40, 30);
            this.panelMainContainer.Size = new System.Drawing.Size(1600, 985);
            this.panelMainContainer.TabIndex = 0;
            // 
            // panelFineRates
            // 
            this.panelFineRates.BackColor = System.Drawing.Color.White;
            this.panelFineRates.Controls.Add(this.lblFineRatesTitle);
            this.panelFineRates.Controls.Add(this.lblRate3);
            this.panelFineRates.Controls.Add(this.lblRate2);
            this.panelFineRates.Controls.Add(this.lblRate1);
            this.panelFineRates.Location = new System.Drawing.Point(800, 650);
            this.panelFineRates.Margin = new System.Windows.Forms.Padding(4);
            this.panelFineRates.Name = "panelFineRates";
            this.panelFineRates.Padding = new System.Windows.Forms.Padding(25, 20, 25, 20);
            this.panelFineRates.Size = new System.Drawing.Size(480, 200);
            this.panelFineRates.TabIndex = 4;
            // 
            // lblFineRatesTitle
            // 
            this.lblFineRatesTitle.AutoSize = true;
            this.lblFineRatesTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFineRatesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblFineRatesTitle.Location = new System.Drawing.Point(25, 20);
            this.lblFineRatesTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFineRatesTitle.Name = "lblFineRatesTitle";
            this.lblFineRatesTitle.Size = new System.Drawing.Size(130, 32);
            this.lblFineRatesTitle.TabIndex = 0;
            this.lblFineRatesTitle.Text = "Fine Rates";
            // 
            // lblRate3
            // 
            this.lblRate3.AutoSize = true;
            this.lblRate3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRate3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblRate3.Location = new System.Drawing.Point(25, 140);
            this.lblRate3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRate3.Name = "lblRate3";
            this.lblRate3.Size = new System.Drawing.Size(361, 25);
            this.lblRate3.TabIndex = 3;
            this.lblRate3.Text = "Damaged Books: Repair/replacement cost";
            // 
            // lblRate2
            // 
            this.lblRate2.AutoSize = true;
            this.lblRate2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRate2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblRate2.Location = new System.Drawing.Point(25, 100);
            this.lblRate2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRate2.Name = "lblRate2";
            this.lblRate2.Size = new System.Drawing.Size(407, 25);
            this.lblRate2.TabIndex = 2;
            this.lblRate2.Text = "Lost Books: Replacement cost + P10 processing";
            // 
            // lblRate1
            // 
            this.lblRate1.AutoSize = true;
            this.lblRate1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRate1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblRate1.Location = new System.Drawing.Point(25, 60);
            this.lblRate1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRate1.Name = "lblRate1";
            this.lblRate1.Size = new System.Drawing.Size(261, 25);
            this.lblRate1.TabIndex = 1;
            this.lblRate1.Text = "Overdue Books: P1.00 per day";
            // 
            // panelPaymentHistory
            // 
            this.panelPaymentHistory.BackColor = System.Drawing.Color.White;
            this.panelPaymentHistory.Controls.Add(this.panelPaymentHistoryHeader);
            this.panelPaymentHistory.Controls.Add(this.panelPaymentHistoryList);
            this.panelPaymentHistory.Location = new System.Drawing.Point(40, 650);
            this.panelPaymentHistory.Margin = new System.Windows.Forms.Padding(4);
            this.panelPaymentHistory.Name = "panelPaymentHistory";
            this.panelPaymentHistory.Size = new System.Drawing.Size(750, 300);
            this.panelPaymentHistory.TabIndex = 3;
            // 
            // panelPaymentHistoryHeader
            // 
            this.panelPaymentHistoryHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.panelPaymentHistoryHeader.Controls.Add(this.panelDollarIcon);
            this.panelPaymentHistoryHeader.Controls.Add(this.lblPaymentHistoryTitle);
            this.panelPaymentHistoryHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPaymentHistoryHeader.Location = new System.Drawing.Point(0, 0);
            this.panelPaymentHistoryHeader.Margin = new System.Windows.Forms.Padding(4);
            this.panelPaymentHistoryHeader.Name = "panelPaymentHistoryHeader";
            this.panelPaymentHistoryHeader.Size = new System.Drawing.Size(750, 50);
            this.panelPaymentHistoryHeader.TabIndex = 1;
            // 
            // panelDollarIcon
            // 
            this.panelDollarIcon.BackColor = System.Drawing.Color.Transparent;
            this.panelDollarIcon.Location = new System.Drawing.Point(20, 10);
            this.panelDollarIcon.Margin = new System.Windows.Forms.Padding(4);
            this.panelDollarIcon.Name = "panelDollarIcon";
            this.panelDollarIcon.Size = new System.Drawing.Size(30, 30);
            this.panelDollarIcon.TabIndex = 1;
            this.panelDollarIcon.Paint += new System.Windows.Forms.PaintEventHandler(this.panelDollarIcon_Paint);
            // 
            // lblPaymentHistoryTitle
            // 
            this.lblPaymentHistoryTitle.AutoSize = true;
            this.lblPaymentHistoryTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaymentHistoryTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPaymentHistoryTitle.Location = new System.Drawing.Point(60, 12);
            this.lblPaymentHistoryTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPaymentHistoryTitle.Name = "lblPaymentHistoryTitle";
            this.lblPaymentHistoryTitle.Size = new System.Drawing.Size(226, 32);
            this.lblPaymentHistoryTitle.TabIndex = 0;
            this.lblPaymentHistoryTitle.Text = "P Payment History";
            // 
            // panelPaymentHistoryList
            // 
            this.panelPaymentHistoryList.AutoScroll = true;
            this.panelPaymentHistoryList.BackColor = System.Drawing.Color.White;
            this.panelPaymentHistoryList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPaymentHistoryList.Location = new System.Drawing.Point(0, 0);
            this.panelPaymentHistoryList.Margin = new System.Windows.Forms.Padding(4);
            this.panelPaymentHistoryList.Name = "panelPaymentHistoryList";
            this.panelPaymentHistoryList.Padding = new System.Windows.Forms.Padding(20);
            this.panelPaymentHistoryList.Size = new System.Drawing.Size(750, 300);
            this.panelPaymentHistoryList.TabIndex = 0;
            // 
            // panelOutstandingFines
            // 
            this.panelOutstandingFines.BackColor = System.Drawing.Color.White;
            this.panelOutstandingFines.Controls.Add(this.lblOutstandingFinesCount);
            this.panelOutstandingFines.Controls.Add(this.panelOutstandingFinesList);
            this.panelOutstandingFines.Location = new System.Drawing.Point(40, 350);
            this.panelOutstandingFines.Margin = new System.Windows.Forms.Padding(4);
            this.panelOutstandingFines.Name = "panelOutstandingFines";
            this.panelOutstandingFines.Size = new System.Drawing.Size(1240, 280);
            this.panelOutstandingFines.TabIndex = 2;
            // 
            // lblOutstandingFinesCount
            // 
            this.lblOutstandingFinesCount.AutoSize = true;
            this.lblOutstandingFinesCount.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutstandingFinesCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblOutstandingFinesCount.Location = new System.Drawing.Point(20, 20);
            this.lblOutstandingFinesCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOutstandingFinesCount.Name = "lblOutstandingFinesCount";
            this.lblOutstandingFinesCount.Size = new System.Drawing.Size(291, 37);
            this.lblOutstandingFinesCount.TabIndex = 1;
            this.lblOutstandingFinesCount.Text = "Outstanding Fines (0)";
            // 
            // panelOutstandingFinesList
            // 
            this.panelOutstandingFinesList.AutoScroll = true;
            this.panelOutstandingFinesList.BackColor = System.Drawing.Color.White;
            this.panelOutstandingFinesList.Location = new System.Drawing.Point(20, 70);
            this.panelOutstandingFinesList.Margin = new System.Windows.Forms.Padding(4);
            this.panelOutstandingFinesList.Name = "panelOutstandingFinesList";
            this.panelOutstandingFinesList.Size = new System.Drawing.Size(1200, 190);
            this.panelOutstandingFinesList.TabIndex = 0;
            // 
            // panelTotalOutstanding
            // 
            this.panelTotalOutstanding.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.panelTotalOutstanding.Controls.Add(this.btnPayNow);
            this.panelTotalOutstanding.Controls.Add(this.lblTotalAmount);
            this.panelTotalOutstanding.Controls.Add(this.panelExclamationIcon);
            this.panelTotalOutstanding.Controls.Add(this.lblTotalOutstandingLabel);
            this.panelTotalOutstanding.Location = new System.Drawing.Point(40, 200);
            this.panelTotalOutstanding.Margin = new System.Windows.Forms.Padding(4);
            this.panelTotalOutstanding.Name = "panelTotalOutstanding";
            this.panelTotalOutstanding.Padding = new System.Windows.Forms.Padding(30, 25, 30, 25);
            this.panelTotalOutstanding.Size = new System.Drawing.Size(1240, 130);
            this.panelTotalOutstanding.TabIndex = 1;
            // 
            // btnPayNow
            // 
            this.btnPayNow.BackColor = System.Drawing.Color.Maroon;
            this.btnPayNow.FlatAppearance.BorderSize = 0;
            this.btnPayNow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPayNow.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPayNow.ForeColor = System.Drawing.Color.White;
            this.btnPayNow.Location = new System.Drawing.Point(995, 40);
            this.btnPayNow.Margin = new System.Windows.Forms.Padding(4);
            this.btnPayNow.Name = "btnPayNow";
            this.btnPayNow.Size = new System.Drawing.Size(185, 50);
            this.btnPayNow.TabIndex = 4;
            this.btnPayNow.Text = "Pay Now";
            this.btnPayNow.UseVisualStyleBackColor = false;
            this.btnPayNow.Click += new System.EventHandler(this.btnPayNow_Click);
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(20)))), ((int)(((byte)(60)))));
            this.lblTotalAmount.Location = new System.Drawing.Point(100, 60);
            this.lblTotalAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(128, 54);
            this.lblTotalAmount.TabIndex = 2;
            this.lblTotalAmount.Text = "P0.00";
            // 
            // panelExclamationIcon
            // 
            this.panelExclamationIcon.BackColor = System.Drawing.Color.Transparent;
            this.panelExclamationIcon.Location = new System.Drawing.Point(30, 50);
            this.panelExclamationIcon.Margin = new System.Windows.Forms.Padding(4);
            this.panelExclamationIcon.Name = "panelExclamationIcon";
            this.panelExclamationIcon.Size = new System.Drawing.Size(40, 40);
            this.panelExclamationIcon.TabIndex = 1;
            this.panelExclamationIcon.Paint += new System.Windows.Forms.PaintEventHandler(this.panelExclamationIcon_Paint);
            // 
            // lblTotalOutstandingLabel
            // 
            this.lblTotalOutstandingLabel.AutoSize = true;
            this.lblTotalOutstandingLabel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalOutstandingLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTotalOutstandingLabel.Location = new System.Drawing.Point(100, 25);
            this.lblTotalOutstandingLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalOutstandingLabel.Name = "lblTotalOutstandingLabel";
            this.lblTotalOutstandingLabel.Size = new System.Drawing.Size(266, 32);
            this.lblTotalOutstandingLabel.TabIndex = 0;
            this.lblTotalOutstandingLabel.Text = "Total Outstanding Fines";
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.lblSubtitle);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Location = new System.Drawing.Point(40, 30);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(4);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1240, 150);
            this.panelHeader.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblSubtitle.Location = new System.Drawing.Point(0, 70);
            this.lblSubtitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(540, 32);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "View your outstanding fines and payment history";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(360, 62);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Fines & Penalties";
            // 
            // MemberFinesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1600, 985);
            this.Controls.Add(this.panelMainContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MemberFinesForm";
            this.Text = "Fines & Penalties";
            this.Load += new System.EventHandler(this.MemberFinesForm_Load);
            this.VisibleChanged += new System.EventHandler(this.MemberFinesForm_VisibleChanged);
            this.panelMainContainer.ResumeLayout(false);
            this.panelFineRates.ResumeLayout(false);
            this.panelFineRates.PerformLayout();
            this.panelPaymentHistory.ResumeLayout(false);
            this.panelPaymentHistoryHeader.ResumeLayout(false);
            this.panelPaymentHistoryHeader.PerformLayout();
            this.panelOutstandingFines.ResumeLayout(false);
            this.panelOutstandingFines.PerformLayout();
            this.panelTotalOutstanding.ResumeLayout(false);
            this.panelTotalOutstanding.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMainContainer;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel panelTotalOutstanding;
        private System.Windows.Forms.Panel panelExclamationIcon;
        private System.Windows.Forms.Label lblTotalOutstandingLabel;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Button btnPayNow;
        private System.Windows.Forms.Panel panelOutstandingFines;
        private System.Windows.Forms.Label lblOutstandingFinesCount;
        private System.Windows.Forms.Panel panelOutstandingFinesList;
        private System.Windows.Forms.Panel panelPaymentHistory;
        private System.Windows.Forms.Panel panelPaymentHistoryHeader;
        private System.Windows.Forms.Panel panelDollarIcon;
        private System.Windows.Forms.Label lblPaymentHistoryTitle;
        private System.Windows.Forms.Panel panelPaymentHistoryList;
        private System.Windows.Forms.Panel panelFineRates;
        private System.Windows.Forms.Label lblFineRatesTitle;
        private System.Windows.Forms.Label lblRate1;
        private System.Windows.Forms.Label lblRate2;
        private System.Windows.Forms.Label lblRate3;
    }
}
