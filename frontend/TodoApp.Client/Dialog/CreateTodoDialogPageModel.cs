using FluentValidation;
using FluentValidation.Results;

namespace TodoApp.Client.Dialog;

public class CreateTodoDialogPageModel
{
    [System.ComponentModel.DataAnnotations.Required]
    public string Title { get; set; }

    public string Description { get; set; }
    public DateTime ScheduleStartDate { get; set; }
    public DateTime ScheduleEndDate { get; set; }

    public bool IsValid()
    {
        var validator = new InlineValidator<CreateTodoDialogPageModel>
            {
                v => v.RuleFor(x => x.Title)
                .NotEmpty().WithMessage("タイトルを入力してください"),
            };
        ValidationResult = validator.Validate(this);

        return ValidationResult.IsValid;
    }

    public ValidationResult? ValidationResult { get; set; } = null;
}