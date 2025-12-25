using BuildingBlocks.Application.CQRS;


namespace Identity.Application.Roles.Commands.RevokeRole;

public record RevokeRoleCommand(Guid UserId,Guid RoleId) : ICommand;

