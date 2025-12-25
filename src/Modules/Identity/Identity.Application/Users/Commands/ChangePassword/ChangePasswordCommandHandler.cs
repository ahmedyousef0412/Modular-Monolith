using BuildingBlocks.Application.Abstractions;
using BuildingBlocks.Application.CQRS;
using Identity.Application.Abstractions;
using Identity.Domain.Abstractions;
using Identity.Domain.Exceptions;
using SharedKernel.Domain;
using SharedKernel.Exceptions;

namespace Identity.Application.Users.Commands.ChangePassword;

public class ChangePasswordCommandHandler
    (
      IUserRepository userRepository,
      IPasswordHasher passwordHasher,
      ICurrentUserService currentUser,
      IIdentityUnitOfWork unitOfWork

    ) : ICommandHandler<ChangePasswordCommand>
{
    public async Task<Result> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
    {
        if (!currentUser.IsAuthenticated)
            throw new UnauthorizedException();

        var user = await userRepository.GetByIdAsync(currentUser.UserId, cancellationToken)
            ?? throw new NotFoundException("User not found");


        if (!passwordHasher.Verify(command.CurrentPassword, user.PasswordHash))
        {
            return Result.Failure(UserErrors.InvalidCurrentPassword);
        }

        if (passwordHasher.Verify(command.NewPassword, user.PasswordHash))
        {
            return Result.Failure(UserErrors.NewPasswordSameAsOld);
        }

        var newPasswordHash = passwordHasher.Hash(command.NewPassword);

        user.ChangePassword(newPasswordHash);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
