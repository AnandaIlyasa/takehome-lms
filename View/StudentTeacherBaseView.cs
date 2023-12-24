using Lms.IService;
using Lms.Model;

namespace Lms.View;

abstract class StudentTeacherBaseView
{
    protected readonly string DateFormat = "yyyy-MM-dd";
    protected readonly string DateTimeFormat = "yyyy-MM-dd hh:mm:ss";

    public void ForumCommentMenu(Forum forum, IForumService forumService)
    {
        while (true)
        {
            Console.WriteLine("\n---- " + forum.ForumName + " ----");
            var commentList = forumService.GetForumCommentList(forum.Id);
            foreach (var comment in commentList)
            {
                Console.WriteLine($"{comment.User.FullName} - {comment.CommentContent} ({comment.CreatedAt.ToString(DateTimeFormat)})");
            }
            Console.WriteLine("\n1. Post New Comment");
            Console.WriteLine("2. Back");
            var selectedOpt = Utils.Utils.GetNumberInputUtil(1, 2);

            if (selectedOpt == 1)
            {
                var comment = Utils.Utils.GetStringInputUtil("Your comment");
                var forumComment = new ForumComment()
                {
                    CommentContent = comment,
                    Forum = forum,
                };
                forumService.PostCommentToForum(forumComment);
            }
            else
            {
                break;
            }
        }
    }
}
