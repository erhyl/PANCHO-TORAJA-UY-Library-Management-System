using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Project5LMS.Helpers;
using Project5LMS.Forms.Admin.UserManagement;

namespace Project5LMS.Forms.Admin.Settings
{
    public partial class AdminSettingsForm : Form
    {
        private string connectionString;

        public AdminSettingsForm()
        {
            InitializeComponent();

            try
            {
                AccessControlHelper.RequireRole("Admin");
                AuditLogger.LogAccessControl("AdminSettingsForm accessed", $"User: {CurrentUser.Email}", "Success");
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                AuditLogger.LogAccessControl("AdminSettingsForm access denied", $"User: {CurrentUser.Email}", "Failed");
                this.Close();
                return;
            }

            try
            {
                connectionString = DatabaseHelper.GetConnectionString();
            }
            catch
            {
                connectionString = "Server=localhost;Database=librarydb;Uid=root;Pwd=;";
            }
        }

        private void AdminSettingsForm_Load(object sender, EventArgs e)
        {
            try
            {
                EnsureSettingsTableExists();
                LoadGeneralSettings();
                LoadLibraryRulesSettings();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in AdminSettingsForm_Load: {ex.Message}");
            }
        }

        private void tabControlSettings_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int selectedIndex = tabControlSettings.SelectedIndex;
                string section = GetSectionFromIndex(selectedIndex);
                LoadSection(section);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in tabControlSettings_SelectedIndexChanged: {ex.Message}");
            }
        }

        private string GetSectionFromIndex(int index)
        {
            switch (index)
            {
                case 0: return "General";
                case 1: return "LibraryRules";
                case 2: return "FineRates";
                case 3: return "UserManagement";
                case 4: return "Notifications";
                case 5: return "Security";
                case 6: return "System";
                default: return "General";
            }
        }

        private void EnsureSettingsTableExists()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string checkTableQuery = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES 
                                              WHERE TABLE_SCHEMA = DATABASE() 
                                              AND TABLE_NAME = 'Settings'";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkTableQuery, conn))
                    {
                        int tableExists = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (tableExists == 0)
                        {
                            string createTableQuery = @"CREATE TABLE IF NOT EXISTS Settings (
                                                        SettingKey VARCHAR(100) PRIMARY KEY,
                                                        SettingValue TEXT,
                                                        Category VARCHAR(50),
                                                        UpdatedDate DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
                                                        )";
                            using (MySqlCommand createCmd = new MySqlCommand(createTableQuery, conn))
                            {
                                createCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error ensuring settings table: {ex.Message}");
            }
        }

        private void LoadSection(string section)
        {
            try
            {
                TabPage currentTab = null;
                switch (section)
                {
                    case "General":
                        currentTab = tabPageGeneral;
                        LoadGeneralSettings();
                        break;
                    case "LibraryRules":
                        currentTab = tabPageLibraryRules;
                        LoadLibraryRulesSettings();
                        break;
                    case "FineRates":
                        currentTab = tabPageFineRates;
                        LoadFineRatesSection();
                        break;
                    case "UserManagement":
                        currentTab = tabPageUserManagement;
                        LoadUserManagementSection();
                        break;
                    case "Notifications":
                        currentTab = tabPageNotifications;
                        LoadNotificationsSection();
                        break;
                    case "Security":
                        currentTab = tabPageSecurity;
                        LoadSecuritySection();
                        break;
                    case "System":
                        currentTab = tabPageSystem;
                        LoadSystemSection();
                        break;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in LoadSection: {ex.Message}");
            }
        }

        private void LoadFineRatesSection()
        {
            try
            {

                string studentRate = GetSetting("FineRate_Student", "1.00");
                txtStudentFineRate.Text = studentRate.StartsWith("$") ? studentRate : "$ " + studentRate;

                string facultyRate = GetSetting("FineRate_Faculty", "0.50");
                txtFacultyFineRate.Text = facultyRate.StartsWith("$") ? facultyRate : "$ " + facultyRate;

                string staffRate = GetSetting("FineRate_Staff", "0.75");
                txtStaffFineRate.Text = staffRate.StartsWith("$") ? staffRate : "$ " + staffRate;

                string guestRate = GetSetting("FineRate_Guest", "1.50");
                txtGuestFineRate.Text = guestRate.StartsWith("$") ? guestRate : "$ " + guestRate;

                string lostCard = GetSetting("LostCardReplacement", "10.00");
                txtLostCardReplacement.Text = lostCard.StartsWith("$") ? lostCard : "$ " + lostCard;

                string maxFine = GetSetting("MaxFineCap", "50.00");
                txtMaxFineCap.Text = maxFine.StartsWith("$") ? maxFine : "$ " + maxFine;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading fine rates settings: {ex.Message}");
            }
        }

        private void btnSaveFineRates_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, TextBox> fineRateBoxes = new Dictionary<string, TextBox>
                {
                    { "Student", txtStudentFineRate },
                    { "Faculty", txtFacultyFineRate },
                    { "Staff", txtStaffFineRate },
                    { "Guest", txtGuestFineRate }
                };

                SaveFineRates(fineRateBoxes, txtLostCardReplacement, txtMaxFineCap);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving fine rates: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadUserManagementSection()
        {
            try
            {

                int staffCount = GetStaffCount();
                lblStaffStatus.Text = $"{staffCount} Users";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading user management section: {ex.Message}");
            }
        }

        private void lnkAdminManage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            NavigateToUserManagement();
        }

        private void lnkStaffManage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            NavigateToUserManagement();
        }

        private void LoadNotificationsSection()
        {
            try
            {

                chkOverdueReminders.Checked = GetSetting("OverdueReminders", "true").ToLower() == "true";
                chkReservationNotifications.Checked = GetSetting("ReservationNotifications", "true").ToLower() == "true";
                chkNewArrivals.Checked = GetSetting("NewArrivals", "false").ToLower() == "true";

                UpdateNotificationButtons(chkOverdueReminders, btnTurnOnOverdueReminders, btnTurnOffOverdueReminders);
                UpdateNotificationButtons(chkReservationNotifications, btnTurnOnReservationNotifications, btnTurnOffReservationNotifications);
                UpdateNotificationButtons(chkNewArrivals, btnTurnOnNewArrivals, btnTurnOffNewArrivals);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading notifications section: {ex.Message}");
            }
        }

        private void UpdateNotificationButtons(CheckBox chk, Button btnTurnOn, Button btnTurnOff)
        {
            UpdateToggleSwitchAppearance(chk);
            btnTurnOn.Visible = !chk.Checked;
            btnTurnOff.Visible = chk.Checked;
        }

        private void UpdateToggleSwitchAppearance(CheckBox chk)
        {
            if (chk.Checked)
            {
                chk.BackColor = Color.FromArgb(13, 110, 253);
            }
            else
            {
                chk.BackColor = Color.FromArgb(200, 200, 200);
            }
        }

        private void chkOverdueReminders_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                UpdateNotificationButtons(chkOverdueReminders, btnTurnOnOverdueReminders, btnTurnOffOverdueReminders);
                SaveSetting("OverdueReminders", chkOverdueReminders.Checked.ToString().ToLower(), "Notifications");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving overdue reminders setting: {ex.Message}");
            }
        }

        private void chkReservationNotifications_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                UpdateNotificationButtons(chkReservationNotifications, btnTurnOnReservationNotifications, btnTurnOffReservationNotifications);
                SaveSetting("ReservationNotifications", chkReservationNotifications.Checked.ToString().ToLower(), "Notifications");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving reservation notifications setting: {ex.Message}");
            }
        }

        private void chkNewArrivals_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                UpdateNotificationButtons(chkNewArrivals, btnTurnOnNewArrivals, btnTurnOffNewArrivals);
                SaveSetting("NewArrivals", chkNewArrivals.Checked.ToString().ToLower(), "Notifications");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving new arrivals setting: {ex.Message}");
            }
        }

        private void btnTurnOnOverdueReminders_Click(object sender, EventArgs e)
        {
            try
            {
                chkOverdueReminders.Checked = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error turning on overdue reminders: {ex.Message}");
            }
        }

        private void btnTurnOffOverdueReminders_Click(object sender, EventArgs e)
        {
            try
            {
                chkOverdueReminders.Checked = false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error turning off overdue reminders: {ex.Message}");
            }
        }

        private void btnTurnOnReservationNotifications_Click(object sender, EventArgs e)
        {
            try
            {
                chkReservationNotifications.Checked = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error turning on reservation notifications: {ex.Message}");
            }
        }

        private void btnTurnOffReservationNotifications_Click(object sender, EventArgs e)
        {
            try
            {
                chkReservationNotifications.Checked = false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error turning off reservation notifications: {ex.Message}");
            }
        }

        private void btnTurnOnNewArrivals_Click(object sender, EventArgs e)
        {
            try
            {
                chkNewArrivals.Checked = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error turning on new arrivals: {ex.Message}");
            }
        }

        private void btnTurnOffNewArrivals_Click(object sender, EventArgs e)
        {
            try
            {
                chkNewArrivals.Checked = false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error turning off new arrivals: {ex.Message}");
            }
        }

        private void LoadSecuritySection()
        {
            try
            {

                txtCurrentPassword.Text = "";
                txtNewPassword.Text = "";
                txtConfirmPassword.Text = "";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading security section: {ex.Message}");
            }
        }

        private void btnUpdatePassword_Click(object sender, EventArgs e)
        {
            try
            {
                UpdatePassword(txtCurrentPassword, txtNewPassword, txtConfirmPassword);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating password: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSystemSection()
        {
            try
            {

                lblSystemVersionValue.Text = "v2.5.1";

                bool isConnected = TestDatabaseConnection();
                string dbStatus = isConnected ? "Connected" : "Disconnected";
                lblDatabaseStatusValue.Text = dbStatus;
                lblDatabaseStatusValue.BackColor = isConnected ? Color.FromArgb(40, 167, 69) : Color.FromArgb(220, 53, 69);

                string lastBackup = GetSetting("LastBackup", DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd hh:mm tt"));
                lblLastBackupValue.Text = lastBackup;

                lblStorageUsedValue.Text = "245 GB / 500 GB";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading system section: {ex.Message}");
            }
        }

        private void LoadLibraryRulesSettings()
        {
            try
            {
                txtStudentLimit.Text = GetSetting("BorrowLimit_Student", "5");
                txtFacultyLimit.Text = GetSetting("BorrowLimit_Faculty", "10");
                txtStaffLimit.Text = GetSetting("BorrowLimit_Staff", "7");
                txtGuestLimit.Text = GetSetting("BorrowLimit_Guest", "3");

                txtStudentPeriod.Text = GetSetting("BorrowPeriod_Student", "14");
                txtFacultyPeriod.Text = GetSetting("BorrowPeriod_Faculty", "30");
                txtStaffPeriod.Text = GetSetting("BorrowPeriod_Staff", "21");
                txtGuestPeriod.Text = GetSetting("BorrowPeriod_Guest", "7");

                txtMaxRenewals.Text = GetSetting("MaxRenewals", "2");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading library rules settings: {ex.Message}");
            }
        }

        private void btnSaveLibraryRules_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, TextBox> borrowingLimits = new Dictionary<string, TextBox>
                {
                    { "Student", txtStudentLimit },
                    { "Faculty", txtFacultyLimit },
                    { "Staff", txtStaffLimit },
                    { "Guest", txtGuestLimit }
                };

                Dictionary<string, TextBox> borrowingPeriods = new Dictionary<string, TextBox>
                {
                    { "Student", txtStudentPeriod },
                    { "Faculty", txtFacultyPeriod },
                    { "Staff", txtStaffPeriod },
                    { "Guest", txtGuestPeriod }
                };

                SaveLibraryRules(borrowingLimits, borrowingPeriods, txtMaxRenewals);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving library rules: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadGeneralSettings()
        {
            try
            {
                txtLibraryName.Text = GetSetting("LibraryName", "City Central Library");
                txtLibraryCode.Text = GetSetting("LibraryCode", "CCL-001");
                txtAddress.Text = GetSetting("Address", "123 Main Street, City, State 12345");
                txtContactEmail.Text = GetSetting("ContactEmail", "contact@library.com");
                txtPhoneNumber.Text = GetSetting("PhoneNumber", "(555) 123-4567");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading general settings: {ex.Message}");
            }
        }

        private void btnSaveGeneral_Click(object sender, EventArgs e)
        {
            SaveGeneralSettings(txtLibraryName, txtLibraryCode, txtAddress, txtContactEmail, txtPhoneNumber);
        }

        private string GetSetting(string key, string defaultValue = "")
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT SettingValue FROM Settings WHERE SettingKey = @Key";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Key", key);
                        object result = cmd.ExecuteScalar();
                        return result?.ToString() ?? defaultValue;
                    }
                }
            }
            catch
            {
                return defaultValue;
            }
        }

        private void SaveSetting(string key, string value, string category = "General")
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"INSERT INTO Settings (SettingKey, SettingValue, Category) 
                                   VALUES (@Key, @Value, @Category)
                                   ON DUPLICATE KEY UPDATE SettingValue = @Value, Category = @Category";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Key", key);
                        cmd.Parameters.AddWithValue("@Value", value);
                        cmd.Parameters.AddWithValue("@Category", category);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving setting: {ex.Message}");
            }
        }

        private void SaveGeneralSettings(TextBox txtLibraryName, TextBox txtLibraryCode, TextBox txtAddress, TextBox txtEmail, TextBox txtPhone)
        {
            SaveSetting("LibraryName", txtLibraryName.Text, "General");
            SaveSetting("LibraryCode", txtLibraryCode.Text, "General");
            SaveSetting("Address", txtAddress.Text, "General");
            SaveSetting("ContactEmail", txtEmail.Text, "General");
            SaveSetting("PhoneNumber", txtPhone.Text, "General");
            MessageBox.Show("General settings saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SaveLibraryRules(Dictionary<string, TextBox> borrowingLimits, Dictionary<string, TextBox> borrowingPeriods, TextBox txtMaxRenewals)
        {
            foreach (var kvp in borrowingLimits)
            {
                SaveSetting($"BorrowLimit_{kvp.Key}", kvp.Value.Text, "LibraryRules");
            }
            foreach (var kvp in borrowingPeriods)
            {
                SaveSetting($"BorrowPeriod_{kvp.Key}", kvp.Value.Text, "LibraryRules");
            }
            SaveSetting("MaxRenewals", txtMaxRenewals.Text, "LibraryRules");
            MessageBox.Show("Library rules saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SaveFineRates(Dictionary<string, TextBox> fineRates, TextBox txtLostCard, TextBox txtMaxFine)
        {
            try
            {
                foreach (var kvp in fineRates)
                {

                    string value = kvp.Value.Text.Replace("$ ", "").Trim();
                    SaveSetting($"FineRate_{kvp.Key}", value, "FineRates");
                }

                string lostCardValue = txtLostCard.Text.Replace("$ ", "").Trim();
                string maxFineValue = txtMaxFine.Text.Replace("$ ", "").Trim();

                SaveSetting("LostCardReplacement", lostCardValue, "FineRates");
                SaveSetting("MaxFineCap", maxFineValue, "FineRates");
                MessageBox.Show("Fine rates saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving fine rates: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdatePassword(TextBox txtCurrent, TextBox txtNew, TextBox txtConfirm)
        {
            if (string.IsNullOrWhiteSpace(txtCurrent.Text) || string.IsNullOrWhiteSpace(txtNew.Text) || string.IsNullOrWhiteSpace(txtConfirm.Text))
            {
                MessageBox.Show("Please fill in all password fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtNew.Text != txtConfirm.Text)
            {
                MessageBox.Show("New password and confirmation do not match.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT PasswordHash FROM Users WHERE UserID = @UserID";
                    string currentHash = "";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", CurrentUser.UserID);
                        object result = cmd.ExecuteScalar();
                        if (result != null) currentHash = result.ToString();
                    }

                    if (!PasswordHasher.Verify(txtCurrent.Text, currentHash))
                    {
                        MessageBox.Show("Current password is incorrect.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string newHash = PasswordHasher.HashPassword(txtNew.Text);
                    string updateQuery = "UPDATE Users SET PasswordHash = @Hash WHERE UserID = @UserID";
                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Hash", newHash);
                        cmd.Parameters.AddWithValue("@UserID", CurrentUser.UserID);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Password updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCurrent.Clear();
                    txtNew.Clear();
                    txtConfirm.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating password: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetStaffCount()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Users WHERE Role = 'LibraryStaff'";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch
            {
                return 0;
            }
        }

        private bool TestDatabaseConnection()
        {
            return DatabaseHelper.TestConnection(out _);
        }

        private void NavigateToUserManagement()
        {
            try
            {
                Control parent = this.Parent;
                while (parent != null && !(parent is Form))
                {
                    parent = parent.Parent;
                }

                if (parent is Form mainForm)
                {
                    var method = mainForm.GetType().GetMethod("LoadFormInPanel");
                    if (method != null)
                    {
                        var userManagementForm = new Project5LMS.Forms.Admin.UserManagement.UserManagementForm();
                        method.Invoke(mainForm, new object[] { userManagementForm });
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error navigating to user management: {ex.Message}");
            }
        }
    }
}
