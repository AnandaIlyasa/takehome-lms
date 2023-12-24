using Lms.Model;

namespace Lms.IService;

internal interface ITaskSubmissionService
{
    void SubmitTask(Submission submission);
    List<Submission> GetStudentSubmissionListBySession(int sessionId);
    List<Submission> GetSubmissionListBySession(int sessionId);
}
