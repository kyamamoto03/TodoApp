using Microsoft.Extensions.Logging;
using NSubstitute;
using TodoApp.Api.Apis;

namespace TodoApp.Api.Test;

internal class ApiServiceFactory
{
    internal static ApiService Create()
    {
        var loggerMoq = Substitute.For<ILogger<ApiService>>();

        var _apiService = new ApiService(loggerMoq);
        return _apiService;
    }
}