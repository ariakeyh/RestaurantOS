using Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Orders;

public class PGPaymentInfoConfig :  IEntityTypeConfiguration<PGPaymentInfo>
{
    public void Configure(EntityTypeBuilder<PGPaymentInfo> builder)
    {
        builder.Property(x=>x.ReferenceNo).HasMaxLength(64).IsRequired();
        builder.Property(x=>x.MaskedCardNo).HasMaxLength(32).IsRequired();
    }
}