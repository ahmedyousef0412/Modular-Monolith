using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.Pagination;
using Inventory.Application.Abstractions;
using Inventory.Application.Dtos.Products;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Domain;

namespace Inventory.Application.Queries.ProductQueries;

public class GetAllProductsQueryHandler(IInventoryDbContext inventoryDbContext) : IQueryHandler<GetAllProductsQuery, PagedList<ProductResponse>>
{
    public async Task<Result<PagedList<ProductResponse>>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
    {
        var productsQuery =  inventoryDbContext.Products.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(query.SearchTerm))
        {
            productsQuery = productsQuery.Where(p => p.Name.Contains(query.SearchTerm));
        }


        var products = productsQuery.Select(p => new ProductResponse(
            p.Id,
            p.Name,
            p.Sku,
            p.Price,
            p.Description
            ));


        var pagedProducts = await PagedList<ProductResponse>
            .CreateAsync(products, query.Page, query.PageSize, cancellationToken);

        return Result.Success(pagedProducts);
    }
}
