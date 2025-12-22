using Identity.Application.Abstractions;
using Identity.Domain.Repositories;
using Identity.Domain.ValueObjects;
using SharedKernel.CQRS;

namespace Identity.Application.Authentication.Commands.ResetPassword;

public class ResetPasswordCommandHandler
    (
         IUserRepository userRepository,
         IPasswordHasher passwordHasher,
         IIdentityUnitOfWork unitOfWork

    ) : ICommandHandler<ResetPasswordCommand, CommandResult>
{
    public async Task<CommandResult> Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
    {
        var email = Email.Create(command.Email);

        var user = await userRepository.GetByEmailAsync(email, cancellationToken);

        if (user is null)
        {
            return CommandResult.Failure(["User with the specified email does not exist."]);
        }

        if (!user.VerifyResetToken(command.Token))
        {
            return CommandResult.Failure(["Invalid or expired password reset token."]);
        }

        var newPasswordHash = passwordHasher.Hash(command.NewPassword);

        user.ChangePassword(newPasswordHash);
        user.CompletePasswordReset();

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return CommandResult.Success();
    }
}
