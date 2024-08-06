using FluentValidation;
using FluentValidation.Results;

namespace TodoApp.API.DTO;

public abstract class IRequestBase
{
    public abstract bool IsValid();

    public ValidationResult? validationResult { get; set; } = null;

}


