namespace Project5LMS.Forms.Admin.UserManagement
{
    partial class UserManagementForm
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
            this.panelUsersContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.panelSearchFilter = new System.Windows.Forms.Panel();
            this.cmbRoleFilter = new System.Windows.Forms.ComboBox();
            this.lblRoleFilter = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.panelMetrics = new System.Windows.Forms.Panel();
            this.panelMetricSuspended = new System.Windows.Forms.Panel();
            this.lblMetricSuspendedValue = new System.Windows.Forms.Label();
            this.lblMetricSuspendedTitle = new System.Windows.Forms.Label();
            this.panelMetricAdministrators = new System.Windows.Forms.Panel();
            this.lblMetricAdministratorsValue = new System.Windows.Forms.Label();
            this.lblMetricAdministratorsTitle = new System.Windows.Forms.Label();
            this.panelMetricActiveUsers = new System.Windows.Forms.Panel();
            this.lblMetricActiveUsersValue = new System.Windows.Forms.Label();
            this.lblMetricActiveUsersTitle = new System.Windows.Forms.Label();
            this.panelMetricTotalUsers = new System.Windows.Forms.Panel();
            this.lblMetricTotalUsersValue = new System.Windows.Forms.Label();
            this.lblMetricTotalUsersTitle = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.btnAddNewUser = new System.Windows.Forms.Button();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelMainContainer.SuspendLayout();
            this.panelSearchFilter.SuspendLayout();
            this.panelMetrics.SuspendLayout();
            this.panelMetricSuspended.SuspendLayout();
            this.panelMetricAdministrators.SuspendLayout();
            this.panelMetricActiveUsers.SuspendLayout();
            this.panelMetricTotalUsers.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMainContainer
            // 
            this.panelMainContainer.AutoScroll = true;
            this.panelMainContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelMainContainer.Controls.Add(this.panelUsersContainer);
            this.panelMainContainer.Controls.Add(this.panelSearchFilter);
            this.panelMainContainer.Controls.Add(this.panelMetrics);
            this.panelMainContainer.Controls.Add(this.panelHeader);
            this.panelMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainContainer.Location = new System.Drawing.Point(0, 0);
            this.panelMainContainer.Name = "panelMainContainer";
            this.panelMainContainer.Padding = new System.Windows.Forms.Padding(24, 24, 24, 24);
            this.panelMainContainer.Size = new System.Drawing.Size(1455, 894);
            this.panelMainContainer.TabIndex = 0;
            // 
            // panelUsersContainer
            // 
            this.panelUsersContainer.AutoScroll = true;
            this.panelUsersContainer.BackColor = System.Drawing.Color.White;
            this.panelUsersContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelUsersContainer.Location = new System.Drawing.Point(24, 295);
            this.panelUsersContainer.Name = "panelUsersContainer";
            this.panelUsersContainer.Padding = new System.Windows.Forms.Padding(0, 16, 0, 0);
            this.panelUsersContainer.Size = new System.Drawing.Size(1407, 575);
            this.panelUsersContainer.TabIndex = 3;
            // 
            // panelSearchFilter
            // 
            this.panelSearchFilter.BackColor = System.Drawing.Color.White;
            this.panelSearchFilter.Controls.Add(this.cmbRoleFilter);
            this.panelSearchFilter.Controls.Add(this.lblRoleFilter);
            this.panelSearchFilter.Controls.Add(this.txtSearch);
            this.panelSearchFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearchFilter.Location = new System.Drawing.Point(24, 235);
            this.panelSearchFilter.Name = "panelSearchFilter";
            this.panelSearchFilter.Padding = new System.Windows.Forms.Padding(16, 16, 16, 16);
            this.panelSearchFilter.Size = new System.Drawing.Size(1407, 60);
            this.panelSearchFilter.TabIndex = 2;
            // 
            // cmbRoleFilter
            // 
            this.cmbRoleFilter.BackColor = System.Drawing.Color.White;
            this.cmbRoleFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRoleFilter.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRoleFilter.FormattingEnabled = true;
            this.cmbRoleFilter.Items.AddRange(new object[] {
            "All Roles",
            "Admin",
            "LibraryStaff",
            "Member"});
            this.cmbRoleFilter.Location = new System.Drawing.Point(900, 16);
            this.cmbRoleFilter.Name = "cmbRoleFilter";
            this.cmbRoleFilter.Size = new System.Drawing.Size(236, 25);
            this.cmbRoleFilter.TabIndex = 2;
            this.cmbRoleFilter.SelectedIndexChanged += new System.EventHandler(this.cmbRoleFilter_SelectedIndexChanged);
            // 
            // lblRoleFilter
            // 
            this.lblRoleFilter.AutoSize = true;
            this.lblRoleFilter.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoleFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblRoleFilter.Location = new System.Drawing.Point(800, 19);
            this.lblRoleFilter.Name = "lblRoleFilter";
            this.lblRoleFilter.Size = new System.Drawing.Size(63, 19);
            this.lblRoleFilter.TabIndex = 1;
            this.lblRoleFilter.Text = "All Roles:";
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.White;
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.Color.Gray;
            this.txtSearch.Location = new System.Drawing.Point(16, 16);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(389, 25);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.Text = "Search users...";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            // 
            // panelMetrics
            // 
            this.panelMetrics.BackColor = System.Drawing.Color.White;
            this.panelMetrics.Controls.Add(this.panelMetricSuspended);
            this.panelMetrics.Controls.Add(this.panelMetricAdministrators);
            this.panelMetrics.Controls.Add(this.panelMetricActiveUsers);
            this.panelMetrics.Controls.Add(this.panelMetricTotalUsers);
            this.panelMetrics.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMetrics.Location = new System.Drawing.Point(24, 115);
            this.panelMetrics.Name = "panelMetrics";
            this.panelMetrics.Size = new System.Drawing.Size(1407, 120);
            this.panelMetrics.TabIndex = 1;
            // 
            // panelMetricSuspended
            // 
            this.panelMetricSuspended.BackColor = System.Drawing.Color.White;
            this.panelMetricSuspended.Controls.Add(this.lblMetricSuspendedValue);
            this.panelMetricSuspended.Controls.Add(this.lblMetricSuspendedTitle);
            this.panelMetricSuspended.Location = new System.Drawing.Point(864, 0);
            this.panelMetricSuspended.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.panelMetricSuspended.Name = "panelMetricSuspended";
            this.panelMetricSuspended.Padding = new System.Windows.Forms.Padding(16, 16, 16, 16);
            this.panelMetricSuspended.Size = new System.Drawing.Size(200, 120);
            this.panelMetricSuspended.TabIndex = 3;
            // 
            // lblMetricSuspendedValue
            // 
            this.lblMetricSuspendedValue.AutoSize = true;
            this.lblMetricSuspendedValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricSuspendedValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricSuspendedValue.Location = new System.Drawing.Point(16, 50);
            this.lblMetricSuspendedValue.Name = "lblMetricSuspendedValue";
            this.lblMetricSuspendedValue.Size = new System.Drawing.Size(38, 45);
            this.lblMetricSuspendedValue.TabIndex = 2;
            this.lblMetricSuspendedValue.Text = "0";
            // 
            // lblMetricSuspendedTitle
            // 
            this.lblMetricSuspendedTitle.AutoSize = true;
            this.lblMetricSuspendedTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricSuspendedTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricSuspendedTitle.Location = new System.Drawing.Point(16, 16);
            this.lblMetricSuspendedTitle.Name = "lblMetricSuspendedTitle";
            this.lblMetricSuspendedTitle.Size = new System.Drawing.Size(99, 19);
            this.lblMetricSuspendedTitle.TabIndex = 1;
            this.lblMetricSuspendedTitle.Text = "⛔ Suspended";
            // 
            // panelMetricAdministrators
            // 
            this.panelMetricAdministrators.BackColor = System.Drawing.Color.White;
            this.panelMetricAdministrators.Controls.Add(this.lblMetricAdministratorsValue);
            this.panelMetricAdministrators.Controls.Add(this.lblMetricAdministratorsTitle);
            this.panelMetricAdministrators.Location = new System.Drawing.Point(576, 0);
            this.panelMetricAdministrators.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.panelMetricAdministrators.Name = "panelMetricAdministrators";
            this.panelMetricAdministrators.Padding = new System.Windows.Forms.Padding(16, 16, 16, 16);
            this.panelMetricAdministrators.Size = new System.Drawing.Size(200, 120);
            this.panelMetricAdministrators.TabIndex = 2;
            // 
            // lblMetricAdministratorsValue
            // 
            this.lblMetricAdministratorsValue.AutoSize = true;
            this.lblMetricAdministratorsValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricAdministratorsValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricAdministratorsValue.Location = new System.Drawing.Point(16, 50);
            this.lblMetricAdministratorsValue.Name = "lblMetricAdministratorsValue";
            this.lblMetricAdministratorsValue.Size = new System.Drawing.Size(38, 45);
            this.lblMetricAdministratorsValue.TabIndex = 2;
            this.lblMetricAdministratorsValue.Text = "0";
            // 
            // lblMetricAdministratorsTitle
            // 
            this.lblMetricAdministratorsTitle.AutoSize = true;
            this.lblMetricAdministratorsTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricAdministratorsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricAdministratorsTitle.Location = new System.Drawing.Point(16, 16);
            this.lblMetricAdministratorsTitle.Name = "lblMetricAdministratorsTitle";
            this.lblMetricAdministratorsTitle.Size = new System.Drawing.Size(122, 19);
            this.lblMetricAdministratorsTitle.TabIndex = 1;
            this.lblMetricAdministratorsTitle.Text = "⚙️ Administrators";
            // 
            // panelMetricActiveUsers
            // 
            this.panelMetricActiveUsers.BackColor = System.Drawing.Color.White;
            this.panelMetricActiveUsers.Controls.Add(this.lblMetricActiveUsersValue);
            this.panelMetricActiveUsers.Controls.Add(this.lblMetricActiveUsersTitle);
            this.panelMetricActiveUsers.Location = new System.Drawing.Point(288, 0);
            this.panelMetricActiveUsers.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.panelMetricActiveUsers.Name = "panelMetricActiveUsers";
            this.panelMetricActiveUsers.Padding = new System.Windows.Forms.Padding(16, 16, 16, 16);
            this.panelMetricActiveUsers.Size = new System.Drawing.Size(200, 120);
            this.panelMetricActiveUsers.TabIndex = 1;
            // 
            // lblMetricActiveUsersValue
            // 
            this.lblMetricActiveUsersValue.AutoSize = true;
            this.lblMetricActiveUsersValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricActiveUsersValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricActiveUsersValue.Location = new System.Drawing.Point(16, 50);
            this.lblMetricActiveUsersValue.Name = "lblMetricActiveUsersValue";
            this.lblMetricActiveUsersValue.Size = new System.Drawing.Size(38, 45);
            this.lblMetricActiveUsersValue.TabIndex = 2;
            this.lblMetricActiveUsersValue.Text = "0";
            // 
            // lblMetricActiveUsersTitle
            // 
            this.lblMetricActiveUsersTitle.AutoSize = true;
            this.lblMetricActiveUsersTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricActiveUsersTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricActiveUsersTitle.Location = new System.Drawing.Point(16, 16);
            this.lblMetricActiveUsersTitle.Name = "lblMetricActiveUsersTitle";
            this.lblMetricActiveUsersTitle.Size = new System.Drawing.Size(107, 19);
            this.lblMetricActiveUsersTitle.TabIndex = 1;
            this.lblMetricActiveUsersTitle.Text = "✅ Active Users";
            // 
            // panelMetricTotalUsers
            // 
            this.panelMetricTotalUsers.BackColor = System.Drawing.Color.White;
            this.panelMetricTotalUsers.Controls.Add(this.lblMetricTotalUsersValue);
            this.panelMetricTotalUsers.Controls.Add(this.lblMetricTotalUsersTitle);
            this.panelMetricTotalUsers.Location = new System.Drawing.Point(0, 0);
            this.panelMetricTotalUsers.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.panelMetricTotalUsers.Name = "panelMetricTotalUsers";
            this.panelMetricTotalUsers.Padding = new System.Windows.Forms.Padding(16, 16, 16, 16);
            this.panelMetricTotalUsers.Size = new System.Drawing.Size(200, 120);
            this.panelMetricTotalUsers.TabIndex = 0;
            // 
            // lblMetricTotalUsersValue
            // 
            this.lblMetricTotalUsersValue.AutoSize = true;
            this.lblMetricTotalUsersValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTotalUsersValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetricTotalUsersValue.Location = new System.Drawing.Point(16, 50);
            this.lblMetricTotalUsersValue.Name = "lblMetricTotalUsersValue";
            this.lblMetricTotalUsersValue.Size = new System.Drawing.Size(38, 45);
            this.lblMetricTotalUsersValue.TabIndex = 2;
            this.lblMetricTotalUsersValue.Text = "0";
            // 
            // lblMetricTotalUsersTitle
            // 
            this.lblMetricTotalUsersTitle.AutoSize = true;
            this.lblMetricTotalUsersTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricTotalUsersTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMetricTotalUsersTitle.Location = new System.Drawing.Point(16, 16);
            this.lblMetricTotalUsersTitle.Name = "lblMetricTotalUsersTitle";
            this.lblMetricTotalUsersTitle.Size = new System.Drawing.Size(99, 19);
            this.lblMetricTotalUsersTitle.TabIndex = 1;
            this.lblMetricTotalUsersTitle.Text = "👥 Total Users";
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.btnAddNewUser);
            this.panelHeader.Controls.Add(this.lblSubtitle);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(24, 24);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1407, 91);
            this.panelHeader.TabIndex = 0;
            // 
            // btnAddNewUser
            // 
            this.btnAddNewUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNewUser.BackColor = System.Drawing.Color.Maroon;
            this.btnAddNewUser.FlatAppearance.BorderSize = 0;
            this.btnAddNewUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNewUser.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddNewUser.ForeColor = System.Drawing.Color.White;
            this.btnAddNewUser.Location = new System.Drawing.Point(1184, 17);
            this.btnAddNewUser.Name = "btnAddNewUser";
            this.btnAddNewUser.Size = new System.Drawing.Size(200, 40);
            this.btnAddNewUser.TabIndex = 2;
            this.btnAddNewUser.Text = "+ Add New User";
            this.btnAddNewUser.UseVisualStyleBackColor = false;
            this.btnAddNewUser.Click += new System.EventHandler(this.btnAddNewUser_Click);
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblSubtitle.Location = new System.Drawing.Point(4, 58);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(287, 19);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Manage system users and access permissions";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitle.Location = new System.Drawing.Point(3, 7);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(294, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "User Management";
            // 
            // UserManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1455, 894);
            this.Controls.Add(this.panelMainContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UserManagementForm";
            this.Text = "User Management";
            this.Load += new System.EventHandler(this.UserManagementForm_Load);
            this.panelMainContainer.ResumeLayout(false);
            this.panelSearchFilter.ResumeLayout(false);
            this.panelSearchFilter.PerformLayout();
            this.panelMetrics.ResumeLayout(false);
            this.panelMetricSuspended.ResumeLayout(false);
            this.panelMetricSuspended.PerformLayout();
            this.panelMetricAdministrators.ResumeLayout(false);
            this.panelMetricAdministrators.PerformLayout();
            this.panelMetricActiveUsers.ResumeLayout(false);
            this.panelMetricActiveUsers.PerformLayout();
            this.panelMetricTotalUsers.ResumeLayout(false);
            this.panelMetricTotalUsers.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMainContainer;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Button btnAddNewUser;
        private System.Windows.Forms.Panel panelMetrics;
        private System.Windows.Forms.Panel panelMetricTotalUsers;
        private System.Windows.Forms.Label lblMetricTotalUsersValue;
        private System.Windows.Forms.Label lblMetricTotalUsersTitle;
        private System.Windows.Forms.Panel panelMetricActiveUsers;
        private System.Windows.Forms.Label lblMetricActiveUsersValue;
        private System.Windows.Forms.Label lblMetricActiveUsersTitle;
        private System.Windows.Forms.Panel panelMetricAdministrators;
        private System.Windows.Forms.Label lblMetricAdministratorsValue;
        private System.Windows.Forms.Label lblMetricAdministratorsTitle;
        private System.Windows.Forms.Panel panelMetricSuspended;
        private System.Windows.Forms.Label lblMetricSuspendedValue;
        private System.Windows.Forms.Label lblMetricSuspendedTitle;
        private System.Windows.Forms.Panel panelSearchFilter;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblRoleFilter;
        private System.Windows.Forms.ComboBox cmbRoleFilter;
        private System.Windows.Forms.FlowLayoutPanel panelUsersContainer;
    }
}
