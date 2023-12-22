using Lms.Config;
using Lms.Model;

namespace Lms.IRepo;

internal interface ISessionRepo
{
    Session GetSessionById(int sessionId);
    List<Session> GetSessionListByLearning(int learningId);
}
