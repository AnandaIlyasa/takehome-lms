using Lms.Model;

namespace Lms.IRepo;

internal interface IForumCommentRepo
{
    ForumComment CreateNewComment(ForumComment forumComment);
    List<ForumComment> GetForumCommentListByForum(int forumId);
}
