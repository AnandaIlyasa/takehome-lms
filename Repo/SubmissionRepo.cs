using Lms.Helper;
using Lms.IRepo;
using Lms.Model;

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
}
