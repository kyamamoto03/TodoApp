using Domain.TodoModel;
using Infra;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;
using TodoApp.Api.Usecase.TodoUsecase.Add;


namespace TodoApp.Api.Usecase.Test.TodoUsecase;

public class AddTest : DbInstance
{
    public TodoDbContext CreateTodoDbContext()
    {
        var _db = new TodoDbContext(new DbContextOptionsBuilder<TodoDbContext>()
       .UseNpgsql(DbConnectionString)
       .Options, null);

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
        var addTodoCommand = new AddTodoCommand()
        {
            TodoId = todoId,
            UserId = userId,
            Title = "Test",
            Description = "Test",
            ScheduleStartDate = DateTime.Now,
            ScheduleEndDate = DateTime.Now,
            TodoItems = new AddTodoCommand.TodoItem[]
            {
                new AddTodoCommand.TodoItem
                {
                    TodoItemId = Guid.NewGuid().ToString(),
                    Title = "Test",
                    ScheduleStartDate = DateTime.Now,
                    ScheduleEndDate = DateTime.Now
                }
            }
        };

        var usecase = new AddTodoUsecase(todoRepository);

        // Act
        await usecase.ExecuteAsync(addTodoCommand);

        // Assert
        var findTodo = await todoRepository.FindByIdAsync(addTodoCommand.TodoId);

        Assert.NotNull(findTodo);
        Assert.Equal(addTodoCommand.Title, findTodo.Title);
    }
}
