using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Project5LMS.Models;
using Project5LMS.Services;
using Project5LMS.Interfaces;
namespace Project5LMS.Forms.Admin.Fines
{
    public partial class PaymentHistoryForm : Form
    {
        private readonly IPaymentService _paymentService;
        private readonly int _memberId;
        public PaymentHistoryForm(int memberId)
        {
            _memberId = memberId;
            _paymentService = ServiceFactory.CreatePaymentService();
            InitializeComponent();
            LoadPaymentHistory();
        }
        public PaymentHistoryForm()
        {
            _memberId = 0;
            _paymentService = null;
            InitializeComponent();
            if (!this.DesignMode)
            {
                _paymentService = ServiceFactory.CreatePaymentService();
                LoadPaymentHistory();
            }
        }
        private void LoadPaymentHistory()
        {
            try
            {
                var payments = _paymentService.GetPaymentsByMember(_memberId).ToList();
                dgvPayments.DataSource = payments.Select(p => new
                {
                    ReceiptNumber = p.ReceiptNumber ?? "N/A",
                    AmountPaid = p.AmountPaid.ToString("C"),
                    PaymentDate = p.PaymentDate.ToString("yyyy-MM-dd HH:mm"),
                    PaymentMode = p.PaymentMode ?? "Cash",
                    ProcessedBy = p.ProcessedBy ?? "System"
                }).ToList();
                var adjustments = _paymentService.GetAdjustmentsByMember(_memberId).ToList();
                dgvAdjustments.DataSource = adjustments.Select(a => new
                {
                    AdjustmentDate = a.AdjustmentDate.ToString("yyyy-MM-dd HH:mm"),
                    OriginalAmount = a.OriginalAmount.ToString("C"),
                    AdjustedAmount = a.AdjustedAmount.ToString("C"),
                    AdjustmentAmount = a.AdjustmentAmount.ToString("C"),
                    Reason = a.Reason ?? "",
                    AdjustedBy = a.AdjustedBy ?? "System"
                }).ToList();
                if (dgvPayments.Columns.Count > 0)
                {
                    dgvPayments.Columns["AmountPaid"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (dgvAdjustments.Columns.Count > 0)
                {
                    dgvAdjustments.Columns["OriginalAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvAdjustments.Columns["AdjustedAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvAdjustments.Columns["AdjustmentAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading payment history: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}