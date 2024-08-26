using Domain.TodoModel.Events;
using MediatR;

namespace TodoApp.Api.Service.UserService.StartTodo;

public class FirstTodoStartDomainEventHandler(IFirstTodoStartService firstTodoStartService) : INotificationHandler<FirstTodoStartDomainEvent>
{
    private readonly IFirstTodoStartService _firstTodoStartService = firstTodoStartService;

    public async Task Handle(FirstTodoStartDomainEvent notification, CancellationToken cancellationToken)
    {
        await _firstTodoStartService.Execute(notification.UserId);
    }
}
