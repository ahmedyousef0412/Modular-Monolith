using Inventory.Application.Dtos.StockItems;
using Inventory.Application.Repository;
using SharedKernel.CQRS;

namespace Inventory.Application.Queries.StockItemQueries;

public class GetStockByProductIdQueryHandler : IQueryHandler<GetStockByProductIdQuery, IReadOnlyList< StockItemDto>>
{

    private readonly IStockReadRepository _stockReadRepository;

    public GetStockByProductIdQueryHandler(IStockReadRepository stockReadRepository)
    {
        _stockReadRepository = stockReadRepository;
    }

    public async Task<IReadOnlyList<StockItemDto>> Handle(GetStockByProductIdQuery query, CancellationToken cancellationToken)
    {
        var stockItems = await _stockReadRepository.GetByProductIdAsync(query.ProductId, cancellationToken); ;

        return stockItems ?? [];
    }
}
