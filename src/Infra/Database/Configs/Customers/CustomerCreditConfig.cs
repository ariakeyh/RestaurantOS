using Domain.Entities.Customers;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Customers;

public class CustomerCreditConfig : IEntityTypeConfiguration<CustomerCredit>
{
    public void Configure(EntityTypeBuilder<CustomerCredit> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasDiscriminator<CustomerCreditType>(nameof(CustomerCredit.Type))
            .HasValue<OrderCustomerCredit>(CustomerCreditType.Cashback)
            .HasValue<ManualCustomerCredit>(CustomerCreditType.Manual);
        
        builder.HasOne(x => x.Customer)
            .WithMany(x=>x.Credits)
            .HasForeignKey(x => x.CustomerId);

        builder.HasMany(x => x.SpendHistories)
            .WithOne(x => x.Credit)
            .HasForeignKey(x => x.CustomerCreditId);
    }
}