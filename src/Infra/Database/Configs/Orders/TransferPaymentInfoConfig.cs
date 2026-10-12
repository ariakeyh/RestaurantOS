using Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Orders;

public class TransferPaymentInfoConfig :  IEntityTypeConfiguration<TransferPaymentInfo>
{
    public void Configure(EntityTypeBuilder<TransferPaymentInfo> builder)
    {
        builder.Property(x => x.ReferenceNo).HasMaxLength(64);
        builder.Property(x => x.DestinationCardNo).HasMaxLength(32);
        builder.Property(x => x.Reason).HasMaxLength(1024);
    }
}