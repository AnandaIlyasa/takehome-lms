using Lms.Model;

namespace Lms.IService;

internal interface ISessionService
{
    SessionAttendance? GetSessionAttendanceStatusByStudent(SessionAttendance sessionAttendance);
    SessionAttendance AttendSession(SessionAttendance sessionAttendance);
    Session GetSessionById(int sessionId);
}
