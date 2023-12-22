using Lms.Config;
using Lms.Model;

namespace Lms.IRepo;

internal interface ISubmissionRepo
{
    Submission CreateNewSubmission(Submission submission);
    List<Submission> GetSubmissionListBySession(int sessionId);
}
