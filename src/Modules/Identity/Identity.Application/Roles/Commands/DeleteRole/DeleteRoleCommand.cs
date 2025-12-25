using BuildingBlocks.Application.CQRS;


namespace Identity.Application.Roles.Commands.DeleteRole;

public record DeleteRoleCommand(Guid Id) : ICommand;

