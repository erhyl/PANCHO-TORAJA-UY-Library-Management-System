using Project5LMS.Data;
using Project5LMS.Helpers;
using Project5LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project5LMS.Services
{
    public class AuthenticationService
    {
        private readonly DatabaseContext _db = new DatabaseContext();

        public User Login(string username, string password)
        {
            string query = $"SELECT * FROM Users WHERE Username='{username}'";
            var dt = _db.ExecuteQuery(query);
            if (dt.Rows.Count == 1)
            {
                string hash = dt.Rows[0]["PasswordHash"].ToString();
                if (PasswordHasher.Verify(password, hash))
                {
                    return new User
                    {
                        UserID = Convert.ToInt32(dt.Rows[0]["UserID"]),
                        FullName = dt.Rows[0]["FullName"].ToString(),
                        Username = dt.Rows[0]["Username"].ToString(),
                        Role = dt.Rows[0]["Role"].ToString()
                    };
                }
            }
            return null;
        }
    }
}
