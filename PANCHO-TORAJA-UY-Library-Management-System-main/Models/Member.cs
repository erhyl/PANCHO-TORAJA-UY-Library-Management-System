using System;
using System.Linq;

namespace Project5LMS.Models
{

    public class Member
    {

        public int MemberID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Status { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }

        public string FullName => string.Join(" ", new[] { FirstName, LastName }.Where(s => !string.IsNullOrWhiteSpace(s)));

        public bool IsActive => Status?.Equals("Active", StringComparison.OrdinalIgnoreCase) == true;

        public bool IsExpired => DateTime.Now > ExpirationDate;

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(FirstName) &&
                   !string.IsNullOrWhiteSpace(LastName) &&
                   !string.IsNullOrWhiteSpace(Email) &&
                   RegistrationDate <= ExpirationDate;
        }

        public static Member Create(string firstName, string lastName, string email, string type)
        {
            var member = new Member
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Type = type,
                RegistrationDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddYears(1),
                Status = "Active"
            };

            if (!member.IsValid())
            {
                throw new ArgumentException("Invalid member data");
            }

            return member;
        }
    }
}
