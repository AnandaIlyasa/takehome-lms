using Lms.Helper;
using Lms.IRepo;
using Lms.Model;

namespace Lms.Repo;

internal class SessionRepo : ISessionRepo
{
    public DatabaseHelper DBHelper { private get; init; }

    public Session GetSessionById(int sessionId)
    {
        var sqlQuery =
            "SELECT " +
                "s.id, " +
                "s.session_name, " +
                "s.session_description, " +
                "s.start_time, " +
                "s.end_time, " +
                "f.id AS forum_id, " +
                "f.forum_name " +
            "FROM " +
                "t_m_session s " +
            "LEFT JOIN " +
                "t_m_forum f ON s.id = f.session_id " +
            "WHERE " +
                "s.id = @session_id";

        var conn = DBHelper.GetConnection();
        conn.Open();

        var sqlCommand = conn.CreateCommand();
        sqlCommand.CommandText = sqlQuery;
        sqlCommand.Parameters.AddWithValue("@session_id", sessionId);
        var reader = sqlCommand.ExecuteReader();
        Session session = new Session();
        if (reader.Read())
        {
            session.Id = (int)reader["id"];
            session.SessionName = (string)reader["session_name"];
            session.SessionDescription = reader["session_description"] is string ? (string)reader["session_description"] : null;
            session.StartTime = TimeOnly.FromTimeSpan((TimeSpan)reader["start_time"]);
            session.EndTime = TimeOnly.FromTimeSpan((TimeSpan)reader["end_time"]);
        }

        conn.Close();

        return session;
    }
}
