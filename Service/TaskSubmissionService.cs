using Lms.Config;
using Lms.Helper;
using Lms.IRepo;
using Lms.IService;
using Lms.Model;

namespace Lms.Service;

internal class TaskSubmissionService : ITaskSubmissionService
{
    readonly ISubmissionRepo _submissionRepo;
    readonly ISubmissionDetailQuestionRepo _submissionDetailRepo;
    readonly ISubmissionDetailFileRepo _submissionDetailFileRepo;
    readonly ILMSFileRepo _fileRepo;
    readonly SessionHelper _sessionHelper;

    public TaskSubmissionService
    (
        ISubmissionRepo submissionRepo,
        ISubmissionDetailQuestionRepo submissionDetailRepo,
        ISubmissionDetailFileRepo submissionDetailFileRepo,
        ILMSFileRepo fileRepo,
        SessionHelper sessionHelper
    )
    {
        _submissionRepo = submissionRepo;
        _submissionDetailRepo = submissionDetailRepo;
        _submissionDetailFileRepo = submissionDetailFileRepo;
        _fileRepo = fileRepo;
        _sessionHelper = sessionHelper;
    }

    public List<Submission> GetStudentSubmissionListBySession(int sessionId)
    {
        var submissionList = _submissionRepo.GetStudentSubmissionListBySession(sessionId, _sessionHelper.UserId);
        return submissionList;
    }

    public List<Submission> GetSubmissionListBySession(int sessionId)
    {
        var submissionList = _submissionRepo.GetSubmissionListBySession(sessionId);
        return submissionList;
    }

    public void SubmitTask(Submission submission)
    {
        using (var context = new DBContextConfig())
        {
            var trx = context.Database.BeginTransaction();

            submission.StudentId = _sessionHelper.UserId;
            submission.CreatedBy = _sessionHelper.UserId;
            submission.CreatedAt = DateTime.Now;
            var insertedSubmission = _submissionRepo.CreateNewSubmission(submission);

            foreach (var questionSubmission in submission.SubmissionDetailQuestionList)
            {
                questionSubmission.SubmissionId = insertedSubmission.Id;
                questionSubmission.CreatedBy = _sessionHelper.UserId;
                questionSubmission.CreatedAt = DateTime.Now;
                _submissionDetailRepo.CreateNewSubmissionDetailQuestion(questionSubmission);
            }

            foreach (var fileSubmission in submission.SubmissionDetailFileList)
            {
                fileSubmission.File.CreatedBy = _sessionHelper.UserId;
                fileSubmission.File.CreatedAt = DateTime.Now;
                var insertedFile = _fileRepo.CreateNewFile(fileSubmission.File);

                fileSubmission.FileId = insertedFile.Id;
                fileSubmission.SubmissionId = insertedSubmission.Id;
                fileSubmission.CreatedBy = _sessionHelper.UserId;
                fileSubmission.CreatedAt = DateTime.Now;
                _submissionDetailFileRepo.CreateNewSubmissionDetailFile(fileSubmission);
            }

            trx.Commit();
        }
    }
}
