using Identity.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(r => r.Name).IsUnique();

        //  OWNED permissions
        builder.OwnsMany(r => r.Permissions, p =>
        {
            p.ToTable("RolePermissions");

            p.WithOwner().HasForeignKey("RoleId");

            // Natural key
            p.HasKey("RoleId", "PermissionCode");

            p.Property(x => x.PermissionCode)
             .HasMaxLength(100)
             .IsRequired();
        });
    }
}
