using BuildingBlocks.Application.CQRS;
using Identity.Application.Abstractions;
using Identity.Domain.Abstractions;
using Identity.Domain.Entity;
using SharedKernel.Domain;

namespace Identity.Application.Roles.Commands.AssignRole;

public class AssignRoleCommandHandler(
    IRoleRepository roleRepository,
    IUserRepository userRepository,
    IIdentityUnitOfWork unitOfWork) : ICommandHandler<AssignRoleCommand>
{
    public async Task<Result> Handle(AssignRoleCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(command.UserId, cancellationToken);

        if (user is null)
            return Result.Failure(Error.NotFound(nameof(User), command.UserId));

        var role = await roleRepository.GetByIdAsync(command.RoleId, cancellationToken);

        if (role is null)
            return Result.Failure(Error.NotFound(nameof(Role), command.RoleId));

        var result = user.AssignRole(role);

        if (result.IsFailure)
            return result;

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
