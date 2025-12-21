using Identity.Domain.Entity;
using System.Security.Claims;

namespace Identity.Application.Abstractions;

public interface ITokenProvider
{

    //(string Token,int ExpiresIn) GenerateToken(User user);
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
}
