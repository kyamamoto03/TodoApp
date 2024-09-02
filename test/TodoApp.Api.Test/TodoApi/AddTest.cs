using Domain.TodoModel;
using Infra;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using TodoApp.Api.Apis;
using TodoApp.API.DTO.Todo.AddTodo;

namespace TodoApp.Api.Test.TodoApi;

public class AddTest : DbInstance
{
    public TodoDbContext CreateTodoDbContext()
    {
        var _db = new TodoDbContext(new DbContextOptionsBuilder<TodoDbContext>()
       .UseNpgsql(DbConnectionString)
       .Options, null);

        return _db;
    }

    public ApiService CreateApiServer()
    {
        var loggerMoq = Substitute.For<ILogger<ApiService>>();
        loggerMoq.LogInformation(It.IsAny<string>());
        loggerMoq.LogWarning(It.IsAny<string>());
        loggerMoq.LogError(It.IsAny<string>());

        var _apiService = new ApiService(loggerMoq);
        return _apiService;
    }

    [Fact]
    public async Task Add_Test()
    {
        // Arrange
        using var _todoDbContext = CreateTodoDbContext();
        ITodoRepository todoRepository = new TodoRepository(_todoDbContext);

        var userId = Guid.NewGuid().ToString();
        var todoId = Guid.NewGuid().ToString();
        var addTodoRequest = new AddTodoRequest()
        {
            TodoId = todoId,
            UserId = userId,
            Title = "Test",
            Description = "Test",
            ScheduleStartDate = DateTime.Now,
            ScheduleEndDate = DateTime.Now,
            TodoItemRequests = new AddTodoRequest.TodoItemRequest[]
            {
                new AddTodoRequest.TodoItemRequest
                {
                    TodoItemId = Guid.NewGuid().ToString(),
                    Title = "Test",
                    ScheduleStartDate = DateTime.Now,
                    ScheduleEndDate = DateTime.Now
                }
            }
        };

        // Act
        await Apis.TodoApi.AddTodoAsync(addTodoRequest, todoRepository, CreateApiServer());

        // Assert
        var findTodo = await todoRepository.FindByIdAsync(addTodoRequest.TodoId);

        Assert.NotNull(findTodo);
        Assert.Equal(addTodoRequest.Title, findTodo.Title);
    }
}