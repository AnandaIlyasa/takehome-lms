namespace Lms.Model;

internal class Session : BaseModel
{
    public string SessionName { get; set; }
    public string? SessionDescription { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public Learning Learning { get; init; }
    public Forum Forum { get; set; } // not mapped
    public List<SessionMaterial> MaterialList { get; set; } // not mapped
    public List<LMSTask> TaskList { get; set; } // not mapped
}
