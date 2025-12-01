using Inventory.Domain.Entity;

namespace Inventory.Application.Repository;

public interface IWarehouseRepository
{

    // 👇 ADD THIS LINE
    Task<Warehouse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);

    // You likely need Update as well for Deactivate/Activate logic
    void Update(Warehouse warehouse);
    void Add(Warehouse warehouse);
} 
