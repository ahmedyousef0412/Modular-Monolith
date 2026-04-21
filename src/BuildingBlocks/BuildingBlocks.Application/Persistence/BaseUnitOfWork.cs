

namespace BuildingBlocks.Application.Persistence;

public abstract class BaseUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
{
    private readonly TContext _context;
    private readonly IMediator _mediator;
   

    public BaseUnitOfWork(TContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

  

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await DispatchDomainEventsAsync(cancellationToken);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {

        var result = await SaveChangesAsync(cancellationToken);
        return result > 0;
    }

    private async Task DispatchDomainEventsAsync(CancellationToken cancellationToken)
    {
        while (true)
        {
            // Only look for AggregateRoots that actually have events waiting
            var domainEntities = _context.ChangeTracker
                .Entries<AggregateRoot>()
                .Where(x => x.Entity.DomainEvents.Any())
                .ToList();

            if (!domainEntities.Any()) break; // Exit loop if no events found

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            // Clear events immediately to prevent double-firing 
            // if a handler triggers another SaveChanges call
            domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent, cancellationToken);
            }
        }
    }
}
