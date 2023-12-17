using Lms.Model;

namespace Lms.IRepo;

internal interface ISessionRepo
{
    Session GetSessionById(int sessionId);
}
