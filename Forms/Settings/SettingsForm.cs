using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using Project5LMS.Helpers;

namespace Project5LMS.Forms.Settings
{
    public partial class SettingsForm : Form
    {
        private Label dbStatusLabel;
        private Button testConnectionButton;

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            // Initialize settings form
            this.Text = "Settings";
            this.Size = new System.Drawing.Size(600, 400);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;

            // Create a simple settings UI
            Label titleLabel = new Label
            {
                Text = "Application Settings",
                Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold),
                Location = new System.Drawing.Point(20, 20),
                AutoSize = true
            };

            Label dbLabel = new Label
            {
                Text = "Database Connection:",
                Location = new System.Drawing.Point(20, 70),
                AutoSize = true
            };

            dbStatusLabel = new Label
            {
                Text = "Status: Not checked",
                Location = new System.Drawing.Point(20, 95),
                AutoSize = true
            };

            testConnectionButton = new Button
            {
                Text = "Test Connection",
                Location = new System.Drawing.Point(20, 120),
                Size = new System.Drawing.Size(150, 30)
            };
            testConnectionButton.Click += TestConnectionButton_Click;

            this.Controls.Add(titleLabel);
            this.Controls.Add(dbLabel);
            this.Controls.Add(dbStatusLabel);
            this.Controls.Add(testConnectionButton);
        }

        private void TestConnectionButton_Click(object sender, EventArgs e)
        {
            string errorMessage;
            if (DatabaseHelper.TestConnection(out errorMessage))
            {
                dbStatusLabel.Text = "Status: Connected";
                dbStatusLabel.ForeColor = Color.Green;
                MessageBox.Show("Database connection successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                dbStatusLabel.Text = $"Status: Failed";
                dbStatusLabel.ForeColor = Color.Red;
                MessageBox.Show($"Database connection failed:\n{errorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
