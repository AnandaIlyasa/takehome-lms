using Lms.Config;
using Lms.Helper;
using Lms.IRepo;
using Lms.Model;

namespace Lms.Repo;

internal class SessionRepo : ISessionRepo
{
    readonly DBContextConfig _context;

    public SessionRepo(DBContextConfig context)
    {
        _context = context;
    }

    public Session GetSessionById(int sessionId)
    {
        var session = _context.Sessions
                    .Where(s => s.Id == sessionId)
                    .First();
        return session;
    }

    public List<Session> GetSessionListByLearning(int learningId)
    {
        var sessionList = _context.Sessions
                        .Where(s => s.LearningId == learningId)
                        .ToList();
        return sessionList;
    }
}
