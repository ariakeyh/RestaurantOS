using Domain.Entities.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Customers;

public class ManualCustomerCreditConfig : IEntityTypeConfiguration<ManualCustomerCredit>
{
    public void Configure(EntityTypeBuilder<ManualCustomerCredit> builder)
    {
        builder.Property(x => x.Reason).HasMaxLength(2056);
    }
}