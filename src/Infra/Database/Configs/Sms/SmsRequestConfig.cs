using Domain.Entities.Sms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Sms;

public class SmsRequestConfig : IEntityTypeConfiguration<SmsRequest>
{
    public void Configure(EntityTypeBuilder<SmsRequest> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Payload).HasMaxLength(2064);

        builder.HasOne(x => x.Shop).WithMany(x => x.SmsRequests).HasForeignKey(x => x.ShopId);
        builder.HasOne(x => x.CreditHistory).WithOne(x => x.SmsRequest).HasForeignKey<SmsRequest>(x => x.CreditHistoryId);
    }
}