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
    public partial class Admin_Dashboard : Form
    {
        public Admin_Dashboard()
        {
            InitializeComponent();
        }

        private void Admin_Dashboard_Load(object sender, EventArgs e)
        {

        }

        // =====================================================
        // METHOD FOR LOADING FORMS INSIDE THE MAIN PANEL
        // =====================================================
        private void LoadFormInPanel(object form)
        {
            if (this.panelMain.Controls.Count > 0)
                this.panelMain.Controls.RemoveAt(0);

            Form f = form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.panelMain.Controls.Add(f);
            this.panelMain.Tag = f;
            f.Show();
        }

        // =====================================================
        // BUTTON EVENTS (SIDE MENU)
        // =====================================================

        private void btn_dashboard_Click(object sender, EventArgs e)
        {
            this.panelMain.Controls.Clear();  // remove whatever is inside
            Label lbl = new Label();
            lbl.Text = "Welcome to the Admin Dashboard!";
            lbl.Dock = DockStyle.Fill;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.Font = new Font("Arial", 20, FontStyle.Bold);
            this.panelMain.Controls.Add(lbl);

        }

        private void btn_Catalog_Click(object sender, EventArgs e)
        {
            LoadFormInPanel(new Catalog());   // catalog form
        }

        private void btn_Circulation_Click(object sender, EventArgs e)
        {
            LoadFormInPanel(new Circulation());   // circulation form
        }

        private void btn_Fines_Click(object sender, EventArgs e)
        {
             LoadFormInPanel(new Fines()); // fines form
        }

        private void btn_Inventory_Click(object sender, EventArgs e)
        {
            LoadFormInPanel(new Inventory()); // inventory form
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            LoadFormInPanel(new Search());  // search form
        }
        private void btn_Members_Click(object sender, EventArgs e)
        {
            LoadFormInPanel(new Members()); // members form
        }
        private void btn_Logout_Click(object sender, EventArgs e)
        {
            Close(); // close the admin dashboard
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        
    }
}
