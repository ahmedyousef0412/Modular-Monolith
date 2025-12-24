using BuildingBlocks.Application.CQRS;
using Inventory.Application.Dtos.Products;


namespace Inventory.Application.Queries.ProductQueries;

public record GetProductBySkuQuery(string Sku) : IQuery<ProductDto>;

