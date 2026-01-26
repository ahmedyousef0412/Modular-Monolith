using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BuildingBlocks.Application.Presentation;
public static class CookieExtensions
{

    public static CookieOptions GetBaseOptions(IWebHostEnvironment env)

    {
        var isDevelopment = env.IsDevelopment();
        return new CookieOptions
        {
            HttpOnly = true,
            Secure = !isDevelopment,
            SameSite = isDevelopment ? SameSiteMode.Lax : SameSiteMode.None,
            IsEssential = true,
        };
        
    }
    

    public static void SetRefreshTokenCookie(this HttpResponse response, string refreshToken, DateTime expires, IWebHostEnvironment env)
    {
        var options = GetBaseOptions(env);
        options.Expires = expires;
        response.Cookies.Append("refreshToken", refreshToken, options);
    }

    public static void DeleteRefreshTokenCookie(this HttpResponse response, IWebHostEnvironment env)
    {
        response.Cookies.Delete("refreshToken", GetBaseOptions(env));
    }
}
