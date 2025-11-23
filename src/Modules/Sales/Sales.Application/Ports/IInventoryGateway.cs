namespace Sales.Application.Ports;

public interface IInventoryGateway
{
    Task<ProductInfo?> GetProductInfoAsync(Guid productId, CancellationToken cancellationToken = default);
}
public record ProductInfo(Guid Id, string Name, string Sku, decimal Price);