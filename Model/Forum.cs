namespace Lms.Model;

internal class Forum : BaseModel
{
    public string ForumName { get; init; }
    public string? ForumDescription { get; init; }
    public Session Session { get; init; }
}
