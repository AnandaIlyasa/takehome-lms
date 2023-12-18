using Lms.IRepo;
using Lms.IService;
using Lms.Model;
using Lms.Repo;

namespace Lms.Service;

internal class ForumService : IForumService
{
    public IForumRepo ForumRepo { private get; init; }
    public IForumCommentRepo ForumCommentRepo { private get; init; }

    public List<ForumComment> GetForumCommentList(int forumId)
    {
        var commentList = ForumRepo.GetForumCommentListByForum(forumId);
        return commentList;
    }

    public ForumComment PostCommentToForum(ForumComment forumComment)
    {
        var newComment = ForumCommentRepo.CreateNewComment(forumComment);
        return newComment;
    }
}
