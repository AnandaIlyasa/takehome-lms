namespace Lms.View;

using Lms.IService;
using Lms.Model;
using Lms.Utils;

internal class StudentView
{
    public IClassService ClassService { private get; init; }
    User _studentUser;

    public void MainMenu(User user)
    {
        _studentUser = user;

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
            var classList = ClassService.GetEnrolledClassList(_studentUser.Id);

            Console.WriteLine("\n--- My Class List ---");
            var number = 1;
            foreach (var studentClass in classList)
            {
                Console.WriteLine($"{number}. {studentClass.ClassTitle} - {studentClass.Teacher.FullName}");
                number++;
            }
            Console.WriteLine(number + ". Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, number, "Select Class");

            if (selectedOpt == number)
            {
                break;
            }
            else
            {
                ShowClassLearningList(classList[selectedOpt - 1].LearningList);
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

    void ShowClassLearningList(List<Learning> learningList)
    {
        while (true)
        {
            Console.WriteLine("\nJava Class Learning List");
            var number = 1;
            foreach (var learning in learningList)
            {
                Console.WriteLine($"{number}. {learning.LearningName} ({learning.LearningDate})");
                number++;
            }
            Console.WriteLine(number + ". Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, number, "Select Learning");

            if (selectedOpt == number)
            {
                break;
            }
            else
            {
                LearningMenu(learningList[selectedOpt - 1].SessionList);
            }
        }
    }

    void LearningMenu(List<Session> sessionList)
    {
        while (true)
        {
            Console.WriteLine("\nLearning-1 Session List");
            var number = 1;
            foreach (var session in sessionList)
            {
                Console.WriteLine($"{number}. {session.SessionName} ({session.StartTime} - {session.EndTime})");
                number++;
            }
            Console.WriteLine(number + ". Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, number, "Select Session To Attend");

            if (selectedOpt == number)
            {
                break;
            }
            else
            {
                SessionMenu();
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
