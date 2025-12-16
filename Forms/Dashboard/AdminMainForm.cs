using Project5LMS.Admin_Dashboard;
using Project5LMS.Controllers;
using Project5LMS.Forms.Dashboard; 
using Project5LMS.Forms.Reservation;
using System;
using System.Windows.Forms;

namespace Project5LMS.Forms.Dashboard
{
    public partial class AdminMainForm : Form
    {
        private string connectionString = "server=localhost;user=root;password=1234;database=lmsdb;";

        public AdminMainForm()
        {
            InitializeComponent();
        }

        // Helper method to load forms inside panelMain
        private void LoadFormInPanel(Form form)
        {
            MainPanel.Controls.Clear();   
            form.TopLevel = false;        
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            MainPanel.Controls.Add(form); 
            MainPanel.Tag = form;
            form.Show();
        }

        private void AdminMainForm_Load(object sender, EventArgs e)
        {
            // Optionally, load the dashboard by default when the form opens
            LoadFormInPanel(new Dashboard_Home());
        }

        private void btn_DashboardHome_Click(object sender, EventArgs e)
        {
            LoadFormInPanel(new Dashboard_Home());
        }

        private void btn_Catalog_Click(object sender, EventArgs e)
        {
            LoadFormInPanel(new CatalogForm());
        }

        private void btn_Members_Click(object sender, EventArgs e)
        {
            LoadFormInPanel(new MembersForm()); // no argument needed now
        }

        private void btn_Circulation_Click(object sender, EventArgs e)
        {
            LoadFormInPanel(new CirculationForm());
        }

        private void btn_Fines_Click(object sender, EventArgs e)
        {
            LoadFormInPanel(new FinesForm());
        }

        private void btn_Inventory_Click(object sender, EventArgs e)
        {
            LoadFormInPanel(new InventoryForm());
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            LoadFormInPanel(new CatalogSearchForm());
        }

        private void btn_Logout_Click(object sender, EventArgs e)
        {
            // Close the dashboard and return to login
            this.Hide();
            LoginForm login = new LoginForm();
            login.Show();
        }

        private void btn_Reservation_Click(object sender, EventArgs e)
        {
            LoadFormInPanel(new ReservationForm());
        }

        // Optional paint events
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void panel2_Paint(object sender, PaintEventArgs e) { }
        private void MainPanel_Paint(object sender, PaintEventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void pictureBox2_Click(object sender, EventArgs e) { }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {

        }

        
    }
}
