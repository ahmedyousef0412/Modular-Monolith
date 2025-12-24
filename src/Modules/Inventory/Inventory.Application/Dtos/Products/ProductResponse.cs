namespace Inventory.Application.Dtos.Products;

public record ProductResponse
(
    Guid Id, 
    string Name,
    string Sku,
    decimal Price,
    string? Description
);

