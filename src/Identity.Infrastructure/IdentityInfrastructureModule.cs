using Identity.Domain.Repositories;
using Identity.Domain.Security;
using Identity.Infrastructure.Persistence;
using Identity.Infrastructure.Repositories;
using Identity.Infrastructure.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure;

public static class IdentityInfrastructureModule
{

    public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services ,IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");


        services.AddSingleton<AuditableEntityInterceptor>();

        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IIdentityUnitOfWork, IdentityUnitOfWork>();




        services.AddDbContext<IdentityDbContext>((sp, options) =>
        {

            var interceptor = sp.GetRequiredService<AuditableEntityInterceptor>();

            options.UseSqlServer(connectionString, sql =>
            {

                sql.MigrationsHistoryTable("__EFMigrationsHistory", "identity");
            })
                .AddInterceptors(interceptor);
        });



        return services;
    }
}
