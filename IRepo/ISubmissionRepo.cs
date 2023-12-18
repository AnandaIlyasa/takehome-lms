using Lms.Model;

namespace Lms.IRepo;

internal interface ISubmissionRepo
{
    Model.Submission CreateNewSubmission(Submission submission);
}
