using Domain.TodoModel;
using Domain.UserModel;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Repository;

public static class RepositoryExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITodoRepository, TodoRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}