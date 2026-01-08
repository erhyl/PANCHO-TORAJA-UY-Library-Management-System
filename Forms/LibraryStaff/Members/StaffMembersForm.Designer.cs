namespace Project5LMS.Forms.LibraryStaff.Members
{
    partial class StaffMembersForm
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
            this.panelTableContainer = new System.Windows.Forms.Panel();
            this.dataGridViewMembers = new System.Windows.Forms.DataGridView();
            this.panelSearchFilter = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.btnAddNewMember = new System.Windows.Forms.Button();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelMainContainer.SuspendLayout();
            this.panelTableContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMembers)).BeginInit();
            this.panelSearchFilter.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMainContainer
            // 
            this.panelMainContainer.AutoScroll = true;
            this.panelMainContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelMainContainer.Controls.Add(this.panelTableContainer);
            this.panelMainContainer.Controls.Add(this.panelSearchFilter);
            this.panelMainContainer.Controls.Add(this.panelHeader);
            this.panelMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainContainer.Location = new System.Drawing.Point(0, 0);
            this.panelMainContainer.Name = "panelMainContainer";
            this.panelMainContainer.Padding = new System.Windows.Forms.Padding(24, 24, 24, 24);
            this.panelMainContainer.Size = new System.Drawing.Size(1200, 800);
            this.panelMainContainer.TabIndex = 0;
            // 
            // panelTableContainer
            // 
            this.panelTableContainer.BackColor = System.Drawing.Color.White;
            this.panelTableContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTableContainer.Controls.Add(this.dataGridViewMembers);
            this.panelTableContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTableContainer.Location = new System.Drawing.Point(24, 73);
            this.panelTableContainer.Name = "panelTableContainer";
            this.panelTableContainer.Padding = new System.Windows.Forms.Padding(15, 16, 15, 16);
            this.panelTableContainer.Size = new System.Drawing.Size(1152, 703);
            this.panelTableContainer.TabIndex = 2;
            // 
            // dataGridViewMembers
            // 
            this.dataGridViewMembers.AllowUserToAddRows = false;
            this.dataGridViewMembers.AllowUserToDeleteRows = false;
            this.dataGridViewMembers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewMembers.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewMembers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewMembers.ColumnHeadersHeight = 45;
            this.dataGridViewMembers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridViewMembers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewMembers.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dataGridViewMembers.Location = new System.Drawing.Point(15, 16);
            this.dataGridViewMembers.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridViewMembers.MultiSelect = false;
            this.dataGridViewMembers.Name = "dataGridViewMembers";
            this.dataGridViewMembers.ReadOnly = true;
            this.dataGridViewMembers.RowHeadersVisible = false;
            this.dataGridViewMembers.RowHeadersWidth = 51;
            this.dataGridViewMembers.RowTemplate.Height = 60;
            this.dataGridViewMembers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewMembers.Size = new System.Drawing.Size(1120, 669);
            this.dataGridViewMembers.TabIndex = 0;
            this.dataGridViewMembers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMembers_CellContentClick);
            this.dataGridViewMembers.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewMembers_CellFormatting);
            this.dataGridViewMembers.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridViewMembers_CellPainting);
            // 
            // panelSearchFilter
            // 
            this.panelSearchFilter.BackColor = System.Drawing.Color.White;
            this.panelSearchFilter.Controls.Add(this.txtSearch);
            this.panelSearchFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearchFilter.Location = new System.Drawing.Point(24, 24);
            this.panelSearchFilter.Name = "panelSearchFilter";
            this.panelSearchFilter.Padding = new System.Windows.Forms.Padding(15, 16, 15, 16);
            this.panelSearchFilter.Size = new System.Drawing.Size(1152, 49);
            this.panelSearchFilter.TabIndex = 1;
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.txtSearch.Location = new System.Drawing.Point(15, 16);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(1122, 27);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.Text = "Search by name, ID, or email...";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            // 
            // panelHeader
            // 
            this.panelHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelHeader.Controls.Add(this.btnAddNewMember);
            this.panelHeader.Controls.Add(this.lblSubtitle);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Location = new System.Drawing.Point(24, 24);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1152, 73);
            this.panelHeader.TabIndex = 0;
            // 
            // btnAddNewMember
            // 
            this.btnAddNewMember.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNewMember.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnAddNewMember.FlatAppearance.BorderSize = 0;
            this.btnAddNewMember.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNewMember.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddNewMember.ForeColor = System.Drawing.Color.White;
            this.btnAddNewMember.Location = new System.Drawing.Point(1020, 16);
            this.btnAddNewMember.Name = "btnAddNewMember";
            this.btnAddNewMember.Size = new System.Drawing.Size(132, 41);
            this.btnAddNewMember.TabIndex = 2;
            this.btnAddNewMember.Text = "+ Add New Member";
            this.btnAddNewMember.UseVisualStyleBackColor = false;
            this.btnAddNewMember.Click += new System.EventHandler(this.btnAddNewMember_Click);
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblSubtitle.Location = new System.Drawing.Point(0, 41);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(296, 19);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Manage library members and their information";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(161, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Members";
            // 
            // StaffMembersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.panelMainContainer);
            this.Name = "StaffMembersForm";
            this.Text = "Members Management";
            this.Load += new System.EventHandler(this.StaffMembersForm_Load);
            this.panelMainContainer.ResumeLayout(false);
            this.panelTableContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMembers)).EndInit();
            this.panelSearchFilter.ResumeLayout(false);
            this.panelSearchFilter.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMainContainer;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Button btnAddNewMember;
        private System.Windows.Forms.Panel panelSearchFilter;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Panel panelTableContainer;
        private System.Windows.Forms.DataGridView dataGridViewMembers;
    }
}
