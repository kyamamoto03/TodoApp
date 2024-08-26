using Domain.TodoModel;
using Infra;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;
using TodoApp.Api.Usecase.TodoUsecase.Add;
using TodoApp.Api.Usecase.TodoUsecase.FindById;

namespace TodoApp.Api.Usecase.Test.TodoUsecase;

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

        var addTodousecase = new AddTodoUsecase(todoRepository);
        await addTodousecase.ExecuteAsync(addTodoCommand);

        // Act
        var usecase = new FindByIdUsecase(todoRepository);
        var findTodo = await usecase.ExecuteAsync(todoId);

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

        var usecase = new FindByIdUsecase(todoRepository);
        // Act
        var findTodo = await usecase.ExecuteAsync(Guid.NewGuid().ToString());

        // Assert
        Assert.Null(findTodo);

    }

}
