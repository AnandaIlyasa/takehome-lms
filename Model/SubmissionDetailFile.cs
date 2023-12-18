namespace Lms.Model;

internal class SubmissionDetailFile : BaseModel
{
    public Submission Submission { get; init; }
    public LMSFile File { get; set; }
    public TaskFile TaskFile { get; set; } // not mapped
}
