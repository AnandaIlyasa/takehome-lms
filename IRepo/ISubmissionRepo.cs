﻿using Lms.Config;
using Lms.Model;

namespace Lms.IRepo;

internal interface ISubmissionRepo
{
    Submission GetStudentSubmissionByTask(int studentId, int taskId);
    Submission CreateNewSubmission(Submission submission);
    int UpdateSubmissionGradeAndNotes(Submission submission);
    List<Submission> GetStudentSubmissionListBySession(int sessionId, int studentId);
    List<Submission> GetSubmissionListBySession(int sessionId);
}
