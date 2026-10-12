using Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Orders;

public class OrderItemConfig: IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(x=> x.Order).WithMany(x=>x.Items).HasForeignKey(x=>x.OrderId);
        builder.HasOne(x=> x.MenuItem).WithMany(x=>x.Orders).HasForeignKey(x=>x.MenuItemId);
    }
}