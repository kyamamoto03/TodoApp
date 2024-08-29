using Domain.Exceptions;
using Domain.UserModel;
using TodoApp.Api.DTO.User.Add;
using TodoApp.Api.DTO.User.GetAll;

namespace TodoApp.Api.Apis;

public static class UserApis
{
    public static RouteGroupBuilder MapUserApiV1(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/user");

        api.MapPost("/GetAll", GetAllAsync);
        api.MapPost("/Add", AddAsync);
        return api;
    }

    public static async Task<AddResponse> AddAsync(AddRequest addRequest, IUserRepository userRepository)
    {
        AddResponse addResponse = new();
        try
        {
            if (addRequest.IsValid() == false)
            {
                addResponse.Fail(addRequest.validationResult.ToString());
                return addResponse;
            }
            if (await userRepository.IsExist(addRequest.Email) == true)
            {
                addResponse.Fail("ユーザが存在しています");
                return addResponse;
            }

            await userRepository.AddAsync(addRequest.UserId, addRequest.UserName, addRequest.Email);
            await userRepository.UnitOfWork.SaveEntitiesAsync();

            addResponse.Success();
        }
        catch (TodoDoaminExceptioon tde)
        {
            addResponse.Fail(tde.Message);
        }
        catch (Exception ex)
        {
            addResponse.Fail(ex.Message);
        }
        return addResponse;
    }

    public static async Task<GetAllResponse> GetAllAsync(IUserRepository userRepository)
    {
        GetAllResponse getAllResponse = new();
        try
        {
            var getAllResult = await userRepository.GetAllAsync();

            //getAllResultをGetAllResponseに詰め替える
            getAllResponse.Users = getAllResult.Select(x => new GetAllResponse.User
            {
                UserId = x.UserId,
                UserName = x.UserName,
                Email = x.Email,
                IsStarted = x.IsStarted,
            });

            getAllResponse.Success();
        }
        catch (TodoDoaminExceptioon tde)
        {
            getAllResponse.Fail(tde.Message);
        }
        catch (Exception ex)
        {
            getAllResponse.Fail(ex.Message);
        }

        return getAllResponse;
    }
}