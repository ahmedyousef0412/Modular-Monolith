using Sales.Domain.Entity;

namespace Sales.Domain.Repository;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(Guid id,CancellationToken cancellationToken = default);
    Task<IEnumerable<Order>> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default);

    Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken = default);
    void Add(Order order, CancellationToken cancellationToken = default);

    void Update(Order order);
}
