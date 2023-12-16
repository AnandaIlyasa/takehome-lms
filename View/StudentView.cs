namespace Lms.View;

using Lms.Utils;

internal class StudentView
{
    public void MainMenu()
    {
        while (true)
        {
            Console.WriteLine("\n--- Student Page ---");
            Console.WriteLine("1. My Class List");
            Console.WriteLine("2. Enroll New Class");
            Console.WriteLine("3. Logout");
            var selectedOpt = Utils.GetNumberInputUtil(1, 3, "Select Class");

            if (selectedOpt == 1)
            {
                ShowEnrolledClassList();
            }
            else if (selectedOpt == 2)
            {
                ShowUnenrolledClassList();
            }
            else
            {
                break;
            }
        }
    }

    void ShowEnrolledClassList()
    {
        while (true)
        {
            Console.WriteLine("\n--- My Class List ---");
            Console.WriteLine("1. Java Class");
            Console.WriteLine("2. C# Class");
            Console.WriteLine("3. Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, 3, "Select Class");

            if (selectedOpt == 1)
            {
                ShowClassLearningList();
            }
            else
            {
                break;
            }
        }
    }

    void ShowUnenrolledClassList()
    {
        while (true)
        {
            Console.WriteLine("\n--- Available Class List ---");
            Console.WriteLine("1. Python Class");
            Console.WriteLine("2. Golang Class");
            Console.WriteLine("3. Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, 3, "Select Class to Enroll");

            if (selectedOpt == 1)
            {
                Console.WriteLine("\nYou successfully enrolled in Python Class");
            }
            else
            {
                break;
            }
        }
    }

    void ShowClassLearningList()
    {
        while (true)
        {
            Console.WriteLine("\nJava Class Learning List");
            Console.WriteLine("1. Learning-1 (2023-12-01)");
            Console.WriteLine("2. Learning-2 (2023-12-02)");
            Console.WriteLine("3. Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, 3, "Select Learning");

            if (selectedOpt == 1)
            {
                LearningMenu();
            }
            else
            {
                break;
            }
        }
    }

    void LearningMenu()
    {
        while (true)
        {
            Console.WriteLine("\nLearning-1 Session List");
            Console.WriteLine("1. Session-1 (11:00 - 11:00)");
            Console.WriteLine("2. Session-2 (12:00 - 12:00)");
            Console.WriteLine("3. Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, 3, "Select Session To Attend");

            if (selectedOpt == 1)
            {
                SessionMenu();
            }
            else
            {
                break;
            }
        }
    }

    void SessionMenu()
    {
        Console.WriteLine("\nSession-1 Menu");
        Console.WriteLine("1. Attend this session");
        Utils.GetNumberInputUtil(1, 1);
        Console.WriteLine("\nPlase wait for Teacher approval to be able to view Session-1 content");
        Thread.Sleep(5000);
        while (true)
        {
            Console.WriteLine("\nSession-1 Menu");
            Console.WriteLine("1. Material-1");
            Console.WriteLine("2. Material-2");
            Console.WriteLine("3. Task-1");
            Console.WriteLine("4. Task-2");
            Console.WriteLine("5. Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, 5);

            if (selectedOpt == 1 || selectedOpt == 2)
            {
                MaterialMenu();
            }
            else if (selectedOpt == 3 || selectedOpt == 4)
            {
                ShowTaskDetail();
            }
            else
            {
                break;
            }
        }
    }

    void MaterialMenu()
    {
        Console.WriteLine("\nMaterial-1 Detail");
        Console.WriteLine("Content: Please watch this tutorial http://youtube.com");
    }

    void ShowTaskDetail()
    {
        while (true)
        {
            Console.WriteLine("\nTask-1 Detail");
            Console.WriteLine("Jelaskan 5 prinsip SOLID");
            Console.WriteLine("1. Attach Submission");
            Console.WriteLine("2. Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, 2);

            if (selectedOpt == 1)
            {
                var fileName = Utils.GetStringInputUtil("File Name");
                var fileExtension = Utils.GetStringInputUtil("File Extension");

                Console.WriteLine("\nYou successfully submit Task-1 task");
            }
            else
            {
                break;
            }
        }
    }
}
