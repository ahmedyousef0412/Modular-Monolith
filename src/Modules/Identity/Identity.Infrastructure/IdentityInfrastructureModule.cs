using BuildingBlocks.Application.Interceptors;
using BuildingBlocks.Application.Security;
using Identity.Application.Abstractions;
using Identity.Application.Authentication;
using Identity.Domain.Abstractions;
using Identity.Infrastructure.Persistence;
using Identity.Infrastructure.Repositories;
using Identity.Infrastructure.Security;
using Identity.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Constants;

namespace Identity.Infrastructure;

public static class IdentityInfrastructureModule
{

    public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services ,IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");


        services.AddSingleton<AuditableEntityInterceptor>();

        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<ITokenProvider, TokenProvider>();
        services.AddSingleton<IRefreshTokenLifetimeProvider, RefreshTokenLifetimeProvider>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();

        services.AddScoped<IIdentityDbContext, IdentityDbContext>();
        services.AddScoped<IIdentityUnitOfWork, IdentityUnitOfWork>();

        services.AddAuthorization(options =>
        {

            var permissions = PermissionsHelper.GetAllPermissions();

            foreach (var permission in permissions)
            {

                options.AddPolicy(permission, policy =>
                policy.RequireClaim("permission", permission));
            }
        });

        services.AddScoped<IPermissionService, PermissionService>();
       
        services.AddOptions<JwtSettings>()
            .BindConfiguration(JwtSettings.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();


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
