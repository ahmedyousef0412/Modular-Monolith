using SharedKernel.CQRS;


namespace Inventory.Application.Queries.Product;

public record GetProductBySkuQuery(string Sku) : IQuery<ProductDto>;

