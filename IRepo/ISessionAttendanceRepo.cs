namespace Lms.IRepo;

using Lms.Config;
using Lms.Model;

internal interface ISessionAttendanceRepo
{
    SessionAttendance? GetSessionAttendanceStatus(int sessionId, int studentId);
    SessionAttendance CreateNewSessionAttendance(SessionAttendance sessionAttendance);
    List<SessionAttendance> GetSessionAttendanceList(int sessionId);
    int UpdateSessionAttendance(SessionAttendance sessionAttendance);
}
