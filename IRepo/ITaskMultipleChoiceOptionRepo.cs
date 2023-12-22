using Lms.Model;

namespace Lms.IRepo;

internal interface ITaskMultipleChoiceOptionRepo
{
    List<TaskMultipleChoiceOption> GetMultipleChoiceOptionListByQuestion(int questionId);
}
