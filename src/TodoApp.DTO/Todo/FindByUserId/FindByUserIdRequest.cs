using FluentValidation;
using TodoApp.API.DTO;

namespace TodoApp.Api.DTO.Todo.FindByUserId;

public class FindByUserIdRequest : IRequestBase
{
    public string UserId { get; set; } = default!;

    public override bool IsValid()
    {
        var validator = new InlineValidator<FindByUserIdRequest>
            {
                v => v.RuleFor(x => x.UserId).NotEmpty()
            };
        validationResult = validator.Validate(this);

        return validationResult.IsValid;
    }
}