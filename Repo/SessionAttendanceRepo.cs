using Lms.Config;
using Lms.Helper;
using Lms.IRepo;
using Lms.Model;

namespace Lms.Repo;

internal class SessionAttendanceRepo : ISessionAttendanceRepo
{
    public DatabaseHelper DBHelper { private get; init; }
    readonly DBContextConfig _context;

    public SessionAttendanceRepo(DBContextConfig context)
    {
        _context = context;
    }

    public SessionAttendance CreateNewSessionAttendance(SessionAttendance sessionAttendance)
    {
        _context.SessionAttendances.Add(sessionAttendance);
        _context.SaveChanges();
        return sessionAttendance;
    }

    public SessionAttendance? GetSessionAttendanceStatus(int sessionId, int studentId)
    {
        //const string sqlQuery =
        //    "SELECT  " +
        //        "is_approved " +
        //    "FROM  " +
        //        "t_r_session_attendance " +
        //    "WHERE " +
        //        "student_id = @student_id " +
        //        "AND session_id = @session_id";

        //var conn = DBHelper.GetConnection();
        //var sqlCommand = conn.CreateCommand();
        //sqlCommand.CommandText = sqlQuery;
        //sqlCommand.Parameters.AddWithValue("@student_id", sessionAttendance.Student.Id);
        //sqlCommand.Parameters.AddWithValue("@session_id", sessionAttendance.Session.Id);

        //conn.Open();
        //var reader = sqlCommand.ExecuteReader();
        //if (reader.Read())
        //{
        //    var isApproved = (bool?)reader["is_approved"];
        //    if (isApproved != null)
        //    {
        //        sessionAttendance.IsApproved = (bool)isApproved;
        //    }
        //}
        //else
        //{
        //    sessionAttendance = null;
        //}

        //conn.Close();
        var sessionAttendance = _context.SessionAttendances
                                .Where(sa => sa.StudentId == studentId && sa.SessionId == sessionId)
                                .FirstOrDefault();
        return sessionAttendance;
    }
}
