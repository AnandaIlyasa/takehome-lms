using Lms.Config;
using Lms.Model;

namespace Lms.IRepo;

internal interface IRoleRepo
{
    Role GetRoleByCode(string roleCode);
}
