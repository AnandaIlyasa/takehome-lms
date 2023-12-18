using Lms.Helper;
using Lms.Model;

namespace Lms.Repo;

internal class ForumCommentRepo : IForumCommentRepo
{
    public DatabaseHelper DBHelper { private get; init; }

    public ForumComment CreateNewComment(ForumComment forumComment)
    {
        const string sqlQuery =
            "INSERT INTO " +
                "t_r_forum_comment (comment_content, user_id, forum_id, created_by, created_at, ver, is_active) " +
            "VALUES " +
                "(@comment_content, @user_id, @forum_id, 1, NOW(), 0, true) " +
            "RETURNING id";

        var conn = DBHelper.GetConnection();
        var sqlCommand = conn.CreateCommand();
        sqlCommand.CommandText = sqlQuery;
        sqlCommand.Parameters.AddWithValue("@comment_content", forumComment.CommentContent);
        sqlCommand.Parameters.AddWithValue("@user_id", forumComment.User.Id);
        sqlCommand.Parameters.AddWithValue("@forum_id", forumComment.Forum.Id);

        conn.Open();
        var newForumCommentId = (int)sqlCommand.ExecuteScalar();
        conn.Close();

        forumComment.Id = newForumCommentId;
        return forumComment;
    }
}
