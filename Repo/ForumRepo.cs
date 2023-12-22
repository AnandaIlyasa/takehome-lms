using Lms.Config;
using Lms.IRepo;
using Lms.Model;

namespace Lms.Repo;

internal class ForumRepo : IForumRepo
{
    readonly DBContextConfig _context;

    public ForumRepo(DBContextConfig context)
    {
        _context = context;
    }

    public Forum GetForumBySession(int sessionId)
    {
        var forum = _context.Forums
                    .Where(f => f.SessionId == sessionId)
                    .First();
        return forum;
    }

    public List<ForumComment> GetForumCommentListByForum(int forumId)
    {
        var commentList = _context.ForumComments
                        .Where(fc => fc.ForumId == forumId)
                        .ToList();
        return commentList;
    }
}
