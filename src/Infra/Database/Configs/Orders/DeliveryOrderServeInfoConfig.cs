using Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Orders;

public class DeliveryOrderServeInfoConfig : IEntityTypeConfiguration<DeliveryOrderServeInfo>
{
    public void Configure(EntityTypeBuilder<DeliveryOrderServeInfo> builder)
    {
        builder.Property(x => x.Address).HasMaxLength(2048).IsRequired();
    }
}