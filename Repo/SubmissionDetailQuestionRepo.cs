using Lms.Config;
using Lms.IRepo;
using Lms.Model;

namespace Lms.Repo;

internal class SubmissionDetailQuestionRepo : ISubmissionDetailQuestionRepo
{
    readonly DBContextConfig _context;

    public SubmissionDetailQuestionRepo(DBContextConfig context)
    {
        _context = context;
    }

    public SubmissionDetailQuestion CreateNewSubmissionDetailQuestion(SubmissionDetailQuestion submissionDetailQuestion)
    {
        _context.SubmissionDetailQuestions.Add(submissionDetailQuestion);
        _context.SaveChanges();
        return submissionDetailQuestion;
    }

    public List<SubmissionDetailQuestion> GetStudentSubmissionDetailQuestionByTask(int taskId)
    {
        throw new NotImplementedException();
    }
}
