using Lms.Helper;
using Lms.IRepo;
using Lms.IService;
using Lms.Model;

namespace Lms.Service;

internal class ForumService : IForumService
{
    readonly IForumCommentRepo _forumCommentRepo;
    readonly SessionHelper _sessionHelper;

    public ForumService(IForumCommentRepo forumCommentRepo, SessionHelper sessionHelper)
    {
        _forumCommentRepo = forumCommentRepo;
        _sessionHelper = sessionHelper;
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
        forumComment.UserId = _sessionHelper.UserId;
        forumComment.CreatedBy = _sessionHelper.UserId;
        forumComment.CreatedAt = DateTime.Now;
        var newComment = _forumCommentRepo.CreateNewComment(forumComment);
        return newComment;
    }
}
