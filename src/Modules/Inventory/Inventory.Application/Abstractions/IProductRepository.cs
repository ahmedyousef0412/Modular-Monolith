using Inventory.Domain.Entity;

namespace Inventory.Application.Abstractions;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Product?> GetBySkuAsync(string sku, CancellationToken token = default);
    Task<bool> ExistsAsync(string sku, CancellationToken cancellationToken = default);
    void Add(Product product);

    void Update(Product product);
}


#region Info
/*
 
 Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default);
 
 GetAllAsync is dangerous. If I eventually have 50,000 products,
 this method will pull all of them into memory, potentially crashing your application.


    Recommendation: 
     In CQRS, "Reading Lists" is usually done in the Query side (returning DTOs),
     not the Repository (which returns Domain Entities).
 
 
 
 */

#endregion