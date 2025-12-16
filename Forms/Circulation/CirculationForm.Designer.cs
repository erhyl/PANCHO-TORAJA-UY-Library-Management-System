namespace Project5LMS.Admin_Dashboard
{
    partial class CirculationForm
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
            this.btncheckout = new System.Windows.Forms.Button();
            this.btnreturn = new System.Windows.Forms.Button();
            this.btnrenewal = new System.Windows.Forms.Button();
            this.btnhistory = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblActiveLoans = new System.Windows.Forms.Label();
            this.lblALnumber = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblOverdue = new System.Windows.Forms.Label();
            this.lblOverduenum = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblAvailableBooks = new System.Windows.Forms.Label();
            this.lblABnum = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblAM = new System.Windows.Forms.Label();
            this.lblAMnum = new System.Windows.Forms.Label();
            this.CirculationPanel = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btncheckout
            // 
            this.btncheckout.FlatAppearance.BorderSize = 0;
            this.btncheckout.Location = new System.Drawing.Point(24, 122);
            this.btncheckout.Name = "btncheckout";
            this.btncheckout.Size = new System.Drawing.Size(75, 28);
            this.btncheckout.TabIndex = 0;
            this.btncheckout.Text = "Checkout";
            this.btncheckout.UseVisualStyleBackColor = true;
            this.btncheckout.Click += new System.EventHandler(this.btncheckout_Click);
            // 
            // btnreturn
            // 
            this.btnreturn.FlatAppearance.BorderSize = 0;
            this.btnreturn.Location = new System.Drawing.Point(105, 122);
            this.btnreturn.Name = "btnreturn";
            this.btnreturn.Size = new System.Drawing.Size(75, 28);
            this.btnreturn.TabIndex = 1;
            this.btnreturn.Text = "Return";
            this.btnreturn.UseVisualStyleBackColor = true;
            this.btnreturn.Click += new System.EventHandler(this.btnreturn_Click);
            // 
            // btnrenewal
            // 
            this.btnrenewal.FlatAppearance.BorderSize = 0;
            this.btnrenewal.Location = new System.Drawing.Point(186, 122);
            this.btnrenewal.Name = "btnrenewal";
            this.btnrenewal.Size = new System.Drawing.Size(75, 28);
            this.btnrenewal.TabIndex = 2;
            this.btnrenewal.Text = "Renewal";
            this.btnrenewal.UseVisualStyleBackColor = true;
            this.btnrenewal.Click += new System.EventHandler(this.btnrenewal_Click);
            // 
            // btnhistory
            // 
            this.btnhistory.FlatAppearance.BorderSize = 0;
            this.btnhistory.Location = new System.Drawing.Point(267, 122);
            this.btnhistory.Name = "btnhistory";
            this.btnhistory.Size = new System.Drawing.Size(77, 28);
            this.btnhistory.TabIndex = 3;
            this.btnhistory.Text = "History";
            this.btnhistory.UseVisualStyleBackColor = true;
            this.btnhistory.Click += new System.EventHandler(this.btnhistory_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblActiveLoans);
            this.panel1.Controls.Add(this.lblALnumber);
            this.panel1.Location = new System.Drawing.Point(23, 21);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(173, 82);
            this.panel1.TabIndex = 4;
            // 
            // lblActiveLoans
            // 
            this.lblActiveLoans.AutoSize = true;
            this.lblActiveLoans.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActiveLoans.Location = new System.Drawing.Point(18, 49);
            this.lblActiveLoans.Name = "lblActiveLoans";
            this.lblActiveLoans.Size = new System.Drawing.Size(100, 20);
            this.lblActiveLoans.TabIndex = 1;
            this.lblActiveLoans.Text = "Active Loans";
            // 
            // lblALnumber
            // 
            this.lblALnumber.AutoSize = true;
            this.lblALnumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblALnumber.Location = new System.Drawing.Point(14, 10);
            this.lblALnumber.Name = "lblALnumber";
            this.lblALnumber.Size = new System.Drawing.Size(36, 39);
            this.lblALnumber.TabIndex = 0;
            this.lblALnumber.Text = "0";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lblOverdue);
            this.panel2.Controls.Add(this.lblOverduenum);
            this.panel2.Location = new System.Drawing.Point(221, 21);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(173, 82);
            this.panel2.TabIndex = 5;
            // 
            // lblOverdue
            // 
            this.lblOverdue.AutoSize = true;
            this.lblOverdue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOverdue.Location = new System.Drawing.Point(18, 49);
            this.lblOverdue.Name = "lblOverdue";
            this.lblOverdue.Size = new System.Drawing.Size(69, 20);
            this.lblOverdue.TabIndex = 1;
            this.lblOverdue.Text = "Overdue";
            this.lblOverdue.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblOverduenum
            // 
            this.lblOverduenum.AutoSize = true;
            this.lblOverduenum.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOverduenum.Location = new System.Drawing.Point(14, 10);
            this.lblOverduenum.Name = "lblOverduenum";
            this.lblOverduenum.Size = new System.Drawing.Size(36, 39);
            this.lblOverduenum.TabIndex = 0;
            this.lblOverduenum.Text = "0";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.lblAvailableBooks);
            this.panel3.Controls.Add(this.lblABnum);
            this.panel3.Location = new System.Drawing.Point(419, 21);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(173, 82);
            this.panel3.TabIndex = 5;
            // 
            // lblAvailableBooks
            // 
            this.lblAvailableBooks.AutoSize = true;
            this.lblAvailableBooks.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvailableBooks.Location = new System.Drawing.Point(18, 49);
            this.lblAvailableBooks.Name = "lblAvailableBooks";
            this.lblAvailableBooks.Size = new System.Drawing.Size(125, 20);
            this.lblAvailableBooks.TabIndex = 1;
            this.lblAvailableBooks.Text = " Available Books";
            // 
            // lblABnum
            // 
            this.lblABnum.AutoSize = true;
            this.lblABnum.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblABnum.Location = new System.Drawing.Point(14, 10);
            this.lblABnum.Name = "lblABnum";
            this.lblABnum.Size = new System.Drawing.Size(36, 39);
            this.lblABnum.TabIndex = 0;
            this.lblABnum.Text = "0";
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.lblAM);
            this.panel4.Controls.Add(this.lblAMnum);
            this.panel4.Location = new System.Drawing.Point(615, 21);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(173, 82);
            this.panel4.TabIndex = 6;
            // 
            // lblAM
            // 
            this.lblAM.AutoSize = true;
            this.lblAM.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAM.Location = new System.Drawing.Point(18, 49);
            this.lblAM.Name = "lblAM";
            this.lblAM.Size = new System.Drawing.Size(122, 20);
            this.lblAM.TabIndex = 1;
            this.lblAM.Text = "Active Members";
            // 
            // lblAMnum
            // 
            this.lblAMnum.AutoSize = true;
            this.lblAMnum.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAMnum.Location = new System.Drawing.Point(14, 10);
            this.lblAMnum.Name = "lblAMnum";
            this.lblAMnum.Size = new System.Drawing.Size(36, 39);
            this.lblAMnum.TabIndex = 0;
            this.lblAMnum.Text = "0";
            // 
            // CirculationPanel
            // 
            this.CirculationPanel.Location = new System.Drawing.Point(12, 159);
            this.CirculationPanel.Name = "CirculationPanel";
            this.CirculationPanel.Size = new System.Drawing.Size(872, 320);
            this.CirculationPanel.TabIndex = 10;
            // 
            // CirculationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(934, 491);
            this.ControlBox = false;
            this.Controls.Add(this.CirculationPanel);
            this.Controls.Add(this.btnhistory);
            this.Controls.Add(this.btnrenewal);
            this.Controls.Add(this.btnreturn);
            this.Controls.Add(this.btncheckout);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CirculationForm";
            this.Text = "Circulation";
            this.Load += new System.EventHandler(this.CirculationForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btncheckout;
        private System.Windows.Forms.Button btnreturn;
        private System.Windows.Forms.Button btnrenewal;
        private System.Windows.Forms.Button btnhistory;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblActiveLoans;
        private System.Windows.Forms.Label lblALnumber;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblOverdue;
        private System.Windows.Forms.Label lblOverduenum;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblAvailableBooks;
        private System.Windows.Forms.Label lblABnum;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblAM;
        private System.Windows.Forms.Label lblAMnum;
        private System.Windows.Forms.Panel CirculationPanel;
    }
}