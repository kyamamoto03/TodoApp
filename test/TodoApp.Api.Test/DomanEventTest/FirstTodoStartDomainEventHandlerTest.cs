using Domain.TodoModel.Events;
using Infra;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;
using TodoApp.Api.DomainEvent.FirstTodoStart;

namespace TodoApp.Api.Test.DomanEventTest;

public class FirstTodoStartDomainEventHandlerTest : DbInstance
{
    public TodoDbContext CreateTodoDbContext()
    {
        var _db = new TodoDbContext(new DbContextOptionsBuilder<TodoDbContext>()
       .UseNpgsql(DbConnectionString)
       .Options, null);

        return _db;
    }

    [Fact]
    public async Task FirstTodoStartDomainEventHandler_Teset()
    {
        //arrange
        UserRepository userRepository = new UserRepository(CreateTodoDbContext());
        FirstTodoStartDomainEventHandler firstTodoStartDomainEventHandler = new FirstTodoStartDomainEventHandler(userRepository);

        var userId = Guid.NewGuid().ToString();
        await userRepository.AddAsync(userId, "Test", "Test");
        await userRepository.UnitOfWork.SaveEntitiesAsync();

        //act
        await firstTodoStartDomainEventHandler.Handle(new FirstTodoStartDomainEvent(userId), new CancellationToken());

        //assert
        var targetUser = await userRepository.FindByIdAsync(userId);
        Assert.NotNull(targetUser);
        Assert.True(targetUser.IsStarted);
    }
}