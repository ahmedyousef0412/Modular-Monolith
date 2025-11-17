using Sales.Infrastructure.Persistence;
using SharedKernel.Entities;

namespace Sales.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{

    private readonly SalesDbContext _context;
    public UnitOfWork(SalesDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
       return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {

        // Dispatch Domain Events collection.
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
