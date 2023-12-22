using Lms.Config;
using Lms.Model;

namespace Lms.IRepo;

internal interface ISubmissionDetailRepo
{
    SubmissionDetailQuestion CreateNewSubmissionDetail(SubmissionDetailQuestion submissionDetail);
}
