using Lms.Config;
using Lms.Helper;
using Lms.IRepo;
using Lms.Model;
using Microsoft.EntityFrameworkCore;

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

    public List<SessionAttendance> GetSessionAttendanceList(int sessionId)
    {
        var attendanceList = _context.SessionAttendances
                                    .Where(sa => sa.SessionId == sessionId)
                                    .Include(sa => sa.Student)
                                    .OrderBy(sa => sa.CreatedAt)
                                    .ToList();
        return attendanceList;
    }

    public SessionAttendance? GetSessionAttendanceStatus(int sessionId, int studentId)
    {
        var sessionAttendance = _context.SessionAttendances
                                .Where(sa => sa.StudentId == studentId && sa.SessionId == sessionId)
                                .FirstOrDefault();
        return sessionAttendance;
    }

    public int UpdateSessionAttendance(SessionAttendance sessionAttendance)
    {
        var attendance = _context.SessionAttendances
                        .Where(sa => sa.Id == sessionAttendance.Id)
                        .First();
        attendance.IsApproved = !sessionAttendance.IsApproved;
        return _context.SaveChanges();
    }
}
