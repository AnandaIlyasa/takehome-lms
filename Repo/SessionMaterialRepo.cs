using Lms.Config;
using Lms.Helper;
using Lms.IRepo;
using Lms.Model;

namespace Lms.Repo;

internal class SessionMaterialRepo : ISessionMaterialRepo
{
    public DatabaseHelper DBHelper { private get; init; }
    readonly DBContextConfig _context;

    public SessionMaterialRepo(DBContextConfig context)
    {
        _context = context;
    }

    public List<SessionMaterial> GetMaterialListBySession(int sessionId)
    {
        var materialList = _context.SessionMaterials
                        .Where(m => m.SessionId == sessionId)
                        .ToList();
        return materialList;
    }
}
