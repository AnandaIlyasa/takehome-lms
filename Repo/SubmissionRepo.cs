using Lms.Helper;
using Lms.IRepo;
using Lms.Model;
using System.Diagnostics;

namespace Lms.Repo;

internal class SubmissionRepo : ISubmissionRepo
{
    public DatabaseHelper DBHelper { private get; init; }

    public Submission CreateNewSubmission(Submission submission)
    {
        const string sqlQuery =
            "INSERT INTO " +
                "t_r_submission (student_id, task_id, created_by, created_at, ver, is_active) " +
            "VALUES " +
                "(@student_id, @task_id, @created_by, NOW(), 0, true) " +
            "RETURNING id";

        var conn = DBHelper.GetConnection();
        var sqlCommand = conn.CreateCommand();
        sqlCommand.CommandText = sqlQuery;
        sqlCommand.Parameters.AddWithValue("@student_id", submission.Student.Id);
        sqlCommand.Parameters.AddWithValue("@task_id", submission.Task.Id);
        sqlCommand.Parameters.AddWithValue("@created_by", submission.CreatedBy);

        conn.Open();
        var newSubmissionId = (int)sqlCommand.ExecuteScalar();
        conn.Close();

        submission.Id = newSubmissionId;
        return submission;
    }

    public List<Submission> GetSubmissionListBySession(int sessionId)
    {
        const string sqlQuery =
                    "SELECT " +
                        "sub.id, " +
                        "sub.grade, " +
                        "sub.teacher_notes, " +
                        "sub.task_id, " +
                        "sub.created_at " +
                    "FROM " +
                        "t_r_submission sub " +
                    "LEFT JOIN " +
                        "t_m_task t ON sub.task_id = t.id " +
                    "LEFT JOIN " +
                        "t_m_session s ON t.session_id = s.id " +
                    "WHERE " +
                        "s.id = @session_id";

        var conn = DBHelper.GetConnection();
        conn.Open();

        var sqlCommand = conn.CreateCommand();
        sqlCommand.CommandText = sqlQuery;
        sqlCommand.Parameters.AddWithValue("@session_id", sessionId);
        var reader = sqlCommand.ExecuteReader();
        var submissionList = new List<Submission>();
        while (reader.Read())
        {
            var submissionId = (int)reader["id"];
            var existingSubmissionId = submissionList.FindIndex(s => s.Id == submissionId);
            if (existingSubmissionId == -1)
            {
                var submission = new Submission()
                {
                    Id = submissionId,
                    Grade = reader["grade"] is float ? (float)reader["grade"] : null,
                    TeacherNotes = reader["teacher_notes"] is string ? (string)reader["teacher_notes"] : null,
                    Task = new LMSTask() { Id = (int)reader["task_id"] },
                    CreatedAt = (DateTime)reader["created_at"],
                };
                submissionList.Add(submission);
            }
        }

        conn.Close();

        return submissionList;
    }
}
