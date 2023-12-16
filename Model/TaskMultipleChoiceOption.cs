namespace Lms.Model;

internal class TaskMultipleChoiceOption : BaseModel
{
    public string OptionChar { get; init; }
    public string? OptionText { get; init; }
    public bool IsCorrect { get; set; }
    public TaskQuestion Question { get; init; }
    public LMSFile? OptionImage { get; init; }
}
