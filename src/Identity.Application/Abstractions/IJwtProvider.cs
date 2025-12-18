using System.Security.Claims;

namespace Identity.Application.Abstractions;

public interface IJwtProvider
{

    //(string Token,int ExpiresIn) GenerateToken(User user);
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
}
