using BuildingBlocks.Application.CQRS;
using Inventory.Application.Dtos.StockItems;

namespace Inventory.Application.Queries.StockItemQueries;

public record GetStockByProductIdQuery(Guid ProductId) : IQuery<IReadOnlyList<StockItemDto>>;


