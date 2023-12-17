namespace Lms.IRepo;

using Lms.Model;

internal interface ISessionAttendanceRepo
{
    SessionAttendance GetSessionAttendanceStatus(SessionAttendance sessionAttendance);
    SessionAttendance CreateNewSessionAttendance(SessionAttendance sessionAttendance);
}
