
namespace BuildingBlocks.Application.Presentation;
public static class CookieExtensions
{
    public static void SetRefreshTokenCookie(this HttpResponse response, string refreshToken, DateTime expires)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = expires,
            Secure = true,
            SameSite = SameSiteMode.Lax,
            IsEssential = true,
        };

        response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
    }
}
