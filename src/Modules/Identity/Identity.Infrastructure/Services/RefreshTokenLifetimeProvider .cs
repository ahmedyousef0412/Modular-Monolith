using Identity.Application.Abstractions;
using Identity.Application.Authentication;
using Microsoft.Extensions.Options;

namespace Identity.Infrastructure.Services;

public class RefreshTokenLifetimeProvider : IRefreshTokenLifetimeProvider
{

    private readonly JwtSettings _settings;

    public RefreshTokenLifetimeProvider(IOptions<JwtSettings> options)
    {
        _settings = options.Value;
    }
    public DateTime GetExpiry(bool rememberMe = false)
    {
        var days = rememberMe ? _settings.RefreshTokenExpirationInDays : 1;
        return DateTime.UtcNow.AddDays(days);
    }
}
