namespace Project5LMS.Admin_Dashboard
{
    partial class RenewForm
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
            this.panelRightSection = new System.Windows.Forms.Panel();
            this.panelConditionsBox = new System.Windows.Forms.Panel();
            this.lblCondition4 = new System.Windows.Forms.Label();
            this.lblCondition3 = new System.Windows.Forms.Label();
            this.lblCondition2 = new System.Windows.Forms.Label();
            this.lblCondition1 = new System.Windows.Forms.Label();
            this.lblConditionsTitle = new System.Windows.Forms.Label();
            this.lblCheckoutGuidelinesTitle = new System.Windows.Forms.Label();
            this.panelLeftSection = new System.Windows.Forms.Panel();
            this.btnProcessRenewal = new System.Windows.Forms.Button();
            this.lblRenewalPolicy = new System.Windows.Forms.Label();
            this.lblRenewalPolicyTitle = new System.Windows.Forms.Label();
            this.txtTransactionID = new System.Windows.Forms.TextBox();
            this.lblTransactionID = new System.Windows.Forms.Label();
            this.lblRenewSubtitle = new System.Windows.Forms.Label();
            this.lblRenewTitle = new System.Windows.Forms.Label();
            this.panelMainContainer.SuspendLayout();
            this.panelRightSection.SuspendLayout();
            this.panelConditionsBox.SuspendLayout();
            this.panelLeftSection.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMainContainer
            // 
            this.panelMainContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.panelMainContainer.Controls.Add(this.panelRightSection);
            this.panelMainContainer.Controls.Add(this.panelLeftSection);
            this.panelMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainContainer.Location = new System.Drawing.Point(0, 0);
            this.panelMainContainer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelMainContainer.Name = "panelMainContainer";
            this.panelMainContainer.Padding = new System.Windows.Forms.Padding(30, 30, 30, 30);
            this.panelMainContainer.Size = new System.Drawing.Size(1942, 1102);
            this.panelMainContainer.TabIndex = 0;
            // 
            // panelRightSection
            // 
            this.panelRightSection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRightSection.BackColor = System.Drawing.Color.White;
            this.panelRightSection.Controls.Add(this.panelConditionsBox);
            this.panelRightSection.Controls.Add(this.lblCheckoutGuidelinesTitle);
            this.panelRightSection.Location = new System.Drawing.Point(1000, 30);
            this.panelRightSection.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelRightSection.Name = "panelRightSection";
            this.panelRightSection.Padding = new System.Windows.Forms.Padding(30, 30, 30, 30);
            this.panelRightSection.Size = new System.Drawing.Size(912, 1042);
            this.panelRightSection.TabIndex = 1;
            // 
            // panelConditionsBox
            // 
            this.panelConditionsBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelConditionsBox.Controls.Add(this.lblCondition4);
            this.panelConditionsBox.Controls.Add(this.lblCondition3);
            this.panelConditionsBox.Controls.Add(this.lblCondition2);
            this.panelConditionsBox.Controls.Add(this.lblCondition1);
            this.panelConditionsBox.Controls.Add(this.lblConditionsTitle);
            this.panelConditionsBox.Location = new System.Drawing.Point(30, 100);
            this.panelConditionsBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelConditionsBox.Name = "panelConditionsBox";
            this.panelConditionsBox.Padding = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.panelConditionsBox.Size = new System.Drawing.Size(852, 300);
            this.panelConditionsBox.TabIndex = 1;
            // 
            // lblCondition4
            // 
            this.lblCondition4.AutoSize = true;
            this.lblCondition4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCondition4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCondition4.Location = new System.Drawing.Point(40, 240);
            this.lblCondition4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCondition4.Name = "lblCondition4";
            this.lblCondition4.Size = new System.Drawing.Size(200, 24);
            this.lblCondition4.TabIndex = 4;
            this.lblCondition4.Text = "• Member in good standing";
            // 
            // lblCondition3
            // 
            this.lblCondition3.AutoSize = true;
            this.lblCondition3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCondition3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCondition3.Location = new System.Drawing.Point(40, 200);
            this.lblCondition3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCondition3.Name = "lblCondition3";
            this.lblCondition3.Size = new System.Drawing.Size(250, 24);
            this.lblCondition3.TabIndex = 3;
            this.lblCondition3.Text = "• No overdue status on the book";
            // 
            // lblCondition2
            // 
            this.lblCondition2.AutoSize = true;
            this.lblCondition2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCondition2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCondition2.Location = new System.Drawing.Point(40, 160);
            this.lblCondition2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCondition2.Name = "lblCondition2";
            this.lblCondition2.Size = new System.Drawing.Size(250, 24);
            this.lblCondition2.TabIndex = 2;
            this.lblCondition2.Text = "• Maximum * renewals allowed";
            // 
            // lblCondition1
            // 
            this.lblCondition1.AutoSize = true;
            this.lblCondition1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCondition1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCondition1.Location = new System.Drawing.Point(40, 120);
            this.lblCondition1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCondition1.Name = "lblCondition1";
            this.lblCondition1.Size = new System.Drawing.Size(350, 24);
            this.lblCondition1.TabIndex = 1;
            this.lblCondition1.Text = "• Book is not reserved by another member";
            // 
            // lblConditionsTitle
            // 
            this.lblConditionsTitle.AutoSize = true;
            this.lblConditionsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConditionsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblConditionsTitle.Location = new System.Drawing.Point(20, 20);
            this.lblConditionsTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblConditionsTitle.Name = "lblConditionsTitle";
            this.lblConditionsTitle.Size = new System.Drawing.Size(200, 25);
            this.lblConditionsTitle.TabIndex = 0;
            this.lblConditionsTitle.Text = "Conditions for Renewal";
            // 
            // lblCheckoutGuidelinesTitle
            // 
            this.lblCheckoutGuidelinesTitle.AutoSize = true;
            this.lblCheckoutGuidelinesTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckoutGuidelinesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCheckoutGuidelinesTitle.Location = new System.Drawing.Point(30, 30);
            this.lblCheckoutGuidelinesTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCheckoutGuidelinesTitle.Name = "lblCheckoutGuidelinesTitle";
            this.lblCheckoutGuidelinesTitle.Size = new System.Drawing.Size(250, 31);
            this.lblCheckoutGuidelinesTitle.TabIndex = 0;
            this.lblCheckoutGuidelinesTitle.Text = "Checkout Guidelines";
            // 
            // panelLeftSection
            // 
            this.panelLeftSection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelLeftSection.BackColor = System.Drawing.Color.White;
            this.panelLeftSection.Controls.Add(this.btnProcessRenewal);
            this.panelLeftSection.Controls.Add(this.lblRenewalPolicy);
            this.panelLeftSection.Controls.Add(this.lblRenewalPolicyTitle);
            this.panelLeftSection.Controls.Add(this.txtTransactionID);
            this.panelLeftSection.Controls.Add(this.lblTransactionID);
            this.panelLeftSection.Controls.Add(this.lblRenewSubtitle);
            this.panelLeftSection.Controls.Add(this.lblRenewTitle);
            this.panelLeftSection.Location = new System.Drawing.Point(30, 30);
            this.panelLeftSection.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelLeftSection.Name = "panelLeftSection";
            this.panelLeftSection.Padding = new System.Windows.Forms.Padding(30, 30, 30, 30);
            this.panelLeftSection.Size = new System.Drawing.Size(950, 1042);
            this.panelLeftSection.TabIndex = 0;
            // 
            // btnProcessRenewal
            // 
            this.btnProcessRenewal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnProcessRenewal.FlatAppearance.BorderSize = 0;
            this.btnProcessRenewal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcessRenewal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProcessRenewal.ForeColor = System.Drawing.Color.White;
            this.btnProcessRenewal.Location = new System.Drawing.Point(30, 350);
            this.btnProcessRenewal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnProcessRenewal.Name = "btnProcessRenewal";
            this.btnProcessRenewal.Size = new System.Drawing.Size(890, 50);
            this.btnProcessRenewal.TabIndex = 6;
            this.btnProcessRenewal.Text = "Process Renewal";
            this.btnProcessRenewal.UseVisualStyleBackColor = false;
            this.btnProcessRenewal.Click += new System.EventHandler(this.btnProcessRenewal_Click);
            // 
            // lblRenewalPolicy
            // 
            this.lblRenewalPolicy.AutoSize = true;
            this.lblRenewalPolicy.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRenewalPolicy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblRenewalPolicy.Location = new System.Drawing.Point(30, 280);
            this.lblRenewalPolicy.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRenewalPolicy.Name = "lblRenewalPolicy";
            this.lblRenewalPolicy.Size = new System.Drawing.Size(300, 24);
            this.lblRenewalPolicy.TabIndex = 5;
            this.lblRenewalPolicy.Text = "Max * renewals per loan *+* days each";
            // 
            // lblRenewalPolicyTitle
            // 
            this.lblRenewalPolicyTitle.AutoSize = true;
            this.lblRenewalPolicyTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRenewalPolicyTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblRenewalPolicyTitle.Location = new System.Drawing.Point(30, 250);
            this.lblRenewalPolicyTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRenewalPolicyTitle.Name = "lblRenewalPolicyTitle";
            this.lblRenewalPolicyTitle.Size = new System.Drawing.Size(150, 24);
            this.lblRenewalPolicyTitle.TabIndex = 4;
            this.lblRenewalPolicyTitle.Text = "Renewal Policy:";
            // 
            // txtTransactionID
            // 
            this.txtTransactionID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTransactionID.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTransactionID.ForeColor = System.Drawing.Color.Gray;
            this.txtTransactionID.Location = new System.Drawing.Point(30, 200);
            this.txtTransactionID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtTransactionID.Name = "txtTransactionID";
            this.txtTransactionID.Size = new System.Drawing.Size(890, 28);
            this.txtTransactionID.TabIndex = 3;
            this.txtTransactionID.Text = "Enter Transaction ID...";
            this.txtTransactionID.Enter += new System.EventHandler(this.txtTransactionID_Enter);
            this.txtTransactionID.Leave += new System.EventHandler(this.txtTransactionID_Leave);
            this.txtTransactionID.TextChanged += new System.EventHandler(this.txtTransactionID_TextChanged);
            // 
            // lblTransactionID
            // 
            this.lblTransactionID.AutoSize = true;
            this.lblTransactionID.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransactionID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTransactionID.Location = new System.Drawing.Point(30, 170);
            this.lblTransactionID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTransactionID.Name = "lblTransactionID";
            this.lblTransactionID.Size = new System.Drawing.Size(250, 24);
            this.lblTransactionID.TabIndex = 2;
            this.lblTransactionID.Text = "Transaction ID or Book ISBN";
            // 
            // lblRenewSubtitle
            // 
            this.lblRenewSubtitle.AutoSize = true;
            this.lblRenewSubtitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRenewSubtitle.ForeColor = System.Drawing.Color.Gray;
            this.lblRenewSubtitle.Location = new System.Drawing.Point(30, 80);
            this.lblRenewSubtitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRenewSubtitle.Name = "lblRenewSubtitle";
            this.lblRenewSubtitle.Size = new System.Drawing.Size(250, 24);
            this.lblRenewSubtitle.TabIndex = 1;
            this.lblRenewSubtitle.Text = "Extend the due date for a loan";
            // 
            // lblRenewTitle
            // 
            this.lblRenewTitle.AutoSize = true;
            this.lblRenewTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRenewTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblRenewTitle.Location = new System.Drawing.Point(30, 30);
            this.lblRenewTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRenewTitle.Name = "lblRenewTitle";
            this.lblRenewTitle.Size = new System.Drawing.Size(150, 31);
            this.lblRenewTitle.TabIndex = 0;
            this.lblRenewTitle.Text = "Renew Book";
            // 
            // RenewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1942, 1102);
            this.Controls.Add(this.panelMainContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "RenewForm";
            this.Text = "Renew Book";
            this.Load += new System.EventHandler(this.RenewForm_Load);
            this.panelMainContainer.ResumeLayout(false);
            this.panelRightSection.ResumeLayout(false);
            this.panelRightSection.PerformLayout();
            this.panelConditionsBox.ResumeLayout(false);
            this.panelConditionsBox.PerformLayout();
            this.panelLeftSection.ResumeLayout(false);
            this.panelLeftSection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMainContainer;
        private System.Windows.Forms.Panel panelLeftSection;
        private System.Windows.Forms.Label lblRenewTitle;
        private System.Windows.Forms.Label lblRenewSubtitle;
        private System.Windows.Forms.Label lblTransactionID;
        private System.Windows.Forms.TextBox txtTransactionID;
        private System.Windows.Forms.Label lblRenewalPolicyTitle;
        private System.Windows.Forms.Label lblRenewalPolicy;
        private System.Windows.Forms.Button btnProcessRenewal;
        private System.Windows.Forms.Panel panelRightSection;
        private System.Windows.Forms.Label lblCheckoutGuidelinesTitle;
        private System.Windows.Forms.Panel panelConditionsBox;
        private System.Windows.Forms.Label lblConditionsTitle;
        private System.Windows.Forms.Label lblCondition1;
        private System.Windows.Forms.Label lblCondition2;
        private System.Windows.Forms.Label lblCondition3;
        private System.Windows.Forms.Label lblCondition4;
    }
}
