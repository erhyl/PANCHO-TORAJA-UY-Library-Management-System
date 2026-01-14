namespace Project5LMS.Forms.Admin.UserManagement
{
    partial class AddUserForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelMainContainer;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelFormContent;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblConfirmPassword;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.ComboBox cmbRole;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panelPermissions;
        private System.Windows.Forms.Label lblPermissions;
        private System.Windows.Forms.Panel panelPermissionsList;
        private System.Windows.Forms.CheckBox chkSystemConfiguration;
        private System.Windows.Forms.CheckBox chkUserManagement;
        private System.Windows.Forms.CheckBox chkMemberManagement;
        private System.Windows.Forms.CheckBox chkCatalogManagement;
        private System.Windows.Forms.CheckBox chkCirculation;
        private System.Windows.Forms.CheckBox chkReservations;
        private System.Windows.Forms.CheckBox chkFineManagement;
        private System.Windows.Forms.CheckBox chkInventory;
        private System.Windows.Forms.CheckBox chkReports;
        private System.Windows.Forms.CheckBox chkSearch;
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.panelFormContent = new System.Windows.Forms.Panel();
            this.panelPermissions = new System.Windows.Forms.Panel();
            this.panelPermissionsList = new System.Windows.Forms.Panel();
            this.chkSearch = new System.Windows.Forms.CheckBox();
            this.chkReports = new System.Windows.Forms.CheckBox();
            this.chkInventory = new System.Windows.Forms.CheckBox();
            this.chkFineManagement = new System.Windows.Forms.CheckBox();
            this.chkReservations = new System.Windows.Forms.CheckBox();
            this.chkCirculation = new System.Windows.Forms.CheckBox();
            this.chkCatalogManagement = new System.Windows.Forms.CheckBox();
            this.chkMemberManagement = new System.Windows.Forms.CheckBox();
            this.chkUserManagement = new System.Windows.Forms.CheckBox();
            this.chkSystemConfiguration = new System.Windows.Forms.CheckBox();
            this.lblPermissions = new System.Windows.Forms.Label();
            this.cmbRole = new System.Windows.Forms.ComboBox();
            this.lblRole = new System.Windows.Forms.Label();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.lblConfirmPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.lblLastName = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelMainContainer.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.panelFormContent.SuspendLayout();
            this.panelPermissions.SuspendLayout();
            this.panelPermissionsList.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMainContainer
            // 
            this.panelMainContainer.BackColor = System.Drawing.Color.White;
            this.panelMainContainer.Controls.Add(this.panelButtons);
            this.panelMainContainer.Controls.Add(this.panelFormContent);
            this.panelMainContainer.Controls.Add(this.lblTitle);
            this.panelMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainContainer.ForeColor = System.Drawing.Color.White;
            this.panelMainContainer.Location = new System.Drawing.Point(0, 0);
            this.panelMainContainer.Margin = new System.Windows.Forms.Padding(4);
            this.panelMainContainer.Name = "panelMainContainer";
            this.panelMainContainer.Padding = new System.Windows.Forms.Padding(40, 30, 40, 30);
            this.panelMainContainer.Size = new System.Drawing.Size(500, 750);
            this.panelMainContainer.TabIndex = 0;
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnCancel);
            this.panelButtons.Controls.Add(this.btnSave);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(40, 670);
            this.panelButtons.Margin = new System.Windows.Forms.Padding(4);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(420, 50);
            this.panelButtons.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(-1, 7);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 39);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(300, 7);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 39);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panelFormContent
            // 
            this.panelFormContent.Controls.Add(this.panelPermissions);
            this.panelFormContent.Controls.Add(this.cmbRole);
            this.panelFormContent.Controls.Add(this.lblRole);
            this.panelFormContent.Controls.Add(this.txtConfirmPassword);
            this.panelFormContent.Controls.Add(this.lblConfirmPassword);
            this.panelFormContent.Controls.Add(this.txtPassword);
            this.panelFormContent.Controls.Add(this.lblPassword);
            this.panelFormContent.Controls.Add(this.txtEmail);
            this.panelFormContent.Controls.Add(this.lblEmail);
            this.panelFormContent.Controls.Add(this.txtLastName);
            this.panelFormContent.Controls.Add(this.lblLastName);
            this.panelFormContent.Controls.Add(this.txtFirstName);
            this.panelFormContent.Controls.Add(this.lblFirstName);
            this.panelFormContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFormContent.Location = new System.Drawing.Point(40, 30);
            this.panelFormContent.Margin = new System.Windows.Forms.Padding(4);
            this.panelFormContent.Name = "panelFormContent";
            this.panelFormContent.Size = new System.Drawing.Size(420, 690);
            this.panelFormContent.TabIndex = 1;
            // 
            // panelPermissions
            // 
            this.panelPermissions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelPermissions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPermissions.Controls.Add(this.panelPermissionsList);
            this.panelPermissions.Controls.Add(this.lblPermissions);
            this.panelPermissions.Location = new System.Drawing.Point(0, 410);
            this.panelPermissions.Margin = new System.Windows.Forms.Padding(4);
            this.panelPermissions.Name = "panelPermissions";
            this.panelPermissions.Size = new System.Drawing.Size(420, 280);
            this.panelPermissions.TabIndex = 12;
            // 
            // panelPermissionsList
            // 
            this.panelPermissionsList.AutoScroll = true;
            this.panelPermissionsList.Controls.Add(this.chkUserManagement);
            this.panelPermissionsList.Controls.Add(this.chkMemberManagement);
            this.panelPermissionsList.Controls.Add(this.chkCatalogManagement);
            this.panelPermissionsList.Controls.Add(this.chkCirculation);
            this.panelPermissionsList.Controls.Add(this.chkReservations);
            this.panelPermissionsList.Controls.Add(this.chkFineManagement);
            this.panelPermissionsList.Controls.Add(this.chkInventory);
            this.panelPermissionsList.Controls.Add(this.chkReports);
            this.panelPermissionsList.Controls.Add(this.chkSearch);
            this.panelPermissionsList.Controls.Add(this.chkSystemConfiguration);
            this.panelPermissionsList.Location = new System.Drawing.Point(0, 35);
            this.panelPermissionsList.Name = "panelPermissionsList";
            this.panelPermissionsList.Padding = new System.Windows.Forms.Padding(10, 5, 10, 10);
            this.panelPermissionsList.Size = new System.Drawing.Size(419, 245);
            this.panelPermissionsList.TabIndex = 1;
            // Order: User Management, Member Management, Catalog Management, Circulation, Reservations, Fine Management, Inventory, Reports, Search, System Configuration
            // 
            // chkUserManagement (1)
            // 
            this.chkUserManagement.AutoSize = true;
            this.chkUserManagement.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkUserManagement.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkUserManagement.Location = new System.Drawing.Point(13, 5);
            this.chkUserManagement.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.chkUserManagement.Name = "chkUserManagement";
            this.chkUserManagement.Size = new System.Drawing.Size(160, 24);
            this.chkUserManagement.TabIndex = 1;
            this.chkUserManagement.Text = "User Management";
            this.chkUserManagement.UseVisualStyleBackColor = true;
            // 
            // chkMemberManagement (2)
            // 
            this.chkMemberManagement.AutoSize = true;
            this.chkMemberManagement.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMemberManagement.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkMemberManagement.Location = new System.Drawing.Point(13, 32);
            this.chkMemberManagement.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.chkMemberManagement.Name = "chkMemberManagement";
            this.chkMemberManagement.Size = new System.Drawing.Size(195, 24);
            this.chkMemberManagement.TabIndex = 2;
            this.chkMemberManagement.Text = "Member Management";
            this.chkMemberManagement.UseVisualStyleBackColor = true;
            // 
            // chkCatalogManagement (3)
            // 
            this.chkCatalogManagement.AutoSize = true;
            this.chkCatalogManagement.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCatalogManagement.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkCatalogManagement.Location = new System.Drawing.Point(13, 59);
            this.chkCatalogManagement.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.chkCatalogManagement.Name = "chkCatalogManagement";
            this.chkCatalogManagement.Size = new System.Drawing.Size(191, 24);
            this.chkCatalogManagement.TabIndex = 3;
            this.chkCatalogManagement.Text = "Catalog Management";
            this.chkCatalogManagement.UseVisualStyleBackColor = true;
            // 
            // chkCirculation (4)
            // 
            this.chkCirculation.AutoSize = true;
            this.chkCirculation.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCirculation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkCirculation.Location = new System.Drawing.Point(13, 86);
            this.chkCirculation.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.chkCirculation.Name = "chkCirculation";
            this.chkCirculation.Size = new System.Drawing.Size(108, 24);
            this.chkCirculation.TabIndex = 4;
            this.chkCirculation.Text = "Circulation";
            this.chkCirculation.UseVisualStyleBackColor = true;
            // 
            // chkReservations (5)
            // 
            this.chkReservations.AutoSize = true;
            this.chkReservations.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkReservations.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkReservations.Location = new System.Drawing.Point(13, 113);
            this.chkReservations.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.chkReservations.Name = "chkReservations";
            this.chkReservations.Size = new System.Drawing.Size(125, 24);
            this.chkReservations.TabIndex = 5;
            this.chkReservations.Text = "Reservations";
            this.chkReservations.UseVisualStyleBackColor = true;
            // 
            // chkFineManagement (6)
            // 
            this.chkFineManagement.AutoSize = true;
            this.chkFineManagement.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkFineManagement.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkFineManagement.Location = new System.Drawing.Point(13, 140);
            this.chkFineManagement.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.chkFineManagement.Name = "chkFineManagement";
            this.chkFineManagement.Size = new System.Drawing.Size(160, 24);
            this.chkFineManagement.TabIndex = 6;
            this.chkFineManagement.Text = "Fine Management";
            this.chkFineManagement.UseVisualStyleBackColor = true;
            // 
            // chkInventory (7)
            // 
            this.chkInventory.AutoSize = true;
            this.chkInventory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkInventory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkInventory.Location = new System.Drawing.Point(13, 167);
            this.chkInventory.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.chkInventory.Name = "chkInventory";
            this.chkInventory.Size = new System.Drawing.Size(98, 24);
            this.chkInventory.TabIndex = 7;
            this.chkInventory.Text = "Inventory";
            this.chkInventory.UseVisualStyleBackColor = true;
            // 
            // chkReports (8)
            // 
            this.chkReports.AutoSize = true;
            this.chkReports.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkReports.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkReports.Location = new System.Drawing.Point(13, 194);
            this.chkReports.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.chkReports.Name = "chkReports";
            this.chkReports.Size = new System.Drawing.Size(90, 24);
            this.chkReports.TabIndex = 8;
            this.chkReports.Text = "Reports";
            this.chkReports.UseVisualStyleBackColor = true;
            // 
            // chkSearch (9)
            // 
            this.chkSearch.AutoSize = true;
            this.chkSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkSearch.Location = new System.Drawing.Point(13, 221);
            this.chkSearch.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.chkSearch.Name = "chkSearch";
            this.chkSearch.Size = new System.Drawing.Size(84, 24);
            this.chkSearch.TabIndex = 9;
            this.chkSearch.Text = "Search";
            this.chkSearch.UseVisualStyleBackColor = true;
            // 
            // chkSystemConfiguration (10)
            // 
            this.chkSystemConfiguration.AutoSize = true;
            this.chkSystemConfiguration.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSystemConfiguration.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkSystemConfiguration.Location = new System.Drawing.Point(13, 248);
            this.chkSystemConfiguration.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.chkSystemConfiguration.Name = "chkSystemConfiguration";
            this.chkSystemConfiguration.Size = new System.Drawing.Size(198, 24);
            this.chkSystemConfiguration.TabIndex = 0;
            this.chkSystemConfiguration.Text = "System Configuration";
            this.chkSystemConfiguration.UseVisualStyleBackColor = true;
            // 
            // lblPermissions
            // 
            this.lblPermissions.AutoSize = true;
            this.lblPermissions.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPermissions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPermissions.Location = new System.Drawing.Point(4, 8);
            this.lblPermissions.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPermissions.Name = "lblPermissions";
            this.lblPermissions.Size = new System.Drawing.Size(123, 24);
            this.lblPermissions.TabIndex = 0;
            this.lblPermissions.Text = "Permissions";
            // 
            // cmbRole
            // 
            this.cmbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRole.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRole.FormattingEnabled = true;
            this.cmbRole.Items.AddRange(new object[] {
            "Admin",
            "LibraryStaff"});
            this.cmbRole.Location = new System.Drawing.Point(0, 368);
            this.cmbRole.Margin = new System.Windows.Forms.Padding(4);
            this.cmbRole.Name = "cmbRole";
            this.cmbRole.Size = new System.Drawing.Size(420, 30);
            this.cmbRole.TabIndex = 5;
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblRole.Location = new System.Drawing.Point(4, 340);
            this.lblRole.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(49, 24);
            this.lblRole.TabIndex = 11;
            this.lblRole.Text = "Role";
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtConfirmPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConfirmPassword.Location = new System.Drawing.Point(0, 303);
            this.txtConfirmPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.PasswordChar = '*';
            this.txtConfirmPassword.Size = new System.Drawing.Size(416, 28);
            this.txtConfirmPassword.TabIndex = 4;
            // 
            // lblConfirmPassword
            // 
            this.lblConfirmPassword.AutoSize = true;
            this.lblConfirmPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfirmPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblConfirmPassword.Location = new System.Drawing.Point(4, 275);
            this.lblConfirmPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblConfirmPassword.Name = "lblConfirmPassword";
            this.lblConfirmPassword.Size = new System.Drawing.Size(162, 24);
            this.lblConfirmPassword.TabIndex = 9;
            this.lblConfirmPassword.Text = "Confirm Password";
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(0, 234);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(416, 28);
            this.txtPassword.TabIndex = 3;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPassword.Location = new System.Drawing.Point(4, 206);
            this.lblPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(92, 24);
            this.lblPassword.TabIndex = 7;
            this.lblPassword.Text = "Password";
            // 
            // txtEmail
            // 
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(0, 165);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(4);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(416, 28);
            this.txtEmail.TabIndex = 2;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblEmail.Location = new System.Drawing.Point(4, 137);
            this.lblEmail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(57, 24);
            this.lblEmail.TabIndex = 5;
            this.lblEmail.Text = "Email";
            // 
            // txtLastName
            // 
            this.txtLastName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLastName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLastName.Location = new System.Drawing.Point(0, 96);
            this.txtLastName.Margin = new System.Windows.Forms.Padding(4);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(416, 28);
            this.txtLastName.TabIndex = 1;
            // 
            // lblLastName
            // 
            this.lblLastName.AutoSize = true;
            this.lblLastName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblLastName.Location = new System.Drawing.Point(4, 68);
            this.lblLastName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(99, 24);
            this.lblLastName.TabIndex = 3;
            this.lblLastName.Text = "Last Name";
            // 
            // txtFirstName
            // 
            this.txtFirstName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFirstName.Location = new System.Drawing.Point(0, 28);
            this.txtFirstName.Margin = new System.Windows.Forms.Padding(4);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(416, 28);
            this.txtFirstName.TabIndex = 0;
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFirstName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblFirstName.Location = new System.Drawing.Point(4, 0);
            this.lblFirstName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(101, 24);
            this.lblFirstName.TabIndex = 1;
            this.lblFirstName.Text = "First Name";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitle.Location = new System.Drawing.Point(44, 30);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(135, 31);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Add User";
            // 
            // AddUserForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 750);
            this.Controls.Add(this.panelMainContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddUserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add User";
            this.Load += new System.EventHandler(this.AddUserForm_Load);
            this.panelMainContainer.ResumeLayout(false);
            this.panelMainContainer.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.panelFormContent.ResumeLayout(false);
            this.panelFormContent.PerformLayout();
            this.panelPermissions.ResumeLayout(false);
            this.panelPermissions.PerformLayout();
            this.panelPermissionsList.ResumeLayout(false);
            this.panelPermissionsList.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion
    }
}