using Domain.TodoModel.Events;
using MediatR;

namespace TodoApp.Api.Usecase.UserUsecase.StartTodo;

public class FirstTodoStartDomainEventHandler(IFirstTodoStartUsecase firstTodoStartUsecase) : INotificationHandler<FirstTodoStartDomainEvent>
{
    private readonly IFirstTodoStartUsecase _firstTodoStartUsecase = firstTodoStartUsecase;

    public async Task Handle(FirstTodoStartDomainEvent notification, CancellationToken cancellationToken)
    {
        await _firstTodoStartUsecase.Execute(notification.UserId);
    }
}
