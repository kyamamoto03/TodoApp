using Domain.SeedOfWork;
using MediatR;

namespace Infra;

static class MediatorExtension
{
    public static async Task DispatchDomainEventsAsync(this IMediator mediator, TodoDbContext ctx)
    {
        if (mediator is null)
        {
            Console.WriteLine("mediator is null");
        }
        else
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }
}