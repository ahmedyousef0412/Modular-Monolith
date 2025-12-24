using BuildingBlocks.Application.CQRS;
using Identity.Application.Abstractions;
using Identity.Domain.Abstractions;
using Identity.Domain.ValueObjects;

namespace Identity.Application.Authentication.Commands.ForgotPassword;

public class ForgotPasswordCommandHandler
    (
      IUserRepository userRepository,
      IIdentityUnitOfWork unitOfWork

    ) : ICommandHandler<ForgotPasswordCommand, CommandResult>
{
    public async Task<CommandResult> Handle(ForgotPasswordCommand command, CancellationToken cancellationToken)
    {
        var email = Email.Create(command.Email);

        var user = await userRepository.GetByEmailAsync(email, cancellationToken);

        if (user is null)
        {
            return CommandResult.Success();
        }

        var resetToken = Guid.NewGuid().ToString();
        var expiresOn = DateTime.UtcNow.AddMinutes(30);

        user.RequestPasswordReset(resetToken, expiresOn);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        //I will send the email with the reset token here using an email service

        return CommandResult.Success();
    }
}
