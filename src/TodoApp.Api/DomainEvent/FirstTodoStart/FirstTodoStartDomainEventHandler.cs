using Domain.Exceptions;
using Domain.TodoModel.Events;
using Domain.UserModel;
using MediatR;

namespace TodoApp.Api.DomainEvent.FirstTodoStart;

public class FirstTodoStartDomainEventHandler(IUserRepository userRepository) : INotificationHandler<FirstTodoStartDomainEvent>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task Handle(FirstTodoStartDomainEvent notification, CancellationToken cancellationToken)
    {
        var userId = notification.UserId;

        Console.WriteLine($"StartTodoService.Execute:UserId:{userId}");

        var targetUser = await _userRepository.FindByIdAsync(userId);
        if (targetUser == null)
        {
            throw new TodoDoaminExceptioon("User not found");
        }

        targetUser.Start();
        await _userRepository.UpdateAsync(targetUser);
        await _userRepository.UnitOfWork.SaveEntitiesAsync();
    }
}