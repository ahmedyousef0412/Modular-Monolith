
using BuildingBlocks.Application.CQRS;

namespace Identity.Application.Roles.Commands.AssignRole;

public record AssignRoleCommand(Guid RoleId, Guid UserId) : ICommand;

