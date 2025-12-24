using Inventory.Application.Dtos.Warehouses;
using Inventory.Application.Queries.WarehouseQueries;


namespace Inventory.Application.Abstractions;

public interface IWarehouseReadRepository
{
    Task<WarehouseByIdDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<WarehouseDto>> GetAllAsync(bool includeInactive,CancellationToken cancellationToken = default);
}
