namespace Lms.Model;

internal class ForumComment : BaseModel
{
    public string CommentContent { get; init; }
    public User User { get; init; }
    public Forum Forum { get; init; }
}
