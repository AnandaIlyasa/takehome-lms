using Lms.Model;

namespace Lms.IService;

internal interface ITaskSubmissionService
{
    void SubmitTask(Submission submission);
}
