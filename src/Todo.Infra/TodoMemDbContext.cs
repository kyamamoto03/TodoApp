using Domain.Todos;
using Infra.Configuration;
using Microsoft.EntityFrameworkCore;


namespace Infra;

public class TodoMemDbContext : DbContext
{
    public TodoMemDbContext(DbContextOptions<TodoMemDbContext> options) : base(options)
    {
    }

    public DbSet<Todo> Todos { get; set; } = default!;
    public DbSet<TodoItem> TodoItems { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TodoConfiguration());
        modelBuilder.ApplyConfiguration(new TodoItemConfiguration());
    }
}
