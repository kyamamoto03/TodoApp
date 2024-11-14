using Domain.UserModel;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infra.Test.IUserRepositoryTest;

public class AddTest : DbInstance
{
    public TodoDbContext CreateTodoDbContext()
    {
        var _db = new TodoDbContext(new DbContextOptionsBuilder<TodoDbContext>()
       .UseNpgsql(DbConnectionString)
       .Options);

        return _db;
    }

    [Fact]
    public async Task ユーザ追加_Test()
    {
        using var _todoDbContext = CreateTodoDbContext();

        IUserRepository userRepository = new UserRepository(_todoDbContext);

        var userId = Guid.NewGuid().ToString();
        var userName = "TestUser";
        var email = "test@example.com";

        await userRepository.AddAsync(userId, userName, email);
        try
        {
            await userRepository.UnitOfWork.SaveEntitiesAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        var findUser = await userRepository.FindByIdAsync(userId);

        Assert.NotNull(findUser);
        Assert.Equal(userId, findUser.UserId);
        Assert.Equal(userName, findUser.UserName);
        Assert.Equal(email, findUser.Email);
    }
}