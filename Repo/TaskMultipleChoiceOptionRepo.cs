using Lms.Config;
using Lms.IRepo;
using Lms.Model;

namespace Lms.Repo;

internal class TaskMultipleChoiceOptionRepo : ITaskMultipleChoiceOptionRepo
{
    readonly DBContextConfig _context;

    public TaskMultipleChoiceOptionRepo(DBContextConfig context)
    {
        _context = context;
    }

    public List<TaskMultipleChoiceOption> GetMultipleChoiceOptionListByQuestion(int questionId)
    {
        var optionList = _context.TaskMultipleChoiceOptions
                        .Where(mco => mco.QuestionId == questionId)
                        .ToList();
        return optionList;
    }
}
