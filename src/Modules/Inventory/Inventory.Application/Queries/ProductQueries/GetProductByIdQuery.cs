using BuildingBlocks.Application.CQRS;
using Inventory.Application.Dtos.Products;


namespace Inventory.Application.Queries.ProductQueries;

public record GetProductByIdQuery(Guid ProductId) : IQuery<ProductDto>;




