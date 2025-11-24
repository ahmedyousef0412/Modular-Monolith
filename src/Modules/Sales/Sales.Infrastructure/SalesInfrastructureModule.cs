using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sales.Application.Ports;
using Sales.Domain.Repository;
using Sales.Infrastructure.Gateways;
using Sales.Infrastructure.Persistence;
using Sales.Infrastructure.Repositories;
using SharedKernel.Entities;

namespace Sales.Infrastructure;

public static class SalesInfrastructureModule 
{
    public static IServiceCollection AddSalesInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SalesDbContext>(options =>
           options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
           sql => sql.MigrationsHistoryTable("__EFMigrationsHistory", "sales")));


        services.AddScoped<IInventoryGateway, InventoryGateway>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ISalesUnitOfWork, SalesUnitOfWork>();

        return services;
    }
}
