using BuildingBlocks.Application.Contracts;
using Inventory.Application.Abstractions;
using Inventory.Domain.Entity;
using MediatR;
using SharedKernel.Exceptions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace Inventory.Application.Queries.ProductQueries;

public class GetProductInfoHandler : IRequestHandler<GetProductInfoQuery, ProductInfoDto>
{
    private readonly IProductRepository _productRepository;
    private readonly IStockReadRepository _stockReadRepository;

    public GetProductInfoHandler(IProductRepository productRepository, IStockReadRepository stockReadRepository)
    {
        _productRepository = productRepository;
        _stockReadRepository = stockReadRepository;
    }

    public async Task<ProductInfoDto> Handle(GetProductInfoQuery query, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(query.ProductId, cancellationToken)
            ?? throw new NotFoundException(nameof(Product), query.ProductId);

        var totalStock = await _stockReadRepository.GetTotalQuantityForProductAsync(query.ProductId, cancellationToken);

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
