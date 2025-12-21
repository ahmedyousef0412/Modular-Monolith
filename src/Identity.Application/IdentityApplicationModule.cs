using FluentValidation;
using FluentValidation.AspNetCore;
using Identity.Application.Abstractions;
using Identity.Application.Authentication;
using Microsoft.Extensions.DependencyInjection;
namespace Identity.Application;

public static class IdentityApplicationModule
{

    public static IServiceCollection AddIdentityApplication(this IServiceCollection services)
    {


        services.AddScoped<IUserClaimsProvider, UserClaimsProvider>();


        // Register MediatR
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(IdentityApplicationModule).Assembly);
        });
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(typeof(IdentityApplicationModule).Assembly);
        return services;
    }
}
