namespace Lms.Model;

internal class User : BaseModel
{
    public string FullName { get; init; }
    public string Email { get; init; }
    public string Pass { get; init; }
    public LMSFile? Photo { get; init; }
    public Role Role { get; init; }
}
