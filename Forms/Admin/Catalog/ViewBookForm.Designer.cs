namespace Project5LMS.Forms.Admin.Catalog
{
    partial class ViewBookForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelMainContainer;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Panel panelFormContent;
        private System.Windows.Forms.Panel panelBookCover;
        private System.Windows.Forms.PictureBox picBookCover;
        private System.Windows.Forms.Label lblNoCoverImage;
        private System.Windows.Forms.Panel panelBasicInfo;
        private System.Windows.Forms.Panel panelAdditionalInfo;
        private System.Windows.Forms.Panel panelInventoryInfo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.Label lblISBN;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblPublisher;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.Label lblAccessionNo;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Label lblTotalCopies;
        private System.Windows.Forms.Label lblAvailable;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.TextBox txtISBN;
        private System.Windows.Forms.TextBox txtCategory;
        private System.Windows.Forms.TextBox txtPublisher;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.TextBox txtAccessionNo;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.TextBox txtTotalCopies;
        private System.Windows.Forms.TextBox txtAvailable;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.TextBox txtSubtitle;
        private System.Windows.Forms.Label lblEditor;
        private System.Windows.Forms.TextBox txtEditor;
        private System.Windows.Forms.Label lblEdition;
        private System.Windows.Forms.TextBox txtEdition;
        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.TextBox txtLanguage;
        private System.Windows.Forms.Label lblPages;
        private System.Windows.Forms.TextBox txtPages;
        private System.Windows.Forms.Label lblCallNumber;
        private System.Windows.Forms.TextBox txtCallNumber;
        private System.Windows.Forms.Label lblBarcode;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Label lblPhysicalDescription;
        private System.Windows.Forms.TextBox txtPhysicalDescription;
        private System.Windows.Forms.Panel panelButtons;
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
            this.panelFormContent = new System.Windows.Forms.Panel();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelInventoryInfo = new System.Windows.Forms.Panel();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtAvailable = new System.Windows.Forms.TextBox();
            this.lblAvailable = new System.Windows.Forms.Label();
            this.txtTotalCopies = new System.Windows.Forms.TextBox();
            this.lblTotalCopies = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.txtAccessionNo = new System.Windows.Forms.TextBox();
            this.lblAccessionNo = new System.Windows.Forms.Label();
            this.panelAdditionalInfo = new System.Windows.Forms.Panel();
            this.txtPhysicalDescription = new System.Windows.Forms.TextBox();
            this.lblPhysicalDescription = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.lblBarcode = new System.Windows.Forms.Label();
            this.txtCallNumber = new System.Windows.Forms.TextBox();
            this.lblCallNumber = new System.Windows.Forms.Label();
            this.txtPages = new System.Windows.Forms.TextBox();
            this.lblPages = new System.Windows.Forms.Label();
            this.txtLanguage = new System.Windows.Forms.TextBox();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.txtEdition = new System.Windows.Forms.TextBox();
            this.lblEdition = new System.Windows.Forms.Label();
            this.txtEditor = new System.Windows.Forms.TextBox();
            this.lblEditor = new System.Windows.Forms.Label();
            this.txtSubtitle = new System.Windows.Forms.TextBox();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.panelBasicInfo = new System.Windows.Forms.Panel();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.lblYear = new System.Windows.Forms.Label();
            this.txtPublisher = new System.Windows.Forms.TextBox();
            this.lblPublisher = new System.Windows.Forms.Label();
            this.txtCategory = new System.Windows.Forms.TextBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.txtISBN = new System.Windows.Forms.TextBox();
            this.lblISBN = new System.Windows.Forms.Label();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelBookCover = new System.Windows.Forms.Panel();
            this.picBookCover = new System.Windows.Forms.PictureBox();
            this.lblNoCoverImage = new System.Windows.Forms.Label();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.panelMainContainer.SuspendLayout();
            this.panelFormContent.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.panelInventoryInfo.SuspendLayout();
            this.panelAdditionalInfo.SuspendLayout();
            this.panelBasicInfo.SuspendLayout();
            this.panelBookCover.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBookCover)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMainContainer
            // 
            this.panelMainContainer.BackColor = System.Drawing.Color.White;
            this.panelMainContainer.Controls.Add(this.panelFormContent);
            this.panelMainContainer.Controls.Add(this.lblFormTitle);
            this.panelMainContainer.Location = new System.Drawing.Point(0, 0);
            this.panelMainContainer.Margin = new System.Windows.Forms.Padding(4);
            this.panelMainContainer.Name = "panelMainContainer";
            this.panelMainContainer.Size = new System.Drawing.Size(1000, 800);
            this.panelMainContainer.TabIndex = 0;
            // 
            // panelFormContent
            // 
            this.panelFormContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelFormContent.AutoScroll = true;
            this.panelFormContent.Controls.Add(this.panelButtons);
            this.panelFormContent.Controls.Add(this.panelInventoryInfo);
            this.panelFormContent.Controls.Add(this.panelAdditionalInfo);
            this.panelFormContent.Controls.Add(this.panelBasicInfo);
            this.panelFormContent.Controls.Add(this.panelBookCover);
            this.panelFormContent.Location = new System.Drawing.Point(0, 80);
            this.panelFormContent.Margin = new System.Windows.Forms.Padding(4);
            this.panelFormContent.Name = "panelFormContent";
            this.panelFormContent.Padding = new System.Windows.Forms.Padding(29, 20, 29, 20);
            this.panelFormContent.Size = new System.Drawing.Size(1000, 720);
            this.panelFormContent.TabIndex = 2;
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnClose);
            this.panelButtons.Location = new System.Drawing.Point(29, 844);
            this.panelButtons.Margin = new System.Windows.Forms.Padding(4);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(942, 50);
            this.panelButtons.TabIndex = 4;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(4, 4);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(934, 39);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelInventoryInfo
            // 
            this.panelInventoryInfo.Controls.Add(this.txtStatus);
            this.panelInventoryInfo.Controls.Add(this.lblStatus);
            this.panelInventoryInfo.Controls.Add(this.txtAvailable);
            this.panelInventoryInfo.Controls.Add(this.lblAvailable);
            this.panelInventoryInfo.Controls.Add(this.txtTotalCopies);
            this.panelInventoryInfo.Controls.Add(this.lblTotalCopies);
            this.panelInventoryInfo.Controls.Add(this.txtLocation);
            this.panelInventoryInfo.Controls.Add(this.lblLocation);
            this.panelInventoryInfo.Controls.Add(this.txtAccessionNo);
            this.panelInventoryInfo.Controls.Add(this.lblAccessionNo);
            this.panelInventoryInfo.Location = new System.Drawing.Point(29, 636);
            this.panelInventoryInfo.Margin = new System.Windows.Forms.Padding(4);
            this.panelInventoryInfo.Name = "panelInventoryInfo";
            this.panelInventoryInfo.Size = new System.Drawing.Size(942, 200);
            this.panelInventoryInfo.TabIndex = 3;
            // 
            // txtStatus
            // 
            this.txtStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStatus.Location = new System.Drawing.Point(0, 95);
            this.txtStatus.Margin = new System.Windows.Forms.Padding(4);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(230, 28);
            this.txtStatus.TabIndex = 9;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblStatus.Location = new System.Drawing.Point(0, 67);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(60, 24);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.Text = "Status";
            // 
            // txtAvailable
            // 
            this.txtAvailable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAvailable.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAvailable.Location = new System.Drawing.Point(468, 95);
            this.txtAvailable.Margin = new System.Windows.Forms.Padding(4);
            this.txtAvailable.Name = "txtAvailable";
            this.txtAvailable.ReadOnly = true;
            this.txtAvailable.Size = new System.Drawing.Size(228, 28);
            this.txtAvailable.TabIndex = 7;
            // 
            // lblAvailable
            // 
            this.lblAvailable.AutoSize = true;
            this.lblAvailable.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvailable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblAvailable.Location = new System.Drawing.Point(468, 67);
            this.lblAvailable.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAvailable.Name = "lblAvailable";
            this.lblAvailable.Size = new System.Drawing.Size(86, 24);
            this.lblAvailable.TabIndex = 6;
            this.lblAvailable.Text = "Available";
            // 
            // txtTotalCopies
            // 
            this.txtTotalCopies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalCopies.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalCopies.Location = new System.Drawing.Point(468, 28);
            this.txtTotalCopies.Margin = new System.Windows.Forms.Padding(4);
            this.txtTotalCopies.Name = "txtTotalCopies";
            this.txtTotalCopies.ReadOnly = true;
            this.txtTotalCopies.Size = new System.Drawing.Size(230, 28);
            this.txtTotalCopies.TabIndex = 5;
            // 
            // lblTotalCopies
            // 
            this.lblTotalCopies.AutoSize = true;
            this.lblTotalCopies.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCopies.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTotalCopies.Location = new System.Drawing.Point(468, 0);
            this.lblTotalCopies.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalCopies.Name = "lblTotalCopies";
            this.lblTotalCopies.Size = new System.Drawing.Size(115, 24);
            this.lblTotalCopies.TabIndex = 4;
            this.lblTotalCopies.Text = "Total Copies";
            // 
            // txtLocation
            // 
            this.txtLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLocation.Location = new System.Drawing.Point(-1, 168);
            this.txtLocation.Margin = new System.Windows.Forms.Padding(4);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.ReadOnly = true;
            this.txtLocation.Size = new System.Drawing.Size(230, 28);
            this.txtLocation.TabIndex = 3;
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblLocation.Location = new System.Drawing.Point(0, 140);
            this.lblLocation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(81, 24);
            this.lblLocation.TabIndex = 2;
            this.lblLocation.Text = "Location";
            // 
            // txtAccessionNo
            // 
            this.txtAccessionNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAccessionNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAccessionNo.Location = new System.Drawing.Point(0, 28);
            this.txtAccessionNo.Margin = new System.Windows.Forms.Padding(4);
            this.txtAccessionNo.Name = "txtAccessionNo";
            this.txtAccessionNo.ReadOnly = true;
            this.txtAccessionNo.Size = new System.Drawing.Size(230, 28);
            this.txtAccessionNo.TabIndex = 1;
            // 
            // lblAccessionNo
            // 
            this.lblAccessionNo.AutoSize = true;
            this.lblAccessionNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccessionNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblAccessionNo.Location = new System.Drawing.Point(4, 0);
            this.lblAccessionNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAccessionNo.Name = "lblAccessionNo";
            this.lblAccessionNo.Size = new System.Drawing.Size(128, 24);
            this.lblAccessionNo.TabIndex = 0;
            this.lblAccessionNo.Text = "Accession No";
            // 
            // panelAdditionalInfo
            // 
            this.panelAdditionalInfo.Controls.Add(this.txtPhysicalDescription);
            this.panelAdditionalInfo.Controls.Add(this.lblPhysicalDescription);
            this.panelAdditionalInfo.Controls.Add(this.txtBarcode);
            this.panelAdditionalInfo.Controls.Add(this.lblBarcode);
            this.panelAdditionalInfo.Controls.Add(this.txtCallNumber);
            this.panelAdditionalInfo.Controls.Add(this.lblCallNumber);
            this.panelAdditionalInfo.Controls.Add(this.txtPages);
            this.panelAdditionalInfo.Controls.Add(this.lblPages);
            this.panelAdditionalInfo.Controls.Add(this.txtLanguage);
            this.panelAdditionalInfo.Controls.Add(this.lblLanguage);
            this.panelAdditionalInfo.Controls.Add(this.txtEdition);
            this.panelAdditionalInfo.Controls.Add(this.lblEdition);
            this.panelAdditionalInfo.Controls.Add(this.txtEditor);
            this.panelAdditionalInfo.Controls.Add(this.lblEditor);
            this.panelAdditionalInfo.Controls.Add(this.txtSubtitle);
            this.panelAdditionalInfo.Controls.Add(this.lblSubtitle);
            this.panelAdditionalInfo.Location = new System.Drawing.Point(29, 348);
            this.panelAdditionalInfo.Margin = new System.Windows.Forms.Padding(4);
            this.panelAdditionalInfo.Name = "panelAdditionalInfo";
            this.panelAdditionalInfo.Size = new System.Drawing.Size(942, 280);
            this.panelAdditionalInfo.TabIndex = 2;
            // 
            // txtPhysicalDescription
            // 
            this.txtPhysicalDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPhysicalDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhysicalDescription.Location = new System.Drawing.Point(0, 184);
            this.txtPhysicalDescription.Margin = new System.Windows.Forms.Padding(4);
            this.txtPhysicalDescription.Multiline = true;
            this.txtPhysicalDescription.Name = "txtPhysicalDescription";
            this.txtPhysicalDescription.ReadOnly = true;
            this.txtPhysicalDescription.Size = new System.Drawing.Size(942, 92);
            this.txtPhysicalDescription.TabIndex = 15;
            // 
            // lblPhysicalDescription
            // 
            this.lblPhysicalDescription.AutoSize = true;
            this.lblPhysicalDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhysicalDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPhysicalDescription.Location = new System.Drawing.Point(0, 162);
            this.lblPhysicalDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPhysicalDescription.Name = "lblPhysicalDescription";
            this.lblPhysicalDescription.Size = new System.Drawing.Size(178, 24);
            this.lblPhysicalDescription.TabIndex = 14;
            this.lblPhysicalDescription.Text = "Physical Description";
            // 
            // txtBarcode
            // 
            this.txtBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBarcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBarcode.Location = new System.Drawing.Point(0, 130);
            this.txtBarcode.Margin = new System.Windows.Forms.Padding(4);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.ReadOnly = true;
            this.txtBarcode.Size = new System.Drawing.Size(230, 28);
            this.txtBarcode.TabIndex = 13;
            // 
            // lblBarcode
            // 
            this.lblBarcode.AutoSize = true;
            this.lblBarcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBarcode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblBarcode.Location = new System.Drawing.Point(0, 108);
            this.lblBarcode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBarcode.Name = "lblBarcode";
            this.lblBarcode.Size = new System.Drawing.Size(81, 24);
            this.lblBarcode.TabIndex = 12;
            this.lblBarcode.Text = "Barcode";
            // 
            // txtCallNumber
            // 
            this.txtCallNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCallNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCallNumber.Location = new System.Drawing.Point(714, 76);
            this.txtCallNumber.Margin = new System.Windows.Forms.Padding(4);
            this.txtCallNumber.Name = "txtCallNumber";
            this.txtCallNumber.ReadOnly = true;
            this.txtCallNumber.Size = new System.Drawing.Size(228, 28);
            this.txtCallNumber.TabIndex = 11;
            // 
            // lblCallNumber
            // 
            this.lblCallNumber.AutoSize = true;
            this.lblCallNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCallNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCallNumber.Location = new System.Drawing.Point(714, 54);
            this.lblCallNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCallNumber.Name = "lblCallNumber";
            this.lblCallNumber.Size = new System.Drawing.Size(115, 24);
            this.lblCallNumber.TabIndex = 10;
            this.lblCallNumber.Text = "Call Number";
            // 
            // txtPages
            // 
            this.txtPages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPages.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPages.Location = new System.Drawing.Point(476, 76);
            this.txtPages.Margin = new System.Windows.Forms.Padding(4);
            this.txtPages.Name = "txtPages";
            this.txtPages.ReadOnly = true;
            this.txtPages.Size = new System.Drawing.Size(230, 28);
            this.txtPages.TabIndex = 9;
            // 
            // lblPages
            // 
            this.lblPages.AutoSize = true;
            this.lblPages.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPages.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPages.Location = new System.Drawing.Point(476, 54);
            this.lblPages.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPages.Name = "lblPages";
            this.lblPages.Size = new System.Drawing.Size(63, 24);
            this.lblPages.TabIndex = 8;
            this.lblPages.Text = "Pages";
            // 
            // txtLanguage
            // 
            this.txtLanguage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLanguage.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLanguage.Location = new System.Drawing.Point(238, 76);
            this.txtLanguage.Margin = new System.Windows.Forms.Padding(4);
            this.txtLanguage.Name = "txtLanguage";
            this.txtLanguage.ReadOnly = true;
            this.txtLanguage.Size = new System.Drawing.Size(230, 28);
            this.txtLanguage.TabIndex = 7;
            // 
            // lblLanguage
            // 
            this.lblLanguage.AutoSize = true;
            this.lblLanguage.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLanguage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblLanguage.Location = new System.Drawing.Point(238, 54);
            this.lblLanguage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(95, 24);
            this.lblLanguage.TabIndex = 6;
            this.lblLanguage.Text = "Language";
            // 
            // txtEdition
            // 
            this.txtEdition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEdition.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEdition.Location = new System.Drawing.Point(0, 76);
            this.txtEdition.Margin = new System.Windows.Forms.Padding(4);
            this.txtEdition.Name = "txtEdition";
            this.txtEdition.ReadOnly = true;
            this.txtEdition.Size = new System.Drawing.Size(230, 28);
            this.txtEdition.TabIndex = 5;
            // 
            // lblEdition
            // 
            this.lblEdition.AutoSize = true;
            this.lblEdition.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEdition.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblEdition.Location = new System.Drawing.Point(0, 54);
            this.lblEdition.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEdition.Name = "lblEdition";
            this.lblEdition.Size = new System.Drawing.Size(68, 24);
            this.lblEdition.TabIndex = 4;
            this.lblEdition.Text = "Edition";
            // 
            // txtEditor
            // 
            this.txtEditor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEditor.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEditor.Location = new System.Drawing.Point(468, 22);
            this.txtEditor.Margin = new System.Windows.Forms.Padding(4);
            this.txtEditor.Name = "txtEditor";
            this.txtEditor.ReadOnly = true;
            this.txtEditor.Size = new System.Drawing.Size(474, 28);
            this.txtEditor.TabIndex = 3;
            // 
            // lblEditor
            // 
            this.lblEditor.AutoSize = true;
            this.lblEditor.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEditor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblEditor.Location = new System.Drawing.Point(468, 0);
            this.lblEditor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEditor.Name = "lblEditor";
            this.lblEditor.Size = new System.Drawing.Size(59, 24);
            this.lblEditor.TabIndex = 2;
            this.lblEditor.Text = "Editor";
            // 
            // txtSubtitle
            // 
            this.txtSubtitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSubtitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubtitle.Location = new System.Drawing.Point(0, 22);
            this.txtSubtitle.Margin = new System.Windows.Forms.Padding(4);
            this.txtSubtitle.Name = "txtSubtitle";
            this.txtSubtitle.ReadOnly = true;
            this.txtSubtitle.Size = new System.Drawing.Size(460, 28);
            this.txtSubtitle.TabIndex = 1;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblSubtitle.Location = new System.Drawing.Point(0, 0);
            this.lblSubtitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(71, 24);
            this.lblSubtitle.TabIndex = 0;
            this.lblSubtitle.Text = "Subtitle";
            // 
            // panelBasicInfo
            // 
            this.panelBasicInfo.Controls.Add(this.txtYear);
            this.panelBasicInfo.Controls.Add(this.lblYear);
            this.panelBasicInfo.Controls.Add(this.txtPublisher);
            this.panelBasicInfo.Controls.Add(this.lblPublisher);
            this.panelBasicInfo.Controls.Add(this.txtCategory);
            this.panelBasicInfo.Controls.Add(this.lblCategory);
            this.panelBasicInfo.Controls.Add(this.txtISBN);
            this.panelBasicInfo.Controls.Add(this.lblISBN);
            this.panelBasicInfo.Controls.Add(this.txtAuthor);
            this.panelBasicInfo.Controls.Add(this.lblAuthor);
            this.panelBasicInfo.Controls.Add(this.txtTitle);
            this.panelBasicInfo.Controls.Add(this.lblTitle);
            this.panelBasicInfo.Location = new System.Drawing.Point(295, 20);
            this.panelBasicInfo.Margin = new System.Windows.Forms.Padding(4);
            this.panelBasicInfo.Name = "panelBasicInfo";
            this.panelBasicInfo.Size = new System.Drawing.Size(676, 320);
            this.panelBasicInfo.TabIndex = 1;
            // 
            // txtYear
            // 
            this.txtYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtYear.Location = new System.Drawing.Point(338, 184);
            this.txtYear.Margin = new System.Windows.Forms.Padding(4);
            this.txtYear.Name = "txtYear";
            this.txtYear.ReadOnly = true;
            this.txtYear.Size = new System.Drawing.Size(338, 28);
            this.txtYear.TabIndex = 11;
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblYear.Location = new System.Drawing.Point(338, 162);
            this.lblYear.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(49, 24);
            this.lblYear.TabIndex = 10;
            this.lblYear.Text = "Year";
            // 
            // txtPublisher
            // 
            this.txtPublisher.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPublisher.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPublisher.Location = new System.Drawing.Point(0, 184);
            this.txtPublisher.Margin = new System.Windows.Forms.Padding(4);
            this.txtPublisher.Name = "txtPublisher";
            this.txtPublisher.ReadOnly = true;
            this.txtPublisher.Size = new System.Drawing.Size(330, 28);
            this.txtPublisher.TabIndex = 9;
            // 
            // lblPublisher
            // 
            this.lblPublisher.AutoSize = true;
            this.lblPublisher.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPublisher.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPublisher.Location = new System.Drawing.Point(0, 162);
            this.lblPublisher.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPublisher.Name = "lblPublisher";
            this.lblPublisher.Size = new System.Drawing.Size(89, 24);
            this.lblPublisher.TabIndex = 8;
            this.lblPublisher.Text = "Publisher";
            // 
            // txtCategory
            // 
            this.txtCategory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCategory.Location = new System.Drawing.Point(338, 130);
            this.txtCategory.Margin = new System.Windows.Forms.Padding(4);
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.ReadOnly = true;
            this.txtCategory.Size = new System.Drawing.Size(338, 28);
            this.txtCategory.TabIndex = 7;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCategory.Location = new System.Drawing.Point(338, 108);
            this.lblCategory.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(85, 24);
            this.lblCategory.TabIndex = 6;
            this.lblCategory.Text = "Category";
            // 
            // txtISBN
            // 
            this.txtISBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtISBN.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtISBN.Location = new System.Drawing.Point(0, 130);
            this.txtISBN.Margin = new System.Windows.Forms.Padding(4);
            this.txtISBN.Name = "txtISBN";
            this.txtISBN.ReadOnly = true;
            this.txtISBN.Size = new System.Drawing.Size(330, 28);
            this.txtISBN.TabIndex = 5;
            // 
            // lblISBN
            // 
            this.lblISBN.AutoSize = true;
            this.lblISBN.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblISBN.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblISBN.Location = new System.Drawing.Point(0, 108);
            this.lblISBN.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblISBN.Name = "lblISBN";
            this.lblISBN.Size = new System.Drawing.Size(52, 24);
            this.lblISBN.TabIndex = 4;
            this.lblISBN.Text = "ISBN";
            // 
            // txtAuthor
            // 
            this.txtAuthor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAuthor.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAuthor.Location = new System.Drawing.Point(0, 76);
            this.txtAuthor.Margin = new System.Windows.Forms.Padding(4);
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.ReadOnly = true;
            this.txtAuthor.Size = new System.Drawing.Size(676, 28);
            this.txtAuthor.TabIndex = 3;
            // 
            // lblAuthor
            // 
            this.lblAuthor.AutoSize = true;
            this.lblAuthor.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAuthor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblAuthor.Location = new System.Drawing.Point(0, 54);
            this.lblAuthor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(66, 24);
            this.lblAuthor.TabIndex = 2;
            this.lblAuthor.Text = "Author";
            // 
            // txtTitle
            // 
            this.txtTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTitle.Location = new System.Drawing.Point(0, 22);
            this.txtTitle.Margin = new System.Windows.Forms.Padding(4);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.ReadOnly = true;
            this.txtTitle.Size = new System.Drawing.Size(676, 28);
            this.txtTitle.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(45, 24);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Title";
            // 
            // panelBookCover
            // 
            this.panelBookCover.Controls.Add(this.picBookCover);
            this.panelBookCover.Controls.Add(this.lblNoCoverImage);
            this.panelBookCover.Location = new System.Drawing.Point(29, 20);
            this.panelBookCover.Margin = new System.Windows.Forms.Padding(4);
            this.panelBookCover.Name = "panelBookCover";
            this.panelBookCover.Size = new System.Drawing.Size(250, 320);
            this.panelBookCover.TabIndex = 0;
            // 
            // picBookCover
            // 
            this.picBookCover.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.picBookCover.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBookCover.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBookCover.Location = new System.Drawing.Point(0, 0);
            this.picBookCover.Margin = new System.Windows.Forms.Padding(4);
            this.picBookCover.Name = "picBookCover";
            this.picBookCover.Size = new System.Drawing.Size(250, 320);
            this.picBookCover.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBookCover.TabIndex = 0;
            this.picBookCover.TabStop = false;
            // 
            // lblNoCoverImage
            // 
            this.lblNoCoverImage.AutoSize = true;
            this.lblNoCoverImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoCoverImage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblNoCoverImage.Location = new System.Drawing.Point(75, 150);
            this.lblNoCoverImage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNoCoverImage.Name = "lblNoCoverImage";
            this.lblNoCoverImage.Size = new System.Drawing.Size(154, 25);
            this.lblNoCoverImage.TabIndex = 1;
            this.lblNoCoverImage.Text = "No Cover Image";
            this.lblNoCoverImage.Visible = false;
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFormTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblFormTitle.Location = new System.Drawing.Point(0, 0);
            this.lblFormTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Padding = new System.Windows.Forms.Padding(29, 20, 0, 20);
            this.lblFormTitle.Size = new System.Drawing.Size(339, 79);
            this.lblFormTitle.TabIndex = 0;
            this.lblFormTitle.Text = "View Book Details";
            // 
            // ViewBookForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1000, 800);
            this.Controls.Add(this.panelMainContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "ViewBookForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View Book Details";
            this.Load += new System.EventHandler(this.ViewBookForm_Load);
            this.panelMainContainer.ResumeLayout(false);
            this.panelMainContainer.PerformLayout();
            this.panelFormContent.ResumeLayout(false);
            this.panelButtons.ResumeLayout(false);
            this.panelInventoryInfo.ResumeLayout(false);
            this.panelInventoryInfo.PerformLayout();
            this.panelAdditionalInfo.ResumeLayout(false);
            this.panelAdditionalInfo.PerformLayout();
            this.panelBasicInfo.ResumeLayout(false);
            this.panelBasicInfo.PerformLayout();
            this.panelBookCover.ResumeLayout(false);
            this.panelBookCover.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBookCover)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion
    }
}
