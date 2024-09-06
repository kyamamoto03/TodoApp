using Domain.TodoModel.Events;
using Infra;
using Infra.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using TodoApp.Api.DomainEventHandler;

namespace TodoApp.Api.Test.DomanEventTest;

public class FirstTodoStartDomainEventHandlerTest : DbInstance
{
    private IMediator _mediatorMock = default!;

    public TodoDbContext CreateTodoDbContext()
    {
        _mediatorMock = Substitute.For<IMediator>();

        var _db = new TodoDbContext(new DbContextOptionsBuilder<TodoDbContext>()
           .UseNpgsql(DbConnectionString)
           .Options,
           _mediatorMock);

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