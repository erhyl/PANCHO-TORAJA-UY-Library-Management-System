namespace Project5LMS.Admin_Dashboard
{
    partial class HistoryForm
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
            this.panelBottomSection = new System.Windows.Forms.Panel();
            this.dta_History = new System.Windows.Forms.DataGridView();
            this.panelTopSection = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.picSearchIcon = new System.Windows.Forms.PictureBox();
            this.lblHistoryTitle = new System.Windows.Forms.Label();
            this.panelMainContainer.SuspendLayout();
            this.panelBottomSection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dta_History)).BeginInit();
            this.panelTopSection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSearchIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMainContainer
            // 
            this.panelMainContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.panelMainContainer.Controls.Add(this.panelBottomSection);
            this.panelMainContainer.Controls.Add(this.panelTopSection);
            this.panelMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainContainer.Location = new System.Drawing.Point(0, 0);
            this.panelMainContainer.Name = "panelMainContainer";
            this.panelMainContainer.Padding = new System.Windows.Forms.Padding(22, 24, 22, 24);
            this.panelMainContainer.Size = new System.Drawing.Size(1455, 894);
            this.panelMainContainer.TabIndex = 0;
            // 
            // panelBottomSection
            // 
            this.panelBottomSection.BackColor = System.Drawing.Color.White;
            this.panelBottomSection.Controls.Add(this.dta_History);
            this.panelBottomSection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBottomSection.Location = new System.Drawing.Point(22, 81);
            this.panelBottomSection.Name = "panelBottomSection";
            this.panelBottomSection.Padding = new System.Windows.Forms.Padding(15, 16, 15, 16);
            this.panelBottomSection.Size = new System.Drawing.Size(1411, 789);
            this.panelBottomSection.TabIndex = 1;
            // 
            // dta_History
            // 
            this.dta_History.AllowUserToAddRows = false;
            this.dta_History.AllowUserToDeleteRows = false;
            this.dta_History.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dta_History.BackgroundColor = System.Drawing.Color.White;
            this.dta_History.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dta_History.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dta_History.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dta_History.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dta_History.Location = new System.Drawing.Point(15, 16);
            this.dta_History.MultiSelect = false;
            this.dta_History.Name = "dta_History";
            this.dta_History.ReadOnly = true;
            this.dta_History.RowHeadersVisible = false;
            this.dta_History.RowTemplate.Height = 50;
            this.dta_History.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dta_History.Size = new System.Drawing.Size(1381, 757);
            this.dta_History.TabIndex = 0;
            // 
            // panelTopSection
            // 
            this.panelTopSection.BackColor = System.Drawing.Color.White;
            this.panelTopSection.Controls.Add(this.txtSearch);
            this.panelTopSection.Controls.Add(this.picSearchIcon);
            this.panelTopSection.Controls.Add(this.lblHistoryTitle);
            this.panelTopSection.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTopSection.Location = new System.Drawing.Point(22, 24);
            this.panelTopSection.Name = "panelTopSection";
            this.panelTopSection.Padding = new System.Windows.Forms.Padding(15, 16, 15, 16);
            this.panelTopSection.Size = new System.Drawing.Size(1411, 57);
            this.panelTopSection.TabIndex = 0;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.Color.Gray;
            this.txtSearch.Location = new System.Drawing.Point(1124, 16);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(272, 24);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.Text = "Search transactions...";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            // 
            // picSearchIcon
            // 
            this.picSearchIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picSearchIcon.BackColor = System.Drawing.Color.Transparent;
            this.picSearchIcon.Location = new System.Drawing.Point(1101, 16);
            this.picSearchIcon.Name = "picSearchIcon";
            this.picSearchIcon.Size = new System.Drawing.Size(22, 24);
            this.picSearchIcon.TabIndex = 1;
            this.picSearchIcon.TabStop = false;
            // 
            // lblHistoryTitle
            // 
            this.lblHistoryTitle.AutoSize = true;
            this.lblHistoryTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHistoryTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblHistoryTitle.Location = new System.Drawing.Point(15, 16);
            this.lblHistoryTitle.Name = "lblHistoryTitle";
            this.lblHistoryTitle.Size = new System.Drawing.Size(217, 26);
            this.lblHistoryTitle.TabIndex = 0;
            this.lblHistoryTitle.Text = "Transaction History";
            // 
            // HistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1455, 894);
            this.Controls.Add(this.panelMainContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "HistoryForm";
            this.Text = "Transaction History";
            this.Load += new System.EventHandler(this.HistoryForm_Load);
            this.panelMainContainer.ResumeLayout(false);
            this.panelBottomSection.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dta_History)).EndInit();
            this.panelTopSection.ResumeLayout(false);
            this.panelTopSection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSearchIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMainContainer;
        private System.Windows.Forms.Panel panelTopSection;
        private System.Windows.Forms.Label lblHistoryTitle;
        private System.Windows.Forms.PictureBox picSearchIcon;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Panel panelBottomSection;
        private System.Windows.Forms.DataGridView dta_History;
    }
}
