using BuildingBlocks.Application.CQRS;


namespace Inventory.Application.Queries.StockItemQueries;

public record GetTotalQuantityForProductQuery(Guid ProductId) : IQuery<int>;

