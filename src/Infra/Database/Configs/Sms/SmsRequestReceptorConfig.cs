using Domain.Entities.Sms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Sms;

public class SmsRequestReceptorConfig : IEntityTypeConfiguration<SmsRequestReceptor>
{
    public void Configure(EntityTypeBuilder<SmsRequestReceptor> builder)
    {
        builder.HasKey(x=>x.Id);
        builder.Property(x => x.ReceptorNumber).IsRequired().HasMaxLength(32);
        builder.Property(x => x.StatusComment).HasMaxLength(64);
        builder.Property(x => x.Message).HasMaxLength(2064);

        builder.HasOne(x => x.SmsRequest).WithMany(x => x.Receptors).HasForeignKey(x => x.SmsRequestId);
    }
}