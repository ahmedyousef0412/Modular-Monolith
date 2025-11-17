using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sales.Domain.Repository;
using Sales.Infrastructure.Persistence;
using Sales.Infrastructure.Repositories;
using SharedKernel.Entities;

namespace Sales.Infrastructure;

public static class SalesInfrastructureModule 
{
    public static IServiceCollection AddSalesInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SalesDbContext>(options =>
           options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
