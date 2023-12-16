namespace Lms.Model;

internal class Class : BaseModel
{
    public string ClassCode { get; init; }
    public string ClassTitle { get; init; }
    public string? ClassDescription { get; init; }
    public LMSFile ClassImage { get; init; }
}
