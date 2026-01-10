using Project5LMS.Models;
namespace Project5LMS.Interfaces
{
    public interface IAuthenticationService
    {
        User Login(string email, string password);
    }
}