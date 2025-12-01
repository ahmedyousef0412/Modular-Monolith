using Inventory.Application.Dtos.StockItems;
using Inventory.Application.Repository;
using Inventory.Domain.Entity;
using SharedKernel.CQRS;



namespace Inventory.Application.Queries.StockItemQueries;

public class GetStockProductAndWarehouseQueryHandler(IStockReadRepository stockReadRepository) 
    : IQueryHandler<GetStockProductAndWarehouseQuery, StockItemDto>
{

    private readonly IStockReadRepository _stockReadRepository = stockReadRepository;

    public async Task<StockItemDto> Handle(GetStockProductAndWarehouseQuery query, CancellationToken cancellationToken)
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
