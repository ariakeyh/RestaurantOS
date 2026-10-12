using Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Orders;

public class CashierOrderConfig: IEntityTypeConfiguration<CashierOrder>
{
    public void Configure(EntityTypeBuilder<CashierOrder> builder)
    {
        builder.HasOne(x => x.Cashier).WithMany().HasForeignKey(x => x.CashierId);
    }
}