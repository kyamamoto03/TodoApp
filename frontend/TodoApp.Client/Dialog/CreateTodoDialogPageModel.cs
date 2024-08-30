using FluentValidation;

namespace TodoApp.Client.Dialog;

public class CreateTodoDialogPageModelValidator : AbstractValidator<CreateTodoDialogPageModel>
{
    public CreateTodoDialogPageModelValidator()
    {
        RuleFor(x => x.Title)
               .NotEmpty().WithMessage("タイトルを入力してください");
        RuleFor(x => x.ScheduleStartDate)
                .NotEmpty()
                .WithMessage("開始日を入力してください");
        RuleFor(x => x.ScheduleEndDate)
                .NotEmpty()
                .WithMessage("終了日を入力してください");
    }
}

public class CreateTodoDialogPageModel
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime? ScheduleStartDate { get; set; } = null;
    public DateTime? ScheduleEndDate { get; set; } = null;
}