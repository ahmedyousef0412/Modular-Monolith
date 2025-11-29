
using Inventory.Application.Queries.ProductQueries;
using SharedKernel.CQRS;

namespace Inventory.Application.Queries.StockItemQueries;

public record GetStockProductAndWarehouseQuery(Guid ProductId, Guid WarehouseId) : IQuery<StockItemDto>;


