namespace Identity.Application.Authentication.Dtos;

public record AuthResponse
 (
    string AccessToken,
    string RefreshToken,
    DateTime RefreshTokenExpiration,
    bool IsPersistent
 );

