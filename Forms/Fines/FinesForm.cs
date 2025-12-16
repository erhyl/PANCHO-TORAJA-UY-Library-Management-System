using Project5LMS.Forms.Circulation;
using Project5LMS.Forms.Fines;
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
    public partial class FinesForm : Form
    {
        public FinesForm()
        {
            InitializeComponent();
        }

        private void LoadFormInPanel(Form form)
        {
            CalculatorPanel.Controls.Clear();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            CalculatorPanel.Controls.Add(form);
            CalculatorPanel.Tag = form;
            form.Show();
        }

        private void FinesForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblActiveLoans_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void btn_FineCalculator_Click(object sender, EventArgs e)
        {
            LoadFormInPanel(new FineCalculatorForm());
        }

        private void CalculatorPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
