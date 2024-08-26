using Domain.TodoModel;
using Infra;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;
using TodoApp.Api.Service.TodoService.Add;
using TodoApp.Api.Service.TodoService.FindById;

namespace TodoApp.Api.Service.Test.TodoService;

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

        var addTodoService = new AddTodoService(todoRepository);
        await addTodoService.ExecuteAsync(addTodoCommand);

        // Act
        var findByIdService = new FindByIdService(todoRepository);
        var findTodo = await findByIdService.ExecuteAsync(todoId);

        // Assert
        Assert.NotNull(findTodo);
        Assert.Equal(todoId, findTodo.TodoId);
        Assert.Equal(addTodoCommand.Title, findTodo.Title);
        Assert.Equal(addTodoCommand.Description, findTodo.Description);
        Assert.Equal(addTodoCommand.ScheduleStartDate, findTodo.ScheduleStartDate);
        Assert.Equal(addTodoCommand.ScheduleEndDate, findTodo.ScheduleEndDate);

    }

    [Fact]
    public async Task 検索_なし_Test()
    {
        // Arrange
        using var _todoDbContext = CreateTodoDbContext();
        ITodoRepository todoRepository = new TodoRepository(_todoDbContext);

        Todo todo = null;

        var fingByIdService = new FindByIdService(todoRepository);
        // Act
        var findTodo = await fingByIdService.ExecuteAsync(Guid.NewGuid().ToString());

        // Assert
        Assert.Null(findTodo);

    }

}
