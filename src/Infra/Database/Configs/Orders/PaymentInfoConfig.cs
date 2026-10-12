using Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Orders;

public class PaymentInfoConfig :  IEntityTypeConfiguration<PaymentInfo>
{
    public void Configure(EntityTypeBuilder<PaymentInfo> builder)
    {
        builder.HasKey(x => x.Id);
        builder.UseTptMappingStrategy();
        builder.HasOne(x=>x.Order)
            .WithMany(x=>x.PaymentInfos)
            .HasForeignKey(x=>x.OrderId);
    }
}