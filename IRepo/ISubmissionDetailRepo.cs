using Lms.Model;

namespace Lms.IRepo;

internal interface ISubmissionDetailRepo
{
    SubmissionDetail CreateNewSubmissionDetail(SubmissionDetail submissionDetail);
}
