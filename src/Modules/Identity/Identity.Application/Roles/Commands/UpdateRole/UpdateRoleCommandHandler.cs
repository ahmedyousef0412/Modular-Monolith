using BuildingBlocks.Application.CQRS;
using Identity.Application.Abstractions;
using Identity.Domain.Abstractions;
using Identity.Domain.Entity;
using SharedKernel.Domain;

namespace Identity.Application.Roles.Commands.UpdateRole;

public class UpdateRoleCommandHandler
(
    IRoleRepository roleRepository ,
    IIdentityUnitOfWork unitOfWork

)
    : ICommandHandler<UpdateRoleCommand>
{
    public async Task<Result> Handle(UpdateRoleCommand command, CancellationToken cancellationToken)
    {
        var role = await roleRepository.GetByIdAsync(command.RoleId, cancellationToken);

        if (role is null)
            return Result.Failure(Error.NotFound(nameof(Role), command.RoleId));

        if (!await roleRepository.IsNameUniqueAsync(command.Name, command.RoleId, cancellationToken))
        {
            return Result.Failure(Error.Conflict($"The role name '{command.Name}' is already taken."));
        }

        role.Update(command.Name,command.Description);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
