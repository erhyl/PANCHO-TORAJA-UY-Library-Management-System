namespace Project5LMS.Forms.Member.Borrowings
{
    partial class MyBorrowingsForm
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
            this.panelBorrowingHistory = new System.Windows.Forms.Panel();
            this.panelHistoryHeader = new System.Windows.Forms.Panel();
            this.lblHistoryTitle = new System.Windows.Forms.Label();
            this.panelHistoryList = new System.Windows.Forms.Panel();
            this.panelCurrentlyBorrowed = new System.Windows.Forms.Panel();
            this.lblCurrentlyBorrowedCount = new System.Windows.Forms.Label();
            this.panelBorrowedList = new System.Windows.Forms.Panel();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelMainContainer.SuspendLayout();
            this.panelBorrowingHistory.SuspendLayout();
            this.panelHistoryHeader.SuspendLayout();
            this.panelCurrentlyBorrowed.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMainContainer
            // 
            this.panelMainContainer.AutoScroll = true;
            this.panelMainContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelMainContainer.Controls.Add(this.panelBorrowingHistory);
            this.panelMainContainer.Controls.Add(this.panelCurrentlyBorrowed);
            this.panelMainContainer.Controls.Add(this.panelHeader);
            this.panelMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainContainer.Location = new System.Drawing.Point(0, 0);
            this.panelMainContainer.Margin = new System.Windows.Forms.Padding(4);
            this.panelMainContainer.Name = "panelMainContainer";
            this.panelMainContainer.Padding = new System.Windows.Forms.Padding(40, 30, 40, 30);
            this.panelMainContainer.Size = new System.Drawing.Size(1600, 985);
            this.panelMainContainer.TabIndex = 0;
            // 
            // panelBorrowingHistory
            // 
            this.panelBorrowingHistory.BackColor = System.Drawing.Color.White;
            this.panelBorrowingHistory.Controls.Add(this.panelHistoryHeader);
            this.panelBorrowingHistory.Controls.Add(this.panelHistoryList);
            this.panelBorrowingHistory.Location = new System.Drawing.Point(40, 605);
            this.panelBorrowingHistory.Margin = new System.Windows.Forms.Padding(4);
            this.panelBorrowingHistory.Name = "panelBorrowingHistory";
            this.panelBorrowingHistory.Size = new System.Drawing.Size(1240, 305);
            this.panelBorrowingHistory.TabIndex = 2;
            // 
            // panelHistoryHeader
            // 
            this.panelHistoryHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.panelHistoryHeader.Controls.Add(this.lblHistoryTitle);
            this.panelHistoryHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHistoryHeader.Margin = new System.Windows.Forms.Padding(4);
            this.panelHistoryHeader.Name = "panelHistoryHeader";
            this.panelHistoryHeader.Size = new System.Drawing.Size(1240, 50);
            this.panelHistoryHeader.TabIndex = 1;
            // 
            // lblHistoryTitle
            // 
            this.lblHistoryTitle.AutoSize = true;
            this.lblHistoryTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHistoryTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblHistoryTitle.Location = new System.Drawing.Point(14, 9);
            this.lblHistoryTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHistoryTitle.Name = "lblHistoryTitle";
            this.lblHistoryTitle.Size = new System.Drawing.Size(267, 32);
            this.lblHistoryTitle.TabIndex = 0;
            this.lblHistoryTitle.Text = "📜 Borrowing History";
            // 
            // panelHistoryList
            // 
            this.panelHistoryList.AutoScroll = true;
            this.panelHistoryList.BackColor = System.Drawing.Color.White;
            this.panelHistoryList.Location = new System.Drawing.Point(0, 50);
            this.panelHistoryList.Margin = new System.Windows.Forms.Padding(4);
            this.panelHistoryList.Name = "panelHistoryList";
            this.panelHistoryList.Padding = new System.Windows.Forms.Padding(20);
            this.panelHistoryList.Size = new System.Drawing.Size(1240, 255);
            this.panelHistoryList.TabIndex = 0;
            // 
            // panelCurrentlyBorrowed
            // 
            this.panelCurrentlyBorrowed.BackColor = System.Drawing.Color.White;
            this.panelCurrentlyBorrowed.Controls.Add(this.lblCurrentlyBorrowedCount);
            this.panelCurrentlyBorrowed.Controls.Add(this.panelBorrowedList);
            this.panelCurrentlyBorrowed.Location = new System.Drawing.Point(40, 154);
            this.panelCurrentlyBorrowed.Margin = new System.Windows.Forms.Padding(4);
            this.panelCurrentlyBorrowed.Name = "panelCurrentlyBorrowed";
            this.panelCurrentlyBorrowed.Size = new System.Drawing.Size(1240, 430);
            this.panelCurrentlyBorrowed.TabIndex = 1;
            // 
            // lblCurrentlyBorrowedCount
            // 
            this.lblCurrentlyBorrowedCount.AutoSize = true;
            this.lblCurrentlyBorrowedCount.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentlyBorrowedCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCurrentlyBorrowedCount.Location = new System.Drawing.Point(20, 20);
            this.lblCurrentlyBorrowedCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrentlyBorrowedCount.Name = "lblCurrentlyBorrowedCount";
            this.lblCurrentlyBorrowedCount.Size = new System.Drawing.Size(359, 37);
            this.lblCurrentlyBorrowedCount.TabIndex = 1;
            this.lblCurrentlyBorrowedCount.Text = "📤 Currently Borrowed (0)";
            // 
            // panelBorrowedList
            // 
            this.panelBorrowedList.AutoScroll = true;
            this.panelBorrowedList.BackColor = System.Drawing.Color.White;
            this.panelBorrowedList.Location = new System.Drawing.Point(20, 70);
            this.panelBorrowedList.Margin = new System.Windows.Forms.Padding(4);
            this.panelBorrowedList.Name = "panelBorrowedList";
            this.panelBorrowedList.Size = new System.Drawing.Size(1200, 340);
            this.panelBorrowedList.TabIndex = 0;
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.lblSubtitle);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Location = new System.Drawing.Point(40, 30);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(4);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1240, 106);
            this.panelHeader.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblSubtitle.Location = new System.Drawing.Point(4, 62);
            this.lblSubtitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(542, 32);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "View your borrowed books and borrowing history";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitle.Location = new System.Drawing.Point(4, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(364, 62);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "My Borrowings";
            // 
            // MyBorrowingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1600, 985);
            this.Controls.Add(this.panelMainContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MyBorrowingsForm";
            this.Text = "My Borrowings";
            this.Load += new System.EventHandler(this.MyBorrowingsForm_Load);
            this.panelMainContainer.ResumeLayout(false);
            this.panelBorrowingHistory.ResumeLayout(false);
            this.panelHistoryHeader.ResumeLayout(false);
            this.panelHistoryHeader.PerformLayout();
            this.panelCurrentlyBorrowed.ResumeLayout(false);
            this.panelCurrentlyBorrowed.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMainContainer;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel panelCurrentlyBorrowed;
        private System.Windows.Forms.Label lblCurrentlyBorrowedCount;
        private System.Windows.Forms.Panel panelBorrowedList;
        private System.Windows.Forms.Panel panelBorrowingHistory;
        private System.Windows.Forms.Panel panelHistoryHeader;
        private System.Windows.Forms.Label lblHistoryTitle;
        private System.Windows.Forms.Panel panelHistoryList;
    }
}
