namespace Lms.Model;

internal class Submission : BaseModel
{
    public float? Grade { get; set; }
    public string? TeacherNotes { get; set; }
    public User Student { get; init; }
    public LMSTask Task { get; init; }
}
