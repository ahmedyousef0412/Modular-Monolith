using Identity.Application.Abstractions;
using Identity.Infrastructure.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Identity.Infrastructure.Services;

public class JwtProvider(IOptions<JwtSettings> jwtSettings) : IJwtProvider
{
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;

    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        // Create the Key(Infrastructure responsibility)
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Create the Token
        var token = new JwtSecurityToken
        (

            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
            signingCredentials: creds
        );

        // Write as string
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];

        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        return Convert.ToBase64String(randomNumber);

    }
}
