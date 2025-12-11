namespace Project5LMS.Admin_Dashboard
{
    partial class Catalog
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
            this.dta_Grid1 = new System.Windows.Forms.DataGridView();
            this.btn_addBook = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_EditBook = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btn_AddCopies = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btn_Import = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dta_Grid1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // dta_Grid1
            // 
            this.dta_Grid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dta_Grid1.Location = new System.Drawing.Point(12, 3);
            this.dta_Grid1.Name = "dta_Grid1";
            this.dta_Grid1.Size = new System.Drawing.Size(1044, 357);
            this.dta_Grid1.TabIndex = 1;
            this.dta_Grid1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dta_Grid1_CellContentClick);
            // 
            // btn_addBook
            // 
            this.btn_addBook.BackColor = System.Drawing.Color.LimeGreen;
            this.btn_addBook.Location = new System.Drawing.Point(3, 3);
            this.btn_addBook.Name = "btn_addBook";
            this.btn_addBook.Size = new System.Drawing.Size(110, 34);
            this.btn_addBook.TabIndex = 2;
            this.btn_addBook.Text = "Add Book";
            this.btn_addBook.UseVisualStyleBackColor = false;
            this.btn_addBook.Click += new System.EventHandler(this.btn_addBook_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_addBook);
            this.panel1.Location = new System.Drawing.Point(12, 398);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(113, 40);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_EditBook);
            this.panel2.Location = new System.Drawing.Point(131, 398);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(113, 40);
            this.panel2.TabIndex = 4;
            // 
            // btn_EditBook
            // 
            this.btn_EditBook.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btn_EditBook.Location = new System.Drawing.Point(3, 3);
            this.btn_EditBook.Name = "btn_EditBook";
            this.btn_EditBook.Size = new System.Drawing.Size(110, 34);
            this.btn_EditBook.TabIndex = 2;
            this.btn_EditBook.Text = "Edit Book";
            this.btn_EditBook.UseVisualStyleBackColor = false;
            this.btn_EditBook.Click += new System.EventHandler(this.btn_EditBook_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btn_AddCopies);
            this.panel3.Location = new System.Drawing.Point(250, 398);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(113, 40);
            this.panel3.TabIndex = 4;
            // 
            // btn_AddCopies
            // 
            this.btn_AddCopies.BackColor = System.Drawing.Color.Gold;
            this.btn_AddCopies.Location = new System.Drawing.Point(3, 3);
            this.btn_AddCopies.Name = "btn_AddCopies";
            this.btn_AddCopies.Size = new System.Drawing.Size(110, 34);
            this.btn_AddCopies.TabIndex = 2;
            this.btn_AddCopies.Text = "Add Copies";
            this.btn_AddCopies.UseVisualStyleBackColor = false;
            this.btn_AddCopies.Click += new System.EventHandler(this.btn_AddCopies_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btn_Delete);
            this.panel4.Location = new System.Drawing.Point(369, 398);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(113, 40);
            this.panel4.TabIndex = 5;
            // 
            // btn_Delete
            // 
            this.btn_Delete.BackColor = System.Drawing.Color.Red;
            this.btn_Delete.Location = new System.Drawing.Point(3, 3);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(110, 34);
            this.btn_Delete.TabIndex = 2;
            this.btn_Delete.Text = "Delete";
            this.btn_Delete.UseVisualStyleBackColor = false;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btn_Import);
            this.panel5.Location = new System.Drawing.Point(675, 401);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(113, 40);
            this.panel5.TabIndex = 4;
            // 
            // btn_Import
            // 
            this.btn_Import.BackColor = System.Drawing.Color.DarkTurquoise;
            this.btn_Import.Location = new System.Drawing.Point(3, 3);
            this.btn_Import.Name = "btn_Import";
            this.btn_Import.Size = new System.Drawing.Size(110, 34);
            this.btn_Import.TabIndex = 2;
            this.btn_Import.Text = "Import";
            this.btn_Import.UseVisualStyleBackColor = false;
            this.btn_Import.Click += new System.EventHandler(this.btn_Import_Click);
            // 
            // Catalog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1520, 927);
            this.ControlBox = false;
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dta_Grid1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Catalog";
            this.Text = "Catalog";
            this.Load += new System.EventHandler(this.Catalog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dta_Grid1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dta_Grid1;
        private System.Windows.Forms.Button btn_addBook;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_EditBook;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btn_AddCopies;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btn_Import;
    }
}