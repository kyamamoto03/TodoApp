using Domain.UserModel;

namespace TodoApp.Api.Usecase.User.StartTodo;

public interface IFirstTodoStartUsecase
{
    Task Execute(string todoId);
}
public class FirstTodoStartUsecase(IUserRepository userRepository) : IFirstTodoStartUsecase
{
    private readonly IUserRepository _userRepository = userRepository;
    public async Task Execute(string todoId)
    {
        Console.WriteLine($"StartTodoUsecase.Execute:TodoId:{todoId}");

        var targetUser = await _userRepository.FindByIdAsync(todoId);
        if (targetUser == null)
        {
            throw new Exception("User not found");
        }

        targetUser.Start();
        await _userRepository.UpdateAsync(targetUser);
        await _userRepository.UnitOfWork.SaveEntitiesAsync();

    }
}
