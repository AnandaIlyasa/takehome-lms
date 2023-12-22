using Lms.Config;
using Lms.Model;

namespace Lms.IRepo;

internal interface ISessionMaterialRepo
{
    List<SessionMaterial> GetMaterialListBySession(int sessionId);
}
