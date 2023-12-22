using Lms.Config;
using Lms.IRepo;
using Lms.IService;
using Lms.Model;

namespace Lms.Service;

internal class TaskSubmissionService : ITaskSubmissionService
{
    readonly ISubmissionRepo _submissionRepo;
    readonly ISubmissionDetailRepo _submissionDetailRepo;
    readonly ISubmissionDetailFileRepo _submissionDetailFileRepo;
    readonly ILMSFileRepo _fileRepo;

    public TaskSubmissionService(ISubmissionRepo submissionRepo, ISubmissionDetailRepo submissionDetailRepo, ISubmissionDetailFileRepo submissionDetailFileRepo, ILMSFileRepo fileRepo)
    {
        _submissionRepo = submissionRepo;
        _submissionDetailRepo = submissionDetailRepo;
        _submissionDetailFileRepo = submissionDetailFileRepo;
        _fileRepo = fileRepo;
    }

    public List<Submission> GetSubmissionListBySession(int sessionId)
    {
        var submissionList = _submissionRepo.GetSubmissionListBySession(sessionId);
        return submissionList;
    }

    public void SubmitTask(Submission submission)
    {
        var insertedSubmission = _submissionRepo.CreateNewSubmission(submission);
        foreach (var fileSubmission in submission.SubmissionDetailFileList)
        {
            var insertedFile = _fileRepo.CreateNewFile(fileSubmission.File);
            fileSubmission.File.Id = insertedFile.Id;
            fileSubmission.Submission.Id = insertedSubmission.Id;
            _submissionDetailFileRepo.CreateNewSubmissionDetailFile(fileSubmission);
        }
        foreach (var questionSubmission in submission.SubmissionDetailList)
        {
            questionSubmission.Submission.Id = insertedSubmission.Id;
            _submissionDetailRepo.CreateNewSubmissionDetail(questionSubmission);
        }
    }
}
