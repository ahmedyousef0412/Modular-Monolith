using Inventory.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Inventory.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
       builder.ToTable("Products");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(250); //I think description can be a bit longer 

        builder.Property(p => p.Sku)
            .IsRequired()
            .HasMaxLength(50);

       



        builder.HasQueryFilter(p => !p.IsDeleted); // Soft delete

    }
}
