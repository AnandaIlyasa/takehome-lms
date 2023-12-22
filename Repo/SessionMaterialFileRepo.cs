using Lms.Config;
using Lms.IRepo;
using Lms.Model;
using Microsoft.EntityFrameworkCore;

namespace Lms.Repo;

internal class SessionMaterialFileRepo : ISessionMaterialFileRepo
{
    readonly DBContextConfig _context;

    public SessionMaterialFileRepo(DBContextConfig context)
    {
        _context = context;
    }

    public List<SessionMaterialFile> GetSessionMaterialFileListByMaterial(int materialId)
    {
        var materialFileList = _context.SessionMaterialFiles
                                .Where(mf => mf.MaterialId == materialId)
                                .Include(mf => mf.File)
                                .ToList();
        return materialFileList;
    }
}
