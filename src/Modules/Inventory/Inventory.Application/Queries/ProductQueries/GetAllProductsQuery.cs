using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.Pagination;
using Inventory.Application.Dtos.Products;

namespace Inventory.Application.Queries.ProductQueries;

public record GetAllProductsQuery(string? SearchTerm) : PagedRequest, IQuery<PagedList<ProductResponse>>;
