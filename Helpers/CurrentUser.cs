using Project5LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project5LMS.Helpers
{
    public static class CurrentUser
    {
        public static int UserID { get; set; }
        public static string FirstName { get; set; }
        public static string LastName { get; set; }
        public static string Role { get; set; }

        // Read-only full name for compatibility. Prefer using FirstName/LastName.
        public static string FullName => string.Join(" ", new[] { FirstName, LastName }.Where(s => !string.IsNullOrWhiteSpace(s)));

        public static void Set(User user)
        {
            UserID = user.UserID;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Role = user.Role;
        }

        public static void Clear()
        {
            UserID = 0;
            FirstName = null;
            LastName = null;
            Role = null;
        }
    }
}
