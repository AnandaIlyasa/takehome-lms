using Lms.IService;
using Lms.Model;
using Lms.IRepo;
using Lms.Helper;
using Lms.Config;
using Lms.Constant;

namespace Lms.Service;

internal class UserService : IUserService
{
    readonly IUserRepo _userRepo;
    readonly IRoleRepo _roleRepo;
    readonly SessionHelper _sessionHelper;

    public UserService(IUserRepo userRepo, IRoleRepo roleRepo, SessionHelper sessionHelper)
    {
        _userRepo = userRepo;
        _sessionHelper = sessionHelper;
        _roleRepo = roleRepo;
    }

    public User CreateNewStudent(User user)
    {
        var role = _roleRepo.GetRoleByCode(RoleCode.Student);
        user.RoleId = role.Id;

        var systemUser = _userRepo.GetUserByRole(RoleCode.System);
        user.CreatedBy = systemUser.Id;
        user.CreatedAt = DateTime.Now;
        user = _userRepo.CreateNewUser(user);
        return user;
    }

    public User? Login(string email, string password)
    {
        var user = _userRepo.GetUserByEmailAndPassword(email, password);
        if (user != null)
        {
            _sessionHelper.UserId = user.Id;
        }
        return user;
    }
}
