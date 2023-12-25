namespace Lms.View;

using Lms.Constant;
using Lms.IService;
using Lms.Model;
using Lms.Utils;

internal class StudentView : StudentTeacherBaseView
{
    readonly IClassService _classService;
    readonly ISessionService _sessionService;
    readonly ITaskSubmissionService _taskSubmissionService;
    readonly IForumService _forumService;

    public StudentView
    (
        IClassService classService,
        ISessionService sessionService,
        ITaskSubmissionService taskSubmissionService,
        IForumService forumService
    )
    {
        _classService = classService;
        _sessionService = sessionService;
        _taskSubmissionService = taskSubmissionService;
        _forumService = forumService;
    }

    public void MainMenu(User student)
    {
        while (true)
        {
            Console.WriteLine("\n--- Student Page - hello, " + student.FullName + " ----");
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
            var classList = _classService.GetEnrolledClassList();

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
            var unEnrolledClassList = _classService.GetUnEnrolledClassList();

            Console.WriteLine("\n--- Available Class List ---");

            var number = 1;
            foreach (var unEnrolledClass in unEnrolledClassList)
            {
                Console.WriteLine($"{number}. {unEnrolledClass.ClassTitle}");
                number++;
            }
            Console.WriteLine(number + ". Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, number, "Select Class to Enroll");

            if (selectedOpt == number)
            {
                break;
            }
            else
            {
                var selectedClass = unEnrolledClassList[selectedOpt - 1];
                _classService.EnrollClass(selectedClass.Id);

                Console.WriteLine($"\nYou successfully enrolled in {selectedClass.ClassTitle} class");
            }
        }
    }

    void ShowClassLearningList(List<Learning> learningList)
    {
        while (true)
        {
            Console.WriteLine("\nLearning List");
            var number = 1;
            foreach (var learning in learningList)
            {
                Console.WriteLine($"{number}. {learning.LearningName} ({learning.LearningDate.ToString(DateFormat)})");
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
            Console.WriteLine("\nSession List");
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
        var attendance = _sessionService.GetStudentAttendanceStatus(session.Id);
        if (attendance == null)
        {
            Console.WriteLine($"\n{session.SessionName} ({session.StartTime} - {session.EndTime})");
            Console.WriteLine("1. Attend this session");
            Utils.GetNumberInputUtil(1, 1);

            _sessionService.AttendSession(session.Id);
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
            var sessionDetail = _sessionService.GetSessionAndContentsById(session.Id);
            while (true)
            {
                Console.WriteLine($"\n{sessionDetail.SessionName} ({sessionDetail.StartTime} - {sessionDetail.EndTime})");
                Console.WriteLine("Description : " + sessionDetail.SessionDescription);

                var number = 1;
                Console.WriteLine($"{number}. {sessionDetail.Forum.ForumName}");
                number++;
                foreach (var material in sessionDetail.MaterialList)
                {
                    Console.WriteLine($"{number}. {material.MaterialName}");
                    number++;
                }
                var submissionList = _taskSubmissionService.GetStudentSubmissionListBySession(sessionDetail.Id);
                foreach (var task in sessionDetail.TaskList)
                {
                    var submission = submissionList.Find(s => s.Task.Id == task.Id);
                    if (submission == null)
                    {
                        Console.WriteLine($"{number}. {task.TaskName}");
                    }
                    else
                    {
                        Console.WriteLine($"{number}. {task.TaskName} - (submitted: {submission.CreatedAt})");
                    }
                    number++;
                }
                Console.WriteLine(number + ". Back");
                var selectedOpt = Utils.GetNumberInputUtil(1, number);

                if (selectedOpt == 1)
                {
                    base.ForumCommentMenu(sessionDetail.Forum, _forumService);
                }
                else if (selectedOpt <= sessionDetail.MaterialList.Count + 1)
                {
                    MaterialMenu(sessionDetail.MaterialList[selectedOpt - 2]);
                }
                else if (selectedOpt > sessionDetail.MaterialList.Count + 1 && selectedOpt < number)
                {
                    var selectedTask = sessionDetail.TaskList[selectedOpt - sessionDetail.MaterialList.Count - 2];
                    var submission = submissionList.Find(s => s.Task.Id == selectedTask.Id);
                    if (submission == null)
                    {
                        ShowTaskDetail(selectedTask);
                    }
                    else
                    {
                        Console.WriteLine("\n" + selectedTask.TaskName + " submission detail");
                        Console.WriteLine("Submitted: " + submission.CreatedAt);
                        Console.WriteLine("Score: " + submission.Grade);
                        Console.WriteLine("Teacher Notes: " + submission.TeacherNotes);
                    }
                }
                else
                {
                    break;
                }
            }
        }
    }

    void MaterialMenu(SessionMaterial material)
    {
        Console.WriteLine("\n" + material.MaterialName + " Detail");
        Console.WriteLine("Description: " + material.MaterialDescription);
        Console.Write("Material File: ");
        foreach (var file in material.MaterialFileList)
        {
            Console.Write($"{file.FileName} ({file.File.FileContent}.{file.File.FileExtension}), ");
        }
        Console.WriteLine();
    }

    void ShowTaskDetail(LMSTask task)
    {
        var submission = new Submission()
        {
            TaskId = task.Id,
            SubmissionDetailQuestionList = new List<SubmissionDetailQuestion>(),
            SubmissionDetailFileList = new List<SubmissionDetailFile>(),
        };
        while (true)
        {
            Console.WriteLine("\nTask Name: " + task.TaskName);
            Console.WriteLine("Description: " + task.TaskDescription);
            Console.WriteLine("Duration: " + task.Duration + " minutes\n");

            var questionList = task.TaskQuestionList;
            var number = 1;
            foreach (var taskQuestion in questionList)
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
                    Console.WriteLine($"{number}. {taskQuestion.QuestionContent} --- Your answer: {answer}");
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
                    Console.Write($"{number}. {taskFile.FileName} - ({taskFile.File.FileContent}.{taskFile.File.FileExtension}) --- Your answer: ");
                    foreach (var submissionFile in existingAnswerList) Console.Write($"{submissionFile.File.FileContent}.{submissionFile.File.FileExtension}, ");
                    Console.WriteLine();
                }
                number++;
            }
            Console.WriteLine(number++ + ". Finish and Submit Task");
            Console.WriteLine(number + ". Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, number, "Select question number to answer");

            if (selectedOpt <= questionList.Count)
            {
                var selectedQuestion = questionList[selectedOpt - 1];
                var existingAnswer = submission.SubmissionDetailQuestionList.Find(q => q.QuestionId == selectedQuestion.Id);
                if (selectedQuestion.QuestionType == QuestionType.MultipleChoice)
                {
                    var optionNumber = 1;
                    foreach (var option in selectedQuestion.OptionList)
                    {
                        Console.WriteLine($"{optionNumber}. {option.OptionChar}) {option.OptionText}");
                        optionNumber++;
                    }
                    var selectedChoice = Utils.GetNumberInputUtil(1, optionNumber - 1, "Select your answer");

                    if (existingAnswer == null)
                    {
                        var answer = new SubmissionDetailQuestion()
                        {
                            QuestionId = selectedQuestion.Id,
                            ChoiceOption = selectedQuestion.OptionList[selectedChoice - 1],
                            ChoiceOptionId = selectedQuestion.OptionList[selectedChoice - 1].Id,
                        };
                        submission.SubmissionDetailQuestionList.Add(answer);
                    }
                    else
                    {
                        existingAnswer.ChoiceOptionId = selectedQuestion.OptionList[selectedChoice - 1].Id;
                        existingAnswer.ChoiceOption = selectedQuestion.OptionList[selectedChoice - 1];
                    }
                }
                else
                {
                    var essayAnswer = Utils.GetStringInputUtil("Your answer");
                    if (existingAnswer == null)
                    {
                        var answer = new SubmissionDetailQuestion()
                        {
                            QuestionId = selectedQuestion.Id,
                            EssayAnswerContent = essayAnswer,
                        };
                        submission.SubmissionDetailQuestionList.Add(answer);
                    }
                    else
                    {
                        existingAnswer.EssayAnswerContent = essayAnswer;
                    }
                }
            }
            else if (selectedOpt > questionList.Count && selectedOpt < number - 1)
            {
                var selectedTaskFile = task.TaskFileList[selectedOpt - questionList.Count - 1];
                var existingAnswer = submission.SubmissionDetailFileList.Find(tf => tf.TaskFileId == selectedTaskFile.Id);
                if (existingAnswer != null)
                {
                    Console.WriteLine("You can not change your answer for this task file\n");
                    continue;
                }

                var numberOfFile = Utils.GetNumberInputUtil(1, 5, "How many file you want to submit");
                for (int i = 0; i < numberOfFile; i++)
                {
                    var filename = Utils.GetStringInputUtil("Filename");
                    var extension = Utils.GetStringInputUtil("Extension");

                    var answer = new SubmissionDetailFile()
                    {
                        TaskFileId = selectedTaskFile.Id,
                        File = new LMSFile()
                        {
                            FileContent = filename,
                            FileExtension = extension,
                        },
                    };
                    submission.SubmissionDetailFileList.Add(answer);
                }
            }
            else if (selectedOpt == number - 1)
            {
                _taskSubmissionService.SubmitTask(submission);
                Console.WriteLine("\nYou successfully submit this task");
                break;
            }
            else
            {
                break;
            }
        }
    }
}
