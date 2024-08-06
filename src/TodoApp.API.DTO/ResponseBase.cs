using FluentValidation.Results;

namespace TodoApp.API.DTO;

public abstract class IResponseBase
{
    private bool _isSuccess;
    public bool IsSuccess => _isSuccess;

    private string _message = string.Empty;

    public string Message => _message;

    public void Success()
    {
        _isSuccess = true;
    }

    public void Fail(string message)
    {
        _isSuccess = false;
        _message = message;
    }
    public void Fail(ValidationResult?  validationResult)
    {
        _isSuccess = false;
        if (validationResult != null)
        {
            _message = validationResult.ToString();
        }
    }
}
