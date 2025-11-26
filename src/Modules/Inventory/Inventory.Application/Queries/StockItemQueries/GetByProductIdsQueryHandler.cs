using Inventory.Application.Queries.ProductQueries;
using Inventory.Application.Repository;
using SharedKernel.CQRS;


namespace Inventory.Application.Queries.StockItemQueries;

public class GetByProductIdsQueryHandler : IQueryHandler<GetByProductIdsQuery, IReadOnlyList<StockItemDto>>
{

    private readonly IStockReadRepository _stockReadRepository;

    public GetByProductIdsQueryHandler(IStockReadRepository stockReadRepository)
    {
        _stockReadRepository = stockReadRepository;
    }

    public async Task<IReadOnlyList<StockItemDto>> Handle(GetByProductIdsQuery query, CancellationToken cancellationToken)
    {
        var stockItems = await _stockReadRepository.GetByProductIdsAndWarehouseAsync(query.ProductIds, query.WarehouseId, cancellationToken);

        return stockItems;
    }
}
