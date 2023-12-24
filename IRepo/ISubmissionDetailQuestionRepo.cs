using Lms.Model;

namespace Lms.IRepo;

internal interface ISubmissionDetailQuestionRepo
{
    SubmissionDetailQuestion CreateNewSubmissionDetailQuestion(SubmissionDetailQuestion submissionDetail);
    List<SubmissionDetailQuestion> GetStudentSubmissionDetailQuestionByTask(int taskId, int studentId);
}
