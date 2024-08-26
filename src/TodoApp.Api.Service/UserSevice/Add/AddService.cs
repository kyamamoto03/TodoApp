using Domain.Exceptions;
using Domain.UserModel;

namespace TodoApp.Api.Service.UserService.Add;

public interface IAddService
{
    Task ExecuteAsync(AddCommand addCommand);
}
public class AddService(IUserRepository userRepository) : IAddService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task ExecuteAsync(AddCommand addCommand)
    {
        if (await _userRepository.IsExist(addCommand.Email) == true)
        {
            throw new TodoDoaminExceptioon("ユーザが存在しています");
        }

        await _userRepository.AddAsync(addCommand.UserId, addCommand.UserName, addCommand.Email);
        await _userRepository.UnitOfWork.SaveEntitiesAsync();
    }
}
