using MediatR;

namespace Inventory.Application.Commands.WarehouseCommands;

public record DeactivateWarehouseCommand(Guid Id) : IRequest;
