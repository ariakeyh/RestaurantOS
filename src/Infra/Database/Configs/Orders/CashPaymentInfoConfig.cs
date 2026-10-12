using Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Orders;

public class CashPaymentInfoConfig : IEntityTypeConfiguration<CashPaymentInfo>
{
    public void Configure(EntityTypeBuilder<CashPaymentInfo> builder)
    {
        builder.Property(x => x.Reason).HasMaxLength(1024);
    }
}