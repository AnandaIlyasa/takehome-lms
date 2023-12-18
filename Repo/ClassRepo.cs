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
            "LEFT JOIN " +
                "t_m_user t ON t.id = c.teacher_id " +
            "LEFT JOIN " +
                "t_m_file f ON c.class_image_id = f.id " +
            "LEFT JOIN " +
                "t_r_student_class sc ON c.id = sc.class_id " +
            "LEFT JOIN " +
                "t_m_learning l ON c.id = l.class_id " +
            "LEFT JOIN " +
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
        while (reader.Read())
        {
            var classId = (int)reader["id"];
            var currentClass = classList.Find(c => c.Id == classId);
            if (currentClass == null)
            {
                currentClass = new Class()
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
                    LearningList = new List<Learning>(),
                };
                classList.Add(currentClass);
            }

            Learning? currentLearning = null;
            int? learningId = reader["learning_id"] is int ? (int)reader["learning_id"] : null;
            if (learningId != null)
            {
                currentLearning = currentClass.LearningList.Find(l => l.Id == learningId);
                if (currentLearning == null)
                {
                    currentLearning = new Learning()
                    {
                        Id = (int)learningId,
                        LearningName = (string)reader["learning_name"],
                        LearningDescription = reader["learning_description"] is string ? (string)reader["learning_description"] : null,
                        LearningDate = DateOnly.FromDateTime((DateTime)reader["learning_date"]),
                        SessionList = new List<Session>(),
                    };
                    currentClass.LearningList.Add(currentLearning);
                }
            }

            Session? currentSession = null;
            int? sessionId = reader["session_id"] is int ? (int)reader["session_id"] : null;
            if (sessionId != null)
            {
                currentSession = currentLearning.SessionList.Find(s => s.Id == sessionId);
                if (currentSession == null)
                {
                    currentSession = new Session()
                    {
                        Id = (int)reader["session_id"],
                        SessionName = (string)reader["session_name"],
                        SessionDescription = reader["session_description"] is string ? (string)reader["session_description"] : null,
                        StartTime = TimeOnly.FromTimeSpan((TimeSpan)reader["start_time"]),
                        EndTime = TimeOnly.FromTimeSpan((TimeSpan)reader["end_time"]),
                    };
                    currentLearning.SessionList.Add(currentSession);
                }
            }
        }

        conn.Close();

        return classList;
    }

    List<Class> IClassRepo.GetUnEnrolledClassListByStudent(int studentId)
    {
        const string sqlQuery =
                    "SELECT " +
                        "c.id, " +
                        "c.class_code, " +
                        "c.class_title, " +
                        "c.class_description " +
                    "FROM " +
                        "t_m_class c " +
                    "WHERE " +
                        "c.id NOT IN " +
                        "( " +
                            "SELECT " +
                                "c.id " +
                            "FROM " +
                                "t_m_class c " +
                            "JOIN " +
                                "t_r_student_class sc ON c.id = sc.class_id " +
                            "WHERE " +
                                "sc.student_id = @student_id" +
                        ")";

        var conn = DBHelper.GetConnection();
        conn.Open();

        var sqlCommand = conn.CreateCommand();
        sqlCommand.CommandText = sqlQuery;
        sqlCommand.Parameters.AddWithValue("@student_id", studentId);
        var reader = sqlCommand.ExecuteReader();
        var classList = new List<Class>();
        while (reader.Read())
        {
            var classId = (int)reader["id"];
            var existingClassId = classList.FindIndex(c => c.Id == classId);
            if (existingClassId == -1)
            {
                var studentClass = new Class()
                {
                    Id = classId,
                    ClassCode = (string)reader["class_code"],
                    ClassTitle = (string)reader["class_title"],
                    ClassDescription = (string)reader["class_description"],
                };
                classList.Add(studentClass);
            }
        }

        conn.Close();

        return classList;
    }
}
