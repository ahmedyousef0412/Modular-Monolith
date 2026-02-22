using BuildingBlocks.Application.CQRS;
using Identity.Application.Abstractions;
using Identity.Application.Authentication.Dtos;
using Identity.Domain.Abstractions;
using Identity.Domain.Exceptions;
using Identity.Domain.ValueObjects;
using SharedKernel.Domain;

namespace Identity.Application.Authentication.Commands.Login;

public class LoginCommandHandler(
    IUserRepository userRepository,
    IIdentityUnitOfWork identityUnitOfWork,
    IPasswordHasher passwordHasher,
    IUserClaimsProvider claimsProvider,
    ITokenProvider tokenProvider,
    IRefreshTokenLifetimeProvider refreshTokenLifetimeProvider)
    : ICommandHandler<LoginCommand, AuthResponse>
{


    public async Task<Result<AuthResponse>> Handle(LoginCommand command, CancellationToken cancellationToken)
    {

        var email = Email.Create(command.Email);

        var user = await userRepository.GetByEmailAsync(email, cancellationToken)
            ?? throw new InvalidCredentialsException();


        var isPasswordValid = passwordHasher.Verify(command.Password, user.PasswordHash);


        if (!isPasswordValid)
            throw new InvalidCredentialsException();


        if (!user.IsActive)
            throw new AccountLockedException(command.Email);

        var claims = await claimsProvider.GetClaimsAsync(user, cancellationToken);

        var token = tokenProvider.GenerateAccessToken(claims);
        var refreshToken = tokenProvider.GenerateRefreshToken();

        var expiry = refreshTokenLifetimeProvider.GetExpiry(command.RememberMe);

        user.AddSession(refreshToken, expiry);

        await identityUnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success( new AuthResponse(token, refreshToken, expiry,command.RememberMe));


    }

    
}
