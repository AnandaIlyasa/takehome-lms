using Lms.Config;
using Lms.Helper;
using Lms.IRepo;
using Lms.Model;

namespace Lms.Repo;

internal class SessionAttendanceRepo : ISessionAttendanceRepo
{
    readonly DBContextConfig _context;

    public SessionAttendanceRepo(DBContextConfig context)
    {
        _context = context;
    }

    public SessionAttendance CreateNewSessionAttendance(SessionAttendance sessionAttendance)
    {
        _context.SessionAttendances.Add(sessionAttendance);
        _context.SaveChanges();
        return sessionAttendance;
    }

    public SessionAttendance? GetSessionAttendanceStatus(int sessionId, int studentId)
    {
        var sessionAttendance = _context.SessionAttendances
                                .Where(sa => sa.StudentId == studentId && sa.SessionId == sessionId)
                                .FirstOrDefault();
        return sessionAttendance;
    }
}
