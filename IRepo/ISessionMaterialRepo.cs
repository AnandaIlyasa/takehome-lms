using Lms.Model;

namespace Lms.IRepo;

internal interface ISessionMaterialRepo
{
    List<SessionMaterial> GetSessionMaterialListBySession(int sessionId);
}
