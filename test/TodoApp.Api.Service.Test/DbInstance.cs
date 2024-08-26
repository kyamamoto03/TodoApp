using System.Diagnostics;
using Testcontainers.PostgreSql;

namespace TodoApp.Api.Service.Test;

public class DbInstance : IAsyncLifetime
{
    private PostgreSqlContainer _postgres;

    private string Dir
    {
        get
        {
            var dir = System.Environment.CurrentDirectory;
            return $"{dir}/../../../../../db/todo-postgresql/init";
        }
    }

    public string DbConnectionString => _postgres.GetConnectionString();

    public Task CreateAsync()
    {
        try
        {
            _postgres = new PostgreSqlBuilder()
            .WithImage("postgres:15-alpine")
            .WithBindMount(Dir, @"/docker-entrypoint-initdb.d")
            //.WithPortBinding(15432, 5432)
            .Build();


        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return _postgres.StartAsync();

    }

    public Task InitializeAsync()
    {
        return CreateAsync();
    }

    public async Task DisposeAsync()
    {
        await _postgres.DisposeAsync();
    }
}
