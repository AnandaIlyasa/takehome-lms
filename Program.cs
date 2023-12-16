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

        var userService = new UserService() { UserRepo = userRepo };

        var superadminView = new SuperAdminView();
        var teacherView = new TeacherView();
        var studentView = new StudentView();
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
