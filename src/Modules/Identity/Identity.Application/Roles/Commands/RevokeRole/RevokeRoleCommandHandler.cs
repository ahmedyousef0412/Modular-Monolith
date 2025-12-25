using BuildingBlocks.Application.CQRS;
using Identity.Application.Abstractions;
using Identity.Domain.Abstractions;
using Identity.Domain.Entity;
using SharedKernel.Domain;

namespace Identity.Application.Roles.Commands.RevokeRole;

public class RevokeRoleCommandHandler
    (
       IUserRepository userRepository,
       IIdentityUnitOfWork unitOfWork
    ): ICommandHandler<RevokeRoleCommand>
{
    public async Task<Result> Handle(RevokeRoleCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(command.UserId, cancellationToken);

        if (user is null)
            return Result.Failure(Error.NotFound(nameof(User), command.UserId));

        var result = user.RemoveRole(command.RoleId);

        if(result.IsFailure)
            return result;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(result);
    }
}
