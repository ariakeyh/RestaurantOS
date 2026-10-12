using Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Orders;

public class PosOrderConfig: IEntityTypeConfiguration<PosOrder>
{
    public void Configure(EntityTypeBuilder<PosOrder> builder)
    {
    }
}