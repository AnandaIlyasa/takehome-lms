namespace Lms.View;

using Lms.Utils;
using Lms.Constant;
using Lms.IService;
using Lms.Model;

internal class MainView
{
    public SuperAdminView SuperAdminView { private get; init; }
    public TeacherView TeacherView { private get; init; }
    public StudentView StudentView { private get; init; }
    public IUserService UserService { private get; init; }

    public void MainMenu()
    {
        while (true)
        {
            Console.WriteLine("\n==== Learning Management System ====");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register As Student");
            var selectedOpt = Utils.GetNumberInputUtil(1, 2);

            if (selectedOpt == 1)
            {
                Login();
            }
            else
            {
                Register();
            }
        }
    }

    void Login()
    {
        Console.WriteLine("\n---- Login Page ----");
        var email = Utils.GetStringInputUtil("Email");
        var password = Utils.GetStringInputUtil("Password");

        var user = UserService.Login(email, password);
        while (user == null)
        {
            Console.WriteLine("\nCredential is wrong!\n");

            email = Utils.GetStringInputUtil("Email");
            password = Utils.GetStringInputUtil("Password");
            user = UserService.Login(email, password);
        }

        if (user.Role.RoleCode == UserRole.SuperAdmin)
        {
            SuperAdminView.MainMenu();
        }
        else if (user.Role.RoleCode == UserRole.Teacher)
        {
            TeacherView.MainMenu();
        }
        else if (user.Role.RoleCode == UserRole.Student)
        {
            StudentView.MainMenu(user);
        }
        else
        {
            Console.WriteLine("\nThis user's role is not valid");
        }
    }

    void Register()
    {
        Console.WriteLine("\n---- Student Register Page ----");
        var fullName = Utils.GetStringInputUtil("Full Name");
        var email = Utils.GetStringInputUtil("Email");
        var password = Utils.GetStringInputUtil("Password");
        var confirmPassword = Utils.GetStringInputUtil("Confirm Password");

        if (password != confirmPassword)
        {
            Console.WriteLine("\nPassword doesn't match. Register failed\n");
            Register();
        }
        else
        {
            var student = new User()
            {
                FullName = fullName,
                Email = email,
                Pass = password,
                Role = new Role() { Id = 2 },
            };
            UserService.CreateUser(student);
            Console.WriteLine($"\nNew student account for {fullName} with email {email} successfully created");
        }
    }
}
