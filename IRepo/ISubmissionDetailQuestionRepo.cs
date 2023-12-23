using Lms.Config;
using Lms.Model;

namespace Lms.IRepo;

internal interface ISubmissionDetailQuestionRepo
{
    SubmissionDetailQuestion CreateNewSubmissionDetailQuestion(SubmissionDetailQuestion submissionDetail);
}
