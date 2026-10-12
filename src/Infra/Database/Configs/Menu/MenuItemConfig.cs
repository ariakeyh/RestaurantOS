using Domain.Entities.Menu;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Menu;

public class MenuItemConfig : IEntityTypeConfiguration<MenuItem>
{
    public void Configure(EntityTypeBuilder<MenuItem> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(64);
        builder.Property(x => x.Description).HasMaxLength(2056);
        builder.Property(x => x.Ingredients).HasMaxLength(2056);
        builder.Property(x => x.ImageName).HasMaxLength(2056);

        builder.HasOne(x => x.Category).WithMany(x => x.Items).HasForeignKey(x => x.CategoryId);
        builder.HasMany(x => x.Orders).WithOne(x => x.MenuItem).HasForeignKey(x => x.MenuItemId);
        builder.HasMany(x=>x.PriceHistories).WithOne(x => x.MenuItem).HasForeignKey(x => x.MenuItemId);
    }
}