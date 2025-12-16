using Inventory.Application;
using Inventory.Infrastructure;

namespace Inventory.Api;

public static class InventoryModules
{
    public static IServiceCollection AddInventoryModules(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInventoryInfrastructure(configuration);
        services.AddInventoryApplication();
    
        return services;
    }
}
