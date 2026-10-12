using Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Orders;

public class PosPaymentInfoConfig :  IEntityTypeConfiguration<PosPaymentInfo>
{
    public void Configure(EntityTypeBuilder<PosPaymentInfo> builder)
    {
        builder.Property(x=>x.MaskedCardNo).HasMaxLength(32).IsRequired();
        builder.Property(x=>x.AcceptorCode).HasMaxLength(64).IsRequired();
        builder.Property(x=>x.TraceNo).HasMaxLength(64).IsRequired();
        builder.Property(x=>x.TerminalNo).HasMaxLength(64).IsRequired();
    }
}