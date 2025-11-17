using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Domain.Entity;

namespace Sales.Infrastructure.Persistence.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
       builder.ToTable("OrderItems");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.ProductName)
               .IsRequired()
               .HasMaxLength(250);

        builder.Property(i => i.Quantity)
               .IsRequired();

        builder.Property(i => i.UnitPrice)
               .HasColumnType("decimal(18,2)")
               .IsRequired();
    }
}
