using Project5LMS.Models;
namespace Project5LMS.Interfaces
{
    public interface IUserService
    {
        User Login(string email, string password);
    }
}