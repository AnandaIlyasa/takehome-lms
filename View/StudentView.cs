namespace Lms.View;

using Lms.IService;
using Lms.Model;
using Lms.Utils;

internal class StudentView
{
    public IClassService ClassService { private get; init; }
    public ISessionService SessionService { private get; init; }
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
                SessionMenu(sessionList[selectedOpt - 1]);
            }
        }
    }

    void SessionMenu(Session session)
    {
        var sessionAttendance = new SessionAttendance()
        {
            Student = _studentUser,
            Session = session,
            IsApproved = false,
        };
        var attendance = SessionService.GetSessionAttendanceStatusByStudent(sessionAttendance);
        if (attendance == null)
        {
            Console.WriteLine($"\n{session.SessionName} ({session.StartTime} - {session.EndTime})");
            Console.WriteLine("1. Attend this session");
            Utils.GetNumberInputUtil(1, 1);
            SessionService.AttendSession(sessionAttendance);
            Console.WriteLine("\nAttend success!");
            Console.WriteLine("Plase wait for Teacher approval to be able to view " + session.SessionName + " content");
        }
        else if (attendance.IsApproved == false)
        {
            Console.WriteLine($"\n{session.SessionName} ({session.StartTime} - {session.EndTime})");
            Console.WriteLine("\nPlase wait for Teacher approval to be able to view " + session.SessionName + " content");
        }
        else
        {
            var sessionDetail = SessionService.GetSessionById(session.Id);
            while (true)
            {
                Console.WriteLine($"\n{session.SessionName} ({session.StartTime} - {session.EndTime})");
                Console.WriteLine("Description : " + sessionDetail.SessionDescription);

                var number = 1;
                foreach (var material in sessionDetail.MaterialList)
                {
                    Console.WriteLine($"{number}. {material.MaterialName}");
                    number++;
                }
                foreach (var task in sessionDetail.TaskList)
                {
                    Console.WriteLine($"{number}. {task.TaskName}");
                    number++;
                }
                Console.WriteLine(number + ". Back");
                var selectedOpt = Utils.GetNumberInputUtil(1, number);

                if (selectedOpt == number)
                {
                    //MaterialMenu(sessionDetail.MaterialList[selectedOpt - 1]);
                    break;
                }
                //else if (selectedOpt == 3 || selectedOpt == 4)
                //{
                //    ShowTaskDetail();
                //}
                //else
                //{
                //    break;
                //}
            }
        }

    }

    void MaterialMenu(SessionMaterial material)
    {
        Console.WriteLine("\n" + material.MaterialName + " Detail");
        Console.WriteLine("Description: " + material.MaterialDescription);
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
