using Lms.Model;

namespace Lms.IService;

internal interface IUserService
{
    User? Login(string email, string password);
    User CreateUser(User user);
}
