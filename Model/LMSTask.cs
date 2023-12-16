namespace Lms.Model;

internal class LMSTask : BaseModel
{
    public string TaskName { get; init; }
    public int Duration { get; init; }
    public Session Session { get; init; }
}
