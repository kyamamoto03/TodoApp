using FluentValidation;
using TodoApp.API.DTO;

namespace TodoApp.Api.DTO.User.Add;

public class AddRequest : IRequestBase
{
    public string UserId { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string Email { get; set; } = default!;

    public override bool IsValid()
    {
        var validator = new InlineValidator<AddRequest>
            {
                v => v.RuleFor(x => x.UserId).NotEmpty(),
                v => v.RuleFor(x => x.UserName).NotEmpty().MaximumLength(50),
                v => v.RuleFor(x => x.Email).NotEmpty().EmailAddress(),
            };
        validationResult = validator.Validate(this);

        return validationResult.IsValid;
    }
}
