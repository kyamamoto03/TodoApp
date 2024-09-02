namespace TodoApp.Api.Apis;

public class ApiService(ILogger<ApiService> logger)
{
    public ILogger<ApiService> Logger { get; } = logger;
}