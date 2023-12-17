using Lms.Service;
using Lms.Helper;
using Lms.Repo;
using Lms.View;

namespace Lms;

internal class Program
{
    static void Main()
    {
        var dbHelper = new DatabaseHelper();

        var userRepo = new UserRepo() { DBHelper = dbHelper };
        var classRepo = new ClassRepo() { DBHelper = dbHelper };
        var sessionAttendanceRepo = new SessionAttendanceRepo() { DBHelper = dbHelper };
        var sessionRepo = new SessionRepo() { DBHelper = dbHelper };
        var sessionMaterialRepo = new SessionMaterialRepo() { DBHelper = dbHelper };
        var sessionTaskRepo = new SessionTaskRepo() { DBHelper = dbHelper };

        var userService = new UserService() { UserRepo = userRepo };
        var classService = new ClassService() { ClassRepo = classRepo };
        var sessionService = new SessionService()
        {
            SessionAttendanceRepo = sessionAttendanceRepo,
            SessionRepo = sessionRepo,
            SessionMaterialRepo = sessionMaterialRepo,
            SessionTaskRepo = sessionTaskRepo,
        };

        var superadminView = new SuperAdminView();
        var teacherView = new TeacherView();
        var studentView = new StudentView()
        {
            ClassService = classService,
            SessionService = sessionService,
        };
        var mainView = new MainView()
        {
            UserService = userService,
            SuperAdminView = superadminView,
            TeacherView = teacherView,
            StudentView = studentView,
        };

        mainView.MainMenu();
    }
}
