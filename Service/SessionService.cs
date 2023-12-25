﻿using Lms.Config;
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
    readonly ISessionMaterialFileRepo _sessionMaterialFileRepo;
    readonly ITaskQuestionRepo _questionRepo;
    readonly ITaskMultipleChoiceOptionRepo _multipleChoiceOptionRepo;
    readonly IForumRepo _forumRepo;
    readonly IForumCommentRepo _forumCommentRepo;
    readonly ITaskFileRepo _taskFileRepo;
    readonly SessionHelper _sessionHelper;

    public SessionService
    (
        ISessionAttendanceRepo sessionAttendanceRepo,
        ISessionRepo sessionRepo,
        ISessionMaterialRepo sessionMaterialRepo,
        ISessionTaskRepo sessionTaskRepo,
        ISessionMaterialFileRepo sessionMaterialFileRepo,
        ITaskQuestionRepo taskQuestionRepo,
        ITaskMultipleChoiceOptionRepo taskMultipleChoiceOptionRepo,
        IForumRepo forumRepo,
        IForumCommentRepo forumCommentRepo,
        ITaskFileRepo taskFileRepo,
        SessionHelper sessionHelper
    )
    {
        _sessionAttendanceRepo = sessionAttendanceRepo;
        _sessionRepo = sessionRepo;
        _sessionMaterialRepo = sessionMaterialRepo;
        _sessionTaskRepo = sessionTaskRepo;
        _sessionMaterialFileRepo = sessionMaterialFileRepo;
        _questionRepo = taskQuestionRepo;
        _multipleChoiceOptionRepo = taskMultipleChoiceOptionRepo;
        _forumRepo = forumRepo;
        _forumCommentRepo = forumCommentRepo;
        _taskFileRepo = taskFileRepo;
        _sessionHelper = sessionHelper;
    }

    public SessionAttendance AttendSession(int sessionId)
    {
        var sessionAttendance = new SessionAttendance()
        {
            StudentId = _sessionHelper.UserId,
            SessionId = sessionId,
            IsApproved = false,
            CreatedAt = DateTime.Now,
            CreatedBy = _sessionHelper.UserId,
        };
        sessionAttendance = _sessionAttendanceRepo.CreateNewSessionAttendance(sessionAttendance);
        return sessionAttendance;
    }

    public SessionAttendance? GetStudentAttendanceStatus(int sessionId)
    {
        var sessionAttendance = _sessionAttendanceRepo.GetSessionAttendanceStatus(sessionId, _sessionHelper.UserId);
        return sessionAttendance;
    }

    public List<SessionAttendance> GetSessionAttendanceList(int sessionId)
    {
        var attendanceList = _sessionAttendanceRepo.GetSessionAttendanceList(sessionId);
        return attendanceList;
    }

    public Session GetSessionAndContentsById(int sessionId)
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
                    optionList = optionList
                                .OrderBy(o => o.OptionChar)
                                .ToList();
                    question.OptionList = optionList;
                }
            }
            task.TaskQuestionList = task.TaskQuestionList
                                    .OrderByDescending(q => q.QuestionType)
                                    .ToList();

            var taskFileList = _taskFileRepo.GetTaskFileList(task.Id);
            task.TaskFileList = taskFileList;
        }

        var forum = _forumRepo.GetForumBySession(sessionId);
        session.Forum = forum;

        var commentList = _forumCommentRepo.GetForumCommentListByForum(sessionId);
        session.Forum.CommentList = commentList;

        return session;
    }

    public void UpdateAttendanceApprovalStatus(SessionAttendance sessionAttendance)
    {
        sessionAttendance.UpdatedBy = _sessionHelper.UserId;
        sessionAttendance.UpdatedAt = DateTime.Now;
        sessionAttendance.IsApproved = !sessionAttendance.IsApproved;
        _sessionAttendanceRepo.UpdateSessionAttendance(sessionAttendance);
    }
}
