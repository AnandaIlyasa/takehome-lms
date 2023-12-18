using Lms.Helper;
using Lms.IRepo;
using Lms.Model;

namespace Lms.Repo;

internal class ForumRepo : IForumRepo
{
    public DatabaseHelper DBHelper { private get; init; }

    public List<ForumComment> GetForumCommentListByForum(int forumId)
    {
        const string sqlQuery =
                    "SELECT " +
                        "fc.id, " +
                        "fc.comment_content, " +
                        "fc.created_at, " +
                        "u.full_name " +
                    "FROM " +
                        "t_r_forum_comment fc " +
                    "JOIN " +
                        "t_m_user u ON fc.user_id = u.id " +
                    "WHERE " +
                        "fc.forum_id = @forum_id";

        var conn = DBHelper.GetConnection();
        conn.Open();

        var sqlCommand = conn.CreateCommand();
        sqlCommand.CommandText = sqlQuery;
        sqlCommand.Parameters.AddWithValue("@forum_id", forumId);
        var reader = sqlCommand.ExecuteReader();
        var commentList = new List<ForumComment>();
        while (reader.Read())
        {
            var forumComment = new ForumComment()
            {
                Id = (int)reader["id"],
                CommentContent = (string)reader["comment_content"],
                User = new User() { FullName = (string)reader["full_name"] },
                CreatedAt = (DateTime)reader["created_at"],
            };
            commentList.Add(forumComment);
        }

        conn.Close();

        return commentList;
    }
}
