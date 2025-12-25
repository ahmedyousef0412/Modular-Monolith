using BuildingBlocks.Application.CQRS;
using Identity.Application.Abstractions;
using Identity.Domain.Abstractions;
using SharedKernel.Domain;

namespace Identity.Application.Roles.Commands.UpdatePermissions;

public class UpdateRolePermissionsCommandHandler
    (
     IRoleRepository roleRepository,
     IIdentityUnitOfWork unitOfWork
    ) : ICommandHandler<UpdateRolePermissionsCommand>
{


    public async Task<Result> Handle(UpdateRolePermissionsCommand command, CancellationToken cancellationToken)
    {
        var role = await roleRepository.GetByIdAsync(command.RoleId, cancellationToken);

        if (role is null)
            return Result.Failure(Error.NotFound("Role", command.RoleId));


        role.UpdatePermissions(command.PermissionCodes);

        role.UpdatePermissions(command.PermissionCodes);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
