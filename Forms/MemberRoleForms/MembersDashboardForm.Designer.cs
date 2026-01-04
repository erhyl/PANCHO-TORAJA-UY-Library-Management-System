namespace Project5LMS.Forms.MemberRoleForms
{
    partial class MembersDashboardForm
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
            this.panelMetricCard1 = new System.Windows.Forms.Panel();
            this.lblMetricValue1 = new System.Windows.Forms.Label();
            this.lblBooksBorrowed = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMetricValue2 = new System.Windows.Forms.Label();
            this.lblBooksOverdue = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblMetricValue3 = new System.Windows.Forms.Label();
            this.lblMembersFines = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblMetricValue4 = new System.Windows.Forms.Label();
            this.lblBooksReservation = new System.Windows.Forms.Label();
            this.lblActiveTransactionsTitle = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnMembersBookRenew = new System.Windows.Forms.Button();
            this.dta_GridMembersCurrentBooks = new System.Windows.Forms.DataGridView();
            this.panelMetricCard1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dta_GridMembersCurrentBooks)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMetricCard1
            // 
            this.panelMetricCard1.BackColor = System.Drawing.Color.White;
            this.panelMetricCard1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMetricCard1.Controls.Add(this.lblMetricValue1);
            this.panelMetricCard1.Controls.Add(this.lblBooksBorrowed);
            this.panelMetricCard1.Location = new System.Drawing.Point(22, 19);
            this.panelMetricCard1.Name = "panelMetricCard1";
            this.panelMetricCard1.Size = new System.Drawing.Size(166, 98);
            this.panelMetricCard1.TabIndex = 1;
            // 
            // lblMetricValue1
            // 
            this.lblMetricValue1.AutoSize = true;
            this.lblMetricValue1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricValue1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricValue1.Location = new System.Drawing.Point(11, 49);
            this.lblMetricValue1.Name = "lblMetricValue1";
            this.lblMetricValue1.Size = new System.Drawing.Size(36, 37);
            this.lblMetricValue1.TabIndex = 1;
            this.lblMetricValue1.Text = "0";
            this.lblMetricValue1.Click += new System.EventHandler(this.lblMetricValue1_Click);
            // 
            // lblBooksBorrowed
            // 
            this.lblBooksBorrowed.AutoSize = true;
            this.lblBooksBorrowed.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBooksBorrowed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblBooksBorrowed.Location = new System.Drawing.Point(11, 16);
            this.lblBooksBorrowed.Name = "lblBooksBorrowed";
            this.lblBooksBorrowed.Size = new System.Drawing.Size(121, 18);
            this.lblBooksBorrowed.TabIndex = 0;
            this.lblBooksBorrowed.Text = "Books Borrowed";
            this.lblBooksBorrowed.Click += new System.EventHandler(this.lblMetricTitle1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblMetricValue2);
            this.panel1.Controls.Add(this.lblBooksOverdue);
            this.panel1.Location = new System.Drawing.Point(282, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(166, 98);
            this.panel1.TabIndex = 2;
            // 
            // lblMetricValue2
            // 
            this.lblMetricValue2.AutoSize = true;
            this.lblMetricValue2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricValue2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricValue2.Location = new System.Drawing.Point(11, 49);
            this.lblMetricValue2.Name = "lblMetricValue2";
            this.lblMetricValue2.Size = new System.Drawing.Size(36, 37);
            this.lblMetricValue2.TabIndex = 1;
            this.lblMetricValue2.Text = "0";
            this.lblMetricValue2.Click += new System.EventHandler(this.lblMetricValue2_Click);
            // 
            // lblBooksOverdue
            // 
            this.lblBooksOverdue.AutoSize = true;
            this.lblBooksOverdue.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBooksOverdue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblBooksOverdue.Location = new System.Drawing.Point(11, 16);
            this.lblBooksOverdue.Name = "lblBooksOverdue";
            this.lblBooksOverdue.Size = new System.Drawing.Size(112, 18);
            this.lblBooksOverdue.TabIndex = 0;
            this.lblBooksOverdue.Text = "Overdue Books";
            this.lblBooksOverdue.Click += new System.EventHandler(this.lblBooksOverdue_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.panelMetricCard1);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Location = new System.Drawing.Point(12, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1236, 160);
            this.panel2.TabIndex = 3;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.lblMetricValue3);
            this.panel3.Controls.Add(this.lblMembersFines);
            this.panel3.Location = new System.Drawing.Point(536, 19);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(166, 98);
            this.panel3.TabIndex = 3;
            // 
            // lblMetricValue3
            // 
            this.lblMetricValue3.AutoSize = true;
            this.lblMetricValue3.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricValue3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricValue3.Location = new System.Drawing.Point(11, 49);
            this.lblMetricValue3.Name = "lblMetricValue3";
            this.lblMetricValue3.Size = new System.Drawing.Size(36, 37);
            this.lblMetricValue3.TabIndex = 1;
            this.lblMetricValue3.Text = "0";
            this.lblMetricValue3.Click += new System.EventHandler(this.lblMetricValue3_Click);
            // 
            // lblMembersFines
            // 
            this.lblMembersFines.AutoSize = true;
            this.lblMembersFines.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMembersFines.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMembersFines.Location = new System.Drawing.Point(11, 16);
            this.lblMembersFines.Name = "lblMembersFines";
            this.lblMembersFines.Size = new System.Drawing.Size(81, 18);
            this.lblMembersFines.TabIndex = 0;
            this.lblMembersFines.Text = "Total Fines";
            this.lblMembersFines.Click += new System.EventHandler(this.lblMembersFines_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.lblMetricValue4);
            this.panel4.Controls.Add(this.lblBooksReservation);
            this.panel4.Location = new System.Drawing.Point(776, 19);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(166, 98);
            this.panel4.TabIndex = 4;
            // 
            // lblMetricValue4
            // 
            this.lblMetricValue4.AutoSize = true;
            this.lblMetricValue4.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricValue4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricValue4.Location = new System.Drawing.Point(11, 49);
            this.lblMetricValue4.Name = "lblMetricValue4";
            this.lblMetricValue4.Size = new System.Drawing.Size(36, 37);
            this.lblMetricValue4.TabIndex = 1;
            this.lblMetricValue4.Text = "0";
            this.lblMetricValue4.Click += new System.EventHandler(this.lblMetricValue4_Click);
            // 
            // lblBooksReservation
            // 
            this.lblBooksReservation.AutoSize = true;
            this.lblBooksReservation.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBooksReservation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblBooksReservation.Location = new System.Drawing.Point(11, 16);
            this.lblBooksReservation.Name = "lblBooksReservation";
            this.lblBooksReservation.Size = new System.Drawing.Size(87, 18);
            this.lblBooksReservation.TabIndex = 0;
            this.lblBooksReservation.Text = "Reservation";
            this.lblBooksReservation.Click += new System.EventHandler(this.lblBooksReservation_Click);
            // 
            // lblActiveTransactionsTitle
            // 
            this.lblActiveTransactionsTitle.AutoSize = true;
            this.lblActiveTransactionsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActiveTransactionsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblActiveTransactionsTitle.Location = new System.Drawing.Point(18, 19);
            this.lblActiveTransactionsTitle.Name = "lblActiveTransactionsTitle";
            this.lblActiveTransactionsTitle.Size = new System.Drawing.Size(175, 24);
            this.lblActiveTransactionsTitle.TabIndex = 4;
            this.lblActiveTransactionsTitle.Text = "My Current Books";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnMembersBookRenew);
            this.panel5.Controls.Add(this.dta_GridMembersCurrentBooks);
            this.panel5.Controls.Add(this.lblActiveTransactionsTitle);
            this.panel5.Location = new System.Drawing.Point(12, 178);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1236, 424);
            this.panel5.TabIndex = 5;
            this.panel5.Paint += new System.Windows.Forms.PaintEventHandler(this.panel5_Paint);
            // 
            // btnMembersBookRenew
            // 
            this.btnMembersBookRenew.FlatAppearance.BorderSize = 0;
            this.btnMembersBookRenew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMembersBookRenew.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMembersBookRenew.Location = new System.Drawing.Point(968, 15);
            this.btnMembersBookRenew.Name = "btnMembersBookRenew";
            this.btnMembersBookRenew.Size = new System.Drawing.Size(131, 35);
            this.btnMembersBookRenew.TabIndex = 5;
            this.btnMembersBookRenew.Text = "Renew";
            this.btnMembersBookRenew.UseVisualStyleBackColor = true;
            // 
            // dta_GridMembersCurrentBooks
            // 
            this.dta_GridMembersCurrentBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dta_GridMembersCurrentBooks.Location = new System.Drawing.Point(22, 59);
            this.dta_GridMembersCurrentBooks.Name = "dta_GridMembersCurrentBooks";
            this.dta_GridMembersCurrentBooks.Size = new System.Drawing.Size(1077, 346);
            this.dta_GridMembersCurrentBooks.TabIndex = 5;
            this.dta_GridMembersCurrentBooks.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dta_GridMembersCurrentBooks_CellContentClick);
            // 
            // MembersDashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1281, 652);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MembersDashboardForm";
            this.Text = "MembersDashboardForm";
            this.Load += new System.EventHandler(this.MembersDashboardForm_Load);
            this.panelMetricCard1.ResumeLayout(false);
            this.panelMetricCard1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dta_GridMembersCurrentBooks)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMetricCard1;
        private System.Windows.Forms.Label lblMetricValue1;
        private System.Windows.Forms.Label lblBooksBorrowed;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblMetricValue2;
        private System.Windows.Forms.Label lblBooksOverdue;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblMetricValue3;
        private System.Windows.Forms.Label lblMembersFines;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblMetricValue4;
        private System.Windows.Forms.Label lblBooksReservation;
        private System.Windows.Forms.Label lblActiveTransactionsTitle;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.DataGridView dta_GridMembersCurrentBooks;
        private System.Windows.Forms.Button btnMembersBookRenew;
    }
}