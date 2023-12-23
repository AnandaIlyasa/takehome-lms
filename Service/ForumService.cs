using Lms.IRepo;
using Lms.IService;
using Lms.Model;

namespace Lms.Service;

internal class ForumService : IForumService
{
    readonly IForumCommentRepo _forumCommentRepo;

    public ForumService(IForumCommentRepo forumCommentRepo)
    {
        _forumCommentRepo = forumCommentRepo;
    }

    public List<ForumComment> GetForumCommentList(int forumId)
    {
        var commentList = _forumCommentRepo.GetForumCommentListByForum(forumId);
        commentList = commentList
                    .OrderBy(c => c.CreatedAt)
                    .ToList();
        return commentList;
    }

    public ForumComment PostCommentToForum(ForumComment forumComment)
    {
        var newComment = _forumCommentRepo.CreateNewComment(forumComment);
        return newComment;
    }
}
