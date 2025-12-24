
using BuildingBlocks.Application.CQRS;
using Inventory.Application.Dtos.StockItems;

namespace Inventory.Application.Queries.StockItemQueries;

public record GetStockProductAndWarehouseQuery(Guid ProductId, Guid WarehouseId) : IQuery<StockItemDto>;


