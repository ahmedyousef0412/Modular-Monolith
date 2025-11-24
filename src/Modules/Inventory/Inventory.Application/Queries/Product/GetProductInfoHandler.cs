using Inventory.Application.Repository;
using MediatR;
using SharedKernel.Contracts;


namespace Inventory.Application.Queries.Product;

public class GetProductInfoHandler : IRequestHandler<GetProductInfoQuery, ProductInfoDto>
{
    private readonly IProductRepository _productRepository;
    private readonly IStockRepository _stockRepository;

    public GetProductInfoHandler(IProductRepository productRepository, IStockRepository stockRepository)
    {
        _productRepository = productRepository;
        _stockRepository = stockRepository;
    }

    public async Task<ProductInfoDto> Handle(GetProductInfoQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);

        if(product is null) return null;

        var totalStock = await _stockRepository.GetTotalQuantityForProductAsync(request.ProductId, cancellationToken);

        var productInfoDto = new ProductInfoDto
        (
            Id : product.Id,
            Name : product.Name,
            Sku: product.Sku,
            Price: product.Price,
            TotalStock: totalStock
        );

        return productInfoDto;
    }
}
