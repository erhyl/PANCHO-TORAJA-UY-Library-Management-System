using Project5LMS.Forms.Circulation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project5LMS.Admin_Dashboard
{
    public partial class CirculationForm : Form
    {
        public CirculationForm()
        {
            InitializeComponent();
        }

        private void LoadFormInPanel(Form form)
        {
            CirculationPanel.Controls.Clear();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            CirculationPanel.Controls.Add(form);
            CirculationPanel.Tag = form;
            form.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void CirculationForm_Load(object sender, EventArgs e)
        {

        }

        private void btnhistory_Click(object sender, EventArgs e)
        {
            LoadFormInPanel(new HistoryForm());
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnrenewal_Click(object sender, EventArgs e)
        {
            LoadFormInPanel(new RenewalForm());
        }

        private void btnreturn_Click(object sender, EventArgs e)
        {
            LoadFormInPanel(new ReturnBookForm());
        }

        private void btncheckout_Click(object sender, EventArgs e)
        {
            LoadFormInPanel(new CheckoutForm());
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {


        }
    }
}
