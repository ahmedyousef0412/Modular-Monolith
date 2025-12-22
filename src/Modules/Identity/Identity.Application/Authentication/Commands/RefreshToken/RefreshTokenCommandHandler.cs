using Identity.Application.Abstractions;
using Identity.Application.Authentication.Dtos;
using Identity.Domain.Exceptions;
using Identity.Domain.Repositories;
using SharedKernel.CQRS;

namespace Identity.Application.Authentication.Commands.RefreshToken;

public class RefreshTokenCommandHandler
    (
        IUserRepository userRepository,
        IIdentityUnitOfWork identityUnitOfWork,
        ITokenProvider tokenProvider,
        IUserClaimsProvider userClaimsProvider,
        IRefreshTokenLifetimeProvider refreshTokenLifetimeProvider
    ) 
    : ICommandHandler<RefreshTokenCommand, AuthResponse>
{

    public async Task<AuthResponse> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByRefreshTokenAsync(command.RefreshToken, cancellationToken)
            ?? throw new InvalidTokenException("Token is invalid or expired");


        var existingToken = user.RefreshTokens.Single(t => t.Token == command.RefreshToken);

        if (existingToken.IsRevoked || existingToken.IsExpired)
        {
            user.RevokeAllRefreshTokens("Attempted reuse of revoked token");

            await identityUnitOfWork.SaveChangesAsync(cancellationToken);

            throw new InvalidTokenException("Token is invalid or expired");
        }


        var claims = await userClaimsProvider.GetClaimsAsync(user, cancellationToken);

        var newAccessToken = tokenProvider.GenerateAccessToken(claims);
        var newRefreshToken = tokenProvider.GenerateRefreshToken();

        user.RotateRefreshToken(
            command.RefreshToken,
            newRefreshToken,
            refreshTokenLifetimeProvider.GetExpiry()
        );

        await identityUnitOfWork.SaveChangesAsync(cancellationToken);

        return new AuthResponse(newAccessToken, newRefreshToken);
    }
}

