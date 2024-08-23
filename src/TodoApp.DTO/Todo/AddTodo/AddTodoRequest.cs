using FluentValidation;

namespace TodoApp.API.DTO.Todo.AddTodo;

public class AddTodoRequest : IRequestBase
{
    public string UserId { get; set; } = default!;
    public string TodoId { get; init; } = default!;
    public string Title { get; init; } = default!;
    public string Description { get; init; } = default!;
    public DateTime ScheduleStartDate { get; init; } = default!;
    public DateTime ScheduleEndDate { get; init; } = default!;

    public TodoItemRequest[] TodoItemRequests { get; init; } = default!;

    public record TodoItemRequest
    {
        public string TodoItemId { get; init; }
        public string Title { get; init; } = default!;
        public DateTime ScheduleStartDate { get; init; } = default!;
        public DateTime ScheduleEndDate { get; init; } = default!;
    }

    public override bool IsValid()
    {
        var validator = new InlineValidator<AddTodoRequest>
            {
                v => v.RuleFor(x => x.UserId).NotEmpty(),
                v => v.RuleFor(x => x.TodoId).NotEmpty(),
                v => v.RuleFor(x => x.Title).NotEmpty().MaximumLength(50),
                v => v.RuleFor(x => x.Description).MaximumLength(500),
                v => v.RuleFor(x => x.ScheduleStartDate).NotEmpty(),
                v => v.RuleFor(x => x.ScheduleEndDate).NotEmpty(),
            };
        validationResult = validator.Validate(this);

        return validationResult.IsValid;

    }

}
