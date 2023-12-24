using Lms.Config;
using Lms.IRepo;
using Lms.Model;

namespace Lms.Repo;

internal class ForumCommentRepo : IForumCommentRepo
{
    readonly DBContextConfig _context;

    public ForumCommentRepo(DBContextConfig context)
    {
        _context = context;
    }

    public ForumComment CreateNewComment(ForumComment forumComment)
    {
        _context.ForumComments.Add(forumComment);
        _context.SaveChanges();
        return forumComment;
    }

    public List<ForumComment> GetForumCommentListByForum(int forumId)
    {
        var commentList = _context.ForumComments
                        .Where(fc => fc.ForumId == forumId)
                        .OrderBy(fc => fc.CreatedAt)
                        .ToList();
        return commentList;
    }
}
