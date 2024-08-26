using Domain.Exceptions;
using Domain.UserModel;

namespace TodoApp.Api.Usecase.UserUsecase.StartTodo;

public interface IFirstTodoStartUsecase
{
    Task Execute(string todoId);
}
public class FirstTodoStartUsecase(IUserRepository userRepository) : IFirstTodoStartUsecase
{
    private readonly IUserRepository _userRepository = userRepository;
    public async Task Execute(string userId)
    {
        Console.WriteLine($"StartTodoUsecase.Execute:UserId:{userId}");

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
