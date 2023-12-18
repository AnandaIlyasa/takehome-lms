using Lms.Helper;
using Lms.IRepo;
using Lms.Model;

namespace Lms.Repo;

internal class StudentClassRepo : IStudentClassRepo
{
    public DatabaseHelper DBHelper { private get; init; }

    public StudentClass CreateNewStudentClass(StudentClass studentClass)
    {
        const string sqlQuery =
            "INSERT INTO " +
                "t_r_student_class (class_id, student_id, created_by, created_at, ver, is_active) " +
            "VALUES " +
                "(@class_id, @student_id, 1, NOW(), 0, true) " +
            "RETURNING id";

        var conn = DBHelper.GetConnection();
        var sqlCommand = conn.CreateCommand();
        sqlCommand.CommandText = sqlQuery;
        sqlCommand.Parameters.AddWithValue("@class_id", studentClass.Class.Id);
        sqlCommand.Parameters.AddWithValue("@student_id", studentClass.Student.Id);
        sqlCommand.Parameters.AddWithValue("@created_by", studentClass.Student.Id);

        conn.Open();
        var newStudentClassId = (int)sqlCommand.ExecuteScalar();
        conn.Close();

        studentClass.Id = newStudentClassId;
        return studentClass;
    }
}
