using Lms.Helper;
using Lms.IRepo;
using Lms.Model;

namespace Lms.Repo;

internal class SubmissionDetailRepo : ISubmissionDetailRepo
{
    public DatabaseHelper DBHelper { private get; init; }

    public SubmissionDetail CreateNewSubmissionDetail(SubmissionDetail submissionDetail)
    {
        const string sqlQuery =
            "INSERT INTO " +
                "t_r_submission_detail (submission_id, question_id, essay_answer_content, choice_option_id, created_by, created_at, ver, is_active) " +
            "VALUES " +
                "(@submission_id, @question_id, @essay_answer_content, @choice_option_id, @created_by, NOW(), 0, true) " +
            "RETURNING id";

        var conn = DBHelper.GetConnection();
        var sqlCommand = conn.CreateCommand();
        sqlCommand.CommandText = sqlQuery;
        sqlCommand.Parameters.AddWithValue("@submission_id", submissionDetail.Submission.Id);
        sqlCommand.Parameters.AddWithValue("@question_id", submissionDetail.Question.Id);
        sqlCommand.Parameters.AddWithValue("@essay_answer_content", submissionDetail.EssayAnswerContent == null ? DBNull.Value : submissionDetail.EssayAnswerContent);
        sqlCommand.Parameters.AddWithValue("@choice_option_id", submissionDetail.ChoiceOption == null ? DBNull.Value : submissionDetail.ChoiceOption.Id);
        sqlCommand.Parameters.AddWithValue("@created_by", submissionDetail.CreatedBy);

        conn.Open();
        var newSubmissionDetailId = (int)sqlCommand.ExecuteScalar();
        conn.Close();

        submissionDetail.Id = newSubmissionDetailId;
        return submissionDetail;
    }
}
