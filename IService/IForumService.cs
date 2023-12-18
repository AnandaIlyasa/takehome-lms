using Lms.Model;

namespace Lms.IService;

internal interface IForumService
{
    List<ForumComment> GetForumCommentList(int forumId);
    ForumComment PostCommentToForum(ForumComment forumComment);
}
