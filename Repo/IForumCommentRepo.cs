using Lms.Model;

namespace Lms.Repo;

internal interface IForumCommentRepo
{
    ForumComment CreateNewComment(ForumComment forumComment);
}
