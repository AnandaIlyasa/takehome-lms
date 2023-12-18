using Lms.Model;

namespace Lms.IRepo;

internal interface IForumRepo
{
    List<ForumComment> GetForumCommentListByForum(int forumId);
}
