using Lms.Config;
using Lms.IRepo;
using Lms.Model;

namespace Lms.Repo;

internal class RoleRepo : IRoleRepo
{
    DBContextConfig _context { get; set; }

    public RoleRepo(DBContextConfig context)
    {
        _context = context;
    }

    public Role GetRoleByCode(string roleCode)
    {
        var role = _context.Roles.Where(r => r.RoleCode == roleCode).First();
        return role;
    }
}
