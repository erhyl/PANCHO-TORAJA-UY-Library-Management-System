using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project5LMS.Forms.Dashboard;

namespace Project5LMS
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // TEMPORARY: Skip login and go directly to AdminMainForm
            Application.Run(new AdminMainForm());
            // Original: Application.Run(new LoginForm());
        }
    }
}
