namespace Lms.Model;

internal class SessionMaterialFile : BaseModel
{
    public LMSFile File { get; init; }
    public SessionMaterial Material { get; init; }
}
