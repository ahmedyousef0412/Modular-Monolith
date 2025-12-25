using Identity.Application.Abstractions;
using Identity.Domain.Entity;
using Identity.Domain.ValueObjects;
using Identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Constants;
using SharedKernel.Exceptions;


namespace Identity.Infrastructure.Seeders;

public class DataSeeder
{

    public static async Task SeedAsync(IdentityDbContext context, IPasswordHasher passwordHasher)
    {

        #region Roles

        if (!context.Roles.Any())
        {
            var admin = Role.Create("Admin", "Administrator role with full permissions");
            var user = Role.Create("User", "Standard user role with limited permissions");

            context.Roles.AddRange(admin, user);
            await context.SaveChangesAsync();

        }

        #endregion

        #region Permissions for Admin Role

        var adminRole = await context.Roles
                             .Include(r => r.Permissions)
                             .FirstOrDefaultAsync(r => r.Name == "Admin") 
                         ?? throw new DomainException("Admin role not found.");


        var allPermissions = PermissionsHelper.GetAllPermissions();


        var existingPermissions = adminRole.Permissions
                        .Select(p => p.PermissionCode)
                        .ToHashSet();

        foreach (var permission in allPermissions)
        {
            if(!existingPermissions.Contains(permission))
                adminRole.AddPermission(permission);
        }

        await context.SaveChangesAsync();


        #endregion

        #region Default Admin User

        if (!context.Users.Any())
        {
            var passwordHash = passwordHasher.Hash("Admin123");

            var adminUser = User.Create(

                 "Ahmed",
                 "Yousef",
                 Email.Create("admin@modularmonolith.com"),
                 passwordHash
            );

            adminUser.AssignRole(adminRole);

            context.Users.Add(adminUser);

            await context.SaveChangesAsync();
        }

        #endregion
    }
}
