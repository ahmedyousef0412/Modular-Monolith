using BuildingBlocks.Application.CQRS;
using Identity.Application.Abstractions;
using Identity.Domain.Abstractions;
using Identity.Domain.Entity;
using SharedKernel.Domain;

namespace Identity.Application.Roles.Commands.CreateRole;

public class CreateRoleCommandHandler(
    IRoleRepository roleRepository,
    IIdentityUnitOfWork unitOfWork) : ICommandHandler<CreateRoleCommand,Guid>
{
    public async Task<Result<Guid>> Handle(CreateRoleCommand command, CancellationToken cancellationToken)
    {
        var isExist = await roleRepository.IsNameUniqueAsync(command.Name, null,cancellationToken);

        if (!isExist)
            return Result<Guid>.Failure(Error.Conflict("Role name you try to add it already exist."));


       var role =   Role.Create(command.Name, command.Description);

        await roleRepository.AddAsync(role);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(role.Id);
    }
}
