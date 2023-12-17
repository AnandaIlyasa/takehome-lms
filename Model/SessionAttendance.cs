namespace Lms.Model;

internal class SessionAttendance : BaseModel
{
    public User Student { get; init; }
    public Session Session { get; init; }
    public bool IsApproved { get; set; }
}
