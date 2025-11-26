using Inventory.Application.Queries.ProductQueries;
using Inventory.Domain.Entity;


namespace Inventory.Application.Mappings;

public static class StockMappingExtensions
{

    //call this method on any IQueryable<StockItem> object like a built-in method
    public static IQueryable<StockItemDto> ProjectToDto(this IQueryable<StockItem> query)
    {
        return query.Select(si => new StockItemDto
            (
                si.Id,
                si.WarehouseId,
                si.Quantity,
                si.MinimumQuantity,
                si.MaximumQuantity,
                si.Quantity > 0 ? StockStatus.InStock : StockStatus.OutOfStock
            )
        );
    }
}
