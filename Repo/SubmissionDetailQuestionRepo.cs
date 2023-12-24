﻿using Lms.Config;
using Lms.IRepo;
using Lms.Model;
using Microsoft.EntityFrameworkCore;

namespace Lms.Repo;

internal class SubmissionDetailQuestionRepo : ISubmissionDetailQuestionRepo
{
    readonly DBContextConfig _context;

    public SubmissionDetailQuestionRepo(DBContextConfig context)
    {
        _context = context;
    }

    public SubmissionDetailQuestion CreateNewSubmissionDetailQuestion(SubmissionDetailQuestion submissionDetailQuestion)
    {
        _context.SubmissionDetailQuestions.Add(submissionDetailQuestion);
        _context.SaveChanges();
        return submissionDetailQuestion;
    }

    public List<SubmissionDetailQuestion> GetStudentSubmissionDetailQuestionByTask(int taskId, int studentId)
    {
        var submissionQuestionList = _context.SubmissionDetailQuestions
                                    .Join(
                                        _context.Submissions,
                                        sdq => sdq.SubmissionId,
                                        s => s.Id,
                                        (sdq, s) => new { sdq, s }
                                    )
                                    .Where(sdqs => sdqs.s.TaskId == taskId && sdqs.s.StudentId == studentId)
                                    .Select(sdqs => sdqs.sdq)
                                    .Include(sdq => sdq.ChoiceOption)
                                    .ToList();
        return submissionQuestionList;
    }
}
