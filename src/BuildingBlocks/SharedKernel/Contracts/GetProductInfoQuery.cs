using MediatR;


namespace SharedKernel.Contracts;


// The Request (Input)
public record GetProductInfoQuery(Guid ProductId) : IRequest<ProductInfoDto>;


// The Response (Output)
public record ProductInfoDto(
    Guid Id,
    string Name,
    string Sku,
    decimal Price, 
    int TotalStock
);