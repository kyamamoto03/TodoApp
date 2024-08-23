using Domain.UserModel;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infra.Test.IUserRepositoryTest;

public class AddTest : IAsyncDisposable
{
    private readonly TodoDbContext _todoDbContext;
    public AddTest()
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
    public async Task ユーザ追加_Test()
    {

        IUserRepository userRepository = new UserRepository(_todoDbContext);

        var userId = Guid.NewGuid().ToString();
        var userName = "TestUser";
        var email = "test@example.com";

        await userRepository.AddAsync(userId, userName, email);
        await userRepository.UnitOfWork.SaveEntitiesAsync();

        var findUser = await userRepository.FindByIdAsync(userId);

        Assert.NotNull(findUser);
        Assert.Equal(userId, findUser.UserId);
        Assert.Equal(userName, findUser.UserName);
        Assert.Equal(email, findUser.Email);
    }
}
