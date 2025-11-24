using Inventory.Application.Repository;
using Inventory.Domain.Repositories;
using Inventory.Infrastructure.Persistence;
using Inventory.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Inventory.Infrastructure;

public static class InventoryInfrastrucureModule
{

    public static IServiceCollection AddInventoryInfrastructure(this IServiceCollection services , IConfiguration configuration)
    {
        services.AddDbContext<InventoryDbContext>(options =>
           options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
           sql => sql.MigrationsHistoryTable("__EFMigrationsHistory", "inventory")));

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IStockRepository, StockRepository>();


        services.AddScoped<IInventoryUnitOfWork, InventoryUnitOfWork>();

        return services;
    }
}
