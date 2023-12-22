using Lms.Model;

namespace Lms.IRepo;

internal interface ISessionMaterialFileRepo
{
    List<SessionMaterialFile> GetSessionMaterialFileListByMaterial(int materialId);
}
