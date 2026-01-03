namespace Project5LMS.Admin_Dashboard
{
    partial class ReturnForm
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
            this.lblMaxFine = new System.Windows.Forms.Label();
            this.lblReferenceFine = new System.Windows.Forms.Label();
            this.lblRegularFine = new System.Windows.Forms.Label();
            this.lblOverdueFinesTitle = new System.Windows.Forms.Label();
            this.lblFineCirculationTitle = new System.Windows.Forms.Label();
            this.panelLeftSection = new System.Windows.Forms.Panel();
            this.btnProcessReturn = new System.Windows.Forms.Button();
            this.lblFineMessage = new System.Windows.Forms.Label();
            this.txtBookISBNBarcode = new System.Windows.Forms.TextBox();
            this.lblBookISBNBarcode = new System.Windows.Forms.Label();
            this.lblReturnSubtitle = new System.Windows.Forms.Label();
            this.lblReturnTitle = new System.Windows.Forms.Label();
            this.panelMainContainer.SuspendLayout();
            this.panelRightSection.SuspendLayout();
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
            this.panelMainContainer.Name = "panelMainContainer";
            this.panelMainContainer.Padding = new System.Windows.Forms.Padding(22, 24, 22, 24);
            this.panelMainContainer.Size = new System.Drawing.Size(1455, 894);
            this.panelMainContainer.TabIndex = 0;
            // 
            // panelRightSection
            // 
            this.panelRightSection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRightSection.BackColor = System.Drawing.Color.White;
            this.panelRightSection.Controls.Add(this.lblMaxFine);
            this.panelRightSection.Controls.Add(this.lblReferenceFine);
            this.panelRightSection.Controls.Add(this.lblRegularFine);
            this.panelRightSection.Controls.Add(this.lblOverdueFinesTitle);
            this.panelRightSection.Controls.Add(this.lblFineCirculationTitle);
            this.panelRightSection.Location = new System.Drawing.Point(1047, 24);
            this.panelRightSection.Name = "panelRightSection";
            this.panelRightSection.Padding = new System.Windows.Forms.Padding(22, 24, 22, 24);
            this.panelRightSection.Size = new System.Drawing.Size(300, 846);
            this.panelRightSection.TabIndex = 1;
            // 
            // lblMaxFine
            // 
            this.lblMaxFine.AutoSize = true;
            this.lblMaxFine.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxFine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMaxFine.Location = new System.Drawing.Point(38, 162);
            this.lblMaxFine.Name = "lblMaxFine";
            this.lblMaxFine.Size = new System.Drawing.Size(197, 18);
            this.lblMaxFine.TabIndex = 4;
            this.lblMaxFine.Text = "• Maximum fine: ₱* per book";
            // 
            // lblReferenceFine
            // 
            this.lblReferenceFine.AutoSize = true;
            this.lblReferenceFine.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReferenceFine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblReferenceFine.Location = new System.Drawing.Point(38, 130);
            this.lblReferenceFine.Name = "lblReferenceFine";
            this.lblReferenceFine.Size = new System.Drawing.Size(205, 18);
            this.lblReferenceFine.TabIndex = 3;
            this.lblReferenceFine.Text = "• Reference Books: ₱* / a day";
            // 
            // lblRegularFine
            // 
            this.lblRegularFine.AutoSize = true;
            this.lblRegularFine.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegularFine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblRegularFine.Location = new System.Drawing.Point(38, 98);
            this.lblRegularFine.Name = "lblRegularFine";
            this.lblRegularFine.Size = new System.Drawing.Size(188, 18);
            this.lblRegularFine.TabIndex = 2;
            this.lblRegularFine.Text = "• Regular Books: ₱* / a day";
            // 
            // lblOverdueFinesTitle
            // 
            this.lblOverdueFinesTitle.AutoSize = true;
            this.lblOverdueFinesTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOverdueFinesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblOverdueFinesTitle.Location = new System.Drawing.Point(22, 65);
            this.lblOverdueFinesTitle.Name = "lblOverdueFinesTitle";
            this.lblOverdueFinesTitle.Size = new System.Drawing.Size(125, 20);
            this.lblOverdueFinesTitle.TabIndex = 1;
            this.lblOverdueFinesTitle.Text = "Overdue Fines";
            // 
            // lblFineCirculationTitle
            // 
            this.lblFineCirculationTitle.AutoSize = true;
            this.lblFineCirculationTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFineCirculationTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblFineCirculationTitle.Location = new System.Drawing.Point(22, 24);
            this.lblFineCirculationTitle.Name = "lblFineCirculationTitle";
            this.lblFineCirculationTitle.Size = new System.Drawing.Size(179, 26);
            this.lblFineCirculationTitle.TabIndex = 0;
            this.lblFineCirculationTitle.Text = "Fine Circulation";
            // 
            // panelLeftSection
            // 
            this.panelLeftSection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelLeftSection.BackColor = System.Drawing.Color.White;
            this.panelLeftSection.Controls.Add(this.btnProcessReturn);
            this.panelLeftSection.Controls.Add(this.lblFineMessage);
            this.panelLeftSection.Controls.Add(this.txtBookISBNBarcode);
            this.panelLeftSection.Controls.Add(this.lblBookISBNBarcode);
            this.panelLeftSection.Controls.Add(this.lblReturnSubtitle);
            this.panelLeftSection.Controls.Add(this.lblReturnTitle);
            this.panelLeftSection.Location = new System.Drawing.Point(22, 24);
            this.panelLeftSection.Name = "panelLeftSection";
            this.panelLeftSection.Padding = new System.Windows.Forms.Padding(22, 24, 22, 24);
            this.panelLeftSection.Size = new System.Drawing.Size(712, 846);
            this.panelLeftSection.TabIndex = 0;
            // 
            // btnProcessReturn
            // 
            this.btnProcessReturn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnProcessReturn.FlatAppearance.BorderSize = 0;
            this.btnProcessReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcessReturn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProcessReturn.ForeColor = System.Drawing.Color.White;
            this.btnProcessReturn.Location = new System.Drawing.Point(22, 244);
            this.btnProcessReturn.Name = "btnProcessReturn";
            this.btnProcessReturn.Size = new System.Drawing.Size(668, 41);
            this.btnProcessReturn.TabIndex = 5;
            this.btnProcessReturn.Text = "Process Return";
            this.btnProcessReturn.UseVisualStyleBackColor = false;
            this.btnProcessReturn.Click += new System.EventHandler(this.btnProcessReturn_Click);
            // 
            // lblFineMessage
            // 
            this.lblFineMessage.AutoSize = true;
            this.lblFineMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFineMessage.ForeColor = System.Drawing.Color.Gray;
            this.lblFineMessage.Location = new System.Drawing.Point(22, 203);
            this.lblFineMessage.Name = "lblFineMessage";
            this.lblFineMessage.Size = new System.Drawing.Size(180, 18);
            this.lblFineMessage.TabIndex = 4;
            this.lblFineMessage.Text = "No overdue fines detected";
            // 
            // txtBookISBNBarcode
            // 
            this.txtBookISBNBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBookISBNBarcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBookISBNBarcode.ForeColor = System.Drawing.Color.Gray;
            this.txtBookISBNBarcode.Location = new System.Drawing.Point(22, 162);
            this.txtBookISBNBarcode.Name = "txtBookISBNBarcode";
            this.txtBookISBNBarcode.Size = new System.Drawing.Size(668, 24);
            this.txtBookISBNBarcode.TabIndex = 3;
            this.txtBookISBNBarcode.Text = "Scan or enter ISBN";
            this.txtBookISBNBarcode.TextChanged += new System.EventHandler(this.txtBookISBNBarcode_TextChanged);
            this.txtBookISBNBarcode.Enter += new System.EventHandler(this.txtBookISBNBarcode_Enter);
            this.txtBookISBNBarcode.Leave += new System.EventHandler(this.txtBookISBNBarcode_Leave);
            // 
            // lblBookISBNBarcode
            // 
            this.lblBookISBNBarcode.AutoSize = true;
            this.lblBookISBNBarcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBookISBNBarcode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblBookISBNBarcode.Location = new System.Drawing.Point(22, 138);
            this.lblBookISBNBarcode.Name = "lblBookISBNBarcode";
            this.lblBookISBNBarcode.Size = new System.Drawing.Size(160, 18);
            this.lblBookISBNBarcode.TabIndex = 2;
            this.lblBookISBNBarcode.Text = "Book ISBN or Barcode";
            // 
            // lblReturnSubtitle
            // 
            this.lblReturnSubtitle.AutoSize = true;
            this.lblReturnSubtitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReturnSubtitle.ForeColor = System.Drawing.Color.Gray;
            this.lblReturnSubtitle.Location = new System.Drawing.Point(22, 65);
            this.lblReturnSubtitle.Name = "lblReturnSubtitle";
            this.lblReturnSubtitle.Size = new System.Drawing.Size(156, 18);
            this.lblReturnSubtitle.TabIndex = 1;
            this.lblReturnSubtitle.Text = "Process a book return";
            // 
            // lblReturnTitle
            // 
            this.lblReturnTitle.AutoSize = true;
            this.lblReturnTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReturnTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblReturnTitle.Location = new System.Drawing.Point(22, 24);
            this.lblReturnTitle.Name = "lblReturnTitle";
            this.lblReturnTitle.Size = new System.Drawing.Size(144, 26);
            this.lblReturnTitle.TabIndex = 0;
            this.lblReturnTitle.Text = "Return Book";
            // 
            // ReturnForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1455, 894);
            this.Controls.Add(this.panelMainContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ReturnForm";
            this.Text = "Return Book";
            this.Load += new System.EventHandler(this.ReturnForm_Load);
            this.panelMainContainer.ResumeLayout(false);
            this.panelRightSection.ResumeLayout(false);
            this.panelRightSection.PerformLayout();
            this.panelLeftSection.ResumeLayout(false);
            this.panelLeftSection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMainContainer;
        private System.Windows.Forms.Panel panelLeftSection;
        private System.Windows.Forms.Label lblReturnTitle;
        private System.Windows.Forms.Label lblReturnSubtitle;
        private System.Windows.Forms.Label lblBookISBNBarcode;
        private System.Windows.Forms.TextBox txtBookISBNBarcode;
        private System.Windows.Forms.Label lblFineMessage;
        private System.Windows.Forms.Button btnProcessReturn;
        private System.Windows.Forms.Panel panelRightSection;
        private System.Windows.Forms.Label lblFineCirculationTitle;
        private System.Windows.Forms.Label lblOverdueFinesTitle;
        private System.Windows.Forms.Label lblRegularFine;
        private System.Windows.Forms.Label lblReferenceFine;
        private System.Windows.Forms.Label lblMaxFine;
    }
}
