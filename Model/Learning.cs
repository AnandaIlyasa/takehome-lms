namespace Lms.Model;

internal class Learning : BaseModel
{
    public string LearningName { get; init; }
    public string? LearningDescription { get; init; }
    public DateOnly LearningDate { get; init; }
    public Class Class { get; init; }
    public List<Session> SessionList { get; set; } // not mapped
}
