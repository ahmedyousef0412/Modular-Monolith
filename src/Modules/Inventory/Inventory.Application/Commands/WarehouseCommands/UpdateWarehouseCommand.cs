using BuildingBlocks.Application.CQRS;

namespace Inventory.Application.Commands.WarehouseCommands;

public record UpdateWarehouseCommand(Guid Id, string Name, string Location) : ICommand;
