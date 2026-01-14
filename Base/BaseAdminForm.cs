using System;
using System.ComponentModel;
using System.Windows.Forms;
using Project5LMS.Helpers;
namespace Project5LMS.Base
{
    public abstract class BaseAdminForm : BaseForm
    {
        protected BaseAdminForm()
        {
            // Prevent designer from trying to instantiate this abstract class
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;
            
            ValidateAccess();
        }
        protected override void ValidateAccess()
        {
            try
            {
                AccessControlHelper.RequireRole("Admin");
                AuditLogger.LogAccessControl($"{GetType().Name} accessed", $"User: {CurrentUser.Email}", "Success");
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                AuditLogger.LogAccessControl($"{GetType().Name} access denied", $"User: {CurrentUser.Email}", "Failed");
                this.Close();
            }
        }
        protected override string[] GetRequiredRoles()
        {
            return new[] { "Admin" };
        }
    }
}