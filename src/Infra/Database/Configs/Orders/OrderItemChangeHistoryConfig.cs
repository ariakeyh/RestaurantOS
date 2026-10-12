using Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Orders;

public class OrderItemChangeHistoryConfig : IEntityTypeConfiguration<OrderItemChangeHistory>
{
    public void Configure(EntityTypeBuilder<OrderItemChangeHistory> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.OrderVersion).HasDefaultValue(1);
        
        builder.HasOne(x=>x.Order)
            .WithMany(x=>x.ChangeHistories)
            .HasForeignKey(x=>x.OrderId);
        
        builder.HasOne(x=>x.MenuItem)
            .WithMany(x=>x.ChangeHistories)
            .HasForeignKey(x=>x.MenuItemId);
    }
}