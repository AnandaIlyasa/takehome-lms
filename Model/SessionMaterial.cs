namespace Lms.Model;

internal class SessionMaterial : BaseModel
{
    public string MaterialName { get; init; }
    public string? MaterialDescription { get; init; }
    public Session Session { get; init; }
    public List<SessionMaterialFile>? MaterialFileList { get; set; } // not mapped
}
