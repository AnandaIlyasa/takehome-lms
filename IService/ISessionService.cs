using Lms.Model;

namespace Lms.IService;

internal interface ISessionService
{
    SessionAttendance? GetStudentAttendanceStatus(int sessionId);
    SessionAttendance AttendSession(int sessionId);
    Session GetSessionById(int sessionId);
}
