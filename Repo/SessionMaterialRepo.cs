using Lms.Helper;
using Lms.IRepo;
using Lms.Model;

namespace Lms.Repo;

internal class SessionMaterialRepo : ISessionMaterialRepo
{
    public DatabaseHelper DBHelper { private get; init; }

    public List<SessionMaterial> GetSessionMaterialListBySession(int sessionId)
    {
        var sqlQuery =
            "SELECT " +
                "m.id, " +
                "m.material_name, " +
                "m.material_description, " +
                "mf.id AS mf_id, " +
                "mf.file_name, " +
                "mff.file_content, " +
                "mff.file_extension " +
            "FROM " +
                "t_m_session_material m " +
            "LEFT JOIN " +
                "t_r_session_material_file mf ON m.id = mf.material_id " +
            "LEFT JOIN " +
                "t_m_file mff ON mf.file_id = mff.id " +
            "WHERE " +
                "m.session_id = @session_id";

        var conn = DBHelper.GetConnection();
        conn.Open();

        var sqlCommand = conn.CreateCommand();
        sqlCommand.CommandText = sqlQuery;
        sqlCommand.Parameters.AddWithValue("@session_id", sessionId);
        var reader = sqlCommand.ExecuteReader();
        var materialList = new List<SessionMaterial>();
        while (reader.Read())
        {
            SessionMaterialFile? materialFile = null;
            if (reader["file_content"] is string)
            {
                var file = new LMSFile()
                {
                    FileContent = (string)reader["file_content"],
                    FileExtension = (string)reader["file_extension"],
                };
                materialFile = new SessionMaterialFile()
                {
                    Id = (int)reader["mf_id"],
                    FileName = reader["file_name"] is string ? (string)reader["file_name"] : null,
                    File = file,
                };
            }

            var mId = (int)reader["id"];
            var mFoundIndex = materialList.FindIndex(m => m.Id == mId);
            if (mFoundIndex == -1)
            {
                var material = new SessionMaterial()
                {
                    Id = mId,
                    MaterialName = (string)reader["material_name"],
                    MaterialDescription = reader["material_description"] is string ? (string)reader["material_description"] : null,
                    MaterialFileList = new List<SessionMaterialFile>(),
                };
                if (materialFile != null) material.MaterialFileList.Add(materialFile);
                materialList.Add(material);
            }
            else
            {
                if (materialFile != null) materialList[mFoundIndex].MaterialFileList?.Add(materialFile);
            }
        }

        conn.Close();

        return materialList;
    }
}
