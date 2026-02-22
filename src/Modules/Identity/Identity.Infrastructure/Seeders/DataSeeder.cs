using Identity.Application.Abstractions;
using Identity.Domain.Constants;
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

    
        if (!await context.Roles.AnyAsync(r => r.Name == RoleConstants.Admin))
        {
            var admin = Role.Create("Admin", "Administrator role with full permissions");
            context.Roles.Add(admin);
        }


        if (!await context.Roles.AnyAsync(r => r.Name == RoleConstants.User))
        {
            var user = Role.Create("User", "Standard user role with limited permissions");
            context.Roles.Add(user);
        }

       
        await context.SaveChangesAsync();

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

        bool hasNewPermissions = false;

        foreach (var permission in allPermissions)
        {
            if (!existingPermissions.Contains(permission))
            {
                adminRole.AddPermission(permission);
                hasNewPermissions = true;
            }
                
        }

        if (hasNewPermissions)
        {
            await context.SaveChangesAsync();
        }


        #endregion

        #region Default Admin User

        if (!await context.Users.AnyAsync())
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
