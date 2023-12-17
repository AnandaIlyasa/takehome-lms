using Lms.Helper;
using Lms.IRepo;
using Lms.Model;

namespace Lms.Repo;

internal class SessionAttendanceRepo : ISessionAttendanceRepo
{
    public DatabaseHelper DBHelper { private get; init; }

    public SessionAttendance CreateNewSessionAttendance(SessionAttendance sessionAttendance)
    {
        const string sqlQuery =
            "INSERT INTO " +
                "t_r_session_attendance (is_approved, student_id, session_id, created_by, created_at, ver, is_active) " +
            "VALUES " +
                "(@is_approved, @student_id, @session_id, 1, NOW(), 0, true) " +
            "RETURNING id";

        var conn = DBHelper.GetConnection();
        var sqlCommand = conn.CreateCommand();
        sqlCommand.CommandText = sqlQuery;
        sqlCommand.Parameters.AddWithValue("@is_approved", sessionAttendance.IsApproved);
        sqlCommand.Parameters.AddWithValue("@student_id", sessionAttendance.Student.Id);
        sqlCommand.Parameters.AddWithValue("@session_id", sessionAttendance.Session.Id);

        conn.Open();
        var newSessionAttendanceId = (int)sqlCommand.ExecuteScalar();
        conn.Close();

        sessionAttendance.Id = newSessionAttendanceId;
        return sessionAttendance;
    }

    public SessionAttendance? GetSessionAttendanceStatus(SessionAttendance sessionAttendance)
    {
        const string sqlQuery =
            "SELECT  " +
                "is_approved " +
            "FROM  " +
                "t_r_session_attendance " +
            "WHERE " +
                "student_id = @student_id " +
                "AND session_id = @session_id";

        var conn = DBHelper.GetConnection();
        var sqlCommand = conn.CreateCommand();
        sqlCommand.CommandText = sqlQuery;
        sqlCommand.Parameters.AddWithValue("@student_id", sessionAttendance.Student.Id);
        sqlCommand.Parameters.AddWithValue("@session_id", sessionAttendance.Session.Id);

        conn.Open();
        var reader = sqlCommand.ExecuteReader();
        if (reader.Read())
        {
            if ((bool?)reader["is_approved"] != null)
            {
                sessionAttendance.IsApproved = (bool)reader["is_approved"];
            }
            else
            {
                return null;
            }
        }
        conn.Close();

        return sessionAttendance;
    }
}
