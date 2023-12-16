namespace Lms.Model;

internal class TaskFile : BaseModel
{
    public string? FileName { get; init; }
    public LMSFile File { get; init; }
}
