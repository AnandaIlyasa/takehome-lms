using Lms.Constant;
using Lms.Helper;
using Lms.IRepo;
using Lms.Model;
using System.Threading.Tasks;

namespace Lms.Repo;

internal class SessionTaskRepo : ISessionTaskRepo
{
    public DatabaseHelper DBHelper { private get; init; }

    public List<LMSTask> GetSessionTaskListBySession(int sessionId)
    {
        var sqlQuery =
            "SELECT " +
                "t.id, " +
                "t.task_name, " +
                "t.task_description, " +
                "t.duration, " +
                "tf.file_name, " +
                "tff.file_content, " +
                "tff.file_extension, " +
                "q.id AS question_id, " +
                "q.question_type, " +
                "q.question_content, " +
                "mco.option_char, " +
                "mco.option_text, " +
                "mco.is_correct " +
            "FROM " +
                "t_m_task t " +
            "LEFT JOIN " +
                "t_r_task_detail td ON t.id = td.task_id  " +
            "LEFT JOIN " +
                "t_r_task_file tf ON td.task_file_id = tf.id " +
            "LEFT JOIN " +
                "t_m_file tff ON tf.file_id = tff.id " +
            "LEFT JOIN " +
                "t_m_task_question q ON td.task_question_id = q.id " +
            "LEFT JOIN " +
                "t_m_task_multiple_choice_option mco ON q.id = mco.question_id " +
            "WHERE " +
                "t.session_id = 2";

        var conn = DBHelper.GetConnection();
        conn.Open();

        var sqlCommand = conn.CreateCommand();
        sqlCommand.CommandText = sqlQuery;
        sqlCommand.Parameters.AddWithValue("@session_id", sessionId);
        var reader = sqlCommand.ExecuteReader();
        var taskList = new List<LMSTask>();
        while (reader.Read())
        {
            var tId = (int)reader["id"];
            var tFoundIndex = taskList.FindIndex(m => m.Id == tId);
            if (tFoundIndex == -1)
            {
                var task = new LMSTask()
                {
                    Id = tId,
                    TaskName = (string)reader["task_name"],
                    TaskDescription = reader["task_description"] is string ? (string)reader["task_description"] : null,
                    Duration = (int)reader["duration"],
                    TaskFileList = new List<TaskFile>(),
                    TaskQuestionList = new List<TaskQuestion>(),
                };
                taskList.Add(task);
            }

            TaskFile? taskFile = null;
            if (reader["file_content"] is string)
            {
                var file = new LMSFile()
                {
                    FileContent = (string)reader["file_content"],
                    FileExtension = (string)reader["file_extension"],
                };
                taskFile = new TaskFile()
                {
                    FileName = reader["file_name"] is string ? (string)reader["file_name"] : null,
                    File = file,
                };
            }
            else
            {
                TaskQuestion taskQuestion;
                var questionType = (string)reader["question_type"];

                var taskId = -1;
                var questionId = -1;
                var qId = (int)reader["question_id"];
                foreach (var task in taskList)
                {
                    foreach (var question in task.TaskQuestionList)
                    {
                        if (question.Id == qId)
                        {
                            taskId = task.Id;
                            questionId = question.Id;
                            break;
                        }
                    }
                }

                if (taskId == -1 && questionId == -1)
                {
                    taskQuestion = new TaskQuestion()
                    {
                        Id = (int)reader["id"],
                        QuestionType = questionType,
                        QuestionContent = (string)reader["question_content"],
                        OptionList = new List<TaskMultipleChoiceOption>(),
                    };
                }
                else
                {
                    var foundTask = taskList[taskId];
                    var foundQuestion = foundTask.TaskQuestionList[questionId];
                    taskQuestion = foundQuestion;
                }

                if (questionType == QuestionType.MultipleChoice)
                {
                    var choiceOption = new TaskMultipleChoiceOption()
                    {
                        OptionChar = (string)reader["option_char"],
                        OptionText = (string)reader["option_text"],
                        IsCorrect = (bool)reader["is_correct"],
                    };
                    taskQuestion.OptionList.Add(choiceOption);
                }

                if (taskId == -1)
                {
                    taskList[taskList.Count - 1].TaskQuestionList.Add(taskQuestion);
                }
                else
                {
                    taskList[taskId].TaskQuestionList.Add(taskQuestion);
                }
            }

            if (taskFile != null)
            {
                if (tFoundIndex == -1) taskList[taskList.Count - 1].TaskFileList?.Add(taskFile);
                else taskList[tFoundIndex].TaskFileList?.Add(taskFile);
            }
        }

        conn.Close();

        return taskList;
    }
}
