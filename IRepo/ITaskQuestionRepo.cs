using Lms.Model;

namespace Lms.IRepo;

internal interface ITaskQuestionRepo
{
    List<TaskQuestion> GetQuestionListByTask(int taskId);
}
