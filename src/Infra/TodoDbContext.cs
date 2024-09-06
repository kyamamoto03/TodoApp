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
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<Todo> Todos { get; set; } = default!;
    public DbSet<TodoItem> TodoItems { get; set; } = default!;

    public DbSet<User> Users { get; set; } = default!;

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEventsAsync(this);

        SupportTimeStampHelper.UpdateTimeStamps(this, DateTime.Now);
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

/// <summary>
/// SaveChanges時にTimeStampを更新するためのヘルパークラス
/// </summary>
public class SupportTimeStampHelper
{
    public static void UpdateTimeStamps(DbContext dbContext, DateTime now)
    {
        foreach (var entity in dbContext.ChangeTracker.Entries())
        {
            if (!(entity.Entity is IModelBase e))
                continue;
            switch (entity.State)
            {
                case EntityState.Added:
                    e.Created(now);
                    break;

                case EntityState.Modified:
                    e.Updated(now);
                    break;
            };
        }
    }
}