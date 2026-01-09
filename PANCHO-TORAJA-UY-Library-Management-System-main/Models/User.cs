using System;
using System.Linq;

namespace Project5LMS.Models
{
    public class User
    {
        public int UserID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Username 
        { 
            get { return Email; } 
            set { Email = value; } 
        }

        public string PasswordHash { get; set; }

        public string Role { get; set; }

        public string FullName => string.Join(" ", new[] { FirstName, LastName }.Where(s => !string.IsNullOrWhiteSpace(s)));
    }
}
