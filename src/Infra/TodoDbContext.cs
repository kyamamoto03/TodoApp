using Domain.SeedOfWork;
using Domain.TodoModel;
using Domain.UserModel;
using Infra.Configuration;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infra;

public class TodoDbContext : DbContext, IUnitOfWork
{
    private readonly IMediator _mediator;

    public TodoDbContext(DbContextOptions<TodoDbContext> options, IMediator mediator) : base(options)
    {
        _mediator = mediator;
    }

    public DbSet<Todo> Todos { get; set; } = default!;
    public DbSet<TodoItem> TodoItems { get; set; } = default!;

    public DbSet<User> Users { get; set; } = default!;

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEventsAsync(this);

        _ = await base.SaveChangesAsync(cancellationToken);

        return true;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TodoConfiguration());
        modelBuilder.ApplyConfiguration(new TodoItemConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}
