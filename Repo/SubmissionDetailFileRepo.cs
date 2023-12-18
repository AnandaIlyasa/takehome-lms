using Lms.Helper;
using Lms.IRepo;
using Lms.Model;

namespace Lms.Repo;

internal class SubmissionDetailFileRepo : ISubmissionDetailFileRepo
{
    public DatabaseHelper DBHelper { private get; init; }

    public SubmissionDetailFile CreateNewSubmissionDetailFile(SubmissionDetailFile submissionDetailFile)
    {
        const string sqlQuery =
            "INSERT INTO " +
                "t_r_submission_detail_file (submission_id, file_id, created_by, created_at, ver, is_active) " +
            "VALUES " +
                "(@submission_id, @file_id, @created_by, NOW(), 0, true) " +
            "RETURNING id";

        var conn = DBHelper.GetConnection();
        var sqlCommand = conn.CreateCommand();
        sqlCommand.CommandText = sqlQuery;
        sqlCommand.Parameters.AddWithValue("@submission_id", submissionDetailFile.Submission.Id);
        sqlCommand.Parameters.AddWithValue("@file_id", submissionDetailFile.File.Id);
        sqlCommand.Parameters.AddWithValue("@created_by", submissionDetailFile.CreatedBy);

        conn.Open();
        var newSubmissionDetailFileId = (int)sqlCommand.ExecuteScalar();
        conn.Close();

        submissionDetailFile.Id = newSubmissionDetailFileId;
        return submissionDetailFile;
    }
}
