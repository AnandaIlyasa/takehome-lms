using Lms.Config;
using Lms.Model;

namespace Lms.IRepo;

internal interface ISubmissionDetailFileRepo
{
    SubmissionDetailFile CreateNewSubmissionDetailFile(SubmissionDetailFile submissionDetailFile);
}
