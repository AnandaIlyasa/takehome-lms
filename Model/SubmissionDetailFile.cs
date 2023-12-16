namespace Lms.Model;

internal class SubmissionDetailFile : BaseModel
{
    public SubmissionDetail SubmissionDetail { get; init; }
    public User Student { get; init; }
    public LMSFile File { get; init; }
}
