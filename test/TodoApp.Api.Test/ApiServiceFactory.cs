using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using TodoApp.Api.Apis;

namespace TodoApp.Api.Test;

internal class ApiServiceFactory
{
    internal static ApiService Create()
    {
        var loggerMoq = Substitute.For<ILogger<ApiService>>();
        loggerMoq.LogInformation(It.IsAny<string>());
        loggerMoq.LogWarning(It.IsAny<string>());
        loggerMoq.LogError(It.IsAny<string>());

        var _apiService = new ApiService(loggerMoq);
        return _apiService;
    }
}