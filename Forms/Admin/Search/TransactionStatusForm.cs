using System;
using System.Drawing;
using System.Windows.Forms;
namespace Project5LMS.Forms.Admin.Search
{
    public class TransactionStatusForm : Form
    {
        private Label lblStatus;
        private ProgressBar progressBar;
        private Button btnCancel;
        private bool _cancelled = false;
        public bool IsCancelled => _cancelled;
        public TransactionStatusForm(string operationName)
        {
            InitializeComponent(operationName);
        }
        private void InitializeComponent(string operationName)
        {
            this.Text = "Processing Transaction";
            this.Size = new Size(400, 150);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ControlBox = false;
            lblStatus = new Label
            {
                Text = $"Processing {operationName}...",
                Font = new Font("Segoe UI", 10F),
                Location = new Point(20, 20),
                AutoSize = true
            };
            progressBar = new ProgressBar
            {
                Style = ProgressBarStyle.Marquee,
                Location = new Point(20, 50),
                Size = new Size(350, 23),
                MarqueeAnimationSpeed = 30
            };
            btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(295, 85),
                Size = new Size(75, 30)
            };
            btnCancel.Click += (s, e) => { _cancelled = true; this.Close(); };
            this.Controls.Add(lblStatus);
            this.Controls.Add(progressBar);
            this.Controls.Add(btnCancel);
        }
        public void UpdateStatus(string message)
        {
            if (lblStatus.InvokeRequired)
            {
                lblStatus.Invoke(new Action<string>(UpdateStatus), message);
            }
            else
            {
                lblStatus.Text = message;
                Application.DoEvents();
            }
        }
    }
}