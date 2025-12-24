using BuildingBlocks.Application.Contracts;
using MediatR;
using Sales.Application.Ports;


namespace Sales.Infrastructure.Gateways;

public class InventoryGateway : IInventoryGateway
{

    private readonly IMediator _mediator;

    public InventoryGateway(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ProductInfo?> GetProductInfoAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        var query = new GetProductInfoQuery(productId);

        var result = await _mediator.Send(query, cancellationToken);

        if (result is null)  return null;

        return new ProductInfo(
            result.Id,
            result.Name,
            result.Sku,
            result.Price
        );

    }
}
