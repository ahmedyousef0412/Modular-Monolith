
using FluentValidation;
using FluentValidation.AspNetCore;
using Sales.Application;
using Sales.Infrastructure;
namespace Sales.Api;

public static class SalesModules
{
    public static IServiceCollection AddSalesModules(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSalesInfrastructure(configuration);
        services.AddSalesApplication();
      
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(typeof(ISalesApplicationMarker).Assembly);



        return services;
    }
}
