using Lms.IRepo;
using Lms.IService;
using Lms.Model;

namespace Lms.Service;

internal class SessionService : ISessionService
{
    public ISessionAttendanceRepo SessionAttendanceRepo { private get; init; }
    public ISessionRepo SessionRepo { private get; init; }
    public ISessionMaterialRepo SessionMaterialRepo { private get; init; }
    public ISessionTaskRepo SessionTaskRepo { private get; init; }

    public SessionAttendance AttendSession(SessionAttendance sessionAttendance)
    {
        sessionAttendance = SessionAttendanceRepo.CreateNewSessionAttendance(sessionAttendance);
        return sessionAttendance;
    }

    public SessionAttendance? GetSessionAttendanceStatusByStudent(SessionAttendance sessionAttendance)
    {
        sessionAttendance = SessionAttendanceRepo.GetSessionAttendanceStatus(sessionAttendance);
        return sessionAttendance;
    }

    public Session GetSessionById(int sessionId)
    {
        var session = SessionRepo.GetSessionById(sessionId);
        var materialList = SessionMaterialRepo.GetSessionMaterialListBySession(sessionId);
        session.MaterialList = materialList;
        var taskList = SessionTaskRepo.GetSessionTaskListBySession(sessionId);
        session.TaskList = taskList;
        return session;
    }
}
