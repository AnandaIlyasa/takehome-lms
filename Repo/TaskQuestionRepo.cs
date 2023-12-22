using Lms.Config;
using Lms.IRepo;
using Lms.Model;
using Microsoft.EntityFrameworkCore;

namespace Lms.Repo;

internal class TaskQuestionRepo : ITaskQuestionRepo
{
    readonly DBContextConfig _context;

    public TaskQuestionRepo(DBContextConfig context)
    {
        _context = context;
    }

    public List<TaskQuestion> GetQuestionListByTask(int taskId)
    {
        var questionList = _context.TaskQuestions
                            .FromSql($@"
                                SELECT
	                                q.*,
                                    q.xmin
                                FROM
	                                t_m_task_question q 
                                INNER JOIN
	                                t_r_task_detail td ON q.id = td.task_question_id 
                                WHERE 
	                                td.task_id = {taskId}
                            ")
                            .ToList();
        return questionList;
    }
}
