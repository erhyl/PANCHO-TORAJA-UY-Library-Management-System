namespace Project5LMS.Forms.Reservation
{
    partial class ReservationForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbnMember = new System.Windows.Forms.TextBox();
            this.dtpReservationDate = new System.Windows.Forms.DateTimePicker();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnBookTitle = new System.Windows.Forms.TextBox();
            this.cmbBookStatus = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.dtpPickupDate = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.cmbStatus = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnCreateReservation = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(31, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "10";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(350, 100);
            this.panel1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(31, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(242, 31);
            this.label2.TabIndex = 2;
            this.label2.Text = "Active Reservation";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(391, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(350, 100);
            this.panel2.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(31, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(241, 31);
            this.label3.TabIndex = 2;
            this.label3.Text = "Pending Approvals";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(31, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 31);
            this.label4.TabIndex = 0;
            this.label4.Text = "12";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Location = new System.Drawing.Point(774, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(350, 100);
            this.panel3.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(31, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 31);
            this.label5.TabIndex = 2;
            this.label5.Text = "Expired";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(31, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 31);
            this.label6.TabIndex = 0;
            this.label6.Text = "3";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(21, 166);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(169, 24);
            this.label7.TabIndex = 3;
            this.label7.Text = "Reservation Details";
            // 
            // tbnMember
            // 
            this.tbnMember.Location = new System.Drawing.Point(25, 234);
            this.tbnMember.Name = "tbnMember";
            this.tbnMember.Size = new System.Drawing.Size(200, 20);
            this.tbnMember.TabIndex = 5;
            // 
            // dtpReservationDate
            // 
            this.dtpReservationDate.Location = new System.Drawing.Point(25, 349);
            this.dtpReservationDate.Name = "dtpReservationDate";
            this.dtpReservationDate.Size = new System.Drawing.Size(200, 20);
            this.dtpReservationDate.TabIndex = 6;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(25, 461);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(200, 21);
            this.comboBox1.TabIndex = 7;
            // 
            // btnBookTitle
            // 
            this.btnBookTitle.Location = new System.Drawing.Point(25, 287);
            this.btnBookTitle.Name = "btnBookTitle";
            this.btnBookTitle.Size = new System.Drawing.Size(200, 20);
            this.btnBookTitle.TabIndex = 8;
            // 
            // cmbBookStatus
            // 
            this.cmbBookStatus.FormattingEnabled = true;
            this.cmbBookStatus.Location = new System.Drawing.Point(983, 170);
            this.cmbBookStatus.Name = "cmbBookStatus";
            this.cmbBookStatus.Size = new System.Drawing.Size(121, 21);
            this.cmbBookStatus.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(22, 215);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(114, 16);
            this.label8.TabIndex = 11;
            this.label8.Text = "Member Name\\ID";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(22, 268);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(106, 16);
            this.label9.TabIndex = 12;
            this.label9.Text = "Book Title\\ ISBN";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(27, 330);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(112, 16);
            this.label10.TabIndex = 13;
            this.label10.Text = "Reservation Date";
            // 
            // dtpPickupDate
            // 
            this.dtpPickupDate.AutoSize = true;
            this.dtpPickupDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpPickupDate.Location = new System.Drawing.Point(27, 390);
            this.dtpPickupDate.Name = "dtpPickupDate";
            this.dtpPickupDate.Size = new System.Drawing.Size(80, 16);
            this.dtpPickupDate.TabIndex = 15;
            this.dtpPickupDate.Text = "Pickup Date";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(25, 409);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker2.TabIndex = 14;
            // 
            // cmbStatus
            // 
            this.cmbStatus.AutoSize = true;
            this.cmbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatus.Location = new System.Drawing.Point(27, 442);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(44, 16);
            this.cmbStatus.TabIndex = 16;
            this.cmbStatus.Text = "Status";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(980, 151);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(44, 16);
            this.label13.TabIndex = 17;
            this.label13.Text = "Status";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(486, 171);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(457, 20);
            this.txtSearch.TabIndex = 18;
            this.txtSearch.Text = "Search Reservation";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(252, 197);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1027, 400);
            this.dataGridView1.TabIndex = 19;
            // 
            // btnCreateReservation
            // 
            this.btnCreateReservation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateReservation.Location = new System.Drawing.Point(16, 0);
            this.btnCreateReservation.Name = "btnCreateReservation";
            this.btnCreateReservation.Size = new System.Drawing.Size(175, 48);
            this.btnCreateReservation.TabIndex = 20;
            this.btnCreateReservation.Text = "Create Reservation";
            this.btnCreateReservation.UseVisualStyleBackColor = true;
            this.btnCreateReservation.Click += new System.EventHandler(this.btnCreateReservation_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnCreateReservation);
            this.panel4.Location = new System.Drawing.Point(22, 488);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(203, 54);
            this.panel4.TabIndex = 21;
            // 
            // ReservationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1325, 636);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.dtpPickupDate);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmbBookStatus);
            this.Controls.Add(this.btnBookTitle);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.dtpReservationDate);
            this.Controls.Add(this.tbnMember);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ReservationForm";
            this.Load += new System.EventHandler(this.ReservationForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbnMember;
        private System.Windows.Forms.DateTimePicker dtpReservationDate;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox btnBookTitle;
        private System.Windows.Forms.ComboBox cmbBookStatus;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label dtpPickupDate;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label cmbStatus;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnCreateReservation;
        private System.Windows.Forms.Panel panel4;
    }
}