using BuildingBlocks.Application.CQRS;
using Identity.Application.Abstractions;
using Identity.Domain.Abstractions;
using Identity.Domain.Entity;
using SharedKernel.Domain;

namespace Identity.Application.Roles.Commands.DeleteRole;

public class DeleteRoleCommandHandler(
    IRoleRepository roleRepository ,
    IIdentityUnitOfWork unitOfWork
    )
    : ICommandHandler<DeleteRoleCommand>
{
    public async Task<Result> Handle(DeleteRoleCommand command, CancellationToken cancellationToken)
    {
        var role = await roleRepository.GetByIdAsync(command.Id,cancellationToken);

        if (role is null)
            return Result.Failure(Error.NotFound(nameof(Role), command.Id));

        role.SoftDelete();

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    
}
