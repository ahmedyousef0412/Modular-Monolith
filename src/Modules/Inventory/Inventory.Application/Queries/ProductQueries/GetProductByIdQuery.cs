using Inventory.Application.Dtos.Products;
using SharedKernel.CQRS;


namespace Inventory.Application.Queries.ProductQueries;

public record GetProductByIdQuery(Guid ProductId) : IQuery<ProductDto>;




