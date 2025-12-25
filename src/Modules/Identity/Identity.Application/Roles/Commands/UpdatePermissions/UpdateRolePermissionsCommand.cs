using BuildingBlocks.Application.CQRS;

namespace Identity.Application.Roles.Commands.UpdatePermissions;

public record UpdateRolePermissionsCommand
(
    Guid RoleId,
    List<string> PermissionCodes 
) : ICommand;
