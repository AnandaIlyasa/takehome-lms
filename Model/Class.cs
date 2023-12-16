namespace Lms.Model;

internal class Class : BaseModel
{
    public User Teacher { get; set; }
    public string ClassCode { get; init; }
    public string ClassTitle { get; init; }
    public string? ClassDescription { get; init; }
    public LMSFile ClassImage { get; init; }
    public List<Learning> LearningList { get; set; } // not mapped
}
