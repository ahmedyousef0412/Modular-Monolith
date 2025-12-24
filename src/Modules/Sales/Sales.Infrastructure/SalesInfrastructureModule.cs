using BuildingBlocks.Application.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sales.Application.Abstractions;
using Sales.Application.Ports;
using Sales.Infrastructure.Gateways;
using Sales.Infrastructure.Persistence;
using Sales.Infrastructure.Repositories;

namespace Sales.Infrastructure;

public static class SalesInfrastructureModule 
{
    public static IServiceCollection AddSalesInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        
        services.AddSingleton<AuditableEntityInterceptor>();

        services.AddScoped<IInventoryGateway, InventoryGateway>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ISalesUnitOfWork, SalesUnitOfWork>();


        services.AddDbContext<SalesDbContext>((sp, options) =>
        {
        
            var interceptor = sp.GetRequiredService<AuditableEntityInterceptor>();

            options.UseSqlServer(connectionString, sql =>
            {
              
                sql.MigrationsHistoryTable("__EFMigrationsHistory", "sales");
            })
                .AddInterceptors(interceptor);
        });

        return services;
    }
}
