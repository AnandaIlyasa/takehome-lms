namespace Lms.View;

using Lms.Constant;
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

    public TeacherView(IClassService classService, ISessionService sessionService, IForumService forumService, ITaskSubmissionService taskSubmissionService)
    {
        _classService = classService;
        _sessionService = sessionService;
        _forumService = forumService;
        _taskSubmissionService = taskSubmissionService;
    }

    public void MainMenu(User teacher)
    {
        while (true)
        {
            Console.WriteLine("\n--- Teacher Page - hello, " + teacher.FullName + " ----");
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
                ShowTaskSubmissionList(taskList[selectedOpt - 1]);
            }
        }
    }

    void ShowTaskSubmissionList(LMSTask task)
    {
        while (true)
        {
            var submissionList = _taskSubmissionService.GetSubmissionListBySession(task.SessionId);
            Console.WriteLine("\n" + task.TaskName + " Submission List");
            var number = 1;
            foreach (var submission in submissionList)
            {
                if (submission.UpdatedAt.HasValue)
                {
                    Console.WriteLine($"{number}. {submission.Student.FullName} - ({submission.CreatedAt.ToString(DateTimeFormat)}) - REVIEWED");
                }
                else
                {
                    Console.WriteLine($"{number}. {submission.Student.FullName} - ({submission.CreatedAt.ToString(DateTimeFormat)})");
                }
                number++;
            }
            Console.WriteLine(number + ". Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, number);

            if (selectedOpt == number)
            {
                break;
            }
            else
            {
                ShowSubmissionDetail(task, submissionList[selectedOpt - 1]);
            }
        }
    }

    void ShowSubmissionDetail(LMSTask task, Submission submission)
    {
        var nMultipleChoice = 0;
        var nMultipleChoiceCorrect = 0;
        foreach (var question in task.TaskQuestionList) if (question.QuestionType == QuestionType.MultipleChoice) nMultipleChoice++;
        foreach (var answer in submission.SubmissionDetailQuestionList) if (answer.ChoiceOption != null && answer.ChoiceOption.IsCorrect) nMultipleChoiceCorrect++;
        var multipleChoiceScore = (double)((double)nMultipleChoiceCorrect / nMultipleChoice) * 100.0d;

        ShowSubmissionInformation(task, submission, multipleChoiceScore);

        Console.WriteLine("\n1. Insert Score and Notes");
        Console.WriteLine("2. Back");
        var selectedOpt = Utils.GetNumberInputUtil(1, 2);

        if (selectedOpt == 1)
        {
            Console.Write("Essay Score: ");
            var score = Convert.ToDouble(Console.ReadLine());
            var notes = Utils.GetStringInputUtil("Notes");

            submission.Grade = (multipleChoiceScore + score) / 2;
            submission.TeacherNotes = notes;

            _taskSubmissionService.InsertScoreAndNotes(submission);
            submission = _taskSubmissionService.GetStudentSubmissionByTask(submission.StudentId, submission.TaskId);

            Console.WriteLine("\nScore and Notes successfully submitted");

            ShowSubmissionInformation(task, submission, multipleChoiceScore);
        }
        else
        {
            return;
        }
    }

    void ShowSubmissionInformation(LMSTask task, Submission submission, double multipleChoiceScore)
    {
        Console.WriteLine($"\n{submission.Student.FullName} {task.TaskName} Submission");
        Console.WriteLine("Submitted: " + submission.CreatedAt.ToString(DateTimeFormat));
        Console.WriteLine("Multiple Choice Score: " + multipleChoiceScore);
        Console.WriteLine("Final Score: " + submission.Grade);
        Console.WriteLine("Notes: " + submission.TeacherNotes);

        ShowStudentAnswerList(task, submission);
    }

    void ShowStudentAnswerList(LMSTask task, Submission submission)
    {
        Console.WriteLine("\n" + submission.Student.FullName + " answer list:");
        var number = 1;
        foreach (var taskQuestion in task.TaskQuestionList)
        {
            var existingAnswer = submission.SubmissionDetailQuestionList.Find(q => q.QuestionId == taskQuestion.Id);
            string? answer = null;
            if (existingAnswer?.EssayAnswerContent is string) answer = existingAnswer.EssayAnswerContent;
            else if (existingAnswer?.ChoiceOption != null) answer = $"({existingAnswer?.ChoiceOption?.OptionChar}) {existingAnswer?.ChoiceOption?.OptionText}";

            if (answer == null)
            {
                Console.WriteLine($"{number}. {taskQuestion.QuestionContent}");
            }
            else
            {
                Console.WriteLine($"{number}. {taskQuestion.QuestionContent} --- answer: {answer}");
            }

            if (taskQuestion.QuestionType == QuestionType.MultipleChoice)
            {
                foreach (var option in taskQuestion.OptionList)
                {
                    Console.WriteLine($"   {option.OptionChar}) {option.OptionText}");
                }
            }
            number++;
        }

        foreach (var taskFile in task.TaskFileList)
        {
            var existingAnswerList = new List<SubmissionDetailFile>();
            foreach (var answerFile in submission.SubmissionDetailFileList)
            {
                if (answerFile.TaskFileId == taskFile.Id) existingAnswerList.Add(answerFile);
            }
            if (existingAnswerList.Count == 0)
            {
                Console.WriteLine($"{number}. {taskFile.FileName} - ({taskFile.File.FileContent}.{taskFile.File.FileExtension})");
            }
            else
            {
                Console.Write($"{number}. {taskFile.FileName} - ({taskFile.File.FileContent}.{taskFile.File.FileExtension}) --- answer: ");
                foreach (var submissionFile in existingAnswerList) Console.Write($"{submissionFile.File.FileContent}.{submissionFile.File.FileExtension}, ");
                Console.WriteLine();
            }
            number++;
        }
    }
}
