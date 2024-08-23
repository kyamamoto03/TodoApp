
using TodoApp.Api.DTO.User.Add;
using TodoApp.Api.DTO.User.GetAll;
using TodoApp.Api.Usecase.User.Add;
using TodoApp.Api.Usecase.User.GetAll;

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

    private static async Task<AddResponse> AddAsync(AddRequest addRequest, IAddUsecase addUsecase)
    {
        AddResponse addResponse = new();
        try
        {
            if (addRequest.IsValid() == false)
            {
                addResponse.Fail(addRequest.validationResult.ToString());
            }
            else
            {

                //addRequestをAddCommandに詰め替える
                var addCommand = new AddCommand(addRequest.UserId, addRequest.UserName, addRequest.Email);

                await addUsecase.ExecuteAsync(addCommand);

                addResponse.Success();
            }
        }
        catch (Exception ex)
        {
            addResponse.Fail(ex.Message);
        }
        return addResponse;
    }
    private static async Task<GetAllResponse> GetAllAsync(IGetAllUsecase getAllUsecase)
    {
        GetAllResponse getAllResponse = new();
        try
        {
            var getAllResult = await getAllUsecase.ExecuteAsync();

            //getAllResultをGetAllResponseに詰め替える
            getAllResponse.Users = getAllResult.Users.Select(x => new GetAllResponse.User
            {
                UserId = x.UserId,
                UserName = x.UserName,
                Email = x.Email,
                IsStarted = x.IsStarted,
            });

            getAllResponse.Success();
        }
        catch (Exception ex)
        {
            getAllResponse.Fail(ex.Message);
        }

        return getAllResponse;
    }
}
