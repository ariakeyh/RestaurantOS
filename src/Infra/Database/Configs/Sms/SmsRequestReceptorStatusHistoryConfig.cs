using Domain.Entities.Sms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Sms;

public class SmsRequestReceptorStatusHistoryConfig : IEntityTypeConfiguration<SmsRequestReceptorStatusHistory>
{
    public void Configure(EntityTypeBuilder<SmsRequestReceptorStatusHistory> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x=>x.PrevStatusComment).HasMaxLength(64);  

        builder.HasOne(x => x.SmsRequestReceptor).WithMany(x => x.StatusHistories)
            .HasForeignKey(x => x.SmsRequestReceptorId);
    }
}