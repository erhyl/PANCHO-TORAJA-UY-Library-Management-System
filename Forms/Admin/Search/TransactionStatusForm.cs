using System;
using System.Drawing;
using System.Windows.Forms;
namespace Project5LMS.Forms.Admin.Search
{
    public partial class TransactionStatusForm : Form
    {
        private bool _cancelled = false;
        private string _operationName = "Transaction";
        public bool IsCancelled => _cancelled;
        public TransactionStatusForm(string operationName)
        {
            _operationName = operationName ?? "Transaction";
            InitializeComponent();
            if (!this.DesignMode)
            {
                lblStatus.Text = $"Processing {_operationName}...";
            }
        }
        public TransactionStatusForm()
        {
            _operationName = "Transaction";
            InitializeComponent();
        }
        private void TransactionStatusForm_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                lblStatus.Text = $"Processing {_operationName}...";
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            _cancelled = true;
            this.Close();
        }
        public void UpdateStatus(string message)
        {
            if (lblStatus != null)
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
}