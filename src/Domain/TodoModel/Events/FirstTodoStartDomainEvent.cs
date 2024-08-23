using MediatR;

namespace Domain.TodoModel.Events;

public class FirstTodoStartDomainEvent : INotification
{
    public string UserId { get; private set; }
    public FirstTodoStartDomainEvent(string userId)
    {
        UserId = userId;    
    }
}
