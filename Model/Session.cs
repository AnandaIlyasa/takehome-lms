namespace Lms.Model;

internal class Session : BaseModel
{
    public string SessionName { get; init; }
    public string? SessionDescription { get; init; }
    public TimeOnly StartTime { get; init; }
    public TimeOnly EndTime { get; init; }
    public Learning Learning { get; init; }
}
