using Domain.TodoModel;
using Infra;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;
using TodoApp.API.DTO.Todo.AddTodo;

namespace TodoApp.Api.Test.TodoApi;

public class AddTest : DbInstance
{
    public TodoDbContext CreateTodoDbContext()
    {
        var _db = new TodoDbContext(new DbContextOptionsBuilder<TodoDbContext>()
           .UseNpgsql(DbConnectionString)
           .Options);

        return _db;
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
        await Apis.TodoApi.AddTodoAsync(addTodoRequest, todoRepository, ApiServiceFactory.Create());

        // Assert
        var findTodo = await todoRepository.FindByIdAsync(addTodoRequest.TodoId);

        Assert.NotNull(findTodo);
        Assert.Equal(addTodoRequest.Title, findTodo.Title);
    }
}