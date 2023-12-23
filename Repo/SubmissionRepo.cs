using Lms.Config;
using Lms.IRepo;
using Lms.Model;

namespace Lms.Repo;

internal class SubmissionRepo : ISubmissionRepo
{
    readonly DBContextConfig _context;

    public SubmissionRepo(DBContextConfig context)
    {
        _context = context;
    }

    public Submission CreateNewSubmission(Submission submission)
    {
        _context.Submissions.Add(submission);
        _context.SaveChanges();
        return submission;
    }

    public List<Submission> GetSubmissionListBySession(int sessionId)
    {
        var query =
            from s in _context.Submissions
            join t in _context.LMSTasks on s.TaskId equals t.Id into stGroup
            from st in stGroup.DefaultIfEmpty()
            join ses in _context.Sessions on st.SessionId equals ses.Id into sesGroup
            select s;

        var submissionList = query.ToList();
        return submissionList;
    }
}
