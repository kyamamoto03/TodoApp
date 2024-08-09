using FluentValidation;
using TodoApp.API.DTO;

namespace TodoApp.Api.DTO.Todo.StartTodo;

public class StartTodoRequest : IRequestBase
{
    public string TodoId { get; set; } = string.Empty;
    public string TodoItemId { get; set; } = string.Empty;
    public DateTime StartDate { get; set; } = default!;
    public override bool IsValid()
    {
        var validator = new InlineValidator<StartTodoRequest>
        {
            v => v.RuleFor(x => x.TodoId).NotEmpty(),
            v => v.RuleFor(x => x.TodoItemId).NotEmpty(),
            v => v.RuleFor(x => x.StartDate).NotEmpty().GreaterThan(new DateTime(2000,1,1))
        };
        validationResult = validator.Validate(this);

        return validationResult.IsValid;
    }
}
