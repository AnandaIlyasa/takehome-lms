namespace Lms.Model;

internal class SessionMaterialFile : BaseModel
{
    public string? FileName { get; init; }
    public LMSFile File { get; init; }
    public SessionMaterial Material { get; init; }
}
