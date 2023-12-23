namespace Lms.IRepo;

using Lms.Config;
using Lms.Model;

internal interface IUserRepo
{
    User? GetUserByEmailAndPassword(string email, string password);
    User CreateNewUser(User user);
    User GetUserByRole(string roleCode);
}
