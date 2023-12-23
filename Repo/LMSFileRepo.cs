namespace Lms.Repo;

using Lms.Config;
using Lms.IRepo;
using Lms.Model;

internal class LMSFileRepo : ILMSFileRepo
{
    readonly DBContextConfig _context;

    public LMSFileRepo(DBContextConfig context)
    {
        _context = context;
    }

    public LMSFile CreateNewFile(LMSFile file)
    {
        _context.LMSFiles.Add(file);
        _context.SaveChanges();
        return file;
    }
}
