using Lms.IRepo;
using Lms.IService;
using Lms.Model;

namespace Lms.Service;

internal class ForumService : IForumService
{
    readonly IForumRepo _forumRepo;
    readonly IForumCommentRepo _forumCommentRepo;

    public ForumService(IForumRepo forumRepo, IForumCommentRepo forumCommentRepo)
    {
        _forumRepo = forumRepo;
        _forumCommentRepo = forumCommentRepo;
    }

    public List<ForumComment> GetForumCommentList(int forumId)
    {
        var commentList = _forumCommentRepo.GetForumCommentListByForum(forumId);
        return commentList;
    }

    public ForumComment PostCommentToForum(ForumComment forumComment)
    {
        var newComment = _forumCommentRepo.CreateNewComment(forumComment);
        return newComment;
    }
}
