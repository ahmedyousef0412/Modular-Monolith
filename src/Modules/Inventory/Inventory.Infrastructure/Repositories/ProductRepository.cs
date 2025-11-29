using Inventory.Application.Repository;
using Inventory.Domain.Entity;
using Inventory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Repositories;

namespace Inventory.Infrastructure.Repositories;

public  class ProductRepository :BaseRepository<Product> ,IProductRepository
{
    public ProductRepository(InventoryDbContext context) : base(context)
    {
    }

    // For activate/deactivate, I use queries that ignore soft-delete filter.
    public override async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<Product?> GetBySkuAsync(string sku, CancellationToken token = default)
    {
        return await _dbSet
            .FirstOrDefaultAsync(p => p.Sku == sku, token);
    }

    public async Task<bool> ExistsAsync(string sku, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AnyAsync(p => p.Sku == sku, cancellationToken);
    }
    
}
