using Domain.Entities.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Customers;

public class CashbackCustomerCreditConfig : IEntityTypeConfiguration<OrderCustomerCredit>
{
    public void Configure(EntityTypeBuilder<OrderCustomerCredit> builder)
    {
        builder.HasOne(x=>x.Order)
            .WithMany()
            .HasForeignKey(x=>x.OrderId)
            .IsRequired();

        builder.HasOne(x => x.CashbackSetting)
            .WithMany(x => x.GeneratedCredits)
            .HasForeignKey(x => x.CashbackSettingId)
            .IsRequired();
    }
}