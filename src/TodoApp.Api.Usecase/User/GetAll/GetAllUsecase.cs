using Domain.UserModel;

namespace TodoApp.Api.Usecase.User.GetAll;

public interface IGetAllUsecase
{
    Task<GetAllResult> ExecuteAsync();
}
public class GetAllUsecase(IUserRepository userRepository) : IGetAllUsecase
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<GetAllResult> ExecuteAsync()
    {
        var users = await _userRepository.GetAllAsync();

        GetAllResult getAllResult = new();
        //usersをGetAllResultに詰め替える
        getAllResult.Users = users.Select(x => new GetAllResult.User
        {
            UserId = x.UserId,
            UserName = x.UserName,
            Email = x.Email,
            IsStarted = x.IsStarted
        });

        return getAllResult;
    }
}
