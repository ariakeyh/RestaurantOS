using Domain.Entities.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Customers;

public class CustomerConfig : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(256);
        builder.HasMany(c => c.Orders).WithOne(o => o.Customer).HasForeignKey(o => o.CustomerId).OnDelete(DeleteBehavior.Restrict);
    }
}