using Lms.Model;

namespace Lms.IRepo;

internal interface ITaskFileRepo
{
    List<TaskFile> GetTaskFileListTask(int taskId);
}
