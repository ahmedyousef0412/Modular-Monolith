using Inventory.Application.Repository;
using Inventory.Domain.Entity;
using Inventory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.Intrinsics.X86;

namespace Inventory.Infrastructure.Repositories;

public  class ProductRepository : IProductRepository
{
    
    private readonly InventoryDbContext _context;

    public ProductRepository(InventoryDbContext context)
    {
        _context = context;
    }

   // For activate/deactivate, I use queries that ignore soft-delete filter.
    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<Product?> GetBySkuAsync(string sku, CancellationToken token = default)
    {
        return await _context.Products
            .FirstOrDefaultAsync(p => p.Sku == sku, token);
    }

    public async Task<bool> ExistsAsync(string sku, CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .AnyAsync(p => p.Sku == sku, cancellationToken);
    }
    public void Add(Product product)
    {
        _context.Products.Add(product);
    }
    public void Update(Product product)
    {
        _context.Products.Update(product);
    }
}
