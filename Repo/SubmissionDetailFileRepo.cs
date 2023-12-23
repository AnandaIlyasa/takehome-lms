using Lms.Config;
using Lms.Helper;
using Lms.IRepo;
using Lms.Model;

namespace Lms.Repo;

internal class SubmissionDetailFileRepo : ISubmissionDetailFileRepo
{
    readonly DBContextConfig _context;

    public SubmissionDetailFileRepo(DBContextConfig context)
    {
        _context = context;
    }

    public SubmissionDetailFile CreateNewSubmissionDetailFile(SubmissionDetailFile submissionDetailFile)
    {
        _context.SubmissionDetailFiles.Add(submissionDetailFile);
        _context.SaveChanges();
        return submissionDetailFile;
    }
}
