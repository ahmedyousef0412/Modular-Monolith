using Identity.Application.Abstractions;
using Identity.Application.Authentication.Dtos;
using Identity.Domain.Exceptions;
using Identity.Domain.Repositories;
using Identity.Domain.ValueObjects;
using SharedKernel.CQRS;

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


    public async Task<AuthResponse> Handle(LoginCommand command, CancellationToken cancellationToken)
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



        user.AddSession(refreshToken, refreshTokenLifetimeProvider.GetExpiry());

        await identityUnitOfWork.SaveChangesAsync(cancellationToken);

        return new AuthResponse(token, refreshToken);


    }
}
