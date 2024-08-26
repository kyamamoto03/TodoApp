using Domain.Exceptions;
using Domain.UserModel;

namespace TodoApp.Api.Service.UserService.StartTodo;

public interface IFirstTodoStartService
{
    Task Execute(string todoId);
}
public class FirstTodoStartService(IUserRepository userRepository) : IFirstTodoStartService
{
    private readonly IUserRepository _userRepository = userRepository;
    public async Task Execute(string userId)
    {
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
