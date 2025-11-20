using System.Diagnostics;
using Testcontainers.PostgreSql;

namespace TodoApp.Api.Test;

public class DbInstance : IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgres = new PostgreSqlBuilder()
            .WithImage("postgres:15-alpine")
            .WithBindMount(Dir, @"/docker-entrypoint-initdb.d")
            .Build();

    private static string Dir
    {
        get
        {
            var dir = System.Environment.CurrentDirectory;
            return $"{dir}/../../../../../src/TodoApp.Api.AppHost/data";
        }
    }

    public string DbConnectionString => _postgres.GetConnectionString();
    public Task InitializeAsync()
    {
        return _postgres.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await _postgres.DisposeAsync();
    }
}