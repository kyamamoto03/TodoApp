using FluentValidation;
using TodoApp.API.DTO;

namespace TodoApp.Api.DTO.Todo.GetStatus;

public class GetStatusRequest : IRequestBase
{
    public string TodoId { get; set; } = default!;

    public int Value { get; set; }

    public override bool IsValid()
    {
        var validator = new InlineValidator<GetStatusRequest>
        {
            v => v.RuleFor(x => x.TodoId).NotEmpty(),
        };
        validationResult = validator.Validate(this);

        return validationResult.IsValid;

    }
}

