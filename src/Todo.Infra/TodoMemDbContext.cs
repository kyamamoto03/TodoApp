using Microsoft.EntityFrameworkCore;
using Todo.Infra.Configuration;


namespace Todo.Infra;

public class TodoMemDbContext : DbContext
{
    public TodoMemDbContext(DbContextOptions<TodoMemDbContext> options) : base(options)
    {
    }

    public DbSet<Domain.Todo> Todos { get; set; } = default!;
    public DbSet<Domain.TodoItem> TodoItems { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TodoConfiguration());
        modelBuilder.ApplyConfiguration(new TodoItemConfiguration());
    }
}
