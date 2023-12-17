namespace Lms.View;

using Lms.Constant;
using Lms.IService;
using Lms.Model;
using Lms.Utils;
using System.Threading.Tasks;

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

                if (selectedOpt <= sessionDetail.MaterialList.Count)
                {
                    MaterialMenu(sessionDetail.MaterialList[selectedOpt - 1]);
                    break;
                }
                else if (selectedOpt > sessionDetail.MaterialList.Count && selectedOpt < number)
                {
                    ShowTaskDetail(sessionDetail.TaskList[selectedOpt - sessionDetail.MaterialList.Count - 1]);
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
    }

    void ShowTaskDetail(LMSTask task)
    {
        var submission = new Submission()
        {
            Student = _studentUser,
            Task = task,
            SubmissionDetailList = new List<SubmissionDetail>(),
            SubmissionDetailFileList = new List<SubmissionDetailFile>(),
        };
        while (true)
        {
            Console.WriteLine("\nTask Name: " + task.TaskName);
            Console.WriteLine("Description: " + task.TaskDescription);
            Console.WriteLine("Duration: " + task.Duration + " minutes\n");

            var number = 1;
            var groupedQuestionList = task.TaskQuestionList
                                        .OrderByDescending(q => q.QuestionType)
                                        .ToList();
            foreach (var taskQuestion in groupedQuestionList)
            {
                var existingAnswer = submission.SubmissionDetailList.Find(q => q.Question.Id == taskQuestion.Id);
                string? answer = null;
                if (existingAnswer?.EssayAnswerContent is string) answer = existingAnswer.EssayAnswerContent;
                else if (existingAnswer?.ChoiceOption != null) answer = $"({existingAnswer?.ChoiceOption?.OptionChar}) {existingAnswer?.ChoiceOption?.OptionText}";

                if (answer == null)
                {
                    Console.WriteLine($"{number}. {taskQuestion.QuestionContent}");
                }
                else
                {
                    Console.WriteLine($"{number}. {taskQuestion.QuestionContent} - Your answer: {answer}");
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
                Console.WriteLine($"{number}. {taskFile.FileName} - ({taskFile.File.FileContent}.{taskFile.File.FileExtension})");
                number++;
            }

            Console.WriteLine(number + ". Back");
            var selectedOpt = Utils.GetNumberInputUtil(1, number, "Select question number to answer");

            if (selectedOpt <= groupedQuestionList.Count)
            {
                var selectedQuestion = groupedQuestionList[selectedOpt - 1];
                var existingAnswer = submission.SubmissionDetailList.Find(q => q.Question.Id == selectedQuestion.Id);
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
                        var answer = new SubmissionDetail()
                        {
                            Question = selectedQuestion,
                            ChoiceOption = selectedQuestion.OptionList[selectedChoice - 1],
                        };
                        submission.SubmissionDetailList.Add(answer);
                    }
                    else
                    {
                        existingAnswer.ChoiceOption = selectedQuestion.OptionList[selectedChoice - 1];
                    }
                }
                else
                {
                    var essayAnswer = Utils.GetStringInputUtil("Your answer");
                    if (existingAnswer == null)
                    {
                        var answer = new SubmissionDetail()
                        {
                            Question = selectedQuestion,
                            EssayAnswerContent = essayAnswer,
                        };
                        submission.SubmissionDetailList.Add(answer);
                    }
                    else
                    {
                        existingAnswer.EssayAnswerContent = essayAnswer;
                    }
                }
            }
            else if (selectedOpt > groupedQuestionList.Count && selectedOpt < number)
            {
                var selectedTaskFile = task.TaskFileList[selectedOpt - groupedQuestionList.Count - 1];
                var existingAnswer = submission.SubmissionDetailFileList.Find(tf => tf.TaskFile.Id == selectedTaskFile.Id);

                var filename = Utils.GetStringInputUtil("Filename");
                var extension = Utils.GetStringInputUtil("Extension");
                if (existingAnswer == null)
                {
                    var answer = new SubmissionDetailFile()
                    {
                        TaskFile = selectedTaskFile,
                        File = new LMSFile()
                        {
                            FileContent = filename,
                            FileExtension = extension,
                        },
                    };
                    submission.SubmissionDetailFileList.Add(answer);
                }
                else
                {
                    existingAnswer.File = new LMSFile()
                    {
                        FileContent = filename,
                        FileExtension = extension,
                    };
                }
            }
            else
            {
                break;
            }
        }
    }
}
