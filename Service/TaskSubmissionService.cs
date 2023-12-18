using Lms.IRepo;
using Lms.IService;
using Lms.Model;

namespace Lms.Service;

internal class TaskSubmissionService : ITaskSubmissionService
{
    public ISubmissionRepo SubmissionRepo { private get; init; }
    public ISubmissionDetailRepo SubmissionDetailRepo { private get; init; }
    public ISubmissionDetailFileRepo SubmissionDetailFileRepo { private get; init; }
    public ILMSFileRepo FileRepo { private get; init; }

    public List<Submission> GetSubmissionListBySession(int sessionId)
    {
        var submissionList = SubmissionRepo.GetSubmissionListBySession(sessionId);
        return submissionList;
    }

    public void SubmitTask(Submission submission)
    {
        var insertedSubmission = SubmissionRepo.CreateNewSubmission(submission);
        foreach (var fileSubmission in submission.SubmissionDetailFileList)
        {
            var insertedFile = FileRepo.CreateNewFile(fileSubmission.File);
            fileSubmission.File.Id = insertedFile.Id;
            fileSubmission.Submission.Id = insertedSubmission.Id;
            SubmissionDetailFileRepo.CreateNewSubmissionDetailFile(fileSubmission);
        }
        foreach (var questionSubmission in submission.SubmissionDetailList)
        {
            questionSubmission.Submission.Id = insertedSubmission.Id;
            SubmissionDetailRepo.CreateNewSubmissionDetail(questionSubmission);
        }
    }
}
