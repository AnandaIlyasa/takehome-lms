namespace Lms.Repo;

using Lms.Helper;
using Lms.IRepo;
using Lms.Model;

internal class LMSFileRepo : ILMSFileRepo
{
    public DatabaseHelper DBHelper { private get; init; }

    public LMSFile CreateNewFile(LMSFile file)
    {
        const string sqlQuery =
            "INSERT INTO " +
                "t_m_file (file_content, file_extension, created_by, created_at, ver, is_active) " +
            "VALUES " +
                "(@file_content, @file_extension, @created_by, NOW(), 0, true) " +
            "RETURNING id";

        var conn = DBHelper.GetConnection();
        var sqlCommand = conn.CreateCommand();
        sqlCommand.CommandText = sqlQuery;
        sqlCommand.Parameters.AddWithValue("@file_content", file.FileContent);
        sqlCommand.Parameters.AddWithValue("@file_extension", file.FileExtension);
        sqlCommand.Parameters.AddWithValue("@created_by", file.CreatedBy);

        conn.Open();
        var newFileId = (int)sqlCommand.ExecuteScalar();
        conn.Close();

        file.Id = newFileId;
        return file;
    }
}
