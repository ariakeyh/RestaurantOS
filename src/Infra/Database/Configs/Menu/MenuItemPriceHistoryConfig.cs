using Domain.Entities.Menu;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Menu;

public class MenuItemPriceHistoryConfig : IEntityTypeConfiguration<MenuItemPriceHistory>
{
    public void Configure(EntityTypeBuilder<MenuItemPriceHistory> builder)
    {
        builder.HasOne(x=>x.MenuItem).WithMany(x=>x.PriceHistories).HasForeignKey(x=>x.MenuItemId);
    }
}