using Identity.Application;
using Identity.Infrastructure;

namespace Identity.Api;

public static class IdentityModule
{
    public static IServiceCollection AddIdentityModule(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddIdentityInfrastructure(configuration);
        services.AddIdentityApplication();

        return services;
    }
}