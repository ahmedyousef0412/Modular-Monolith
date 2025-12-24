using Inventory.Domain.Entity;

namespace Inventory.Application.Abstractions;

public interface IWarehouseRepository
{

   
    Task<Warehouse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);

    void Update(Warehouse warehouse);
    void Add(Warehouse warehouse);
} 
