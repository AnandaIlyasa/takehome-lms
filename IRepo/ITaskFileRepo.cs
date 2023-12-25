using Lms.Model;

namespace Lms.IRepo;

internal interface ITaskFileRepo
{
    List<TaskFile> GetTaskFileList(int taskId);
}
