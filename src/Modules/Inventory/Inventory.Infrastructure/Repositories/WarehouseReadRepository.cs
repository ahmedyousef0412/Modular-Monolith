using Inventory.Application.Dtos.Warehouses;
using Inventory.Application.Queries.WarehouseQueries;
using Inventory.Application.Repository;
using Inventory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Repositories;

public class WarehouseReadRepository : IWarehouseReadRepository
{

    private readonly InventoryDbContext _context;
    public WarehouseReadRepository(InventoryDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<WarehouseDto>> GetAllAsync(bool includeInactive, CancellationToken cancellationToken = default)
    {
        var query = _context.Warehouses.AsNoTracking();

        if (!includeInactive)
            query = query.Where(x => x.IsActive);

        return await query
            .OrderBy(w => w.Name)
            .Select(w => new WarehouseDto(w.Id, w.Name,w.Location)) 
            .ToListAsync(cancellationToken);
    }

    public async Task<WarehouseByIdDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Warehouses
        .AsNoTracking()
        .Where(w => w.Id == id)
        .Select(w => new WarehouseByIdDto(w.Id, w.Name, w.Location, w.IsActive))
        .FirstOrDefaultAsync(cancellationToken);
    }
}
