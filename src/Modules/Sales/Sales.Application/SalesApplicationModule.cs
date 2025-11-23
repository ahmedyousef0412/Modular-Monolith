using Microsoft.Extensions.DependencyInjection;
using Sales.Application.Services;
using System.Reflection;

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

        
    
        return services;
    }
}
