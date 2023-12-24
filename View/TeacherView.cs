namespace Lms.View;

using Lms.IService;
using Lms.Model;
using Lms.Utils;
using System.Diagnostics;

internal class TeacherView : StudentTeacherBaseView
{
    readonly IClassService _classService;
    readonly ISessionService _sessionService;
    readonly IForumService _forumService;
    readonly ITaskSubmissionService _taskSubmissionService;
    User _teacherUser;

    public TeacherView(IClassService classService, ISessionService sessionService, IForumService forumService, ITaskSubmissionService taskSubmissionService)
    {
        _classService = classService;
        _sessionService = sessionService;
        _forumService = forumService;
        _taskSubmissionService = taskSubmissionService;
    }

    public void MainMenu(User user)
    {
        _teacherUser = user;

        while (true)
        {
            Console.WriteLine("\n--- Teacher Page - hello, " + _teacherUser.FullName + " ----");
            var classList = _classService.GetClassListByTeacher();
            var number = 1;
            foreach (var cl in classList)
            {
                Console.WriteLine($"{number}. {cl.ClassTitle}");
                number++;
            }

            Console.WriteLine(number + ". Logout");
            var selectedOpt = Utils.GetNumberInputUtil(1, number, "Select Class");

            if (selectedOpt == number)
            {
                break;
            }
            else
            {
                ShowClassLearningList(classList[selectedOpt - 1]);
            }
        }
    }

    void ShowClassLearningList(Class selectedClass)
    {
        while (true)
        {
            Console.WriteLine("\n" + selectedClass.ClassTitle + " Class Learning List");
            var number = 1;
            foreach (var learning in selectedClass.LearningList)
            {
                Console.WriteLine($"{number}. {learning.LearningName} ({learning.LearningDate.ToString(DateFormat)})");
                number++;
            }
            Console.WriteLine(number++ + ". Create New Learning");
            Console.WriteLine(number + ". Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, number, "Select Learning");

            if (selectedOpt == number)
            {
                break;
            }
            else if (selectedOpt == number - 1)
            {
                CreateNewLearning();
            }
            else
            {
                LearningMenu(selectedClass.LearningList[selectedOpt - 1]);
            }
        }
    }

    void CreateNewLearning()
    {
        var learningTitle = Utils.GetStringInputUtil("Learning Title");
        var learningDescription = Utils.GetStringInputUtil("Learning Description");
        var learningDate = Utils.GetDateTimeInputUtil("Learning Date", DateFormat);

        Console.WriteLine($"\nNew learning {learningTitle} on {learningDate.ToString(DateFormat)} successfully created");
    }

    void LearningMenu(Learning learning)
    {
        while (true)
        {
            Console.WriteLine($"\n{learning.LearningName} ({learning.LearningDate.ToString(DateFormat)}) Session List");
            var number = 1;
            foreach (var session in learning.SessionList)
            {
                Console.WriteLine($"{number}. {session.SessionName} ({session.StartTime} - {session.EndTime})");
                number++;
            }
            Console.WriteLine(number++ + ". Create New Session");
            Console.WriteLine(number + ". Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, number, "Select Session");

            if (selectedOpt == number)
            {
                break;
            }
            else if (selectedOpt == number - 1)
            {
                CreateNewSession();
            }
            else
            {
                SessionMenu(learning.SessionList[selectedOpt - 1]);
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

    void SessionMenu(Session session)
    {
        while (true)
        {
            var sessionDetail = _sessionService.GetSessionAndContentsById(session.Id);
            Console.WriteLine($"\n{sessionDetail.SessionName} ({sessionDetail.StartTime} - {sessionDetail.EndTime})");
            Console.WriteLine("Description : " + sessionDetail.SessionDescription + "\n");

            Console.WriteLine("1. Attendance List");
            Console.WriteLine("2. Forum");
            Console.WriteLine("3. Material");
            Console.WriteLine("4. Task");
            Console.WriteLine("5. Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, 5);

            if (selectedOpt == 1)
            {
                ShowAttendanceList(sessionDetail);
            }
            else if (selectedOpt == 2)
            {
                base.ForumCommentMenu(session.Forum, _forumService);
            }
            else if (selectedOpt == 3)
            {
                MaterialMenu(sessionDetail.MaterialList);
            }
            else if (selectedOpt == 4)
            {
                TaskMenu(sessionDetail.TaskList);
            }
            else
            {
                break;
            }
        }
    }

    void ShowAttendanceList(Session session)
    {
        while (true)
        {
            var attendanceList = _sessionService.GetSessionAttendanceList(session.Id);
            Console.WriteLine("\nAttendance List :");
            var number = 1;
            foreach (var attendance in attendanceList)
            {
                var approvalStatus = attendance.IsApproved ? "Approved" : "Not Approved";
                Console.WriteLine($"{number}. {attendance.Student.FullName} ({approvalStatus} - {attendance.CreatedAt.ToString(DateFormat)})");
                number++;
            }
            Console.WriteLine(number + ". Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, number, "Select Student to Update Approval Status");

            if (selectedOpt == number)
            {
                break;
            }
            else
            {
                _sessionService.UpdateAttendanceApprovalStatus(attendanceList[selectedOpt - 1]);
                Console.WriteLine("\nAttendance status successfully updated");
            }
        }
    }

    void MaterialMenu(List<SessionMaterial> materialList)
    {
        while (true)
        {
            Console.WriteLine("\nMaterial List");
            var number = 1;
            foreach (var material in materialList)
            {
                Console.WriteLine($"{number}. {material.MaterialName}");
                number++;
            }
            Console.WriteLine(number++ + ". Create New Material");
            Console.WriteLine(number + ". Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, number);

            if (selectedOpt == number - 1)
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

    void TaskMenu(List<LMSTask> taskList)
    {
        while (true)
        {
            Console.WriteLine("\nTask List");
            var number = 1;
            foreach (var task in taskList)
            {
                Console.WriteLine($"{number}. {task.TaskName} - (Duration: {task.Duration} minutes)");
                number++;
            }
            Console.WriteLine(number++ + ". Create New Task");
            Console.WriteLine(number + ". Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, number);

            if (selectedOpt == number)
            {
                break;
            }
            else if (selectedOpt == number - 1)
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
                ShowTaskDetail(taskList[selectedOpt - 1]);
            }
        }
    }

    // TODO : create scoring & review feature
    void ShowTaskDetail(LMSTask task)
    {
        while (true)
        {
            var submissionList = _taskSubmissionService.GetSubmissionListBySession(task.SessionId);
            Console.WriteLine("\n" + task.TaskName + " Submission List");
            var number = 1;
            foreach (var submission in submissionList)
            {
                Console.WriteLine($"{number}. {submission.Student.FullName} - ({submission.CreatedAt.ToString(DateFormat)})");
                number++;
            }
            Console.WriteLine(number + ". Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, number);

            if (selectedOpt == 3)
            {
                break;
            }
        }
    }
}
