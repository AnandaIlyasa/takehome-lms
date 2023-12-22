using Lms.Config;
using Lms.IRepo;
using Lms.Model;

namespace Lms.Repo;

internal class LearningRepo : ILearningRepo
{
    readonly DBContextConfig _context;

    public LearningRepo(DBContextConfig context)
    {
        _context = context;
    }

    public List<Learning> GetLearningListByClass(int classId)
    {
        var learningList = _context.Learnings
                        .Where(l => l.ClassId == classId)
                        .ToList();
        return learningList;
    }
}
