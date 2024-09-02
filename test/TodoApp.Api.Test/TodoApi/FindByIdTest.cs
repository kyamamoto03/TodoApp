using Domain.TodoModel;
using Infra;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;
using TodoApp.API.DTO.Todo.AddTodo;

namespace TodoApp.Api.Test.TodoApi;

public class FindByIdTest : DbInstance
{
    public TodoDbContext CreateTodoDbContext()
    {
        var _db = new TodoDbContext(new DbContextOptionsBuilder<TodoDbContext>()
       .UseNpgsql(DbConnectionString)
       .Options, null);

        return _db;
    }

    [Fact]
    public async Task 検索_あり_Test()
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
        await Apis.TodoApi.AddTodoAsync(addTodoRequest, todoRepository, ApiServiceFactory.Create());

        // Act
        var findTodo = await todoRepository.FindByIdAsync(todoId);

        // Assert
        Assert.NotNull(findTodo);
        Assert.Equal(todoId, findTodo.TodoId);
        Assert.Equal(addTodoRequest.Title, findTodo.Title);
        Assert.Equal(addTodoRequest.Description, findTodo.Description);
        Assert.Equal(addTodoRequest.ScheduleStartDate, findTodo.ScheduleStartDate);
        Assert.Equal(addTodoRequest.ScheduleEndDate, findTodo.ScheduleEndDate);
    }

    [Fact]
    public async Task 検索_なし_Test()
    {
        // Arrange
        using var _todoDbContext = CreateTodoDbContext();
        ITodoRepository todoRepository = new TodoRepository(_todoDbContext);

        var 存在しないTodoId = Guid.NewGuid().ToString(); ;
        // Act
        var findTodo = await todoRepository.FindByIdAsync(存在しないTodoId);
        // Assert
        Assert.Null(findTodo);
    }
}