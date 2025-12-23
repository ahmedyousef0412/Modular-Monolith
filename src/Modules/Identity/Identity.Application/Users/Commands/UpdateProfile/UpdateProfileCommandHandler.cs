using Identity.Domain.Repositories;
using SharedKernel.Abstractions;
using SharedKernel.CQRS;
using SharedKernel.Exceptions;

namespace Identity.Application.Users.Commands.UpdateProfile;

public class UpdateProfileCommandHandler
    (
        IUserRepository userRepository,
        IIdentityUnitOfWork unitOfWork,
        ICurrentUserService currentUser
    ) : ICommandHandler<UpdateProfileCommand, CommandResult>
{
    public async Task<CommandResult> Handle(UpdateProfileCommand command, CancellationToken cancellationToken)
    {
        // 1. Security: Get ID from Token (Not from the user input)
        var userId = currentUser.UserId;

       
        var user = await userRepository.GetByIdAsync(userId, cancellationToken)
          ?? throw new NotFoundException("User not found");

        user.UpdateProfile(command.FirstName, command.LastName);

        
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return CommandResult.Success();
    }
}
