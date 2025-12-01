using MediatR;

namespace Inventory.Application.Commands.WarehouseCommands;

public record ActivateWarehouseCommand(Guid Id) : IRequest;
