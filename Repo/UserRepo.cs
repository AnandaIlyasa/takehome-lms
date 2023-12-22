using Lms.Config;
using Lms.Helper;
using Lms.IRepo;
using Lms.Model;
using Microsoft.EntityFrameworkCore;

namespace Lms.Repo;

internal class UserRepo : IUserRepo
{
    readonly DBContextConfig _context;

    public UserRepo(DBContextConfig context)
    {
        _context = context;
    }

    public User CreateNewUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }

    public User? GetUserByEmailAndPassword(string email, string password)
    {
        var user = _context.Users
                .Where(u => u.Email == email && EF.Functions.Like(u.Pass, password) && u.IsActive == true)
                .Include(u => u.Role)
                .FirstOrDefault();
        return user;
    }
}
