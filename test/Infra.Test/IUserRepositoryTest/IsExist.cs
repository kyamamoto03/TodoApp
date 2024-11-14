using Domain.UserModel;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infra.Test.IUserRepositoryTest;

public class IsExist : DbInstance
{
    public TodoDbContext CreateTodoDbContext()
    {
        var _db = new TodoDbContext(new DbContextOptionsBuilder<TodoDbContext>()
       .UseNpgsql(DbConnectionString)
       .Options);

        return _db;
    }

    [Fact]
    public async Task ユーザが存在_OK_Test()
    {
        using var _todoDbContext = CreateTodoDbContext();

        IUserRepository userRepository = new UserRepository(_todoDbContext);

        var userId = Guid.NewGuid().ToString();
        var userName = "TestUser";
        var email = "IsExist@example.com";

        await userRepository.AddAsync(userId, userName, email);
        await userRepository.UnitOfWork.SaveEntitiesAsync();

        var isExist = await userRepository.IsExist(email);

        Assert.True(isExist);
    }

    [Fact]
    public async Task ユーザが存在しない_OK_Test()
    {
        using var _todoDbContext = CreateTodoDbContext();

        IUserRepository userRepository = new UserRepository(_todoDbContext);

        var userId = Guid.NewGuid().ToString();
        var userName = "TestUser";
        var email = "IsExist2@example.com";

        await userRepository.AddAsync(userId, userName, email);
        await userRepository.UnitOfWork.SaveEntitiesAsync();

        var isExist = await userRepository.IsExist("notfound@example.com");

        Assert.False(isExist);
    }
}