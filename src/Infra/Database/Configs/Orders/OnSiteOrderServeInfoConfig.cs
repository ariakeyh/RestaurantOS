using Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Orders;

public class OnSiteOrderServeInfoConfig : IEntityTypeConfiguration<OnSiteOrderServeInfo>
{
    public void Configure(EntityTypeBuilder<OnSiteOrderServeInfo> builder)
    {
        builder.Property(x => x.TableNumber).IsRequired();
    }
}