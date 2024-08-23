using Domain.UserModel;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Testcontainers.PostgreSql;

namespace Infra.Test.IUserRepositoryTest;

public class AddTest : IAsyncLifetime
{
    private readonly TodoDbContext _todoDbContext;

    private PostgreSqlContainer _postgres;
    private string Dir
    {
        get
        {
            var dir = System.Environment.CurrentDirectory;
            return $"{dir}/../../../../../db/todo-postgresql/init";
        }
    }
    public AddTest()
    {
        _todoDbContext = new TodoDbContext(new DbContextOptionsBuilder<TodoDbContext>()
            .UseNpgsql("TodoMemDbContext")
            .Options, null);
    }

    public Task InitializeAsync()
    {
        try
        {
            _postgres = new PostgreSqlBuilder()
            .WithImage("postgres:15-alpine")
            .WithDatabase("db")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .WithPortBinding(15432,5432)
            .WithBindMount(Dir, @"/docker-entrypoint-initdb.d")
            .Build();

        }catch(Exception ex)
        {
            Debug.WriteLine(ex.Message);    
        }
        return _postgres.StartAsync();
    }
    async Task IAsyncLifetime.DisposeAsync()
    {
        await _todoDbContext.DisposeAsync();

        await _postgres.DisposeAsync();
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
