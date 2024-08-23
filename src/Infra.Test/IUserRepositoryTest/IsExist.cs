using Domain.UserModel;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infra.Test.IUserRepositoryTest;

public class IsExist : IAsyncDisposable
{
    private readonly TodoDbContext _todoDbContext;
    public IsExist()
    {
        _todoDbContext = new TodoDbContext(new DbContextOptionsBuilder<TodoDbContext>()
            .UseInMemoryDatabase("TodoMemDbContext")
            .Options, null);
    }

    public async ValueTask DisposeAsync()
    {
        await _todoDbContext.DisposeAsync();
    }

    [Fact]
    public async Task ユーザが存在_OK_Test()
    {

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
