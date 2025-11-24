using Inventory.Application.Services;
using Microsoft.Extensions.DependencyInjection;
namespace Inventory.Application;

public static class InventoryApplicationModule
{

    public static IServiceCollection AddInventoryApplication(this IServiceCollection services)
    {

        services.AddTransient<IInventoryMappingService, InventoryMappingService>();

        // Register MediatR
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(InventoryApplicationModule).Assembly);
        });

        return services;
    }
}