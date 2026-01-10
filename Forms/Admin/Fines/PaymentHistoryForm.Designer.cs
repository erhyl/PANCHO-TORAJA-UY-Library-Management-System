namespace Project5LMS.Forms.Admin.Fines
{
    partial class PaymentHistoryForm
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPagePayments = new System.Windows.Forms.TabPage();
            this.dgvPayments = new System.Windows.Forms.DataGridView();
            this.tabPageAdjustments = new System.Windows.Forms.TabPage();
            this.dgvAdjustments = new System.Windows.Forms.DataGridView();
            this.tabControl.SuspendLayout();
            this.tabPagePayments.SuspendLayout();
            this.tabPageAdjustments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdjustments)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPagePayments);
            this.tabControl.Controls.Add(this.tabPageAdjustments);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(900, 600);
            this.tabControl.TabIndex = 0;
            // 
            // tabPagePayments
            // 
            this.tabPagePayments.Controls.Add(this.dgvPayments);
            this.tabPagePayments.Location = new System.Drawing.Point(4, 28);
            this.tabPagePayments.Name = "tabPagePayments";
            this.tabPagePayments.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePayments.Size = new System.Drawing.Size(892, 568);
            this.tabPagePayments.TabIndex = 0;
            this.tabPagePayments.Text = "Payments";
            this.tabPagePayments.UseVisualStyleBackColor = true;
            // 
            // dgvPayments
            // 
            this.dgvPayments.AllowUserToAddRows = false;
            this.dgvPayments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPayments.BackgroundColor = System.Drawing.Color.White;
            this.dgvPayments.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvPayments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPayments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPayments.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvPayments.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvPayments.Location = new System.Drawing.Point(3, 3);
            this.dgvPayments.Name = "dgvPayments";
            this.dgvPayments.ReadOnly = true;
            this.dgvPayments.RowHeadersWidth = 51;
            this.dgvPayments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPayments.Size = new System.Drawing.Size(886, 562);
            this.dgvPayments.TabIndex = 0;
            // 
            // tabPageAdjustments
            // 
            this.tabPageAdjustments.Controls.Add(this.dgvAdjustments);
            this.tabPageAdjustments.Location = new System.Drawing.Point(4, 28);
            this.tabPageAdjustments.Name = "tabPageAdjustments";
            this.tabPageAdjustments.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAdjustments.Size = new System.Drawing.Size(892, 568);
            this.tabPageAdjustments.TabIndex = 1;
            this.tabPageAdjustments.Text = "Adjustments";
            this.tabPageAdjustments.UseVisualStyleBackColor = true;
            // 
            // dgvAdjustments
            // 
            this.dgvAdjustments.AllowUserToAddRows = false;
            this.dgvAdjustments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAdjustments.BackgroundColor = System.Drawing.Color.White;
            this.dgvAdjustments.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvAdjustments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAdjustments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAdjustments.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvAdjustments.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvAdjustments.Location = new System.Drawing.Point(3, 3);
            this.dgvAdjustments.Name = "dgvAdjustments";
            this.dgvAdjustments.ReadOnly = true;
            this.dgvAdjustments.RowHeadersWidth = 51;
            this.dgvAdjustments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAdjustments.Size = new System.Drawing.Size(886, 562);
            this.dgvAdjustments.TabIndex = 0;
            // 
            // PaymentHistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PaymentHistoryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Payment History";
            this.tabControl.ResumeLayout(false);
            this.tabPagePayments.ResumeLayout(false);
            this.tabPageAdjustments.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdjustments)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPagePayments;
        private System.Windows.Forms.DataGridView dgvPayments;
        private System.Windows.Forms.TabPage tabPageAdjustments;
        private System.Windows.Forms.DataGridView dgvAdjustments;
    }
}

