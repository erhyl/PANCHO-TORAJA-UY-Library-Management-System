namespace Project5LMS.Admin_Dashboard
{
    partial class MembersForm
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
            this.panelMainContainer = new System.Windows.Forms.Panel();
            this.panelBottomSection = new System.Windows.Forms.Panel();
            this.dta_Members = new System.Windows.Forms.DataGridView();
            this.panelSearchFilters = new System.Windows.Forms.Panel();
            this.btnAddMember = new System.Windows.Forms.Button();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.cmbTypes = new System.Windows.Forms.ComboBox();
            this.panelSearchBox = new System.Windows.Forms.Panel();
            this.picSearchIcon = new System.Windows.Forms.PictureBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.panelMetricsContainer = new System.Windows.Forms.Panel();
            this.panelMetricCard4 = new System.Windows.Forms.Panel();
            this.lblMetricValue4 = new System.Windows.Forms.Label();
            this.lblMetricTitle4 = new System.Windows.Forms.Label();
            this.panelMetricCard3 = new System.Windows.Forms.Panel();
            this.lblMetricValue3 = new System.Windows.Forms.Label();
            this.lblMetricTitle3 = new System.Windows.Forms.Label();
            this.panelMetricCard2 = new System.Windows.Forms.Panel();
            this.lblMetricValue2 = new System.Windows.Forms.Label();
            this.lblMetricTitle2 = new System.Windows.Forms.Label();
            this.panelMetricCard1 = new System.Windows.Forms.Panel();
            this.lblMetricValue1 = new System.Windows.Forms.Label();
            this.lblMetricTitle1 = new System.Windows.Forms.Label();
            this.panelMainContainer.SuspendLayout();
            this.panelBottomSection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dta_Members)).BeginInit();
            this.panelSearchFilters.SuspendLayout();
            this.panelSearchBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSearchIcon)).BeginInit();
            this.panelMetricsContainer.SuspendLayout();
            this.panelMetricCard4.SuspendLayout();
            this.panelMetricCard3.SuspendLayout();
            this.panelMetricCard2.SuspendLayout();
            this.panelMetricCard1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMainContainer
            // 
            this.panelMainContainer.AutoScroll = true;
            this.panelMainContainer.BackColor = System.Drawing.Color.White;
            this.panelMainContainer.Controls.Add(this.panelBottomSection);
            this.panelMainContainer.Controls.Add(this.panelSearchFilters);
            this.panelMainContainer.Controls.Add(this.panelMetricsContainer);
            this.panelMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainContainer.Location = new System.Drawing.Point(0, 0);
            this.panelMainContainer.Margin = new System.Windows.Forms.Padding(4);
            this.panelMainContainer.Name = "panelMainContainer";
            this.panelMainContainer.Padding = new System.Windows.Forms.Padding(30);
            this.panelMainContainer.Size = new System.Drawing.Size(1942, 1102);
            this.panelMainContainer.TabIndex = 0;
            // 
            // panelBottomSection
            // 
            this.panelBottomSection.Controls.Add(this.dta_Members);
            this.panelBottomSection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBottomSection.Location = new System.Drawing.Point(30, 250);
            this.panelBottomSection.Margin = new System.Windows.Forms.Padding(4);
            this.panelBottomSection.Name = "panelBottomSection";
            this.panelBottomSection.Size = new System.Drawing.Size(1882, 822);
            this.panelBottomSection.TabIndex = 2;
            // 
            // dta_Members
            // 
            this.dta_Members.AllowUserToAddRows = false;
            this.dta_Members.AllowUserToDeleteRows = false;
            this.dta_Members.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dta_Members.BackgroundColor = System.Drawing.Color.White;
            this.dta_Members.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dta_Members.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dta_Members.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dta_Members.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dta_Members.Location = new System.Drawing.Point(0, 0);
            this.dta_Members.Margin = new System.Windows.Forms.Padding(4);
            this.dta_Members.MultiSelect = false;
            this.dta_Members.Name = "dta_Members";
            this.dta_Members.ReadOnly = true;
            this.dta_Members.RowHeadersVisible = false;
            this.dta_Members.RowHeadersWidth = 51;
            this.dta_Members.RowTemplate.Height = 60;
            this.dta_Members.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dta_Members.Size = new System.Drawing.Size(1882, 822);
            this.dta_Members.TabIndex = 0;
            this.dta_Members.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dta_Members_CellContentClick);
            this.dta_Members.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dta_Members_CellDoubleClick);
            // 
            // panelSearchFilters
            // 
            this.panelSearchFilters.Controls.Add(this.btnAddMember);
            this.panelSearchFilters.Controls.Add(this.cmbStatus);
            this.panelSearchFilters.Controls.Add(this.cmbTypes);
            this.panelSearchFilters.Controls.Add(this.panelSearchBox);
            this.panelSearchFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearchFilters.Location = new System.Drawing.Point(30, 190);
            this.panelSearchFilters.Margin = new System.Windows.Forms.Padding(4);
            this.panelSearchFilters.Name = "panelSearchFilters";
            this.panelSearchFilters.Size = new System.Drawing.Size(1882, 60);
            this.panelSearchFilters.TabIndex = 1;
            // 
            // btnAddMember
            // 
            this.btnAddMember.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddMember.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.btnAddMember.FlatAppearance.BorderSize = 0;
            this.btnAddMember.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddMember.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddMember.ForeColor = System.Drawing.Color.White;
            this.btnAddMember.Location = new System.Drawing.Point(1680, 10);
            this.btnAddMember.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddMember.Name = "btnAddMember";
            this.btnAddMember.Size = new System.Drawing.Size(200, 40);
            this.btnAddMember.TabIndex = 3;
            this.btnAddMember.Text = "+ Add New Member";
            this.btnAddMember.UseVisualStyleBackColor = false;
            this.btnAddMember.Click += new System.EventHandler(this.btnAddMember_Click);
            // 
            // cmbStatus
            // 
            this.cmbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(1520, 15);
            this.cmbStatus.Margin = new System.Windows.Forms.Padding(4);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(150, 30);
            this.cmbStatus.TabIndex = 2;
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);
            // 
            // cmbTypes
            // 
            this.cmbTypes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypes.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTypes.FormattingEnabled = true;
            this.cmbTypes.Location = new System.Drawing.Point(1362, 15);
            this.cmbTypes.Margin = new System.Windows.Forms.Padding(4);
            this.cmbTypes.Name = "cmbTypes";
            this.cmbTypes.Size = new System.Drawing.Size(150, 30);
            this.cmbTypes.TabIndex = 1;
            this.cmbTypes.SelectedIndexChanged += new System.EventHandler(this.cmbTypes_SelectedIndexChanged);
            // 
            // panelSearchBox
            // 
            this.panelSearchBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSearchBox.BackColor = System.Drawing.Color.White;
            this.panelSearchBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSearchBox.Controls.Add(this.picSearchIcon);
            this.panelSearchBox.Controls.Add(this.txtSearch);
            this.panelSearchBox.Location = new System.Drawing.Point(0, 15);
            this.panelSearchBox.Margin = new System.Windows.Forms.Padding(4);
            this.panelSearchBox.Name = "panelSearchBox";
            this.panelSearchBox.Size = new System.Drawing.Size(1350, 40);
            this.panelSearchBox.TabIndex = 4;
            // 
            // picSearchIcon
            // 
            this.picSearchIcon.BackColor = System.Drawing.Color.Transparent;
            this.picSearchIcon.Location = new System.Drawing.Point(10, 8);
            this.picSearchIcon.Margin = new System.Windows.Forms.Padding(4);
            this.picSearchIcon.Name = "picSearchIcon";
            this.picSearchIcon.Size = new System.Drawing.Size(24, 24);
            this.picSearchIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picSearchIcon.TabIndex = 1;
            this.picSearchIcon.TabStop = false;
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.Color.Gray;
            this.txtSearch.Location = new System.Drawing.Point(45, 8);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(1300, 21);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.Text = "Search by MemberID or email";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            // 
            // panelMetricsContainer
            // 
            this.panelMetricsContainer.Controls.Add(this.panelMetricCard4);
            this.panelMetricsContainer.Controls.Add(this.panelMetricCard3);
            this.panelMetricsContainer.Controls.Add(this.panelMetricCard2);
            this.panelMetricsContainer.Controls.Add(this.panelMetricCard1);
            this.panelMetricsContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMetricsContainer.Location = new System.Drawing.Point(30, 30);
            this.panelMetricsContainer.Margin = new System.Windows.Forms.Padding(4);
            this.panelMetricsContainer.Name = "panelMetricsContainer";
            this.panelMetricsContainer.Size = new System.Drawing.Size(1882, 160);
            this.panelMetricsContainer.TabIndex = 0;
            // 
            // panelMetricCard4
            // 
            this.panelMetricCard4.BackColor = System.Drawing.Color.White;
            this.panelMetricCard4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMetricCard4.Controls.Add(this.lblMetricValue4);
            this.panelMetricCard4.Controls.Add(this.lblMetricTitle4);
            this.panelMetricCard4.Location = new System.Drawing.Point(1410, 0);
            this.panelMetricCard4.Margin = new System.Windows.Forms.Padding(4);
            this.panelMetricCard4.Name = "panelMetricCard4";
            this.panelMetricCard4.Size = new System.Drawing.Size(220, 120);
            this.panelMetricCard4.TabIndex = 3;
            // 
            // lblMetricValue4
            // 
            this.lblMetricValue4.AutoSize = true;
            this.lblMetricValue4.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricValue4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricValue4.Location = new System.Drawing.Point(15, 60);
            this.lblMetricValue4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricValue4.Name = "lblMetricValue4";
            this.lblMetricValue4.Size = new System.Drawing.Size(43, 46);
            this.lblMetricValue4.TabIndex = 1;
            this.lblMetricValue4.Text = "0";
            // 
            // lblMetricTitle4
            // 
            this.lblMetricTitle4.AutoSize = true;
            this.lblMetricTitle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTitle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricTitle4.Location = new System.Drawing.Point(15, 20);
            this.lblMetricTitle4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricTitle4.Name = "lblMetricTitle4";
            this.lblMetricTitle4.Size = new System.Drawing.Size(108, 24);
            this.lblMetricTitle4.TabIndex = 0;
            this.lblMetricTitle4.Text = "Suspended";
            // 
            // panelMetricCard3
            // 
            this.panelMetricCard3.BackColor = System.Drawing.Color.White;
            this.panelMetricCard3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMetricCard3.Controls.Add(this.lblMetricValue3);
            this.panelMetricCard3.Controls.Add(this.lblMetricTitle3);
            this.panelMetricCard3.Location = new System.Drawing.Point(940, 0);
            this.panelMetricCard3.Margin = new System.Windows.Forms.Padding(4);
            this.panelMetricCard3.Name = "panelMetricCard3";
            this.panelMetricCard3.Size = new System.Drawing.Size(220, 120);
            this.panelMetricCard3.TabIndex = 2;
            // 
            // lblMetricValue3
            // 
            this.lblMetricValue3.AutoSize = true;
            this.lblMetricValue3.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricValue3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricValue3.Location = new System.Drawing.Point(15, 60);
            this.lblMetricValue3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricValue3.Name = "lblMetricValue3";
            this.lblMetricValue3.Size = new System.Drawing.Size(43, 46);
            this.lblMetricValue3.TabIndex = 1;
            this.lblMetricValue3.Text = "0";
            // 
            // lblMetricTitle3
            // 
            this.lblMetricTitle3.AutoSize = true;
            this.lblMetricTitle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTitle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricTitle3.Location = new System.Drawing.Point(15, 20);
            this.lblMetricTitle3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricTitle3.Name = "lblMetricTitle3";
            this.lblMetricTitle3.Size = new System.Drawing.Size(73, 24);
            this.lblMetricTitle3.TabIndex = 0;
            this.lblMetricTitle3.Text = "Inactive";
            // 
            // panelMetricCard2
            // 
            this.panelMetricCard2.BackColor = System.Drawing.Color.White;
            this.panelMetricCard2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMetricCard2.Controls.Add(this.lblMetricValue2);
            this.panelMetricCard2.Controls.Add(this.lblMetricTitle2);
            this.panelMetricCard2.Location = new System.Drawing.Point(470, 0);
            this.panelMetricCard2.Margin = new System.Windows.Forms.Padding(4);
            this.panelMetricCard2.Name = "panelMetricCard2";
            this.panelMetricCard2.Size = new System.Drawing.Size(220, 120);
            this.panelMetricCard2.TabIndex = 1;
            // 
            // lblMetricValue2
            // 
            this.lblMetricValue2.AutoSize = true;
            this.lblMetricValue2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricValue2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricValue2.Location = new System.Drawing.Point(15, 60);
            this.lblMetricValue2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricValue2.Name = "lblMetricValue2";
            this.lblMetricValue2.Size = new System.Drawing.Size(43, 46);
            this.lblMetricValue2.TabIndex = 1;
            this.lblMetricValue2.Text = "0";
            // 
            // lblMetricTitle2
            // 
            this.lblMetricTitle2.AutoSize = true;
            this.lblMetricTitle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTitle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricTitle2.Location = new System.Drawing.Point(15, 20);
            this.lblMetricTitle2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricTitle2.Name = "lblMetricTitle2";
            this.lblMetricTitle2.Size = new System.Drawing.Size(61, 24);
            this.lblMetricTitle2.TabIndex = 0;
            this.lblMetricTitle2.Text = "Active";
            // 
            // panelMetricCard1
            // 
            this.panelMetricCard1.BackColor = System.Drawing.Color.White;
            this.panelMetricCard1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMetricCard1.Controls.Add(this.lblMetricValue1);
            this.panelMetricCard1.Controls.Add(this.lblMetricTitle1);
            this.panelMetricCard1.Location = new System.Drawing.Point(0, 0);
            this.panelMetricCard1.Margin = new System.Windows.Forms.Padding(4);
            this.panelMetricCard1.Name = "panelMetricCard1";
            this.panelMetricCard1.Size = new System.Drawing.Size(220, 120);
            this.panelMetricCard1.TabIndex = 0;
            // 
            // lblMetricValue1
            // 
            this.lblMetricValue1.AutoSize = true;
            this.lblMetricValue1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricValue1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricValue1.Location = new System.Drawing.Point(15, 60);
            this.lblMetricValue1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricValue1.Name = "lblMetricValue1";
            this.lblMetricValue1.Size = new System.Drawing.Size(43, 46);
            this.lblMetricValue1.TabIndex = 1;
            this.lblMetricValue1.Text = "0";
            // 
            // lblMetricTitle1
            // 
            this.lblMetricTitle1.AutoSize = true;
            this.lblMetricTitle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTitle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricTitle1.Location = new System.Drawing.Point(15, 20);
            this.lblMetricTitle1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetricTitle1.Name = "lblMetricTitle1";
            this.lblMetricTitle1.Size = new System.Drawing.Size(136, 24);
            this.lblMetricTitle1.TabIndex = 0;
            this.lblMetricTitle1.Text = "Total Members";
            // 
            // MembersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1942, 1102);
            this.Controls.Add(this.panelMainContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MembersForm";
            this.Text = "Members";
            this.Load += new System.EventHandler(this.MembersForm_Load);
            this.panelMainContainer.ResumeLayout(false);
            this.panelBottomSection.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dta_Members)).EndInit();
            this.panelSearchFilters.ResumeLayout(false);
            this.panelSearchBox.ResumeLayout(false);
            this.panelSearchBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSearchIcon)).EndInit();
            this.panelMetricsContainer.ResumeLayout(false);
            this.panelMetricCard4.ResumeLayout(false);
            this.panelMetricCard4.PerformLayout();
            this.panelMetricCard3.ResumeLayout(false);
            this.panelMetricCard3.PerformLayout();
            this.panelMetricCard2.ResumeLayout(false);
            this.panelMetricCard2.PerformLayout();
            this.panelMetricCard1.ResumeLayout(false);
            this.panelMetricCard1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMainContainer;
        private System.Windows.Forms.Panel panelMetricsContainer;
        private System.Windows.Forms.Panel panelMetricCard1;
        private System.Windows.Forms.Label lblMetricTitle1;
        private System.Windows.Forms.Label lblMetricValue1;
        private System.Windows.Forms.Panel panelMetricCard2;
        private System.Windows.Forms.Label lblMetricValue2;
        private System.Windows.Forms.Label lblMetricTitle2;
        private System.Windows.Forms.Panel panelMetricCard3;
        private System.Windows.Forms.Label lblMetricValue3;
        private System.Windows.Forms.Label lblMetricTitle3;
        private System.Windows.Forms.Panel panelMetricCard4;
        private System.Windows.Forms.Label lblMetricValue4;
        private System.Windows.Forms.Label lblMetricTitle4;
        private System.Windows.Forms.Panel panelSearchFilters;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cmbTypes;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Button btnAddMember;
        private System.Windows.Forms.Panel panelBottomSection;
        private System.Windows.Forms.DataGridView dta_Members;
        private System.Windows.Forms.Panel panelSearchBox;
        private System.Windows.Forms.PictureBox picSearchIcon;
    }
}
