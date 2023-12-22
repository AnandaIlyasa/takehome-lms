using Lms.Config;
using Lms.Constant;
using Lms.Helper;
using Lms.IRepo;
using Lms.IService;
using Lms.Model;

namespace Lms.Service;

internal class SessionService : ISessionService
{
    readonly ISessionAttendanceRepo _sessionAttendanceRepo;
    readonly ISessionRepo _sessionRepo;
    readonly ISessionMaterialRepo _sessionMaterialRepo;
    readonly ISessionTaskRepo _sessionTaskRepo;
    readonly SessionHelper _sessionHelper;
    readonly ISessionMaterialFileRepo _sessionMaterialFileRepo;
    readonly ITaskQuestionRepo _questionRepo;
    readonly ITaskMultipleChoiceOptionRepo _multipleChoiceOptionRepo;
    readonly IForumRepo _forumRepo;
    readonly IForumCommentRepo _forumCommentRepo;
    readonly ITaskFileRepo _taskFileRepo;

    public SessionService
    (
        ISessionAttendanceRepo sessionAttendanceRepo,
        ISessionRepo sessionRepo,
        ISessionMaterialRepo sessionMaterialRepo,
        ISessionTaskRepo sessionTaskRepo,
        SessionHelper sessionHelper,
        ISessionMaterialFileRepo sessionMaterialFileRepo,
        ITaskQuestionRepo taskQuestionRepo,
        ITaskMultipleChoiceOptionRepo taskMultipleChoiceOptionRepo,
        IForumRepo forumRepo,
        IForumCommentRepo forumCommentRepo,
        ITaskFileRepo taskFileRepo
    )
    {
        _sessionAttendanceRepo = sessionAttendanceRepo;
        _sessionRepo = sessionRepo;
        _sessionMaterialRepo = sessionMaterialRepo;
        _sessionTaskRepo = sessionTaskRepo;
        _sessionHelper = sessionHelper;
        _sessionMaterialFileRepo = sessionMaterialFileRepo;
        _questionRepo = taskQuestionRepo;
        _multipleChoiceOptionRepo = taskMultipleChoiceOptionRepo;
        _forumRepo = forumRepo;
        _forumCommentRepo = forumCommentRepo;
        _taskFileRepo = taskFileRepo;
    }

    public SessionAttendance AttendSession(int sessionId)
    {
        var sessionAttendance = new SessionAttendance()
        {
            StudentId = _sessionHelper.UserId,
            SessionId = sessionId,
            IsApproved = false,
        };
        sessionAttendance = _sessionAttendanceRepo.CreateNewSessionAttendance(sessionAttendance);
        return sessionAttendance;
    }

    public SessionAttendance? GetStudentAttendanceStatus(int sessionId)
    {
        var sessionAttendance = _sessionAttendanceRepo.GetSessionAttendanceStatus(sessionId, _sessionHelper.UserId);
        return sessionAttendance;
    }

    public Session GetSessionById(int sessionId)
    {
        var session = _sessionRepo.GetSessionById(sessionId);
        var materialList = _sessionMaterialRepo.GetMaterialListBySession(sessionId);
        session.MaterialList = materialList;
        foreach (var material in materialList)
        {
            var materialFileList = _sessionMaterialFileRepo.GetSessionMaterialFileListByMaterial(material.Id);
            material.MaterialFileList = materialFileList;
        }

        var taskList = _sessionTaskRepo.GetTaskListBySession(sessionId);
        session.TaskList = taskList;
        foreach (var task in taskList)
        {
            var questionList = _questionRepo.GetQuestionListByTask(task.Id);
            task.TaskQuestionList = questionList;
            foreach (var question in questionList)
            {
                if (question.QuestionType == QuestionType.MultipleChoice)
                {
                    var optionList = _multipleChoiceOptionRepo.GetMultipleChoiceOptionListByQuestion(question.Id);
                    question.OptionList = optionList;
                }
            }

            var taskFileList = _taskFileRepo.GetTaskFileListTask(task.Id);
            task.TaskFileList = taskFileList;
        }

        var forum = _forumRepo.GetForumBySession(sessionId);
        session.Forum = forum;

        var commentList = _forumCommentRepo.GetForumCommentListByForum(sessionId);
        session.Forum.CommentList = commentList;

        return session;
    }
}
