using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Project5LMS.Models;
using Project5LMS.Services;
namespace Project5LMS.Forms.Admin.Search
{
    public partial class BrowseFiltersForm : Form
    {
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public bool UseWeightedPopularity { get; private set; }
        public DialogResult Result { get; private set; } = DialogResult.Cancel;
        public BrowseFiltersForm(string browseType)
        {
            InitializeComponent();
            lblBrowseType.Text = browseType;
            if (browseType.Contains("New Arrivals"))
            {
                dtpStartDate.Value = DateTime.Now.AddDays(-30);
                dtpEndDate.Value = DateTime.Now;
                chkWeightedPopularity.Visible = false;
            }
            else if (browseType.Contains("Popular"))
            {
                chkWeightedPopularity.Visible = true;
                chkWeightedPopularity.Checked = true;
                dtpStartDate.Enabled = false;
                dtpEndDate.Enabled = false;
            }
        }
        private void btnApply_Click(object sender, EventArgs e)
        {
            if (lblBrowseType.Text.Contains("New Arrivals"))
            {
                StartDate = dtpStartDate.Value;
                EndDate = dtpEndDate.Value;
            }
            else
            {
                UseWeightedPopularity = chkWeightedPopularity.Checked;
            }
            Result = DialogResult.OK;
            this.Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Result = DialogResult.Cancel;
            this.Close();
        }
    }
}