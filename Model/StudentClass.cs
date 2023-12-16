namespace Lms.Model;

internal class StudentClass : BaseModel
{
    public Class Class { get; init; }
    public User Student { get; init; }
}
