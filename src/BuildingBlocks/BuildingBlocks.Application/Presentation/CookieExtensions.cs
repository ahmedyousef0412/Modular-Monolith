using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BuildingBlocks.Application.Presentation;
public static class CookieExtensions
{

    public static CookieOptions GetBaseOptions(IWebHostEnvironment env)
    {
        
        return new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite =  SameSiteMode.None,
            IsEssential = true,
            Expires = DateTime.UtcNow.AddDays(14),
            Path = "/",
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
