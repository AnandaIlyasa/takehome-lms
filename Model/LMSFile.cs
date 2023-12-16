namespace Lms.Model;

internal class LMSFile : BaseModel
{
    public string FileContent { get; init; }
    public string FileExtension { get; init; }
}
