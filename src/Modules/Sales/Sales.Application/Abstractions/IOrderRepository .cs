using Sales.Domain.Entity;

namespace Sales.Application.Abstractions;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(Guid id,CancellationToken cancellationToken = default);
    Task<IEnumerable<Order>> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default);

    Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken = default);
    void Add(Order order);

}
