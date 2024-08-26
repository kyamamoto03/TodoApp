namespace Domain.Exceptions;

public class TodoDoaminExceptioon : Exception
{
    public TodoDoaminExceptioon()
    { }

    public TodoDoaminExceptioon(string message)
        : base(message)
    { }

    public TodoDoaminExceptioon(string message, Exception innerException)
        : base(message, innerException)
    { }
}
