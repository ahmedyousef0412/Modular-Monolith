using Inventory.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Persistence.Configurations;

public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        builder.ToTable(nameof(Warehouse),"inventory");

        builder.HasKey(w => w.Id);

        builder.Property(w => w.Name)
               .HasMaxLength(100)
               .IsRequired();

        builder.HasIndex(w => w.Name) //Unique Name to prevent duplicates
               .IsUnique();


        builder.Property(w => w.Location)
               .HasMaxLength(200)
               .IsRequired();
    }
}
