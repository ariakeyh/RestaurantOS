using Domain.Entities.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Customers;

public class CustomerCreditSpendHistoryConfig : IEntityTypeConfiguration<CustomerCreditSpendHistory>
{
    public void Configure(EntityTypeBuilder<CustomerCreditSpendHistory> builder)
    {
        builder.HasKey(x => x.Id);
    }
}