using BuildingBlocks.Application.CQRS;

namespace Identity.Application.Roles.Commands.UpdateRole;

public record UpdateRoleCommand(Guid RoleId, string Name, string Description) : ICommand;

