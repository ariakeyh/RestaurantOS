using Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Orders;

public class OrderConfig: IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);
        builder.UseTptMappingStrategy();

        builder.Property(x => x.OrderVersion).HasDefaultValue(1);
        builder.Property(x=>x.OrderNumber).HasMaxLength(10);
        builder.HasIndex(x=>x.OrderNumber).IsUnique();
        builder.HasMany(x=> x.Items).WithOne(x=>x.Order).HasForeignKey(x=>x.OrderId);
        builder.HasMany(x=> x.StatusHistories).WithOne(x=>x.Order).HasForeignKey(x=>x.OrderId);
        builder.HasOne(x=> x.Shop).WithMany(x=>x.Orders).HasForeignKey(x=>x.ShopId);
    }
}