namespace Project5LMS.Forms.Admin.Catalog
{
    partial class ManageBookCopiesForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelMainContainer;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Panel panelBookInfo;
        private System.Windows.Forms.Label lblBookTitle;
        private System.Windows.Forms.Label lblBookAuthor;
        private System.Windows.Forms.Label lblBookISBN;
        private System.Windows.Forms.Label lblTotalCopies;
        private System.Windows.Forms.Label lblAvailableCopies;
        private System.Windows.Forms.Panel panelCopies;
        private System.Windows.Forms.DataGridView dataGridViewCopies;
        private System.Windows.Forms.Label lblSummary;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnAddCopy;
        private System.Windows.Forms.Button btnUpdateStatus;
        private System.Windows.Forms.Button btnUpdateLocation;
        private System.Windows.Forms.Button btnDeleteCopy;
        private System.Windows.Forms.Button btnClose;
        
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }
        
        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.panelMainContainer = new System.Windows.Forms.Panel();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnDeleteCopy = new System.Windows.Forms.Button();
            this.btnUpdateLocation = new System.Windows.Forms.Button();
            this.btnUpdateStatus = new System.Windows.Forms.Button();
            this.btnAddCopy = new System.Windows.Forms.Button();
            this.panelCopies = new System.Windows.Forms.Panel();
            this.lblSummary = new System.Windows.Forms.Label();
            this.dataGridViewCopies = new System.Windows.Forms.DataGridView();
            this.panelBookInfo = new System.Windows.Forms.Panel();
            this.lblAvailableCopies = new System.Windows.Forms.Label();
            this.lblTotalCopies = new System.Windows.Forms.Label();
            this.lblBookISBN = new System.Windows.Forms.Label();
            this.lblBookAuthor = new System.Windows.Forms.Label();
            this.lblBookTitle = new System.Windows.Forms.Label();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.panelMainContainer.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.panelCopies.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCopies)).BeginInit();
            this.panelBookInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMainContainer
            // 
            this.panelMainContainer.BackColor = System.Drawing.Color.White;
            this.panelMainContainer.Controls.Add(this.panelButtons);
            this.panelMainContainer.Controls.Add(this.panelCopies);
            this.panelMainContainer.Controls.Add(this.panelBookInfo);
            this.panelMainContainer.Controls.Add(this.lblFormTitle);
            this.panelMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainContainer.Location = new System.Drawing.Point(0, 0);
            this.panelMainContainer.Margin = new System.Windows.Forms.Padding(4);
            this.panelMainContainer.Name = "panelMainContainer";
            this.panelMainContainer.Padding = new System.Windows.Forms.Padding(20);
            this.panelMainContainer.Size = new System.Drawing.Size(1000, 700);
            this.panelMainContainer.TabIndex = 0;
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnClose);
            this.panelButtons.Controls.Add(this.btnDeleteCopy);
            this.panelButtons.Controls.Add(this.btnUpdateLocation);
            this.panelButtons.Controls.Add(this.btnUpdateStatus);
            this.panelButtons.Controls.Add(this.btnAddCopy);
            this.panelButtons.Location = new System.Drawing.Point(20, 576);
            this.panelButtons.Margin = new System.Windows.Forms.Padding(4);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(960, 50);
            this.panelButtons.TabIndex = 3;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(810, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(150, 50);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnDeleteCopy
            // 
            this.btnDeleteCopy.BackColor = System.Drawing.Color.Maroon;
            this.btnDeleteCopy.FlatAppearance.BorderSize = 0;
            this.btnDeleteCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteCopy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteCopy.ForeColor = System.Drawing.Color.White;
            this.btnDeleteCopy.Location = new System.Drawing.Point(597, 0);
            this.btnDeleteCopy.Margin = new System.Windows.Forms.Padding(4);
            this.btnDeleteCopy.Name = "btnDeleteCopy";
            this.btnDeleteCopy.Size = new System.Drawing.Size(150, 50);
            this.btnDeleteCopy.TabIndex = 3;
            this.btnDeleteCopy.Text = "Delete Copy";
            this.btnDeleteCopy.UseVisualStyleBackColor = false;
            this.btnDeleteCopy.Click += new System.EventHandler(this.btnDeleteCopy_Click);
            // 
            // btnUpdateLocation
            // 
            this.btnUpdateLocation.BackColor = System.Drawing.Color.Maroon;
            this.btnUpdateLocation.FlatAppearance.BorderSize = 0;
            this.btnUpdateLocation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateLocation.ForeColor = System.Drawing.Color.White;
            this.btnUpdateLocation.Location = new System.Drawing.Point(411, 0);
            this.btnUpdateLocation.Margin = new System.Windows.Forms.Padding(4);
            this.btnUpdateLocation.Name = "btnUpdateLocation";
            this.btnUpdateLocation.Size = new System.Drawing.Size(168, 50);
            this.btnUpdateLocation.TabIndex = 2;
            this.btnUpdateLocation.Text = "Update Location";
            this.btnUpdateLocation.UseVisualStyleBackColor = false;
            this.btnUpdateLocation.Click += new System.EventHandler(this.btnUpdateLocation_Click);
            // 
            // btnUpdateStatus
            // 
            this.btnUpdateStatus.BackColor = System.Drawing.Color.Maroon;
            this.btnUpdateStatus.FlatAppearance.BorderSize = 0;
            this.btnUpdateStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateStatus.ForeColor = System.Drawing.Color.White;
            this.btnUpdateStatus.Location = new System.Drawing.Point(172, 0);
            this.btnUpdateStatus.Margin = new System.Windows.Forms.Padding(4);
            this.btnUpdateStatus.Name = "btnUpdateStatus";
            this.btnUpdateStatus.Size = new System.Drawing.Size(171, 50);
            this.btnUpdateStatus.TabIndex = 1;
            this.btnUpdateStatus.Text = "Update Status";
            this.btnUpdateStatus.UseVisualStyleBackColor = false;
            this.btnUpdateStatus.Click += new System.EventHandler(this.btnUpdateStatus_Click);
            // 
            // btnAddCopy
            // 
            this.btnAddCopy.BackColor = System.Drawing.Color.Maroon;
            this.btnAddCopy.FlatAppearance.BorderSize = 0;
            this.btnAddCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddCopy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddCopy.ForeColor = System.Drawing.Color.White;
            this.btnAddCopy.Location = new System.Drawing.Point(0, 0);
            this.btnAddCopy.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddCopy.Name = "btnAddCopy";
            this.btnAddCopy.Size = new System.Drawing.Size(150, 50);
            this.btnAddCopy.TabIndex = 0;
            this.btnAddCopy.Text = "Add Copy";
            this.btnAddCopy.UseVisualStyleBackColor = false;
            this.btnAddCopy.Click += new System.EventHandler(this.btnAddCopy_Click);
            // 
            // panelCopies
            // 
            this.panelCopies.Controls.Add(this.lblSummary);
            this.panelCopies.Controls.Add(this.dataGridViewCopies);
            this.panelCopies.Location = new System.Drawing.Point(20, 168);
            this.panelCopies.Margin = new System.Windows.Forms.Padding(4);
            this.panelCopies.Name = "panelCopies";
            this.panelCopies.Size = new System.Drawing.Size(960, 400);
            this.panelCopies.TabIndex = 2;
            // 
            // lblSummary
            // 
            this.lblSummary.AutoSize = true;
            this.lblSummary.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSummary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblSummary.Location = new System.Drawing.Point(0, 370);
            this.lblSummary.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Padding = new System.Windows.Forms.Padding(5);
            this.lblSummary.Size = new System.Drawing.Size(95, 30);
            this.lblSummary.TabIndex = 1;
            this.lblSummary.Text = "Summary:";
            // 
            // dataGridViewCopies
            // 
            this.dataGridViewCopies.AllowUserToAddRows = false;
            this.dataGridViewCopies.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewCopies.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewCopies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCopies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewCopies.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewCopies.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewCopies.Name = "dataGridViewCopies";
            this.dataGridViewCopies.RowHeadersWidth = 51;
            this.dataGridViewCopies.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewCopies.Size = new System.Drawing.Size(960, 400);
            this.dataGridViewCopies.TabIndex = 0;
            this.dataGridViewCopies.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCopies_CellEndEdit);
            // 
            // panelBookInfo
            // 
            this.panelBookInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.panelBookInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBookInfo.Controls.Add(this.lblAvailableCopies);
            this.panelBookInfo.Controls.Add(this.lblTotalCopies);
            this.panelBookInfo.Controls.Add(this.lblBookISBN);
            this.panelBookInfo.Controls.Add(this.lblBookAuthor);
            this.panelBookInfo.Controls.Add(this.lblBookTitle);
            this.panelBookInfo.Location = new System.Drawing.Point(20, 60);
            this.panelBookInfo.Margin = new System.Windows.Forms.Padding(4);
            this.panelBookInfo.Name = "panelBookInfo";
            this.panelBookInfo.Padding = new System.Windows.Forms.Padding(15);
            this.panelBookInfo.Size = new System.Drawing.Size(960, 100);
            this.panelBookInfo.TabIndex = 1;
            // 
            // lblAvailableCopies
            // 
            this.lblAvailableCopies.AutoSize = true;
            this.lblAvailableCopies.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvailableCopies.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblAvailableCopies.Location = new System.Drawing.Point(700, 40);
            this.lblAvailableCopies.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAvailableCopies.Name = "lblAvailableCopies";
            this.lblAvailableCopies.Size = new System.Drawing.Size(91, 20);
            this.lblAvailableCopies.TabIndex = 4;
            this.lblAvailableCopies.Text = "Available:";
            // 
            // lblTotalCopies
            // 
            this.lblTotalCopies.AutoSize = true;
            this.lblTotalCopies.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCopies.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTotalCopies.Location = new System.Drawing.Point(700, 15);
            this.lblTotalCopies.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalCopies.Name = "lblTotalCopies";
            this.lblTotalCopies.Size = new System.Drawing.Size(121, 20);
            this.lblTotalCopies.TabIndex = 3;
            this.lblTotalCopies.Text = "Total Copies:";
            // 
            // lblBookISBN
            // 
            this.lblBookISBN.AutoSize = true;
            this.lblBookISBN.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBookISBN.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblBookISBN.Location = new System.Drawing.Point(15, 60);
            this.lblBookISBN.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBookISBN.Name = "lblBookISBN";
            this.lblBookISBN.Size = new System.Drawing.Size(53, 20);
            this.lblBookISBN.TabIndex = 2;
            this.lblBookISBN.Text = "ISBN:";
            // 
            // lblBookAuthor
            // 
            this.lblBookAuthor.AutoSize = true;
            this.lblBookAuthor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBookAuthor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblBookAuthor.Location = new System.Drawing.Point(15, 40);
            this.lblBookAuthor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBookAuthor.Name = "lblBookAuthor";
            this.lblBookAuthor.Size = new System.Drawing.Size(63, 20);
            this.lblBookAuthor.TabIndex = 1;
            this.lblBookAuthor.Text = "Author:";
            // 
            // lblBookTitle
            // 
            this.lblBookTitle.AutoSize = true;
            this.lblBookTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBookTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblBookTitle.Location = new System.Drawing.Point(15, 15);
            this.lblBookTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBookTitle.Name = "lblBookTitle";
            this.lblBookTitle.Size = new System.Drawing.Size(109, 25);
            this.lblBookTitle.TabIndex = 0;
            this.lblBookTitle.Text = "Book Title";
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblFormTitle.Location = new System.Drawing.Point(20, 20);
            this.lblFormTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(318, 36);
            this.lblFormTitle.TabIndex = 0;
            this.lblFormTitle.Text = "Manage Book Copies";
            // 
            // ManageBookCopiesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.panelMainContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1000, 700);
            this.Name = "ManageBookCopiesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage Book Copies";
            this.Load += new System.EventHandler(this.ManageBookCopiesForm_Load);
            this.panelMainContainer.ResumeLayout(false);
            this.panelMainContainer.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.panelCopies.ResumeLayout(false);
            this.panelCopies.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCopies)).EndInit();
            this.panelBookInfo.ResumeLayout(false);
            this.panelBookInfo.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion
    }
}
