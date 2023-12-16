namespace Lms.View;

using Lms.Utils;

internal class SuperAdminView
{
    public void MainMenu()
    {
        while (true)
        {
            Console.WriteLine("\n--- Super Admin Page ---");
            Console.WriteLine("1. Register New Teacher");
            Console.WriteLine("2. Create New Class");
            Console.WriteLine("3. Logout");
            var selectedOpt = Utils.GetNumberInputUtil(1, 3);

            if (selectedOpt == 1)
            {
                RegisterNewTeacher();
            }
            else if (selectedOpt == 2)
            {
                CreateNewClass();
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

        Console.WriteLine($"\nNew teacher {fullName} with email {email} successfully created");
    }

    void CreateNewClass()
    {
        var classCode = Utils.GetStringInputUtil("Class Code");
        var className = Utils.GetStringInputUtil("Class Name");
        var classDescription = Utils.GetStringInputUtil("Class Description");
        var classImage = Utils.GetStringInputUtil("Class Image Filename");
        var classImageExtenstion = Utils.GetStringInputUtil("Class Image File Extension");

        Console.WriteLine("Select Teacher");
        Console.WriteLine("1. Andi");
        Console.WriteLine("2. Budi");
        var teacher = Utils.GetNumberInputUtil(1, 2);

        Console.WriteLine($"\nNew class {className} with teacher Andi successfully created");
    }
}
