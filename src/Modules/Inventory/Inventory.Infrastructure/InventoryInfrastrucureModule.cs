using Inventory.Application.Repository;
using Inventory.Domain.Repositories;
using Inventory.Infrastructure.Persistence;
using Inventory.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Interceptors;


namespace Inventory.Infrastructure;

public static class InventoryInfrastrucureModule
{

    public static IServiceCollection AddInventoryInfrastructure(this IServiceCollection services , IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
       
        
        services.AddSingleton<AuditableEntityInterceptor>();

        

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IStockRepository, StockRepository>();
        services.AddScoped<IStockReadRepository, StockReadRepository>();
        services.AddScoped<IWarehouseRepository, WarehouseRepository>();
        services.AddScoped<IWarehouseReadRepository, WarehouseReadRepository>();


        services.AddScoped<IInventoryUnitOfWork, InventoryUnitOfWork>();


        services.AddDbContext<InventoryDbContext>((sp, options) =>
        {
            
            var interceptor = sp.GetRequiredService<AuditableEntityInterceptor>();

            
            options.UseSqlServer(connectionString, sql =>
            {
               
                sql.MigrationsHistoryTable("__EFMigrationsHistory", "inventory");
            })
            .AddInterceptors(interceptor);
        });

        return services;
    }
}
