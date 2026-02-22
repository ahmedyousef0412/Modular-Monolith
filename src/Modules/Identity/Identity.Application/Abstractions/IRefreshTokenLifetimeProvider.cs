namespace Identity.Application.Abstractions;

public interface IRefreshTokenLifetimeProvider
{
    DateTime GetExpiry(bool rememberMe = false);
}
