
using TodoApp.Api.DTO.User.GetAll;
using TodoApp.Api.Usecase.User.GetAll;

namespace TodoApp.Api.Apis;

public static class UserApis
{
    public static RouteGroupBuilder MapUserApiV1(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/user");

        api.MapPost("/GetAll", GetAllAsync);

        return api;
    }

    private static async Task<GetAllResponse> GetAllAsync(IGetAllUsecase getAllUsecase)
    {
        GetAllResponse response = new();
        try
        {
            var getAllResult = await getAllUsecase.ExecuteAsync();

            //getAllResultをGetAllResponseに詰め替える
            response.Users = getAllResult.Users.Select(x => new GetAllResponse.User
            {
                UserId = x.UserId,
                UserName = x.UserName,
                Email = x.Email
            });

            response.Success();
        }
        catch(Exception ex)
        {
            response.Fail(ex.ToString());
        }

        return response;
    }
}
