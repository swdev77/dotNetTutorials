using Gatherly.Domain.Primitives;
using Gatherly.Domain.Repositories;
using Gatherly.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Gatherly.Persistence;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ConvertDomainEventsToOutboxMessages();
        UpdateAuditableEntities();

        await _context.SaveChangesAsync(cancellationToken);
    }

    private void ConvertDomainEventsToOutboxMessages()
    {
        var outboxMessages = _context.ChangeTracker
            .Entries<AggregateRoot>()
            .Select(x => x.Entity)
            .SelectMany(aggregateRoot =>
            {
                var domainEvents = aggregateRoot.GetDomainEvents();
                aggregateRoot.ClearDomainEvents();
                return domainEvents;
            })
                .Select(domainEvent => new OutboxMessage
                {
                    Id = Guid.NewGuid(),
                    OccurredOnUtc = DateTime.UtcNow,
                    Type = domainEvent.GetType().Name,
                    Content = JsonConvert.SerializeObject(domainEvent,
                        new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All
                        })
                })
                .ToList();

        _context.Set<OutboxMessage>().AddRange(outboxMessages);
    }

    private void UpdateAuditableEntities()
    {
        var entities = _context.ChangeTracker
            .Entries<IAuditableEntity>();

        foreach (var entity in entities)
        {
            if (entity.State == EntityState.Added)
            {
                entity.Property(x => x.CreatedOnUtc).CurrentValue = DateTime.UtcNow;
            }

            if (entity.State == EntityState.Modified)
            {
                entity.Property(x => x.ModifiedOnUtc).CurrentValue = DateTime.UtcNow;
            }
        }
    }
}