namespace Lms.View;

using Lms.Utils;

internal class TeacherView
{
    public void MainMenu()
    {
        while (true)
        {
            Console.WriteLine("\n--- Teacher Page ---");
            Console.WriteLine("1. Java Class");
            Console.WriteLine("2. C# Class");
            Console.WriteLine("3. Logout");
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

    void ShowClassLearningList()
    {
        while (true)
        {
            Console.WriteLine("\nJava Class Learning List");
            Console.WriteLine("1. Learning-1 (2023-12-01)");
            Console.WriteLine("2. Learning-2 (2023-12-02)");
            Console.WriteLine("3. Create New Learning");
            Console.WriteLine("4. Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, 4, "Select Learning");

            if (selectedOpt == 1)
            {
                LearningMenu();
            }
            else if (selectedOpt == 3)
            {
                CreateNewLearning();
            }
            else
            {
                break;
            }
        }
    }

    void CreateNewLearning()
    {
        var dateFormat = "yyyy-MM-dd";
        var learningTitle = Utils.GetStringInputUtil("Learning Title");
        var learningDescription = Utils.GetStringInputUtil("Learning Description");
        var learningDate = Utils.GetDateTimeInputUtil("Learning Date", dateFormat);

        Console.WriteLine($"\nNew learning {learningTitle} on {learningDate.ToString(dateFormat)} successfully created");
    }

    void LearningMenu()
    {
        while (true)
        {
            Console.WriteLine("\nLearning-1 Session List");
            Console.WriteLine("1. Session-1 (11:00 - 11:00)");
            Console.WriteLine("2. Session-2 (12:00 - 12:00)");
            Console.WriteLine("3. Create New Session");
            Console.WriteLine("4. Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, 4, "Select Session");

            if (selectedOpt == 1)
            {
                SessionMenu();
            }
            else if (selectedOpt == 3)
            {
                CreateNewSession();
            }
            else
            {
                break;
            }
        }
    }

    void CreateNewSession()
    {
        var timeFormat = "HH:mm";
        var sessionTitle = Utils.GetStringInputUtil("Session Title");
        var sessionDescription = Utils.GetStringInputUtil("Session Description");
        var sessionStart = Utils.GetDateTimeInputUtil("Session Start Time", timeFormat);
        var sessionEnd = Utils.GetDateTimeInputUtil("Session End Time", timeFormat);

        Console.WriteLine($"\nNew session {sessionTitle} ({sessionStart.ToString(timeFormat)} - {sessionEnd.ToString(timeFormat)}) successfully created");
    }

    void SessionMenu()
    {
        while (true)
        {
            Console.WriteLine("\nSession-1 Menu");
            Console.WriteLine("1. Material");
            Console.WriteLine("2. Task");
            Console.WriteLine("3. Attendance List");
            Console.WriteLine("4. Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, 4);

            if (selectedOpt == 1)
            {
                MaterialMenu();
            }
            else if (selectedOpt == 2)
            {
                TaskMenu();
            }
            else if (selectedOpt == 3)
            {
                ShowAttendanceList();
            }
            else
            {
                break;
            }
        }
    }

    void MaterialMenu()
    {
        while (true)
        {
            Console.WriteLine("\nSession-1 Material");
            Console.WriteLine("1. Material-1");
            Console.WriteLine("2. Add Material");
            Console.WriteLine("3. Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, 3);

            if (selectedOpt == 2)
            {
                Console.WriteLine("\nSelect Material Type");
                Console.WriteLine("1. File");
                Console.WriteLine("2. Text");
                var selectedType = Utils.GetNumberInputUtil(1, 2);
                if (selectedType == 1)
                {
                    var fileName = Utils.GetStringInputUtil("File Name");
                    var fileExtenstion = Utils.GetStringInputUtil("File Extension");
                }
                else
                {
                    var materialText = Utils.GetStringInputUtil("Material");
                }
                Console.WriteLine("\nNew material successfully created");
            }
            else
            {
                break;
            }
        }
    }

    void TaskMenu()
    {
        while (true)
        {
            Console.WriteLine("\nSession-1 Task");
            Console.WriteLine("1. Task-1");
            Console.WriteLine("2. Add Task");
            Console.WriteLine("3. Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, 3);

            if (selectedOpt == 1)
            {
                ShowTaskDetail();
            }
            else if (selectedOpt == 2)
            {
                Console.WriteLine("\nSelect Task Type");
                Console.WriteLine("1. File");
                Console.WriteLine("2. Essay");
                Console.WriteLine("3. Multiple Choice");
                var selectedType = Utils.GetNumberInputUtil(1, 3);
                if (selectedType == 1)
                {
                    var fileName = Utils.GetStringInputUtil("File Name");
                    var fileExtenstion = Utils.GetStringInputUtil("File Extension");
                }
                else if (selectedType == 2)
                {
                    var essayQuestion = Utils.GetStringInputUtil("Essay Question");
                }
                else
                {
                    var multipleChoiceQuestion = Utils.GetStringInputUtil("Multiple Choice Question");
                }
                Console.WriteLine("\nNew task successfully created");
            }
            else
            {
                break;
            }
        }
    }

    void ShowTaskDetail()
    {
        while (true)
        {
            Console.WriteLine("\nTask-1 Detail");
            Console.WriteLine("Jelaskan 5 prinsip SOLID");
            Console.WriteLine("Submission List:");
            Console.WriteLine("1. Andi (2023-12-12 10:10)");
            Console.WriteLine("2. Budi (2023-12-12 10:10)");
            Console.WriteLine("3. Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, 3);

            if (selectedOpt == 3)
            {
                break;
            }
        }
    }

    void ShowAttendanceList()
    {
        while (true)
        {
            Console.WriteLine("\nAttendance List :");
            Console.WriteLine("1. Andi (Approved)");
            Console.WriteLine("2. Budi (Need Approval)");
            Console.WriteLine("3. Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, 3, "Select Student to Approve");

            if (selectedOpt == 3)
            {
                break;
            }
            else
            {
                Console.WriteLine("\nYou have approved Budi");
            }
        }
    }
}
