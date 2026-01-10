namespace Project5LMS.Forms.Member.Reservations
{
    partial class MemberReservationsForm
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
            this.panelInfo = new System.Windows.Forms.Panel();
            this.lblInfoTitle = new System.Windows.Forms.Label();
            this.lblInfoRule5 = new System.Windows.Forms.Label();
            this.lblInfoRule4 = new System.Windows.Forms.Label();
            this.lblInfoRule3 = new System.Windows.Forms.Label();
            this.lblInfoRule2 = new System.Windows.Forms.Label();
            this.lblInfoRule1 = new System.Windows.Forms.Label();
            this.panelActiveReservations = new System.Windows.Forms.Panel();
            this.panelReservationsHeader = new System.Windows.Forms.Panel();
            this.panelBookmarkIcon = new System.Windows.Forms.Panel();
            this.lblActiveReservationsCount = new System.Windows.Forms.Label();
            this.panelReservationsList = new System.Windows.Forms.Panel();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelMainContainer.SuspendLayout();
            this.panelInfo.SuspendLayout();
            this.panelActiveReservations.SuspendLayout();
            this.panelReservationsHeader.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMainContainer
            // 
            this.panelMainContainer.AutoScroll = true;
            this.panelMainContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelMainContainer.Controls.Add(this.panelInfo);
            this.panelMainContainer.Controls.Add(this.panelActiveReservations);
            this.panelMainContainer.Controls.Add(this.panelHeader);
            this.panelMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainContainer.Location = new System.Drawing.Point(0, 0);
            this.panelMainContainer.Name = "panelMainContainer";
            this.panelMainContainer.Padding = new System.Windows.Forms.Padding(30, 24, 30, 24);
            this.panelMainContainer.Size = new System.Drawing.Size(1200, 800);
            this.panelMainContainer.TabIndex = 0;
            this.panelMainContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMainContainer_Paint);
            // 
            // panelInfo
            // 
            this.panelInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.panelInfo.Controls.Add(this.lblInfoTitle);
            this.panelInfo.Controls.Add(this.lblInfoRule5);
            this.panelInfo.Controls.Add(this.lblInfoRule4);
            this.panelInfo.Controls.Add(this.lblInfoRule3);
            this.panelInfo.Controls.Add(this.lblInfoRule2);
            this.panelInfo.Controls.Add(this.lblInfoRule1);
            this.panelInfo.Location = new System.Drawing.Point(600, 201);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Padding = new System.Windows.Forms.Padding(19, 16, 19, 16);
            this.panelInfo.Size = new System.Drawing.Size(536, 244);
            this.panelInfo.TabIndex = 2;
            // 
            // lblInfoTitle
            // 
            this.lblInfoTitle.AutoSize = true;
            this.lblInfoTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblInfoTitle.Location = new System.Drawing.Point(19, 16);
            this.lblInfoTitle.Name = "lblInfoTitle";
            this.lblInfoTitle.Size = new System.Drawing.Size(226, 25);
            this.lblInfoTitle.TabIndex = 0;
            this.lblInfoTitle.Text = "How Reservations Work";
            // 
            // lblInfoRule5
            // 
            this.lblInfoRule5.AutoSize = true;
            this.lblInfoRule5.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoRule5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblInfoRule5.Location = new System.Drawing.Point(19, 195);
            this.lblInfoRule5.Name = "lblInfoRule5";
            this.lblInfoRule5.Size = new System.Drawing.Size(374, 20);
            this.lblInfoRule5.TabIndex = 4;
            this.lblInfoRule5.Text = "� Maximum 5 active reservations allowed per member";
            // 
            // lblInfoRule4
            // 
            this.lblInfoRule4.AutoSize = true;
            this.lblInfoRule4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoRule4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblInfoRule4.Location = new System.Drawing.Point(19, 162);
            this.lblInfoRule4.Name = "lblInfoRule4";
            this.lblInfoRule4.Size = new System.Drawing.Size(416, 20);
            this.lblInfoRule4.TabIndex = 3;
            this.lblInfoRule4.Text = "� You can cancel a reservation at any time before it\'s fulfilled";
            // 
            // lblInfoRule3
            // 
            this.lblInfoRule3.AutoSize = true;
            this.lblInfoRule3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoRule3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblInfoRule3.Location = new System.Drawing.Point(19, 130);
            this.lblInfoRule3.Name = "lblInfoRule3";
            this.lblInfoRule3.Size = new System.Drawing.Size(396, 20);
            this.lblInfoRule3.TabIndex = 2;
            this.lblInfoRule3.Text = "� Reserved books will be held for 3 days after notification";
            // 
            // lblInfoRule2
            // 
            this.lblInfoRule2.AutoSize = true;
            this.lblInfoRule2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoRule2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblInfoRule2.Location = new System.Drawing.Point(19, 98);
            this.lblInfoRule2.Name = "lblInfoRule2";
            this.lblInfoRule2.Size = new System.Drawing.Size(433, 20);
            this.lblInfoRule2.TabIndex = 1;
            this.lblInfoRule2.Text = "� You\'ll be notified via email when the book becomes available";
            // 
            // lblInfoRule1
            // 
            this.lblInfoRule1.AutoSize = true;
            this.lblInfoRule1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoRule1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblInfoRule1.Location = new System.Drawing.Point(19, 65);
            this.lblInfoRule1.Name = "lblInfoRule1";
            this.lblInfoRule1.Size = new System.Drawing.Size(483, 20);
            this.lblInfoRule1.TabIndex = 0;
            this.lblInfoRule1.Text = "� You can reserve books that are currently borrowed by other members";
            // 
            // panelActiveReservations
            // 
            this.panelActiveReservations.BackColor = System.Drawing.Color.White;
            this.panelActiveReservations.Controls.Add(this.panelReservationsHeader);
            this.panelActiveReservations.Controls.Add(this.panelReservationsList);
            this.panelActiveReservations.Location = new System.Drawing.Point(30, 162);
            this.panelActiveReservations.Name = "panelActiveReservations";
            this.panelActiveReservations.Size = new System.Drawing.Size(562, 406);
            this.panelActiveReservations.TabIndex = 1;
            // 
            // panelReservationsHeader
            // 
            this.panelReservationsHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.panelReservationsHeader.Controls.Add(this.panelBookmarkIcon);
            this.panelReservationsHeader.Controls.Add(this.lblActiveReservationsCount);
            this.panelReservationsHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelReservationsHeader.Location = new System.Drawing.Point(0, 0);
            this.panelReservationsHeader.Name = "panelReservationsHeader";
            this.panelReservationsHeader.Size = new System.Drawing.Size(562, 41);
            this.panelReservationsHeader.TabIndex = 1;
            // 
            // panelBookmarkIcon
            // 
            this.panelBookmarkIcon.BackColor = System.Drawing.Color.Transparent;
            this.panelBookmarkIcon.Location = new System.Drawing.Point(15, 8);
            this.panelBookmarkIcon.Name = "panelBookmarkIcon";
            this.panelBookmarkIcon.Size = new System.Drawing.Size(22, 24);
            this.panelBookmarkIcon.TabIndex = 1;
            this.panelBookmarkIcon.Paint += new System.Windows.Forms.PaintEventHandler(this.panelBookmarkIcon_Paint);
            // 
            // lblActiveReservationsCount
            // 
            this.lblActiveReservationsCount.AutoSize = true;
            this.lblActiveReservationsCount.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActiveReservationsCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblActiveReservationsCount.Location = new System.Drawing.Point(45, 10);
            this.lblActiveReservationsCount.Name = "lblActiveReservationsCount";
            this.lblActiveReservationsCount.Size = new System.Drawing.Size(214, 25);
            this.lblActiveReservationsCount.TabIndex = 0;
            this.lblActiveReservationsCount.Text = "Active Reservations (0)";
            // 
            // panelReservationsList
            // 
            this.panelReservationsList.AutoScroll = true;
            this.panelReservationsList.BackColor = System.Drawing.Color.White;
            this.panelReservationsList.Location = new System.Drawing.Point(0, 0);
            this.panelReservationsList.Name = "panelReservationsList";
            this.panelReservationsList.Padding = new System.Windows.Forms.Padding(15, 16, 15, 16);
            this.panelReservationsList.Size = new System.Drawing.Size(562, 406);
            this.panelReservationsList.TabIndex = 0;
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.lblSubtitle);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Location = new System.Drawing.Point(30, 24);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1106, 122);
            this.panelHeader.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblSubtitle.Location = new System.Drawing.Point(10, 58);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(360, 25);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "View and manage your book reservations";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(251, 51);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Reservations";
            // 
            // MemberReservationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.panelMainContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MemberReservationsForm";
            this.Text = "Reservations";
            this.Load += new System.EventHandler(this.MemberReservationsForm_Load);
            this.panelMainContainer.ResumeLayout(false);
            this.panelInfo.ResumeLayout(false);
            this.panelInfo.PerformLayout();
            this.panelActiveReservations.ResumeLayout(false);
            this.panelReservationsHeader.ResumeLayout(false);
            this.panelReservationsHeader.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.Panel panelMainContainer;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel panelActiveReservations;
        private System.Windows.Forms.Panel panelReservationsHeader;
        private System.Windows.Forms.Panel panelBookmarkIcon;
        private System.Windows.Forms.Label lblActiveReservationsCount;
        private System.Windows.Forms.Panel panelReservationsList;
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.Label lblInfoTitle;
        private System.Windows.Forms.Label lblInfoRule1;
        private System.Windows.Forms.Label lblInfoRule2;
        private System.Windows.Forms.Label lblInfoRule3;
        private System.Windows.Forms.Label lblInfoRule4;
        private System.Windows.Forms.Label lblInfoRule5;
    }
}