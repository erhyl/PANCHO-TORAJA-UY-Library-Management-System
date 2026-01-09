using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Project5LMS.Helpers;

namespace Project5LMS.Helpers
{
    public static class AuditLogger
    {
        private static readonly string LogDirectory = Path.Combine(Application.StartupPath, "Logs");
        private static readonly string AuditLogFile = Path.Combine(LogDirectory, $"Audit_{DateTime.Now:yyyyMMdd}.log");

        static AuditLogger()
        {
            if (!Directory.Exists(LogDirectory))
            {
                Directory.CreateDirectory(LogDirectory);
            }
        }

        public static void Log(string action, string details = "", string result = "Success")
        {
            try
            {
                string logEntry = FormatLogEntry(action, details, result);
                WriteToFile(logEntry);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Audit logging error: {ex.Message}");
            }
        }

        public static void LogSecurity(string action, string details = "", string result = "Success")
        {
            Log($"SECURITY: {action}", details, result);
        }

        public static void LogDataModification(string action, string details = "", string result = "Success")
        {
            Log($"DATA_MODIFICATION: {action}", details, result);
        }

        public static void LogCirculation(string action, string details = "", string result = "Success")
        {
            Log($"CIRCULATION: {action}", details, result);
        }

        public static void LogAccessControl(string action, string details = "", string result = "Success")
        {
            Log($"ACCESS_CONTROL: {action}", details, result);
        }

        private static string FormatLogEntry(string action, string details, string result)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ");
            sb.Append($"User: {CurrentUser.Email ?? "Unknown"} (ID: {CurrentUser.UserID}, Role: {CurrentUser.Role ?? "Unknown"}) | ");
            sb.Append($"Action: {action} | ");
            if (!string.IsNullOrWhiteSpace(details))
            {
                sb.Append($"Details: {details} | ");
            }
            sb.Append($"Result: {result}");
            return sb.ToString();
        }

        private static void WriteToFile(string logEntry)
        {
            lock (typeof(AuditLogger))
            {
                using (StreamWriter writer = new StreamWriter(AuditLogFile, true, Encoding.UTF8))
                {
                    writer.WriteLine(logEntry);
                }
            }
        }

        public static string GetTodayLogPath()
        {
            return AuditLogFile;
        }
    }
}
