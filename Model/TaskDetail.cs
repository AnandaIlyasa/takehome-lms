namespace Lms.Model;

internal class TaskDetail : BaseModel
{
    public LMSTask Task { get; init; }
    public TaskFile? TaskFile { get; set; }
    public TaskQuestion? TaskQuestion { get; set; }
}
