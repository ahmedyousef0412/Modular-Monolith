using Identity.Application.Abstractions;
using Identity.Application.Authentication;
using Microsoft.Extensions.Options;

namespace Identity.Infrastructure.Repositories;

public class RefreshTokenLifetimeProvider : IRefreshTokenLifetimeProvider
{

    private readonly JwtSettings _settings;

    public RefreshTokenLifetimeProvider(IOptions<JwtSettings> options)
    {
        _settings = options.Value;
    }
    public DateTime GetExpiry()
    {
        return DateTime.UtcNow.AddDays(_settings.RefreshTokenExpirationInDays);
    }
}
