using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Sales.Application.Services;

namespace Sales.Application;

public static class SalesApplicationModule
{

    public static IServiceCollection AddSalesApplication(this IServiceCollection services)
    {


        services.AddTransient<IOrderMappingService, OrderMappingService>();


        // Register MediatR

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(SalesApplicationModule).Assembly);
        });

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(typeof(SalesApplicationModule).Assembly);

        return services;
    }
}
