using Lms.Config;
using Lms.IRepo;
using Lms.Model;
using Microsoft.EntityFrameworkCore;

namespace Lms.Repo;

internal class TaskFileRepo : ITaskFileRepo
{
    readonly DBContextConfig _context;

    public TaskFileRepo(DBContextConfig context)
    {
        _context = context;
    }

    public List<TaskFile> GetTaskFileListTask(int taskId)
    {
        var taskFileList = _context.TaskFiles
                            .Join(
                                _context.TaskDetails,
                                tf => tf.Id,
                                td => td.TaskFileId,
                                (tf, td) => new { tf, td }
                            )
                            .Where(tftd => tftd.td.TaskId == taskId)
                            .Select(tftd => tftd.tf)
                            .GroupBy(tf => tf.Id)
                            .Select(tf => tf.First())
                            .ToList();
        return taskFileList;
    }
}
