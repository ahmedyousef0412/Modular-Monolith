using Inventory.Application;
using Inventory.Infrastructure;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace Inventory.Api;

public static class InventoryModules
{
    public static IServiceCollection AddInventoryModules(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInventoryInfrastructure(configuration);
        services.AddInventoryApplication();
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(typeof(IInventoryApplicationMarker).Assembly);
        return services;
    }
}
