using Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Orders;

public class TakeAwayOrderServeInfoConfig : IEntityTypeConfiguration<TakeAwayOrderServeInfo>
{
    public void Configure(EntityTypeBuilder<TakeAwayOrderServeInfo> builder)
    {
    }
}