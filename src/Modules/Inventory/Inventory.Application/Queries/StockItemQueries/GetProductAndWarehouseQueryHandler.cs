using Inventory.Application.Repository;
using SharedKernel.CQRS;
using SharedKernel.Exceptions;
using Inventory.Domain.Entity;
using Inventory.Application.Queries.ProductQueries;



namespace Inventory.Application.Queries.StockItemQueries;

public class GetProductAndWarehouseQueryHandler(IStockReadRepository stockReadRepository) : IQueryHandler<GetProductAndWarehouseQuery, StockItemDto>
{

    private readonly IStockReadRepository _stockReadRepository = stockReadRepository;

    public async Task<StockItemDto> Handle(GetProductAndWarehouseQuery query, CancellationToken cancellationToken)
    {
       var stockItemDto = await _stockReadRepository
            .GetByProductAndWarehouseAsync(query.ProductId, query.WarehouseId, cancellationToken);

        if(stockItemDto is null)
        {
            return new StockItemDto
            (
                Id:Guid.Empty,
                WarehouseId : query.WarehouseId,
                Quantity: 0,
                MinimumQuantity: 0,
                MaximumQuantity: 0,
                Status: StockStatus.OutOfStock
            );
        }

        return stockItemDto;
    }

}
