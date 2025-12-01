namespace Inventory.Application.Dtos.Warehouses;

public record WarehouseByIdDto(Guid Id, string Name, string Loaction, bool IsActive);
