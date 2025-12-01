using MediatR;

namespace Inventory.Application.Commands.WarehouseCommands;

public record CreateWarehouseCommand(string Name, string Location) : IRequest<Guid>;
