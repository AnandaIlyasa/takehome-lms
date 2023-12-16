using Lms.Helper;
using Lms.IRepo;
using Lms.Model;

namespace Lms.Repo;

internal class ClassRepo : IClassRepo
{
    public DatabaseHelper DBHelper { private get; init; }

    List<Class> IClassRepo.GetClassListByStudent(int studentId)
    {
        const string sqlQuery =
            "SELECT " +
                "c.id, " +
                "c.class_code, " +
                "c.class_title, " +
                "c.class_description, " +
                "t.full_name, " +
                "f.file_content, " +
                "f.file_extension, " +
                "l.id AS learning_id, " +
                "l.learning_name, " +
                "l.learning_description, " +
                "l.learning_date, " +
                "s.id AS session_id, " +
                "s.session_name, " +
                "s.session_description, " +
                "s.start_time, " +
                "s.end_time " +
            "FROM " +
                "t_m_class c " +
            "INNER JOIN " +
                "t_m_user t ON t.id = c.teacher_id " +
            "INNER JOIN " +
                "t_m_file f ON c.class_image_id = f.id " +
            "INNER JOIN " +
                "t_r_student_class sc ON c.id = sc.class_id " +
            "INNER JOIN " +
                "t_m_learning l ON c.id = l.class_id " +
            "INNER JOIN " +
                "t_m_session s ON l.id = s.learning_id " +
            "WHERE " +
                "sc.student_id = @student_id";

        var conn = DBHelper.GetConnection();
        conn.Open();

        var sqlCommand = conn.CreateCommand();
        sqlCommand.CommandText = sqlQuery;
        sqlCommand.Parameters.AddWithValue("@student_id", studentId);
        var reader = sqlCommand.ExecuteReader();
        var classList = new List<Class>();
        var learningList = new List<Learning>();
        while (reader.Read())
        {

            var session = new Session()
            {
                Id = (int)reader["session_id"],
                SessionName = (string)reader["session_name"],
                SessionDescription = reader["session_description"] is string ? (string)reader["session_description"] : null,
                StartTime = TimeOnly.FromTimeSpan((TimeSpan)reader["start_time"]),
                EndTime = TimeOnly.FromTimeSpan((TimeSpan)reader["end_time"]),
            };

            var learningId = (int)reader["learning_id"];
            var existingLearningId = learningList.FindIndex(l => l.Id == learningId);
            if (existingLearningId == -1)
            {
                var learning = new Learning()
                {
                    Id = learningId,
                    LearningName = (string)reader["learning_name"],
                    LearningDescription = reader["learning_description"] is string ? (string)reader["learning_description"] : null,
                    LearningDate = DateOnly.FromDateTime((DateTime)reader["learning_date"]),
                    SessionList = new List<Session> { session }
                };
                learningList.Add(learning);
            }
            else
            {
                learningList[existingLearningId].SessionList.Add(session);
            }

            var classId = (int)reader["id"];
            var existingClassId = classList.FindIndex(c => c.Id == classId);
            if (existingClassId == -1)
            {
                var studentClass = new Class()
                {
                    Id = classId,
                    Teacher = new User() { FullName = (string)reader["full_name"] },
                    ClassCode = (string)reader["class_code"],
                    ClassTitle = (string)reader["class_title"],
                    ClassDescription = (string)reader["class_description"],
                    ClassImage = new LMSFile()
                    {
                        FileContent = (string)reader["file_content"],
                        FileExtension = (string)reader["file_extension"],
                    },
                    LearningList = learningList,
                };
                classList.Add(studentClass);
            }
            else
            {
                classList[existingClassId].LearningList = learningList;
            }
        }

        conn.Close();

        return classList;
    }

    List<Class> IClassRepo.GetUnEnrolledClassList()
    {
        throw new NotImplementedException();
    }
}
