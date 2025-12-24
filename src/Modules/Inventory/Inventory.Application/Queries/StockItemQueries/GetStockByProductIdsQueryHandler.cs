using BuildingBlocks.Application.CQRS;
using Inventory.Application.Abstractions;
using Inventory.Application.Dtos.StockItems;
using SharedKernel.Domain;


namespace Inventory.Application.Queries.StockItemQueries;

public class GetStockByProductIdsQueryHandler : IQueryHandler<GetStockByProductIdsQuery, IReadOnlyList<StockItemDto>>
{

    private readonly IStockReadRepository _stockReadRepository;

    public GetStockByProductIdsQueryHandler(IStockReadRepository stockReadRepository)
    {
        _stockReadRepository = stockReadRepository;
    }

    public async Task<Result<IReadOnlyList<StockItemDto>>> Handle(GetStockByProductIdsQuery query, CancellationToken cancellationToken)
    {
        var stockItems = await _stockReadRepository.GetByProductIdsAndWarehouseAsync(query.ProductIds, query.WarehouseId, cancellationToken);

        return Result<IReadOnlyList<StockItemDto>>.Success(stockItems);
    }
}
