using Lms.IService;
using Lms.Model;
using Lms.IRepo;

namespace Lms.Service;

internal class UserService : IUserService
{
    public IUserRepo UserRepo { private get; init; }

    public User CreateUser(User user)
    {
        var newUser = UserRepo.CreateNewUser(user);
        return newUser;
    }

    public User? Login(string email, string password)
    {
        User? user = UserRepo.GetUserByEmailAndPassword(email, password);
        return user;
    }
}
