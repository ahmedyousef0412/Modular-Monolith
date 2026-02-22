using BuildingBlocks.Application.CQRS;
using Identity.Application.Abstractions;
using Identity.Domain.Abstractions;
using Identity.Domain.ValueObjects;
using SharedKernel.Domain;

namespace Identity.Application.Authentication.Commands.ResetPassword;

public class ResetPasswordCommandHandler
    (
         IUserRepository userRepository,
         IPasswordHasher passwordHasher,
         IIdentityUnitOfWork unitOfWork

    ) : ICommandHandler<ResetPasswordCommand>
{
    public async Task<Result> Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
    {
        var email = Email.Create(command.Email);

        var user = await userRepository.GetByEmailAsync(email, cancellationToken);

        if (user is null)
        {
            return Result.Failure(UserErrors.InvalidToken);
        }

        if (!user.VerifyResetToken(command.Token))
        {
            return Result.Failure(UserErrors.InvalidToken);
        }

        if(command.NewPassword != command.ConfirmNewPassword)
        {
            return Result.Failure(UserErrors.PasswordsDoNotMatch);
        }

        var newPasswordHash = passwordHasher.Hash(command.NewPassword);

        user.ChangePassword(newPasswordHash);
        user.CompletePasswordReset();

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
