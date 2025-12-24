using Microsoft.EntityFrameworkCore;
using Sales.Application.Abstractions;
using Sales.Domain.Entity;
using Sales.Infrastructure.Persistence;

namespace Sales.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
   private readonly SalesDbContext _context;

    public OrderRepository(SalesDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Orders.Include(o => o.Items)
              .AsNoTracking()
              .OrderByDescending(o => o.CreatedAt)
             .ToListAsync(cancellationToken);
           
    }

    public async Task<IEnumerable<Order>> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        return await _context.Orders
            .Include(o => o.Items)
            .AsNoTracking()
            .Where(o  => o.CustomerId == customerId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync(cancellationToken);
            
    }

    public async Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }
  
    
    public void Add(Order order)
    {
        _context.Orders.Add(order);
    }
   
}
