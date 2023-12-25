namespace Lms.View;

using Lms.Utils;
using Lms.Model;
using Lms.IService;

internal class SuperAdminView
{
    readonly IUserService _userService;
    readonly IClassService _classService;

    public SuperAdminView(IUserService userService, IClassService classService)
    {
        _userService = userService;
        _classService = classService;
    }

    public void MainMenu(User superadmin)
    {
        while (true)
        {
            Console.WriteLine("\n--- Super Admin Page - hello, " + superadmin.FullName + " ----");
            Console.WriteLine("1. Register New Teacher");
            Console.WriteLine("2. Show All Classes");
            Console.WriteLine("3. Logout");
            var selectedOpt = Utils.GetNumberInputUtil(1, 3);

            if (selectedOpt == 1)
            {
                RegisterNewTeacher();
            }
            else if (selectedOpt == 2)
            {
                ShowAllClassList();
            }
            else
            {
                break;
            }
        }
    }

    void RegisterNewTeacher()
    {
        var fullName = Utils.GetStringInputUtil("Full Name");
        var email = Utils.GetStringInputUtil("Email");
        var teacher = new User()
        {
            FullName = fullName,
            Email = email,
        };
        _userService.CreateNewTeacher(teacher);

        Console.WriteLine($"\nNew teacher {fullName} with email {email} successfully created");
    }

    void ShowAllClassList()
    {
        while (true)
        {
            Console.WriteLine("\nAll Classes");
            var allClassList = _classService.GetAllClassList();
            var number = 1;
            foreach (var item in allClassList)
            {
                Console.WriteLine($"{number}. {item.ClassTitle} - {item.Teacher.FullName}");
                number++;
            }

            Console.WriteLine("\nMenu");
            Console.WriteLine("1. Create New Class");
            Console.WriteLine("2. Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, 2);

            if (selectedOpt == 1)
            {
                CreateNewClass();
            }
            else
            {
                break;
            }
        }
    }

    void CreateNewClass()
    {
        var classCode = Utils.GetStringInputUtil("Class Code");
        var className = Utils.GetStringInputUtil("Class Name");
        var classDescription = Utils.GetStringInputUtil("Class Description");
        var classImage = Utils.GetStringInputUtil("Class Image Filename");
        var classImageExtenstion = Utils.GetStringInputUtil("Class Image File Extension");

        var teacherList = _userService.GetTeacherList();
        Console.WriteLine("Select Teacher");
        var number = 1;
        foreach (var teacher in teacherList)
        {
            Console.WriteLine($"{number}. {teacher.FullName}");
            number++;
        }
        var selectedOpt = Utils.GetNumberInputUtil(1, number);

        var newClass = new Class()
        {
            TeacherId = teacherList[selectedOpt - 1].Id,
            ClassCode = classCode,
            ClassTitle = className,
            ClassDescription = classDescription,
            ClassImage = new LMSFile()
            {
                FileContent = classImage,
                FileExtension = classImageExtenstion,
            },
        };

        _classService.CreateNewClass(newClass);

        Console.WriteLine($"\nNew class {className} with teacher {teacherList[selectedOpt - 1].FullName} successfully created");
    }
}
