﻿namespace Lms.Model;

internal class TaskQuestion : BaseModel
{
    public string QuestionType { get; init; }
    public string? QuestionContent { get; init; }
    public LMSFile? Image { get; init; }
    public List<TaskMultipleChoiceOption> optionList { get; set; }
}