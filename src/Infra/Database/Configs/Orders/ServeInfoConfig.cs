using Domain.Entities.Orders;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Orders;

public class ServeInfoConfig : IEntityTypeConfiguration<OrderServeInfo>
{
    public void Configure(EntityTypeBuilder<OrderServeInfo> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasDiscriminator<ServeInfoType>(nameof(OrderServeInfo.Type))
                    .HasValue<DeliveryOrderServeInfo>(ServeInfoType.Delivery)
                    .HasValue<OnSiteOrderServeInfo>(ServeInfoType.OnSite)
                    .HasValue<TakeAwayOrderServeInfo>(ServeInfoType.TakeAway);
                
        builder.HasOne(x => x.Order)
            .WithOne(x => x.ServeInfo)
            .HasForeignKey<OrderServeInfo>(x => x.Id);
    }
}