using Lms.Model;

namespace Lms.IRepo;

internal interface ISubmissionDetailFileRepo
{
    SubmissionDetailFile CreateNewSubmissionDetailFile(SubmissionDetailFile submissionDetailFile);
    List<SubmissionDetailFile> GetStudentSubmissionDetailFileByTask(int taskId, int studentId);
}
