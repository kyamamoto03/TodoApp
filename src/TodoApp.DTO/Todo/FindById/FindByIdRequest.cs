using FluentValidation;

namespace TodoApp.API.DTO.Todo.FindById;

public class FindByIdRequest : IRequestBase
{
    public string TodoId { get; set; } = string.Empty;

    public override bool IsValid()
    {
        var validator = new InlineValidator<FindByIdRequest>
            {
                v => v.RuleFor(x => x.TodoId).NotEmpty()

            };
        validationResult = validator.Validate(this);

        return validationResult.IsValid;

    }
}
