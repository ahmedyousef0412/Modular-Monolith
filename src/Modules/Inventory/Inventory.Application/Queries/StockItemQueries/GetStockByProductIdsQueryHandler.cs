using Inventory.Application.Queries.ProductQueries;
using Inventory.Application.Repository;
using SharedKernel.CQRS;


namespace Inventory.Application.Queries.StockItemQueries;

public class GetStockByProductIdsQueryHandler : IQueryHandler<GetStockByProductIdsQuery, IReadOnlyList<StockItemDto>>
{

    private readonly IStockReadRepository _stockReadRepository;

    public GetStockByProductIdsQueryHandler(IStockReadRepository stockReadRepository)
    {
        _stockReadRepository = stockReadRepository;
    }

    public async Task<IReadOnlyList<StockItemDto>> Handle(GetStockByProductIdsQuery query, CancellationToken cancellationToken)
    {
        var stockItems = await _stockReadRepository.GetByProductIdsAndWarehouseAsync(query.ProductIds, query.WarehouseId, cancellationToken);

        return stockItems;
    }
}
