using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Project5LMS.Helpers;
using Project5LMS.Services;
using Project5LMS.Data;
using Project5LMS.Interfaces;
namespace Project5LMS.Forms.Admin.Settings
{
    public partial class AdminSettingsForm : Form
    {
        private readonly ISettingsService _settingsService;
        private readonly DatabaseContext _dbContext;
        public AdminSettingsForm()
        {
            InitializeComponent();
            _dbContext = ServiceFactory.GetDbContext();
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
            _settingsService = ServiceFactory.CreateSettingsService();
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
                case 3: return "Notifications";
                case 4: return "Security";
                case 5: return "System";
                default: return "General";
            }
        }
        private void EnsureSettingsTableExists()
        {
            try
            {
                _settingsService.EnsureSettingsTableExists();
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
                txtStudentFineRate.Text = (studentRate.StartsWith("₱") || studentRate.StartsWith("Php") || studentRate.StartsWith("$")) ? studentRate : "₱ " + studentRate;
                string facultyRate = GetSetting("FineRate_Faculty", "0.50");
                txtFacultyFineRate.Text = (facultyRate.StartsWith("₱") || facultyRate.StartsWith("Php") || facultyRate.StartsWith("$")) ? facultyRate : "₱ " + facultyRate;
                string staffRate = GetSetting("FineRate_Staff", "0.75");
                txtStaffFineRate.Text = (staffRate.StartsWith("₱") || staffRate.StartsWith("Php") || staffRate.StartsWith("$")) ? staffRate : "₱ " + staffRate;
                string guestRate = GetSetting("FineRate_Guest", "1.50");
                txtGuestFineRate.Text = (guestRate.StartsWith("₱") || guestRate.StartsWith("Php") || guestRate.StartsWith("$")) ? guestRate : "₱ " + guestRate;
                string lostCard = GetSetting("LostCardReplacement", Constants.LostCardReplacementFee.ToString("F2"));
                txtLostCardReplacement.Text = (lostCard.StartsWith("₱") || lostCard.StartsWith("Php") || lostCard.StartsWith("$")) ? lostCard : "₱ " + lostCard;
                string maxFine = GetSetting("MaxFineCap", "50.00");
                txtMaxFineCap.Text = (maxFine.StartsWith("₱") || maxFine.StartsWith("Php") || maxFine.StartsWith("$")) ? maxFine : "₱ " + maxFine;
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
                
                // Add backup button if it doesn't exist
                bool backupButtonExists = false;
                foreach (Control control in panelSystemInfoCard.Controls)
                {
                    if (control is Button btn && btn.Name == "btnBackupDatabase")
                    {
                        backupButtonExists = true;
                        break;
                    }
                }
                if (!backupButtonExists)
                {
                    Button btnBackup = new Button
                    {
                        Name = "btnBackupDatabase",
                        Text = "Backup Database",
                        Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                        ForeColor = Color.White,
                        BackColor = Color.FromArgb(13, 110, 253),
                        FlatStyle = FlatStyle.Flat,
                        FlatAppearance = { BorderSize = 0 },
                        Size = new Size(200, 45),
                        Location = new Point(20, 230),
                        Cursor = Cursors.Hand
                    };
                    btnBackup.Click += BtnBackupDatabase_Click;
                    panelSystemInfoCard.Controls.Add(btnBackup);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading system section: {ex.Message}");
            }
        }
        
        private void BtnBackupDatabase_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "SQL Files (*.sql)|*.sql|All Files (*.*)|*.*";
                    saveDialog.FileName = $"LibraryDB_Backup_{DateTime.Now:yyyyMMdd_HHmmss}.sql";
                    saveDialog.DefaultExt = "sql";
                    saveDialog.Title = "Save Database Backup";
                    
                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        Cursor = Cursors.WaitCursor;
                        bool success = PerformDatabaseBackup(saveDialog.FileName);
                        Cursor = Cursors.Default;
                        
                        if (success)
                        {
                            string backupTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm tt");
                            _settingsService.SaveSetting("LastBackup", backupTime, "System");
                            lblLastBackupValue.Text = backupTime;
                            
                            AuditLogger.LogDataModification("Database Backup",
                                $"Backup created: {saveDialog.FileName}",
                                "Success");
                            
                            MessageBox.Show(
                                $"Database backup created successfully!\n\n" +
                                $"Location: {saveDialog.FileName}\n" +
                                $"Time: {backupTime}",
                                "Backup Success",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(
                                "Failed to create database backup. Please check:\n" +
                                "1. Database connection\n" +
                                "2. File permissions\n" +
                                "3. Available disk space",
                                "Backup Failed",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show($"Error creating backup: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AuditLogger.LogDataModification("Database Backup Failed",
                    $"Error: {ex.Message}",
                    "Failed");
            }
        }
        
        private bool PerformDatabaseBackup(string filePath)
        {
            try
            {
                string connectionString = DatabaseHelper.GetConnectionString();
                if (string.IsNullOrEmpty(connectionString))
                {
                    // Try to get from App.config
                    connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnectionString"]?.ConnectionString;
                }
                
                if (string.IsNullOrEmpty(connectionString))
                {
                    System.Diagnostics.Debug.WriteLine("Connection string not found");
                    return false;
                }
                
                // Extract database name from connection string
                string databaseName = ExtractDatabaseName(connectionString);
                if (string.IsNullOrEmpty(databaseName))
                {
                    System.Diagnostics.Debug.WriteLine("Could not extract database name");
                    return false;
                }
                
                // Use mysqldump if available, otherwise create SQL script manually
                string mysqldumpPath = FindMysqldumpPath();
                if (!string.IsNullOrEmpty(mysqldumpPath))
                {
                    return BackupUsingMysqldump(mysqldumpPath, connectionString, databaseName, filePath);
                }
                else
                {
                    // Fallback: Create SQL script with table structures and data
                    return BackupUsingSQLScript(connectionString, databaseName, filePath);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Backup error: {ex.Message}");
                return false;
            }
        }
        
        private string ExtractDatabaseName(string connectionString)
        {
            try
            {
                var builder = new MySql.Data.MySqlClient.MySqlConnectionStringBuilder(connectionString);
                return builder.Database;
            }
            catch
            {
                // Try manual parsing
                var parts = connectionString.Split(';');
                foreach (var part in parts)
                {
                    if (part.Trim().StartsWith("Database=", StringComparison.OrdinalIgnoreCase))
                    {
                        return part.Split('=')[1].Trim();
                    }
                }
                return null;
            }
        }
        
        private string FindMysqldumpPath()
        {
            string[] commonPaths = {
                @"C:\Program Files\MySQL\MySQL Server 8.0\bin\mysqldump.exe",
                @"C:\Program Files\MySQL\MySQL Server 5.7\bin\mysqldump.exe",
                @"C:\xampp\mysql\bin\mysqldump.exe",
                @"C:\wamp64\bin\mysql\mysql8.0.27\bin\mysqldump.exe",
                @"C:\Program Files (x86)\MySQL\MySQL Server 8.0\bin\mysqldump.exe"
            };
            
            foreach (var path in commonPaths)
            {
                if (System.IO.File.Exists(path))
                    return path;
            }
            
            // Try to find in PATH
            string pathEnv = Environment.GetEnvironmentVariable("PATH");
            if (!string.IsNullOrEmpty(pathEnv))
            {
                foreach (var dir in pathEnv.Split(';'))
                {
                    string fullPath = System.IO.Path.Combine(dir, "mysqldump.exe");
                    if (System.IO.File.Exists(fullPath))
                        return fullPath;
                }
            }
            
            return null;
        }
        
        private bool BackupUsingMysqldump(string mysqldumpPath, string connectionString, string databaseName, string outputPath)
        {
            try
            {
                var builder = new MySql.Data.MySqlClient.MySqlConnectionStringBuilder(connectionString);
                string server = builder.Server;
                string user = builder.UserID;
                string password = builder.Password;
                int port = (int)builder.Port;
                
                var processInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = mysqldumpPath,
                    Arguments = $"--host={server} --port={port} --user={user} --password={password} --single-transaction --routines --triggers {databaseName}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                
                using (var process = System.Diagnostics.Process.Start(processInfo))
                {
                    using (var fileStream = new System.IO.FileStream(outputPath, System.IO.FileMode.Create))
                    using (var writer = new System.IO.StreamWriter(fileStream))
                    {
                        process.OutputDataReceived += (sender, e) =>
                        {
                            if (!string.IsNullOrEmpty(e.Data))
                                writer.WriteLine(e.Data);
                        };
                        process.BeginOutputReadLine();
                        process.WaitForExit();
                        
                        if (process.ExitCode == 0)
                            return true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"mysqldump error: {ex.Message}");
            }
            return false;
        }
        
        private bool BackupUsingSQLScript(string connectionString, string databaseName, string outputPath)
        {
            try
            {
                using (var writer = new System.IO.StreamWriter(outputPath))
                {
                    writer.WriteLine("-- Library Management System Database Backup");
                    writer.WriteLine($"-- Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                    writer.WriteLine($"-- Database: {databaseName}");
                    writer.WriteLine();
                    writer.WriteLine($"CREATE DATABASE IF NOT EXISTS `{databaseName}`;");
                    writer.WriteLine($"USE `{databaseName}`;");
                    writer.WriteLine();
                    
                    using (var conn = _dbContext.GetConnection())
                    {
                        conn.Open();
                        
                        // Get all tables
                        string tablesQuery = "SHOW TABLES";
                        var tables = new List<string>();
                        using (var cmd = new MySqlCommand(tablesQuery, conn))
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                tables.Add(reader[0].ToString());
                            }
                        }
                        
                        // Backup each table
                        foreach (var table in tables)
                        {
                            writer.WriteLine($"-- Table: {table}");
                            writer.WriteLine($"DROP TABLE IF EXISTS `{table}`;");
                            
                            // Get CREATE TABLE statement
                            string createTableQuery = $"SHOW CREATE TABLE `{table}`";
                            using (var cmd = new MySqlCommand(createTableQuery, conn))
                            using (var reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    writer.WriteLine(reader[1].ToString() + ";");
                                }
                            }
                            
                            writer.WriteLine();
                            
                            // Get table data
                            string selectQuery = $"SELECT * FROM `{table}`";
                            using (var cmd = new MySqlCommand(selectQuery, conn))
                            using (var reader = cmd.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    writer.WriteLine($"-- Data for table `{table}`");
                                    while (reader.Read())
                                    {
                                        var values = new List<string>();
                                        int fieldCount = (int)reader.FieldCount;
                                        for (int i = 0; i < fieldCount; i++)
                                        {
                                            if (reader.IsDBNull(i))
                                                values.Add("NULL");
                                            else
                                            {
                                                string value = reader[i].ToString();
                                                value = value.Replace("'", "''");
                                                value = value.Replace("\\", "\\\\");
                                                values.Add($"'{value}'");
                                            }
                                        }
                                        writer.WriteLine($"INSERT INTO `{table}` VALUES ({string.Join(", ", values)});");
                                    }
                                    writer.WriteLine();
                                }
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SQL script backup error: {ex.Message}");
                return false;
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
            return _settingsService.GetSetting(key, defaultValue);
        }
        private void SaveSetting(string key, string value, string category = "General")
        {
            _settingsService.SaveSetting(key, value, category);
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
                    string value = IDFormatter.ParseCurrency(kvp.Value.Text).ToString("F2");
                    SaveSetting($"FineRate_{kvp.Key}", value, "FineRates");
                }
                string lostCardValue = IDFormatter.ParseCurrency(txtLostCard.Text).ToString("F2");
                string maxFineValue = IDFormatter.ParseCurrency(txtMaxFine.Text).ToString("F2");
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
                using (var conn = _dbContext.GetConnection())
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
        private bool TestDatabaseConnection()
        {
            return DatabaseHelper.TestConnection(out _);
        }
    }
}