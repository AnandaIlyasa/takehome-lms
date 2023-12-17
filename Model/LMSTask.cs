namespace Lms.Model;

internal class LMSTask : BaseModel
{
    public string TaskName { get; init; }
    public string? TaskDescription { get; init; }
    public int Duration { get; init; }
    public Session Session { get; init; }
    public List<TaskFile> TaskFileList { get; set; } // not mapped
    public List<TaskQuestion> TaskQuestionList { get; set; } // not mapped
}
