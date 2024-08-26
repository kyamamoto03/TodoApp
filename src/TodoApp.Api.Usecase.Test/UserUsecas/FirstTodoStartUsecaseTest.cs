using Domain.TodoModel;
using Domain.UserModel;
using Infra;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;
using TodoApp.Api.Usecase.TodoUsecase.Add;
using TodoApp.Api.Usecase.UserUsecase.StartTodo;

namespace TodoApp.Api.Usecase.Test.UserUsecas;

public class FirstTodoStartUsecaseTest : DbInstance
{
    public TodoDbContext CreateTodoDbContext()
    {
        var _db = new TodoDbContext(new DbContextOptionsBuilder<TodoDbContext>()
       .UseNpgsql(DbConnectionString)
       .Options, null);

        return _db;

    }


    [Fact]
    public async Task FirstTodoStart_IsStartedがTrue_Test()
    {
        // Arrange
        using var _todoDbContext = CreateTodoDbContext();
        IUserRepository userRepository = new UserRepository(_todoDbContext);
        ITodoRepository todoRepository = new TodoRepository(_todoDbContext);

        // ユーザを追加
        var userId = Guid.NewGuid().ToString();
        var userName = "TestUser";
        var email = "TestMail@example.com";

        await userRepository.AddAsync(userId, userName, email);

        // Todo追加
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
        await usecase.ExecuteAsync(addTodoCommand);

        // Act
        IFirstTodoStartUsecase firstTodoStartUsecase = new FirstTodoStartUsecase(userRepository);
        await firstTodoStartUsecase.Execute(userId);

        var findUser = await userRepository.FindByIdAsync(userId);

        // Assert
        Assert.NotNull(findUser);
        Assert.True(findUser.IsStarted);
        Assert.Equal(userName, findUser.UserName);
        Assert.Equal(email, findUser.Email);

    }
}
