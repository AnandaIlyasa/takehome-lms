﻿namespace Lms.Model;

internal class SubmissionDetail : BaseModel
{
    public string? EssayAnswerContent { get; init; }
    public Submission Submission { get; init; }
    public TaskQuestion Question { get; init; }
    public User Student { get; init; }
    public TaskMultipleChoiceOption? ChoiceOption { get; init; }
}
