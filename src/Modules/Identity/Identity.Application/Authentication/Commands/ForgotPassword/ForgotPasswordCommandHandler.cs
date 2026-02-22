using BuildingBlocks.Application.CQRS;
using Identity.Application.Abstractions;
using Identity.Domain.Abstractions;
using Identity.Domain.ValueObjects;
using SharedKernel.Domain;
using System.Security.Cryptography;

namespace Identity.Application.Authentication.Commands.ForgotPassword;

public class ForgotPasswordCommandHandler
    (
      IUserRepository userRepository,
      IIdentityUnitOfWork unitOfWork,
      IPasswordHasher passwordHasher

    ) : ICommandHandler<ForgotPasswordCommand>
{
    public async Task<Result> Handle(ForgotPasswordCommand command, CancellationToken cancellationToken)
    {
        var email = Email.Create(command.Email);

        var user = await userRepository.GetByEmailAsync(email, cancellationToken);

        if (user is null)
            return Result.Success();
        
        if (user.PasswordResetRequestedRecently())
            return Result.Success();


        var rawToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        var hashedToken = passwordHasher.Hash(rawToken);
        var expiresOn = DateTime.UtcNow.AddMinutes(30);
       

        user.RequestPasswordReset(hashedToken, expiresOn);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        //I will send the email with the reset token here using an email service

        return Result.Success();
    }
}
