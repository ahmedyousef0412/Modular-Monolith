using BuildingBlocks.Application.CQRS;
using Identity.Application.Abstractions;
using Identity.Domain.Abstractions;
using SharedKernel.Domain;

namespace Identity.Application.Authentication.Commands.RevokeToken;

public class RevokeTokenCommandHandler(IUserRepository userRepository , IIdentityUnitOfWork identityUnitOfWork) 
    : ICommandHandler<RevokeTokenCommand>
{
    public async Task<Result> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByRefreshTokenAsync(request.RefreshToken, cancellationToken);

        if (user is null)
            return Result.Success();
        

        var token = user.RefreshTokens.SingleOrDefault(rt => rt.Token == request.RefreshToken);

        if(token is not null && token.IsActive)
            token.Revoke("Logged out by user");

        await identityUnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();

    }
}
