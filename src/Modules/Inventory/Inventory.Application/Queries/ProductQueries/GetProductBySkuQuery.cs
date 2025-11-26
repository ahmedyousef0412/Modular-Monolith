using SharedKernel.CQRS;


namespace Inventory.Application.Queries.ProductQueries;

public record GetProductBySkuQuery(string Sku) : IQuery<ProductDto>;

