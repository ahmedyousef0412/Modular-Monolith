using Identity.Application.Abstractions;
using Identity.Domain.Exceptions;
using Identity.Domain.Repositories;
using SharedKernel.Abstractions;
using SharedKernel.CQRS;
using SharedKernel.Exceptions;

namespace Identity.Application.Users.Commands.ChangePassword;

public class ChangePasswordCommandHandler
    (
      IUserRepository userRepository,
      IPasswordHasher passwordHasher,
      ICurrentUserService currentUser,
      IIdentityUnitOfWork unitOfWork

    ) : ICommandHandler<ChangePasswordCommand, CommandResult>
{
    public async Task<CommandResult> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
    {
        if (!currentUser.IsAuthenticated)
            throw new UnauthorizedException();

        var user = await userRepository.GetByIdAsync(currentUser.UserId, cancellationToken)
            ?? throw new NotFoundException("User not found");


        if (!passwordHasher.Verify(command.CurrentPassword, user.PasswordHash))
        {
            return CommandResult.Failure(["Current password is incorrect."]);
        }

        if (passwordHasher.Verify(command.NewPassword, user.PasswordHash))
        {
            return CommandResult.Failure(["New password cannot be the same as the old password."]);
        }

        var newPasswordHash = passwordHasher.Hash(command.NewPassword);

        user.ChangePassword(newPasswordHash);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return CommandResult.Success();
    }
}
