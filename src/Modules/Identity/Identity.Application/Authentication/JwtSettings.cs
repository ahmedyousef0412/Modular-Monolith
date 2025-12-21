using System.ComponentModel.DataAnnotations;

namespace Identity.Application.Authentication;

public class JwtSettings
{
    public const string SectionName = "JwtSettings";

    [Required]
    public string Key { get; init; } = string.Empty;

    [Required]
    public string Issuer { get; init; } = string.Empty;

    [Required]
    public string Audience { get; init; } = string.Empty;

    [Range(1,60)]
    public int DurationInMinutes { get; init; }

    [Range(1, 30)]
    public int RefreshTokenExpirationInDays { get; init; }
}
